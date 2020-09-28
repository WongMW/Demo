<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EnhancedListingEditForm.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.FirmsSearch.EnhancedListingEditForm" %>
<%@ Register TagPrefix="uc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%-- bootstrap  --%>
<!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous"/>

<!-- Optional theme -->
<%--<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css" integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp" crossorigin="anonymous"/>--%>
<!-- Override bootstrap css for corporate styles -->
<link href="../../../CSS/bootstrap-override.min.css" rel="stylesheet" />
<!-- Latest compiled and minified JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>
 <!-- BootstrapValidator JS -->
<script type="text/javascript" src="//cdn.jsdelivr.net/jquery.bootstrapvalidator/0.5.0/js/bootstrapValidator.min.js"></script>

<!-- Susan Wong 10-08-2017: sumo  Multiselect dropdown files -->
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery.sumoselect/3.0.2/sumoselect.min.css">
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.sumoselect/3.0.2/jquery.sumoselect.min.js"></script>
<!-- bootstrap multiselect -->
<link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
<script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js" type="text/javascript"></script>


<uc2:User id="User1" runat="server" />
<%--<div class="raDiv" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="up1">
        <ProgressTemplate>
            <div class="dvProcessing">
                <div class="loading-bg">
                    <img src="/Images/CAITheme/bx_loader.gif" />
                    <span>LOADING...<br /><br />Please do not leave or close this window while payment is processing.</span>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>--%>
<%--<asp:ScriptManager ID="scriptmanager1" runat="server">  
</asp:ScriptManager> --%> 
<%--<asp:UpdatePanel ID="up1" runat="server" >
    <ContentTemplate>--%>

		 <%--<form id="form1" >--%>
<%--        <div class="form-section-full-border no-margin">
            <h2>1. Fill in shared details for your firms</h2>
            <p>The details in this section only need to be filled in once and will automatically be shared by all your firms.</p>
        </div>
      <div class="form-section-half-border no-margin">
            <div class="form-group">
                <span class="label-title">No. of employees<span class="required"></span></span>
				 <asp:DropDownList ID="dde"  runat="server" class ="form-control"  name="dde"  >                           
                                <asp:ListItem  Enabled="true" Text="Please select no of employees" Value=""></asp:ListItem>                                               
                                <asp:ListItem Text="Self-employed " Value="1" ></asp:ListItem>
                                <asp:ListItem Text="1-10 employees" Value="2"></asp:ListItem>
                                <asp:ListItem Text="11-50 employees" Value="3"></asp:ListItem>
                                <asp:ListItem Text="51-200 employees" Value="4"></asp:ListItem>      
                                <asp:ListItem Text="201-500 employees" Value="5"></asp:ListItem>
                                <asp:ListItem Text="501-1000 employees" Value="6"></asp:ListItem>
                                <asp:ListItem Text="1001-5000 employees" Value="7"></asp:ListItem>
					            <asp:ListItem Text="5001-10,000 employees" Value="6"></asp:ListItem>
                                <asp:ListItem Text="10,000 + employees" Value="7"></asp:ListItem>              
                               </asp:DropDownList>
              
            </div>
        </div>
        <div class="form-section-half-border no-margin">
            <div class="form-group">
                <span class="label-title">No. of partners<span class="required"></span></span>
              <asp:TextBox ID="txtPartnerCount" runat="server" width="100%"></asp:TextBox>

            </div>
        </div>
        <div class="form-section-half-border no-margin">
            <div class="form-group">
                <span class="label-title"> Firm Description<span class="required"></span> (350 characters max)</span>
                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" maxlenght="350" width="100%" height="142px" Style="resize:none;" CssClass="char-max-350"></asp:TextBox>
                <div><span id="chars" style="font-weight:bold;">350</span> characters remaining</div>
            </div>
        </div>
			<!-- image logo upload  -->
        <div class="form-section-half-border no-margin">
            <div class="form-group">
                <span class="label-title">Firm logo<span class="required"></span></span>
				 <div class="col-sm-6">
					
						<div class="logo-spec">
							 <p><Strong>Image requirements:</Strong><br />
                    Image size: <strong>600 x 600</strong> pixels<br />
                    Image ratio: <strong>1:1</strong>
								  </p>
						 </div>
					</div>
				</div>
				<div class="col-sm-6">						
					<asp:Image ID="ImgPrv" Height="150px" Width="150px" runat="server"  alt=""   /><br />				
					<asp:FileUpload ID="fu" runat="server"  /><br/>
					<span id="spnDocMsg" class="error" style="display: none;"></span>
                        <br />

					<asp:Button ID="btnupload" runat="server" Text="Upload" OnClick="btnupload_Click" class=""    /> <br />
                    <asp:Label ID ="ful" runat="server"></asp:Label>

 				 
                </div>
            </div>--%>
		<!-- image logo upload  -->
        

        
        <%-- REPEATER START: Everything BELOW this needs to be a repeater, 1 per enhanced listing--%>
