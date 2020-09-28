<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ClassesScheduler__c.ascx.vb"
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
    <asp:UpdatePanel ID="UpdatepnlorderDetail" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <table width="100%" id="tabData" runat="server" class="data-form">
                <tr>
                    <td>
                        <asp:LinkButton ID="lnkCalendar" runat="server" Text="Calendar View"></asp:LinkButton>
                        <asp:LinkButton ID="lnkList" runat="server" Text="List View"></asp:LinkButton>
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
                        <asp:Panel ID="pnlList" runat="server">
                            <telerik:RadGrid ID="grdClasses" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                                AllowFilteringByColumn="true" SortingSettings-SortedDescToolTip="Sorted Descending"
                                SortingSettings-SortedAscToolTip="Sort Ascending">
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView AllowSorting="true" AllowNaturalSort="false">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="ClassName" HeaderText="Class" SortExpression="ClassName"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="100%" HeaderStyle-Width="230px" />
                                        <telerik:GridBoundColumn DataField="Lesson" HeaderText="Lesson" SortExpression="Lesson"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="100%" HeaderStyle-Width="230px" />
                                        <telerik:GridBoundColumn DataField="Subject" HeaderText="Subject" SortExpression="Subject"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="100%" HeaderStyle-Width="150px" />
                                        <telerik:GridDateTimeColumn DataField="StartDate" HeaderText="Start Date" FilterControlWidth="100%"
                                            SortExpression="StartDate" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                            HeaderStyle-Width="150px" ShowFilterIcon="false" EnableTimeIndependentFiltering="true" />
                                        <telerik:GridDateTimeColumn DataField="EndDate" HeaderText="End Date" FilterControlWidth="100%"
                                            HeaderStyle-Width="150px" SortExpression="EndDate" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="EqualTo" ShowFilterIcon="false" EnableTimeIndependentFiltering="true" />
                                        <telerik:GridBoundColumn SortExpression="Duration" HeaderText="Duration" HeaderButtonType="TextButton"
                                            DataField="Duration" AutoPostBackOnFilter="true" ShowFilterIcon="false" HeaderStyle-Width="5%"
                                            ItemStyle-Width="5%">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="InstructorName" HeaderText="Instructor" SortExpression="InstructorName"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="100%" />
                                        <%-- <telerik:GridBoundColumn DataField="Type" HeaderText="Type" SortExpression="Type"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="80%" />--%>
                                        <telerik:GridTemplateColumn HeaderText="Course Materials" ShowFilterIcon="false"
                                            HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" CommandName="Download"
                                                    CommandArgument='<%# Eval("CoursePartID")%>' Visible='<%# IIf(Eval("IsAssignment")=true,false,true) %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </asp:Panel>
                        <asp:Label ID="lblGridMsg" runat="server" Text="Record(s) not found" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;" colspan="2">
                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="submitBtn" />
                    </td>
                </tr>
            </table>
            <%-- </telerik:RadAjaxPanel>--%>
            <%-- <uc1:InstructorValidator ID="InstructorValidator1" runat="server" />--%>
            <cc1:User ID="User1" runat="server"></cc1:User>
            <telerik:RadWindow ID="radDownloadDocuments" runat="server" Width="500px" Height="300px"
                Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                Title="Download Documents" Behavior="None">
                <ContentTemplate>
                    <div>
                        <table width="100%">
                            <tr>
                                <td>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td width="5%">
                                </td>
                                <td width="90%">
                                    <b>Documents</b><br />
                                    <asp:Panel ID="pnlDownloadDocuments" runat="Server" Style="border: 1px Solid #000000;">
                                        <table class="data-form" width="100%">
                                            <tr>
                                                <td class="RightColumn">
                                                    <ucRecordAttachment:RecordAttachments__c ID="ucDownload" runat="server" AllowView="True"
                                                        AllowAdd="false" AllowDelete="false" ViewDescription="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td width="5%">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td align="right">
                                    <asp:Button ID="btnClose" runat="server" Text="Cancel" CssClass="submitBtn" />
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </telerik:RadWindow>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
