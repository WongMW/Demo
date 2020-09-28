<%--Aptify e-Business 5.5.1, July 2013--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/ProductCategory__c.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.ProductCategory__c" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness.ProductCatalog" Assembly="ProductCategoryLinkString" %>
<%@ Register TagPrefix="uc1" TagName="ProdListingGrid" Src="../Aptify_Custom__c/ProdListingGrid__c.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ProdCategories" Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Custom__c/ProdCategories__c.ascx" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<%@ Register TagPrefix="cc6" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<div class="content-container clearfix">
    <cc2:productcategorylinkstring id="ProductCategoryLinkString1" runat="server" hyperlinkrootcategory="True" rootcategorytext="" visible="false" font-bold="true"></cc2:productcategorylinkstring>
</div>
<uc1:prodlistinggrid id="ProdListingGrid" headertext="{ProductCategory}" showheaderifempty="False" runat="server"></uc1:prodlistinggrid>
<cc6:user id="User1" runat="server" />

<%--<div id="ProdSubCategory" style="width:100%; float:left;">
<uc1:ProdCategories ID="ProdCategories" HeaderText="Sub-Categories" ShowHeaderIfEmpty="false" runat="server" />
</div>--%>

<script type="text/javascript">
    $(function() {
        var fullPath = location.href;
        $("div.aptify-category-side-nav > ul > li > div a.aptify-category-link").each(function () {
            if ($(this).prop("href") === fullPath) {
                $(this).parents('li').each(function () {
                    $(this).addClass("current");
                    $(this).children('div.rmText').children('.toggleBtn').addClass('open');
                    $(this).children('div.rmSlide').children('ul').addClass('showChildren');
                });
            }
        });
    });
</script>
