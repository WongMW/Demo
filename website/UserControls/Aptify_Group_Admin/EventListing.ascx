<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EventListing.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CustomerService.EventListing" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="Rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="content-container clearfix">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td align="left">
                        <asp:Label ID="lblRegistrationResult" class="MeetingHeader" runat="server" Style="font-weight: 700"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblSelections" CssClass="textfont" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <Rad:RadGrid ID="RadgrdWaitingList" runat="server" AutoGenerateColumns="False" EnableViewState="true"
                                GridLines="None" CellSpacing="0" AllowPaging="True" SortingSettings-SortedDescToolTip="Sorted Descending"
                                SortingSettings-SortedAscToolTip="Sorted Ascending" AllowSorting="True" AllowFilteringByColumn="True">
                                <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView DataKeyNames="Subscriber" AllowMultiColumnSorting="False" AllowNaturalSort="false">
                                    <CommandItemSettings ExportToPdfText="Export to PDF" />
                                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                                    </RowIndicatorColumn>
                                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                                    </ExpandCollapseColumn>
                                    <Columns>
                                        <Rad:GridTemplateColumn Visible="false" AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkRenewal" runat="server" OnCheckedChanged="ToggleRowSelection"
                                                    AutoPostBack="True" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="headerChkbox" runat="server" OnCheckedChanged="ToggleSelectedState"
                                                    AutoPostBack="True" />
                                            </HeaderTemplate>
                                        </Rad:GridTemplateColumn>
                                        <Rad:GridBoundColumn UniqueName="OrderID" HeaderText="Order ID" DataField="OrderID"
                                            AutoPostBackOnFilter="true" SortExpression="OrderID" CurrentFilterFunction="Contains"
                                            ShowFilterIcon="false" >
                                            <ItemStyle Width="100px" />
                                            </Rad:GridBoundColumn>
                                        <Rad:GridTemplateColumn Visible="false" DataField="AttendeeID" UniqueName="AttendeeID"
                                            HeaderText="ID" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                            ShowFilterIcon="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"AttendeeID") %>'> </asp:Label>
                                            </ItemTemplate>
                                        </Rad:GridTemplateColumn>
                                        <Rad:GridTemplateColumn DataField="Subscriber" SortExpression="Subscriber" UniqueName="Subscriber"
                                            HeaderText="Name" FilterControlWidth="200px" AllowFiltering="true" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                            <ItemTemplate>
                                                <%--<div>--%>
                                                    <table>
                                                        <tr>
                                                            <td><%-- Neha,issue 16001,5/07/13, added css for image heightwidth and allignment of Name,Title,Adderess --%>
                                                                <div class="imgmember">
                                                                <rad:RadBinaryImage ID="RadBinaryImgPhoto"  CssClass="PeopleImage" runat="server"  AutoAdjustImageControlSize="false"  ResizeMode="Fill" />
                                                                </div>
                                                                    <%--<Rad:RadBinaryImage runat="server" ID="RadBinaryImgPhoto" DataValue='<%#IIf(Typeof(Eval("photo")) is DBNull, Nothing, Eval("photo"))%>'
                                                                            AutoAdjustImageControlSize="false" Width="60px" Height="60px" ToolTip='<%#Eval("Subscriber", "Photoof {0}") %>'
                                                                            AlternateText='<%#Eval("Subscriber", "Photoof {0}") %>' />--%><%--</div>--%>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Subscriber") %>'></asp:Label><br />
                                                                <asp:Label ID="lblTitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Title") %>'></asp:Label><br />
                                                                <%-- <ul>
                                                                        <li>
                                                                            <label>
                                                                            </label>
                                                                            <%#Eval("Subscriber")%>
                                                                        </li>
                                                                        <li>
                                                                            <label>
                                                                            </label>
                                                                            <%#Eval("Title")%>
                                                                        </li>
                                                                    </ul>--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ItemTemplate>
                                        </Rad:GridTemplateColumn>
                                        <Rad:GridBoundColumn UniqueName="City" HeaderText="City" DataField="City" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                        <Rad:GridBoundColumn UniqueName="MeetingTitle" HeaderText="Meeting Name" DataField="MeetingTitle"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                        <Rad:GridTemplateColumn DataField="Status" UniqueName="Status" HeaderText="Status"
                                            SortExpression="Status" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                            ShowFilterIcon="false">
                                            <ItemStyle HorizontalAlign="Left" Width="120px"></ItemStyle>
                                            <HeaderStyle Font-Bold="true" Width="120px"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblstatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Status") %>'> </asp:Label>
                                            </ItemTemplate>
                                        </Rad:GridTemplateColumn>
                                        <Rad:GridBoundColumn Visible="false" DataField="OrderDetailID" HeaderText="OrderDetailID"
                                            SortExpression="OrderDetailID" UniqueName="OrderDetailID" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        </Rad:GridBoundColumn>
                                        <Rad:GridTemplateColumn HeaderText="Badge Information" Visible="false" UniqueName="TemplateEditColumn"
                                            AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="PreviewLink" runat="server" Visible="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"OrderDetailID") %>'
                                                    CommandName="Preview" Text="Preview"></asp:LinkButton>
                                                <asp:LinkButton ID="EditBagdeLink" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"OrderDetailID") %>'
                                                    CommandName="EditBagde" Text="Preview/Edit" CausesValidation="false"></asp:LinkButton>
                                                    <asp:Label ID="lblNotApplicable" runat="server" Text="Not Applicable" Visible="false" ></asp:Label>
                                            </ItemTemplate>
                                        </Rad:GridTemplateColumn>
                                    </Columns>
                                    <EditFormSettings>
                                        <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                        </EditColumn>
                                    </EditFormSettings>
                                    <ItemStyle Width="30px" />
                                </MasterTableView>
                                <FilterMenu EnableImageSprites="False">
                                </FilterMenu>
                            </Rad:RadGrid>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <cc2:User ID="User1" runat="server" />
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <asp:Button ID="BtnBack" runat="server" class="submitBtn" Text="Back" />
            </td>
        </tr>
    </table>
