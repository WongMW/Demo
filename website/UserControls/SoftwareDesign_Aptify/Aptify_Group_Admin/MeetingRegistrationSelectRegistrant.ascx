<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Group_Admin/MeetingRegistrationSelectRegistrant.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.MeetingRegistrationSelectRegistrant" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<%--Amruta, Issue 16769, Added UpdateProgress control.--%>
<div class="dvUpdateProgress" style="overflow: visible;">
    <asp:UpdateProgress ID="updatePanelMain" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="updatePanelButton">
        <ProgressTemplate>
            <div class="dvProcessing" style="position: fixed; top: 0; right: 0;">
                <table class="tblFullHeightWidth">
                    <tr>
                        <td class="tdProcessing" style="vertical-align: middle">Please wait...
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<div class="content-container clearfix">
    <table id="tblMain" runat="server" class="data-form">
        <tr>
            <td colspan="2">
                <img alt="Web Image" src="" runat="server" id="imgWebImage" visible="false" />
                <asp:Label runat="server" ID="lblName" CssClass="MeetingName" /><br />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label runat="server" CssClass="MeetingDates" ID="lblDates" />
            </td>
        </tr>
        <tr runat="server" visible="false" id="trSessionParent">
            <td>
                <asp:HiddenField runat="server" ID="hfParentID" />
                Part of:
                <asp:HyperLink runat="server" ID="lnkParent">
                    <asp:Label runat="server" ID="lblParent" />
                </asp:HyperLink></td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" ID="lblPlace" /><br />
                <asp:Label runat="server" ID="lblLocation" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label runat="server" ID="lblText" Text=""></asp:Label><asp:Label runat="server"
                    ID="lblAvailableSpace"></asp:Label></td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" ID="lblMessage" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblError" runat="server" Visible="False"></asp:Label></td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table id="tblmember" runat="server" width="100%" class="data-form">
                <tr>
                    <td>
                        <rad:RadGrid ID="grdmember" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                            PagerStyle-PageSizeLabelText="Records Per Page" AllowFilteringByColumn="true" AllowSorting="true"
                            SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending" OnItemCreated="grdmember_GridItemCreated">
                            <GroupingSettings CaseSensitive="false" />
                    <PagerStyle CssClass="sd-pager" />

                            <MasterTableView AllowFilteringByColumn="true" ClientDataKeyNames="AttendeeID" AllowNaturalSort="false">

                                <Columns>
                                    <rad:GridTemplateColumn HeaderText="" AllowFiltering="false">
                                        <ItemStyle HorizontalAlign="Center" CssClass="gridAlign"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkRegistrant" runat="server" />
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridBoundColumn DataField="AttendeeID" Visible="false"></rad:GridBoundColumn>
                                    <rad:GridTemplateColumn Visible="False" DataField="AttendeeID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPersonID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"AttendeeID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Member" DataField="AttendeeID_FirstLast" SortExpression="AttendeeID_FirstLast"
                                        AutoPostBackOnFilter="true" FilterControlWidth="210px" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <ItemStyle HorizontalAlign="Center" CssClass="LeftAlign"></ItemStyle>
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <div class="imgmember">
                                                            <rad:RadBinaryImage ID="imgmember" runat="server" CssClass="PeopleImage" AutoAdjustImageControlSize="false"></rad:RadBinaryImage>
                                                        </div>
                                                    </td>
                                                    <td class="memberListtd">
                                                        <%-- Suraj Issue 16154 , 5/6/13 , remove the highperlink  So that a group admin, when registering for a meeting is not able to click the hyperlink of the linked persons --%>
                                                        <asp:Label ID="lblMember" CssClass="namelink" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"AttendeeID_FirstLast") %>'>
                                                        </asp:Label><br />
                                                        <asp:Label ID="lblMemberTitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Title") %>'></asp:Label><br />
                                                        <asp:Label ID="lbladdress" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"City") %>'> </asp:Label></td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Email" DataField="Email" SortExpression="Email"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        FilterControlWidth="210px">
                                        <ItemStyle HorizontalAlign="Center" CssClass="LeftAlign Emailstyle gridAlign"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmail" HeaderText="Email" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Email") %>'> </asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Member Type" SortExpression="MemberType" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Custom" ShowFilterIcon="false" FilterControlWidth="80px"
                                        DataField="MemberType">
                                        <ItemStyle HorizontalAlign="Center" CssClass="gridAlign"></ItemStyle>
                                        <ItemTemplate>

                                            <asp:Label ID="lblMemberType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"MemberType") %>'> </asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Badge Information" AllowFiltering="false">
                                        <ItemStyle HorizontalAlign="Center" CssClass="gridAlign" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="PreviewLink" runat="server" Visible="false" CommandName="Preview"
                                                Text="Preview"></asp:LinkButton><asp:LinkButton ID="EditBagdeLink" runat="server"
                                                    CommandName="EditBagde" Text="Preview/Edit"></asp:LinkButton>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBadgeName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"AttendeeID_FirstLast") %>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBadgeTitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Title") %>'>></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBadgeCompany" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Company") %>'>></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </rad:RadGrid></td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel runat="server" ID="UpEditBadgeInfo" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <rad:RadWindow ID="UserListDialog" runat="server" Modal="True" Skin="Default"
                                    BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="Move" ForeColor="#C59933"
                                    IconUrl="" Title="Badge Information" AutoSize="false" MinimizeIconUrl="" Height="161px">
                                    <ContentTemplate>
                                        <div style="background-color: #f4f3f1;">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="lblBadgeInformation" colspan="2"></td>
                                                    <%--  <td></td>--%>
                                                </tr>
                                                <tr>
                                                    <td class="rightAlign" style="padding-bottom: 5px;">
                                                        <b>Name: </b></td>
                                                    <td>
                                                        <asp:TextBox ID="txtBadgeName" Width="180px" TabIndex="1" runat="server"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td class="rightAlign" style="padding-bottom: 5px;">
                                                        <b>Title:</b> </td>
                                                    <td>
                                                        <asp:TextBox ID="txtBadgeTitle" runat="server" Width="180px" TabIndex="2"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td class="rightAlign" style="padding-bottom: 5px;">
                                                        <b>Company:</b> </td>
                                                    <td>
                                                        <asp:TextBox ID="txtBadgeCompany" runat="server" Width="180px" TabIndex="3"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td class="LeftAlign">
                                                        <br />
                                                        <asp:Button ID="btnUpdate" class="submitBtn" TabIndex="27" runat="server" Width="90px"
                                                            Text="Save" ValidationGroup="EditProfileControl" />&nbsp;&nbsp;
                                                        <asp:Button ID="btnCancel" Width="90px" runat="server" Text="Cancel" class="submitBtn"
                                                            TabIndex="28" OnClientClick="OnClientClick();" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </ContentTemplate>
                                </rad:RadWindow>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="UserListDialog" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="updatePanelButton" runat="server" ChildrenAsTriggers="True" class="cai-table">
        <ContentTemplate>
            <asp:Button runat="server" ID="btnSubmit" Text="Register" CssClass="submitBtn" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <cc3:User ID="User1" runat="server" />
    <cc2:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="False" />
    <asp:HiddenField runat="server" ID="hfSessions" />
</div>

