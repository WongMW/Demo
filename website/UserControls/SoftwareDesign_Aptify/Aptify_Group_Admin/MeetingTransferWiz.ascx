<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Group_Admin/MeetingTransferWiz.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.MeetingTransferWiz" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="uc1" TagName="CreditCard" Src="../Aptify_General/CreditCard.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<script type="text/javascript">
    function SetUniqueRadioButton(current) {
        for (i = 0; i < document.forms[0].elements.length; i++) {
            elm = document.forms[0].elements[i]
            if (elm.type == 'radio') {
                elm.checked = false;
            }
        }

        current.checked = true;
    }

</script>
<div>
    <div>
        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
    </div>
    <div>
        <asp:Wizard ID="WizardMeetingTransfer" runat="server" ActiveStepIndex="0" CssClass="transfer-widget">
            <SideBarStyle VerticalAlign="Top" CssClass="WizardSidebar" />
            <NavigationButtonStyle CssClass="submitBtn WizardButton" BorderColor="ActiveCaption" />
            <NavigationStyle CssClass="actions" />

            <WizardSteps>
                <asp:WizardStep ID="WizardStep1" runat="server" Title="1. Select a Meeting">
                    <div class="cai-table">
                        <asp:Label ID="lblStep1" runat="server" Text="Step 1: Select a Meeting/Session" CssClass="profile-title"></asp:Label>
                        <asp:Label ID="lblStep1Msg" runat="server" Text="Meetings/Sessions Registrations"></asp:Label>
                        <asp:UpdatePanel ID="upnlUpcomingMeeting" runat="server" ChildrenAsTriggers="false"
                            UpdateMode="Conditional">
                            <ContentTemplate>
                                <rad:RadGrid ID="grdUpcomingMeeting" runat="server" AllowPaging="true" GridLines="None"
                                    AutoPostBack="true" AutoGenerateColumns="false" AllowFilteringByColumn="true"
                                    AllowSorting="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                    OnItemCreated="grdUpcomingMeeting_GridItemCreated">
                                    <PagerStyle CssClass="sd-pager" />
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView AllowNaturalSort="false" ClientDataKeyNames="MeetingID">
                                        <NoRecordsTemplate>
                                            No Meeting Available.
                                        </NoRecordsTemplate>
                                        <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                                        <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <rad:GridTemplateColumn HeaderText="Select" AllowFiltering="false">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text="Select:" CssClass="mobile-label"></asp:Label>
                                                    <asp:RadioButton ID="optSelectMeeting" runat="server" class="cai-table-data" GroupName="Meeting" Onclick="SetUniqueRadioButton(this)" CausesValidation="false" />
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <%--  <rad:GridBoundColumn DataField="MeetingID" Visible="false"></rad:GridBoundColumn>--%>
                                            <rad:GridTemplateColumn HeaderText="MeetingID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMeetingID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MeetingID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn Visible="True" HeaderText="Meeting" DataField="Meeting" SortExpression="Meeting"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="200px">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text="Meeting:" CssClass="mobile-label"></asp:Label>
                                                    <asp:Label ID="lblMeetingName" runat="server" class="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "Meeting") %>' />
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn HeaderText="Is Session" AllowFiltering="false">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text="Is Session:" CssClass="mobile-label"></asp:Label>
                                                    <div class="cai-table-data">
                                                        <asp:CheckBox ID="chkIsSession" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsSession") %>'
                                                            Enabled="false" />
                                                    </div>

                                                    <asp:Label runat="server" Text="Start Date:" CssClass="mobile-label"></asp:Label>
                                                    <asp:Label ID="lblMeetingName2" runat="server" class="cai-table-data no-desktop" Text='<%# DataBinder.Eval(Container.DataItem, "StartDate") %>' />
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridDateTimeColumn Visible="True" HeaderText="Start Date" DataField="StartDate"
                                                SortExpression="StartDate" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                                ShowFilterIcon="false" DataType="System.DateTime" UniqueName="GridDateTimeColumnStartDate"
                                                EnableTimeIndependentFiltering="true" FilterControlWidth="150px" HeaderStyle-CssClass="no-mob"
                                                ItemStyle-CssClass="no-mob" FilterControlToolTip="Select a Filter Date">
                                            </rad:GridDateTimeColumn>
                                            <rad:GridTemplateColumn Visible="true" HeaderText="Venue" DataField="VENUE" SortExpression="VENUE"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="200px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMeetingVenue" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "VENUE") %>' />
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn HeaderText="ParentID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblParentID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ParentID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                        </Columns>
                                        <EditFormSettings>
                                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                            </EditColumn>
                                        </EditFormSettings>
                                    </MasterTableView>
                                </rad:RadGrid>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="grdUpcomingMeeting" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </asp:WizardStep>


                <asp:WizardStep ID="WizardStep2" runat="server" Title="2. Select an Existing Attendee   ">
                    <div class="cai-table">
                        <asp:Label ID="lblStep2" runat="server" Text="Step 2: Select an Attendee" CssClass="profile-title"></asp:Label>
                        <asp:Label ID="lblMeetingTitle" runat="server" Text="" CssClass="textfontsub"></asp:Label>
                        <asp:UpdatePanel ID="upnlMeetingRegistrant" runat="server" ChildrenAsTriggers="false"
                            UpdateMode="Conditional">
                            <ContentTemplate>
                                <rad:RadGrid ID="grdMeetingRegistrant" runat="server" AllowPaging="true" AllowSorting="true"
                                    AutoPostBack="true" AutoGenerateColumns="false" CellSpacing="0" GridLines="None"
                                    AllowFilteringByColumn="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                    OnItemCreated="grdMeetingRegistrant_GridItemCreated">
                                    <PagerStyle CssClass="sd-pager" />
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView AllowNaturalSort="false" ClientDataKeyNames="AttendeeID">
                                        <NoRecordsTemplate>
                                            No Attendee Available.
                                        </NoRecordsTemplate>
                                        <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                                        <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <rad:GridTemplateColumn HeaderText="Select" AllowFiltering="false">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text="Select:" CssClass="mobile-label"></asp:Label>
                                                    <asp:RadioButton ID="optSelectAttendee" class="cai-table-data" runat="server" Onclick="SetUniqueRadioButton(this)" />
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn HeaderText="Photo" AllowFiltering="false">
                                                <ItemTemplate>
                                                    <%-- Neha,issue 16001,5/07/13, added css for image heightwidth and allignment of Name,Title,Adderess --%>
                                                    <div class="imgmember">
                                                        <asp:Label runat="server" Text="Photo:" CssClass="mobile-label"></asp:Label>
                                                        <rad:RadBinaryImage ID="ImgAttendeePhoto" runat="server" CssClass="PeopleImage cai-table-data" AutoAdjustImageControlSize="false" />
                                                    </div>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <%-- <rad:GridBoundColumn DataField="AttendeeID" Visible="false"></rad:GridBoundColumn>--%>
                                            <rad:GridTemplateColumn HeaderText="ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text="Attendee #:" CssClass="mobile-label"></asp:Label>
                                                    <asp:Label ID="lblAttendeeID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AttendeeID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn HeaderText="Attendees" DataField="AttendeeID_FirstLast" SortExpression="AttendeeID_FirstLast"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="150px">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text="Attendee:" CssClass="mobile-label"></asp:Label>
                                                    <asp:Label ID="lblAttendeeName" runat="server" class="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "AttendeeID_FirstLast") %>'></asp:Label>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn HeaderText="Title" DataField="Title" SortExpression="Title"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="150px">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text="Title:" CssClass="mobile-label"></asp:Label>
                                                    <asp:Label ID="lblAttendeeTitle" runat="server" class="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "Title") %>'></asp:Label>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn HeaderText="Status" DataField="AttendeeStatus_Name" SortExpression="AttendeeStatus_Name"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text="Status:" CssClass="mobile-label"></asp:Label>
                                                    <asp:Label ID="lblAttendeeStatus" runat="server" class="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "AttendeeStatus_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text="Status:" CssClass="mobile-label"></asp:Label>
                                                    <asp:Label ID="lblStatusID" runat="server" class="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "StatusID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text="Order #:" CssClass="mobile-label"></asp:Label>
                                                    <asp:Label ID="lblOrderID" runat="server" class="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "OrderID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text="Company:" CssClass="mobile-label"></asp:Label>
                                                    <asp:Label ID="lblComapanyID" runat="server" class="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "CompanyID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text="Details:" CssClass="mobile-label"></asp:Label>
                                                    <asp:Label ID="lblOrderLineID" runat="server" class="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDetailID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn HeaderText="City" DataField="City" SortExpression="City"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="150px">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text="City:" CssClass="mobile-label"></asp:Label>
                                                    <asp:Label ID="lblCity" runat="server" class="cai-table-data" Text='<%# DataBinder.Eval(Container.DataItem, "City") %>'></asp:Label>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                        </Columns>
                                        <EditFormSettings>
                                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                            </EditColumn>
                                        </EditFormSettings>
                                    </MasterTableView>
                                </rad:RadGrid>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="grdMeetingRegistrant" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </asp:WizardStep>

                <asp:WizardStep ID="WizardStep3" runat="server" Title="3. Select New Meeting   ">
                    <div class="cai-table">
                        <asp:Label ID="Label1" runat="server" Text="Step 3: Select a Meeting/Session" CssClass="profile-title"></asp:Label>
                        <asp:Label ID="Label2" runat="server" Text="Upcoming/Ongoing Meetings/Sessions"></asp:Label>
                        <br />
                        <asp:UpdatePanel ID="upnlNewMeetings" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                            <ContentTemplate>
                                <rad:RadGrid ID="grdNewMeetings" runat="server" AllowPaging="true" AllowSorting="true"
                                    SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                    GridLines="None" AutoPostBack="true" AutoGenerateColumns="false" AllowFilteringByColumn="true" OnItemCreated="grdNewMeetings_GridItemCreated">
                                    <GroupingSettings CaseSensitive="false" />
                                    <PagerStyle CssClass="sd-pager" />
                                    <MasterTableView AllowNaturalSort="false" ClientDataKeyNames="MeetingID">
                                        <NoRecordsTemplate>
                                            No Meeting Available.
                                        </NoRecordsTemplate>
                                        <Columns>
                                            <rad:GridTemplateColumn HeaderText="Select" AllowFiltering="false">
                                                <ItemTemplate>
                                                    <asp:RadioButton ID="optSelectNewMeeting" runat="server" GroupName="Meeting" Onclick="SetUniqueRadioButton(this)" />
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridBoundColumn DataField="MeetingID" Visible="false"></rad:GridBoundColumn>
                                            <rad:GridTemplateColumn HeaderText="MeetingID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNewMeetingID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MeetingID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn Visible="True" HeaderText="Meeting" DataField="Meeting" SortExpression="Meeting"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="200px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNewMeetingName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Meeting") %>' />
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn HeaderText="Is Session" AllowFiltering="false">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkIsSession2" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsSession") %>'
                                                        Enabled="false" />
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridDateTimeColumn Visible="True" HeaderText="Start Date" DataField="StartDate"
                                                SortExpression="StartDate" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                                ShowFilterIcon="false" DataType="System.DateTime" UniqueName="GridDateTimeColumnStartDate"
                                                EnableTimeIndependentFiltering="true" FilterControlWidth="150px" FilterControlToolTip="Select a Filter Date">
                                            </rad:GridDateTimeColumn>
                                            <rad:GridTemplateColumn Visible="true" HeaderText="Venue" DataField="VENUE" SortExpression="VENUE"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="200px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNewMeetingVenue" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "VENUE") %>' />
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn HeaderText="ParentID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblParentID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ParentID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </rad:RadGrid>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="grdNewMeetings" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </asp:WizardStep>

                <asp:WizardStep ID="WizardStep4" runat="server" Title="4. Review and Confirm Replacement">
                    <div class="cai-table">
                        <asp:Label ID="Label3" runat="server" Text="Step 4: Review and Confirm Replacement" CssClass="profile-title"></asp:Label>

                        <asp:Label ID="lblNewPrice" Text="" runat="server" Font-Bold="true"></asp:Label>
                        <br />
                        <asp:Label ID="lblFinishmessage" Text="" runat="server"></asp:Label>
                        <br />
                        <br />
                        <rad:RadWindow ID="CreditcardWindow" runat="server" VisibleOnPageLoad="false" Modal="true"
                            Behaviors="Move" Title="Payment Information" VisibleStatusbar="false" Skin="Default"
                            IconUrl="" Width="470px" Height="365px" BackColor="#DADADA" ForeColor="#bda797">
                            <ContentTemplate>
                                <table class="data-form">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblBalance" runat="server" Font-Bold="true"></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <uc1:CreditCard ID="CreditCard" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:CheckBox ID="chkMakePayment" runat="server" Visible="false" />
                                            <asp:Button ID="btnOK" runat="server" Text="Make Payment" CssClass="submitBtn" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                                CssClass="submitBtn" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </rad:RadWindow>
                    </div>
                </asp:WizardStep>
            </WizardSteps>
        </asp:Wizard>
    </div>
