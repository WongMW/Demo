<%@ Control Language="VB" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.AbstractManagement.ReviewSingleAbstract" CodeFile="~/UserControls/Aptify_Abstract_Management/ReviewSingleAbstract.ascx.vb" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EbusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEbusinessUser" %>

<div class="content-container clearfix">
    <asp:Label ID="lblError" runat="server" Text="Label" Visible="False"></asp:Label>
    <TABLE id="tblMain" class="data-form" runat="server">
    <TR>
      <TD class="LeftColumn">Subject</TD>
      <TD class="RightColumn">
        <asp:Label id="lblSubject" runat="server" AptifyDataField="Subject" /></TD>
    </TR>
    <TR>
      <TD class="LeftColumn">Submitted</TD>
      <TD class="RightColumn">
        <asp:Label id="lblSubmittedBy" runat="server" AptifyDataField="SubmittedBy" /></TD>
    </TR>
    <TR>
      <TD class="LeftColumn">Date</TD>
      <TD class="RightColumn">
        <asp:Label id="lblDateSubmitted" runat="server" AptifyDataField="DateSubmitted" /></TD>
    </TR>
    <TR>
      <TD class="LeftColumn">Title</TD>
      <TD class="RightColumn">
        <asp:Label id="lblTitle" runat="server" AptifyDataField="Title" /></TD>
    </TR>
    <TR>
      <TD class="LeftColumn">Category</TD>
      <TD class="RightColumn">
        <asp:Label id="lblCategory" runat="server" AptifyDataField="Category" /></TD>
    </TR>
    <TR>
      <TD class="LeftColumn">Summary</TD>
      <TD class="RightColumn">
        <asp:Label id="lblSummary" runat="server" AptifyDataField="Summary" /></TD>
    </TR>
    <TR>
      <TD class="LeftColumn">Body</TD>
      <TD class="RightColumn">
        <asp:Label id="lblBody" runat="server" AptifyDataField="Body" /></TD>
    </TR>
  </TABLE>
  <cc3:User id="AptifyEbusinessUser1" runat="server"></cc3:User>
</div>
    
