using System;
using System.Collections.Generic;
using System.Web;
using Telerik.Sitefinity.Services;
using System.Text.RegularExpressions;
using System.IO;


/*
 * Unit to store all of the properties required to submit and execute a search
 * Values may come from URL Query string or from defaults set here
 * 
 */
namespace SoftwareDesign.Search.Models
{
    class QueryModel
    {

        const string BOOSTEDHITS_FILE = "boostedhits.json";
        const string queryParameter = "searchQuery";
        const string indexParameter = "indexCatalogue";
        const string refinerParameter = "r"; // just mapping from the request parameters - this defines the refinement
        
        public const string AllIndexes = "website,taxsource,chariot,accountancyireland,carb,products";  // if none is available, search these ones
        private string defaultIndexName = AllIndexes;
 
        string[] defaultIndexes = { "website", "taxsource", "chariot", "accountancyireland","carb","products" };
        private const int DefaultPageSize = 10;
        private const int DefaultPageNo = 1;

        private int pageNumber = -1;
        private int pageSize = -1;

        // Map to store readable version of index names
        private static Dictionary<string, string> indexNames = new Dictionary<string, string>();

        // Map to store query terms against doc IDs to boost
        private static Dictionary<string, string> boostedHits = new Dictionary<string, string>();

        

        // Constructor - set the indexName map : displayable labels for each index
        // The set of index names is coupled to the scripts to index into elasticsearch so the names are fixed
        // hence we create a set of displayable labels for each index
        // likewise the refiners are set from a field value in data pushed to elasticsearch
        // We want to choose the labels to show for each facet without having to re-index
        // Hence the map from refiner value (from the indexed field) and refiner label
        public QueryModel()
        {
            if (indexNames.Count == 0)
            {
                indexNames.Add("website", "charteredaccountants.ie");
                indexNames.Add("taxsource", "TaxSource");
                indexNames.Add("chariot", "CHARIOT");
                indexNames.Add("accountancyireland", "accountancyireland.ie");
                indexNames.Add("carb", "CARB");
                indexNames.Add("products","Shop");
                indexNames.Add(AllIndexes, "all content");
            }
        }


        /// <summary>
        /// The query text - should come from query but default = *
        /// </summary>
        private string queryText = null;

        /// <summary>
        /// The refiners - name-value pairs - field name and field value - default = null
        /// </summary>
        private RefinerItem refiner = null;

        /// <summary>
        /// The index name that we want to search over - only 1 allowed - from querystring default = website
        /// </summary>
        private string indexName = null;

        /// <summary>
        /// The alternate indexes - other available elastic indexes 
        /// We want to exclude from the main search but include in a cat-count search
        /// </summary>
        private string[] alternateIndexes = null;
        

        public void setPageNumber(int _pageNumber){
            pageNumber = _pageNumber;
        }

        public int getPageNumber(){

            if (String.IsNullOrEmpty(GetParamFromURLQueryString("page"))){
                pageNumber = DefaultPageNo;
            } else {
                pageNumber = Int32.Parse(GetParamFromURLQueryString("page"));
            }
            return pageNumber;
        }

        public void setPageSize(int _size){
            pageSize = _size;
        }

        public int getPageSize(){
            if (String.IsNullOrEmpty(GetParamFromURLQueryString("n")))
            {
                pageSize = DefaultPageSize;
            }
            else
            {
                pageSize = Int32.Parse(GetParamFromURLQueryString("n"));
            }
            return pageSize;
            
        }

        /// <summary>
        /// Gets the alternate indexes.
        /// One index is used to run a search and generate hits
        /// All other Elastic indexes are searched over to get the hit count
        /// </summary>
        /// <returns></returns>
        public string getAlternateIndexes()
        {
            if (GetParamFromURLQueryString(indexParameter) != null && !GetParamFromURLQueryString(indexParameter).Equals("_all"))
            {
                return this.getAlternateIndexes(this.getIndexName());
            }
            else
            {
                return AllIndexes;
            }
        }


        /// <summary>
        /// Query text as written by the end user
        /// </summary>
        /// 
        public void setQueryText(string qtext){
            this.queryText = qtext;
        }

