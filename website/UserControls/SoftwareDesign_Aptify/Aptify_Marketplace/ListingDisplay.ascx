<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Marketplace/ListingDisplay.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.MarketPlace.ListingDisplay" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<cc3:User ID="User1" runat="server" />
<table class="data-form">
	<tr>
		<td class="LeftColumnMarketPlace" >Company 
					Name:</td>
		<td><asp:label id="lblCompanyName" runat="server" ></asp:label></td>
		<td >
			<asp:Button id="btnEdit"  runat="server" Text="Edit Listing" CssClass="submitBtn" ></asp:Button>
			<asp:Button id="btnRequest"  runat="server" Text="Request Information" CssClass="submitBtn" ></asp:Button></td>
	</tr>
	<tr>
		<td  class="LeftColumnMarketPlace">
			Company Contact:
		</td>
		<td><asp:label id="lblCompanyContact" runat="server"></asp:label></td>
		<td >
	    </td>
	</tr>
</table>
<table id="tblListingInfo" class="data-form">
	<tr>
		<td  class="LeftColumnMarketPlace">Name</td>
		<td class="RightColumn"><asp:label id="lblName" runat="server" ></asp:label></td>
	</tr>
	<tr>
		<td  class="LeftColumnMarketPlace">Listing Type</td>
		<td class="RightColumn"><asp:label id="lblListingType" runat="server" ></asp:label></td>
	</tr>
	<tr>
		<td class="LeftColumnMarketPlace">
			Category
		</td>
		<td class="RightColumn"><asp:label id="lblCategory" runat="server" ></asp:label></td>
	</tr>
	<tr>
		<td class="LeftColumnMarketPlace">Offering Type</td>
		<td class="RightColumn"><asp:label id="lblOfferingType" runat="server" ></asp:label></td>
	</tr>
	<tr>
		<td class="LeftColumnMarketPlace">Description</td>
		<td class="RightColumn"><asp:label id="lblDescription" runat="server"  ></asp:label></td>
	</tr>
	<tr>
		<td class="LeftColumnMarketPlace">Vendor Web Site</td>
		<td class="RightColumn"><asp:hyperlink id="lnkCompanyURL" runat="server"></asp:hyperlink></td>
	</tr>
	<tr>
		<td class="LeftColumnMarketPlace">Request Information Email
				
		</td>
		<td class="RightColumn"><asp:hyperlink id="lnkEmail" runat="server" ></asp:hyperlink></td>
	</tr>
</table>
<p>
	<%--<asp:Label id="lblHTML" runat="server" ></asp:Label>--%>
</p>
