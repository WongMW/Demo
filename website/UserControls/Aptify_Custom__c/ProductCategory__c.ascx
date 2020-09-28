<%--Aptify e-Business 5.5.1, July 2013--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ProductCategory__c.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.ProductCategory__c" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness.ProductCatalog" Assembly="ProductCategoryLinkString" %>
<%@ Register TagPrefix="uc1" TagName="ProdListingGrid" Src="../Aptify_Custom__c/ProdListingGrid__c.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ProdCategories" Src="~/UserControls/Aptify_Custom__c/ProdCategories__c.ascx" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<%@ Register TagPrefix="cc6" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
 
<div class="content-container clearfix">
    <cc2:ProductCategoryLinkString id="ProductCategoryLinkString1" runat="server" HyperlinkRootCategory="True" RootCategoryText="All Categories"  Font-Bold="true" ></cc2:ProductCategoryLinkString>
</div>
<uc1:ProdListingGrid id="ProdListingGrid" HeaderText="{ProductCategory}" ShowHeaderIfEmpty="False" runat="server"></uc1:ProdListingGrid>
    <cc6:User ID="User1" runat="server" />
    
<%--<div id="ProdSubCategory" style="width:100%; float:left;">
<uc1:ProdCategories ID="ProdCategories" HeaderText="Sub-Categories" ShowHeaderIfEmpty="false" runat="server" />
</div>--%>
