<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Chapters/Chapter.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Chapters.ChapterControl"
    Debug="true" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="uc1" TagName="ChapterMember" Src="ChapterMember.ascx" %>
<%@ Register TagPrefix="uc2" TagName="NameAddressBlock" Src="../Aptify_General/NameAddressBlock.ascx" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="content-container clearfix">
    <table class="data-form" runat="server" id="tblTopLevel">
        <tr>
            <td>
                <asp:Label ID="lblChapterName" runat="server" CssClass="CommitteeName"></asp:Label><br />
                <br />
                <asp:HyperLink ID="lnkReports" runat="server"><strong>Reports |</strong></asp:HyperLink>&nbsp;
                <asp:HyperLink ID="lnkLocation" runat="server"><strong>Edit Chapter |</strong></asp:HyperLink>&nbsp;
                <asp:HyperLink ID="lnkOfficers" runat="server"><strong>Officers |</strong></asp:HyperLink>&nbsp;
                <asp:HyperLink ID="lnkMeetings" runat="server"><strong>Meetings</strong></asp:HyperLink>
                <uc2:NameAddressBlock ID="blkAddress" runat="server"></uc2:NameAddressBlock>
                <br />
                <strong>Members</strong>
            </td>
        </tr>
        <tr>
            <td>
                <table class="data-form">
                    <tr>
                        <td>
                            <asp:Button ID="cmdNew" runat="server" Text="Add Member(s)" CssClass="submitBtn"></asp:Button>
                            <%--    Suraj Issue 16145,5/9/13  A potentially dangerous Request.Path value was detected from the client (?) so update panel remove--%>
                            <rad:RadGrid ID="grdMembers" runat="server" AutoGenerateColumns="false" CssClass="data-form"
                                AllowPaging="true" AllowFilteringByColumn="true" SortingSettings-SortedDescToolTip="Sorted Descending"
                                SortingSettings-SortedAscToolTip="Sorted Ascending">
                                <PagerStyle CssClass="sd-pager" />
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                                    <Columns>
                                        <rad:GridTemplateColumn HeaderText="ID" DataField="ID" SortExpression="ID" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="EqualTo" ShowFilterIcon="false" FilterControlWidth="80%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnSelect" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>' />
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridBoundColumn DataField="FirstName" HeaderText="First Name" SortExpression="FirstName"
                                            FooterText="" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="80%" />
                                        <rad:GridBoundColumn DataField="LastName" HeaderText="Last Name" SortExpression="LastName"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="80%" />
                                        <rad:GridBoundColumn DataField="Title" HeaderText="Title" SortExpression="Title"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="80%" />
                                        <rad:GridBoundColumn DataField="Email" HeaderText="Email" SortExpression="Email"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="80%" />
                                        <rad:GridBoundColumn Visible="false" DataField="ID" SortExpression="ID" HeaderText="ID"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                            FilterControlWidth="80%" />
                                    </Columns>
                                </MasterTableView>
                            </rad:RadGrid>
                            <p>
                            </p>
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <cc3:User ID="User1" runat="server" />
</div>
