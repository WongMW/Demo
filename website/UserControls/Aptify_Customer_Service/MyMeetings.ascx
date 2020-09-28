<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MyMeetings.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.CustomerService.MyMeetings" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%--<%@ Register Src="MeetingActionControl.ascx" TagName="MeetingActionControl" TagPrefix="uc1" %>--%>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%--Anil B Issue 11155 and notification--%>
<script type="text/javascript">
    function HideControl() {
        var Open = document.getElementById("<%= Open.ClientID %>");
        var HiddenOpen = document.getElementById("<%= HiddenOpen.ClientID %>")
        var hdnShowNotification = document.getElementById("<%= hdnShowNotification.ClientID %>");
        hdnShowNotification.value = "HideControl";

        if (HiddenOpen.value == "None") {
            Open.style.display = "inherit";
            HiddenOpen.value = "inherit";
        }
        else {

            Open.style.display = "None";
            HiddenOpen.value = "None";
        }

    }

    function HideNotification() {
        var hdnShowNotification = document.getElementById("<%= hdnShowNotification.ClientID %>");
        if (hdnShowNotification.value != "HideControl" && hdnShowNotification.value != "ShowNotification") {
            var Open = document.getElementById("<%= Open.ClientID %>");
            var HiddenOpen = document.getElementById("<%= HiddenOpen.ClientID %>")
            Open.style.display = "None";
            HiddenOpen.value = "None";
        }

        hdnShowNotification.value = "";
    }

    function ShowNotification() {
        var hdnShowNotification = document.getElementById("<%= hdnShowNotification.ClientID %>");
        if (hdnShowNotification.value != "CloseNotification") {
            var Open = document.getElementById("<%= Open.ClientID %>");
            var HiddenOpen = document.getElementById("<%= HiddenOpen.ClientID %>")
            Open.style.display = "inherit";
            HiddenOpen.value = "inherit";

            hdnShowNotification.value = "ShowNotification";
        }
    }

    window.onload = function () {
        document.body.onclick = HideNotification;
    }

    function CloseNotification() {
        var hdnShowNotification = document.getElementById("<%= hdnShowNotification.ClientID %>");
        var Open = document.getElementById("<%= Open.ClientID %>");
        var HiddenOpen = document.getElementById("<%= HiddenOpen.ClientID %>")
        Open.style.display = "None";
        HiddenOpen.value = "None";
        hdnShowNotification.value = "CloseNotification";
    }

    window.onload = function () {
        document.body.onclick = HideNotification;
    }



</script>
<div align="right" style="z-index: -1 Important;" class="dvTopNotificationDiv">
    <ul class="ttw-notification-menu">
        <li class="notification-menu-item first-item" id="Li1"><a>Meetings Notification </a>
            <span class="notification-bubble" onclick="HideControl();" title="Notifications"
                style="background-color: rgb(245, 108, 126); display: inline;">
                <asp:Label ID="lblTotalNotificationCount" runat="server" Text="0"></asp:Label>
            </span></li>
    </ul>
</div>
<%--Anil B For 11155 on 16-04-2013
Implement wait processing--%>
<div class="dvUpdateProgress">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" AssociatedUpdatePanelID="UpdatePanel2" 
        DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing">
                <table class="tblFullHeightWidth">
                    <tr>
                        <td class="tdProcessing">
                            Please wait...
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
 <%--  Anil B for issue 11155 on 3-05-2013
   Add update progress for grid--%>
