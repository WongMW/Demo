<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Product_Catalog/FindProduct.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.FindProductControl" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div>
    <%--Nalini Issue 12436 date:01/12/2011--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlmain" runat="server" DefaultButton="cmdSearch">
                <table id="tblMain" runat="server" class="data-form" border="0">
                    <tr>
                        <td valign="middle">
                            <asp:TextBox ID="txtName" runat="server" Width="220px" />
                            <asp:TextBox ID="txtDescription" runat="server" Visible="false" />
                        </td>
                        <td valign="middle">
                            <asp:DropDownList runat="server" ID="cmbCategory" DataTextField="WebName" DataValueField="ID"
                                Width="180px">
                            </asp:DropDownList>
                        </td>
                        <td valign="middle">
                            <asp:Button CssClass="submitBtn" ID="cmdSearch" runat="server" Text="Find Products"></asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr runat="server" id="trNoResults">
                        <td colspan="3">The system could not locate any matching records. Please try again.
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <div id="trResults" runat="server">
                <%--Neha Changes for Issue 14456--%>
                <rad:RadGrid ID="grdResults" runat="server" AutoGenerateColumns="False" AllowPaging="true" AllowFilteringByColumn="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending">
                    <GroupingSettings CaseSensitive="false" />
                    <PagerStyle CssClass="sd-pager" />
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                        <Columns>
                            <rad:GridTemplateColumn HeaderText="Product" DataField="WebName" FilterListOptions="VaryByDataType" SortExpression="WebName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkWebName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebName") %>'
                                        NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"ViewProductPageUrl") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle Width="30%" />
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Description" DataField="WebDescription" FilterListOptions="VaryByDataType" SortExpression="" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%">
                                <ItemTemplate>
                                    <asp:Literal ID="ltDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebDescription") %>'></asp:Literal>
                                </ItemTemplate>
                                <ItemStyle Width="40%" />
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Category" DataField="Category" FilterListOptions="VaryByDataType" SortExpression="Category" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkCategory" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Category") %>'
                                        NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"ViewProductCatagoryPageUrl") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle Width="30%" />
                            </rad:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </rad:RadGrid>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