</div>

<table id="tblTransferConfirmation" runat="server" class="order-confirmation" border="1"
    width="100%">
    <tr>
        <td>
            <table width="100%">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblCompleteMsg" runat="server" Text=""></asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="lblMeetingTransferconfrimation" runat="server" Text=" Click on 'Send Mail' to send Meeting Transfer Confirmation mail."
                            CssClass="textfontsub"></asp:Label>
                        <asp:Button ID="btnSendMail" runat="server" Text="Send Mail" CssClass="submitBtn" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="SendEmailLabel" runat="server"></asp:Label><br />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table width="100%">
                <tr>
                    <td width="75px" class="OrderConfirmationNoFontHeader">
                        <asp:Image runat="server" ID="companyLogo" alt="CompanyLogo image URL not set" />
                    </td>
                    <td width="300px" class="OrderConfirmationNoFontHeader">
                        <p>
                            <b>
                                <asp:Label ID="lblcompanyAddress" runat="server" Text=""> </asp:Label></b>
                        </p>
                    </td>
                    <td align="right" width="150px" class="OrderConfirmationNoFontHeader">
                        <table width="100%">
                            <tr>
                                <td>&nbsp;<b> Phone:</b>
                                </td>
                                <td style="text-align: center;">(202)<span style="display: none;">_</span>555-1234
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;<b> Fax:</b>
                                </td>
                                <td style="text-align: center;">(202)<span style="display: none;">_</span>555-4321
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="tblRowMain" runat="server">
        <td width="100%">
            <table width="100%">
                <tr>
                    <td class="bordercolor">
                        <table style="padding-left: 10px;">
                            <tr>
                                <td align="right">
                                    <b>Attendee:</b>
                                </td>
                                <td>
                                    <asp:Label ID="lblOriginalAttendee" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <b>Order Number:</b>
                                </td>
                                <td>
                                    <asp:Label ID="lblOrderID" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <b>Order Total:</b>
                                </td>
                                <td>
                                    <asp:Label ID="lblOrderTotal" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <b>Order Balance:</b>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblOrderBalance"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <b>Amount Paid:</b>
                                </td>
                                <td>
                                    <asp:Label ID="lblAmountPaid" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>

<cc1:User ID="User1" runat="server" />
<cc2:AptifyShoppingCart runat="Server" ID="ShoppingCart1" />
