using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Newtonsoft.Json.Linq;
using Telerik.Sitefinity;

namespace SitefinityWebApp.migration
{
    public partial class getMigrationDetails : System.Web.UI.Page
    {
        Dictionary<string, string> urlMap = new Dictionary<string, string>();
        Dictionary<string, JArray> pageProperties = new Dictionary<string, JArray>();

        protected void Page_Load(object sender, EventArgs e)
        {
            var pages = App.WorkWith()
                           .Pages()
                           .Get()
                           .ToList();

            String totalPagesInSitefinity = pages.Count.ToString();

            JObject o = JObject.Parse(System.IO.File.ReadAllText(Request.MapPath("exportedProperties.json")));
            JArray allPagesForProperties = JArray.Parse(System.IO.File.ReadAllText(Request.MapPath("allPagesForProperties.json")));

            /*
             * pageID-URL => [pageID, title (hierarchy), URL, Full OLD URL]
             */

            foreach (var prop in o.Properties())
            {
                if (prop.Name.Contains("-URL"))
                {
                    urlMap.Add(prop.Name.Replace("-URL", ""), ((JArray)o[prop.Name])[2].ToString());
                    continue;
                }

                pageProperties.Add(prop.Name, (o[prop.Name]).GetType() == typeof(JArray) ? (JArray)o[prop.Name] : new JArray());
            }

            int totalPagesToMigrate = allPagesForProperties.Count;

            var json = Json.Encode(new
            {
                totalPagesToMigrate = totalPagesToMigrate,
                totalPagesInSitefinity = totalPagesInSitefinity
            });

            Response.Clear();
            Response.ContentType = "application/json; charset=utf-8";
            Response.Write(json);
            Response.End();
        }
    }
}