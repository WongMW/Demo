<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Meetings/Calendar.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Meetings.Calendar" %>
<%@ Register Assembly="AptifyEBusinessUser" Namespace="Aptify.Framework.Web.eBusiness"
    TagPrefix="cc1" %>

<div>
    <asp:HyperLink ID="MeetingGridPage" runat="server" Text="Grid View" />
</div>



<div class="calendar-filters">
    <div>
        <asp:Label ID="Label1" runat="server" Text="Month"></asp:Label>

        <asp:DropDownList ID="cmbMonth" runat="server">
            <asp:ListItem Value="1">January</asp:ListItem>
            <asp:ListItem Value="2">February</asp:ListItem>
            <asp:ListItem Value="3">March</asp:ListItem>
            <asp:ListItem Value="4">April</asp:ListItem>
            <asp:ListItem Value="5">May</asp:ListItem>
            <asp:ListItem Value="6">June</asp:ListItem>
            <asp:ListItem Value="7">July</asp:ListItem>
            <asp:ListItem Value="8">August</asp:ListItem>
            <asp:ListItem Value="9">September</asp:ListItem>
            <asp:ListItem Value="10">October</asp:ListItem>
            <asp:ListItem Value="11">November</asp:ListItem>
            <asp:ListItem Value="12">December</asp:ListItem>
        </asp:DropDownList>
    </div>

    <div>
        <asp:Label ID="Label2" runat="server" Text="Year"></asp:Label>
        <asp:DropDownList ID="cmbYear" runat="server">
            <asp:ListItem>2015</asp:ListItem>
            <asp:ListItem>2016</asp:ListItem>
            <asp:ListItem>2017</asp:ListItem>
            <asp:ListItem>2018</asp:ListItem>
            <asp:ListItem>2019</asp:ListItem>
            <asp:ListItem>2020</asp:ListItem>
            <asp:ListItem>2021</asp:ListItem>
            <asp:ListItem>2022</asp:ListItem>
            <asp:ListItem>2023</asp:ListItem>
            <asp:ListItem>2024</asp:ListItem>
        </asp:DropDownList>

    </div>
    <div>
        <asp:Button ID="btnUpdate" runat="server" Font-Size="9pt" Text="Go" CssClass="submitBtn" />
    </div>
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
        <%-- Dilip issue 12721 23/01/2012--%>
    </asp:Calendar>
</div>

<asp:Label ID="Label5" runat="server" Font-Size="9pt" ForeColor="Gray" Height="17px"
    Text="Category: " Visible="False"></asp:Label><asp:DropDownList ID="cmbBottomCategory"
        runat="server" Width="129px" Visible="False">
        <asp:ListItem>All</asp:ListItem>
        <asp:ListItem>Internal</asp:ListItem>
        <asp:ListItem>External</asp:ListItem>
    </asp:DropDownList>
<noscript>
</noscript>

<div style="display: none;">
    <asp:Label ID="Label4" runat="server" Font-Size="9pt" ForeColor="Black" Height="17px"
        Text="Select Month/Year: " Font-Bold="True"></asp:Label>
    <asp:DropDownList ID="cmbBottomMonth"
        runat="server">
        <asp:ListItem Value="1">January</asp:ListItem>
        <asp:ListItem Value="2">February</asp:ListItem>
        <asp:ListItem Value="3">March</asp:ListItem>
        <asp:ListItem Value="4">April</asp:ListItem>
        <asp:ListItem Value="5">May</asp:ListItem>
        <asp:ListItem Value="6">June</asp:ListItem>
        <asp:ListItem Value="7">July</asp:ListItem>
        <asp:ListItem Value="8">August</asp:ListItem>
        <asp:ListItem Value="9">September</asp:ListItem>
        <asp:ListItem Value="10">October</asp:ListItem>
        <asp:ListItem Value="11">November</asp:ListItem>
        <asp:ListItem Value="12">December</asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="cmbBottomYear" runat="server">
             <asp:ListItem>2015</asp:ListItem>
            <asp:ListItem>2016</asp:ListItem>
            <asp:ListItem>2017</asp:ListItem>
            <asp:ListItem>2018</asp:ListItem>
            <asp:ListItem>2019</asp:ListItem>
            <asp:ListItem>2020</asp:ListItem>
            <asp:ListItem>2021</asp:ListItem>
            <asp:ListItem>2022</asp:ListItem>
            <asp:ListItem>2023</asp:ListItem>
            <asp:ListItem>2024</asp:ListItem>
    </asp:DropDownList>
    <%--  Dilip changes--%>
    <asp:Button ID="cmbBottomGo" runat="server" Font-Size="9pt" Text="Go" CssClass="submitBtn" />
</div>
<cc1:User ID="User1" runat="server"></cc1:User>
