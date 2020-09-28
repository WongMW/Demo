<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EnhancedVacanciesSearchResults.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.TrainingVacancies.EnhancedVacanciesSearchResults" %>
<link href="<%= ResolveUrl("~/Scripts/InHouse/bootstrap/3.3.7/css/bootstrap.min.css") %>" rel="stylesheet" type="text/css"/>
<link href="<%= ResolveUrl("~/Scripts/InHouse/bootstrap/3.3.7/css/bootstrap-theme.min.css") %>" rel="stylesheet" type="text/css"/>
<link href="<%= ResolveUrl("~/CSS/bootstrap-override.min.css") %>" rel="stylesheet" type="text/css"/>
<link href="<%= ResolveUrl("~/Scripts/InHouse/SumoSelect/sumoselect-3.0.2.min.css") %>" rel="stylesheet" type="text/css"/>
<link href="<%= ResolveUrl("~/Scripts/InHouse/Jquery/UI/jquery-ui-1.12.1.min.css") %>" rel="stylesheet" type="text/css"/>

<script src="<%= ResolveUrl("~/Scripts/InHouse/Jquery/UI/jquery-ui-1.12.1.min.js") %>"></script>
<script src="<%= ResolveUrl("~/Scripts/InHouse/ejs-2.7.1.min.js") %>"></script>
<script src="<%= ResolveUrl("~/Scripts/InHouse/SumoSelect/sumoselect-3.0.2.min.js") %>"></script>

<style>
    .list-item {
        color: white;
    }

    .item-date > h3 {
        margin-top: 30px;
        font-size: 16px !important;
        font-weight: 600;
        margin-left: 10px;
        margin-bottom: 0px !important;
    }

    .item-date > h4 {
        margin-top: 0px;
        font-size: 14px !important;
        font-weight: 100;
        margin-left: 10px !important;
        margin-bottom: 30px;
    }

    .item-info > h3 {
        margin-top: 20px;
        font-size: 16px !important;
        font-weight: 600;
        margin-left: 10px;
    }

    .item-info > h4 {
        margin-top: 0px;
        font-size: 14px !important;
        font-weight: 100;
        margin-left: 10px !important;
    }

    .item-info > p {
        margin-top: 15px;
        font-size: 14px !important;
        font-weight: 100;
        margin-bottom: 15px;
        margin-left: 10px;
    }

    .search-pagination {
        margin-bottom: 30px;
        padding-top: 10px;
        border-bottom: 1px solid #d2d2d2;
        border-top: 1px solid #d2d2d2;
        padding-bottom: 10px;
        margin-top: 50px;
    }

    .divider {
        margin-left: 0px !important;
        margin-right: 0px !important;
        border-bottom: 1px solid #d2d2d2;
    }

    .item-title > h3 {
        margin-top: 0px !important;
        font-size: 16px !important;
        font-weight: 700;
        margin-left: 5px !important;
        margin-bottom: 0px !important;
    }

    .item-content > p {
    }

    .show-more {
        cursor: pointer;
    }

    .show-less {
        cursor: pointer;
        display: none;
    }
</style>
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
<div class="container-fluid">
    <div class="row">
        <div class="title-holder" style="text-align: center;">
            <h1>
                <span id="baseTemplatePlaceholder_content_C007_ctl00_ctl00_MessageLabel">Training vacancies listing</span>
            </h1>
        </div>
    </div>
