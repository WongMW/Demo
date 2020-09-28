<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Product_Catalog/ShippingControl.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.ShippingControl"%>
<%@ Register TagPrefix="uc1" TagName="CartGrid" Src="CartGrid.ascx" %>
<%@ Register TagPrefix="uc2" TagName="NameAddressBlock" Src="../Aptify_General/NameAddressBlock.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%--Nalini Issue#12578--%>
<div class="shipping-details">
    <div class="tdbgcolorshipping">
        <strong>Shipping details</strong>
    </div>
    <!-- Changes made for Issue 5131, Changes made by Vijay Sitlani on 11-22-2007-->
    <%--Nalini issue 12578--%>
    <div>
        <strong>Shipping to:</strong>
    </div>
    <!-- Changes made for Issue 5131, Changes made by Vijay Sitlani on 11-22-2007-->
    <div class="address-block">
        <uc2:NameAddressBlock ID="NameAddressBlock" runat="server"></uc2:NameAddressBlock>

        <div>
             <br /> <br />
            <asp:Button ID="lnkChangeAddress" runat="server" Text="Change Address" CssClass="submitBtn" />
        </div>
        <div>
            
        </div>
    </div>

    <%-- <div class="tdchangebuttonshipping">
           
        </div>--%>
</div>
<cc1:AptifyShoppingCart runat="Server" ID="ShoppingCart1" />
<cc2:User ID="User1" runat="server" />