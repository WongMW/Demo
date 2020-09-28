<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_General/ProdCategoryBar.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.ProdCategoryBar" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div id="divcatmenu">
    <rad:RadMenu ID="RadMenu1" runat="server" CssClass="aptify-category-side-nav">
	<ItemTemplate >
            <a runat="server" class="aptify-category-link" href='<%# Container.NavigateUrl %>'><%# Container.Text %></a>
        </ItemTemplate>
        <%--<Items >
            <rad:RadMenuItem Text="Diplomas" NavigateUrl="~/lll/lllcourses.aspx" />
        </Items>--%>
    </rad:RadMenu>
</div>