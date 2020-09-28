<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/MeetingRegistration__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.MeetingRegistrationControl__c" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%--<%@ Register TagName="Meetings" TagPrefix="uc1" Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Meetings/Meeting.ascx" %>--%>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<!-- Susan Wong, ticket #19369 add load screen -->
<div class="raDiv" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" AssociatedUpdatePanelID="updatePanelMain" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing"><div class="loading-bg">
                <img src="/Images/CAITheme/bx_loader.gif" />
                <span>LOADING...<br /><br />
                    Please do not leave or close this window while the request is processing.</span></div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>


<div class="cai-table meeting-register mobile-table">
    <asp:UpdatePanel ID="updatePanelMain" runat="server" ChildrenAsTriggers="True" class="meeting-registration" >
        <ContentTemplate>
            <asp:Label ID="lblError" runat="server" Visible="False" Font-Bold="True" ForeColor="Red"></asp:Label>
            <div class="page-message">
                <asp:Label ID="lblAddedAtendee" runat="server" Text="Added attendees"></asp:Label>
            </div>
            <div>
                <asp:Panel ID="pnlAddMember" runat="server" ScrollBars="Auto">
                    <asp:GridView ID="grdAddMember" AutoGenerateColumns="false" runat="server" ShowFooter="False"
                        AllowPaging="false" ClientIDMode="Static" >
                        <Columns>
                            <asp:BoundField DataField="RowNumber" HeaderText="Row number" Visible="false" />
                            <asp:TemplateField>
                                <HeaderStyle CssClass="rgHeader" />
                                <HeaderTemplate>
                                    <asp:Label ID="lblFname" Text="Name" runat="server"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text="Name:" CssClass="mobile-label"></asp:Label>
                                    <span class="cai-table-data">
                                    <asp:Label ID="lblAtendeeFirstName" runat="server"  Text='<%#DataBinder.Eval(Container.DataItem,"first Name")%>'></asp:Label>
                                    <asp:Label ID="lblAtendeeLastName" runat="server"  Text='<%#DataBinder.Eval(Container.DataItem,"Last Name")%>'></asp:Label>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle CssClass="rgHeader" />
                                <HeaderTemplate>
                                    <asp:Label ID="lblEmail" Text="Email" runat="server"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text="Email:" CssClass="mobile-label"></asp:Label>
                                    <asp:Label ID="lblAttendeeEmail" CssClass="cai-table-data" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Email")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <HeaderStyle CssClass="rgHeader" />
                                <HeaderTemplate>
                                    <asp:Label ID="lblBadgeInformation" Text="Badge information" runat="server"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblBadgeName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Badge Information Name")%>'></asp:Label><br />
                                    <asp:Label ID="lblBadgeTitle" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Badge Information Title")%>'></asp:Label><br />
                                    <asp:Label ID="lblBadgeCompany" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Badge Information Company")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField>
                                <HeaderStyle CssClass="rgHeader" />
                                <HeaderTemplate>
                                    <asp:Label ID="lbllstSessions" Text="List of sessions for attendee" runat="server" Visible="false"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text="Sessions:" CssClass="mobile-label" Visible="false"></asp:Label>
                                    <asp:LinkButton ID="lnklstSessonForAttendee" CssClass="cai-table-data" Text="View/edit sessions" runat="server"
                                        CommandName="EditSession" CommandArgument='<%#Eval("RowNumber") %>' Visible="false"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <asp:TemplateField Visible="false">
                                <HeaderStyle CssClass="rgHeader" />
                                <HeaderTemplate>
                                    <asp:Label ID="lblEditInformation" Text="Edit attendee info" runat="server"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text="Edit attendee info:" CssClass="mobile-label"></asp:Label>
                                    <asp:ImageButton ID="btnEditAttendee" runat="server" CssClass="cai-table-data" CommandName="EditRow" CommandArgument='<%#Eval("RowNumber") %>' ImageUrl="~/Images/Edit.png" />
                                    <asp:LinkButton ID="lnkEditInfo" CssClass="no-mob" Text="Edit" runat="server" Font-Underline="true"
                                        CommandName="EditRow" CommandArgument='<%#Eval("RowNumber") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField>
                                <HeaderStyle CssClass="rgHeader" />
                                <HeaderTemplate>
                                    <asp:Label ID="lblDelete" Text=" " runat="server"></asp:Label>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" height="40px"/>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text=" " CssClass="mobile-label"></asp:Label>
                                    <asp:Button ID="btndelete" runat="server" CssClass="cai-table-data submitBtn cai-btn-red-inverse" Text="Remove" CommandName="DeleteRow"
                                        CommandArgument='<%#Eval("RowNumber") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <HeaderStyle CssClass="rgHeader" />
                                <ItemTemplate>
                                    <asp:Label ID="lblFoodPreference1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Attendee FoodPreferenceID")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <HeaderStyle CssClass="rgHeader" />
                                <ItemTemplate>
                                    <asp:Label ID="plblTravelPreference" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Attendee TravelPreferenceID")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <HeaderStyle CssClass="rgHeader" />
                                <ItemTemplate>
                                    <asp:Label ID="plblGolfHandicap" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Attendee GolfHandicap")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <HeaderStyle CssClass="rgHeader" />
                                <ItemTemplate>
                                    <asp:Label ID="plblSpecialRequest" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Attendee SpecialRequest")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <HeaderStyle CssClass="rgHeader" />
                                <ItemTemplate>
                                    <asp:Label ID="plblotherPreference" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Attendee OtherPreference")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </div>
            <br />
            <asp:Button ID="addMoreAttendees" CssClass="submitBtn cai-btn-red-inverse" runat="server" Text="Add More Attendees" />
            <%--Added by sheela as part Bug #18439--%>
	        <asp:Label ID="lblAvailSpace" runat="server" Text="" Visible="false"></asp:Label>
            <div id="meetingTitle" style="display: none;">
                <div class="cai-form clearfix" id="addAttendeeTable">
                    <div id="trAttendee" runat="server">
                        <span class="form-title">Add attendee</span>

                        <div class="field-group">
                            <asp:Label ID="lblNameInfo" Text="Product name:" Font-Bold="true" runat="server"></asp:Label>
                            <asp:Label runat="server" ID="lblMeeting" /><br />
                            <asp:Label ID="lblPriceInfo" Text="Price:" Font-Bold="true" runat="server"></asp:Label>
                            <asp:Label ID="lblPrice" runat="server"></asp:Label>
                        </div>

                        <div class="field-group">
                            <asp:Label ID="lblAvailableSpaceText" runat="server" Text="" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblAvailableSpace" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        </div>

                        <div id="tblInner" runat="server" class="field-group">
                            <div class="page-message">
                                <cc2:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="False" />
                                <%--HP Issue#9091: changed message header to clarify functionality--%>
                                <asp:Label ID="lblTopMessageInfo" runat="server" Text=" Please verify the attendee and complete the form below. When done, click Add Attendee   for registrants to the meeting."></asp:Label>
                                <asp:ValidationSummary ID="vldSummary" ValidationGroup="Done" runat="server"></asp:ValidationSummary>
                            </div>
                            <div class="page-message">
                                <asp:Label ID="lblMeetingRegistrationError" runat="server" Visible="false"></asp:Label>
                            </div>
                        </div>

                        <div class="field-group">
                            <span class="RequiredField">*</span>
                            <asp:Label ID="lblMandetoryInfo" runat="server" Text="Mandatory fields"></asp:Label>

                        </div>
                    </div>
                    <div>

                        <div class="field-group">
                            <span class="label-title">* First name:</span>
                            <asp:TextBox ID="txtFirstName" CssClass="txtMeetingRegistration" runat="server" TabIndex="1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="vldAttendeeFname" runat="server" Font-Size="8pt"
                                ValidationGroup="Done" Display="None" ControlToValidate="txtFirstName" ErrorMessage="Please specify an attendee first name."></asp:RequiredFieldValidator>

                        </div>

                        <div class="field-group">
                            <span class="label-title RequiredField">* Last name:</span>
                            <asp:TextBox ID="txtLastName" runat="server" CssClass="txtMeetingRegistration" TabIndex="2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="vldAttendeeLname" runat="server" Font-Size="8pt"
                                ValidationGroup="Done" Display="None" ControlToValidate="txtLastName" ErrorMessage="Please specify an attendee last name."></asp:RequiredFieldValidator>

                        </div>

                        <div class="field-group">
                            <span class="label-title RequiredField">* Email:</span>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="txtMeetingRegistration" TabIndex="3" placeholder="Note: please use registered email address for members and for non-members a unique email address"></asp:TextBox>
                            <span style="font-size:12px;"><strong>Members</strong> must enter their <strong>registered email address</strong> to obtain <strong>CPD hours</strong> upon completing this course</span>			
                            <asp:RequiredFieldValidator ID="rfValidatorEmail" runat="server" Font-Size="8pt"
                                ValidationGroup="Done" Display="None" ControlToValidate="txtEmail" ErrorMessage="Please specify an attendee email address."></asp:RequiredFieldValidator>
                            <%--Suraj Issue 15210 ,2/11/13 RegularExpressionValidator validator --%>
                            <asp:RegularExpressionValidator ID="reValidatorEmail" runat="server" ErrorMessage="Please enter a valid email address."
                                ControlToValidate="txtEmail" ValidationGroup="Done" Display="None" ValidationExpression="[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+(?:[A-Z]{2}|com|COM|org|ORG|net|NET|edu|EDU|gov|GOV|mil|MIL|biz|BIZ|info|INFO|mobi|MOBI|name|NAME|aero|AERO|asia|ASIA|jobs|JOBS|museum|MUSEUM|in|IN|ie|IE|co|CO)\b"></asp:RegularExpressionValidator>
                        </div>
                       <%--below div added for Redmine # 20848
						<div class="field-group">
							<span class="label-title RequiredField">* Date of Birth:</span>
						 	 <telerik:RadDatePicker ID="rdpBirthdate" runat="server" CausesValidation="True"   ></telerik:RadDatePicker>
							 
						</div>
						End Redmine # 20848--%>

                        <div class="field-group">
                            <span class="label-title">Comments/preferences:</span>
                            <asp:TextBox ID="txtSpecialRequest" runat="server" CssClass="txtMeetingRegistration" TabIndex="11"></asp:TextBox>
                        </div>

                        <div class="field-group actions">
                            <asp:Button ID="btnAddInfo" ToolTip="Add attendee record to grid" ValidationGroup="Done"
                                CssClass="submitBtn" TabIndex="13" runat="server" Text="Add attendee" />
                        </div>
                        <div>
                            <div class="dvSectionBadge" runat="server" visible="false">
                                <fieldset class="meetFieldSet">
                                    <legend class="meetLegend"><b>Badge information</b></legend>

                                    <div class="leftColumnMeetingRegistration">
                                        Name:
                                    </div>
                                    <div class="rightColumnMeetingRegistration">
                                        <asp:TextBox ID="txtBadgeName" CssClass="txtMeetingRegistration" TabIndex="5" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="leftColumnMeetingRegistration">
                                        Title:
                                    </div>
                                    <div class="rightColumnMeetingRegistration">
                                        <asp:TextBox ID="txtBadgeTitle" runat="server" CssClass="txtMeetingRegistration"
                                            TabIndex="6"></asp:TextBox>
                                    </div>

                                    <div class="leftColumnMeetingRegistration">
                                        Company:
                                    </div>
                                    <div class="rightColumnMeetingRegistration">
                                        <asp:TextBox ID="txtCompany" runat="server" CssClass="txtMeetingRegistration" TabIndex="7"></asp:TextBox>
                                    </div>

                                    <div class="dvBottomFieldset">
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                        <div>
                            <div class="dvSectionBadge" runat="server" visible="false">
                                <fieldset class="meetFieldSet">
                                    <legend class="meetLegend"><b>Attendee preferences</b></legend>

                                    <div class="leftColumnMeetingRegistrationPreferences">
                                        Food pref:
                                    </div>
                                    <div class="rightColumnMeetingRegistration">
                                        <asp:DropDownList ID="ddlFoodPreference" runat="server" CssClass="txtMeetingRegistration ddlTravelPreference"
                                            TabIndex="8">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="leftColumnMeetingRegistrationPreferences">
                                        Travel pref:
                                    </div>
                                    <div class="rightColumnMeetingRegistration">
                                        <asp:DropDownList ID="ddlTravelPreference" runat="server" CssClass="txtMeetingRegistration ddlTravelPreference"
                                            TabIndex="9">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="leftColumnMeetingRegistrationPreferences">
                                        Golf handicap:
                                    </div>
                                    <div class="rightColumnMeetingRegistration">
                                        <asp:TextBox ID="txtGolfHandicape" runat="server" CssClass="txtMeetingRegistration"
                                            TabIndex="10"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please enter a numeric value for golf handicap."
                                            ControlToValidate="txtGolfHandicape" ValidationGroup="Done" Display="None" ValidationExpression="^\d+(\.\d{1,4})?$"></asp:RegularExpressionValidator>
                                    </div>

                                    <div class="leftColumnMeetingRegistrationPreferences" valign="top">
                                        Other pref:
                                    </div>
                                    <div class="rightColumnMeetingRegistration">
                                        <asp:TextBox ID="txtOtherPreference" runat="server" CssClass="txtMeetingRegistration"
                                            TabIndex="12"></asp:TextBox>
                                    </div>

                                    <div class="dvBottomFieldset">
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

            <div id="dvLeft" class="dvLeftStyle">
                <div class="actions-left">
                    <asp:Button ID="lnkMeetingPage" Text="Back to Event Page" runat="server" CssClass="submitBtn no-button"></asp:Button>
                </div>
                <div class="actions-right">
                    <asp:Button ID="btnAddRegistrant" ValidationGroup="Add" CssClass="submitBtn"
                        runat="server" TabIndex="14" Text="Next"  OnClientClick="addToCartEvent(); return true;" />
                    <asp:Button ID="btnUpdateAttendeeInfo" ValidationGroup="Done" CssClass="submitBtn"
                        runat="server" TabIndex="15" Text="Update" />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAddRegistrant" />
            <asp:PostBackTrigger ControlID="lnkMeetingPage" />
            <asp:PostBackTrigger ControlID="btnUpdateAttendeeInfo" />
            <asp:PostBackTrigger ControlID="grdAddMember" />
        </Triggers>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <rad:RadWindow ID="popEditAttendee" runat="server" CssClass="popEditAttendeeWindow"
                Modal="True" Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
                ForeColor="#BDA797" Title="Edit attendee" Width="500px" Height="300px" Behavior="None" IconUrl="~/Images/Attendee_16.png">
                <ContentTemplate>
                    <asp:Label ID="lblPopErrorMessage" Text="" Visible="false" ForeColor="Red" runat="server"></asp:Label>
                    <table style="margin-top: 5px; margin-left: 5px; margin-right: 5px;">
                        <tr>
                            <td>
                                <span class="RequiredField">* First name:</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPopFirstName" CssClass="txtMeetingRegistration" runat="server" width="300px"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <span class="RequiredField">* Last name:</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPopLastName" runat="server" CssClass="txtMeetingRegistration" width="300px"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td><span class="RequiredField">* Email:</span> </td>
                            <td>
                                <asp:TextBox ID="txtPopEmail" runat="server" CssClass="txtMeetingRegistration" width="300px"></asp:TextBox>
                                <%--Suraj Issue 15210 ,3/14/13 RegularExpressionValidator validator --%>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter a valid email address."
                                    ControlToValidate="txtPopEmail" ValidationGroup="EditProfileControl" Display="None" ValidationExpression="[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+(?:[A-Z]{2}|com|COM|org|ORG|net|NET|edu|EDU|gov|GOV|mil|MIL|biz|BIZ|info|INFO|mobi|MOBI|name|NAME|aero|AERO|asia|ASIA|jobs|JOBS|museum|MUSEUM|in|IN|ie|IE|co|CO)\b"></asp:RegularExpressionValidator>
                            </td>
                        </tr>

                        <tr>
                            <td><span>Comments/preferences:</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPopSpecialRequest" runat="server" CssClass="txtMeetingRegistration"
                                    TabIndex="30" width="300px"></asp:TextBox>
                            </td>
                        </tr>

                        <tr style="display: none;">

                            <td class="lblBadgeInformation" colspan="2">Badge information
                            </td>

                            <td class="leftColumnMeetingRegistrationPopupEdit">Name:
                            </td>
                            <td class="rightColumnMeetingRegistration">
                                <asp:TextBox ID="txtPopBadgeName" CssClass="txtMeetingRegistration" TabIndex="24"
                                    runat="server"></asp:TextBox>
                            </td>

                            <td class="leftColumnMeetingRegistrationPopupEdit">Title:
                            </td>
                            <td class="rightColumnMeetingRegistration">
                                <asp:TextBox ID="txtPopBadgeTitle" runat="server" CssClass="txtMeetingRegistration"
                                    TabIndex="25"></asp:TextBox>
                            </td>

                            <td class="leftColumnMeetingRegistrationPopupEdit"></td>
                            <td class="rightColumnMeetingRegistration"></td>
                            <td class="leftColumnMeetingRegistrationPopupEdit">Company:
                            </td>
                            <td class="rightColumnMeetingRegistration">
                                <asp:TextBox ID="txtPopBadgeCompany" runat="server" CssClass="txtMeetingRegistration"
                                    TabIndex="26"></asp:TextBox>
                            </td>

                            <td colspan="2" class="lblAdendeePreference">Attendee preference
                            </td>
                            <td class="leftColumnMeetingRegistration"></td>
                            <td></td>

                            <td class="leftColumnMeetingRegistrationPopupEdit">Food pref:
                            </td>
                            <td class="rightColumnMeetingRegistration">
                                <asp:DropDownList ID="ddlPopFoodPreference" runat="server" CssClass="txtMeetingRegistration ddlTravelPreference"
                                    TabIndex="27">
                                </asp:DropDownList>
                            </td>
                            <td class="leftColumnMeetingRegistrationPopupEdit">Travel pref:
                            </td>
                            <td class="rightColumnMeetingRegistration">
                                <asp:DropDownList ID="ddlPopTravelPreference" runat="server" CssClass="txtMeetingRegistration ddlTravelPreference"
                                    TabIndex="28">
                                </asp:DropDownList>
                            </td>

                            <td class="leftColumnMeetingRegistrationPopupEdit">Golf handicap:
                            </td>
                            <td class="rightColumnMeetingRegistration">
                                <asp:TextBox ID="txtPopGolfHandicap" runat="server" CssClass="txtMeetingRegistration"
                                    TabIndex="29"></asp:TextBox>
                            </td>

                            <td class="leftColumnMeetingRegistrationPopupEdit">Other pref:
                            </td>
                            <td colspan="2" class="rightColumnMeetingRegistration">
                                <asp:TextBox ID="txtPopOtherPreference" runat="server" CssClass="txtMeetingRegistration"
                                    TabIndex="31"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td class="actions">
                                <asp:Button ID="btnPopUpOk" class="submitBtn btnSaveCancel" runat="server" Text="Save" ValidationGroup="DoneOnEdit" />
                                <asp:Button ID="btnPopUpCancel" runat="server" Text="Cancel" class="submitBtn btnSaveCancel" />
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
            <rad:RadWindow ID="radDuplicateUser" runat="server" Height="220px" width="450px"
                Modal="True" Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
                ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="Alert" Behavior="None">
                <ContentTemplate>
                    <table cellpadding="0" cellspacing="0" style="background-color: #f4f3f1; height: 100%; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblAlert" runat="server" Text="" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
				&nbsp;
                                <asp:Button ID="btnok" runat="server" Text="OK" class="submitBtn" OnClick="btnok_Click"
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
                            <td style="padding-left: 5px; padding-right: 5px;">This email ID already exists. Is this the person you are looking for?
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 5px; padding-right: 5px;">
                                <asp:GridView ID="grdmember" AutoGenerateColumns="false" runat="server" ShowFooter="False"
                                    AllowPaging="true" PageSize="5">
                                    <PagerStyle CssClass="sd-pager" />
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
            <rad:RadWindow ID="radValidateGrdRec" runat="server"
                Modal="True" Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
                ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="Alert" Behavior="None">
                <ContentTemplate>
                    <table style="background-color: #f4f3f1; height: 100%; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                        <tr>
                            <td align="left">
                                <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="This attendee is already added. Please use a different email address or contact customer service for assistance."></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnVGROk" runat="server" Text="OK" class="submitBtn" OnClick="btnVGROk_Click" ValidationGroup="ok" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </rad:RadWindow>
            <rad:RadWindow ID="radChangeEmail" runat="server" Modal="True"
                Skin="Default" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None"
                ForeColor="#BDA797" IconUrl="~/Images/Alert.png" Title="Alert" Behavior="None">
                <ContentTemplate>
                    <table style="background-color: #f4f3f1; height: 100%; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                        <tr>
                            <td align="center">
                                <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Please use different email ID."></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnChangeEamilOk" runat="server" Text="OK" class="submitBtn"
                                    OnClick="btnChangeEamilOk_Click" ValidationGroup="ok" />&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </rad:RadWindow>



            <rad:RadWindow ID="radPopUpEditListSession" runat="server"
                CssClass="dvPopUpEditListSession" Skin="Default" Modal="True" BackColor="#f4f3f1"
                VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797" IconUrl="~/Images/edit-sessions.png"
                Title="Edit list of sessions" Behavior="None">
                <ContentTemplate>
                    <table class="tblEditAtendee" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="tdAttendeeInfoEditSession">
                                <table>
                                    <tr>
                                        <td colspan="2" class="tdlstSessionAttendeeName">Attendee name:
                                        </td>
                                        <td class="lblAtendeeNemeFieldonRegistration">
                                            <asp:Label ID="lblAttendeeName" runat="server"></asp:Label>
                                        </td>
                                        <td class="tdlstSessionAttendeeName">Email ID:
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
                                    <rad:RadGrid ID="grdMeetingSession" runat="server" AutoGenerateColumns="False"
                                        AllowFilteringByColumn="false" AllowSorting="True" AllowPaging="true" PagerStyle-AlwaysVisible="true">
                                        <PagerStyle CssClass="sd-pager" />
                                        <GroupingSettings CaseSensitive="false" />
                                        <GroupingSettings CaseSensitive="false" />
                                        <GroupingSettings CaseSensitive="false" />
                                        <GroupingSettings CaseSensitive="false" />
                                        <MasterTableView AllowFilteringByColumn="false" AllowSorting="True">
                                            <Columns>
                                                <rad:GridTemplateColumn HeaderText="Select" AllowFiltering="false">

                                                    <HeaderStyle />
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
                                                        <asp:HyperLink ID="lnkWebName" CssClass="hlkMeetingSessionGrid" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebName") %>'></asp:HyperLink>
                                                    </ItemTemplate>

                                                </rad:GridTemplateColumn>
                                                <rad:GridBoundColumn UniqueName="gridDateTimeColumnStartDate" HeaderText="Start date & time" DataField="StartDate" AllowFiltering="true"
                                                    FilterControlWidth="100px" SortExpression="StartDate" AutoPostBackOnFilter="true"
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" />

                                                <rad:GridBoundColumn UniqueName="gridDateTimeColumnEndDate" HeaderText="End date & time" DataField="EndDate" AllowFiltering="true"
                                                    FilterControlWidth="100px" SortExpression="EndDate" AutoPostBackOnFilter="true"
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" />

                                                <rad:GridTemplateColumn HeaderText="Place" AllowFiltering="true" DataField="Location"
                                                    SortExpression="Location" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                    ShowFilterIcon="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Location") %>'></asp:Label><asp:Label
                                                            ID="lblProductID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem,"ProductID") %>'></asp:Label>
                                                    </ItemTemplate>

                                                </rad:GridTemplateColumn>
                                                <rad:GridBoundColumn DataField="Price" HeaderText="Registration price" HeaderStyle-HorizontalAlign="Right"
                                                    ItemStyle-HorizontalAlign="Right" AllowFiltering="true"
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
                                <asp:Button ID="btnEditSession" CssClass="submitBtn btnSaveCancel" TabIndex="32" runat="server"
                                    Text="OK" />&nbsp;&nbsp;
                            <asp:Button ID="btnCancelSession" runat="server" Text="Cancel" CssClass="submitBtn btnSaveCancel"
                                TabIndex="33" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </rad:RadWindow>

            <rad:RadWindow ID="radMeetingSessionCountInfo" runat="server"
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
            <rad:RadWindow ID="radMeetingSessionConflictMessage" runat="server" Modal="True" CssClass="winMeetingConflicts" Skin="Default" BackColor="#f4f3f1"
                VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797" IconUrl="~/Images/Alert.png"
                Title="Alert" Behavior="None">
                <ContentTemplate>
                    <div>
                        <asp:ListView ID="lstErrorMessage" runat="server">
                            <ItemTemplate>
                                <asp:Label ID="lblErrorMessage" runat="server" Text='<% #Eval("ErrorMessage")%>'></asp:Label>
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

</div>

<script type="text/javascript">
    jQuery(function ($) {
        $('document').ready(function () {
            if ($.trim($('.dvAddedAtendee').html()).length <= 0) {
                $('#<%= btnAddInfo.ClientID %>').click();
            }
        });

        $(document).on('click', "#<%=addMoreAttendees.ClientID%>", function () {
            $('#meetingTitle').css('display', 'block');
            $(this).hide();
            return false;
        });
    });

    
</script>