        public string getQueryText()
        {
            if (this.queryText == null)
            {
                this.queryText = GetParamFromURLQueryString(queryParameter);
            }
            if (this.queryText != null){
                return buildQuery(this.queryText);
            } else
            {
                return "*";  // default query is to match everything
            }
        }


        public void setIndexName(string _indexName)
        {
            // if the Url QueryString has multiple index names, only take the first one
            if (this.indexName.Equals(null) && isValidIndex(_indexName))
            {
                string wordsMode = GetParamFromURLQueryString("wordsMode");

                // If request comes from the embedded search form, it will have an index=website and wordsMode=0
                // If that is true, then now we want the query to go over all indexes, not the one called "website"
                // On the other hand, if someone refines by the website category, we DO want the query to restrict to website
                if (_indexName.Equals("_all") || _indexName.Equals("all content"))
                {
                    this.indexName = defaultIndexName;
                }
                else
                {
                    this.indexName = Regex.Replace(_indexName.ToLower(), @"\s+", "");
                }
            }
        }

        public string getIndexName()
        {
            if (this.indexName == null)
            {
                this.indexName = GetParamFromURLQueryString(indexParameter);
            }
            string wordsMode = GetParamFromURLQueryString("wordsMode");
            if (!wordsMode.Equals(string.Empty) && this.indexName.ToLower().Equals("website"))
            {
                this.indexName = "_all";
            }

            if (this.indexName != null && ! this.indexName.Equals("_all")){
                 this.indexName = Regex.Replace(this.indexName, @"\s+", "");
            } else {
                this.indexName = defaultIndexName;
            }

            return this.indexName;            
        }

        public string getIndexLabel(){
            return indexNames[getIndexName()];
        }

        public static string getRefinerLabel(string s)
        {
            if (indexNames.ContainsKey(s.ToLower()))
            {
                return indexNames[s.ToLower()];
            }
            else
            {
                return "N/A";
            }
        }

        public RefinerItem getRefiner()
        {

            if (GetSelectedRefiner() != null)
            {
                this.refiner = GetSelectedRefiner();
            }
            return this.refiner;
        }

        public string getOptionalBoostQuery()
        {

            string boostedHitQueryId = string.Empty;

            if (this.getQueryText() != null && this.getQueryText() != string.Empty && this.getQueryText() != "*")
            {
                boostedHitQueryId = getBoostedHitIdFile(this.getQueryText());
                if (boostedHitQueryId != string.Empty)
                {
                    if (boostedHitQueryId.IndexOf(":") > 0)
                    {
                        string ids = "";
                        foreach (string s in boostedHitQueryId.Split(':'))
                        {
                            ids = ids + "id:" + s + " OR ";
                        }
                        return ids.Substring(0, ids.Length - 4);
                    }
                    else
                    {
                        return "id:" + boostedHitQueryId;
                    }
                }
                if (buildOptionalQuery(this.getQueryText()) != null)
                {
                    return buildOptionalQuery(this.getQueryText());
                }
            }

            return null;
        }

        /// <summary>
        /// Gets a param from the URL
        /// </summary>
        /// <returns></returns>
        private string GetParamFromURLQueryString(string param)
        {
            string text = string.Empty;
            HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
            if (currentHttpContext != null)
            {
                text = HttpUtility.UrlDecode((currentHttpContext.Request.QueryString[param] ?? string.Empty));
                //  text = text.Trim().ToLower();
            }
            return text;
        }

        /// <summary>
        /// Get the list of refiners from the query string
        /// in theory there should only be one refiner parameter
        /// to have more than one, we would need multi-select on the reuslts interface
        /// like check boxes
        /// input will be r=name:value
        /// where name is the name of an aggregator and needs to be translated to a field name
        /// value is the string to look for
        /// </summary>
        /// 
        /// <returns>a refinerItem reflecting the refinement requested in the query or null if no refinement</returns>
        private RefinerItem GetSelectedRefiner()
        {
            RefinerItem selectedRefiner = new RefinerItem();

            HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
            var nameValues = HttpUtility.ParseQueryString(currentHttpContext.Request.QueryString.ToString());

            string refiner = GetParamFromURLQueryString(refinerParameter);
            if (refiner != null && refiner.IndexOf(":")>0){
                selectedRefiner.RefinementName = refiner.Split(':')[0];
                selectedRefiner.RefinementValue = refiner.Split(':')[1];
                nameValues.Remove(refinerParameter);
                selectedRefiner.RefinementUrl = String.Format("{0}?{1}", currentHttpContext.Request.Url.AbsolutePath, nameValues);
            }
            else
            {
                selectedRefiner = null;
            }
            return selectedRefiner;
        }


