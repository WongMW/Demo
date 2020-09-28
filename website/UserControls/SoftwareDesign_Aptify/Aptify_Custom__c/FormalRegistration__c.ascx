<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/FormalRegistration__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Education.FormalRegistration__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register TagPrefix="uc1" TagName="Profile__c" Src="Profile__c.ascx" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/UserControls/Aptify_Custom__c/RecordAttachments__c.ascx" TagPrefix="uc2"
    TagName="RecordAttachments__c" %>

<script type="text/javascript">
    function CollapseExpand(me, HiddenPanelState) {
        $('#' + me).slideToggle('slow');
    }

  <%--  function HideLabel(sender, args) {
        var input = args.get_fileName();
        var n = input.indexOf(".");
        var fileExtension = input.substr(n + 1);
        var extensionArrayList = new Array("png", "jpg", "txt", "doc", "docx", "pdf", "bmp", "gif");
        for (var i = 0; i < extensionArrayList.length; i++) {
            if (fileExtension == extensionArrayList[i]) {
                if (document.getElementById('<%=lblErrorFile.ClientID%>'))
                    document.getElementById('<%=lblErrorFile.ClientID%>').style.display = 'none';
                return false;
            }
        }
    }--%>

    function openReportWindow() {
        window.open(window.document.getElementById('<%= hdnReportPath.ClientID %>').value);
    }
</script>

<style type="text/css">
    .tooltip{
        display: inline;
        position: relative;

    }

    .tooltip:hover:after{
    background: #003D51;
    background: rgba(0,61,81,.9);
    border-radius: 5px;
    bottom: 26px;
    color: #fff;
    content: attr(title);
    left: 20%;
    padding: 5px 15px;
    position: absolute;
    z-index: 98;
    width: 220px;
    font-size: 14px;
}

    .tooltip:hover:before{
    border: solid;
    border-color: #003D51 transparent;
    border-width: 6px 6px 0 6px;
    bottom: 20px;
    content: "";
    left: 50%;
    position: absolute;
    z-index: 99;
}

    .qtext {
        font-size: 13px;
    }
    /*added new css for redmine #20591*/  
    .WindowPosition {
        width:400px;
        height:500px;
        padding-top: 12%;
        overflow: hidden !important;
    }
</style>


