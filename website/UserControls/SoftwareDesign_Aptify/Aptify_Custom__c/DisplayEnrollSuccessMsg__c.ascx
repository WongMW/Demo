<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/DisplayEnrollSuccessMsg__c.ascx.vb" Inherits="DisplayEnrollSuccessMsg__c" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<div>
    <br />
    <asp:Label ID="lblEnrollSuccess" runat="server"></asp:Label>
</div>
 <cc2:User runat="server" ID="User1" />
