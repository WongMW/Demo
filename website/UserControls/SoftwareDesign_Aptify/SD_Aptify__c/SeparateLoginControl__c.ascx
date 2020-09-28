<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SeparateLoginControl__c.ascx.cs" Inherits="SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c.SeparateLoginControl__c" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessLogin" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Label ID="lblWelcome" runat="server">Welcome</asp:Label>
        <br />
        <asp:Label ID="lblMultiLogin" runat="server"></asp:Label>
        <br />
        <asp:DropDownList runat="server" ID="cmdWebUser" CssClass="txtBoxEditProfile" Width="200px" AutoPostBack="false"></asp:DropDownList>
        <asp:Button ID="btnLogin" runat="server" Text="Update" AutoPostBack="true" CssClass="submitBtn" OnClick="btnLogin_Click"></asp:Button>
        <cc1:AptifyWebUserLogin ID="WebUserLogin1" runat="server" Visible="true"></cc1:AptifyWebUserLogin>
        <cc2:AptifyShoppingCart ID="ShoppingCartLogin" runat="server"></cc2:AptifyShoppingCart>
    </ContentTemplate>
</asp:UpdatePanel>