<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EventRegistrations.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CustomerService.EventRegistrations" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../Aptify_General/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>
<style type="text/css">
    .rcOkButton
    {
        margin-right: 4px !important;
    }
    .rcTodayButton
    {
        display: none;
    }
    .RadDateWidth
    {
        width: 146px !important;
    }
    .RadTabStripTop_Sunset .rtsSelected
    {
        background-repeat: no-repeat;
        background: none !important;
    }
    .RadTabStrip .rtsLink, .RadTabStripVertical .rtsLink
    {
        background-repeat: no-repeat;
        background: none !important;
    }
    #content ul
    {
        font-size: 11px;
        padding: 14px 0 0;
    }
    .RadTabStrip .rtsLink, .RadTabStripVertical .rtsLink
    {
        padding-left: 0;
        padding-right: 0;
    }
</style>
<!-- content start -->
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadMonthYearPicker1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadMonthYearPicker"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings> 
</telerik:RadAjaxManager>
<!-- content end -->
<div>
    <table width="100%">
        <tr>
            <td align="left" colspan="3">
                <telerik:RadTabStrip ID="RadTabEventStrip" Width="100%" Skin="Sunset" SelectedIndex="0"
                    runat="server" MultiPageID="RadMultiPageUpcomingRegistration" EnableEmbeddedSkins="true"
                    Height="20px">
                    <Tabs>
                        <telerik:RadTab id="t1" runat="server" Width="139px" Selected="True" Font-Bold="true"
                            Text="Upcoming Events" PageViewID="PvUpcomingRegistration">
                        </telerik:RadTab>
                        <telerik:RadTab id="t2" runat="server" Width="139px" Text="Past Events" Font-Bold="true"
                            PageViewID="PvPastRegistration">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <br />
            </td>
        </tr>
    </table>
</div>
<div class="CSSForTdBottom" style="padding-top:6px !important;" >
    <asp:Image ID="Image2" CssClass="ImgBarlineWidth" ImageUrl="~/Images/BarLine.png"
        runat="server" />