<%--        <div class="repeater-holder">
            <div class="trigger-title">
                <span class="outside-cai-form form-title">Edit details <span class="increment-num"></span>. [Firm Name]<span class="plus"></span></span></div>
            <div class="trigger-section">
                <div class="cai-form" style="margin-bottom:0px">
                    <div class="form-section-full-border no-margin" style="padding:20px">
                        <asp:Label ID="Label8" runat="server" CssClass="status-title-msg">Status: </asp:Label>
                        <asp:Label ID="lblStatusMsg" Text="" CssClass="status-title-msg status-msg-color" style="width:auto" runat="server">Unedited</asp:Label>
                     
                    </div>
                </div>
                <div class="form-section-full-border no-margin">
                    <h2>2. Provide additional details</h2>
                    <p>You can select <strong>up to 3 specialisms</strong> and <strong>up to 3 sectors</strong> that applies to this firm</p>
                </div>
                <div class="form-section-half-border no-margin">
                    <div class="form-group">
                        <span class="label-title">Specialisms<span class="required"></span></span>
                        <asp:CheckBoxList ID="chkSpecialisms" runat="server" RepeatColumns="2" CssClass="checkbox-align">
                            <asp:ListItem Value="1">Audit</asp:ListItem>
                            <asp:ListItem Value="2">Business advisory / Management consulting</asp:ListItem>
                            <asp:ListItem Value="3">Company secretarial services</asp:ListItem>
                            <asp:ListItem Value="4">Corporate finance advise</asp:ListItem>
                            <asp:ListItem Value="5">Tax</asp:ListItem>
                            <asp:ListItem Value="6">Financial reporting / Accounts preparation</asp:ListItem>
                            <asp:ListItem Value="7">Forensic accounting</asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                </div>
                <div class="form-section-half-border no-margin">
                    <div class="form-group">
                        <span class="label-title">Industry sectors<span class="required"></span></span>
                        <asp:CheckBoxList ID="chkSectors" runat="server" RepeatColumns="2" CssClass="checkbox-align">
                            <asp:ListItem Value="1">Agriculture</asp:ListItem>
                            <asp:ListItem Value="2">Family business</asp:ListItem>
                            <asp:ListItem Value="3">Not for profit / Charity</asp:ListItem>
                            <asp:ListItem Value="4">Property / Construction</asp:ListItem>
                            <asp:ListItem Value="5">Professional services</asp:ListItem>
                            <asp:ListItem Value="6">Retail</asp:ListItem>
                            <asp:ListItem Value="7">Wholesale</asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                </div>
                <div class="form-section-full-border no-margin">
                    <div class="form-group">
                        <span class="label-title">Google maps location (optional)</span>
                    </div>
                </div>
                <div class="form-section-half-border no-margin">
                    <ol style="margin-left:20px">
                        <li>Go to <a href="https://www.google.com/maps" target="_blank">Google maps</a></li>
                        <li>Enter the name of your company / company address into Google Maps search box and click the <i class="fas fa-search"></i> "Search" button</li>
                        <li>Click on the <i class="fas fa-share-alt"></i> "Share" button</li>
                        <li>Click on "Copy link" and paste into the provided textbox here</li>
                    </ol>
                </div>
                <div class="form-section-half-border no-margin">
                    <div class="form-group">
                        <p style="margin-bottom:5px;font-weight:bold">This firm's location is:</p>
                        <asp:TextBox ID="txtGoogleMaps" runat="server" width="100%"></asp:TextBox>
                        <span class="txtbox-help-text">Copy and paste link here</span>
                    </div>
                </div>
                <div class="form-section-full-border">
                    <h2>3. Web enable your listing</h2>
                    <p class="info-tip">You can save your edits at any time if you want to return later to finish your editing</p>
                    <div class="form-group">
                        <asp:CheckBox Text="" runat="server" CssClass="checkbox-align"/>&nbsp;
                        <asp:Label ID="Label1" Font-bold="true" runat="server">Firm details are correct and it's ready to be web enable</asp:Label>
                    </div>
                </div>
                <div class="form-section-full-border no-margin center">
                    <div class="form-group">
                        <asp:Button ID="btnSaveListing" runat="server"  style="margin:10px 0px 0px" CssClass="submitBtn" Text="Save firm details" OnClick="btnSaveListing_Click" />
                    </div>
                </div>
                <div class="cai-form" style="margin:20px 0px 0px">
                    <div class="form-section-full-border no-margin center">
                        <h3>Non editable info for this firm (based on Annual Returns)</h3>
                    </div>
                    <div class="form-section-half-border no-margin">
                        <div class="form-group">
                            <span class="label-title">Website</span>
                            <asp:Label ID="lblFirmWebsite" Text="" runat="server">http://www.google.com</asp:Label>
                        </div>
                    </div>
                    <div class="form-section-half-border no-margin">
                        <div class="form-group">
                            <span class="label-title">Email</span>
                            <asp:Label ID="lblFirmEmail" Text="" runat="server">info@website.com</asp:Label>
                        </div>
                    </div>
                    <div class="form-section-half-border no-margin">
                        <div class="form-group">
                            <span class="label-title">Phone</span>
                            <asp:Label ID="lblFirmPhone" Text="" runat="server">01 123 4567</asp:Label>
                        </div>
                    </div>
                    <div class="form-section-half-border no-margin">
                        <div class="form-group">
                            <span class="label-title">Fax</span>
                            <asp:Label ID="lblFirmFax" Text="" runat="server">01 123 4567</asp:Label>
                        </div>
                    </div>
                    <div class="form-section-half-border no-margin">
                        <div class="form-group">
                            <span class="label-title">Address</span>
                            <asp:Label ID="lblFirmAddress" Text="" runat="server">49 Pearse Street, Dublin 2, Ireland</asp:Label>
                        </div>
                    </div>
                    <div class="form-section-half-border no-border">
                        <div class="form-group">
                            <span class="label-title">Need to change something?</span>
                            Send your request to <a href="mailto:professionalstandards@charteredaccountants.ie">Professional Standards</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>
        <%-- REPEATER END: Everything ABOVE this needs to be a repeater, 1 per enhanced listing--%>

        <%-- REPEATER START: THIS SECTION IS JUST AN EXAMPLE, CAN BE DELETED --%>
       <%-- <div class="repeater-holder">
            <div class="trigger-title">
                <span class="outside-cai-form form-title">Edit details <span class="increment-num"></span>. [Firm Name]<span class="plus"></span></span></div>
            <div class="trigger-section">
                <div class="cai-form" style="margin-bottom:0px">
                    <div class="form-section-full-border no-margin" style="padding:20px">
                        <asp:Label ID="Label2" runat="server" CssClass="status-title-msg">Status: </asp:Label>
                        <asp:Label ID="Label3" Text="" CssClass="status-title-msg status-msg-color" style="width:auto" runat="server">Unedited</asp:Label>
                      
                    </div>
                </div>
                <div class="form-section-full-border no-margin">
                    <h2>2. Provide additional details</h2>
                    <p>You can select <strong>up to 3 specialisms</strong> and <strong>up to 3 sectors</strong> that applies to this firm</p>
                </div>
                <div class="form-section-half-border no-margin">
                    <div class="form-group">
                        <span class="label-title">Specialisms<span class="required"></span></span>
                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatColumns="2" CssClass="checkbox-align">
                            <asp:ListItem Value="1">Audit</asp:ListItem>
                            <asp:ListItem Value="2">Business advisory / Management consulting</asp:ListItem>
                            <asp:ListItem Value="3">Company secretarial services</asp:ListItem>
                            <asp:ListItem Value="4">Corporate finance advise</asp:ListItem>
                            <asp:ListItem Value="5">Tax</asp:ListItem>
                            <asp:ListItem Value="6">Financial reporting / Accounts preparation</asp:ListItem>
                            <asp:ListItem Value="7">Forensic accounting</asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                </div>
                <div class="form-section-half-border no-margin">
                    <div class="form-group">
                        <span class="label-title">Industry sectors<span class="required"></span></span>
                        <asp:CheckBoxList ID="CheckBoxList2" runat="server" RepeatColumns="2" CssClass="checkbox-align">
                            <asp:ListItem Value="1">Agriculture</asp:ListItem>
                            <asp:ListItem Value="2">Family business</asp:ListItem>
                            <asp:ListItem Value="3">Not for profit / Charity</asp:ListItem>
                            <asp:ListItem Value="4">Property / Construction</asp:ListItem>
                            <asp:ListItem Value="5">Professional services</asp:ListItem>
                            <asp:ListItem Value="6">Retail</asp:ListItem>
                            <asp:ListItem Value="7">Wholesale</asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                </div>
                <div class="form-section-full-border no-margin">
                    <div class="form-group">
                        <span class="label-title">Google maps location (optional)</span>
                    </div>
                </div>
                <div class="form-section-half-border no-margin">
                    <ol style="margin-left:20px">
                        <li>Go to <a href="https://www.google.com/maps" target="_blank">Google maps</a></li>
                        <li>Enter the name of your company / company address into Google Maps search box and click the <i class="fas fa-search"></i> "Search" button</li>
                        <li>Click on the <i class="fas fa-share-alt"></i> "Share" button</li>
                        <li>Click on "Copy link" and paste into the provided textbox here</li>
                    </ol>
                </div>
                <div class="form-section-half-border no-margin">
                    <div class="form-group">
                        <p style="margin-bottom:5px;font-weight:bold">This firm's location is:</p>
                        <asp:TextBox ID="TextBox1" runat="server" width="100%"></asp:TextBox>
                        <span class="txtbox-help-text">Copy and paste link here</span>
                    </div>
                </div>
                <div class="form-section-full-border">
                    <h2>3. Web enable your listing</h2>
                    <p class="info-tip">You can save your edits at any time if you want to return later to finish your editing</p>
                    <div class="form-group">
                        <asp:CheckBox Text="" runat="server" CssClass="checkbox-align"/>&nbsp;
                        <asp:Label ID="Label4" Font-bold="true" runat="server">Firm details are correct and it's ready to be web enable</asp:Label>
                    </div>
                </div>
                <div class="form-section-full-border no-margin center">
                    <div class="form-group">
                        <asp:Button ID="Button1" runat="server" width="60%" style="margin:10px 0px 0px" CssClass="submitBtn" Text="Save this listing" />
                    </div>
                </div>
                <div class="cai-form" style="margin:20px 0px 0px">
                    <div class="form-section-full-border no-margin center">
                        <h3>Non editable info for this firm (based on Annual Returns)</h3>
                    </div>
                    <div class="form-section-half-border no-margin">
                        <div class="form-group">
                            <span class="label-title">Website</span>
                            <asp:Label ID="Label5" Text="" runat="server">http://www.google.com</asp:Label>
                        </div>
                    </div>
                    <div class="form-section-half-border no-margin">
                        <div class="form-group">
                            <span class="label-title">Email</span>
                            <asp:Label ID="Label6" Text="" runat="server">info@website.com</asp:Label>
                        </div>
                    </div>
                    <div class="form-section-half-border no-margin">
                        <div class="form-group">
                            <span class="label-title">Phone</span>
                            <asp:Label ID="Label7" Text="" runat="server">01 123 4567</asp:Label>
                        </div>
                    </div>
                    <div class="form-section-half-border no-margin">
                        <div class="form-group">
                            <span class="label-title">Fax</span>
                            <asp:Label ID="Label9" Text="" runat="server">01 123 4567</asp:Label>
                        </div>
                    </div>
                    <div class="form-section-half-border no-margin">
                        <div class="form-group">
                            <span class="label-title">Address</span>
                            <asp:Label ID="Label10" Text="" runat="server">49 Pearse Street, Dublin 2, Ireland</asp:Label>

                        </div>
                    </div>
                    <div class="form-section-half-border no-border">
                        <div class="form-group">
                            <span class="label-title">Need to change something?</span>
                            Send your request to <a href="mailto:professionalstandards@charteredaccountants.ie">Professional Standards</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>
        <%-- REPEATER END: THIS SECTION IS JUST AN EXAMPLE, CAN BE DELETED --%>
        
       <%-- <div class="form-section-full-border no-margin center">
            <div class="form-group">
                <p class="info-note" style="margin-top:20px">All listings <Strong>must be approved by a member of staff</Strong>. Once approved, the listing will show on the website. The listing will then remain on the site for the next <Strong>365 days</Strong>.</p>
                <p>Saved all changes for your listings? Submit them for approval when  you are ready. <strong>Once changes are submitted they can NOT be changed/edited</strong> so please ensure all details are accurate and correct before submitting.</p>
                <asp:Button ID="Button2" runat="server" width="60%" style="margin:10px 0px 30px" CssClass="submitBtn disabled-btn" Text="Submit for approval" />
            </div>
        </div>--%>

			 <h1> Enhanced List firm details </h1>
			<%-- <% foreach (CountryList c in  cnt)
				 { %> <!-- loop through the list -->
					<div>
						 <!-- write out the name of the site -->
						 <div class="col-xs-4">
                                   <div id="id"><span class="glyphicon glyphicon-time"></span> Updated <%= c.CountryId %></div>
                                </div>
                                <div class="col-xs-4">
                                    <div id="country"><span class="glyphicon glyphicon-map-marker"></span> <%= c.CountryName %></div>
                                </div>
					</div>

			
			  
				<% } %>--%>


			 			 

			<%-- </form>--%>
