<%@ Control Language="VB" AutoEventWireup="false" CodeFile="FirmCourseEnrollmentSummary__c.ascx.vb"
    Inherits="FirmCourseEnrollmentSummary__c" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="ajax" Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<style type="text/css">
    .inactive
    {
        display: none;
    }
</style>
<div class="maindiv">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
        <div style="overflow-x: auto; width: 100%">
            <telerik:RadPivotGrid runat="server" ID="gvFirmCourseEnrollmentSummary" Skin="Default"
                Width="120%" AllowPaging="True" AllowSorting="true" LocalizationPath="~/App_GlobalResources/"
                ShowFilterHeaderZone="true" AggregatesLevel="2" AllowFiltering="False" PageSize="10"
                ColumnGroupsDefaultExpanded="false" ColumnHeaderZoneText="ColumnHeaderZone" Culture="en-US"
                Visible="true" RowTableLayout="Tabular" AggregatesPosition="Columns">
                <%--<RowTotalCellStyle CssClass="inactive" />--%>
                <%-- <TotalsSettings RowsSubTotalsPosition="None" ColumnGrandTotalsPosition="Last" RowGrandTotalsPosition="Last"
                    GrandTotalsVisibility="RowsAndColumns" />--%>
                <TotalsSettings RowsSubTotalsPosition="Last" ColumnGrandTotalsPosition="Last" RowGrandTotalsPosition="Last"
                    GrandTotalsVisibility="RowsAndColumns" />
                <ClientSettings EnableFieldsDragDrop="true">
                    <Scrolling AllowVerticalScroll="false" SaveScrollPosition="true"></Scrolling>
                </ClientSettings>
                <Fields>
                    <telerik:PivotGridRowField DataField="Location" Caption="Location" CellStyle-Width="9%">
                    </telerik:PivotGridRowField>
                    <%--<telerik:PivotGridRowField DataField="StudentNumber" Caption="#" CellStyle-Width="5%">
                    </telerik:PivotGridRowField>
                    <telerik:PivotGridRowField DataField="LastName" Caption="Last Name" CellStyle-Width="9%">
                    </telerik:PivotGridRowField>
                    <telerik:PivotGridRowField DataField="FirstName" Caption="First Name" CellStyle-Width="9%">
                    </telerik:PivotGridRowField>--%>
                    <telerik:PivotGridRowField DataField="StudentName" Caption="Student" CellStyle-Width="23%">
                    </telerik:PivotGridRowField>
                    <%--<telerik:PivotGridRowField DataField="RouteOfEntry" Caption="Route Of Entry" CellStyle-Width="10%">
                    </telerik:PivotGridRowField>--%>
                    <telerik:PivotGridReportFilterField DataField="RouteOfEntry" Caption="Route Of Entry"
                        CellStyle-Width="10%">
                    </telerik:PivotGridReportFilterField>
                    <telerik:PivotGridColumnField DataField="Curriculum" Caption="Curriculum" UniqueName="Curriculum">
                    </telerik:PivotGridColumnField>
                    <%-- <telerik:PivotGridColumnField DataField="Type" Caption="Type" UniqueName="Type">
                        <CellTemplate>
                            <asp:Label ID="lblCapPart" runat="server" Text='<%#(Container.DataItem).Substring((Container.DataItem).IndexOf(";") + 1) %>'></asp:Label>
                        </CellTemplate>
                    </telerik:PivotGridColumnField>--%>
                    <telerik:PivotGridColumnField DataField="CourseName" Caption="Course" UniqueName="CourseName">
                    </telerik:PivotGridColumnField>
                    <telerik:PivotGridAggregateField Aggregate="Sum" DataField="Price">
                    </telerik:PivotGridAggregateField>
                </Fields>
            </telerik:RadPivotGrid>
        </div>
        <div class="info-data">
            <div class="row-div clearfix">
                <%--Siddharth-9th May 16: Added label for UAT item G3-49--%>
                <asp:Label ID="lblCurrency" runat="server" Font-Bold="true"></asp:Label>
                <%--Siddharth-9th May 16: Added label for UAT item G3-49--%>
            </div>
            <div class="row-div clearfix">
            </div>
            <div class="row-div clearfix">
                <div class="label-div w98">
                    <asp:Label ID="lblWebRemittaneNumber" Visible="false" runat="server" Font-Bold="true"
                        Text=""></asp:Label>
                    <asp:Button ID="btnSubmit" Text="Submit" runat="server" CssClass="submit-Btn" />
                    <asp:Button ID="btnBack" Text="Back" runat="server" CssClass="submit-Btn" />
                </div>
                <div class="label-div w98">
                    <asp:Label ID="lblWebRemittaneNumberText" Visible="false" runat="server" Font-Bold="true"
                        Text=""></asp:Label>
                </div>
            </div>
        </div>
        <asp:Label ID="lblNoRecords" runat="server"></asp:Label>
        <cc1:User ID="loggedInUser" runat="server" />
    </telerik:RadAjaxPanel>
</div>
