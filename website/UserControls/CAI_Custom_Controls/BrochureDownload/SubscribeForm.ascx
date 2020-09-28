<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubscribeForm.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.BrochureDownload.SubscribeForm" %>

<!-- Bootstrap CSS -->
 <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet"/>
<%-- <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.2/css/bootstrap-select.min.css" />--%>
<%--<link rel="stylesheet" href="//cdn.jsdelivr.net/fontawesome/4.1.0/css/font-awesome.min.css" />--%>

<!-- BootstrapValidator CSS -->
    <link rel="stylesheet" href="//cdn.jsdelivr.net/jquery.bootstrapvalidator/0.5.0/css/bootstrapValidator.min.css"/>

<!-- jQuery and Bootstrap JS -->
<%--<script type="text/javascript" src="//cdn.jsdelivr.net/jquery/1.11.1/jquery.min.js"></script>--%>
<%--    <script type="text/javascript" src="//cdn.jsdelivr.net/bootstrap/3.2.0/js/bootstrap.min.js"></script>--%>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
   <%-- <script src="https://cdnjs.cloudflare.com/ajax/libs/1000hz-bootstrap-validator/0.11.5/validator.min.js"></script>--%>

 <!-- BootstrapValidator JS -->
    <script type="text/javascript" src="//cdn.jsdelivr.net/jquery.bootstrapvalidator/0.5.0/js/bootstrapValidator.min.js"></script>
 <%--   <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.2/js/bootstrap-select.min.js"></script>--%>


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
                     //ctl00$ctl00$baseTemplatePlaceholder$content$C001$fname: {
                     <%=fname.UniqueID%>: {
                         validators: {
                             notEmpty: {
                                 message: 'First name is required'
                             },

                             regexp: {
                                 regexp: /^[a-zA-Z\s]+$/,
                                 message: 'The first name must contain only alphabetic characters'
                             },

                         }
                     },// End of first name validation
  
                     //ctl00$ctl00$baseTemplatePlaceholder$content$C001$lname: {
                     <%=lname.UniqueID%>: {
                         validators: {
                             notEmpty: {
                                 message: 'Last name is required'
                             },

                             regexp: {
                                 regexp: /^[a-zA-Z\s]+$/,
                                 message: 'The Last name must contain only alphabetic characters'
                             }

                         }
                     },// End of last name validation

                     <%=email.UniqueID%>: {
                         validators: {
                             notEmpty: {
                                 message: 'Email is required'
                             },

                             regexp: {
                                 regexp: '^[^@\\s]+@([^@\\s]+\\.)+[^@\\s]+$',
                                 message: 'Not a valid email address'
                             }

                         }
                     },// End of email validation
                      
                     
                     <%=countyrcode.UniqueID%>: {
                         validators: {
                             notEmpty: {
                                 message: 'Country code is required and cannot be empty'
                             },

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
                     },// End of country code validation

                     <%=mobilearea.UniqueID%>: {
                         validators: {
                             notEmpty: {
                                 message: 'Country code is required and cannot be empty'
                             },

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
                     },// Mobile\Area code validation

                     <%=number.UniqueID%>: {
                         validators: {
                             notEmpty: {
                                 message: 'Mobile number is required and cannot be empty'
                             },

                             regexp: {
                                 regexp: /^\d+$/,
                                 message: 'The value is not a valid number'
                             }

                         }

                     },// number code validation end

                     <%=ddi.UniqueID%>: {
                         validators: {
                             callback: {
                                 message: 'Select area of interest',
                                 callback: function(value, validator, $field) {
                                     /* Get the selected options */
                                     var options = validator.getFieldElements('<%=ddi.UniqueID%>').val();                                  
                                     return (options != null && options.length > 0);
                                 }
                                                
                             }
                         }// dropdown code validation end




                     }
                 }
             });

            

         });
    </script>
<%--begin thank you jumbotron--%>
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
</style>

