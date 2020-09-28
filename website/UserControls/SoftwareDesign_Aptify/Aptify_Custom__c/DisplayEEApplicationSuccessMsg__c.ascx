<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/DisplayEEApplicationSuccessMsg__c.ascx.vb" Inherits="DisplayEEApplicationSuccessMsg__c" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<div style="text-align:left" >

    <asp:Label ID="lblEEApplicationSuccess" runat="server" Font-Bold="true"></asp:Label>
    <br />
    </div>

<div style="text-align:center">
    <br />
    <asp:Button ID="btnPrint" runat="server" Text="print eligibility & exemption application form" Class="submitBtn"/>
    <br />
</div>
<div>
    <p>&nbsp;</p>
</div>
 <cc2:User runat="server" ID="User1" />
