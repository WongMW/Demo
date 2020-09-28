<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/MainCourseScheduleCalendar__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Meetings.MainCourseScheduleCalendar" %>
<%@ Register Assembly="AptifyEBusinessUser" Namespace="Aptify.Framework.Web.eBusiness"
    TagPrefix="cc1" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>




    <asp:UpdatePanel ID="UpdatepnlorderDetail" runat="server">
        <ContentTemplate>
            <div class="calendar-filters">
            <div>
                <asp:Label ID="Label1" runat="server" CssClass="label-title" Text="Select Curriculum: "></asp:Label>
                <asp:DropDownList ID="cmbCurriculum" runat="server">
                </asp:DropDownList>
            </div>

            <div>                
                <asp:Label ID="Label2" runat="server" CssClass="label-title" Text="Select Year: "></asp:Label>
                <asp:DropDownList ID="cmbYear" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </div>

            <div>       
                <asp:Label ID="Label3" runat="server" CssClass="label-title" Text="Select Time Table: "></asp:Label>
                <asp:DropDownList ID="drpTimeTable" runat="server">
                </asp:DropDownList>
            </div>

            <div></div>
</div>

        </ContentTemplate>
    </asp:UpdatePanel>

<div class="actions">
    <asp:Button ID="btnUpdate" runat="server" Font-Size="9pt" Text="Go" CssClass="submitBtn" />
</div>

<div class="calendar">
    <asp:Calendar ID="Calendar1" runat="server"
        NextMonthText="Next" PrevMonthText="Prev" SelectMonthText="" SelectWeekText=""
        CellSpacing="1">
        <WeekendDayStyle HorizontalAlign="Left" VerticalAlign="Top" />
        <TitleStyle CssClass="header" />
        <NextPrevStyle CssClass="next-prev-btn" />
        <DayStyle CssClass="day" />
        <SelectedDayStyle CssClass="selected" />
        <TodayDayStyle CssClass="day current" />
    </asp:Calendar>
</div>


<cc1:User ID="User1" runat="server"></cc1:User>
