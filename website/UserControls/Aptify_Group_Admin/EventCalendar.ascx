<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EventCalendar.ascx.vb"
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

<div style="width: 190px; float:left;">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="right">
                <telerik:RadCalendar ID="RadEventCalendar" runat="server" ViewSelectorText="x" AutoPostBack="True"
                    EnableMultiSelect="False" Width="177px" Height="159px">
                    <WeekendDayStyle CssClass="rcWeekend"></WeekendDayStyle>
                    <CalendarTableStyle CssClass="rcMainTable"></CalendarTableStyle>
                    <OtherMonthDayStyle CssClass="rcOtherMonth"></OtherMonthDayStyle>
                    <OutOfRangeDayStyle CssClass="rcOutOfRange"></OutOfRangeDayStyle>
                    <DisabledDayStyle CssClass="rcDisabled"></DisabledDayStyle>
                    <SelectedDayStyle CssClass="rcSelected"></SelectedDayStyle>
                    <DayOverStyle CssClass="rcHover"></DayOverStyle>
                    <FastNavigationStyle CssClass="RadCalendarMonthView RadCalendarMonthView_Sunset">
                    </FastNavigationStyle>
                    <ViewSelectorStyle CssClass="rcViewSel"></ViewSelectorStyle>
                    <ClientEvents OnDateClick="DateClick" />
                </telerik:RadCalendar>
            </td>
        </tr>
        <tr>
            <td align="left">
            </td>
        </tr>
        <tr>
            <td align="left" style="padding-right:3px;padding-left:9px;">
                <asp:Label ID="lblcaldateinfo" runat="server" Text="Click on dates in the Calendar and view registered events below:"
                    Font-Bold="False" ForeColor="#333333" Font-Size="8pt" Width="100%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblmsg" runat="server" ForeColor="#F27D3E"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" style="padding-left:8px;">
                <asp:Label ID="lblSelectedDate" runat="server" ForeColor="#f27d3e" Font-Size="11pt"
                   ></asp:Label>
            </td>
        </tr>
    </table>
    <div id="firsteventdetails" runat="server">
        <table id="first" width="100%">
            <tr>
                <td style="width: 250px; padding-left:8px; padding-top:10px;">
                    <asp:Label ID="lblEventName" runat="server" Font-Size="9pt" Font-Bold="True" ForeColor="#906227"></asp:Label>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td align="left" style="height: 23px; width: 68px; padding-left:9px;">
                    <asp:Label ID="lblRegisteredCount" runat="server" Font-Bold="True" Text="Registered:"
                        Font-Size="8pt" ForeColor="#333333"></asp:Label>
                </td>
                <td align="left" style="height: 23px; width: 122px; padding-left:7px;">
                    <asp:Label ID="lblRegisteredCt" runat="server" ForeColor="#2e9b09"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 68px;padding-left:9px;">
                    <asp:Label ID="lbleventwaitlist" runat="server" Font-Bold="True" Text="Waitlist:"
                        Font-Size="8pt" ForeColor="#333333"></asp:Label>
                </td>
                <td align="left" style="width: 122px;padding-left:7px;">
                    <asp:Label ID="lblWaitlistCount" runat="server" ForeColor="#f72c04"></asp:Label>
                </td>
                <td align="left" style="padding-right:12px;">
                    <asp:ImageButton ID="imgbtnrefresh" runat="server" Font-Size="9pt" ImageUrl="~/Images/Refresh.png" />
                </td>
            </tr>
        </table>
    </div>
    <div id="secondeventdetails" runat="server">
        <table width="100%">
            <tr>
                <td colspan="3" style="padding-top:10px;">
                    <asp:Image ID="imgdivider" runat="server" ImageUrl="~/Images/Line_daashboard.png"
                        Width="190px" />
                </td>
            </tr>
            <tr>
                <td colspan="3" style="padding-left:9px; padding-top:10px;">
                    <asp:Label ID="lblMeetingTitle" runat="server" Font-Size="9pt" Font-Bold="True"
                        ForeColor="#906227"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="width: 97px">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 69px;padding-left:9px;">
                    <asp:Label ID="lblMeetingRegistered" runat="server" Font-Bold="True" Text="Registered:"
                        Font-Size="8pt" ForeColor="#333333"></asp:Label>
                </td>
                <td align="left" style="width: 122px;padding-left:13px;">
                    <asp:Label ID="lblMRegisteredCt" runat="server" ForeColor="#2e9b09"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 69px;padding-left:9px;">
                    <asp:Label ID="lblMeetingWaitlist" runat="server" Font-Bold="True" Text="Waitlist:"
                        Font-Size="8pt" ForeColor="#333333"></asp:Label>
                </td>
                <td align="left" style="width: 122px;padding-left:13px;">
                    <asp:Label ID="lblMWaitlistCount" runat="server" ForeColor="#f72c04"></asp:Label>
                </td>
                <td align="left" style="padding-right:12px;">
                    <asp:ImageButton ID="imgbtnRefresh2" runat="server" Font-Size="9pt" ImageUrl="~/Images/Refresh.png" />
                </td>
            </tr>
        </table>
    </div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
<cc1:User ID="user1" runat="server" />