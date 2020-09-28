<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.RoomBookingApplications__cGridClass" CodeFile="~/UserControls/Aptify_Custom__c/RoomBookingApplications__cGrid.ascx.vb" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<div class="content-container clearfix">
    <div>&nbsp;</div>
    <table runat="server" id="tblMain" class="data-form">
        <tr>
            <td style="text-align:center;">
                <asp:Button ID="cmdNewRecord" Text="Create Room Booking " runat="server" CssClass="submitBtn"/>
                <div>&nbsp;</div>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="grdMain" runat="server" AutoGenerateColumns="False" runat="server" AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending" AllowFilteringByColumn="true" CssClass="cai-table">
                    <GroupingSettings CaseSensitive="false"/>
                    <PagerStyle CssClass="sd-pager" />
                    <MasterTableView AllowFilteringByColumn="false" AllowSorting="true" AllowNaturalSort="false">
                        <Columns>

                            <telerik:GridHyperLinkColumn Text="ID" DataTextField="ID" HeaderText="ID" ItemStyle-Font-Underline="true" ItemStyle-ForeColor="Blue" SortExpression="ID" DataNavigateUrlFields="ID" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                            <telerik:GridBoundColumn DataField="Requester" HeaderText="Requester" SortExpression="Requester" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                            <telerik:GridBoundColumn DataField="OnBehalfOf" HeaderText="On behalf of" SortExpression="OnBehalfOf" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                            <telerik:GridBoundColumn DataField="Venue" HeaderText="Venue" SortExpression="Venue" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                            <telerik:GridBoundColumn DataField="MeetingTitle" HeaderText="Meeting title" SortExpression="MeetingTitle" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                            <telerik:GridBoundColumn DataField="StartDate" HeaderText="Start date" SortExpression="StartDate" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                            <telerik:GridBoundColumn DataField="EndDate" HeaderText="End date" SortExpression="EndDate" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                            <telerik:GridBoundColumn DataField="Seats" HeaderText="Seats" SortExpression="Seats" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                            <telerik:GridBoundColumn DataField="RoomType" HeaderText="Room type" SortExpression="RoomType" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                            <telerik:GridBoundColumn DataField="AssignedRoom" HeaderText="Assigned room" SortExpression="AssignedRoom" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="False" />
    <cc3:User ID="AptifyEbusinessUser1" runat="server" />
</div>
