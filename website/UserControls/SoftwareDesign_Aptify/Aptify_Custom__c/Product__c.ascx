<%@ Register Src="TrackClick__c.ascx" TagName="TrackClick" TagPrefix="uc2" %>
<%--Aptify e-Business 5.5.1, July 2013--%>
<%@ Control Language="VB" AutoEventWireup="false" Debug=" true" CodeFile="~/UserControls/Aptify_Custom__c/Product__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.Product__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness.ProductCatalog"
    Assembly="ProductCategoryLinkString" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessHierarchyTree" %>
<%@ Register TagPrefix="uc1" TagName="ProductTopicCodesGrid" Src="../Aptify_Product_Catalog/ProductTopicCodesGrid.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="cc6" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="uc1" TagName="ProductGroupingContentsGrid" Src="../Aptify_Product_Catalog/ProductGroupingContentsGrid.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RelatedProductsGrid" Src="../Aptify_Product_Catalog/RelatedProductsGrid.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FeaturedProduct" Src="../Aptify_Product_Catalog/FeaturedProducts.ascx" %>
<%@ Register TagPrefix="Rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Custom__c/RecordAttachments__c.ascx" TagPrefix="uc3"
    TagName="RecordAttachments__c" %>
<%@ Register TagPrefix="uc5" TagName="TopicCodesGrid" Src="../Aptify_Product_Catalog/ProductTopicCodesGrid.ascx" %>

<style>
    .ICETBLabelVal-qty-box {
        left: 0;
    }

    .ICETBLabelVal-price .product-price {
        line-height: 0.9 !important;
    }
</style>
<%--Product Catlog Performance--%>
<asp:HiddenField ID="hdnPerson" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hdnProduct" runat="server" ClientIDMode="Static" />

<%-- Susan Wong, Ticket #18964 start - Add load screen --%>
<div class="raDiv" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing">
                <div class="loading-bg">
                    <img src="/Images/CAITheme/bx_loader.gif" />
                    <span>LOADING...<br />
                        <br />
                        Please do not leave or close this window while the request is processing.</span>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<%-- Susan Wong, Ticket #18964 end - Add load screen --%>