<div id="divContent" runat="server">
    <asp:HiddenField ID="hdnPersonDetailsState" runat="server" Value="1" />
    <asp:HiddenField ID="hdnEmployementDetailsState" runat="server" Value="1" />
    <asp:HiddenField ID="hdnExemptionsGrantedState" runat="server" Value="1" />
    <asp:HiddenField ID="hdnStatusReasonState" runat="server" Value="1" />
    <asp:HiddenField ID="hdnEligibilityState" runat="server" Value="1" />
    <div id="MainTable">
        
        <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
        <br />
        <asp:Panel ID="pnlData" runat="server">
            <asp:Label ID="lblExemptionsNotFound" runat="server"></asp:Label>
            <rad:RadGrid ID="grdFormalReg" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                AllowFilteringByColumn="false" SortingSettings-SortedDescToolTip="Sorted Descending"
                AllowSorting="false" SortingSettings-SortedAscToolTip="Sorted Ascending" CssClass="cai-table mobile-table">
                <GroupingSettings CaseSensitive="false" />
                <MasterTableView AllowFilteringByColumn="false" AllowSorting="true" AllowNaturalSort="false">
                    <Columns>
                        <rad:GridTemplateColumn HeaderText="Registration type" DataField="Type" AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" >
                            <ItemTemplate>
                                <span class="mobile-label">Registration type:</span>
                                <asp:HyperLink CssClass="cai-table-data" ID="lnkRegType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Type") %>'
                                    NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"DataNavigateUrl") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn HeaderText="Registration status" DataField="Status" AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" >
                            <ItemTemplate>
                                <span class="mobile-label">Registration status:</span>
                                <asp:Label CssClass="cai-table-data" ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Status") %>'></asp:Label>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn HeaderText="Start date" DataField="ContractStartDate" AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" >
                            <ItemTemplate>
                                <span class="mobile-label">Start date:</span>
                                <asp:Label CssClass="cai-table-data" ID="lblStartDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ContractStartDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn HeaderText="End date" DataField="ContractExpireDate" AutoPostBackOnFilter="true"
                            CurrentFilterFunction="Contains" ShowFilterIcon="false" >
                            <ItemTemplate>
                                <span class="mobile-label">End date:</span>
                                <asp:Label CssClass="cai-table-data" ID="lblEndDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ContractExpireDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </rad:RadGrid>
        </asp:Panel>

        <div class="cai-form">
            <asp:Panel ID="pnlDetails" runat="server">
                <div class="form-title">Status</div>
                <div>
                    <asp:HiddenField ID="hdnReportPath" runat="server" />
                    <asp:HiddenField ID="hidDateRegistered" runat="server" />
                    <asp:HiddenField ID="hidFormalID" runat="server" />
                    <asp:HiddenField ID="hidEERouteOfEntry" runat="server" />
                </div>
                <div>
                    <div class="field-group">
                        <span class="label-title-inline">Status:</span>
                        <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                    </div>

                    <div class="field-group" style="display:none;">
                        <span class="label-title-inline">Type:</span>
                        <asp:Label ID="lblType" runat="server" Text=""></asp:Label>
                    </div>

                    <div class="field-group">
                        <span class="label-title-inline">Route of entry:</span>
                        <asp:Label ID="lblRouteOfEntry" runat="server" Text=""></asp:Label>
                        <asp:HiddenField ID="hidRouteOfEntry" runat="server" />
                    </div>
                </div>

                <div class="expand form-title" id="HeadPersonalDetails" onclick="CollapseExpand('PersonalDetails','hdnPersonDetailsState')">Personal details:</div>
                <div id="PersonalDetails" class="active">
                    <asp:UpdatePanel ID="UpdatepnlorderDetail" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <div runat="server" id="EduDetails" class="form-section-half-border">
                                <div class="field-group">
                                    <div class="label-title">Student number:</div>
                                    <asp:TextBox ID="txtStudentNo" Enabled="false" runat="server"></asp:TextBox>
                                </div>

                                <div class="field-group">
                                    <div class="label-title ">Student name:  </div>
                                    <asp:TextBox ID="txtStudentName" Enabled="false" runat="server"></asp:TextBox>
                                </div>

                                <div class="field-group">
                                    <div class="label-title ">Gender:</div>
                                    <asp:TextBox ID="txtGender" Enabled="false" runat="server"></asp:TextBox>
                                </div>

                                <div class="field-group">
                                    <div class="label-title ">Country of origin: </div>
                                    <asp:TextBox ID="txtCountryOrigin" Enabled="false" runat="server"></asp:TextBox>
                                </div>

                                <div class="field-group">
                                    <div class="label-title ">Email: </div>
                                    <asp:TextBox ID="txtEmail" Enabled="false" runat="server"></asp:TextBox>
                                </div>

                                <div class="field-group">
                                    <div class="label-title ">Mobile:</div>
                                    <asp:TextBox ID="txtMobile" Enabled="false" runat="server"></asp:TextBox>
                                </div>

