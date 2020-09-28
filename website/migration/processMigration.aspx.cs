using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity;
using System.Web.Helpers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using System.IO;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.Model;

namespace SitefinityWebApp.migration
{
    public partial class processMigration : System.Web.UI.Page
    {
        SqlConnection externalSqlConnection;

        Dictionary<string, string> urlMap = new Dictionary<string, string>();
        Dictionary<string, JArray> pageProperties = new Dictionary<string, JArray>();

        Guid TemplateCAIBase = new Guid("da060148-08b3-649f-a7d8-ff000079c5aa");
        String PlaceholderBaseTemplateMainContent = "T4A303854026_Col00";

        Guid TemplateInnerOption2 = new Guid("d9500148-08b3-649f-a7d8-ff000079c5aa");
        String PlaceholderInnerOption2MainContent = "TD51CD774019_Col01";
        String PlaceholderInnerOption2Sidebar = "TD51CD774019_Col00";

        Guid TemplateInnerOption1 = new Guid("ae0c0148-08b3-649f-a7d8-ff000079c5aa");
        String PlaceholderInnerOption1MainContent = "T51ED2A20017_Col01";
        String PlaceholderInnerOption1Sidebar = "T51ED2A20016_Col00";

        JObject linksDetails = null;
        JObject filesDetails = null;
        List<String> linksList = new List<string>();
        List<String> filesList = new List<String>();

        String sqlKeywords = "select tblKeyword.Keyword from tblPageKeyword JOIN tblKeyword ON tblKeyword.pkID = tblPageKeyword.fkKeywordID where fkLanguageBranchID = 1 and tblPageKeyword.fkPageID = {0}";
        String sqlCategories = "select pkID, fkPageID, CategoryName, CategoryDescription from tblCategoryPage JOIN tblCategory ON tblCategory.pkID = tblCategoryPage.fkCategoryID where fkPageID = {0}";

