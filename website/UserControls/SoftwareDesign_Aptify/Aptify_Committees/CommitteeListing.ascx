<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Committees.CommitteeListingControl"
    CodeFile="~/UserControls/Aptify_Committees/CommitteeListing.ascx.vb" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEbusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="content-container clearfix cai-table">
    <asp:UpdatePanel ID="updPanelGrid" runat="server">
        <ContentTemplate>
            <rad:RadGrid ID="grdCommittees" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                AllowFilteringByColumn="true" SortingSettings-SortedAscToolTip="Sorted Ascending" SortingSettings-SortedDescToolTip="Sorted Descending" CssClass="mobile-table">
                <GroupingSettings CaseSensitive="false" />
                <PagerStyle CssClass="sd-pager" />
                <MasterTableView AllowSorting="true" AllowNaturalSort="false">
                    <Columns>
                        <rad:GridTemplateColumn DataField="Name" HeaderText="Name" SortExpression="Name" AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%" HeaderStyle-Width="170px">
                           <ItemTemplate>
                               <span class="mobile-label">Name:</span>
                              <asp:Label CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Name") %>'></asp:Label>
                           </ItemTemplate>
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn DataField="Description" HeaderText="Description" SortExpression="Description"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                             <ItemTemplate>
                               <span class="mobile-label">Description:</span>
                              <asp:Label CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Description") %>'></asp:Label>
                           </ItemTemplate>
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn DataField="Goals" HeaderText="Goals" SortExpression="Goals"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                             <ItemTemplate>
                               <span class="mobile-label">Goals:</span>
                              <asp:Label CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Goals") %>'></asp:Label>
                           </ItemTemplate>
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn DataField="DateFounded" UniqueName="GridDateTimeColumnDateFounded" HeaderText="Date Founded"
                            SortExpression="DateFounded" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                            ShowFilterIcon="false" DataType="System.DateTime" FilterControlWidth="170px" HeaderStyle-Width="170px">
                             <ItemTemplate>
                               <span class="mobile-label">Date Founded:</span>
                              <asp:Label CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DateFounded")%>'></asp:Label>
                           </ItemTemplate>
                        </rad:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </rad:RadGrid>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
