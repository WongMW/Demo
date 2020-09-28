<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Calendar.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Meetings.Calendar" %>
<%@ Register Assembly="AptifyEBusinessUser" Namespace="Aptify.Framework.Web.eBusiness"
    TagPrefix="cc1" %>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td align="left" width="50%" style="padding-left: 5px;">
            &nbsp;<asp:HyperLink ID="MeetingGridPage" runat="server" Text="Grid View" />
        </td>
      <%-- Amruta IssueID 15386,3/4/2013,Revert Meeting Center Calendar View Back to 5.5 Version--%>
            <%--<td align="center" class="contentSm" style="height: 30px" width="30%">
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Label ID="Label2" runat="server" Font-Size="9pt" ForeColor="Gray" Height="17px"
                    Text="Category: " Visible="False"></asp:Label>&nbsp;<asp:DropDownList ID="DropDownList1" runat="server"
                        Width="129px" Visible="False">
                        <asp:ListItem>All</asp:ListItem>
                        <asp:ListItem>Internal</asp:ListItem>
                        <asp:ListItem>External</asp:ListItem>
                    </asp:DropDownList><noscript></noscript></td>--%>
            <td align="right" class="contentSm" style="width: 50%; height: 40px; padding-right: 10px;">
            <asp:Label ID="Label1" runat="server" Font-Size="9pt" ForeColor="Black" Height="17px"
                Text="Select Month/Year: " Font-Bold="True"></asp:Label>&nbsp;
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
            &nbsp;<asp:DropDownList ID="cmbYear" runat="server">
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
            <%-- Dilip changes 12721--%>
            <asp:Button ID="btnUpdate" runat="server" Font-Size="9pt" Text="Go" CssClass="submitBtn" />&nbsp;
        </td>
    </tr>
    <tr>
        <td align="center" width="100%" colspan="4">
            <%-- Dilip issue 12721 23/01/2012--%>
            <%--   remove Height="500px" BorderStyle="Solid"--%>
            <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderWidth="2px" 
                 Font-Names="Verdana" Font-Size="9pt" BorderColor="LightGray"
                 NextPrevFormat="FullMonth" Width="98%" 
                NextMonthText="" PrevMonthText="" SelectMonthText="" SelectWeekText="" 
                CellSpacing="1" >
                <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                <WeekendDayStyle HorizontalAlign="Left" VerticalAlign="Top" /> 
                <TodayDayStyle BackColor="Gainsboro" BorderColor="Maroon" BorderStyle="Solid" BorderWidth="2" HorizontalAlign="Left" VerticalAlign="Top" />
                <OtherMonthDayStyle ForeColor="#999999" HorizontalAlign="Left" VerticalAlign="Top" />
                <DayStyle BackColor="White" HorizontalAlign="Left" VerticalAlign="Top" BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px" Font-Size="7pt" />
                <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" Font-Strikeout="False" Wrap="True" HorizontalAlign="Left" />
                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt" />
                <%-- Dilip issue 12721 23/01/2012--%>
                <TitleStyle CssClass="tdcalender" />
            </asp:Calendar>
        </td>
    </tr>
    <tr>
        <td align="center" class="contentSm" style="height: 30px" width="30%">
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:Label ID="Label5" runat="server" Font-Size="9pt" ForeColor="Gray" Height="17px"
                Text="Category: " Visible="False"></asp:Label>&nbsp;<asp:DropDownList ID="cmbBottomCategory"
                    runat="server" Width="129px" Visible="False">
                    <asp:ListItem>All</asp:ListItem>
                    <asp:ListItem>Internal</asp:ListItem>
                    <asp:ListItem>External</asp:ListItem>
                </asp:DropDownList>
            <noscript>
            </noscript>
        </td>
        <td align="right" class="contentSm" style="width: 39%; height: 22px; padding-right: 8px;">
            <asp:Label ID="Label4" runat="server" Font-Size="9pt" ForeColor="Black" Height="17px"
                Text="Select Month/Year: " Font-Bold="True"></asp:Label>&nbsp;<asp:DropDownList ID="cmbBottomMonth"
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
            &nbsp;<asp:DropDownList ID="cmbBottomYear" runat="server">
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
             <asp:Button ID="cmbBottomGo" runat="server" Font-Size="9pt" Text="Go" CssClass="submitBtn" />&nbsp;
        </td>
    </tr>
</table>
<cc1:User ID="User1" runat="server"></cc1:User>
