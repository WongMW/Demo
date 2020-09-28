<%@ Control Language="VB" AutoEventWireup="false" CodeFile="InstructorClasses__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Education.InstructorClasses__c" %>
<%@ Register Src="InstructorValidator__c.ascx" TagName="InstructorValidator" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%--Suraj Issue 14452,5/7/13 Remove the in line css which is not used any where --%>
<style type="text/css">
    /*.RadScheduler .rsHeader
    {
        z-index: 1;
    }*/
</style>
<div class="content-container clearfix">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <table width="100%" id="tabData" runat="server" class="data-form">
            <tr>
                <td>
                    <%--<asp:HyperLink ID="hypCalendar" runat="server" Text="Calendar View" OnClick="ViewCalendar()"></asp:HyperLink>
                <asp:HyperLink ID="hypList" runat="server" Text="List View" OnClick="ViewList()"></asp:HyperLink>--%>
                    <asp:LinkButton ID="lnkCalendar" runat="server" Text="Calendar View"></asp:LinkButton>
                    <asp:LinkButton ID="lnkList" runat="server" Text="List View"></asp:LinkButton>
                </td>
                <td align="right">
                    <asp:DropDownList runat="server" ID="cmbType" AutoPostBack="True">
                        <asp:ListItem>Current Classes</asp:ListItem>
                        <asp:ListItem>Future Classes</asp:ListItem>
                        <asp:ListItem>Past Classes</asp:ListItem>
                        <asp:ListItem>All Classes</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="pnlCalendar" runat="server">                        
                        <telerik:RadScheduler runat="server" ID="radSchedulerLecturer" SelectedView="MonthView"
                            DataKeyField="ClassID" DataStartField="StartDate" DataEndField="EndDateTime"
                            DataSubjectField="ClassNPartName" AppointmentStyleMode="Default" HoursPanelTimeFormat="HH"
                            StartEditingInAdvancedForm="false" Width="100%" Height="90%" Skin="Default" AllowEdit="false"
                            AllowInsert="false" AllowDelete="false" >
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
                        <rad:RadGrid ID="grdClasses" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                            AllowFilteringByColumn="true" SortingSettings-SortedDescToolTip="Sorted Descending"
                            SortingSettings-SortedAscToolTip="Sort Ascending">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AllowSorting="true" AllowNaturalSort="false">
                                <Columns>
                                    <rad:GridBoundColumn DataField="ClassTitle" HeaderText="Class Name" SortExpression="ClassTitle"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        FilterControlWidth="80%" HeaderStyle-Width="270px" />
                                    <rad:GridBoundColumn DataField="Name" HeaderText="Lesson" SortExpression="Name"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        FilterControlWidth="80%" HeaderStyle-Width="270px" />
                                    <rad:GridDateTimeColumn DataField="StartDate" HeaderText="Start Date" FilterControlWidth="80%"
                                        HeaderStyle-Width="270px" SortExpression="StartDate" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="EqualTo" ShowFilterIcon="false" EnableTimeIndependentFiltering="true" />
                                    <rad:GridDateTimeColumn DataField="EndDate" HeaderText="End Date" FilterControlWidth="80%"
                                        HeaderStyle-Width="270px" SortExpression="EndDate" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="EqualTo" ShowFilterIcon="false" EnableTimeIndependentFiltering="true" />
                                    <rad:GridBoundColumn DataField="Type" HeaderText="Type" SortExpression="Type" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%"
                                        HeaderStyle-Width="270px" />
                                </Columns>
                            </MasterTableView>
                        </rad:RadGrid>
                    </asp:Panel>
                    <asp:Label ID="lblGridMsg" runat="server" Text="Record(s) not found" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
    </telerik:RadAjaxPanel>
    <uc1:InstructorValidator ID="InstructorValidator1" runat="server" />
    <cc1:User ID="User1" runat="server"></cc1:User>
</div>
