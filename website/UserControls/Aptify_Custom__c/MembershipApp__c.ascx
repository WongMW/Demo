<%@ Control Language="VB" Debug="true" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/MembershipApp__c.ascx.vb"
    Inherits="MembershipApp__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register Assembly="Telerik.Web.UI" TagPrefix="telerik" Namespace="Telerik.Web.UI" %>
<%--<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<script type="text/javascript">
    function printDiv() {
        document.getElementById('<%= lnkChangeContactDetails.ClientID %>').style.visibility = "hidden";
        document.getElementById('<%= lnkChangeEmploymentDetails.ClientID %>').style.visibility = "hidden";
        var content = "<html><body>";
        content += document.getElementById('<%= divContent.ClientID %>').innerHTML;
        content += "</body>";
        content += "</html>";
        document.getElementById('<%= lnkChangeContactDetails.ClientID %>').style.visibility = 'visible';
        document.getElementById('<%= lnkChangeEmploymentDetails.ClientID %>').style.visibility = 'visible';
        var printWin = window.open('', '', 'left=0,top=0,width=1000,height=500,toolbar=0,scrollbars=0,status =0');
        printWin.document.write(content);
        printWin.document.close();
        printWin.focus();
        printWin.print();
        printWin.close();
    }

    jQuery(function ($) {
        $('.form-title').click(function () {
            var image = $(this).find('input[type="image"]');
            if (image.attr('src') === '../Images/uparrow.jpg') {
                image.attr('src', '../Images/downarrow.jpg');
            } else {
                image.attr('src', '../Images/uparrow.jpg');
            }
        });
    });
</script>

