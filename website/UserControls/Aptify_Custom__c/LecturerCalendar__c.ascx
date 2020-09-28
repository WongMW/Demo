<%@ Control Language="VB" AutoEventWireup="false" CodeFile="LecturerCalendar__c.ascx.vb"
    Inherits="UserControls_Aptify_Custom__c_LecturerCalendar__c" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="ebizuser" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<link href="../../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<link href="../../CSS/DivTag.css" rel="stylesheet" type="text/css" />
<link href="../../CSS/Div_Data_form.css" rel="stylesheet" type="text/css" />
<script src="http://code.jquery.com/jquery-latest.min.js" type="text/javascript"></script>
<telerik:RadScriptBlock runat="server" ID="RadScriptBlock1">
    <script type="text/javascript">
        function pageLoad() {
            //$telerik.$(".rsDateHeader").click(function (e) { return false; });            
        }
        function ToggleDatePicker() {
            debugger
            var chkAllDay = document.getElementById("<%=chkAllDayEvent.ClientID %>")
            if (chkAllDay.checked == true) {
                var radStartTime = $find('<%= radStartTime.ClientID %>');
                var radEndTime = $find('<%= radEndTime.ClientID %>');
                radStartTime.set_enabled(false);
                radEndTime.set_enabled(false);
                radStartTime.get_dateInput().set_value("00:00:00");
                radEndTime.get_dateInput().set_value("23:59:59");
            }
            else if (chkAllDay.checked == false) {
                var radStartTime = $find('<%= radStartTime.ClientID %>');
                var radEndTime = $find('<%= radEndTime.ClientID %>');
                radStartTime.set_enabled(true);
                radEndTime.set_enabled(true);
            }
        }
        function OnClientTimeSlotClick(sender, args) {
            debugger
            var chkAllDay = document.getElementById("<%=chkAllDayEvent.ClientID %>");
            var radWindow = $find("<%= radCalendarUpdate.ClientID %>");
            var radStartDate = $find("<%= radStartDate.ClientID %>");
            var radEndDate = $find("<%= radEndDate.ClientID %>");
            var radStartTime = $find("<%= radStartTime.ClientID %>");
            var radEndTime = $find("<%= radEndTime.ClientID %>");
            var btnSave = document.getElementById("<%= btnSave.ClientID %>");
            var lblMessage = document.getElementById("<%= lblMessage.ClientID %>");
            lblMessage.style.display = "none"
            btnSave.style.display = "inline";
            var rString = "";
            var startTime = sender.get_selectedSlots()[0].get_startTime()
            var selectionLength = sender.get_selectedSlots().length;
            var endTime;
            if (selectionLength == 1) {
                endTime = startTime
            }
            else {
                endTime = sender.get_selectedSlots()[selectionLength - 1].get_startTime()
            }
            radStartDate.set_selectedDate(startTime);
            radEndDate.set_selectedDate(endTime)
            if ((startTime.toTimeString()).split(' ')[0] == "00:00:00") {
                startTime = (startTime.toTimeString()).replace("00:00:00", "00:00:00");
                chkAllDay.checked = true
                radStartTime.set_enabled(false);
                radEndTime.set_enabled(false);
            }
            if ((endTime.toTimeString()).split(' ')[0] == "00:00:00") {
                endTime = (endTime.toTimeString()).replace("00:00:00", "23:59:59");
                chkAllDay.checked = true
                radStartTime.set_enabled(false);
                radEndTime.set_enabled(false);
            }

            radStartTime.get_dateInput().set_value(startTime);
            radEndTime.get_dateInput().set_value(endTime);
            radWindow.show();
        }
    </script>
</telerik:RadScriptBlock>
<style type="text/css">
    .RadScheduler .rsHeader
    {
        height: 30px;
        line-height: 30px;
        position: relative;        
        background-color: Gray;
        background-position: 0 0;
        background-repeat: repeat-x;
    }
    .RadScheduler .rsHeader li
    {
        margin-top: 1px;
    }
    .rsApt .rsAptContent .custom-table
    {
        width: 100%;
        border-collapse: collapse;
    }
    
    .rsApt .rsAptContent .custom-table td
    {
        border: 0;
    }
    
    .demo-container .RadScheduler .appointmentHeader
    {
        padding-bottom: 3px;
        border-bottom: 1px solid #777;
    }
    table.RadCalendar_Default
    {
        background: white;
        font-family: "Segoe UI" ,Arial,Helvetica,sans-serif;
        font-size: 12px;
        width: 200px;
        height: 20px;
    }
    .hidden
    {
        display: none;
    }
    .show
    {
        display: inline;
    }
    .popup_Buttons
    {
        margin: 10px;
    }
    .table
    {
        border-spacing: 5px 5px;
    }
