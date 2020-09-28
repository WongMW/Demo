<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_searchResults.ascx.cs" Inherits="SoftwareDesign.Controls.SDWidgets.uc_searchResults" %>

<link href="CSS/elasticsearch.css" rel="stylesheet" />
<script type="text/javascript">

    <!-- http://www.jquerybyexample.net/2012/06/get-url-parameters-using-jquery.html -->
    function getUrlParameter(sParam) {
        var sPageURL = decodeURIComponent(window.location.search.substring(1)),
            sURLVariables = sPageURL.split('&'),
            sParameterName,
            i;

        for (i = 0; i < sURLVariables.length; i++) {
            sParameterName = sURLVariables[i].split('=');

            if (sParameterName[0] === sParam) {
                return sParameterName[1] === undefined ? true : sParameterName[1];
            }
        }
    };

    $(document).ready(function () {
        if ( getUrlParameter('r')) {
            $('#selectedlistcontainer').show();
        } else {
            $('#selectedlistcontainer').hide();
        }
    });

    $(document).ready(function () {

        $(".show_more").click(function () {
            $(".all_categories").toggle();
            $(".show_more").toggle();
            $(".refinerobject").toggleClass("open closed");
            return false;
        });
    });

</script>

<div id="main">
    <asp:Panel ID="pnlResultPanel" runat="server">
        <div id="hitsZoneTop">
          <div id="resultinfo1">
            <div id="resultsTopInfo">
                <span><asp:Label ID="lblPrimarySearchLabel" runat="server"></asp:Label></span>
            </div>
            <div id="resultnumbers">
                <asp:Label ID="lblPrimaryResultCount" runat="server"></asp:Label>
            </div>
          </div>
        </div>

        <div id="refineZone">
            <asp:Panel ID="pnlAllResultsCountAsLink" runat="server">
                <div id="refiner_results_info">
                    <asp:HyperLink ID="lnk_SearchAllIndexes" runat="server"></asp:HyperLink> 
                </div>            
            </asp:Panel>
            
            
            <div id="selectsource">
                <asp:Panel ID="pnlSelectNoSourceSelected" runat="server">
                    <div class="refiner_header_lbl">FILTER BY CONTENT SOURCE</div>
                </asp:Panel>
                <asp:Panel ID="pnlSelectSourceSelected" runat="server">   
                    <div class="refiner_header_lbl">FILTERED BY CONTENT SOURCE</div>
                   <div class="selectedlistcontainer">
                       <div id="search-youve-selected-header">You have selected</div>
                        <ul>
                            <li class="selectedrefinerlist">
                                <asp:HyperLink ID="lnk_RemoveRefine" runat="server"></asp:HyperLink>
                            </li>
                        </ul>
                    </div>

                    <div id="alternate_sources"><span id="alternate_sources_label">Other content sources :-</span></div>
                    <script type="text/javascript">
                        jQuery(function ($) {
                            $('#selectsource .selectablesource').css("opacity", "0.6");
                            $('#selectsource .selectablesource').css("filter", "alpha(opacity=60)");
                            $('#selectsource .selectablesource.selected').css("filter", "inherit");
                            $('#selectsource .selectablesource.selected').css("opacity", "1.0");
                            $('#selectsource .selectablesource.selected').css("border-left", "2px solid #8C1D40");
                            $('#selectsource .selectablesource.selected').css("background-color", "rgba(242, 242, 242, 1)");
                            $('#selectsource .selectablesource.selected').css("margin-bottom", "12px");
                            $('#selectsource .selectablesource.selected').css("font-weight", "bold");

                        });

                    </script>
                  </asp:Panel>

                    <asp:Repeater ID="rptAlternateIndex" runat="server" OnItemDataBound="rptAlternateIndex_ItemDataBound">
				 <ItemTemplate>
					<div class="refinerobject">
                        <asp:Repeater ID="rptAlternateItems" runat="server">
                            <ItemTemplate>
                                    <div class="selectablesource filter">
                                        <a class="FilterLink" href="<%# DataBinder.Eval(Container.DataItem, "RefinementUrl") %>">
                                            <span class="refine_facet_lbl"><%# DataBinder.Eval(Container.DataItem, "RefinementLabel") %></span><span class="refine_facet_count"><%# DataBinder.Eval(Container.DataItem, "RefinementCount") %></span>
                                        </a>
                                    </div>
                            </ItemTemplate>
                        </asp:Repeater>
					</div>
                 </ItemTemplate>
                    </asp:Repeater>
                
            </div>

            <div class="separator"></div>

            <asp:Panel ID="pnlRefineZone" runat="server">
            <div id="refineyoursearch">
                <asp:Panel ID="pnlRefineByTax" runat="server">
                    <div class="refiner_header_lbl">FILTER BY TAX SECTION</div>
                </asp:Panel>
                <asp:Panel ID="pnlRefinedByTax" runat="server">
                    <div class="refiner_header_lbl">FILTERED BY TAX SECTION</div>
                </asp:Panel>
                    <asp:ListView runat="server" ID="searchBreadcrumb">
                        <ItemTemplate>
                         <div class="selectedlistcontainer">
                            <div id="search-youve-selected-header">You have selected</div>
                            <ul>
                                <li class="selectedrefinerlist">
                                    <a href="<%# DataBinder.Eval(Container.DataItem, "RefinementUrl") %>">
                                        <span><%# DataBinder.Eval(Container.DataItem, "RefinementValue") %></span>
                                    </a>
                                 </li>
                             </ul>
                             </div>
                        </ItemTemplate>
                    </asp:ListView>
                <asp:Repeater ID="rptRefinement" runat="server" OnItemDataBound="rptRefinement_ItemDataBound">
                <ItemTemplate>
                    <div class="refinerobject refinebysection">
                        <asp:Repeater ID="rptRefinementItems" runat="server">
                            <ItemTemplate>
