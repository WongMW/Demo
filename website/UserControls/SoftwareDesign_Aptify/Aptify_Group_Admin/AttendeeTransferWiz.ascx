<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Group_Admin/AttendeeTransferWiz.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.AttendeeTransferWiz" %>
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
        <asp:Wizard ID="WizardMeetingTransfer" runat="server" CssClass="transfer-widget">
            <SideBarStyle VerticalAlign="Top" CssClass="WizardSidebar" />
            <NavigationButtonStyle CssClass="submitBtn WizardButton" BorderColor="ActiveCaption" />
            <NavigationStyle CssClass="actions" />
            <WizardSteps>
                <asp:WizardStep runat="server" Title="1. Select a Meeting">

                    <div class="cai-table attendee-transfer">
                        <asp:Label ID="lblStep1" runat="server" Text="Step 1: Select a Meeting/Session" CssClass="profile-title"></asp:Label>
                        <asp:Label ID="lblStep1Msg" runat="server" Text="Meetings/Sessions Registrations"></asp:Label>

                        <asp:UpdatePanel ID="upnlUpcomingMeeting" runat="server" ChildrenAsTriggers="false"
                            UpdateMode="Conditional">
                            <ContentTemplate>
                                <rad:RadGrid ID="grdUpcomingMeeting" runat="server" AllowPaging="true" AllowSorting="true"
                                    SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                    GridLines="None" AutoPostBack="true" AllowFilteringByColumn="true" AutoGenerateColumns="false" OnItemCreated="grdUpcomingMeeting_GridItemCreated" CssClass="attendee-transfer">
                                    <GroupingSettings CaseSensitive="false" />
                                    <PagerStyle CssClass="sd-pager" />
                                    <MasterTableView AllowNaturalSort="false" ClientDataKeyNames="MeetingID" CssClass="cai-table">
                                        <NoRecordsTemplate>
                                            No Meeting Available.
                                        </NoRecordsTemplate>
                                        <Columns>
                                            <rad:GridTemplateColumn HeaderText="Select" AllowFiltering="false">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text="Select:" CssClass="mobile-label"></asp:Label>
                                                    <asp:RadioButton ID="optSelectMeeting" CssClass="cai-table-data" runat="server" Onclick="SetUniqueRadioButton(this)" />
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <%--<rad:GridBoundColumn DataField="MeetingID" Visible="false"></rad:GridBoundColumn>--%>
                                            <rad:GridTemplateColumn HeaderText="MeetingID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMeetingID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MeetingID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn Visible="True" HeaderText="Meeting" DataField="Meeting" SortExpression="Meeting"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="200px">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text="Meeting:" CssClass="mobile-label"></asp:Label>
                                                    <asp:Label ID="lblMeetingName" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Meeting") %>' />
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn HeaderText="Is Session" AllowFiltering="false">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text="In Session:" CssClass="mobile-label"></asp:Label>
                                                    <asp:CheckBox ID="chkIsSession" CssClass="cai-table-data" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsSession") %>'
                                                        Enabled="false" />
                                                    <asp:Label runat="server" Text="Start Date:" CssClass="mobile-label"></asp:Label>
                                                    <asp:Label class="cai-table-data no-desktop" ID="OrderDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StartDate", "{0:MMMM d, yyyy}")%>'></asp:Label>

                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridDateTimeColumn Visible="True" HeaderText="Start Date" DataField="StartDate" UniqueName="GridDateTimeColumnStartDate"
                                                SortExpression="StartDate" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                                ShowFilterIcon="false" DataType="System.DateTime" ItemStyle-CssClass="no-mob"
                                                EnableTimeIndependentFiltering="true" FilterControlWidth="150px" FilterControlToolTip="Select a Filter Date">
                                            </rad:GridDateTimeColumn>
                                            <rad:GridTemplateColumn Visible="true" HeaderText="Venue" DataField="VENUE" SortExpression="VENUE"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="200px">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text="Venue:" CssClass="mobile-label"></asp:Label>
                                                    <asp:Label ID="lblMeetingVenue" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "VENUE") %>' />
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
                                <asp:AsyncPostBackTrigger ControlID="grdUpcomingMeeting" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                </asp:WizardStep>
                <asp:WizardStep runat="server" Title="2. Select an Existing Attendee">

                    <div class="cai-table">
                        <asp:Label ID="lblStep2" runat="server" Text="Step 2: Select an Attendee" CssClass="profile-title"></asp:Label>
                        <asp:Label ID="lblMeetingTitle" runat="server" Text="" CssClass="textfontsub"></asp:Label>
                        <asp:UpdatePanel ID="upnlMeetingRegistrant" runat="server" ChildrenAsTriggers="false"
                            UpdateMode="Conditional">
                            <ContentTemplate>
                                <rad:RadGrid ID="grdMeetingRegistrant" runat="server" AllowPaging="true" AllowSorting="true"
                                    SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                    AutoPostBack="true" AllowFilteringByColumn="true" AutoGenerateColumns="false"
                                    CellSpacing="0" GridLines="None" OnItemCreated="grdMeetingRegistrant_GridItemCreated" CssClass="cai-table">
                                    <GroupingSettings CaseSensitive="false" />
                                    <PagerStyle CssClass="sd-pager" />
                                    <MasterTableView AllowNaturalSort="false" ClientDataKeyNames="AttendeeID">
                                        <NoRecordsTemplate>
                                            No Attendee Available.
                                        </NoRecordsTemplate>
                                        <Columns>
                                            <rad:GridTemplateColumn HeaderText="Select" AllowFiltering="false">
                                                <ItemTemplate>
                                                    <asp:RadioButton ID="optSelectAttendee" runat="server" Onclick="SetUniqueRadioButton(this)" />
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn HeaderText="Photo" AllowFiltering="false">
                                                <ItemTemplate>
                                                    <%-- Neha,issue 16001,5/07/13, added css for Name,Title,Adderess --%>
                                                    <div class="imgmember">
                                                        <%-- Neha,issue 14810,03/09/13,added css for Radbinaryimage --%>
                                                        <rad:RadBinaryImage ID="ImgAttendeePhoto" runat="server" CssClass="PeopleImage" AutoAdjustImageControlSize="false"></rad:RadBinaryImage>
                                                    </div>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridBoundColumn DataField="AttendeeID" Visible="false"></rad:GridBoundColumn>
                                            <rad:GridTemplateColumn HeaderText="ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAttendeeID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AttendeeID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn HeaderText="Attendees" DataField="AttendeeID_FirstLast" SortExpression="AttendeeID_FirstLast"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="150px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAttendeeName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AttendeeID_FirstLast") %>'></asp:Label>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn HeaderText="Title" DataField="Title" SortExpression="Title"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="150px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAttendeeTitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Title") %>'></asp:Label>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn HeaderText="Status" DataField="AttendeeStatus_Name" SortExpression="AttendeeStatus_Name"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAttendeeStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AttendeeStatus_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatusID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StatusID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOrderID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblComapanyID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CompanyID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOrderLineID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDetailID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn HeaderText="City" DataField="City" SortExpression="City"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="150px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "City") %>'></asp:Label>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </rad:RadGrid>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="grdMeetingRegistrant" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                </asp:WizardStep>
                <asp:WizardStep runat="server" Title="3. Replace Attendee">
                    <div runat="server" id="DivStep3" class="cai-table">
                        <asp:Label ID="lblStep3ReplaceAttendee" runat="server" Text="Step 3: Replace Attendee" CssClass="profile-title"></asp:Label>

                        <asp:Label ID="lblStep3" runat="server" CssClass="textfontsub" ForeColor="Black"
                            Text="Select a person who is not currently registered to attend the meeting in place of the attendee you selected in Step 2."></asp:Label>
                        <asp:UpdatePanel ID="upnlWaitingList" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                            <ContentTemplate>
                                <rad:RadGrid ID="grdWaitingList" runat="server" AllowPaging="true" AllowFilteringByColumn="true"
                                    AllowSorting="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                    AutoPostBack="true" AutoGenerateColumns="false" CellSpacing="0" GridLines="None" OnItemCreated="grdWaitingList_GridItemCreated" CssClass="cai-table">
                                    <GroupingSettings CaseSensitive="false" />
                                    <PagerStyle CssClass="sd-pager" />
                                    <MasterTableView AllowNaturalSort="false" ClientDataKeyNames="AttendeeID">
                                        <NoRecordsTemplate>
                                            No Attendee Available.
                                        </NoRecordsTemplate>
                                        <Columns>
                                            <rad:GridTemplateColumn HeaderText="Select" AllowFiltering="false">
                                                <ItemTemplate>
                                                    <asp:RadioButton ID="optSelectNewAttendee" runat="server" Onclick="SetUniqueRadioButton(this)" />
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn HeaderText="Photo" AllowFiltering="false">
                                                <ItemTemplate>
                                                    <%-- Neha,issue 16001,5/07/13, added css for image --%>
                                                    <div class="imgmember">
                                                        <%-- Neha,issue 14810,03/09/13,added css for Radbinaryimage --%>
                                                        <rad:RadBinaryImage ID="ImgNewAttendeePhoto" CssClass="PeopleImage" runat="server"
                                                            AutoAdjustImageControlSize="false"></rad:RadBinaryImage>
                                                    </div>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridBoundColumn DataField="AttendeeID" Visible="false"></rad:GridBoundColumn>
                                            <rad:GridTemplateColumn HeaderText="ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNewAttendeeID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AttendeeID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn HeaderText="Attendees" DataField="AttendeeID_FirstLast" SortExpression="AttendeeID_FirstLast"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="150px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNewAttendeeName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AttendeeID_FirstLast") %>'></asp:Label>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn HeaderText="Title" DataField="Title" SortExpression="Title"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="150px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNewAttendeeTitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Title") %>'></asp:Label>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn HeaderText="City" DataField="City" SortExpression="City"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="150px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "City") %>'></asp:Label>
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </rad:RadGrid>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="grdWaitingList" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                </asp:WizardStep>
                <asp:WizardStep runat="server" Title="4. Review and Confirm Replacement">
                    <div class="cai-table">
                        <asp:Label ID="lblStep4" runat="server" Text="Step 4: Review and Confirm Replacement" CssClass="profile-title"></asp:Label><br />

                        <asp:Label ID="lblNewPrice" Text="" runat="server"></asp:Label>
                        <asp:Label ID="lblFinishmessage" Text="" runat="server"></asp:Label>
                        <br />
                        <rad:RadWindow ID="CreditcardWindow" runat="server" VisibleOnPageLoad="false" Modal="true"
                            Behaviors="Move" Title="Payment Information" VisibleStatusbar="false" Skin="Default"
                            BackColor="#DADADA" ForeColor="#bda797" Width="500px" Height="500px" CssClass="cai-table">
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
<div id="tblTransferConfirmation" runat="server" class="order-confirmation" border="1"
    width="100%">

    <div>
        <asp:Label ID="lblCompleteMsg" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text=" Select attendee to send Attendee Transfer Confirmation mail."
            CssClass="textfontsub"></asp:Label>
    </div>

    <div>
        <asp:Label ID="SendEmailLabel" runat="server" Text=""></asp:Label><br />
    </div>

    <div>

        <div>
            <asp:CheckBox ID="chkSendMailtoAttendee" runat="server" Text=" Previous Attendee"
                Onclick="ShownHideBtn()" />
        </div>
        <div>
            <asp:CheckBox ID="chkSendMailtoNewAttendee" runat="server" Text=" New Attendee" Onclick="ShownHideBtn()" />
        </div>
        <div>
            <asp:Button ID="btnSendMail" runat="server" Text="Send Mail" CssClass="submitBtn"
                ClientIDMode="Static" />
        </div>

    </div>


    <div width="75px" class="OrderConfirmationNoFontHeader">
        <asp:Image runat="server" ID="companyLogo" alt="CompanyLogo image URL not set" />
    </div>
    <div width="300px" class="OrderConfirmationNoFontHeader">
        <p>
            <b>
                <asp:Label ID="lblcompanyAddress" runat="server" Text="" Font-Size="Small"> </asp:Label></b>
        </p>
    </div>
    <div align="right" width="150px" class="OrderConfirmationNoFontHeader">
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
    </div>

    <div id="tblRowMain" runat="server">

        <div>
            <b>Original Attendee:</b>
        </div>
        <div>
            <asp:Label ID="lblOriginalAttendee" runat="server"></asp:Label>
        </div>

        <div>
            <b>Order Number:</b>
        </div>
        <div>
            <asp:Label ID="lblOriginalOrderID" runat="server"></asp:Label>
        </div>

        <div>
            <b>Order Total:</b>
        </div>
        <div>
            <asp:Label ID="lblOriOrderTotal" runat="server"></asp:Label>
        </div>

        <div>
            <b>Order Balance:</b>
        </div>
        <div>
            <asp:Label runat="server" ID="lblOriOrderBalance"></asp:Label>
        </div>

        <div>
            <b>New Attendee:</b>
        </div>
        <div>
            <asp:Label runat="server" ID="lblNewAttendee"></asp:Label>
        </div>

        <div>
            <b>Order Number:</b>
        </div>
        <div>
            <asp:Label ID="lblNewOrderID" runat="server"></asp:Label><br />
        </div>

        <div>
            <b>Order Total:</b>
        </div>
        <div>
            <asp:Label ID="lblNewOrderTotal" runat="server"></asp:Label>
        </div>

        <div>
            <b>Order Balance:</b>
        </div>
        <div>
            <asp:Label ID="lblNewOrderBalance" runat="server"></asp:Label>
        </div>

        <div>
            <b>Amount Paid:</b>
        </div>
        <div>
            <asp:Label ID="lblAmountPaid" runat="server" Text=""></asp:Label>
        </div>

    </div>
</div>
<cc1:User ID="User1" runat="server" />
<cc2:AptifyShoppingCart runat="Server" ID="ShoppingCart1" />
<script type="text/javascript">

    function ShownHideBtn() {
        var chk1 = document.getElementById('<%=chkSendMailtoAttendee.ClientID %>');
        var chk2 = document.getElementById('<%=chkSendMailtoNewAttendee.ClientID %>');
        var btn = document.getElementById('<%=btnSendMail.ClientID %>');
        btn.style.display = "none";
        if (chk1.checked == true || chk2.checked == true) {
            btn.style.display = "block";
        }
        else {
            btn.style.display = "none";
        }

    }
    ShownHideBtn();
</script>
