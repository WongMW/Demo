using Aptify.Framework.Web.eBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Services;
using System.Text;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Model;

namespace SoftwareDesign.Controls.SDWidgets
{
    public partial class uc_podcastListing : BaseUserControlAdvanced
    {
        public string ChannelTitle { get; set; }
        public string ImageTitle { get; set; }
        public string DocumentLibraryName { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Copyright { get; set; }
        public string WebMaster { get; set; }
        public string ManagingEditor { get; set; }
        public string AtomLink { get; set; }
        public string LastBuildDate { get; set; }
        public string PublicationDate { get; set; }
        public string itunes_author { get; set; }
        public string itunes_summary { get; set; }
        public string itunes_subtitle { get; set; }
        public string itunes_name { get; set; }
        public string itunes_email { get; set; }
        public string itunes_explicit { get; set; }
        public string itunes_keywords { get; set; }
        public string itunes_image { get; set; }
        public string itunes_category { get; set; }

        private static string GetAbsoluteUrl(string url)
        {
            return !string.IsNullOrEmpty(url) ? RouteHelper.GetAbsoluteUrl(url) : "";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SystemManager.IsDesignMode || SystemManager.IsPreviewMode)
            {
                txtPodcastListing.Text =
                    "XML Generation Widget - only executes in non editing mode." + "<br />" +
                    "• For DocumentLibraryName, create a library and sub folders in Documents & Files and use the format \"Podcasts/2017\"" + "<br />" +
                    "• For itunes_category use the format \"Business/Business News, Business/Careers, News & Politics\"";
                return;
            }
            
            string thisPageUrl = SiteMapBase.GetActualCurrentNode().Url;

            // default title settings
            if(String.IsNullOrEmpty(ChannelTitle))
            {
                ChannelTitle = "Accountancy Ireland";
            }
            if (String.IsNullOrEmpty(ImageTitle))
            {
                ImageTitle = "Accountancy Ireland";
            }
            // ---

            var xml = new StringBuilder();

            xml.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            xml.AppendLine("<rss xmlns:atom=\"http://www.w3.org/2005/Atom\" xmlns:itunes=\"http://www.itunes.com/dtds/podcast-1.0.dtd\" version=\"2.0\">");
            xml.AppendLine("  <channel>");

            xml.AppendLine("    <title>" + ChannelTitle + "</title>");
            xml.AppendLine("    <link>" + HttpUtility.HtmlEncode(GetAbsoluteUrl(Link)) + "</link>");
            xml.AppendLine("    <image>");
            {
                xml.AppendLine("      <url>" + HttpUtility.HtmlEncode(GetAbsoluteUrl(itunes_image)) + "</url>");
                xml.AppendLine("      <title>" + ImageTitle + "</title>");
                xml.AppendLine("      <link>" + HttpUtility.HtmlEncode(GetAbsoluteUrl(Link)) + "</link>");
            }
            xml.AppendLine("    </image>");
            xml.AppendLine("    <description>" + HttpUtility.HtmlEncode(Description) + "</description>");
            xml.AppendLine("    <language>en-gb</language>");
            xml.AppendLine("    <copyright>" + HttpUtility.HtmlEncode(Copyright) + "</copyright>");
            xml.AppendLine("    <webMaster>" + HttpUtility.HtmlEncode(WebMaster) + "</webMaster>");
            xml.AppendLine("    <managingEditor>" + HttpUtility.HtmlEncode(ManagingEditor) + "</managingEditor>");
            xml.AppendLine("    <atom:link href=\"" + HttpUtility.HtmlEncode(GetAbsoluteUrl(thisPageUrl)) + "\" rel=\"self\" type=\"application/rss+xml\"/>");
            xml.AppendLine("    <lastBuildDate>" + HttpUtility.HtmlEncode(LastBuildDate) + "</lastBuildDate>");
            xml.AppendLine("    <pubDate>" + HttpUtility.HtmlEncode(PublicationDate) + "</pubDate>");

            xml.AppendLine("    <itunes:author>" + HttpUtility.HtmlEncode(itunes_author) + "</itunes:author>");
            xml.AppendLine("    <itunes:summary>" + HttpUtility.HtmlEncode(itunes_summary) + "</itunes:summary>");
            xml.AppendLine("    <itunes:subtitle>" + HttpUtility.HtmlEncode(itunes_subtitle) + "</itunes:subtitle>");
            xml.AppendLine("    <itunes:owner>");
            {
                xml.AppendLine("      <itunes:name>" + HttpUtility.HtmlEncode(itunes_name) + "</itunes:name>");
                xml.AppendLine("      <itunes:email>" + HttpUtility.HtmlEncode(itunes_email) + "</itunes:email>");
            }
            xml.AppendLine("    </itunes:owner>");
            xml.AppendLine("    <itunes:explicit>" + HttpUtility.HtmlEncode(itunes_explicit) + "</itunes:explicit>");
            xml.AppendLine("    <itunes:keywords>" + HttpUtility.HtmlEncode(itunes_keywords) + "</itunes:keywords>");
            xml.AppendLine("    <itunes:image href=\"" + HttpUtility.HtmlEncode(GetAbsoluteUrl(itunes_image)) + "\"/>");

            IReadOnlyCollection<IReadOnlyCollection<string>> categoryPaths = itunes_category
                .Split(',')
                .Select(
                    categoryPath => categoryPath
                        .Split('/')
                        .Select(categoryNode => HttpUtility.HtmlEncode(categoryNode.Trim()))
                        .ToArray()
                )
                .ToArray();
            
            foreach (IGrouping<string, IReadOnlyCollection<string>> topLevelCategory in categoryPaths.GroupBy(categoryNodes => categoryNodes.First()))
            {
                int depth = 4;
                xml.AppendLine(new string(' ', depth) + "<itunes:category text=\"" + topLevelCategory.Key + "\">");

                depth += 2;
                IEnumerable<IEnumerable<string>> childCategoryPathsExcludingTopLevelNode = topLevelCategory.Select(pathIncludingTopLevelNode => pathIncludingTopLevelNode.Skip(1));
                foreach (IEnumerable<string> childCategoryNodes in childCategoryPathsExcludingTopLevelNode)
                {
                    foreach (string categoryNode in childCategoryNodes)
                    {
                        xml.AppendLine(new string(' ', depth) + "<itunes:category text=\"" + categoryNode + "\">");
                        depth += 2;
                    }
                    foreach (string categoryNode in childCategoryNodes)
                    {
                        depth -= 2;
                        xml.AppendLine(new string(' ', depth) + "</itunes:category>");
                    }
                }
                depth -= 2;

                xml.AppendLine(new string(' ', depth) + "</itunes:category>");
            }
            
            // lets get all documents under DocumentLibraryName
            var librariesManager = LibrariesManager.GetManager();

            IFolder folderOrNull = null;
            string[] folderNames = DocumentLibraryName.Split('/');
            using (IEnumerator<string> folderNameEnumerator = folderNames.Cast<string>().GetEnumerator())
            {
                if (folderNameEnumerator.MoveNext())
                {
                    folderOrNull = librariesManager
                        .GetDocumentLibraries()
                        .FirstOrDefault(library => library.Title.Equals(folderNameEnumerator.Current));

                    while(folderOrNull != null && folderNameEnumerator.MoveNext())
                    {
                        folderOrNull = librariesManager
                            .GetChildFolders(folderOrNull)
                            .FirstOrDefault(childFolder => childFolder.Title.Equals(folderNameEnumerator.Current));
                    }
                }
            }

            if (folderOrNull != null)
            {
                //Note Bugs in Sitefinity: item.Status is always Live and item.UIStatus is always Draft
                Document[] documents = librariesManager
                    .GetChildItems(folderOrNull)
                    .ToArray()
                    .Select(item => item as Document)
                    .Where(item => item != null && item.IsPublished())
                    .GroupBy(item => item.Title.ToString())
                    .Select(group => group.FirstOrDefault())
                    .OrderBy(document => document.Ordinal)
                    .ToArray();

                foreach (Document document in documents)
                {
                    var duration = document.GetValue("itunes_duration");
                    var subtitle = document.GetValue("itunes_subtitle");

                    string urlWithoutQueryString = document.Url.Substring(0, document.Url.IndexOf("?"));

                    //https://stackoverflow.com/questions/284775/how-do-i-parse-and-convert-datetime-s-to-the-rfc-822-date-time-format
                    const string rfc822Format = "r";

                    xml.AppendLine("    <item>");
                    xml.AppendLine("      <guid>" + HttpUtility.HtmlEncode(urlWithoutQueryString) + "</guid>");
                    xml.AppendLine("      <title>" + HttpUtility.HtmlEncode(document.Title) + "</title>");
                    xml.AppendLine("      <description>" + HttpUtility.HtmlEncode(document.Description) + "</description>");
                    xml.AppendLine("      <itunes:summary>" + HttpUtility.HtmlEncode(document.Description) + "</itunes:summary>");
                    xml.AppendLine("      <itunes:subtitle>" + HttpUtility.HtmlEncode(subtitle) + "</itunes:subtitle>");
                    xml.AppendLine("      <enclosure url=\"" + HttpUtility.HtmlEncode(urlWithoutQueryString) + "\" length=\"" + document.TotalSize + "\" type=\"audio/mpeg\"/>");
                    xml.AppendLine("      <itunes:duration>" + HttpUtility.HtmlEncode(duration) + "</itunes:duration>");
                    xml.AppendLine("      <pubDate>" + document.PublicationDate.ToString(rfc822Format) + "</pubDate>");
                    xml.AppendLine("    </item>");
                }
            }

            // -----

            xml.AppendLine("  </channel>");
            xml.AppendLine("</rss>");

            Response.Clear();
            Response.ContentType = "text/xml";
            Response.Write(xml.ToString());
            Response.End();
        }
        
        public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("SoftwareDesign.Controls.SDWidgets.uc_podcastListing.ascx");
    }
}
