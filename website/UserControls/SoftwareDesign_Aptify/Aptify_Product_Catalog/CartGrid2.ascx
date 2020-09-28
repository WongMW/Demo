<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Product_Catalog/CartGrid2.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.CartGrid2" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%--Nalini Issue 12436 date:01/12/2011--%>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table id="tblCart" runat="server" width="100%" class="data-form cart-grid cai-table center">
            <tr>
                <td>
                    <%--Navin Prasad Issue 11032--%>
                    <rad:RadGrid ID="grdMain" runat="server" AutoGenerateColumns="False" Width="100%">
                        <MasterTableView>
                            <Columns>
                                <rad:GridTemplateColumn Visible="False" HeaderText="Product ID">
                                    <ItemTemplate>                                       
                                        <asp:Label ID="lblProductID" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ProductID") %>' />
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <%-- Neha Changes for issue 14456--%>
                                <rad:GridTemplateColumn HeaderText="Auto Renew" UniqueName="AutoRenew">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <HeaderStyle  />
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkRenew" Enabled="false"></asp:CheckBox>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Product name">
                                    <ItemTemplate>
                                        <%--  'Anil B for issue 15341 on 20-03-2013
                                            'Set Product name as Web Name--%>
                                         <asp:Label runat="server" Text="Product name:" CssClass="mobile-label"></asp:Label>
                                        <asp:LinkButton runat="server" CssClass="cai-table-data" PostBackUrl="" CausesValidation="false" ID="link"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.WebName") %>' CommandName="Link"></asp:LinkButton>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <%--Neha issue no. 14456,03/15/13, removed sorting--%>
                                <%--Susan, change layout to suit mobile too--%>
                                <%--<rad:GridBoundColumn DataField="Description" HeaderText="Description" AllowSorting="false" />--%>
                                <rad:GridTemplateColumn HeaderText="Product description" DataField="Description">
                                    <HeaderStyle HorizontalAlign="Center" Width="300px" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="Description" CssClass="mobile-label"></asp:Label>
                                        <asp:Label ID="lblDescription" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Description") %>'></asp:Label>
                                    </ItemTemplate>                                   
                                </rad:GridTemplateColumn>

                                <rad:GridTemplateColumn HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="Quantity:" CssClass="mobile-label"></asp:Label>
                                        <asp:Label ID="lblQuantity" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Quantity") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle></HeaderStyle>
                                    <ItemStyle></ItemStyle>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Unit price">
                                    <ItemTemplate>
                                         <asp:Label runat="server" Text="Unit price:" CssClass="mobile-label"></asp:Label>
                                        <asp:Label ID="lblPrice" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Price") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle></HeaderStyle>
                                    <ItemStyle></ItemStyle>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Subtotal">
                                    <ItemTemplate>
                                         <asp:Label runat="server" Text="Subtotal:" CssClass="mobile-label"></asp:Label>
                                        <asp:Label ID="lblExtended" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Extended") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle></HeaderStyle>
                                    <ItemStyle></ItemStyle>
                                </rad:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </rad:RadGrid>
                    <%--<asp:GridView ID="grdMain" runat="server" AutoGenerateColumns="False" Width="100%">
                <Columns>
                    <asp:TemplateField Visible="False" HeaderText="Product ID">
                        <ItemTemplate>
                            <asp:Label ID="lblProductID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ProductID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                       <asp:TemplateField HeaderText="Product">
                        <ItemTemplate>
                                    <b>
                                        <asp:LinkButton runat="server" PostBackUrl="" CausesValidation="false" ID="link"
                                            Text='<%# DataBinder.Eval(Container, "DataItem.Product") %>' CommandName="Link"></asp:LinkButton></b>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                      <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:Label ID="lblQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Quantity") %>'></asp:Label>
                            </ItemTemplate>
                               <HeaderStyle  Width="60px"></HeaderStyle>
                        <ItemStyle ></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit Price">
                            <ItemTemplate>
                                <asp:Label ID="lblPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Price") %>'></asp:Label>
                            </ItemTemplate>
                              <HeaderStyle  Width="60px"></HeaderStyle>
                        <ItemStyle ></ItemStyle>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Total Price">
                            <ItemTemplate>
                                <asp:Label ID="lblExtended" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Extended") %>'></asp:Label>
                            </ItemTemplate>
                                 <HeaderStyle  Width="65px"></HeaderStyle>
                        <ItemStyle ></ItemStyle>
                        </asp:TemplateField>
                </Columns>
                <PagerSettings Mode="Numeric" />
            </asp:GridView>
                    --%>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
        </table>
    </ContentTemplate>
    <%--<Triggers>
        <asp:AsyncPostBackTrigger ControlID="grdMain" EventName="PageIndexChanging" />
         <asp:AsyncPostBackTrigger ControlID="grdMain" EventName="RowCommand" />
    </Triggers>--%>
</asp:UpdatePanel>
<cc2:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="false"></cc2:AptifyShoppingCart>
