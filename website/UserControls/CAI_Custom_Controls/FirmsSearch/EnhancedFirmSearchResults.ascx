<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EnhancedFirmSearchResults.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.FirmsSearch.EnhancedFirmSearchResults" %>
<%@ Register TagName="PremiumResults" TagPrefix="cai" Src="~/UserControls/CAI_Custom_Controls/FirmsSearch/EnhancedFirmTopResults.ascx" %>
<%-- bootstrap  --%>
<!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous" />
<!-- Optional theme -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css" integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp" crossorigin="anonymous" />
<!-- Override bootstrap css for corporate styles -->
<link href="../../../CSS/bootstrap-override.min.css" rel="stylesheet" />
<!-- Multiselect dropdown files -->
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery.sumoselect/3.0.2/sumoselect.min.css">
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.sumoselect/3.0.2/jquery.sumoselect.min.js"></script>
<link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
<script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
<%--<div class="raDiv" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="PanelEnhancedFirmSearchResults">
        <ProgressTemplate>
            <div class="dvProcessing">
                <div class="loading-bg">
                    <img src="/Images/CAITheme/bx_loader.gif" />
                    <span>LOADING...<br /><br />Please do not leave or close this window while payment is processing.</span>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>--%>
<%--<asp:UpdatePanel ID="PanelEnhancedFirmSearchResults" runat="server" >
    <ContentTemplate>--%>
<style>
    .filter-box{
        margin-bottom: 20px;
    }