<div class="dvUpdateProgress">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3" 
        DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing">
                <table class="tblFullHeightWidth">
                    <tr>
                        <td class="tdProcessing">
                            Please wait...
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<div class="content-container clearfix dvMyMeetingSetAlign">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="True">
        <ContentTemplate>
            <table id="tblMain" runat="server" class="data-form">
            <tr>
             <%--  Anil B for issue 11155 on 3-05-2013
                       Added wrapper for grid info--%>
            <td  colspan="5">
            <div style="width:900px; overflow:hidden;">
            <asp:Label ID="lblSearchInfo"  runat="server" Font-Bold="true" ></asp:Label>
            </div>
            </td>
            </tr>
                <tr>
                    <td width="400px">
                        <%--  Anil B for issue 11155 on 21-03-2013
                    Changed rad combobox to asp dropdown--%>
                        <b>Select Meeting:</b>
                        <asp:DropDownList runat="server" ID="cmbCategory"  AutoPostBack="true" CssClass="DDLTelMyMeeting">
                            <%--Anil B For 11155 on 16-04-2013
                           Change categories--%>
                            <asp:ListItem Text="All"></asp:ListItem>
                            <asp:ListItem Text="Upcoming"></asp:ListItem>
                            <asp:ListItem Text="Past"></asp:ListItem>
                        </asp:DropDownList>
                        <b style="padding-left: 10px;">Select Status:</b>
                        <asp:DropDownList runat="server" ID="cmbStatus" CssClass="DDLTelMyMeeting">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <%--Sandeep issue 14450 8/3/13  merge <td>--%>
                        <%--Anil B For 11155 on 16-04-2013
                           Change Control into datetime picker and used radtooltip control--%>
                        <b>Start Date: </b>                      
                        <telerik:RadDatePicker ID="txtStartDate"    runat="server">
                            <DatePopupButton ToolTip="" />
                        </telerik:RadDatePicker>
                        <telerik:RadToolTip ID="RadToolTipStartDate" runat="server" IsClientID="true" Text="Find Meetings that start on or after the specified date and time."
                            AutoCloseDelay="20000" />
                    </td>
                    <td>
                        <%--Anil B For 11155 on 16-04-2013
                           Change Control into datetime picker and used radtooltip control--%>
                        <b>End Date:</b>
                        <telerik:RadDatePicker ID="txtEndDate" runat="server" title="title" ToolTip="Find Meetings that End on or before the specified date."
                            CssClass="datePicker">
                        </telerik:RadDatePicker>
                        <telerik:RadToolTip ID="RadToolTipEndtDate" runat="server" IsClientID="true" Text="Find Meetings that End on or before the specified date and time."
                            AutoCloseDelay="20000" />
                    </td>
                    <td>
                     <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="True">
                    <ContentTemplate>
                        <asp:Button runat="server" ID="btnSearch" CssClass="submitBtn" Text="Search" />
                        </ContentTemplate>
                    <Triggers>          
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                     </Triggers>
    </asp:UpdatePanel>
                    </td>
                    <td align="right">
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <%--Suraj issue 14450 2/8/13  removed three step sorting ,added tooltip and added date column--%>
                        <%--  Anil B for issue 11155 on 21-03-2013
                            Remove filtering of grid--%>
                        <%--Anil B For 11155 on 16-04-2013
                          Used detail grid--%>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" ChildrenAsTriggers="True">
                            <ContentTemplate>
                        <rad:RadGrid ID="grdMeetings" runat="server" AutoGenerateColumns="False" AllowFilteringByColumn="False"
                            AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                            PageSize="5" PagerStyle-PageSizeLabelText="Records Per Page" AllowSorting="True">
                            <GroupingSettings CaseSensitive="false" />
                            <SortingSettings SortedAscToolTip="Sorted Ascending" SortedDescToolTip="Sorted Descending" />
                            <MasterTableView DataKeyNames="MeetingID" AllowNaturalSort="false" AllowSorting="True"
                                AllowPaging="True">
                                <DetailTables>
                                    <telerik:GridTableView DataKeyNames="MeetingID" Width="100%" runat="server" AllowFilteringByColumn="false"
                                        AllowNaturalSort="false" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                        SortingSettings-SortedDescToolTip="Sorted Descending" NoDetailRecordsText="Nothing to display">
                                        <Columns>
                                            <rad:GridTemplateColumn HeaderText="Name" DataField="WebName" FilterControlWidth="200px"
                                                SortExpression="WebName" HeaderStyle-Width="250px" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lnkWebName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebName") %>'
                                                        NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"MeetingUrl") %>'></asp:HyperLink>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn HeaderText="Location" DataField="Location" FilterControlWidth="150px"
                                                SortExpression="Location" HeaderStyle-Width="150px" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Location") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="top" />
                                            </rad:GridTemplateColumn>
                                            <rad:GridDateTimeColumn DataField="StartDate" UniqueName="GridDateTimeColumnStartDate"
                                                HeaderText="Start Date" FilterControlWidth="150px" SortExpression="StartDate"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" DataType="System.DateTime"
                                                ShowFilterIcon="false" />
                                            <rad:GridDateTimeColumn DataField="EndDate" UniqueName="GridDateTimeColumnEndDate"
                                                HeaderText="End Date" FilterControlWidth="150px" SortExpression="EndDate" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="EqualTo" DataType="System.DateTime" ShowFilterIcon="false"
                                                EnableTimeIndependentFiltering="true" />
                                            <rad:GridTemplateColumn HeaderText="Status" DataField="Status" AllowFiltering="true"
                                                FilterControlWidth="100px" HeaderStyle-Width="100px" SortExpression="Status"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Status") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="top" />
                                            </rad:GridTemplateColumn>
                                        </Columns>
                                    </telerik:GridTableView>
                                </DetailTables>
                                <Columns>
                                    <rad:GridTemplateColumn HeaderText="Name" DataField="WebName" FilterControlWidth="200px"
                                        SortExpression="WebName" HeaderStyle-Width="250px" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkWebName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebName") %>'
                                                NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"MeetingUrl") %>'></asp:HyperLink>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Location" DataField="Location" FilterControlWidth="150px"
                                        SortExpression="Location" HeaderStyle-Width="150px" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Location") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="top" />
                                    </rad:GridTemplateColumn>
                                    <rad:GridDateTimeColumn DataField="StartDate" UniqueName="GridDateTimeColumnStartDate"
                                        HeaderText="Start Date" FilterControlWidth="150px" SortExpression="StartDate"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" DataType="System.DateTime"
                                        ShowFilterIcon="false" />
                                    <rad:GridDateTimeColumn DataField="EndDate" UniqueName="GridDateTimeColumnEndDate"
                                        HeaderText="End Date" FilterControlWidth="150px" SortExpression="EndDate" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="EqualTo" DataType="System.DateTime" ShowFilterIcon="false"
                                        EnableTimeIndependentFiltering="true" />
                                    <rad:GridTemplateColumn HeaderText="Status" DataField="Status" AllowFiltering="true"
                                        FilterControlWidth="100px" HeaderStyle-Width="100px" SortExpression="Status"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Status") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="top" />
                                    </rad:GridTemplateColumn>
                                    <rad:GridBoundColumn DataField="OrderID" HeaderText="Comment Text" Visible="false">
                                    </rad:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </rad:RadGrid>
                        </ContentTemplate>                   
                    </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="cmbCategory" />
            <%--<asp:AsyncPostBackTrigger ControlID="btnSearch" />--%>
        </Triggers>
    </asp:UpdatePanel>