        protected void Page_Load(object sender, EventArgs e)
        {
            int itemIndex = int.Parse(Request.QueryString["item"]);
            bool onlyLinks = !String.IsNullOrEmpty(Request.QueryString["onlyLinks"]);
            bool onlyNewsItems = !String.IsNullOrEmpty(Request.QueryString["news"]);
            bool onlyPodcasts = !String.IsNullOrEmpty(Request.QueryString["podcasts"]);
            bool onlyCreateCategories = !String.IsNullOrEmpty(Request.QueryString["createCats"]);
            String specificTypeID = Request.QueryString["specificTypeID"];

            if (onlyCreateCategories)
            {
                String eMessage = "";

                var categories = MakeEnterpriseDBQuery("select pkID, fkParentID, CategoryName, CategoryDescription from tblCategory WHERE fkParentID IS NOT NULL ORDER BY pkID, fkParentID ASC", 
                    new Dictionary<string, int>()
                    {
                        { "pkID", 0 },
                        { "fkParentID", 1 },
                        { "CategoryName", 2 },
                        { "CategoryDescription", 3 }
                    }
                );

                foreach (var cat in categories)
                {
                    // lets find parent
                    Dictionary<string, object> parentCat = categories.Find(c => c["pkID"].ToString().Equals(cat["fkParentID"].ToString()));

                    SDMigrationSnippets.CreateHierarchicalTaxonomyAndTaxon(
                        cat["CategoryName"].ToString(),
                        cat["CategoryDescription"].ToString(),
                        Regex.Replace(cat["CategoryName"].ToString(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-") + "-" + cat["pkID"],
                        parentCat != null ? Regex.Replace(parentCat["CategoryName"].ToString(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-") + "-" + parentCat["pkID"] : ""
                    );
                }

                var _json = Json.Encode(new
                {
                    currentPage = itemIndex,
                    totalPages = 0,
                    error = !String.IsNullOrEmpty(eMessage),
                    ex = eMessage
                });

                Response.Clear();
                Response.ContentType = "application/json; charset=utf-8";
                Response.Write(_json);
                Response.End();

                return;
            }

            var pages = App.WorkWith()
                           .Pages()
                           .Get()
                           .ToList();

            var totalPages = pages.Count.ToString();

            JObject o = JObject.Parse(System.IO.File.ReadAllText(Request.MapPath("exportedProperties.json")));
            JArray allPagesForProperties = JArray.Parse(System.IO.File.ReadAllText(Request.MapPath("allPagesForProperties.json")));
            linksDetails = JObject.Parse(System.IO.File.ReadAllText(Request.MapPath("linksOutput.json")));
            filesDetails = JObject.Parse(System.IO.File.ReadAllText(Request.MapPath("foundFiles.json")));

            foreach (var prop in filesDetails.Properties())
            {
                filesList.Add(prop.Name);
            }

            foreach (var prop in linksDetails.Properties())
            {
                linksList.Add(prop.Name);
            }

            // checking if only links
            if (onlyLinks)
            {
                var eMessage = "";

                var episerverPages = App.WorkWith().Pages().Where(x =>
                        !String.IsNullOrEmpty(x.GetString("EpiserverUrlPath"))
                    ).Get();

                var page = episerverPages.ToArray()[itemIndex];

                if (page != null)
                {
                    var pageManager = PageManager.GetManager();
                    var temp = pageManager.EditPage(page.GetPageData().Id);

                    if (temp != null)
                    {
                        // Remove the first child control in the controls collection
                        if (temp.Controls.Count > 0)
                        {
                            List<PageDraftControl> newControls = new List<PageDraftControl>();
                            Dictionary<Guid, String> newControlValues = new Dictionary<Guid,string>();
                            foreach (var control in temp.Controls)
                            {
                                if (control.Caption.Equals("Content Block"))
                                {
                                    foreach (var p in control.Properties)
                                    {
                                        if (p.Name.Equals("Html"))
                                        {
                                            String newValue = ProcessPageLinks(p.Value);
                                            if (!newValue.Equals(p.Value))
                                            {
                                                p.Value = newValue;
                                                temp.Controls.Remove(control);
                                                newControls.Add(control);
                                                newControlValues.Add(control.Id, newValue);
                                            }
                                        }
                                    }
                                }
                            }

                            pageManager.PagesLifecycle.CheckIn(temp);
                            pageManager.SaveChanges();

                            foreach (var control in newControls)
                            {
                                SDMigrationSnippets.AddContentControlToPage(this, page.PageId, control.PlaceHolder, newControlValues[control.Id], control.SiblingId);
                            }
                        }

                    }
                }

                var _json = Json.Encode(new
                {
                    currentPage = itemIndex,
                    totalPages = episerverPages.Count(),
                    error = !String.IsNullOrEmpty(eMessage),
                    ex = eMessage
                });

                Response.Clear();
                Response.ContentType = "application/json; charset=utf-8";
                Response.Write(_json);
                Response.End();

                return;
            }

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

            if (!String.IsNullOrEmpty(specificTypeID))
            {
                JArray newItems = new JArray();

                foreach (JArray itm in allPagesForProperties)
                {
                    if (itm.ToArray().Length >= 6)
                    {
                        JObject details = (JObject)itm[5];
                        String fkPageTypeID = details["fkPageTypeID"].ToString();

                        if (fkPageTypeID.Equals(specificTypeID))
                        {
                            newItems.Add(itm);
                        }
                    }
                }

                allPagesForProperties = newItems;
            }
            

            JArray item = (JArray)allPagesForProperties[itemIndex];

            String errorMessage = "";
            String urlError = "";

            if (item.ToArray().Length >= 6)
            {
                JObject details = (JObject)item[5];
                String fkPageTypeID = details["fkPageTypeID"].ToString();
                String pageID = details["pkID"].ToString();
                String pageName = details["Name"].ToString();
                String relativeUrl = urlMap[pageID];

                try
                {
                    if (onlyPodcasts)
                    {
                        if (fkPageTypeID.Equals("305") || fkPageTypeID.Equals("306"))
                        {
                            // lets not process couple of URLs
                            if (!relativeUrl.Replace("//", "/").Equals("/Accountancy-Ireland-Site"))
                            {
                                ProcessPage(pageID, fkPageTypeID, pageName, pageProperties[pageID], relativeUrl);
                            }
                        }
                    }
                    else if (!String.IsNullOrEmpty(specificTypeID))
                    {
                        if (fkPageTypeID.Equals(specificTypeID))
                        {
                            ProcessPage(pageID, fkPageTypeID, pageName, pageProperties[pageID], relativeUrl);
                        }
                    }
                    // checking if only news items
                    else if ((onlyNewsItems && fkPageTypeID.Equals("331") && isNewsArticleUrl(relativeUrl, fkPageTypeID)) || !onlyNewsItems)
                    {
                        // lets not process couple of URLs
                        if (!relativeUrl.Replace("//", "/").ToLower().Equals("/accountancy-ireland-site") &&
                            !relativeUrl.Replace("//", "/").ToLower().Equals("/corporate-site/cpd") &&
                            relativeUrl.Replace("//", "/").ToLower().IndexOf("house-site") < 0 &&
                            relativeUrl.Replace("//", "/").ToLower().IndexOf("about-us") < 0 &&
                            relativeUrl.Replace("//", "/").ToLower().IndexOf("prospective-students") < 0)
                        {
                            ProcessPage(pageID, fkPageTypeID, pageName, pageProperties[pageID], relativeUrl);
                        }
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = ex.ToString();
                    urlError = relativeUrl;
                }
            } else {
                errorMessage = "Item has less than 6 columns";
            }

            var json = Json.Encode(new
            {
                currentPage = itemIndex,
                totalPages = allPagesForProperties.Count,
                error = !String.IsNullOrEmpty(errorMessage),
                ex = errorMessage,
                urlError = urlError
            });

            Response.Clear();
            Response.ContentType = "application/json; charset=utf-8";
            Response.Write(json);
            Response.End();
        }

        private int GetPublishedYearFromUrl(string url)
        {
            var years = new List<int> { 2000, 2001, 2002, 2003, 2004, 2005, 2006, 2007, 2008, 2009, 2010, 2011, 2012, 2013, 2014, 2015, 2016 };
            var found = false;
            for (var y = 0; !found && y < years.Count; y++)
            {
                if (url.EndsWith(years[y].ToString()) || url.EndsWith(years[y].ToString() + "/") || url.IndexOf("/" + years[y].ToString() + "/") >= 0)
                {
                    return years[y];
                }
            }

            return 0;
        }

        private int GetPublishedMonthFromUrl(string url)
        {
            var months = new List<String>() { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            var years = new List<int> { 2000, 2001, 2002, 2003, 2004, 2005, 2006, 2007, 2008, 2009, 2010, 2011, 2012, 2013, 2014, 2015, 2016 };
            var found = false;
            for (var y = 0; !found && y < years.Count; y++)
            {
                for (var m = 0; !found && m < months.Count; m++)
                {
                    if (url.IndexOf(years[y] + "/" + months[m]) >= 0)
                    {
                        return m + 1;
                    }
                }
            }

            return 0;
        }

        private DateTime? GetPublishedDateFromUrl(string url)
        {
            var months = new List<String>() { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            var years = new List<int> { 2000, 2001, 2002, 2003, 2004, 2005, 2006, 2007, 2008, 2009, 2010, 2011, 2012, 2013, 2014, 2015, 2016 };
            var found = false;
            for (var y = 0; !found && y < years.Count; y++)
            {
                for (var m = 0; !found && m < months.Count; m++)
                {
                    if (url.IndexOf(years[y] + "/" + months[m]) >= 0)
                    {
                        // by default first date of the month
                        DateTime d = new DateTime(years[y], m + 1, 1);

                        return d;
                    }
                }
            }

            return null;
        }

        private bool isNewsArticleUrl(string url, string pageTypeId)
        {
            var months = new List<String>() { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            var years = new List<int> { 2000, 2001, 2002, 2003, 2004, 2005, 2006, 2007, 2008, 2009, 2010, 2011, 2012, 2013, 2014, 2015, 2016 };
            var found = false;
            for(var y = 0; !found && y < years.Count; y++) {
                for(var m = 0; !found && m < months.Count; m++) {
                    if(url.IndexOf(years[y] + "/" + months[m]) >= 0 ) {
                        return true;
                    }
                }
            }

            if (pageTypeId.Equals("331"))
            {
                return true;
            }

            return false;
        }

        private JObject GetPropertyByKey(String key, JArray properties)
        {
            foreach (var prop in properties)
            {
                var p = (JObject)prop;

                if (p["Name"].ToString().Equals(key))
                {
                    return p;
                }
            }

            return null;
        }

        private Guid CreateAllPagesForEpiserverURL(String url)
        {
            var parts = url.Split('/');

            // case where URL is the root like : /Corporate Site
            if (parts.Length == 2)
            {
                return Guid.Empty;
            }

            String urlSoFar = "";
            Guid lastCreatedId = Guid.Empty;

            for (int i = 1; i < parts.Length - 1; i++)
            {
                String urlName = parts[i];
                String partUrl = urlSoFar + "/" + urlName;

                Guid parentPageId = SDMigrationSnippets.GetPageByEpiserverUrlPath(urlSoFar);

                if (SDMigrationSnippets.GetPageByEpiserverUrlPath(partUrl).Equals(Guid.Empty))
                {
                    lastCreatedId = SDMigrationSnippets.CreatePageNativeAPI(urlName.Replace('-', ' '), parentPageId, String.Empty, partUrl, String.Empty, String.Empty, String.Empty);
                }
                else
                {
                    lastCreatedId = SDMigrationSnippets.GetPageByEpiserverUrlPath(partUrl);
                }

                urlSoFar = partUrl;
            }

            return lastCreatedId;
        }

        private String extractStringFromProperty(JObject prop)
        {
            if (prop == null)
            {
                return String.Empty;
            }

            if (!String.IsNullOrEmpty(prop["String"].ToString()))
            {
                return prop["String"].ToString();
            }

            if (!String.IsNullOrEmpty(prop["LongString"].ToString()))
            {
                return prop["LongString"].ToString();
            }

            if (!String.IsNullOrEmpty(prop["PageLink"].ToString()))
            {
                return prop["PageLink"].ToString();
            }

            if (!String.IsNullOrEmpty(prop["Date"].ToString()))
            {
                return prop["Date"].ToString();
            }

            return String.Empty;
        }

        private Double? extractNumberFromProperty(JObject prop)
        {
            if (prop == null)
            {
                return null;
            }

            if (!String.IsNullOrEmpty(prop["Number"].ToString()))
            {
                return double.Parse(prop["Number"].ToString());
            }

            if (!String.IsNullOrEmpty(prop["FloatNumber"].ToString()))
            {
                return double.Parse(prop["FloatNumber"].ToString());
            }

            return null;
        }

        Dictionary<String, String> fileNamesCache = new Dictionary<string, string>();

        public String ProcessPageFiles(String content, String ext)
        {
            return ProcessPageFiles(content, ext, String.Empty, String.Empty, String.Empty, null);
        }

        public String ProcessPageFiles(String content, String ext, String category, String title, String description, DateTime? publishedDate)
        {
            String finalContent = content;

            // lets retrieve all occurences of links in the content
            var reg = new Regex("~\\/link[^.]+\\." + ext + "[\\?\"]?");
            var match = reg.Match(content);

            while (match.Success)
            {
                var groups = match.Groups;
                for (int i = 0; i < groups.Count; i++)
                {
                    var g = groups[i];
                    var cc = g.Captures;
                    for (var j = 0; j < cc.Count; j++)
                    {
                        var c = cc[j];
                        var originalLink = c.Value;
                        originalLink = originalLink.Replace("?", "").Replace("\"", "");

                        var link = filesList.Find(l => l.Equals(originalLink.Replace("~/link/", "").Replace("."+ext, "")));
                        if (!String.IsNullOrEmpty(link))
                        {
                            var filePath = filesDetails[link].ToString();

                            // lets find out name of the file
                            String fileName = fileNamesCache.ContainsKey(link) ? fileNamesCache[link] : "";

                            if (String.IsNullOrEmpty(fileName))
                            {
                                var r = MakeEnterpriseDBQuery("SELECT pkID, Name FROM tblItem WHERE LOWER(REPLACE(pkID, '-', '')) = '"+link+"'", new Dictionary<string, int>()
                                {
                                    { "pkID", 0 },
                                    { "Name", 1 }
                                });

                                if (r.Count() > 0 && !String.IsNullOrEmpty(r[0]["Name"].ToString()))
                                {
                                    var someNewName = r[0]["Name"].ToString();

                                    if (!someNewName.Equals("1") &&
                                        !someNewName.Equals("2") &&
                                        !someNewName.Equals("3") &&
                                        !someNewName.Equals("4") &&
                                        !someNewName.Equals("5") &&
                                        !someNewName.Equals("6") &&
                                        !someNewName.Equals("7") &&
                                        !someNewName.Equals("8") &&
                                        !someNewName.Equals("9") &&
                                        !someNewName.Equals("0"))
                                    {
                                        fileName = someNewName;
                                    }
                                }
                            }

                            // checking if filename still empty
                            if (String.IsNullOrEmpty(fileName))
                            {
                                // unable to find name, lets use name as guid then
                                var fInfo = new FileInfo(filePath);
                                fileName = link + fInfo.Extension;
                            }

                            var documentLibraryName = "Migration";

                            // determine parent directory
                            if (filePath.StartsWith("L:\\episerver\\vpp\\"))
                            {
                                documentLibraryName = filePath.Replace("L:\\episerver\\vpp\\", "").Split('\\')[0];
                            }

                            if (!String.IsNullOrEmpty(category))
                            {
                                documentLibraryName = category;
                            }

                            // lets upload file to the Sitefinity
                            Guid documentId = SDMigrationSnippets.UploadFile(fileName, link, filePath, documentLibraryName, title, description, publishedDate);
                            String docUrl = SDMigrationSnippets.GetFileURL(link);

                            // lets replace content with correct url
                            finalContent = finalContent.Replace(originalLink, docUrl.Replace("http://dev.charteredaccountants.ie", ""));
                        }
                    }
                }

                match = match.NextMatch();
            }

            if(!ext.Equals(ext.ToUpper())) {
                finalContent = ProcessPageFiles(finalContent, ext.ToUpper());
            }

            return finalContent;
        }

        public String ProcessPageLinks(String content)
        {
            String finalContent = content;

            String[] extensions = new String[]
            {
                "png", "gif", "jpg", "jpeg", "doc", "docx", "ppt", "pptx", "csv",
                "xls", "xlsx", "html", "mp3", "pdf", "swf", "zip", "odt", "m4v"
            };

            for (var i = 0; i < extensions.Length; i++)
            {
                finalContent = ProcessPageFiles(finalContent, extensions[i]);
            }

            // lets retrieve all occurences of links in the content
            var reg = new Regex("~\\/link[^.]+\\.aspx[\\?\"]");
            var match = reg.Match(content);

            while (match.Success)
            {
                var groups = match.Groups;
                for (int i = 0; i < groups.Count; i++)
                {
                    var g = groups[i];
                    var cc = g.Captures;
                    for (var j = 0; j < cc.Count; j++)
                    {
                        var c = cc[j];
                        var val = c.Value;
                        val = val.Replace("?", "").Replace("\"", "");

                        var link = linksList.Find(l => l.Equals(val));
                        if (!String.IsNullOrEmpty(link))
                        {
                            var linkDetails = (JArray)linksDetails[link];
                            var linkToUse = linkDetails[2].ToString();

                            finalContent = finalContent.Replace(link, linkToUse);

                            // making sure that page exists in the system
                            JObject details = (JObject)linkDetails[5];
                            String fkPageTypeID = details["fkPageTypeID"].ToString();
                            String pageID = details["pkID"].ToString();
                            String pageName = details["Name"].ToString();

                            // getting all page properties
                            ProcessPage(pageID, fkPageTypeID, pageName, (JArray)linkDetails[6], linkToUse);
                        }
                    }
                }

                match = match.NextMatch();
            }

            return finalContent;
        }

        List<String> processedPages = new List<string>();

        private void ProcessPage(string pageID, string fkPageTypeID, string pageName, JArray properties, String relativeURL)
        {
            if (processedPages.Contains(pageID))
            {
                return;
            }

            processedPages.Add(pageID);

            // first lets create hierarchy for the page
            Guid parentPageId = Guid.Empty;

            // checking if proper news article structure
            if (!fkPageTypeID.Equals("331") || !isNewsArticleUrl(relativeURL, fkPageTypeID))
            {
                parentPageId = CreateAllPagesForEpiserverURL(relativeURL);
            }

            // getting properties that is shared between all of the pages
            String p_mainIntro = extractStringFromProperty(GetPropertyByKey("MainIntro", properties));
            String p_metaAuthor = extractStringFromProperty(GetPropertyByKey("MetaAuthor", properties));
            String p_metaKeywords = extractStringFromProperty(GetPropertyByKey("MetaKeywords", properties));
            String p_metaDescription = extractStringFromProperty(GetPropertyByKey("MetaDescription", properties));
            String p_pageName = extractStringFromProperty(GetPropertyByKey("PageName", properties));
            String p_pageHeading = extractStringFromProperty(GetPropertyByKey("PageHeading", properties));

            // checking if page does not exist
            var testPageNode = SDMigrationSnippets.GetPagedDataBy(pageName, parentPageId, pageID, relativeURL, false);

            if (testPageNode != null)
            {
                return;
            }

            if (!String.IsNullOrEmpty(p_mainIntro) && String.IsNullOrEmpty(p_metaDescription))
            {
                p_metaDescription = p_mainIntro;
            }

            if (!String.IsNullOrEmpty(p_pageHeading))
            {
                pageName = p_pageHeading;
            }
            else if (!String.IsNullOrEmpty(p_pageName))
            {
                pageName = p_pageName;
            }

            /*
                MetaAuthor - Sitefinity Metadata
                MetaKeywords - Sitefinity Metadata
                MetaDescription - Sitefinity Metadata
                PageName, PageHeading used as a title of the page
            */

            // checking type of page
            #region (DONE) link resource
            if (fkPageTypeID.Equals("14"))
            {
                // Use Inner Page Option 2
                String p_Heading = extractStringFromProperty(GetPropertyByKey("Title", properties));
                String p_MainBody = extractStringFromProperty(GetPropertyByKey("MainIntro", properties));

                Guid pageId = SDMigrationSnippets.CreatePageNativeAPI(pageName, parentPageId, pageID, relativeURL, p_metaAuthor, p_metaDescription, p_metaKeywords);
                SDMigrationSnippets.AssignTemplate(pageId, TemplateInnerOption2);
                // lets create some sample content
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption2MainContent, "<h1>" + (String.IsNullOrEmpty(p_Heading) ? pageName : p_Heading) + "</h1>");
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption2MainContent, p_MainBody);
            }
            #endregion
            #region (TODO) RSS Source
            else if (fkPageTypeID.Equals("28") || fkPageTypeID.Equals("311"))
            {
                // RSS Source

            }
            #endregion
            #region 330 (TODO) XForm Aptify Widget (XFormID)
            else if (fkPageTypeID.Equals("330")) // TODO : XForm Aptify Widget (XFormID)
            {
                // Inner Option Page 1
                String p_PageName = extractStringFromProperty(GetPropertyByKey("PageName", properties));
                String p_Section1Title = extractStringFromProperty(GetPropertyByKey("Section1Title", properties));
                String p_Section1Content = extractStringFromProperty(GetPropertyByKey("Section1Content", properties));
                String p_XForm = extractStringFromProperty(GetPropertyByKey("XForm", properties));
                String p_Section2Content = extractStringFromProperty(GetPropertyByKey("Section2Content", properties));

                Guid pageId = SDMigrationSnippets.CreatePageNativeAPI(pageName, parentPageId, pageID, relativeURL, p_metaAuthor, p_metaDescription, p_metaKeywords);
                SDMigrationSnippets.AssignTemplate(pageId, TemplateInnerOption1);
                // lets create some sample content
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1MainContent, "<h1>" + (String.IsNullOrEmpty(p_PageName) ? pageName : p_PageName) + "</h1>");
                if (!String.IsNullOrEmpty(p_Section1Title))
                {
                    SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1MainContent, "<h2>" + p_Section1Title + "</h2>");
                }
                if (!String.IsNullOrEmpty(p_Section1Content))
                {
                    SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1MainContent, p_Section1Content);
                }
                if (!String.IsNullOrEmpty(p_XForm))
                {
                    SDMigrationSnippets.AddControlToPage(pageId, PlaceholderInnerOption1MainContent, "~/UserControls/SoftwareDesign_Aptify/SD_Aptify__c/StudentContactForms__c.ascx", "StudentContactForms__c", new Dictionary<string, string>() 
                    {
                        { "ThankYouMessage", "Thank you for your post!" },
                        { "FormTitle", (String.IsNullOrEmpty(p_PageName) ? pageName : p_PageName) }
                    });
                }
                if (!String.IsNullOrEmpty(p_Section2Content))
                {
                    SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1MainContent, p_Section2Content);
                }

                /*
                    PageName - use name of the page as Page Title stored in content widget as H1
                    Section1Title - Content widget placed after pagename control
                    Section1Content - content widget placed after section1title
                    XForm - AptifyForm widget ID
                    Section2Content - content widget placed after xform widget
                 */
            }
            #endregion
            #region 343 (TODO) Listing of Pages
            else if (fkPageTypeID.Equals("343")) // TODO: Listing of pages
            {
                // Inner Option Page 1
                String p_MainBody = extractStringFromProperty(GetPropertyByKey("MainBody", properties));
                String p_MainIntro = extractStringFromProperty(GetPropertyByKey("MainIntro", properties));
                String p_SourcePage = extractStringFromProperty(GetPropertyByKey("SourcePage", properties));
                Double? p_MaxRows = extractNumberFromProperty(GetPropertyByKey("MaxRows", properties));

                Guid pageId = SDMigrationSnippets.CreatePageNativeAPI(pageName, parentPageId, pageID, relativeURL, p_metaAuthor, p_metaDescription, p_metaKeywords);
                SDMigrationSnippets.AssignTemplate(pageId, TemplateInnerOption1);

                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1MainContent, "<h1>" + pageName + "</h1>");
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1MainContent, "<div style='display: none' class='epi_MainIntro'>" + p_MainIntro + "</div>");
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1MainContent, p_MainBody);
                /*
                   This page renders all pages that has files associated with the page. 
                    MainBody - content widget to be placed after title
                    MainIntro - to be stored on a page property
                    SourcePage - page from where all files are taken from
                    MaxRows - total number of files to be shown from SourcePage
                    ShowDate - date to be shown or not on the control
                 * Control to be created to make use of SourcePage, MaxRows and ShowDate
                 */
            }
            #endregion
            #region 367 (TODO) Three column layout for left, centre and right columns
            else if (fkPageTypeID.Equals("367")) // TODO: Three column layout for left, centre and right columns
            {
                // Inner Option Page 2
                String p_Headlinewidget = extractStringFromProperty(GetPropertyByKey("Headlinewidget", properties));
                String p_LeftHomepageColumnHTML = extractStringFromProperty(GetPropertyByKey("LeftHomepageColumnHTML", properties));
                String p_CentreHomepageColumnHTML = extractStringFromProperty(GetPropertyByKey("CentreHomepageColumnHTML", properties));
                String p_RightHomepageColumnHTML = extractStringFromProperty(GetPropertyByKey("RightHomepageColumnHTML", properties));

                Guid pageId = SDMigrationSnippets.CreatePageNativeAPI(pageName, parentPageId, pageID, relativeURL, p_metaAuthor, p_metaDescription, p_metaKeywords);
                SDMigrationSnippets.AssignTemplate(pageId, TemplateInnerOption2);

                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption2MainContent, p_Headlinewidget);
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption2MainContent, p_LeftHomepageColumnHTML);
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption2MainContent, p_CentreHomepageColumnHTML);
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption2MainContent, p_RightHomepageColumnHTML);
                /*
                    Headlinewidget to be placed as first content block
                    LeftHomepageColumnHTML - Left layout column HTML with content widget
                    CentreHomepageColumnHTML - Center Column HTML with content widget
                    RightHomepageColumnHTML - Right Column HTML with content widget
                 */
            }
            #endregion
            #region 340 (DONE) news landing
            else if (fkPageTypeID.Equals("340")) // TODO
            {
                // News Landing Page
                Guid pageId = SDMigrationSnippets.CreatePageNativeAPI(pageName, parentPageId, pageID, relativeURL, p_metaAuthor, p_metaDescription, p_metaKeywords);
                SDMigrationSnippets.AssignTemplate(pageId, TemplateInnerOption1);

                List<String> categories = new List<string>();
                DateTime? dateFrom = null;
                DateTime? dateTo = null;

                int filteredYear = GetPublishedYearFromUrl(relativeURL);
                int filteredMonth = GetPublishedMonthFromUrl(relativeURL);

                if (filteredYear != 0 && filteredMonth != 0)
                {
                    dateFrom = new DateTime(filteredYear, filteredMonth, 1, 0, 0, 0);
                    dateTo = new DateTime(filteredYear, filteredMonth, DateTime.DaysInMonth(filteredYear, filteredMonth), 23, 59, 59);
                }
                else if (filteredYear != 0)
                {
                    dateFrom = new DateTime(filteredYear, 1, 1, 0, 0, 0);
                    dateTo = new DateTime(filteredYear, 12, 31, 23, 59, 59);
                }

                // lets retrieve categories
                var _categories = MakeEnterpriseDBQuery(String.Format(sqlCategories, pageID), new Dictionary<string, int>()
                    {
                        { "pkID", 0 },
                        { "fkPageID", 1 },
                        { "CategoryName", 2 },
                        { "CategoryDescription", 3 }
                    });

                foreach (var cat in _categories)
                {
                    String catName = cat["CategoryName"].ToString();
                    categories.Add(catName);
                }

                if (categories.Count <= 0)
                {
                    categories.Add("Chartered Accountants Ireland");
                }

                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1MainContent, "<h1>" + pageName + "</h1>");
                SDMigrationSnippets.AddLatestNewsWidgetControlToPage(SDMigrationSnippets.GetPageByEpiserverPageId(pageID), PlaceholderInnerOption1MainContent, categories, dateFrom, dateTo);
            }
            #endregion
            #region 305 (DONE)
            else if (fkPageTypeID.Equals("305"))
            {
                // File Creation in the category "Podcasts"
                String p_ItemName = extractStringFromProperty(GetPropertyByKey("ItemName", properties));
                String p_ItemDescription = extractStringFromProperty(GetPropertyByKey("ItemDescription", properties));
                String p_itemEnclosure = extractStringFromProperty(GetPropertyByKey("itemEnclosure", properties)); // mp3 link
                String p_mimeType = extractStringFromProperty(GetPropertyByKey("mimeType", properties));
                String p_itemSize = extractStringFromProperty(GetPropertyByKey("itemSize", properties));

                // ID for the document folder holder
                var ds = MakeEnterpriseDBQuery("select tblPage.fkParentID, tblPageLanguage.StartPublish from tblPageLanguage JOIN tblPage ON tblPage.pkID = tblPageLanguage.fkPageID where fkPageID = " + pageID, new Dictionary<string, int>(){
                    { "fkParentID", 0 },
                    { "StartPublish", 1 }
                });

                if (ds.Count > 0)
                {
                    String documentListHolder = "Podcasts_" + ds[0]["fkParentID"];
                    p_itemEnclosure = ProcessPageFiles(p_itemEnclosure, "mp3", documentListHolder, p_ItemName, p_ItemDescription, (DateTime)ds.First()["StartPublish"]);
                }
            }
            #endregion
            #region 306 (DONE)
            else if (fkPageTypeID.Equals("306"))
            {
                String p_Link = extractStringFromProperty(GetPropertyByKey("Link", properties));
                String p_Description = extractStringFromProperty(GetPropertyByKey("Description", properties));
                String p_Copyright = extractStringFromProperty(GetPropertyByKey("Copyright", properties));
                String p_itunes_image = extractStringFromProperty(GetPropertyByKey("itunes_image", properties));
                String p_itunes_author = extractStringFromProperty(GetPropertyByKey("itunes_author", properties));
                String p_itunes_explicit = extractStringFromProperty(GetPropertyByKey("itunes_explicit", properties));
                String p_itunes_subtitle = extractStringFromProperty(GetPropertyByKey("itunes_subtitle", properties));
                String p_itunes_summary = extractStringFromProperty(GetPropertyByKey("itunes_summary", properties));
                String p_itunes_name = extractStringFromProperty(GetPropertyByKey("itunes_name", properties));
                String p_itunes_email = extractStringFromProperty(GetPropertyByKey("itunes_email", properties));
                String p_itunes_category = extractStringFromProperty(GetPropertyByKey("itunes_category", properties));

                // Custom page that will generate XML
                Guid pageId = SDMigrationSnippets.CreatePageNativeAPI(pageName, parentPageId, pageID, relativeURL, p_metaAuthor, p_metaDescription, p_metaKeywords);
                SDMigrationSnippets.AssignTemplate(pageId, TemplateInnerOption1);
                // lets create some sample content
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1MainContent, "<h1>" + pageName + "</h1>");
                SDMigrationSnippets.AddControlToPage(pageId, PlaceholderInnerOption1MainContent, "~/SoftwareDesign/SoftwareDesign.Controls.SDWidgets.uc_podcastListing.ascx", "uc_podcastListing", new Dictionary<string, string>() 
                {
                    { "DocumentLibraryName", "Podcasts_" + pageID },
                    { "Link",  p_Link},
                    { "Description",  p_Description},
                    { "Copyright",  p_Copyright},
                    { "itunes_image",  p_itunes_image},
                    { "itunes_author",  p_itunes_author},
                    { "itunes_explicit",  p_itunes_explicit},
                    { "itunes_subtitle",  p_itunes_subtitle},
                    { "itunes_summary",  p_itunes_summary},
                    { "itunes_name",  p_itunes_name},
                    { "itunes_email",  p_itunes_email},
                    { "itunes_category",  p_itunes_category}
                });
            }
            #endregion
            #region 307 - DONE - BUT there is only one page that is using MainListRoot and Count, and it is marked for non migration
            else if (fkPageTypeID.Equals("307")) // TODO: rootPageListing
            {
                // Inner Option Page 1
                String p_Heading = extractStringFromProperty(GetPropertyByKey("Heading", properties));
                String p_MainBody = extractStringFromProperty(GetPropertyByKey("MainBody", properties));
                String p_SecondaryBody = extractStringFromProperty(GetPropertyByKey("SecondaryBody", properties));
                String p_MainListRoot = extractStringFromProperty(GetPropertyByKey("MainListRoot", properties));
                Double? p_MainListCount = extractNumberFromProperty(GetPropertyByKey("MainListCount", properties));

                Guid pageId = SDMigrationSnippets.CreatePageNativeAPI(pageName, parentPageId, pageID, relativeURL, p_metaAuthor, p_metaDescription, p_metaKeywords);
                SDMigrationSnippets.AssignTemplate(pageId, TemplateInnerOption1);
                // lets create some sample content
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1MainContent, "<h1>" + (String.IsNullOrEmpty(p_Heading) ? pageName : p_Heading) + "</h1>");
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1MainContent, p_MainBody);
                SDMigrationSnippets.AddControlToPage(pageId, PlaceholderInnerOption1MainContent, "~/SoftwareDesign/SoftwareDesign.Controls.SDWidgets.uc_rootPageListing.ascx", "uc_rootPageListing", new Dictionary<string, string>() 
                {
                    { "ListCount", p_MainListCount.HasValue ? p_MainListCount.Value.ToString() : "20" }
                });
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1Sidebar, p_SecondaryBody);
                /*
                    Heading - content widget as H1
                    MainBody - content widget
                    SecondaryBody - content widget inside left side column
                    MainListRoot - widget to be created with page selector that will list pages selected 
                    MainListCount - limit number of pages shown from MainListRoot widget
                 */
            }
            #endregion
            #region (DONE) pages to be created as landing pages only
            else if (fkPageTypeID.Equals("196") || fkPageTypeID.Equals("215"))
            {
                // pages to be not migrated, however page to be created using base template
                Guid pageId = SDMigrationSnippets.CreatePageNativeAPI(pageName, parentPageId, pageID, relativeURL, p_metaAuthor, p_metaDescription, p_metaKeywords);
                SDMigrationSnippets.AssignTemplate(pageId, TemplateCAIBase);
            }
            #endregion
            #region 304 (DONE)
            else if (fkPageTypeID.Equals("304")) // TODO: podcastDisplay based on the URL
            {
                // Use Inner Page Option 1
                // ~/SoftwareDesign/SoftwareDesign.Controls.SDWidgets.uc_podcastDisplay.ascx
                String p_podcastURL = extractStringFromProperty(GetPropertyByKey("podcastURL", properties));
                String p_SecondaryBody = extractStringFromProperty(GetPropertyByKey("SecondaryBody", properties));

                Guid pageId = SDMigrationSnippets.CreatePageNativeAPI(pageName, parentPageId, pageID, relativeURL, p_metaAuthor, p_metaDescription, p_metaKeywords);
                SDMigrationSnippets.AssignTemplate(pageId, TemplateInnerOption1);
                // lets create some sample content
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1MainContent, "<h1>" + pageName + "</h1>");
                SDMigrationSnippets.AddControlToPage(pageId, PlaceholderInnerOption1MainContent, "~/SoftwareDesign/SoftwareDesign.Controls.SDWidgets.uc_podcastDisplay.ascx", "uc_podcastDisplay", new Dictionary<string, string>()
                {
                    { "PodcastURL", p_podcastURL }
                });
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1Sidebar, p_SecondaryBody);

                /*
                    podcastURL - podcast widget that takes URL as parameter to configure it
                    SecondaryBody - content widget inside left side column
                 */
            }
            #endregion
            #region 214 (done)
            else if (fkPageTypeID.Equals("214")) // DONE
            {
                // Use Inner Page Option 2
                String p_Heading = extractStringFromProperty(GetPropertyByKey("Heading", properties));
                String p_MainBody = extractStringFromProperty(GetPropertyByKey("MainBody", properties));
                String p_SecondaryBody = extractStringFromProperty(GetPropertyByKey("SecondaryBody", properties));

                Guid pageId = SDMigrationSnippets.CreatePageNativeAPI(pageName, parentPageId, pageID, relativeURL, p_metaAuthor, p_metaDescription, p_metaKeywords);
                SDMigrationSnippets.AssignTemplate(pageId, TemplateInnerOption2);
                // lets create some sample content
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption2MainContent, "<h1>" + (String.IsNullOrEmpty(p_Heading) ? pageName : p_Heading) + "</h1>");
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption2MainContent, p_MainBody);
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption2Sidebar, p_SecondaryBody);
                /*
                    Heading - content widget as H1
                    MainBody - content widget
                    SecondaryBody - content widget inside left side column
                 */
            }
            #endregion
            #region 259 (done)
            else if (fkPageTypeID.Equals("259")) // DONE
            {
                // Use Inner Page Option 2
                String p_Heading = extractStringFromProperty(GetPropertyByKey("Heading", properties));
                String p_MainBody = extractStringFromProperty(GetPropertyByKey("MainBody", properties));
                String p_SecondaryBody = extractStringFromProperty(GetPropertyByKey("SecondaryBody", properties));

                Guid pageId = SDMigrationSnippets.CreatePageNativeAPI(pageName, parentPageId, pageID, relativeURL, p_metaAuthor, p_metaDescription, p_metaKeywords);
                SDMigrationSnippets.AssignTemplate(pageId, TemplateInnerOption2);
                // lets create some sample content
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption2MainContent, "<h1>" + (String.IsNullOrEmpty(p_Heading) ? pageName : p_Heading) + "</h1>");
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption2MainContent, p_MainBody);
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption2Sidebar, p_SecondaryBody);
                /*
                    Heading - content widget as H1
                    MainBody - content widget
                    SecondaryBody - content widget inside left side column
                 */
            }
            #endregion
            #region 260 (DONE)
            else if (fkPageTypeID.Equals("260")) // DONE
            {
                // Use Inner Page Option 2
                String p_Heading = extractStringFromProperty(GetPropertyByKey("Heading", properties));
                String p_MainBody = extractStringFromProperty(GetPropertyByKey("MainBody", properties));

                Guid pageId = SDMigrationSnippets.CreatePageNativeAPI(pageName, parentPageId, pageID, relativeURL, p_metaAuthor, p_metaDescription, p_metaKeywords);
                SDMigrationSnippets.AssignTemplate(pageId, TemplateInnerOption2);
                // lets create some sample content
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption2MainContent, "<h1>" + (String.IsNullOrEmpty(p_Heading) ? pageName : p_Heading) + "</h1>");
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption2MainContent, p_MainBody);
                /*
                    Heading - content widget as H1
                    MainBody - content widget
                 */
            }
            #endregion
            #region 270 (Done)
            else if (fkPageTypeID.Equals("270")) // Done
            {
                // Use Inner Page Option 1
                String p_Heading = extractStringFromProperty(GetPropertyByKey("Heading", properties));
                String p_MainBody = extractStringFromProperty(GetPropertyByKey("MainBody", properties));
                String p_SecondaryBody = extractStringFromProperty(GetPropertyByKey("SecondaryBody", properties));
                // ~/link/8910e4bfd32940258373316127a4c328.pdf
                String p_DocumentInternalPath = extractStringFromProperty(GetPropertyByKey("DocumentInternalPath", properties));
                String p_fileName = "";
                String p_fileLink = "#";
                if (!String.IsNullOrEmpty(p_DocumentInternalPath))
                {
                    var originalPath = p_DocumentInternalPath;
                    p_fileLink = ProcessPageLinks(p_DocumentInternalPath);

                    // checking if file found
                    if (originalPath.Equals(p_fileLink))
                    {
                        p_fileLink = String.Empty;
                    }

                    p_DocumentInternalPath = p_DocumentInternalPath.Replace("~/link/", "").Split('.')[0];

                    // lets make DB request for the data in p_DocumentInternalPath
                    var ds = MakeEnterpriseDBQuery("SELECT * FROM tblItem where LOWER(REPLACE(pkID, '-', '')) = '" + p_DocumentInternalPath + "'", new Dictionary<string, int>
                    {
                        { "Name", 2 }
                    });

                    if (ds.Count > 0)
                    {
                        p_fileName = ds[0]["Name"].ToString();
                    }
                }

                Guid pageId = SDMigrationSnippets.CreatePageNativeAPI(pageName, parentPageId, pageID, relativeURL, p_metaAuthor, p_metaDescription, p_metaKeywords);
                SDMigrationSnippets.AssignTemplate(pageId, TemplateInnerOption1);
                // lets create some sample content
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1MainContent, "<h1>" + (String.IsNullOrEmpty(p_Heading) ? pageName : p_Heading) + "</h1>");
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1MainContent, p_MainBody);
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1Sidebar, p_SecondaryBody);
                if (!String.IsNullOrEmpty(p_fileLink))
                {
                    SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1Sidebar, "<a href='" + p_fileLink + "'>" + p_fileName + "</a>");
                }
            }
            #endregion
            #region 271 (DONE)
            else if (fkPageTypeID.Equals("271")) // Done
            {
                // Use Inner Page Option 1
                String p_Heading = extractStringFromProperty(GetPropertyByKey("Heading", properties));
                String p_MainBody = extractStringFromProperty(GetPropertyByKey("MainBody", properties));
                String p_SecondaryBody = extractStringFromProperty(GetPropertyByKey("SecondaryBody", properties));
                String p_DocumentListRoot = extractStringFromProperty(GetPropertyByKey("DocumentListRoot", properties));

                // we need to find out all of the children from given list root
                var ds = MakeEnterpriseDBQuery("select tblPageLanguage.Name as URLName, tblPageDefinition.Name, " +
                        "tblProperty.Number, " +
                        "tblProperty.FloatNumber, " +
                        "tblProperty.Date, " +
                        "tblProperty.String, " +
                        "tblProperty.PageLink, " +
                        "tblProperty.LongString, " +
                        "tblPage.pkID " +
                        "from tblPage " +
                        "JOIN tblPageLanguage ON tblPageLanguage.fkPageID = tblPage.pkID " +
                        "JOIN tblProperty ON tblProperty.fkPageID = tblPage.pkID  " +
                        "JOIN tblPageDefinition ON tblPageDefinition.pkID = tblProperty.fkPageDefinitionID  " +
                        "WHERE tblPage.pkID IN (select fkPageID from tblPageLanguage where fkPageID IN (select pkID from tblPage where fkParentID = " + p_DocumentListRoot + "))" +
                        "ORDER BY tblPage.pkID ASC", new Dictionary<string, int>
                    {
                        { "URLName", 0 },
                        { "Name", 1 },
                        { "String", 5 },
                        { "LongString", 7 },
                        { "pkID", 8 }
                    });

                Guid pageId = SDMigrationSnippets.CreatePageNativeAPI(pageName, parentPageId, pageID, relativeURL, p_metaAuthor, p_metaDescription, p_metaKeywords);
                SDMigrationSnippets.AssignTemplate(pageId, TemplateInnerOption1);
                // lets create some sample content
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1MainContent, "<h1>" + (String.IsNullOrEmpty(p_Heading) ? pageName : p_Heading) + "</h1>");
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1MainContent, p_MainBody);
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1Sidebar, p_SecondaryBody);

                if (ds.Count > 0)
                {
                    String childTitle = "";
                    String childDescription = "";
                    String childFileName = "";
                    String childFileLink = "#";
                    String curPageID = "";

                    String childHtmlOutput = "<p><b>{0}</b></p><p>{1}</p><p>Download: <a href='{2}'>{3}</a></p>";

                    foreach (var item in ds)
                    {
                        if (String.IsNullOrEmpty(curPageID))
                        {
                            curPageID = item["pkID"].ToString();
                            childTitle = item["URLName"].ToString();
                        }
                        else if (!curPageID.Equals(item["pkID"].ToString()))
                        {
                            // lets place content block
                            if (!String.IsNullOrEmpty(childFileLink))
                            {
                                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1MainContent,
                                    String.Format(childHtmlOutput, childTitle, childDescription, childFileLink, childFileName));
                            }

                            curPageID = item["pkID"].ToString();
                        }

                        if (curPageID.Equals(item["pkID"].ToString()))
                        {
                            if (item["Name"].Equals("MainBody"))
                            {
                                childDescription = item["LongString"].ToString();
                                childDescription = ClearAllHTMLTags(childDescription);
                            }
                            else if (item["Name"].Equals("DocumentInternalPath"))
                            {
                                if (!String.IsNullOrEmpty(item["String"].ToString()))
                                {
                                    childFileName = item["String"].ToString();

                                    var originalPath = childFileName;
                                    childFileLink = ProcessPageLinks(childFileName);

                                    // checking if file found
                                    if (originalPath.Equals(childFileLink))
                                    {
                                        childFileLink = String.Empty;
                                    }

                                    childFileName = item["String"].ToString().Replace("~/link/", "").Split('.')[0];

                                    // lets make DB request for the data in p_DocumentInternalPath
                                    var ds_fileName = MakeEnterpriseDBQuery("SELECT * FROM tblItem where LOWER(REPLACE(pkID, '-', '')) = '" + childFileName + "'", new Dictionary<string, int>
                                        {
                                            { "Name", 2 }
                                        });

                                    if (ds_fileName.Count > 0)
                                    {
                                        childFileName = ds_fileName[0]["Name"].ToString();
                                    }
                                }
                            }
                        }
                    }

                    // lets place content block
                    if (!String.IsNullOrEmpty(childFileLink))
                    {
                        SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1MainContent,
                            String.Format(childHtmlOutput, childTitle, childDescription, childFileLink, childFileName));
                    }
                }
            }
            #endregion
            #region FUTURE MIGRATIONS
            else if (fkPageTypeID.Equals("280") || fkPageTypeID.Equals("282") || fkPageTypeID.Equals("283") || fkPageTypeID.Equals("286"))
            {
                // FUTURE MIGRATIONS
            }
            #endregion
            #region 331 (DONE)
            else if (fkPageTypeID.Equals("331")) // DONE
            {
                // Inner Option Page 1
                String p_Title = extractStringFromProperty(GetPropertyByKey("Title", properties));
                String p_MainBody = extractStringFromProperty(GetPropertyByKey("MainBody", properties));
                String p_Document = extractStringFromProperty(GetPropertyByKey("Document", properties));

                if(SDMigrationSnippets.GetNewsItemBy(pageID) != null)
                {
                    return;
                }

                if (isNewsArticleUrl(relativeURL, fkPageTypeID))
                {
                    // we need to delete existing page with pageID
                    SDMigrationSnippets.DeletePageWithEpiServerID(pageID);

                    // lets retrieve categories
                    var categories = MakeEnterpriseDBQuery(String.Format(sqlCategories, pageID), new Dictionary<string, int>()
                    {
                        { "pkID", 0 },
                        { "fkPageID", 1 },
                        { "CategoryName", 2 },
                        { "CategoryDescription", 3 }
                    });

                    // lets retrieve correct publication date
                    String sql = "select fkPageID, StartPublish, Created, Changed, Saved from tblPageLanguage where fkPageID IN (" + pageID + ")";

                    var articlePublishedRow = MakeEnterpriseDBQuery(sql, new Dictionary<string, int>()
                    {
                        { "fkPageID", 0 },
                        { "StartPublish", 1 },
                        { "Created", 2 },
                        { "Changed", 3 },
                        { "Saved", 4 }
                    });

                    DateTime? publishedDate = GetPublishedDateFromUrl(relativeURL);

                    // checking if published date exists
                    if (articlePublishedRow.Count > 0)
                    {
                        DateTime rowDate = RetrieveMatchingDate(publishedDate, new List<DateTime?>()
                        {
                            (DateTime?)(articlePublishedRow.First()["StartPublish"].ToString().IsNullOrEmpty() ? null : articlePublishedRow.First()["StartPublish"]),
                            (DateTime?)(articlePublishedRow.First()["Created"].ToString().IsNullOrEmpty() ? null : articlePublishedRow.First()["Created"]),
                            (DateTime?)(articlePublishedRow.First()["Changed"].ToString().IsNullOrEmpty() ? null : articlePublishedRow.First()["Changed"]),
                            (DateTime?)(articlePublishedRow.First()["Saved"].ToString().IsNullOrEmpty() ? null : articlePublishedRow.First()["Saved"])
                        });

                        publishedDate = rowDate;
                    }

                    // checking if published date exists
                    if (publishedDate.HasValue)
                    {
                        SDMigrationSnippets.AddNewsControlToPage(this, pageID, publishedDate.Value, p_Title, p_MainBody, relativeURL, categories);
                    }
                    else
                    {
                        throw new Exception("Published date is not found for pageID: " + pageID);
                    }
                }
                else
                {
                    Guid pageId = SDMigrationSnippets.CreatePageNativeAPI(pageName, parentPageId, pageID, relativeURL, p_metaAuthor, p_metaDescription, p_metaKeywords);
                    SDMigrationSnippets.AssignTemplate(pageId, TemplateInnerOption1);
                    // lets create some sample content
                    SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1MainContent, "<h1>" + (String.IsNullOrEmpty(p_Title) ? pageName : p_Title) + "</h1>");
                    SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1MainContent, p_MainBody);

                    String p_fileName = "";
                    String p_fileLink = "#";
                    if (!String.IsNullOrEmpty(p_Document))
                    {
                        var originalPath = p_Document;
                        p_fileLink = ProcessPageLinks(p_Document);

                        // checking if file found
                        if (originalPath.Equals(p_fileLink))
                        {
                            p_fileLink = String.Empty;
                        }

                        p_Document = p_Document.Replace("~/link/", "").Split('.')[0];

                        // lets make DB request for the data in p_DocumentInternalPath
                        var ds = MakeEnterpriseDBQuery("SELECT * FROM tblItem where LOWER(REPLACE(pkID, '-', '')) = '" + p_Document + "'", new Dictionary<string, int>
                        {
                            { "Name", 2 }
                        });

                        if (ds.Count > 0)
                        {
                            p_fileName = ds[0]["Name"].ToString();
                        }

                        if (!String.IsNullOrEmpty(p_fileLink))
                        {
                            SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1Sidebar, "<a href='" + p_fileLink + "'>" + p_fileName + "</a>");
                        }
                    }
                }

                /*
                    Title - use name of the page as Page Title stored in content widget as H1
                    DatePublished - date page is published
                    MainBody - content widget placed after section1title
                 * 
                 * by default all values were 1 for HidePublicationDate, so we will ignore this field, and not show published date
                    HidePublicationDate - Indicator to show/hide publication date (create widget to show published date, add if this is true)
                 * 
                    Document - link to the attached document and to be shown after MainBody content widget
                 */
            }
            #endregion
            #region 333 (DONE)
            else if (fkPageTypeID.Equals("333")) // DONE
            {
                // Inner Option Page 1
                String p_MainBody = extractStringFromProperty(GetPropertyByKey("MainBody", properties));
                String p_MainIntro = extractStringFromProperty(GetPropertyByKey("MainIntro", properties));
                String p_PageSecondaryBody = extractStringFromProperty(GetPropertyByKey("PageSecondaryBody", properties));

                Guid pageId = SDMigrationSnippets.CreatePageNativeAPI(pageName, parentPageId, pageID, relativeURL, p_metaAuthor, p_metaDescription, p_metaKeywords);
                SDMigrationSnippets.AssignTemplate(pageId, TemplateInnerOption1);

                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1MainContent, "<h1>" + pageName + "</h1>");
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1MainContent, "<div style='display: none' class='epi_MainIntro'>" + p_MainIntro + "</div>");
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1MainContent, p_MainBody);
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1Sidebar, p_PageSecondaryBody);
            }
            #endregion
            #region 342 (DONE)
            else if (fkPageTypeID.Equals("342")) // DONE
            {
                // Inner Option Page 1
                String p_Title = extractStringFromProperty(GetPropertyByKey("Title", properties));
                String p_MainBody = extractStringFromProperty(GetPropertyByKey("MainBody", properties));
                String p_MainIntro = extractStringFromProperty(GetPropertyByKey("MainIntro", properties));
                String p_File1 = extractStringFromProperty(GetPropertyByKey("File1", properties));
                String p_File2 = extractStringFromProperty(GetPropertyByKey("File2", properties));

                Guid pageId = SDMigrationSnippets.CreatePageNativeAPI(pageName, parentPageId, pageID, relativeURL, p_metaAuthor, p_metaDescription, p_metaKeywords);
                SDMigrationSnippets.AssignTemplate(pageId, TemplateInnerOption1);

                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1MainContent, "<h1>" + (String.IsNullOrEmpty(p_Title) ? pageName : p_Title) + "</h1>");
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1MainContent, "<div style='display: none' class='epi_MainIntro'>" + p_MainIntro + "</div>");
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1MainContent, p_MainBody);

                String p_fileName_1 = "";
                String p_fileLink_1 = "#";
                String p_fileName_2 = "";
                String p_fileLink_2 = "#";
                if (!String.IsNullOrEmpty(p_File1))
                {
                    var originalPath = p_File1;
                    p_fileLink_1 = ProcessPageLinks(p_File1);

                    // checking if file found
                    if (originalPath.Equals(p_fileLink_1))
                    {
                        p_fileLink_1 = String.Empty;
                    }

                    p_File1 = p_File1.Replace("~/link/", "").Split('.')[0];

                    // lets make DB request for the data in p_DocumentInternalPath
                    var ds = MakeEnterpriseDBQuery("SELECT * FROM tblItem where LOWER(REPLACE(pkID, '-', '')) = '" + p_File1 + "'", new Dictionary<string, int>
                    {
                        { "Name", 2 }
                    });

                    if (ds.Count > 0)
                    {
                        p_fileName_1 = ds[0]["Name"].ToString();
                    }
                }
                if (!String.IsNullOrEmpty(p_File2))
                {
                    var originalPath = p_File2;
                    p_fileLink_2 = ProcessPageLinks(p_File2);

                    // checking if file found
                    if (originalPath.Equals(p_fileLink_2))
                    {
                        p_fileLink_2 = String.Empty;
                    }

                    p_File2 = p_File2.Replace("~/link/", "").Split('.')[0];

                    // lets make DB request for the data in p_DocumentInternalPath
                    var ds = MakeEnterpriseDBQuery("SELECT * FROM tblItem where LOWER(REPLACE(pkID, '-', '')) = '" + p_File2 + "'", new Dictionary<string, int>
                    {
                        { "Name", 2 }
                    });

                    if (ds.Count > 0)
                    {
                        p_fileName_2 = ds[0]["Name"].ToString();
                    }
                }

                if (!String.IsNullOrEmpty(p_fileLink_1) && !String.IsNullOrEmpty(p_fileLink_2))
                {
                    SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1MainContent, "<h3>Attached Files</h3>");
                }

                if (!String.IsNullOrEmpty(p_fileLink_1))
                {
                    SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1Sidebar, "<a href='" + p_fileLink_1 + "'>" + p_fileName_1 + "</a>");
                }

                if (!String.IsNullOrEmpty(p_fileLink_2))
                {
                    SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1Sidebar, "<a href='" + p_fileLink_2 + "'>" + p_fileName_2 + "</a>");
                }
            }
            #endregion
            #region 346 (DONE)
            else if (fkPageTypeID.Equals("346")) // DONE
            {
                // Inner Option Page 1
                String p_Title = extractStringFromProperty(GetPropertyByKey("Title", properties));
                String p_MainURL = extractStringFromProperty(GetPropertyByKey("MainURL", properties));

                Guid pageId = SDMigrationSnippets.CreatePageNativeAPI(pageName, parentPageId, pageID, relativeURL, p_metaAuthor, p_metaDescription, p_metaKeywords);
                SDMigrationSnippets.AssignTemplate(pageId, TemplateInnerOption1);

                p_MainURL = ProcessPageLinks(p_MainURL);

                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1MainContent, "<h1>" + (String.IsNullOrEmpty(p_Title) ? pageName : p_Title) + "</h1>");
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption1Sidebar, "<a href='" + p_MainURL + "'>" + (String.IsNullOrEmpty(p_Title) ? pageName : p_Title) + "</a>");


                /*
                    Title/PageName - use name of the page as Page Title stored in content widget as H1
                    MainURL - to be shown after title field and Title, together with date published to be shown
                 */
            }
