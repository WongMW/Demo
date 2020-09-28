<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CartGrid.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.CartGrid" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%--Nalini Issue 12436 date:01/12/2011--%>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <%--Aparna issue no. 9142 for showing message for meeting--%>
        <div style="min-height: 25px">
            <asp:Image ID="imgwarning" runat="server" Visible="false" ImageUrl="~/Images/warning.png" />
            <asp:Label ID="lblshowmessage" Font-Bold="true" Visible="false" Text="If you remove the main meeting, all associated sessions will be removed." runat="server" ForeColor="black"></asp:Label>    
        </div>
        <table id="tblCart" runat="server" width="100%" class="data-form">
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
                                <rad:GridTemplateColumn HeaderText="Remove">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="left" Width="50px" />
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkRemove" Visible='<%#DataBinder.Eval(Container.DataItem,"ParentSequence")<=0%>' AutoPostBack="true" OnCheckedChanged="chkRemove_CheckedChanged">
                                        </asp:CheckBox>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                               <%--  'Anil B for issue 15341 on 20-03-2013
                                'Set Product name as Web Name--%>
                                <rad:GridTemplateColumn HeaderText="Product" DataField="WebName">
                                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                    <ItemTemplate>
                                        <b>
                                            <asp:HyperLink runat="server" ID="link" Text='<%# DataBinder.Eval(Container, "DataItem.WebName") %>'></asp:HyperLink></b>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <%--Neha issue no. 14456, removed sorting--%>
                                <rad:GridBoundColumn DataField="Description" HeaderText="Item Description" HeaderStyle-Width="200px" AllowSorting="false"/>
                                <rad:GridTemplateColumn HeaderText="Unit Price" DataField="Price">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Price") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="70px" />
                                    <HeaderStyle HorizontalAlign="Right" Width="70px" />
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Quantity" DataField="Quantity">
                                    <HeaderStyle HorizontalAlign="Left" Width="70px" />
                                    <ItemStyle HorizontalAlign="Left" Width="70px" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtQuantity" Style="text-align: right;" Width="50" Text='<%# DataBinder.Eval(Container.DataItem,"Quantity")%>'
                                            runat="server" Enabled='<%#GetRowQuantityEnabled(Container)%>'>
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Total Price" DataField="Extended">
                                    <ItemTemplate>
                                        <asp:Label ID="lblExtended" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Extended") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" CssClass="griditempaddingRight" Width="90px" />
                                    <HeaderStyle HorizontalAlign="Right" Width="90px" />
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Auto Renew" Visible="false">
                                    <ItemStyle HorizontalAlign="Center" Width="90px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="left" Width="90px" />
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkRenew">
                                        </asp:CheckBox>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Details">
                                    <HeaderStyle HorizontalAlign="Right" Width="60px" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <%-- CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>"
		                                            <asp:LinkButton ID="cmdDetails" Text='Details...' runat="server" Visible='<%#DataBinder.Eval(Container.DataItem,"_x_HasExtendedDetails")%>' CausesValidation="false" 
		                                                CommandName="Detail" ></asp:LinkButton>--%>
                                        <%--    Amruta Issue 14949 16/11/2012 --%>
                                        <asp:HyperLink ID="hlnkDetails" Text='Details...' runat="server" Visible='<%#DataBinder.Eval(Container.DataItem,"_x_HasExtendedDetails")%>'></asp:HyperLink>
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
<cc2:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="false"></cc2:AptifyShoppingCart>
