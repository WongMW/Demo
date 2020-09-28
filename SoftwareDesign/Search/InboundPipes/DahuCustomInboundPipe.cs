using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Publishing.Pipes;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using System.Threading;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Libraries.Model;
using System.Linq;
using System.Globalization;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.GenericContent.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Web;
using Nest;
using System.Text.RegularExpressions;
using System.Reflection;


/// <summary>
/// Summary description for CustomPageInboundPipe
/// </summary>
/// 
namespace SoftwareDesign.Search.InboundPipes
{

    public class DahuUtils
    {

        private static string[] excludes = {"not-in-use-currently",
                           "/knowledge-centre/tax/tax-possible-delete",
                           "/current-student/review-for-deletion",
                           "/customerservice",
                           "/abstractmanagement",
                           "/forums",
                           "/meetingseventsmanagement",
                           "/committees"
                       };


        // Rules for excluding pages from the index
        // input = subSection metadata attribute for the page
        // output = true if the subSection is valid - false if it is in the blacklist
        public static bool canIndexPage(String p)
        {
            foreach (string e in excludes){
                if (p.IndexOf(e) >= 0)
                {
                    return false;
                }
            }
            return true; // The URL did not match our blacklist so it should be OK
        }

        // Input = URL for page
        // Output = first URL fragment after the hostname/domain name or "Miscellaneous" if not a valid URL fragment
        // eg input = "http://www.charteredaccountants.ie/Events/myevent.aspx?foo=bar"  => return "Events"
        // eg input = "/errorpage.aspx" => return ""
        public static string getUrlContext(String url)
        {
            // URL could be absolute, relative, or start with '~/'

            if (url.StartsWith("http")  )
            {
                url = url.Substring(url.IndexOf("//") + 2);
                if (url.IndexOf("/") > 0)
                {
                    url = url.Substring(url.IndexOf("/")+1);
                } // if URL doesn't have a "/" in it, then it must be just the hostname  eg "http://www.charteredaccountants.ie"
            }
            else if (url.StartsWith("~/"))
            {
                url = url.Substring(2);
            }
            else if (url.StartsWith("/"))
            {
                url = url.Substring(1);
            }
            else
            {
                return "Miscellaneous";  // Something not right about this URL so stick it in the Misc bucket
            }

            // OK should now have a relative URL with no hostname and without any leading "/"
            if (url.IndexOf("/") > 0)
            {
                url = url.Substring(0,url.IndexOf("/"));
            }

            if (url.EndsWith(".aspx") || url.EndsWith(".asp") || url.EndsWith(".html") || url.EndsWith(".htm"))
            {
                return "Miscellaneous";
            }
            else
            {
                return url;
            }
        }

        /**
         * 
         * Need to escape whitespace in category names because Sitefinity does not allow us to change elasticsearch tokenisation 
         * 
         * */
        public static void setSiteSubsection(string subsection, Telerik.Sitefinity.Publishing.WrapperObject wrapperObject)
        {
            if (subsection != null && wrapperObject != null)
            {

                subsection = subsection.ToLower();
                subsection = subsection.Replace(" ", "_");
                subsection = subsection.Replace("-","_");
                wrapperObject.SetOrAddProperty("SiteSubsection", subsection);
            }
        }

    }

    /**
     *
     * PAGES are sent to this pipeline where we can find static HTML content blocks, plus public fields in TitleWidget and AboutWidget
     * 
     */
    public class CustomPageInboundPipe : PageInboundPipe
    {
        string message;

        // http://www.sitefinity.com/blogs/veselin-vasilev-blog/2013/06/20/how-to-exclude-a-page-from-sitefinity-internal-search
        protected override IEnumerable<PageNode> LoadPageNodes()
        {
            return base.LoadPageNodes().Where(n => base.CanProcessItem(n));
                
        }


