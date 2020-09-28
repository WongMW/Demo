<%@ Control Language="VB" Debug="true" AutoEventWireup="false" CodeFile="AbatmentsFormData__c.ascx.vb"
    Inherits="AbatmentsFormData__c" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="../Aptify_General/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc2" %>
<%@ Register Src="organisationAddress__c.ascx" TagName="organisationAddress__c" TagPrefix="uc1" %>
 <%--<%@ Register Src="~/UserControls/Aptify_Custom__c/RecordAttachments__c.ascx" TagPrefix="uc2"
    TagName="RecordAttachments__c" %>--%>
    <%@ Register Src="~/UserControls/Aptify_Custom__c/BeforeSaveRecordAttachments__c.ascx"
    TagName="SMAARecordAttachments" TagPrefix="uc2" %>

<link  href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
<!-- Override bootstrap css for corporate styles -->
<link href="../../../CSS/bootstrap-override.min.css" rel="stylesheet" /> <%-- Susan Wong, #18954 Improve UI --%>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
<script type="text/javascript">
    function ShowPopup() {
        $("#myModal").modal('show');
    }
    //Function added for Redmine #20582
    function AllowNumericOnly(evt)//[0..9]
    {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode >= 48 && charCode <= 57 || charCode == 46)
            return true;
        else
            return false;
    }
    </script>
<style type="text/css">
.modal-header-success {
    color:#fff;
    padding:9px 15px;
    border-bottom:1px solid #eee;
    background-color: #5cb85c;
    -webkit-border-top-left-radius: 5px;
    -webkit-border-top-right-radius: 5px;
    -moz-border-radius-topleft: 5px;
    -moz-border-radius-topright: 5px;
     border-top-left-radius: 5px;
     border-top-right-radius: 5px;
}
</style>
<%-- Susan Wong, #18954 Tidy up page --%>
<!--This is the correct ABATEMENT FORM -->
<%-- Susan Wong, #18954 Improve UI START --%>
<div>
    <asp:Label ID="Label1" runat="server" class="main-label-title">Status:</asp:Label>
    <asp:Label ID="lblStatus" runat="server" Text="" class="main-label-data"></asp:Label><br />
    <asp:Label ID="lblRejectedReason" runat="server" Visible="false" class="main-label-title">Reason:</asp:Label>
    <asp:Label ID="lblRejectedMessage" runat="server" Text="" Visible="false" class="main-label-data"></asp:Label>
    <asp:Label ID="Label2" runat="server"></asp:Label>
</div>
<div>
    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" ForeColor="Green"></asp:Label>
