<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Committees.CommitteeListingControl"
    CodeFile="CommitteeListing.ascx.vb" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEbusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="content-container clearfix">
    <asp:UpdatePanel ID="updPanelGrid" runat="server">
        <ContentTemplate>
            <rad:RadGrid ID="grdCommittees" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                AllowFilteringByColumn="true" SortingSettings-SortedAscToolTip="Sorted Ascending" SortingSettings-SortedDescToolTip ="Sorted Descending">
                <GroupingSettings CaseSensitive="false" />
                <MasterTableView AllowSorting="true" AllowNaturalSort="false">
                    <Columns>
                        <rad:GridBoundColumn DataField="Name" HeaderText="Name" SortExpression="Name" AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%" HeaderStyle-Width="170px" />
                        <rad:GridBoundColumn DataField="Description" HeaderText="Description" SortExpression="Description"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%" HeaderStyle-Width="270px" />
                        <rad:GridBoundColumn DataField="Goals" HeaderText="Goals" SortExpression="Goals"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%" HeaderStyle-Width="370px" />
                        <rad:GridDateTimeColumn DataField="DateFounded" UniqueName="GridDateTimeColumnDateFounded" HeaderText="Date Founded" 
                        SortExpression="DateFounded" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" EnableTimeIndependentFiltering ="true"
                            ShowFilterIcon="false" DataType="System.DateTime" FilterControlWidth="170px" HeaderStyle-Width="170px" />
                    </Columns>
                </MasterTableView>
            </rad:RadGrid>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
