<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/ViewMyClasses__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Meetings.ViewMyClasses__c" %>
<%@ Register Assembly="AptifyEBusinessUser" Namespace="Aptify.Framework.Web.eBusiness"
    TagPrefix="cc1" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<script language="javascript" type="text/javascript">
    function ViewCalendar() {
        //alert("Hi");
        var pnlCalendar = document.getElementById("<% =pnlCalendar.ClientID %>")
        var pnlList = document.getElementById("<% =pnlList.ClientID %>")
        var hypCalendar = document.getElementById("<% =hypCalendar.ClientID %>")
        var hypList = document.getElementById("<% =hypList.ClientID %>")
        pnlCalendar.style.display = "block";
        pnlList.style.display = "none";
        hypCalendar.style.display = "none";
        hypList.style.display = "block";
        //alert("Hii");
    }

    function ViewList() {
        //alert("Hi");
        var pnlCalendar = document.getElementById("<% =pnlCalendar.ClientID %>")
        var pnlList = document.getElementById("<% =pnlList.ClientID %>")
        var hypCalendar = document.getElementById("<% =hypCalendar.ClientID %>")
        var hypList = document.getElementById("<% =hypList.ClientID %>")
        pnlCalendar.style.display = "none";
        pnlList.style.display = "block";
        hypCalendar.style.display = "block";
        hypList.style.display = "none";
        //alert("Hii");
    }
</script>
<table border="0" cellpadding="0" cellspacing="0" width="100%" id="tabData">
    <tr>
        <td align="right" nowrap="nowrap">
            <%--<asp:UpdatePanel ID="UpdatepnlorderDetail" runat="server">
                <ContentTemplate>--%>
            <asp:Label ID="Label1" runat="server" Font-Size=" 9pt" ForeColor="Black" Height="17px"
                Text="Select Academic Cycle: " Font-Bold="True"></asp:Label>&nbsp;
            <asp:DropDownList ID="cmbAcademicCycle" runat="server" AutoPostBack="true">
            </asp:DropDownList>
            <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
        </td>
    </tr>
    <tr>
        <td nowrap="nowrap">
            <asp:HyperLink ID="hypCalendar" runat="server" Text="Calendar View" OnClick="ViewCalendar()"></asp:HyperLink>
            <asp:HyperLink ID="hypList" runat="server" Text="List View" OnClick="ViewList()"></asp:HyperLink>
            <%--<asp:LinkButton ID="lnkCalendar" runat="server" Text="Calenda View" OnClientClick="ViewCalendar()"></asp:LinkButton>--%>
            <%--<asp:LinkButton ID="lnkList" runat="server" Text="List View"></asp:LinkButton>--%>
        </td>
    </tr>
    <tr>
        <td align="center" width="100%">
            <asp:Panel ID="pnlCalendar" runat="server">
                <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderWidth="2px" Font-Names="Verdana"
                    Font-Size="9pt" BorderColor="LightGray" NextPrevFormat="FullMonth" Width="98%"
                    NextMonthText="" PrevMonthText="" SelectMonthText="" SelectWeekText="" CellSpacing="1">
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
                    <TitleStyle CssClass="tdcalender" />
                </asp:Calendar>
            </asp:Panel>
            <asp:Panel ID="pnlList" runat="server">
                <rad:RadGrid ID="grdClasses" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                    AllowFilteringByColumn="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sort Ascending">
                    <PagerStyle CssClass="sd-pager" />
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView AllowSorting="true" AllowNaturalSort="false">
                        <Columns>
                            <rad:GridTemplateColumn DataField="ClassTitle" SortExpression="Committee" HeaderText="Class Name" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%" HeaderStyle-Width="170px">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkCommittee" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ClassTitle") %>'
                                        NavigateUrl='<%# GetNavigationURL(DataBinder.Eval(Container.DataItem,"ClassID").ToString()) %>' />
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridBoundColumn DataField="Name" HeaderText="Class Part" SortExpression="CommitteeTerm" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%" HeaderStyle-Width="270px" />
                            <rad:GridBoundColumn DataField="StartDate" HeaderText="Start Date" SortExpression="StartDate" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%" HeaderStyle-Width="270px" />
                            <rad:GridBoundColumn DataField="EndDate" HeaderText="End Date" SortExpression="EndDate" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%" HeaderStyle-Width="270px" />
                        </Columns>
                    </MasterTableView>
                </rad:RadGrid>
            </asp:Panel>
            <asp:Label ID="lblGridMsg" runat="server" Text="No record found" Visible="false"></asp:Label>
        </td>
    </tr>
</table>
<cc1:User ID="User1" runat="server"></cc1:User>
