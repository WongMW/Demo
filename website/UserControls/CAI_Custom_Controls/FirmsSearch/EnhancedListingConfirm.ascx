<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EnhancedListingConfirm.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.FirmsSearch.EnhancedListingConfirm" %>
<%@ Register TagPrefix="uc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="uc1" TagName="PaymentStep" Src="~/UserControls/CAI_Custom_Controls/FirmsSearch/EnhancedListingPaymentStep.ascx" %>


<%-- bootstrap  --%>
<!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous"/>
<!-- Optional theme -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css" integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp" crossorigin="anonymous"/>
<!-- Override bootstrap css for corporate styles -->
<link href="../../../CSS/bootstrap-override.min.css" rel="stylesheet" />
<!-- Latest compiled and minified JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>

<!-- bootstrap multiselect -->
<link href="https://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
<script src="https://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js" type="text/javascript"></script>
<style>
	.modal-header-success {
		color: #fff;
		padding: 9px 15px;
		border-bottom: 1px solid #eee;
		background-color: #5cb85c;
		-webkit-border-top-left-radius: 5px;
		-webkit-border-top-right-radius: 5px;
		-moz-border-radius-topleft: 5px;
		-moz-border-radius-topright: 5px;
		border-top-left-radius: 5px;
		border-top-right-radius: 5px;
	}
	 .thank-you-pop h1{
	font-size: 42px;
    margin-bottom: 25px;
	color:#5C5C5C;
}
.thank-you-pop p{
	font-size: 20px;
    margin-bottom: 27px;
 	color:#5C5C5C;
}
th{
    font-weight: bold;
}

</style>
<!-- User with firm details -->
<div class="form-section-full-border no-margin">
    <h2 class="no-margin">User details:</h2>
    <div class="form-section-half-border">
        <asp:Label runat="server" ID="Label1" class="main-label-title">User name:</asp:Label>
        <asp:Label ID="lName" runat="server" class="main-label-data"></asp:Label>
    </div>
    <div class="form-section-half-border no-border">
        <asp:Label runat="server" ID="Label2" class="main-label-title">User firm:</asp:Label>
        <asp:Label ID="lFirmName" runat="server" class="main-label-data"></asp:Label>
    </div>
</div>
<hr />
<!-- List of firms for editing -->
<div class="form-section-full-border no-margin">
    <h2 class="no-margin">Edit your listings</h2>
    <p style="font-size:20px!important">Remember to save all your changes and when you're ready <a href="#submitnow"><strong>submit your edits</strong></a> for approval.<br />
        Having problem editing your firm details? Please refer to our premium listing <a href="/PremiumListing/Help" target="_blank"><strong><i class="far fa-question-circle"></i> Help</strong></a> page.
    </p>

