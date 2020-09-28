<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DisplayMessage.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Forums.DisplayMessageControl" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagName="message" TagPrefix="uc1" Src="Message.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Forums" Src="Forums.ascx" %>
<%@ Register src="ForumTitle.ascx" tagname="ForumTitle" tagprefix="uc2" %>

			    <table runat="server" id="tblMain">
                    <tr>
                        <td>
                            <asp:label id="lblDiscussionForum" runat="server" /></td>
                    </tr>
				    <tr>
					    <td><asp:HyperLink  runat="server" id="lnkForum" /><br />
						    <asp:label id="lblDescription" runat="server"></asp:label>
						    <uc1:message id="Message" runat="server"></uc1:message>
					    </td>
				    </tr>
			    </table>
    <cc2:User runat="server" ID="User1" />