<div id="divRecognizedexperience" visible ="false" runat="server" class="field-group">
                                                                <div class="label-title ">Required experience for membership:</div>
                                                                <asp:TextBox ID="txtRecognizedexperience" Enabled="false" runat="server"></asp:TextBox>
                                                            </div>

                            </div>
                            <div runat="server" id="Div1" class="form-section-half-border">
                                <div class="field-group">
                                    <div class="label-title ">Address 1:</div>
                                    <asp:TextBox ID="txtAddress1" Enabled="false" runat="server"></asp:TextBox>
                                </div>

                                <div class="field-group">
                                    <div class="label-title ">Address 2: </div>
                                    <asp:TextBox ID="txtAddress2" Enabled="false" runat="server"></asp:TextBox>
                                </div>

                                <div class="field-group">
                                    <div class="label-title ">Address 3:</div>
                                    <asp:TextBox ID="txtAddress3" Enabled="false" runat="server"></asp:TextBox>
                                </div>

                                <div class="field-group">
                                    <div class="label-title ">City:</div>
                                    <asp:TextBox ID="txtCity" Enabled="false" runat="server"></asp:TextBox>
                                </div>

				<div class="field-group">
                                    <div class="label-title ">County:</div>
                                    <asp:TextBox ID="txtCounty" Enabled="false" runat="server"></asp:TextBox>
                                </div>
                           

                                <div class="field-group">
                                    <div class="label-title ">Postal code:</div>
                                    <asp:TextBox ID="txtPostalCode" Enabled="false" runat="server"></asp:TextBox>
                                </div>

   </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <asp:Panel ID="pnlEmploymentDetails" runat="server">
                    <div class="expand form-title" id="HeadEmploymentDetails" onclick="CollapseExpand('Employement','hdnEmployementDetailsState')">Employment details:</div>
                    <div id="Employement" class="active">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <div runat="server" id="Div2" class="form-section-half-border">
                                    <div class="field-group">
                                        <div class="label-title ">Firm name: </div>
                                        <asp:TextBox ID="txtFirmName" Enabled="false" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="field-group">
                                        <div class="label-title ">Address 1:</div>
                                        <asp:TextBox ID="txtfirmAddress1" Enabled="false" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="field-group">
                                        <div class="label-title ">Address 2:</div>
                                        <asp:TextBox ID="txtfirmAddress2" Enabled="false" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="field-group">
                                        <div class="label-title ">Address 3:</div>
                                        <asp:TextBox ID="txtfirmAddress3" Enabled="false" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="field-group">
                                        <div class="label-title ">City:</div>
                                        <asp:TextBox ID="txtfirmCity" Enabled="false" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="field-group">
                                        <div class="label-title ">Postal code:</div>
                                        <asp:TextBox ID="txtfirmPostalCode" Enabled="false" runat="server"></asp:TextBox>
                                    </div>

                                </div>

                                <div runat="server" id="Div3" class="form-section-half-border">

                                    <div class="field-group">
                                        <div class="label-title ">County: </div>
                                        <asp:TextBox ID="txtfirmCounty" Enabled="false" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="field-group">
                                        <div class="label-title ">Country: </div>
                                        <asp:TextBox ID="txtfirmCountry" Enabled="false" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="field-group">
                                        <div class="label-title ">Start date: </div>
                                        <asp:TextBox ID="txtfirmStartDate" Enabled="false" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="field-group">
                                        <div class="label-title ">End date: </div>
                                        <asp:TextBox ID="txtfirmEndDate" Enabled="false" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="field-group">
                                        <div class="label-title ">Contract duration:</div>
                                        <asp:TextBox ID="txtfirmContractDur" Enabled="false" runat="server"></asp:TextBox>
                                    </div>

                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlEligibility" runat="server">
                    <div id="H1" class="expand form-title" onclick="CollapseExpand('Eligibility','hdnEligibilityState')">Eligibility for registration:</div>
                    <div id="Eligibility" class="active">
                        <asp:Panel ID="Panel1" runat="server">
                            <div class="field-group">
                                <div runat="server" id="divRoute" class="label-title">
                                </div>
                            </div>
                            <div class="field-group">
                                <asp:Panel ID="Panel2" runat="server">
                                    <ul class="route-list">
                                        <li>
                                            <div runat="server" id="divRoutelevel">
                                            </div>
                                        </li>
                                        <li>
                                            <asp:RadioButton ID="chkRouteA" runat="server" GroupName="Group1" />
                                            <span><u>Route A</u> with four year's work experience (any discipline)</span>
                                        </li>

                                        <li>
                                            <asp:RadioButton ID="chkRouteB" runat="server" GroupName="Group1" />
                                            <span><u>Route B</u> without four year's work experience (any discipline)</span>
                                        </li>
                                        <li>
                                            <div runat="server" id="divEligibility1" class="field-group" style="display:none;">
                                            </div>
                                        </li>
                                    </ul>
                                </asp:Panel>
                            </div>

                            <div id="divEligibility2" runat="server" class="field-group">
                            </div>
                        </asp:Panel>
                    </div>
                </asp:Panel>

                <div id="HeadDeclaration" class="expand form-title" onclick="CollapseExpand('ExemptionsGranted','hdnExemptionsGrantedState')">Terms and conditions:</div>
                <div id="ExemptionsGranted" class="active">
                    <asp:Panel ID="pnlCotractDeclaration" runat="server">
                        <div class="cai-form-content">
                            <div id="divFlexibleDeclaration" visible="false" runat="server">
                                <div id="divFlexibleDeclarationText" runat="server" style="display:none;">
                                </div>
                            </div>
                            <div id="divContractDeclaration" visible="false" runat="server" style="display:none;">
                                <div id="divContractDeclaration2" runat="server">
                                </div>
                            </div>
                            <div runat="server" id="Div4">
                                <div style="display:none;">
                                    <asp:Label ID="lblSubmittedDate" runat="server" CssClass="label-title" Text="Submitted date:"></asp:Label>
                                    <asp:TextBox ID="txtSubmitteddate" Enabled="false" runat="server"></asp:TextBox>
                                </div>

                                <div id="divWitnessName" runat="server" visible="false">
                                    <asp:Label ID="Label1" runat="server" CssClass="label-title" Text="Witness name(block):"></asp:Label>
                                    <asp:TextBox ID="txtWitnessName" Enabled="false" runat="server"></asp:TextBox>
                                </div>
                                <div id="divWitnessSign" runat="server" style="display:none;" visible="false">
                                    <asp:Label ID="Label2" runat="server" CssClass="label-title" Text="Witness signature:"></asp:Label>
                                    <asp:TextBox ID="txtWitnessSign" Enabled="false" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div runat="server" id="Div5" style="display:none;">
                                <span class="label-title">Signature:</span>
                                <asp:TextBox ID="txtSignature" Enabled="false" runat="server"></asp:TextBox>
                            </div>
                            <div id="divTermsandconditions" runat="server">
                                <asp:CheckBox ID="chkTermsandconditions" Text="" runat="server" AutoPostBack="true"  /><%-- updated for redmine #20464--%>
                                <asp:Label runat="server">Please tick the box to confirm you have read and agree to the <a href="/Current-Student/Student-Centre/contract-terms-and-conditions" target="_blank" style="text-decoration:underline;">terms and conditions</a> of your formal registration.</asp:Label>
                                <div class="info-tip"><strong>Training Contract:</strong> once you have ticked the box and agreed to the above T&C's, you will then be given an option to 'PRINT REGISTRATION FORM'. Please print this form, sign and return the original to your employer as soon as possible to complete your formal registration.<br />
                                <strong>Flexible Route:</strong> once you have ticked the box and agreed to the above T&C's, and selected Route A/B, you will then be given an option to 'PRINT REGISTRATION FORM'. Please print this form, sign and return the original to Training Support as soon as possible to complete your formal registration.<br />
                                    *The witness signature must be someone who can verify your identity.</div>
                            </div>
 <%--comment on 27/04/16 from APTIFY 
                            <div runat="server" id="divAttachments" visible="false">
                                <span class="Error">
                                    <asp:Label ID="lblErrorFile" Visible="false" runat="server">Error</asp:Label>
                                </span>
                                <uc2:RecordAttachments__c ID="RecordAttachments__c" runat="server" AllowView="True" AllowAdd="True" AllowDelete="false" />
                            </div>--%>

