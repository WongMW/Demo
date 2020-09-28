<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactForm.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.ContactForm.ContactForm" %>



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
<!-- Bootstrap max length Validator JS -->
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-maxlength/1.7.0/bootstrap-maxlength.min.js"></script>





     <script type="text/javascript">  
      
         function  ShowDiv() {

                 $("#f1").hide();
                 $("#jt2").show();

         };



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
					 			message: 'Select Area of Interest',
					 			callback: function(value, validator, $field) {
					 				/* Get the selected options */
					 				var options = validator.getFieldElements('<%=ddi.UniqueID%>').val();                                  
					 				return (options != null && options.length > 0);
					 			       }
                                                
					 				}
					 	          }
											 },// dropdown code validation end

                     <%=txtArea.UniqueID%>: {
                         validators: {
                             notEmpty: {
                                 message: 'The Message is required and cannot be empty'
                             },
                         	stringLength: {
         							min: 1,
         							max: 1000,
         							message: '1000 characters max'
                         	}
                         }// txtArea code validation end




                     }
                 }
             });

         	//multiline text box area max length your query
         	$("#<%=txtArea.ClientID%>").attr("maxlength", 1000);        
         	var maxLength = 1000;
         	$("#<%=txtArea.ClientID%>").keyup(function() {
         		var length = $(this).val().length;
         		var length = maxLength-length;
         		$('#chars').text(length);
         		
         	});

         });
    </script>  
<style>
    .icon-circle .fa{
    font-size           : 25px;
    color               : #e84700;
    margin              : 0 auto;
    height              : 80px;
    width               : 80px;
    border-radius       : 50%;
    border              :2px solid #e84700;
    line-height         : 80px;
    cursor              : pointer;
    -webkit-transition  : all  ease-in-out 0.35s;
    -moz-transition     : all  ease-in-out 0.35s;
    -o-transition       : all  ease-in-out 0.35s;
    -ms-transition      : all  ease-in-out 0.35s;
    transition          : all  ease-in-out 0.20s,background-color ease-in-out 0.05s;
}

.icon-circle  .fa:hover{
    
    background-color        : #e84700;    
    color                   : #fff;
    border                  : 1px solid #e84711;
    -moz-box-shadow         : inset 0px 0px 0px 5px #ffffff;
    -o-box-shadow           : inset 0px 0px 0px 5px #ffffff;
    -ms-box-shadow          : inset 0px 0px 0px 5px #ffffff;
    -webkit-box-shadow      : inset 0px 0px 0px 5px #ffffff;
    box-shadow              : inset 0px 0px 0px 5px #ffffff;
    -ms-transform           : scale(1.2,1.2); 
    -webkit-transform       : scale(1.2,1.2);
    -moz-transform          : scale(1.2,1.2); 
    -o-transform            : scale(1.2,1.2); 
    transform               : scale(1.2,1.2);  
}

.icon-circle  i:before{
    margin-left         : 0px;
    font-size           : 40px;
}

@media (min-width:320px) and (max-width:768px) {


 .icon-circle .fa{
    font-size           : 15px;
    color               : #e84700;
    margin              : 0 auto;
    height              : 40px;
    width               : 40px;
    border-radius       : 50%;
    border              :2px solid #e84700;
    line-height         : 40px;
    cursor              : pointer;
    -webkit-transition  : all  ease-in-out 0.35s;
    -moz-transition     : all  ease-in-out 0.35s;
    -o-transition       : all  ease-in-out 0.35s;
    -ms-transition      : all  ease-in-out 0.35s;
    transition          : all  ease-in-out 0.20s,background-color ease-in-out 0.05s;
}

.icon-circle  .fa:hover{
    
    background-color        : #e84700;    
    color                   : #fff;
    border                  : 1px solid #e84711;
    -moz-box-shadow         : inset 0px 0px 0px 5px #ffffff;
    -o-box-shadow           : inset 0px 0px 0px 5px #ffffff;
    -ms-box-shadow          : inset 0px 0px 0px 5px #ffffff;
    -webkit-box-shadow      : inset 0px 0px 0px 5px #ffffff;
    box-shadow              : inset 0px 0px 0px 5px #ffffff;
    -ms-transform           : scale(1.2,1.2); 
    -webkit-transform       : scale(1.2,1.2);
    -moz-transform          : scale(1.2,1.2); 
    -o-transform            : scale(1.2,1.2); 
    transform               : scale(1.2,1.2);  
}

.icon-circle  i:before{
    margin-left         : 0px;
    font-size           : 20px;
}   
}

.ifacebook .fa{
    color               :#3B5998;
    border              :2px solid #3B5998;
}

.ifacebook .fa:hover{
    
    background-color        : #3B5998;    
    color                   : #fff;
    border                  : 1px solid #3B5998;

}

.itwittter .fa{
    color               : #33ccff;
    border              :2px solid #33ccff;
}

.itwittter .fa:hover{
    
    background-color        : #33ccff;    
    color                   : #fff;
    border                  : 1px solid #33ccff;

}

.iFlickr .fa{
    color               : #ff0281;
    border              :2px solid #ff0281;
}

.iFlickr .fa:hover{
    
    background-color        : #ff0281;    
    color                   : #fff;
    border                  : 1px solid #ff0281;

}

.iLinkedin .fa{
    color               : #007bb7;
    border              :2px solid #007bb7;
}

