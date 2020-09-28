<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AMLWhistleblowing__c.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.AML.AMLWhistleblowing__c" %>
<link href="<%= ResolveUrl("~/Scripts/InHouse/bootstrap/3.3.7/css/bootstrap.min.css") %>" rel="stylesheet" type="text/css"/>
<link href="<%= ResolveUrl("~/Scripts/InHouse/bootstrap/bootstrapValidator-0.5.0.min.css") %>" rel="stylesheet" type="text/css"/>
<link href="<%= ResolveUrl("~/CSS/bootstrap-override.min.css") %>" rel="stylesheet" type="text/css"/>

<script src="<%= ResolveUrl("~/Scripts/InHouse/bootstrap/3.3.7/js/bootstrap.min.js") %>"></script>
<script src="<%= ResolveUrl("~/Scripts/InHouse/bootstrap/bootstrapValidator-0.5.0.min.js") %>"></script>

<script type="text/javascript">  
$(document).ready(function () {
    $('#form1').bootstrapValidator({
        // To use feedback icons, ensure that you use Bootstrap v3.1.0 or later
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            // First name validation START
            <%=txtFirstName.UniqueID%>: {
                validators: {
                    regexp: {
                        regexp: /^[a-zA-Z\s]+$/,
                        message: 'The first name must contain only alphabetic characters'
                    }
                }
            },// First name validation END
            // Last name validation START
            <%=txtLastName.UniqueID%>: {
                validators: {
                    regexp: {
                        regexp: /^[a-zA-Z\s]+$/,
                        message: 'The last name must contain only alphabetic characters'
                    }
                }
            },// End of last name validation
            // Email validation START
            <%=txtEmail.UniqueID%>: {
                validators: {
                    regexp: {
                        regexp: '^[^@\\s]+@([^@\\s]+\\.)+[^@\\s]+$',
                        message: 'Not a valid email address'
                    }
                }
            },// Email validation END
            // Phone Country Code validation START      
            <%=txtCountryCode.UniqueID%>: {
                validators: {
                    stringLength: {
                        min: 2,
                        max: 3,
                        message: 'Country code must be between 2 - 3 numbers long e.g. 353 or 44'
                    },
                    regexp: {
                        regexp: /^\d+$/,
                        message: 'The value is not a valid number'
                    }
                }
            },// Phone Country Code validation END
            // Phone Area Code validation START 
            <%=txtMobileArea.UniqueID%>: {
                validators: {
                    stringLength: {
                        min: 2,
                        max: 5,
                        message: 'Mobile code or area code must be between 2 - 5 numbers long e.g. 01 or 087'
                    },
                    regexp: {
                        regexp: /^\d+$/,
                        message: 'The value is not a valid number'
                    }
                }
            },// Phone Area Code validation END
            // Phone Number validation START 
            <%=txtNumber.UniqueID%>: {
                validators: {
                    regexp: {
                        regexp: /^\d+$/,
                        message: 'The value is not a valid number'
                    }
                }
            },// Phone Number validation END
            // Entities to Disclose validation START 
            <%=txtEntitiesDisclose.UniqueID%>: {
                validators: {
                    notEmpty: {
                        message: 'Entities to disclose is required'
                    },
                }
            },// Entities to Disclose validation END
            // Information to Disclose validation START 
            <%=txtInfoDisclose.UniqueID%>: {
                validators: {
                    notEmpty: {
                        message: 'Information to disclose is required'
                    },
                }
            },// Entities to Disclose validation END
        }
    });
});
</script>

