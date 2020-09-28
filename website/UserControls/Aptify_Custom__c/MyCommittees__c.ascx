<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Committees.MyCommitteesControl__c"
    CodeFile="MyCommittees__c.ascx.vb" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEbusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="content-container clearfix">
    <table id="tblMain" runat="server" class="data-form">
        <tr>
            <td>
                <asp:DropDownList ID="cmbType" runat="server" AutoPostBack="True">
                    <asp:ListItem Value="Current">Current</asp:ListItem>
                    <asp:ListItem Value="Future">Future</asp:ListItem>
                    <asp:ListItem Value="Past">Past</asp:ListItem>
                    <asp:ListItem Value="All">All</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:UpdatePanel ID="updPanelGrid" runat="server">
                    <ContentTemplate>
                        <rad:RadGrid ID="grdCommittees" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                            AllowFilteringByColumn="true" SortingSettings-SortedDescToolTip ="Sorted Descending" SortingSettings-SortedAscToolTip="Sort Ascending">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AllowSorting="true" AllowNaturalSort="false">
                                <Columns>
                                    <rad:GridTemplateColumn DataField="Committee" SortExpression="Committee" HeaderText="Name"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%" HeaderStyle-Width="170px">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkCommittee" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Committee") %>'
                                                NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"DataNavigateUrl") %>' />
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridBoundColumn DataField="CommitteeTerm" HeaderText="Term" SortExpression="CommitteeTerm"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%" HeaderStyle-Width ="270px"/>
                                    <rad:GridBoundColumn DataField="Title" HeaderText="Title" SortExpression="Title"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"  FilterControlWidth="80%" HeaderStyle-Width ="270px"/>
                                </Columns>
                            </MasterTableView>
                        </rad:RadGrid>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:Label ID="lblNoCommittees" runat="server" Text="No qualifying Committees were found."
                    Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
    <cc3:User runat="server" ID="User1" />
</div>
