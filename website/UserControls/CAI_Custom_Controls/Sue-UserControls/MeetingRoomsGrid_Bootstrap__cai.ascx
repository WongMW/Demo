<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.MeetingRoomsGridClass" CodeFile="MeetingRoomsGrid_Bootstrap__cai.ascx.vb" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<div class="content-container clearfix table-responsive"> 
    <table runat="server" id="tblMain" class="data-form table table-bordered">
       <tr>
           <td>
               <asp:Button id="cmdNewRecord" class="btn btn-default" Text="New Meeting Rooms Record" Runat="server" /> 
           </td>
       </tr>
       <tr>
           <td>
               <Telerik:RadGrid ID="grdMain" Runat="server" AutoGenerateColumns="False" runat="server"  AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending" AllowFilteringByColumn = "true" > 
               <GroupingSettings CaseSensitive="false" />  
               <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false" >  
                   <Columns>
                       <Telerik:GridHyperLinkColumn Text="ID" DataTextField="ID" HeaderText="ID" SortExpression= "ID" DataNavigateUrlFields="ID" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="Name" HeaderText="Name"   SortExpression= "Name"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="Description" HeaderText="Description"   SortExpression= "Description"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="Parent" HeaderText="Parent"   SortExpression= "Parent"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="Venue" HeaderText="Venue"   SortExpression= "Venue"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="Width" HeaderText="Width"   SortExpression= "Width"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="Depth" HeaderText="Depth"   SortExpression= "Depth"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="HandicapAccess" HeaderText="Handicap Access"   SortExpression= "HandicapAccess"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="MeetingTypesAllowed" HeaderText="Meeting Types Allowed"   SortExpression= "MeetingTypesAllowed"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="ChargeableProduct" HeaderText="Chargeable Product"   SortExpression= "ChargeableProduct"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                   </Columns>
                   </MasterTableView>
               </Telerik:RadGrid>
           </td>
       </tr>
    </table>
    <asp:Label id="lblError" ForeColor="Red" runat="server" Visible="False"  />
    <cc3:User id="AptifyEbusinessUser1" runat="server" />
</div> 
