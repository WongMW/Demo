<%--Aptify e-Business 5.5.1, July 2013--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/ProdListingGrid__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.ProdListingGrid__c" %>
<%@ Register TagPrefix="uc1" TagName="FeaturedProduct" Src="~/UserControls/SoftwareDesign_Aptify/SD_Aptify__c/FeaturedProducts.ascx" %>
<%@ Register TagPrefix="uc2" TagName="FindProduct" Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Custom__c/FindProduct__c.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="uc1" TagName="ProdCategoryBar" Src="~/UserControls/SoftwareDesign_Aptify/Aptify_General/ProdCategoryBar.ascx" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc6" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register Src="TrackClick__c.ascx" TagName="TrackClick" TagPrefix="uc2" %>

<h6 runat="server" id="lblHeader" class="main-title marg-btm-20px" visible="false"
    style="display: none;"></h6>

<asp:HiddenField ID="hdnStatus" runat="server" ClientIDMode="Static" />
<%--Product Catlog Performance--%>
<asp:HiddenField ID="hdnPerson" runat="server" ClientIDMode="Static" />

<div class="raDiv" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing">
                <img src="/Images/CAITheme/bx_loader.gif" />
                <span>Loading</span>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<div class="content-container clearfix" id="divMain" runat="server">
    <div id="ProdNavbar" runat="server" class="ProdNavBar">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="sidebar-cat-nav">
                    <a id="<%= ClientID %>" class="sfNavToggle">&#9776;</a>
                </div>
                <div class="BrowseProduct aptify ProductCategoryDiv sfShown">
                    <h6 runat="server" id="lblProdCatHeader" class="BrowseProduct"></h6>
                    <div class="">
                        <uc1:ProdCategoryBar ID="ProdCategoryBar" runat="server" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="category-products">
        <asp:UpdatePanel ID="UpdatePanelgrdMain" runat="server">
            <ContentTemplate>
                <div class="sf_cols">
                    <div class="sf_colsOut sf_2cols_1_67">
                        <div id="baseTemplatePlaceholder_content_ctl00_ctl03_ctl03_C014_Col00" class="sf_colsIn sf_2cols_1in_67">
                            <div style="margin-top: 10px;">
                                <uc2:FindProduct ID="FindProduct" headertext="{Find Product}" showheaderifempty="False" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="sf_colsOut sf_2cols_2_33">
                        <div id="baseTemplatePlaceholder_content_ctl00_ctl03_ctl03_C014_Col01" class="sf_colsIn sf_2cols_2in_33">
                            <div class="button-block style-1">
                                <a style="text-decoration: none;" class="btn-full-width btn tooltip" href="/Event-Calendar" alt="Use the event calendar to find events by dates">CPD and events calendar</a>
                            </div>
                        </div>
                    </div>
                </div>
                <rad:RadGrid ID="grdMain" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                    ShowFooter="false" EnableTheming="true" emptydatatext="No Products to display"
                    AllowPaging="true">
                    <PagerStyle CssClass="sd-pager" />
                    <MasterTableView PagerStyle-PageSizeControlType="None">
                        <Columns>
                            <rad:GridTemplateColumn ItemStyle-HorizontalAlign="Center"
                                ItemStyle-CssClass="product-listing">
                                <ItemTemplate>
                                <div class="custom-prod">
                                    <asp:Image ID="ImgProd" runat="server" CssClass="Imgproduct" />
                                    </div>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn ItemStyle-HorizontalAlign="left" ItemStyle-CssClass="product-listing">
                                <ItemTemplate>
                                      <div class="title-link">
                                       <asp:HyperLink CssClass="product-name" ID="lnkProduct" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebName") %>' Font-Bold="true" onclick='<%# GetGtmObject(Container.DataItem)  %>'></asp:HyperLink>  
                                          <%-- #21000 <asp:HyperLink CssClass="product-name" ID="lnkProduct" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebName") %>' Font-Bold="true"
                                                      onclick='<%# GetGtmObject(Container.DataItem)  %>'
                                           ></asp:HyperLink> --%>
                                    </div>
                                    <asp:Label ID="lblDescription" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem,"WebDescription") %>'></asp:Label>


                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn ItemStyle-Width="35%" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="product-listing">
                                <ItemTemplate>
                                    <table class="tblPrice">
                                        <tr>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblPriceForYou" runat="server" Visible="false" Text="Price:" Font-Bold="true"></asp:Label>
                                                <asp:HiddenField ID="hdnProdID" runat="server" ClientIDMode="Static" Value='<%# DataBinder.Eval(Container.DataItem,"ID") %>' />
                                            </td>
                                            <td style="text-align: left;">
                                                <%-- Changes for Price calculation performance improvement --%>
                                                <asp:Label ID="lblPriceForYouVal" runat="server" CssClass="ICELabel price" Text="Price loading..." Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="display: none;">
                                                <asp:Label ID="lblItemCode" runat="server" Text="Item Code:" Font-Bold="true" Visible="false"></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblItemCodeVal" runat="server" Font-Bold="true" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn ItemStyle-CssClass="product-listing">
                                <ItemTemplate>
                                    <%--  <asp:Button ID="lnkAddToCart" runat="server" Text="Add To Cart " CssClass="submitBtn btn-full-width" Onclick="lnkAddToCart_Click"  />--%>
                                    <asp:Button ID="btnAddToCart" CssClass="submitBtn btn-full-width" runat="server" Text="View Product" OnClientClick="<%# GetGtmObject(Container.DataItem)  %>"></asp:Button>
                                    <%--  <asp:Button ID="btnAddToCart" CommandName="AddToCart" CssClass="submitBtn btn-full-width" CommandArgument='<%# Eval("ID")%>'
                                                        runat="server" Text="View Product"></asp:Button> --%>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </rad:RadGrid>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div>
            <uc1:FeaturedProduct ID="FeaturedProducts" headertext="{Featured Product}" showheaderifempty="False"
                runat="server" />
        </div>
    </div>