</div>          <div id="myR3">
				<asp:Repeater ID ="R3" runat="server" OnItemDataBound="R3_ItemDataBound"  >
					<ItemTemplate>
						<div class="panel-group" id="accordion">							
							<div class="panel parent-panel">
								<%-- PARENT FIRMS START --%>
								<div class="panel-heading">
                                    <div class="col-xs-11">
                                        <asp:CheckBox ID="pfcb" runat="server" CssClass="checkbox-align" Text='<%# Eval("fname") %>'></asp:CheckBox>
                                        <asp:Label  ID="fis" Text="" runat="server" CssClass="fd-edit-status"></asp:Label>
                                    </div>
                                    <asp:HiddenField runat="server" ID="pid" value='<%# Eval("ID") %>' />
                                    <asp:HiddenField runat="server" ID="pfId" value='<%# Eval("fid") %>' />
                                    <div class="trigger pull-right col-xs-1">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#collapse<%# Eval("fid") %>" title="Click to edit this firm"><i class="fas fa-edit"></i></a>
                                    </div>
                                </div>
                                <div  id="collapse<%# Eval("fid") %>" class="panel-collapse collapse">
                                    <div class="panel-body">
                                        <asp:Panel ID="ps" runat="server" Visible=" false" CssClass="alert alert-success text-center" >
                                            <div class="text-center">
                                                <strong>Success!</strong> Firm details are saved and ready to submit for approval.
                                            </div>
                                        </asp:Panel>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <span class="label-title">No. of employees<span class="required"></span></span>
												<asp:DropDownList ID="ddpen"  runat="server" class ="ddpen form-control"  name="ddpen" DataTextField="name"  DataValueField="id" >                           
													<asp:ListItem  Enabled="true" Text="No of employees" Value=""></asp:ListItem>                                               
													<asp:ListItem Text="Self-employed" Value="1" ></asp:ListItem>
													<asp:ListItem Text="1-10 employees" Value="2"></asp:ListItem>
													<asp:ListItem Text="11-50 employees" Value="3"></asp:ListItem>
													<asp:ListItem Text="51-200 employees" Value="4"></asp:ListItem>      
													<asp:ListItem Text="201-500 employees" Value="5"></asp:ListItem>
													<asp:ListItem Text="501-1000 employees" Value="6"></asp:ListItem>
													<asp:ListItem Text="1001-5000 employees" Value="7"></asp:ListItem>
													<asp:ListItem Text="5001-10000 employees" Value="8"></asp:ListItem>
													<asp:ListItem Text="10000-15000 employees" Value="9"></asp:ListItem>              
												</asp:DropDownList>
                                                <asp:CustomValidator  runat="server" ID="cvp1" ControlToValidate="ddpen" Display="Dynamic"  SetFocusOnError="true" ForeColor="Red"  Text="*Select no of employees" ValidateEmptyText="true"></asp:CustomValidator>
                                            </div>
											<div class="col-sm-6">
												<span class="label-title">No. of partners<span class="required"></span></span>
												<asp:TextBox ID="tbpnop" runat="server" width="100%"></asp:TextBox>
												<asp:CustomValidator  runat="server" ID="cvp2" ControlToValidate="tbpnop" Display="Dynamic"  SetFocusOnError="true" ForeColor="Red"  Text="*Enter no of partners" ValidateEmptyText="true"></asp:CustomValidator>
												<asp:RegularExpressionValidator ID="Re1" ControlToValidate="tbpnop" runat="server"  ForeColor="Red" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"></asp:RegularExpressionValidator>
											</div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <span class="label-title">Firm description<span class="required"></span> (350 characters max)</span>
                                                <asp:TextBox ID="tbpdes" runat="server" TextMode="MultiLine" maxlenght="350" width="100%" height="142px" Style="resize:none;" CssClass="char-max-350"></asp:TextBox>
                                                <div class="char-count"><span class="chars-num" style="font-weight:bold;">350</span> characters remaining</div>
                                                <asp:CustomValidator runat="server" ID="cvp3" ControlToValidate="tbpdes" Display="Dynamic" SetFocusOnError="true" ForeColor="Red" ValidateEmptyText="true" Text="*Firm description cannot be empty"></asp:CustomValidator>
                                            </div>
														<div class="col-sm-6">
														   <span class="label-title">Firm logo<span class="required"></span> <a href="/PremiumListing/Help#HelpLogo" target="_blank" style="float: right;"><i class="far fa-question-circle"></i> Help</a></span>
															<div class="col-md-8 upload-div">
																
																<asp:FileUpload ID="fu" runat="server" />
																<asp:RequiredFieldValidator ID="furf" runat="server" Display="Dynamic" ErrorMessage="*Firm logo required" ControlToValidate="fu" ValidationGroup="g1" ForeColor="Red" ></asp:RequiredFieldValidator>
																<asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="fu" Display="Dynamic" runat="server"  ForeColor="Red" ErrorMessage="Invalid File Type/Extension"  ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif|.jpg|.JPG)$"></asp:RegularExpressionValidator>
																
													 
															    <asp:LinkButton ID="Bfu" CssClass="btn btn-danger" runat="server" Text="Upload" OnCommand="btnSave_Command"  Visible="false" CommandName="upload" CommandArgument='<%# Eval("fid") %>'></asp:LinkButton>								
																<br />																
															    <asp:Label ID ="ful" runat="server" ForeColor ="Red"></asp:Label>
																<asp:CustomValidator runat ="server" ID ="cvp4" ControlToValidate="fu" Display="Dynamic" SetFocusOnError="true" ForeColor="Red"></asp:CustomValidator>

															</div>
															<div class="col-md-4 preview-div">
																<asp:Image ID="ImgPrv" Height="150px" Width="150px" runat="server"   alt="" class="ImgPrv"  /><br />
														   </div>
													   </div>
													</div>
												    <div class="row">
														 
													   <div class="col-sm-6 firms-ddl">									   												  												   
															<span class="label-title">Specialisms<span class="required"></span> (select up to 3)</span>
                                                            <p class="info-error hide-it">You can only select a <strong>max of 3 specialisms</strong></p>
															<asp:ListBox ID="lbpspec" runat="server" SelectionMode="Multiple"  CssClass="msel" DataTextField="name" DataValueField="ID">																
															</asp:ListBox>
														   <br />
														   <asp:CustomValidator ID="cvp5" runat="server" Text ="Select upto 3 specialisms" ForeColor="Red" ControlToValidate="lbpspec"  ValidateEmptyText="true" ></asp:CustomValidator>
													   </div>
														<div class="col-sm-6 firms-ddl">
															 <span class="label-title">Industry sectors<span class="required"></span> (select up to 3)</span>
                                                             <p class="info-error hide-it">You can only select a <strong>max of 3 specialisms</strong></p>												
														   <asp:ListBox ID="lbpis" runat="server" SelectionMode="Multiple" CssClass="msel"  DataTextField="name" DataValueField="ID" >
															</asp:ListBox>
														   <br />
															<asp:CustomValidator ID="cvp6" runat="server" Text ="Select upto 3 Industry sectors" ForeColor="red" ControlToValidate="lbpis"  ValidateEmptyText="true"></asp:CustomValidator>
													   </div>
													</div>
												    <div class="row">
														<div>
															<span class="label-title col-xs-12">Google maps location (optional)</span>
                                                            <div class="col-sm-6">
                                                                <p class="no-margin">Need some <strong><a href="/PremiumListing/Help#HelpGooglemaps" target="_blank"><i class="far fa-question-circle"></i> Help</a></strong></p>
															    <ol style="margin-left:20px">
																    <li>Go to <a href="https://www.google.com/maps" target="_blank">Google maps</a></li>
																    <li>Enter the name of your company / company address into Google Maps search box and click the <i class="fas fa-search"></i> "Search" button</li>
																    <li>Click on the <i class="fas fa-share-alt"></i> "Share" button</li>
																    <li>Click on "Copy link" and paste into the provided textbox here</li>
															    </ol>
                                                            </div>
															<div class="form-group col-sm-6">
																<p style="margin-bottom:5px;font-weight:bold">This firm's location is: <a href="/PremiumListing/Help#HelpGooglemaps" target="_blank" style="float: right;"><i class="far fa-question-circle"></i> Help</a></p>
																<asp:TextBox ID="tblocgmap" class="tblocgmap form-control" placeholder="Copy and paste Google Map link here" runat="server" width="100%" ></asp:TextBox>
																 <span class="txtbox-help-text">Copy and paste Google Map link here</span>
															</div>
														</div>
													       </div>
														

												

													
						                         	<div class="row">
														<div class="form-group" style="width:auto;text-align:center" >
															<asp:LinkButton ID="btnSave" CssClass="cai-btn cai-btn-red-inverse"  runat="server" Text="Save edits" OnCommand="btnSave_Command"   CommandName="btnSave" CommandArgument='<%# Eval("fid") %>'></asp:LinkButton>
															
													   </div>
													</div>
						                  </div>
										</div>
							    
					            <%-- PARENT FIRMS END --%>

            					    <%-- SUB OFFICES START --%>

					
					
					
						 <asp:Repeater ID="R4" runat="server" OnItemDataBound="R4_ItemDataBound">
							<ItemTemplate>
                                <div class="panel child-panel">
                                    <div class="panel-heading">
                                        <div class="col-xs-11">
                                            <asp:CheckBox ID="sfcb" runat="server" CssClass="checkbox-align" Text='<%# Eval("fname") %>'></asp:CheckBox>
                                            <asp:Label  ID="sis" Text=""  runat="server" CssClass="fd-edit-status"></asp:Label>
                                        </div>
                                        <asp:HiddenField runat="server" ID="sid" value='<%# Eval("ID") %>' />
                                        <asp:HiddenField runat="server" ID="sfId" value='<%# Eval("fid") %>' />
                                        <div class="trigger pull-right col-xs-1">
                                            <a data-toggle="collapse" href="#collapse<%# Eval("fid") %>"title="Click to edit this firm"><i class="fas fa-edit"></i></a>
                                        </div>
                                    </div>
												 <div  class="panel-collapse collapse" id="collapse<%# Eval("fid") %>">
													<div class="panel-body">
														<asp:Panel ID="pss" runat="server" Visible=" false" CssClass="alert alert-success text-center" >
															<%--<a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
																<asp:Label ID="labelMessage" runat="server" />--%>
														 <strong>Success!</strong> Firm details are  saved and ready to submit for approval. 
														</asp:Panel>
                                                    												    <div class="row">
													   <div class="col-sm-6 firms-ddl">									   												  												   
															<span class="label-title">Specialisms<span class="required"></span> (select up to 3)</span>
                                                            <p class="info-error hide-it">You can only select a <strong>max of 3 specialisms</strong></p>
															<asp:ListBox ID="lbsspec" runat="server" SelectionMode="Multiple"  CssClass="msel" DataTextField="name" DataValueField="id">
															</asp:ListBox>
														   <br />
														   <asp:CustomValidator ID="cvs1" runat="server" Text ="Select upto 3 specialisms" ForeColor="Red" ControlToValidate="lbsspec"  ValidateEmptyText="true" ></asp:CustomValidator>
													   </div>
														<div class="col-sm-6 firms-ddl">
															 <span class="label-title">Industry sectors<span class="required"></span> (select up to 3)</span>
                                                             <p class="info-error hide-it">You can only select a <strong>max of 3 sectors</strong></p>												
														   <asp:ListBox ID="lbsis" runat="server" SelectionMode="Multiple" CssClass="msel"  DataTextField="name" DataValueField="id" >							
															</asp:ListBox>
														   <br />
															<asp:CustomValidator ID="cvs2" runat="server" Text ="Select upto 3 Industry sectors" ForeColor="red" ControlToValidate="lbsis"  ValidateEmptyText="true"></asp:CustomValidator>
													   </div>
													</div>
												    <div class="row">
														<div>
                                                            <span class="label-title col-xs-12">Google maps location (optional)</span>
                                                            <div class="col-sm-6">
                                                                <p class="no-margin">Need some <strong><a href="/PremiumListing/Help#HelpGooglemaps" target="_blank"><i class="far fa-question-circle"></i> Help</a></strong></p>
															    <ol style="margin-left:20px">
																    <li>Go to <a href="https://www.google.com/maps" target="_blank">Google maps</a></li>
																    <li>Enter the name of your company / company address into Google Maps search box and click the <i class="fas fa-search"></i> "Search" button</li>
																    <li>Click on the <i class="fas fa-share-alt"></i> "Share" button</li>
																    <li>Click on "Copy link" and paste into the provided textbox here</li>
															    </ol>
                                                            </div>
															<div class="form-group col-sm-6">
																<p style="margin-bottom:5px;font-weight:bold">This firm's location is: <a href="/PremiumListing/Help#HelpGooglemaps" target="_blank" style="float: right;"><i class="far fa-question-circle"></i> Help</a></p>
																<asp:TextBox ID="tblocgmaps" class="tblocgmap form-control" placeholder="Copy and paste Google Map link here" runat="server" width="100%" ></asp:TextBox>
																 <span class="txtbox-help-text">Copy and paste Google Map link here</span>
															</div>
														</div>
													</div>
														<div class="row">
														<div class="form-group" style="width:auto;text-align:center" >
															<asp:LinkButton ID="btnSavesub" CssClass="cai-btn cai-btn-red-inverse"  runat="server" Text="Save edits" OnCommand="btnSavesub_Command"   CommandName="btnSave" CommandArgument='<%# Eval("fid") %>'></asp:LinkButton>
															
													   </div>
													</div>

													</div>
												</div>
									  </div>
							</ItemTemplate>
                  </asp:Repeater>

						<%-- SUB OFFICES END --%>
						 
										</div>
								</div>
					</ItemTemplate>

				</asp:Repeater>
				</div>
                <div class="panel-group" id="submitnow">
                    <p style="font-size:20px!important">Saved all changes for your listings? <strong>Submit your changes</strong> for approval when you're ready.<br />
                        <strong>Once changes are submitted they can NOT be changed/edited</strong> so please ensure all details are accurate and correct before submitting.</p>
                    <p class="info-note" style="font-size:18px!important;margin-top:20px">All listings <Strong>must be reviewed & approved by a member of staff</Strong>. Once approved, you’ll receive a confirmation email and the premium listing will show on the website. The premium listing will then remain on the site for the next <strong>365 days</strong>.</p>
                     <div class="center-block">
                         <asp:Button ID="SubmitApproval" runat="server" CssClass="cai-btn cai-btn-red center-block" Text="Submit for approval" OnClick="SubmitApproval_Click" />
                     </div>
                </div>

