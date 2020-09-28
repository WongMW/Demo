using Aptify.Framework.Web.eBusiness;
using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Web.UI;
using System.Xml;
using System.Text;

namespace SoftwareDesign.Controls.SDWidgets
{
    public class PodcastRSSItem
    {
        public String Title { get; set; }
        public String Description { get; set; }
        public String PubDate { get; set; }
        public String Link { get; set; }
    }

    public partial class uc_podcastDisplay : BaseUserControlAdvanced
    {
        public String PodcastURL { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(PodcastURL))
            {
                XmlDocument rssXmlDoc = new XmlDocument();
                List<PodcastRSSItem> items = new List<PodcastRSSItem>();
                try
                {
                    rssXmlDoc.Load(PodcastURL);

                    // Parse the Items in the RSS file
                    XmlNodeList rssNodes = rssXmlDoc.SelectNodes("rss/channel/item");

                    StringBuilder rssContent = new StringBuilder();

                    // Iterate through the items in the RSS file
                    foreach (XmlNode rssNode in rssNodes)
                    {
                        XmlNode rssSubNode = rssNode.SelectSingleNode("title");
                        string title = rssSubNode != null ? rssSubNode.InnerText : "";

                        rssSubNode = rssNode.SelectSingleNode("enclosure");
                        bool hasLink =
                            rssSubNode != null &&
                            rssSubNode.Attributes != null &&
                            rssSubNode.Attributes["url"] != null &&
                            rssSubNode.Attributes["url"].Value != null;
                        string link = hasLink? rssSubNode.Attributes["url"].Value : "";
                        
                        rssSubNode = rssNode.SelectSingleNode("description");
                        string description = rssSubNode != null ? rssSubNode.InnerText : "";

                        rssSubNode = rssNode.SelectSingleNode("pubDate");
                        string pubDate = rssSubNode != null ? rssSubNode.InnerText : "";

                        items.Add(new PodcastRSSItem()
                        {
                            Title = title,
                            Description = description,
                            Link = link,
                            PubDate = pubDate
                        });
                    }

                    podcastRepeater.DataSource = items;
                    podcastRepeater.DataBind();
                }
                catch (Exception ex)
                {
                    txtPodcastURL.Text = ex.Message;
                }
            }
        }

        public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("SoftwareDesign.Controls.SDWidgets.uc_podcastDisplay.ascx");
    }
}