<div class="cai-form">
    <div class="cai-form-content">
        <asp:Button ID="btnPrint" runat="server" CssClass="btn print-btn" Text="Print" />
        <asp:ImageButton ID="imbExpand" runat="server" ImageUrl="~/Images/downarrow.jpg" Visible="false" AlternateText="(Show Details...)" />
        <asp:ImageButton ID="imbCollaps" runat="server" ImageUrl="~/Images/uparrow.jpg" Visible="false"
            AlternateText="(Show Details...)" />

        <div id="divContent" runat="server">
            <div id="MainTable">
                <img runat="server" id="companyLogo" src="" />
                <asp:Label ID="lblAddress" runat="server" />

                <span class="label-title">Application Type:</span>
                <asp:Label ID="lblApplicationWebName" runat="server" />
                <span class="label-title">Application Status:</span>
                <asp:Label ID="lblApplicationType" runat="server" />

                <div id="trQuatAmt" runat="server" visible="false">
                    <span class="label-title">Quotation Amount:</span>
                    <asp:Label ID="lblQuatCuurency" runat="server" />
                    <asp:Label ID="lblQuationAmount" runat="server" />
                </div>

                <asp:Label ID="lblMsg" runat="server" />
            </div>
        </div>
    </div>
    <div id="Genral" runat="server">
        <asp:Panel ID="pnlgeneral" runat="server">
            <div class="tdPersonalInfo" style="width: 100%">
                <span class="form-title">APPLICATION CONTACT DETAILS
                        <span style="float: right;">
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/uparrow.jpg" AlternateText="(Show Details...)" />
                        </span>
                </span>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlgeneral1" runat="server">
            <div class="cai-form-content">
                <div class="field-group">
                    <span class="label-title">To the Council of Chartered Accountants Ireland:
                    </span>

                    <span>I,
                    </span>

                    <span>
                        <asp:Label ID="lblfname" runat="server"></asp:Label>
                        <asp:Label ID="lblsname" runat="server"></asp:Label>
                        <asp:Label ID="lbllname" runat="server"></asp:Label>
                    </span>
                    <span>
                        <asp:LinkButton ID="lnkChangeContactDetails" CausesValidation="false" runat="server">Change Contact Details</asp:LinkButton>
                    </span>
                </div>

                <div class="address-section-half">
                    <div class="field-group">
                        <span class="label-title">Address Line1:</span>
                        <asp:TextBox ID="TxtLine1" Enabled="false" runat="server"></asp:TextBox>

                        <span class="label-title">Address Line2:</span>
                        <asp:TextBox ID="TxtLine2" Enabled="false" runat="server"></asp:TextBox>

                        <span class="label-title">Address Line3:</span>
                        <asp:TextBox ID="TxtLine3" Enabled="false" runat="server"></asp:TextBox>

                        <span class="label-title">Telephone no:</span>
                        <asp:TextBox ID="TxtLandlineNo" runat="server"></asp:TextBox>

                        <span class="label-title">Mobile No:</span>
                        <asp:TextBox ID="Txtpmobileno" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="address-section-half">
                    <div class="field-group">
                        <span class="label-title">Country:</span>
                        <asp:DropDownList ID="cmbCountry" Enabled="false" CssClass="cmbUserProfileCountry" runat="server"
                            AutoPostBack="true">
                        </asp:DropDownList>

                        <span class="label-title">
                            <asp:Label ID="Label1" runat="server"> ZIP Code:</asp:Label>
                        </span>
                        <asp:TextBox ID="txtZipCode" Enabled="false" CssClass="txtUserProfileZipCode" runat="server"></asp:TextBox>

                        <span class="label-title">
                            <asp:Label ID="Label5" runat="server"> City:</asp:Label>
                        </span>
                        <asp:TextBox ID="txtCity" Enabled="false" CssClass="txtUserProfileCity" runat="server"></asp:TextBox>

                        <span class="label-title">
                            <asp:Label ID="Label9" runat="server"> State:</asp:Label>
                        </span>
                        <span>
                            <asp:DropDownList ID="cmbState" Enabled="false" CssClass="cmbUserProfileState" runat="server">
                            </asp:DropDownList>
                        </span>

                        <span class="label-title">
                            <span class="RequiredField">*</span>Email:
                        </span>
                        <span>
                            <asp:TextBox ID="TxtEmail" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtEmail"
                                Display="Dynamic" ErrorMessage="Email Required" Font-Size="X-Small" ForeColor="Red"></asp:RequiredFieldValidator>
                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TxtEmail"
                                Display="Dynamic" ErrorMessage="Invalid  Email " Font-Size="X-Small" ForeColor="Red"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>--%>
                            <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" Display="Dynamic"
                                ControlToValidate="TxtEmail" ErrorMessage="Invalid Email "
                                ForeColor="Red"></asp:RegularExpressionValidator>
                        </span>
                    </div>
                </div>

                <div class="field-group">
                    <span class="label-title">
                        <span class="RequiredField">*</span> Date of Birth:
                    </span>

                    <rad:RadDatePicker ID="rdpBirthdate" Enabled="false" runat="server" Calendar-ShowOtherMonthsDays="false"
                        MinDate="01/01/1900" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                    </rad:RadDatePicker>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Birth Date"
                        ForeColor="Red" ControlToValidate="rdpBirthdate"></asp:RequiredFieldValidator>
                </div>

                <div class="field-group">

                    <asp:Label ID="lblAssociateMemberText" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lblResiprocalText" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblResiprocalLoweerText" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lblResiprocalBLowerText" runat="server" Text="" Visible="false"></asp:Label>
                </div>

                <div class="field-group">
                    <asp:Label ID="lblRegistryDept" runat="server" Text=""></asp:Label>
                </div>

                <div class="address-section-half">
                    <div class="field-group">
                        <span class="label-title">SIGNATURE OF STUDENT:
                        </span>
                        <asp:TextBox ID="TextBox12" runat="server" Style="margin-left: 0px"></asp:TextBox>
                    </div>
                </div>

                <div class="address-section-half">
                    <div class="field-group">
                        <span class="label-title">Date:
                        </span>
                        <asp:TextBox ID="rdpformentrydate" runat="server" CssClass="txtBoxEditProfile" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <Ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="Server" TargetControlID="pnlgeneral1"
            ExpandControlID="pnlgeneral" CollapseControlID="pnlgeneral" Collapsed="false"
            BehaviorID="cpeForGeneral" ImageControlID="Image1" ExpandedText="(Hide Details...)"
            CollapsedText="(Show Details...)" ExpandedImage="~/Images/uparrow.jpg" CollapsedImage="~/Images/downarrow.jpg"
            SuppressPostBack="true" />
    </div>
    <div>
        <asp:Panel ID="pnlAbatment" runat="server" Visible="false">
            <span class="form-title">
                <span>ELIGIBLE FOR ABATEMENT</span>
                <span style="float: right;">
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/uparrow.jpg"
                        AlternateText="(Show Details...)" CausesValidation="false" />
                </span>
            </span>
        </asp:Panel>
        <asp:Panel ID="pnlAbatementDetail" runat="server" CssClass="panel" Visible="false">
            <div class="cai-form-content">
                <div class="field-group">
                    <asp:Label ID="lblAbatement" runat="server" Text=""></asp:Label>
                    <asp:LinkButton ID="btnAbatementPage" runat="server" CausesValidation="false">Click</asp:LinkButton>
                </div>
            </div>

        </asp:Panel>
        <Ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="Server" TargetControlID="pnlAbatementDetail"
            ExpandControlID="pnlAbatment" CollapseControlID="pnlAbatment" Collapsed="True"
            BehaviorID="cpeForAbatment" ImageControlID="Image1" ExpandedText="(Hide Details...)"
            CollapsedText="(Show Details...)" ExpandedImage="~/Images/uparrow.jpg" CollapsedImage="~/Images/downarrow.jpg"
            SuppressPostBack="true" />
    </div>
    <div id="Trainning" runat="server">
        <asp:Panel ID="pnlconfidential" runat="server">
            <span class="form-title">
                <asp:Label ID="lblTrainingAndCAIDiary" runat="server" Text="TRAINING AND CAI DIARY DETAILS"></asp:Label>
                <span style="float: right;">
                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/downarrow.jpg"
                        AlternateText="(Show Details...)" CausesValidation="false" />
                </span>
            </span>
        </asp:Panel>
        <asp:Panel ID="pnlconfidential1" runat="server" CssClass="panel">
            <div class="cai-form-content">

                <div class="field-group">
                    <div id="idTraining" runat="server">
                        <span class="label-title">Training</span>
                    </div>
                    <div id="idTrainingCompleted" runat="server">
                        <span>I completed my training contract on
                        </span>
                    </div>
                </div>

                <div id="idTrainingExpiryDate" runat="server" class="field-group">
                    <span class="label-title">Expiry Date:</span>
                    <rad:RadDatePicker ID="RdpExpirydate" runat="server" Calendar-ShowOtherMonthsDays="false"
                        MinDate="01/01/1900" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                    </rad:RadDatePicker>
                </div>

                <div class="field-group">
                    <span>With:</span>
                </div>

                <div class="address-section-half">
                    <div class="field-group">
                        <div id="idTrainingFirmName" runat="server">
                            <span class="label-title">Firm Name:</span>
                            <asp:TextBox ID="txtCompany" runat="server" AutoPostBack="true"></asp:TextBox>
                            <Ajax:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" BehaviorID="autoComplete"
                                CompletionInterval="10" CompletionListElementID="divwidth" CompletionSetCount="12"
                                EnableCaching="true" MinimumPrefixLength="1" ServiceMethod="GetCompanyList" ServicePath="~/GetCompanyList__c.asmx"
                                TargetControlID="txtCompany">
                            </Ajax:AutoCompleteExtender>
                        </div>

                        <div id="idTrainingAddressLine1" runat="server">
                            <span class="label-title">Address Line1:</span>
                            <asp:TextBox ID="txtTrainingFirmLine1" runat="server"></asp:TextBox>
                        </div>

                        <div id="idTrainingAddressLine2" runat="server">
                            <span class="label-title">Address Line2:</span>
                            <asp:TextBox ID="txtTrainingFirmLine2" runat="server"></asp:TextBox>
                        </div>

                        <div id="idTrainingAddressLine3" runat="server">
                            <span class="label-title">Address Line3:</span>
                            <asp:TextBox ID="txtTrainingFirmLine3" runat="server"></asp:TextBox>
                        </div>

                        <div id="idTrainingTelephoneNo" runat="server">
                            <span class="label-title">Telephone no:</span>
                            <asp:TextBox ID="txtTrainingFirmTelephone" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="address-section-half">
                    <div class="field-group">
                        <div id="idTrainingCountry" runat="server">
                            <span class="label-title">Country:
                            </span>
                            <asp:DropDownList ID="cmbTrainingFirmCountry" runat="server"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            <span class="label-title">
                                <asp:Label ID="Label2" runat="server"> ZIP Code:</asp:Label>
                            </span>
                            <asp:TextBox ID="txtTrainingFirmZipCode" CssClass="txtUserProfileZipCode" runat="server"></asp:TextBox>
                        </div>

                        <div id="idTrainingCityState" runat="server">
                            <span class="label-title">
                                <asp:Label ID="Label14" runat="server"> City:</asp:Label>
                            </span>
                            <asp:TextBox ID="txtTrainingFirmCity" runat="server"></asp:TextBox>

                            <span class="label-title">
                                <asp:Label ID="Label4" runat="server"> State:</asp:Label>
                            </span>
                            <asp:DropDownList ID="cmbTrainingFirmState" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="field-group">
                    <span class="label-title">CAI DIARY</span>
                    <asp:CheckBox ID="chkFinalTrainingReviewEnclosed" runat="server" Text="" />
                    <div id="idChkExpSummary" runat="server">
                        <asp:CheckBox ID="chkExpSummaryFormEnclosed" runat="server" Text="" />
                    </div>
                </div>
                 <%-- ''Added By Pradip 2016-07-11--%>
                <div class="field-group">
                    <asp:LinkButton ID="lnkPD2" CausesValidation="false" runat="server">Admission to Membership</asp:LinkButton>
                </div>
            </div>
        </asp:Panel>
        <Ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="Server" TargetControlID="pnlconfidential1"
            ExpandControlID="pnlconfidential" CollapseControlID="pnlconfidential" Collapsed="True"
            BehaviorID="cpeForConfidential" ImageControlID="Image1" ExpandedText="(Hide Details...)" CollapsedText="(Show Details...)"
            ExpandedImage="~/Images/uparrow.jpg" CollapsedImage="~/Images/downarrow.jpg"
            SuppressPostBack="true" />
    </div>
    <div id="idREQ" runat="server" visible="false">
        <asp:Panel ID="pnlREQ" runat="server">
            <span class="form-title">
                <span>RECOGNISED EXPERIENCE FOR QUALIFICATION (REQ)</span>
                <span style="float: right;">
                    <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/Images/downarrow.jpg"
                        AlternateText="(Show Details...)" />
                </span>
            </span>
        </asp:Panel>
        <asp:Panel ID="pnlREQDetails" runat="server">
            <div class="field-group">
                <span>I Confirm that I have completed the requisite period of experience for admission to membership(REQ)</span>

                <asp:DataList ID="dlREQ" runat="server" Height="24px">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkREQ" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Name")%>' />
                        <asp:Label ID="lblREQTopicCodeID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ID")%>'
                            Visible="False"></asp:Label>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </asp:Panel>
        <Ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender10" runat="Server" TargetControlID="pnlREQDetails"
            ExpandControlID="pnlREQ" CollapseControlID="pnlREQ" Collapsed="True" ImageControlID="Image1"
            ExpandedText="(Hide Details...)" CollapsedText="(Show Details...)" ExpandedImage="~/Images/uparrow.jpg"
            CollapsedImage="~/Images/downarrow.jpg" SuppressPostBack="true" />
    </div>
    <div id="idExamDetails" runat="server">
        <asp:Panel ID="pnlrecordadmission" runat="server">
            <span class="form-title">
                <span>EXAM DETAILS
                </span>
                <span style="float: right;">
                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/downarrow.jpg"
                        AlternateText="(Show Details...)" />
                </span>
            </span>
        </asp:Panel>
        <asp:Panel ID="pnlrecordadmission1" runat="server">
            <div class="cai-form-content">
                <div class="field-group">
                    <span class="label-title">I.T. PROGRAMME</span>

                    <asp:DataList ID="dlITProgramme" runat="server" Height="24px">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkITProgramme" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Name")%>' />
                            <asp:Label ID="lblITProgrammeTopicCodeID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ID")%>'
                                Visible="False"></asp:Label>
                        </ItemTemplate>
                    </asp:DataList>

                    <span class="label-title">COMPANY LAW MODULE</span>
                    <asp:DataList ID="dlCompanyLawModule" runat="server" Height="24px">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkCompanyLawModule" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Name")%>' />
                            <asp:Label ID="lblCompanyLawModuleTopicCodeID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ID")%>'
                                Visible="False"></asp:Label>
                        </ItemTemplate>
                    </asp:DataList>

                    <span>(tick appropriate box)
                    </span>
                </div>
            </div>
        </asp:Panel>
        <Ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender4" runat="Server" TargetControlID="pnlrecordadmission1"
            ExpandControlID="pnlrecordadmission" CollapseControlID="pnlrecordadmission" Collapsed="True"
            ImageControlID="Image1" ExpandedText="(Hide Details...)" CollapsedText="(Show Details...)"
            ExpandedImage="~/Images/uparrow.jpg" CollapsedImage="~/Images/downarrow.jpg"
            SuppressPostBack="true" />
    </div>
    <div id="idCertificateDetails" runat="server">
        <asp:Panel ID="pnlrequirement" runat="server">
            <span class="form-title">
                <span>CERTIFICATE DETAILS
                </span>
                <span style="float: right;">
                    <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Images/downarrow.jpg"
                        AlternateText="(Show Details...)" />
                </span>
            </span>
        </asp:Panel>
        <asp:Panel ID="pnlrequirement1" runat="server">
            <div class="cai-form-content">
                <div id="trNameOfCertificate" runat="server" class="field-group">
                    <span class="label-title">Name For Certificate:</span>
                    <asp:TextBox ID="txtCertifiedBy" runat="server"></asp:TextBox>
                </div>


                <div id="trNameOfCertificateSignedText" runat="server" class="field-group">
                    <span class="label-title">CERTIFICATE TO BE COMPLETED AND SIGNED OFF BY CHARTERED ACCOUNTANTS IRELAND MEMBER IN RECOGNISED TRAINING FIRM</span>
                    <div id="trNameOfCertificateMemberInst" runat="server">
                        <asp:TextBox ID="TextBox20" runat="server"></asp:TextBox>
                        being a Member of Institute, certify that
                    </div>
                </div>

                <div id="trNameOfCertificateStudent" runat="server" class="field-group">
                    <span class="label-title">Student
                    </span>

                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    Completed his/her service satisfactorily under training Contract with me
                </div>

                <div id="trNameOfCertificateFromToDate" runat="server" class="field-group thirds">
                    <div class="third">
                        <span class="label-title">From</span>

                        <rad:RadDatePicker ID="rdStartdate" runat="server" Calendar-ShowOtherMonthsDays="false"
                            MinDate="01/01/1900" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                        </rad:RadDatePicker>
                    </div>

                    <div class="third">
                        <span class="label-title">To</span>
                        <rad:RadDatePicker ID="rdEndDate" runat="server" Calendar-ShowOtherMonthsDays="false"
                            MinDate="01/01/1900" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                        </rad:RadDatePicker>
                    </div>

                    <div class="third">
                        <span class="label-title">For Period of (Years)</span>
                        <asp:TextBox ID="txtPeriod" runat="server"></asp:TextBox>
                    </div>

                </div>
                <div style="display: none" class="field-group">
                    <asp:CompareValidator runat="server" ID="cmpCalenders" ControlToValidate="rdEndDate"
                        ForeColor="Red" ControlToCompare="rdStartdate" Operator="GreaterThan" Type="Date"
                        ErrorMessage="" SetFocusOnError="True" />
                </div>

                <div id="trNameOfCertificateAdmittedText" runat="server" class="field-group">
                    <asp:Label ID="lblAdmited" runat="server" Text=""></asp:Label>
                </div>

                <div class="address-section-half">
                    <div class="field-group" id="trNameOfCertificatesignCA" runat="server">
                        <span class="label-title">SIGNATURE OF CHARTERED ACCOUNTANTS IRELAND MEMBER:
                        </span>
                        <asp:TextBox ID="TextBox27" runat="server"></asp:TextBox>

                    </div>
                </div>

                <div class="address-section-half">
                    <div class="field-group" id="trNameOfCertificatesignStuDate" runat="server">
                        <span class="label-title">Date:
                        </span>
                        <rad:RadDatePicker ID="RadDatePicker3" runat="server" Calendar-ShowOtherMonthsDays="false"
                            MinDate="01/01/1900" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                        </rad:RadDatePicker>
                    </div>
                </div>

                <div class="field-group" id="trTrainingInBusiness" runat="server">
                    <span class="label-title">TRAINING IN BUSINESS</span>
                </div>
                <div class="field-group" id="trTrainingInBusinessText" runat="server">
                    <asp:Label ID="lblTrainingInBusiness" runat="server" Text=""></asp:Label>
                </div>

                <div class="address-section-half">
                    <div class="field-group" id="trTrainingInBusinessStudSign" runat="server">
                        <span class="label-title">SIGNATURE OF STUDENT:
                        </span>
                        <asp:TextBox ID="TextBox29" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="address-section-half">
                    <div class="field-group" id="trTrainingInBusinessDate" runat="server">
                        <span class="label-title">Date:</span>
                        <rad:RadDatePicker ID="RadDatePicker4" runat="server" Calendar-ShowOtherMonthsDays="false"
                            MinDate="01/01/1900" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                        </rad:RadDatePicker>
                    </div>
                </div>

                <div class="field-group" id="trTrainingInBusinessBottomLine" runat="server">
                    <asp:Label ID="lblinBusinessTextLine" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </asp:Panel>
        <Ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender5" runat="Server" TargetControlID="pnlrequirement1"
            ExpandControlID="pnlrequirement" CollapseControlID="pnlrequirement" Collapsed="True"
            ImageControlID="Image1" ExpandedText="(Hide Details...)" CollapsedText="(Show Details...)"
            ExpandedImage="~/Images/uparrow.jpg" CollapsedImage="~/Images/downarrow.jpg"
            SuppressPostBack="true" />
    </div>
    <div>
        <asp:Panel ID="Panel1" runat="server">
            <span class="form-title">
                <span>EMPLOYMENT DETAILS
                </span>
                <span style="float: right;">
                    <asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="~/Images/uparrow.jpg"
                        AlternateText="(Show Details...)" CausesValidation="false" />
                </span>
            </span>
        </asp:Panel>
        <asp:Panel ID="Panel2" runat="server" CssClass="panel">
            <div class="cai-form-content">
                <div class="field-group">
                    <span class="label-title">CURRENT EMPLOYMENT </span>
                    <asp:LinkButton ID="lnkChangeEmploymentDetails" CausesValidation="false" runat="server">Change Employment Details</asp:LinkButton>
                </div>

                <div class="address-section-half">
                    <div class="field-group">
                        <span class="label-title">Firm Name: </span>
                        <asp:TextBox ID="txtFirmName" runat="server" AutoPostBack="true" Enabled="false"></asp:TextBox>
                        <asp:HiddenField ID="hidCompanyID" runat="server" />
                         <asp:HiddenField ID="hidTrainingFirmID" Value ="0" runat="server" />
                        <span class="label-title">Firm Address Line1:
                        </span>
                        <asp:TextBox ID="txtFirmLine1" runat="server" Enabled="false"></asp:TextBox>

                        <span class="label-title">Address Line2
                        </span>
                        <asp:TextBox ID="txtFirmLine2" runat="server" Enabled="false"></asp:TextBox>

                        <span class="label-title">Address Line3
                        </span>
                        <asp:TextBox ID="txtFirmLine3" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>

                <div class="address-section-half">
                    <div class="field-group">
                        <span class="label-title">Country:
                        </span>
                        <asp:DropDownList ID="cmbFirmCountry" CssClass="cmbUserProfileCountry" runat="server"
                            AutoPostBack="true" Enabled="false">
                        </asp:DropDownList>
                        <span class="label-title">
                            <asp:Label ID="Label6" runat="server"> ZIP Code:</asp:Label>
                        </span>
                        <asp:TextBox ID="txtFirmZipCode" CssClass="txtUserProfileZipCode" runat="server"
                            Enabled="false"></asp:TextBox>


                        <span class="label-title">
                            <asp:Label ID="Label7" runat="server"> City:</asp:Label>
                        </span>
                        <asp:TextBox ID="txtFirmCity" CssClass="txtUserProfileCity" runat="server" Enabled="false"></asp:TextBox>
                        <span class="label-title">
                            <asp:Label ID="Label8" runat="server"> State:</asp:Label>
                        </span>
                        <asp:DropDownList ID="cmbFirmState" CssClass="cmbUserProfileState" runat="server"
                            Enabled="false">
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="address-section-half">
                    <div class="field-group">
                        <span class="label-title">Telephone no:
                        </span>
                        <asp:TextBox ID="txtFirmTelephoneNo" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>

                <div class="address-section-half">
                    <div class="field-group">
                        <span class="label-title">Fax No:
                        </span>
                        <asp:TextBox ID="txtFirmFaxNo" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>

                <div class="address-section-half">
                    <div class="field-group">
                        <span class="label-title">Job Title:
                        </span>
                        <asp:TextBox ID="txtFirmJobTitle" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="address-section-half">
                    <div class="field-group">
                        <div class="field-group" id="tr2" runat="server">
                            <span class="label-title">Job Start Date:
                            </span>
                            <rad:RadDatePicker ID="rdpJobStartDate" runat="server" Calendar-ShowOtherMonthsDays="false"
                                MinDate="01/01/1900" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                            </rad:RadDatePicker>
                        </div>
                    </div>
                </div>



                <div class="field-group">
                    <asp:CheckBox ID="chkIsBusiness" runat="server" Text="In Business" />
                    <asp:CheckBox ID="chkIsPracticeOffice" runat="server" Text="Practicising Office" />
                </div>
                <div class="field-group">
                    <span class="label-title">Business Category:
                    </span>

                    <asp:DropDownList ID="cmbFirmBusinessCategory" runat="server">
                        <asp:ListItem>Please Select Business Category</asp:ListItem>
                        <asp:ListItem>Banking</asp:ListItem>
                        <asp:ListItem>Manufacturing</asp:ListItem>
                    </asp:DropDownList>

                </div>
            </div>
        </asp:Panel>
        <Ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender8" runat="Server" TargetControlID="Panel2"
            ExpandControlID="Panel1" CollapseControlID="Panel1" Collapsed="True" ImageControlID="Image1"
            ExpandedText="(Hide Details...)" CollapsedText="(Show Details...)" ExpandedImage="~/Images/uparrow.jpg"
            CollapsedImage="~/Images/downarrow.jpg" SuppressPostBack="true" />
    </div>
    <div id="idFAE" runat="server">
        <asp:Panel ID="pnlFinalAdmittingExam" runat="server">
            <span class="form-title">
                <span>FINAL ADMITTING EXAMINATIONS
                </span>
                <span style="float: right;">
                    <asp:ImageButton ID="ImageButton9" runat="server" ImageUrl="~/Images/downarrow.jpg"
                        AlternateText="(Show Details...)" />
                </span>
            </span>
        </asp:Panel>
        <asp:Panel ID="pnlFinalAdmittingExamDetails" runat="server">
            <div class="field-group cai-form-content">
                <span>I passed my FAE on
                    <rad:RadDatePicker ID="txtFAEDate" runat="server" Calendar-ShowOtherMonthsDays="false"
                        MinDate="01/01/1900" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                    </rad:RadDatePicker>
                    (Do not Submit results)
                </span>
            </div>
        </asp:Panel>
        <Ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender9" runat="Server" TargetControlID="pnlFinalAdmittingExamDetails"
            ExpandControlID="pnlFinalAdmittingExam" CollapseControlID="pnlFinalAdmittingExam"
            Collapsed="True" ImageControlID="Image1" ExpandedText="(Hide Details...)" CollapsedText="(Show Details...)"
            ExpandedImage="~/Images/uparrow.jpg" CollapsedImage="~/Images/downarrow.jpg"
            SuppressPostBack="true" />
    </div>
    <div id="idEvevationProg" runat="server" visible="false">
        <asp:Panel ID="pnlEvevationProg" runat="server">
            <span class="form-title">ELEVATION PROGRAMME
                <span style="float: right;">
                    <asp:ImageButton ID="ImageButton11" runat="server" ImageUrl="~/Images/downarrow.jpg"
                        AlternateText="(Show Details...)" />
                </span>
            </span>
        </asp:Panel>
        <asp:Panel ID="pnlEvevationProgDetails" runat="server">
            <div class="field-group">
                <asp:Label ID="lblEvevationProgramme" runat="server" Text=""></asp:Label>
            </div>
            <div class="field-group">
                <span class="label-title">SIGNATURE OF STUDENT:
                </span>
                <asp:TextBox ID="TextBox2" runat="server" Width="199px"></asp:TextBox>
            </div>
            <div class="field-group">
                <span class="label-title">Date:</span>
                <rad:RadDatePicker ID="txtElevationDate" runat="server" Calendar-ShowOtherMonthsDays="false"
                    MinDate="01/01/1900" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                </rad:RadDatePicker>
            </div>
            <div class="field-group">
                <asp:Label ID="lblEvlevationText" runat="server" Text=""></asp:Label>
            </div>
        </asp:Panel>
        <Ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender11" runat="Server" TargetControlID="pnlEvevationProgDetails"
            ExpandControlID="pnlEvevationProg" CollapseControlID="pnlEvevationProg" Collapsed="True"
            ImageControlID="Image1" ExpandedText="(Hide Details...)" CollapsedText="(Show Details...)"
            ExpandedImage="~/Images/uparrow.jpg" CollapsedImage="~/Images/downarrow.jpg"
            SuppressPostBack="true" />
    </div>
    <div id="idCurrentMembership" runat="server" visible="false">
        <asp:Panel ID="CurrentMembership" runat="server">
            <span class="form-title">
                <span>CURRENT MEMBERSHIP</span>
                <span style="float: right;">
                    <asp:ImageButton ID="ImageButton12" runat="server" ImageUrl="~/Images/downarrow.jpg"
                        AlternateText="(Show Details...)" />
                </span>
            </span>
        </asp:Panel>
        <asp:Panel ID="CurrentMembershipDetails" runat="server">
            <div class="field-group">
                <asp:Label ID="lblCurrentMemUpperText" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblCurrentMembershipResiB" runat="server" Text="" Visible="false"></asp:Label>
                <span class="label-title">Date Of Admission:</span>
                <rad:RadDatePicker ID="txtDateOfAdmission" runat="server" Calendar-ShowOtherMonthsDays="false"
                    MinDate="01/01/1900" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                </rad:RadDatePicker>
            </div>

            <div class="field-group">
                <span class="label-title">Membership Number:
                </span>
                <asp:TextBox ID="txtMembershipNumber" runat="server" Width="199px"></asp:TextBox>
            </div>

            <div class="field-group">
                <asp:Label ID="lblCuuentMembershipLowerText" runat="server" Text=""></asp:Label>
            </div>

            <div class="field-group">
                <span class="label-title">FUTURE CORRESPONDENCE</span>
                <span>Please send all correspondence to my </span>
                <asp:CheckBox ID="chkHome" runat="server" Text="Home" />
                <asp:CheckBox ID="chkOffice" runat="server" Text="Office" />
            </div>

            <div class="field-group">
                <span class="label-title">REMITTANCE</span>
                <asp:Label ID="lblRemittanceUpperText" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblRemittanceLowerText" runat="server" Text="" />
                <asp:Label ID="lblPrefeeredCurrency" runat="server" Text=""></asp:Label>
                <asp:TextBox ID="txtRemittanceAmount" runat="server" Width="199px" Enabled="false"></asp:TextBox>
            </div>

            <div class="field-group">
                <span class="label-title">SIGNATURE OF APPLICANT:
                </span>
                <asp:TextBox ID="TextBox4" runat="server" Width="199px"></asp:TextBox>
            </div>

            <div class="field-group">
                <span class="label-title">Date:</span>
                <rad:RadDatePicker ID="txtRemitatanceDate" runat="server" Calendar-ShowOtherMonthsDays="false"
                    MinDate="01/01/1900" MaxDate="01/01/9999" Calendar-ShowRowHeaders="false">
                </rad:RadDatePicker>
            </div>

        </asp:Panel>
        <Ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender12" runat="Server" TargetControlID="CurrentMembershipDetails"
            ExpandControlID="CurrentMembership" CollapseControlID="CurrentMembership" Collapsed="True"
            ImageControlID="Image1" ExpandedText="(Hide Details...)" CollapsedText="(Show Details...)"
            ExpandedImage="~/Images/uparrow.jpg" CollapsedImage="~/Images/downarrow.jpg"
            SuppressPostBack="true" />
    </div>
    <div>
        <asp:Panel ID="pnleducation" runat="server">
            <span class="form-title">
                <span>CHECK LIST DETAILS
                </span>
                <span style="float: right;">
                    <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Images/downarrow.jpg"
                        AlternateText="(Show Details...)" />
                </span>
            </span>
        </asp:Panel>
        <asp:Panel ID="pnleducation1" runat="server">
            <div class="cai-form-content">
                <div class="field-group">
                    <span class="label-title">
                        <asp:Label ID="lblEducation" runat="server" Text=""></asp:Label>
                    </span>

                    <div class="field-group">
                        <asp:CheckBox ID="chkStudentSignatureForOfficerGroup" runat="server" Text="Student signature for Officer Group" />
                    </div>

                    <div class="field-group">
                        <asp:CheckBox ID="chkFinalReview" runat="server" Text="Final Review CA/Summary Form of Record of Experience" />
                    </div>

                    <div class="field-group">
                        <asp:CheckBox ID="chkChangeName" runat="server" Text="change of name" />
                    </div>

                    <div class="field-group">
                        <asp:CheckBox ID="chkCurrentEmployer" runat="server" Text="Current Employer" />
                    </div>

                    <div class="field-group">
                        <asp:CheckBox ID="chkSignatureOfIrelandMember" runat="server" Text="Signature of chartered accountants Ireland Member" />
                        <span>(Recognised Training Firm)</span>
                    </div>

                    <div class="field-group">
                        <asp:CheckBox ID="chkSignatureOfStudent" runat="server" Text="TIB:Signature of student" />
                        <span>(if applicable)</span>
                    </div>

                    <div class="field-group">
                        <asp:CheckBox ID="chkElevationProgramme" runat="server" Text="Elevation Programme" />
                        <span>(if applicable)</span>
                    </div>

                    <div class="field-group">
                        <asp:CheckBox ID="chkApplicantSignatureForCouncil" runat="server" Text="Applicant Signature For Council" />
                    </div>

                    <div class="field-group">
                        <asp:CheckBox ID="chkFutureCorrespondence" runat="server" Text="Future Correspondence" />
                    </div>

                    <div class="field-group">
                        <asp:CheckBox ID="chkCertificateName" runat="server" Text="Certificate Name" />
                    </div>

                    <div class="field-group">
                        <asp:CheckBox ID="chkRemittance" runat="server" Text="Remittance" />
                    </div>

                    <div class="field-group">
                        <asp:CheckBox ID="chkCurrentMembership" runat="server" Text="Current Membership" />
                        <span>(attach letter from your institute)</span>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <Ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender7" runat="Server" TargetControlID="pnleducation1"
            ExpandControlID="pnleducation" CollapseControlID="pnleducation" Collapsed="True"
            ImageControlID="Image1" ExpandedText="(Hide Details...)" CollapsedText="(Show Details...)"
            ExpandedImage="~/Images/uparrow.jpg" CollapsedImage="~/Images/downarrow.jpg"
            SuppressPostBack="true" />
    </div>
    <div id="divStatusPnl" runat="server" visible="false">
        <asp:Panel ID="pnlStatusReson" runat="server">
            <table style="width: 100%">
                <tr>
                    <td class="tdPersonalInfo" style="width: 100%">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 5%"></td>
                                <td>MEMBERSHIP APPLICATION STATUS REASON
                                </td>
                                <td>
                                    <div style="float: right;">
                                        <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Images/downarrow.jpg"
                                            AlternateText="(Show Details...)" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlStatusResonDetails" runat="server">
            <table style="width: 100%">
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="grdMembershipStatusReson" runat="server" AutoGenerateColumns="False"
                            Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Status Reason">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatusResonID" runat="server" Text='<%# Eval("ID")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblStatusReason" runat="server" Text='<%# Eval("StatusReason")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Membership Application Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMembershipApplicationStatusID" runat="server" Text='<%# Eval("MembershipApplicationStatusID")%>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblMembershipApplicationStatus" runat="server" Text='<%# Eval("MembershipApplicationStatus")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Comments">
                                    <ItemTemplate>
                                        <asp:Label ID="lblComments" runat="server" Text='<%# Eval("Comments")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status Response">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatusResponse" runat="server" Text='<%# Eval("StatusResponse")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td valign="middle">Response:
                    </td>
                    <td valign="top" align="left">
                        <asp:TextBox ID="txtStatusResonse" runat="server" TextMode="MultiLine" Width="500px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td valign="top" align="left">
                        <asp:Button ID="btnPost" runat="server" Text="Post" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <Ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender6" runat="Server" TargetControlID="pnlStatusResonDetails"
            ExpandControlID="pnlStatusReson" CollapseControlID="pnlStatusReson" Collapsed="True"
            ImageControlID="Image1" ExpandedText="(Hide Details...)" CollapsedText="(Show Details...)"
            ExpandedImage="~/Images/uparrow.jpg" CollapsedImage="~/Images/downarrow.jpg"
            SuppressPostBack="true" />
    </div>
