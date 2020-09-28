<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/StudentResults__c.ascx.vb"
    Inherits="UserControls_Aptify_Custom__c_StudentResults__c" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="ajax" Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<style type="text/css">
    .style1 {
        width: 142px;
    }

    .active {
        display: block;
    }

    .inactive {
        display: none;
    }

    .collapse {
        display: none;
    }

    .expand {
        cursor: pointer;
    }

    .ui-draggable .ui-dialog-titlebar {
        background-image: none;
        background-color: rgb(231, 210, 182);
        color: Black;
        border: 1px solid #F4F3F1;
    }

    .ui-widget-content {
        border: 1px solid #F4F3F1;
    }

        .ui-state-default, .ui-widget-content .ui-state-default, .ui-widget-header .ui-state-default {
            background-image: none;
            background-color: blue;
            color: white;
            border: none;
        }

            .ui-state-default:hover {
                background-image: none;
                background-color: blue;
                color: white;
                border: none;
                font-weight: bolder;
            }

    .ui-dialog-content ui-widget-content {
        border: 1px solid #F4F3F1;
    }

    .ui-icon:hover {
        background-color: Blue;
    }

    .ui-dialog-buttonset {
        margin-right: 50%;
    }
</style>
<style type="text/css">
    .RadPivotGrid td {
        padding: 0;
    }

    div.qsf-right-content .qsf-col-wrap {
        position: static;
    }

    .RadPivotGrid_Metro .rpgContentZoneDiv td {
        white-space: wrap;
    }

    .popup_Buttons {
        margin: 10px;
    }

    .passed {
        width: 25px;
        height: 20px;
        background-color: Green;
    }

    .failed {
        width: 25px;
        height: 20px;
        background-color: red;
    }

    .gray {
        width: 25px;
        height: 20px;
        background-color: Gray;
    }

    .appeal {
        width: 25px;
        height: 20px;
        background-color: Yellow;
    }

    .GridDock {
        overflow-x: auto;
        overflow-y: hidden;
        width: 900px;
        padding: 0 0 17px 0;
        border-color: Black;
    }

    /*Added BY Pradip 2016-04-20 For BFP Scroll Bar Issue*/
    #page {
        width: 100% !important;
        padding: 0px;
        background: white; /*margin: 0px auto 0px auto;*/
    }

    #page-inner {
        width: 100% !important;
        margin: 0 auto;
        background-color: White; /*overflow: auto;*/
    }

    #home #content {
        background: -webkit-linear-gradient(top, #f0f0f0 100%,#ffffff 100%);
        width: 100% !important;
        padding: 10px;
        vertical-align: top;
        background-color: White;
        margin-top: 10px;
    }

    .RadMenu .rmRootGroup {
        width: 100% !important;
    }
    /*.RadPivotGrid RadPivotGrid_Default rpgTabular rpgFieldsWindowConfigurationPanel rpgStackedConfigurationPanel
    {
        width: 100%;
        overflow: scroll !important;
    }
    .RadPivotGrid
    {
        overflow: scroll !important;
    }*/

    .RadPivotGrid_Default {
        border: 1px solid #d9d9d9;
        background: white;
        color: #333;
        font-size: 12px;
        line-height: 16px;
        font-family: "Segoe UI",Arial,Helvetica,sans-serif; /*overflow: scroll !important;*/
    }
</style>
<script type="text/javascript">
    //hdnFAEUpdate
    $(document).ready(function () {

        var PanelState2 = document.getElementById('hdnFAEUpdate').value;

        if (PanelState2 == '1') {
            $('#divFAEUpdate').removeClass("collapse").addClass("active");
        }
    });

    function CollapseExpand(me, HiddenPanelState) {
        var Panelstate = $('#' + me).attr("class");
        $('#' + me).slideToggle('slow');
        if (Panelstate == "collapse") {

            $('#' + me).removeClass("collapse").addClass("active");
            SetPanelState(HiddenPanelState, 1)
        }
        else {
            $('#' + me).removeClass("active").addClass("collapse");
            SetPanelState(HiddenPanelState, 0)

        }

    }

    function SetPanelState(HiddenPanelState, StateValue) {

        if (HiddenPanelState == 'hdnFAEUpdate') {
            document.getElementById('hdnFAEUpdate').value = StateValue;
        }
    }

