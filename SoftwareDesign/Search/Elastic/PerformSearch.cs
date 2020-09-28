using System;
using System.Linq;
using System.Collections.Generic;
using Nest;
using System.Web;
using Telerik.Sitefinity.Services;
using SoftwareDesign.Search.ViewModels;
using SoftwareDesign.Search.Models;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Modules.Pages;
using System.Globalization;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.News.Model;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.Lists.Model;
using Telerik.Sitefinity.Modules.Lists;
using Telerik.Sitefinity.RelatedData;

namespace SoftwareDesign.Search.Elastic
{
    public class PerformSearch
    {
        public String ElasticsearchUrl { get; set; }
        public String RefinerList { get; set; }

        const string TitlePropery = "title";
        const string ContentPropery = "content";

        private CultureInfo cultInfoEN = CultureInfo.GetCultureInfo("en");

        // GET: Results
        public ResultViewModel SearchIndex()
        {
            // Create some variables to populate later

            // List to store search result items
            var resultsModel = new List<ResultsModel>();
            ResultViewModel resultViewModel = new ResultViewModel();

            // instantiate a new query - reads all data from request to build the right values
            QueryModel query = new QueryModel();

            // List of facets to refine by
            List<Refiner> refinerItems = new List<Refiner>();

            // List of facets to refine by
            List<Refiner> alternateItems = new List<Refiner>();

            // List of facets that have been selected in this query (should only ever be one )
            List<RefinerItem> selectedItems = new List<RefinerItem>();

            List<SuggestsModel> suggests = new List<SuggestsModel>();

            // dunno
            ISearchResponse<ResultsModel> results = new SearchResponse<ResultsModel>();

            // storage unit to put search results information for the secondary search
            // Alongside the primary query to generate result hits, we run a second query
            // Purpose is to get counts of the available documents in other indexes, in case
            // the user wants to switch context and search another data source
            // Although its a generic result set, it has zero hits, only refiner facets and counts
            ISearchResponse<ResultsModel> catCountResultSet = new SearchResponse<ResultsModel>();

            try
            {

                // STuff to open connection to elastic
                var node = new Uri(ElasticsearchUrl);
                var settings = new ConnectionSettings(node);
                var client = new ElasticClient(settings);

                // get the list of refiners from the control's property
                string[] refinerList = RefinerList.Split(';');

                // The query request includes a refine parameter to choose a category to filter by
                if (query.getRefiner() == null)
                {

                    // Now run the search
                    results = PlainSearch(client, query);
                }
                else
                {

                    // there is a refinement on the query request so pass it through to the next step
                    selectedItems.Add(query.getRefiner());

                    resultViewModel.SelectedRefiners = selectedItems;
                    results = RefinedSearch(client, query);
                }

                catCountResultSet = runCategoryCountSearch(client, query);


                if (results.Hits.Any())
                {
                    // Populate the document hits model and get the highlighted summary
                    foreach (Hit<ResultsModel> hit in results.Hits)
                    {
                        try
                        {
                            ResultsModel resultItem = GetDocumentWithHighlight(hit);
                            string resultUrl = "/";  // if we have no hit url, we have a problem. Use this relative URL to site home page

                            //                        if (resultItem.SiteSection != null && String.Equals(resultItem.SiteSection.ToLower(), "website"))
                            if (resultItem.SiteSection != null && resultItem.SiteSection.ToLower().StartsWith("website"))
                            {
                                resultItem.link = hit.Id;  // Sitefinity docs have an ID that needs to be translated to a URL

                                if (resultItem.ContentType != null && string.Equals(resultItem.ContentType, "Telerik.Sitefinity.Libraries.Model.Document"))
                                {
                                    resultUrl = getUrlFromDocumentGUID(Guid.Parse(resultItem.Id));
                                }
                                else if (resultItem.ContentType != null && String.Equals(resultItem.ContentType, "Telerik.Sitefinity.News.Model.NewsItem"))
                                {
                                    resultUrl = getUrlFromNewsItemGUID(Guid.Parse(resultItem.Id));
                                }
                                else if (resultItem.ContentType != null && String.Equals(resultItem.ContentType, "Telerik.Sitefinity.Lists.Model.ListItem"))
                                {
                                    resultUrl = GetListItemURLById(Guid.Parse(resultItem.Id));
                                }
                                else if (resultItem.ContentType != null && String.Equals(resultItem.ContentType, "Telerik.Sitefinity.Pages.Model.PageNode"))
                                {
                                    resultUrl = GetUrlByPageNodeId(Guid.Parse(resultItem.Id), cultInfoEN.ToString(), true);
                                }
                                else if (resultItem.SiteSubsection != null && resultItem.SiteSubsection.ToLower().Equals("products"))
                                {
                                    resultUrl = resultItem.link;
                                }
                            }
                            else if (resultItem.SiteSection != null)
                            {
                                // Other, external web-crawled docs have a URL as their link ??? need to check
                                resultUrl = resultItem.link;
                            }

                            resultItem.ResultUrl = resultUrl;
                            resultItem.is_secure = hit.Source.is_secure;
                            resultItem.indexName = query.getIndexLabel();
                            resultsModel.Add(resultItem);
                        }
                        catch (Exception ex)
                        {
                            //skipping results without resultUrl
                        }
                    }

                    // get the refiners
                    if (results.Aggregations.Count > 0)
                    {
                        string currentRefinerName = null;
                        if (query.getRefiner() != null && query.getRefiner().RefinementName != null)
                        {
                            currentRefinerName = query.getRefiner().RefinementName;
                        }

                        foreach (var refinerItem in results.Aggregations)
                        {
                            // if refiner has already been selected, do not display it
                            if (currentRefinerName == null || !String.Equals(currentRefinerName.ToLower(), refinerItem.Key.ToLower()))
                            {
                                Refiner refiners = new Refiner();

                                refiners.RefinerName = refinerItem.Key.ToUpper();
                                // get the refiners from the search result
                                var aggregates = results.Aggs.Terms(refinerItem.Key).Buckets.ToList();
                                List<RefinerItem> refinementItems = GetRefinerList(aggregates, refinerItem.Key);
                                refiners.RefinementItems = refinementItems;

                                refinerItems.Add(refiners);
                            }
                        }
                    }

                    if (catCountResultSet != null && catCountResultSet.Aggregations.Count > 0)
                    {
                        string currentIndex = query.getIndexName();  // should never be null - all queries must have an index
                        foreach (var alternateIndexItem in catCountResultSet.Aggregations)
                        {
                            // should only be one set of aggregations called "alternate_sources"
                            if (String.Equals(alternateIndexItem.Key.ToLower(), "alternate_sources"))
                            {
                                Refiner alternates = new Refiner();
                                var aggregates = catCountResultSet.Aggs.Terms(alternateIndexItem.Key).Buckets.ToList();
                                List<RefinerItem> alternateItemsList = GetAlternatesList(aggregates, alternateIndexItem.Key, query.getQueryText());
                                alternates.RefinementItems = alternateItemsList;
                                alternateItems.Add(alternates);

                            }
                        }
                    }



                    // Get the results information                  
                    resultViewModel.NumHits = (int)results.Total;



                }

                // Get the pagination data
                var pager = new Pager((int)resultViewModel.NumHits, query.getPageNumber());

                resultViewModel.ResultModel = resultsModel;
                resultViewModel.RefinersModel = refinerItems;
                resultViewModel.AlternativesModel = alternateItems;
                resultViewModel.Pager = pager;
                resultViewModel.QueryTerm = query.getQueryText();
                resultViewModel.indexName = query.getIndexName();
                resultViewModel.indexDisplayName = query.getIndexLabel();
                if (query.getIndexLabel().Equals("all content"))
                {
                    resultViewModel.AllIndexesNumHits = (int)results.Total;
                }
                else
                {
                    resultViewModel.AllIndexesNumHits = (int)results.Total + (int)catCountResultSet.Total;
                }


                if (resultViewModel.SelectedRefiners == null)
                {
                    // Should already have assigned this to the result object if the query had a refine parameter
                    // but add it again just in case
                    resultViewModel.SelectedRefiners = selectedItems;
                }

                foreach (string s in GetSpellcheckSuggestions(client, query))
                {
                    SuggestsModel suggest = new SuggestsModel();
                    suggest.SuggestText = s;
                    suggest.SuggestUrl = "/search-landing?indexCatalogue=_all&searchQuery=" + s;
                    suggests.Add(suggest);
                }

                resultViewModel.SpellSuggestions = suggests;

                return resultViewModel;
            }
            catch (Exception ex)
            {
                string stack = ex.StackTrace;
                string ex_message = ex.Message;
                return resultViewModel;
            }
        }

