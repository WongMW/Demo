using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Workflow;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.News.Model;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.OpenAccess;
using Telerik.Sitefinity.SitefinityExceptions;
using System.Globalization;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Libraries.Model;
using System.IO;
using Telerik.Sitefinity.Web.Model;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;

namespace SitefinityWebApp.migration
{
    public partial class SDMigrationSnippets
    {
        public static Guid CreatePageNativeAPI(string pageName)
        {
            return CreatePageNativeAPI(pageName, Guid.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
        }

        /// <summary>
        /// Function to retrieve page id based on the EpiServerURL Path
        /// </summary>
        /// <param name="path">unique path of the episerver url</param>
        /// <returns></returns>
        public static Guid GetFileByLinkId(String linkId)
        {
            // checking if page already exists with given epiServerPageId
            if (!String.IsNullOrEmpty(linkId))
            {
                Guid link = Guid.Parse(linkId);

                LibrariesManager librariesManager = LibrariesManager.GetManager();
                Document library = librariesManager.GetDocuments().Where(
                    b => b.Id.Equals(link)
                ).FirstOrDefault();

                if (library != null)
                {
                    return library.Id;
                }
            }

            return Guid.Empty;
        }

        /// <summary>
        /// Function to retrieve page id based on the EpiServerURL Path
        /// </summary>
        /// <param name="path">unique path of the episerver url</param>
        /// <returns></returns>
        public static Guid GetImageFileByLinkId(String linkId)
        {
            // checking if page already exists with given epiServerPageId
            if (!String.IsNullOrEmpty(linkId))
            {
                Guid link = Guid.Parse(linkId);

                LibrariesManager librariesManager = LibrariesManager.GetManager();
                Image library = librariesManager.GetImages().Where(
                    b => b.Id.Equals(link)
                ).FirstOrDefault();

                if (library != null)
                {
                    return library.Id;
                }
            }

            return Guid.Empty;
        }

        public static String GetFileURL(String linkId)
        {
            // checking if page already exists with given epiServerPageId
            if (!String.IsNullOrEmpty(linkId))
            {
                Guid link = Guid.Parse(linkId);

                LibrariesManager librariesManager = LibrariesManager.GetManager();
                Document library = librariesManager.GetDocuments().Where(
                    b => b.Id.Equals(link)
                ).FirstOrDefault();

                if (library != null)
                {
                    return library.Url;
                }
                else
                {
                    Image image = librariesManager.GetImages().Where(
                        b => b.Id.Equals(link)
                    ).FirstOrDefault();

                    if (image != null)
                    {
                        return image.Url;
                    }
                }
            }

            return String.Empty;
        }

        public static Guid GetPageByEpiserverPageId(String epiServerPageId)
        {
            // checking if page already exists with given epiServerPageId
            if (!String.IsNullOrEmpty(epiServerPageId))
            {
                var fluentPages = App.WorkWith().Pages();
                var page = fluentPages.Where(
                        x => x.GetString("EpiserverPageId") != null &&
                        epiServerPageId.Equals(x.GetString("EpiserverPageId"))
                    ).Get().FirstOrDefault();
                if (page != null)
                {
                    return page.Id;
                }
            }

            return Guid.Empty;
        }

        /// <summary>
        /// Function to retrieve page id based on the EpiServerURL Path
        /// </summary>
        /// <param name="path">unique path of the episerver url</param>
        /// <returns></returns>
        public static Guid GetPageByEpiserverUrlPath(String path)
        {
            // checking if page already exists with given epiServerPageId
            if (!String.IsNullOrEmpty(path))
            {
                if (path.Length > 256)
                {
                    path = path.Substring(path.Length - 255);
                }

                var fluentPages = App.WorkWith().Pages();
                var page = fluentPages.Where(x => 
                        x.GetString("EpiserverUrlPath") != null &&
                        path.Equals(x.GetString("EpiserverUrlPath"))
                    ).Get().FirstOrDefault();
                if (page != null)
                {
                    return page.Id;
                }
            }
            
            return Guid.Empty;
        }

        /// <summary>
        /// Create a page with the native API
        /// </summary>
        /// <param name="pageName"></param>
        /// <param name="parentPageNodeId"></param>
        public static Guid CreatePageNativeAPI(string pageName, Guid parentPageNodeId, String epiServerPageId, String epiServerUrl, String author, String description, String keywords)
        {
            PageNode pageNode = GetPagedDataBy(pageName, parentPageNodeId, epiServerPageId, epiServerUrl, true);

            PageManager manager = PageManager.GetManager();

            if (pageNode == null)
            {
                PageNode parent = manager.GetPageNode(parentPageNodeId);
                var pageId = Guid.NewGuid();

                pageNode = manager.CreatePage(parent, pageId, NodeType.Standard);
            }

            PageData pageData = pageNode.GetPageData();
            pageData.Culture = Thread.CurrentThread.CurrentCulture.ToString();
            pageData.HtmlTitle = pageName;
            pageData.Description = description;
            pageData.Keywords = ClearAllHTMLTags(keywords);
            if (pageData.Keywords.Length > 500)
            {
                pageData.Keywords = pageData.Keywords.ToString().Substring(0, 499);
            }
            // author to be added

            pageNode.Name = pageName;
            pageNode.Description = pageName;
            pageNode.Title = pageName;
            if (!String.IsNullOrEmpty(epiServerUrl))
            {
                pageNode.UrlName = epiServerUrl.Split('/').Last();
            }
            else
            {
                pageNode.UrlName = Regex.Replace(pageName.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");
            }
            pageNode.ShowInNavigation = true;
            pageNode.DateCreated = DateTime.UtcNow;
            pageNode.LastModified = DateTime.UtcNow;
            pageNode.ApprovalWorkflowState = "Published";
            manager.SaveChanges();

            if (!String.IsNullOrEmpty(epiServerPageId))
            {
                var fluentPages = App.WorkWith().Pages();
                var page = fluentPages.Where(x => x.PageId.Equals(pageNode.PageId)).Get().FirstOrDefault();
                if (page != null)
                {
                    //Setting custom field with name Hint of type LString
                    page.SetString("EpiserverPageId", epiServerPageId);

                    fluentPages.SaveChanges();
                }
            }

            if (!String.IsNullOrEmpty(epiServerUrl))
            {
                var fluentPages = App.WorkWith().Pages();
                var page = fluentPages.Where(x => x.PageId.Equals(pageNode.PageId)).Get().FirstOrDefault();
                if (page != null)
                {
                    if (epiServerUrl.Length > 256)
                    {
                        epiServerUrl = epiServerUrl.Substring(epiServerUrl.Length - 255);
                    }

                    //Setting custom field with name Hint of type LString
                    page.SetString("EpiserverUrlPath", epiServerUrl);

                    fluentPages.SaveChanges();
                }
            }

            return pageNode.PageId;
        }

        public static PageNode GetPagedDataBy(string pageName, Guid parentPageNodeId, string epiServerPageId, string epiServerUrl, bool removeControls)
        {
            PageNode pageNode = null;

            // checking if page already exists with given epiServerPageId
            if (!String.IsNullOrEmpty(epiServerPageId))
            {
                var fluentPages = App.WorkWith().Pages();
                var page = fluentPages.Where(
                        x => x.GetString("EpiserverPageId") != null &&
                        epiServerPageId.Equals(x.GetString("EpiserverPageId"))
                    ).Get().FirstOrDefault();
                if (page != null)
                {
                    if(removeControls)
                    {
                        // lets remove all controls added to the page
                        RemoveAllControlsOnPage(page.PageId);
                    }

                    pageNode = page;
                }
            }

            if (!String.IsNullOrEmpty(epiServerUrl) && pageNode == null)
            {
                var fluentPages = App.WorkWith().Pages();
                var page = fluentPages.Where(
                        x => x.GetString("EpiserverUrlPath") != null &&
                        epiServerUrl.Equals(x.GetString("EpiserverUrlPath"))
                    ).Get().FirstOrDefault();
                if (page != null)
                {
                    if (removeControls)
                    {
                        // lets remove all controls added to the page
                        RemoveAllControlsOnPage(page.PageId);
                    }

                    pageNode = page;
                }
            }

            PageManager manager = PageManager.GetManager();

            if (parentPageNodeId == Guid.Empty)
            {
                parentPageNodeId = SiteInitializer.CurrentFrontendRootNodeId;
            }

            return pageNode;
        }

        public static string ClearAllHTMLTags(string html)
        {
            string regex = @"(<.+?>|&nbsp;)";
            var result = System.Text.RegularExpressions.Regex.Replace(html, regex, "").Trim();

            return result;
        }

        private static void RemoveAllControlsOnPage(Guid pageNodeId)
        {
            var pageManager = PageManager.GetManager();
            var page = pageManager.GetPageNodes().Where(p => p.PageId == pageNodeId).SingleOrDefault();

            if (page != null)
            {
                var temp = pageManager.EditPage(page.GetPageData().Id);

                if (temp != null)
                {
                    // Remove the first child control in the controls collection
                    if (temp.Controls.Count > 0)
                    {
                        temp.Controls.Clear();
                    }

                    pageManager.PagesLifecycle.CheckIn(temp);
                    pageManager.SaveChanges();
                }
            }
        }

        public static void AssignTemplate(Guid pageId, Guid templateId)
        {
            PageManager manager = PageManager.GetManager();

            var temp = manager.EditPage(pageId);
            temp.TemplateId = templateId;

            manager.PagesLifecycle.CheckIn(temp);
            manager.SaveChanges();
        }


        /// <summary>
        /// Generate unique control Id
        /// </summary>
        /// <param name="pageDraft"></param>
        /// <returns></returns>
        public static string GenerateUniqueControlIdForPage(PageDraft pageDraft)
        {
            int controlsCount = 0;

            if (pageDraft != null)
            {
                controlsCount = pageDraft.Controls.Count;
            }

            return String.Format("C" + controlsCount.ToString().PadLeft(3, '0'));
        }

        /// <summary>
        /// Get the last control in a placeholder
        /// </summary>
        /// <param name="pageDraft"></param>
        /// <param name="placeHolder"></param>
        /// <returns></returns>
        public static Guid GetLastControlInPlaceHolderInPageId(PageDraft pageDraft, string placeHolder)
        {
            var id = Guid.Empty;
            PageDraftControl control;

            var controls = new List<PageDraftControl>(pageDraft.Controls.Where(c => c.PlaceHolder == placeHolder));

            while (controls.Count > 0)
            {
                var cntrls = controls.Where(c => c.SiblingId == id);
                if(cntrls.Count() == 1) {
                    control = cntrls.SingleOrDefault();
                }
                else if (cntrls.Count() > 1)
                {
                    control = cntrls.Last();
                }
                else
                {
                    control = new PageDraftControl();
                }

                id = control.Id;

                controls.Remove(control);
            }

            return id;
        }

        /// <summary>
        /// Add Control to a Page
        /// </summary>
        /// <param name="pageNodeId"></param>
        /// <param name="control"></param>
        /// <param name="placeHolder"></param>
        /// <param name="caption"></param>
        public static void AddControlToPage(Guid pageNodeId, Control control, string placeHolder, string caption, Dictionary<String, String> properties)
        {
            var pageManager = PageManager.GetManager();
            var page = pageManager.GetPageNodes().Where(p => p.Id == pageNodeId).SingleOrDefault();

            if (page != null)
            {
                //since 7.0 PageData must be retrieved via the GetPageData method instead of the Page property
                //see API changes in Sitefinity 7.0 for details

                //valid before 7.0
                //var temp = pageManager.EditPage(page.Page.Id);

                //valid since 7.0
                var temp = pageManager.EditPage(page.GetPageData().Id);

                if (temp != null)
                {
                    if (string.IsNullOrEmpty(control.ID))
                    {
                        control.ID = SDMigrationSnippets.GenerateUniqueControlIdForPage(temp);
                    }

                    var pageControl = pageManager.CreateControl<PageDraftControl>(control, placeHolder);

                    foreach (KeyValuePair<string, string> property in properties)
                    {
                        var prop = pageControl.Properties.FirstOrDefault(p => p.Name == property.Key);
                        if (prop == null)
                        {
                            prop = new ControlProperty();
                            prop.Id = Guid.NewGuid();
                            prop.Name = property.Key;
                            prop.Value = property.Value;
                            pageControl.Properties.Add(prop);
                        }
                    }

                    pageControl.Caption = caption;
                    pageControl.SiblingId = SDMigrationSnippets.GetLastControlInPlaceHolderInPageId(temp, placeHolder);
                    pageManager.SetControlDefaultPermissions(pageControl);
                    temp.Controls.Add(pageControl);

                    pageManager.PagesLifecycle.CheckIn(temp);
                    pageManager.SaveChanges();

                    // Publish
                    var bag = new Dictionary<string, string>();
                    bag.Add("ContentType", typeof(PageNode).FullName);
                    WorkflowManager.MessageWorkflow(pageNodeId, typeof(PageNode), null, "Publish", false, bag);
                }
            }
        }

        public static NewsItem GetNewsItemBy(String epiServerPageId)
        {
            NewsItem newsItem = null;

            // checking if page already exists with given epiServerPageId
            if (!String.IsNullOrEmpty(epiServerPageId))
            {
                var fluentPages = App.WorkWith().NewsItems();
                var newsItemNode = fluentPages.Where(
                        x => !String.IsNullOrEmpty(x.GetValue<String>("EpiserverPageId")) &&
                            x.GetValue<String>("EpiserverPageId").Equals(epiServerPageId)
                    ).Get().FirstOrDefault();
                if (newsItemNode != null)
                {
                    newsItem = newsItemNode;
                }
            }

            return newsItem;
        }

        public static void AddNewsControlToPage(processMigration p_object, String epiServerPageId, DateTime publicationDate, string newsTitle, string newsContent, String relativeUrl, List<Dictionary<string, object>> categories)
        {
            NewsItem newsItem = GetNewsItemBy(epiServerPageId);

            newsContent = p_object.ProcessPageLinks(newsContent);

            NewsManager newsManager = NewsManager.GetManager();

            var articleId = Guid.NewGuid();
            Boolean exists = false;
             
            //The news item is created as a master. The newsId is assigned to the master.
            if (newsItem == null)
            {
                newsItem = newsManager.CreateNewsItem(articleId);
            }
            else
            {
                articleId = newsItem.Id;
                exists = true;

                newsItem = newsManager.GetNewsItem(articleId);
            }

            //Set the properties of the news item.
            newsItem.Title = newsTitle;
            newsItem.Content = newsContent;
            newsItem.DateCreated = publicationDate;
            newsItem.PublicationDate = publicationDate;
            newsItem.LastModified = publicationDate;
            newsItem.UrlName = relativeUrl.Split('/').Last();

            String urlName = newsItem.UrlName;
            int duplicateEntries = 0;
            bool succeeded = false;

            newsItem.SetString("EpiserverPageId", epiServerPageId);

            //Recompiles and validates the url of the news item.
            if (!exists)
            {
                while(!succeeded)
                {
                    try
                    {
                        newsManager.RecompileAndValidateUrls(newsItem);
                        succeeded = true;
                    }
                    catch (WebProtocolException ex)
                    {
                        duplicateEntries++;
                        newsItem.UrlName = urlName + duplicateEntries;
                    } catch(Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            //Save the changes.
            newsManager.SaveChanges();

            // getting list of categories

            List<string> cats = new List<string>();

            foreach (var cat in categories)
            {
                String catName = Regex.Replace(cat["CategoryName"].ToString(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-") + "-" + cat["pkID"];
                cats.Add(catName);
            }

            AddTaxonomiesToNews(articleId, cats, new List<string>() { }, "");

            newsItem = newsManager.GetNewsItem(articleId);
            newsItem.DateCreated = publicationDate;
            newsItem.PublicationDate = publicationDate;
            newsItem.LastModified = publicationDate;
            newsItem.ApprovalWorkflowState = "Published";
            newsManager.Lifecycle.PublishWithSpecificDate(newsItem, publicationDate);
            newsManager.SaveChanges();


            //Publish the news item. The published version acquires new ID.
            /*var bag = new Dictionary<string, string>();
            bag.Add("ContentType", typeof(NewsItem).FullName);
            WorkflowManager.MessageWorkflow(articleId, typeof(NewsItem), null, "Publish", true, bag);*/
        }

        /// <summary>
        /// Adds the taxonomies to news.
        /// </summary>
        /// <param name="newsItemId">The news item id.</param>
        /// <param name="categories">The categories.</param>
        /// <param name="tags">The tags.</param>
        public static void AddTaxonomiesToNews(Guid newsItemId, IEnumerable<string> categories, IEnumerable<string> tags, string providerName)
        {
            NewsManager newsManager = null;
            if (providerName.IsNullOrEmpty())
            {
                newsManager = NewsManager.GetManager();
            }
            else
            {
                newsManager = NewsManager.GetManager(providerName);
            }

            NewsItem newsItem = newsManager.GetNewsItem(newsItemId);
            if (newsItem == null)
            {
                throw new ItemNotFoundException(string.Format(CultureInfo.CurrentCulture, "News item with id {0} was not found.", newsItemId));
            }

            var taxonomyManager = TaxonomyManager.GetManager();
            if (categories != null)
            {
                if (categories.Count() > 0)
                {
                    HierarchicalTaxon category = null;
                    foreach (var c in categories)
                    {
                        category = taxonomyManager.GetTaxa<HierarchicalTaxon>().SingleOrDefault(t => t.UrlName.ToString().Equals(c));
                        if (category != null)
                        {
                            if (!newsItem.Organizer.TaxonExists("Category", category.Id))
                            {
                                newsItem.Organizer.AddTaxa("Category", category.Id);
                            }
                        }
                    }
                }
            }

            if (tags != null)
            {
                if (tags.Count() > 0)
                {
                    FlatTaxon tag = null;
                    foreach (var tg in tags)
                    {
                        tag = taxonomyManager.GetTaxa<FlatTaxon>().SingleOrDefault(t => t.Title == tg);
                        if (tag != null)
                        {
                            if (!newsItem.Organizer.TaxonExists("Tags", tag.Id))
                            {
                                newsItem.Organizer.AddTaxa("Tags", tag.Id);
                            }
                        }
                    }
                }
            }

            newsManager.SaveChanges();
        }

        public static void CreateHierarchicalTaxonomyAndTaxon(String taxonomyName, String taxonomyDescription, String taxonomyUrl, String parentTaxonomyName)
        {
            //gets an instance of the taxonomy manager
            TaxonomyManager manager = TaxonomyManager.GetManager();

            var rootTaxonomy = manager.GetTaxonomies<HierarchicalTaxonomy>().Where(t => t.Name.ToLower().Equals("categories")).FirstOrDefault();

            if (rootTaxonomy != null)
            {
                var taxon = manager.CreateTaxon<HierarchicalTaxon>();
                taxon.Title = taxonomyName;
                taxon.Name = taxonomyName;
                taxon.UrlName = taxonomyUrl;
                taxon.Description = taxonomyDescription;
                taxon.Taxonomy = rootTaxonomy;

                // now lets try to find parent taxonomy
                var parentTaxon = String.IsNullOrEmpty(parentTaxonomyName) ? null : 
                                    manager.GetTaxa<HierarchicalTaxon>().Where(t => t.UrlName.ToString().ToLower().Equals(parentTaxonomyName)).FirstOrDefault();

                if (parentTaxon != null)
                {
                    parentTaxon.Subtaxa.Add(taxon);
                }
                else
                {
                    rootTaxonomy.Taxa.Add(taxon);
                }

                //Save all changes done up to now.
                manager.SaveChanges();
            }
        }

        /// <summary>
        /// Add New Control to a Page
        /// </summary>
        /// <param name="pageNodeId"></param>
        /// <param name="placeHolder"></param>
        /// <param name="html"></param>
        public static void AddContentControlToPage(processMigration p_object, Guid pageNodeId, string placeHolder, string html, Guid siblingId)
        {
            if (String.IsNullOrEmpty(html))
            {
                return;
            }

            html = p_object.ProcessPageLinks(html);

            var pageManager = PageManager.GetManager();
            var page = pageManager.GetPageNodes().Where(p => p.PageId == pageNodeId).SingleOrDefault();

            var contentBlock = new Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlock();
            contentBlock.Html = html;

            var draftControl = pageManager.CreateControl<PageDraftControl>(contentBlock, placeHolder);
            draftControl.Caption = "Content Block";

            var temp = pageManager.EditPage(page.GetPageData().Id);

            if (temp != null)
            {
                if (!Guid.Empty.Equals(siblingId))
                {
                    draftControl.SiblingId = siblingId;
                }
                else
                {
                    draftControl.SiblingId = GetLastControlInPlaceHolderInPageId(temp, placeHolder);
                }
                temp.Controls.Add(draftControl);

                //Save the changes
                pageManager.PublishPageDraft(temp);
                pageManager.SaveChanges();
            }
        }

        /// <summary>
        /// Add New Control to a Page
        /// </summary>
        /// <param name="pageNodeId"></param>
        /// <param name="placeHolder"></param>
        /// <param name="html"></param>
        public static void AddContentControlToPage(processMigration p_object, Guid pageNodeId, string placeHolder, string html)
        {
            AddContentControlToPage(p_object, pageNodeId, placeHolder, html, Guid.Empty);
        }

        public static void AddControlToPage(Guid pageNodeId, string placeHolder, String controlPath, String caption, Dictionary<String, String> properties)
        {
            var pageManager = PageManager.GetManager();
            var page = pageManager.GetPageNodes().Where(p => p.PageId == pageNodeId).SingleOrDefault();

            if (page != null)
            {
                var temp = pageManager.EditPage(page.GetPageData().Id);

                if (temp != null)
                {
                    var pageControl = pageManager.CreateControl<PageDraftControl>(controlPath, placeHolder);

                    foreach (KeyValuePair<string, string> property in properties)
                    {
                        var prop = pageControl.Properties.FirstOrDefault(p => p.Name == property.Key);
                        if (prop == null)
                        {
                            prop = new ControlProperty();
                            prop.Id = Guid.NewGuid();
                            prop.Name = property.Key;
                            prop.Value = property.Value;
                            pageControl.Properties.Add(prop);
                        }
                    }

                    pageControl.Caption = caption;
                    pageControl.SiblingId = GetLastControlInPlaceHolderInPageId(temp, placeHolder);
                    pageManager.SetControlDefaultPermissions(pageControl);
                    temp.Controls.Add(pageControl);

                    //pageManager.PagesLifecycle.CheckIn(temp);
                    pageManager.PublishPageDraft(temp);
                    pageManager.SaveChanges();
                }
            }
        }

        internal static void DeletePageWithEpiServerID(string pageID)
        {
            // checking if page already exists with given epiServerPageId
            if (!String.IsNullOrEmpty(pageID))
            {
                var fluentPages = App.WorkWith().Pages();
                var page = fluentPages.Where(x =>
                        x.GetString("EpiserverPageId") != null &&
                        pageID.Equals(x.GetString("EpiserverPageId"))
                    ).Get().FirstOrDefault();
                if (page != null)
                {
                    PageManager pageManager = PageManager.GetManager();

                    page.ApprovalWorkflowState = "Unpublished";
                    pageManager.UnpublishPage(page.GetPageData());
                    pageManager.SaveChanges();

                    App.WorkWith().Pages()
                        .Where(pN => (pN != null && pN.PageId.Equals(page.PageId)))
                        .Delete()
                        .SaveChanges();
                }
            }
        }

        internal static Guid UploadImageFile(string fileName, string link, string filePath, string parentDirectory)
        {
            parentDirectory = "Images - " + parentDirectory;

            LibrariesManager librariesManager = LibrariesManager.GetManager();

            //Check whether the parent DocumentLibrary exists.
            var count = 0;

            App.WorkWith().Albums().Where(i => i.Title.Equals(parentDirectory)).Count(out count);

            Guid parentDirectoryId = Guid.NewGuid();

            if (count == 0)
            {
                App.WorkWith().Album().CreateNew(parentDirectoryId)
                .Do(b =>
                {
                    b.Title = parentDirectory;
                    b.DateCreated = DateTime.UtcNow;
                    b.LastModified = DateTime.UtcNow;
                    b.BlobStorageProvider = "FileSystem";
                    b.UrlName = Regex.Replace(parentDirectory.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");
                })
                .SaveChanges();
            }
            else
            {
                Album library = librariesManager.GetAlbums().Where(b => b.Title.Equals(parentDirectory)).FirstOrDefault();

                parentDirectoryId = library.Id;
            }

            Guid documentId = GetImageFileByLinkId(link);

            // lets check if file does not exists
            if (!documentId.Equals(Guid.Empty))
            {
                return documentId;
            }
            else
            {
                documentId = Guid.Parse(link);
            }

            var fInfo = new FileInfo(filePath);

            var s = new StreamReader(filePath);

            Image document = librariesManager.GetImages().Where(d => d.Id == documentId).FirstOrDefault();

            if (document == null)
            {
                document = librariesManager.CreateImage(documentId);

                //Set the parent document library.
                Album documentLibrary = librariesManager.GetAlbums().Where(d => d.Id == parentDirectoryId).SingleOrDefault();
                document.Parent = documentLibrary;

                //Set the properties of the document.
                document.Title = fileName.Replace(fInfo.Extension, "");
                document.DateCreated = DateTime.UtcNow;
                document.PublicationDate = DateTime.UtcNow;
                document.LastModified = DateTime.UtcNow;
                document.UrlName = Regex.Replace(document.Title.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");
                document.MediaUrl = fileName;

                //Recompiles and validates the url of the document.
                librariesManager.RecompileAndValidateUrls(document);

                //Upload the document file.
                librariesManager.Upload(document, s.BaseStream, fInfo.Extension);

                //Save the changes.
                librariesManager.SaveChanges();

                //Publish the DocumentLibraries item. The live version acquires new ID.
                var bag = new Dictionary<string, string>();
                bag.Add("ContentType", typeof(Image).FullName);
                WorkflowManager.MessageWorkflow(documentId, typeof(Image), null, "Publish", false, bag);
            }

            return documentId;
        }

        internal static Guid UploadFile(string fileName, string link, string filePath, string parentDirectory)
        {
            return UploadFile(fileName, link, filePath, parentDirectory, String.Empty, String.Empty, null);
        }

        internal static Guid UploadFile(string fileName, string link, string filePath, string parentDirectory, String title, String description, DateTime? publishedDate)
        {
            // check if file is image or document
            String[] imageExtensions = new String[]
            {
                ".png", ".gif", ".jpg", ".jpeg"
            };

            for (var i = 0; i < imageExtensions.Length; i++)
            {
                if (filePath.IndexOf(imageExtensions[i]) >= 0)
                {
                    return UploadImageFile(fileName, link, filePath, parentDirectory);
                }
            }

            parentDirectory = "Files - " + parentDirectory;

            LibrariesManager librariesManager = LibrariesManager.GetManager();

            //Check whether the parent DocumentLibrary exists.
            var count = 0;

            App.WorkWith().DocumentLibraries().Where(i => i.Title.Equals(parentDirectory)).Count(out count);

            Guid parentDirectoryId = Guid.NewGuid();

            if (count == 0)
            {
                App.WorkWith().DocumentLibrary().CreateNew(parentDirectoryId)
                .Do(b =>
                {
                    b.Title = parentDirectory;
                    b.DateCreated = DateTime.UtcNow;
                    b.LastModified = DateTime.UtcNow;
                    b.BlobStorageProvider = "FileSystem";
                    b.UrlName = Regex.Replace(parentDirectory.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");
                })
                .SaveChanges();
            }
            else
            {
                DocumentLibrary library = librariesManager.GetDocumentLibraries().Where(b => b.Title.Equals(parentDirectory)).FirstOrDefault();

                parentDirectoryId = library.Id;
            }

            Guid documentId = GetFileByLinkId(link);

            // lets check if file does not exists
            if (!documentId.Equals(Guid.Empty))
            {
                return documentId;
            }
            else
            {
                documentId = Guid.Parse(link);
            }

            var fInfo = new FileInfo(filePath);

            var s = new StreamReader(filePath);

            Document document = librariesManager.GetDocuments().Where(d => d.Id == documentId).FirstOrDefault();

            if (document == null)
            {
                document = librariesManager.CreateDocument(documentId);

                //Set the parent document library.
                DocumentLibrary documentLibrary = librariesManager.GetDocumentLibraries().Where(d => d.Id == parentDirectoryId).SingleOrDefault();
                document.Parent = documentLibrary;

                //Set the properties of the document.
                document.Title = String.IsNullOrEmpty(title) ? fileName.Replace(fInfo.Extension, "") : title;
                document.DateCreated = publishedDate.HasValue ? publishedDate.Value : DateTime.UtcNow;
                document.PublicationDate = publishedDate.HasValue ? publishedDate.Value : DateTime.UtcNow;
                document.LastModified = publishedDate.HasValue ? publishedDate.Value : DateTime.UtcNow;
                document.Description = description;
                document.UrlName = Regex.Replace(document.Title.ToLower(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");
                document.MediaUrl = fileName;

                //Recompiles and validates the url of the document.
                librariesManager.RecompileAndValidateUrls(document);

                //Upload the document file.
                librariesManager.Upload(document, s.BaseStream, fInfo.Extension);

                //Save the changes.
                librariesManager.SaveChanges();

                //Publish the DocumentLibraries item. The live version acquires new ID.
                var bag = new Dictionary<string, string>();
                bag.Add("ContentType", typeof(Document).FullName);
                WorkflowManager.MessageWorkflow(documentId, typeof(Document), null, "Publish", false, bag);
            }

            return documentId;
        }

        internal static void AddLatestNewsWidgetControlToPage(Guid pId, string PlaceholderInnerOption1MainContent, List<string> categories, DateTime? dateFrom, DateTime? dateTo)
        {
            var cntrl = new Telerik.Sitefinity.Modules.News.Web.UI.NewsView();
            var def = (ContentViewMasterDefinition)cntrl.ControlDefinition.Views[0];
            QueryData d = new QueryData();
            List<QueryItem> items = new List<QueryItem>();
            int itemIndex = 0;

            if (categories.Count > 0)
            {
                // categories
                items.Add(new QueryItem()
                {
                    IsGroup = true,
                    Ordinal = itemIndex,
                    Join = "AND",
                    ItemPath = "_" + itemIndex,
                    Value = null,
                    Condition = null,
                    Name = "Categories"
                });

                for (var i = 0; i < categories.Count; i++)
                {
                    var cat = categories[i];

                    var taxonomyManager = TaxonomyManager.GetManager();
                    var cats = taxonomyManager.GetTaxa<HierarchicalTaxon>().Where(t => t.Name.ToString().Equals(cat));
                    HierarchicalTaxon category;
                    if (cats.Count() > 0)
                    {
                        category = cats.First();

                        items.Add(new QueryItem()
                        {
                            IsGroup = false,
                            Ordinal = i,
                            Join = "OR",
                            ItemPath = "_" + itemIndex + "_" + i,
                            Value = category.Id.ToString(),
                            Condition = new Condition()
                            {
                                FieldName = "Category",
                                FieldType = "System.Guid",
                                Operator = "Contains"
                            },
                            Name = cat
                        });
                    }
                }

                itemIndex++;
            }

            if (dateFrom.HasValue && dateTo.HasValue)
            {
                items.Add(new QueryItem()
                {
                    IsGroup = true,
                    Ordinal = itemIndex,
                    Join = "AND",
                    ItemPath = "_" + itemIndex,
                    Value = null,
                    Condition = null,
                    Name = "Dates"
                });
                items.Add(new QueryItem()
                {
                    IsGroup = false,
                    Ordinal = 0,
                    Join = "AND",
                    ItemPath = "_" + itemIndex + "_0",
                    Value = String.Format("{0:r}", dateFrom),
                    Condition = new Condition()
                    {
                        FieldName = "PublicationDate",
                        FieldType = "System.DateTime",
                        Operator = ">"
                    },
                    Name = dateFrom.Value.ToString()
                });
                items.Add(new QueryItem()
                {
                    IsGroup = false,
                    Ordinal = 1,
                    Join = "AND",
                    ItemPath = "_" + itemIndex + "_1",
                    Value = String.Format("{0:r}", dateTo),
                    Condition = new Condition()
                    {
                        FieldName = "PublicationDate",
                        FieldType = "System.DateTime",
                        Operator = "<"
                    },
                    Name = dateTo.Value.ToString()
                });
            }

            d.QueryItems = items.ToArray();

            def.AdditionalFilter = d;
            def.AllowPaging = true;
            def.TemplateKey = "0f830148-08b3-649f-a7d8-ff000079c5aa";
            def.ItemsPerPage = 12;
            cntrl.ControlDefinition.Views[0] = def;

            var defSingle = cntrl.ControlDefinition.Views[1];
            defSingle.TemplateKey = "f2de0165-c715-4cb3-806e-8d57843db7ab";
            cntrl.ControlDefinition.Views[1] = defSingle;

            SDMigrationSnippets.AddControlToPage(pId, cntrl, PlaceholderInnerOption1MainContent, "News", new Dictionary<string, string>());
        }
    }
}