<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/SixMonthlySummaryMentor__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.SixMonthlySummaryMentor__c" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<div class="info-data">
    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
     

    <div class="cai-table mobile-table">
        <telerik:RadGrid ID="radStudentReview" runat="server" AutoGenerateColumns="false" AllowSorting="true" AllowPaging="true" PageSize="5"
          CssClass="cai-table" PagerStyle-PageSizeControlType="None">
            <PagerStyle CssClass="sd-pager" />
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView AllowFilteringByColumn="false"  AllowSorting="true" AllowNaturalSort="false">
                <Columns>
                    <telerik:GridTemplateColumn DataField="StudentNo" HeaderText="Student no." SortExpression="StudentNo"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                        ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <span class="mobile-label">Student #:</span>
                            <asp:Label ID="Label1" CssClass="cai-table-data" runat="server" Text='<%# Eval("StudentNo")%>'></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Full name" AllowFiltering="true" SortExpression="FirstLast"
                        AutoPostBackOnFilter="true" DataField="FirstLast" CurrentFilterFunction="Contains"
                        ShowFilterIcon="false">
                        <ItemTemplate>
                            <span class="mobile-label">Full name:</span>
                            <asp:LinkButton CssClass="cai-table-data" ID="lnkStudentName" runat="server" ForeColor="Blue" CommandName="PersonName"
                                Font-Underline="true" CommandArgument='<%# Eval("StudentID")%>' Text='<%# Eval("FirstLast")%>'></asp:LinkButton>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="BusinessUnit" HeaderText="Business unit" SortExpression="BusinessUnit"
                        AutoPostBackOnFilter="true" AllowFiltering="true" CurrentFilterFunction="Contains"
                        ShowFilterIcon="false" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <span class="mobile-label">Business unit:</span>
                            <asp:Label ID="Label2" CssClass="cai-table-data" runat="server" Text='<%# Eval("BusinessUnit")%>'></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
                <NoRecordsTemplate>
                    <asp:Label ID="lblNoRecord" runat="server" Text="No record found" Font-Bold="true"
                        ForeColor="Red"></asp:Label>
                </NoRecordsTemplate>
            </MasterTableView>
        </telerik:RadGrid>

        <div class="actions">
            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="submitBtn" CausesValidation="false" />
        </div>
    </div>
</div>
<cc3:User ID="User1" runat="server" />
