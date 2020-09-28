<%@ Control Language="vb" AutoEventWireup="false" Codefile="CreateMessage.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Forums.CreateMessage"   %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>
<table width="100%">
    <tr>
        <td>
            <b>New Post</b></td>
    </tr>
	<tr >
		<td>
		    Subject:<br /><asp:TextBox id="txtSubject" runat="server" Width="99%"  CssClass="txtfontfamily" ></asp:TextBox></td>
	</tr>
	<tr>
		<td>
            Message:<br /><asp:TextBox ID="txtBody" MultiLine="True" Wrap="true" 
                runat="server" Width="99%" Height="200px" TextMode="MultiLine" CssClass="txtfontfamily txtRestrictResize" />
        </td>
	</tr>
    <tr height="40px">
        <td colspan="2" valign="middle">
            <asp:Button runat="server" ID="cmdSave" Text="Save" CssClass="submitBtn" />
            <asp:Button runat="server" ID="cmdCancel" Text="Cancel" CssClass="submitBtn" />
        </td>
    </tr>
</table>
<cc2:User runat="server" ID="User1" />
