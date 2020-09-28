<%--Aptify e-Business 5.5.1, July 2013--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/PendingChangesDetails__c.ascx.vb" Debug="true"
    Inherits="Aptify.Framework.Web.eBusiness.CustomerService.PendingChangesDetails__c" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<div class="content-container clearfix" id="divTop" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <%--Commented by Vaishali - 8/4/2017
                <rad:RadGrid ID="rgvPendingchange" runat="server" AutoGenerateColumns="False"
                AllowPaging="true" SortingSettings-SortedAscToolTip="Sorted Ascending" PageSize="5" PagerStyle-PageSizeLabelText="Records Per Page" CssClass="cai-table">
                <PagerStyle CssClass="sd-pager" />
                <MasterTableView>
                    <Columns>

                        <rad:GridBoundColumn DataField="FieldName" HeaderText="Field" />
                        <rad:GridBoundColumn DataField="Changes" HeaderText="Current Value" />
                        <rad:GridBoundColumn DataField="NewValue" HeaderText="Proposed Value" />
                        <rad:GridBoundColumn DataField="ScheduledDate" HeaderText="Scheduled Date" Visible="false" />
                        <%--    <rad:GridBoundColumn DataField="Type" HeaderText="Specific Type" /> --%>
            <%--    </Columns>
                </MasterTableView>
            </rad:RadGrid>--%>
            <%-- Susan Wong, Ticket #18528 improve usability start - PENDING CHANGES SECTION --%>
            <%-- <asp:GridView ID="rgvPendingchange" runat="server" AutoGenerateColumns="false" AllowSorting="true" AllowPaging="true" PageSize="5" PagerStyle-PageSizeLabelText="Records Per Page" CssClass="cai-table">
                <PagerStyle CssClass="sd-pager" /> --%>
            <asp:GridView ID="rgvPendingchange" runat="server" AutoGenerateColumns="false" AllowSorting="true">
            <%-- Susan Wong, Ticket #18528 improve usability end - PENDING CHANGES SECTION --%>
                <Columns>
                    <asp:BoundField DataField="FieldName" HeaderText="Field changed" />
                    <asp:BoundField DataField="Changes" HeaderText="Change from" />
                    <asp:BoundField DataField="NewValue" HeaderText="Change to" />
                    <asp:BoundField DataField="ScheduledDate" HeaderText="Scheduled Date" Visible="false" />
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>

