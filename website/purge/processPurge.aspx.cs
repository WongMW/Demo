using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web.Helpers;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Fluent.Pages;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;

namespace SitefinityWebApp.purge
{
    public partial class processPurge : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = Request.QueryString["item"].ToString();
            if (!string.IsNullOrEmpty(url))
            {
                PageNode page = App.WorkWith().Pages().LocatedIn(PageLocation.Frontend).Where(p => p.UrlName.Equals(url)).Get().FirstOrDefault();

                var _json = Json.Encode("");

                if (page != null)
                {
                    List<PageNode> purgedPages = PurgePage(page);
                    _json = Json.Encode(new
                    {
                        pageCount = purgedPages.Count
                    });
                }

                Response.Clear();
                Response.ContentType = "application/json; charset=utf-8";
                Response.Write(_json);
                Response.End();

                return;
            }
        }

        private static List<PageNode> PurgePage(PageNode selectedNode)
        {
            //get all children nodes
            List<PageNode> nodes = App.WorkWith().Pages().LocatedIn(PageLocation.Frontend).Where(p => p.Parent.Id.Equals(selectedNode.Id)).Get().ToList();
            List<PageNode> children = new List<PageNode>();
            string status = "Parent Name: " + selectedNode.Parent.UrlName + " Current Name: " + selectedNode.UrlName + "\r\n";

            //if children
            if (nodes.Count > 0)
            {
                //for each child, recursively do the same
                foreach (PageNode page in nodes)
                {
                    children = PurgePage(page);
                }
            }

            //delete
            PageManager pageManager = PageManager.GetManager();
            PageData pageToDelete =
                pageManager.GetPageDataList().FirstOrDefault(p => p.NavigationNode.Id == selectedNode.Id);
            if (pageToDelete != null)
            {
                status += "DELETING PAGE: " + selectedNode.UrlName + "\r\n";
                pageManager.Delete(pageToDelete);
                pageManager.SaveChanges();
                status += "DELETED \r\n";
            }

            List<PageNode> pages = nodes.Concat(children).ToList();
            StreamWriter file = new StreamWriter("c:\\logs\\cai\\purgedata.txt", true);
            file.WriteLine(status);
            file.Close();

            return pages;
        }
        
    }
}