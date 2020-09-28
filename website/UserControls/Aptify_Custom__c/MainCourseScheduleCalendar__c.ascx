<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MainCourseScheduleCalendar__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Meetings.MainCourseScheduleCalendar" %>
<%@ Register Assembly="AptifyEBusinessUser" Namespace="Aptify.Framework.Web.eBusiness"
    TagPrefix="cc1" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td align="right" nowrap="nowrap" colspan="3">
            <asp:UpdatePanel ID="UpdatepnlorderDetail" runat="server">
                <ContentTemplate>
                    <asp:Label ID="Label1" runat="server" Font-Size="9pt" ForeColor="Black" Height="17px"
                        Text="Select Curriculum And Year and Time Table: " Font-Bold="True"></asp:Label>&nbsp;
                    <asp:DropDownList ID="cmbCurriculum" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="cmbYear" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:DropDownList ID="drpTimeTable" runat="server" Width="100px">
                    </asp:DropDownList>
                    &nbsp;
                    <%-- Dilip changes 12721--%>
                </ContentTemplate>
            </asp:UpdatePanel>
          </td>
            <td>
                <asp:Button ID="btnUpdate" runat="server" Font-Size="9pt" Text="Go" CssClass="submitBtn" />
                &nbsp;
            </td>
    </tr>
    <tr>
        <td align="center" width="100%" colspan="4">
            <%-- Dilip issue 12721 23/01/2012--%>
            <%--   remove Height="500px" BorderStyle="Solid"--%>
            <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderWidth="2px" Font-Names="Verdana"
                Font-Size="9pt" BorderColor="LightGray" NextPrevFormat="FullMonth" Width="98%"
                NextMonthText="" PrevMonthText="" SelectMonthText="" SelectWeekText="" CellSpacing="1" >
                <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                <WeekendDayStyle HorizontalAlign="Left" VerticalAlign="Top" />
                <TodayDayStyle BackColor="Gainsboro" BorderColor="Maroon" BorderStyle="Solid" BorderWidth="2"
                    HorizontalAlign="Left" VerticalAlign="Top" />
                <OtherMonthDayStyle ForeColor="#999999" HorizontalAlign="Left" VerticalAlign="Top" />
                <DayStyle BackColor="White" HorizontalAlign="Left" VerticalAlign="Top" BorderColor="LightGray"
                    BorderStyle="Solid" BorderWidth="1px" Font-Size="7pt" />
                <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" Font-Strikeout="False"
                    Wrap="True" HorizontalAlign="Left" />
                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt" />
                <%-- Dilip issue 12721 23/01/2012--%>
                <TitleStyle CssClass="tdcalender" />
            </asp:Calendar>
        </td>
    </tr>
</table>
<cc1:User ID="User1" runat="server"></cc1:User>
