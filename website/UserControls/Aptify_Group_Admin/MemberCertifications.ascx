<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MemberCertifications.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.MemberCertifications" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="clearfix topPaddingSet">
    <div>
        <asp:Label ID="lblmsg" runat="server" Font-Bold="true" Text=""></asp:Label>
    </div>
    <table id="tblMain" runat="server" class="data-form">
        <tr>
            <td colspan="2">
                <asp:UpdatePanel ID="upGridPanel" runat="server">
                    <ContentTemplate>
                   <%-- Anil B For issue 14344 on 28-03-2013
                    Set icon for sorting also set filtering --%>
                        <rad:RadGrid ID="grdMembersCertifications" runat="server" AutoGenerateColumns="False" SortingSettings-SortedDescToolTip="Sorted Descending" AllowSorting="True"
                            Width="99%" EnableViewState="true" AllowPaging="true" PageSize="5" PagerStyle-PageSizeLabelText="Records Per Page" SortingSettings-SortedAscToolTip="Sorted Ascending"
                            AllowFilteringByColumn="true">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AllowFilteringByColumn="true"  AllowNaturalSort="false" AllowMultiColumnSorting="false">
                                <Columns>
                                    <rad:GridTemplateColumn HeaderText="Member" DataField="FirstLast" HeaderStyle-Width="250px"
                                        SortExpression="FirstLast" AutoPostBackOnFilter="true" FilterControlWidth="220px"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <ItemStyle HorizontalAlign="Left" CssClass="LeftAlignCert"></ItemStyle>
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td><%-- Neha,issue 16001,5/07/13, added class for image heightwidth and allignment of Name,Title,Adderess --%>
                                                  <div class="imgmember">
                                                    <%-- Neha,issue 14810,03/09/13,added Radbinaryimage --%>
                                                        <rad:RadBinaryImage ID="imgmember" runat="server" CssClass="PeopleImage" AutoAdjustImageControlSize="false" ResizeMode="Fill">
                                                        </rad:RadBinaryImage>
                                                        </div>
                                                    </td>
                                                    <td class="memberListtd">
                                                        <asp:HyperLink ID="lblMember" CssClass="namelink" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FirstLast") %>'
                                                            NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"AdminEditprofileUrl") %>'></asp:HyperLink><br />
                                                        <asp:Label ID="lblMemberTitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"title") %>'></asp:Label>
                                                        <br />
                                                        <asp:Label ID="lbladdress" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"address") %>'> </asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Certification Count" HeaderStyle-Width="120px" CurrentFilterFunction="EqualTo"
                                        DataField="TotalCirtification" SortExpression="TotalCirtification" AutoPostBackOnFilter="true"
                                        FilterControlWidth="120px"  ShowFilterIcon="false">
                                        <ItemStyle HorizontalAlign="Left" CssClass="LeftAlignCert CentralAlign"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TotalCirtification") %>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Total Unit" DataField="UnitTotal" SortExpression="UnitTotal"
                                        HeaderStyle-Width="120px" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                        ShowFilterIcon="false" FilterControlWidth="120px">
                                        <ItemStyle HorizontalAlign="Left" CssClass="LeftAlignCert CentralAlign"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalUnit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"UnitTotal") %>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Email" DataField="Email" SortExpression="Email"
                                        HeaderStyle-Width="200px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false" FilterControlWidth="180px">
                                        <ItemStyle HorizontalAlign="Left" CssClass="LeftAlignCert setEmailStyle"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDateGranted" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Email") %>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Submit New CEU" HeaderStyle-Width="120px" AllowFiltering="false">
                                        <ItemStyle HorizontalAlign="Left" CssClass="LeftAlignCert setEmailStyle"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hlnkNewCEU" CssClass="namelink" runat="server" Text="Submit New CEU"
                                                NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"CEUSubmissionPage") %>'></asp:HyperLink><br />
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