</div>
<div class="container-fluid">

    <div class="dir-panel firm-directory firm">
        <div class="filter-box">

            <div class="row">
                <div class="form-group col-sm-12 col-md-3 txtbox-padding" style="margin-top: 5px;">
                    <asp:TextBox ID="fLocation" runat="server" class="form-control" name="fLocation" placeholder="Location" />
                </div>
                <div class="form-group col-sm-12 col-md-3 txtbox-padding" style="margin-top: 5px;">
                    <form method="get"><%-- DO NOT REMOVE THIS EMPTY FORM --%></form>
                    <form method="get" style="margin-left: 10px; margin-right: 10px;">
                        <span class="multiselect-pill authorisation-pill">Vacancy Type</span>
                        <asp:ListBox ID="fVacancyType" runat="server" SelectionMode="Multiple" CssClass="SlectBox">
                            <asp:ListItem Text="Training Contract" Value="Training Contract"></asp:ListItem>
                            <asp:ListItem Text="Flexible Route" Value="Flexible Route"></asp:ListItem>
                            <asp:ListItem Text="Internship up to 3 months" Value="Internship up to 3 months"></asp:ListItem>
                            <asp:ListItem Text="Internship over 3 months" Value="Internship over 3 months"></asp:ListItem>
                        </asp:ListBox>
                    </form>
                </div>
                <div class="form-group col-sm-12 col-md-3 txtbox-padding" style="margin-top: 5px;">
                    <form method="get"><%-- DO NOT REMOVE THIS EMPTY FORM --%></form>
                    <form method="get" style="margin-left: 10px; margin-right: 10px;">
                        <span class="multiselect-pill sector-pill">Train in Type</span>
                        <asp:ListBox ID="fTrainInType" runat="server" SelectionMode="Multiple" CssClass="SlectBox">
                            <asp:ListItem Text="Business" Value="Business"></asp:ListItem>
                            <asp:ListItem Text="Practice" Value="Practice"></asp:ListItem>
                            <asp:ListItem Text="Public sector" Value="Public sector"></asp:ListItem>
                        </asp:ListBox>
                    </form>
                </div>
                <div class="form-group col-sm-12 col-md-3 txtbox-padding">
                    <button id="btnSearchTrainingVacancies" class="cai-btn cai-btn-red" style="width: 100%">Search </button>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12 pill-list"></div>
            </div>
            <div class="row">
                <div class="form-group  col-xs-12 col-md-3" style="text-align: left; line-height: 50px;">
                    <span id="result-count" class="totalBasedOnFilters"></span>
                </div>
            </div>
        </div>
        <div style="clear: both;"></div>
        <div class="trainingResultsContainerFirst" style="margin-bottom: 20px;">
            <asp:Panel runat="server" ID="pnlTrainingSearchResults" Visible="true" CssClass="trainingResultsContainer">
                <div id="search-result">
                </div>

                <div class="container-fluid search-pagination" id="search-pagination" style="display: none;">

                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-lg-12">
                                <h4 id="pagination-text"></h4>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
    <%-- 1. Firm Directory END--%>
</div>
<br />
<br />
<br />
<%--    </ContentTemplate>
</asp:UpdatePanel>--%>

