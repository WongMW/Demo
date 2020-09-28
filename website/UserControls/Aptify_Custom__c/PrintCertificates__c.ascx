<%--Aptify e-Business 5.5.1 SR1, June 2014--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PrintCertificates__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Education.PrintCertificates" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="table-div">
    <div class="row-div">
        <telerik:RadGrid ID="grdsubscription" runat="server" AutoGenerateColumns="False"
            AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
            PagerStyle-PageSizeLabelText="Records Per Page" PageSize="10">
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false"
                EnableNoRecordsTemplate="true" ShowHeadersWhenNoRecords="true" AllowPaging="true"
                PageSize="10">
                <Columns>
                    <telerik:GridBoundColumn DataField="SeqNumber" ItemStyle-Width="10%" FilterControlWidth="50%"
                        HeaderText="Seq. Number" SortExpression="SeqNumber" AutoPostBackOnFilter="false"
                        CurrentFilterFunction="EqualTo" ShowFilterIcon="false" />
                    <telerik:GridBoundColumn DataField="ProductName" ItemStyle-Width="20%" FilterControlWidth="80%"
                        HeaderText="Authorisation Name" SortExpression="ProductName" AutoPostBackOnFilter="false"
                        CurrentFilterFunction="EqualTo" ShowFilterIcon="false" />
                    <telerik:GridBoundColumn DataField="StartDate" ItemStyle-Width="10%" DataFormatString="{0:d}"
                        FilterControlWidth="60%" HeaderText="Start Date" SortExpression="StartDate" AutoPostBackOnFilter="false"
                        CurrentFilterFunction="EqualTo" ShowFilterIcon="false" />
                    <telerik:GridBoundColumn DataField="EndDate" ItemStyle-Width="10%" DataFormatString="{0:d}"
                        FilterControlWidth="60%" HeaderText="End Date" SortExpression="EndDate" AutoPostBackOnFilter="false"
                        CurrentFilterFunction="EqualTo" ShowFilterIcon="false" />
                    <telerik:GridBoundColumn DataField="Status" ItemStyle-Width="10%" FilterControlWidth="80%"
                        HeaderText="Status" SortExpression="Status" AutoPostBackOnFilter="false" CurrentFilterFunction="EqualTo"
                        ShowFilterIcon="false" />
                    <telerik:GridBoundColumn DataField="FirstLast" ItemStyle-Width="25%" FilterControlWidth="80%"
                        HeaderText="Recipient (First Last)" SortExpression="FirstLast" AutoPostBackOnFilter="false"
                        CurrentFilterFunction="EqualTo" ShowFilterIcon="false" />
                    <telerik:GridTemplateColumn HeaderText="" DataField="" SortExpression="" ItemStyle-Width="10%"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <ItemTemplate>
                            <asp:Button CssClass="submitBtn" runat="server" ID="btnPrint" CommandName="Print"
                                CommandArgument='<%# Eval("SPID")%>' Text="Print" />
                        </ItemTemplate>
                        <ItemStyle />
                    </telerik:GridTemplateColumn>
                </Columns>
                <NoRecordsTemplate>
                    <asp:Label ID="lblNoGainingExp" runat="server" Text="No Record Found" Font-Bold="true"
                        ForeColor="Red"></asp:Label>
                </NoRecordsTemplate>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
</div>
<cc3:User ID="User1" runat="server" />
