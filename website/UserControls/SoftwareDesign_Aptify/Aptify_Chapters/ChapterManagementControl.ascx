<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Chapters/ChapterManagementControl.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Chapters.ChapterManagementControl" %>

<div class="content-container clearfix">
  <table id="tblMain" runat="server" class="data-form">
    <tr>
      <td>
        <strong><asp:HyperLink ID="lnkChapters" runat="server">My Chapters</asp:HyperLink></strong><br/>
        Shows the chapters that you are linked to and allows you to view 
          and, if you have the required level of permissions, to edit the membership 
          roster for the chapters.
      </td>
    </tr>
    <tr>
      <td>
        <strong><asp:HyperLink ID="lnkChapterSearch" runat="server">Find Chapters</asp:HyperLink></strong><br/>
        Search for chapters by name, location, and other 
            attributes
      </td>
    </tr>
  </table>
</div>