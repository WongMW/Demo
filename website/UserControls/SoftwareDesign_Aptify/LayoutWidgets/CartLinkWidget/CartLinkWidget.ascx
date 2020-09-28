<%@ Control Language="C#" %>
<%@ Register Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Product_Catalog/ItemsInCart.ascx" TagName="ItemsInCart" TagPrefix="uc4" %>


<div runat="server" class="sf_cols">
    <div runat="server" class="sf_colsOut sf_1cols_1_100">
        <div runat="server" class="sf_colsIn sf_1cols_1in_100">
            <div class="cart-link-widget">
                <asp:HyperLink ID="PageLink" runat="server">
                    <i class="fa fa-shopping-cart" id="search-icon"></i>
                    <div class="counter" style="display:none;"></div>
                </asp:HyperLink>
            </div>
        </div>
    </div>
</div>

<uc4:ItemsInCart ID="ItemsInCart1" runat="server" />

<script type="text/javascript">
    jQuery(function ($) {
        $('.cartButtonHolder').hide();
        var text = $('.cartButtonHolder .HeaderFontStyle1 a').text().trim();
        start = text.indexOf('Item');
        var amount = text.substring(0, start-1);
        $('.cart-link-widget .counter').text(amount);

        if (amount !== "0") {
            $('.cart-link-widget .counter').show();
        }
    });
</script>