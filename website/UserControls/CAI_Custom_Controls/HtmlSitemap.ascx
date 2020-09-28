<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HtmlSitemap.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.HtmlSitemap" %>
<%@ Register Src="~/UserControls/CAI_Custom_Controls/HtmlSitemapPart.ascx" TagPrefix="td" TagName="SitemapPart" %>

<asp:Repeater ID="sitemap" runat="server" OnItemDataBound="sitemap_ItemDataBound">
    <HeaderTemplate>
        <style>
            ul.sitemap ul li {padding-left: 20px;}
            ul.sitemap ul li:before {content: "- ";}
            ul.sitemap .fa{display:none}
        </style>
        <ul class="sitemap">
    </HeaderTemplate>
    <ItemTemplate>
        <td:SitemapPart runat="server" id="sitemapPart"></td:SitemapPart>
    </ItemTemplate>
    <FooterTemplate>
        </ul>
    </FooterTemplate>
</asp:Repeater>