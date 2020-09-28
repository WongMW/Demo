using Newtonsoft.Json;
using SitefinityWebApp.UserControls.CAI_Custom_Controls.FirmsSearch;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Telerik.Sitefinity;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.News.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SitemapGenerator.Data;
using Telerik.Sitefinity.Web;
using static SitefinityWebApp.UserControls.CAI_Custom_Controls.FirmsSearch.EnhancedFirmSearchResults;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls
{
    public partial class XmlSitemapGenerator : System.Web.UI.UserControl
    {
        public int TotalNews
        {
            get
            {
                return (!string.IsNullOrEmpty(totalNewsHiddenField.Value)) ? int.Parse(totalNewsHiddenField.Value) : 0;
            }
            set
            {
                totalNewsHiddenField.Value = value.ToString();
            }
        }
        public List<TDSitemapEntry> Entries
        {
            get
            {

                return JsonConvert.DeserializeObject<List<TDSitemapEntry>>(entutiesHiddenField.Value);
            }
            set
            {
                entutiesHiddenField.Value = JsonConvert.SerializeObject(value);
            }
        }

        [Serializable]
        public struct TDSitemapEntry
        {
            public int Priority { get; set; }
            public String Location { get; set; }

            public string ToXml()
            {
                return String.Format("<url><loc>{0}</loc><priority>{1}</priority></url>", Location, Priority);
            }
        }

        private const int MAX_PROCESS_AT_A_TIME = 1000;
        private const int MAX_PROCESS_AT_A_TIME_STEP_4 = 200;



        protected void Page_Load(object sender, EventArgs e)
        {
            var scriptManager = ScriptManager.GetCurrent(Page);
            scriptManager.AsyncPostBackTimeout = 36000;
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            List<TDSitemapEntry> entries = new List<TDSitemapEntry>();

            //getting site url to be included in the sitemap
            var sysConfig = Telerik.Sitefinity.Configuration.Config.Get<SystemConfig>();

            var urlHost = String.Empty;
            if (sysConfig != null && sysConfig.SiteUrlSettings != null && !String.Empty.Equals(sysConfig.SiteUrlSettings.Host))
            {
                urlHost = "https://" + sysConfig.SiteUrlSettings.Host;
            }

            if (Entries == null || (currItemIndex.Value.Equals("0") && currentStep.Value.Equals("1")))
            {
                Entries = entries;
            }

            entries = Entries;

            //getting site url to be included in the sitemap
            FillSitemapWithPages(entries, urlHost, "3");
            FillSitemapWithNews(entries, urlHost, "2");
            FillSitemapWithJobsEntries(entries, urlHost, "1");

            //FillSitemapWithProducts(entries, urlHost, "3");      
            //FillSitemapWithFirmsDirectoryEntries(entries, urlHost, "1");
            //---------
            Entries = entries;

            var newTxt = "Current Step: " + currentStep.Value + ", Index: " + currItemIndex.Value + ", Total Entries: " + Entries.Count;

            if (currentStep.Value.Equals("4"))
            {
                SaveSitemap("~/Sitemaps/sitemap-pages.xml");

                newTxt += "... completed! Accessible at: " + urlHost + ResolveUrl("~/Sitemaps/sitemap-pages.xml");

                lblProgress.Text = newTxt;
            }

            lblProgress.Text = newTxt;
        }

        protected void btnGenerateProduct_Click(object sender, EventArgs e)
        {
            List<TDSitemapEntry> entries = new List<TDSitemapEntry>();

            //getting site url to be included in the sitemap
            var sysConfig = Telerik.Sitefinity.Configuration.Config.Get<SystemConfig>();

            var urlHost = String.Empty;
            if (sysConfig != null && sysConfig.SiteUrlSettings != null && !String.Empty.Equals(sysConfig.SiteUrlSettings.Host))
            {
                urlHost = "https://" + sysConfig.SiteUrlSettings.Host;
            }

            Entries = entries;

            var newTxt = "Current Step: " + currentStep.Value + ", Index: " + currItemIndex.Value + ", Total Entries: " + Entries.Count;

            FillSitemapWithProducts(entries, urlHost, currentStep.Value);

            Entries = entries.Distinct().ToList();

            if (currentStep.Value.Equals("2"))
            {
                SaveSitemap("~/Sitemaps/sitemap-products.xml");

                newTxt += "... completed! Accessible at: " + urlHost + ResolveUrl("~/Sitemaps/sitemap-products.xml");
            }

            lblProductProgress.Text = newTxt;
        }

        protected void btnGenerateFirm_Click(object sender, EventArgs e)
        {
            List<TDSitemapEntry> entries = new List<TDSitemapEntry>();

            //getting site url to be included in the sitemap
            var sysConfig = Telerik.Sitefinity.Configuration.Config.Get<SystemConfig>();

            var urlHost = String.Empty;
            if (sysConfig != null && sysConfig.SiteUrlSettings != null && !String.Empty.Equals(sysConfig.SiteUrlSettings.Host))
            {
                urlHost = "https://" + sysConfig.SiteUrlSettings.Host;
            }

            Entries = entries;

            var newTxt = "Current Step: " + currentStep.Value + ", Index: " + currItemIndex.Value + ", Total Entries: " + Entries.Count;

            FillSitemapWithFirmsDirectoryEntries(entries, urlHost, currentStep.Value);
            Entries = entries;

            if (currentStep.Value.Equals("2"))
            {
                SaveSitemap("~/Sitemaps/sitemap-firms.xml");

                newTxt += "... completed! Accessible at: " + urlHost + ResolveUrl("~/Sitemaps/sitemap-firms.xml");
            }

            lblFirmProgress.Text = newTxt;
        }

        private void SaveSitemap(string path)
        {
            // save to sitemap.gz file
            String xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><urlset xmlns:xhtml=\"http://www.w3.org/1999/xhtml\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">";

            foreach (var entry in Entries)
            {
                xml += entry.ToXml();
            }

            xml += "</urlset>";

            var sitemapPath = HttpContext.Current.Server.MapPath(path);
            File.WriteAllText(sitemapPath, xml);
        }


        private void FillSitemapWithPages(List<TDSitemapEntry> entries, string urlHost, String stepNumber)
        {
            if (!currentStep.Value.Equals(stepNumber))
            {
                return;
            }

            int currIndex = int.Parse(currItemIndex.Value);
            int progressIndex = 0;
            int processedSoFar = MAX_PROCESS_AT_A_TIME;

            PageManager pageManager = PageManager.GetManager();
            IQueryable<PageNode> pageNodes = App.WorkWith().Pages().Where(
                pN => (pN.GetPageData() != null
                    && pN.GetPageData().Status == ContentLifecycleStatus.Live
                    && !pN.GetPageData().NavigationNode.IsBackend)
            ).Get();
            foreach (var pageItem in pageNodes)
            {
                if (currIndex <= progressIndex)
                {
                    var newSitemapEntry = new TDSitemapEntry();
                    newSitemapEntry.Priority = 1;
                    newSitemapEntry.Location = urlHost + ResolveUrl(pageItem.GetFullUrl());

                    entries.Add(newSitemapEntry);

                    currIndex++;
                    processedSoFar--;
                }

                progressIndex++;

                if (processedSoFar <= 0)
                {
                    break;
                }
            }

            if (currIndex >= pageNodes.Count())
            {
                currentStep.Value = (int.Parse(stepNumber) + 1).ToString();
                currItemIndex.Value = "0";
            }
            else
            {
                currItemIndex.Value = currIndex.ToString();
            }
        }

        private void FillSitemapWithNews(List<TDSitemapEntry> entries, string urlHost, String stepNumber)
        {
            if (!currentStep.Value.Equals(stepNumber))
            {
                return;
            }

            int currIndex = int.Parse(currItemIndex.Value);
            int processedSoFar = MAX_PROCESS_AT_A_TIME_STEP_4;

            NewsManager newsManager = NewsManager.GetManager();

            if (TotalNews <= 0)
            {
                TotalNews = newsManager.GetNewsItems().Where(newsItem => (newsItem.Status == ContentLifecycleStatus.Live) && (newsItem.Visible)).ToList().Count();
            }
            var news = newsManager.GetNewsItems().Where(newsItem => ((newsItem.Status == ContentLifecycleStatus.Live) && (newsItem.Visible))).Skip(currIndex).Take(processedSoFar).ToList();
            foreach (var newsItem in news)
            {
                var newSitemapEntry = new TDSitemapEntry();
                newSitemapEntry.Priority = 1;
                newSitemapEntry.Location = SystemManager.GetContentLocationService().GetItemDefaultLocation(newsItem).ItemAbsoluteUrl;

                entries.Add(newSitemapEntry);

                currIndex++;
            }

            if (currIndex >= TotalNews)
            {
                currentStep.Value = (int.Parse(stepNumber) + 1).ToString();
                currItemIndex.Value = "0";
            }
            else
            {
                currItemIndex.Value = currIndex.ToString();
            }
        }

        private void FillSitemapWithProducts(List<TDSitemapEntry> entries, string urlHost, String stepNumber)
        {
            if (!currentStep.Value.Equals(stepNumber))
            {
                return;
            }

            int currIndex = int.Parse(currItemIndex.Value);
            int progressIndex = 0;
            int processedSoFar = MAX_PROCESS_AT_A_TIME;

            var connectionString = SoftwareDesign.Helper.GetAptifyEntitiesConnectionString();
            var categoryIds = new List<int>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                var cmd = new SqlCommand("spGetProductsCategory__c", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = con;

                var rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    categoryIds.Add(int.Parse(rdr["ID"].ToString()));
                }

                rdr.Close();

                var totalCount = 0;

                foreach (var categoryId in categoryIds)
                {
                    cmd = new SqlCommand("SpGetProductForPreferredCurrency__c", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@CurrencyType", "Euro");
                    cmd.Parameters.AddWithValue("@ExcludeProductID", "0");
                    cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                    cmd.Parameters.AddWithValue("@ShowMeetingsLinkToClass", false);

                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        totalCount++;
                    }

                    rdr.Close();
                }

                foreach (var categoryId in categoryIds)
                {
                    if (processedSoFar <= 0)
                    {
                        break;
                    }

                    cmd = new SqlCommand("SpGetProductForPreferredCurrency__c", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@CurrencyType", "Euro");
                    cmd.Parameters.AddWithValue("@ExcludeProductID", "0");
                    cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                    cmd.Parameters.AddWithValue("@ShowMeetingsLinkToClass", false);

                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        if (currIndex <= progressIndex)
                        {
                            var productURLToUse = rdr["ProductURLToUse"].ToString();
                            var productId = rdr["ID"].ToString();

                            var newSitemapEntry = new TDSitemapEntry();
                            newSitemapEntry.Priority = 1;
                            newSitemapEntry.Location = urlHost +
                                (String.IsNullOrEmpty(productURLToUse) ? "/ProductCatalog/Product.aspx" : productURLToUse.Replace("~", "")) +
                                "?ID=" + productId;

                            entries.Add(newSitemapEntry);

                            currIndex++;
                            processedSoFar--;
                        }

                        progressIndex++;

                        if (processedSoFar <= 0)
                        {
                            break;
                        }
                    }

                    rdr.Close();
                }

                if (currIndex >= totalCount)
                {
                    currentStep.Value = (int.Parse(stepNumber) + 1).ToString();
                    currItemIndex.Value = "0";
                }
                else
                {
                    currItemIndex.Value = currIndex.ToString();
                }
            }
        }

        private void FillSitemapWithJobsEntries(List<TDSitemapEntry> entries, string urlHost, String stepNumber)
        {
            if (!currentStep.Value.Equals(stepNumber))
            {
                return;
            }

            int currIndex = int.Parse(currItemIndex.Value);
            int progressIndex = 0;
            int processedSoFar = MAX_PROCESS_AT_A_TIME;

            var doc = new XmlDocument();
            doc.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/JSExport.xml"));

            var jobUrlTmpl = "/Professional-development/Job-Search/Job-Search-Listing?jobID={0}";
            try
            {
                foreach (XmlElement el in doc.DocumentElement.ChildNodes)
                {
                    if (el.Name.Equals("Jobs"))
                    {
                        foreach (XmlElement e in el.ChildNodes)
                        {
                            if (currIndex <= progressIndex)
                            {
                                var newSitemapEntry = new TDSitemapEntry();
                                var formattedJob = e.InnerText;
                                var replacingText = "\r\n";
                                formattedJob = e.InnerText.Replace(replacingText, "");
                                formattedJob = formattedJob.Replace("#SH", "");
                                formattedJob = formattedJob.Replace("#EH", "");
                                formattedJob = formattedJob.Replace("*SB", "");
                                formattedJob = formattedJob.Replace("*EB", "");
                                formattedJob = formattedJob.Replace("\\SL", " ");
                                formattedJob = formattedJob.Replace("\\EL", " ");

                                newSitemapEntry.Priority = 1;
                                newSitemapEntry.Location = urlHost + String.Format(jobUrlTmpl, e.GetAttribute("DBID"));

                                entries.Add(newSitemapEntry);

                                currIndex++;
                                processedSoFar--;
                            }

                            progressIndex++;

                            if (processedSoFar <= 0)
                            {
                                break;
                            }
                        }

                        if (currIndex >= el.ChildNodes.Count)
                        {
                            currentStep.Value = (int.Parse(stepNumber) + 1).ToString();
                            currItemIndex.Value = "0";
                            break;
                        }
                        else
                        {
                            currItemIndex.Value = currIndex.ToString();
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
            }
        }

        private void FillSitemapWithFirmsDirectoryEntries(List<TDSitemapEntry> entries, string urlHost, String stepNumber)
        {
            if (!currentStep.Value.Equals(stepNumber))
            {
                return;
            }

            int currIndex = int.Parse(currItemIndex.Value);
            int progressIndex = 0;
            int processedSoFar = MAX_PROCESS_AT_A_TIME;

            //preparing all entries for the enhanced firms listing
            var _params = new FirmSearchParams(0);
            _params.currentPage = 1;
            var __currIndex = 0;
            var dt = _params.BuildSearchQuery();
            var outOfPages = true;
            while (dt.Rows.Count > 0 && __currIndex < dt.Rows.Count)
            {
                if (currIndex <= progressIndex)
                {
                    var row = dt.Rows[__currIndex];
                    var newSitemapEntry = new TDSitemapEntry();

                    var obj = new EnhancedListingObj()
                    {
                        Title = row["FirmName"] != null ? row["FirmName"].ToString() : String.Empty,
                        City = row["City"] != null && !String.IsNullOrEmpty(row["City"].ToString()) ? row["City"].ToString() : (row["County"] != null ? row["County"].ToString() : ""),
                        Phone = dt.Columns.Contains("ContactNo") && row["ContactNo"] != null ? row["ContactNo"].ToString() : String.Empty,
                        Url = row["WebSite"] != null ? row["WebSite"].ToString() : String.Empty,
                        ID = row["FId"] != null ? row["FId"].ToString() : String.Empty
                    };

                    newSitemapEntry.Priority = 1;
                    newSitemapEntry.Location = urlHost + obj.SinglePageUrl;
                    entries.Add(newSitemapEntry);

                    currIndex++;
                    processedSoFar--;
                }

                __currIndex++;

                progressIndex++;

                if (processedSoFar <= 0)
                {
                    outOfPages = false;
                    break;
                }

                if (dt.Rows.Count <= __currIndex)
                {
                    __currIndex = 0;
                    _params.currentPage += 1;
                    dt = _params.BuildSearchQuery();
                }
            }

            if (outOfPages)
            {
                currentStep.Value = (int.Parse(stepNumber) + 1).ToString();
                currItemIndex.Value = "0";
            }
            else
            {
                currItemIndex.Value = currIndex.ToString();
            }
        }
    }
}