<%--<h1> Test data</h1>
<% foreach (var site in Sites) { %> 
<!-- loop through the list -->
  <div>
    <%= site %> 
	  <!-- write out the name of the site -->
  </div>
<% } %> --%>




<%--<table class="table">
    <thead>
      <tr>
        <th>Firstname</th>
        <th>Lastname</th>
        <th>Email</th>
      </tr>
    </thead>	
    <tbody>
		<% foreach (var f  in flist) { %>
						  <tr>
							<td><%= f.FirmId %></td>
							<td><%= f.FirmName %></td>
							
			<td><button type="button" class="btn btn-default" data-toggle="modal" data-target="#myModal1">  Firm Details</button></td>					 
            <td><button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal2"> Add Enhanced list</button></td>
			<td><button type="button" class="btn btn-info" data-toggle="modal" data-target="#myModal">  Submit</button></td>
       							
						  </tr>
	
	
	 <% }%>
		</tbody>
	</table>--%>
<!-- Modal -->
<div id="addModal" class="modal fade" role="dialog"  >
  <div class="modal-dialog modal-lg">
    <!-- Modal content-->
    <div class="modal-content" >
      <div class="modal-header modal-header-primary">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Enhanced listing Deatils</h4>
      </div>
      <div class="modal-body">
		<form >
			
       <div class="form-group">
                <span class="label-title">No. of employees<span class="required"></span></span>
				 <asp:DropDownList ID="dde"  runat="server" class ="form-control"  name="dde"  >                           
                                <asp:ListItem  Enabled="true" Text="No of employees" Value=""></asp:ListItem>                                               
                                <asp:ListItem Text="Self-employed " Value="1" ></asp:ListItem>
                                <asp:ListItem Text="1-10 employees" Value="2"></asp:ListItem>
                                <asp:ListItem Text="11-50 employees" Value="3"></asp:ListItem>
                                <asp:ListItem Text="51-200 employees" Value="4"></asp:ListItem>      
                                <asp:ListItem Text="201-500 employees" Value="5"></asp:ListItem>
                                <asp:ListItem Text="501-1000 employees" Value="6"></asp:ListItem>
                                <asp:ListItem Text="1001-5000 employees" Value="7"></asp:ListItem>
					            <asp:ListItem Text="5001-10,000 employees" Value="6"></asp:ListItem>
                                <asp:ListItem Text="10,000 + employees" Value="7"></asp:ListItem>              
                               </asp:DropDownList>
				  </div>
       
   
       <%-- <div class="form-section-half-border no-margin">--%>
            <div class="form-group">
		
				  <span class="label-title">No. of partners<span class="required"></span></span>
				  <asp:TextBox ID="txtPartnerCount" runat="server" width="100%"></asp:TextBox>
				</div>
		
		<%--</div>--%>
      <%--  <div class="form-section-half-border no-margin">--%>
            <div class="form-group">
				
					<span class="label-title"> Firm Description<span class="required"></span> (350 characters max)</span>
					<asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" maxlenght="350" width="100%" height="142px" Style="resize:none;" CssClass="char-max-350"></asp:TextBox>
					<div><span id="chars" style="font-weight:bold;">350</span> characters remaining</div>
				
            </div>
        <%--</div>--%>
        <%--<div class="form-section-half-border no-margin">--%>
            <div class="form-group">
                <span class="label-title">Firm logo<span class="required"></span></span>
			<%--	 <div class="col-sm-6">
					
						<div class="logo-spec">
							 <p><Strong>Image requirements:</Strong><br />
                    Image size: <strong>600 x 600</strong> pixels<br />
                    Image ratio: <strong>1:1</strong>
								  </p>
						 </div>
					</div>--%>
				
				<div class="col-sm-12">					
						<asp:Image ID="ImgPrv" Height="150px" Width="150px" runat="server"  alt=""   /><br />
					   <asp:FileUpload ID="fu" runat="server"  /><br/>
					<span id="spnDocMsg" class="error" style="display: none;"></span>
                        <br />
				<%--	<asp:Button ID="btnupload" runat="server" Text="Upload" OnClick="btnupload_Click" class=""    /> <br /> --%>
                    <asp:Label ID ="ful" runat="server"></asp:Label>
                </div>
              </div>
			
