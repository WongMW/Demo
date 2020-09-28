using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.FirmsSearch
{
    public partial class EnhancedFirmTopResults : Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
    {
        #region Get/Set methods
        public List<EnhancedListingObj> Results
        {
            get
            {
                var val = (List<EnhancedListingObj>)ViewState["EnhancedSearchTopListing_Results"];

                return val != null ? val : new List<EnhancedListingObj>();
            }
            set
            {
                ViewState["EnhancedSearchTopListing_Results"] = value;
                CurrentSearchListing = value;
            }
        }
        public List<EnhancedListingObj> CurrentSearchListing
        {
            get
            {
                var val = (List<EnhancedListingObj>)Session["EnhancedSearchTopListing_PreviousResults"];

                return val != null ? val : new List<EnhancedListingObj>();
            }
            set
            {
                Session["EnhancedSearchTopListing_PreviousResults"] = value;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            // SAMPLE DATA
            if (!Page.IsPostBack)
            {
                // change visibility state to be non visible totally
                this.Visible = false;
                
                LoadSearchResults();
            }
            // -----
        }

        /// <summary>
        /// Call this method after you have set County, City and Company properties of the user control
        /// </summary>
        public void LoadSearchResults()
        {
            if(Results != null && Results.Count > 0)
            {
                resultsTable.DataSource = Results;
                resultsTable.DataBind();
                this.Visible = true;
            } else
            {
                this.Visible = false;
            }
        }
    }

    [Serializable]
    public class EnhancedListingObj
    {
        private String url;

        public String ID { get; set; }
        public String Image { get; set; }
        public String Title { get; set; }
        public String City { get; set; }
        public String Phone { get; set; }
        public int ParentID { get; set; }
        public String Url
        {
            get
            {
                return url;
            }
            set
            {
                url = value;

                // preprocessing URL if it does not have HTTP in front
                url = url.Trim();
                if (!String.IsNullOrEmpty(url) && (!url.Contains("http://") || url.Contains("https://")))
                {
                    url = "http://" + url;
                }
            }
        }

        private String customSinglePageUrl = String.Empty;

        public String SinglePageUrl
        {
            get
            {
                if(!String.IsNullOrEmpty(customSinglePageUrl))
                {
                    return customSinglePageUrl;
                }

                String url = String.Empty;
                if(String.IsNullOrEmpty(this.Title) || String.IsNullOrEmpty(this.ID))
                {
                    return "#";
                }
                url = this.Title.ToLower()
                    .Replace("   ", " ")
                    .Replace("  ", " ")
                    .Replace(" ", "-")
                    .Replace("---", "-")
                    .Replace("--", "-")
                    .Replace("/", "")
                    .Replace("'", "")
                    .Replace("&", "and")
                    .Trim();

                // lets append ID to the end of the url
                url += "-" + this.ID;

                String singlePageURL = "enhancedlistingfirmdetails";
                String configUrl = ConfigurationManager.AppSettings["enhancedListingSingleFirmPageURL"];
                if(!String.IsNullOrEmpty(configUrl))
                {
                    singlePageURL = configUrl;
                }

                if(singlePageURL.StartsWith("/"))
                {
                    singlePageURL = singlePageURL.Substring(1);
                }

                return "/" + singlePageURL + "?firm=" + url;
            }
            set
            {
                customSinglePageUrl = value;
            }
        }
    }
}
