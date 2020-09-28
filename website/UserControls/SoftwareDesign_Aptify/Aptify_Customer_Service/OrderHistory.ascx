<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Customer_Service/OrderHistory.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CustomerService.OrderHistoryControl" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="content-container clearfix cai-table order-history" id="divTop" runat="server">
<p>
  <asp:Button ID="btnPrint" runat="server" Visible="false" Text="Get Statement"   />
	
</p>
&nbsp;
&nbsp;
    <div style="display:none;">
        <asp:Label ID="Label1" runat="server"><b>Order Type</b></asp:Label>
        <asp:DropDownList ID="cmbOrderType" runat="server" AutoPostBack="True" CssClass="marg-btm-20px">
            <asp:ListItem Value="-1">All</asp:ListItem>
            <asp:ListItem Value="1">Regular</asp:ListItem>
            <asp:ListItem Value="4">Quotations</asp:ListItem>
            <asp:ListItem Value="3">Cancellations</asp:ListItem>
        </asp:DropDownList>
    </div>
    <p>
        <%--Navin Prasad Issue 11032--%>
        <%--Update Panel added by Suvarna D IssueID: 12436 on Dec 1, 2011 --%>
        <asp:UpdatePanel ID="updPanelGrid" runat="server" CssClass="order-history cart-grid">
            <ContentTemplate>
                <%--Suraj issue 14450 2/7/13  removed three step sorting ,added tooltip and added date column--%>
                <rad:RadGrid ID="grdMain" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                    SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                    AllowFilteringByColumn="true" CssClass="order-history" OnItemCommand="grdMain_ItemCommand">
                    <PagerStyle CssClass="sd-pager" />
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView AllowFilteringByColumn="false" AllowSorting="true" AllowNaturalSort="false">
                        <%-- 'Suraj Issue 15287 4/9/13, if the grid dont have any record then grid should visible and it should show "No recors " msg--%>
                        <NoRecordsTemplate>
                            No Order History Available.
                        </NoRecordsTemplate>
                        <SortExpressions>
                            <telerik:GridSortExpression FieldName="ID" SortOrder="Descending" />
                        </SortExpressions>

                        <Columns>
                            <rad:GridHyperLinkColumn DataNavigateUrlFields="ID" DataTextField="ID"
                                FilterControlWidth="" HeaderText="Order #" SortExpression="ID" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo" ShowFilterIcon="false" DataTextFormatString="{0}" UniqueName="OrderID"  Visible="false"/>

 <telerik:GridBoundColumn DataField="ID" HeaderText="Order #" SortExpression="ID" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"  />
                            <telerik:GridDateTimeColumn DataField="OrderDate" DataType="System.DateTime" UniqueName="GridDateTimeColumnOrderDate" Visible="false">
                            </telerik:GridDateTimeColumn>

                            <rad:GridDateTimeColumn DataField="OrderDate"
                                HeaderText="Date" FilterControlWidth="" HeaderStyle-Width="170px" HeaderStyle-Height="100%" SortExpression="OrderDate"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" DataType="System.DateTime"
                                ShowFilterIcon="false" EnableTimeIndependentFiltering="true" FilterListOptions="VaryByDataType" 
                                DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />

                            <rad:GridTemplateColumn DataField="OrderStatus" HeaderText="Shipping Status" SortExpression="OrderStatus"
                                FilterControlWidth="" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text="Order #:" CssClass="mobile-label"></asp:Label>
                                    <a class="cai-table-data no-desktop" href="<%= Me.OrderConfirmationURL + "?ID=" %><%# DataBinder.Eval(Container.DataItem, "ID")%>"><%# DataBinder.Eval(Container.DataItem, "ID")%></a>

                                    <asp:Label runat="server" Text="Order Date:" CssClass="mobile-label"></asp:Label>
                                    <asp:Label class="cai-table-data no-desktop" ID="OrderDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDate", "{0:MMMM d, yyyy}")%>'></asp:Label>

                                    <asp:Label runat="server" Text="Shipping Status:" CssClass="mobile-label" Visible="false"></asp:Label>
                                    <asp:Label class="cai-table-data no-mob" ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderStatus")%>' Visible="false"/>

                                    <asp:Label runat="server" Text="Total:" CssClass="mobile-label"></asp:Label>
                                    <asp:Label class="cai-table-data no-mob" ID="Label2" runat="server" Text='<%# String.Format("{0} {1}", DataBinder.Eval(Container.DataItem, "CurrencyType"), DataBinder.Eval(Container.DataItem, "CALC_GrandTotal"))%>' />



                                </ItemTemplate>
                            </rad:GridTemplateColumn>

                            <rad:GridCalculatedColumn HeaderText="Total" DataType="System.String"
                                DataFields="CurrencyType, CALC_GrandTotal" SortExpression="CALC_GrandTotal" CurrentFilterFunction="EqualTo"
                                Expression='{0} + " " + {1}' FilterControlWidth="" AutoPostBackOnFilter="true"
                                ShowFilterIcon="false" >
                            </rad:GridCalculatedColumn>
			<rad:GridButtonColumn HeaderText="Invoice report" Text="Invoice report" UniqueName="Invoicereport" CommandName="Invoicereport"></rad:GridButtonColumn>
                             <rad:GridButtonColumn HeaderText="Payment receipt" Text="Payment receipt" UniqueName="Paymentreceipt" CommandName="Paymentreceipt"></rad:GridButtonColumn>
                        </Columns>
                    </MasterTableView>
                </rad:RadGrid>
            </ContentTemplate>
        </asp:UpdatePanel>
    </p>

    <cc1:User runat="server" ID="User1" />
</div>
