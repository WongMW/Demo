
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AptifyNavBar_NoBootstrap__cai.ascx.vb" Inherits="Aptify.PublicWebSite.AptifyNavBar"  %> 
<div id="theContainer" runat="server">
        <ul class="link-list">
            <li>
            <h2><asp:Label runat="server" ID="lblTitle">Quick Links</asp:Label> </h2>
            </li>
        <asp:Repeater runat="server" ID="Repeater1">
		<HeaderTemplate>
		</HeaderTemplate>
            <ItemTemplate>
                <li class="<%#DataBinder.Eval(Container.DataItem, "CssClass") %>"><a href="<%#DataBinder.Eval(Container.DataItem, "URL") %>"><%#DataBinder.Eval(Container.DataItem, "Title")%></a> </li>
            </ItemTemplate>
        </asp:Repeater>
	  </ul> 
</div>