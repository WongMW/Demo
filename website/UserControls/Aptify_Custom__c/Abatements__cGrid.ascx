<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.Abatements__cGridClass" CodeFile="Abatements__cGrid.ascx.vb" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<div class="content-container clearfix"> 
    <table runat="server" id="tblMain" class="data-form">
       <tr>
           <td>
               <asp:Button id="cmdNewRecord" Text="New Abatements Record" Runat="server" /> 
           </td>
       </tr>
       <tr>
           <td>
               <Telerik:RadGrid ID="grdMain" Runat="server" AutoGenerateColumns="False" runat="server"  AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending" AllowFilteringByColumn = "true" > 
               <GroupingSettings CaseSensitive="false" />  
               <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false" >  
                   <Columns>
                       <Telerik:GridHyperLinkColumn Text="ID" DataTextField="ID" HeaderText="ID" SortExpression= "ID" DataNavigateUrlFields="ID" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="Person" HeaderText="Person"   SortExpression= "Person"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="Status" HeaderText="Status"   SortExpression= "Status"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="IllHealth" HeaderText="Ill Health"   SortExpression= "IllHealth"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="CareerBreak" HeaderText="Career Break"   SortExpression= "CareerBreak"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="RegisteredUnemployed" HeaderText="Registered Unemployed"   SortExpression= "RegisteredUnemployed"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="PartTime" HeaderText="Part Time"   SortExpression= "PartTime"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="EarlyRetirement" HeaderText="Early Retirement"   SortExpression= "EarlyRetirement"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="AnnualIncomeLow" HeaderText="Annual Income Low"   SortExpression= "AnnualIncomeLow"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="AnnualIncomeHigh" HeaderText="Annual Income High"   SortExpression= "AnnualIncomeHigh"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="PercentageReduction" HeaderText="Percentage Reduction"   SortExpression= "PercentageReduction"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="ProductCategory" HeaderText="Product Category"   SortExpression= "ProductCategory"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="Product" HeaderText="Product"   SortExpression= "Product"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="Campaign" HeaderText="Campaign"   SortExpression= "Campaign"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="CurrencyTypeID__c" HeaderText="Currency Type"   SortExpression= "CurrencyTypeID__c"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                   </Columns>
                   </MasterTableView>
               </Telerik:RadGrid>
           </td>
       </tr>
    </table>
    <asp:Label id="lblError" ForeColor="Red" runat="server" Visible="False"  />
    <cc3:User id="AptifyEbusinessUser1" runat="server" />
</div> 
