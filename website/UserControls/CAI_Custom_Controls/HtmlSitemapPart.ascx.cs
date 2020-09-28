using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RelatedData;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls
{
    public partial class HtmlSitemapPart : Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
    {
        public PageNode Node { get; set; }
        public Dictionary<PageNode, Object> SubNodes { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Node != null)
                {
                    var sysConfig = Telerik.Sitefinity.Configuration.Config.Get<Telerik.Sitefinity.Services.SystemConfig>();

                    String urlHost = String.Empty;
                    if (sysConfig != null && sysConfig.SiteUrlSettings != null && !String.Empty.Equals(sysConfig.SiteUrlSettings.Host))
                    {
                        urlHost = "https://" + sysConfig.SiteUrlSettings.Host;
                    }

                    lnkTitle.Text = Node.Title.PersistedValue;
                    lnkTitle.NavigateUrl = (!String.IsNullOrEmpty(Node.GetDefaultUrl()) && !Node.UrlName.PersistedValue.StartsWith("/") ? Node.GetDefaultUrl() : Node.UrlName.PersistedValue);
                    if(!lnkTitle.NavigateUrl.StartsWith("https://") && !lnkTitle.NavigateUrl.StartsWith("http://"))
                    {
                        lnkTitle.NavigateUrl = urlHost + lnkTitle.NavigateUrl;
                    }
                }

                if (SubNodes != null && SubNodes.Keys.Count > 0)
                {
                    sitemap.DataSource = SubNodes;
                    sitemap.DataBind();
                }
            }
        }

        protected void sitemap_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem == null)
            {
                return;
            }

            var d = (KeyValuePair<PageNode, object>)e.Item.DataItem;

            var node = d.Key;
            var kids = (Dictionary<PageNode, object>)d.Value;

            var cntrl = (HtmlSitemapPart)LoadControl("~/UserControls/CAI_Custom_Controls/HtmlSitemapPart.ascx");
            e.Item.Controls.Add(cntrl);
            cntrl.Node = node;
            cntrl.SubNodes = kids;
        }
    }
}