<asp:Panel ID="pnlAMLForm" runat="server" Visible="true" CssClass="pnlAMLForm">
    <div class="container-fluid bootstrap-style">
        <div class="row-fluid" style="margin-top:20px;">
            <form  id="form1" class="form-horizontal" >
            <fieldset>
                <legend class="text-center" style="color:#861F41">Personal information</legend>
                <p>You can choose to tell us a little about yourself. This is not mandatory, you can choose to leave this section unfilled if you do not wish to disclose your details.</p>
                <span style="color: #861F41" runat="server" id="lblErrorMessage" visible="false"></span>
                <asp:Label ID="lblSuccessMessage" Text="added" runat="server" ForeColor="Blue"></asp:Label>
                <br/>
                 <!--  First Name -->
                <div class="form-group row">
                    <label class="col-sm-2 col-form-label" for="txtFirstName">First name</label>
                    <div class="col-sm-10" runat="server" id="holderTxtFirstName">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="fas fa-user"></i></span>                                       
                            <asp:TextBox id="txtFirstName" runat="server" class="form-control txtFirstName" placeholder="First Name" name="txtFirstName"></asp:TextBox>
                        </div>
                        <i class="form-control-feedback glyphicon glyphicon-remove" style="top: 0px; z-index: 100;"></i>
                    </div>
                </div>
                <!--  Last Name -->
                <div class ="form-group row">  
                    <label class="col-sm-2 col-form-label" for="txtLastName">Last name</label>  
                    <div class="col-sm-10" runat="server" id="holderTxtLastName">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="fas fa-user"></i></span>                    
                            <asp:TextBox id="txtLastName" runat="server" class="form-control" placeholder="Last Name" name="txtLastName"></asp:TextBox>
                        </div>
                        <i class="form-control-feedback glyphicon glyphicon-remove" style="top: 0px; z-index: 100;"></i>
                    </div>
                </div>
                <!--  Address -->
                <div class ="form-group row">  
                    <label class="col-sm-2 col-form-label" for="txtAddress">Address</label>  
                    <div class="col-sm-10" runat="server" id="holderTxtAddress">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="fas fa-map-marker-alt"></i></span>                    
                            <asp:TextBox id="txtAddress" runat="server" class="form-control" placeholder="Address" name="txtAddress"></asp:TextBox>
                        </div>
                        <i class="form-control-feedback glyphicon glyphicon-remove" style="top: 0px; z-index: 100;"></i>
                    </div>
                </div>
                <!--  Email -->
                <div class="form-group row">   
                    <label class="col-sm-2 col-form-label" for="txtEmail">Email</label>  
                    <div class="col-sm-10" runat="server" id="holderTxtEmail">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="fas fa-envelope"></i></span>                    
                            <asp:TextBox id="txtEmail" runat="server" class="form-control"  placeholder="Email" name="txtEmail"></asp:TextBox>
                        </div>
                        <i class="form-control-feedback glyphicon glyphicon-remove" style="top: 0px; z-index: 100;"></i>
                    </div>
                </div>
                <!--  Contact Number -->
                <div class="form-group row">
                    <label class="col-sm-2 col-form-label" for="countyrcode">Contact Number</label>
                    <div class="">  
                        <div class="col-xs-3 col-sm-3" runat="server" id="holderTxtCountryCode">
                            <asp:TextBox id="txtCountryCode" runat="server" class="form-control"  placeholder="Country code"  name="countrycode"  ></asp:TextBox>
                            <i class="form-control-feedback glyphicon glyphicon-remove" style="top: 0px; z-index: 100;"></i>
                            <span class="txtbox-help-text">E.g. 353 (for ROI) | 44 (for UK)</span>
                        </div>
                        <div class="col-xs-3 col-sm-3" runat="server" id="holderTxtMobileArea">
                            <asp:TextBox id="txtMobileArea" runat="server" class="form-control"  placeholder="Mobile\area code"  name="mobilearea"  ></asp:TextBox>
                            <i class="form-control-feedback glyphicon glyphicon-remove" style="top: 0px; z-index: 100;"></i>
                            <span class="txtbox-help-text">E.g. 087 for Mob | 01 for Dublin</span>
                        </div>
                        <div class="col-xs-6 col-sm-4" runat="server" id="holderTxtNumber">
                            <asp:TextBox id="txtNumber" runat="server" class="form-control"  placeholder="Phone number"  name="number"  ></asp:TextBox>
                            <i class="form-control-feedback glyphicon glyphicon-remove" style="top: 0px; z-index: 100;"></i>
                        </div>
                    </div>  
                </div>
                <!--  Allow Communication -->
				<!-- PATCH FIX CHECKBOXES #21366 -->
                <div class="form-group row">
                    <div class="col-sm-2"></div>                        
                        <asp:CheckBox ID="chkAllowCommunication" CssClass="checkbox-inline col-xs-12 col-sm-10" runat="server" Checked="false" Text="Please tick this box if you DO NOT wish to be contacted by the Institute to clarify this information." />                               
                </div>

            </fieldset>
            <fieldset>
                <legend class="text-center" style="color:#861F41">Incident information</legend>
                <p>Please provide as much information as you can about the incident you wish to report.</p>
                <!--  Entities to Disclose -->
                <div class="form-group row">   
                    <label class="col-sm-2 col-form-label required" for="txtEntitiesDisclose">Entities to disclose</label>  
                    <div class="col-sm-10" runat="server" id="holderTxtEntitiesDisclose">
                        <div class="input-group">
                            <span class="input-group-addon">
                                <i class="fas fa-portrait"></i><br /><br />
                                <i class="fas fa-building"></i>
                            </span>                
                            <asp:TextBox id="txtEntitiesDisclose" runat="server" class="form-control"  placeholder="Name and address of Firm and/or Member you wish to disclose information about" name="txtEntitiesDisclose" TextMode="multiline" Columns="50" Rows="5" style="resize:vertical;"></asp:TextBox>
                        </div>
                        <i class="form-control-feedback glyphicon glyphicon-remove" style="top: 0px; z-index: 100;"></i>
                    </div>
                </div>
                <!--  Information to Disclose -->
                <div class="form-group row">   
                    <label class="col-sm-2 col-form-label required" for="txtEntitiesDisclose">Information to disclose</label>  
                    <div class="col-sm-10" runat="server" id="holderTxtInfoDisclose">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="fas fa-info"></i></span>                    
                            <asp:TextBox id="txtInfoDisclose" runat="server" class="form-control"  placeholder="Information you want to disclose" name="txtEntitiesDisclose" TextMode="multiline" Columns="50" Rows="5" style="resize:vertical;"></asp:TextBox>
                        </div>
                        <i class="form-control-feedback glyphicon glyphicon-remove" style="top: 0px; z-index: 100;"></i>
                    </div>
                </div>
                <!--  Allow Info Shared -->
				<!-- PATCH FIX CHECKBOXES #21366 -->
                <div class="form-group row">
                    <div class="col-sm-2"></div>                        
                        <asp:CheckBox ID="chkAllowSharedInfo" CssClass="checkbox-inline col-xs-12 col-sm-10" runat="server" Checked="false" Text="Please tick this box if you DO NOT want us to share any evidence or correspondence with the accountancy firm or member." />                               
                </div>
                <!--  Use of Personal Information -->
                <div class="form-group row sfContentBlock" style="margin:0px 0px 15px">                  
                    <div class="col-xs-12" style="padding:0px">
                        <h4>Use and protection of your personal information</h4>
                        <p>The Institute will use the information which you have provided in this form to carry out the Institute’s responsibilities as a regulator and as a professional body and will hold and protect the information in accordance with the Institute’s <a href="https://www.charteredaccountants.ie/Privacy-policy" target="_blank">privacy statement</a>, which explains your rights in relation to your personal data.</p>
                    </div>
                </div>
                <!-- Submit Button -->
                <div class="form-group row">
                    <div class="text-right">
                        <asp:Button ID="btnSave" runat="server" Text="Submit" CssClass="submitBtn" OnClick="btnSave_Click" />
                    </div>
                </div>
            </fieldset>
            </form>
        </div>
    </div>
</asp:Panel>
