<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Marketplace/RequestInfo.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.MarketPlace.RequestInfo" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<cc3:User ID="User1" runat="server" />

<table id="tblListingInfo" class="data-form">
	<tr>
	</tr>
	<tr>
		<td>Enter any comments that you would like to send along with this request below: </td>
	</tr>
	<tr>
		<td>
            <asp:TextBox ID="txtComments" runat="server" Height="180px" TextMode="MultiLine" 
                Width="480  px"></asp:TextBox>
        </td>
	</tr>
	<tr>
		<td>
			<asp:Button id="btnSubmit" runat="server" Text="Submit Request" CssClass="submitBtn" ></asp:Button>
		</td>
	</tr>
</table>
<p>
    &nbsp;</p>

