<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConnectForm.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.CharteredConnect.ConnectForm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<!-- Bootstrap CSS -->
 <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet"/>


<!-- BootstrapValidator CSS -->
    <link rel="stylesheet" href="//cdn.jsdelivr.net/jquery.bootstrapvalidator/0.5.0/css/bootstrapValidator.min.css"/>
<!--  intl-tel-input CSS -->
   <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/intl-tel-input/12.1.13/css/intlTelInput.css" />


<script  type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/12.1.13/js/intlTelInput.min.js"></script>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/12.1.13/js/utils.js"></script>

<!-- jQuery and Bootstrap JS -->

<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
   <%-- <script src="https://cdnjs.cloudflare.com/ajax/libs/1000hz-bootstrap-validator/0.11.5/validator.min.js"></script>--%>

 <!-- BootstrapValidator JS -->
    <script type="text/javascript" src="//cdn.jsdelivr.net/jquery.bootstrapvalidator/0.5.0/js/bootstrapValidator.min.js"></script>
<style>
<!-- ADDING STYLES OVERWRITTEN BY BOOTSTRAP STYLES -->
body, .office-link {font-size:16px !important;line-height: 1.4em;}
.title-holder{padding: 10px 0px;margin-top: 14px;}
h1, h2, #Tabs>ul.nav-tabs>li>a{font-family: 'Source Sans Pro', sans-serif;line-height: 1.4em;font-weight: 700;color: #003D51;}
h1{font-size: 32px;}
h2, #Tabs>ul.nav-tabs>li>a{font-size: 24px;}
a, a:link, a:visited, a:hover, a:active {color: #003D51;}
.main-nav li a, .link-holder a, .sfNavHorizontalDropDown .k-item > a.k-link, .sfNavHorizontal li.loginli a {font-size: 16px;}
.main-nav li a:hover, .link-holder a:hover, .sfNavHorizontal li.loginli a:hover{text-decoration: none !important;}
.link-holder a, .sfNavHorizontal li.loginli a{color: #fff}
.footer-wrapper-main li, .footer-wrapper-main li > a, .footer-wrapper-main p, .tel-number {line-height: 1.4em;}
.sfnewsletterForm.sfSubscribe ol.sfnewsletterFieldsList li.sfnewsletterField input, .dataTables_filter input[type=search] {font-weight: normal;}
.sfnewsletterForm.sfSubscribe ol.sfnewsletterFieldsList {margin-bottom:0px;}
.footer-wrapper-main h2, .footer-wrapper-main li > a, .office-link {font-family: 'Source Sans Pro', sans-serif;}
.footer-navs .link-holder a {color: #8C1D40;font-size: 12px;}
.nav-tabs>li>a{background-color:#eee;}
.nav-tabs>li{float: left;margin-bottom: -1px;}
.main-title-banner > h2{margin:0pc!important;}
.sfContentViews.sfSingleContentView{text-align: left;}
.col-sm-10:not(.has-error) .form-control-feedback,
.col-sm-10:not(.has-error) .help-block,
.col-xs-4:not(.has-error) .form-control-feedback,
.col-xs-4:not(.has-error) .help-block {
    display: none;
}
</style>

<script type="text/javascript">
    $(function () {
        $(document).on("keypress", "input", function () {
            if ($(this).closest(".has-error").length) $(this).closest(".has-error").removeClass("has-error");
        });
    });
</script>

<style type="text/css">
       .autocomplete_completionListElement {
        visibility: hidden;
        margin: 0px !important;
        background-color: white;
        color: windowtext;
        border: buttonshadow;
        border-width: 1px;
        border-style: solid;
        cursor: default;
        overflow: scroll;
        height: 150px;
        text-align: left;
        list-style-type: none;
        font-size: 10pt;
        z-index:999;
    }
       
       .maroon-info{background-color:#8C1D40!important;color:#FFF!important;}
       .maroon-info:before{content: "NOTE: "!important;}
</style>

<script type="text/javascript">
    function ClientSelectedAwardingBody(sender, e) {
        document.getElementById("<%=hdnAwardingBody.ClientID %>").value = e.get_text();
    }
</script>

<div id="designerLayoutRoot" class="sfContentViews sfSingleContentView">
    <div id="block" runat="server" class="button-block">
        <asp:Panel ID="pnlFormToFill" runat="server" Visible="true" CssClass="pnlFormToFill">
            <div class="container-fluid bootstrap-style">
                <div class="row-fluid" style=" margin-top: 20px;">
                    <fieldset>
                        <legend class="text-center" style="color:#861F41">Fill in the form</legend>
                        <p>Please tell us a little about yourself by filling out the form below.</p>

                        <p style="color: #861F41" runat="server" id="lblErrorMessage" visible="false"></p>

                        <!--  First Name -->
                        <div class="form-group row">
                            <label class="col-sm-2 col-form-label required" for="txtFirstName"> First Name</label>
                            <div class="col-sm-10" runat="server" id="holderTxtFirstName">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>                                       
                                    <asp:TextBox id="txtFirstName" runat="server" class="form-control txtFirstName" placeholder="First Name" name="txtFirstName"></asp:TextBox>
                                </div>
                                <i class="form-control-feedback glyphicon glyphicon-remove" style="top: 0px; z-index: 100;"></i>
                                <small id="lblFirstNameError" runat="server" class="help-block"></small>
                            </div>
                        </div>
                        <!--  Last Name -->
                        <div class ="form-group row">  
                            <label class="col-sm-2 col-form-label required" for="txtLastName"> Last Name</label>  
                            <div class="col-sm-10" runat="server" id="holderTxtLastName">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>                    
                                    <asp:TextBox id="txtLastName" runat="server" class="form-control" placeholder="Last Name" name="txtLastName"></asp:TextBox>
                                </div>
                                <i class="form-control-feedback glyphicon glyphicon-remove" style="top: 0px; z-index: 100;"></i>
                                <small id="lblLastNameError" runat="server" class="help-block"></small>
                            </div>
                        </div>
                    
                        <!--  Email -->
                        <div class="form-group row">   
                            <label class="col-sm-2 col-form-label required" for="txtEmail"> Email</label>  
                            <div class="col-sm-10" runat="server" id="holderTxtEmail">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-envelope"></i></span>                    
                                    <asp:TextBox id="txtEmail" runat="server" class="form-control"  placeholder="Email" name="txtEmail"  ></asp:TextBox>
                                </div>
                                <i class="form-control-feedback glyphicon glyphicon-remove" style="top: 0px; z-index: 100;"></i>
                                <small id="lblEmailError" runat="server" class="help-block"></small>
                            </div>
                        </div>
                    
                        <!--  College Name -->
                        <div class="form-group row">   
                            <label class="col-sm-2 col-form-label required" for="txtEmail"> College Name</label>  
                            <div class="col-sm-10" runat="server" id="holderTxtCollegeName">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-education"></i></span>                    
                                    <asp:TextBox ID="txtAwardingBody" placeholder="Type 3 letters and select from dropdown" runat="server"
                                        CssClass="form-control" AutoComplete="off" AutoCompleteType="Disabled" MaxLength="3" />
                                    <ajax:autocompleteextender id="extAwardingBody" runat="server" targetcontrolid="txtAwardingBody"
                                        behaviorid="auto3" servicepath="~/WebServices/GetCompanyDetails__c.asmx" enablecaching="false"
                                        minimumprefixlength="2" firstrowselected="false" servicemethod="GetAwardingBodies"
                                        usecontextkey="true" completionsetcount="10" onclientitemselected="ClientSelectedAwardingBody"
                                        completionlistcssclass="autocomplete_completionListElement" />
                                </div>
                                <asp:HiddenField ID="hdnAwardingBody" runat="server" Value="0" />
                                <i class="form-control-feedback glyphicon glyphicon-remove" style="top: 0px; z-index: 100;"></i>
                                <small id="lblAwardingBodyError" runat="server" class="help-block"></small>
                            </div>
                        </div>

                        <!--  Graduation Year -->
                        <div class ="form-group row">  
                            <label class="col-sm-2 col-form-label required" for="txtLastName"> Graduation Year</label>  
                            <div class="col-sm-10" runat="server" id="holderTxtGraduationYear">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>                    
                                    <asp:TextBox MaxLength="4" id="txtGraduationYear" runat="server" class="form-control" placeholder="Graduation Year" name="txtGraduationYear"></asp:TextBox>
                                </div>
                                <i class="form-control-feedback glyphicon glyphicon-remove" style="top: 0px; z-index: 100;"></i>
                                <small id="lblGraduationYearError" runat="server" class="help-block"></small>
                            </div>
                        </div>
						<!-- PATCH FIX CHECKBOXES #21366 -->
                        <div class="form-group row">    
                            <div class="col-sm-2"></div>                        
                                <asp:CheckBox ID="chkAllowCommunication" CssClass="checkbox-inline col-xs-12 col-sm-10" runat="server" Checked="false" Text="Please contact me about related products and services of the Institute (you can opt-out at any time)." />                               
                        </div>

                        <div class="form-group row">    
                            <div class="col-sm-2"></div>                        
                            <div class="col-xs-12 col-sm-10">
                                <h4>Use and protection of your personal information</h4>
                                <p>The Institute will use the information which you have provided in this form to respond to your request or process your transaction and will hold and protect it in accordance with the Institute's <a href="https://www.charteredaccountants.ie/Privacy-policy" target="_blank">privacy statement</a>, which explains your rights in relation to your personal data.</p>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="text-right">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" CssClass="submitBtn" OnClick="btnSave_Click" />
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
        </asp:Panel>
    </div>
</div>