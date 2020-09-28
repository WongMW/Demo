<%@ control language="vb" autoeventwireup="false" codefile="~/UserControls/Aptify_Product_Catalog/itemsincart.ascx.vb" inherits="Aptify.Framework.Web.eBusiness.itemsincart" %> 

<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %> 

<div>
<table id = "tblItemcount" runat ="server" border ="0" width="100%" class="cartButtonHolder">
    <tr align ="right">    
        <td>
        <asp:Label ID="lblViewcart" runat = "server" Text = "View Cart" CssClass="HeaderFontStyle2"></asp:Label>
        <img id="imgCart" src=""  runat="server" />
        
        <asp:Label ID="lblItemsInCart" runat="server" Text="Label" CssClass="HeaderFontStyle1"></asp:Label>
        
        </td>
    </tr>
</table>

<cc2:AptifyShoppingCart id="ShoppingCart1" runat="server"></cc2:AptifyShoppingCart>
</div>