        /// <summary>
        /// Uses the provided document hit to pull out the highlighted summary
        /// </summary>
        /// <param name="hit"></param>
        /// <returns></returns>
        private ResultsModel GetDocumentWithHighlight(Hit<ResultsModel> hit)
        {
            ResultsModel document = hit.Source as ResultsModel;
            string highlights = GetHighlights(hit);

            if (highlights != null && highlights.Length > 15)   // minimum size for valid snippet
            {
                document.HitHighlightedSummary = highlights;
            }
            else if (hit.Source.Content != null && hit.Source.Content.Length > 254)
            {
                document.HitHighlightedSummary = hit.Source.Content.Substring(0, 254);
            }
            else if (hit.Source.Content != null)
            {
                document.HitHighlightedSummary = hit.Source.Content;
            }

            return document;
        }

        /// <summary>
        /// Get the highlighted summary
        /// </summary>
        /// <param name="hit"></param>
        /// <returns></returns>
        private string GetHighlights(IHit<object> hit)
        {
            string text = string.Empty;
            foreach (string current in hit.Highlights.Keys)
            {
                if (!string.IsNullOrEmpty(text))
                {
                    text += ", ";
                }

                var highlight = hit.Highlights[current];
                text += highlight.Highlights.Aggregate((string i, string j) => i + ", " + j);
            }
            return text;
        }


