<%@ Control Language="VB" AutoEventWireup="false" CodeFile="OpenCart.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.CustomerService.OpenCartControl" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="content-container clearfix">
    <table id="tblMain" runat="server" width="100%">
        <tr>
            <td>
                <asp:UpdatePanel ID="updPanelGrid" runat="server">
                    <ContentTemplate>
                        <%--Suraj issue 14450 2/7/13  removed three step sorting ,added tooltip --%>
                        <rad:RadGrid ID="grdSavedCarts" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                            SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                            AllowFilteringByColumn="true">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AllowSorting="true" AllowFilteringByColumn="true" AllowNaturalSort="false">
               <%-- 'Suraj Issue 15287 4/9/13, if the grid dont have any record then grid should visible and it should show "No recors " msg--%>
                                <NoRecordsTemplate>
                                    No Cart Available.
                                </NoRecordsTemplate>
                                <Columns>
                                    <rad:GridTemplateColumn HeaderText="Name" DataField="Name" SortExpression="Name"
                                        FilterControlWidth="80%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Name") %>'
                                                NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"ShoppingCartUrl") %>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <HeaderStyle Width="25%" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                        <ItemStyle Width="25%" Wrap="True" />
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Description" DataField="Description" SortExpression=""
                                        FilterControlWidth="80%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblDescription" Text='<%# DataBinder.Eval(Container.DataItem,"Description") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="75%" />
                                        <ItemStyle Width="25%" Wrap="True" />
                                    </rad:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </rad:RadGrid>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
    <cc1:User runat="server" ID="User1" />
    <cc3:AptifyShoppingCart runat="server" ID="ShoppingCart1" />
</div>
