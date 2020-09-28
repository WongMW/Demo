<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Group_Admin/EventCalendar.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Admin.EventCalendar" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<script language="javascript" type="text/javascript">
    function DateClick(sender, args) {
        if (args.get_renderDay().IsSelected)
            args.set_cancel(true);
    }
</script>

<asp:UpdatePanel ID="update1" runat="server">
    <ContentTemplate>
        <div>
            <telerik:RadCalendar ID="RadEventCalendar" runat="server" ViewSelectorText="x" AutoPostBack="True"
                EnableMultiSelect="False" ShowRowHeaders="false">
                <WeekendDayStyle CssClass="rcWeekend"></WeekendDayStyle>
                <CalendarTableStyle CssClass="rcMainTable"></CalendarTableStyle>
                <OtherMonthDayStyle CssClass="rcOtherMonth"></OtherMonthDayStyle>
                <OutOfRangeDayStyle CssClass="rcOutOfRange"></OutOfRangeDayStyle>
                <DisabledDayStyle CssClass="rcDisabled"></DisabledDayStyle>
                <SelectedDayStyle CssClass="rcSelected"></SelectedDayStyle>
                <DayOverStyle CssClass="rcHover"></DayOverStyle>
                <FastNavigationStyle CssClass="RadCalendarMonthView RadCalendarMonthView_Sunset"></FastNavigationStyle>
                <ViewSelectorStyle CssClass="rcViewSel"></ViewSelectorStyle>
                <ClientEvents OnDateClick="DateClick" />
            </telerik:RadCalendar>

            <asp:Label ID="lblcaldateinfo" runat="server" Text="Click on dates in the Calendar and view registered events" Width="100%"></asp:Label>

            <asp:Label ID="lblmsg" runat="server"></asp:Label>

            <asp:Label ID="lblSelectedDate" runat="server"></asp:Label>

            <div id="firsteventdetails" runat="server">

                <asp:Label ID="lblEventName" runat="server"></asp:Label>

                <asp:Label ID="lblRegisteredCount" runat="server" Text="Registered:"></asp:Label>
                <asp:Label ID="lblRegisteredCt" runat="server"></asp:Label>

                <asp:Label ID="lbleventwaitlist" runat="server" Text="Waitlist:"></asp:Label>

                <asp:Label ID="lblWaitlistCount" runat="server"></asp:Label>

                <asp:ImageButton ID="imgbtnrefresh" runat="server" ImageUrl="~/Images/Refresh.png" />

            </div>
            <div id="secondeventdetails" runat="server">

                <asp:Image ID="imgdivider" runat="server" ImageUrl="~/Images/Line_daashboard.png" Width="190px" />

                <asp:Label ID="lblMeetingTitle" runat="server"></asp:Label>

                <asp:Label ID="lblMeetingRegistered" runat="server" Text="Registered:"></asp:Label>

                <asp:Label ID="lblMRegisteredCt" runat="server"></asp:Label>

                <asp:Label ID="lblMeetingWaitlist" runat="server" Text="Waitlist:"></asp:Label>

                <asp:Label ID="lblMWaitlistCount" runat="server"></asp:Label>

                <asp:ImageButton ID="imgbtnRefresh2" runat="server" ImageUrl="~/Images/Refresh.png" />

            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<cc1:User ID="user1" runat="server" />
