<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Product_Catalog/CartGrid.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.CartGrid" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<%--Nalini Issue 12436 date:01/12/2011--%>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <%--Aparna issue no. 9142 for showing message for meeting--%>
        <div>
            <asp:Image ID="imgwarning" runat="server" Visible="false" ImageUrl="~/Images/warning.png" />
            <asp:Label ID="lblshowmessage" Font-Bold="true" Visible="false" Text="If you remove the main meeting, all associated sessions will be removed." runat="server" ForeColor="black"></asp:Label>
        </div>
        <div id="tblCart" runat="server" width="100%">
            <tr>
                <td>
                    <rad:RadGrid ID="grdMain" runat="server" AutoGenerateColumns="False" CssClass="cart-grid">
                        <MasterTableView>
                            <Columns>
                                <rad:GridTemplateColumn Visible="False" HeaderText="Product ID" DataField="ProductID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProductID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ProductID") %>' />
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <%--  'Anil B for issue 15341 on 20-03-2013
                                'Set Product name as Web Name--%>
                                <rad:GridTemplateColumn HeaderText="Product name" DataField="WebName">
                                    <HeaderStyle HorizontalAlign="Center" Width="300px" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="Product:" CssClass="cartFieldLabel"></asp:Label><asp:HyperLink runat="server" CssClass="cai-table-data" ID="link" Text='<%# DataBinder.Eval(Container, "DataItem.WebName")%>'></asp:HyperLink>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <%--Neha issue no. 14456, removed sorting--%>
                                <rad:GridBoundColumn DataField="Description" HeaderText="Product description"  HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob"  HeaderStyle-Width="300px" AllowSorting="false" />
                                <rad:GridTemplateColumn HeaderText="Unit price" DataField="Price">
                                    <ItemTemplate>
                                          <asp:Label runat="server" Text="Item Description:" CssClass="mobile-label"></asp:Label>
                                        <asp:Label ID="Label1" CssClass="cai-table-data no-desktop" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Description") %>'></asp:Label>
                                        <asp:Label runat="server" Text="Price:" CssClass="cartFieldLabel"></asp:Label>
                                        <asp:Label ID="lblPrice" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Price") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle />
                                    <HeaderStyle />
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Quantity" DataField="Quantity">
                                    <HeaderStyle />
                                    <ItemStyle  />
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="QTY:" CssClass="cartFieldLabel"></asp:Label>
                                        <asp:TextBox ID="txtQuantity" CssClass="cai-table-data" Width="50px"  style="text-align: center" Text='<%# DataBinder.Eval(Container.DataItem,"Quantity")%>'
                                            runat="server" Enabled='<%#GetRowQuantityEnabled(Container)%>'>
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Total price" DataField="Extended">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="Total price:" CssClass="cartFieldLabel"></asp:Label>
                                        <asp:Label ID="lblExtended" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Extended") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="griditempaddingRight"  />
                                    <HeaderStyle  />
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Auto renew" Visible="false">
                                    <ItemStyle HorizontalAlign="Center" ></ItemStyle>
                                    <HeaderStyle />
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" CssClass="" ID="chkRenew"></asp:CheckBox>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Details" Visible="false">
                                    <HeaderStyle Width="60px" />
                                    <ItemStyle />
                                    <ItemTemplate>

                                        <%-- CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>"
		                                            <asp:LinkButton ID="cmdDetails" Text='Details...' runat="server" Visible='<%#DataBinder.Eval(Container.DataItem,"_x_HasExtendedDetails")%>' CausesValidation="false" 
		                                                CommandName="Detail" ></asp:LinkButton>--%>
                                        <%--    Amruta Issue 14949 16/11/2012 --%>
                                            <asp:Label runat="server" Text="Details:" CssClass="cartFieldLabel"></asp:Label>
                                            <asp:HyperLink ID="hlnkDetails" Text='Details...' CssClass="cai-table-data" runat="server" Visible='<%#DataBinder.Eval(Container.DataItem,"_x_HasExtendedDetails")%>'></asp:HyperLink>
                                            <asp:HiddenField ID="hdnRowCount" runat="server" />
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <%-- 'Anil B Issue 15441 on 13 May 2013
                                    'Bind product type and product parent id--%>
                                <rad:GridTemplateColumn HeaderText="" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProductType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ProductType") %>'></asp:Label>
                                        <asp:Label ID="lblProductParentID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ParentID") %>'></asp:Label>
                                        <asp:HiddenField runat="server" ID="hdAttendeeID" Value='<%#DataBinder.Eval(Container.DataItem,"__ExtendedAttributeObjectData") %>' />
                                        <asp:HiddenField runat="server" ID="hdAttendeeIDSession" Value='<%#DataBinder.Eval(Container.DataItem,"_xCalculatedPriceName") %>' />
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Remove">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <HeaderStyle />
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="Remove:" CssClass="cartFieldLabel"></asp:Label><asp:CheckBox runat="server" ID="chkRemove" Visible='<%#DataBinder.Eval(Container.DataItem,"ParentSequence")<=0%>' AutoPostBack="true" CssClass="remove-checkbox cai-table-data" OnCheckedChanged="chkRemove_CheckedChanged"></asp:CheckBox>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </rad:RadGrid>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
        </div>
    </ContentTemplate>
    <%--  <Triggers>
        <asp:AsyncPostBackTrigger ControlID="grdMain" EventName="PageIndexChanging" />
        <asp:AsyncPostBackTrigger ControlID="grdMain" EventName="RowCommand" />
    </Triggers>--%>
</asp:UpdatePanel>
<cc2:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="false"></cc2:AptifyShoppingCart>
