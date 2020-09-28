using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.News.Model;
using Telerik.Sitefinity.Security.Claims;

namespace SitefinityWebApp.UpdateNews
{
    public partial class UpdateNews : System.Web.UI.Page
    {
        protected NewsManager newsMgr = NewsManager.GetManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ClaimsManager.GetCurrentIdentity() == null || !ClaimsManager.GetCurrentIdentity().IsAuthenticated || !ClaimsManager.GetCurrentIdentity().IsBackendUser)
            {
                Response.Redirect("/sitefinity");
                Response.End();
                return;
            }

            if (!Page.IsPostBack)
            {
                var NewsItemsInCategory = newsMgr.GetNewsItems().Where(p => p.Status == ContentLifecycleStatus.Live && p.Visible);
                List<NewsItem> newsList = NewsItemsInCategory.ToList();
                var _json = Json.Encode("");
                int itemsUpdated = 0;
                int itemsUnpublished = 0;

                foreach (NewsItem live in newsList)
                {
                    NewsItem master = (NewsItem)newsMgr.Lifecycle.GetMaster(live);
                    if (master.ExpirationDate == null)
                    {
                        try
                        {
                            var tempItem = (NewsItem)newsMgr.Lifecycle.CheckOut(master);

                            DateTime publicationDate = master.PublicationDate;
                            DateTime expiryDate;

                            if (master.ExpirationDate == null)
                            {
                                expiryDate = publicationDate.AddYears(2);
                                tempItem.ExpirationDate = expiryDate;
                                itemsUpdated++;
                            }

                            if (tempItem.ExpirationDate < DateTime.Now)
                            {
                                live.Visible = false;
                                tempItem.Visible = false;
                                live.ApprovalWorkflowState = "Unpublished";
                                tempItem.ApprovalWorkflowState = "Unpublished";
                                newsMgr.Lifecycle.Unpublish(live);
                                itemsUnpublished++;
                            }

                            newsMgr.Lifecycle.CheckIn(tempItem);
                            newsMgr.SaveChanges();
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }

                _json = Json.Encode(new
                {
                    itemsCount = newsList.Count,
                    itemsUpdated = itemsUpdated,
                    itemsUnpublished = itemsUnpublished,
                });

                Response.Clear();
                Response.ContentType = "application/json; charset=utf-8";
                Response.Write(_json);
                Response.End();
            }
        }
    }
}