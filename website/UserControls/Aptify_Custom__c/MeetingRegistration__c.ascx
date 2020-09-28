<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MeetingRegistration__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.MeetingRegistrationControl__c" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%--<%@ Register TagName="Meetings" TagPrefix="uc1" Src="~/UserControls/Aptify_Meetings/Meeting.ascx" %>--%>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<div class="dvUpdateProgress">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" AssociatedUpdatePanelID="updatePanelMain"
        DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing">
                <table class="tblFullHeightWidth">
                    <tr>
                        <td class="tdProcessing">
                            Please wait...
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<asp:UpdatePanel ID="updatePanelMain" runat="server" ChildrenAsTriggers="True">
    <ContentTemplate>
        <asp:Label ID="lblError" runat="server" Visible="False" Font-Bold="True" ForeColor="Red"></asp:Label>
        <table id="tblInner" runat="server">
            <tr>
                <td>
                    <cc2:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="False" />
                    <%--HP Issue#9091: changed message header to clarify functionality--%>
                    <asp:Label ID="lblTopMessageInfo" runat="server" CssClass="lblTopMessage" Text=" Please verify the attendee and complete the form below. When done, click Add Attendee   for registrants to the meeting."></asp:Label>                   
                    <asp:ValidationSummary ID="vldSummary" ValidationGroup="Done"  runat="server"></asp:ValidationSummary>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMeetingRegistrationError" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
        <div id="meetingTitle" class="meetingTitleDiv classLeftDiv">
            <asp:Label runat="server" ID="lblMeeting" /><br />
        </div>
        <div>
            <table>
                <tr>
                    <td class="rightColumnMeetingRegistration classLeftDiv lblPriceTDWidth">
                        <asp:Label ID="lblPriceInfo" Text="Price:" Font-Bold="true" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblPrice" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="rightColumnMeetingRegistration classLeftDiv lblPriceTDWidth">
                        <asp:Label ID="lblAvailableSpaceText" runat="server" Text="" Font-Bold="true"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblAvailableSpace"  runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="rightColumnMeetingRegistration classLeftDiv" colspan="2">
                        <asp:Label ID="lblMessage" ForeColor="Red" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div id="dvLeft" class="dvLeftStyle">
            <div class="BorderDiv dvMeetBorder">
                <table class="tblFullWidth" cellpadding="0" cellspacing="0">
                    <tr id="trAttendee" runat="server">
                        <td class="tdAtendeesInfo" colspan="3">
                            <table class="tblFullWidth" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="tdAddAtendeeWidth">
                                    </td>
                                    <td align="left">
                                        Add Attendee
                                    </td>
                                    <td class="tdRequiredFieldInfo">
                                        <span class="RequiredField">*</span>
                                        <asp:Label ID="lblMandetoryInfo" runat="server" CssClass="lblMandatory" Text="Mandatory fields"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <div class="dvSectionOnMeetingRegistration">
                                <table>
                                    <tr>
                                        <td class="leftColumnMeetingRegistration">
                                            <span class="RequiredField">*</span>First Name:
                                        </td>
                                        <td class="rightColumnMeetingRegistration">
                                            <asp:TextBox ID="txtFirstName" CssClass="txtMeetingRegistration" runat="server" TabIndex="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="vldAttendeeFname" runat="server" Font-Size="8pt"
                                                ValidationGroup="Done" Display="None" ControlToValidate="txtFirstName" ErrorMessage="Please specify an attendee First Name."></asp:RequiredFieldValidator>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftColumnMeetingRegistration">
                                            <span class="RequiredField">*</span>Last Name:
                                        </td>
                                        <td class="rightColumnMeetingRegistration">
                                            <asp:TextBox ID="txtLastName" runat="server" CssClass="txtMeetingRegistration" TabIndex="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="vldAttendeeLname" runat="server" Font-Size="8pt"
                                                ValidationGroup="Done" Display="None" ControlToValidate="txtLastName" ErrorMessage="Please specify an attendee Last Name."></asp:RequiredFieldValidator>
                                           
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="leftColumnMeetingRegistration">
                                            <span class="RequiredField">*</span>Email:
                                        </td>
                                        <td class="rightColumnMeetingRegistration">
                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="txtMeetingRegistration" TabIndex="3"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfValidatorEmail" runat="server" Font-Size="8pt"
                                                    ValidationGroup="Done" Display="None" ControlToValidate="txtEmail" ErrorMessage="Please specify an attendee Email Address."></asp:RequiredFieldValidator>
                                                <%--Suraj Issue 15210 ,2/11/13 RegularExpressionValidator validator --%>
                                                <asp:RegularExpressionValidator ID="reValidatorEmail" runat="server" ErrorMessage="Please enter a valid Email Address."
                                                    ControlToValidate="txtEmail" ValidationGroup="Done" Display="None" ></asp:RegularExpressionValidator>
                                            </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="dvAddAtendeeButton">
                                <asp:Button ID="btnAddInfo" ToolTip="Add attendee record to grid" ValidationGroup="Done"
                                    CssClass="submitBtn" TabIndex="13" runat="server" Text="Add Attendee" />
                            </div>
                        </td>
                        <td valign="top">
                            <div class="dvSectionBadge">
                                <fieldset class="meetFieldSet">
                                    <legend class="meetLegend"><b>Badge Information</b></legend>
                                    <table>
                                        <tr>
                                            <td class="leftColumnMeetingRegistration">
                                                Name:
                                            </td>
                                            <td class="rightColumnMeetingRegistration">
                                                <asp:TextBox ID="txtBadgeName" CssClass="txtMeetingRegistration" TabIndex="5" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftColumnMeetingRegistration">
                                                Title:
                                            </td>
                                            <td class="rightColumnMeetingRegistration">
                                                <asp:TextBox ID="txtBadgeTitle" runat="server" CssClass="txtMeetingRegistration"
                                                    TabIndex="6"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftColumnMeetingRegistration">
                                                Company:
                                            </td>
                                            <td class="rightColumnMeetingRegistration">
                                                <asp:TextBox ID="txtCompany" runat="server" CssClass="txtMeetingRegistration" TabIndex="7"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="dvBottomFieldset">
                                    </div>
                            </div>
                            </fieldset>
                        </td>
                        <td valign="top">
                            <div class="dvSectionBadge">
                                <fieldset class="meetFieldSet">
                                    <legend class="meetLegend"><b>Attendee Preferences</b></legend>
                                    <table>
                                        <tr>
                                            <td colspan="2">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftColumnMeetingRegistrationPreferences">
                                                Food Pref:
                                            </td>
                                            <td class="rightColumnMeetingRegistration">
                                                <asp:DropDownList ID="ddlFoodPreference" runat="server" CssClass="txtMeetingRegistration ddlTravelPreference"
                                                    TabIndex="8">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftColumnMeetingRegistrationPreferences">
                                                Travel Pref:
                                            </td>
                                            <td class="rightColumnMeetingRegistration">
                                                <asp:DropDownList ID="ddlTravelPreference" runat="server" CssClass="txtMeetingRegistration ddlTravelPreference"
                                                    TabIndex="9">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftColumnMeetingRegistrationPreferences">
                                                Golf Handicap:
                                            </td>
                                            <td class="rightColumnMeetingRegistration">
                                                <asp:TextBox ID="txtGolfHandicape" runat="server" CssClass="txtMeetingRegistration"
                                                    TabIndex="10"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please enter a numeric value for Golf Handicap."
                                                    ControlToValidate="txtGolfHandicape" ValidationGroup="Done" Display="None" ValidationExpression="^\d+(\.\d{1,4})?$"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftColumnMeetingRegistrationPreferences">
                                                Special Request:
                                            </td>
                                            <td class="rightColumnMeetingRegistration">
                                                <asp:TextBox ID="txtSpecialRequest" runat="server" CssClass="txtMeetingRegistration"
                                                    TabIndex="11"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftColumnMeetingRegistrationPreferences" valign="top">
                                                Other Pref:
                                            </td>
                                            <td class="rightColumnMeetingRegistration">
                                                <asp:TextBox ID="txtOtherPreference" runat="server" CssClass="txtMeetingRegistration"
                                                    TabIndex="12"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="dvBottomFieldset">
                                    </div>
                            </div>
                            </fieldset>
            </div>
            </td> </tr> </table>
        </div>
        <div class="dvAddedAtendee">
            <asp:Label ID="lblAddedAtendee" runat="server" Text="Added Attendees" CssClass="lblAddedAtendee"></asp:Label>
        </div>
        <div>
            <asp:Panel ID="pnlAddMember" runat="server" ScrollBars="Auto">
                <asp:GridView ID="grdAddMember" AutoGenerateColumns="false" runat="server" ShowFooter="False"
                    AllowPaging="false">
                    <Columns>
                        <asp:BoundField DataField="RowNumber" HeaderText="Row Number" Visible="false" />
                        <asp:TemplateField>
                            <HeaderStyle CssClass="GridViewHeaderStyle" HorizontalAlign="Left" />
                            <HeaderTemplate>
                                <asp:Label ID="lblFname" Text="Name" runat="server"></asp:Label>
                            </HeaderTemplate>
                            <ItemStyle CssClass="gridItemStyleRegMeet" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:Label ID="lblAtendeeFirstName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"first Name")%>'></asp:Label>
                                <asp:Label ID="lblAtendeeLastName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Last Name")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle CssClass="GridViewHeaderStyle" Width="240px" />
                            <HeaderTemplate>
                                <asp:Label ID="lblEmail" Text="Email" runat="server"></asp:Label>
                            </HeaderTemplate>
                            <ItemStyle CssClass="gridItemStyleRegMeet" Width="240px" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:Label ID="lblAttendeeEmail" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Email")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                            <HeaderTemplate>
                                <asp:Label ID="lblBadgeInformation" Text="Badge Information" runat="server"></asp:Label>
                            </HeaderTemplate>
                            <ItemStyle CssClass="gridItemStyleRegMeet" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:Label ID="lblBadgeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Badge Information Name")%>'></asp:Label><br />
                                <asp:Label ID="lblBadgeTitle" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Badge Information Title")%>'></asp:Label><br />
                                <asp:Label ID="lblBadgeCompany" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Badge Information Company")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle CssClass="GridViewHeaderStyle" Width="220px" />
                            <HeaderTemplate>
                                <asp:Label ID="lbllstSessions" Text="List of Sessions for Attendee" runat="server"></asp:Label>
                            </HeaderTemplate>
                            <ItemStyle VerticalAlign="Middle" CssClass="gridItemStyleRegMeet" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lnklstSessonForAttendee" Text="View/Edit Sessions" runat="server"
                                    CommandName="EditSession" CommandArgument='<%#Eval("RowNumber") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle CssClass="GridViewHeaderStyle LeftTextCenter" HorizontalAlign="Center"
                                Width="150px" />
                            <HeaderTemplate>
                                <asp:Label ID="lblEditInformation" Text="Edit Attendee Info" runat="server"></asp:Label>
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="MiddleTextCenter"
                                Width="150px" />
                            <ItemTemplate>
                             <%--'Anil B for Issue 14381 on 19-03-2013
                              'Add Command argument on Image for editing record--%>
                                <asp:ImageButton ID="btnEditAttendee"  runat="server" CommandName="EditRow" CommandArgument='<%#Eval("RowNumber") %>' ImageUrl="~/Images/Edit.png" />
                                <asp:LinkButton ID="lnkEditInfo" Text="Edit" runat="server" Font-Underline="true"
                                    CommandName="EditRow" CommandArgument='<%#Eval("RowNumber") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle CssClass="GridViewHeaderStyle LeftTextCenter" HorizontalAlign="Center" />
                            <HeaderTemplate>
                                <asp:Label ID="lblDelete" Text="Delete Attendee" runat="server"></asp:Label>
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btndelete" runat="server" ImageUrl="~/Images/Delete.png" CommandName="DeleteRow"
                                    CommandArgument='<%#Eval("RowNumber") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <HeaderStyle CssClass="GridViewHeaderStyle LeftTextCenter" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblFoodPreference1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Attendee FoodPreferenceID")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <HeaderStyle CssClass="GridViewHeaderStyle LeftTextCenter" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="plblTravelPreference" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Attendee TravelPreferenceID")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <HeaderStyle CssClass="GridViewHeaderStyle LeftTextCenter" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="plblGolfHandicap" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Attendee GolfHandicap")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <HeaderStyle CssClass="GridViewHeaderStyle LeftTextCenter" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="plblSpecialRequest" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Attendee SpecialRequest")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <HeaderStyle CssClass="GridViewHeaderStyle LeftTextCenter" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="plblotherPreference" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Attendee OtherPreference")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </div>
        <div class="dvCheckoutButton">
            <table class="tblFullWidth" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="tdCheckoutButton">
                        <asp:LinkButton ID="lnkMeetingPage" Text="Back to Event Page" runat="server" Font-Underline="true"
                            Font-Bold="true"></asp:LinkButton>
                        &nbsp;&nbsp;<asp:Button ID="btnAddRegistrant" ValidationGroup="Add" CssClass="submitBtn"
                            runat="server" TabIndex="14" Text="Proceed to checkout" />
                        <asp:Button ID="btnUpdateAttendeeInfo" ValidationGroup="Done" CssClass="submitBtn"
                            runat="server" TabIndex="15" Text="Update" />
                    </td>
                </tr>
            </table>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnAddRegistrant" />
        <asp:PostBackTrigger ControlID="lnkMeetingPage" />
        <asp:PostBackTrigger ControlID="btnUpdateAttendeeInfo" />
        <asp:PostBackTrigger ControlID="grdAddMember" />
    </Triggers>
