<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/CartGrid__c.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.CartGrid__c" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%--Nalini Issue 12436 date:01/12/2011--%>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <%--Aparna issue no. 9142 for showing message for meeting--%>
        <div style="min-height: 25px">
            <asp:Image ID="imgwarning" runat="server" Visible="false" ImageUrl="~/Images/warning.png" />
            <asp:Label ID="lblshowmessage" Font-Bold="true" Visible="false" Text="If you remove the main meeting, all associated sessions will be removed." runat="server" ForeColor="black"></asp:Label>
        </div>
        <table id="tblCart" runat="server" width="100%" class="data-form center cart-grid">
            <tr>
                <td>
                    <rad:RadGrid ID="grdMain" runat="server" AutoGenerateColumns="False">
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
                                        <asp:Label runat="server" Text="Product name:" CssClass="mobile-label"></asp:Label>
                                        <asp:HyperLink runat="server" class="cai-table-data" ID="link" Text='<%# DataBinder.Eval(Container, "DataItem.WebName") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <%--Neha issue no. 14456, removed sorting--%>
                                <%--Susan, change layout to suit mobile too--%>
                                <%--<rad:GridBoundColumn DataField="Description" HeaderText="Item Description" HeaderStyle-Width="300px" AllowSorting="false" />--%>
                                <rad:GridTemplateColumn HeaderText="Product description" DataField="Description">
                                    <HeaderStyle HorizontalAlign="Center" Width="300px" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="Description" CssClass="mobile-label"></asp:Label>
                                        <asp:Label ID="lblDescription" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Description") %>'></asp:Label>
                                    </ItemTemplate>                                   
                                </rad:GridTemplateColumn>

                                <rad:GridTemplateColumn HeaderText="Unit price" DataField="Price">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="Price:" CssClass="mobile-label"></asp:Label>
                                        <asp:Label ID="lblPrice" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Price") %>'></asp:Label>
                                    </ItemTemplate>                                   
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Quantity" DataField="Quantity">                                    
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="Quantity:" CssClass="mobile-label"></asp:Label>
                                        <asp:TextBox ID="txtQuantity" CssClass="cai-table-data" style="width:50px;text-align:center;" Text='<%# DataBinder.Eval(Container.DataItem,"Quantity")%>'
                                            runat="server" Enabled='<%#GetRowQuantityEnabled(Container)%>'>
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Subtotal" DataField="Extended">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="Subtotal:" CssClass="mobile-label"></asp:Label>
                                        <asp:Label ID="lblExtended" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Extended") %>'></asp:Label>
                                    </ItemTemplate>                                  
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Auto Renew" Visible="false">                                   
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="Remove:" CssClass="mobile-label"></asp:Label>
                                        <asp:CheckBox runat="server" CssClass="cai-table-data" ID="chkRenew"></asp:CheckBox>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Details" Visible="false">
                                    <HeaderStyle/>
                                    <ItemStyle />
                                    <ItemTemplate>
                                        <%-- CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>"
		                                            <asp:LinkButton ID="cmdDetails" Text='Details...' runat="server" Visible='<%#DataBinder.Eval(Container.DataItem,"_x_HasExtendedDetails")%>' CausesValidation="false" 
		                                                CommandName="Detail" ></asp:LinkButton>--%>
                                        <%--    Amruta Issue 14949 16/11/2012 --%>
                                        <asp:HyperLink ID="hlnkDetails" Text='Details...' runat="server" Visible='<%#DataBinder.Eval(Container.DataItem,"_x_HasExtendedDetails")%>'></asp:HyperLink>
                                        <asp:HiddenField ID="hdnRowCount" runat="server" />
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Remove">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <HeaderStyle/>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="Remove:" CssClass="mobile-label"></asp:Label>
                                        <asp:CheckBox runat="server" ID="chkRemove" Visible='<%#DataBinder.Eval(Container.DataItem,"ParentSequence")<=0%>' AutoPostBack="true" CssClass="cai-table-data" OnCheckedChanged="chkRemove_CheckedChanged"></asp:CheckBox>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <%-- 'Anil B Issue 15441 on 13 May 2013
                                    'Bind product type and product parent id--%>
                                <rad:GridTemplateColumn HeaderText="" Visible="false" ItemStyle-CssClass="order-now">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProductType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ProductType") %>'></asp:Label>
                                        <asp:Label ID="lblProductParentID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ParentID") %>'></asp:Label>
                                        <asp:HiddenField runat="server" ID="hdAttendeeID" Value='<%#DataBinder.Eval(Container.DataItem,"__ExtendedAttributeObjectData") %>' />
                                        <asp:HiddenField runat="server" ID="hdAttendeeIDSession" Value='<%#DataBinder.Eval(Container.DataItem,"_xCalculatedPriceName") %>' />
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
        </table>
    </ContentTemplate>
    <%--  <Triggers>
        <asp:AsyncPostBackTrigger ControlID="grdMain" EventName="PageIndexChanging" />
        <asp:AsyncPostBackTrigger ControlID="grdMain" EventName="RowCommand" />
    </Triggers>--%>
</asp:UpdatePanel>
<cc3:User runat="Server" ID="User1" />
<cc2:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="false"></cc2:AptifyShoppingCart>
