using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoftwareDesign.Search.Models;
namespace SoftwareDesign.Search.ViewModels
{
    public class ResultViewModel
    {
        /// <summary>
        /// Total number of hits returned message
        /// </summary>
        /// /// <value>The TotalHitsMessage.</value>
//        public String SimpleTotalHitsMessage { get; set; }

        /// <summary>
        /// Gets or sets the total hits message for the secondary hit info
        /// </summary>
        /// <value>
        /// The secondary total hits message.
        /// </value>
//        public String SecondaryTotalHitsMessage { get; set; }

        /// <summary>
        /// Total number of hits returned
        /// </summary>
        /// /// <value>The TotalHits.</value>
        public Int32 NumHits { get; set; }
        
        /// <summary>
        /// Gets or sets the combined hit count
        /// Hit count for the current query plus the alternate indexes
        /// </summary>
        /// <value>
        /// The combined hit count.
        /// </value>
//        public String CombinedHitCountMessage { get; set;}

        /// <summary>
        /// Gets or sets the combined hit count.
        /// Hit count for the current query plus the alternate indexes

        /// </summary>
        /// <value>
        /// The combined hit count.
        /// </value>
        public Int32 AllIndexesNumHits { get; set; }

        /// <summary>
        /// The query term
        /// </summary>
        /// /// <value>The QueryTerm.</value>
        public String QueryTerm { get; set; }

        /// <summary>
        /// Gets or sets the name of the index.
        /// </summary>
        /// <value>
        /// The name of the index.
        /// </value>
        public String indexName { get; set; }

        /// <summary>
        /// Gets or sets the display name of the index.
        /// </summary>
        /// <value>
        /// The display name of the index.
        /// </value>
        public String indexDisplayName { get; set; }

        /// <summary>
        /// selected refiners
        /// </summary>
        /// /// <value>The SelectedRefiner.</value>
        public IEnumerable<RefinerItem> SelectedRefiners { get; set; }

        /// <summary>
        /// Pagination Model
        /// </summary>
        /// /// <value>The Pager.</value>
        public Pager Pager { get; set; }

        /// <summary>
        /// The Model containing all of the Hits
        /// </summary>
        /// /// <value>The ResultModel.</value>
        public IEnumerable<ResultsModel> ResultModel { get; set; }

        /// <summary>
        /// The model containing all of the sub-category refiners
        /// </summary>
        /// /// <value>The RefinersModel.</value>
        public IEnumerable<Refiner> RefinersModel { get; set; }

        /// <summary>
        /// Model to store array of alternative sources
        /// ie all the other index sources not included in the current search
        /// along with hit counts if they were to be selected
        /// </summary>
        /// /// <value> Alternative RefinersModel </value>
        public IEnumerable<Refiner> AlternativesModel { get; set; }

        /// <summary>
        /// Gets or sets the spell suggestions.
        /// </summary>
        /// <value>
        /// The spell suggestions.
        /// </value>
        public IEnumerable<SuggestsModel> SpellSuggestions { get; set; }

    }
}