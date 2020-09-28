<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AttendeeTransferWiz.ascx.vb"
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
<table width="100%">
    <tr>
        <td>
            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td valign="top">
            <asp:Wizard ID="WizardMeetingTransfer" runat="server" Font-Size="8px">
                <SideBarButtonStyle Font-Size="12px" CssClass="WizardSidebar" Height="30" Width="210" />
                <SideBarStyle VerticalAlign="Top" CssClass="WizardSidebar" BorderWidth="1" Height="100px" />
                <HeaderStyle CssClass="textfontsub"></HeaderStyle>
                <SideBarButtonStyle Font-Size="12px" />
                <NavigationButtonStyle CssClass="submitBtn WizardButton" BorderColor="ActiveCaption" />
                <WizardSteps>
                    <asp:WizardStep runat="server" Title="1. Select a Meeting   ">
                        <table>
                            <tr>
                                <td>
                                    &nbsp; &nbsp; &nbsp; &nbsp;
                                </td>
                                <td>
                                    <div>
                                        <asp:Label ID="lblStep1" runat="server" Text="Step 1: Select a Meeting/Session" Font-Bold="true"
                                            Font-Size="12px"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblStep1Msg" runat="server" Text="Meetings/Sessions Registrations"
                                            CssClass="textfontsub" Font-Size="12px"></asp:Label>
                                        <br />
                                        <asp:UpdatePanel ID="upnlUpcomingMeeting" runat="server" ChildrenAsTriggers="false"
                                            UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <rad:RadGrid ID="grdUpcomingMeeting" runat="server" AllowPaging="true" AllowSorting="true"
                                                    SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                                    GridLines="None" AutoPostBack="true" AllowFilteringByColumn="true" AutoGenerateColumns="false" OnItemCreated="grdUpcomingMeeting_GridItemCreated">
                                                    <GroupingSettings CaseSensitive="false" />
                                                    <MasterTableView  AllowNaturalSort="false" ClientDataKeyNames="MeetingID">
                                                     <NoRecordsTemplate>
                                    No Meeting Available.
                                </NoRecordsTemplate>
                                                        <Columns>
                                                            <rad:GridTemplateColumn HeaderText="Select" AllowFiltering="false">
                                                                <ItemTemplate>
                                                                    <asp:RadioButton ID="optSelectMeeting" runat="server" Onclick="SetUniqueRadioButton(this)" />
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
                                                                    <asp:Label ID="lblMeetingName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Meeting") %>' />
                                                                </ItemTemplate>
                                                            </rad:GridTemplateColumn>
                                                            <rad:GridTemplateColumn HeaderText="Is Session" AllowFiltering="false">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkIsSession" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsSession") %>'
                                                                        Enabled="false" />
                                                                </ItemTemplate>
                                                            </rad:GridTemplateColumn>
                                                            <rad:GridDateTimeColumn Visible="True" HeaderText="Start Date" DataField="StartDate" UniqueName="GridDateTimeColumnStartDate"
                                                                SortExpression="StartDate" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                                                ShowFilterIcon="false" DataType="System.DateTime" 
                                                                EnableTimeIndependentFiltering="true" FilterControlWidth="150px"  FilterControlToolTip="Select a Filter Date"  >
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
                                                    </MasterTableView>
                                                </rad:RadGrid>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="grdUpcomingMeeting" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <br />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:WizardStep>
                    <asp:WizardStep runat="server" Title="2. Select an Existing Attendee   ">
                        <table>
                            <tr>
                                <td>
                                    &nbsp; &nbsp; &nbsp; &nbsp;
                                </td>
                                <td>
                                    <div>
                                        <asp:Label ID="lblStep2" runat="server" Text="Step 2: Select an Attendee" Font-Bold="true"
                                            Font-Size="12px"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblMeetingTitle" runat="server" Text="" CssClass="textfontsub" Font-Size="12px"></asp:Label>
                                        <asp:UpdatePanel ID="upnlMeetingRegistrant" runat="server" ChildrenAsTriggers="false"
                                            UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <rad:RadGrid ID="grdMeetingRegistrant" runat="server" AllowPaging="true" AllowSorting="true"
                                                    SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                                    AutoPostBack="true" AllowFilteringByColumn="true" AutoGenerateColumns="false"
                                                    CellSpacing="0" GridLines="None" OnItemCreated="grdMeetingRegistrant_GridItemCreated">
                                                    <GroupingSettings CaseSensitive="false" />
                                                    <MasterTableView Width="720px" AllowNaturalSort="false" ClientDataKeyNames="AttendeeID">
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
                                                                    <rad:RadBinaryImage ID="ImgAttendeePhoto" runat="server" CssClass="PeopleImage" AutoAdjustImageControlSize="false">
                                                                    </rad:RadBinaryImage>
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
                                        <br />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:WizardStep>
                    <asp:WizardStep runat="server" Title="3. Replace Attendee   ">
                        <table>
                            <tr>
                                <td>
                                    &nbsp; &nbsp; &nbsp; &nbsp;
                                </td>
                                <td>
                                    <div runat="server" id="DivStep3" width="100%">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblStep3ReplaceAttendee" runat="server" Text="Step 3: Replace Attendee"
                                                        Font-Bold="true" Font-Size="12px"></asp:Label>
                                                    <br />
                                                    <asp:Label ID="lblStep3" runat="server" CssClass="textfontsub" ForeColor="Black"
                                                        Font-Size="12px" Text="Select a person who is not currently registered to attend the meeting in place of the attendee you selected in Step 2."></asp:Label>
                                                    <asp:UpdatePanel ID="upnlWaitingList" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <rad:RadGrid ID="grdWaitingList" runat="server" AllowPaging="true" AllowFilteringByColumn="true"
                                                                AllowSorting="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                                                AutoPostBack="true" AutoGenerateColumns="false" CellSpacing="0" GridLines="None" OnItemCreated="grdWaitingList_GridItemCreated">
                                                                <GroupingSettings CaseSensitive="false" />
                                                                <MasterTableView Width="720px" AllowNaturalSort="false" ClientDataKeyNames="AttendeeID">
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
                                                                        <rad:GridBoundColumn DataField ="AttendeeID" Visible="false"></rad:GridBoundColumn>
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
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:WizardStep>
                    <asp:WizardStep runat="server" Title="4. Review and Confirm Replacement">
                        <table width="720px">
                            <tr>
                                <td>
                                    &nbsp; &nbsp; &nbsp; &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblStep4" runat="server" Text="Step 4: Review and Confirm Replacement"
                                        Font-Bold="true" Font-Size="12px"></asp:Label><br />
                                    <br />
                                     <asp:Label ID="lblNewPrice" Text="" runat="server" Font-Bold="true" Font-Size="12px"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblFinishmessage" Text="" runat="server" Font-Size="12px"></asp:Label>
                                    <br />
                                    <rad:RadWindow ID="CreditcardWindow" runat="server" VisibleOnPageLoad="false" Modal="true"
                                        Behaviors="Move" Title="Payment Information" VisibleStatusbar="false" Skin="Default"
                                        IconUrl=""  BackColor="#DADADA" ForeColor="#bda797" Width="470px" Height="365px" >
                                        <ContentTemplate>
                                            <table class="data-form">
                                                <tr>
                                                    <td>
                                                         <asp:Label ID="lblBalance" runat="server" Font-Bold="true" Font-Size="12px"></asp:Label>
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
                                    <%----%>
                                </td>
                            </tr>
                        </table>
                        <br />
                    </asp:WizardStep>
                </WizardSteps>
            </asp:Wizard>
        </td>
        <td>
            &nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
