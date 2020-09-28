<%@ Control Language="VB" AutoEventWireup="false" CodeFile="OrderSummary.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.OrderSummaryControl"%>
<%@ Register TagPrefix="uc1" TagName="CartGrid" Src="CartGrid.ascx" %>
<%@ Register TagPrefix="uc2" TagName="NameAddressBlock" Src="../Aptify_General/NameAddressBlock.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<Script language="javascript" type="text/javascript">
    window.history.forward(1);
</Script>

<%--Nalini Issue#12578--%>
<table id="tblMain" runat="server" width="100%" class="data-form">
    <tr>
	    <td class="LeftColumn">Sub-Total:</td>
	    <td class="rightplacetd" ><asp:label id="lblSubTotal" runat="server"></asp:label></td>
        <td>
        </td>
    </tr>
    <tr>
	    <td class="LeftColumn">Shipping &amp; Handling:</td>
	    <td class="rightplacetd" ><asp:label id="lblShipping" Runat="server"></asp:label></td>
        <td>
        </td>
    </tr>
    <tr>
	    <td  class="LeftColumn">Tax:</td>
	    <td  class="rightplacetd" ><asp:label id="lblTax" Runat="server"></asp:label></td>
        
    </tr>
    <tr>
	    <td  class="LeftColumn">Total:</td>
        <td  class="rightplacetd" ><asp:label id="lblTotal" Runat="server"></asp:label></td>
    </tr>
</table>
 <cc1:AptifyShoppingCart runat="Server" ID="ShoppingCart1" />

