<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ChapterReportViewer.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Chapters.ChapterReportViewerControl" Debug = "true" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc4" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessHierarchyTree" %>
<div class="content-container clearfix">
<table id="tblMain" runat="server">
    <tr>
        <td>
            <asp:label id="lblTitle" runat="server"></asp:label></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblError" runat="server"></asp:Label></td>
    </tr>
</table>
<asp:linkbutton id="lnkChapter"  CssClass="lnkChapterReportViewer" Runat="server">Go To Chapter</asp:linkbutton>
<asp:linkbutton id="lnkReports"  CssClass="lnkChapterReportViewer" Runat="server">Chapter Reports</asp:linkbutton>
<asp:Panel ID="pnl" runat="server" Width="100%" ScrollBars="Both"  >
    <CR:crystalreportviewer id="rptViewerMain" runat="server"  AutoDataBind="True" />
        </asp:Panel>
    <cc3:User id="User1" runat="server" />
</div>