 public override void PushData(IList<PublishingSystemEventInfo> items)
        {


            var node = new Uri("http://elastic-test:9200");
            var settings = new ConnectionSettings(node);
            settings.DefaultIndex("website");
            var client = new ElasticClient(settings);
        
            var searchIndexName = this.PipeSettings.PublishingPoint.Name.ToLower();
            string document_content = string.Empty;

            foreach (PublishingSystemEventInfo item in items)
            {

                document_content = string.Empty;

                if (item.ItemType != typeof(PageData).FullName && item.ItemType != typeof(PageNode).FullName)
                {
                    continue;
                }

                string itemAction = item.ItemAction;
                string str = itemAction;

                if (itemAction == null)
                {
                    continue;
                }
                if (str == "SystemObjectDeleted")
                {

                    client.Delete<DahuIndexablePage>(this.GetPageNode(item).Id.ToString());
                } else if (str == "SystemObjectAdded" || str == "SystemObjectModified")
                {

                    var pageNode = this.GetPageNode(item);

                    if (!this.ShouldProcessNode(pageNode, item))
                    {
                        continue;
                    }
                    var culture = CultureInfo.GetCultureInfo("en");
                    var url = String.Empty;
                    // Get the URL of the pageNode
                    url = pageNode.GetUrl(culture);
                    url = UrlPath.ResolveUrl(url, true, true);

                    if (url.Contains("not-in-use-currently"))
                    {
                        continue;
                    }

                    if (pageNode.Title.ToLower().Equals("article-item"))
                    {
                        continue;
                    }

                    if (pageNode.Title.ToLower().Equals("ai-articles"))
                    {
                        continue;
                    }

                    try
                    {
                        PageManager pManager = PageManager.GetManager();
                        ContentManager contentmanager = ContentManager.GetManager();
                        string pagetitle = string.Empty;

                        foreach (PageControl ctrl in pageNode.GetPageData().Controls)
                        {
                            try
                            {
                                var mycontrol = ctrl;
                                var control = pManager.LoadControl(ctrl);


                                if (control is ContentBlock)
                                {
                                    var mycontrol1 = ctrl;

                                    ContentBlock content = control as ContentBlock;
                                    document_content = document_content + " " + content.Html;

                                }
                                else if (control.GetType().ToString().EndsWith("TitleWidget"))  // "AboutWidget"   - need Title and Subtitle
                                {
                                    
                                    System.Web.UI.Control titleWidgetControl = control as System.Web.UI.Control;
                                    foreach (System.Web.UI.Control values in titleWidgetControl.Controls)
                                    {
                                        System.Web.UI.ControlCollection messageLabel = values.Controls;
                                        foreach (Object o in messageLabel){
                                            if (o is System.Web.UI.Control)
                                            {
                                                System.Web.UI.Control messageLabelControl = o as System.Web.UI.UserControl;
                                                foreach (Object o1 in messageLabelControl.Controls)
                                                {
                                                    if (o1 is System.Web.UI.WebControls.Label)
                                                    {
                                                        document_content = document_content + " " + ((System.Web.UI.WebControls.Label)o1).Text;
                                                        if (pageNode.Title == "Home" && ! ((System.Web.UI.WebControls.Label)o1).Text.IsNullOrEmpty())
                                                        {
                                                            pagetitle = ((System.Web.UI.WebControls.Label)o1).Text;
                                                        }
                                                    }
                                                }

                                            }
                                        }
                                    }
                                     

                                }
/*
                                else if (control.GetType().ToString().EndsWith("AboutWidget"))
                                {

                                    Telerik.Sitefinity.Web.UI.SimpleView aboutWidgetSimpleView = control as Telerik.Sitefinity.Web.UI.SimpleView;
                                    foreach (var aboutWidgetControl in aboutWidgetSimpleView.Controls)
                                    {
                                        if (aboutWidgetControl is Telerik.Sitefinity.Web.UI.GenericContainer)
                                        {
                                            Telerik.Sitefinity.Web.UI.GenericContainer aboutWidgetContainer = aboutWidgetControl as Telerik.Sitefinity.Web.UI.GenericContainer;

                                            foreach (var aboutWidgetControlValues in ((Telerik.Sitefinity.Web.UI.GenericContainer)aboutWidgetControl).Controls)
                                            {
                                                var allFields = aboutWidgetControlValues.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).OrderBy(pi => pi.Name).ToList();
                                                foreach (FieldInfo myField in allFields)
                                                {
                                                    var myValue = myField.GetValue(aboutWidgetControlValues);
                                                    if (myValue is System.Web.UI.WebControls.Label)
                                                    {
                                                        document_content = document_content + " " + ((System.Web.UI.WebControls.Label)myValue).Text;
                                                    }
                                                }
                                            }
                                        }    
                                    }
                                }
 */
                            } catch (Exception e1){
                                // Exception in loading a control - its someone else's control so its their problem to fix! Cannot index any content on the control so ignore the exception. 
                                message = "PageInboundPipe : PushData : Exception : " + e1.ToString();
                                Telerik.Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Writer.Write(message);

                            }
                        }

                        document_content = Regex.Replace(document_content, "<.*?>", String.Empty);
                        document_content = Regex.Replace(document_content, "&nbsp;", String.Empty);
                        document_content = Regex.Replace(document_content, "&rsquo;", "'");
                        document_content = Regex.Replace(document_content, "&amp;", "&");

                        if (pagetitle == string.Empty)
                        {
                            pagetitle = pageNode.Title;
                        }

                        var pageToIndex = new DahuIndexablePage
                        {
                            id = pageNode.Id.ToString(),
                            title = pagetitle,
                            originalItemId = pageNode.Id.ToString(),
                            contentType = "Telerik.Sitefinity.Pages.Model.PageNode",
                            summary = pagetitle,
                            content = document_content,
                            siteSection = "Website",
                            language = "",
                            elaticsearchIdPropertyName = pageNode.Id.ToString(),
                            pipeId = ""

                        };

                        if (str == "SystemObjectModified")
                        {

                            client.Delete<DahuIndexablePage>(pageNode.Id.ToString());
                        }

                        client.Index(pageToIndex);



                    } catch (Exception e){
                        message = "PageInboundPipe : PushData : Loop :: Exception " + e.ToString() ;
                        Telerik.Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Writer.Write(message);
                    }

                }
            }
        }

        
        private bool ShouldProcessNode(PageNode node, PublishingSystemEventInfo item = null)
        {
            CultureInfo cultureInfo;
            if (node == null || node.NodeType == NodeType.Group || node.HasLinkedNode() || node.GetPageData(null) == null || !node.IncludeInSearchIndex || node.IsBackend)
            {
                return false;
            }
            if (item != null && item.ItemAction != "SystemObjectDeleted")
            {
                if (item.Language != null)
                {
                    cultureInfo = CultureInfo.GetCultureInfo(item.Language);
                }
                else
                {
                    cultureInfo = null;
                }
                if (!this.IsPagePublished(node, cultureInfo))
                {
                    return false;
                }
            }

            // DO not index the "Enrol on Course" pages
            if (node != null && node.Id.Equals("ca070148-08b3-649f-a7d8-ff000079c5aa")){
                return false;
            }

            return true;
        }

