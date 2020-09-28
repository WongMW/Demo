<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MyCertifications.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Education.MyCertificationsControl" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="content-container clearfix">
    <table id="tblMain" runat="server" class="data-form">
        <tr>
            <td>
                <asp:DropDownList runat="server" ID="cmbType" AutoPostBack="True" Font-Size="9pt"
                    Width="148px">
                    <asp:ListItem Selected="True" Value="Granted">Granted</asp:ListItem>
                    <asp:ListItem Value="Cancelled">Cancelled</asp:ListItem>
                    <asp:ListItem Value="Expired">Expired</asp:ListItem>
                    <asp:ListItem>All</asp:ListItem>
                    <asp:ListItem>Declared</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width: 100px; text-align: right">
                <asp:HyperLink ID="hlnkSubmitNewCEU" runat="server" BorderColor="Transparent" Font-Bold="True"
                    Font-Size="14px" Width="255px">Submit New CEU</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <%-- Navin Prasad Issue 11032--%>
                <%--Nalini Issue 12436 date:01/12/2011--%>
                <%--Amruta Issue 13285 date:12/11/2012--%>
                <asp:UpdatePanel ID="UppanelGrid" runat="server">
                    <ContentTemplate>
                        <%--Neha Changes for Issue 14452--%>
                        <rad:RadGrid ID="grdMyCertifications" runat="server" AutoGenerateColumns="False"
                            AllowPaging="true" AllowFilteringByColumn="true" SortingSettings-SortedDescToolTip="Sorted Descending"
                            SortingSettings-SortedAscToolTip="Sorted Ascending">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                                <Columns>
                                    <rad:GridTemplateColumn HeaderText="Certification" DataField="Title" SortExpression="Title"
                                        FilterControlWidth="80%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkTitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Title") %>'
                                                NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"CertificationUrl") %>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" />
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Type" DataField="Type" SortExpression="Type"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        FilterControlWidth="80%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Type") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" />
                                    </rad:GridTemplateColumn>
                                      <%--Suraj Issue 14829 4/29/13, 4/29/13,5/6/13  remove DataFormatString for  remove time from grid   --%>
                                    <rad:GridDateTimeColumn UniqueName="GridDateTimeColumnStartDate" AllowSorting="true"
                                        Visible="True" HeaderText="Date Granted" DataField="DateGranted" SortExpression="DateGranted"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ReadOnly="true" ShowFilterIcon="false"
                                        DataType="System.DateTime" EnableTimeIndependentFiltering="true" ItemStyle-Width ="170px" FilterControlWidth="170px">
                                    </rad:GridDateTimeColumn>
                                     <%--Suraj Issue 14829,14452, 4/29/13,5/6/13  remove DataFormatString for  remove time from grid    --%>
                                    <rad:GridDateTimeColumn UniqueName="GridDateTimeColumnEndDate" AllowSorting="true"
                                        Visible="True" HeaderText="Expiration Date" DataField="ExpirationDate" SortExpression="ExpirationDate"
                                        ReadOnly="true" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                        DataType="System.DateTime" EnableTimeIndependentFiltering="true" ItemStyle-Width ="170px" FilterControlWidth="170px">
                                    </rad:GridDateTimeColumn>
                                    <rad:GridTemplateColumn HeaderText="Status" DataField="Status" SortExpression="Status"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        FilterControlWidth="80%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Status") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="10pt" />
                                    </rad:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </rad:RadGrid>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <cc1:User runat="server" ID="User1" />
</div>
