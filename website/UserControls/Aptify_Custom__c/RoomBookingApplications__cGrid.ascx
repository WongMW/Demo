<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.RoomBookingApplications__cGridClass" CodeFile="RoomBookingApplications__cGrid.ascx.vb" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<div class="content-container clearfix"> 
    <table runat="server" id="tblMain" class="data-form">
       <tr>
           <td>
               <asp:Button id="cmdNewRecord" Text="Create Room Booking " Runat="server" /> 
           </td>
       </tr>
       <tr>
           <td>
               <Telerik:RadGrid ID="grdMain" Runat="server" AutoGenerateColumns="False" runat="server"  AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending" AllowFilteringByColumn = "true" > 
               <GroupingSettings CaseSensitive="false" />  
               <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false" >  
                   <Columns>
                       
                       <Telerik:GridHyperLinkColumn Text="ID" DataTextField="ID" HeaderText="ID" ItemStyle-Font-Underline="true" ItemStyle-ForeColor="Blue" SortExpression= "ID" DataNavigateUrlFields="ID" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="Requester" HeaderText="Requester"   SortExpression= "Requester"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="OnBehalfOf" HeaderText="OnBehalfOf"   SortExpression= "OnBehalfOf"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="Venue" HeaderText="Venue"   SortExpression= "Venue"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="MeetingTitle" HeaderText="Meeting Title"   SortExpression= "MeetingTitle"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="StartDate" HeaderText="Start Date"   SortExpression= "StartDate"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="EndDate" HeaderText="End Date"   SortExpression= "EndDate"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="Seats" HeaderText="Seats"   SortExpression= "Seats"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="RoomType" HeaderText="Room Type"   SortExpression= "RoomType"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="AssignedRoom" HeaderText="Assigned Room"   SortExpression= "AssignedRoom"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                   </Columns>
                   </MasterTableView>
               </Telerik:RadGrid>
           </td>
       </tr>
    </table>
    <asp:Label id="lblError" ForeColor="Red" runat="server" Visible="False"  />
    <cc3:User id="AptifyEbusinessUser1" runat="server" />
</div> 
