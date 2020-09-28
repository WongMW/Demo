<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/ClassesScheduler__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Education.ClassesScheduler__c" %>
<%--<%@ Register Src="InstructorValidator__c.ascx" TagName="InstructorValidator" TagPrefix="uc1" %>--%>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/UserControls/Aptify_Custom__c/RecordAttachments__c.ascx" TagPrefix="ucRecordAttachment"
    TagName="RecordAttachments__c" %>
<%--Suraj Issue 14452,5/7/13 Remove the in line css which is not used any where --%>
<style type="text/css">
    /*.RadScheduler .rsHeader
    {
        z-index: 1;
    }*/
      .rsWrap {
        height:34px !important;
    }
    .rsApt
    {
        height:34px !important;
    }

    @media screen and (max-width:420px)
    {
        .mob-view input[type="radio"]{
            clear: left;float:left;margin-top:10px;
        }
        .mob-view input[type=radio] + label {
            float:left;margin:5px 0px 0px 0px;
        }
        #ctl00_ctl00_baseTemplatePlaceholder_content_ClassesSchedulerc_grdClasses_ctl00 tr td:last-child {
            margin-bottom:20px;border-bottom: 1px solid #000;width: 100% !important;padding-bottom: 20px;
        }
    }
</style>
<div class="content-container clearfix">
    <%-- <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>--%>
    <div class="dvUpdateProgress" style="overflow: visible;">
        <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
            <ProgressTemplate>
                <div class="dvProcessing" style="height: 1760px;">
                    <table class="tblFullHeightWidth">
                        <tr>
                            <td class="tdProcessing" style="vertical-align: middle">
                                Please wait...
                            </td>
                        </tr>
                    </table>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">--%>
  <%--    <asp:UpdatePanel ID="UpdatepnlorderDetail" runat="server" UpdateMode="Always">
        <ContentTemplate>--%>
            <table width="100%" id="tabData" runat="server" class="data-form">
                <tr>
                    <td>
                         <div class="actions mob-view">
            <asp:RadioButton ID="rdoCurrentAcademicCycle" runat="server" GroupName="AcadmicCycle"
                Text="Academic cycle: " AutoPostBack="true" Checked="true" />
            <asp:RadioButton ID="rdoNextAcadmicCycle" runat="server" GroupName="AcadmicCycle"
                Text="Academic cycle: " AutoPostBack="true" />
        </div>
                    </td>
                    <td align="right">
                        &nbsp;
                    </td>
                </tr>
                 <tr>
                    <td style="text-align: right;" colspan="2">
 			            <asp:Button ID="btnExport" runat="server" Text="Export to excel" OnClick="btnExport_Click" CssClass="submitBtn" />
                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="submitBtn" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <%-- WongS, Modified for #20779 Start--%>
                        <asp:LinkButton ID="lnkCalendar" runat="server" Text="Calendar view"><i class="far fa-calendar-alt"></i> Calendar view</asp:LinkButton>
                        <asp:LinkButton ID="lnkList" runat="server" Text="List view"><i class="far fa-list-alt"></i> List view</asp:LinkButton>
                         <%-- WongS, Modified for #20779 End --%>
                    </td>
                    <td align="right">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel ID="pnlCalendar" runat="server">
                            <telerik:RadScheduler runat="server" ID="radSchedulerLecturer" SelectedView="MonthView"
                                DataKeyField="ClassID" DataStartField="StartDate" DataEndField="EndDateTime"
                                DataSubjectField="ClassNPartName" AppointmentStyleMode="Default" HoursPanelTimeFormat="HH"
                                StartEditingInAdvancedForm="false" Width="100%" Height="90%" Skin="Default" AllowEdit="false"
                                AllowInsert="false" AllowDelete="false">
                                <AppointmentTemplate>
                                    <div class="appointmentHeader">
                                        <asp:Label ID="lblTime" runat="server" Font-Bold="true" Text='<%#IIf(((Eval("Start","{0:HH:mm:ss}"))="00:00:00" AND (Eval("End","{0:HH:mm:ss}"))="23:59:59"),"All Day",Eval("Start","{0:HH:mm:ss}") & " - " & Eval("End","{0:HH:mm:ss}")) %>'></asp:Label>
                                    </div>
                                    <div class="appointmentHeader">
                                        <asp:Label ID="lblSubject" runat="server" Font-Bold="true" Text='<%#Eval("Subject") %>'></asp:Label>
                                    </div>
                                </AppointmentTemplate>
                                <MonthView UserSelectable="true" />
                                <WeekView UserSelectable="false" />
                                <DayView UserSelectable="false" />
                                <MultiDayView UserSelectable="false" />
                                <TimelineView UserSelectable="false" />
                                <AppointmentContextMenus>
                                    <rad:RadSchedulerContextMenu Visible="false">
                                    </rad:RadSchedulerContextMenu>
                                    <telerik:RadSchedulerContextMenu Visible="false">
                                    </telerik:RadSchedulerContextMenu>
                                </AppointmentContextMenus>
                            </telerik:RadScheduler>
                        </asp:Panel>
                       <div class="plain-table stu-timetable-grid"> <%-- WongS, Modified for #20779 --%>
                        <asp:Panel ID="pnlList" runat="server">
                              <telerik:RadGrid ID="grdClasses" runat="server" AutoGenerateColumns="False" AllowPaging="true" RenderMode="Lightweight"
                                AllowFilteringByColumn="false" SortingSettings-SortedDescToolTip="Sorted Descending"
                                SortingSettings-SortedAscToolTip="Sort Ascending" Width="100%" Height="90%" PageSize="10" PagerStyle CssClass="sd-pager">
                                <GroupingSettings CaseSensitive="false" />
                                <ExportSettings ExportOnlyData="True" IgnorePaging="True">
                                <Excel Format="Xlsx" FileExtension="xlsx" AutoFitImages="True" />
                                </ExportSettings>
                                <MasterTableView AllowSorting="true" AllowNaturalSort="false">
                                    <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                    <Columns>
                                        
                                        <telerik:GridDateTimeColumn DataField="StartDate" HeaderText="Start date" FilterControlWidth="100%"
                                            SortExpression="StartDate" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                            HeaderStyle-Width="140px" ShowFilterIcon="false" EnableTimeIndependentFiltering="true" />

                                     <%--   <telerik:GridDateTimeColumn DataField="EndDate" HeaderText="End Date" FilterControlWidth="100%"
                                            HeaderStyle-Width="150px" SortExpression="EndDate" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="EqualTo" ShowFilterIcon="false" EnableTimeIndependentFiltering="true" />--%>

                                         <telerik:GridBoundColumn SortExpression="Duration" HeaderText="Duration" HeaderButtonType="TextButton"
                                            DataField="Duration" AutoPostBackOnFilter="true" ShowFilterIcon="false"> <%-- WongS, Modified for #20779 --%>
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn DataField="Lesson" HeaderText="Session" SortExpression="Lesson"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="100%" HeaderStyle-Width="230px" />
                                        <%-- WongS, Modified for #20779 Start --%>
                                        <telerik:GridBoundColumn DataField="Venue" HeaderText="Venue" SortExpression="Venue"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="100%" HeaderStyle-Width="150px"  />
                                        <%-- WongS, Modified for #20779 End --%>
                                        <telerik:GridBoundColumn DataField="InstructorName" HeaderText="Lecturer" SortExpression="InstructorName"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="100%" />

                                          <telerik:GridBoundColumn DataField="StudentGroup" HeaderText="Student group" SortExpression="StudentGroup"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="100%" HeaderStyle-Width="150px" />

                                         <telerik:GridBoundColumn DataField="StudentSubGroup" HeaderText="Student subgroup" SortExpression="StudentSubGroup"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="100%" HeaderStyle-Width="150px"  />

                                        <telerik:GridBoundColumn DataField="ClassName" HeaderText="Class" SortExpression="ClassName"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="100%" HeaderStyle-Width="10px" display="false"/>
                                        
                                        <telerik:GridBoundColumn DataField="Subject" HeaderText="Subject" SortExpression="Subject"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="100%" HeaderStyle-Width="10px" display="false"/>
                                        
                                        <%-- <telerik:GridBoundColumn DataField="Type" HeaderText="Type" SortExpression="Type"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="80%" />--%>
                                        <telerik:GridTemplateColumn HeaderText="Course materials" ShowFilterIcon="false"
                                            HeaderStyle-Width="5%" ItemStyle-Width="5%" Visible="false"> <%-- WongS, Modified for #20779 --%>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDownload" runat="server" Text="Download material" CommandName="Download"
                                                    CommandArgument='<%# Eval("CoursePartID")%>' Visible='<%# IIf(Eval("IsAssignment")=true,false,true) %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
 				<ExportSettings ExportOnlyData="true" >
                                    <Excel Format="Xlsx" FileExtension="xlsx" AutoFitImages="True" />
                                </ExportSettings>
                            </telerik:RadGrid>
                        </asp:Panel>
                       </div>
                        <asp:Label ID="lblGridMsg" runat="server" Text="Record(s) not found" Visible="false"></asp:Label>
                    </td>
                </tr>
               
            </table>
            <%-- </telerik:RadAjaxPanel>--%>
            <%-- <uc1:InstructorValidator ID="InstructorValidator1" runat="server" />--%>
            <cc1:User ID="User1" runat="server"></cc1:User>
            <telerik:RadWindow ID="radDownloadDocuments" runat="server" Width="600px" Height="350px"
                Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                Title="Download documents" Behavior="None">
                <ContentTemplate>
                    <div class="cai-table">
                        <table width="100%">
                            <tr>
                                <td>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                                <td>
                                    
                                    <asp:Panel ID="pnlDownloadDocuments" runat="Server">
                                        <table class="data-form" width="100%">
                                            <tr>
                                                <td class="RightColumn">
                                                     <%--Modified as part of #19968--%>
                                                    <ucRecordAttachment:RecordAttachments__c ID="RecordAttachments__c" runat="server" AllowView="True"
                                                        AllowAdd="false" AllowDelete="false" ViewDescription="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td align="center">
                                    <asp:Button ID="btnClose" runat="server" Text="Cancel" CssClass="submitBtn" />
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </telerik:RadWindow>
        <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</div>

<script type="text/javascript">
    $(function () {
        $('body').css('overflow', 'hidden');
        $('.TelerikModalOverlay').css('width', '100%');
    });

    window.onload = function () {
        if (!$('.RadWindow').is(':visible')) {
            $('body').css('overflow', 'visible');
        }
    };
</script>
