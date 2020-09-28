<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SaveCart.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.SaveCartControl"  %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
    <style type="text/css">
        .style1
        {
            height: 61px;
        }
    </style>
    <table id="tblMain" runat="server" width="100%">
	    <tr id="trInfo" runat="server">
	        <td colspan="2">       
            <asp:Label ID="lblInfo" runat="server">Enter a name and description below and the cart will be saved for your future convenience.</asp:Label>
		    </td>
	    </tr>
	    <tr id="trResult" runat="server">
	        <td colspan="2">       
                <asp:Label ID="lblResult" runat="server" Visible="False" Font-Bold="True"></asp:Label>
            </td>
	    </tr>
	    <tr id="trName" runat="server">
        <td valign="top" align="left" width="10%">
            <asp:Label ID="lblName" runat="server"><b>Cart Name:</b></asp:Label>
		    </td>
        <td>
            <asp:TextBox ID="txtName" runat="server" TextMode="SingleLine" Width="370px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Name Is Required"
                ControlToValidate="txtName" ForeColor="Red"></asp:RequiredFieldValidator>
		    </td>
	    </tr>
    <tr style="width:1px;">
        <td style="width:1px;">
           &nbsp;
        </td>
        
    </tr>
	    <tr id="trDescription" runat="server">
        <td valign="top" align="left" class="style1">
            <asp:Label Width="25%" ID="lblDescription" runat="server"><b>Description:</b>
                </asp:Label>
		    </td>
        <td valign="top" class="style1">
            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="372px"
                Height="70px" Style="margin-bottom: 10px; font-family: Segoe UI, Regular;"></asp:TextBox>
        </td>
	    </tr>
	    <tr id="trSave" runat="server">
        <td>
        </td>
        <td>
            <asp:Button ID="cmdSaveCurrent" runat="server" Text="Update Current Cart" Visible="False"
               ></asp:Button>
                <asp:Button ID="cmdSave" runat="server" Text="Save As New Cart" Visible="False" />
            <asp:Button ID="cmdCancel" runat="server" Text="Cancel" CausesValidation="false" />
            </td>
	    </tr>
    </table>
    <cc1:AptifyShoppingCart runat="server" ID="ShoppingCart1" />

