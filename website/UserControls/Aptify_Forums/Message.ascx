
 <%@ Control Language="VB" AutoEventWireup="false" CodeFile="Message.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Forums.Message" %> 

 <%@ Register Src="../Aptify_General/RecordAttachments.ascx" TagName="RecordAttachments" TagPrefix="uc1" %> 
 <%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %> 
<%-- Nalini 12458--%>
<div class="content-container clearfix">
<table id="tblMain" runat="server" class="data-form">
	<tr>
		<td><asp:Label ID="Label3" Runat="server"><b>Subject:</b></asp:Label></td>
		<td><asp:Label ID="lblSubject" Runat="server"></asp:Label></td>
		<td><asp:Label ID="Label1" Runat="server"><b>Sent:</b></asp:Label></td>
		<td><asp:Label ID="lblSent" Runat="server"></asp:Label></td>
	</tr>
	<tr>
		<td><asp:Label ID="Label4" Runat="server"><b>From:</b></asp:Label></td>
		<td><asp:Label ID="lblFrom" Runat="server"></asp:Label></td>
		<td><asp:Label ID="Label2" Runat="server"><b>Attachments:</b></asp:Label></td>
		<td>
            <asp:Label ID="lblAttachments" runat="server"></asp:Label><br />
            <asp:Button ID="btnAttachments" runat="server" CssClass="submitBtn" Text="View/Add" Visible="false" />
        </td>
	</tr>
	<tr>
		<td colspan="4">
			
			<asp:Label ID="lblBody" Runat="server"></asp:Label>
			<asp:Label id="lblError" runat="server" Visible="False"></asp:Label>
		</td>
	</tr>
	<tr runat="server" id="trAttachments" visible="false">
	    <td colspan="4">
			
            <uc1:RecordAttachments ID="RecordAttachments" runat="server" AllowView="true" />
	    </td>
	</tr>
</table>
<cc2:User runat="server" ID="User1" />
</div>
