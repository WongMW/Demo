<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Group_Admin/CompanyMembership.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CustomerService.CompanyMembership" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc4" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div>
    <%--  <p></p>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="cai-table">
        <ContentTemplate>
            <div class="page-message">
                <asp:Label ID="lblGridInfo" runat="server" Font-Bold="true" Text=""></asp:Label>
                <br />
                <asp:Label ID="lblmsg" runat="server" Font-Bold="true" Text=""></asp:Label>
            </div>
            <table id="Table1" runat="server" width="100%" class="data-form">
                <tr>
                    <td>
                        <%-- Suraj Issue 14302 , 4/17/13 ,two step sorting and add tool tips  --%>
                        <rad:RadGrid ID="grdperson" runat="server" AutoGenerateColumns="False" AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending"
                            SortingSettings-SortedAscToolTip="Sorted Ascending"
                            AllowFilteringByColumn="True" CssClass="memberships">
                            <PagerStyle CssClass="sd-pager" />
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                                <CommandItemSettings ExportToPdfText="Export to PDF" />
                                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                                </ExpandCollapseColumn>
                                <%-- Suraj issue 15287 4/5/13 , no record found msg displayed so we use "NoRecordsTemplate" --%>
                                <NoRecordsTemplate>
                                    No Members Available.
                                </NoRecordsTemplate>
                                <Columns>
                                    <rad:GridTemplateColumn HeaderText="Remove" AllowFiltering="false">
                                        <ItemStyle HorizontalAlign="Center" CssClass="checkbox"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="" />
                                        <HeaderTemplate>
                                            <asp:Label runat="server" Text="Select:" CssClass="mobile-label no-desktop"></asp:Label>
                                            <asp:CheckBox ID="chkAllPerson" CssClass="cai-table-data" runat="server" OnCheckedChanged="ToggleSelectedState"
                                                AutoPostBack="True" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text="Select:" CssClass="mobile-label no-desktop"></asp:Label>
                                            <asp:CheckBox ID="chkperson" runat="server" CssClass="cai-table-data" OnCheckedChanged="chkSelectChanged" AutoPostBack="True" />
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Person ID" DataField="ID" SortExpression="ID"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <HeaderStyle Width="" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" CssClass="gridAlign"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text="Order #:" CssClass="mobile-label no-desktop"></asp:Label>
                                            <asp:Label ID="lblPersonID" CssClass="cai-table-data" HeaderText="Person ID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>'> </asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Member" DataField="FirstLast" SortExpression="FirstLast"
                                        FilterControlWidth="" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <ItemStyle Width="" CssClass="member-profile-td"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                        <ItemTemplate>
                                            <%-- Neha,issue 16001,5/07/13, added css for image heightwidth and allignment of Name,Title,Adderess --%>
                                            <div class="imgmember">
                                                <%-- Neha,issue 14810,03/09/13,added Radbinaryimage --%>
                                                <asp:Label runat="server" Text="Profile Image:" CssClass="mobile-label"></asp:Label>
                                                <rad:RadBinaryImage ID="imgmember" CssClass="PeopleImage cai-table-data" runat="server"
                                                    AutoAdjustImageControlSize="false" ResizeMode="Fill" />
                                            </div>
                                            <div class="member-profile-details">
                                                <asp:Label runat="server" Text="Name:" CssClass="mobile-label"></asp:Label>
                                                <asp:Label ID="lblMember" CssClass="cai-table-data member-name-label" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FirstLast") %>'></asp:Label>
                                                <asp:Label runat="server" Text="Title:" CssClass="mobile-label"></asp:Label>
                                                <asp:Label ID="lblMemberTitle" CssClass="cai-table-data member-label" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"title") %>'></asp:Label>
                                                <asp:Label runat="server" Text="Address:" CssClass="mobile-label"></asp:Label>
                                                <asp:Label ID="lbladdress" CssClass="cai-table-data member-label" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"address") %>'> </asp:Label>

                                            </div>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Email" ItemStyle-Wrap="true" HeaderStyle-Width=""
                                        DataField="Email" SortExpression="Email" FilterControlWidth="" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <ItemStyle HorizontalAlign="left" Width="" CssClass="Emailstyle" Wrap="true"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text="Email:" CssClass="mobile-label"></asp:Label>
                                            <asp:Label ID="lblEmail" HeaderText="Email" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Email") %>'> </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="" VerticalAlign="Top" />
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderTooltip="Apply this product for all members" HeaderText="Membership Type"
                                        AllowFiltering="true" FilterControlWidth="" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                        UniqueName="MemberType">
                                        <ItemStyle HorizontalAlign="left" CssClass="drop-down-td"></ItemStyle>
                                        <FilterTemplate>
                                            <%-- Suraj Issue DataBind, 2/4/12 RadComboBox --%>
                                            <telerik:RadComboBox FilterControlWidth="100%" ItemStyle-Width="100%" ID="RadddlHeaderMemberType" CssClass="ddlStyleMeeting" OnDataBound="RadddlHeaderMemberType_DataBound"
                                                AppendDataBoundItems="true" runat="server" OnSelectedIndexChanged="ddlHeaderMemberType_SelectedIndexChanged">
                                            </telerik:RadComboBox>
                                        </FilterTemplate>
                                        <HeaderTemplate>
                                            Select Product
                                            <%--<asp:DropDownList ID="ddlHeaderMemberType"  CssClass="ddlStyleMeeting" Height="18px" runat="server" OnSelectedIndexChanged="ddlHeaderMemberTypeChanged" AutoPostBack="True">
                                    </asp:DropDownList>--%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%-- Suraj Issue DataBind, 11/22/12 increase the width of dropdown --%>
                                            <asp:Label runat="server" Text="Select Product:" CssClass="mobile-label"></asp:Label>
                                            <asp:DropDownList ID="ddlMemberType" runat="server" Width="" CssClass="cai-table-data"
                                                OnSelectedIndexChanged="ddlMemberTypeChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Price" AllowFiltering="false" UniqueName="Price">
                                        <ItemStyle HorizontalAlign="Right" CssClass=""></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" />
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text="Price:" CssClass="mobile-label"></asp:Label>
                                            <asp:Label ID="lblPrice" CssClass="cai-table-data" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Auto Renew" AllowFiltering="false">
                                        <HeaderStyle Width="" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" CssClass="drop-down-td"></ItemStyle>
                                        <ItemTemplate>
                                            <%--  <asp:CheckBox ID="chkAutoRenew" runat="server"  /> --%>
                                            <asp:Label runat="server" Text="Auto Renew:" CssClass="mobile-label"></asp:Label>
                                            <asp:DropDownList ID="ddlAutoRenew" CssClass="cai-table-data" runat="server">
                                                <asp:ListItem Text="No"></asp:ListItem>
                                                <asp:ListItem Text="Yes"></asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                </Columns>
                                <EditFormSettings>
                                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                    </EditColumn>
                                </EditFormSettings>
                            </MasterTableView>
                            <FilterMenu EnableImageSprites="False">
                            </FilterMenu>
                        </rad:RadGrid>
                        <%--  </div>          --%>
                    </td>
                </tr>
            </table>
            <div class="actions">
                <asp:Button ID="btnPurchaseMemberships" CssClass="submitBtn" runat="server" Text="Proceed to checkout" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</div>
<cc1:User ID="User1" runat="server"></cc1:User>
<cc4:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="False"></cc4:AptifyShoppingCart>