</style>
<div class="container-fluid">
    <div class="row">
        <div class="col-xs-4 less-space-nav">
            <a href="#firm" aria-controls="firm" class="nav-directory-btns firm-dir-button triggerbookmark" onclick="myFunction(this);">Firm directory</a>
        </div>
        <div class="col-xs-4 less-space-nav">
            <a href="#member" aria-controls="member" class="nav-directory-btns mem-dir-button triggerbookmark" onclick="myFunction(this);">Members directory</a>
        </div>
        <div class="col-xs-4 less-space-nav">
            <a href="#prac" aria-controls="prac" class="nav-directory-btns mem-prac-dir-button triggerbookmark" onclick="myFunction(this);">Members in practice directory</a>
        </div>
    </div>
    <%-- 1. Firm Directory START--%>
    <div class="dir-panel firm-directory firm">
        <div class="filter-box">
            <div class="row">
                <div class="form-group col-sm-12 col-md-3 txtbox-padding">
                    <asp:DropDownList ID="cmbCountrySelect" runat="server" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                </div>
                <div class="form-group col-sm-12 col-md-3 txtbox-padding">
                    <asp:TextBox ID="idFirmCity" runat="server" class="form-control" ClientIDMode="Static" placeholder="City or county" />
                    <script type="text/javascript">
                        jQuery(function () {
                            //EDUARDO - 02-01-2020
                                   <%-- $("#<%= idFirmCity.ClientID %>").on("keyup", function () {
                                        performAjaxCountOfResultsBasedOnParams();
                                    });--%>
                            $("#<%= idFirmCity.ClientID %>").autocomplete({
                                source: function (request, response) {
                                    $.ajax({
                                        url: '<%= Page.ResolveUrl("~/WebServices/WebService.asmx/GetCountyCityByCountry") %>',
                                        data: JSON.stringify({
                                            country: ($('#<%= cmbCountrySelect.ClientID %> :selected').text().toLowerCase() == "country" ? "" : $('#<%= cmbCountrySelect.ClientID %> :selected').text()),
                                            city: request.term
                                        }),
                                        dataType: "json",
                                        type: "POST",
                                        contentType: "application/json; charset=utf-8",
                                        dataFilter: function (data) { return data; },
                                        success: function (data) {
                                            response($.map(data.d, function (item) {
                                                return {
                                                    label: item.Name,
                                                }
                                            }
                                            ))

                                        },
                                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                                            alert("error" + errorThrown);
                                        }
                                    });
                                },
                                minLength: 1,
                                response: function (event, ui) {
                                    let len = ui.content.length;
                                    //EDUARDO - 02-01-2020
                                    //performAjaxCountOfResultsBasedOnParams();
                                }
                            });
                        });
                    </script>
                </div>
                <div class="form-group col-sm-12 col-md-6 txtbox-padding">
                    <asp:TextBox class="form-control" ID="idFirmName" runat="server" ClientIDMode="Static" placeholder="Search by company name..(enter first 3 characters)" />
                    <script type="text/javascript">
                        jQuery(function () {
                            //EDUARDO - 02-01-2020
                                    <%--$("#<%= idFirmName.ClientID %>").on("keyup", function () {
                                        performAjaxCountOfResultsBasedOnParams();
                                    });--%>
                            $("#<%= idFirmName.ClientID %>").autocomplete({
                                source: function (request, response) {
                                    $.ajax({
                                        url: '<%= Page.ResolveUrl("~/WebServices/WebService.asmx/GetCountryCompanyName") %>',
                                        data: JSON.stringify({
                                            countryId: $("#<%= cmbCountrySelect.ClientID %>").val(),
                                            city: $("#<%= idFirmCity.ClientID %>").val(),
                                            firmName: request.term,
                                            sectors: $("#<%= cmbIndustrySectors.ClientID %>").val(),
                                            specialisms: $("#<%= cmbSpecialisms.ClientID %>").val(),
                                            authorisations: $("#<%= cmbAuthorisations.ClientID %>").val()
                                        }),
                                        dataType: "json",
                                        type: "POST",
                                        contentType: "application/json; charset=utf-8",
                                        dataFilter: function (data) { return data; },
                                        success: function (data) {
                                            response($.map(data.d, function (item) {
                                                return {
                                                    label: item.Name,
                                                    value: item.Id
                                                }
                                            }
                                            ))

                                            <%--if (data != null && data.d != null && data.d.length > 0) {
                                                $("#<%= btnSearchFirm.ClientID %>").removeAttr("disabled");
                                            } else {
                                                $("#<%= btnSearchFirm.ClientID %>").attr("disabled", "disabled");
                                            }--%>
                                        },
                                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                                            alert("error" + errorThrown);
                                        }
                                    });
                                },
                                minLength: 3,
                                focus: function (event, ui) {
                                    //$("#<%= idFirmName.ClientID %>").val(ui.item.label);
                                    return false;
                                },
                                //EDUARDO - 02-01-2020
                                select: function (event, ui) {
                                    $("#<%= idFirmName.ClientID %>").val(ui.item.label);
                                    //performAjaxCountOfResultsBasedOnParams();
                                    return false;
                                },
                                response: function (event, ui) {
                                    let len = ui.content.length;
                                    //performAjaxCountOfResultsBasedOnParams();
                                }
                            });
                        });
                    </script>
                </div>
            </div>
            <div class="row">
                <div class="form-group col-xs-6 col-md-3">
                    <form method="get"><%-- DO NOT REMOVE THIS EMPTY FORM --%></form>
                    <form method="get">
                        <span class="multiselect-pill authorisation-pill">Authorisations</span>
                        <asp:ListBox ID="cmbAuthorisations" runat="server" SelectionMode="Multiple" DataTextField="Name" DataValueField="ID" CssClass="SlectBox"></asp:ListBox>
                    </form>
                </div>
                <div class="form-group col-xs-6 col-md-3">
                    <form method="get">
                        <span class="multiselect-pill sector-pill">Industry sector</span>
                        <asp:ListBox ID="cmbIndustrySectors" runat="server" SelectionMode="Multiple" DataTextField="Name" DataValueField="Name" CssClass="SlectBox"></asp:ListBox>
                    </form>
                </div>
                <div class="form-group col-xs-6 col-md-3">
                    <form method="get">
                        <span class="multiselect-pill specialism-pill">Specialisms</span>
                        <asp:ListBox ID="cmbSpecialisms" runat="server" SelectionMode="Multiple" DataTextField="Name" DataValueField="Name" CssClass="SlectBox"></asp:ListBox>
                    </form>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12 pill-list"></div>
            </div>
            <div class="row">
                <div class="form-group  col-xs-12 col-md-3" style="text-align: left; line-height: 50px;">
                    <span data-format="{0} results found" class="totalBasedOnFilters"></span>
                </div>
                <div class="form-group  col-xs-12 col-md-3 pull-right">
                    <asp:Button ID="btnSearchFirm" runat="server" Text="Search" CssClass="cai-btn cai-btn-red" Style="width: 100%" OnClientClick="btnSearchFirms_click(event);" />
                </div>
            </div>
        </div>

        <div class="firmResultsContainerFirst">
            <asp:Panel runat="server" ID="pnlFirmSearchResults" Visible="false" CssClass="firmResultsContainer">
                <cai:PremiumResults runat="server" ID="caiPremiumResults" />

                <h2 class="h2-small float-left">Other companies search results:</h2>
                <div class="secondary-directory-search">
                    <table>
                        <asp:Repeater runat="server" ID="firmGeneralSearchRepeater">
                            <HeaderTemplate>
                                <tr>
                                    <th>Firm name</th>
                                    <th>City/town</th>
                                    <th>Phone</th>
                                    <th>Website</th>
                                    <th>&nbsp;</th>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="cell-1">
                                        <span class="firm-name"><%# DataBinder.Eval(Container.DataItem, "Title") %></span>
                                    </td>
                                    <td class="cell-2">
                                        <span class="mob-title">Location:</span>
                                        <span class="mob-details"><%# DataBinder.Eval(Container.DataItem, "City") %></span>
                                    </td>
                                    <td class="cell-3">
                                        <span class="mob-title">Phone:</span>
                                        <span class="mob-details"><%# DataBinder.Eval(Container.DataItem, "Phone") %></span>
                                    </td>
                                    <td class="cell-4">
                                        <span class="mob-title">Link:</span>
                                        <span class="mob-details">
                                            <a runat="server" visible='<%# !String.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "Url").ToString()) %>' href='<%# DataBinder.Eval(Container.DataItem, "Url") %>' target="_blank">
                                                <%# DataBinder.Eval(Container.DataItem, "Url").ToString().Replace("https://", "").Replace("http://", "") %>
                                            </a>
                                            <span runat="server" visible='<%# String.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "Url").ToString()) %>'>N/A
                                            </span>
                                        </span>
                                    </td>
                                    <td class="cell-5"><a href="<%# DataBinder.Eval(Container.DataItem, "SinglePageUrl").ToString() %>" class="follow-arrow"></a></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Repeater ID="firmSearchPagination" runat="server">
                            <HeaderTemplate>
                                <tr>
                                    <td colspan="5">
                                        <div class="dataTables_paginate paging_simple_numbers">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <a href="<%# DataBinder.Eval(Container.DataItem, "Url") %>" class="paginate_button <%# DataBinder.Eval(Container.DataItem, "CssClass") %>" data-page="<%# DataBinder.Eval(Container.DataItem, "Page") %>">
                                    <%# DataBinder.Eval(Container.DataItem, "Text") %>
                                </a>
                            </ItemTemplate>
                            <FooterTemplate>
                                </div>
                                            </td>
                                        </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </asp:Panel>
        </div>
    </div>
    <%-- 1. Firm Directory END--%>
    <%-- 2. Members Directory START--%>
    <div class="dir-panel mem-directory member">
        <div class="filter-box">
            <div class="row">
                <div class="form-group col-sm-12 col-md-3 txtbox-padding">
                    <asp:DropDownList ID="idMemCountry" runat="server" DataTextField="Name" ClientIDMode="Static" DataValueField="ID"></asp:DropDownList>

                </div>
                <div class="form-group col-sm-12 col-md-3 txtbox-padding">
                    <asp:TextBox ID="IdMemCity" runat="server" class="form-control" ClientIDMode="Static" placeholder="City or county..(enter first 2 characters)" />
                </div>
                <div class="form-group col-sm-12 col-md-6 txtbox-padding">

                    <asp:TextBox class="form-control" ID="IdMember" runat="server" ClientIDMode="Static" placeholder="Search by member name..(enter first 3 characters)" />

                </div>
            </div>
            <div class="row">
                <div class="form-group  col-xs-12 col-md-3" style="text-align: left; line-height: 50px;">
                    <span id="spantc" class="totalBasedOnFilters"></span>
                </div>
                <div class="form-group  col-xs-12 col-md-3 pull-right">
                    <asp:Button ID="btnSearchMember" runat="server" Text="Search" CssClass="cai-btn cai-btn-red" Style="width: 100%" OnClientClick="return BindMemberTable()" />

                </div>
            </div>
        </div>
        <div class="memdir-search-result">
            <h2 class="h2-small float-left">Members search results:</h2>
            <div class="secondary-directory-search mem-directory-search">
                <table id="tmemdir">
                    <thead>
                        <tr>
                            <th>Member name</th>
                            <th>Firm name</th>
                            <th>Admission year</th>
                            <th>City/town</th>
                            <th>Country</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <%-- 2. Members Directory END--%>
    <%-- 3. Members in Practice Directory START--%>
    <div class="dir-panel mem-practice-directory prac">
        <div class="filter-box">
            <div class="row">
                <div class="form-group col-sm-12 col-md-3 txtbox-padding">
                    <%-- <asp:DropDownList ID="idMemPracCountry" runat="server" ClientIDMode="Static"  DataTextField="Name" DataValueField="ID"></asp:DropDownList>--%>
                    <select id="idMemPracCountry" class="form-control" title="Select a country">
                        <%-- <option selected disabled hidden>Country</option> --%>
                        <option selected>Country</option>
                    </select>
                </div>
                <div class="form-group col-sm-12 col-md-3 txtbox-padding">
                    <asp:TextBox ID="idMemPracCity" runat="server" class="form-control" ClientIDMode="Static" placeholder="City or county..(enter first 2 characters)" />
                </div>
                <div class="form-group col-sm-12 col-md-6 txtbox-padding">
                    <asp:TextBox class="form-control" ID="idMemPrac" runat="server" ClientIDMode="Static" placeholder="Search by member in practice name..(enter first 3 characters)" />
                </div>
            </div>
            <div class="row">

                <div class="form-group col-xs-6 col-md-3">
                    <form method="get">
                        <span class="multiselect-pill authorisation-pill">Authorisations</span>
                        <select id="s4" multiple="multiple" placeholder="Authorisations" class="SlectBox">
                            <option value="ILC">ILC - Insolvency Licence Holders (GB/NI) </option>
                            <option value="IPC">IPC - Insolvency Practising Certificate Holders (ROI) </option>
                            <option value="PC">PC - Practising Certificate</option>
                        </select>
                    </form>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12 pill-list"></div>
            </div>
            <div class="row">
                <div class="form-group  col-xs-12 col-md-3" style="text-align: left; line-height: 50px;">
                    <span id="spantcmp" class="totalBasedOnFilters"></span>
                </div>
                <div class="form-group  col-xs-12 col-md-3 pull-right">
                    <asp:Button ID="btnSearchMemPrac" runat="server" Text="Search" CssClass="cai-btn cai-btn-red" Style="width: 100%" OnClientClick="return BindMemPracTable()" />

                </div>
            </div>
        </div>
        <div class="mem-pracdir-search-result">
            <h2 class="h2-small float-left">Members in practice search results:</h2>
            <div class="secondary-directory-search mem-prac-directory-search">
                <table id="tmempracdir">
                    <thead>
                        <tr>
                            <th>Member name</th>
                            <th>Firm name</th>
                            <th>ILC</th>
                            <th>IPC</th>
                            <th>PC</th>
                            <th>City/town</th>
                            <th>Country</th>
                        </tr>
                    </thead>
                    <tbody id="mpbtable">
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <%-- 3. Members in Practice Directory END--%>
</div>
<%--    </ContentTemplate>
</asp:UpdatePanel>--%>

