<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Product_Catalog/RelatedProductsGrid.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.RelatedProductsGrid" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<div style="padding-bottom: 10px;">
    <h6 id="H1" runat="server" class="textfontsub featuredTitle">Customers who viewed this item also viewed</h6>
</div>
<div id="divGrid" runat="server" class="DivGrid clearfix featured-products">
    <%--Suvarna Deshmukh IssueID-12433,12430 and 12434 On Dec 13,2011 commented and added to implement new designs for ebusiness--%>
    <asp:DataList ID="grdMain" runat="server" HorizontalAlign="Left">
        <ItemTemplate>
            <asp:Image ID="ImgProd" runat="server" CssClass="Image" />
            <div class="description">
                <div class="productTitleLink">
                    <%--https://redmine.softwaredesign.ie/issues/18058 Added navigation URL property --%>
                    <asp:HyperLink ID="lnkProduct" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebName") %>' onclick='<%# GetGtmObject(Container.DataItem)  %>'></asp:Hyperlink> 
                    <%-- #21000 <asp:HyperLink ID="lnkProduct" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebName") %>'
                        Font-Bold="true" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "ProdPageURL").ToString %>'
                                   onclick='<%# GetGtmObject(Container.DataItem)  %>' ></asp:Hyperlink> --%>
                </div>
                <asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"GridDescription") %>' Font-Size="12px"></asp:Label>
                <%--Rashmi P, Issue 13150,8/21/12, The prompt text is not displayed for related product  --%>
                <asp:Label ID="lblWebPrompttext" runat="server" Text='<%# databinder.eval(container.dataitem,"PromptText") %>'></asp:Label>
                <%--https://redmine.softwaredesign.ie/issues/18058 Added productpageurl as URL instead of ? which current page url --%>
                <a runat="server" class="btn buyNowBtn" onclick='<%# GetGtmObject(Container.DataItem)  %>'>View product</a>
                <%-- #21000 <a runat="server" href='<%# DataBinder.Eval(Container.DataItem, "ProdPageURL").ToString %>'
                     class="btn buyNowBtn"
                   onclick='<%# GetGtmObject(Container.DataItem)  %>'>View Product</a>--%>
            </div>
        </ItemTemplate>
    </asp:DataList>
</div>
<cc1:User ID="User1" runat="server" />
