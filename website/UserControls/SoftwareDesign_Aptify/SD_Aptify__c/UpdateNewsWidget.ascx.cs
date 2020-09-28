using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Aptify.Framework.Web.eBusiness;
using Telerik.Web.UI;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.UI;
using DocumentFormat.OpenXml.Office.CustomUI;
using ServiceStack;
using Telerik.OpenAccess;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.News.Model;


namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c
{
    public partial class UpdateNewsWidget : Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
    {

        protected NewsManager newsMgr = NewsManager.GetManager();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                var NewsItemsInCategory = newsMgr.GetNewsItems().Where(p => p.Status == ContentLifecycleStatus.Master);
                List<NewsItem> newsList = NewsItemsInCategory.ToList();

                foreach (NewsItem item in newsList)
                {
                    if (item.ExpirationDate == null)
                    {
                        var tempItem = (NewsItem)newsMgr.Lifecycle.CheckOut(item);

                        DateTime publicationDate = item.PublicationDate;
                        DateTime expiryDate = publicationDate.AddYears(2);

                        tempItem.ExpirationDate = expiryDate;
                        newsMgr.Lifecycle.CheckIn(tempItem);
                    }
                }
            }
        }
    }
}
