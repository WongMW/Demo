<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Product_Catalog/ProductsSoldTogetherGrid.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.ProductsSoldTogether" %>
<asp:Label ID="lblError" runat="server"></asp:Label>

<asp:DataList ID="DataList1" runat="server" GridLines="Horizontal" HorizontalAlign="Left" RepeatColumns="4" RepeatDirection="Horizontal" Width="100%" BorderColor="#FFC080" BorderStyle="Solid" BorderWidth="1px">
<HeaderTemplate>
Continue Shopping: People who bought this also bought...

</HeaderTemplate>
<ItemTemplate>
<table>
    <tr>
        <td align="center" style="width:100px">
            <a href='<%# DataBinder.Eval(Container.DataItem,"ProdPageURL") %>' runat="server" id="lnkRelImage"><img alt="ImageUrl" src='<%# DataBinder.Eval(Container.DataItem,"ProdImageURL") %>'  /></a>
        </td>
    </tr>
    <tr>
        <td align="center" style="width:100px">
            <a href='<%# DataBinder.Eval(Container.DataItem,"ProdPageURL") %>' runat="server" id="lnkRelName"><asp:Label ID="lblProdName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Name") %>'></asp:Label></a> 
        </td>
    </tr>
    <tr>
        <td align="center" style="width:100px">
            <asp:Label ID="lblGridDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"gridDescription") %>'></asp:Label>
        </td>
    </tr>
    
</table>

</ItemTemplate>
<FooterTemplate>

</FooterTemplate>
    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
    <HeaderStyle BackColor="#FFC080" BorderColor="#FFC080" />
</asp:DataList>