</div>
<br />
<cc6:User ID="User1" runat="server" />
<uc2:TrackClick ID="TrackClick1" runat="server" />
<cc2:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="False"></cc2:AptifyShoppingCart>


<%--Product Catlog Performance--%>
<script type="text/javascript" language="javascript">

    $(document).ready(function () {
        //GetProductPrice();
        //Paresh: Changes made to Abort Ajax Call
        $(document.getElementsByClassName("submitBtn")).click(AbortAjax);

        $('.sfNavToggle').click(function () {
            $('.ProductCategoryDiv').toggleClass("sfShown");
        });

        $('.product-listing .title-link a.product-name').each(function () {
            var text = $(this).text();
            if (text.length > 62) {
                text = text.substr(0, 59);
                text += "...";
                $(this).text(text);
            }
        });

        var ftProd = $('.featured-products-container');
        if ($('.product-listing').length === 0 && !(ftProd != undefined && ftProd != null && ftProd.is(":visible"))) {
            $('.category-products > div:nth-child(2)').html('<span class="empty-category">No products found for this category</span><p class="empty-category-p">Sorry, seems we there is no product matching your search, try to choose a different product/category to browse.</p><div class="sfimageWrp"><img title="No shop products found in this category" alt="No shop products found in this category" src="/Images/NoShopProduct.jpg"></div>');
        }
    });


    //Paresh: Changes made to Abort Ajax Call
    function AbortAjax() {
        $.each(xhrPool, function (idx, jqXHR) {
            jqXHR.abort();
        });
    }
    var xhrPool = [];
    //
    function GetChildControl(element, id) {
        var child_elements = element.getElementsByClassName("ICELabel");
        return child_elements[0];
    };

    function GetProductPrice() {
        //debugger;
        var gridView = document.getElementById("<%=grdMain.ClientID %>"); // Code added by GM for Redmine #20053
        if (gridView != null) { // Code added/updated by GM for Redmine #20053/20115
            var rows = gridView.getElementsByTagName("tr")// Code added by GM for Redmine #20053
            if (rows.length > 0) // Code added by GM for Redmine #20053
            {
                var rows = document.getElementById("<%=grdMain.ClientID %>").getElementsByClassName("tblPrice");
                var message = "";
                for (var i = 0; i < rows.length; i++) {
                    var label = GetChildControl(rows[i], "lblPriceForYouVal");
                    var productID = $(label).closest('tr').find("input:hidden").val();

                    var queryString = getUrlVars();
                    //Changes HTTP to HTTPS as part of 20095
                    // for live website its to be HTTPS but in UAT should be HTTP as part of #20115                    
                    var webmethod = "<%= Page.ResolveUrl("~/webservices/CourseEnrolments__c.asmx/GetProductPriceWithCampaign") %>";
                    //var webmethod = "https" + "://" + document.location.host + "/webservices/CourseEnrolments__c.asmx/GetProductPriceWithCampaign";

                    var personID = $('#hdnPerson').val();
                    var campaignID = -2;
                    var parmeter = JSON.stringify({ 'ProductID': productID, 'CampaignID': campaignID, 'shipToID': personID, 'billToID': personID });
                    ajaxObj = $.ajax({
                        type: "POST",
                        url: webmethod,
                        data: parmeter,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        //Paresh: Changes made to Abort Ajax Call
                        beforeSend: function (jqXHR) {
                            xhrPool.push(jqXHR);
                        },
                        complete: function (jqXHR, textStatus) {
                            xhrPool = $.grep(xhrPool, function (x) { return x != jqXHR });
                        },
                        //
                        success: function (response) {
                            var sResponse = JSON.parse(response.d);
                            if (sResponse.length > 0) {
                                //debugger;
                                var returnPrice = sResponse.split("~")[0];
                                var returnProdID = sResponse.split("~")[1];
                                //Added as part of #20508
                                var returnMemberPrice = sResponse.split("~")[2];
                                var grid = document.getElementById("<%=grdMain.ClientID %>").getElementsByClassName("tblPrice");
                                for (var i = 0; i < grid.length; i++) {
                                    var label = GetChildControl(rows[i], "lblPriceForYouVal");
                                    var productID = $(label).closest('tr').find("input:hidden").val();
                                    if (productID == returnProdID) {
                                        //Added as part of #20508
                                        if (typeof returnMemberPrice === "undefined") {
                                            $(label).text(returnPrice);

                                        }
                                        else
                                            $(label).text(returnPrice + " " + returnMemberPrice);
                                    }
                                }
                            }
                        },
                        failure: function (msg) {
                            $('#hdnStatus').val(msg);
                        }
                    });


                }
            } //   Redmine #20053
        } // End Redmine #20053
    }

    function getUrlVars() {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    }
</script>
