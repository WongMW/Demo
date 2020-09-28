<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.CasesGridClass" CodeFile="CasesGrid.ascx.vb" %>

<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<div class="content-container clearfix"> 
    <table runat="server" id="tblMain" class="data-form">
       <tr>
           <td>
               <asp:Button id="cmdNewRecord" Text="New Cases Record" Runat="server" /> 
           </td>
       </tr>
       <tr>
           <td>
               <Telerik:RadGrid ID="grdMain" Runat="server" AutoGenerateColumns="False"   AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending" AllowFilteringByColumn = "true" > 
               <GroupingSettings CaseSensitive="false" />  
               <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false" >  
                   <Columns>
                       <Telerik:GridHyperLinkColumn Text="ID" DataTextField="ID" HeaderText="ID" SortExpression= "ID" DataNavigateUrlFields="ID" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="Title" HeaderText="Title"   SortExpression= "Title"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="CaseCategory" HeaderText="Case Category"   SortExpression= "CaseCategory"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="CaseType" HeaderText="Case Type"   SortExpression= "CaseType"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="CasePriority" HeaderText="Priority"   SortExpression= "CasePriority"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="CaseStatus" HeaderText="Status"   SortExpression= "CaseStatus"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="DateRecorded" HeaderText="Date Recorded"   SortExpression= "DateRecorded"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="RecordedBy" HeaderText="Recorded By"   SortExpression= "RecordedBy"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       
                   </Columns>
                   </MasterTableView>
               </Telerik:RadGrid>
           </td>
       </tr>
    </table>
    <asp:Label id="lblError" ForeColor="Red" runat="server" Visible="False"  />
    <cc3:User id="AptifyEbusinessUser1" runat="server" />
</div> 