<%-- comment on 27/04/16 from APTIFY 
                               <div class="actions">
                                <asp:Button ID="btnCreate" runat="server" CssClass="submitBtn" Text="Create" />
                            </div>--%>
                            <div id="divContractNotice" visible="false" runat="server" style="display:none;">
                                <div id="divContractNotice2" runat="server">
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>

                <asp:Panel ID="pnlStatusReason" Visible="false" runat="server">
                    <div id="HeadStatusReason" class="expand form-title" onclick="CollapseExpand('StatusReason','hdnStatusReasonState')">Status reason</div>
                    <div id="StatusReason" class="active">
                        <asp:Panel ID="Panel3" Style="padding-left: 30pt;" runat="server">
                            <span>Status reason:</span>
                            <asp:TextBox ID="txtStatusReason" Enabled="false" Width="600px" Height="40px" Text=""
                                TextMode="MultiLine" runat="server"></asp:TextBox>
                        </asp:Panel>
                    </div>
                </asp:Panel>
				<div class="field-group warning-msg-box">
                    <asp:Label id="lblpopups" runat="server" >Please disable your browser's popup blocker before you print your contract. <a href="/Prospective-Students/browser-popups.aspx" target="_blank" style="text-decoration:underline;">You can find out how to do this here.</a></asp:Label></div>
                
                <%--<asp:Label id="lblpopups2" runat="server">Please disable your browser's popup blocker before you print your contract. <a href="/Prospective-Students/browser-popups.aspx" target="_blank">You can find out how to do this here.</a></asp:Label></div>--%>
                <div class="actions field-group">
                  <%--Commented below code and rename Submit button text as per Redmine #20464--%>
                    <%--<asp:Button ID="btnSubmit" runat="server" CssClass="submitBtn" Text="Submit & Print" Visible="false" />--%>
					<asp:Button ID="btnSubmit" runat="server" CssClass="submitBtn" Text="PRINT REGISTRATION FORM" Visible="false" />  <%--Updated button Text property by GM for Redmine # 20464 --%>
                    
                    <asp:Button ID="btnPrint" runat="server" CssClass="submitBtn" Text="Print contract" Visible="false"/>					<asp:Button ID="Button1" runat="server" CssClass="submitBtn" Text="Submit" Visible="false" />  <%--Visible="false" for Redmine # 20464 --%>
                    <asp:Button ID="btnSubmitContract" runat="server" CssClass="submitBtn" Text="PRINT REGISTRATION FORM" Visible="false" />  <%--New button added and updated Text property by GM for Redmine # 20464 --%>
                    <asp:Button ID="btnBack" Visible="false" runat="server" CssClass="submitBtn" Text="Back" />
                </div>
                
            </asp:Panel>
        </div>
    </div>
