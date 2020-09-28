<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Product_Catalog/FeaturedProducts.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.FeaturedProductsControl" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<div>
    <h6 id="H1" runat="server" class="textfontsub featuredTitle">Featured Products</h6>
</div>
<div id="divGrid" runat="server" class="DivGrid clearfix">
    <div class="data-form featured-products">
        <asp:DataList ID="grdFeaturedProducts" runat="server" HorizontalAlign="Left">
            <ItemTemplate>
                <asp:Image ID="ImgProd" runat="server" CssClass="Image" />
                <div class="description">
                    <asp:HyperLink ID="lnkName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Name") %>' Font-Bold="true"></asp:HyperLink>
                    <br />
                    <asp:Literal ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Description")%>'></asp:Literal>
                </div>
            </ItemTemplate>
        </asp:DataList>
    </div>
    <cc1:User runat="server" ID="User1" />
</div>
<div id="noData" runat="server" class="content-container clearfix">
    <div class="data-form">
        <label style="font-weight: bold">
            From time to time, products will be listed here based on your areas of interest
                and past purchases.
        </label>
    </div>
</div>
