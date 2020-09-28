<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginLogoutLink.ascx.cs" Inherits="SoftwareDesign.Controls.SDWidgets.LoginLogoutLink" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessLogin" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <ul class="sfNavHorizontal sfNavList">
            <li class="loginli" runat="server">
                <asp:LinkButton ID="PageLink" CausesValidation="false" AutoPostBack="false" runat="server" OnClick="LinkButton1_Click">Login</asp:LinkButton>
                <ul id="dropdownList" runat="server" visible="false" class="nav-logout-dropdown">
                    <li>
                        <asp:LinkButton ID="LinkButton2" CausesValidation="false" runat="server" OnClick="OpenProfile">My Account</asp:LinkButton>
                    </li>
                    <li>
                        <asp:LinkButton ID="LinkButton1" CausesValidation="false" runat="server" OnClick="LogoutClick">Signout</asp:LinkButton>
                    </li>
                </ul>
            </li>
        </ul>
    </ContentTemplate>
</asp:UpdatePanel>


<cc2:AptifyWebUserLogin ID="WebUserLogin" runat="server" />
<cc2:User runat="server" ID="cntrlUser" />
