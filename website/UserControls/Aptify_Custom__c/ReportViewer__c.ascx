<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ReportViewer__c.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.ReportViewer__c" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<div style="width:500px;">

<%--<asp:Panel ID="pnl" runat="server" Width="100%" ScrollBars="Both"  >--%>

    <CR:crystalreportviewer id="rptViewerMain" runat="server"  AutoDataBind="True" />
<%--</asp:Panel>--%>
<cc3:User id="User1" runat="server" />
</div>