</asp:UpdatePanel>
</div>
<asp:UpdatePanel ID="UpdatePanel4" runat="server">
    <ContentTemplate>
        <rad:RadWindow ID="popEditAttendee" runat="server" Width="700px" Height="340px" CssClass="popEditAttendeeWindow"
            Modal="True" Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
            ForeColor="#BDA797" Title="Edit Attendee" Behavior="None" IconUrl="~/Images/Attendee_16.png">
            <ContentTemplate>
           
                <table class="tblEditAtendee" cellpadding="0" cellspacing="0">   
                <%-- 'Anil B for Issue 14381 on 19-03-2013
                'Add lable to Show error--%>
                 <tr>
                        <td class="" colspan="4">
                             <asp:Label ID="lblPopErrorMessage" Text="" Visible="false" ForeColor="Red" runat="server" ></asp:Label>
                        </td>                        
                    </tr>


                    <tr>
                        <td class="leftColumnMeetingRegistrationPopupEdit">
                         <%--'Anil B for Issue 14381 on 19-03-2013
                              'Add Required field symbol--%>
                        <span class="RequiredField">*</span>
                            First Name:
                        </td>
                        <td class="rightColumnMeetingRegistration">
                            <asp:TextBox ID="txtPopFirstName" CssClass="txtMeetingRegistration" TabIndex="20"
                                runat="server"></asp:TextBox>    
                                                         
                        </td>
                        <td class="lblBadgeInformation" colspan="2">
                            Badge Information
                        </td>
                    </tr>
                    <tr>
                        <td class="leftColumnMeetingRegistrationPopupEdit">
                        <span class="RequiredField">*</span>
                            Last Name:
                        </td>
                        <td class="rightColumnMeetingRegistration">
                            <asp:TextBox ID="txtPopLastName" runat="server" CssClass="txtMeetingRegistration"
                                TabIndex="21"></asp:TextBox>                      

                        </td>
                        <td class="leftColumnMeetingRegistrationPopupEdit">
                            Name:
                        </td>
                        <td class="rightColumnMeetingRegistration">
                            <asp:TextBox ID="txtPopBadgeName" CssClass="txtMeetingRegistration" TabIndex="24"
                                runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="leftColumnMeetingRegistrationPopupEdit">
                        <span class="RequiredField">*</span>
                            Email:
                        </td>
                        <td class="rightColumnMeetingRegistration">
                            <asp:TextBox ID="txtPopEmail" runat="server" CssClass="txtMeetingRegistration" TabIndex="22"></asp:TextBox>
                              <%--Suraj Issue 15210 ,3/14/13 RegularExpressionValidator validator --%>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter a valid Email Address."
                                                ControlToValidate="txtPopEmail" ValidationGroup="EditProfileControl" Display="None" ></asp:RegularExpressionValidator>
                        </td>
                        <td class="leftColumnMeetingRegistrationPopupEdit">
                            Title:
                        </td>
                        <td class="rightColumnMeetingRegistration">
                            <asp:TextBox ID="txtPopBadgeTitle" runat="server" CssClass="txtMeetingRegistration"
                                TabIndex="25"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="leftColumnMeetingRegistrationPopupEdit">
                        </td>
                        <td class="rightColumnMeetingRegistration">
                        </td>
                        <td class="leftColumnMeetingRegistrationPopupEdit">
                            Company:
                        </td>
                        <td class="rightColumnMeetingRegistration">
                            <asp:TextBox ID="txtPopBadgeCompany" runat="server" CssClass="txtMeetingRegistration"
                                TabIndex="26"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="lblAdendeePreference">
                            Attendee Preference
                        </td>
                        <td class="leftColumnMeetingRegistration">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="leftColumnMeetingRegistrationPopupEdit">
                            Food Pref:
                        </td>
                        <td class="rightColumnMeetingRegistration">
                            <asp:DropDownList ID="ddlPopFoodPreference" runat="server" CssClass="txtMeetingRegistration ddlTravelPreference"
                                TabIndex="27">
                            </asp:DropDownList>
                        </td>
                        <td class="leftColumnMeetingRegistrationPopupEdit">
                            Travel Pref:
                        </td>
                        <td class="rightColumnMeetingRegistration">
                            <asp:DropDownList ID="ddlPopTravelPreference" runat="server" CssClass="txtMeetingRegistration ddlTravelPreference"
                                TabIndex="28">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="leftColumnMeetingRegistrationPopupEdit">
                            Golf Handicap:
                        </td>
                        <td class="rightColumnMeetingRegistration">
                            <asp:TextBox ID="txtPopGolfHandicap" runat="server" CssClass="txtMeetingRegistration"
                                TabIndex="29"></asp:TextBox>
                        </td>
                        <td class="leftColumnMeetingRegistrationPopupEdit">
                            Special Request:
                        </td>
                        <td class="rightColumnMeetingRegistration">
                            <asp:TextBox ID="txtPopSpecialRequest" runat="server" CssClass="txtMeetingRegistration"
                                TabIndex="30"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="leftColumnMeetingRegistrationPopupEdit">
                            Other Pref:
                        </td>
                        <td colspan="2" class="rightColumnMeetingRegistration">
                            <asp:TextBox ID="txtPopOtherPreference" runat="server" CssClass="txtMeetingRegistration"
                                TabIndex="31"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                        <td align="right" class="tdAttendeeEditButton">
                            <asp:Button ID="btnPopUpOk" class="submitBtn btnSaveCancel" TabIndex="32" runat="server"
                                Text="Save" ValidationGroup="DoneOnEdit" />&nbsp;&nbsp;
                            <asp:Button ID="btnPopUpCancel" runat="server" Text="Cancel" class="submitBtn btnSaveCancel"
                                TabIndex="33" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HiddenField runat="server" ID="hgrdindex" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </rad:RadWindow>
        <rad:RadWindow ID="radDuplicateUser" runat="server" Width="650px" Height="120px"
            Modal="True" Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
            ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="Alert" Behavior="None">
            <ContentTemplate>
                <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                    height: 100%; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblAlert" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnok" runat="server" Text="OK" Width="70px" class="submitBtn" OnClick="btnok_Click"
                                ValidationGroup="ok" />&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </rad:RadWindow>
        <rad:RadWindow ID="radSimilarRecords" runat="server" CssClass="PopupRadSimilarRecords"
            Skin="Default" Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
            ForeColor="#BDA797" IconUrl="~/Images/crossdelete.png" Title="Error" Behavior="None">
            <ContentTemplate>
                <table>
                    <tr>
                        <td style="padding-left: 5px; padding-right: 5px;">
                            This Email ID Already Exists. Is this the person you are looking for?
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 5px; padding-right: 5px;">
                            <asp:GridView ID="grdmember" AutoGenerateColumns="false" runat="server" ShowFooter="False"
                                AllowPaging="true" PageSize="5">
                                <Columns>
                                    <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" />
                                    <asp:TemplateField HeaderText="Member">
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Image ID="imgmember" CssClass="imgmember" runat="server" />
                                                    </td>
                                                    <td class="memberListtdReg">
                                                        <asp:Label ID="lblMember" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FirstLast") %>'
                                                            Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="padding-top: 5px; padding-right: 5px;">
                            <asp:Button ID="btnGetData" runat="server" Text="Yes" class="submitBtn" />
                            &nbsp;
                            <asp:Button ID="btnNo" runat="server" Text="No" class="submitBtn" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </rad:RadWindow>
        <rad:RadWindow ID="radValidateGrdRec" runat="server" Width="650px" Height="120px"
            Modal="True" Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
            ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="Alert" Behavior="None">
            <ContentTemplate>
                <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                    height: 100%; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                    <tr>
                        <td align="left">
                            <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="This Attendee is already added. Please use a different email address or contact Customer Service for assistance."></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnVGROk" runat="server" Text="OK" Width="70px" class="submitBtn"
                                OnClick="btnVGROk_Click" ValidationGroup="ok" />&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </rad:RadWindow>
        <rad:RadWindow ID="radChangeEmail" runat="server" Width="220px" Height="100px" Modal="True"
            Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
            ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="Alert" Behavior="None">
            <ContentTemplate>
                <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                    height: 100%; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                    <tr>
                        <td align="center">
                            <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Please use different Email ID."></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnChangeEamilOk" runat="server" Text="OK" Width="50px" class="submitBtn"
                                OnClick="btnChangeEamilOk_Click" ValidationGroup="ok" />&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </rad:RadWindow>
        
       

        <rad:RadWindow ID="radPopUpEditListSession" runat="server" Width="850px" Height="300px"
            CssClass="dvPopUpEditListSession" Skin="Default" Modal="True" BackColor="#f4f3f1"
            VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797" IconUrl="~/Images/edit-sessions.png"
            Title="Edit List of Sessions" Behavior="None">
            <ContentTemplate>
                <table class="tblEditAtendee" cellpadding="0" cellspacing="0" >
                    <tr>
                        <td class="tdAttendeeInfoEditSession">
                            <table>
                                <tr>
                                    <td colspan="2" class="tdlstSessionAttendeeName">
                                        Attendee Name:
                                    </td>
                                    <td class="lblAtendeeNemeFieldonRegistration">
                                        <asp:Label ID="lblAttendeeName" runat="server"></asp:Label>
                                    </td>
                                    <td class="tdlstSessionAttendeeName">
                                        Email ID:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblEmailID" class="lblAtendeeNemeFieldonRegistration" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <asp:HiddenField ID="hdnGrdRowIndex" runat="server" Visible="false" Value="" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="tdGrdMeetingSession">
                            <div class="dvGrdMeetingSession"> 
                                <rad:RadGrid ID="grdMeetingSession" runat="server" AutoGenerateColumns="False" Width="99%" 
                                    AllowFilteringByColumn="false" AllowSorting="True" AllowPaging="true" PagerStyle-AlwaysVisible ="true">
                                    <GroupingSettings CaseSensitive="false" />
                                    <GroupingSettings CaseSensitive="false" />
                                    <GroupingSettings CaseSensitive="false" />
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView AllowFilteringByColumn="false" AllowSorting="True">
                                        <Columns>
                                            <rad:GridTemplateColumn HeaderText="Select" AllowFiltering="false">
                                                <ItemStyle HorizontalAlign="left"></ItemStyle>
                                                <HeaderStyle HorizontalAlign="left" Width="60px" />
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAllSession" runat="server" OnCheckedChanged="ToggleSelectedState"
                                                        AutoPostBack="True" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSession" runat="server" />
                                                </ItemTemplate>
                                            </rad:GridTemplateColumn>
                                            <rad:GridTemplateColumn HeaderText="Session" AllowFiltering="true" DataField="WebName"
                                                SortExpression="WebName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false">
                                                <ItemTemplate>
                                                 <%--'Anil B for Issue 14381 on 19-03-2013
                                                    'Add CSS to remove text decoration on mouse hover --%>
                                                    <asp:HyperLink ID="lnkWebName" CssClass="hlkMeetingSessionGrid" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebName") %>'></asp:HyperLink></ItemTemplate>
                                                <ItemStyle Font-Size="10pt" />
                                            </rad:GridTemplateColumn>
                                            <rad:GridBoundColumn UniqueName="gridDateTimeColumnStartDate" HeaderText="Start Date & Time" DataField="StartDate" AllowFiltering="true"
                                                FilterControlWidth="100px" SortExpression="StartDate" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                           
                                            <rad:GridBoundColumn UniqueName="gridDateTimeColumnEndDate" HeaderText="End Date & Time" DataField="EndDate" AllowFiltering="true"
                                                FilterControlWidth="100px" SortExpression="EndDate" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                          
                                            <rad:GridTemplateColumn HeaderText="Place" AllowFiltering="true" DataField="Location"
                                                SortExpression="Location" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Location") %>'></asp:Label><asp:Label
                                                        ID="lblProductID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem,"ProductID") %>'></asp:Label></ItemTemplate>
                                                <ItemStyle Font-Size="10pt" />
                                            </rad:GridTemplateColumn>
                                            <rad:GridBoundColumn DataField="Price" HeaderText="Registration Price" HeaderStyle-HorizontalAlign="Right"
                                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="140px" AllowFiltering="true"
                                                SortExpression="Price" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false" />
                                        </Columns>
                                    </MasterTableView>
                                </rad:RadGrid>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="tdSelectSessionButtunSection">
                            <asp:Button ID="btnEditSession" CssClass="submitBtn btnSaveCancel"  TabIndex="32" runat="server"
                                Text="OK" />&nbsp;&nbsp;
                            <asp:Button ID="btnCancelSession" runat="server" Text="Cancel" CssClass="submitBtn btnSaveCancel"
                                TabIndex="33" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </rad:RadWindow>
     
        <rad:RadWindow ID="radMeetingSessionCountInfo" runat="server" Width="350px" Height="100px"
            Modal="True" Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
            ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="Alert" Behavior="None">
            <ContentTemplate>
                <table class="tblEditAtendee" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblMeetingCountZero" runat="server" Font-Bold="true" Text="There are no sessions associated with this meeting."></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnMeetingSessionCountInfo" runat="server" Text="OK" class="submitBtn"
                                ValidationGroup="ok" />&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </rad:RadWindow>
        <rad:RadWindow ID="radMeetingSessionConflictMessage" runat="server" Modal="True" Width="400px"
            Height="175px" CssClass="winMeetingConflicts" Skin="Default" BackColor="#f4f3f1"
            VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797" IconUrl="~/Images/Alert.png"
            Title="Alert" Behavior="None">
            <ContentTemplate>
                <div>
                    <asp:ListView ID="lstErrorMessage" runat="server">
                        <ItemTemplate>
                            <asp:Label ID="lblErrorMessage" runat="server" Text='<% #eval("ErrorMessage") %>'></asp:Label>
                            <br />
                        </ItemTemplate>
                    </asp:ListView>
                </div>
                <div class="dvBottomFieldset">
                </div>
                <div class="btnOkMeetingPage">
                    <asp:Button ID="btnMeetingSessionConflictOK" runat="server" Text="OK" class="submitBtn"
                        ValidationGroup="ok" />&nbsp;&nbsp;
                </div>
            </ContentTemplate>
        </rad:RadWindow>
    </ContentTemplate>
</asp:UpdatePanel>
<cc3:User ID="User1" runat="server" />