        /// <summary>
        /// Gets the alternate indexes. 
        /// If "selected" index is "_all" then we want all indexes included in the search and  
        /// we want all indexes included in the "alternate" search (better approach would be
        /// to build an aggregator from the website but this is quicker to implement)
        /// If there is a specific index in the query, then the "alternate indexes" is all indexes
        /// except the chosen one
        /// Takes an index name - returns all other valid indexes except this one
        /// </summary>
        /// <param name="_index">The named _index </param>
        /// <returns>array of valid indexes with the named index removed</returns>
        private string getAlternateIndexes(string _index)
        {
            if (GetParamFromURLQueryString(indexParameter) != null && GetParamFromURLQueryString(indexParameter).Equals("_all"))
            {
                return defaultIndexName;
            }
            else
            {
                string altIndexes = "";
                foreach (string i in defaultIndexes)
                {
                    if (!_index.Equals(i))
                    {
                        altIndexes += i + ",";
                    }
                }
                return altIndexes.Substring(0,altIndexes.Length-1);
            }
        }

        private bool isValidIndex(string _index)
        {
            bool isValid = false;
            if (_index.Equals("_all"))
            {
                return true;
            }
            foreach (string i in defaultIndexes){
                if (_index.Equals(i))
                {
                    return true;
                }
            }
            return isValid;
        }

        private string buildQuery(string q)
        {
            string query = q;

            if (query.IndexOf(":") > 0 && !(query.IndexOf("title:") >=0))
            {
                query = query.Replace(":"," ");
            }

            if (query.IndexOf("title:") >= 0 || query.IndexOf("\"") >=0 || query.IndexOf("\'") >=0
                || query.IndexOf(" OR ") > 0 || query.IndexOf(" AND ") > 0 || query.IndexOf(" NOT ") > 0
                || (query.IndexOf("(") > 0 && query.IndexOf(")") > query.IndexOf("("))
                )
            {
                return query;
            }
            else 
            {
                return "\"" + query + "\"";
            }
        }

        private string buildOptionalQuery(string q)
        {
            string query = q.ToLower().Replace("\"", "");

            if (query.StartsWith("accountant in") || query.StartsWith("accountants in"))
            {
                return "title:\"find a firm\"";
            }
            return null;
        }

        private string getBoostedHitIdFile(string q)
        {
            QueryModel.loadBoostHits();
            string query = q.Replace("\"", "");
            if (boostedHits.ContainsKey(query))
            {
                return boostedHits[query];
            } else
            {
                return string.Empty;
            }

        }

        private static void loadBoostHits()
        {
            boostedHits.Clear();
            var lines = File.ReadAllLines(HttpContext.Current.Server.MapPath(BOOSTEDHITS_FILE));
            for (var i = 0; i < lines.Length; i+= 1)
            {
                string line = (string)lines[i];
                if (line.IndexOf(":") > 0)
                {
                    line = line.Replace("\"", "");
                    line = line.TrimStart(' ');
                    line = line.TrimEnd(',');
                    string queryTerm = line.Substring(0, line.IndexOf(":"));
                    string id = line.Substring(line.IndexOf(":")+1);
                    if (null != queryTerm && queryTerm.Length > 0 && null != id && id.Length > 0)
                    {
                        // do we already have a boost for this keyword? - append the new ID to end of existing queryID string, separated by ":"
                        if (boostedHits.ContainsKey(queryTerm))
                        {
                            string newqterm = boostedHits[queryTerm] + ":" + id;
                            boostedHits.Remove(queryTerm);
                            boostedHits.Add(queryTerm, newqterm);
                        }
                        else
                        {
                            boostedHits.Add(queryTerm, id);
                        }
                    }
                }

            }
        }


    }
}
