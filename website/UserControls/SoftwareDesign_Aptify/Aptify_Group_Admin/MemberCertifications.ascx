<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Group_Admin/MemberCertifications.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.MemberCertifications" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="clearfix topPaddingSet cai-table">
    <div>
        <asp:Label ID="lblmsg" runat="server" Font-Bold="true" Text=""></asp:Label>
    </div>
    <table id="tblMain" runat="server" class="data-form member-certification">
        <tr>
            <td colspan="2">
                <asp:UpdatePanel ID="upGridPanel" runat="server">
                    <ContentTemplate>
                        <rad:RadGrid ID="grdMembersCertifications" runat="server" AutoGenerateColumns="False" SortingSettings-SortedDescToolTip="Sorted Descending" AllowSorting="True"
                            Width="100%" EnableViewState="true" AllowPaging="true" PageSize="5" PagerStyle-PageSizeLabelText="Records Per Page" SortingSettings-SortedAscToolTip="Sorted Ascending"
                            AllowFilteringByColumn="true">
                            <PagerStyle CssClass="sd-pager" />
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AllowFilteringByColumn="true" AllowNaturalSort="false" AllowMultiColumnSorting="false" CssClass="member-certifications">
                                <Columns>
                                    <rad:GridTemplateColumn HeaderText="Member" DataField="FirstLast"
                                        SortExpression="FirstLast" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <ItemStyle Width="" CssClass="member-profile-td"></ItemStyle>
                                        <ItemTemplate>
                                            <%-- Neha,issue 16001,5/07/13, added class for image heightwidth and allignment of Name,Title,Adderess --%>
                                            <div class="imgmember">
                                                <asp:Label runat="server" Text="Profile Image:" CssClass="mobile-label"></asp:Label>
                                                <%-- Neha,issue 14810,03/09/13,added Radbinaryimage --%>
                                                <rad:RadBinaryImage ID="imgmember" runat="server" CssClass="PeopleImage cai-table-data" AutoAdjustImageControlSize="false" ResizeMode="Fill"></rad:RadBinaryImage>
                                            </div>
                                            <asp:Label runat="server" Text="Name:" CssClass="mobile-label"></asp:Label>
                                            <asp:HyperLink ID="lblMember" CssClass="member-name-label cai-table-data " runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FirstLast")%>' NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"AdminEditprofileUrl") %>'></asp:HyperLink>
                                            <asp:Label runat="server" Text="Title" CssClass="mobile-label"></asp:Label>
                                            <asp:Label ID="lblMemberTitle" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"title") %>'></asp:Label>
                                            <asp:Label runat="server" Text="Location:" CssClass="mobile-label member-name-label"></asp:Label>
                                            <asp:Label ID="lbladdress" runat="server" class="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"address") %>'> </asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Certification Count" CurrentFilterFunction="EqualTo"
                                        DataField="TotalCirtification" SortExpression="TotalCirtification" AutoPostBackOnFilter="true"
                                        ShowFilterIcon="false" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text="Certification Count:" CssClass="mobile-label"></asp:Label>
                                            <asp:Label ID="lblCount" class="cai-table-data " runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TotalCirtification") %>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Total Unit" DataField="UnitTotal" SortExpression="UnitTotal"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                        ShowFilterIcon="false" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text="Total Units:" CssClass="mobile-label"></asp:Label>
                                            <asp:Label ID="lblTotalUnit" class="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"UnitTotal") %>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Email" DataField="Email" SortExpression="Email"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text="Email:" CssClass="mobile-label"></asp:Label>
                                            <asp:Label ID="lblDateGranted" class="cai-table-data " runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Email") %>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Submit New CEU" AllowFiltering="false" ItemStyle-CssClass="order-now">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text="Submit New CEU:" CssClass="mobile-label"></asp:Label>
                                            <asp:HyperLink ID="hlnkNewCEU" CssClass="cai-table-data" runat="server" Text="Submit New CEU"
                                                NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"CEUSubmissionPage") %>'></asp:HyperLink>
                                        </ItemTemplate>
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
