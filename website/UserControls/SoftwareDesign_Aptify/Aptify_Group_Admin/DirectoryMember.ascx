<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Group_Admin/DirectoryMember.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CustomerService.DirectoryMember" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" class="cai-table">
    <ContentTemplate>
        <div>
 
            <table id="tblmember" runat="server" width="100%" class="data-form company-directory">
                <tr>
                    <td colspan="2">
                        <rad:RadGrid ID="grdmember" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            PagerStyle-PageSizeLabelText="Records Per Page"
                            AllowFilteringByColumn="True" CellSpacing="0" CssClass="company-members" GridLines="None" OnItemCreated="grdmember_GridItemCreated">
                            <PagerStyle CssClass="sd-pager" />
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AllowFilteringByColumn="true" AllowSorting="true">
                                <CommandItemSettings ExportToPdfText="Export to PDF" />
                                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column"
                                    Visible="True">
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column"
                                    Visible="True">
                                </ExpandCollapseColumn>
                                <Columns>
                                    <%--  Amruta,Issue 14878,03/11/2013,Added UniqueName Property to acess required column when redirect on this control from group admin dashborad--%>
                                    <rad:GridBoundColumn HeaderText="ID" DataField="ID" Visible="False" ItemStyle-CssClass="IdWidth " FilterControlWidth="" CurrentFilterFunction="EqualTo" ShowFilterIcon="false" SortExpression="ID" HeaderStyle-Width="" AutoPostBackOnFilter="true" UniqueName="ID">
                                        <ItemStyle CssClass="IdWidth" />
                                    </rad:GridBoundColumn>
                                    <rad:GridTemplateColumn HeaderText="Member" DataField="FirstLast" SortExpression="FirstLast"
                                        AutoPostBackOnFilter="true" FilterControlWidth="" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false" UniqueName="DirectoryMemberName">
                                        <ItemStyle HorizontalAlign="Center" CssClass="member-profile-td" Width=""></ItemStyle>
                                        <ItemTemplate>
                                            <%-- Neha,issue 16001,5/07/13, added css for image heightwidth and allignment of Name,Title,Adderess--%>
                                            <div class="imgmember">
                                                <%-- Neha,issue 14810,03/09/13,added Radbinaryimage --%>
                                                <asp:Label runat="server" Text="Profile Image:" CssClass="mobile-label no-desktop"></asp:Label>
                                                <rad:RadBinaryImage ID="imgmember" CssClass="PeopleImage cai-table-data" runat="server" AutoAdjustImageControlSize="false" />
                                            </div>
                                            <div class="member-profile-details">
                                                <asp:Label runat="server" Text="Name:" CssClass="mobile-label member-name-label no-desktop"></asp:Label>
                                                <asp:HyperLink ID="lblMember" CssClass="namelink cai-table-data member-name-label" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FirstLast") %>'
                                                    NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"AdminEditprofileUrl") %>'></asp:HyperLink>
                                                <asp:Label runat="server" Text="Order:" CssClass="mobile-label no-desktop"></asp:Label>
                                                <asp:Label ID="lblMemberTitle" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"title") %>'></asp:Label>
                                                <asp:Label runat="server" Text="Order:" CssClass="mobile-label no-desktop"></asp:Label>
                                                <asp:Label ID="lbladdress" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"address") %>'> </asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Member" DataField="FirstLast" SortExpression="FirstLast"
                                        AutoPostBackOnFilter="true" FilterControlWidth="" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false" Visible="false" UniqueName="MemberName">
                                        <ItemStyle HorizontalAlign="Center" CssClass="LeftAlign tdVerticalAlignMiddle" Width=""></ItemStyle>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lblMemberName" CssClass="namelink" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FirstLast") %>'
                                                NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"AdminEditprofileUrl") %>'></asp:HyperLink><br />
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Email" DataField="Email" SortExpression="Email"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        FilterControlWidth="">
                                        <ItemStyle HorizontalAlign="Center" CssClass="LeftAlign Emailstyle tdVerticalAlignMiddle" Width=""></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text="Email:" CssClass="mobile-label no-desktop"></asp:Label>
                                            <asp:Label ID="lblEmail" HeaderText="Email" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Email") %>'> </asp:Label>
                                            <asp:Label runat="server" Text="Membership Type:" CssClass="mobile-label no-desktop"></asp:Label>
                                            <asp:Label ID="Label1" HeaderText="Email" CssClass="cai-table-data no-desktop" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MemberType")%>'> </asp:Label>
                                            <asp:Label runat="server" Text="Start Date:" CssClass="mobile-label no-desktop"></asp:Label>
                                            <asp:Label ID="Label2" HeaderText="Email" CssClass="cai-table-data no-desktop" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "JoinDate")%>'> </asp:Label>
                                            <asp:Label runat="server" Text="End Date:" CssClass="mobile-label no-desktop"></asp:Label>
                                            <asp:Label ID="Label3" HeaderText="Email" CssClass="cai-table-data no-desktop" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DuesPaidThru")%>'> </asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridBoundColumn HeaderText="Membership Type" DataField="MemberType" FilterControlWidth=""
                                        ItemStyle-CssClass="no-mob" HeaderStyle-CssClass="no-mob" SortExpression="MemberType"
                                        AutoPostBackOnFilter="true" ItemStyle-Width=""
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" UniqueName="MemberShipType">
                                        <ItemStyle CssClass="LeftAlign tdVerticalAlignMiddle no-mob" />
                                    </rad:GridBoundColumn>
                                    <rad:GridBoundColumn HeaderText="Start Date" DataField="JoinDate" ItemStyle-Width=""
                                        FilterControlWidth="" ItemStyle-CssClass="no-mob" HeaderStyle-CssClass="no-mob" SortExpression="JoinDate"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false" UniqueName="StartDate">
                                        <ItemStyle CssClass="LeftAlign tdVerticalAlignMiddle no-mob" />
                                    </rad:GridBoundColumn>
                                    <rad:GridBoundColumn HeaderText="End Date" DataField="DuesPaidThru" ItemStyle-Width=""
                                        FilterControlWidth="" ItemStyle-CssClass="no-mob" HeaderStyle-CssClass="no-mob" SortExpression="DuesPaidThru"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false" UniqueName="EndDate">
                                        <ItemStyle CssClass="LeftAlign tdVerticalAlignMiddle no-mob" />
                                    </rad:GridBoundColumn>
                                    <rad:GridTemplateColumn HeaderText="Status" SortExpression="Status" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth=""
                                        DataField="Status">
                                        <ItemStyle HorizontalAlign="Center" CssClass="LeftAlign tdVerticalAlignMiddle"></ItemStyle>
                                        <ItemTemplate>

                                            <rad:RadBinaryImage ID="imgstaus" CssClass="imgstaus" runat="server" />

                                            <asp:Label runat="server" Text="Status:" CssClass="mobile-label no-desktop"></asp:Label>
                                            <asp:Label ID="lblstatus" runat="server" CssClass="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem,"Status") %>'> </asp:Label>

                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Remove" AllowFiltering="false" UniqueName="Remove" Visible="false">
                                        <ItemStyle Width="" VerticalAlign="Middle" HorizontalAlign="Center" CssClass="tdVerticalAlignMiddle"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPersonID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>'
                                                Visible="false"></asp:Label>
                                            <asp:Label runat="server" Text="Remove:" CssClass="mobile-label no-desktop"></asp:Label>
                                            <asp:CheckBox ID="chkRmvCompLink" runat="server" CssClass="cai-table-data" AutoPostBack="true" ToolTip="Remove Person From Company" Visible="false" />
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                </Columns>
                                <EditFormSettings>
                                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                    </EditColumn>
                                </EditFormSettings>
                            </MasterTableView>
                            <PagerStyle PageSizeLabelText="Records Per Page" />
                            <FilterMenu EnableImageSprites="False">
                            </FilterMenu>
                        </rad:RadGrid>
                    </td>
                </tr>
            </table>
            <div class="actions">

                <asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>
                <asp:Button ID="btnRmvCompLink" Visible="false" runat="server" Text="Remove From Company" CssClass="submitBtn" />

            </div>
        </div>
        <rad:RadWindow ID="radConfirm" runat="server" Width="570px" Height="100px" Modal="True"
            Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
            ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="Alert" Behavior="None">
            <ContentTemplate>
                <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1; height: 100%; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblConfirm" runat="server" Font-Bold="true" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnConfirm" runat="server" Text="Yes" Width="50px" class="submitBtn"
                                OnClick="btnConfirm_Click" ValidationGroup="ok" />&nbsp;
                            <asp:Button ID="btnNo" runat="server" Text="No" Width="50px" class="submitBtn" OnClick="btnNo_Click"
                                ValidationGroup="ok" />&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </rad:RadWindow>

        <rad:RadWindow ID="radRCValidation" runat="server" Width="400px" Height="100px" Modal="True"
            Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
            ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="Alert" Behavior="None">
            <ContentTemplate>
                <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1; height: 100%; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblRCValidation" runat="server" Font-Bold="true" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnradRCValidation" runat="server" Text="OK" Width="50px" class="submitBtn" OnClick="btnNo_Click"
                                ValidationGroup="ok" />&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </rad:RadWindow>
    </ContentTemplate>
</asp:UpdatePanel>
<cc2:User ID="User1" runat="server"></cc2:User>