<%--            <div class="trigger-section">
                <div class="cai-form" style="margin-bottom:0px">
                    <div class="form-section-full-border no-margin" style="padding:20px">
                        <asp:Label ID="Label12" runat="server" CssClass="status-title-msg">Status: </asp:Label>
                        <asp:Label ID="Label13" Text="" CssClass="status-title-msg status-msg-color" style="width:auto" runat="server">Unedited</asp:Label>

                    </div>
                </div> --%>
           <%--     <div class="form-section-full-border no-margin">--%>
			<%--<div class="form-group">
                   <h2>2. Provide additional details</h2>
                    <p>You can select <strong>up to 3 specialisms</strong> and <strong>up to 3 sectors</strong> that applies to this firm</p>
               </div>--%>
               <%-- <div class="form-section-half-border no-margin">--%>
                    <div class="form-group">
						<div class="col-sm-12">
							<p>You can select <strong>up to 3 specialisms</strong> and <strong>up to 3 sectors</strong> that applies to this firm</p>
                        <span class="label-title"><strong>Specialisms :</strong><span class="required"></span></span>
                        <asp:ListBox ID="lstspec" runat="server" SelectionMode="Multiple" >
                            <asp:ListItem Value="1" Text="Audit" />
                            <asp:ListItem Value="2" Text="Business advisory / Management consulting"/>
                            <asp:ListItem Value="3" Text="Company secretarial services"/>
                            <asp:ListItem Value="4" Text="Corporate finance advise"/>
                            <asp:ListItem Value="5" Text="Tax" />
                            <asp:ListItem Value="6" Text="Financial reporting / Accounts preparation"/>
                            <asp:ListItem Value="7" Text="Forensic accounting"/>
                        </asp:ListBox>


               </div>
               </div>
			<%--	</div>--%>
                <%--</div>--%>
              <%--  <div class="form-section-half-border no-margin">--%>
                    <div class="form-group">
						<div class="col-sm-12">
                        <span class="label-title"><strong>Industry sectors :</strong><span class="required"></span></span>
                        <asp:ListBox ID="lstis" runat="server" SelectionMode="Multiple" >
                            <asp:ListItem Value="1" Text="Agriculture" />
                            <asp:ListItem Value="2" Text="Family business"/>
                            <asp:ListItem Value="3" Text="Not for profit / Charity"/>
                            <asp:ListItem Value="4" Text="Property / Construction"/>
                            <asp:ListItem Value="5" Text="Professional services"/>
                            <asp:ListItem Value="6" Text="Retail"/>
                            <asp:ListItem Value="7" Text="Wholesale"/>
                        </asp:ListBox>
							<%--<select  id="s2" multiple="multiple" onchange="console.log('changed', this)" placeholder=" Industry sectors" class="SlectBox-grp">
								<option value="1">Agriculture</option>
								<option value="2">Family business</option>
								<option value="3">Not for profit / Charity</option>                  
								<option value="4">Property / Construction</option>
								<option value="5">Professional services</option>
								<option value="6">Retail </option>
								<option value="7">Wholesale</option>    
								</select>--%>
                    </div>
						</div>
               <%-- </div>--%>
                <%--<div class="form-section-full-border no-margin">--%>
                    <div class="form-group">
                        <span class="label-title">Google maps location (optional)</span>
                   <%-- </div>--%>
                <%--</div>--%>
                <%--<div class="form-section-half-border no-margin">--%>
                    <ol style="margin-left:20px">
                        <li>Go to <a href="https://www.google.com/maps" target="_blank">Google maps</a></li>
                        <li>Enter the name of your company / company address into Google Maps search box and click the <i class="fas fa-search"></i> "Search" button</li>
                        <li>Click on the <i class="fas fa-share-alt"></i> "Share" button</li>
                        <li>Click on "Copy link" and paste into the provided textbox here</li>
                    </ol>
               <%-- </div>--%>
               <%-- <div class="form-section-half-border no-margin">--%>
                    <div class="form-group">
                        <p style="margin-bottom:5px;font-weight:bold">This firm's location is:</p>
                        <asp:TextBox ID="TextBox4" runat="server" width="100%"></asp:TextBox>
                        <span class="txtbox-help-text">Copy and paste link here</span>
                    </div>
                </div>
		   </form>
			</div>
      <div class="modal-footer">
		   <%--<button type="button" class="btn btn-primary">Save changes</button>--%>
		  <asp:Button ID="b1save" Text ="Save changes" class="cai-btn cai-btn-red" runat="server"  />
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
      </div>
   </div>
	</div>
	
	</div>



<!-- Modal -->
<div id="M2" class="modal fade" role="dialog">
  <div class="modal-dialog">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Modal Header 2</h4>
      </div>
      <div class="modal-body">
        <p>Some text in the modal.</p>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
      </div>
    </div>

  </div>
