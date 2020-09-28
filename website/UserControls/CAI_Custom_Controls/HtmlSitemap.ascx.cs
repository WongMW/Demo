using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RelatedData;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls
{
    public partial class HtmlSitemap : Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
    {
        private String shopPageNodeId = "60080148-08b3-649f-a7d8-ff000079c5aa";
        private String topLevelPageId = "f669d9a7-009d-4d83-ddaa-000000000002";

        /// <summary>
        /// Product Category ID's to be generated into the sitemap
        /// </summary>
        private List<int> ProductCategories { get; set; }

        /// <summary>
        /// Product Category ID's to be generated into the sitemap separated by commas
        /// </summary>
        public String ProductCategoriesString { get; set; }

        /// <summary>
        /// Indicator used to generate only firms
        /// </summary>
        public Boolean GenerateFirmsOnly { get; set; }
        /// <summary>
        /// Firms Directory Title to be used as a top level item in sitemap
        /// </summary>
        public String FirmsDirectoryTitle { get; set; }
        /// <summary>
        /// Firms Directory URL to be used as a top level item in sitemap
        /// </summary>
        public String FirmsDirectoryUrl { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsBackend() && !IsPostBack)
            {
                ProductCategories = new List<int>();
                // sample to be removed
                if (String.IsNullOrEmpty(ProductCategoriesString))
                {
                    ProductCategoriesString = "398,396,397,329,331,399,414";
                }
                // >>>>>> sample to be removed

                if (String.IsNullOrEmpty(ProductCategoriesString))
                {
                    ProductCategoriesString = "";
                }

                var pCats = ProductCategoriesString.Split(',');
                foreach(var c in pCats)
                {
                    if(!String.IsNullOrEmpty(c))
                    {
                        var cc = c.Trim();
                        var cc_int = -1;
                        if(int.TryParse(cc, out cc_int))
                        {
                            ProductCategories.Add(cc_int);
                        }
                    }
                }

                var sysConfig = Telerik.Sitefinity.Configuration.Config.Get<Telerik.Sitefinity.Services.SystemConfig>();

                String urlHost = String.Empty;
                if(sysConfig != null && sysConfig.SiteUrlSettings != null && !String.Empty.Equals(sysConfig.SiteUrlSettings.Host))
                {
                    urlHost = "https://" + sysConfig.SiteUrlSettings.Host;
                }

                var stmp = GenerateShortSitemap(urlHost);

                sitemap.DataSource = stmp;
                sitemap.DataBind();
            }
        }

        private void FillTheParentsInList(PageNode pageNode, Dictionary<PageNode, Object> listOfAllPages)
        {
            List<PageNode> st = new List<PageNode>();
            PageNode _pgNode = pageNode;
            if(!_pgNode.Id.ToString().Equals(topLevelPageId))
            {
                st.Insert(0, _pgNode);
            }
            while(_pgNode.Parent != null && !_pgNode.Parent.Id.ToString().Equals(topLevelPageId))
            {
                _pgNode = _pgNode.Parent;
                st.Insert(0, _pgNode);
            }

            var _listOfAllPages = listOfAllPages;

            foreach(var n in st)
            {
                if(_listOfAllPages.Keys.Where(a => a.Id.Equals(n.Id)).Count() == 0)
                {
                    if(!_listOfAllPages.ContainsKey(n))
                        _listOfAllPages.Add(n, new Dictionary<PageNode, Object>());
                }

                foreach(var n2 in _listOfAllPages)
                {
                    if(n2.Key.Id.Equals(n.Id))
                    {
                        _listOfAllPages = (Dictionary<PageNode, Object>)n2.Value;
                        break;
                    }
                }
            }
        }

        private String GetCsvOutOfStructure(Dictionary<PageNode, Object> listOfAllPages, String urlHost, String delimiter, String indent)
        {
            var csv = new StringBuilder();

            foreach (var n in listOfAllPages)
            {
                csv.AppendLine(
                    n.Key.Id + delimiter +
                    indent + n.Key.Title.ToString() + delimiter + 
                    urlHost + n.Key.GetDefaultUrl()
                );

                var extraTxt = GetCsvOutOfStructure((Dictionary <PageNode, Object>)n.Value, urlHost, delimiter, indent + "- ");
                if(!String.IsNullOrEmpty(extraTxt))
                {
                    csv.Append(extraTxt);
                }
            }

            return csv.ToString();
        }

        private String GenerateShortSitemapCsv(String urlHost)
        {
            var listOfAllPages = GenerateShortSitemap(urlHost);

            var allPagesCsvText = GetCsvOutOfStructure(listOfAllPages, urlHost, "~", "");

            return allPagesCsvText;
        }

        private Dictionary<PageNode, Object> GenerateShortSitemap(String urlHost)
        {
            var listOfAllPages = new Dictionary<PageNode, Object>();

            // checking if only firms to be generated
            if(GenerateFirmsOnly)
            {
                Dictionary<PageNode, object> firmsPages = new Dictionary<PageNode, object>();

                var firmTopNode = new PageNode();
                firmTopNode.Name = String.IsNullOrEmpty(FirmsDirectoryTitle) ? "Firms directory" : FirmsDirectoryTitle;
                firmTopNode.Title = new Telerik.Sitefinity.Model.Lstring(firmTopNode.Name);
                firmTopNode.UrlName = new Telerik.Sitefinity.Model.Lstring(String.IsNullOrEmpty(FirmsDirectoryUrl) ? "/Find-a-Firm/Firms-Directory" : FirmsDirectoryUrl);

                var _params = new FirmsSearch.EnhancedFirmSearchResults.FirmSearchParams(0);
                _params.currentPage = 1;
                var currIndex = 0;
                var dt = _params.BuildSearchQuery();
                while(dt.Rows.Count > 0 && currIndex < dt.Rows.Count)
                {
                    var row = dt.Rows[currIndex];

                    int firmParentId = -1;
                    int.TryParse(row["ParentID"].ToString(), out firmParentId);

                    if(firmParentId <= 0)
                    {
                        var obj = new FirmsSearch.EnhancedListingObj()
                        {
                            Title = row["FirmName"] != null ? row["FirmName"].ToString() : String.Empty,
                            City = row["City"] != null && !String.IsNullOrEmpty(row["City"].ToString()) ? row["City"].ToString() : (row["County"] != null ? row["County"].ToString() : ""),
                            Phone = dt.Columns.Contains("ContactNo") && row["ContactNo"] != null ? row["ContactNo"].ToString() : String.Empty,
                            Url = row["WebSite"] != null ? row["WebSite"].ToString() : String.Empty,
                            ID = row["FId"] != null ? row["FId"].ToString() : String.Empty
                        };

                        var firmNode = new PageNode();
                        firmNode.Name = row["FirmName"].ToString();
                        firmNode.Title = new Telerik.Sitefinity.Model.Lstring(firmNode.Name);
                        firmNode.UrlName = obj.SinglePageUrl;

                        if(!firmsPages.ContainsKey(firmNode))
                            firmsPages.Add(firmNode, new Dictionary<PageNode, object>());
                    }

                    currIndex += 1;

                    if (dt.Rows.Count <= currIndex) {
                        currIndex = 0;
                        _params.currentPage += 1;
                        dt = _params.BuildSearchQuery();
                    }
                }
                if(!listOfAllPages.ContainsKey(firmTopNode))
                    listOfAllPages.Add(firmTopNode, firmsPages);

                return listOfAllPages;
            }

            var fluentPages = Telerik.Sitefinity.App.WorkWith().Pages();
            var allPages = fluentPages.Get().OrderBy(o => o.Ordinal);

            foreach (var page in allPages)
            {
                var isInShortMap = page.GetValue<Boolean?>("IncludeInShortSitemap");

                // ignor deleted, backend and not live pages
                if(
                    (page.IsBackend
                    || page.IsDeleted
                    || page.GetPageData() == null
                    || page.GetPageData().Status != Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live)
                    && !page.Id.ToString().ToLower().Equals(shopPageNodeId)
                )
                {
                    continue;
                }

                // checking if to be included in the sitemap
                if(!isInShortMap.HasValue || !isInShortMap.Value)
                {
                    continue;
                }

                FillTheParentsInList(page, listOfAllPages);
            }

            listOfAllPages = SortByOrdinal(listOfAllPages);

            FillSitemapWithProducts(listOfAllPages, urlHost);

            return listOfAllPages;
        }

        private Dictionary<PageNode, Object> SortByOrdinal(Dictionary<PageNode, Object> source)
        {
            var _listOfAllPages = source.OrderBy(a => a.Key.Ordinal);
            var listOfAllPages = new Dictionary<PageNode, Object>();

            foreach(var k in _listOfAllPages)
            {
                if(!listOfAllPages.ContainsKey(k.Key))
                    listOfAllPages.Add(k.Key, SortByOrdinal((Dictionary<PageNode, Object>)k.Value));
            }

            return listOfAllPages;
        }

        private void FillSitemapWithProducts(Dictionary<PageNode, Object> sitemap, String urlHost)
        {
            if(ProductCategories == null || ProductCategories.Count == 0)
            {
                return;
            }

            var shopNode = new KeyValuePair<PageNode, object>();
            foreach(var k in sitemap)
            {
                if (k.Key.Id.ToString().Equals(shopPageNodeId))
                {
                    shopNode = k;
                }
            }

            // checking if found shop
            if(shopNode.Key == null)
            {
                return;
            }

            var connectionString = SoftwareDesign.Helper.GetAptifyEntitiesConnectionString();
            var con = new SqlConnection(connectionString);

            con.Open();

            var cmd = new SqlCommand("spGetProductsCategory__c", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Connection = con;

            var rdr = cmd.ExecuteReader();
            var categories = new Dictionary<int, String>();

            while (rdr.Read())
            {
                var catId = int.Parse(rdr["ID"].ToString());
                if(ProductCategories != null && ProductCategories.Contains(catId))
                {
                    if(!categories.ContainsKey(catId))
                        categories.Add(catId, rdr["Name"].ToString());
                }
            }

            rdr.Close();
            
            // lets go through each of the category and retrieve its products
            foreach(var catId in ProductCategories)
            {
                if(categories.ContainsKey(catId))
                {
                    var catName = categories[catId];
                    var catUrl = /*urlHost + */"/ProductCatalog/ProductCategory.aspx?ID=" + catId;

                    var catPageNode = new PageNode();
                    catPageNode.Name = catName;
                    catPageNode.Title = new Telerik.Sitefinity.Model.Lstring(catName);
                    catPageNode.UrlName = new Telerik.Sitefinity.Model.Lstring(catUrl);

                    // lets retrieve all products for this category
                    cmd = new SqlCommand("SpGetProductForPreferredCurrency__c", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@CurrencyType", "Euro");
                    cmd.Parameters.AddWithValue("@ExcludeProductID", "0");
                    cmd.Parameters.AddWithValue("@CategoryID", catId);
                    cmd.Parameters.AddWithValue("@ShowMeetingsLinkToClass", false);

                    rdr = cmd.ExecuteReader();

                    Dictionary<PageNode, object> catProducts = new Dictionary<PageNode, object>();

                    while (rdr.Read())
                    {
                        var productURLToUse = rdr["ProductURLToUse"].ToString();
                        var productId = rdr["ID"].ToString();
                        String name = rdr["WebName"].ToString();
                        String loc = /*urlHost + */(String.IsNullOrEmpty(productURLToUse) ? "/ProductCatalog/Product.aspx" : productURLToUse.Replace("~", "")) + "?ID=" + productId;

                        var productNode = new PageNode();
                        productNode.Name = name;
                        productNode.Title = new Telerik.Sitefinity.Model.Lstring(name);
                        productNode.UrlName = new Telerik.Sitefinity.Model.Lstring(loc);
                        if(!catProducts.ContainsKey(productNode))
                            catProducts.Add(productNode, new Dictionary<PageNode, object>());
                    }

                    rdr.Close();

                    var v = (Dictionary<PageNode, Object>)shopNode.Value;
                    if(!v.ContainsKey(catPageNode))
                        v.Add(catPageNode, catProducts);
                }
            }

            con.Close();
        }

        protected void sitemap_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.DataItem == null)
            {
                return;
            }

            var d = (KeyValuePair<PageNode, object>)e.Item.DataItem;

            var node = d.Key;
            var kids = (Dictionary<PageNode, object>)d.Value;

            var sitemapP = (HtmlSitemapPart)e.Item.FindControl("sitemapPart");

            sitemapP.Node = node;
            sitemapP.SubNodes = kids;
        }
    }
}
