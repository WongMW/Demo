<%@ Control Language="VB" AutoEventWireup="false" CodeFile="RequestInfoComplete.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.MarketPlace.RequestInfoComplete" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<cc3:User ID="User1" runat="server" />

<table id="tblRequestInfoResult" class="data-form" width="100%">
	<tr>
		<td></td>
	</tr>
	<tr>
		<td style="font-weight: bold">Thank you for using International Demo Association MarketPlace!</td>
	</tr>
	<tr>
		<td>
            <hr />
        </td>
	</tr>
	<tr>
		<td>
            <asp:Label ID="lblSuccess" runat="server" 
                
                Text="Thank you for using the International Demo Association MarketPlace. Your request has been expedited for the listing(s) you have selected. You should be receiving feedback soon on your requests. If you do not, please visit our site again and review your requests." 
                Width="100%"></asp:Label>
            <asp:Label ID="lblFailure" runat="server" 
                
                Text="A failure occurred during the information request. Please try again later an if the prolem continues, contact our web technical support." 
                ForeColor="Red" Width="100%"></asp:Label>
        </td>
	</tr>
	<tr>
		<td>
		</td>
	</tr>
</table>

