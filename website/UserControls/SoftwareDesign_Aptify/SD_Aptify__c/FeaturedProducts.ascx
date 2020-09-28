<%@ Control Language="VB" AutoEventWireup="false" CodeFile="FeaturedProducts.ascx.vb"
    Inherits="SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c.FeaturedProductsControl" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<div>
    <h6 id="H1" runat="server" class="textfontsub featuredTitle">Featured products</h6>
</div>
<div id="divGrid" runat="server" class="DivGrid clearfix featured-products-container">
    <div class="data-form featured-products">
        <asp:DataList ID="grdFeaturedProducts" runat="server" HorizontalAlign="Left">
            <ItemTemplate>
                <asp:Image ID="ImgProd" runat="server" CssClass="Image" />
                <div class="description">
                    <div class="productTitleLink">
                        <asp:Hyperlink ID="lnkName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>' Font-Bold="true" onclick='<%# GetGtmObject(Container.DataItem)  %>'></asp:Hyperlink> 
                        <%-- #21000 <asp:Hyperlink ID="Hyperlink1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Name") %>' Font-Bold="true"
                                        onclick='<%# GetGtmObject(Container.DataItem)  %>' ></asp:Hyperlink> --%>
                    </div>
                    <asp:Literal ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Description")%>' Visible ="false"></asp:Literal>
                    <a runat="server" id="anchorViewProduct" class="btn buyNowBtn" onclick='<%# GetGtmObject(Container.DataItem)  %>'>View product</a>
                    <%-- #21000 <a runat="server" id="a1" href='<%# String.Format(ViewProductPage & "?ID={0}", DataBinder.Eval(Container.DataItem, "ID").ToString)   %>' 
                       onclick='<%# GetGtmObject(Container.DataItem)  %>'
                        class="btn buyNowBtn">View product</a>--%>
                </div>
            </ItemTemplate>
        </asp:DataList>
    </div>
    <cc1:User runat="server" ID="User1" />
</div>
<div id="noData" runat="server" class="content-container clearfix" visible="false">
    <div class="data-form">
        <label style="font-weight: bold">
            From time to time, products will be listed here based on your areas of interest
                and past purchases.
        </label>
    </div>
</div>
<script>
    (function ($) {
        $('.featured-products .description .productTitleLink a').each(function () {
            var text = $(this).text();
            if (text.length > 62) {
                text = text.substr(0, 59);
                text += "...";
                $(this).text(text);
            }
        });

    })(jQuery);
</script>