<!-- Submit confirm   Modal -->
  <div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog modal-lg">
      <div class="modal-content">
        <div class="modal-header modal-header-success"">
         <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h1 class="text-center"><i class="glyphicon glyphicon-ok-circle" ></i> Submission confirmation </h1>
        </div>
        <div class="modal-body">
			<div class="thank-you-pop">
				<h1 class="text-center">Thank You!</h1>
				<p class="text-center"><strong>Your submission is received for premium listing to Chartered Accountants Ireland.</strong> Once this has been approved we will send you another email confirming the date the subscription has started from. <br /></p>
  <hr>
  <p class="text-center" >
    Having trouble?  Contact us: <a href="mailto:premiumlisting@charteredaccountants.ie">premiumlisting@charteredaccountants.ie</a>
  </p>
			<div class="thank-you-pop">		
			 <table class="table">
				<thead>
				  <tr>
					<th class="font-weight-bold">Submitted Firm deatils</th>			
				  </tr>
				</thead>								
			<tbody>
				  <% foreach (FirmDetails sf in sflist) { %> <!-- loop through the list -->		
			<tr>
				<td>
			  <span class="glyphicon glyphicon-home" aria-hidden="true"></span> <%= sf.Fname  %> <!-- write out the name of the site -->	
					</td>			
			</tr>
				<% } %>
			   </tbody>
				
			
			</table>
        </div>
        <div class="modal-footer">
          <button type="button" class="cai-btn cai-btn-red"" data-dismiss="modal">Close</button>
        </div>
      </div>
			</div>
        </div>
	  </div>
	  </div>
					  	
				

        