</div>
<div class="sfContentBlock">
    <cc1:User ID="AptifyEbusinessUser1" runat="server" />
     <%-- Changes in RadWindow  properties for redmine #20591 --%>
    <telerik:RadWindow ID="radMockTrial" runat="server" Width="400px" Height="400px"
        Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
        Title="Formal registration" Behavior="None" CssClass="WindowPosition">
        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblWarning" runat="server" Text=""></asp:Label>
						<br />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnDownload" runat="server" Text="terms & conditions" Width="80%" CssClass="submitBtn" Visible="false" /><%--'Changes for Redmine #20591--%>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnOk" runat="server" Text="Ok" Width="70px" class="submitBtn" />
                        <asp:Button ID="btnOKValidation" runat="server" Visible="false" Text="close" Width="90px"
                            class="submitBtn" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </telerik:RadWindow>
    <telerik:RadWindow ID="radCreateMsg" runat="server" Width="350px" Height="120px"
        Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
        Title="Formal registration" Behavior="None">
        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblCreate" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnCreateOK" runat="server" Text="Ok" Width="70px" class="submitBtn" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </telerik:RadWindow>
    <%--RadWindow code added by GM for Redmine #20464--%>
	<telerik:RadWindow ID="radSumbitWin" runat="server" Width="350px" Height="120px"
        Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
        Title="Formal registration" Behavior="None">
        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblMsg" runat="server" Text="Training contract signed by student"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnUpdate" runat="server" Text="Print" Width="70px" class="submitBtn" />	<%--Change button text by GM for Redmine #20464--%>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </telerik:RadWindow>
</div>
