<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Product_Catalog/ProdListingGrid.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.ProdListingGrid" %>
<%@ Register TagPrefix="uc1" TagName="FeaturedProduct" Src="FeaturedProducts.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="uc1" TagName="ProdCategoryBar" Src="~/UserControls/SoftwareDesign_Aptify/Aptify_General/ProdCategoryBar.ascx" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<h6 runat="server" id="lblHeader" class="textfont" visible="false"></h6>
<div class="content-container clearfix" id="divMain" runat="server">
    <div id="ProdNavbar" runat="server" class="ProdNavBar">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="BrowseProduct">
                    <h6 runat="server" id="lblProdCatHeader" class="BrowseProduct"></h6>
                    <div>
                        <uc1:ProdCategoryBar ID="ProdCategoryBar" runat="server" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div style="width: 75%; margin-left: 15px; float: left;">
        <asp:UpdatePanel ID="UpdatePanelgrdMain" runat="server">
            <ContentTemplate>
                <rad:RadGrid ID="grdMain" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                    ShowFooter="false" EnableTheming="true" EmptyDataText="No Products to display" AllowPaging="true">
                    <PagerStyle CssClass="sd-pager" />
                    <MasterTableView>
                        <Columns>
                            <rad:GridTemplateColumn ItemStyle-Width="20%" ItemStyle-Height="120px" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-CssClass="product-listing ">
                                <ItemTemplate>
                                    <asp:Image ID="ImgProd" runat="server" Height="100px" Width="100px" CssClass="Imgproduct" />
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn ItemStyle-Width="45%" ItemStyle-HorizontalAlign="left" ItemStyle-CssClass="product-listing">
                                <ItemTemplate>
                                    <div>
                                        <asp:HyperLink ID="lnkProduct" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebName") %>'
                                            Font-Bold="true"></asp:HyperLink>
                                    </div>
                                    <asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebDescription") %>'></asp:Label>
                                    <br />
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn ItemStyle-Width="35%" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="product-listing">
                                <ItemTemplate>
                                    <table class="tblPrice">
                                        <tr>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblItemCode" runat="server" Text="Item Code:" Font-Bold="true" Visible="false"></asp:Label>
                                            </td>
                                            <td style="text-align: left; padding-left: 3px;">
                                                <asp:Label ID="lblItemCodeVal" runat="server" Font-Bold="true" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblPriceForYou" runat="server" Text="Price:" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td style="text-align: left; padding-left: 3px;">
                                                <asp:Label ID="lblPriceForYouVal" runat="server" CssClass="ICELabel"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </rad:RadGrid>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div>
            <uc1:FeaturedProduct ID="FeaturedProducts" HeaderText="{Featured Product}" ShowHeaderIfEmpty="False"
                runat="server" />
        </div>
    </div>
</div>
<br />
<cc2:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="False"></cc2:AptifyShoppingCart>
