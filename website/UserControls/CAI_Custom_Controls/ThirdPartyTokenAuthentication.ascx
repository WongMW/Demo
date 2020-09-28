<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ThirdPartyTokenAuthentication.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.ThirdPartyTokenAuthentication" %>
<%@ Register TagPrefix="uc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<asp:Panel runat="server" ID="pnlSettings" Visible="false">
    <div>Vendor Key: <asp:Label runat="server" ID="lblVendorKey"></asp:Label></div>
    <div>Vendor URL: <asp:Label runat="server" ID="lblVendorUrl"></asp:Label></div>
    <div>Token Timeout: <asp:Label runat="server" ID="lblTokenTimeout"></asp:Label></div>
</asp:Panel>


<uc2:User id="User1" runat="server" />