</script>
<script type="text/javascript">

</script>
<%-- Added by  Ashwini for redmine log #17426 --%>

<div>
    &nbsp;
</div>

<%-- End redmine log #17426 --%>
<div class="cai-form">
    <div class="form-title">Student results</div>
    <div class="link">
    <asp:LinkButton ID="lnkCAP1" runat="server" CssClass="cai-btn cai-btn-aqua-inverse" Width="60px" style="text-align:center; text-decoration:none;">CAP1</asp:LinkButton>
    <asp:LinkButton ID="lnkCAP2" runat="server" CssClass="cai-btn cai-btn-aqua-inverse" Width="60px" style="text-align:center; text-decoration:none;">CAP2</asp:LinkButton>
    <asp:LinkButton ID="lnkFAE" runat="server" CssClass="cai-btn cai-btn-aqua-inverse" Width="60px" style="text-align:center; text-decoration:none;">FAE</asp:LinkButton>
 </div>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadFormDecorator ID="QsfFromDecorator" runat="server" DecoratedControls="All"
        EnableRoundedCorners="false" />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
        <div class="info-data">
        </div>
        <div class="info-data form-content">
            <div class="row-div clearfix" style="padding:0px 20px">
                <div class="label-div w40">
                    <div class="info-data">
                        <div class="row-div clearfix">
                        </div>
                        <div class="row-div clearfix">
                        </div>
                        <div class="row-div clearfix">
                        </div>
                        <div class="row-div clearfix field-group grid-33-34-33">
                            <div class="label-div w25" align="left">
                                <asp:Label ID="lblAcademicYear" runat="server" CssClass="label-title" Text="Academic year: "></asp:Label>
                            </div>
                            <div class="field-div1 w25" align="left">
                                <asp:DropDownList ID="ddlAcademicYear"  CssClass="cai-table-data" runat="server" AutoPostBack="true" Width="250px">
                                </asp:DropDownList>
                            </div>
                        </div>


                        <div class="row-div clearfix field-group grid-33-34-33">
                            <div class="label-div w25" align="left">
                                <asp:Label ID="Label1" runat="server" CssClass="label-title" Text="Current stage: "></asp:Label>
                            </div>
                            <div class="field-div1 w25" align="left">
                                <asp:DropDownList ID="ddlCurrentStage"  CssClass="cai-table-data" runat="server" Width="250px" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
						 <div class="row-div clearfix field-group grid-33-34-33">
                            <div class="label-div w25" align="left">
                                            <asp:Label ID="Label22" runat="server" CssClass="label-title" Text="Session: "></asp:Label>
                                        </div>
                                         <div class="field-div1 w25" align="left">
                                               <asp:DropDownList ID="ddlSession" CssClass="cai-table-data" runat="server" Width="250px" AutoPostBack="true">
                                </asp:DropDownList>
                                           
                                        </div>
                          </div>
                    </div>
                </div>
                <%-- <div class="label-div w25">
                    <div class="info-data">
                        <div class="row-div clearfix field-group">
                            <div class="label-div w15">
                                <div class="passed">
                                </div>
                            </div>
                            <div class="field-div1 w35">
                                <asp:Label ID="lblPassed" runat="server" Text="FAE Passed"></asp:Label>
                            </div>
                        </div>
                        <div class="row-div clearfix field-group">
                            <div class="label-div w15">
                                <div class="failed">
                                </div>
                            </div>
                            <div class="field-div1 w35">
                                <asp:Label ID="lblFailed" runat="server" Text="FAE Failed"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>--%>
                <div class="label-div w25 grid-100">
                    <div class="info-data">
                        <div class="row-div clearfix field-group">
                            <div class="label-div w15">
                                <div class="gray">
                                </div>
                            </div>
                            <div class="field-div1 w35">
                                <asp:Label ID="lblResultPublish" runat="server" Text="Result not published"></asp:Label>
                            </div>
                        </div>
                        <div class="row-div clearfix field-group" style="display:none;">
                            <div class="label-div w15">
                                <div class="appeal">
                                </div>
                            </div>
                            <div class="field-div1 w35">
                                <asp:Label ID="lblAppealScore" runat="server" Text="Score after appeal"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row-div clearfix field-group" align="right">
                <asp:LinkButton ID="lnkAssignment" runat="server">Assignment details</asp:LinkButton>
            </div>
        </div>

        <div>
            <div style="padding:0px 20px;">
                <input type="hidden" name="hdnFAEUpdate" id="hdnFAEUpdate" value="0" />
                <h2 class="expand" id="HeadFAEUpdate" onclick="CollapseExpand('divFAEUpdate','hdnFAEUpdate')">Top score:</h2>
            </div>
            <%-- class="collapse"--%>
            <div id="divFAEUpdate" class="collapse" style="padding:0px 20px;">
                <br />
                <asp:UpdatePanel ID="upTimeTable" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                     
                        <%-- <div style="overflow-x: auto; width: 102%">--%>
                        <telerik:RadGrid ID="grdTopScore" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                            SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                            PagerStyle-PageSizeLabelText="Records Per Page" PageSize="10" CssClass="cai-table">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false"
                                EnableNoRecordsTemplate="true" ShowHeadersWhenNoRecords="true" AllowPaging="true"
                                PageSize="10" >
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderText="Student number" DataField="" SortExpression=""
                                        ItemStyle-Width="10%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="lbtnStudent" CommandName="Student" CommandArgument='<%# Eval("StudentID")%>'
                                                Text='<%# Eval("StudentOldID")%>' />
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="FirstLast" ItemStyle-Width="20%" FilterControlWidth="80%"
                                        HeaderText="Name" SortExpression="FirstLast" AutoPostBackOnFilter="false" CurrentFilterFunction="EqualTo"
                                        ShowFilterIcon="false" />
                                    <telerik:GridBoundColumn DataField="Stage" ItemStyle-Width="20%" FilterControlWidth="80%"
                                        HeaderText="Current stage" SortExpression="Stage" AutoPostBackOnFilter="false"
                                        CurrentFilterFunction="EqualTo" ShowFilterIcon="false" />
                                    <telerik:GridBoundColumn DataField="ExamSession" ItemStyle-Width="20%" FilterControlWidth="80%"
                                        HeaderText="Session" SortExpression="ExamSession" AutoPostBackOnFilter="false"
                                        CurrentFilterFunction="EqualTo" ShowFilterIcon="false" />
                                    <telerik:GridBoundColumn DataField="Comments" ItemStyle-Width="20%" FilterControlWidth="80%"
                                        HeaderText="Comment" SortExpression="Comments" AutoPostBackOnFilter="false" CurrentFilterFunction="EqualTo"
                                        ShowFilterIcon="false" />
                                </Columns>
                                <NoRecordsTemplate>
                                    <asp:Label ID="lblNoGainingExp" runat="server" Text="No record found" Font-Bold="true"
                                        ForeColor="Red"></asp:Label>
                                </NoRecordsTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <%-- </div>--%>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div>
            <br />
            <br />
        </div>