<script type="text/javascript">

    function replaceURLWithHTMLLinks(text) {
        var exp = /(\b(((https?|ftp|file|):\/\/)|www[.])[-A-Z0-9+&@#\/%?=~_|!:,.;]*[-A-Z0-9+&@#\/%=~_|])/ig;
        var temp = text.replace(exp, "<a href=\"$1\" target=\"_blank\">$1</a>");
        var result = "";

        while (temp.length > 0) {
            var pos = temp.indexOf("href=\"");
            if (pos == -1) {
                result += temp;
                break;
            }
            result += temp.substring(0, pos + 6);

            temp = temp.substring(pos + 6, temp.length);
            if ((temp.indexOf("://") > 8) || (temp.indexOf("://") == -1)) {
                result += "http://";
            }
        }

        return result;
    }

    const template = `
            <$ for(var i = 0; i < items.length; i++) { const item = items[i]; $>
            <div class ="training-item">
                        <div class ="container-fluid" style="margin-top:20px; ">
                            <div class ="row list-item" style="min-height:100px;background-color:#8C1D40">
                                <div class ="col-lg-3 col-md-3 item-info">
                                    <h3> <$= item.TVJobTitle $></h3>
                                    <h4> <$= item.TVJobTown $> </h4>
                                    <h4 style="margin-bottom:15px !important;"><$= item.TVJobCounty $> </h4>
                                </div>
                                <div class ="col-lg-6 col-md-6  item-info">
                                    <p> <$= item.TVCompanyDescription $> </p>
                                </div>
                                <div class ="col-lg-3  col-md-3  item-date">
                                    <div style="float:right;margin-top:35px;font-size:20px;margin-bottom:35px;margin-right:15px;">
                                        <i class ="far fa-angle-down show-more" rel="<$= item.TVID $>" id="btn-show-more-<$= item.TVID $>"></i>
                                        <i class ="far fa-angle-up show-less" rel="<$= item.TVID $>"  id="btn-show-less-<$= item.TVID $>"></i>
                                    </div>
                                    <h3>Date Posted</h3>
                                    <h4> <$= item.TVJobDatePosted $> </h4>

                                </div>
                            </div>
                         </div>

                        <div id="show-more-<$= item.TVID $>" class ="container-fluid" style="padding-top:0px;border-left:1px solid #d2d2d2;border-right:1px solid #d2d2d2;border-bottom:1px solid #d2d2d2;display:none;">
                            <div class ="row" style="padding:20px 0px;">
                                <div class ="col-lg-3 item-title">
                                    <h3>Title </h3>
                                </div>
                                <div class ="col-lg-9 item-content">
                                    <p> <$= item.TVJobTitle $> </p>
                                </div>
                            </div>
                            <div class ="row divider"></div>
                            <div class ="row" style="padding:20px 0px;">
                                <div class ="col-lg-3 item-title">
                                    <h3> Date posted </h3>
                                </div>
                                <div class ="col-lg-9 item-content">
                                    <p>  <$= item.TVJobDatePosted $> </p>
                                </div>
                            </div>
                            <div class ="row divider"></div>
                            <div class ="row" style="padding:20px 0px;">
                                <div class ="col-lg-3 item-title">
                                    <h3> Company name </h3>
                                </div>
                                <div class ="col-lg-9 item-content">
                                    <p>  <$= item.TVCompanyName $> </p>
                                </div>
                            </div>
                            <div class ="row divider"></div>
                            <div class ="row" style="padding:20px 0px;">
                                <div class ="col-lg-3 item-title">
                                    <h3> Location </h3>
                                </div>
                                <div class ="col-lg-9 item-content">
                                    <p>  <$= item.TVJobTown $>, <$= item.TVJobCounty $> </p>
                                </div>
                            </div>
                            <div class ="row divider"></div>
                            <div class ="row" style="padding:20px 0px;">
                                <div class ="col-lg-3 item-title">
                                    <h3> Benefits and renumeration </h3>
                                </div>
                                <div class ="col-lg-9 item-content">
                                    <p>  <$= item.TVBenefits $> </p>
                                </div>
                            </div>
                            <div class ="row divider"></div>
                            <div class ="row" style="padding:20px 0px;">
                                <div class ="col-lg-3 item-title">
                                    <h3> About the company </h3>
                                </div>
                                <div class ="col-lg-9 item-content">
                                    <p>  <$= item.TVCompanyDescription $> </p>
                                </div>
                            </div>
                            <div class ="row divider"></div>
                            <div class ="row" style="padding:20px 0px;">
                                <div class ="col-lg-3 item-title">
                                    <h3> Vacancy type </h3>
                                </div>
                                <div class ="col-lg-9 item-content">
                                    <p>  <$= item.TVVacancyType.replace(',',', ') $> </p>
                                </div>
                            </div>
                            <div class ="row divider"></div>
                            <div class ="row" style="padding:20px 0px;">
                                <div class ="col-lg-3 item-title">
                                    <h3> Train in type </h3>
                                </div>
                                <div class ="col-lg-9 item-content">
                                    <p>  <$= item.TVTrainInType $> </p>
                                </div>
                            </div>
                            <div class ="row divider"></div>
                            <div class ="row" style="padding:20px 0px;">
                                <div class ="col-lg-3 item-title">
                                    <h3> Job spec </h3>
                                </div>
                                <div class ="col-lg-9 item-content">
                                    <p>  <$= item.TVJobSpec $> </p>
                                </div>
                            </div>
                            <div class ="row divider"></div>
                            <div class ="row" style="padding:20px 0px;">
                                <div class ="col-lg-3 item-title">
                                    <h3> Requirements </h3>
                                </div>
                                <div class ="col-lg-9 item-content">
                                    <p>  <$= item.TVJobRequirements $> </p>
                                </div>
                            </div>
                            <div class ="row divider"></div>
                            <div class ="row" style="padding:20px 0px;">
                                <div class ="col-lg-3 item-title">
                                    <h3> How to apply </h3>
                                </div>
                                <div class ="col-lg-9 item-content">
                                    <p> <$- replaceURLWithHTMLLinks(item.TVHowToApply) $> </p>
                                </div>
                            </div>
                            <div class ="row divider"></div>
                            <div class ="row" style="padding:20px 0px;">
                                <div class ="col-lg-3 item-title">
                                    <h3> Closing date </h3>
                                </div>
                                <div class ="col-lg-9 item-content">
                                    <p>  <$= item.TVJobClosingDate $> </p>
                                </div>
                            </div>
                            <div class ="row divider"></div>
                            <div class ="row" style="padding:20px 0px;">
                                <div class ="col-lg-3 item-title">
                                    <h3> Website </h3>
                                </div>
                                <div class ="col-lg-9 item-content">
                                    <p>  <$- replaceURLWithHTMLLinks(item.TVWebSite) $> </p>
                                </div>
                            </div>
                        </div>
                           </div>
              <$ } $>

            `;


    function removeFunction(ab) {
        var c = ab.getAttribute('title');
        ab.remove();
        $('.SumoSelect ul.options li.selected label').each(function () {
            var d = $(this).text()
            if (d == c) {
                $(this).parent().click();
            }
        });
    };

    function calculatePagination(count, page, pageSize) {
        let numPages = count / pageSize;
        let html = '';
        for (let i = 0 ; i < numPages; i++) {
            html += i == page ? '<span style="margin-left:5px">' + (i + 1) + '</span>' : ' <a href="javascript:doSearch(' + i + ',' + pageSize + ')" style="margin-left:5px;">' + (i + 1) + '</a> ';
        }
        $('#pagination-text').html(html);
    }

    function doSearch(page, pageSize) {
        let data = {
            "location": "",
            "vacancyType": "",
            "trainingType": "",
            "pageSize": pageSize,
            "page": page
        }
        data.location = $('#<%= fLocation.ClientID%>').val();
        data.vacancyType = $('#<%= fVacancyType.ClientID%>').val() ? $('#<%= fVacancyType.ClientID%>').val().join() : '';
        data.trainingType = $('#<%= fTrainInType.ClientID%>').val() ? $('#<%= fTrainInType.ClientID%>').val().join() : '';
        $.ajax({
            url: '<%= Page.ResolveUrl("~/WebServices/WebService.asmx/SearchTrainingVacancies") %>',
                data: JSON.stringify(data),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    const result = data.d.Result.result;
                    const count = data.d.Result.count;
                    ejs.delimiter = '$';
                    $('#result-count').html('' + count + ' results found');
                    calculatePagination(data.d.count, data.d.page, data.d.pageSize);
                    $('#search-pagination').show();
                    const html = ejs.render(template, { items: result, replaceURLWithHTMLLinks: replaceURLWithHTMLLinks });
                    $('#search-result').html(html);
                    $('.show-more').click(function (event) {
                        event.stopPropagation();
                        event.preventDefault();
                        const item = $(this).attr('rel');
                        $('#btn-show-more-' + item).hide();
                        $('#btn-show-less-' + item).show();
                        $('#show-more-' + item).slideDown("slow");
                    });

                    $('.show-less').click(function (event) {
                        event.stopPropagation();
                        event.preventDefault();
                        const item = $(this).attr('rel');
                        $('#btn-show-less-' + item).hide();
                        $('#btn-show-more-' + item).show();
                        $('#show-more-' + item).slideUp("slow");
                    });
                }
            });
        }


        $(window).load(function () {

            $('#btnSearchTrainingVacancies').click(function (event) {
                event.stopPropagation();
                event.preventDefault();
                doSearch(0, 50);
            });

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
            $('.SlectBox').SumoSelect();
            $('.SumoSelect').addClass("hide-it");
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
                var arr = [];
                i = 0;
                $(this).closest('.row').siblings('.row').children('.pill-list').html("");
                var chkSelectedItem = $(this).closest('.form-group').parent('.row').children('.form-group').children('form').children('.SumoSelect').children('.multiple').children('.options').children('.selected');
                chkSelectedItem.each(function () {
                    var textValue = $(this).text();
                    arr[i++] = $(this).text();
                    $(this).closest('.row').siblings('.row').children('.pill-list').append('<span class="grey-pill" onclick="removeFunction(this);" title="' + textValue + '">' + textValue + '</span>');
                });
                //performAjaxCountOfResultsBasedOnParams();
            });

            $(".SlectBox").change();

        });
</script>
