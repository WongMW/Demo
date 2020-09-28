<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SingleForum.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Forums.SingleForum" %>
<%@ Register Src="ForumMessageGrid.ascx" TagName="ForumMessageGrid" TagPrefix="uc1" %>
<%@ Register Src="Message.ascx" TagName="Message" TagPrefix="uc2" %>
<%@ Register Src="CreateMessage.ascx" TagName="CreateMessage" TagPrefix="uc3" %>
<!-- This control is intended for use on other pages outside of the main
     forum browsing area so that a forum can easily be linked to other
     areas of the site such as committees, chapters, education, meetings, etc.
!-->
<table width="100%">
    <tr>
        <td>
            <uc1:ForumMessageGrid ID="ForumMessageGrid" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            <uc2:Message Visible="false" ID="Message" runat="server" />
            <uc3:CreateMessage Visible="false" ID="CreateMessage" runat="server" />
        </td>
    </tr>
</table>