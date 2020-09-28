
 <%@ Control Language="VB" AutoEventWireup="false" CodeFile="AddForum.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Forums.AddForumControl" %> 


<div class="content-container clearfix">
    <table id="tblMain" runat="server" class="data-form">
	    <tr>
		    <td class="LeftColumn"><asp:Label id="Label1" runat="server">Name:</asp:Label></td>
		    <td class="RightColumn"><asp:TextBox id="txtName" AptifyDataField="Name" runat="server" width="200px" ></asp:TextBox></td>
	    </tr>
	    <tr>
		    <td class="LeftColumn"><asp:Label id="Label2" runat="server">Description:</asp:Label></td>
		    <td class="RightColumn"><asp:TextBox id="txtDescription" AptifyDataField="Description" runat="server" TextMode="MultiLine" Width="300px" Height="150px" ></asp:TextBox></td>
	    </tr>
	    <tr>
		    <td></td>
		    <td><asp:Button id="cmdCreateForum" runat="server" Text="Create Forum" CssClass="submitBtn"  ></asp:Button>
			    <asp:Label id="lblError" runat="server" Visible="False"></asp:Label></td>
	    </tr>
    </table>
</div>