<!--                                <%#  Container.ItemIndex > 7 ? "<div class='filter all_categories'>": "<div class='filter'>" %> -->
                                <div class='filter'>
                                    <a class="FilterLink" href="<%# DataBinder.Eval(Container.DataItem, "RefinementUrl") %>">
                                    <span class="refine_facet_lbl"><%# DataBinder.Eval(Container.DataItem, "RefinementLabel") %></span><span class="refine_facet_count"><%# DataBinder.Eval(Container.DataItem, "RefinementCount") %></span>
                                    </a>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </ItemTemplate>
                </asp:Repeater>
                <!--
                <asp:Panel ID="pnlShowMore" runat="server">
                   <div class="showmoretoggle"><a class="show_more" style="display:block;" href="">Show more</a><a class="show_more" style="display:none;" href="">Show less</a></div>
                </asp:Panel>
                -->
                </div> <!-- refineyoursearch -->
            </asp:Panel>
        </div><!-- refinezone-->

        <div id="resultlist">
            <div id="resultlist_header">
                <span id="resultlist_header_counts"><asp:Label ID="lblDetailedResultCount" runat="server"></asp:Label></span>, 
                <span id="resultlist_header_pages"><asp:Label ID="lblShowingHits" runat="server"></asp:Label></span>
            </div>
            <asp:Repeater ID="rptResults" runat="server">
                <ItemTemplate>
                    <div class="resultItem">
                        <div class="title">
                            <span class="title_text">
                            <a href='<%# Eval("ResultUrl") %>'><%# Eval("Title") %></a>
                            </span>
                            <span class="title_info">
                                <%#  !string.IsNullOrEmpty(Eval("is_secure").ToString()) && String.Equals(Eval("is_secure").ToString(),"1") ?
"<img class='issecure' src='/Images/AuthorizedCourse.gif'/>":""%>

                                <%#  (!string.IsNullOrEmpty(Eval("indexName").ToString()) && 
                                (String.Equals(Eval("indexName").ToString(),"all content") || String.Equals(Eval("indexName").ToString(),"Shop") )) ?  
     "<span class='result_source result_"+Eval("SiteSection").ToString().ToLower()+"'>"+((String)Eval("SiteSection")).Replace("Products","Shop")+"</span>" : "" %>
                            </span>
                        </div>
                        <div class="resultSummary">
                            <span><%# Eval("HitHighlightedSummary") %></span>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div id="pageselect">            
            <ul id="pagination" class="pagination" runat="server" visible="true">
                <asp:PlaceHolder ID="plhPagingControl" runat="server"></asp:PlaceHolder>
            </ul>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlNoResults" runat="server">
        <div id="noresults">
            <p>
                <span><asp:Label ID="lblNoResultText" runat="server"></asp:Label>
                </span>
            </p>
        </div>
        <div id="noresultshelp">
            <span><asp:Label ID="lblNoResultHelpMessage" runat="server"></asp:Label></span>
            
            <asp:Repeater ID="rptSuggests" runat="server">
                <ItemTemplate>
                   <div class="suggest_label">Did you mean <a class="suggest_link" href="<%# Eval("SuggestUrl") %>"><%# Eval("SuggestText") %></a><div>
                </ItemTemplate>
            </asp:Repeater>
            <p>
                    <div id="alternative_indexes_zero_hits">
                        <asp:HyperLink ID="lnk_SearchAllIndexesZeroHits" runat="server"></asp:HyperLink>
                    </div>
            </p>
       </div>
    </asp:Panel>
</div>


<script type="text/javascript">
    $(function() {
        $('.filter-search').click(function () {
            $(this).toggleClass('opened');
        });

        $('.refine-search').click(function () {
            $(this).toggleClass('opened');
        });
    });
</script>
