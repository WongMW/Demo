<%@ Control Language="VB" AutoEventWireup="false" CodeFile="LLLEnrollmentApplication__c.ascx.vb"
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
    </script>
<div class="dvUpdateProgress" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing" style="height: 1300px;">
                <table class="tblFullHeightWidth">
                    <tr>
                        <td class="tdProcessing" style="vertical-align: middle">
                            Please wait...
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<div class="info-data">

         <asp:HiddenField ID="hdnCompanyId" runat="server" Value="" />
    <asp:HiddenField ID="hdnAwardingBody" runat="server" Value="0" />
            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
            <div class="row-div clearfix">
                <div class="label-div w30">
                    &nbsp;
                </div>
                <div class="field-div1 w100">
                    <div class="info-data" style="margin: 0 5% 0 6% !important;">
                        <div class="row-div clearfix" id="divApplicantName" runat="server">
                            <div style="text-align: left;" class="field-div1 w12">
                                <b>Applicant Name:</b>
                            </div>
                            <div class="field-div1 w60" style="text-align: left;">
                                <asp:TextBox ID="txtApplicantName" Width="350px" MaxLength="50" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row-div clearfix" id="divFirstName" runat="server" visible="false">
                            <div style="text-align: left;" class="field-div1 w12">
                                <b>First Name:</b>
                            </div>
                            <div class="field-div1 w17" style="text-align: left;">
                                <asp:TextBox ID="txtFirstName" Width="150px" MaxLength="50" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                            <div style="text-align: left;" class="field-div1 w10">
                                <b>Middle Name:</b>
                            </div>
                            <div class="field-div1 w30" style="text-align: left;">
                                <asp:TextBox ID="txtMiddleName" Width="100px" MaxLength="50" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row-div clearfix" id="divLastName" runat="server" visible="false">
                            <div style="text-align: left;" class="field-div1 w12">
                                <b>Last Name:</b>
                            </div>
                            <div class="field-div1 w60" style="text-align: left;">
                                <asp:TextBox ID="txtLastName" Width="150px" MaxLength="50" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w12">
                                <b>Member Type:</b>
                            </div>
                            <div class="field-div1 w60" style="text-align: left;">
                                <asp:TextBox ID="txtMemberType" Width="350px" MaxLength="50" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w12">
                                <b>Firm Name:</b>
                            </div>
                            <div class="field-div1 w60" style="text-align: left;">
                                <asp:TextBox ID="txtFirmName" AutoPostBack="true" runat="server" Width="350px" Enabled="false" />
                                <Ajax:AutoCompleteExtender ID="autoCompanyName" runat="server" BehaviorID="autoComplete"
                                    CompletionInterval="10" CompletionSetCount="1" EnableCaching="true" MinimumPrefixLength="1"
                                    ServiceMethod="GetCompanyDetails" ServicePath="~/WebServices/GetCompanyDetails__c.asmx"
                                    TargetControlID="txtFirmName" UseContextKey="true"  OnClientItemSelected="ClientItemSelected">
                                </Ajax:AutoCompleteExtender>
                            </div>
                        </div>

                           <div class="row-div clearfix" id="divAccBodytxtID" runat="server" visible="false">
                            <div style="text-align: left;" class="field-div1 w90">
                                <b> 
                                    <asp:Label ID="lblAccounatcyBodytxt" runat="server" Text=" "></asp:Label></b>
                            </div>
                            <div class="field-div1 w10" style="text-align: left;">
                              
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w12">
                                <b>Accountancy Body:</b>
                            </div>
                            <div class="field-div1 w60" style="text-align: left;">
                                <asp:TextBox ID="txtAccountancyBody" runat="server" CssClass="textbox" AutoPostBack="true"
                                    Width="120px" Enabled="True" />
                                    
                                <Ajax:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" BehaviorID="autoE"
                                    CompletionInterval="10" CompletionSetCount="1" EnableCaching="true" MinimumPrefixLength="1"
                                    ServiceMethod="GetAccountancyBodies" ServicePath="~/WebServices/GetCompanyDetails__c.asmx"
                                    TargetControlID="txtAccountancyBody" UseContextKey="true"  OnClientItemSelected="ClientSelectedAwardingBody">
                                </Ajax:AutoCompleteExtender>
                                
                            </div>
                           
                        </div>

                        <div class="row-div clearfix" id="divUpload" runat="server" visible="false">
                            <div style="text-align: left;" class="field-div1 w12">
                                <b>Upload Document:</b>
                            </div>
                            <div class="field-div1 w60" style="text-align: left;">
                                <uc2:SMAARecordAttachments ID="raUploadDocs"   runat="server"></uc2:SMAARecordAttachments>
                                
                            </div>
                         
                        </div>
                        <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w12">
                                <b>Application Type:</b>
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
                                <b>Class:</b>
                            </div>
                            <div class="field-div1 w60" style="text-align: left;">
                                <asp:TextBox ID="txtClass" Width="350px" MaxLength="50" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div style="text-align: left;" class="field-div1 w12">
                                <span style="color: Red;">*</span><b>Who Pays:</b>
                            </div>
                            <div class="field-div1 w60" style="text-align: left;">
                                <asp:DropDownList ID="cmbWhoPays" runat="server" Width="350px" AutoPostBack="true">
                                     <asp:ListItem Text="Bill to Firm" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Pay now with Own Credit Card" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Pay now with Firm Credit Card" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row-div clearfix" id="divPO" runat="server">
                            <div style="text-align: left;" class="field-div1 w12">
                                <span style="color: Red;">*</span> <b>PO Number:</b>
                            </div>
                            <div class="field-div1 w60" style="text-align: left;">
                                <asp:TextBox ID="txtPONumber" Width="350px" MaxLength="50" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPONumber"
                                    ErrorMessage="PO Number Required" Display="Dynamic" CssClass="required-label" ValidationGroup="VGG"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div style="text-align: left;" class="row-div clearfix">
                            <div style="text-align: left; font-weight: normal;" class="label-div w12">
                                <b>Comments:</b>
                            </div>
                            <div class="field-div1 w60" style="text-align: left;">
                                <asp:TextBox ID="txtComments" Width="350px" MaxLength="50" runat="server" Height="150"
                                    TextMode="MultiLine" Style="resize: none; margin-left: -10px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="info-data">
                            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblPaymentSummery" runat="server" Text="Payment Summary" Visible="false"
                                Font-Bold="true"></asp:Label>
                            <div class="info-data" id="divPaySummary" runat="server" visible="false">
                                <div class="row-div clearfix">
                                    &nbsp;</div>
                                <div class="row-div clearfix">
                                    <div class="label-div-left-align w40">
                                        <div class="info-data">
                                            <div class="row-div clearfix">
                                                <div class="label-div w55">
                                                    <b>
                                                        <asp:Label ID="lblAmount" runat="server" Text="Total Amount:" Visible="false"></asp:Label></b>
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
                                                        <asp:Label ID="lblStudentPaidLabel" runat="server" Text="Amount To Be Paid By Student:"
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
                                                        <asp:Label ID="lblFirmPaidLabel" runat="server" Text="Amount To Be Paid By Firm:"
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
                                                        <asp:Label ID="lblTax" runat="server" Text="Tax Amount:" Visible="false"></asp:Label></b>
                                                </div>
                                                <div class="field-div1 w75">
                                                    <b>
                                                        <asp:Label ID="lblTaxAmount" runat="server" Text="" Visible="false"></asp:Label></b>
                                                </div>
                                            </div>
                                            <div class="row-div clearfix">
                                                <div class="label-div w55">
                                                    <asp:Label ID="lblIntialAmt" runat="server" Text="Intial Amount:"></asp:Label></b>
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

                             <asp:Button ID="btnChangeAddress" runat="server" Text="Change Address" CausesValidation="false" />
                            
                            <div class="info-data" id="divCreditCard" runat="server" visible="false">
                                <div class="row-div clearfix">
                                    <div class="field-div1 w200">
                                        <b>
                                            <uc1:CreditCard ID="CreditCard" runat="server" />
                                        </b>
                                    </div>
                                    <div class="label-div-left-align w50">
                                        <div class="info-data">
                                            <div class="row-div clearfix">
                                                <div class="label-div w50">
                                                    <asp:Label ID="lblPaymentPlan" runat="server" Text="Payment Plan:" Visible="false"></asp:Label></b>
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
                                                <div class="field-div1 w70">
                                                    <telerik:RadGrid ID="radPaymentPlanDetails" runat="server" AutoGenerateColumns="False"
                                                        AllowPaging="false" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                                        AllowFilteringByColumn="false" Visible="false" AllowSorting="false" Width="200px">
                                                        <GroupingSettings CaseSensitive="false" />
                                                        <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false">
                                                            <Columns>
                                                                <telerik:GridTemplateColumn HeaderText="Days" AllowFiltering="false" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbldays" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "days") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="Schedule Date" AllowFiltering="false">
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
                                 <span style="color: Red;">*</span> <asp:CheckBox ID="chkTermsAndConditions" runat="server" />
                            </div>
                        </div>
                        <div style="text-align: left;" class="row-div clearfix">
                            <div style="text-align: left; margin-left: 100px;" class="field-div1 w60">
                            <%--    <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="True">
        <ContentTemplate>--%>
                                <asp:Button ID="btnSave" runat="server" Text="Submit" Height="25px" ValidationGroup="VGG" />
                                <asp:Button ID="btnCheckOut" runat="server" Text="Submit" Height="25px" Visible="false" ValidationGroup="VGG" />
                                   <telerik:RadWindow ID="radWindow" runat="server" Width="350px" Modal="True" BackColor="#f4f3f1"
                VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797" Title="Life Long Learning
            Enrollment Application" Behavior="None" Height="150px">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                        padding-left: 5px; padding-right: 5px; padding-top: 5px;">
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
            Enrollment Application" Behavior="None" Height="150px">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                        padding-left: 5px; padding-right: 5px; padding-top: 5px;">
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
                                    <asp:Button ID="btnAcceptTerms" runat="server" Text="Ok" Width="70px" class="submitBtn" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </telerik:RadWindow>
                                 <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
         
            <cc3:User ID="User1" runat="server" />
            <cc2:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="False"></cc2:AptifyShoppingCart>
       
</div>
