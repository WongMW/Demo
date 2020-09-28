<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/InstructorClasses__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Education.InstructorClasses__c" %>
<%@ Register Src="InstructorValidator__c.ascx" TagName="InstructorValidator" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
 <%--  redmine 13806--%>
<%@ Register Src="~/UserControls/Aptify_Custom__c/RecordAttachments__c.ascx" TagPrefix="ucRecordAttachment"
    TagName="RecordAttachments__c" %>
<div class="content-container clearfix">

    <%--  redmine 13806--%>
   <%-- <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>--%>

    <%-- <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">--%>
        <div id="tabData" runat="server">

            <asp:LinkButton ID="lnkCalendar" runat="server" Text="Calendar View"></asp:LinkButton>
            <asp:LinkButton ID="lnkList" runat="server" Text="List View"></asp:LinkButton>

            <asp:DropDownList runat="server" ID="cmbType" AutoPostBack="True">
                <asp:ListItem>Current Classes</asp:ListItem>
                <asp:ListItem>Future Classes</asp:ListItem>
                <asp:ListItem>Past Classes</asp:ListItem>
                <asp:ListItem>All Classes</asp:ListItem>
            </asp:DropDownList>

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
            <div class="cai-table">
             <%--  redmine 13806--%>
            <asp:Panel ID="pnlList" runat="server">
                <rad:RadGrid ID="grdClasses" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                    AllowFilteringByColumn="false" SortingSettings-SortedDescToolTip="Sorted Descending"
                    SortingSettings-SortedAscToolTip="Sort Ascending">
                    <PagerStyle CssClass="sd-pager" />
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView AllowSorting="false" AllowNaturalSort="false" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left">
                        <Columns>
                            <%-- <rad:GridTemplateColumn DataField="ClassTitle" HeaderText="Class name" SortExpression="ClassTitle"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" visible="false">
                                 <ItemTemplate>
                                    <span class="mobile-label">Class name:</span>
                                    <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("ClassTitle")%>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>--%>
                            <rad:GridTemplateColumn DataField="Name" HeaderText="Lesson" SortExpression="Name"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-HorizontalAlign="left">
                                 <ItemTemplate>
                                    <span class="mobile-label" style="text-align:left;">Lesson:</span>
                                    <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                             <rad:GridTemplateColumn DataField="GroupName" HeaderText="Group Name" SortExpression="GroupName"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <ItemTemplate>
                                    <span class="mobile-label">Group Name:</span>
                                    <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("GroupName")%>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                              <rad:GridTemplateColumn DataField="SubGroup1" HeaderText="Sub Group 1" SortExpression="SubGroup1"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <ItemTemplate>
                                    <span class="mobile-label">Sub Group1:</span>
                                    <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("SubGroup1")%>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn DataField="StartDate" HeaderText="Start date"
                                SortExpression="StartDate" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo" ShowFilterIcon="false" ItemStyle-HorizontalAlign="left">
                                 <ItemTemplate>
                                    <span class="mobile-label" style="text-align:left;">Start date:</span>
                                    <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("StartDate")%>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn DataField="EndDate" HeaderText="End date"
                                SortExpression="EndDate" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo" ShowFilterIcon="false" ItemStyle-HorizontalAlign="left">
                                 <ItemTemplate>
                                    <span class="mobile-label" style="text-align:left;">End date:</span>
                                    <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("EndDate")%>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn DataField="Venue" HeaderText="Venue" SortExpression="Venue"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <ItemTemplate>
                                    <span class="mobile-label">Venue:</span>
                                    <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("Venue")%>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Materials" ShowFilterIcon="false"
                                HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" CommandName="Download"
                                        CommandArgument='<%# Eval("CoursePartID")%>' Visible='<%# IIf(Eval("IsAssignment")=true,false,true) %>'></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <%--  <rad:GridTemplateColumn DataField="Type" HeaderText="Type" SortExpression="Type" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" >
                                 <ItemTemplate>
                                    <span class="mobile-label">Type:</span>
                                    <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("Type")%>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>--%>
                        </Columns>
                    </MasterTableView>
                </rad:RadGrid>
            </asp:Panel>
            </div>
            <asp:Label ID="lblGridMsg" runat="server" Text="Record(s) not found" Visible="false"></asp:Label>

        </div>
     <%--</telerik:RadAjaxPanel>--%>
    <uc1:InstructorValidator ID="InstructorValidator1" runat="server" />
    <cc1:User ID="User1" runat="server"></cc1:User>
    <%--  redmine 13806--%>
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
                                                    <%--Modified as part of #19968--%>
                                                    <ucRecordAttachment:RecordAttachments__c ID="RecordAttachments__c" runat="server" AllowView="True"
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
</div>
