<%@ Control Language="vb" AutoEventWireup="false" Debug="true" Inherits="Aptify.Framework.Web.eBusiness.Generated.CasesGridClass" CodeFile="~/UserControls/Aptify_Custom__c/CasesGrid__c.ascx.vb" %>

<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<style type="text/css">
    .auto-style1 {
        width: 100%;
    }
</style>


<div class="content-container clearfix">
    <table runat="server" id="tblMain" class="data-form">
        <tr>
            <td>
                <table class="auto-style1">
                    <tr>
                        <td style="width: 5%; text-align: right">
                            <asp:Button ID="cmdNewRecord" Text="New Case" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="grdMain" runat="server" AutoGenerateColumns="False" AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending" AllowFilteringByColumn="true">
                    <GroupingSettings CaseSensitive="false" />
                    <PagerStyle CssClass="sd-pager" />
                    <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                        <Columns>
                            <telerik:GridHyperLinkColumn Text="ID" DataTextField="ID" HeaderText="ID" SortExpression="ID" DataNavigateUrlFields="ID" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                            <telerik:GridBoundColumn DataField="Title" HeaderText="Title" SortExpression="Title" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                            <telerik:GridBoundColumn DataField="CaseCategory" HeaderText="Case Category" SortExpression="CaseCategory" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                            <telerik:GridBoundColumn DataField="CaseType" HeaderText="Case Type" SortExpression="CaseType" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                            <%--<Telerik:GridBoundColumn DataField="CasePriority" HeaderText="Priority"   SortExpression= "CasePriority"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />--%>
                            <telerik:GridBoundColumn DataField="CaseStatus" HeaderText="Status" SortExpression="CaseStatus" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                            <telerik:GridBoundColumn DataField="DateRecorded" HeaderText="Date Recorded" SortExpression="DateRecorded" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                            <telerik:GridBoundColumn DataField="RecordedBy" HeaderText="Recorded By" SortExpression="RecordedBy" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />

                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="False" />
</div>
<cc1:User ID="User1" runat="server" />
