<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CompanyMembership.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CustomerService.CompanyMembership" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc4" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div>
    <%--  <p></p>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <asp:Label ID="lblGridInfo" runat="server" Font-Bold="true" Text=""></asp:Label>
            </div>
            <div>
                <asp:Label ID="lblmsg" runat="server" Font-Bold="true" Text=""></asp:Label>
            </div>
            <table id="Table1" runat="server" width="100%" class="data-form">
                <tr>
                    <td>
                       <%-- Suraj Issue 14302 , 4/17/13 ,two step sorting and add tool tips  --%>
                        <rad:RadGrid ID="grdperson" runat="server" AutoGenerateColumns="False" AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending"
                            SortingSettings-SortedAscToolTip="Sorted Ascending"
                            AllowFilteringByColumn="True">
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
                                        <ItemStyle HorizontalAlign="Center" CssClass="gridAlign"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="60px" />
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAllPerson" runat="server" OnCheckedChanged="ToggleSelectedState"
                                                AutoPostBack="True" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkperson" runat="server" OnCheckedChanged="chkSelectChanged" AutoPostBack="True" />
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Person ID" DataField="ID" SortExpression="ID"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <HeaderStyle Width="110px" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" CssClass="gridAlign"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPersonID" CssClass="" HeaderText="Person ID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>'> </asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Member" DataField="FirstLast" SortExpression="FirstLast"
                                        FilterControlWidth="150px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <ItemStyle HorizontalAlign="Center" Width="350px" CssClass="LeftAlign"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                    <%-- Neha,issue 16001,5/07/13, added css for image heightwidth and allignment of Name,Title,Adderess --%>
                                                       <div class="imgmember">
                                                        <%-- Neha,issue 14810,03/09/13,added Radbinaryimage --%>
                                                        <rad:RadBinaryImage ID="imgmember" CssClass="PeopleImage" runat="server"
                                                            AutoAdjustImageControlSize="false" ResizeMode="Fill" />
                                                       </div>
                                                    </td>
                                                    <td class="memberListtd">
                                                        <asp:Label ID="lblMember" CssClass="namelink" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FirstLast") %>'></asp:Label><br />
                                                        <asp:Label ID="lblMemberTitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"title") %>'></asp:Label>
                                                        <br />
                                                        <asp:Label ID="lbladdress" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"address") %>'> </asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Email" ItemStyle-Wrap="true" HeaderStyle-Width="150px"
                                        DataField="Email" SortExpression="Email" FilterControlWidth="150px" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <ItemStyle HorizontalAlign="left" Width="150px" CssClass="Emailstyle" Wrap="true">
                                        </ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmail" HeaderText="Email" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Email") %>'> </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="150px" VerticalAlign="Top" />
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderTooltip="Apply this product for all members" HeaderText="Membership Type"
                                        AllowFiltering="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                        UniqueName="MemberType">
                                        <ItemStyle HorizontalAlign="left" CssClass="gridAlign"></ItemStyle>
                                        <FilterTemplate>
                                            <%-- Suraj Issue DataBind, 2/4/12 RadComboBox --%>
                                            <telerik:RadComboBox ID="RadddlHeaderMemberType" CssClass="ddlStyleMeeting" OnDataBound="RadddlHeaderMemberType_DataBound"
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
                                            <asp:DropDownList ID="ddlMemberType" runat="server" Width="162px" CssClass="ddlStyleMeeting"
                                                OnSelectedIndexChanged="ddlMemberTypeChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Price" AllowFiltering="false" UniqueName="Price">
                                        <ItemStyle HorizontalAlign="Right" CssClass="gridAlign"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" Width="150px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrice" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Auto Renew" AllowFiltering="false">
                                        <HeaderStyle Width="120px" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" CssClass="gridAlign"></ItemStyle>
                                        <ItemTemplate>
                                            <%--  <asp:CheckBox ID="chkAutoRenew" runat="server"  /> --%>
                                            <asp:DropDownList ID="ddlAutoRenew" CssClass="ddlAutoRenewEnrollNewMembership" runat="server">
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="height: 10px">
    </div>
    <div style="float: right; height: 40px; padding: 10px; padding-right: 7px;">
        <asp:Button ID="btnPurchaseMemberships" CssClass="submitBtn" runat="server" Text="Proceed to checkout" />
    </div>
</div>
<cc1:User ID="User1" runat="server"></cc1:User>
<cc4:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="False"></cc4:AptifyShoppingCart>
