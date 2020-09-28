<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_General/GenericLogin.ascx.vb"
    Inherits="UserControls_Aptify_General_GenericLogin" %>
<%@ Register Src="~/UserControls/SoftwareDesign_Aptify/Aptify_General/LoginSF4.ascx" TagName="LoginSF4"
    TagPrefix="uc2" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<div>    
    
<asp:Label ID="lblstat" style="display:none;" runat="server" Text="Currently You are not logged in! Please Login Now."></asp:Label>
      
    <div class="BorderDiv">
        <uc2:LoginSF4 ID="LoginSF4" runat="server" />
        <cc2:User runat="Server" ID="User1" />
    </div>
</div>