<div style="padding:0px 20px;">
       
        <telerik:RadPivotGrid runat="server" ID="gvStudentResult" Skin="Default" Width="100%" CssClass="StudentResultgrid"
                AllowPaging="false" AllowSorting="true" AllowFiltering="false" ColumnHeaderZoneText="ColumnHeaderZone"
                ColumnGroupsDefaultExpanded="true" Culture="en-US" LocalizationPath="~/App_GlobalResources/"
                TotalsSettings-RowsSubTotalsPosition="None" TotalsSettings-ColumnsSubTotalsPosition="None"
                TotalsSettings-ColumnGrandTotalsPosition="None" ClientSettings-EnableFieldsDragDrop="false" TotalsSettings-GrandTotalsVisibility="None"
               >
            <TotalsSettings ColumnGrandTotalsPosition="None" ColumnsSubTotalsPosition="None"
                GrandTotalsVisibility="None" RowGrandTotalsPosition="None" RowsSubTotalsPosition="None" />
           <%-- <PagerStyle Mode="NextPrevAndNumeric" AlwaysVisible="true" ChangePageSizeLabelText="Page Size:"></PagerStyle>--%>
            <TotalsSettings GrandTotalsVisibility="None" ColumnsSubTotalsPosition="None" ColumnGrandTotalsPosition="None" />
            <ClientSettings EnableFieldsDragDrop="false">
               <Scrolling  SaveScrollPosition="true"></Scrolling>
            </ClientSettings>
            <Fields>
                <telerik:PivotGridRowField DataField="StudentID" Caption="#" >
                </telerik:PivotGridRowField>
                <telerik:PivotGridRowField DataField="LastName" Caption="Last name" >
                </telerik:PivotGridRowField>
                <telerik:PivotGridRowField DataField="FirstName" Caption="First name" >
                </telerik:PivotGridRowField>
                <telerik:PivotGridRowField DataField="Route" Caption="Route" >
                </telerik:PivotGridRowField>
                <telerik:PivotGridRowField DataField="CurrentStage" Caption="Current stage" UniqueName="CurrentStage">
                </telerik:PivotGridRowField>
                <telerik:PivotGridRowField DataField="CAP1" Caption="CAP1" UniqueName="CAP1" >
                </telerik:PivotGridRowField>
                <telerik:PivotGridRowField DataField="CAP2" Caption="CAP2" UniqueName="CAP2" >
                </telerik:PivotGridRowField>
                <telerik:PivotGridRowField DataField="FAE" Caption="FAE" UniqueName="FAE" >
                </telerik:PivotGridRowField>
                <telerik:PivotGridRowField DataField="Venue" Caption="Venue" UniqueName="Venue" >
                </telerik:PivotGridRowField>
                <telerik:PivotGridColumnField DataField="Curriculum" Caption="Curriculum" UniqueName="Curriculum">
                </telerik:PivotGridColumnField>
                <telerik:PivotGridColumnField DataField="Type" Caption="Type" UniqueName="Type">
                </telerik:PivotGridColumnField>
                <telerik:PivotGridColumnField DataField="Course" Caption="Course" UniqueName="Course">
                </telerik:PivotGridColumnField>
                <telerik:PivotGridAggregateField DataField="Score" >
                    <CellTemplate>
                        <asp:Label ID="lblScore" Text='<%#(Container.DataItem) %>' Visible='<%# IIf((Container.DataItem) <= -1,false, true)%>'
                            width="50px"  runat="server"></asp:Label>
                    </CellTemplate>
                </telerik:PivotGridAggregateField>
            </Fields>
        </telerik:RadPivotGrid>
</div>
        <%--</div>--%>
        <asp:Label ID="lblNoRecords" runat="server"></asp:Label>
        <div class="info-data">
            <div class="row-div clearfix">
                <div class="label-div w20">
                    <br />
                </div>
            </div>
        </div>
        <div class="info-data form-content">
            <div class="row-div clearfix field-group">
                <div class="label-div w90">
                    <asp:Button ID="btnBack" runat="server" CssClass="submitBtn" Text="Back" Height="32px" />
                </div>
            </div>
        </div>
        <cc1:User ID="loggedInUser" runat="server" />
    </telerik:RadAjaxPanel>
</div>
