<%@ Control Language="VB" AutoEventWireup="false" CodeFile="FeaturedProducts.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.FeaturedProductsControl" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<div style="padding-bottom:10px;">
<h6 id="H1" runat="server" class="textfontsub">Featured Products</h6>
</div>
<div id="divGrid" runat="server" class="DivGrid clearfix" >
    <table class="data-form">
        <tr>
            <td valign="top" style="font-size:12px; padding-top:5px;">
                The products shown below have been specially selected for you based on your areas
                of interest and previous purchases.
            </td>
        </tr>
        <tr>
            <td>
            <asp:DataList ID="grdFeaturedProducts" runat="server" HorizontalAlign="Left" >
                <ItemTemplate>
                    <table width="100%">
                        <tr>
                            <td valign="middle" align="center" style="height:100px; width:20%;">
                                <asp:Image ID="ImgProd" runat="server"  CssClass="Image"  />
                             </td>
                             <%--Aparna add Literal tag  for showing data in proper format--%>
                             <%--<td style="width:5%;"></td>--%>
                             <td align="left"  valign="top" style="width:50%;" >
                             <asp:HyperLink ID="lnkName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Name") %>' Font-Bold="true"></asp:HyperLink>
                             <br />

                           <asp:Literal ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Description")%>'></asp:Literal>
                             </td>
                        </tr>
                    </table>
                         </ItemTemplate>
                          <%-- Commented by Suvarna to remove View All Link from page
                           <FooterTemplate>
                         <asp:Label ID = "lblViewAll" runat="server" Text="View All" ></asp:Label>
                         <a href="#"><img id="imgViewAll" alt="" src="~/Images/view-all-icon-hover.png" runat="server" /></a>
                         </FooterTemplate>
                         <FooterStyle HorizontalAlign="Right" ForeColor="#71582d" />--%>

            </asp:DataList>

            

          <%--  Navin Prasad Issue 11032--%>
                <%--<asp:GridView ID="grdFeaturedProducts" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="Product">
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Name") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                    </Columns>
                </asp:GridView>--%>


                <%--<asp:DataGrid ID="grdFeaturedProducts" runat="server" AutoGenerateColumns="True">
                    <Columns>
                        <asp:HyperLinkColumn DataNavigateUrlField="ID" DataTextField="Name" HeaderText="Product">
                        </asp:HyperLinkColumn>
                        <asp:BoundColumn DataField="Description" HeaderText="Description"></asp:BoundColumn>
                    </Columns>
                    <PagerStyle Mode="NumericPages"></PagerStyle>
                </asp:DataGrid>--%>
            </td>
        </tr>
    </table>
    <cc1:User runat="server" ID="User1" />
</div>
<div id="noData" runat="server" class="content-container clearfix">
    <table class="data-form">
        <tr>
            <td style="font-weight: bold">
                From time to time, products will be listed here based on your areas of interest
                and past purchases.
            </td>
        </tr>
    </table>
</div>
