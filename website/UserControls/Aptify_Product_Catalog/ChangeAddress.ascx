<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ChangeAddress.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.ChangeAddressControl" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<div class="content-container clearfix">
    <table id="tblMain" runat="server" class="data-form">
        <tr>
            <td>
                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                <p>
                    <asp:HyperLink ID="lnkNewAddress" runat="server">Click here to add a new address</asp:HyperLink></p>
                <p>
                    Choose a
                    <asp:Label ID="lblType" runat="server">Shipping/Billing</asp:Label>&nbsp;Address
                </p>
            </td>
        </tr>
        <tr>
        <%--Nalini Issue#13102--%>
            <td style="vertical-align:bottom;">
                <asp:DataList ID="lstAddress" runat="server" RepeatColumns="2" Width="100%">
                    <SelectedItemStyle></SelectedItemStyle>
                    <HeaderTemplate>
                        <font class="tdchangeAddressBackground">Address Book</font>
                    </HeaderTemplate>
                    <AlternatingItemStyle></AlternatingItemStyle>
                    <ItemStyle Width="350px" VerticalAlign="Bottom" HorizontalAlign="NotSet"></ItemStyle>
                    <ItemTemplate>
                        <div style="vertical-align:bottom;" class="divstylechangeAddress">
                        <font size="2">
                                <b><%# DataBinder.Eval(Container.DataItem, "Type")%></b><br />
                            <%# DataBinder.Eval(Container.DataItem,"AddressLine1") %>
                            <%# DataBinder.Eval(Container.DataItem,"AddressLine2") %>
                            <%# DataBinder.Eval(Container.DataItem,"AddressLine3") %>
                                <%# DataBinder.Eval(Container.DataItem,"City") %>&nbsp;&nbsp;<%# DataBinder.Eval(Container.DataItem,"State") %>&nbsp;
                            <%# DataBinder.Eval(Container.DataItem,"ZipCode") %>
                            <br>
                            <%# DataBinder.Eval(Container.DataItem,"Country") %>
                        </font>
                        
                       
                        <%--Nalini Issue#12578--%>
                        </div>
                        <div>
                        <asp:Button ID="cmdUseAddress" AlternateText="Use this address" runat="server" CssClass="cmdbutton" Text="Use this Address" />
                        <asp:Button ID="cmdEditAddress" AlternateText="Edit Address" runat="server" Text="Edit"  CssClass="cmdbutton"/>
                        </div>
                    </ItemTemplate>
                    <FooterStyle></FooterStyle>
                    <HeaderStyle></HeaderStyle>
                </asp:DataList>
            </td>
        </tr>
    </table>
    <cc2:User runat="Server" ID="User1" />
    <cc1:AptifyShoppingCart runat="server" ID="ShoppingCart1" />
</div>
