<%@ Control Language="vb" AutoEventWireup="false" Debug="true" Inherits="Aptify.Framework.Web.eBusiness.Generated.OnlineRefundRequests__cGridClass" CodeFile="OnlineRefundRequests__cGrid.ascx.vb" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<div class="content-container clearfix"> 
    <table runat="server" id="tblMain" class="data-form">
       <tr>
           <td>
               <asp:Button id="cmdNewRecord" Text="New Refund Requests" Runat="server" /> 
           </td>
       </tr>
       <tr>
           <td>
               <Telerik:RadGrid ID="grdMain" Runat="server" AutoGenerateColumns="False" runat="server"  AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending" AllowFilteringByColumn = "true" > 
               <GroupingSettings CaseSensitive="false" />  
               <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false" >  
                   <Columns>
                       <Telerik:GridHyperLinkColumn Text="ID" DataTextField="ID" HeaderText="ID" SortExpression= "ID" DataNavigateUrlFields="ID" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="OrderID" HeaderText="Order"   SortExpression= "OrderID"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="Request" HeaderText="Request"   SortExpression= "Request"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="Status" HeaderText="Status"   SortExpression= "Status"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="Amount" HeaderText="Amount"   SortExpression= "Amount"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="Response" HeaderText="Response"   SortExpression= "Response"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                             
                   </Columns>
                   </MasterTableView>
               </Telerik:RadGrid>
           </td>
       </tr>
    </table>
    <asp:Label id="lblError" ForeColor="Red" runat="server" Visible="False"  />
    <cc3:User id="AptifyEbusinessUser1" runat="server" />
</div> 
