<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Education/MyCertifications.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Education.MyCertificationsControl" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="content-container clearfix cai-table">
    <div id="tblMain" runat="server" class="data-form">
        <div class="dropdown">
            <asp:DropDownList runat="server" ID="cmbType" AutoPostBack="True" Font-Size="9pt"
                Width="148px">
                <asp:ListItem Selected="True" Value="Granted">Granted</asp:ListItem>
                <asp:ListItem Value="Cancelled">Cancelled</asp:ListItem>
                <asp:ListItem Value="Expired">Expired</asp:ListItem>
                <asp:ListItem>All</asp:ListItem>
                <asp:ListItem>Declared</asp:ListItem>
            </asp:DropDownList>
        </div> 
         <div class="actions">
            <asp:HyperLink ID="hlnkSubmitNewCEU" runat="server" >Submit New CEU</asp:HyperLink>
        </div>  
        <asp:UpdatePanel ID="UppanelGrid" runat="server">
            <ContentTemplate>

                <rad:RadGrid ID="grdMyCertifications" runat="server" AutoGenerateColumns="False"
                    AllowPaging="true" AllowFilteringByColumn="true" SortingSettings-SortedDescToolTip="Sorted Descending"
                    SortingSettings-SortedAscToolTip="Sorted Ascending" CssClass="mobile-table">
                    <PagerStyle CssClass="sd-pager" />
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                        <Columns>
                            <rad:GridTemplateColumn HeaderText="Certification" DataField="Title" SortExpression="Title"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <ItemTemplate>
                                    <span class="mobile-label">Title:</span>
                                    <asp:HyperLink ID="lnkTitle" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"Title") %>'
                                        NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"CertificationUrl") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Type" DataField="Type" SortExpression="Type"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <ItemTemplate>
                                    <span class="mobile-label">Type:</span>
                                    <asp:Label ID="lblType" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"Type") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridDateTimeColumn UniqueName="GridDateTimeColumnStartDate" AllowSorting="true"
                                Visible="True" HeaderText="Date Granted" DataField="DateGranted" SortExpression="DateGranted"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ReadOnly="true" ShowFilterIcon="false"
                                DataType="System.DateTime" EnableTimeIndependentFiltering="true">
                                <ItemStyle CssClass="no-mob" />
                            </rad:GridDateTimeColumn>
                            <rad:GridTemplateColumn HeaderText="Date Granted" DataField="DateGranted" SortExpression="DateGranted"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <ItemTemplate>
                                    <span class="mobile-label">Date Granted:</span>
                                    <asp:Label ID="lblStatus" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"DateGranted") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridDateTimeColumn UniqueName="GridDateTimeColumnEndDate" AllowSorting="true"
                                Visible="True" HeaderText="Expiration Date" DataField="ExpirationDate" SortExpression="ExpirationDate"
                                ReadOnly="true" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                DataType="System.DateTime" EnableTimeIndependentFiltering="true">
                                <ItemStyle CssClass="no-mob" />
                            </rad:GridDateTimeColumn>
                             <rad:GridTemplateColumn HeaderText="Expiration Date" DataField="ExpirationDate" SortExpression="ExpirationDate"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <ItemTemplate>
                                    <span class="mobile-label">Expiration Date:</span>
                                    <asp:Label ID="lblStatus" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "ExpirationDate")%>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Status" DataField="Status" SortExpression="Status"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <ItemTemplate>
                                    <span class="mobile-label">Status:</span>
                                    <asp:Label ID="lblStatus" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"Status") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </rad:RadGrid>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
    <cc1:User runat="server" ID="User1" />
</div>