<uc1:PaymentStep runat="server" id="PaymentStep" Visible="false"></uc1:PaymentStep>
<uc2:User id="User1" runat="server" />

<script>
    //Susan Wong, Max character red text warning display
    var maxLength = 350;
    $('.char-max-350').each(function () {
        $(this).keyup(function () {
            var length = $(this).val().length;
            var length = maxLength - length;
            $(this).siblings('.char-count').children('.chars-num').text(length);
            if (length < 15)
            { $(this).siblings('.char-count').children('.chars-num').css("color", "red"); }
        });
    });
    //Susan Wong, Show preview before upload
    var imagesPreview = function (input, placeToInsertImagePreview) {
        if (input.files) {
            var filesAmount = input.files.length;
            for (i = 0; i < filesAmount; i++) {
                var reader = new FileReader();
                reader.onload = function (event) {
                    $($.parseHTML('<img>')).attr('src', event.target.result).appendTo(placeToInsertImagePreview);
                }
                reader.readAsDataURL(input.files[i]);
            }
        }
    };
    $('input[type = file]').on('change', function () {
        var relatedImg = $(this).parent('.upload-div').siblings('.preview-div');
        $(relatedImg).empty();
        imagesPreview(this, relatedImg);
    });
    //Susan Wong, assign colors to status'
    $(document).ready(function () {
        $('.fd-edit-status').each(function () {
            var fdStatus = $(this).text();
            if (fdStatus.indexOf('Pending edit') > -1) {
                $(this).css('background-color', '#d00006');
            }
            else if (fdStatus.indexOf('Ready to submit') > -1) {
                $(this).css('background-color', '#fea500');
            }
            else if (fdStatus.indexOf('Pending approval') > -1) {
                $(this).css('background-color', '#0171c5');
            }
            else if (fdStatus.indexOf('Approved & active') > -1) {
                $(this).css('background-color', '#73ba36');
            }
        });
    });
    //Susan Wong, error msg on too many selections
    $(".msel").change(function () {
        i = 0;
        var checkedItems = $(this).siblings('.btn-group').children('.dropdown-menu').children('.active')
        checkedItems.each(function () {
            i++;
            if (i > 3) {
                $(this).closest('.multiselect-native-select').siblings('.info-error').removeClass("hide-it");
            }
            else {
                $(this).closest('.multiselect-native-select').siblings('.info-error').addClass("hide-it");
            }
        });   
    });

	// Jim code to enable  SUBMIT button  FOR APPROVAL button only if  checkbox is checked for any firms 
    $(document).ready(function () { /*code here*/
        // repeater R3 for parent firms
    	var ip = 0;
    	$('#myR3 [id*=pfcb], [id*=sfcb]').each(function () {
    		if ($(this).prop('checked') == true) {
    		//alert($(this).next('label').text());
    		ip = ip + 1;
    			//	$("#<%= SubmitApproval.ClientID %>").removeAttr("disabled");
    			//alert(ip);
				}   
    		else
    		{
    			//$("#<%= SubmitApproval.ClientID %>").attr("disabled", "disabled");
    		}
    	});
    	if (ip > 0)
    	{ $("#<%= SubmitApproval.ClientID %>").removeAttr("disabled"); }
    	else
    	{ $("#<%= SubmitApproval.ClientID %>").attr("disabled", "disabled");}
				// repeater R3 for parent firms
<%--    	$('#myR4 [id*=sfcb]').each(function () {
    		if ($(this).prop('checked') == true)
    		{
    			$("#<%= SubmitApproval.ClientID %>").removeAttr("disabled");
	}   
    		else
    		{ $("#<%= SubmitApproval.ClientID %>").attr("disabled", "disabled"); }
    	});--%>
    });
	$('[id*=lstspec]').multiselect({
		//includeSelectAllOption: true
	});
	$('.msel').multiselect({
		//includeSelectAllOption: true
	});
	// this is firing after postback to save the collapse state 
	function keepopenaftersave(id) {
		$("#collapse" + id).addClass("panel-collapse collapse in");
	}
	function showmodal()
	{$('#myModal').modal('show');}
</script>