<script type="text/javascript">
    //Susan Wong, BOOKMARK TRIGGER SCRIPT START
    $(window).load(function () {
        // IF NO BOOKMARK ON URL THEN SHOW FIRM BY DEFAULT
        if (document.location.hash == 0) {
            document.location.hash = 'firm';
        }
        // GET BOOKMARK VALUE AND SET BOOKMARKED TAB TO CURRENT
        let bookmark = location.href.split("#")[1];
        $('a.triggerbookmark').each(function () {
            let $this = $(this);
            if ($this.attr("aria-controls") == bookmark) {
                $this.parent().siblings().children().removeClass("current");
                $this.addClass("current");
            }
        });
        // GET BOOKMARK AND FIND PANEL TO MATCHES TO EXPOSE AND HIDE ALL OTHERS
        $('.dir-panel').each(function () {
            let $this = $(this);
            if ($this.hasClass(bookmark)) {
                $this.siblings('.dir-panel')
                    .removeClass("show-it")
                    .addClass("hide-it");
                $this.addClass("show-it");
            }
        });
    });
    // ON CLICK OF TAB TO HIDE AND SHOW PANELS
    $('a.mem-dir-button').click(function () {
        $('.mem-directory').removeClass("hide-it");
        $('.mem-directory').addClass("show-it");
        $('.firm-directory').removeClass("show-it");
        $('.firm-directory').addClass("hide-it");
        $('.mem-practice-directory').removeClass("show-it");
        $('.mem-practice-directory').addClass("hide-it");
    });
    $('a.firm-dir-button').click(function () {
        $('.mem-directory').removeClass("show-it");
        $('.mem-directory').addClass("hide-it");
        $('.firm-directory').removeClass("hide-it");
        $('.firm-directory').addClass("show-it");
        $('.mem-practice-directory').removeClass("show-it");
        $('.mem-practice-directory').addClass("hide-it");
    });
    $('a.mem-prac-dir-button').click(function () {
        $('.mem-practice-directory').removeClass("hide-it");
        $('.mem-practice-directory').addClass("show-it");
        $('.mem-directory').removeClass("show-it");
        $('.mem-directory').addClass("hide-it");
        $('.firm-directory').removeClass("show-it");
        $('.firm-directory').addClass("hide-it");
    });
    // UPDATE URL WHEN TRIGGER IS CLICKED
    function myFunction(lnk) {
        $('a.triggerbookmark').click(function () {
            let $this = $(this);
            let btnbookmark = $this.attr("aria-controls");
            let yScroll = document.body.scrollTop;
            document.location.hash = btnbookmark;
            document.body.scrollTop = yScroll;
        });
    }
    //Susan Wong, BOOKMARK TRIGGER SCRIPT END

    window.xhrAjaxCountOfResultsBasedOnParams = "";

    function performAjaxCountOfResultsBasedOnParams() {
        let data = {
            countryId: $("#<%= cmbCountrySelect.ClientID %>").val(),
            city: $("#<%= idFirmCity.ClientID %>").val(),
            firmName: $("#<%= idFirmName.ClientID %>").val(),
            sectors: $("#<%= cmbIndustrySectors.ClientID %>").val(),
            specialisms: $("#<%= cmbSpecialisms.ClientID %>").val(),
            authorisations: $("#<%= cmbAuthorisations.ClientID %>").val()
        };

        if (window.xhrAjaxCountOfResultsBasedOnParams && window.xhrAjaxCountOfResultsBasedOnParams.readyState != 4) {
            window.xhrAjaxCountOfResultsBasedOnParams.abort();
        }

        window.xhrAjaxCountOfResultsBasedOnParams = $.ajax({
            url: '<%= Page.ResolveUrl("~/WebServices/WebService.asmx/EnhancedListingFirmSearchCount") %>',
            data: JSON.stringify(data),
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataFilter: function (data) { return data; },
            success: function (data) {
                let totalResultsFound = data.d;
                if (totalResultsFound && totalResultsFound != "-1" && totalResultsFound != "0") {
                    //$("#<%= btnSearchFirm.ClientID %>").removeAttr("disabled");
                    $(".totalBasedOnFilters").show();
                    $(".totalBasedOnFilters").first().html($(".totalBasedOnFilters").data("format").replace("{0}", totalResultsFound));
                } else {
                    //$("#<%= btnSearchFirm.ClientID %>").attr("disabled", "disabled");

                    // checking if no results found
                    if (totalResultsFound == "0" || totalResultsFound == "-1") {
                        $(".totalBasedOnFilters").show();
                        $(".totalBasedOnFilters").first().html($(".totalBasedOnFilters").data("format").replace("{0}", "no results"));
                    } else {
                        $(".totalBasedOnFilters").hide();
                    }

                }
            }
        });
    }

    // Close multiselect if user clicks anywhere else
    $(document).click(function () {
        $(".SumoSelect").each(function () {
            if ($(this).hasClass("show-it open")) {
                $(this)
                    .removeClass("show-it open")
                    .addClass("hide-it");
                $(this).siblings('.multiselect-pill')
                    .removeClass("hide-it")
                    .addClass("show-it");
            }
        });
    });

    //// Initiate multiselect checkbox
    //$(function () {
    //    $('.mem-practice-directory').addClass('hide-it');
    //    $('.mem-directory').addClass('hide-it');
    //    $('.firm-directory').addClass('show-it');
    //	// code for add url  parameter type = firm 
    //    //var w = window.location;
    //   // if (w.search.indexOf("type") == -1) {
    //    //	window.location = w + (w.search.indexOf("?") == -1 ? "?" : "&") + "type=firm";
    //  //  }             
    //	//jim code end
    //    $('.firm-dir-button').addClass('current');
    //});

    function initSearchFilters() {
        performAjaxCountOfResultsBasedOnParams();
        // Initiate multiselect checkbox
        $('.SlectBox').SumoSelect();
        // Hide multiselect on load
        $('.SumoSelect').addClass("hide-it");
        // Show section based on button clicked
        $('.firm-dir-button').click(function () {
            $('.mem-directory')
                .addClass("hide-it")
                .removeClass("show-it");
            $('.mem-practice-directory')
                .addClass("hide-it")
                .removeClass("show-it");
            $('.firm-directory')
                .addClass("show-it")
                .removeClass("hide-it");
            $('.nav-directory-btns').removeClass("current");
            $(this).addClass("current");
            // reset controls
            clearfirmsdirsearch()
            //jim code
            // var url = window.location.href;
            // var tval = 'firm';
            // var nurl = url.replace(/(type=)[^\&]+/, '$1' + tval);
            // window.location.href = nurl
            //jim code end

            $('.firm-dir-button').addClass('current');
        });
        $('.mem-dir-button').click(function () {
            //alert('member-driectory client side');
            clearmemberdirsearch()
            $('.mem-practice-directory')
                .addClass("hide-it")
                .removeClass("show-it");
            $('.firm-directory')
                .addClass("hide-it")
                .removeClass("show-it");
            $('.mem-directory')
                .addClass("show-it")
                .removeClass("hide-it");
            $('.nav-directory-btns').removeClass("current");
            $(this).addClass("current");
            // jim code 
            // var url = window.location.href;
            // var tval = 'members';
            // var nurl = url.replace(/(type=)[^\&]+/, '$1' + tval);
            // window.location.href =nurl
            // jim cod eend
        });
        $('.mem-prac-dir-button').click(function () {

            //alert('member-practice client side');
            $('.mem-directory')
                .addClass("hide-it")
                .removeClass("show-it");
            $('.firm-directory')
                .addClass("hide-it")
                .removeClass("show-it");
            $('.mem-practice-directory')
                .addClass("show-it")
                .removeClass("hide-it");
            $('.nav-directory-btns').removeClass("current");
            $(this).addClass("current");
            clearmembersinpracticedirsearch()
            // code begin
            // var url = window.location.href;
            // var tval = 'member-in-practice';
            // var nurl = url.replace(/(type=)[^\&]+/, '$1' + tval);
            //  window.location.href = nurl
            //code end 

        });
        // Show multiselect when pill is clicked
        $('.multiselect-pill').click(function (event) {
            $(this)
                .addClass("hide-it")
                .removeClass("show-it");
            $(this).siblings('.SumoSelect')
                .removeClass("hide-it")
                .addClass("show-it open");
            event.stopPropagation();
        });
        // Create small pill for each selected option
        $(".SlectBox").change(function () {
            let arr = [];
            i = 0;
            $(this).closest('.row').siblings('.row').children('.pill-list').html("");
            let chkSelectedItem = $(this).closest('.form-group').parent('.row').children('.form-group').children('form').children('.SumoSelect').children('.multiple').children('.options').children('.selected');
            chkSelectedItem.each(function () {
                let textValue = $(this).text();
                arr[i++] = $(this).text();
                $(this).closest('.row').siblings('.row').children('.pill-list').append('<span class="grey-pill" onclick="removeFunction(this);" title="' + textValue + '">' + textValue + '</span>');
            });
        });

        $(".SlectBox").change();

        //EDUARDO - 02-01-2020
        $("#<%= cmbCountrySelect.ClientID %>").change(function () {
            //performAjaxCountOfResultsBasedOnParams();
        });
    }

    // Remove small pill and selected option when you "x" on small pills
    function removeFunction(ab) {
        let c = ab.getAttribute('title');
        ab.remove();
        $('.SumoSelect ul.options li.selected label').each(function () {
            let d = $(this).text()
            if (d == c) {
                $(this).parent().click();
            }
        });
    };

    function btnSearchFirms_click(e) {
        e.stopPropagation();
        e.preventDefault();

        performAjaxCountOfResultsBasedOnParams();

        // lets perform search
        let cmbCountrySelect = $("#<%= cmbCountrySelect.ClientID %>").val();
        let idFirmCity = $("#<%= idFirmCity.ClientID %>").val();
        let idFirmName = $("#<%= idFirmName.ClientID %>").val();
        let cmbIndustrySectors = $("#<%= cmbIndustrySectors.ClientID %>").val();
        let cmbSpecialisms = $("#<%= cmbSpecialisms.ClientID %>").val();
        let cmbAuthorisations = $("#<%= cmbAuthorisations.ClientID %>").val();

        let data = {
            type: "firm"
        };

        if (cmbCountrySelect && cmbCountrySelect != -1) {
            data.country = cmbCountrySelect;
        }
        if (idFirmCity) {
            data.city = idFirmCity;
        }
        if (idFirmName) {
            data.firmName = idFirmName;
        }
        if (cmbIndustrySectors && cmbIndustrySectors.length > 0) {
            data.sectors = cmbIndustrySectors.join(',');
        }
        if (cmbSpecialisms && cmbSpecialisms.length > 0) {
            data.specialisms = cmbSpecialisms.join(',');
        }
        if (cmbAuthorisations && cmbAuthorisations.length > 0) {
            data.authorisations = cmbAuthorisations.join(',');
        }

        let url = "<%= Request.Url.GetLeftPart(UriPartial.Path) %>";
                //$("#<%= btnSearchFirm.ClientID %>").attr("disabled", "disabled").addClass("disabled");

        let originalSearchText = $("#<%= btnSearchFirm.ClientID %>").val();
        $("#<%= btnSearchFirm.ClientID %>").val('Searching...');

        $.ajax({
            url: url,
            data: data,
            method: "get",
            complete: function (response) {
                let html = response.responseText;
                let responseLength = $(response.responseText).find('.firmResultsContainer').length;
                // checking if results exists
                if (responseLength > 0) {
                    // checking if results already exists or not
                    if ($('.firmResultsContainer').length <= 0) {
                        $('.firmResultsContainerFirst').addClass('firmResultsContainer');
                    }

                    $('.firmResultsContainer').html($(response.responseText).find('.firmResultsContainer').html())
                } else {
                    if ($('.firmResultsContainer').length <= 0) {
                        $('.firmResultsContainerFirst').addClass('firmResultsContainer');
                    }

                    $('.firmResultsContainer').empty();
                }

                $("#<%= btnSearchFirm.ClientID %>").removeAttr("disabled").removeClass("disabled");
                        $("#<%= btnSearchFirm.ClientID %>").val(originalSearchText);

                        window.history.pushState(data, document.title, this.url);
                    }
                });
            }

            function pageLoad() {
                initSearchFilters();
            }

            // rest  Firms directory  
            function clearfirmsdirsearch() {
                $("#<%= cmbCountrySelect.ClientID %>").selectedIndex = 0;
                $("#<%= idFirmCity.ClientID %>").val('');
                $("#<%= idFirmName.ClientID %>").val('');
                $('.totalBasedOnFilters').hide();
                $(".firmResultsContainerFirst").hide();
                $("#<%= btnSearchFirm.ClientID %>").val('Search');
                //$("#<%= btnSearchFirm.ClientID %>").attr("disabled", "disabled");
                //alert($("#<%= cmbIndustrySectors.ClientID %>").val());
            }


    // begin code  for  membersearch
    // rest Members directory  
    function clearmemberdirsearch() {
        $("#idMemCountry")[0].selectedIndex = 0;
        $("#<%= IdMemCity.ClientID %>").val('');
        $("#IdMember").val('');
        $('#spantc').html('');
        $(".memdir-search-result").hide();
    }
    // default disable and hide settings 
    // $("#<%= btnSearchMember.ClientID %>").attr("disabled", "disabled");     
    $(".memdir-search-result").hide();
    if ($("#idMemCountry")[0].selectedIndex == 0) {
        $("#IdMemCity").prop('disabled', true);
    }
    else { $("#IdMemCity").prop('disabled', false); }



    $("#<%= idMemCountry.ClientID %>").change(function () {

        $("#<%= IdMemCity.ClientID %>").val('');
        $("#IdMember").val('');
        $('#spantc').html('');
        $("#<%= btnSearchMember.ClientID %>").val('Search');
        //$("#<%= btnSearchMember.ClientID %>").prop('disabled', true);
        $(".memdir-search-result").hide();
        if ($("#idMemCountry")[0].selectedIndex == 0) {

            $("#IdMemCity").prop('disabled', true);
        }
        else { $("#IdMemCity").prop('disabled', false); }
        BindMemberCity()

    });

    function BindMemberCity() {
        $("#<%= IdMemCity.ClientID %>").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: '<%= Page.ResolveUrl("~/WebServices/WebService.asmx/GetCountyCityByCountry") %>',
                    data: JSON.stringify({
                        country: $('#<%= idMemCountry.ClientID %> :selected').text(),
                        city: request.term
                    }),
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) { return data; },
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                label: item.Name,
                            }
                        }
                        ))

                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("error" + errorThrown);
                    }
                });
            },
            minLength: 2,
            response: function (event, ui) {
                let len = ui.content.length;

                //$('#spantc').css('display', 'inline');
                //$('#spantc').html(len + ' Results Found');
            }
        });



    }
    // member search box
    $('#IdMember').on('input', function () {

        if ($("#IdMember").val().length > 2) {

            BindCompanyWithValue();
            //$("#btnSearchMember").prop('disabled', false);
        }
        else {

            //$("#<%= btnSearchMember.ClientID %>").attr("disabled", "disabled");
            $('#spantc').css('display', 'none');
            $(".memdir-search-result").hide();
        }
    });



    //  show search button if records are > 0

    function BindCompanyWithValue() {
        $("#IdMember").autocomplete({

            source: function (request, response) {

                $.ajax({
                    url: '<%= Page.ResolveUrl("~/WebServices/WebService.asmx/GetCountyCityByCountryMemebersDirectory") %>',
                            data: JSON.stringify({
                                country: $('#idMemCountry :selected').text(),
                                city: $("#IdMemCity").val().trim(),
                                name: request.term
                            }),
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            dataFilter: function (data) { return data; },
                            success: function (data) {
                                response($.map(data.d, function (item) {
                                    return {
                                        label: item.mname,
                                        value: item.mid
                                    }
                                }
                                ))



                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                alert("error" + errorThrown);
                            }
                        });
                    },
                    minLength: 3,
                    focus: function (event, ui) {

                        $("#IdMember").val(ui.item.label);
                        return false;
                    },

                    select: function (event, ui) {
                        return false;
                    },
                    response: function (event, ui) {
                        let len = ui.content.length;

                        if (len == "0") {
                            //btnSearchMember hide
                            //	$("#<%= btnSearchMember.ClientID %>").attr("disabled", "disabled");
                            //EDUARDO 
                            //$('#spantc').css('display', 'none');


                        }
                        else {
                            //alert(len);
                            //$("#<%= btnSearchMember.ClientID %>").removeAttr("disabled");

                            //EDUARDO 
                            //$('#spantc').css('display', 'inline');
                            //$('#spantc').html(len + ' Results Found');
                        }
                    }

                });
            }

    // Bind  table after search 	
    function BindMemberTable() {


        $("#<%= btnSearchMember.ClientID %>").val('Searching...');
        let mcname = $('#<%= idMemCountry.ClientID %> :selected').text();
        let memname = $("#<%= IdMember.ClientID %>").val();
        let fcname = $("#<%= IdMemCity.ClientID %>").val();

        $.ajax({
            url: '<%= Page.ResolveUrl("~/WebServices/WebService.asmx/GetELMemebersDirectory") %>',
            data: { country: mcname, city: fcname, mname: memname },
            method: "post",
            dataType: 'json',

            success: function (data) {
                $(".memdir-search-result").show();
                let tbmem = $("#tmemdir tbody");
                tbmem.empty();
                $(data).each(function (index, mem) {
                    tbmem.append('<tr><td class="cell-1"><span class="mem-name">' + mem.mname + '</span></td><td class="cell-2"><span class="mob-details">' + mem.fname + '</span></td><td class="cell-3"><span class="mob-details">' + mem.adate + '</span></td><td class="cell-4"><span class="mob-details">' + mem.city + '</span></td><td class="cell-4"><span class="mob-details">' + mem.country + '</span></td></tr>');
                });

                let len = 0;

                if (data != null && data.length != null && data.length > 0) {
                    len = data.length;
                }

                if (len == "0") {
                    //EDUARDO 
                    $('#spantc').css('display', 'inline');
                    $('#spantc').html('No results Found');

                }
                else {
                    //EDUARDO 
                    $('#spantc').css('display', 'inline');
                    $('#spantc').html(len + ' Results Found');
                }

                $("#<%= btnSearchMember.ClientID %>").val('Search');

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("error:" + Error);

                $("#<%= btnSearchMember.ClientID %>").val('Search');
            }
        });
        return false;
    }

        //end code for member search  

        // members in practice code starts here    
        // default disable and hide settings 
        //    $("#<%= btnSearchMemPrac.ClientID %>").attr("disabled", "disabled");       	
    $(".mem-pracdir-search-result").hide();
    $("#<%= idMemPracCity.ClientID %>").prop('disabled', true);

    GetDropDownData();
    function GetDropDownData() {

        $.ajax({
            type: "POST",
            url: '<%= Page.ResolveUrl("~/WebServices/WebService.asmx/GetCountryDetails") %>',
            data: "{}",
            dataType: "json",
            contentType: "application/json",
            success: function (data) {
                $.each(data.d, function (data, value) {
                    $("#idMemPracCountry").append($("<option></option>").val(value.CountryId).html(value.CountryName));
                })
            }

        });

    }
    // on dropdown chnage
    $('#idMemPracCountry').change(function () {
        $("#<%= idMemPracCity.ClientID %>").val('');
        $("#idMemPrac").val('');
        $('#spantcmp').html('');
        $("#<%= btnSearchMemPrac.ClientID %>").val('Search');
        //	$("#<%= btnSearchMemPrac.ClientID %>").prop('disabled', true);
        $(".mem-pracdir-search-result").hide();
        if ($(this).val() === "") {
            //$("#<%= idMemPracCity.ClientID %>").prop('disabled', true);
        }
        else {
            $("#<%= idMemPracCity.ClientID %>").prop('disabled', false);
        }


        BindMemPracDir();
        BindMemPracCity()
        //BindMemPracTable()

    });

    // member search box
    $('#idMemPrac').on('input', function () {

        if ($("#idMemPrac").val().length > 2) {

            BindMemPracDir()
            //	$("#btnSearchMemPrac").prop('disabled', false);

        }
        else {

            //$("#<%= btnSearchMemPrac.ClientID %>").attr("disabled", "disabled");
            $('#spantcmp').css('display', 'none');
            $(".memdir-search-result").hide();
        }
    });

    // bind memebr practice city 
    //idMemPracCountry

    function BindMemPracCity() {
        let val = $('#idMemPracCountry :selected').text();
        //alert(val);
        $("#<%= idMemPracCity.ClientID %>").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: '<%= Page.ResolveUrl("~/WebServices/WebService.asmx/GetCountyCityByCountry") %>',
                    data: JSON.stringify({
                        country: $('#idMemPracCountry :selected').text(),
                        city: request.term
                    }),
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) { return data; },
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                label: item.Name,
                                //value:item.Id
                            }
                        }
                        ))

                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("error" + errorThrown);
                    }
                });
            },
                    minLength: 1,
                    response: function (event, ui) {
                        //var len = ui.content.length;


                        //	$('#spantcmp').css('display', 'inline');
                        //	$('#spantcmp').html(ccount + ' Results Found');
                    }
                });

                //BindMemPracTable();
                //memberfiltertabledata();
    }

    $("#<%= idMemPracCity.ClientID %>").on("keyup", function () {


        let value = $(this).val().toLowerCase();
        $("#mpbtable tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)

        });



    });



    function BindMemPracDir() {

        let mpcname = $("#<%= idMemPracCity.ClientID %>").val();
        let au = [];
        $('#s4 option:selected').each(function (i) {
            au.push($(this).val());

        });


        $("#<%= idMemPrac.ClientID %>").autocomplete({

            source: function (request, response) {
                $.ajax({
                    url: '<%= Page.ResolveUrl("~/WebServices/WebService.asmx/GetELMIPDirectory") %>',
                    data: JSON.stringify({
                        country: $('#idMemPracCountry :selected').text(),
                        city: mpcname.trim(),
                        mn: request.term,
                        authorisations: au.toString()
                    }),
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) { return data; },
                    success: function (data) {
                        response($.map(data.d, function (items) {

                            if (items != null && items.length > 0) {
                                let valueToReturn = [];
                                for (var i = 0; i < items.length; i++) {
                                    let item = items[i];

                                    valueToReturn.push({
                                        label: item.mipname,
                                        value: item.mipid
                                    });
                                }
                                return valueToReturn;
                            }
                        }
                        ))



                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("error" + errorThrown);
                    }
                });
            },
            minLength: 3,
            focus: function (event, ui) {

                $("#idMemPrac").val(ui.item.label);
                return false;
            },

            select: function (event, ui) {
                return false;
            },
            response: function (event, ui) {
                let len = ui.content.length;

                if (len == "0") {
                    //btnSearchMember hide
                    //$("#<%= btnSearchMemPrac.ClientID %>").attr("disabled", "disabled");
                    //EDUARDO
                    //$('#spantcmp').css('display', 'none');

                }
                else {
                    $("#<%= btnSearchMemPrac.ClientID %>").removeAttr("disabled");

                    //EDUARDO
                    //$('#spantcmp').css('display', 'inline');
                    //$('#spantcmp').html(len + ' Results Found');
                }
            }

        });
    }



    function BindMemPracTable() {

        $("#<%= btnSearchMemPrac.ClientID %>").val('Searching...');
        let autho = [];
        $('#s4 option:selected').each(function (i) {
            autho.push($(this).val());

        });


        let mpco = $('#idMemPracCountry :selected').text();
        let mpmname = $("#idMemPrac").val();
        let mpci = $("#<%= idMemPracCity.ClientID %>").val();





        $.ajax({
            url: '<%= Page.ResolveUrl("~/WebServices/WebService.asmx/GetELMIPDirectoryTable") %>',
                    data: { country: mpco, city: mpci, mn: mpmname, authorisations: autho.toString() },
                    method: "post",
                    dataType: 'json',

                    success: function (data) {
                        let len = 0;
                        $(".mem-pracdir-search-result").show();
                        let tbmp = $("#tmempracdir tbody");
                        tbmp.empty();
                        $(data).each(function (index, mp) {
                            $("#<%= btnSearchMemPrac.ClientID %>").val('Search');

                             tbmp.append('<tr><td class="cell-1"><span class="mem-name">' + mp.mname + '</span></td><td class="cell-2"><span class="mob-details">' + mp.fname + '</span></td><td class="cell-3"><span class="mob-details">' + mp.ilc + '</span></td><td class="cell-4"><span class="mob-details">' + mp.ipc + '</span></td><td class="cell-5"><span class="mob-details">' + mp.pc + '</span></td><td td class="cell-6"><span class="mob-details">' + mp.city + '</span></td><td  class="cell-7"><span class="mob-details">' + mp.country + '</span></td></tr>');
                         });

                        if (data != null && data.length != null && data.length > 0)
                        {
                            len = data.length;
                        }

                        if (len > 0)
                        {
                            $('#spantcmp').css('display', 'inline');
                            $('#spantcmp').html(len + ' Results Found');
                        }
                        else
                        {
                            $('#spantcmp').css('display', 'inline');
                            $('#spantcmp').html('No results Found');
                        }
                     },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("error:" + Error);
                    }
                });


                     return false;

                 }


                 $("#idMemPrac").on("keyup", function () {
                     let value = $(this).val().toLowerCase();
                     $("#mpbtable tr").filter(function () {
                         $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                     });
                 });


                 // clear members in practice directory

                 function clearmembersinpracticedirsearch() {
                     $('#idMemPracCountry')[0].selectedIndex = 0;
                     $("#<%= idMemPracCity.ClientID %>").val('');
            $("#idMemPrac").val('');
            $(".mem-pracdir-search-result").hide();
            $("#<%= btnSearchMemPrac.ClientID %>").attr("disabled", "disabled");
        $('#spantcmp').css('display', 'none');
            //alert($('#s4 option:selected').val());
            //$('#s4 option:selected').removeAttr('selected');
        $('#s4 option:selected').prop('selected', false);

    }

    // members in practice code ends here

</script>
