<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.PersonListing" CodeFile="PersonListing.ascx.vb"  %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEbusinessUser" %>

<div class="content-container clearfix"> 
	<table runat="server" id="tblMain" class="data-form">
        <tr >
            <td class="LeftColumn" colspan="2">
                <asp:Label ID="lblResult" class="meeting-label" runat="server" 
                    ></asp:Label>
           </td>
           
        </tr>
        <tr>
            <td class="LeftColumn">
               <asp:Label id="lblName" runat="server" Text="Name:" class="meeting-label"/> 
            </td>
            <td class="RightColumn">
            	<asp:Label id="lblPersonName" runat="server" class="meeting-label"></asp:Label><br/>
     </td>
        </tr>
		<tr>
			<td class="LeftColumn"> <asp:Label id="lblCompany" runat="server" Text="Company:" class="meeting-label"/> </td>
			<td class="RightColumn">
				<asp:Label id="lblCompanyName" runat="server"></asp:Label></td>
		</tr>
		<tr>
			<td class="LeftColumn"><asp:Label id="lblTitle" runat="server" Text="Title:" class="meeting-label"/> </td>
			<td class="RightColumn">
				<asp:Label id="lblPersonTitle" runat="server"></asp:Label></td>
		</tr>
		<tr>
			<td class="LeftColumn"><asp:Label id="lblPersonAddress" runat="server" Text="Address:" class="meeting-label"/> </td>
			<td class="RightColumn">
				<asp:Label id="lblAddress" runat="server"></asp:Label></td>
		</tr>
		<tr>
			<td class="LeftColumn"><asp:Label id="lblPersonEmail" runat="server" Text="Email:" class="meeting-label"/> </td>
			<td class="RightColumn">
				<asp:Label id="lblEmail" runat="server"></asp:Label></td>
		</tr>
		<tr>
			<td class="LeftColumn"><asp:Label id="lblPersonPhone" runat="server" Text="Phone:" class="meeting-label"/> </td>
			<td class="RightColumn">
				<asp:Label id="lblPhone" runat="server"></asp:Label></td>
		</tr>
		<tr>
			<td class="LeftColumn"><asp:Label id="lblPersonFax" runat="server" Text="Fax:" class="meeting-label"/> </td>
			<td class="RightColumn">
				<asp:Label id="lblFax" runat="server"></asp:Label></td>
		</tr>
	</table>
</div>