</div>

		 <div class="form-group">
			 <div class="alert alert-success">
                <p class="info-note" style="margin-top:20px">Please click on <Strong>Add/Edit details</Strong> to upload firm deatils and  use  <Strong>SAVE CHANGES </Strong>button to save the details</p>
                <p>sample test . <strong>Once changes are submitted they can NOT be changed/edited</strong> so please ensure all details are accurate and correct before submitting.</p>
	            <div class="center-block">
               <%-- <asp:Button ID="Button1" runat="server"  style="margin:10px 0px 30px" CssClass="submitBtn disabled-btn  center-block" Text="Submit for approval" />--%>
				</div>
				 </div>
  </div>



<!-- jim  modal popup end -->
<!-- jim   gridview  test -->
<h1>  Firm List  </h1>
<%--		<div style ="display:none">
<asp:GridView ID="g1" runat="server" CssClass="table" GridLines="None" AutoGenerateColumns="false" DataKeyNames="FirmId"  OnRowDataBound="g1_RowDataBound" >
	<Columns>
		<asp:BoundField DataField="FirmId" HeaderText="FirmId" />
        <asp:BoundField DataField="FirmName" HeaderText="Firm Name" />
        <asp:TemplateField ShowHeader="False">
            <ItemTemplate>
                <asp:Button ID="badd" runat="server"  class="btn  btn-success"  Text="Add"   OnClick="DisplayModal"   />
            </ItemTemplate>		  
        </asp:TemplateField>
		        <asp:TemplateField ShowHeader="False">
            <ItemTemplate>
                <asp:Button ID="bedit" runat="server"  class="btn btn-primary"  
                    Text="Edit"  CommandName="beidt" CommandArgument='<%#Eval("FirmId") %>' />
            </ItemTemplate>		  
        </asp:TemplateField>
		<asp:TemplateField ShowHeader="False">
            <ItemTemplate>
				<button type="button" class="btn  btn-info" data-toggle="modal" data-target="#addModal"  >   Submit</button>
            </ItemTemplate>	
        </asp:TemplateField>
	</Columns>
	
</asp:GridView>
</div>--%>

		<!-- open all + close all buttons for collapse -->
		<div style ="display:none">
		<div class="controls ">
			<div class="pull-right">
			<button class="btn btn-primary open-button" type="button">
				Open all
			</button>
			<button class="btn btn-primary close-button" type="button">
			 Close all
			 </button>

		</div>
		</div>
			</div>



