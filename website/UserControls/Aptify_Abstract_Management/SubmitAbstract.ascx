<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.AbstractManagement.SubmitAbstract" CodeFile="SubmitAbstract.ascx.vb" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<style type="text/css">
    .style1
    {
        width: 374px;
    }
</style>

<div class="content-container clearfix"> 
    <table runat="server" id="tblMain" class="data-form">
      <tr>
      <%--  aparna Issue 8271 for add fontname and size for textbox--%>
        <td class="LeftColumnAbstract" colspan="2">Subject</td>
        <td class="style1" colspan="2">
          <asp:TextBox id="txtSubject" runat="server" AptifyDataField="Subject"  Width="80%" Font-Names="Segoe UI, Arial, Helvetica" Font-Size="12px"></asp:TextBox></td>
      </tr>
      <tr>
       <%--  aparna Issue 8271 for add fontname and size for textbox--%>
        <td class="LeftColumnAbstract" colspan="2">Title</td>
        <td class="style1" colspan="2">
          <asp:TextBox id="txtTitle" runat="server" TextMode="MultiLine" 
                AptifyDataField="Title" Width="80%" Height="75px" Font-Names="Segoe UI, Arial, Helvetica" Font-Size="12px"></asp:TextBox></td>
      </tr>
      <tr>
       <%--  aparna Issue 8271 for add fontname and size for textbox--%>
        <td class="LeftColumnAbstract" colspan="2">Category</td>
        <td class="style1" colspan="2">
          <asp:DropDownList id="cmbCategory" runat="server" AptifyDataField="CategoryID" Font-Names="Segoe UI, Arial, Helvetica" Font-Size="12px" 
                AptifyListTextField="Name" AptifyListValueField="ID" AptifyListSQL="" 
                Width="180px" ></asp:DropDownList></td>
      </tr>
      <tr>
       <%--  aparna Issue 8271 for add fontname and size for textbox--%>
        <td class="LeftColumnAbstract" colspan="2">Summary</td>
        <td class="style1" colspan="2">
          <asp:TextBox id="txtSummary" runat="server" TextMode="MultiLine" 
                AptifyDataField="Summary" Width="80%"  Height="100px" Font-Names="Segoe UI, Arial, Helvetica" Font-Size="12px" ></asp:TextBox></td>
      </tr>
      <tr>
       <%--  aparna Issue 8271 for add fontname and size for textbox--%>
        <td class="LeftColumnAbstract" colspan="2">Body</td>
        <td class="style1" colspan="2">
          <asp:TextBox id="txtBody" runat="server" TextMode="MultiLine" 
                AptifyDataField="Body" Width="80%"  Height="100px"  Font-Names="Segoe UI, Arial, Helvetica" Font-Size="12px"></asp:TextBox></td>
      </tr>
      <tr>
    <%--  Nalini Issue 12734--%>
        <td width="10px">&nbsp;</td>
        <td colspan="2">&nbsp;</td>
        <td align="left"><asp:Button ID="cmdSubmit" Runat="server" Text="Submit Abstract" CssClass="submitBtn" ></asp:Button></td>
      </tr>
    </table>
    <asp:Label id="lblMessage" runat="server" Visible="False"  />
    <cc3:User id="AptifyEbusinessUser1" runat="server" />
</div>