</div>
<asp:UpdatePanel runat="server" ID="UpEditBadgeInfo" UpdateMode="Always">
    <ContentTemplate>
        <Rad:RadWindow ID="UserListDialog" CssClass="EditBadgeInforwIcon" runat="server"
            Modal="True" Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
            Height="180px" Width="300px" ForeColor="#C59933" IconUrl=" " Title="Badge Information">
            <ContentTemplate>
                <div style="background-color: #f4f3f1; padding: 5px;">
                    <table cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;">
                        <tr>
                            <td class="lblBadgeInformation" colspan="3">
                            </td>
                            <%--  <td></td>--%>
                        </tr>
                        <tr style="padding-bottom: 5px;">
                            <td style="padding-bottom: 5px;" class="rightAlign">
                                <b></b>
                            </td>
                            <td style="padding-bottom: 5px;" class="rightAlign">
                                <b>
                                    <asp:Label runat="server" ID="lbl" Text="*" ForeColor="red"></asp:Label>Name:</b>
                            </td>
                            <td style="padding-bottom: 5px;" class="width: 268px">
                                &nbsp;<asp:TextBox ID="txtBadgeName" Width="180px" TabIndex="1" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="padding-bottom: 5px;">
                            <td style="padding-bottom: 5px;" class="rightAlign">
                                <b></b>
                            </td>
                            <td style="padding-bottom: 5px;" class="rightAlign">
                                <b>
                                    <asp:Label runat="server" ID="Label1" Text="*" ForeColor="red"></asp:Label>Title:</b>
                            </td>
                            <td style="padding-bottom: 5px;" class="width: 268px">
                                &nbsp;<asp:TextBox ID="txtBadgeTitle" runat="server" Width="180px" TabIndex="2"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="padding-bottom: 5px;">
                            <td style="padding-bottom: 5px;" class="rightAlign">
                                <b></b>
                            </td>
                            <td style="padding-bottom: 5px;" class="rightAlign">
                                <b>
                                    <asp:Label runat="server" ID="Label2" Text="*" ForeColor="red"></asp:Label>Company:</b>
                            </td>
                            <td style="padding-bottom: 5px;" class="width: 268px">
                                &nbsp;<asp:TextBox ID="txtBadgeCompany" runat="server" Width="180px" TabIndex="3"></asp:TextBox>
                            </td>
                        </tr>
                        <tr runat="server" id="trMsg" visible="true">
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtTemp" Style="display: none;" CausesValidation="true" runat="server"
                                    Width="180px" TabIndex="3"></asp:TextBox>
                                <asp:RequiredFieldValidator Style="margin-left: 66px;" ID="RequiredFieldValidator3"
                                    ValidationGroup="BadgeGroup" runat="server" ControlToValidate="txtTemp" ForeColor="Red"
                                    ErrorMessage="All the above fields are mandatory."></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td align="right" style="padding-top: 6px;" class="width: 268px;">
                                <asp:Button ID="BtnUpdate" OnClientClick="return javascript:check();" class="submitBtn"
                                    TabIndex="27" runat="server" Width="80px" Text="Save" ValidationGroup="BadgeGroup"
                                    CausesValidation="true" />
                                <asp:Button ID="BtnCancel" Width="80px" runat="server" Text="Cancel" class="submitBtn"
                                    TabIndex="28" OnClientClick="OnClientClick();" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </Rad:RadWindow>
    </ContentTemplate>
    <%-- OnClick="BtnUpdate_Click"
          OnClick="BtnCancel_Click"--%>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="UserListDialog" />
    </Triggers>
</asp:UpdatePanel>