<!-- Accordian test -->

			
<%--								 <table id="t3" class="table">	--%>
<form id="sff" class="form-horizontal" >
						<asp:Repeater ID ="R3" runat="server" OnItemDataBound="R3_ItemDataBound"  >												
							<ItemTemplate>
								
								<!--  start of parent firms -->	
								<div class="panel-group">							
									<div class="panel panel-danger">
										 <div class="panel-heading a">						
											 <asp:CheckBox ID="pfcb" runat="server" Text='<%# Eval("fname") %>'  > </asp:CheckBox>
											 <asp:HiddenField runat="server" ID="pfId" value='<%# Eval("fid") %>' />
											 <div class="pull-right">
												<a data-toggle="collapse" href="#collapse<%# Eval("fid") %> ">Add/Edit details</a>
											</div>
										 </div>
										 <div  class="panel-collapse collapse" id="collapse<%# Eval("fid") %>">
											<div class="panel-body">
											<%--	<form id="pff" class="form-horizontal" >--%>
													 <div class="row">
														<div class="col-sm-6">
														<span class="label-title">No. of employees<span class="required"></span></span>
															<asp:DropDownList ID="ddpen"  runat="server" class ="ddpen form-control"  name="ddpen"  >                           
																		<asp:ListItem  Enabled="true" Text="No of employees" Value=""></asp:ListItem>                                               
																		<asp:ListItem Text="Self-employed " Value="1" ></asp:ListItem>
																		<asp:ListItem Text="1-10 employees" Value="2"></asp:ListItem>
																		<asp:ListItem Text="11-50 employees" Value="3"></asp:ListItem>
																		<asp:ListItem Text="51-200 employees" Value="4"></asp:ListItem>      
																		<asp:ListItem Text="201-500 employees" Value="5"></asp:ListItem>
																		<asp:ListItem Text="501-1000 employees" Value="6"></asp:ListItem>
																		<asp:ListItem Text="1001-5000 employees" Value="7"></asp:ListItem>
																		<asp:ListItem Text="5001-10,000 employees" Value="6"></asp:ListItem>
																		<asp:ListItem Text="10,000 + employees" Value="7"></asp:ListItem>              
														   </asp:DropDownList>
															<span>
																<%--<asp: ID="rfp1" runat="server" ControlToValidate="ddpen" Display="Dynamic" ErrorMessage="Select atleast one " "></asp:>--%>
																<%--<asp:CustomValidator  runat="server" ID="cvp1" ControlToValidate="ddpen" Display="Dynamic" ErrorMessage="Select One"></asp:CustomValidator>--%>
															</span>
													  </div>
														<div class="col-sm-6">
														  <span class="label-title">No. of partners<span class="required"></span></span>
														  <asp:TextBox ID="txtpcount" runat="server" width="100%" CssClass="txtpcountcss"   ></asp:TextBox>
														</div>
												     </div>
													<div class="row">
													   <div class="col-sm-6">				
															<span class="label-title"> Firm Description<span class="required"></span> (350 characters max)</span>
															<asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" maxlenght="350" width="100%" height="142px" Style="resize:none;" CssClass="char-max-350"></asp:TextBox>
															<div><span id="chars" style="font-weight:bold;">350</span> characters remaining</div>
													   </div>
														<div class="col-sm-6">	
															<span class="label-title">Firm logo<span class="required"></span></span>
																<asp:Image ID="ImgPrv" Height="150px" Width="150px" runat="server"  alt=""   /><br />
																<asp:FileUpload ID="fu" runat="server"  /><br/>
																<span id="spnDocMsg" class="error" style="display: none;"></span>
																<br />																
															    <asp:Label ID ="ful" runat="server"></asp:Label>
														</div>
													</div>
													<div class="row">
													   <p>You can select <strong>up to 3 specialisms</strong> and <strong>up to 3 sectors</strong> that applies to this firm</p>
													   <div class="col-sm-6">													   
															<span class="label-title"><strong>Specialisms :</strong><span class="required"></span></span>
															<asp:ListBox ID="pfspec" runat="server" SelectionMode="Multiple"  CssClass="msel">
																<asp:ListItem Value="1" Text="Audit" />
																<asp:ListItem Value="2" Text="Business advisory / Management consulting"/>
																<asp:ListItem Value="3" Text="Company secretarial services"/>
																<asp:ListItem Value="4" Text="Corporate finance advise"/>
																<asp:ListItem Value="5" Text="Tax" />
																<asp:ListItem Value="6" Text="Financial reporting / Accounts preparation"/>
																<asp:ListItem Value="7" Text="Forensic accounting"/>
															</asp:ListBox>
													   </div>
													   <div class="col-sm-6">
														    <span class="label-title"><strong>Industry sectors :</strong><span class="required"></span></span>
													
														   <asp:ListBox ID="pfindsec" runat="server" SelectionMode="Multiple" CssClass="msel"  >
																<asp:ListItem Value="1" Text="Agriculture" />
																<asp:ListItem Value="2" Text="Family business"/>
																<asp:ListItem Value="3" Text="Not for profit / Charity"/>
																<asp:ListItem Value="4" Text="Property / Construction"/>
																<asp:ListItem Value="5" Text="Professional services"/>
																<asp:ListItem Value="6" Text="Retail"/>
																<asp:ListItem Value="7" Text="Wholesale"/>
															</asp:ListBox>
															
													   </div>
                                                    </div>
													<div class="row">
														<div class="col-sm-10">
															<span class="label-title">Google maps location (optional)</span>
															<ol style="margin-left:20px">
																<li>Go to <a href="https://www.google.com/maps" target="_blank">Google maps</a></li>
																<li>Enter the name of your company / company address into Google Maps search box and click the <i class="fas fa-search"></i> "Search" button</li>
																<li>Click on the <i class="fas fa-share-alt"></i> "Share" button</li>
																<li>Click on "Copy link" and paste into the provided textbox here</li>
															</ol>
															<div class="form-group">
																<p style="margin-bottom:5px;font-weight:bold">This firm's location is:</p>
																<asp:TextBox ID="tblocgmap" class="tblocgmap form-control" placeholder="Copy and paste Google Map link here" runat="server" width="50%" ></asp:TextBox>
														<%--		<span class="txtbox-help-text">Copy and paste link here</span>--%>
															</div>
														</div>
														</div>
													<div class="row>">
															<div class="form-group center-block ">
															 <asp:Button ID="btnSave" runat="server" Text="Save"  OnClick="Save"   />
															</div>
													</div>
                                              <%--  </form>--%>
											</div>
										</div>
								<%--	</div>--%>
								<!--  end of parent firms -->
												
									<!-- start of sub  firms -->					
								 <asp:Repeater ID="R4" runat="server">
									<ItemTemplate>
										<div class="panel panel-info">
												 <div class="panel-heading">																	
														 <asp:CheckBox ID="pfcb" runat="server" Text='<%# Eval("fname") %>'  > </asp:CheckBox>
													  <div class="pull-right">
											 			 <a data-toggle="collapse" href="#collapse<%# Eval("fid") %>">Add/Edit details</a>
														</div>
												 </div>
												 <div  class="panel-collapse collapse" id="collapse<%# Eval("fid") %>">
													<div class="panel-body">
														<%--<form id="sff" class="form-horizontal">--%>
												<div  class="row">	
												<div class="form-group">
													<div class="col-sm-6">							
														<span class="label-title"><strong>Specialisms :</strong><span class="required"></span></span>
														<asp:ListBox ID="sfspec" runat="server" SelectionMode="Multiple" CssClass="msel" >
															<asp:ListItem Value="1" Text="Audit" />
															<asp:ListItem Value="2" Text="Business advisory / Management consulting"/>
															<asp:ListItem Value="3" Text="Company secretarial services"/>
															<asp:ListItem Value="4" Text="Corporate finance advise"/>
															<asp:ListItem Value="5" Text="Tax" />
															<asp:ListItem Value="6" Text="Financial reporting / Accounts preparation"/>
															<asp:ListItem Value="7" Text="Forensic accounting"/>
														</asp:ListBox>
												</div>	
												<div class="col-sm-6">	
												<span class="label-title"><strong>Industry sectors :</strong><span class="required"></span></span>
													<asp:ListBox ID="sfindsec" runat="server" SelectionMode="Multiple" CssClass="msel">
														<asp:ListItem Value="1" Text="Agriculture" />
														<asp:ListItem Value="2" Text="Family business"/>
														<asp:ListItem Value="3" Text="Not for profit / Charity"/>
														<asp:ListItem Value="4" Text="Property / Construction"/>
														<asp:ListItem Value="5" Text="Professional services"/>
														<asp:ListItem Value="6" Text="Retail"/>
														<asp:ListItem Value="7" Text="Wholesale"/>
													</asp:ListBox>
												</div>
											</div>
											</div>
										<div class="row">
														<div class="col-sm-10">
															<span class="label-title">Google maps location (optional)</span>
															<ol style="margin-left:20px">
																<li>Go to <a href="https://www.google.com/maps" target="_blank">Google maps</a></li>
																<li>Enter the name of your company / company address into Google Maps search box and click the <i class="fas fa-search"></i> "Search" button</li>
																<li>Click on the <i class="fas fa-share-alt"></i> "Share" button</li>
																<li>Click on "Copy link" and paste into the provided textbox here</li>
															</ol>
															<div class="form-group">
																<p style="margin-bottom:5px;font-weight:bold">This firm's location is:</p>
																<asp:TextBox ID="tblocgmap" class="tblocgmap form-control" placeholder="Copy and paste Google Map link here" runat="server" width="50%" ></asp:TextBox>
														<%--		<span class="txtbox-help-text">Copy and paste link here</span>--%>
															</div>
														</div>
														</div>
															<div class="form-group center-block ">
																<asp:Button ID="bsfsave" Text ="Save changes" class="cai-btn cai-btn-red center-block" runat="server" OnClick="Savesub" />
															</div>

													   <%--</form>--%>
			
													</div>
												</div>
										</div>				
									</ItemTemplate>				
								 </asp:Repeater>	
									
								<!-- endof  of sub  firms -->
										</div>
										</div>
							</ItemTemplate>
							</asp:Repeater>
					<%--  	</table>--%>
			<%--	</form>--%>


<!-- Submit button start -->
 <div class="form-group">
                <p class="info-note" style="margin-top:20px">All listings <Strong>must be approved by a member of staff</Strong>. Once approved, the listing will show on the website. The listing will then remain on the site for the next <Strong>365 days</Strong>.</p>
                <p>Saved all changes for your listings? Submit them for approval when  you are ready. <strong>Once changes are submitted they can NOT be changed/edited</strong> so please ensure all details are accurate and correct before submitting.</p>
	            <div class="center-block">
                <asp:Button ID="bapp" runat="server"  style="margin:10px 0px 30px" CssClass="submitBtn disabled-btn  center-block" Text="Submit for approval" />
				</div>
  </div>
<!-- Submit button end -->

<%--<form id="form2" runat="server">
    <div>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <asp:TextBox ID ></asp:TextBox>
            </ItemTemplate>
        </asp:Repeater>           
        <asp:Button ID="btnSave" runat="server" Text="Button" OnClick="btnSave_Click" />
    </div>
</form>--%>
<%--   </ContentTemplate>

</asp:UpdatePanel>--%>

