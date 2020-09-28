<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DynamicProductsGridSmall.ascx.vb" Inherits="DynamicProductsGrid" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<div class="content-container clearfix">
    <asp:label runat="server" ID="lblError" /><br />
    <asp:DataList ID="DataList1" runat="server" >
        <HeaderTemplate>
            <asp:Label runat="server" ID="lblHeader"></asp:Label>
        </HeaderTemplate>
        <ItemTemplate>
            <table>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td><a href='<%# DataBinder.Eval(Container.DataItem,"ProdPageURL") %>' ><img alt="ImageUrl" id="imgProduct" runat="server" src='<%# DataBinder.Eval(Container.DataItem,"ProdImageURL") %>'  /></a></td>
                        </tr>
                    </table>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td><a href='<%# DataBinder.Eval(Container.DataItem,"ProdPageURL") %>' ><asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Name") %>'></asp:Label></a></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblProdName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"GridDescription") %>'></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            </table>
        </ItemTemplate>
        <FooterTemplate>
        </FooterTemplate>
    </asp:DataList>
    <cc1:User runat="server" ID="User1" />
</div>