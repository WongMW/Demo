<%@ Control Language="VB" AutoEventWireup="false" CodeFile="StudentResults__c.ascx.vb"
    Inherits="UserControls_Aptify_Custom__c_StudentResults__c" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="ajax" Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<style type="text/css">
    .RadPivotGrid td
    {
        padding:0;    
    }
    div.qsf-right-content .qsf-col-wrap
    {
        position: static;
    }
    .RadPivotGrid_Metro .rpgContentZoneDiv td
    {
        white-space: wrap;
    }
    .popup_Buttons
    {
        margin: 10px;
    }
    .passed
    {
        width: 25px;
        height: 20px;
        background-color: Green;
    }
    .failed
    {
        width: 25px;
        height: 20px;
        background-color: red;
    }
    .gray
    {
        width: 25px;
        height: 20px;
        background-color: Gray;
    }
    .appeal
    {
        width: 25px;
        height: 20px;
        background-color: Yellow;
    }
    .GridDock
    {
        overflow-x: auto;
        overflow-y: hidden;
        width: 900px;
        padding: 0 0 17px 0;
        border-color: Black;
    }
</style>
<script type="text/javascript">

</script>
<div class="maindiv">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadFormDecorator ID="QsfFromDecorator" runat="server" DecoratedControls="All"
        EnableRoundedCorners="false" />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
        <div class="info-data">
        </div>
        <div class="info-data">
            <div class="row-div clearfix">
                <div class="label-div w40">
                    <div class="info-data">
                        <div class="row-div clearfix">
                        </div>
                        <div class="row-div clearfix">
                        </div>
                        <div class="row-div clearfix">
                        </div>
                        <div class="row-div clearfix">
                            <div class="label-div w25" align="left">
                                <asp:Label ID="lblAcademicYear" runat="server" Font-Bold="True" Font-Size="8" Text="Academic Year: "></asp:Label>
                            </div>
                            <div class="field-div1 w25" align="left">
                                <asp:DropDownList ID="ddlAcademicYear" runat="server" AutoPostBack="true" Width="70%">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="label-div w25">
                    <div class="info-data">
                        <div class="row-div clearfix">
                            <div class="label-div w15">
                                <div class="passed">
                                </div>
                            </div>
                            <div class="field-div1 w35">
                                <asp:Label ID="lblPassed" runat="server" Font-Bold="True" Font-Size="8" Text="FAE Passed"></asp:Label>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div class="label-div w15">
                                <div class="failed">
                                </div>
                            </div>
                            <div class="field-div1 w35">
                                <asp:Label ID="lblFailed" runat="server" Font-Bold="True" Font-Size="8" Text="FAE Failed"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="label-div w25">
                    <div class="info-data">
                        <div class="row-div clearfix">
                            <div class="label-div w15">
                                <div class="gray">
                                </div>
                            </div>
                            <div class="field-div1 w35">
                                <asp:Label ID="lblResultPublish" runat="server" Font-Bold="True" Font-Size="8" Text="Result not published"></asp:Label>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div class="label-div w15">
                                <div class="appeal">
                                </div>
                            </div>
                            <div class="field-div1 w35">
                                <asp:Label ID="lblAppealScore" runat="server" Font-Bold="True" Font-Size="8" Text="Score After Appeal"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row-div clearfix" align="right">
                <asp:LinkButton ID="lnkAssignment" runat="server">Assignment Details</asp:LinkButton>
            </div>
        </div>
        <div style="overflow-x: auto; width: 100%">
            <telerik:RadPivotGrid runat="server" ID="gvStudentResult" AllowPaging="True" PageSize="10"
                Skin="Default" AccessibilitySettings-OuterTableCaption="" Width="120%" AllowFiltering="True"
                AllowSorting="true" RowHeaderCellStyle-Width="66px" RowHeaderCellStyle-Font-Bold="false"
                ShowFilterHeaderZone="false" ShowColumnHeaderZone="false" ShowDataHeaderZone="false"
                ShowRowHeaderZone="True" ColumnHeaderZoneText="ColumnHeaderZone" ColumnGroupsDefaultExpanded="false"
                Culture="en-US" LocalizationPath="~/App_GlobalResources/">
                <TotalsSettings ColumnGrandTotalsPosition="None" ColumnsSubTotalsPosition="None"
                    GrandTotalsVisibility="None" RowGrandTotalsPosition="None" RowsSubTotalsPosition="None" />
                <PagerStyle Mode="NextPrevAndNumeric" AlwaysVisible="true" ChangePageSizeLabelText="Page Size:">
                </PagerStyle>
                <TotalsSettings GrandTotalsVisibility="None" ColumnsSubTotalsPosition="None" ColumnGrandTotalsPosition="None" />
                <ClientSettings EnableFieldsDragDrop="true">
                    <Scrolling AllowVerticalScroll="false" SaveScrollPosition="true"></Scrolling>
                </ClientSettings>
                <Fields>
                    <telerik:PivotGridRowField DataField="StudentID" Caption="#" CellStyle-Width="5%">                        
                    </telerik:PivotGridRowField>
                    <telerik:PivotGridRowField DataField="LastName" Caption="Last Name" CellStyle-Width="9%">
                    </telerik:PivotGridRowField>
                    <telerik:PivotGridRowField DataField="FirstName" Caption="First Name" CellStyle-Width="9%">
                    </telerik:PivotGridRowField>
                    <telerik:PivotGridRowField DataField="Route" Caption="Route" CellStyle-Width="7%">
                    </telerik:PivotGridRowField>
                    <telerik:PivotGridRowField DataField="CurrentStage" Caption="Current Stage" UniqueName="CurrentStage"
                        CellStyle-Width="10%">
                    </telerik:PivotGridRowField>
                    <telerik:PivotGridRowField DataField="CAP1" Caption="CAP1" UniqueName="CAP1" CellStyle-Width="6%">
                    </telerik:PivotGridRowField>
                    <telerik:PivotGridRowField DataField="CAP2" Caption="CAP2" UniqueName="CAP2" CellStyle-Width="6%">
                    </telerik:PivotGridRowField>
                    <telerik:PivotGridRowField DataField="FAE" Caption="FAE" UniqueName="FAE" CellStyle-Width="6%">
                    </telerik:PivotGridRowField>
                    <telerik:PivotGridRowField DataField="Venue" Caption="Venue" UniqueName="Venue" CellStyle-Width="7%">
                    </telerik:PivotGridRowField>
                    <telerik:PivotGridColumnField DataField="Curriculum" Caption="Curriculum" UniqueName="Curriculum">
                    </telerik:PivotGridColumnField>
                    <telerik:PivotGridColumnField DataField="Type" Caption="Type" UniqueName="Type">
                    </telerik:PivotGridColumnField>
                    <telerik:PivotGridColumnField DataField="Course" Caption="Course" UniqueName="Course">
                    </telerik:PivotGridColumnField>
                    <telerik:PivotGridAggregateField DataField="Score">
                        <CellTemplate>
                            <asp:Label ID="lblScore" Text='<%#(Container.DataItem) %>' Visible='<%# IIf((Container.DataItem) <= -1,false, true)%>'
                                Height="100%" Width="100%" runat="server"></asp:Label>
                        </CellTemplate>
                    </telerik:PivotGridAggregateField>
                </Fields>
            </telerik:RadPivotGrid>
        </div>
        <asp:Label ID="lblNoRecords" runat="server"></asp:Label>
        <div class="info-data">
            <div class="row-div clearfix">
                <div class="label-div w20">
                    <br />
                </div>
            </div>
        </div>
        <div class="info-data">
            <div class="row-div clearfix">
                <div class="label-div w90">
                    <asp:Button ID="btnBack" runat="server" CssClass="submit-Btn" Text="Back" Width="8%" />
                </div>
            </div>
        </div>
        <cc1:User ID="loggedInUser" runat="server" />
    </telerik:RadAjaxPanel>
</div>