</div>
<%-- Susan Wong, #18954 Improve UI END --%>
<div>
    <div class="sfContentBlock abatement-msg-box">
        <asp:Label ID="lblABTName" CssClass="abatement-name" runat="server"></asp:Label>
        <asp:Label ID="lbldesc" runat="server" CssClass="abatement-desc"></asp:Label>
    </div>
    <table>
        <tr style="display:none;">
            <td style="align:left;">
                <asp:Label ID="lblDescriptionText" runat="server" Width="900px" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr runat="Server" id="MaternityDes" visible="false" style="display:none;">
            <td>
                <asp:Label ID="lblMoreDiscription" runat="server" Width="900px" visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="display:none;">
                <asp:Label ID="lblIwish" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="display:none;">
                <asp:Label ID="lblcriteria" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="display:none;"">
                <asp:Label ID="lblAtrributevalue" runat="server" Font-Size="Medium"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel1" runat="server" Style="border: 0px; Width=100%">
        <table class="abatement-app-form" style="width: 100%;font-weight:normal">
            <tr>
                <td colspan="5">
                    <div class="sfContentBlock">
                        <a href="profile.aspx" class="cai-btn cai-btn-navy-inverse">Manage your online profile</a> <%-- Susan Wong, #18954 Improve UI--%>
                    </div>
                    <div class="form-section-half-border">
	                    <div class="field-group">
		                    <asp:Label runat="server" CssClass="label-title">Name</asp:Label>
                            <asp:Label ID="lblfname" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblsname" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblsalutation" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lbllname" runat="server"></asp:Label>
	                    </div>
                    </div>
                    <div class="form-section-half-border">
	                    <div class="field-group">
		                    <asp:Label runat="server" CssClass="label-title">Date</asp:Label>
                            <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
	                    </div>
                    </div>
                    
                    <div class="form-section-half-border" runat="server">
	                    <div class="field-group">
		                    <asp:Label runat="server" CssClass="label-title">Type<span class="required"></span></asp:Label>
                            <asp:DropDownList ID="ddlAbatementType" runat="server" AutoPostBack="true">
                            <%--<asp:Label ID="Lblabatetypecheck" runat="server" Font-Bold="true"></asp:Label>
                            <asp:CheckBox ID="checkReason" runat="server" />--%>
                    </asp:DropDownList>
                     <%--Added RequiredFieldValidator as part of log #20288 --%>
                    <asp:RequiredFieldValidator ID="RequiredAbatementType" runat="server" ErrorMessage="Please select Abatement type"
                        ForeColor="Red" ControlToValidate="ddlAbatementType" InitialValue="--Select--"></asp:RequiredFieldValidator>
	                    </div>
                    </div>
                    <div class="form-section-half-border">
	                    <div class="field-group">
		                    <asp:Label runat="server" CssClass="label-title">Reason<span class="required"></span></asp:Label>
                            <asp:TextBox ID="txtReason" mode="multiline" runat="server" Height="300px"></asp:TextBox>
                             <%--Added RequiredFieldValidator as part of log #20288 --%>
                            <asp:RequiredFieldValidator ID="RequiredReason" runat="server" ErrorMessage="Reason required"
                                ForeColor="Red" ControlToValidate="txtReason" InitialValue=""></asp:RequiredFieldValidator>
	                    </div>
                    </div>
                    <div class="form-section-half-border">
	                    <div class="field-group">
		                    <asp:Label runat="server" CssClass="label-title">Level of income<span class="required"></span></asp:Label>
                            <asp:DropDownList ID="ddlstatus" runat="server"></asp:DropDownList>
                            <%--Modified RequiredFieldValidator as part of log #20288 --%>
                            <asp:RequiredFieldValidator ID="RequiredIncomeLevel" runat="server" ErrorMessage="Please select level of income"
                                ForeColor="Red" ControlToValidate="ddlstatus" InitialValue="--Select--"></asp:RequiredFieldValidator>
                            <%-- <asp:DropDownList ID="ddlLevelofIncome" runat="server"></asp:DropDownList>--%>
                            <%-- <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem>Level A: Annual income is less than €15,000</asp:ListItem>
                            <asp:ListItem>Level B: Annual Income is €15,000 - €27,000</asp:ListItem>
                            <asp:ListItem>Level C: Annual income is €27,000 - €38,000</asp:ListItem>--%>
	                    </div>
                    </div>
                    <div class="form-section-half-border">
	                    <div class="field-group">
		                    <asp:Label runat="server" CssClass="label-title">Annual income <asp:Label ID="lblPrefeeredCurrency" runat="server" Text=""></asp:Label><span class="required"></span></asp:Label>
                            <asp:TextBox ID="txtAnnualIncome" runat="server" onkeypress="return AllowNumericOnly(event)"></asp:TextBox>  <%-- onkeypress added for Redmine #20582--%>
                            <%--Added RequiredFieldValidator as part of log #20288 --%>
                            <asp:RequiredFieldValidator ID="RequiredAnnualIncome" runat="server" ControlToValidate="txtAnnualIncome"
                                Display="Dynamic" ErrorMessage="Annual Income required" ForeColor="Red"></asp:RequiredFieldValidator>

	                    </div>
                    </div>
                    <div class="form-section-half-border">
	                    <div class="field-group">
		                    <asp:Label runat="server" CssClass="label-title">Email<span class="required"></span></asp:Label>
                            <asp:TextBox ID="TxtEmail" runat="server"></asp:TextBox>
                            <%--Added RequiredFieldValidator as part of log #20288 --%>
                            <asp:RequiredFieldValidator ID="RequiredEmail" runat="server" ControlToValidate="TxtEmail"
                        Display="Dynamic" ErrorMessage="Email address Required" ForeColor="Red"></asp:RequiredFieldValidator>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtEmail"
                                Display="Dynamic" ErrorMessage="Email Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TxtEmail"
                                Display="Dynamic" ErrorMessage="Invalid  Email " ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>--%>
	                    </div>
                    </div>
                    <div class="form-section-half-border no-border">
	                    <div class="field-group">
		                    <asp:Label runat="server" CssClass="label-title">Phone</asp:Label>
                            <asp:TextBox ID="txtPhoneAreaCode" runat="server" CssClass="txtUserProfileAreaCodeSmall" MaxLength="5" Width="20%"></asp:TextBox>
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="txtUserProfileAreaCode" MaxLength="11" Width="78.555%"></asp:TextBox>
                            <span class="txtbox-help-text" style="text-align-last:left">Area or mob prefix / number</span>
	                    </div>
                    </div>
                    <div class="field-group">
	                    <span class="required"></span> These fields are required
                    </div>
                    <asp:Label ID="lblAbatementYear" runat="server" Font-Bold="True" visible="false"></asp:Label>
                </td>
            </tr>
            <%--<tr style="height: 30px" id="idAbatementType" runat="server" visible="true">
                <td class="auto-style23" align="left" valign="top" width="200px"></td>
                <td class="auto-style25"></td>
                <td class="auto-style28"></td>
                <td colspan="2"></td>
            </tr>--%>
            <%--<tr style="height: 30px">
                <td class="auto-style23" align="left" valign="top" >
                    <b>Status : </b>&nbsp;
                </td>
                <td class="auto-style25" colspan="2">
                 <b>   <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label></b>
                </td>
                <td colspan="2">
                </td>
            </tr>--%>
            <%--<tr style="height: 40px">
                <td class="auto-style23" align="left" valign="top" width="200px"></td>
                <td class="auto-style25" colspan="2"></td>
                <td colspan="2"></td>
            </tr>
            <tr colspan="2">
                <td>&nbsp;</td>
            </tr>
            <tr style="height: 40px">
                <td class="auto-style23" align="left" valign="top" width="200px">
                    <asp:Label ID="LbllevelIncome" runat="server" Font-Bold="true"><span class="RequiredField">*</span>Level of income: </asp:Label>&nbsp;
                </td>
                <td class="auto-style25" colspan="2">
                </td>
                <td colspan="2">
                </td>
            </tr>
            <tr style="height: 40px">
                <td class="auto-style23" align="left" valign="top" width="200px"></td>
                <td class="auto-style25" colspan="2"></td>
                <td colspan="2"></td>
            </tr>
            <tr>
                <td></td>
                <td colspan="2"></td>
            </tr>
            <tr runat="server"  >
                <td class="auto-style16" align="left" valign="top" width="200px">
                    <asp:Label ID="lblemail1" runat="server" Font-Bold="true"><span class="RequiredField">*</span>Email address :</asp:Label>&nbsp;
                </td>
                <td class="auto-style17" colspan="2"></td>
                <td class="auto-style10"></td>
                <td class="auto-style18"></td>
            </tr>
            <tr runat="server"  >
                <td class="auto-style19" align="left" valign="top" width="200px">
                    <asp:Label ID="lblphn" runat="server" Font-Bold="true"> Phone number :</asp:Label>&nbsp;
                </td>
                <td class="auto-style21" colspan="2"></td>
                <td colspan="2" class="auto-style21"></td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>--%>
            <%-- Susan Wong, #18954 Improve UI START --%>
            <tr id="trRecordAttachment" runat="server" class="cai-form" style="height: 60px;" >
                <td colspan="5">
                    <div class="form-section-full-border" style="margin-bottom:10px">
                        <h2 style="font-size:24px">Upload required supporting documents</h2>
                        <p class="info-note">You will need to upload your documents here. Documents should be in <strong>PDF</strong> or <strong>Microsoft Word</strong> format.</p>
                        <div class="form-group">
                            <span class="label-title">Upload documents:<span class="required"></span></span>
                            <div class="form-section-half-border no-border">
                                <ol style="margin-left:20px">
                                    <li>Click on <code>Choose file</code></li>
                                    <li>Find file and click <code>Open</code></li>
                                    <li>Click on <code>Upload</code></li>
                                    <li>Click on <code>Save</code> OR <code>Submit</code></li>
                                </ol>
                            </div>
                            <asp:Panel ID="Panel2" runat="Server" class="form-section-half-border no-border">
                                <table runat="server" id="Table1" class="data-form" width="100%">
                                    <tr>
                                        <td class="plain-table">
                                            <uc2:SMAARecordAttachments ID="raUploadDocs" runat="server" AllowView="True"
                                                AllowAdd="True" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>
                    </div>
                </td>
            </tr>
            <%-- Susan Wong, #18954 Improve UI END --%>
            <%--<tr id="trRejectMessage" runat="server" visible="false">
                <td class="auto-style23" style="font-weight: bold" align="right">
                    <b>Reject Message :</b>&nbsp;<br />
                </td>
                <td class="auto-style4" colspan="4">
                    <b><asp:Label ID="lblRejectedMessage" runat="server" Text=""></asp:Label></b>
                </td>
                <td></td>
                <td></td>
            </tr>--%>
            <tr id="trAbatementStatusReason" runat="server" visible="false">
                <td class="auto-style23" style="font-weight: bold" align="right">
                    <b>Abatement status reasons :</b>&nbsp;<br />
                </td>
                <td class="auto-style4" colspan="4">
                    <asp:GridView ID="grdAbatmentStatusReason" runat="server" AutoGenerateColumns="False" Width="500px">
                        <Columns>
                            <%--<asp:TemplateField HeaderText="Company">
                                <ItemTemplate>
                                    <asp:Label ID="lblAllCompany" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblCompanyID" runat="server" Text='<%# Eval("CompanyID")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblCompany" runat="server" Text='<%# Eval("Company")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Status Reason">
                                <ItemTemplate>
                                                        
                                    <asp:Label ID="lblStatusReason" runat="server" Text='<%# Eval("StatusReason")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Is Completed">
                                <ItemTemplate>
                                    <asp:Label ID="lblIsCompleted" runat="server" Text='<%# Eval("IsCompleted")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Abatement Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblAbatementStatusID" runat="server" Text='<%# Eval("AbatementStatusID")%>' Visible="false" />
                                    <asp:Label ID="lblAbatementStatus" runat="server" Text='<%# Eval("AbatementStatus")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle />
                    </asp:GridView>
                   <%-- <telerik:RadGrid ID="grdAbatmentStatusReason" runat="server" AutoGenerateColumns="False"
                        Width="500px" AllowSorting="false">
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView>
                            <Columns>
                                <telerik:GridBoundColumn DataField="StatusReason" HeaderText="Status Reason" />
                                <telerik:GridBoundColumn DataField="IsCompleted" HeaderText="Is Completed" />
                                <telerik:GridBoundColumn DataField="AbatementStatus" HeaderText="Abatement Status" />
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>--%>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:Label ID="lblNote" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:CheckBox ID="chkPartTimeEmp" runat="server" Visible="false" />
                    <asp:Label ID="lblPartTimeEmp" runat="server" Text=""></asp:Label> 
                 </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:CheckBox ID="chkAbatementBottom" runat="server" Visible="false" />
                    <asp:Label ID="lblBottomText" runat="server" Text="" style="font-weight:bold"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="5" style="display:none;">
                    <asp:Label ID="lblBottomLastText" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lblTick" runat="server" Text=" please tick box"></asp:Label>
                    <asp:CheckBox ID="Chkinfo" runat="server" Text="" />
                </td>
            </tr>
            <tr>
                <td colspan="5" style="display:none;">
                    <strong>Please return completed form by post to: </strong>
                </td>
            </tr>
            <tr>
                <td colspan="5" style="display:none;">
                    <uc1:organisationAddress__c ID="organisationAddress__c1" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="5" style="display:none;">
                    <asp:Label ID="lblAbtementFormBottomText" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="5"  style="padding-top:20px;">
                    <asp:Label ID="lblMsg1" runat="server" Font-Bold="True" ForeColor="Green"></asp:Label><br />
                    <input type="button" id="btnPrint" onclick="window.print();" value="Print" runat="server" class="submitBtn"/>
                    <asp:Button ID="btnsubmit" runat="server" Text="Save" class="submitBtn" />
                    <asp:Button ID="btnSave" runat="server" Text="Submit" class="submitBtn"  OnClick="btnSave_Click"></asp:Button> <br />

                </td>
            </tr>
        <tr>
            <td colspan="5">
                <h4>Use and protection of your personal information</h4>
                <p>The Institute of Chartered Accountants in Ireland (“the Institute”) will use the information contained in this form together with any other information otherwise 
                furnished by you or by other third parties for the purposes of processing this application; managing and administering your membership; and generally for the 
                performance by the Institute of its regulatory, supervisory and statutory functions, as more fully described in the Institute’s <a href="https://www.charteredaccountants.ie/Privacy-statement" target="_blank"><strong>privacy statement</strong></a>, which explains 
                your rights in relation to your personal data. You acknowledge you have read and understand the privacy statement.</p>
            </td>
        </tr>
        </table>
    </asp:Panel>
</div>
<cc3:User ID="AptifyEbusinessUser1" runat="server" />
              <!-- popup modal -->
            <div class="modal fade" id="myModal">
                <div class="modal-dialog">
                    <div class="modal-content">
                <div class="modal-header modal-header-success">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4><i class="glyphicon glyphicon-thumbs-up"></i> Submitted successfully</h4>
                </div>
                        <div class="modal-body">
                            <asp:Label ID="lblMessage" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="cai-btn cai-btn-red-inverse" data-dismiss="modal">
                                Close</button>
                           <%-- <button type="button" class="cai-btn cai-btn-red" data-dismiss="modal">CLOSE</button> --%> 
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>
            <!-- /.modal -->
<script>
    $(document).ready(function () {
        if ($('.abatement-name').text().trim() === '') {
            $('.abatement-msg-box').css('display', 'none');
        }
        else { $('.abatement-msg-box').css('display', 'block'); }
    });

    
</script>
