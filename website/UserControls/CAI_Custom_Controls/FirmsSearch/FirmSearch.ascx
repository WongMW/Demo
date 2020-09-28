<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FirmSearch.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.FirmsSearch.FirmSearch" %>
<%--<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>--%>

   <link rel='stylesheet' href="https://cdn.datatables.net/1.10.12/css/jquery.dataTables.min.css"/>
	<!-- comment out to rid of table width styles issue -->
    <link rel ='stylesheet'href="https://cdn.datatables.net/plug-ins/1.10.12/features/searchHighlight/dataTables.searchHighlight.css" />

<%-- bootstrap  --%>
 <!-- Latest compiled and minified CSS -->

<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous"/>
<!-- Optional theme -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css" integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp" crossorigin="anonymous"/>  
<link href="../../../CSS/custom.min.css" rel="stylesheet" />


<style>
#Tabs{margin-bottom: 80px;}
.dataTables_filter{margin: 0 auto;text-align: center !important;}
.dataTables_filter label{width: 100%;padding: 10px 0px;font-weight: 600;font-size: 16px;}
.dataTables_filter input[type=search]{width: 86%; height: 42px; margin-bottom: 10px;border: 1px solid #ccc; padding: 0 2%}
.table-div {margin: 0 auto;}
.table-div table.dataTable {margin: 0 5%;width:90% !important;}
.nav-tabs>li.active>a,.nav-tabs>li.active>a:focus,.nav-tabs>li.active>a:hover{background-color:#ccc}
.nav.nav-tabs > li.active > a,
.dataTables_filter label,
.tab-content table th,.dataTables_wrapper .dataTables_info, 
.table-div .dataTables_wrapper .dataTables_paginate .paginate_button,
.dataTables_wrapper .dataTables_paginate .paginate_button.disabled:hover,
.dataTables_wrapper .dataTables_paginate .ellipsis,
.firmpractype {color:#FFF !important;}
.dataTables_filter input[type=search], .table-div .dataTables_wrapper .dataTables_paginate .paginate_button.current{color:#666 !important;}
.nav.nav-tabs > li.active > a, .tab-content{background-color:#8C1D40;}
.dataTables_info, #example1_paginate, #example2_paginate, #example3_paginate {font-weight: bold;width: 90%;margin:0 5%;}
#example1_paginate, #example2_paginate, #example3_paginate {padding-bottom: 20px !important;}
.show {display: block;}
.hide {display: none;}
<!-- FIX TABLE STYLING -->
table.dataTable th .sorting {background-image: url(https://cdn.datatables.net/1.10.12/images/sort_both.png);}
table.dataTable thead .sorting, table.dataTable thead .sorting_asc, table.dataTable thead .sorting_desc, table.dataTable thead .sorting_asc_disabled, table.dataTable thead .sorting_desc_disabled {
    background-repeat: no-repeat;
    background-position: center right;}
table.dataTable thead .sorting, table.dataTable thead .sorting_asc, table.dataTable thead .sorting_desc {
    cursor: pointer;}
table.dataTable thead th, table.dataTable tfoot th {
    font-weight: bold;}
table.dataTable, table.dataTable th, table.dataTable td {
    -webkit-box-sizing: content-box;
    box-sizing: content-box;}
table.dataTable th, table.dataTable td {
padding: 10px 18px !important;
    border-bottom: 1px solid #111;}

table.dataTable.stripe tbody tr.odd, table.dataTable.display tbody tr.odd {
    background-color: #f9f9f9;}
table.dataTable.row-border tbody tr:first-child th, table.dataTable.row-border tbody tr:first-child td, table.dataTable.display tbody tr:first-child th, table.dataTable.display tbody tr:first-child td {
    border-top: none;}

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
</style>

<div class="raDiv" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" class="ProcessingIndicator" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing">
                <img src="/Images/CAITheme/bx_loader.gif" />
                <span>Loading</span>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<%--<div class="panel panel-default" style="width: 100%; padding: 10px; margin: 10px">--%>
<div class="title-holder"><h1><span id="baseTemplatePlaceholder_content_C033_ctl00_ctl00_MessageLabel">Find a firm/member/member in practice</span></h1></div>

    <div id="Tabs" role="tabpanel">
        <!-- Nav tabs -->
        <ul class="nav nav-tabs" role="tablist">
            <li class="active"><a href="#firm" aria-controls="firm" role="tab" data-toggle="tab" class="firmdirectorylink triggerbookmark" onclick="myFunction(this);">Firms directory</a></li>
            <li><a href="#member" aria-controls="member" role="tab" data-toggle="tab" class="memberdirectorylink triggerbookmark" onclick="myFunction(this);">Members directory</a></li>
            <li><a href="#prac" aria-controls="prac" role="tab" data-toggle="tab" class="membersinpracticelink triggerbookmark" onclick="myFunction(this);">Members in practice directory</a></li>

        </ul>
        <!-- Tab panes -->
        <div class="tab-content" style="padding-top: 20px">
            <!-- firm Directory  -->
             <div role="tabpanel" class="tab-pane active" id="firm">
                 <div class="row" >
					<div class="col-xs-10 col-xs-push-1"><p class="info-note firm-text">use this tab to <strong>find a firm in your area</strong></p></div>		
                    <div class="col-xs-10 col-xs-push-1 firmpractype"><b>AUD</b> = Auditor<br /><b>DPB</b> = Designated Professional Body<br /><b>IB</b> = Investment Business</div>
                </div>
			<div class="table-div">
                             <table id="example1" class="display"  cellspacing="0" width="100%">
                                <thead>
                                    <tr>
                                        <th>FId</th>
                                        <th style="width:45%">Firm name</th>
                                        <th style="width:9%">AUD</th>
                                        <th style="width:9%">DPB</th>
                                        <th style="width:9%">IB</th>
                                        <th style="width:14%">City/town</th>
                                        <th style="width:14%">Country</th>                  
                                    </tr>
                                </thead>
                            </table>
			</div>
            </div>
            <!-- member Directory  -->
            <div role="tabpanel" class="tab-pane active" id="member">
				<div class="row" >
					<div class="col-xs-10 col-xs-push-1"><p class="info-note mem-text">use this tab to <strong>find out if an individual is a member</strong> of Chartered Accountants Ireland</p></div>
				</div>
               <div class="table-div">
                	<table id="example2" class="display"  cellspacing="0" width="100%">
                     	<thead>
                            <tr>
                                <th>MId</th>
                                <th style="width:20%">Member Name</th>
                                <th>FId</th>							
                                <th style="width:25%">Firm Name</th>
								<th style="width:25%">Year of admission</th>
                                <th style="width:10%">City/town</th>
                                <th style="width:18%">Country</th>
                            </tr>
                     	</thead>
              		</table>
               </div>
        </div>
        <!-- members in practice -->
         <div role="tabpanel" class="tab-pane active" id="prac">
                 <div class="row" >
					<div class="col-xs-10 col-xs-push-1"><p class="info-note prac-text">use this tab to <strong>find a Chartered Accountants Ireland member in practice</strong></p></div>
                    <div class="col-xs-10 col-xs-push-1 firmpractype"><b>ILC</b> = Insolvency Licence Holders (GB/NI) <br /><b>IPC</b> = Insolvency Practising Certificate Holders (ROI) <br /><b>PC</b> = Practising Certificate</div>
                </div>
			<div class="table-div">
                             <table id="example3" class="display"  cellspacing="0" width="100%">
                                <thead>
                                    <tr>
                                        <th>FId</th>
                                        <th>MId </th>
                                        <th style="width:35%">Member name</th>
                                        <th style="width:12%">PC</th>
                                        <th style="width:12%">IPC</th>
                                        <th style="width:12%">ILC</th>                                       
                                        <th style="width:24%">Firm name</th>                  
                                    </tr>
                                </thead>
                            </table>
			</div>
            </div>
        </div>
    </div>
<%--</div>--%>
<script type="text/javascript">
     $(document).ready(function () {
         $('#updateProcessingIndicator').show(); // Show loading

         // Firm Directory
         $.ajax({
             //  asmx service to retrive the  data from server side
             url: '<%= ResolveUrl("FirmDetailService.asmx/GetFirmDetails")%>',
             method: 'post',
             dataType: 'json',
             success: function (data) {
                 $('#example1').DataTable({
                     "info":     false,
                     "order": [[1, "asc"]],
                     searchHighlight: true,
                     lengthChange: false,
                    // search: {
                    //     smart: false
                    // },
                     language: {
                         searchPlaceholder: "Search records"
                     },
                     data: data,
                     'columns': [
                                  { 'data': 'FId', 'visible': false },
                                  {
                                      'data': 'FirmName',
                                      'render': function (data, type, row, meta) {
                                          var idc = row['FId'];
                                          return '<a  href="about-us/FirmsDetails?type=f&id=' + idc + '">' + data + '</a>';
                                      }
                                  },
                                  { 'data': 'AUD',  },
                                  { 'data': 'DPB',  },
                                  { 'data': 'IB',  },
                                  { 'data': 'City' },
                                  { 'data': 'Country' },
                     ]
                 }
                      );
             }
         });


         // Member Directory
         $.ajax({
             //  asmx service to retrive the  data from server side
             url: '<%= ResolveUrl("FirmDetailService.asmx/GetMemberDetails")%>',
            method: 'post',
            dataType: 'json',
            success: function (data) {
                $('#example2').DataTable({
                    "info": false,
                    "order": [[1, "asc"]],
                    searchHighlight: true,
                    lengthChange: false,
                    //search: {
                    //    smart: false
                   // },
                    language: {
                        searchPlaceholder: "Search records"
                    },
                    data: data,
                    'columns': [
                                 { 'data': 'MId', 'visible': false },
                                 {
                                     'data': 'MemberName',                    
                                 },
                                 { 'data': 'FId', 'visible': false },
                                 {
                                     'data': 'FirmName'
                                     //'render': function (data, type, row, meta) {
                                     //    var idc = row['FId'];
                                     //    return '<a  href="about-us/FirmsDetails?type=f&id=' + idc + '">' + data + '</a>';
                                     //}
                                 }
								 ,
								 //Begin:#19895
								 { 'data': 'AdmittanceDate',
								  'render': function(data, type, full, meta){
												 if(type === 'display'){
													data = data.substr(6, 4);
												 }

												return data;
										}
								 },
								  //End:#19895
                                 { 'data': 'City' },
                                 { 'data': 'Country' },
                                
                    ]
                }
                );
            }
        });
        
    
         //  Member in Practice
     $.ajax({
             //  asmx service to retrive the  data from server side
             url: '<%= ResolveUrl("FirmDetailService.asmx/GetMemberPractice")%>',
             method: 'post',
             dataType: 'json',
             success: function (data) {
                 $('#example3').DataTable({
                     "info": false,
                     'order':[[ 2, 'asc']],
                     searchHighlight: true,
                     lengthChange: false,
                     //search: {
                     //    smart: false
                     //},
                     language: {
                         searchPlaceholder: "Search records"
                     },
                     data: data,
                     'columns': [
                                  { 'data': 'MId', 'visible': false },
                                  { 'data': 'FId', 'visible': false },
                                  { 'data': 'MemberName' },
                                  { 'data': 'PC', },
                                  { 'data': 'IPC', },
                                  { 'data': 'ILC', },
                                  { 'data': 'FirmName' },
                                  
                     ]
                 }
                      );
             }
         });






    });
    </script>

<script>
$(window).load(function(){
	$('html,body').scrollTop(0);
	var delay=2000; 
	setTimeout(function() {
	//if (	$('#firm').is(':visible')	) {$('#firm').addClass("hide");}
	    if ($('#member').is(':visible')) { $('#member').addClass("hide");}
	    if ($('#prac').is(':visible')) { $('#prac').addClass("hide");}
	}, delay);
	
	if (document.location.hash == 0)
	{
		document.location.hash = 'firm';
	}
});
$( document ).ready(function() {
	$('.ProcessingIndicator').addClass("show");
	var delay2=3000; 
	setTimeout(function() {
			$('.ProcessingIndicator').removeClass("show");
			$('.ProcessingIndicator').addClass("hide");
	}, delay2);

	// GET BOOKMARK VALUE
	var bookmark = location.href.split("#")[1];
	$('a.triggerbookmark').each(function() {
		var $this = $(this);
		if($this.attr("aria-controls") == bookmark)
		{
			$this.parent().siblings().removeClass("active");
			$this.parent().addClass("active");
		}
	});
});
$('a.memberdirectorylink').click(function() {
	$('#member').removeClass("hide");
	$('#member').addClass("show");
	$('#firm').removeClass("show");
	$('#firm').addClass("hide");
	$('#prac').removeClass("show");
	$('#prac').addClass("hide");
});
$('a.firmdirectorylink').click(function() {
	$('#member').removeClass("show");
	$('#member').addClass("hide");
	$('#firm').removeClass("hide");
	$('#firm').addClass("show");
	$('#prac').removeClass("show");
	$('#prac').addClass("hide");
});
$('a.membersinpracticelink').click(function () {
    $('#prac').removeClass("hide");
    $('#prac').addClass("show");
    $('#member').removeClass("show");
    $('#member').addClass("hide");
    $('#firm').removeClass("show");
    $('#firm').addClass("hide");
});

// UPDATE URL WHEN TRIGGER IS CLICKED
function myFunction(lnk) {
$('a.triggerbookmark').click(function() {
	var $this = $(this);
	var btnbookmark = $this.attr("aria-controls");
	var yScroll=document.body.scrollTop;
	document.location.hash = btnbookmark;
 	document.body.scrollTop=yScroll;
});
}

// ALLOWS FOR DRAGGING SCREEN WHEN TABLE IS TOO WIDE 
$.fn.attachDragger = function () {
    var attachment = false, lastPosition, position, difference;
    $($(this).selector).on("mousedown mouseup mousemove", function (e) {
        if (e.type == "mousedown") attachment = true, lastPosition = [e.clientX, e.clientY];
        if (e.type == "mouseup") attachment = false;
        if (e.type == "mousemove" && attachment == true) {
            position = [e.clientX, e.clientY];
            difference = [(position[0] - lastPosition[0]), (position[1] - lastPosition[1])];
            $(this).scrollLeft($(this).scrollLeft() - difference[1]);
            $(this).scrollTop($(this).scrollTop() - difference[0]);
            lastPosition = [e.clientX, e.clientY];
        }
    });
    $(window).on("mouseup", function () {
        attachment = false;
    });
}

$(document).ready(function () {
    $("#Tabs .tab-content table.dataTable").attachDragger();
});
</script>
