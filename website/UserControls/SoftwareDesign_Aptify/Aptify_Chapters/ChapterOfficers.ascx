<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Chapters/ChapterOfficers.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Chapters.ChapterOfficersControl" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="uc1" TagName="ChapterMember" Src="ChapterMember.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NameAddressBlock" Src="../Aptify_General/NameAddressBlock.ascx" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="content-container clearfix">
    <table id="tblMain" runat="server" class="data-form">
        <tr>
            <td>
                <asp:Label ID="lblChapterName" runat="server" CssClass="CommitteeName"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="cmbType" runat="server" AutoPostBack="True">
                    <asp:ListItem Value="Current" Selected="True">Current</asp:ListItem>
                    <asp:ListItem Value="Past">Past</asp:ListItem>
                    <asp:ListItem Value="All">All</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="updPanelGrid" runat="server">
                    <ContentTemplate>
                        <rad:RadGrid ID="grdRoles" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                            AllowFilteringByColumn="true">
                            <PagerStyle CssClass="sd-pager" />
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AllowSorting="true" AllowFilteringByColumn="true">
                                <Columns>
                                    <rad:GridBoundColumn DataField="ChapterRoleType" HeaderText="Role" SortExpression="ChapterRoleType"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="100px" />
                                    <rad:GridBoundColumn DataField="Person" HeaderText="Person" SortExpression="Person"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="180px" />
                                    <rad:GridDateTimeColumn UniqueName="GridDateTimeColumnStartDate" AllowSorting="true" Visible="True" HeaderText="Start Date" DataField="StartDate"
                                        SortExpression="StartDate" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                        ShowFilterIcon="false" DataType="System.DateTime" EnableTimeIndependentFiltering="true" FilterControlWidth="200px" DataFormatString="{0:MMMM dd, yyyy}">
                                        <ItemStyle Width="150px" />
                                    </rad:GridDateTimeColumn>
                                    <rad:GridDateTimeColumn UniqueName="GridDateTimeColumnEndDate" AllowSorting="true" Visible="True" HeaderText="End Date" DataField="EndDate" SortExpression="EndDate"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                        EnableTimeIndependentFiltering="true" FilterControlWidth="200px" DataType="System.DateTime" DataFormatString="{0:MMMM dd, yyyy}">
                                        <ItemStyle Width="150px" />
                                    </rad:GridDateTimeColumn>
                                </Columns>
                            </MasterTableView>
                        </rad:RadGrid>

                    </ContentTemplate>

                </asp:UpdatePanel>

            </td>
        </tr>
        <tr>
            <td>
                <asp:LinkButton ID="lnkChapter" runat="server">Go To Chapter</asp:LinkButton>&nbsp;<asp:Label
                    ID="lblError" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <cc3:User ID="User1" runat="server" />
</div>
