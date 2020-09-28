<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FirmDirectoryItem.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.FirmsSearch.FirmDirectoryItem" %>

 <link rel='stylesheet' href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />

    <script type="text/javascript">
        $(document).ready(function () {
            $("#demo").on("hide.bs.collapse", function () {
                $("#bp").html('<span class="glyphicon glyphicon-collapse-down"></span> View Principals');
            });
            $("#demo").on("show.bs.collapse", function () {
                $("#bp").html('<span class="glyphicon glyphicon-collapse-up"></span> Hide Principals ');
            });
        });
  </script>
<style>
#firm-listing span.glyphicon {color:#8C1D40;font-size: 16px;}
#firm-listing .btn-default:hover span.glyphicon {color:#fff;}
.principals{padding: 0px;margin-bottom: 20px; }
.principals > #demo{background: #ccc; padding: 20px; font-weight: bold;}
<!-- ADDING STYLES OVERWRITTEN BY BOOTSTRAP STYLES -->
body, .office-link {font-size:16px !important;line-height: 1.4em;}
.title-holder{padding: 10px 0px;margin-top: 14px;}
h1, h2{font-family: 'Source Sans Pro', sans-serif;line-height: 1.4em;font-weight: 700;color: #003D51;}
h1{font-size: 32px;}
h2{font-size: 24px;}
a, a:link, a:visited, a:hover, a:active {color: #003D51;}
.main-nav li a, .link-holder a, .sfNavHorizontalDropDown .k-item > a.k-link, .sfNavHorizontal li.loginli a {font-size: 16px;}
.main-nav li a:hover, .link-holder a:hover, .sfNavHorizontal li.loginli a:hover{text-decoration: none !important;}
.link-holder a, .sfNavHorizontal li.loginli a{color: #fff}
.footer-wrapper-main li, .footer-wrapper-main li > a, .footer-wrapper-main p, .tel-number {line-height: 1.4em;}
.sfnewsletterForm.sfSubscribe ol.sfnewsletterFieldsList li.sfnewsletterField input, .dataTables_filter input[type=search] {font-weight: normal;}
.sfnewsletterForm.sfSubscribe ol.sfnewsletterFieldsList {margin-bottom:0px;}
.footer-wrapper-main h2, .footer-wrapper-main li > a, .office-link {font-family: 'Source Sans Pro', sans-serif;}
.footer-navs .link-holder a {color: #8C1D40;font-size: 12px;}
.btn-default, a.btn-default{color: #8C1D40;}
.btn-default{border: 2px solid #8C1D40;border-radius: 1px; -webkit-border-radius: 1px;}
.btn-default:hover{border: 2px solid #8C1D40;border-radius: 1px; -webkit-border-radius: 1px;color: #fff; background-color: #8C1D40;-webkit-transition: background-color 1s;transition: background-color 1s;}
.btn-default:hover, .btn:hover {padding:6px 12px;}
</style>

<!-- JIM'S CODE START -->
<div class="container clearfix"> 
    <div class="row">
        <!-- This Div will be invisible if the type is FIRM-->
        <% if(itemType == "f") { %>
   
            <div class="col-xs-12 col-md-8 col-md-push-2">
                <div id="firm-listing">
                    <div class="row">
			<div class="title-holder"><h1><span id="baseTemplatePlaceholder_content_C033_ctl00_ctl00_MessageLabel"><asp:Label ID="lblFirmName" runat="server" Text="" /></span></h1></div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12"><h2><asp:Label ID="liblc" runat="server" Text="" /></h2></div>             
                    </div>
  <div class ="row">   
          
      <div class="">
        <button id="bp" type="button" class="col-xs-12 btn btn-default" data-toggle="collapse" runat="server" data-target="#demo">
            <span class="glyphicon glyphicon-collapse-down"></span> View Principals
        </button>
        </div>
       <div class="col-xs-12 principals">
          <div id="demo" class="collapse">
                            <asp:Repeater ID="R1" runat="server">
                                            <ItemTemplate>                                                                    
                                                <%# Eval("pname") %> <br />
                                            </ItemTemplate>
                                        </asp:Repeater> 
          </div>

          </div>  
  </div>                       
<%--                    <div class="row">
                        <div class="col-xs-4"> <strong><asp:Label ID="ltradingname" runat="server" Text="Trading Names : "></asp:Label></strong></div>
                        <div class="col-xs-8"><asp:Label ID="rtradingname" runat="server" Text="" /></div>
                    </div>--%>
                    <div class="row">
                        <div class="col-xs-4"> <strong><asp:Label ID="ltradingname" runat="server" Text="Trading Names : "></asp:Label></strong></div>
                        <div class="col-xs-8"><asp:Repeater ID="R3" runat="server">
                                            <ItemTemplate>                                                                    
                                            <%# Eval("TradingName") %> <br />
                                            </ItemTemplate>
                                        </asp:Repeater> </div>

<%--                                             <div class="text-center">
                                    <div class="col-xs-12"><span class="glyphicon glyphicon-option-horizontal"></span> </div>
                     </div>--%>

                    </div>


                    <div class ="row">
                        
                                <div class="text-center">                                                                     
                                      <b class="col-xs-12" > <asp:Label ID="lblfirmm" runat="server" Text="" /></b>                                  
                                </div>
                     </div>
                    <div class="row">
                        <div class="col-xs-4"><span class="glyphicon glyphicon-home"></span> <strong>Address :</strong></div>
                        <div class="col-xs-8">
                            <asp:Label ID="lblAddressLine1" runat="server" Text="" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4"><span class="glyphicon glyphicon-phone-alt"></span> <strong>Phone:</strong></div>
                        <div class="col-xs-8"><asp:Label ID="lblPhone" runat="server" Text="Phone Number" /></div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4"><span class="glyphicon glyphicon-print"></span> <strong>Fax:</strong></div>
                        <div class="col-xs-8"><asp:Label ID="lblFax" runat="server" Text="Fax Number" /></div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4"><span class="glyphicon glyphicon-envelope"></span> <strong>Main Email:</strong></div>
                        <div class="col-xs-8"><a href="" class="emailadd"><asp:Label ID="lblMainEmail" runat="server" Text="" class="emaillabel"/></a></div>
                    </div>
                 <%--
                    <div class="row">
                        <div class="col-xs-4"><span class="glyphicon glyphicon-envelope"></span> <strong>Info Email:</strong></div>
                        <div class="col-xs-8"><a href="" class="emailadd"><asp:Label ID="lblInfoEmail" runat="server" Text="" class="emaillabel"/></a></div>
                    </div>
    <div class="row">
                        <div class="col-xs-4"><span class="glyphicon glyphicon-envelope"></span> <strong>Jobs Email:</strong></div>
                        <div class="col-xs-8"><a href="" class="emailadd"><asp:Label ID="lblJobsEmail" runat="server" Text="" class="emaillabel"/></a></div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4"><span class="glyphicon glyphicon-envelope"></span> <strong>Training Email:</strong></div>
                        <div class="col-xs-8"><a href="" class="emailadd"><asp:Label ID="lblTrainingEmail" runat="server" Text="" class="emaillabel"/></a></div>
                    </div>--%>
                    <div class="row">
                        <div class="col-xs-4"><span class="glyphicon glyphicon-link"></span> <strong>Website:</strong></div>
                        <div class="col-xs-8"><a href="" target="_blank" class="websiteadd"><asp:Label ID="lblWebsite" runat="server" Text="" class="websitelabel"/></a></div>
                    </div>                               
                </div>
                <br />
                    <asp:Repeater ID="R2" runat="server">                 
                        <ItemTemplate>
                            <div class ="row">
                                <div class=" text-center">
                                     <b class="col-xs-12" ><%# Eval("firmName") %></b> <br />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-4"><span class="glyphicon glyphicon-home"></span> <strong>Address :</strong></div>
                                <div class="col-xs-8">
                                     <%# Eval("add") %> <br />
                                </div>
                            </div>                                                              
                    <div class="row">
                        <div class="col-xs-4"><span class="glyphicon glyphicon-phone-alt"></span> <strong>Phone:</strong></div>
                        <div class="col-xs-8">  <%# Eval("areacode") + " "+ Eval("phoneno") %> <br /> </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4"><span class="glyphicon glyphicon-print"></span> <strong>Fax:</strong></div>
                        <div class="col-xs-8"> <%# Eval("faxno") %> <br /></div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4"><span class="glyphicon glyphicon-envelope"></span> <strong>Email:</strong></div>
                        <div class="col-xs-8"><a href="mailto:#"> <%# Eval("mainemail") %> <br /></a></div>
                    </div>
                    <div class ="row">
                                <div class=" text-center">
                                    <div class="col-xs-12"><span class="glyphicon glyphicon-option-horizontal"></span> </div>
                                </div>
                    </div>
                            <br />
                        </ItemTemplate>
                        </asp:Repeater> 
            </div>



        <% } %>

        <!-- This Div will be invisible if the type is MEMBER-->
        <% if(itemType == "m") { %>
            <div class="col-xs-12 col-md-6 col-md-push-3">
                <div id="member-listing">
                    <div class="row">
                        <h1 class="col-xs-12"><asp:Label ID="lblMemberFullName" runat="server" Text="Member Name" /></h1>
                    </div>
                    <div class="row">
                        <div class="col-xs-4"><span class="glyphicon glyphicon-time"></span> <strong>Member since:</strong></div>
                        <div class="col-xs-8"><asp:Label ID="lblMemberJoinDate" runat="server" Text="Member Join Date" /></div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4"><span class="glyphicon glyphicon-briefcase"></span> <strong>Company:</strong></div>
                        <div class="col-xs-8"><asp:Label ID="lblMemberCompany" runat="server" Text="Member Company" /></div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4"><span class="glyphicon glyphicon-envelope"></span> <strong>Email:</strong></div>
                        <div class="col-xs-8"><asp:Label ID="lblMemberEmail" runat="server" Text="Member Email" /></div>
                    </div>
                </div>
            </div>
        <% } %>

        <asp:Label id="lblError" ForeColor="Red" runat="server" Visible="False"  />

    </div>
    <div class="row" style="margin-bottom:80px;">
        <div class="cai-marg-top-30">
            <a  id ="jb1" class ="col-xs-12 col-md-8 col-md-push-2 btn btn-default" href="javascript:history.back(1)"><i class="fa fa-arrow-left" aria-hidden="true"></i> Back to Listings</a>
        </div>
    </div>
  <%--  <a href='javascript:history.go(-1)'>Go Back to Previous Page</a> <br />
    <a href="#" onclick="window.history.back();return false;">Back</a> <br />
    <a href="#" onclick="history.go(-1);return false;" style="text-decoration:underline;">Back</a> <br />--%>
</div> 
<!-- JIM'S CODE END -->
<!-- SUE'S SCRIPT START -->
<script>
$(document).ready(function () {
	$("a.emailadd").each(function () {
		var $this = $(this);
		var x = $(this).children('span.emaillabel').text().trim();
		var emailaddurl='mailto:'+ x + '?subject=Chartered%20Accountants%20Ireland%20Firms%20Directory';
		$(this).attr('href', emailaddurl);
	});
	$("a.websiteadd").each(function () {
		var $this = $(this);
		var x = $(this).children('span.websitelabel').text().trim();
		var y = "http://";
		if (x.indexOf('http://') >= 0) {
			var y = "";
		}
		else if (x.indexOf('https://') >= 0) {
			var y = "";
		}
		var websiteurl= y + x;
		$(this).attr('href', websiteurl);
	});
});	
</script>
<!-- SUE'S SCRIPT END -->
