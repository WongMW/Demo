<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Chapters/ChapterMeetings.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Chapters.ChapterMeetingsControl" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc5" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="uc1" TagName="ChapterMember" Src="ChapterMember.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NameAddressBlock" Src="../Aptify_General/NameAddressBlock.ascx" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="content-container clearfix">

    <table id="tblMain" runat="server" class="data-form">
        <tr>
            <td colspan="2">
                <asp:Label ID="lblError" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblChapterName" runat="server" CssClass="CommitteeName"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="cmbType" runat="server" AutoPostBack="True">
                    <asp:ListItem Value="Planned" Selected="True">Planned</asp:ListItem>
                    <asp:ListItem Value="Past">Past</asp:ListItem>
                    <asp:ListItem Value="All">All</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="cmdAdd" runat="server" Text="Add Meeting" CssClass="submitBtn"></asp:Button>
                <asp:UpdatePanel ID="updPanelGrid" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <rad:RadGrid ID="grdMeetings" runat="server" DataKeyNames="ID" AutoGenerateColumns="false" AllowPaging="true"
                            AllowFilteringByColumn="true">
                            <PagerStyle CssClass="sd-pager" />
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AllowFilteringByColumn="true" AllowSorting="true">
                                <Columns>
                                    <rad:GridTemplateColumn Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>

                                    <%-- <rad:GridHyperLinkColumn DataNavigateUrlFields="ID"  DataTextField="Name" DataNavigateUrlFormatString=""
                                        HeaderText="Meeting" />--%>

                                    <telerik:GridTemplateColumn AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains" DataField="Name" FilterControlWidth="175px"
                                        HeaderText="Meeting" ShowFilterIcon="false" SortExpression="Name">
                                        <HeaderStyle />
                                        <ItemStyle CssClass="RadFieldWidth" />
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkMeetingTitle" runat="server"
                                                NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"MeetingTitleUrl") %>'
                                                Text='<%# DataBinder.Eval(Container.DataItem,"Name") %>'></asp:HyperLink>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>


                                    <rad:GridBoundColumn DataField="Type" HeaderText="Type" SortExpression="Type" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                    <rad:GridBoundColumn DataField="Description" HeaderText="Description" SortExpression="Description"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowSorting="false" FilterControlWidth="120px" />
                                    <rad:GridDateTimeColumn UniqueName="GridDateTimeColumnStartDate" AllowSorting="true" Visible="True" HeaderText="Start Date" DataField="StartDate"
                                        SortExpression="StartDate" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                        ShowFilterIcon="false" DataType="System.DateTime" EnableTimeIndependentFiltering="true" FilterControlWidth="200px">
                                        <ItemStyle Width="150px" />
                                    </rad:GridDateTimeColumn>
                                    <rad:GridBoundColumn DataField="Status" HeaderText="Status" SortExpression="Status"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false" />
                                    <rad:GridTemplateColumn HeaderText="Delete" AllowFiltering="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/Delete.png" CommandName="Delete"
                                                CommandArgument="<%# CType(Container, GridDataItem).ItemIndex%>" />
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>

                                </Columns>
                            </MasterTableView>
                        </rad:RadGrid>
                        <asp:Label ID="lblNoMeetings" runat="server" Visible="False">No Meetings</asp:Label>
                    </ContentTemplate>
                    <%--<Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cmdAdd" EventName="Click"  />
                      
                    </Triggers>--%>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:LinkButton ID="lnkChapter" runat="server">Go To Chapter</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>

    <cc5:AptifyShoppingCart runat="server" ID="ShoppingCart1" />
    <cc3:User ID="User1" runat="server" />
</div>
