<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/FirmCourseEnrollmentSummary__c.ascx.vb"
    Inherits="FirmCourseEnrollmentSummary__c" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="ajax" Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<style type="text/css">
    .inactive {
        display: none;
    }
 .rpgDataCell.rpgRowTotalDataCell {
        font-weight: bold !important;
    }
.collapse {
    display: none;
}
</style>
<script type="text/javascript">
    function HideCells() {
        var cell1 = document.getElementById("ctl00_ctl00_baseTemplatePlaceholder_content_FirmCourseEnrollmentSummary__c_gvFirmCourseEnrollmentSummary_ctl00_ctl00_rc_RouteOfEntry");

        var cell2 = document.getElementById("ctl00_ctl00_baseTemplatePlaceholder_content_FirmCourseEnrollmentSummary__c_gvFirmCourseEnrollmentSummary_ctl01_ctl00_rc_Price");
        if (cell1 != undefined) {
            cell1.className = "collapse";
        }

        if (cell2 != undefined) {
            cell2.className = "collapse";
        }
    }
</script>

<div class="maindiv">
    <%--Commented RadAjaxPanel and added Update Panel to resolve Postback issue--%>
    <%--<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>--%>
    <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">--%>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing">
                <img src="/Images/CAITheme/bx_loader.gif" />
                <span>Loading</span>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
          <%--<div style="overflow-x: auto; width: 100%">--%>
 <div>
            <telerik:RadPivotGrid runat="server" ID="gvFirmCourseEnrollmentSummary" Skin="Default"
                Width="100%"  AllowSorting="true" LocalizationPath="~/App_GlobalResources/"
                ShowFilterHeaderZone="true" AggregatesLevel="2" AllowFiltering="False" 
                ColumnGroupsDefaultExpanded="false" ColumnHeaderZoneText="ColumnHeaderZone" Culture="en-US"
                Visible="true" RowTableLayout="Tabular" AggregatesPosition="Columns">
                 <%-- <PagerStyle CssClass="sd-pager" />--%>

                <%--<RowTotalCellStyle CssClass="inactive" />--%>
                <%-- <TotalsSettings RowsSubTotalsPosition="None" ColumnGrandTotalsPosition="Last" RowGrandTotalsPosition="Last"
                    GrandTotalsVisibility="RowsAndColumns" />--%>
                <TotalsSettings RowsSubTotalsPosition="Last" ColumnGrandTotalsPosition="Last" RowGrandTotalsPosition="Last"
                    GrandTotalsVisibility="RowsAndColumns" />
                <ClientSettings EnableFieldsDragDrop="true">
                     <%-- <Scrolling AllowVerticalScroll="false" SaveScrollPosition="true"></Scrolling>--%>
                    <Scrolling SaveScrollPosition="true"></Scrolling>
                </ClientSettings>
                <Fields>
                    <telerik:PivotGridRowField DataField="Location" Caption="Location" CellStyle-Width="20%">
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
                    <asp:Button ID="btnSubmit" Text="Submit" runat="server" CssClass="submitBtn" />
                    <asp:Button ID="btnBack" Text="Back" runat="server" CssClass="submitBtn" />
                </div>
                <div class="label-div w98">
                    <asp:Label ID="lblWebRemittaneNumberText" Visible="false" runat="server" Font-Bold="true"
                        Text=""></asp:Label>
                </div>
            </div>
        </div>
        <asp:Label ID="lblNoRecords" runat="server"></asp:Label>
        <cc1:User ID="loggedInUser" runat="server" />
    </ContentTemplate>        
    </asp:UpdatePanel>
    <%--</telerik:RadAjaxPanel>--%>
</div>
