
 <%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Forums/Forum.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Forums.ForumControl" %> 

 <%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %> 
 <%@ Register TagPrefix="uc1" TagName="Message" Src="Message.ascx" %> 
 <%@ Register TagPrefix="uc2" TagName="CreateMessage" Src="CreateMessage.ascx" %> 
 <%@ Register TagPrefix="uc3" TagName="ForumMessageGrid" Src="ForumMessageGrid.ascx" %> 
 <%@ Register TagPrefix="uc4" TagName="Forums" Src="Forums.ascx" %> 
 <%@ Register TagPrefix="uc5" TagName="ForumTitle" Src="ForumTitle.ascx" %> 
 <%@ Register src="ForumTitle.ascx" tagname="ForumTitle" tagprefix="uc3" %> 

<div class="content-container clearfix">

<table id="tblMain" runat="server">
   
    <tr>
        <td><uc3:ForumMessageGrid id="ForumMessageGrid" runat="server" StyleMainTable="false" /></td>
    </tr>
    <tr>
        <td></td>
    </tr>
    <tr id="trMessage" runat="server">
        <td><uc1:Message id="Message" runat="server" StyleMainTable="false" />
            <uc2:CreateMessage StyleMainTable="false" id="CreateMessage" runat="server" Visible="False"></uc2:CreateMessage>
        </td>
    </tr>
</table>

    <cc2:User ID="User1" runat="server" />
</div>