<%-- Susan Wong, Ticket #18964 start - Add load screen --%>
<asp:UpdatePanel ID="Updatepnl" runat="server">
    <ContentTemplate>
        <div class="content-container clearfix" style="display: none;">
            <cc1:ProductCategoryLinkString ID="ProductCategoryLinkString1" runat="server" HyperlinkRootCategory="True"
                Font-Bold="true" RootCategoryText="All Categories"></cc1:ProductCategoryLinkString>
        </div>
        <div id="outerdiv" class="product-wrapper">
            <asp:Panel ID="productdetailpanel" runat="server">
                <div id="ProdDetails" class="product-container clearfix cai-table">
                    <div class="product-image-section">
                        <div class="product-image">
                            <asp:Image ID="imgProduct" runat="server" CssClass="Imgproduct schema-prod-img"></asp:Image>
                        </div>

                    </div>
                    <%-- Anil changess for issue 12996  --%>
                    <%-- Aparna issue 9025,9042 for Add panel to hide controls for non-web enabled product--%>
                    <div class="product-details">
                        <h1 class="textfont">
                            <asp:Label ID="lblName" runat="server" CssClass="schema-prod-name"></asp:Label>
                        </h1>
                        <br />
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        <div class="ICETBLabel">
                            <asp:Label ID="lblAvailable" runat="server" Text="Available:" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblavailval" runat="server" ForeColor="Green" Font-Bold="true" CssClass="schema-availability">In stock</asp:Label>
                            <img alt="Item Not Available" id="imgNotAvailable" src="" visible="false" runat="server" style="margin-left: 3px;" />
                        </div>
                        <div class="summary">
                            <div class="overview-label">Summary</div>
                            <asp:Label ID="lblSummary" runat="server" Text="" Font-Bold="true"></asp:Label>
                            <div class="short-desc">
                                <asp:Label ID="lblDescription" runat="server" Text="-" CssClass="schema-prod-description"></asp:Label>
                            </div>
                            <div runat="server" id="divISBN">
                                <div class="overview-label">ISBN: &nbsp;<asp:Label ID="lblISBN" Font-Bold="false" runat="server" Text="" CssClass="schema-isbn"></asp:Label></div>
                            </div>
                            <%--Added as part of log #20141--%>
                            <div runat="server" id="divDatePublished">
                                <div class="overview-label">Published: &nbsp;<asp:Label ID="lblDatePublished" Font-Bold="false" runat="server" Text="" CssClass="schema-datePublished"></asp:Label></div>
                            </div>
                            <div runat="server" id="divFormat">
                                <div class="overview-label">Format: &nbsp;<asp:Label ID="lblFormat" runat="server" Font-Bold="false" Text="" CssClass="schema-bookFormat"></asp:Label></div>
                            </div>
                            <%--end--%>
                        </div>
                        <%-- <tr>
                <td class="ICETBLabel">
                <asp:Label ID="lblChkAutoRenew" runat="server" Text="Auto Renew:" Font-Bold="true"></asp:Label>
                </td>
                <td class="ICETBLabelval">
                <asp:CheckBox ID="ChkAutoRenew" runat="server" />
                </td>
                </tr>--%>
                        <%-- Suvarna D Control alignment done for the IssueId 12720 on 19 Jan , 2012 --%>
                        <div class="ICETBLabelVal">
                            <uc1:ProductGroupingContentsGrid ID="ProductGroupingContentsGrid" runat="server"
                                Visible="false" />
                        </div>
                        <div class="ICETBLabel-pricing">
                            <div class="msgCart">
                                <asp:Label ID="lblAdded" Text="added" runat="server" ForeColor="Red"></asp:Label>
                            </div>
                            <div class="ICETBLabelVal-price">
                                <asp:Label ID="lblPricing" runat="server" Text="Price:" CssClass="price-label"></asp:Label>
                                <asp:Label ID="lblPrice" runat="server" Text="-" CssClass="product-price schema-prod-price"></asp:Label>&nbsp;<asp:Label ID="lblMemSavings" runat="server" ForeColor="DarkGreen" Font-Bold="true" Visible="false"></asp:Label>
                                <%--Added as part of #20508--%>
                                <asp:Label ID="lblMemberPrice" runat="server" Text="" ForeColor="DarkGreen" CssClass="product-price schema-prod-mem-price"></asp:Label>
                                &nbsp;
                            </div>
                            <div class="ICETBLabelVal-qty-box">
                                <asp:Label ID="lblQty" runat="server" Text="Qty" class="qty-box-label"></asp:Label>
                                <asp:TextBox ID="txtQuantity" runat="server" class="qty-box">1</asp:TextBox>
                                <asp:Label ID="lblSellingUnits" runat="server">Selling units</asp:Label>
                            </div>
                            <div class="product-btnCart style-1">
                                <asp:Button ID="lnkAddToCart" runat="server" Text="Add To Cart" CssClass="submitBtn" />
                                <asp:Button ID="lnkViewCart" runat="server" Text="View My Cart" CssClass="submitBtn" />
                                <asp:Button runat="server" ID="btnBack" Text="Back" CssClass="submitBtn cai-btn-red-inverse" />
                            </div>
                            <div style="clear: both; text-align: right; padding-top: 5px;">
                                <asp:CheckBox ID="ChkRequiredAgreement" runat="server" TextAlign="Left" Text="" />
                                <asp:Label ID="lblTicketCondtion" runat="server"></asp:Label>
                            </div>
                        </div>
                        <%-- Susan Wong, Ticket #18526 start - Moved product description up --%>
                        <div class="product-full-details">
                            <div class="overview-label">
                                <h2>
                                    <asp:Label ID="lblprodDesc" runat="server" Text="Description" CssClass="overview-label" Style="display: none;"></asp:Label>Description</h2>
                                <%--Added as part of log #20594--%>
                                <div style="clear: both;">
                                    <div class="overview-label" style="float: left;">Product type: &nbsp;</div>
                                    <div style="line-height: 30px">
                                        <asp:Label ID="lblProductType" runat="server" />
                                    </div>
                                </div>
                                <div style="clear: both;">
                                    <div class="overview-label" style="float: left;">Category: &nbsp;</div>
                                    <div style="line-height: 30px">
                                        <asp:Label ID="lblCategory" runat="server" CssClass="schema-prod-category" />
                                    </div>
                                </div>
                                <%--end--%>
                                <%--Added as part of log #20141--%>
                                <div runat="server" id="divEducationalUse">
                                    <div class="overview-label">Who is this for: &nbsp;<asp:Label ID="lblEducationalUse" Font-Bold="false" runat="server" Text="" CssClass="schema-educationalUse"></asp:Label></div>
                                </div>
                                <div runat="server" id="divEdition">
                                    <div class="overview-label">Edition: &nbsp;<asp:Label ID="lblEdition" Font-Bold="false" runat="server" Text="" CssClass="schema-bookEdition"></asp:Label></div>
                                </div>
                                <div runat="server" id="divCopyrightDate">
                                    <div class="overview-label">Copyright date: &nbsp;<asp:Label ID="lblCopyrightDate" Font-Bold="false" runat="server" Text="" CssClass="schema-copyrightYear"></asp:Label></div>
                                </div>
                                <div>
                                    <%--Added for Log #20442--%>
                                    <div runat="server" id="divWeight">
                                        <div class="overview-label">
                                            Weight: &nbsp;
                                            <asp:Label ID="lblWeight" Font-Bold="false" runat="server" Text="" CssClass="schema-prod-weight"></asp:Label>
                                        </div>
                                    </div>
                                    <div runat="server" id="divDimension">
                                        <div class="overview-label">Dimension: &nbsp;<asp:Label ID="lblDimension" Font-Bold="false" runat="server" Text=""></asp:Label></div>
                                    </div>
                                    <%--End of #20442--%>
                                </div>
                                <div runat="server" id="divKeywords">
                                    <asp:Label ID="lblKeywords" Style="font-size: 0px" runat="server" Text="" CssClass="schema-keywords"></asp:Label>
                                </div>
                                <%--end--%>
                                <div style="font-size: 16px !important; font-weight: normal; display: block; clear: both/* font-family:Source Sans Pro, sans-serif; */">
                                    <div class="full-detail-content">
                                        <asp:Label ID="lblLongDescription" runat="server" Text="Not available" />
                                    </div>
                                </div>
                                <!-- #19451 Susan Wong: Add Topic codes to template -->
                                <uc5:TopicCodesGrid ID="TopicCodesGrid1" ShowHeaderIfEmpty="False" runat="server" />
                            </div>
                            <uc2:TrackClick ID="TrackClick__c" runat="server" />
                            <Rad:RadWindow ID="radMockTrial" runat="server" Width="350px" Height="160px" Modal="True"
                                BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                                Title="Product" Behavior="None">
                                <ContentTemplate>
                                    <asp:Label ID="lblWarning" runat="server" Font-Bold="true"></asp:Label>
                                    <div style="text-align: center;">
                                        <asp:Button ID="btnYes" runat="server" Text="OK" Width="70px" class="submitBtn" />
                                    </div>
                                    <%-- <asp:Button ID="btnNo" runat="server" Text="No" Width="70px" class="submitBtn" />--%>
                                </ContentTemplate>
                            </Rad:RadWindow>
                        </div>
                        <%-- Susan Wong, Ticket #18526 end - Moved product description up --%>
                    </div>
                    <div runat="server" id="trProductCode">
                        <div class="ICETBLabel">
                            <asp:Label ID="lblItemCode" runat="server" Text="Item code:" Font-Bold="true"></asp:Label>
                        </div>
                        <div class="ICETBLabelVal">
                            <asp:Label ID="lblCode" runat="server" Text="-"></asp:Label>
                        </div>
                    </div>
                    <%-- Suvarna D Aliggnment done for the IssueId 12720 on 19 Jan , 2012 --%>
                    <div class="other-date">
                        <div class="ICETBLabel">
                            <asp:Label ID="lblNote" runat="server" Text="*Note: " Font-Bold="true" Visible="False"></asp:Label>
                        </div>
                        <div class="ICETBLabelval">
                            <asp:Label ID="lblProductMessage" runat="server" Visible="False">Label</asp:Label>
                        </div>

                        <div id="trIdTrainingPoint" runat="server" visible="false" style="display: none!important;">
                            <asp:Label ID="lblTrainingPoints" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </div>
                        <%-- Suvarna D Aliggnment done for the IssueId 12720 on 19 Jan , 2012 --%>
                        <div class="ICETBLabel">
                            <asp:Label ID="lblNewerProduct" runat="server" Text="A newer version of this product is available: "
                                Font-Bold="true" Visible="False"></asp:Label>
                        </div>
                        <div class="ICETBLabel">
                            <asp:Button ID="btnNewVersion" runat="server" Text="View latest product version"
                                CssClass="ProdButtonLink" SkinID="Test" Visible="false" />
                        </div>
                        <div>
                            <asp:Label ID="Label2" Visible="False" runat="server"></asp:Label>
                        </div>
                        <%--End by Suvarna D Aliggnment done for the IssueId 12720 on 19 Jan , 2012 --%>
                    </div>

                </div>
            </asp:Panel>
        </div>
        <!-- END content-container -->
        <%-- Susan Wong, Ticket #18526 start - Moved featured product up --%>
        <div id="Products" class="vertical-layout">
            <div class="featured-products-slider-wrapper">
                <div class="featured-products-slider">
                    <uc1:RelatedProductsGrid ID="RelatedProductsGrid" HeaderText="{Related Product}" ShowHeaderIfEmpty="False" runat="server" />
                </div>
            </div>
        </div>
        <%-- Susan Wong, Ticket #18526 end - Moved featured product up --%>
        <div id="trRecordAttachment" runat="server" visible="true">
            <b>Documents</b><br />
            <asp:Panel ID="Panel1" runat="Server" Style="border: 1px Solid #000000;">
                <div runat="server" id="Table2" class="data-form" width="100%">
                    <div class="RightColumn">
                        <uc3:RecordAttachments__c ID="RecordAttachments__c" runat="server" AllowView="True"
                            AllowAdd="True" AllowDelete="false" />
                    </div>
                </div>
            </asp:Panel>
            <cc6:User ID="User1" runat="server" />
            <cc2:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="true"></cc2:AptifyShoppingCart>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<%--Product Catlog Performance--%>