<%--end thank you jumbotron--%>
<div class="container-fluid bootstrap-style" id ="f1">
        <div class="row-fluid" >
                    <form  id="form1"  class="form-horizontal" >
                      <fieldset>
                            <legend class="text-center" style="color:#861F41">TO CONTINUE WITH THE DOWNLOAD PLEASE PROVIDE THE FOLLOWING INFORMATION</legend>
                         <!--  First Name -->
                            <div class="form-group row">
                                 <label class="col-sm-2 col-form-label required" for="fname"> First name</label>
                                  <div class="col-sm-10">
                                      <div class="input-group">
                                                <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>                                       
                                                <asp:TextBox id="fname" runat="server" class="form-control " placeholder="First name" name="fname"></asp:TextBox>
                                          </div>
                                  </div>
                            </div>
                        <!--  Last Name -->
                            <div class ="form-group row">  
                                <label class="col-sm-2 col-form-label required" for="lname"> Last name</label>  
                                 <div class="col-sm-10">
                                     <div class="input-group">
                                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>                    
                                            <asp:TextBox id="lname" runat="server" class="form-control" placeholder="Last name" name="lname"></asp:TextBox>
                                         </div>
                                 </div>
                            </div>
                    
                         <!--  Email -->
                            <div class="form-group row">   
                                <label class="col-sm-2 col-form-label required" for="email"> Email</label>  
                                    <div class="col-sm-10">
                                        <div class="input-group">
                                                    <span class="input-group-addon"><i class="glyphicon glyphicon-envelope"></i></span>                    
                                         <%-- <input type="email" class="form-control" id="inputEmail" placeholder="Email" required runat="server" onchange="UserOrEmailAvailability()"/>--%>
                                        <asp:TextBox id="email" runat="server" class="form-control"  placeholder="Email" name="email"  ></asp:TextBox>
                                            </div>
                                  </div>
                               </div>
                         <!--  Contact Number -->
                            <div class="form-group row">
                                <label class="col-sm-2 col-form-label required" for="countyrcode">Contact number</label>
                                 <div class="">  
                                     <div class="col-xs-4 col-sm-3">
                                          <asp:TextBox id="countyrcode" runat="server" class="form-control"  placeholder="Country code"  name="countrycode"  ></asp:TextBox>
                                     </div>
                                    <div class="col-xs-4 col-sm-3">
                                          <asp:TextBox id="mobilearea" runat="server" class="form-control"  placeholder="Mobile/area code"  name="mobilearea" ></asp:TextBox>
                                    </div>
                                    <div class="col-xs-4 col-sm-4">
                                           <asp:TextBox id="number" runat="server" class="form-control"  placeholder="Phone number"  name="number"  MaxLength="8"></asp:TextBox>
                                     </div>
                                  </div>  
                            </div>



                        <!--  Drop down Area of Interest -->
                    <div class="form-group row">

                        <label class="col-sm-2 col-form-label required" for="ddi">Which of the following best describes you?</label>
                        <div class="col-sm-10"> 
                          <div class="input-group">
                          <span class="input-group-addon"><i class="glyphicon glyphicon-star"></i></span>  
                            <asp:DropDownList ID="ddi"  runat="server" class ="form-control"  name="ddi"  >                           
                                <asp:ListItem  Enabled="true" Text="Please select one that best describes you" Value=""></asp:ListItem>                                               
                                <asp:ListItem Text="Interested in the Flexible Route " Value="1" ></asp:ListItem>
                                <asp:ListItem Text="Going into a training contract" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Accounting technician student/member" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Researching for a relative or friend" Value="4"></asp:ListItem>      
                                <asp:ListItem Text="Academic" Value="5"></asp:ListItem>
                                <asp:ListItem Text="Employer" Value="6"></asp:ListItem>
                                <asp:ListItem Text="International student" Value="7"></asp:ListItem>           
                               </asp:DropDownList>
                                                                        </div>
                            </div>
                     </div>

					<!-- PATCH FIX CHECKBOXES #21366 -->
                    <div class="form-group row">    
                            <div class="col-sm-2"></div>                        
                                  <asp:CheckBox ID="cb1" CssClass="checkbox-inline col-xs-12 col-sm-10" runat="server" Checked="false" Text=" Please tick this box if you'd like to receive communications from us about how you can study and train to become a Chartered Accountant for the upcoming intake" />
                      </div>
                    <div class="form-group row">

                        <div class="text-right">
                            <asp:Button ID="btnSave" runat="server" Text="Download brochure" class="submitBtn"  OnClick="btnSave_Click" />
                        </div>
                    </div>
                  </fieldset>
                </form>


            </div>
    </div>