</div>
<div id="Open" runat="server" class="notification-list-wrapper dvNotificationPosition"
    style="display: none" onclick="ShowNotification();">
    <div class="uiScrollableAreaTrack" style="min-height: 67px; max-height: 400px; overflow-y: auto;
        overflow-x: hidden; background-color: #D0D0D0;">
        <div>
            <div class="dvNotification">
                <asp:HiddenField ID="hdnShowNotification" runat="server" Value="HideControl" />
                <asp:Label runat="server" ID="lblNotificationDivHeading" CssClass="lblNotificationHeading"
                    Text="Your Registered Event"></asp:Label>
                <div style="float: right; cursor: pointer;">
                    <img id="ImgClose" runat="server" onclick="CloseNotification();" /></div>
            </div>
            <div class="dvMidLocationOfNotification">
                <asp:HiddenField ID="HiddenOpen" runat="server" Value="None" />
                <asp:BulletedList ID="blListUpcomingMeetings" BulletStyle="NotSet" runat="server"
                    CssClass="notification-list-menu">
                </asp:BulletedList>
            </div>
        </div>
        <div>
            <div class="dvNotification">
                <asp:Label runat="server" ID="lblNotificationForYou" CssClass="lblNotificationHeading"
                    Text="Upcoming Event"></asp:Label>
            </div>
            <div class="dvMidLocationOfNotification">
                <asp:BulletedList ID="blListMeetingsforyou" BulletStyle="NotSet" runat="server" CssClass="notification-list-menu">
                </asp:BulletedList>
            </div>
        </div>
        <%--'Suraj S Issue 15426,4/15/13 , remove the Announcements section from the My Meetings page.--%>
        <div class="dvlastNotification">
        </div>
    </div>
</div>
<cc1:User ID="User1" runat="server" />
