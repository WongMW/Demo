<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/LLLEnrollmentApplication__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.LLLEnrollmentApplication__c" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="uc1" TagName="CreditCard" Src="~/UserControls/Aptify_Custom__c/CreditCard__c.ascx" %>
<%@ Register Src="~/UserControls/Aptify_Custom__c/BeforeSaveRecordAttachments__c.ascx" TagName="SMAARecordAttachments"
    TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<script type="text/javascript">
    function ClientItemSelected(sender, e) {
        document.getElementById("<%=hdnCompanyId.ClientID %>").value = e.get_value();
    }
    function ClientSelectedAwardingBody(sender, e) {
        document.getElementById("<%=hdnAwardingBody.ClientID %>").value = e.get_value();
    }
    function ValidateCheckBox(sender, args) {

        if (document.getElementById("<%=chkTermsAndConditions.ClientID %>").checked == true) {

            args.IsValid = true;

        } else {

            args.IsValid = false;
        }


    }
</script>
<div class="raDiv" style="overflow: visible;"><%-- this is the real file --%>
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing"><div class="loading-bg">
                <img src="/Images/CAITheme/bx_loader.gif" />
                <span>This process can take a few minutes</span></div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<asp:UpdatePanel ID="upnlApplication" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
<div class="info-data cai-form">
  <div class="form-title">Enrolment application form</div>
    <asp:HiddenField ID="hdnCompanyId" runat="server" Value="" />
    <asp:HiddenField ID="hdnAwardingBody" runat="server" Value="0" />
    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
    <div class="row-div clearfix">
        <div class="label-div w30">
            &nbsp;
        </div>
        <div class="field-div1 w100 data-form">
            <div class="info-data" style="margin: 0 5% 0 6% !important;">
              <div class="form-section-half-border">
                <div class="row-div clearfix" id="divApplicantName" runat="server">
                    <div style="text-align: left;" class="field-div1 w12">
                        <b>Applicant name:</b>
                    </div>
                    <div class="field-div1 w60" style="text-align: left;">
                        <asp:TextBox ID="txtApplicantName" Width="350px" MaxLength="50" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="row-div clearfix" id="divFirstName" runat="server" visible="false">
                    <div style="text-align: left;" class="field-div1 w12">
                        <b>First name:</b>
                    </div>
                    <div class="field-div1 w17" style="text-align: left;">
                        <asp:TextBox ID="txtFirstName" Width="350px" MaxLength="50" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                    <div style="text-align: left;" class="field-div1 w10">
                        <b>Middle name:</b>
                    </div>
                    <div class="field-div1 w30" style="text-align: left;">
                        <asp:TextBox ID="txtMiddleName" Width="350px" MaxLength="50" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="row-div clearfix" id="divLastName" runat="server" visible="false">
                    <div style="text-align: left;" class="field-div1 w12">
                        <b>Last name:</b>
                    </div>
                    <div class="field-div1 w60" style="text-align: left;">
                        <asp:TextBox ID="txtLastName" Width="350px" MaxLength="50" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="row-div clearfix">
                    <div style="text-align: left;" class="field-div1 w12">
                        <b>Member type:</b>
                    </div>
                    <div class="field-div1 w60" style="text-align: left;">
                        <asp:TextBox ID="txtMemberType" Width="350px" MaxLength="50" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>

                <div class="row-div clearfix" style="display:none;">
                    <div style="text-align: left;" class="field-div1 w12">
                        <b>Application type:</b>
                    </div>
                    <div class="field-div1 w60" style="text-align: left;">
                        <asp:TextBox ID="txtApplicationType" Width="350px" MaxLength="50" runat="server"
                            Enabled="false"></asp:TextBox>
                        <%-- <asp:DropDownList ID="cmbLLLCourseCategory" runat="server" AutoPostBack="true" Width="350px" />
                                <asp:RequiredFieldValidator InitialValue="Select Qualification" ID="Req_ID"
                                    Display="Dynamic" runat="server" ControlToValidate="cmbLLLCourseCategory"
                                    Text="" ErrorMessage="Qualification Required" CssClass="required-label"></asp:RequiredFieldValidator>--%>
                    </div>
                </div>

                <div class="row-div clearfix">
                    <div style="text-align: left;" class="field-div1 w12">
                        <b>Course:</b>
                    </div>
                    <div class="field-div1 w60" style="text-align: left;">
                        <asp:TextBox ID="txtClass" Width="350px" MaxLength="50" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>

                <div class="row-div clearfix">
                    <div style="text-align: left;" class="field-div1 w12">
                        <b>Firm name:</b>
                    </div>
                    <div class="field-div1 w60" style="text-align: left;">
                        <asp:TextBox ID="txtFirmName" AutoPostBack="true" runat="server" Width="350px" Enabled="false" />
                        <Ajax:AutoCompleteExtender ID="autoCompanyName" runat="server" BehaviorID="autoComplete"
                            CompletionInterval="10" CompletionSetCount="1" EnableCaching="true" MinimumPrefixLength="1"
                            ServiceMethod="GetCompanyDetails" ServicePath="~/WebServices/GetCompanyDetails__c.asmx"
                            TargetControlID="txtFirmName" UseContextKey="true" OnClientItemSelected="ClientItemSelected">
                        </Ajax:AutoCompleteExtender>
                    </div>
                </div>

                <div class="row-div clearfix" id="divAccountancyBody" runat="server">
                    <div style="text-align: left;" class="field-div1 w12">
                        <b>Accountancy body:</b>
                    </div>
                    <div class="field-div1 w60" style="text-align: left;">
                        <asp:TextBox ID="txtAccountancyBody" runat="server" CssClass="textbox" AutoPostBack="true"
                            Width="350px" Enabled="True" />

                        <Ajax:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" BehaviorID="autoE"
                            CompletionInterval="10" CompletionSetCount="1" EnableCaching="true" MinimumPrefixLength="1"
                            ServiceMethod="GetAccountancyBodies" ServicePath="~/WebServices/GetCompanyDetails__c.asmx"
                            TargetControlID="txtAccountancyBody" UseContextKey="true" OnClientItemSelected="ClientSelectedAwardingBody">
                        </Ajax:AutoCompleteExtender>

                    </div>

                </div>

              </div>
            <div class="form-section-half-border"> <%--section panel 2 --%>



                <div class="row-div clearfix" id="divAdd1" runat="server" visible="false">
                    <div style="text-align: left;" class="field-div1 w12">
                        <span style="color: Red;">*</span>   <b>Address 1:</b>
                    </div>
                    <div class="field-div1 w60" style="text-align: left;">
                        <asp:TextBox runat="server" ID="txtAddressLine1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please add address 1"
                            Display="Dynamic" CssClass="required-label" ControlToValidate="txtAddressLine1" ValidationGroup="VGG"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row-div clearfix" id="divAdd2" runat="server" visible="false">
                    <div style="text-align: left;" class="field-div1 w12">
                        <b>Address 2:</b>
                    </div>
                    <div class="field-div1 w60" style="text-align: left;">
                        <asp:TextBox runat="server" ID="txtAddressLine2"></asp:TextBox>
                    </div>
                </div>
                <div class="row-div clearfix" id="divAdd3" runat="server" visible="false">
                    <div style="text-align: left;" class="field-div1 w12">
                        <b>Address 3:</b>
                    </div>
                    <div class="field-div1 w60" style="text-align: left;">
                        <asp:TextBox runat="server" ID="txtAddressLine3"></asp:TextBox>
                    </div>
                </div>
                <div class="row-div clearfix" id="divCity" runat="server" visible="false">
                    <div style="text-align: left;" class="field-div1 w12">
                        <b>City, county, postal code:</b>
                    </div>
                    <div class="field-div1 w60" style="text-align: left;">
                        <asp:TextBox runat="server" ID="txtCity" Width="145px"></asp:TextBox>
                        <asp:DropDownList runat="server" ID="cmbState" DataTextField="State" DataValueField="State"
                            Width="50px">
                        </asp:DropDownList>
                        <asp:TextBox runat="server" ID="txtZipCode" Width="135px"></asp:TextBox>
                    </div>
                </div>
                <div class="row-div clearfix" id="divCountry" runat="server" visible="false">
                    <div style="text-align: left;" class="field-div1 w12">
                        <b>Country:</b>
                    </div>
                    <div class="field-div1 w60" style="text-align: left;">
                        <asp:DropDownList runat="server" ID="cmbCountry" DataTextField="Country" DataValueField="ID"
                            Width="340px" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ControlToValidate="cmbCountry" ID="RequiredFieldValidator2"
                                ErrorMessage="Please select a country"
                                InitialValue="0" runat="server" ForeColor="Red" Display="Dynamic" ValidationGroup="VGG">
                            </asp:RequiredFieldValidator>
                    </div>
                </div>



                <div class="row-div clearfix" id="divAccBodytxtID" runat="server" visible="false">
                    <div style="text-align: left; display:none;" class="field-div1 w90">
                        <b>
                            <asp:Label ID="lblAccounatcyBodytxt" runat="server" Text=" "></asp:Label></b>
                    </div>
                    <div class="field-div1 w10" style="text-align: left;">
                    </div>
                </div>

                <div class="row-div clearfix" id="divUpload" runat="server" visible="false">
                    <div style="text-align: left;" class="field-div1 w12">
                        <b>Upload document:</b>
                    </div>
                    <div class="field-div1 w60" style="text-align: left;">
                        <uc2:SMAARecordAttachments ID="raUploadDocs" runat="server"></uc2:SMAARecordAttachments>

                    </div>

                </div>
              <%--   <div class="row-div clearfix">
                    <div style="text-align: left;" class="field-div1 w12">
                        <b>Application Type:</b>
                    </div>
                    <div class="field-div1 w60" style="text-align: left;">
                        <asp:TextBox ID="x" Width="350px" MaxLength="50" runat="server"
                            Enabled="false"></asp:TextBox>
                        <%-- <asp:DropDownList ID="cmbLLLCourseCategory" runat="server" AutoPostBack="true" Width="350px" />
                                <asp:RequiredFieldValidator InitialValue="Select Qualification" ID="Req_ID"
                                    Display="Dynamic" runat="server" ControlToValidate="cmbLLLCourseCategory"
                                    Text="" ErrorMessage="Qualification Required" CssClass="required-label"></asp:RequiredFieldValidator>--%>
                  <%--  </div>
                </div>--%>
              <%--   <div class="row-div clearfix">
                    <div style="text-align: left;" class="field-div1 w12">
                        <b>Class:</b>
                    </div>
                    <div class="field-div1 w60" style="text-align: left;">
                        <asp:TextBox ID="" Width="350px" MaxLength="50" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>--%>
                <div class="row-div clearfix">
                    <div style="text-align: left;" class="field-div1 w12">
                        <span style="color: Red;">*</span><b>Who pays:</b>
                    </div>
                    <div class="field-div1 w60" style="text-align: left;">
                        <asp:DropDownList ID="cmbWhoPays" runat="server" Width="350px" AutoPostBack="true">
                           <asp:ListItem Text="Bill to firm" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Pay now with own credit card" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Pay now with firm credit card" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row-div clearfix" id="divPO" runat="server">
                    <div style="text-align: left;" class="field-div1 w12">
                        <span style="color: Red;">*</span> <b>PO number:</b>
                    </div>
                    <div class="field-div1 w60" style="text-align: left;">
                        <asp:TextBox ID="txtPONumber" Width="350px" MaxLength="50" runat="server"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPONumber"
                            ErrorMessage="PO Number Required" Display="Dynamic" CssClass="required-label" ValidationGroup="VGG"></asp:RequiredFieldValidator>--%>
                    </div>
                </div>
                 <div class="row-div clearfix" id="divCompanyAddressID" runat="server" visible="false">
                    <div style="text-align: left;" class="field-div1 w12">
                        <span style="color: Red;">*</span> <b>Company address</b>
                    </div>
                   <div class="field-div1 w60" style="text-align: left;">
                        <div id="dvadd1" runat="server">
                            <asp:Label ID="lblAddressLine1" runat="server"></asp:Label><br />
                        </div>
                        <div id="dvadd2" runat="server">
                            <asp:Label ID="lblAddressLine2" runat="server"></asp:Label>
                            <asp:Literal ID="brAddressLine2" runat="server" Text="<br>"></asp:Literal></div>
                        <div id="dvadd3" runat="server">
                            <asp:Label ID="lblAddressLine3" runat="server"></asp:Label><asp:Literal ID="brAddressLine3"
                                runat="server" Text="<br>"></asp:Literal>
                        </div>
                         <div id="dvCityStateZip" runat="server"><asp:Label ID="lblCity" runat="server"></asp:Label><asp:Label ID="lblCityComma" runat="server"
                            Text=",">&nbsp;</asp:Label><asp:Label ID="lblState" runat="server"></asp:Label>
                        <asp:Label ID="lblZipCode" runat="server"></asp:Label><br /></div> 
                       <div id="dvCountry" runat="server"> <asp:Label ID="lblCountry" runat="server"></asp:Label><br /></div> 
                        <asp:Button ID="Button1" runat="server" Text="Change address" CausesValidation="false" CssClass="submitBtn"/>
                    </div>
                </div>
                <div style="text-align: left;" class="row-div clearfix">
                    <div style="text-align: left; font-weight: normal;" class="label-div w12">
                        <b>Comments:</b>
                    </div>
                    <div class="field-div1 w60 field-group" style="text-align: left;">
                        <asp:TextBox ID="txtComments" Width="350px" MaxLength="50" runat="server" Height="150"
                            TextMode="MultiLine" Style="resize: none; margin-left: -15px"></asp:TextBox>
                    </div>
                </div>
            </div>
                <div class="info-data field-group">
                    <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                    <asp:Label ID="lblPaymentSummery" runat="server" Text="Payment summary" Visible="false"
                        Font-Bold="true"></asp:Label>
                    <div class="info-data" id="divPaySummary" runat="server" visible="false">
                        <div class="row-div clearfix">
                            &nbsp;
                        </div>
                        <div class="row-div clearfix">
                            <div class="label-div-left-align w40">
                                <div class="info-data">
                                    <div class="row-div clearfix">
                                        <div class="label-div w55">
                                            <b>
                                                <asp:Label ID="lblAmount" runat="server" Text="Total amount:" Visible="false"></asp:Label></b>
                                        </div>
                                        <div class="field-div1 w75">
                                            <b>
                                                <asp:Label ID="lblTotalAmount" runat="server" Text="" Visible="false"></asp:Label></b>
                                            <asp:Label ID="lblStagePaymentTotal" runat="server" Text="" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row-div clearfix" id="divStudentPaid" runat="server" visible="false">
                                        <div class="label-div w55">
                                            <b>
                                                <asp:Label ID="lblStudentPaidLabel" runat="server" Text="Amount to be paid by student:"
                                                    Visible="false"></asp:Label></b>
                                        </div>
                                        <div class="field-div1 w75">
                                            <b>
                                                <asp:Label ID="lblAmountPaidStudent" runat="server" Text="" Visible="false"></asp:Label></b>
                                        </div>
                                    </div>
                                    <div class="row-div clearfix" id="divFirmPay" runat="server" visible="false">
                                        <div class="label-div w55">
                                            <b>
                                                <asp:Label ID="lblFirmPaidLabel" runat="server" Text="Amount to be paid by firm:"
                                                    Visible="false"></asp:Label></b>
                                        </div>
                                        <div class="field-div1 w75">
                                            <b>
                                                <asp:Label ID="lblAmountPaidFirm" runat="server" Text="" Visible="false"></asp:Label></b>
                                        </div>
                                    </div>
                                    <div class="row-div clearfix">
                                        <div class="label-div w55">
                                            <b>
                                                <asp:Label ID="lblTax" runat="server" Text="Tax amount:" Visible="false"></asp:Label></b>
                                        </div>
                                        <div class="field-div1 w75">
                                            <b>
                                                <asp:Label ID="lblTaxAmount" runat="server" Text="" Visible="false"></asp:Label></b>
                                        </div>
                                    </div>
                                    <div class="row-div clearfix">
                                        <div class="label-div w55">
                                            <asp:Label ID="lblIntialAmt" runat="server" Text="Initial amount:"></asp:Label></b>
                                        </div>
                                        <div class="field-div1 w75">
                                            <asp:Label ID="lblCurrency" runat="server" Text="" Visible="false"></asp:Label>
                                            <asp:Label ID="lblIntialAmount" runat="server" Text=""></asp:Label>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                   <div class="row-div clearfix">
                    <asp:Button ID="btnChangeAddress" runat="server" Text="Change address" CausesValidation="false" CssClass="submitBtn" Visible="false"/>
                   </div>
                    <div class="info-data" id="divCreditCard" runat="server" visible="false">
                        <div class="row-div clearfix">
                            <div class="field-div1 w200">
                                <b>
                                     <asp:Label ID="lblCreditFailed" runat="server" ForeColor="Red" Visible="false" ></asp:Label><br />
                                    <uc1:CreditCard ID="CreditCard" runat="server" />
                                </b>
                            </div>
                            <div class="label-div-left-align w50">
                                <div class="info-data">
                                    <div class="row-div clearfix">
                                        <div class="label-div w50">
                                            <asp:Label ID="lblPaymentPlan" runat="server" Text="Payment plan:" Visible="false"></asp:Label></b>
                                        </div>
                                        <div class="field-div1 w40">
                                            <asp:DropDownList ID="ddlPaymentPlan" runat="server" AutoPostBack="true" Width="200px">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row-div clearfix">
                                        <div class="label-div w25">
                                            &nbsp;
                                        </div>
                                        <div class="field-div1 w70 cai-table">
                                            <telerik:RadGrid ID="radPaymentPlanDetails" runat="server" AutoGenerateColumns="False"
                                                AllowPaging="false" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                                AllowFilteringByColumn="false" Visible="false" AllowSorting="false" Width="300px">
                                                <GroupingSettings CaseSensitive="false" />
                                                <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false">
                                                    <Columns>
                                                        <telerik:GridTemplateColumn HeaderText="Days" AllowFiltering="false" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldays" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "days") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Schedule date" AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblScheduleDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ScheduleDate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Percentage" AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPercentage" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Percentage") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="Amount" AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmt" runat="server" Text='<%# SetStagePaymentAmount(Eval("Percentage"),(Eval("days"))) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row-div clearfix" id="divTermsCondition" runat="server" visible="false">
                    <div style="text-align: left;" class="field-div1 w12">
                        &nbsp;
                    </div>
                    <div class="field-div1 w60" style="text-align: left;">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="True">
        <ContentTemplate>
                        <span style="color: Red;">*</span>
                        <asp:CheckBox ID="chkTermsAndConditions" runat="server" />
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ForeColor="Red"  ErrorMessage="Terms and conditions required" Display="Dynamic" CssClass="required-label" ClientValidationFunction="ValidateCheckBox" ValidationGroup="VGG"></asp:CustomValidator><br />
                        &nbsp;<span>Please tick to confirm you have read our </span><asp:HyperLink NavigateUrl="/professional-development/specialist-qualifications/specialist-qualifications-terms-and-conditions" runat="server" ID="lblWebDesc" Text="terms and conditions" Target="_blank"/>  
            </ContentTemplate> 
            </asp:UpdatePanel> 
                    </div>
                </div>
                <div style="text-align: left;" class="row-div clearfix field-group">&nbsp;
                    <div class="field-div1 w60">
                        <%--    <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="True">
        <ContentTemplate>--%>
                        <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="VGG" CssClass="submitBtn"/>
                        <asp:Button ID="btnCheckOut" runat="server" Text="Submit" Visible="false" ValidationGroup="VGG" CssClass="submitBtn"/>
                        <telerik:RadWindow ID="radWindow" runat="server" Width="350px" Modal="True" BackColor="#f4f3f1"
                            VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797" Title="Life Long Learning
            enrolment application"
                            Behavior="None" Height="150px">
                            <ContentTemplate>
                                <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <div>
                                                <br />
                                            </div>
                                            <div>
                                                <asp:Button ID="btnOK" runat="server" Text="Ok" Width="70px" class="submitBtn" CausesValidation="false" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </telerik:RadWindow>
                        <telerik:RadWindow ID="radAcceptTerms" runat="server" Width="350px" Modal="True" BackColor="#f4f3f1"
                            VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797" Title="Life Long Learning
            enrolment application"
                            Behavior="None" Height="150px">
                            <ContentTemplate>
                                <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1; padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblAcceptTermsCondition" runat="server" Font-Bold="true" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <div>
                                                <br />
                                            </div>
                                            <div>
                                                <asp:Button ID="btnAcceptTerms" runat="server" Text="Ok" Width="70px" class="submitBtn" CausesValidation="false"/>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </telerik:RadWindow>
                        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    <%--Added by Prachi for Redmine #15496--%>
    <telerik:RadWindow ID="radCompanyDupeCheck" runat="server" Width="350px" Modal="True" BackColor="#f4f3f1"
                VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797" Title="Life Long Learning
            Enrollment Application" Behavior="None" Height="150px">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                        padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblCompanyDupeCheck" runat="server" Font-Bold="true" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <div>
                                    <br />
                                </div>
                                <div>
                                    <asp:Button ID="btnCopanyDupeCheck" runat="server" Text="Ok" Width="70px" class="submitBtn" OnClick="btnCopanyDupeCheck_Click" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </telerik:RadWindow>
            <%--End Redmine #15496--%>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <cc3:User ID="User1" runat="server" />
    <cc2:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="False"></cc2:AptifyShoppingCart>

</div>
        
        
    </ContentTemplate>
    <triggers>
              <asp:PostBackTrigger ControlID="raUploadDocs" />
             
        </triggers>
</asp:UpdatePanel>
