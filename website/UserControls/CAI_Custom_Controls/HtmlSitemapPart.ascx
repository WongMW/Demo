<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HtmlSitemapPart.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.HtmlSitemapPart" %>

<li>
    <asp:HyperLink runat="server" ID="lnkTitle"></asp:HyperLink>
    <asp:Repeater ID="sitemap" runat="server" OnItemDataBound="sitemap_ItemDataBound">
        <HeaderTemplate>
            <ul>
        </HeaderTemplate>
        <ItemTemplate>
            
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>
</li>