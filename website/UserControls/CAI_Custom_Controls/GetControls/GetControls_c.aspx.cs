using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.Pages;
using System.IO;
using System.Xml;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using System.Web.Services;
using System.Web.Script.Serialization;
using Telerik.Sitefinity.Data;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.GetControls
{
    public partial class GetControls_c : System.Web.UI.Page
    {
        private ToolboxesConfig _config;
        private Toolbox _controls;
        protected void Page_Load(object sender, EventArgs e)
        {
            var configManager = ConfigManager.GetManager();
            _config = configManager.GetSection<ToolboxesConfig>();
            _controls = _config.Toolboxes["PageControls"];
            var scriptManager = ScriptManager.GetCurrent(Page);
            scriptManager.AsyncPostBackTimeout = 36000;
        }

        protected void GetControls_Click(object sender, EventArgs e)
        {
            var usedControls = GetUsedControls();

            bool first = true;

            string usedControlsTxt = "";

            foreach (var item in usedControls)
            {
                usedControlsTxt += (first) ? "Actual Name; Toolbox Name; Path; PageUrl; \n\n\n\n" : " \n";
                usedControlsTxt += $"{item.ActualName}; {item.ToolboxName}; {item.Path}; {item.PageUrl};";
                first = false;
            }

            File.WriteAllText(HttpContext.Current.Server.MapPath("/UsedControls.csv"), usedControlsTxt);

            Response.ContentType = "text";
            Response.AppendHeader("Content-Disposition", "attachment; filename=UsedControls.csv");
            Response.TransmitFile(HttpContext.Current.Server.MapPath("/UsedControls.csv"));
            Response.End();
        }

        protected void GetPagesButton_Click(object sender, EventArgs e)
        {
            var txt = "";
            var controlName = searchPagesTextBox.Text.Trim(' ');
            if (!string.IsNullOrEmpty(controlName))
            {
                var resultPagesList = GetPagesByControlName(controlName);

                txt = "<table> <tr> <th>Page id</th> <th>Page name</th>  <th>Url</th> </tr>";
                foreach (var pageInfo in resultPagesList)
                {
                    txt += "<tr> <td>" + pageInfo.Pageid + "</td><td>" + pageInfo.PageName + "</td><td>" + pageInfo.PageUrl + "</td> </tr>";
                }

                txt += "</table>";

                if (resultPagesList.Count == 0)
                {
                    txt = "Nothing found";
                }
            }
            else
            {
                txt = "Nothing found";
            }
            pagesListLabel.Text = txt;
        }

        protected void GetPageControlButton_Click(object sender, EventArgs e)
        {
            var txt = "";
            var pageName = searchPageControlsTextBox.Text.Trim(' ');
            try
            {
                var resultPageControlsList = GetPageControls(pageName);
                txt = "<table> <tr> <th>Actual Name</th> <th>Toolbox Name</th>  <th>Path</th> </tr>";
                foreach (var controlInfo in resultPageControlsList)
                {
                    txt += "<tr> <td>" + controlInfo.ActualName + "</td><td>" + controlInfo.ToolboxName + "</td><td>" + controlInfo.Path + "</td> </tr>";
                }

                txt += "</table>";

                if (resultPageControlsList.Count == 0)
                {
                    txt = "Nothing found";
                }
            }
            catch
            {
                txt = "Nothing found";
            }
            controlsListLabel.Text = txt;
        }


        private List<UsedControlInfo> GetUsedControls(string pageId = null, bool enabledUrlPath = true)
        {
            List<ControlPathPageId> usedControlsInfoList = GetUsedControlsQuery(pageId);

            var usedControlsWithoutChild = (enabledUrlPath)
                ? usedControlsInfoList.Select(x => new UsedControlInfo(x.Path, GetUrlPathByPageId(x.PageId))).ToList()
                : usedControlsInfoList.Select(x => new UsedControlInfo(x.Path)).ToList();

            var usedControls = GetChildrenControls(usedControlsWithoutChild);
            return usedControls;
        }

        private static List<ControlPathPageId> GetUsedControlsQuery(string pageId)
        {
            var pm = PageManager.GetManager();
            var context = (pm.Provider as IOpenAccessDataProvider).GetContext();
            var wherePageId = (string.IsNullOrEmpty(pageId) ? "" : $"and od.page_id ='{pageId}'");
            var usedControlsInfoList = context.ExecuteQuery<ControlPathPageId>(
                $@"select  object_type Path, convert(nvarchar(100), page_id)  PageId 
                from(
                    select distinct od.object_type, od.page_id , ROW_NUMBER() OVER(PARTITION BY od.object_type ORDER BY od.object_type desc ) rn
                    from sf_object_data od
                    inner join sf_page_data pd on od.page_id = pd.content_id
                    inner join sf_page_node pn on pn.content_id = pd.content_id
                    where pd.status = {(int)ContentLifecycleStatus.Live} and pd.visible = 1 {wherePageId}
                ) a
                where rn =1").ToList();
            return usedControlsInfoList;
        }

        private List<PageInfo> GetPagesByControlName(string controlName)
        {
            var targetControl = GetUsedControls(null,false).FirstOrDefault(c => c.ToolboxName == controlName);

            List<PageInfo> resultQuery = new List<PageInfo>();

            resultQuery.AddRange(GetPagesByControlNameQuery(controlName));

            if (targetControl != null)
            {
                foreach (var rootControlName in targetControl.RootControlsList)
                {
                   resultQuery.AddRange(GetPagesByControlNameQuery(rootControlName));
                }
            }

            foreach (var pageInfo in resultQuery)
            {
                pageInfo.PageUrl = GetUrlPathByPageId(pageInfo.Pageid);
            }

            return resultQuery;
        }

        private List<PageInfo> GetPagesByControlNameQuery(string controlName)
        {
            var pm = PageManager.GetManager();
            var context = (pm.Provider as IOpenAccessDataProvider).GetContext();

            var resultQuery = context.ExecuteQuery<PageInfo>(
                @"SELECT convert(nvarchar(100), sf_object_data.page_id) as Pageid, sf_page_node.url_name_ as PageName
                FROM sf_object_data 
                inner  join sf_page_data  on sf_page_data.content_id = sf_object_data.page_id 
                left  join sf_page_node  on sf_page_data.page_node_id = sf_page_node.id 
                where ( (sf_object_data.object_type like '%" + controlName + ".ascx') or (sf_object_data.object_type like '%" + controlName + "'))" +
                $"and sf_page_data.status = {(int)ContentLifecycleStatus.Live} and sf_page_data.visible = 1 and page_node_id is not null").ToList();
            
            if( resultQuery == null) resultQuery = new List<PageInfo>();
            
            return resultQuery;
        }

        private List<UsedControlInfo> GetChildrenControls(List<UsedControlInfo> controlInfoList,string rootControlName=null)
        {
            bool controlAdded = false; 
            var result = new List<UsedControlInfo>();
            result.AddRange(controlInfoList);

            foreach (var controlInfo in controlInfoList) 
            {
                if (File.Exists(Server.MapPath(controlInfo.Path)))
                {
                    var lines = File.ReadAllLines(Server.MapPath(controlInfo.Path)).ToList();
                    var linesWithControlRegister = lines.Where(l => l.Contains("Register") && l.Contains("TagName") && l.Contains("Src")).ToList();
                   
                    foreach (var line in linesWithControlRegister)
                    {
                        var intermediateSubstring = line.Substring(line.IndexOf("Src=") + 5);
                        var childControlPath = intermediateSubstring.Substring(0, intermediateSubstring.IndexOf("\""));

                        if (!string.IsNullOrEmpty(childControlPath))
                        {
                            if (!result.Any(x=>x.Path == childControlPath))
                            {
                                var newControlInfo = new UsedControlInfo(childControlPath);
                                newControlInfo.PageUrl = controlInfo.PageUrl;
                                result.Add(newControlInfo);
                                controlAdded = true;
                            }

                            var root = (string.IsNullOrEmpty(rootControlName)) ? controlInfo.ToolboxName : rootControlName;
                            
                            if (!result.FirstOrDefault(x => x.Path == childControlPath).RootControlsList.Any(cName=> cName == root))
                                result.FirstOrDefault(x => x.Path == childControlPath)?.RootControlsList.Add(root);
                        }
                    }
                }
            }

            if (controlAdded)
                return GetChildrenControls(result, rootControlName);

            return result;
        }
        
        private string GetUrlPathByPageId(string pageId)
        {
            var pageUrlInfo = GetPageUrlInfoQuery(pageId);

            string resultUrl = pageUrlInfo.UrlName;
            resultUrl += (!string.IsNullOrEmpty(pageUrlInfo.Extension)) ? pageUrlInfo.Extension : "";

            while (!string.IsNullOrEmpty(pageUrlInfo.ParentId))
            {
                pageUrlInfo = GetPageUrlInfoQuery(pageUrlInfo.ParentId);

                if (pageUrlInfo == null)
                {
                    resultUrl = "/" + resultUrl;
                    break;
                }

                resultUrl = string.IsNullOrEmpty(pageUrlInfo.ParentId) ? "/" + resultUrl : pageUrlInfo.UrlName + "/" + resultUrl;
            }

            return resultUrl;
        }

        private static PageUrlInfo GetPageUrlInfoQuery(string pageId)
        {
            var pm = PageManager.GetManager();
            var context = (pm.Provider as IOpenAccessDataProvider).GetContext();
            var getPageNodeQuery = context.ExecuteQuery<PageUrlInfo>(
                @"SELECT  convert(nvarchar(100), content_id) ContentId, convert(nvarchar(100), parent_id) ParentId, extension Extension, url_name_ UrlName
                FROM sf_page_node 
                where content_id ='" + pageId + "'").ToList();

            return getPageNodeQuery.FirstOrDefault();
        }

        private List<UsedControlInfo> GetPageControls(string pageName)
        {
            var pm = PageManager.GetManager();
            var context = (pm.Provider as IOpenAccessDataProvider).GetContext();
            var resultQuery = context.ExecuteQuery<PageInfo>(
                $@"SELECT convert(nvarchar(100), content_id) as Pageid, url_name_ as PageName
                FROM sf_page_node 
                where url_name_='{pageName}'").ToList();

            var pageInfo = resultQuery.FirstOrDefault(x=>!string.IsNullOrEmpty(x.Pageid));
            if (pageInfo == null)
                return new List<UsedControlInfo>();

            return GetUsedControls(pageInfo.Pageid);
        }

        private class PageInfo
        {
            public string Pageid { get; set; }
            public string PageName { get; set; }
            public string PageUrl { get; set; }
        }

        private class PageUrlInfo
        {
            public string ContentId { get; set; }
            public string ParentId { get; set; }
            public string UrlName { get; set; }
            public string Extension { get; set; }
        }

        private class ControlPathPageId
        {
            public string Path { get; set; }
            public string PageId { get; set; }
        }

        private class UsedControlInfo
        {
            public UsedControlInfo(string path, string pageUrl = null)
            {
                Path = path;
                ToolboxName = System.IO.Path.GetFileName(path).Replace(".ascx","");
                ActualName = System.IO.Path.GetFileName(path);
                PageUrl = pageUrl;
                RootControlsList = new List<string>();
            }
            public string ToolboxName { get; set; }
            public string ActualName { get; set; }
            public string Path { get; set; }
            public string PageUrl { get; set; }
            public List<string> RootControlsList { get; set; }
        }
    }
}
