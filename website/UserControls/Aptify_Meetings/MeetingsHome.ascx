<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MeetingsHome.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Meetings.MeetingsHome" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="cc6" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="content-container clearfix">
    <%--Nalini Issue 12436 date:01/12/2011--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table id="tblMain" runat="server" class="data-form">
                <tr>
                    <%--   Dilip changes issue 12717--%>
                    <td>
                        <b>Meeting Category:</b>
                        <asp:DropDownList runat="server" ID="cmbCategory" AutoPostBack="true">
                        </asp:DropDownList>
                        &nbsp;
                        <asp:DropDownList runat="server" ID="cmbStatus" AutoPostBack="true">
                            <asp:ListItem Text="Upcoming"></asp:ListItem>
                            <asp:ListItem Text="Past"></asp:ListItem>
                            <asp:ListItem Text="All"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        <asp:HyperLink ID="MeetingsCalendarPage" runat="server" Text="Calendar View" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblMessage" runat="server" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <rad:RadGrid ID="grdMeetings" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                            AllowFilteringByColumn="True" AllowSorting="True">
                            <GroupingSettings CaseSensitive="false" />
                            <SortingSettings SortedAscToolTip="Sorted Ascending" SortedDescToolTip="Sorted Descending" />
                            <MasterTableView DataKeyNames="ID" AllowFilteringByColumn="True" AllowNaturalSort="false"
                                AllowSorting="True" AllowPaging="True">
                                <DetailTables>
                                    <%-- suraj Issue 14457 ,  3/7/13 Remove sort expression from all the child gride,Issue  14829 , 4/19/13 , add Name="ChildGrid" for Gridtableview  --%>
                                    <%--Amruta,Issue 15349 ,3/25/2013,Changed message from "No child records to display" to "Nothing to display" for child grid--%>
                                    <telerik:GridTableView DataKeyNames="ID"  Name="ChildGrid" Width="100%" runat="server" AllowFilteringByColumn="false"
                                        AllowNaturalSort="false" SortingSettings-SortedDescToolTip="Sorted Descending"
                                        SortingSettings-SortedAscToolTip="Sorted Ascending" AllowPaging="false" NoDetailRecordsText="Nothing to display">
                                        <CommandItemSettings ExportToPdfText="Export to PDF" />
                                        <Columns>
                                            <rad:GridTemplateColumn HeaderText="Session" SortExpression="WebName" AllowFiltering="false"
                                                DataField="WebName" FilterControlWidth="60px" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lnkWebName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebName") %>'
                                                        NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"MeetingUrl") %>'></asp:HyperLink>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="top" Width="60px" />
                                                <HeaderStyle CssClass="DetailTablesSessionColumn" />
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn HeaderText="Category" SortExpression="WebCategoryName" DataField="WebCategoryName"
                                                AllowFiltering="false" FilterControlWidth="60px" HeaderStyle-Width="60px" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWebCategoryName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebCategoryName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="top" />
                                            </rad:GridTemplateColumn>
                                           <%--  Suraj Issue 14457 4/29/13, change UniqueName also remove DataFormatString --%>
                                            <rad:GridDateTimeColumn AllowFiltering="false" SortExpression="AvailableUntil" UniqueName="GridDateTimeColumnRegisteredDateDetails"
                                                FilterControlWidth="100px" Visible="True" HeaderText="Registered By Date" DataField="AvailableUntil"
                                                AutoPostBackOnFilter="false" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                                DataType="System.DateTime"  EnableTimeIndependentFiltering="true">
                                                <HeaderStyle CssClass="DetailTablesRegisteredByDate" />
                                                <ItemStyle VerticalAlign="top" Width="100px" />
                                            </rad:GridDateTimeColumn>
                                            <%-- suraj Issue 14457 , 4/25/13 Remove HeaderTooltip --%>
                                            <rad:GridTemplateColumn HeaderText="Description" DataField="Smalldesc" AllowFiltering="false"
                                                FilterControlWidth="80px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains"
                                                SortExpression="" HeaderTooltip="" ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSmalldesc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Smalldesc") %>'></asp:Label>
                                                    <rad:RadToolTip ID="RadToolTip1" runat="server" TargetControlID="lblSmalldesc" Animation="Slide"
                                                        RelativeTo="Element" Position="BottomCenter" RenderInPageRoot="true">
                                                        <%# DataBinder.Eval(Container.DataItem, "VerboseDescription")%>
                                                    </rad:RadToolTip>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="DetailTablesDescription" />
                                                <ItemStyle VerticalAlign="top" />
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn HeaderText="Location" SortExpression="Location" DataField="Location"
                                                AllowFiltering="false" FilterControlWidth="70px" AutoPostBackOnFilter="false"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Location") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="DetailTablesLocation" />
                                                <ItemStyle VerticalAlign="top" />
                                            </rad:GridTemplateColumn>
                                            <%--  Suraj Issue 14457 4/29/13, change UniqueName  also remove DataFormatString--%>
                                            <rad:GridDateTimeColumn UniqueName="GridDateTimeColumnStartDateDetails" SortExpression="StartDate"
                                                AllowFiltering="false" Visible="True" HeaderText="Start Date" DataField="StartDate"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ReadOnly="true" FilterControlWidth="90px"
                                                ShowFilterIcon="false"  DataType="System.DateTime"
                                                EnableTimeIndependentFiltering="true">
                                                <ItemStyle Width="90px" VerticalAlign="Top" />
                                                <HeaderStyle CssClass="DetailTablesOnorAfter" />
                                            </rad:GridDateTimeColumn>
                                            <%--  Suraj Issue 14457 4/29/13, change UniqueName  also remove DataFormatString--%>
                                            <rad:GridDateTimeColumn UniqueName="GridDateTimeColumnEndDateDetails" SortExpression="EndDate"
                                                FilterControlWidth="90px" AllowFiltering="false" Visible="True" HeaderText="End Date"
                                                DataField="EndDate" ReadOnly="true" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                                ShowFilterIcon="false"  DataType="System.DateTime"
                                                EnableTimeIndependentFiltering="true">
                                                <HeaderStyle CssClass="DetailTablesEndDate" />
                                                <ItemStyle Width="90px" VerticalAlign="Top" />
                                            </rad:GridDateTimeColumn>
                                            <rad:GridTemplateColumn HeaderText="Price" SortExpression="Price" DataField="Price"
                                                AllowFiltering="false" FilterControlWidth="60px" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="right"></ItemStyle>
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="right" Width="70px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Price") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="DetailTablesPrice" />
                                                <ItemStyle Width="90px" VerticalAlign="Top" />
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn HeaderText="Rating" DataField="MeetingRate" AllowFiltering="false"
                                                FilterControlWidth="60px">
                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="left"></ItemStyle>
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="left" Width="60px" />
                                                <ItemTemplate>
                                                    <rad:RadRating ID="radRateIDMain" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"MeetingRate") %>'
                                                        Skin="Default" Enabled="false" Precision="Half">
                                                    </rad:RadRating>
                                                    <center>
                                                        <asp:Label ID="lblRatingDetails" runat="server" Text="Not Yet Rated" Font-Size="Smaller"></asp:Label></center>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="DetailTablesRating" />
                                            </rad:GridTemplateColumn>
                                        </Columns>
                                    </telerik:GridTableView>
                                </DetailTables>
                                <CommandItemSettings ExportToPdfText="Export to PDF" />
                                <Columns>
                                    <rad:GridTemplateColumn HeaderText="ID" HeaderButtonType="TextButton" SortExpression="MeetingID"
                                        DataField="ID" UniqueName="ID" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                        ShowFilterIcon="false" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="Product" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Meeting" AllowFiltering="true" DataField="WebName"
                                        FilterControlWidth="70px" SortExpression="WebName" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkWebName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebName") %>'
                                                NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"MeetingUrl") %>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="top" />
                                        <HeaderStyle CssClass="MasterSessionColumn" />
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Category" DataField="WebCategoryName" AllowFiltering="true"
                                        SortExpression="WebCategoryName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWebCategoryName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebCategoryName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="top" />
                                        <HeaderStyle CssClass="MasterRegisteredByDate" />
                                    </rad:GridTemplateColumn>
                                    <rad:GridDateTimeColumn UniqueName="GridDateTimeColumnRegisteredDate" AllowSorting="true"
                                        FilterControlWidth="110px" Visible="True" HeaderText="Registered By Date" DataField="AvailableUntil"
                                        SortExpression="AvailableUntil" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                        ShowFilterIcon="false" DataType="System.DateTime" EnableTimeIndependentFiltering="true">
                                        <HeaderStyle CssClass="MasterRegisteredByDate" />
                                        <ItemStyle VerticalAlign="top" Width="90px" />
                                    </rad:GridDateTimeColumn>
                                    <%-- suraj Issue 14457 , 4/25/13 Remove HeaderTooltip --%>
                                    <rad:GridTemplateColumn HeaderText="Description" DataField="Smalldesc" AllowFiltering="true"
                                        SortExpression="" HeaderTooltip="" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSmalldesc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Smalldesc") %>'></asp:Label>
                                            <rad:RadToolTip ID="RadToolTip1" runat="server" TargetControlID="lblSmalldesc" Animation="Slide"
                                                RelativeTo="Element" Position="BottomCenter" RenderInPageRoot="true">
                                                <%# DataBinder.Eval(Container.DataItem, "VerboseDescription")%>
                                            </rad:RadToolTip>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="top" />
                                        <HeaderStyle CssClass="MasterDescription" />
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Location" DataField="Location" AllowFiltering="true"
                                        SortExpression="Location" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Location") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="top" />
                                        <HeaderStyle CssClass="MasterLocation" />
                                    </rad:GridTemplateColumn>
                                    <rad:GridDateTimeColumn UniqueName="GridDateTimeColumnStartDate" AllowSorting="true"
                                        FilterControlWidth="90px" Visible="True" HeaderText="Start Date" DataField="StartDate"
                                        SortExpression="StartDate" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                        ShowFilterIcon="false" DataType="System.DateTime" EnableTimeIndependentFiltering="true">
                                        <ItemStyle Width="90px" VerticalAlign="Top" />
                                        <HeaderStyle CssClass="MasterOnorAfter" />
                                    </rad:GridDateTimeColumn>
                                    <rad:GridDateTimeColumn UniqueName="GridDateTimeColumnEndDate" AllowSorting="true"
                                        AllowFiltering="false" FilterControlWidth="100px" Visible="True" HeaderText="End Date"
                                        DataField="EndDate" SortExpression="EndDate" ReadOnly="true" AutoPostBackOnFilter="false"
                                        CurrentFilterFunction="EqualTo" ShowFilterIcon="false" DataType="System.DateTime"
                                        EnableTimeIndependentFiltering="true">
                                        <HeaderStyle CssClass="MasterEndDate" />
                                        <ItemStyle Width="90px" VerticalAlign="Top" />
                                    </rad:GridDateTimeColumn>
                                    <rad:GridTemplateColumn HeaderText="Price" DataField="Price" AllowFiltering="true"
                                        FilterControlWidth="70px" SortExpression="Price" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="right"></ItemStyle>
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="right" Width="70px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Price") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="MasterPrice" />
                                        <ItemStyle Width="70px" VerticalAlign="Top" />
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Rating" DataField="MeetingRate" AllowFiltering="false"
                                        FilterControlWidth="60px">
                                        <ItemStyle VerticalAlign="Top" Width="110px" Height="20px"></ItemStyle>
                                        <HeaderStyle CssClass="MasterRating" Width="110px" />
                                        <ItemTemplate>
                                            <rad:RadRating ID="radRateID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"MeetingRate") %>'
                                                Skin="Default" Enabled="false" Precision="Half">
                                            </rad:RadRating>
                                            <center>
                                                <asp:Label ID="lblpendingrating" runat="server" Text="Not Yet Rated" Font-Size="Smaller"></asp:Label></center>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </rad:RadGrid>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<cc6:User ID="User1" runat="server" />
<cc2:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="False"></cc2:AptifyShoppingCart>