<!--test repeater-->
	<asp:Repeater ID="R5" runat="server" OnItemDataBound="R5_DataBound">
     
    <ItemTemplate>
		<div class="panel-group" id="accordion">
		      <div class="panel panel-default">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        <%--<asp:HyperLink id="hl1" NavigateUrl="#collapse<%# Eval("id") %>" Text ="add deatils"  runat="server"/>--%>
                       <a data-toggle="collapse" data-parent="#accordion" id="link1" href="#collapse<%# Eval("id") %>"><%# Eval("id") %> </a>
                    </h4>
                </div>
				   <div id="collapse<%# Eval("id") %>" class="panel-collapse collapse ">
					<div class="panel-body">
						<%-- <h3> <%# Eval("name") %></h3>--%>
						
						<asp:HiddenField runat="server" ID="hid" Value='<%# Eval("id") %>' />
                         <td>
							 <asp:Label ID="Id" runat="server" Text='<%# Eval("Id") %>' />      
						</td>
						<td>
						<asp:TextBox ID="name" runat="server" Text='<%# Eval("name") %>' />
					   </td>
					   <td>
						<asp:Button Text="Get Value main" runat="server" OnClick="GetValueMain" />
					 </td>
						</div>
					   </div>

				  <asp:Repeater ID="R6" runat="server">
									<ItemTemplate>
										
												  <div class="panel panel-default">
													<div class="panel-heading">
														<h4 class="panel-title">
															<%--<asp:HyperLink id="hl1" NavigateUrl="#collapse<%# Eval("id") %>" Text ="add deatils"  runat="server"/>--%>
														   <a data-toggle="collapse" data-parent="#accordion" id="link1" href="#collapse<%# Eval("id") %>"><%# Eval("id") %> </a>
														</h4>
													</div>
												   <div id="collapse<%# Eval("id") %>" class="panel-collapse collapse ">
													<div class="panel-body">
														<%-- <h3> <%# Eval("name") %></h3>--%>
						
														<asp:HiddenField runat="server" ID="HiddenField1" Value='<%# Eval("id") %>' />
														 <td>
															 <asp:Label ID="Id" runat="server" Text='<%# Eval("Id") %>' />      
														</td>
														<td>
														<asp:TextBox ID="name" runat="server" Text='<%# Eval("name") %>' />
													   </td>
													   <td>
														<asp:Button Text="Get Value sub" runat="server" OnClick="GetValueSub" />
													 </td>
														</div>
													   </div>
													  </div>




										</ItemTemplate>
					  </asp:Repeater>
				  										</div>
										</div>
    </ItemTemplate>

</asp:Repeater>

    </form>

<!-- test -->


<script>
	function expand() {
		alert('expand');
		$('.collapse').collapse('show');
	}
	function collapse() {
		$('.collapse').collapse('hide');
	}

	$(".open-button").on("click", function () {
		$(this).closest('.collapse-group').find('.collapse').collapse('show');
	});

	$(".close-button").on("click", function () {
		$(this).closest('.collapse-group').find('.collapse').collapse('hide');
	});

	//$("#bpedit").click(function(e) {
	//	e.preventDefault();
	//	$(".panel-collapse").collapse("hide");
	//});

	//$("#collapse_hide").click(function(e) {
	//	e.preventDefault();
	//	$(".panel-collapse").collapse("hide");
	//});



	//alert(jQuery.fn.jquery);
	// max char 350 code
	//$('.ddpen').change(function () {

	//	var v = $('.ddpen').val();
	//	if (v < 1) {
	//		$('.ddpen').css("border", "1px solid red");
	//		$('.bpfsave').prop('disabled', true);
	//		return false;
	//	}
	//	else {
	//		$('.ddpen').css("border", "1px solid #ccc");
	//		$('.bpfsave').prop('disabled', false);;
	//	}

	//});
		


    var maxLength = 350;
    $('.char-max-350').keyup(function () {
        var length = $(this).val().length;
        var length = maxLength - length;
        $('#chars').text(length);
        if (length < 15)
        { $('#chars').css("color", "red"); }
    });

	//Imagepreview for image upload
	function ShowImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=ImgPrv.ClientID%>').prop('src', e.target.result)
                        .width(150)
                        .height(150);
                };
                reader.readAsDataURL(input.files[0]);
                }
	}

	// modal popup on button click insert
	function openModal() {
		$('[id*=addModal]').modal('show');
	} 
	// multi select 
	window.testSelAlld = $('#s1').SumoSelect({
		placeholder: 'Select specialiasm',
		//okCancelInMulti: true,
		isClickAwayOk: true
		//triggerChangeCombined: false
		//selectAll: false,
		//search: false,
		//searchText: 'Type here to search specialiasm',
	});

	//seelct only 3 value
	$("#s1").change(function() {
		if($("select option:selected").length > 3) { 
			//alert('You can select upto 3 options only');
			$(this).removeAttr("selected");
		}

	});

	window.testSelAlld = $('#s2').SumoSelect({
		placeholder: 'Industry sector',
		//okCancelInMulti: true,
		isClickAwayOk: true
		//triggerChangeCombined: false
		//selectAll: false,
		//search: false,
		//searchText: 'Type here to search specialiasm',
	});

	$('[id*=lstspec]').multiselect({
		//includeSelectAllOption: true
	});

	$('[id*=lstis]').multiselect({
		//includeSelectAllOption: true
	});
	// multi select inside repeater
	$('.msel').multiselect({
		//includeSelectAllOption: true
	});

	

	//var last_valid_selection = null;

	//$('#s1').change(function(event) {

	//	if ($(this).val().length > 3) {

	//		$(this).val(last_valid_selection);
	//		//alert($(this).val().length);
	//	} else {
	//		last_valid_selection = $(this).val();
	//	}
	//});


	 $(function () {
        $('#<%=fu.ClientID %>').change(
            function () {
            	var fileExtension = ['jpeg', 'jpg', 'png', 'gif', 'PNG', 'JPEG' ];
				 // validation file formats
                if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                	// 'jpeg', 'jpg', 'png', 'gif', 'PNG', 'JPEG'
                	<%--$('#<%=btnupload.ClientID %>').attr("disabled", true);--%>
                	$('#spnDocMsg').text("Sorry!! Upload only jpg, jpeg, png, gif files").show();
                	// Clear fileuload control selected file
                	$(this).replaceWith($(this).val('').clone(true));
                	//$('#ImgPrv').hide();
                  $('#<%=ImgPrv.ClientID%>').hide();
                }
                else {

                	 //Check and restrict the file size to 32 KB.
                	if ($(this).get(0).files[0].size > (2 * 1024 * 1024)) {
                		$('#spnDocMsg').text("Sorry!! Max allowed file size is 2 Mb").show();
                	// Clear fileuload control selected file
                	$(this).replaceWith($(this).val('').clone(true));
                	//Disable Submit Button
                	<%--$('#<%=btnupload.ClientID %>').attr("disabled", true);--%>

                		//$('#ImgPrv').hide();
                		$('#<%=ImgPrv.ClientID%>').hide();
                	} 
                	else {
                //	Clear and Hide message span
                		$('#spnDocMsg').text('').hide();
                //	Enable Submit Button
                		$('#btnupload').prop('disabled', false);
                		//
                		//$('#ImgPrv').show();
                		ShowImagePreview(this);
                		$('#<%=ImgPrv.ClientID%>').show();
                	//	alert($(this).val());
                		  }



                    <%--$('#<%=btnupload.ClientID %>').attr("disabled", false);
                    $('#<%=ful.ClientID %>').html(" ");--%>
                } 
            })  
    })  



	// upload image validate file extension 
	function validatefu() {

		// 2MB -> 2 * 1024 * 1024
		var maxFileSize =  2 * 1024 * 1024
		var fileUpload = $('#<%=fu.UniqueID%>');

		if (fileUpload.val() == '') {
			return false;
		}
		else {
			//if (fileUpload[0].files[0].size < maxFileSize) {
			//	$('#btnupload').prop('disabled', false);
			//	return true;
			//} else {
			//	$('#ful').text('File too big !')
			//	return false;
			//}
			alert($('#fu')[0].files[0].size);
		}
	}
	// custom validation fu


 
		//this code will be executed when a new file is selected
		$('#fu').bind('change', function () {
  
			//converts the file size from bytes to MB
			var fileSize = this.files[0].size / 1024 / 1024;

			//  max file size 2MB -> 2 * 1024 * 1024
			var maxFileSize =  2 * 1024 * 1024
  
			//gets the full file name including the extension
			var fileName = this.files[0].name;
  
			//finds where the extension starts
			var dotPosition = fileName.lastIndexOf(".");
  
			//gets only the extension
			var fileExt = fileName.substring(dotPosition);
  
			//checks whether the file is .png and less than 1 MB
			if (fileSize <= maxFileSize  && fileExt == ".png") {
  
				//successfully validated
				alert(fileName);
  
			}
			else
			{ alert(fileExt);   }
		});
		
	


	function test() {
		// Get uploaded file extension
		//var ext = $("#fu").val().split('.').pop();
		alert('ext');
	}

			


