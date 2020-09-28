using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using SoftwareDesign.Search.Elastic;
using SoftwareDesign.Search.ViewModels;
using SoftwareDesign.Search.Models;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using Aptify.Framework.Web.eBusiness;

namespace SoftwareDesign.Controls.SDWidgets
{
    public partial class uc_searchResults : BaseUserControlAdvanced
    {
        public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("SoftwareDesign.Controls.SDWidgets.uc_searchResults.ascx");

        [Category("Elasticsearch node url"), Description("Elasticsearch URL")]
        public String ElasticsearchUrl { get; set; }

        [Category("List of Refiner Properties"), Description("A list of properties to refined by separated by a semicolon (;)")]
        public String RefinerList { get; set; }

        Int32 CategoryCount = 0;
        // AlternateIndexHasHits = true if at least one of the other indexes has at least one hit
        Boolean AlternateIndexHasHits = false;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["ReturnToPage"] == null)
            {
                Session["ReturnToPage"] = Request.RawUrl;
            }

            string searchQuery = HttpUtility.UrlDecode(Request.QueryString["searchQuery"]);
            string index = Request.QueryString["indexCatalogue"];
            string refinerCategory = Request.QueryString["r"];

            if (!String.IsNullOrEmpty(searchQuery) && !String.IsNullOrEmpty(index))
            {
                PerformSearch search = new PerformSearch();
                ResultViewModel resultViewModel = new ResultViewModel();

                search.RefinerList = RefinerList;
                search.ElasticsearchUrl = ElasticsearchUrl;
                resultViewModel = search.SearchIndex();

             
               
                if (resultViewModel != null && resultViewModel.NumHits > 0)
                {
                    // Bind the search results
                    BindResults(resultViewModel);


                    // Display the appropriate panels
                    pnlResultPanel.Visible = true;
                    pnlNoResults.Visible = false;
                    pnlShowMore.Visible = true;
                    
                  
                    if (( resultViewModel.indexName.Equals("taxsource")) &&  CategoryCount > 0 || !String.IsNullOrEmpty(refinerCategory) )  
                        // We have some categories so show the Refiner panel
                    {
                        pnlRefineZone.Visible = true;
                        if (CategoryCount > 7)  // More than 7 categories so show the Show More/Less toggle
                        {
                            pnlShowMore.Visible = true;
                        }
                        else
                        {
                            pnlShowMore.Visible = false;
                        }
                        if (!string.IsNullOrEmpty(refinerCategory))
                        {
                            pnlRefineByTax.Visible = false;
                            pnlRefinedByTax.Visible = true;
                        }
                        else
                        {
                            pnlRefineByTax.Visible = true;
                            pnlRefinedByTax.Visible = false;

                        }
                    }
                    else
                    {
                        pnlRefineZone.Visible = false;
                    }

                    if (index != null && index.Equals("_all"))
                    {
                        pnlSelectSourceSelected.Visible = false;
                        pnlSelectNoSourceSelected.Visible = true;
                    }
                    else
                    {
                        pnlSelectSourceSelected.Visible = true;
                        pnlSelectNoSourceSelected.Visible = false;

                    }
                }
                else
                {
                    BindSuggests(resultViewModel);
                    // Display the appropriate panels
                    pnlResultPanel.Visible = false;
                    pnlNoResults.Visible = true;
                    if (index.Equals("_all")){
                        lblNoResultText.Text = "No results were found for " + searchQuery;
                    } else {
                        lblNoResultText.Text = "No results were found for " + searchQuery + " in " + resultViewModel.indexDisplayName;
                    }
                    lblNoResultHelpMessage.Text = "If you looking for a specific page and you can’t find it here, you can send an email to webmaster@charteredaccountants.ie and we will try and help you find what you are looking for.";
                }
            }
            else
            {
                // Display the appropriate panels
                pnlResultPanel.Visible = false;
                pnlNoResults.Visible = true;
            }
        }

        /// <summary>
        /// Binds the suggests. No results so look for the type-ahead suggests to bind to the control
        /// </summary>
        /// <param name="resultViewModel">The result view model.</param>
        private void BindSuggests(ResultViewModel resultViewModel)
        {
            try
            {
                rptSuggests.DataSource = resultViewModel.SpellSuggestions;
                rptSuggests.DataBind();
            } catch(Exception ex)
            {
                Helper.LogApplicationLevelException("uc_searchResults", "BindSuggests", ex);
            }

            // Number of results from running query over all indexes
            Int32 AllIndexesNumHits = resultViewModel.AllIndexesNumHits;
            if (AllIndexesNumHits > 0)
            {

                // Build the link to !"Search All Content"
                lnk_SearchAllIndexesZeroHits.Text = "Expand your search (" + AllIndexesNumHits.ToString() + " possible hits are available)";
                lnk_SearchAllIndexesZeroHits.NavigateUrl = "/search-landing?searchQuery=" + resultViewModel.QueryTerm + "&indexCatalogue=_all";
            }

        }

        /// <summary>
        /// Bind the results to the controls
        /// </summary>
        /// <param name="resultViewModel">resultViewModel</param>
        private void BindResults(ResultViewModel resultViewModel)
        {
            string index = Request.QueryString["indexCatalogue"];

            if (resultViewModel.RefinersModel.Count() == 1)
            {
                // We have one facet in the results - lets see how many categories
                Refiner _refiner = resultViewModel.RefinersModel.FirstOrDefault();
                if (_refiner != null)
                {
                    CategoryCount = _refiner.RefinementItems.Count;
                }

            }

            // Number of results from running query over all indexes
            Int32 AllIndexesNumHits = resultViewModel.AllIndexesNumHits;
    
            // Number of results from running query with filters over a selected index
            Int32 ThisQueryNumHits = resultViewModel.NumHits;

            // There are some hits in at least one of the other non-selected index
            if (AllIndexesNumHits > ThisQueryNumHits)
            {
                AlternateIndexHasHits = true;
            }

            lblPrimarySearchLabel.Text = "You searched for " + resultViewModel.QueryTerm;
            lblPrimaryResultCount.Text = AllIndexesNumHits.ToString() + " results";

            // Build the link to !"Search All Content"
            lnk_SearchAllIndexes.Text = "Search All Content (" + AllIndexesNumHits.ToString() + ")";
            lnk_SearchAllIndexes.NavigateUrl = "/search-landing?searchQuery=" + resultViewModel.QueryTerm + "&indexCatalogue=_all";


            lnk_RemoveRefine.Text = resultViewModel.indexDisplayName+" ("+ThisQueryNumHits.ToString()+")";
            lnk_RemoveRefine.NavigateUrl = "/search-landing?searchQuery=" + resultViewModel.QueryTerm + "&indexCatalogue=_all";

            String hitLabel = (ThisQueryNumHits > 1) ? "hits" : "hit";
            String indexLabel = (index.Equals("_all")) ? " " + hitLabel + " from all content sources" : " " + hitLabel + " from <span class='result_source result_" + index + "'>" + resultViewModel.indexDisplayName + "</span>";

            lblDetailedResultCount.Text = "Search Results : " + ThisQueryNumHits.ToString() + indexLabel;

            Int32 currentPage = resultViewModel.Pager.CurrentPage;
            Int32 firstHit = resultViewModel.Pager.PageSize * (resultViewModel.Pager.CurrentPage - 1) + 1;
            Int32 lastHit = 0;
            if (firstHit + resultViewModel.Pager.PageSize > resultViewModel.NumHits)
            {
                lastHit = resultViewModel.NumHits;
            }
            else
            {
                lastHit = firstHit + resultViewModel.Pager.PageSize -1;
            }

            if (lastHit == firstHit)
            {
                lblShowingHits.Text = "";
            }
            else
            {
                lblShowingHits.Text = "showing results " + firstHit.ToString() + " to " + lastHit.ToString();
            }

            try
            {
                // Bind the result list to the control
                rptResults.DataSource = resultViewModel.ResultModel;
                rptResults.DataBind();
            }
            catch (Exception ex)
            {
                Helper.LogApplicationLevelException("uc_searchResults", "Bind the result list to the control", ex);
            }

            // Bind the refiner groups list to the control
            if (index.ToLower().Equals("taxsource"))
            {
                try
                {
                    rptRefinement.DataSource = resultViewModel.RefinersModel;
                    rptRefinement.DataBind();
                }
                catch (Exception ex)
                {
                    Helper.LogApplicationLevelException("uc_searchResults", "Bind the refiner groups list to the control (1)", ex);
                }
            }

            try
            {
                // Bind the refiner groups list to the control
                rptAlternateIndex.DataSource = resultViewModel.AlternativesModel;
                rptAlternateIndex.DataBind();
            }
            catch (Exception ex)
            {
                Helper.LogApplicationLevelException("uc_searchResults", "Bind the refiner groups list to the control (2)", ex);
            }

            try
            {
                // Bind selected refiner info
                searchBreadcrumb.DataSource = resultViewModel.SelectedRefiners;
                searchBreadcrumb.DataBind();
            }
            catch (Exception ex)
            {
                Helper.LogApplicationLevelException("uc_searchResults", "Bind selected refiner info", ex);
            }


            // Bind paging information
            BindPagingControl(resultViewModel.Pager);
        }       

        #region Databinding functions

        /// <summary>
        /// Bind the Refinement Items with their values and counts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptRefinement_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Find the child refinement items repeater
            Repeater rptRefinementItems = (Repeater)e.Item.FindControl("rptRefinementItems");
            
            try
            {
                // Get the refinement items list and bind it to the repeater
                Refiner refinerList = (Refiner)e.Item.DataItem;
                List<RefinerItem> refinementItems = refinerList.RefinementItems;
                rptRefinementItems.DataSource = refinementItems;
                rptRefinementItems.DataBind();
            }
            catch (Exception ex)
            {
                Helper.LogApplicationLevelException("uc_searchResults", "rptRefinement_ItemDataBound", ex);
            }
        }


        /// <summary>
        /// Bind the Alternate Index Items with their values and counts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptAlternateIndex_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Find the child refinement items repeater
            Repeater rptAlternateItems = (Repeater)e.Item.FindControl("rptAlternateItems");
            
            try
            {
                // Get the refinement items list and bind it to the repeater
                Refiner refinerList = (Refiner)e.Item.DataItem;
                List<RefinerItem> refinementItems = refinerList.RefinementItems;

                rptAlternateItems.DataSource = refinementItems;
                rptAlternateItems.DataBind();
            }
            catch (Exception ex)
            {
                Helper.LogApplicationLevelException("uc_searchResults", "rptAlternateIndex_ItemDataBound", ex);
            }
        }

        
        /// <summary>
        /// Binds the paging data to the control
        /// </summary>
        /// <param name="pager"></param>
        private void BindPagingControl(SoftwareDesign.Search.Models.Pager pager)
        {
            if (pager.EndPage > 1)
            {
                if (pager.CurrentPage > 1)
                {
                    // add the first page link
                    HtmlGenericControl frstListItem = new HtmlGenericControl("li");                    
                    LinkButton lnk1stPageButton = CreatePagingLinkButton("First", 1);
                    frstListItem.Controls.Add(lnk1stPageButton);
                    plhPagingControl.Controls.Add(frstListItem);

                    // add the second page link
                    HtmlGenericControl prevListItem = new HtmlGenericControl("li");
                    LinkButton lnkPrevPageButton = CreatePagingLinkButton("Previous", pager.CurrentPage - 1);
                    prevListItem.Controls.Add(lnkPrevPageButton);
                    plhPagingControl.Controls.Add(prevListItem);
                }

                for(int page = pager.StartPage; page <= pager.EndPage; page++)
                {
                    HtmlGenericControl listItem = new HtmlGenericControl("li");

                    if (page == pager.CurrentPage)
                        listItem.Attributes.Add("class", "active");

                    LinkButton lnkPageButton = CreatePagingLinkButton(page.ToString(), page);
                    listItem.Controls.Add(lnkPageButton);
                    plhPagingControl.Controls.Add(listItem);
                }
            }
        }

        /// <summary>
        /// Create the paging link buttons
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns></returns>
        private LinkButton CreatePagingLinkButton(string pageText, int pageNo)
        {
            LinkButton lnkBtnPager = new LinkButton();
            lnkBtnPager.Text = pageText;

            // Update the page number in the URL
            var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            nameValues.Set("page", pageNo.ToString());
            lnkBtnPager.PostBackUrl = String.Format("{0}?{1}", Request.Url.AbsolutePath, nameValues);

            return lnkBtnPager;
        }

        #endregion  
    }
}
