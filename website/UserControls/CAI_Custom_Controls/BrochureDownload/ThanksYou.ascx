<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ThanksYou.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.BrochureDownload.ThanksYou" %>
<link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet"/>
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css">
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>


<%--<!-- Font Awesome -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
<!-- Bootstrap core CSS -->
<
<link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.0.0/css/bootstrap.min.css" rel="stylesheet">
<!-- Material Design Bootstrap -->
<link href="https://cdnjs.cloudflare.com/ajax/libs/mdbootstrap/4.5.0/css/mdb.min.css" rel="stylesheet">

<!-- JQuery -->
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
<!-- Bootstrap tooltips -->
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.13.0/umd/popper.min.js"></script>
<!-- Bootstrap core JavaScript -->
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.0.0/js/bootstrap.min.js"></script>
<!-- MDB core JavaScript -->
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/mdbootstrap/4.5.0/js/mdb.min.js"></script>--%>



<style>
.text-center {
    text-align: center;
}
.icon-circle .fab{
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

.icon-circle  .fab:hover{
    
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


 .icon-circle .fab{
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

.icon-circle  .fab:hover{
    
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

.ifacebook .fab{
    color               :#3B5998;
    border              :2px solid #3B5998;
}

.ifacebook .fab:hover{
    
    background-color        : #3B5998;    
    color                   : #fff;
    border                  : 1px solid #3B5998;

}

.itwittter .fab{
    color               : #33ccff;
    border              :2px solid #33ccff;
}

.itwittter .fab:hover{
    
    background-color        : #33ccff;    
    color                   : #fff;
    border                  : 1px solid #33ccff;

}

.iFlickr .fab{
    color               : #ff0281;
    border              :2px solid #ff0281;
}

.iFlickr .fab:hover{
    
    background-color        : #ff0281;    
    color                   : #fff;
    border                  : 1px solid #ff0281;

}

.iLinkedin .fab{
    color               : #007bb7;
    border              :2px solid #007bb7;
}

.iLinkedin .fab:hover{
    
    background-color        :#007bb7;    
    color                   : #fff;
    border                  : 1px solid #007bb7;

}

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

<div class="jumbotron text-xs-center bootstrap-style" id="jt1"  >

    <div>
  <h1 class="display-3">Thank You!</h1>
        <p><strong>You can download our Chartered Accountants Ireland brochure by clicking on the button below.</strong></p>
         <asp:LinkButton class="submitBtn big" id="Link" Text="Download broucher"  OnClick="DownloadButton_Click" runat="server">
          <span class="glyphicon glyphicon-save"></span> Download Brochure
        </asp:LinkButton>
      
  <hr />
  <p class="lead" style="display:none"><strong>Custom text goes here </strong>  Custom text goes here.</p>

   <p>Have a minute ? Help us share the love! Follow us on Twitter and like us on Facebook to keep you up to date with all our news and announcements.</p>


   <div class="container-fluid">
    <div class="row ">
		<div class="sicon">
			<div class="col-lg-2 col-md-2 col-sm-2 col-xs-3 text-center">
				<div class="icon-circle">
					<a href="https://www.facebook.com/CharteredAccountantsIreland" class="ifacebook" title="Facebook" target="_blank"><i class="fab fa-facebook-f"></i></a>
				</div>
			</div>
			<div class="col-lg-2 col-md-2 col-sm-2 col-xs-3 text-center">
				<div class="icon-circle">
					<a href="https://twitter.com/charteredaccirl" class="itwittter" title="Twitter" target="_blank"><i class="fab fa-twitter"></i></a>
				</div>
			</div>
			<div class="col-lg-2 col-md-2 col-sm-2 col-xs-3 text-center">
				<div class="icon-circle">
					<a href="https://www.flickr.com/photos/irishcharteredaccountants" class="iFlickr" title="Flickr" target="_blank"><i class="fab fa-flickr"></i></a>
				</div>
			</div>
			<div class="col-lg-2 col-md-2 col-sm-2 col-xs-3 text-center">
				<div class="icon-circle">
					<a href="https://www.linkedin.com/groups/1783368/profile" class="iLinkedin" title="Linkedin" target="_blank"><i class="fab fa-linkedin"></i></a>
				</div>
			</div>
		</div>
	</div>
</div> 
 <div style="display:none">
 <!--Facebook-->
<button type="button" class="btn btn-fb"><i class="fa fa-facebook pr-1"></i> Facebook</button>
<!--Twitter-->
<button type="button" class="btn btn-tw"><i class="fa fa-twitter pr-1"></i> Twitter</button>
<!--Google +-->
<button type="button" class="btn btn-gplus"><i class="fa fa-google-plus pr-1"></i> Google +</button>
<!--Linkedin-->
<button type="button" class="btn btn-li"><i class="fa fa-linkedin pr-1"></i> Linkedin</button>
     </div>
  <hr>
  <p style="display:none">
    Having trouble? <a href="">Contact us</a>
  </p>
  <p class="lead" style="display:none">
    <a class="btn btn-primary btn-sm" href="https://www.charteredaccountants.ie/" role="button">Continue to homepage</a>
  </p>
        </div>
</div>