</table>
<table id="tblTransferConfirmation" runat="server" class="order-confirmation" border="1"
    width="100%">
    <tr>
        <td>
            <table width="100%">
                <tr>
                    <td>
                        <asp:Label ID="lblCompleteMsg" runat="server" Text=""></asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="Label2" runat="server" Text=" Select attendee to send Attendee Transfer Confirmation mail."
                            CssClass="textfontsub" Font-Size="12px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="SendEmailLabel" runat="server" Text=""></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellspacing="3">
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkSendMailtoAttendee" runat="server" Text=" Previous Attendee"
                                        Onclick="ShownHideBtn()" />
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkSendMailtoNewAttendee" runat="server" Text=" New Attendee" Onclick="ShownHideBtn()" />
                                </td>
                                <td>
                                    <asp:Button ID="btnSendMail" runat="server" Text="Send Mail" CssClass="submitBtn"
                                        ClientIDMode="Static" />
                                </td>
                            </tr>
                        </table>
                        <br />
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
                                <asp:Label ID="lblcompanyAddress" runat="server" Text="" Font-Size="Small"> </asp:Label></b>
                        </p>
                    </td>
                    <td align="right" width="150px" class="OrderConfirmationNoFontHeader">
                        <table width="100%">
                            <tr>
                                <td>
                                    &nbsp;<b> Phone:</b>
                                </td>
                                <td style="text-align: center;">
                                    (202)<span style="display: none;">_</span>555-1234
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;<b> Fax:</b>
                                </td>
                                <td style="text-align: center;">
                                    (202)<span style="display: none;">_</span>555-4321
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
                                    <b>Original Attendee:</b>
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
                                    <asp:Label ID="lblOriginalOrderID" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <b>Order Total:</b>
                                </td>
                                <td>
                                    <asp:Label ID="lblOriOrderTotal" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <b>Order Balance:</b>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblOriOrderBalance"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 1px;">
                        &nbsp;
                    </td>
                    <td class="bordercolor">
                        <table style="padding-left: 10px;">
                            <tr>
                                <td align="right">
                                    <b>New Attendee:</b>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblNewAttendee"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <b>Order Number:</b>
                                </td>
                                <td>
                                    <asp:Label ID="lblNewOrderID" runat="server"></asp:Label><br />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <b>Order Total:</b>
                                </td>
                                <td>
                                    <asp:Label ID="lblNewOrderTotal" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <b>Order Balance:</b>
                                </td>
                                <td>
                                    <asp:Label ID="lblNewOrderBalance" runat="server"></asp:Label>
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
