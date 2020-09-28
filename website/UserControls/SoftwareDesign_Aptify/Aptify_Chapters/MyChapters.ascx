<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Chapters/MyChapters.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Chapters.MyChaptersControl" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<div class="content-container clearfix">
    <table id="tblMain" runat="server" class="data-form">
        <tr>
            <td>
                <asp:UpdatePanel ID="updPanelGrid" runat="server">
                    <ContentTemplate>
                        <rad:RadGrid ID="grdChapters" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                            AllowFilteringByColumn="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending">
                            <GroupingSettings CaseSensitive="false" />
                            <PagerStyle CssClass="sd-pager" />
                            <MasterTableView AllowSorting="true" AllowNaturalSort="false">
                                <Columns>
                                    <rad:GridTemplateColumn HeaderText="Chapter" DataField="Name" SortExpression="Name"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%">
                                        <ItemTemplate>
                                            <asp:HyperLink Text='<%# DataBinder.Eval(Container.DataItem,"Name") %>' ID="lnkName" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"DataNavigateUrl") %>' />
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridBoundColumn DataField="Role" HeaderText="Role" SortExpression="Role"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="80%" />
                                </Columns>
                            </MasterTableView>
                        </rad:RadGrid>
                        <%--  <asp:GridView ID="grdChapters" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:HyperLinkField Text="Name" DataNavigateUrlFields="ID" DataTextField="Name" HeaderText="Chapter" />
                        <asp:BoundField DataField="Role" HeaderText="Role" />
                    </Columns>
                    <PagerSettings Mode="Numeric" />
                </asp:GridView>--%>
                    </ContentTemplate>
                    <%-- <Triggers>
                <asp:AsyncPostBackTrigger ControlID = "grdChapters" EventName="PageIndexChanging" />
                </Triggers>--%>
                </asp:UpdatePanel>
                <%--End of addition by Suvarna D IssueID: 12436 --%>

                
            </td>
        </tr>
    </table>
    <cc3:User ID="User1" runat="server" />
</div>
