<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_General/SecurityError.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.SecurityError"  %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<div class="content-container clearfix">
  <asp:Label ID="lblMessage" Runat="server" Visible="False"></asp:Label>
</div>
<cc2:User runat="server" ID="User" />