        private bool IsPagePublished(PageNode node, CultureInfo culture)
        {
            PageData pageData = node.GetPageData(culture);
            if (SystemManager.CurrentContext.AppSettings.Multilingual && culture.Equals(SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage) && pageData.PublishedTranslations.Count == 0 && pageData.Visible)
            {
                return true;
            }
            return pageData.IsPublishedInCulture(culture);
        }

    }

    /// <summary>
    /// Summary description for CustomContentInboundPipe
    /// </summary>
    public class CustomContentInboundPipe : Telerik.Sitefinity.Publishing.Pipes.ContentInboundPipe
    {

        string message = string.Empty;

        protected override void SetProperties(WrapperObject wrapperObject, IContent contentItem)
        {

            base.SetProperties(wrapperObject, contentItem);
            wrapperObject.SetOrAddProperty("SiteSection", "Website");
        }

    }



    public class CustomDocumentInboundPipe : DocumentInboundPipe
    {

        string message = string.Empty;
         
        
        public override IEnumerable<WrapperObject> GetConvertedItemsForMapping(params object[] items)
        {
            var baseItems = base.GetConvertedItemsForMapping(items);


            foreach (var item in baseItems)
            {
                (item as WrapperObject).AddProperty("SiteSection", "Website");
            }
            
            return baseItems;
        }
          
         
    }

    public class DahuIndexablePage
    {
        public string id { get; set; }
        public string title { get; set;  }
        public string originalItemId { get; set; }
        public string contentType { get; set; }
        public string link { get; set; }
        public string summary { get; set; }
        public string content {  get; set;}
        public string siteSection { get; set; }
        public string language { get; set; }
        public string elaticsearchIdPropertyName { get; set; }
        public string pipeId {get; set;}
    }

}