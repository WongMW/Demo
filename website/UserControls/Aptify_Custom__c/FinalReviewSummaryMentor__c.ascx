<%@ Control Language="VB" AutoEventWireup="false" CodeFile="FinalReviewSummaryMentor__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.FinalReviewSummaryMentor__c" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<div class="info-data">
    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
    <div class="row-div clearfix" align="right">
        <%-- <asp:HyperLink ID="lnkHelp" Text="Help" runat="server" Target="_blank"></asp:HyperLink>--%>
    </div>
    <div class="row-div clearfix">
        <div class="field-div1 w100">
            <telerik:RadGrid ID="radStudentReview" runat="server" AutoGenerateColumns="False"
                AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                AllowFilteringByColumn="true" AllowSorting="true" Width="150px" PageSize="5">
                <GroupingSettings CaseSensitive="false" />
                <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                    <Columns>
                        <telerik:GridBoundColumn DataField="StudentNo" HeaderText="Student No." SortExpression="StudentNo"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                            FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn HeaderText="Full Name" AllowFiltering="true" SortExpression="FirstLast"
                            AutoPostBackOnFilter="true" DataField="FirstLast" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkStudentName" runat="server" ForeColor="Blue" CommandName="PersonName"
                                    Font-Underline="true" CommandArgument='<%# Eval("StudentID")%>' Text='<%# Eval("FirstLast")%>'></asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="BusinessUnit" HeaderText="Business Unit" SortExpression="BusinessUnit"
                            AutoPostBackOnFilter="true" AllowFiltering="true" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
            <br />
            <div style="text-align: right;">
                <asp:Button ID="btnBack" runat="server" Text="Back" Height="25px" CausesValidation="false" />
            </div>
        </div>
    </div>
</div>
<cc3:User ID="User1" runat="server" />
