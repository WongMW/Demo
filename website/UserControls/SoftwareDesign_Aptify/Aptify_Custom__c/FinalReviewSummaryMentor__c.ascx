<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/FinalReviewSummaryMentor__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.FinalReviewSummaryMentor__c" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<div class="info-data">
    &nbsp;
    <div class="cai-table">
    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
    <div class="row-div clearfix" align="right">
        <%-- <asp:HyperLink ID="lnkHelp" Text="Help" runat="server" Target="_blank"></asp:HyperLink>--%>
    </div>
    <div class="row-div clearfix">
        <div class="field-div1 w100">
            <telerik:RadGrid ID="radStudentReview" runat="server" AutoGenerateColumns="False"
                AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                AllowFilteringByColumn="true" AllowSorting="true" Width="50%" PageSize="5">
                <GroupingSettings CaseSensitive="false" />
                <MasterTableView AllowFilteringByColumn="false" AllowSorting="true" AllowNaturalSort="false">
                    <Columns>
                        <telerik:GridBoundColumn DataField="StudentNo" HeaderText="Student no." SortExpression="StudentNo"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                            FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                        <telerik:GridTemplateColumn HeaderText="Full name" AllowFiltering="true" SortExpression="FirstLast"
                            AutoPostBackOnFilter="true" DataField="FirstLast" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkStudentName" runat="server" ForeColor="Blue" CommandName="PersonName"
                                    Font-Underline="true" CommandArgument='<%# Eval("StudentID")%>' Text='<%# Eval("FirstLast")%>'></asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="BusinessUnit" HeaderText="Business unit" SortExpression="BusinessUnit"
                            AutoPostBackOnFilter="true" AllowFiltering="true" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
            <br />
            <div class="actions">
                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="submitBtn" CausesValidation="false" />
            </div>
        </div>
     </div>
    </div>
</div>
<cc3:User ID="User1" runat="server" />
