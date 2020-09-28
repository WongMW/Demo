<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/LecturerCalendar__c.ascx.vb"
    Inherits="UserControls_Aptify_Custom__c_LecturerCalendar__c" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="ebizuser" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
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
            <telerik:RadWindow ID="radCalendarUpdate" runat="server" Width="500px" Height="580px"
                Modal="True" VisibleStatusbar="False" Behaviors="None"
                Title="Calendar Update" Behavior="None" CssClass="cai-form">
                <ContentTemplate>
                    <div>
                        <div class="info-data">
                            <div>
                                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>

                                <span ID="lblName" runat="server" class="label-title"><span class="required-label">*</span>Name:</span>

                                <asp:TextBox ID="txtName" runat="server" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvName" runat="server" ForeColor="Red" ErrorMessage="Name cannot be left blank"
                                    Display="Dynamic" ControlToValidate="txtName" ValidationGroup="btnSave">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div>
                                <asp:Label ID="lblDescription" runat="server" Text="Description:" CssClass="label-title"></asp:Label>
                                <asp:TextBox ID="txtDescription" runat="server"  TextMode="MultiLine" ></asp:TextBox>
                            </div>
                            <div>
                                <asp:Label ID="Label1" runat="server" Text="All Day Event" CssClass="label-title"></asp:Label>
                                <asp:Label ID="lblAllDayEvent" runat="server"  CssClass="">
                                    <asp:CheckBox ID="chkAllDayEvent" runat="server" onclick="ToggleDatePicker()" />
                                   
                                </asp:Label>
                                
                            </div>
                            <div>

                                <asp:Label ID="lblStartDate" runat="server" Text="Start Date:"  CssClass="label-title"></asp:Label>

                                <telerik:RadDatePicker ID="radStartDate" runat="server" >
                                    <Calendar ID="Calendar1" runat="server" EnableKeyboardNavigation="true">
                                    </Calendar>
                                </telerik:RadDatePicker>
                                <telerik:RadTimePicker ID="radStartTime" runat="server" TimeView-TimeFormat="HH:mm:ss"
                                    DateInput-DateFormat="HH:mm:ss" DateInput-DisplayDateFormat="HH:mm:ss" >
                                </telerik:RadTimePicker>

                            </div>
                            <div>
                                <asp:Label ID="lblEndDate" runat="server" Text="End Date:"  CssClass="label-title"></asp:Label>

                                <telerik:RadDatePicker ID="radEndDate" runat="server" >
                                    <Calendar ID="Calendar2" runat="server" EnableKeyboardNavigation="true">
                                    </Calendar>
                                </telerik:RadDatePicker>
                                <telerik:RadTimePicker ID="radEndTime" runat="server" TimeView-TimeFormat="HH:mm:ss"
                                    DateInput-DateFormat="HH:mm:ss" DateInput-DisplayDateFormat="HH:mm:ss" >
                                </telerik:RadTimePicker>
                                <div class="actions">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" Class="submitBtn" ValidationGroup="btnSave"
                                        UseSubmitBehavior="false" OnClientClick="this.disabled='true';" Style="display: none" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="submitBtn" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </telerik:RadWindow>
        </div>
        <telerik:RadWindow ID="radWindowPopup" runat="server" Width="350px" Height="150px"
            Modal="True" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
            Title="Lecturer Calendar" Behavior="None">
            <ContentTemplate>
                <div class="info-data">
                    <div>
                        <asp:Label ID="lblValidationMessage" runat="server" Text=""></asp:Label>
                    </div>

                    <div class="actions">
                        <asp:Button ID="btnOk" runat="server" Text="OK" class="submitBtn" />
                        <asp:Button ID="btnDelete" runat="server" Text="OK" class="submitBtn" />
                        <asp:Button ID="btnCancelDelete" runat="server" Text="Cancel" class="submitBtn" />
                    </div>
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
        <div class="info-data">
            <div>
                <div class="actions">
                    <asp:LinkButton ID="lbtnCalendar" runat="server"  Text="Calendar View"></asp:LinkButton>
                    <asp:LinkButton ID="lbtnListView" runat="server"  Text="List View"></asp:LinkButton>
                </div>
                <div class="calendar">
                    <asp:Panel ID="pnlCalendar" runat="server">
                        <telerik:RadScheduler runat="server" ID="radSchedulerLecturer" SelectedView="MonthView"
                            EnableExactTimeRendering="true" DataKeyField="ID" DataStartField="StartDateTime"
                            DataEndField="EndDateTime" DataSubjectField="Name" StartInsertingInAdvancedForm="true"
                            OnClientTimeSlotClick="OnClientTimeSlotClick" OnAppointmentClick="radSchedulerLecturer_AppointmentClick"
                            AppointmentStyleMode="Default" HoursPanelTimeFormat="HH:mm" DayStartTime="08:00:00"
                            DayEndTime="23:59:59" WorkDayStartTime="08:00:00" WorkDayEndTime="20:00:00" CustomAttributeNames="Description,CategoryID"
                            StartEditingInAdvancedForm="true" OverflowBehavior="Expand" Width="100%" Height="90%"
                            Skin="Default" DisplayDeleteConfirmation="false" >
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
                            AllowFilteringByColumn="True" OnNeedDataSource="gvHolidays_NeedDataSource" AutoGenerateColumns="false" CssClass="cai-table">
                            <PagerStyle CssClass="sd-pager" />
                            <MasterTableView>
                                <Columns>
                                    <telerik:GridTemplateColumn DataField="Name" HeaderText="Event" SortExpression="Name"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <span class="mobile-label">Name:</span>
                                            <asp:LinkButton ID="lnkEditLink" runat="server" Text='<%# Eval("Name") %>' CommandName="EditEvent"
                                                CommandArgument='<%# Eval("ID") & ";" & Eval("Name")%>' CssClass="cai-table-data" ></asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                     <telerik:GridTemplateColumn DataField="Description" HeaderText="Description" SortExpression="Description"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" >
                                        <ItemTemplate>
                                            <span class="mobile-label">Description:</span>
                                            <asp:Label runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"Description") %>'></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn DataField="Start" HeaderText="Start Date"
                                        SortExpression="Start" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                        ShowFilterIcon="false" >
                                         <ItemTemplate>
                                            <span class="mobile-label">Start Date:</span>
                                            <asp:Label runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "Start")%>'></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn DataField="End" HeaderText="End Date"
                                        SortExpression="End" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                        ShowFilterIcon="false" >
                                         <ItemTemplate>
                                            <span class="mobile-label">End Date:</span>
                                            <asp:Label runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "End")%>'></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </telerik:RadAjaxPanel>
</div>
<ebizuser:User ID="LoggedInUser" runat="server" />
