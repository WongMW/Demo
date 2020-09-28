<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ProductCategory.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.ProductCategoryControl" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness.ProductCatalog" Assembly="ProductCategoryLinkString" %>
<%@ Register TagPrefix="uc1" TagName="ProdListingGrid" Src="ProdListingGrid.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ProdCategories" Src="ProdCategories.ascx" %>

<div class="content-container clearfix">
    <cc2:ProductCategoryLinkString id="ProductCategoryLinkString1" runat="server" HyperlinkRootCategory="True" RootCategoryText="All Categories"  Font-Bold="true" ></cc2:ProductCategoryLinkString>
</div>
<uc1:ProdListingGrid id="ProdListingGrid" HeaderText="{ProductCategory}" ShowHeaderIfEmpty="False" runat="server"></uc1:ProdListingGrid>
<%--<div id="ProdSubCategory" style="width:100%; float:left;">
<uc1:ProdCategories ID="ProdCategories" HeaderText="Sub-Categories" ShowHeaderIfEmpty="false" runat="server" />
</div>--%>