#endregion
            #region 366 (DONE)
            else if (fkPageTypeID.Equals("366")) // DONE
            {
                // Inner Option Page 2
                String p_Title = extractStringFromProperty(GetPropertyByKey("Title", properties));
                String p_MainContent = extractStringFromProperty(GetPropertyByKey("MainContent", properties));
                String p_PageTitle = extractStringFromProperty(GetPropertyByKey("PageTitle", properties));
                if (!String.IsNullOrEmpty(p_PageTitle) && String.IsNullOrEmpty(p_Title))
                {
                    p_Title = p_PageTitle;
                }
                String p_SecondaryContent = extractStringFromProperty(GetPropertyByKey("SecondaryContent", properties));

                Guid pageId = SDMigrationSnippets.CreatePageNativeAPI(pageName, parentPageId, pageID, relativeURL, p_metaAuthor, p_metaDescription, p_metaKeywords);
                SDMigrationSnippets.AssignTemplate(pageId, TemplateInnerOption2);

                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption2MainContent, "<h1>" + (String.IsNullOrEmpty(p_Title) ? pageName : p_Title) + "</h1>");
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption2MainContent, p_MainContent);
                SDMigrationSnippets.AddContentControlToPage(this, pageId, PlaceholderInnerOption2Sidebar, p_SecondaryContent);

                /*
                    Title/PageName/PageTitle - use name of the page as Page Title stored in content widget as H1
                    MainContent - content widget to be placed after title
                    SecondaryContent - content widget inside left side column
                 */
            }
            #endregion
        }

        private DateTime RetrieveMatchingDate(DateTime? publishedDate, List<DateTime?> list)
        {
            DateTime? returnValue = null;

            foreach (var date in list)
            {
                if (date.HasValue)
                {
                    returnValue = date.Value;
                }

                // comparing if published dated from URL matches row date
                if (date.HasValue && publishedDate.HasValue && publishedDate.Value.Year == date.Value.Year && publishedDate.Value.Month == date.Value.Month)
                {
                    return date.Value;
                }
            }

            return publishedDate.HasValue ? publishedDate.Value : (returnValue.HasValue ? returnValue.Value : new DateTime());
        }

        private string ClearAllHTMLTags(string html)
        {
            return SDMigrationSnippets.ClearAllHTMLTags(html);
        }

        private List<Dictionary<string, Object>> MakeEnterpriseDBQuery(string sql, Dictionary<String, int> fields)
        {
            List<Dictionary<string, Object>> result = new List<Dictionary<string, object>>();

            if(externalSqlConnection == null) {
                externalSqlConnection = new SqlConnection("Server=10.93.30.95\\MSSQL2014;Database=EnterpriseSiteContent;User Id=migrationUser;Password=$ea5he11;");
            }
            
            externalSqlConnection.Open();

            using (SqlCommand command = new SqlCommand(sql, externalSqlConnection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Dictionary<string, object> obj = new Dictionary<string, object>();
                        foreach (var field in fields)
                        {
                            obj.Add(field.Key, reader.GetValue(field.Value));
                        }

                        result.Add(obj);
                    }
                }
            }

            externalSqlConnection.Close();

            return result;
        }
    }
}