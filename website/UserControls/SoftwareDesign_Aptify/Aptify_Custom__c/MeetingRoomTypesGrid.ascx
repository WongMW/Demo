<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.MeetingRoomTypesGridClass" CodeFile="~/UserControls/Aptify_Custom__c/MeetingRoomTypesGrid.ascx.vb" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<div class="content-container clearfix">
    <table runat="server" id="tblMain" class="data-form">
        <tr>
            <td>
                <asp:Button ID="cmdNewRecord" Text="New Meeting Room Types Record" runat="server" />
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
                            <telerik:GridBoundColumn DataField="Name" HeaderText="Name" SortExpression="Name" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                            <telerik:GridBoundColumn DataField="Description" HeaderText="Description" SortExpression="Description" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="False" />
    <cc3:User ID="AptifyEbusinessUser1" runat="server" />
</div>