</div>
<div>
    <telerik:RadMultiPage ID="RadMultiPageUpcomingRegistration" runat="server" SelectedIndex="0">
        <telerik:RadPageView ID="PvUpcomingRegistration" runat="server">
            <div>
                <table class="CSSForTdBottom" width="60%">
                    <tr>
                        <td style="padding-left: 4px;">
                            <b>Select Month:</b>&nbsp;
                            <telerik:RadMonthYearPicker ToolTip="Select a Month" Skin="Default" Width="163px"
                                Height="20px" ID="dtpMonthYearPicker" runat="server" AutoPostBack="True" Culture="en-US"
                                OnSelectedDateChanged="RadMonthYearPicker_SelectedDateChanged">
                                <DateInput ID="DateInput2" runat="server" AutoPostBack="True" DateFormat="MMMM, yyyy"
                                    DisplayDateFormat="MMMM, yyyy" DisplayText="" type="text" value="">
                                </DateInput>
                                <DatePopupButton HoverImageUrl="" ToolTip="Select a Month" ImageUrl="" />
                            </telerik:RadMonthYearPicker>
                        </td>
                        <td style="width: 40px">
                            <b>Or</b>
                        </td>
                        <td class="RightColumn" style="padding-left: 3px;">
                            <b>Select Events:</b>&nbsp;
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEvent" runat="server" AutoPostBack="True" ToolTip="Select a Event"
                                CssClass="cmbUserProfileState" Width="175px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table width="100%" style="padding-left: 4px;">
                    <tr>
                        <td>
                            <telerik:RadGrid ID="grdResults" runat="server" AllowPaging="True" AllowSorting="True" EnableViewState="true"  AllowFilteringByColumn="true"
                                AutoGenerateColumns="False" OnNeedDataSource="grdResults_NeedDataSource" CellSpacing="0" AllowMultiRowSelection="False" 
                                SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                DataKeyNames="ProductID" GridLines="None" Width="99%">
                                <GroupingSettings CaseSensitive="false" />
                                <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                                <MasterTableView DataKeyNames="MeetingID" AllowFilteringByColumn="True" AllowMultiColumnSorting="false" AllowNaturalSort="false">
                                 <%--Amruta,Issue 15349 ,3/25/2013,Changed message from "No child records to display" to "Nothing to display" for child grid--%>
                                    <DetailTables>
                                        <telerik:GridTableView DataKeyNames="MeetingID" Width="100%" runat="server" AllowFilteringByColumn="false" NoDetailRecordsText="Nothing to display" AllowSorting="false" >
                                            <Columns>
                                                <telerik:GridTemplateColumn HeaderText="Session" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains"
                                                    AllowFiltering="false" DataField="MeetingTitle" FilterControlWidth="150px" ShowFilterIcon="false"
                                                    SortExpression="MeetingTitle">
                                                    <HeaderStyle Width="224px" />
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="lnkWebName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"MeetingTitle") %>'
                                                            NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"MeetingUrl") %>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridDateTimeColumn UniqueName="GridDateTimeColumnStartDate" AllowSorting="true"
                                                    AllowFiltering="false" FilterControlWidth="183px" Visible="True" HeaderText="Date"
                                                    DataField="MonthDate" SortExpression="MonthDate" ShowFilterIcon="false" EnableTimeIndependentFiltering="true">
                                                    <ItemStyle VerticalAlign="top" />
                                                    <HeaderStyle Width="183px" />
                                                </telerik:GridDateTimeColumn>
                                                <telerik:GridTemplateColumn HeaderText="Venue" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains"
                                                    AllowFiltering="false" DataField="Venue" FilterControlWidth="150px" ShowFilterIcon="false"
                                                    SortExpression="Venue">
                                                    <HeaderStyle Width="228px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Venue") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="top" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Registered Members Count"  AllowFiltering="false" UniqueName="RegisteredMembersCount"
                                                    ShowFilterIcon="false">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <table id="Table3" border="0" width="100%">
                                                            <tr>
                                                                <td style="padding-left: 21px; width: 35%">
                                                                    <center>
                                                                        <asp:HyperLink ID="lnkMConfirmedPast" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"AdminConfirmedUrl") %>'
                                                                            CssClass="MeetingHeader" Text='<%# DataBinder.Eval(Container.DataItem,"Confirmed")%>'></asp:HyperLink>
                                                                    </center>
                                                                </td>
                                                                <td style="padding-left: 8px; width: 35%">
                                                                    <center>
                                                                        <asp:HyperLink ID="lnkMWaitList0" runat="server" CssClass="MeetingHeader" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"AdminWaitListUrl") %>'
                                                                            Text='<%# DataBinder.Eval(Container.DataItem,"WaitList") %>'></asp:HyperLink>
                                                                    </center>
                                                                </td>
                                                                <td style="width: 35%" valign="top">
                                                                    <center>
                                                                        <asp:HyperLink ID="lnksMAll0" runat="server" CssClass="MeetingHeader" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"AdminAllUrl") %>'
                                                                            Text='<%# DataBinder.Eval(Container.DataItem,"sAll") %>'></asp:HyperLink>
                                                                    </center>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="top" />
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </telerik:GridTableView>
                                    </DetailTables>
                                    <Columns>
                                        <telerik:GridTemplateColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                            DataField="MeetingTitle" FilterControlWidth="175px" HeaderText="Event/Session"
                                            ShowFilterIcon="false" SortExpression="MeetingTitle">
                                            <HeaderStyle />
                                            <ItemStyle CssClass="RadFieldWidth" />
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkMeetingTitle" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"AdminMeetingTitleUrl") %>'
                                                    Text='<%# DataBinder.Eval(Container.DataItem,"MeetingTitle") %>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridDateTimeColumn UniqueName="GridDateTimeColumnStartDate" AllowSorting="true"
                                            FilterControlWidth="140px" AllowFiltering="false" Visible="True" HeaderText="Date"
                                            DataField="MonthDate" SortExpression="MonthDate" ShowFilterIcon="false" EnableTimeIndependentFiltering="true">
                                            <ItemStyle CssClass="RadDateWidth" />
                                        </telerik:GridDateTimeColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                            DataField="Venue" HeaderText="Venue" FilterControlWidth="150px" ShowFilterIcon="false"
                                            SortExpression="Venue">
                                            <ItemStyle CssClass="RadFieldWidth" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn AutoPostBackOnFilter="true" DataField="ProductID" AllowFiltering="true"
                                            HeaderText="Registered Members Count" SortExpression="ProductID" UniqueName="TemplateColumn">
                                            <ItemStyle CssClass="RadCountFieldWidth" />
                                            <HeaderTemplate>
                                                <table id="tblMRegistered" cellspacing="1" width="100%">
                                                    <tr>
                                                        <td align="center"colspan="3">
                                                            <b>
                                                                <center>
                                                                    Registered Members Count</center>
                                                            </b>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table id="Table2" border="0" width="100%">
                                                    <tr>
                                                        <td style="padding-left: 21px; width: 35%">
                                                            <center>
                                                                <asp:HyperLink ID="lnkMConfirmed" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"AdminConfirmedUrl") %>'
                                                                    CssClass="MeetingHeader" Text='<%# DataBinder.Eval(Container.DataItem,"Confirmed")%>'></asp:HyperLink>
                                                            </center>
                                                        </td>
                                                        <td style="width: 35%">
                                                            <center>
                                                                <asp:HyperLink ID="lnkMWaitList" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"AdminWaitListUrl") %>'
                                                                    CssClass="MeetingHeader" Text='<%# DataBinder.Eval(Container.DataItem,"WaitList") %>'></asp:HyperLink>
                                                            </center>
                                                        </td>
                                                        <td align="center" style="width: 35%">
                                                            <asp:HyperLink ID="lnksMAll" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"AdminAllUrl") %>'
                                                                CssClass="MeetingHeader" Text='<%# DataBinder.Eval(Container.DataItem,"sAll") %>'></asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <FilterTemplate>
                                                <table border="0" width="100%">
                                                    <tr>
                                                        <td width="35%">
                                                            <center>
                                                                <asp:Label ID="lblid" runat="server" class="CssStatusText" Text="Confirmed"></asp:Label>
                                                            </center>
                                                        </td>
                                                        <td align="center" width="35%">
                                                            <asp:Label ID="Label1" runat="server" class="CssStatusText" Text="WaitList"></asp:Label>
                                                        </td>
                                                        <td align="center" width="35%">
                                                            <center>
                                                                <asp:Label ID="Label2" runat="server" class="CssStatusText" Text="All"></asp:Label>
                                                            </center>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FilterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                    <EditFormSettings>
                                        <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                        </EditColumn>
                                    </EditFormSettings>
                                </MasterTableView>
                                <FilterMenu EnableImageSprites="False">
                                </FilterMenu>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="PvPastRegistration" runat="server" BorderStyle="None">
            <div>
                <table class="CSSForTdBottom" width="60%">
                    <tr>
                        <td style="padding-left: 20px;" class="tdCSSForTd" colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 4px;">
                            <b>Select Month:</b>&nbsp;&nbsp;
                            <telerik:RadMonthYearPicker ToolTip="Select a Month" Skin="Default" Width="163px"
                                Height="20px" ID="RadMonthYearPickerPast" runat="server" AutoPostBack="True"
                                Culture="en-US" OnSelectedDateChanged="RadMonthYearPickerPast_SelectedDateChanged">
                                <DateInput ID="DateInput2" runat="server" AutoPostBack="True" DateFormat="MMMM, yyyy"
                                    DisplayDateFormat="MMMM, yyyy" DisplayText="" type="text" value="">
                                </DateInput>
                                <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Select a Month" />
                            </telerik:RadMonthYearPicker>
                        </td>
                        <td style="width: 40px">
                            <b>Or</b>
                        </td>
                        <td class="RightColumn">
                            <b>Select Events:</b>&nbsp;
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPastEvent" ToolTip="Select a Event" runat="server" AutoPostBack="True"
                                CssClass="cmbUserProfileState" Width="175px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdCSSForTd" colspan="3">
                        </td>
                    </tr>
                </table>
                <table style="padding-left: 4px;" width="100%">
                    <tr>
                        <td>
                            <telerik:RadGrid ID="grdResultsPast" runat="server" AllowFilteringByColumn="True"
                                SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellSpacing="0"
                                DataKeyNames="ProductID" GridLines="None" Width="99%">
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView DataKeyNames="MeetingID" AllowFilteringByColumn="true" AllowSorting="true"
                                    AllowNaturalSort="false">
                                    <DetailTables>
                                        <telerik:GridTableView DataKeyNames="MeetingID" Width="100%" runat="server" AllowFilteringByColumn="false" NoDetailRecordsText="Nothing to display" AllowSorting ="false">
                                            <Columns>
                                                <telerik:GridTemplateColumn ItemStyle-CssClass="child" HeaderText="Session" AutoPostBackOnFilter="false"
                                                    CurrentFilterFunction="Contains" DataField="MeetingTitle" FilterControlWidth="150px"
                                                    ShowFilterIcon="false" SortExpression="MeetingTitle">
                                                    <HeaderStyle Width="226px" />
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="lnkWebName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"MeetingTitle") %>'
                                                            NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"MeetingUrl") %>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridDateTimeColumn UniqueName="GridDateTimeColumnStartDate" AllowSorting="true"
                                                    FilterControlWidth="182px" AllowFiltering="false" Visible="True" HeaderText="Date"
                                                    DataField="MonthDate" SortExpression="MonthDate" ShowFilterIcon="false" EnableTimeIndependentFiltering="true">
                                                    <HeaderStyle Width="182px" />
                                                </telerik:GridDateTimeColumn>
                                                <telerik:GridTemplateColumn HeaderText="Venue" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains"
                                                    DataField="Venue" FilterControlWidth="150px" ShowFilterIcon="false" SortExpression="Venue">
                                                    <HeaderStyle Width="228px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Venue") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="top" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Registered Members Count" ShowFilterIcon="false">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <table id="Table3" border="0" width="100%">
                                                            <tr>
                                                                <td style="padding-left: 25px; width: 7%;">
                                                                    <center>
                                                                        <asp:HyperLink ID="lnkMConfirmedPast" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"AdminConfirmedUrl") %>'
                                                                            CssClass="MeetingHeader" Text='<%# DataBinder.Eval(Container.DataItem,"Confirmed")%>'></asp:HyperLink>
                                                                    </center>
                                                                </td>
                                                                <td style="padding-left: 45px; width: 35%">
                                                                    <center>
                                                                        <asp:HyperLink ID="lnkMWaitList0" runat="server" CssClass="MeetingHeader" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"AdminWaitListUrl") %>'
                                                                            Text='<%# DataBinder.Eval(Container.DataItem,"WaitList") %>'></asp:HyperLink>
                                                                    </center>
                                                                </td>
                                                                <td style="width: 29%" valign="top">
                                                                    <center>
                                                                        <asp:HyperLink ID="lnksMAll0" runat="server" CssClass="MeetingHeader" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"AdminAllUrl") %>'
                                                                            Text='<%# DataBinder.Eval(Container.DataItem,"sAll") %>'></asp:HyperLink>
                                                                    </center>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="top" />
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </telerik:GridTableView>
                                    </DetailTables>
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="ID" HeaderButtonType="TextButton" SortExpression="MeetingID"
                                            DataField="Productid" UniqueName="ID" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                            ShowFilterIcon="false" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="Product" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Productid") %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn DataField="MeetingTitle" HeaderText="Event/Session" FilterControlWidth="175px"
                                            SortExpression="MeetingTitle" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                            ShowFilterIcon="false">
                                            <ItemStyle CssClass="RadFieldWidth" />
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkMeetingTitlePast" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"AdminMeetingTitleUrlPast") %>'
                                                    Text='<%# DataBinder.Eval(Container.DataItem,"MeetingTitle") %>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridDateTimeColumn UniqueName="GridDateTimeColumnStartDate" AllowSorting="true"
                                            FilterControlWidth="100px" AllowFiltering="false" Visible="True" HeaderText="Date"
                                            DataField="MonthDate" SortExpression="MonthDate" ShowFilterIcon="false" EnableTimeIndependentFiltering="true">
                                            <ItemStyle CssClass="RadDateWidth" />
                                        </telerik:GridDateTimeColumn>
                                        <telerik:GridBoundColumn DataField="Venue" HeaderText="Venue" FilterControlWidth="150px"
                                            SortExpression="Venue" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                            ShowFilterIcon="false">
                                            <ItemStyle CssClass="RadFieldWidth" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="true" FilterControlAltText="test" HeaderStyle-HorizontalAlign="Center"
                                            HeaderText="Registered Members Count" SortExpression="ProductID" UniqueName="TemplateColumn">
                                            <ItemStyle CssClass="RadCountFieldWidth" />
                                            <HeaderTemplate>
                                                <table id="tblMRegisteredPast" cellspacing="1" width="100%">
                                                    <tr>
                                                        <td align="center" colspan="3">
                                                            <center>
                                                                <b>Registered Members Count</b></center>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table id="Table3" border="0" width="100%">
                                                    <tr>
                                                        <td style="padding-left: 0px; width: 23%">
                                                            <center>
                                                                <asp:HyperLink ID="lnkMConfirmedPast" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"AdminConfirmedUrlPast") %>'
                                                                    CssClass="MeetingHeader" Text='<%# DataBinder.Eval(Container.DataItem,"Confirmed")%>'></asp:HyperLink>
                                                            </center>
                                                        </td>
                                                        <td style="padding-left: 20px; width: 35%">
                                                            <center>
                                                                <asp:HyperLink ID="lnkMWaitList0" runat="server" CssClass="MeetingHeader" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"AdminWaitListUrlPast") %>'
                                                                    Text='<%# DataBinder.Eval(Container.DataItem,"WaitList") %>'></asp:HyperLink>
                                                            </center>
                                                        </td>
                                                        <td style="width: 35%" valign="top">
                                                            <center>
                                                                <asp:HyperLink ID="lnksMAll0" runat="server" CssClass="MeetingHeader" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"AdminAllUrlPast") %>'
                                                                    Text='<%# DataBinder.Eval(Container.DataItem,"sAll") %>'></asp:HyperLink>
                                                            </center>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <FilterTemplate>
                                                <table border="0" width="100%">
                                                    <tr>
                                                        <td width="22%">
                                                            <center>
                                                                <b>
                                                                    <asp:Label ID="Label4" runat="server" class="CssStatusText" Text="Confirmed"></asp:Label>
                                                                </b>
                                                            </center>
                                                        </td>
                                                        <td width="35%">
                                                            <center>
                                                                <b>
                                                                    <asp:Label ID="Label3" runat="server" class="CssStatusText" Text="WaitList"></asp:Label>
                                                                </b>
                                                            </center>
                                                        </td>
                                                        <td align="center" width="35%">
                                                            <b>
                                                                <asp:Label ID="Label1" runat="server" class="CssStatusText" Text="All"></asp:Label></b>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FilterTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </div>
        </telerik:RadPageView>
    </telerik:RadMultiPage></div>
<div>
    <asp:Label ID="lblMessage" runat="server"></asp:Label>
    <cc2:User ID="User1" runat="server" />
</div>