<%--	//form validation 
	$('#form1').bootstrapValidator({
		// To use feedback icons, ensure that you use Bootstrap v3.1.0 or later
		feedbackIcons: {
			valid: 'glyphicon glyphicon-ok',
			invalid: 'glyphicon glyphicon-remove',
			validating: 'glyphicon glyphicon-refresh'
		},
		fields: {
                     <%=dde.UniqueID%>: {
                     	validators: {
                     		callback: {
                     			message: 'Select No of Employees',
                     			callback: function(value, validator, $field) {
                     				/* Get the selected options */
                     				var options = validator.getFieldElements('<%=dde.UniqueID%>').val();                                  
                     				return (options != null && options.length > 0);
                     			}
                                                
                     		}
                     	}},// dropdown code validation end
			 <%=txtPartnerCount.UniqueID%>: {
                         validators: {
                             notEmpty: {
                                 message: 'Number of Partners is required and cannot be empty'
                             },

                             regexp: {
                                 regexp: /^\d+$/,
                                 message: 'The value is not a valid number'
                             }

                         }

			  },// number of partners  validation end 

			 <%=txtDescription.UniqueID%>: {
                         validators: {
                             notEmpty: {
                                 message: 'Description is required and cannot be empty'
                             },

                            
                         }

			 },
			// Description 

			 <%=fu.UniqueID%>: {
				validators: {
					file: {
						extension: 'jpeg,png,gif',
						type: 'image/jpeg,image/png,image/gif',
						maxSize: 2048 * 1024,
						message: 'Only jpeg/png/gif formats with Maximum 2 MB'
						
					},
					notEmpty: {
						message: 'Image required and cannot be empty'
					}
					

				}
			 }, // end of validation

			                      <%=lstspec.UniqueID%>: {
                     	validators: {
                     		callback: {
                     			message: 'Select  any 3 Specialisation',
                     			callback: function(value, validator, $field) {
                     				/* Get the selected options */
                     				var options = validator.getFieldElements('<%=lstspec.UniqueID%>').val();                                  
                     				return (options != null && options.length < 4);
                     			}
                                                
                     		}
                     	},
			                      	notEmpty:
										{ message : 'Select Specialisation'}
			                      },// dropdown code validation end




		}
	});--%>



	//form validation 
	$('#pff').bootstrapValidator({
		// To use feedback icons, ensure that you use Bootstrap v3.1.0 or later
		feedbackIcons: {
			valid: 'glyphicon glyphicon-ok',
			invalid: 'glyphicon glyphicon-remove',
			validating: 'glyphicon glyphicon-refresh'
		},
		fields: {
			    ddpen: {
                     	validators: {
                     		callback: {
                     			message: 'Select No of Employees',
                     			callback: function(value, validator, $field) {
                     				/* Get the selected options */
                     				var options = validator.getFieldElements('<%=dde.UniqueID%>').val();                                  
                     				return (options != null && options.length > 0);
                     			}
                                                
                     		}
                     	}
			    },
			tblocgmap: {
                         validators: {
                             notEmpty: {
                                 message: 'Number of Partners is required and cannot be empty'
                             },

                             regexp: {
                                 regexp: /^\d+$/,
                                 message: 'The value is not a valid number'
                             }

                         }

			  }// number of partners  validation end 

		}
	});

	//var prm = Sys.WebForms.PageRequestManager.getInstance();
	//if (prm != null) {
	//	prm.add_initializeRequest(function (sender, args)  {
	//		if (sender._postBackSettings.panelsToUpdate != null) {
	//			validfu();
	//		}
	//	});
	//}


   





</script>
  <style type="text/css">
			.modal-header-primary {
	color:#fff;
    padding:9px 15px;
    border-bottom:1px solid #eee;
    background-color: #8C1D40;
    -webkit-border-top-left-radius: 5px;
    -webkit-border-top-right-radius: 5px;
    -moz-border-radius-topleft: 5px;
    -moz-border-radius-topright: 5px;
     border-top-left-radius: 5px;
     border-top-right-radius: 5px;
}

	.help-block {
    display: block;
    margin-top: 5px;
    margin-bottom: 10px;
    color:red;
}
  	.error {
  		background-color: #e4150f;
  		font-weight: 300;
  		font-size: 12px;
  		padding: 3px 6px 3px 6px;
  		color: #fff;
  		text-align: center;
  		white-space: nowrap;
  	}
  	/*.dropdown-menu > .active > a, .dropdown-menu > .active > a:focus, .dropdown-menu > .active > a:hover {
		  background-color: lightyellow;
  	}*/

	  .zeroPadding {
  padding: 0 !important;
}






</style>
