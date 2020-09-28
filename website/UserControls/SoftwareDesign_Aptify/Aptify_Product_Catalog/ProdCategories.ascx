<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Product_Catalog/ProdCategories.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.ProdCategories" %>
<%@ Register TagPrefix="uc1" TagName="RecommentedProduct" Src="RelatedProductsGrid.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FeaturedProduct" Src="FeaturedProducts.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindProduct" Src="FindProduct.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ProdCategoryBar" Src="~/UserControls/SoftwareDesign_Aptify/Aptify_General/ProdCategoryBar.ascx" %>
<h1 runat="server" id="lblHeader">
    ICE Store
</h1>
 <%--dilip issue 12719 Can we change the image at top right 19/1/2012--%>
  <div>
    <asp:Image ID="ImgSideImage" runat="server" Width="160px" Height="140px" ImageUrl="" ImageAlign="Right" />
       </div>
<%--dilip issue 12719 Can we change the image at top right 19/1/2012--%>
<div id="outerDiv" class="OuterDiv">
    <h6 runat="server" id="lblPageHeaderText" class="main-title">
    </h6>
    <div id="ProductCategoryDiv" class="ProductCategoryDiv">
        <div class="BrowseProduct">
            <h6 runat="server" id="lblProdCatHeader" class="BrowseProduct">
            </h6>
            <div class="ProdCategory" >
             <uc1:ProdCategoryBar ID="ProdCategoryBar" runat="server" />
                <%--<asp:DataList ID="lstCategories" runat="server" Width="100%">
                    <ItemTemplate>
                        <table width="100%">
                            <tr>
                                <td valign="top" align="left">
                                    <img alt="" runat="server" id="imgProdCategory" width="21" height="23" src="~/Images/NoImageAvailable.jpg">
                                    <asp:HyperLink runat="server" ID="lnkProdCategory" CssClass="lnkDecoration"></asp:HyperLink>
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr>
                                <td class="CategorySeparator">
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <FooterStyle></FooterStyle>
                    <HeaderStyle></HeaderStyle>
                </asp:DataList>--%>
            </div>
        </div>
    </div>
    <div style="width: 58%;" class="div">
        <h6 runat="server" id="lblWelcomeText" class="textfont">
        </h6>
        <br />
        <div class="FindProduct">
            <uc1:FindProduct ID="FindProduct" HeaderText="{Find Product}" ShowHeaderIfEmpty="False"
                runat="server" />
        </div>
        <br />
        <div>
            <uc1:FeaturedProduct ID="FeaturedProducts" HeaderText="{Featured Product}" ShowHeaderIfEmpty="False"
                runat="server" />
        </div>
    </div>
    <div style="width:20%;" class="div">
       <%-- <table>
            <tr>
                <td align="right">
                    <asp:Image ID="ImgFeaturedProd" runat="server" Width="178px" Height="151px" ImageUrl="~/Images/side-image.png"
                        ImageAlign="Middle" />
                </td>
            </tr>
            <tr>
                <td align="left">
                   
                </td>
            </tr>
        </table>--%>
    </div>
</div>
