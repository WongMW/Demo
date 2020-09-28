using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.Pages;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.GetControlls
{
    public partial class GetPageId_c : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var pm = PageManager.GetManager();

            var pageNodes = pm.GetPageNodes().Where(pn => (pn.Page.Status == ContentLifecycleStatus.Live) && (pn.Page.Visible == true));

            var s = "{";
            bool first = true;
            foreach (var pageNode in pageNodes)
            {
                if (!string.IsNullOrEmpty(pageNode.Page.HtmlTitle.Value))
                {
                    var url = (pageNode == null) ? "" : pageNode.GetUrl();
                    s += (first) ? "" : ",";
                    s += "\"" + pageNode.Page.HtmlTitle.Value + "\": \"" + pageNode.Page.Id.ToString() + " " + url + "\"\n";
                    first = false;
                }
            }
            s += "}";

            File.WriteAllText(HttpContext.Current.Server.MapPath("/id.json"), s);

            Response.ContentType = "json/application";
            Response.AppendHeader("Content-Disposition", "attachment; filename=id.json");
            Response.TransmitFile(HttpContext.Current.Server.MapPath("/id.json"));
            Response.End();
        }
    }
}