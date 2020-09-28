<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_General/SubmissionComplete.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.SubmissionCompleteControl" %>
<%@ Register Assembly="AptifyEBusinessUser" Namespace="Aptify.Framework.Web.eBusiness"
    TagPrefix="cc1" %>
    
<div class="content-container clearfix">
    <table class="data-form">
      <tr>
        <td colspan="2" align="center">
          <hr size="1" />
            <br />
            <asp:Label ID="lblMessage" runat="server" Font-Size="Larger"></asp:Label>
            <br />
            <br />
          <hr size="1" />
            <asp:Label ID="lblRedirectMessage" runat="server" Font-Size="Smaller"></asp:Label><br />
            <br />
        </td>
      </tr>
    </table>
</div>
