<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Committees.MyCommitteesControl"
    CodeFile="~/UserControls/Aptify_Committees/MyCommittees.ascx.vb" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEbusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="content-container clearfix">
    <div id="tblMain" runat="server" class="data-form">

        <asp:DropDownList ID="cmbType" runat="server" AutoPostBack="True" Visible="false">
            <asp:ListItem Value="Current">Current</asp:ListItem>
            <asp:ListItem Value="Future">Future</asp:ListItem>
            <asp:ListItem Value="Past">Past</asp:ListItem>
            <asp:ListItem Value="All">All</asp:ListItem>
        </asp:DropDownList>
        <br />
        <asp:UpdatePanel ID="updPanelGrid" runat="server">
            <ContentTemplate>
                <rad:RadGrid ID="grdCommittees" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                    AllowFilteringByColumn="false" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sort Ascending" CssClass="cai-table mobile-table">
                    <GroupingSettings CaseSensitive="false" />
                    <PagerStyle CssClass="sd-pager" />
                    <MasterTableView AllowSorting="true" AllowNaturalSort="false">
                        <Columns>
                            <rad:GridTemplateColumn DataField="Committee" SortExpression="Committee" HeaderText="Committee name" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <ItemTemplate>
                                    <span class="mobile-label">Committee name:</span>
                                    <asp:HyperLink CssClass="cai-table-data" ID="lnkCommittee" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Committee") %>'
                                        NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"DataNavigateUrl") %>' />
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn DataField="CommitteeTerm" HeaderText="Term" SortExpression="CommitteeTerm" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <ItemTemplate>
                                    <span class="mobile-label">Term:</span>
                                    <asp:Label CssClass="cai-table-data"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CommitteeTerm")%>' />
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn DataField="Title" HeaderText="Title" SortExpression="Title" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" Visible="false" >
                                <ItemTemplate>
                                    <span class="mobile-label">Title:</span>
                                    <asp:Label CssClass="cai-table-data"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Title")%>' />
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </rad:RadGrid>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Label ID="lblNoCommittees" runat="server" Text="No qualifying committees were found."
            Visible="False"></asp:Label>

    </div>
    <cc3:User runat="server" ID="User1" />
</div>
