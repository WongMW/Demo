
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AptifyNavBar_Bootstrap__cai.ascx.vb" Inherits="Aptify.PublicWebSite.AptifyNavBar"  %> 
<div class="navbar navbar-default">
    <div class="container-fluid">
        <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#mynavbar-content">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="#">Chartered Accountants</a>
        </div>
        <div class="collapse navbar-collapse" id="mynavbar-content">
            <div id="theContainer" runat="server">
                <ul class="nav navbar-nav">
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
        </div>
    </div>
</div>