</style>
<div class="content-container clearfix">
    <div>
        <asp:HiddenField ID="hfTabIndex" runat="server" />
        <asp:HiddenField ID="hfTimeOffSet" runat="server" />
        <asp:Label ID="lblPageMessage" runat="server"></asp:Label>
    </div>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <div>
            <telerik:RadWindow ID="radCalendarUpdate" runat="server" Width="600px" Height="380px"
                Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                Title="Calendar Update" Behavior="None">
                <ContentTemplate>
                    <div>
                        <div class="content-container clearfix">
                            <div>
                                <div>
                                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="info-data">
                            <div class="row-div clearfix">
                                <div class="label-div w20">
                                    <span class="required-label">*</span>
                                    <asp:Label ID="lblName" runat="server" Text="Name:" Font-Bold="true"></asp:Label>
                                </div>
                                <div class="field-div w90">
                                    <asp:TextBox ID="txtName" runat="server" Width="70%"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvName" runat="server" ForeColor="Red" ErrorMessage="Name cannot be left blank"
                                        Display="Dynamic" ControlToValidate="txtName" ValidationGroup="btnSave">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row-div clearfix">
                                <div class="label-div w20">
                                    <asp:Label ID="lblDescription" runat="server" Text="Description:" Font-Bold="true"></asp:Label>
                                </div>
                                <div class="field-div w90">
                                    <asp:TextBox ID="txtDescription" runat="server" Width="70%" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row-div clearfix">
                                <div class="label-div w20">
                                    <asp:Label ID="lblAllDayEvent" runat="server" Text="All Day Event:" Font-Bold="true"></asp:Label>
                                </div>
                                <div class="field-div w90">
                                    <asp:CheckBox ID="chkAllDayEvent" runat="server" onclick="ToggleDatePicker()" />
                                </div>
                            </div>
                            <div class="row-div clearfix">
                                <div class="label-div w20">
                                    <asp:Label ID="lblStartDate" runat="server" Text="Start Date:" Font-Bold="true"></asp:Label>
                                </div>
                                <div class="field-div w90">
                                    <telerik:RadDatePicker ID="radStartDate" runat="server" Width="35%">
                                        <Calendar ID="Calendar1" runat="server" EnableKeyboardNavigation="true">
                                        </Calendar>
                                    </telerik:RadDatePicker>
                                    <telerik:RadTimePicker ID="radStartTime" runat="server" TimeView-TimeFormat="HH:mm:ss"
                                        DateInput-DateFormat="HH:mm:ss" DateInput-DisplayDateFormat="HH:mm:ss" Width="35%">
                                    </telerik:RadTimePicker>
                                </div>
                            </div>
                            <div class="row-div clearfix">
                                <div class="label-div w20">
                                    <asp:Label ID="lblEndDate" runat="server" Text="End Date:" Font-Bold="true"></asp:Label>
                                </div>
                                <div class="field-div w90">
                                    <telerik:RadDatePicker ID="radEndDate" runat="server" Width="35%">
                                        <Calendar ID="Calendar2" runat="server" EnableKeyboardNavigation="true">
                                        </Calendar>
                                    </telerik:RadDatePicker>
                                    <telerik:RadTimePicker ID="radEndTime" runat="server" TimeView-TimeFormat="HH:mm:ss"
                                        DateInput-DateFormat="HH:mm:ss" DateInput-DisplayDateFormat="HH:mm:ss" Width="35%">
                                    </telerik:RadTimePicker>
                                </div>
                            </div>
                            <div class="row-div clearfix">
                                <div class="label-div w20">
                                    <br />
                                </div>
                            </div>
                            <div class="row-div clearfix">
                                <div class="label-div w90" align="right">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" Class="submitBtn" ValidationGroup="btnSave"
                                        UseSubmitBehavior="false" OnClientClick="this.disabled='true';" style="display:none" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="submitBtn" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </telerik:RadWindow>
        </div>
        <telerik:RadWindow ID="radWindowPopup" runat="server" Width="350px" Height="150px"
            Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
            Title="Lecturer Calendar" Behavior="None">
            <ContentTemplate>
                <div class="info-data">
                    <div class="row-div clearfix">
                        <b>
                            <asp:Label ID="lblValidationMessage" runat="server" Text=""></asp:Label></b>
                        <br />
                    </div>
                    <div class="row-div clearfix">
                        <br />
                    </div>
                    <div class="row-div clearfix" align="center">
                        <asp:Button ID="btnOk" runat="server" Text="OK" Width="20%" class="submitBtn" />
                        <asp:Button ID="btnDelete" runat="server" Text="OK" Width="20%" class="submitBtn" />
                        <asp:Button ID="btnCancelDelete" runat="server" Text="Cancel" Width="20%" class="submitBtn" />
                    </div>
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
        <div class="info-data">
            <div class="row-div clearfix">
                <div class="label-div w10">
                    <asp:LinkButton ID="lbtnCalendar" runat="server" Text="Calendar View"></asp:LinkButton>
                    <asp:LinkButton ID="lbtnListView" runat="server" Text="List View"></asp:LinkButton>
                </div>
                <div class="label-div w90">
                    <asp:Panel ID="pnlCalendar" runat="server">
                        <telerik:RadScheduler runat="server" ID="radSchedulerLecturer" SelectedView="MonthView"
                            EnableExactTimeRendering="true" DataKeyField="ID" DataStartField="StartDateTime"
                            DataEndField="EndDateTime" DataSubjectField="Name" StartInsertingInAdvancedForm="true"
                            OnClientTimeSlotClick="OnClientTimeSlotClick" OnAppointmentClick="radSchedulerLecturer_AppointmentClick"
                            AppointmentStyleMode="Default" HoursPanelTimeFormat="HH:mm" DayStartTime="08:00:00"
                            DayEndTime="23:59:59" WorkDayStartTime="08:00:00" WorkDayEndTime="20:00:00" CustomAttributeNames="Description,CategoryID"
                            StartEditingInAdvancedForm="true" OverflowBehavior="Expand" Width="100%" Height="90%"
                            Skin="Default" DisplayDeleteConfirmation="false">
                            <AdvancedForm Modal="true"></AdvancedForm>
                            <AppointmentTemplate>
                                <div class="appointmentHeader">
                                    <asp:Label ID="lblTime" runat="server" Font-Bold="true" Text='<%#IIf((Eval("Start","{0:HH:mm:ss}"))="00:00:00","All Day",Eval("Start","{0:HH:mm:ss}") & " - " & Eval("End","{0:HH:mm:ss}")) %>'></asp:Label>
                                </div>
                                <div>
                                    <asp:Label ID="lblSubject" runat="server" Font-Bold="true" Text='<%#Eval("Subject") %>'></asp:Label>
                                </div>
                            </AppointmentTemplate>
                            <AdvancedInsertTemplate>
                                <asp:Label ID="lblTest1" runat="server" Text=""></asp:Label>
                            </AdvancedInsertTemplate>
                            <AdvancedEditTemplate>
                                <asp:Label ID="lblTest" runat="server" Text=""></asp:Label>
                            </AdvancedEditTemplate>
                            <MonthView UserSelectable="true" />
                            <WeekView UserSelectable="false" />
                            <DayView UserSelectable="false" />
                            <MultiDayView UserSelectable="false" />
                            <TimelineView UserSelectable="false" />
                            <TimeSlotContextMenuSettings EnableDefault="false"></TimeSlotContextMenuSettings>
                        </telerik:RadScheduler>
                    </asp:Panel>
                    <asp:Panel ID="pnlListView" runat="server">
                        <telerik:RadGrid ID="gvHolidays" runat="server" AllowPaging="True" AllowSorting="True"
                            AllowFilteringByColumn="True" OnNeedDataSource="gvHolidays_NeedDataSource" AutoGenerateColumns="false">
                            <FilterItemStyle HorizontalAlign="Left" />
                            <MasterTableView>
                                <Columns>
                                    <telerik:GridTemplateColumn DataField="Name" HeaderText="Event" SortExpression="Name"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        FilterControlWidth="100%" HeaderStyle-Width="25%" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEditLink" runat="server" Text='<%# Eval("Name") %>' CommandName="EditEvent"
                                                CommandArgument='<%# Eval("ID") & ";" & Eval("Name")%>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="Description" HeaderText="Description" SortExpression="Description"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        FilterControlWidth="100%" HeaderStyle-Width="25%" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridDateTimeColumn DataField="Start" HeaderText="Start Date" FilterControlWidth="100%"
                                        HeaderStyle-Width="25%" SortExpression="Start" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                        ShowFilterIcon="false" EnableTimeIndependentFiltering="true" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridDateTimeColumn DataField="End" HeaderText="End Date" FilterControlWidth="100%"
                                        HeaderStyle-Width="25%" SortExpression="End" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                        ShowFilterIcon="false" EnableTimeIndependentFiltering="true" ItemStyle-HorizontalAlign="Left" />
                                </Columns>
                            </MasterTableView></telerik:RadGrid>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </telerik:RadAjaxPanel>
</div>
<ebizuser:User ID="LoggedInUser" runat="server" />