<% If lnkAddToCart.Visible = True Then%>
<script type="text/javascript" language="javascript">
    function GetProductPrice() {
        let queryString = getUrlVars();
    <%--//Changes HTTP to HTTPS as part of 20095--%>
    let webmethod = "<%= Page.ResolveUrl("~/webservices/CourseEnrolments__c.asmx/GetProductPriceWithCampaign") %>";
    let personID = $('#hdnPerson').val();
    let productID = $('#hdnProduct').val();
    let campaignID = -1;
    if (queryString != null && queryString.length > 0) {
        if (typeof queryString["cID"] != 'undefined') {
            campaignID = queryString["cID"];
        }
        if (typeof queryString["ID"] != 'undefined') {
            productID = queryString["ID"];
        }
        if (typeof queryString["PersonID"] != 'undefined') {
            personID = queryString["PersonID"];
        }
    }

    let parmeter = JSON.stringify({ 'ProductID': productID, 'CampaignID': campaignID, 'shipToID': personID, 'billToID': personID });

    $.ajax({
        type: "POST",
        url: webmethod,
        data: parmeter,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var sResponse = JSON.parse(response.d);

            if (sResponse.length > 0) {
                <%--//Added as part of #20508--%>
                var returnPrice = sResponse.split("~")[0];
                var returnProdID = sResponse.split("~")[1];
                var returnMemberPrice = sResponse.split("~")[2];

                if (typeof returnPrice !== "undefined") {
                    $('#<%= lblPrice.ClientID%>').text(returnPrice);

                }

                if (typeof returnMemberPrice !== "undefined") {
                    $('#<%= lblMemberPrice.ClientID%>').text("(" + returnMemberPrice + " Member price)");
                    $('#<%= lblMemberPrice.ClientID%>').text($('#<%= lblMemberPrice.ClientID%>').text().replace('((', '(').replace(') M', ' M'))
                }

            }
        },
        failure: function (msg) {
            alert(msg);
        }
    });
}
</script>
<% End If %>
<script type="text/javascript" language="javascript">

    $(document).ready(function () {
        if ($('.featured-products-slider div.featured-products').length == 0) {
            $('.featured-products-slider-wrapper')
                .css("padding", "0px")
                .css("border", "0px");
        }
        <% If lnkAddToCart.Visible = True Then%>
        GetProductPrice();
        <% End If %>
    });
    function getUrlVars() {
        let vars = [], hash;
        let hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (let i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    }
    <%--Susan Wong, Ticket #18964 end - Add load screen--%>
    <%--Susan Wong: Ticket #20594 hide category name if it's Practice Toolkit--%>
    var prodCat = $('#baseTemplatePlaceholder_content_Product_lblCategory').text().trim();
    if (prodCat === "Practice toolkits ALL A-Z") {
        document.getElementById('baseTemplatePlaceholder_content_Product_lblCategory').innerHTML = "N/A";
    }
    <%--Susan Wong: Ticket #19467 hide related products when empty--%>
    if ($('#baseTemplatePlaceholder_content_Product_lnkViewCart').length > 0) {
        $('#baseTemplatePlaceholder_content_Product_lnkAddToCart').addClass('cai-btn-red-inverse');
    }

    <%--Susan Wong: Ticket #18740 hide related products when empty--%>

    $('.skills-topiccodes>div>div>div>table>tbody>tr>td:nth-child(1)').each(function () {
        let Mycelltxt = $(this).text().trim();
        if (Mycelltxt.indexOf('1') >= 0) {
            $(this).html('<span class="skill-code skill-1-code">' + Mycelltxt + '</span>');
        }
        else if (Mycelltxt.indexOf('2') >= 0) {
            $(this).html('<span class="skill-code skill-2-code">' + Mycelltxt + '</span>');
        }
        else if (Mycelltxt.indexOf('3') >= 0) {
            $(this).html('<span class="skill-code skill-3-code">' + Mycelltxt + '</span>');
        }
        else if (Mycelltxt.indexOf('4') >= 0) {
            $(this).html('<span class="skill-code skill-4-code">' + Mycelltxt + '</span>');
        }
    });
</script>