        /// <summary>
        /// Run a full text search against Elasticsearch index
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="indexMarker">The index marker.</param>
        /// <param name="queryTerm">The query term.</param>
        /// <param name="pageNo">The page no.</param>
        /// <param name="refinerList">The refiner list.</param>
        /// <returns></returns>
        private ISearchResponse<ResultsModel> PlainSearch(ElasticClient client, QueryModel query)
        {
            ISearchResponse<ResultsModel> result = null;
            // All queries need to exclude any page nodes that are not of type = dahuindexablepage
            // They have been sent incorrectly by Sitefinity and are duplicates
            // Hence we need to ensure all queries have this following clause to exclude the dupes
            //q=NOT+contentType:"Telerik.Sitefinity.Pages.Model.PageNode"%20AND%20_type:document
            QueryContainer excludeQuery1 = new TermQuery
            {
                Field = "contentType",
                Value = "Telerik.Sitefinity.Pages.Model.PageNode",
                Boost = 4
            };
            QueryContainer excludeQuery2 = new TermQuery
            {
                Field = "_type",
                Value = "document",
                Boost = 1
            };


            try
            {
                if (query.getQueryText().Equals("*"))
                {
                    result = client.Search<ResultsModel>(d => d
                    .Index(query.getIndexName())
                    .AllTypes()
                    .Query(q => q.MatchAll() && !(excludeQuery1 && excludeQuery2))
                    .Aggregations(a => a
                        .Terms("siteSubsection", k => k.Field(kf => kf.SiteSubsection)
                            .ExecutionHint(TermsAggregationExecutionHint.GlobalOrdinalsHash)
                            .Size(0)))
                    .From((query.getPageNumber() - 1) * query.getPageSize())
                    .Size(query.getPageSize())
                    );
                }
                else
                {
                    var _fields = (new List<string>() { "title^2", "content" }).ToArray();

                    QueryContainer qry = new QueryStringQuery()
                    {
                        DefaultOperator = Operator.And,
                        Query = query.getQueryText(),
                        Fields = _fields
                    };

                    if (query.getOptionalBoostQuery() != null)
                    {

                        QueryContainer optqry = new QueryStringQuery()
                        {
                            DefaultOperator = Operator.And,
                            Query = query.getOptionalBoostQuery(),
                        };


                        result = client.Search<ResultsModel>(d => d
                            .Index(query.getIndexName())
                            .AllTypes()
                            .Query(q => q
                                .Bool(b => b
                                    .Must((optqry || qry) && !(excludeQuery1 && excludeQuery2))
                                )
                            )
                            .Aggregations(a => a
                                .Terms("siteSubsection", k => k.Field(kf => kf.SiteSubsection)
                                .ExecutionHint(TermsAggregationExecutionHint.GlobalOrdinals)
                                .Size(0)))
                            // Highlight the hits on matching on the content field
                            .Highlight(h => h
                                .PreTags("<b>")
                                .PostTags("</b>")
                               .Fields(f => f
                                 .Field(e => e.Content)
                                 .ForceSource(true)
                                 .PreTags("<b>")
                                .PostTags("</b>")))
                            .Highlight(h => h
                              .PreTags("<b>")
                              .PostTags("</b>")
                              .Fields(f => f
                                .Field(e => e.Title)
                                .PreTags("<b>")
                                .PostTags("</b>")))
                            .From((query.getPageNumber() - 1) * query.getPageSize())
                            .Size(query.getPageSize())
                        );
                    }
                    else
                    {

                        result = client.Search<ResultsModel>(d => d
                        .Index(query.getIndexName())
                        .AllTypes()
                        .Query(q => q
                            .Bool(b => b
                            .Must(qry && !(excludeQuery1 && excludeQuery2))))
                            //q
                            .Aggregations(a => a
                                  .Terms("siteSubsection", k => k.Field(kf => kf.SiteSubsection)
                                  .ExecutionHint(TermsAggregationExecutionHint.GlobalOrdinals)
                                  .Size(0)))
                        // Highlight the hits on matching on the content field
                        .Highlight(h => h
                                .PreTags("<b>")
                                .PostTags("</b>")
                                .Fields(f => f
                                    .Field(e => e.Content)
                                    .ForceSource(true)
                                    .PreTags("<b>")
                                    .PostTags("</b>")))
                        .Highlight(h => h
                                .PreTags("<b>")
                                .PostTags("</b>")
                                .Fields(f => f
                                    .Field(e => e.Title)
                                    .PreTags("<b>")
                                    .PostTags("</b>")))
                        .From((query.getPageNumber() - 1) * query.getPageSize())
                        .Size(query.getPageSize())
                        );
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                string stack = ex.StackTrace;
                string ex_message = ex.Message;
            }
            return result;
        }


        private ISearchResponse<ResultsModel> runCategoryCountSearch(ElasticClient client, QueryModel query)
        {
            QueryContainer excludeQuery1 = new TermQuery
            {
                Field = "contentType",
                Value = "Telerik.Sitefinity.Pages.Model.PageNode"
            };
            QueryContainer excludeQuery2 = new TermQuery
            {
                Field = "_type",
                Value = "document"
            };


            ISearchResponse<ResultsModel> result = null;
            try
            {
                if (query.getQueryText().Equals("*"))
                {
                    result = client.Search<ResultsModel>(d => d
                    .Index(query.getAlternateIndexes())
                    .AllTypes()
                    .MatchAll()
                    .Aggregations(a => a
                        .Terms("alternate_sources", s => s.Field(sf => sf.SiteSection).ExecutionHint(TermsAggregationExecutionHint.GlobalOrdinalsHash)))
                    .Size(0)
                    );
                }
                else
                {
                    var _fields = (new List<string>() { "title^2", "content" }).ToArray();

                    QueryContainer qry = new QueryStringQuery()
                    {
                        DefaultOperator = Operator.And,
                        Query = query.getQueryText(),
                        Fields = _fields
                    };

                    if (query.getOptionalBoostQuery() != null)
                    {

                        QueryContainer optqry = new QueryStringQuery()
                        {
                            DefaultOperator = Operator.And,
                            Query = query.getOptionalBoostQuery(),
                        };

                        result = client.Search<ResultsModel>(d => d
                        .Index(query.getAlternateIndexes())
                        .AllTypes()
                        .Query(q => q
                            .Bool(b => b
                            .Must(optqry || qry)))
                        .Aggregations(a => a
                            .Terms("alternate_sources", s => s.Field(sf => sf.SiteSection).ExecutionHint(TermsAggregationExecutionHint.GlobalOrdinals)))
                        .Size(0)
                        );

                    }
                    else
                    {

                        result = client.Search<ResultsModel>(d => d
                        .Index(query.getAlternateIndexes())
                        .AllTypes()
                        .Query(q => q
                            .Bool(b => b
                            .Must(qry)))
                        .Aggregations(a => a
                            .Terms("alternate_sources", s => s.Field(sf => sf.SiteSection).ExecutionHint(TermsAggregationExecutionHint.GlobalOrdinals)))
                        .Size(0)
                        );
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                string stack = ex.StackTrace;
                string ex_message = ex.Message;
            }
            return result;
        }


        /// <summary>
        /// Run a full text search with a selected refiner against the Elasticsearch index
        /// </summary>
        /// <param name="client"></param>
        /// <param name="indexMarker"></param>
        /// <param name="refinerNames"></param>
        /// <returns></returns>
        private ISearchResponse<ResultsModel> RefinedSearch(ElasticClient client, QueryModel query)
        {

            QueryContainer excludeQuery1 = new TermQuery
            {
                Field = "contentType",
                Value = "Telerik.Sitefinity.Pages.Model.PageNode"
            };
            QueryContainer excludeQuery2 = new TermQuery
            {
                Field = "_type",
                Value = "document"
            };


            // get the selected refiner property and value
            string refinerProperty = query.getRefiner().RefinementName;
            string refinerValue = query.getRefiner().RefinementValue;

            // We could have a mapping from the aggregation name to the field name but why bother/
            // Just stick to a naming convention that says we always use the fieldname as the refinement aggregation name
            //            string filterFieldName = "";
            //            if (refinerProperty.Equals("filter by") || refinerProperty.Equals("FILTER BY")) { filterFieldName = "SiteSection"; }
            //            if (refinerProperty.Equals("refiner your search") || refinerProperty.Equals("REFINER YOUR SEARCH")) { filterFieldName = "SiteSubsection"; }

            string filterFieldName = refinerProperty;

            var _fields = (new List<string>() { "title^2", "content" }).ToArray();

            ISearchResponse<ResultsModel> result = null;


            QueryContainer qry = new QueryStringQuery()
            {
                DefaultOperator = Operator.And,
                Query = query.getQueryText(),
                Fields = _fields
            };

            if (query.getOptionalBoostQuery() != null)
            {

                QueryContainer optqry = new QueryStringQuery()
                {
                    DefaultOperator = Operator.And,
                    Query = query.getOptionalBoostQuery(),
                };


                result = client.Search<ResultsModel>(d => d
        .Index(query.getIndexName())
        .AllTypes()
        .Query(fq => fq
            .Nested(fqq => fqq
                .Query(q => q
                    .Bool(qb => qb
                    .Must(optqry || qry)))
                .Query(ff => ff
                    .Bool(b => b
                        .Must(m1 => m1.Term(refinerProperty, refinerValue))))
                ))
        .Aggregations(a => a
            .Terms("siteSubsection", c => c.Field(cf => cf.SiteSubsection)
            .Size(0)))
        .Highlight(h => h
            .PreTags("<b>")
            .PostTags("</b>")
            .Fields(f => f
                .Field(e => e.Content)
                .ForceSource(true)
                .PreTags("<em>")
                .PostTags("</em>")))
        .Highlight(h => h
            .PreTags("<b>")
            .PostTags("</b>")
            .Fields(f => f
                .Field(e => e.Title)
                .PreTags("<em>")
                .PostTags("</em>")))
        .From((query.getPageNumber() - 1) * query.getPageSize())
        .Size(query.getPageSize())
    );
            }
            else
            {
                result = client.Search<ResultsModel>(d => d
        .Index(query.getIndexName())
        .AllTypes()
        .Query(fq => fq
            .Nested(fqq => fqq
                .Query(q => q
                    .Bool(qb => qb
                    .Must(qry)))
                .Query(ff => ff
                    .Bool(b => b
                        .Must(m1 => m1.Term(refinerProperty, refinerValue))))
                ))
        .Aggregations(a => a
            .Terms("siteSubsection", c => c.Field(cf => cf.SiteSubsection)
            .Size(0)))
        .Highlight(h => h
            .PreTags("<b>")
            .PostTags("</b>")
            .Fields(f => f
                .Field(e => e.Content)
                .ForceSource(true)
                .PreTags("<em>")
                .PostTags("</em>")))
        .Highlight(h => h
            .PreTags("<b>")
            .PostTags("</b>")
            .Fields(f => f
                .Field(e => e.Title)
                .PreTags("<em>")
                .PostTags("</em>")))
        .From((query.getPageNumber() - 1) * query.getPageSize())
        .Size(query.getPageSize())
    );
            }



            return result;
        }

        // TODO - Finish this function
        private List<string> GetSpellcheckSuggestions(ElasticClient client, QueryModel query)
        {

            List<string> suggestions = new List<string>();

            // Have to restrict spell-suggestions to main website elastic index cos 
            // it fails if you extend to include the other ones
            var result = client.Suggest<SuggestsModel>(s => s
                .Index("website")
                .Phrase("phrase-suggestion", m => m
                .Text(query.getQueryText())
                .Field("content")
                .Size(3)
                )
            );
            /*
                        var result = client.Suggest<SuggestsModel>(s => s
                            .Term("term-suggestion", m => m
                                .Text(query.getQueryText())
                                .Size(3)
                                .OnField("content")
                            )
                        );
            */

            foreach (Nest.Suggest<SuggestsModel> s in result.Suggestions["phrase-suggestion"].ToList())
            {
                if (s.Options != null && s.Options.Count() > 0)
                {
                    foreach (Nest.SuggestOption<SuggestsModel> option in s.Options)
                    {
                        suggestions.Add(option.Text);
                    }
                }
            }
            return suggestions;

        }

        #region Helper methods



        /// <summary>
        /// From a result set, look for the aggregate values for potential refiner objects
        /// Remove any the match the currently applied refiner
        /// Get the list of refiner key, values
        /// </summary>
        /// <param name="refiner"></param>
        /// <param name="refinerName"></param>
        /// <returns></returns>

        private List<RefinerItem> GetRefinerList(IList<Nest.KeyedBucket<string>> refiner, string refinerName)
        {
            List<RefinerItem> refinementItems = new List<RefinerItem>();

            foreach (var aggs in refiner)
            {
                // update the refiner model with the refiner details
                RefinerItem aggsValues = new RefinerItem();
                aggsValues.RefinementValue = aggs.Key;
                aggsValues.RefinementCount = aggs.DocCount.ToString();
                aggsValues.RefinementLabel = aggs.Key;

                // Get the current query string and create a parameter for the refiners
                string currURLString = GetCurrentUrlString();
                aggsValues.RefinementUrl = String.Format("{0}&r={1}:{2}", currURLString, refinerName, aggs.Key);
                refinementItems.Add(aggsValues);
            }

            return refinementItems;
        }

        /// <summary>
        /// From a result set, look for refiners that represent the set of Elasticsearch indexes
        /// Difference from a regular refiner is that this is not going to be used as a refiner parameter
        /// instead it will switch the request to use a specific elasticsearch index, using the indexCatalogue parameter
        /// By default, a request will have indexCatalogue set to _all, which is interpreted as search over all registered indexes.
        /// Here will find a set of potential refiners by catalogue, and if one is selected, submit a request with a new
        /// indexCatalogue parameter, with the same query. 
        /// </summary>
        /// <param name="refiner"></param>
        /// <param name="refinerName"></param>
        /// <returns></returns>

        private List<RefinerItem> GetAlternatesList(IList<Nest.KeyedBucket<string>> refiner, string refinerName, string query)
        {
            List<RefinerItem> refinementItems = new List<RefinerItem>();

            foreach (var aggs in refiner)
            {
                // update the refiner model with the refiner details
                RefinerItem aggsValues = new RefinerItem();
                aggsValues.RefinementValue = aggs.Key;
                aggsValues.RefinementCount = aggs.DocCount.ToString();
                string refinerLabelOrig = Regex.Replace(aggs.Key.ToLower(), @"\s+", "");
                string refinerLabel = QueryModel.getRefinerLabel(refinerLabelOrig);
                aggsValues.RefinementLabel = refinerLabel;

                // Get the current query string and create a parameter for the refiners
                string currURLString = GetCurrentUrlString();

                string sq = "";
                string baseUrl = "";

                if (currURLString.IndexOf("searchQuery=") > 0)
                {
                    sq = currURLString.Substring(currURLString.IndexOf("searchQuery=") + 12);
                    if (sq != null && sq.IndexOf("&") > 0)
                    {
                        sq = sq.Substring(0, sq.IndexOf("&"));
                    }
                }
                else { sq = "*"; }

                if (currURLString.IndexOf("?") > 0)
                {
                    baseUrl = currURLString.Substring(0, currURLString.IndexOf("?"));
                }
                else
                {
                    baseUrl = "/search-landing";
                }

                string newurl = baseUrl + "?indexCatalogue=" + Regex.Replace(aggs.Key.ToLower(), @"\s+", "") + "&searchQuery=" + sq;
                aggsValues.RefinementUrl = newurl;
                refinementItems.Add(aggsValues);
            }

            return refinementItems;
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
                text = (currentHttpContext.Request.QueryString[param] ?? string.Empty);
                //  text = text.Trim().ToLower();
            }
            return text;
        }



        /// <summary>
        /// /// Get the current URL
        /// </summary>
        /// <returns></returns>

        private string GetCurrentUrlString()
        {
            string currentURL = string.Empty;
            HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
            if (currentHttpContext != null)
            {
                currentURL = (currentHttpContext.Request.RawUrl ?? string.Empty);
            }
            return currentURL;
        }



        /// <summary>
        /// Get the full URL path of the indexed Sitefinity page
        /// </summary>
        /// <param name="pageNodeId"></param>
        /// <param name="targetCulture"></param>
        /// <param name="resolveAsAbsolutUrl"></param>
        /// <returns></returns>
        public string GetUrlByPageNodeId(Guid pageNodeId, string targetCulture, bool resolveAsAbsolutUrl)
        {
            var manager = PageManager.GetManager();

            // Get the pageNode 
            var pageNode = manager.GetPageNodes().FirstOrDefault(x => x.Id == pageNodeId);

            var culture = CultureInfo.GetCultureInfo(targetCulture);

            var url = String.Empty;
            if (pageNode != null)
            {
                // Get the URL of the pageNode 
                url = pageNode.GetUrl(culture);
                if (resolveAsAbsolutUrl)
                {
                    // Get the absolute URL of the pageNode 
                    url = UrlPath.ResolveUrl(url, true, true);
                }
            }

            return url;
        }


        /// <summary>
        /// Get the full URL path of the indexed Sitefinity document
        /// </summary>
        /// <param name="pageNodeId"></param>
        /// <param name="targetCulture"></param>
        /// <param name="resolveAsAbsolutUrl"></param>
        /// <returns></returns>
        public string getUrlFromDocumentGUID(Guid docId)
        {

            var library = LibrariesManager.GetManager();
            var doc = library.GetDocument(docId);
            string url = doc.ResolveMediaUrl(true);
            return url;
        }


        public string getUrlFromNewsItemGUID(Guid docId)
        {
            String url = string.Empty;

            var clService = SystemManager.GetContentLocationService();
            var location = clService.GetItemDefaultLocation(typeof(NewsItem), null, docId);

            if (location != null)
            {
                return location.ItemAbsoluteUrl;
            }
            else
            {
                return "/";
            }

            /*
                        NewsItem newsItem = GetNewsItemNativeAPI(docId);
                        if (newsItem != null)
                        {
                            url = SystemManager.GetContentLocationService().GetItemDefaultLocation(newsItem).ItemAbsoluteUrl;
                        }
                        if (url != string.Empty)
                        {
                            return url;
                        }
                        else
                        {
                            return "/";
                        }
            */
        }


        /// <summary>
        /// Gets the news item native API.
        /// </summary>
        /// <param name="masterNewsId">The master news identifier.</param>
        /// <returns></returns>
        private NewsItem GetNewsItemNativeAPI(Guid masterNewsId)
        {
            NewsManager newsManager = NewsManager.GetManager();
            NewsItem item = newsManager.GetNewsItems().Where(newsItem => newsItem.Id == masterNewsId).FirstOrDefault();

            NewsItem liveItem = null;


            if (item != null)
            {
                liveItem = newsManager.Lifecycle.GetLive(item) as NewsItem;
            }
            if (liveItem != null)
            {
                return liveItem;
            }
            else
            {
                return item;
            }
        }



        public string GetListItemURLById(Guid _id)
        {
            ListsManager listsManager = ListsManager.GetManager();
            ListItem listItem = listsManager.GetListItems().Where(i => (i.Id == _id))
                .FirstOrDefault();
            if (listItem != null && listItem.Urls != null && listItem.Parent != null)
            {

                var clService = SystemManager.GetContentLocationService();
                var allLocations = clService.GetItemLocations(listItem);

                string firstUrl = string.Empty;

                foreach (var location in allLocations)
                {
                    if (firstUrl == string.Empty && location != null)
                    {
                        firstUrl = location.ItemAbsoluteUrl;
                    }
                }
                if (firstUrl != null)
                {
                    return firstUrl;
                }
                else
                {
                    return "/";
                }
            }
            else
            {
                return "/";
            }
        }

        #endregion
    }
}
