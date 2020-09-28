<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.RoomBookingApplications__cGridClass" CodeFile="~/UserControls/Aptify_Meetings/RoomBookingApplications__cGrid.ascx.vb" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<div class="content-container clearfix">
    <table runat="server" id="tblMain" class="data-form">
        <tr>
            <td>
                <asp:Button ID="cmdNewRecord" Text="Create Room Booking " runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="grdMain" runat="server" AutoGenerateColumns="False" runat="server" AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending" AllowFilteringByColumn="true">
                    <GroupingSettings CaseSensitive="false" />
                    <PagerStyle CssClass="sd-pager" />
                    <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                        <Columns>

                            <telerik:GridHyperLinkColumn Text="ID" DataTextField="ID" HeaderText="ID" SortExpression="ID" DataNavigateUrlFields="ID" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                            <telerik:GridBoundColumn DataField="Requester" HeaderText="Requester" SortExpression="Requester" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                            <telerik:GridBoundColumn DataField="OnBehalfOf" HeaderText="OnBehalfOf" SortExpression="OnBehalfOf" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                            <telerik:GridBoundColumn DataField="Venue" HeaderText="Venue" SortExpression="Venue" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                            <telerik:GridBoundColumn DataField="MeetingTitle" HeaderText="Meeting Title" SortExpression="MeetingTitle" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                            <telerik:GridBoundColumn DataField="StartDate" HeaderText="Start Date" SortExpression="StartDate" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                            <telerik:GridBoundColumn DataField="EndDate" HeaderText="End Date" SortExpression="EndDate" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                            <telerik:GridBoundColumn DataField="Seats" HeaderText="Seats" SortExpression="Seats" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                            <telerik:GridBoundColumn DataField="RoomType" HeaderText="Room Type" SortExpression="RoomType" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                            <telerik:GridBoundColumn DataField="AssignedRoom" HeaderText="Assigned Room" SortExpression="AssignedRoom" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="False" />
    <cc3:User ID="AptifyEbusinessUser1" runat="server" />
</div>