</div>

<div class="actions">
    <asp:Button ID="cmdSave" runat="server" Text="" CssClass="btn"></asp:Button>
    <asp:Button ID="btnSubmit" runat="server" Text="" CssClass="btn" />
    <asp:Button ID="btnSubmitAndPay" runat="server" Text="" CssClass="btn" />
</div>

<div class="data-form">
    <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="False" />
    <cc3:User ID="AptifyEbusinessUser1" runat="server" />
    <cc1:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="False" />
</div>

<rad:RadWindow ID="radMockTrial" runat="server" Width="400px" Height="20px" Modal="True"
    VisibleStatusbar="False" Behaviors="None" Title="Membership Application" Behavior="None">
    <ContentTemplate>
        <div class="modal-content">
            <asp:Label ID="lblWarning" runat="server"></asp:Label>

            <div class="actions">
                <asp:Button ID="btnYes" runat="server" Text="Yes" class="submitBtn" />
                <asp:Button ID="btnNo" runat="server" Text="No" class="submitBtn" />
            </div>
        </div>
    </ContentTemplate>
</rad:RadWindow>
<rad:RadWindow ID="radSubmitPay" runat="server" Width="350px" Height="120px" Modal="True"
    BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
    Title="Membership Application" Behavior="None">
    <ContentTemplate>
        <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1; height: 100%; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
            <tr>
                <td align="center">
                    <asp:Label ID="lblSubmitPayMsg" runat="server" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="btnPayOk" runat="server" Text="Yes" Width="70px" class="submitBtn" />
                    <asp:Button ID="btnPayNo" runat="server" Text="No" Width="70px" class="submitBtn" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
</rad:RadWindow>
