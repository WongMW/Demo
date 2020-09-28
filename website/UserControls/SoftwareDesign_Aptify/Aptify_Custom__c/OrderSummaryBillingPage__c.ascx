<%--Aptify e-Business 5.5.1, July 2013--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/OrderSummaryBillingPage__c.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.OrderSummaryBillingControl"%>
<%@ Register TagPrefix="uc1" TagName="CartGrid" Src="../Aptify_Product_Catalog/CartGrid.ascx" %>
<%@ Register TagPrefix="uc2" TagName="NameAddressBlock" Src="../Aptify_General/NameAddressBlock.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<Script language="javascript" type="text/javascript">
    window.history.forward(1);
</Script>

<%--Nalini Issue#12578--%>
<div id="tblMain" runat="server"  >
     <div class="tdbgcolorshipping">
        <span class="billing-label">Order summary</span>
    </div>
    <div>
	    <span  >Sub-total:</span>
	    <span class="cai-table-data right" ><asp:label id="lblSubTotal" runat="server"></asp:label></span>       
    </div>
    <div>
	    <span  >Shipping &amp; handling:</span>
	    <span class="cai-table-data right" ><asp:label id="lblShipping" Runat="server"></asp:label></span>       
    </div>
    <div>
    <%-- Changes by Ganesh I on 24/03/2014 --%>
	    <span   >VAT:</span>
	    <span  class="cai-table-data right" ><asp:label id="lblTax" Runat="server"></asp:label></span>        
    </div>
    <div>
	    <span  >Total:</span>
        <span  class="cai-table-data right" ><asp:label id="lblTotal" Runat="server" Font-Size="X-Large"></asp:label></span>
    </div>
</div>
 <cc1:AptifyShoppingCart runat="Server" ID="ShoppingCart1" />
<cc2:User runat="server" ID="User1" />