.iLinkedin .fa:hover{
    
    background-color        :#007bb7;    
    color                   : #fff;
    border                  : 1px solid #007bb7;

}
<!-- ADDING STYLES OVERWRITTEN BY BOOTSTRAP STYLES -->
.jumbotron {
    background-color:transparent !important; 
}

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
<%--begin thank you jumbotron--%>
<div class="jumbotron text-xs-center bootstrap-style" id="jt2" style="display:none" >
  <h1 class="display-3">Thank You!</h1>
  <p><strong>Thank you for your query, we'll be in touch soon.</strong></p>
  <hr>
    <p>Have a minute ? Help us share the love! Follow us on Twitter and like us on Facebook to keep you up to date with all our news and announcements.</p>
  <div class="container-fluid">
    <div class="row ">
   
		<div class="sicon">

			<div class="col-lg-2 col-md-2 col-sm-2 col-xs-3 text-center">
				<div class="icon-circle">
					<a href="https://www.facebook.com/CharteredAccountantsIreland" class="ifacebook" title="Facebook" target="_blank"><i class="fa fa-facebook"></i></a>
				</div>
			</div>
     
			<div class="col-lg-2 col-md-2 col-sm-2 col-xs-3 text-center">
				<div class="icon-circle">
					<a href="https://twitter.com/charteredaccirl" class="itwittter" title="Twitter" target="_blank"><i class="fa fa-twitter"></i></a>
				</div>
			</div>
      
			<div class="col-lg-2 col-md-2 col-sm-2 col-xs-3 text-center">
				<div class="icon-circle">
					<a href="https://www.flickr.com/photos/irishcharteredaccountants" class="iFlickr" title="Flickr" target="_blank"><i class="fa fa-flickr"></i></a>
				</div>
			</div>
      
			<div class="col-lg-2 col-md-2 col-sm-2 col-xs-3 text-center">
				<div class="icon-circle">
					<a href="https://www.linkedin.com/groups/1783368/profile" class="iLinkedin" title="Linkedin" target="_blank"><i class="fa fa-linkedin"></i></a>
				</div>
			</div>

		</div>

	</div>
</div> 
      <hr>
</div>

<div class="jumbotron text-xs-center" id="jt1" " style="display:none" >
  <hr>
  
  <h1 class="display-3">Thank You!</h1>
  <p class="lead"><strong>Please check downloaded Chartered Accountants brochure your</strong> for further instructions on how to complete your account setup.</p>
  <hr>
  <p>
    Having trouble? <a href="">Contact us</a>
  </p>
  <p class="lead">
    <a class="btn btn-primary btn-sm" href="https://www.charteredaccountants.ie/" role="button">Continue to homepage</a>
  </p>
</div>

<%--end thank you jumbotron--%>
<div class="container-fluid bootstrap-style" id ="f1">
        <div class="row-fluid" >
                    <form  id="form1"  class="form-horizontal" >
                      <fieldset>
                            <legend class="text-center" style="color:#861F41">Contact Form </legend>
                             <%--<p>Please tell us a little about yourself by filling out the form below. Once you click submit you will be sent to the exemption listing page this will give you an indication of what your starting point may be.</p>--%>
                         <!--  First Name -->
                            <div class="form-group row">
                                 <label class="col-sm-2 col-form-label required" for="fname"> First Name</label>
                                  <div class="col-sm-10">
                                      <div class="input-group">
                                                <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>                                       
                                                <asp:TextBox id="fname" runat="server" class="form-control " placeholder="First Name" name="fname"></asp:TextBox>
                                          </div>
                                  </div>
                            </div>
                        <!--  Last Name -->
                            <div class ="form-group row">  
                                <label class="col-sm-2 col-form-label required" for="lname"> Last Name</label>  
                                 <div class="col-sm-10">
                                     <div class="input-group">
                                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>                    
                                            <asp:TextBox id="lname" runat="server" class="form-control" placeholder="Last Name" name="lname"></asp:TextBox>
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
                                <label class="col-sm-2 col-form-label required" for="countyrcode">Contact Number</label>
                                 <div class="">  
                                     <div class="col-xs-4 col-sm-3">
                                          <asp:TextBox id="countyrcode" runat="server" class="form-control"  placeholder="Country code"  name="countrycode"  ></asp:TextBox>
                                     </div>
                                    <div class="col-xs-4 col-sm-3">
                                          <asp:TextBox id="mobilearea" runat="server" class="form-control"  placeholder="Mobile\area code"  name="mobilearea"  ></asp:TextBox>
                                    </div>
                                    <div class="col-xs-4 col-sm-4">
                                           <asp:TextBox id="number" runat="server" class="form-control"  placeholder="Phone number"  name="number"  ></asp:TextBox>
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



                    <div class="form-group row">

                        <label class="col-sm-2 col-form-label required" for="txtArea">Your query  </label>
                        <div class="col-md-10"> 
                          <div class="">
                              <asp:textbox id="txtArea" runat="server" maxlength="1000" class="form-control" placeholder="Message goes here (1000 characters max)"  TextMode="multiline" rows="10" name="txtArea"></asp:textbox>							  
                           </div>
							<span id="chars">1000</span> characters remaining
                         </div>
                     </div>

					<!-- PATCH FIX CHECKBOXES #21366 -->
                    <div class="form-group row">    
                            <div class="col-sm-2"></div>                        
                                  <asp:CheckBox ID="cb1" CssClass="checkbox-inline col-xs-12 col-sm-10" runat="server" Checked="false" Text=" Please tick this box if you'd like to receive communications from us about how you can study and train to become a Chartered Accountant for the upcoming intake" />
                      </div>
                    <div class="form-group row">

                        <div class="text-right">
                            <asp:Button ID="btnSave" runat="server" Text="Submit" class="submitBtn"   OnClick="btnSave_Click" />
                        </div>
                    </div>
                  </fieldset>
                </form>


            </div>
    </div>





