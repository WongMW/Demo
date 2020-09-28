<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Group_Admin/EventRegistrations.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CustomerService.EventRegistrations" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../Aptify_General/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc1" %>

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
<div class="cai-tabs">
    <telerik:RadTabStrip ID="RadTabEventStrip" SelectedIndex="0" runat="server" MultiPageID="RadMultiPageUpcomingRegistration">
        <Tabs>
            <telerik:RadTab id="t1" runat="server" Selected="True" Font-Bold="true" Text="Upcoming Events" PageViewID="PvUpcomingRegistration">
            </telerik:RadTab>
            <telerik:RadTab id="t2" runat="server" Text="Past Events" Font-Bold="true" PageViewID="PvPastRegistration">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>

    <telerik:RadMultiPage ID="RadMultiPageUpcomingRegistration" runat="server" SelectedIndex="0" CssClass="tabs-body cai-table">
        <telerik:RadPageView ID="PvUpcomingRegistration" runat="server">
            <div class="filters">
                <span class="label-title">Select Month:</span>
                <telerik:RadMonthYearPicker ToolTip="Select a Month" Skin="Default" ID="dtpMonthYearPicker" runat="server" AutoPostBack="True" Culture="en-US"
                    OnSelectedDateChanged="RadMonthYearPicker_SelectedDateChanged">
                    <DateInput ID="DateInput2" runat="server" AutoPostBack="True" DateFormat="MMMM, yyyy"
                        DisplayDateFormat="MMMM, yyyy" DisplayText="" type="text" value="">
                    </DateInput>
                    <DatePopupButton HoverImageUrl="" ToolTip="Select a Month" ImageUrl="" />
                </telerik:RadMonthYearPicker>
                <br />
                <span class="label-title">Or</span><br />
                <span class="label-title">Select Events:</span>
                <asp:DropDownList ID="ddlEvent" runat="server" AutoPostBack="True" ToolTip="Select a Event"
                    CssClass="cmbUserProfileState">
                </asp:DropDownList>
            </div>

            <telerik:RadGrid ID="grdResults" runat="server" AllowPaging="True" AllowSorting="True" EnableViewState="true" AllowFilteringByColumn="true"
                AutoGenerateColumns="False" OnNeedDataSource="grdResults_NeedDataSource" CellSpacing="0" AllowMultiRowSelection="False"
                SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                DataKeyNames="ProductID" GridLines="None" CssClass="event-reg-table">
                <GroupingSettings CaseSensitive="false" />
                <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                <MasterTableView DataKeyNames="MeetingID" AllowFilteringByColumn="True" AllowMultiColumnSorting="false" AllowNaturalSort="false">
                    <%--Amruta,Issue 15349 ,3/25/2013,Changed message from "No child records to display" to "Nothing to display" for child grid--%>
                    <DetailTables>
                        <telerik:GridTableView DataKeyNames="MeetingID" runat="server" AllowFilteringByColumn="false" NoDetailRecordsText="Nothing to display" AllowSorting="false">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="Session" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains"
                                    AllowFiltering="false" DataField="MeetingTitle" ShowFilterIcon="false"
                                    SortExpression="MeetingTitle">
                                    <ItemTemplate>
                                        <span class="mobile-label">Session:</span>
                                        <asp:HyperLink ID="lnkWebName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"MeetingTitle") %>'
                                            NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"MeetingUrl") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="GridDateTimeColumnStartDate" AllowSorting="true"
                                    AllowFiltering="false" Visible="True" HeaderText="Date"
                                    DataField="MonthDate" SortExpression="MonthDate" ShowFilterIcon="false">
                                     <ItemTemplate>
                                        <span class="mobile-label">Date:</span>
                                        <asp:Label ID="lblLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MonthDate")%>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Venue" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains"
                                    AllowFiltering="false" DataField="Venue" ShowFilterIcon="false"
                                    SortExpression="Venue">
                                    <ItemTemplate>
                                        <span class="mobile-label">Venue:</span>
                                        <asp:Label ID="lblLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Venue") %>'></asp:Label>
                                    </ItemTemplate>

                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Registered Members Count" AllowFiltering="false" UniqueName="RegisteredMembersCount"
                                    ShowFilterIcon="false">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <span class="mobile-label">Registered Members Count:</span>
                                        <div id="Table3">
                                            <span class="mobile-label">Registered Members Confirmed:</span>                                
                                            <asp:HyperLink ID="lnkMConfirmedPast" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"AdminConfirmedUrl") %>'
                                                CssClass="MeetingHeader cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"Confirmed")%>'></asp:HyperLink>
                                            <span class="mobile-label">Registered Members WaitList:</span>
                                            <asp:HyperLink ID="lnkMWaitList0" runat="server" CssClass="MeetingHeader cai-table-data" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"AdminWaitListUrl") %>'
                                                Text='<%# DataBinder.Eval(Container.DataItem,"WaitList") %>'></asp:HyperLink>
                                            <span class="mobile-label">Registered Members All:</span>
                                            <asp:HyperLink ID="lnksMAll0" runat="server" CssClass="MeetingHeader cai-table-data" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"AdminAllUrl") %>'
                                                Text='<%# DataBinder.Eval(Container.DataItem,"sAll") %>'></asp:HyperLink>
                                        </div>
                                    </ItemTemplate>

                                </telerik:GridTemplateColumn>
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                    <Columns>
                        <telerik:GridTemplateColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            DataField="MeetingTitle" HeaderText="Event/Session"
                            ShowFilterIcon="false" SortExpression="MeetingTitle">
                            <ItemTemplate>
                                <span class="mobile-label">Event/Session:</span>
                                <asp:HyperLink ID="lnkMeetingTitle" CssClass="cai-table-data" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"AdminMeetingTitleUrl") %>'
                                    Text='<%# DataBinder.Eval(Container.DataItem,"MeetingTitle") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="GridDateTimeColumnStartDate" AllowSorting="true"
                            AllowFiltering="false" Visible="True" HeaderText="Date"
                            DataField="MonthDate" SortExpression="MonthDate" ShowFilterIcon="false">
                            <ItemTemplate>
                                <span class="mobile-label">Date:</span>
                                <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("MonthDate")%>'></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            DataField="Venue" HeaderText="Venue" ShowFilterIcon="false"
                            SortExpression="Venue">
                            <ItemTemplate>
                                <span class="mobile-label">Venue:</span>
                                <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("Venue")%>'></asp:Label>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn AutoPostBackOnFilter="true" DataField="ProductID" AllowFiltering="true"
                            HeaderText="Registered Members Count" SortExpression="ProductID" UniqueName="TemplateColumn">
                            <HeaderTemplate>
                                <span class="mobile-label">Venue:</span>
                                <div id="tblMRegistered">
                                    <span class="label-title">Registered Members Count</span>
                                </div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div id="Table2">
                                    <span class="mobile-label">Registered Members Confirmed:</span>
                                    <asp:HyperLink ID="lnkMConfirmed" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"AdminConfirmedUrl") %>'
                                        CssClass="MeetingHeader  cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"Confirmed")%>'></asp:HyperLink>
                                    <span class="mobile-label">Registered Members WaitList:</span>
                                    <asp:HyperLink ID="lnkMWaitList" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"AdminWaitListUrl") %>'
                                        CssClass="MeetingHeader  cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"WaitList") %>'></asp:HyperLink>
                                    <span class="mobile-label">Registered Members All:</span>
                                    <asp:HyperLink ID="lnksMAll" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"AdminAllUrl") %>'
                                        CssClass="MeetingHeader cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"sAll") %>'></asp:HyperLink>
                                </div>
                            </ItemTemplate>
                            <FilterTemplate>
                                <asp:Label ID="lblid" runat="server" class="CssStatusText" Text="Confirmed"></asp:Label>
                                <asp:Label ID="Label1" runat="server" class="CssStatusText" Text="WaitList"></asp:Label>
                                <asp:Label ID="Label2" runat="server" class="CssStatusText" Text="All"></asp:Label>
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
        </telerik:RadPageView>

        <telerik:RadPageView ID="PvPastRegistration" runat="server">
            <div class="filters">
                <span class="label-title">Select Month:</span>
                <telerik:RadMonthYearPicker ToolTip="Select a Month" Skin="Default" ID="RadMonthYearPickerPast" runat="server"
                    AutoPostBack="True" Culture="en-US" OnSelectedDateChanged="RadMonthYearPickerPast_SelectedDateChanged">
                    <DateInput ID="DateInput2" runat="server" AutoPostBack="True" DateFormat="MMMM, yyyy"
                        DisplayDateFormat="MMMM, yyyy" DisplayText="" type="text" value="">
                    </DateInput>
                    <DatePopupButton HoverImageUrl="" ImageUrl="" ToolTip="Select a Month" />
                </telerik:RadMonthYearPicker>
                <br />
                <span class="label-title">Or</span><br />
                <span class="label-title">Select Events:</span>
                <asp:DropDownList ID="ddlPastEvent" ToolTip="Select a Event" runat="server" AutoPostBack="True"
                    CssClass="cmbUserProfileState">
                </asp:DropDownList>
            </div>

            <telerik:RadGrid ID="grdResultsPast" runat="server" AllowFilteringByColumn="True"
                SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellSpacing="0"
                DataKeyNames="ProductID" GridLines="None" CssClass="event-reg-table">
                <GroupingSettings CaseSensitive="false" />
                <PagerStyle CssClass="sd-pager" />
                <MasterTableView DataKeyNames="MeetingID" AllowFilteringByColumn="true" AllowSorting="true"
                    AllowNaturalSort="false">
                    <DetailTables>
                        <telerik:GridTableView DataKeyNames="MeetingID" runat="server" AllowFilteringByColumn="false" NoDetailRecordsText="Nothing to display" AllowSorting="false">
                            <Columns>
                                <telerik:GridTemplateColumn ItemStyle-CssClass="child" HeaderText="Session" AutoPostBackOnFilter="false"
                                    CurrentFilterFunction="Contains" DataField="MeetingTitle"
                                    ShowFilterIcon="false" SortExpression="MeetingTitle">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnkWebName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"MeetingTitle") %>'
                                            NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"MeetingUrl") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridDateTimeColumn UniqueName="GridDateTimeColumnStartDate" AllowSorting="true"
                                    AllowFiltering="false" Visible="True" HeaderText="Date"
                                    DataField="MonthDate" SortExpression="MonthDate" ShowFilterIcon="false" EnableTimeIndependentFiltering="true">
                                </telerik:GridDateTimeColumn>
                                <telerik:GridTemplateColumn HeaderText="Venue" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains"
                                    DataField="Venue" ShowFilterIcon="false" SortExpression="Venue">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Venue") %>'></asp:Label>
                                    </ItemTemplate>

                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Registered Members Count" ShowFilterIcon="false">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <div id="Table3">
                                            <asp:HyperLink ID="lnkMConfirmedPast" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"AdminConfirmedUrl") %>' CssClass="MeetingHeader" Text='<%# DataBinder.Eval(Container.DataItem,"Confirmed")%>'></asp:HyperLink>

                                            <asp:HyperLink ID="lnkMWaitList0" runat="server" CssClass="MeetingHeader" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"AdminWaitListUrl") %>' Text='<%# DataBinder.Eval(Container.DataItem,"WaitList") %>'></asp:HyperLink>

                                            <asp:HyperLink ID="lnksMAll0" runat="server" CssClass="MeetingHeader" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"AdminAllUrl") %>'
                                                Text='<%# DataBinder.Eval(Container.DataItem,"sAll") %>'></asp:HyperLink>
                                        </div>
                                    </ItemTemplate>

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
                        <telerik:GridTemplateColumn DataField="MeetingTitle" HeaderText="Event/Session"
                            SortExpression="MeetingTitle" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false">
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkMeetingTitlePast" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"AdminMeetingTitleUrlPast") %>'
                                    Text='<%# DataBinder.Eval(Container.DataItem,"MeetingTitle") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridDateTimeColumn UniqueName="GridDateTimeColumnStartDate" AllowSorting="true"
                            AllowFiltering="false" Visible="True" HeaderText="Date"
                            DataField="MonthDate" SortExpression="MonthDate" ShowFilterIcon="false" EnableTimeIndependentFiltering="true">
                        </telerik:GridDateTimeColumn>
                        <telerik:GridBoundColumn DataField="Venue" HeaderText="Venue"
                            SortExpression="Venue" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            ShowFilterIcon="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn AllowFiltering="true" FilterControlAltText="test" HeaderStyle-HorizontalAlign="Center"
                            HeaderText="Registered Members Count" SortExpression="ProductID" UniqueName="TemplateColumn">
                            <HeaderTemplate>
                                <div id="tblMRegisteredPast">
                                    <span class="label-title">Registered Members Count</span>
                                </div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div id="Table3">
                                    <asp:HyperLink ID="lnkMConfirmedPast" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"AdminConfirmedUrlPast") %>'
                                        CssClass="MeetingHeader" Text='<%# DataBinder.Eval(Container.DataItem,"Confirmed")%>'></asp:HyperLink>

                                    <asp:HyperLink ID="lnkMWaitList0" runat="server" CssClass="MeetingHeader" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"AdminWaitListUrlPast") %>'
                                        Text='<%# DataBinder.Eval(Container.DataItem,"WaitList") %>'></asp:HyperLink>

                                    <asp:HyperLink ID="lnksMAll0" runat="server" CssClass="MeetingHeader" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"AdminAllUrlPast") %>' Text='<%# DataBinder.Eval(Container.DataItem,"sAll") %>'></asp:HyperLink>
                                </div>
                            </ItemTemplate>
                            <FilterTemplate>
                                <asp:Label ID="Label4" runat="server" class="CssStatusText" Text="Confirmed"></asp:Label>
                                <asp:Label ID="Label3" runat="server" class="CssStatusText" Text="WaitList"></asp:Label>
                                <asp:Label ID="Label1" runat="server" class="CssStatusText" Text="All"></asp:Label>
                            </FilterTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </telerik:RadPageView>
    </telerik:RadMultiPage>

    <asp:Label ID="lblMessage" runat="server"></asp:Label>
    <cc2:User ID="User1" runat="server" />
</div>
