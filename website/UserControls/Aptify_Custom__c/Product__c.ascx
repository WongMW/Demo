<%@ Register Src="TrackClick__c.ascx" TagName="TrackClick" TagPrefix="uc2" %>
<%--Aptify e-Business 5.5.1, July 2013--%>
<%@ Control Language="VB" AutoEventWireup="false" Debug=" true" CodeFile="Product__c.ascx.vb"
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
<%@ Register Src="~/UserControls/Aptify_Custom__c/RecordAttachments__c.ascx" TagPrefix="uc3"
    TagName="RecordAttachments__c" %>
<style type="text/css">
    .style1
    {
        width: 100%;
    }
</style>
<div class="content-container clearfix">
    <cc1:ProductCategoryLinkString ID="ProductCategoryLinkString1" runat="server" HyperlinkRootCategory="True"
        Font-Bold="true" RootCategoryText="All Categories"></cc1:ProductCategoryLinkString>
</div>
<div style="vertical-align: text-top;">
    <h6 class="textfont">
        <asp:Label ID="lblName" runat="server"></asp:Label></h6>
    <br />
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
</div>
<div id="outerdiv">
    <div id="ProdDetails">
        <table width="100%">
            <tr>
                <td rowspan="2" valign="top" class="auto-style2">
                    <asp:Image ID="imgProduct" runat="server" Height="200px" Width="190px" CssClass="Imgproduct">
                    </asp:Image>
                </td>
                <%-- Anil changess for issue 12996  --%>
                <td valign="top" style="width: 100%; vertical-align: top;">
                    <%-- Aparna issue 9025,9042 for Add panel to hide controls for non-web enabled product--%>
                    <asp:Panel ID="productdetailpanel" runat="server">
                        <table style="width: 100%;">
                            <tr>
                                <td class="ICETBLabel">
                                    <asp:Label ID="lblAvailable" runat="server" Text="Available:" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="ICETBLabelVal">
                                    <asp:Label ID="lblavailval" runat="server" ForeColor="Green" Font-Bold="true">In Stock</asp:Label>
                                    <img alt="Item Not Available" id="imgNotAvailable" src="" visible="false" runat="server"
                                        style="margin-left: 3px;" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="ICETBLabel">
                                    <asp:Label ID="lblQuantity" runat="server" Text="Quantity:" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="ICETBLabelVal">
                                    <asp:TextBox ID="txtQuantity" runat="server" Width="28px">1</asp:TextBox>
                                    <asp:Label ID="lblSellingUnits" runat="server">Selling Units</asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td style="width: 30%;">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="ICETBLabel">
                                    <asp:Label ID="lblPricing" runat="server" Text="Price:" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="ICETBLabelVal">
                                    <asp:Label ID="lblPrice" runat="server" Text="-" CssClass="ICELabel"></asp:Label>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td style="width: 30%;" class="btnCart">
                                    <asp:Button ID="lnkAddToCart" runat="server" Text="Add To Cart" CssClass="submitBtn" />
                                </td>
                            </tr>
                          <%-- <tr>
                                <td class="ICETBLabel">
                                    <asp:Label ID="lblChkAutoRenew" runat="server" Text="Auto Renew:" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="ICETBLabelval">
                                    <asp:CheckBox ID="ChkAutoRenew" runat="server" />
                                </td>
                            </tr>--%>
                            <%-- Suvarna D Control alignment done for the IssueId 12720 on 19 Jan , 2012 --%>
                            <tr>
                                <td class="ICETBLabel">
                                </td>
                                <td class="ICETBLabelVal">
                                    <asp:Label ID="lblMemSavings" runat="server" ForeColor="DarkGreen" Font-Bold="true"
                                        Visible="false"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td style="width: 40%;" rowspan="2">
                                    <table class="style1">
                                        <tr>
                                            <td class="msgCart">
                                                <asp:Label ID="lblAdded" Visible="False" runat="server" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="btnCart">
                                                <asp:Button ID="lnkViewCart" runat="server" Text="View My Cart" CssClass="submitBtn" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr runat="server" id="trProductCode">
                                <td class="ICETBLabel">
                                    <asp:Label ID="lblItemCode" runat="server" Text="Item Code:" Font-Bold="true"></asp:Label>
                                </td>
                                <td class="ICETBLabelVal">
                                    <asp:Label ID="lblCode" runat="server" Text="-"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 2px;">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td style="width: 30%;" class="btnCart">
                                    &nbsp;
                                </td>
                            </tr>
                            <%-- Suvarna D Aliggnment done for the IssueId 12720 on 19 Jan , 2012 --%>
                            <tr>
                                <td class="ICETBLabel">
                                    <asp:Label ID="lblNote" runat="server" Text="*Note: " Font-Bold="true" Visible="False"></asp:Label>
                                </td>
                                <td class="ICETBLabelval" colspan="4">
                                    <asp:Label ID="lblProductMessage" runat="server" Visible="False">Label</asp:Label>
                                    &nbsp; &nbsp; &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 2px;">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr id="trIdTrainingPoint" runat="server" visible="false">
                                <td style="width: 2px;" colspan="2">
                                    <asp:Label ID="lblTrainingPoints" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <%-- Suvarna D Aliggnment done for the IssueId 12720 on 19 Jan , 2012 --%>
                            <tr>
                                <td class="ICETBLabel" colspan="2">
                                    <asp:Label ID="lblNewerProduct" runat="server" Text="A newer version of this product is available: "
                                        Font-Bold="true" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="ICETBLabel" colspan="2">
                                    <asp:Button ID="btnNewVersion" runat="server" Text="View latest product version"
                                        CssClass="ProdButtonLink" SkinID="Test" Visible="false" />
                                </td>
                            </tr>
                            <%--<tr>
                            <td style="text-align: left; padding-left: 5px; padding-bottom: 5px; padding-top: 5px;"
                                colspan="2">
                                <asp:Label ID="lblAdded" Visible="False" runat="server"></asp:Label>
                            </td>
                        </tr>--%>
                            <%--End by Suvarna D Aliggnment done for the IssueId 12720 on 19 Jan , 2012 --%>
                        </table>
                    </asp:Panel>
                </td>
                <%--<td style="width: 30%;">
                   
                  
                   
                </td>--%>
            </tr>
            <tr>
                <td colspan="2" style="width: 1px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblSummary" runat="server" Text="Summary:" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblDescription" runat="server" Text="-"></asp:Label>
                </td>
                <td>
                </td>
            </tr>
  <tr>
                <td colspan="2">
                    <div runat="server" id="divISBN">
                        <div class="overview-label">
                            ISBN Number

                             <div class="short-desc">
                                 <asp:Label ID="lblISBN" runat="server" Text=""></asp:Label>
                             </div>

                        </div>

                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblprodDesc" runat="server" Text="Product Description:" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblLongDescription" runat="server" Text="Not Available" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CheckBox ID="ChkRequiredAgreement" runat="server" TextAlign="Left" Text="" />
                    <asp:Label ID="lblTicketCondtion" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <uc1:ProductGroupingContentsGrid ID="ProductGroupingContentsGrid" runat="server"
                        Visible="false" />
                </td>
            </tr>
            <tr id="trRecordAttachment" runat="server" visible="false">
                <td colspan="2">
                    <b>Documents</b><br />
                    <asp:Panel ID="Panel1" runat="Server" Style="border: 1px Solid #000000;">
                        <table runat="server" id="Table2" class="data-form" width="100%">
                            <tr>
                                <td class="RightColumn">
                                    <uc3:RecordAttachments__c ID="RecordAttachments__c" runat="server" AllowView="True"
                                        AllowAdd="True" AllowDelete="false" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <cc6:User ID="User1" runat="server" />
        <cc2:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="False"></cc2:AptifyShoppingCart>
    </div>
    <%--<div id="Products" style="float: right; width: 30%;">
        <table>
            <tr>
                <td>
                    <uc1:RelatedProductsGrid ID="RelatedProductsGrid" HeaderText="{Related Product}" ShowHeaderIfEmpty="False" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <uc1:FeaturedProduct ID="FeaturedProducts" HeaderText="{Featured Product}" ShowHeaderIfEmpty="False"
                        runat="server" />
                </td>
            </tr>
        </table>
    </div>--%>
</div>
<uc2:TrackClick ID="TrackClick__c" runat="server" />
<Rad:RadWindow ID="radMockTrial" runat="server" Width="350px" Height="120px" Modal="True"
    BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
    Title="Product" Behavior="None">
    <ContentTemplate>
        <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
            height: 100%; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
            <tr>
                <td align="center">
                    <asp:Label ID="lblWarning" runat="server" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="btnYes" runat="server" Text="OK" Width="70px" class="submitBtn" />
                    <%-- <asp:Button ID="btnNo" runat="server" Text="No" Width="70px" class="submitBtn" />--%>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</Rad:RadWindow>