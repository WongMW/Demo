using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftwareDesign.Search.Models
{
    public class ResultsModel
    {
        /// <summary>
        /// Result Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Result Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Result Content\Body
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Result Content Type
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Results Hit Highlighted summary text
        /// </summary>
        public string HitHighlightedSummary { get; set; }

        /// <summary>
        /// Result Keywords
        /// </summary>
        public string Keywords { get; set; }        

        /// <summary>
        /// Result Url Name
        /// </summary>
        public string UrlName { get; set; }

        /// <summary>
        /// Result Url
        /// </summary>
        public string ResultUrl { get; set; }

        /// <summary>
        /// Result Filter - dahuSiteSection
        /// </summary>
        public string SiteSection { get; set; }

        /// <summary>
        /// Result Filter - dahuSiteSubsection
        /// </summary>
        public string SiteSubsection { get; set; }
 
        /// <summary>
        /// Result Link - URL field for non-Sitefinity content
        /// </summary>
        public string link {get; set; }

        /// <summary>
        /// Gets or sets the is_secure property 
        /// </summary>
        /// <value>
        /// is_secure = true if the URL requires authorization to access
        /// </value>
        public Int32 is_secure { get; set; }

        /// <summary>
        /// Gets or sets the name of the index.
        /// </summary>
        /// <value>
        /// The name of the index.
        /// </value>
        public string indexName { get; set; }

    
    }
}