<%--Aptify e-Business 5.5.1, July 2013--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/MeetingRegistrationSelectRegistrant__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.MeetingRegistrationSelectRegistrant__c" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%--Amruta, Issue 16769, Added UpdateProgress control.--%>
<div class="raDiv" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing"><div class="loading-bg">
                <img src="/Images/CAITheme/bx_loader.gif" />
                <span>LOADING...<br /><br />
                    Please do not leave or close this window while the request is processing.</span></div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<div class="content-container clearfix"style="margin-bottom:20px">
    <table id="tblMain" runat="server" class="data-form ">
        <tr>
            <td colspan="2">
                <h2>Product name: <asp:Label runat="server" ID="lblName" CssClass="MeetingName" /></h2>
                <strong><i class="far fa-calendar-alt"></i> Date:</strong>  <asp:Label runat="server" CssClass="MeetingDates" ID="lblDates" /><br />
                <strong><i class="fas fa-map-marker-alt"></i> Location:</strong>  <asp:Label runat="server" ID="lblPlace" /> <asp:Label runat="server" ID="lblLocation" />
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
            <td colspan="2">
                <strong><asp:Label runat="server" ID="lblText" Text=""></asp:Label>:</strong>  <asp:Label runat="server" ID="lblAvailableSpace"></asp:Label></td>
        </tr>
        <tr><td><p>&nbsp;</p></td></tr>
        <tr>
            <td>
                <asp:Label runat="server" ID="lblMessage" Text="" CssClass="info-note"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblError" runat="server" Visible="False"></asp:Label></td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table id="tblmember" runat="server" width="100%" class="data-form group-add-attendees">
                <tr>
                    <td>
                        <rad:RadGrid ID="grdmember" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                            PagerStyle-PageSizeLabelText="Records Per Page" AllowFilteringByColumn="true" AllowSorting="true"
                            SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending" 
                            EnableEmbeddedSkins="false" Skin="rgFilterBox" OnItemCreated="grdmember_GridItemCreated">
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
                                        AutoPostBackOnFilter="true" FilterControlWidth="100%" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <ItemStyle HorizontalAlign="Center" CssClass="LeftAlign"></ItemStyle>
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td width="0px">
                                                        <div class="imgmember">
                                                            <rad:RadBinaryImage ID="imgmember" runat="server" CssClass="PeopleImage" AutoAdjustImageControlSize="false" Visible="False"></rad:RadBinaryImage>
                                                        </div>
                                                    </td>
                                                    <td class="memberListtd">
                                                        <%-- Suraj Issue 16154 , 5/6/13 , remove the highperlink  So that a group admin, when registering for a meeting is not able to click the hyperlink of the linked persons --%>
                                                        <asp:Label ID="lblMember" CssClass="namelink" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"AttendeeID_FirstLast") %>'>
                                                        </asp:Label>
                                                        <asp:Label ID="lblMemberTitle" runat="server" Visible="False" Text='<%# DataBinder.Eval(Container.DataItem,"Title") %>'></asp:Label>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="City" DataField="City" SortExpression="City"
                                        AutoPostBackOnFilter="true" FilterControlWidth="100%" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <ItemStyle HorizontalAlign="Center" CssClass="LeftAlign"></ItemStyle>
                                        <ItemTemplate>
                                                    <span class="memberListtd">
                                                        <asp:Label ID="lbladdress" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"City") %>'> </asp:Label></td>
                                                    </span>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Email" DataField="Email" SortExpression="Email"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        FilterControlWidth="210px" Visible="False">
                                        <ItemStyle HorizontalAlign="Center" CssClass="LeftAlign Emailstyle gridAlign"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmail" HeaderText="Email" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Email") %>'> </asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Member Type" SortExpression="MemberType" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="100%"
                                        DataField="MemberType" Visible="False">
                                        <ItemStyle HorizontalAlign="Center" CssClass="gridAlign"></ItemStyle>
                                        <ItemTemplate>

                                            <asp:Label ID="lblMemberType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"MemberType") %>'> </asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Badge Information" AllowFiltering="false" Visible="False">
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
    <asp:UpdatePanel ID="updatePanelButton" runat="server" ChildrenAsTriggers="True">
        <ContentTemplate>
            <div class="cart-options"><asp:Button runat="server" ID="btnSubmit" Text="Register" CssClass="submitBtn" /></div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <cc3:User ID="User1" runat="server" />
    <cc2:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="False" />
    <asp:HiddenField runat="server" ID="hfSessions" />
</div>
<script>
    /*Susan Wong #20065 Highlight selected rows*/
    function pageLoad() {
        var delay = 10;
        setTimeout(function () {
            $("[id*=chkRegistrant]").change(function () {
                if ($(this).is(":checked")) {
                    $(this).closest('tr').addClass("checked-item");
                }
                else {
                    $(this).closest('tr').removeClass("checked-item");
                }
            });
        }, delay);
    }
</script>
