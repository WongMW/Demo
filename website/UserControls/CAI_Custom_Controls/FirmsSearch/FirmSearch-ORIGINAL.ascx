<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FirmSearch.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.Jim_UserControls.FirmDirectory.FirmSearch" %>
<%--<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>--%>

    <link rel='stylesheet' href="https://cdn.datatables.net/1.10.12/css/jquery.dataTables.min.css"/>
    <link rel ='stylesheet'href="https://cdn.datatables.net/plug-ins/1.10.12/features/searchHighlight/dataTables.searchHighlight.css" />

<%-- bootstrap  --%>
 <!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous"/> 
<!-- Optional theme -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css" integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp" crossorigin="anonymous"/>
    
   <%-- <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.0/jquery.min.js">
    <script src="https://cdn.datatables.net/1.10.12/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/plug-ins/1.10.12/features/searchHighlight/dataTables.searchHighlight.min.js"></script>
    <script src="https://bartaz.github.io/sandbox.js/jquery.highlight.js"></script>--%>



<script type="text/javascript">
     $(document).ready(function () {
         $.ajax({
             //  asmx service to retrive the  data from server side
             url: '<%= ResolveUrl("FirmDetailService.asmx/GetMemberDetails")%>',
            method: 'post',
            dataType: 'json',
            success: function (data) {
                $('#example1').DataTable({
                    searchHighlight: true,
                    lengthChange: false,
                    search: {
                        smart: false
                    },
                    language: {
                        searchPlaceholder: "Search records"
                    },
                    data: data,
                    'columns': [
                                 { 'data': 'MId', 'visible': false },
                                 {
                                     'data': 'MemberName',
                                     //  'render': function (data, type, row, meta) {
                                     //      var idp = row['MId'];                                   
                                     //      return '<a target="_blank" href="FirmsDeatils.aspx?type=m&id=' + idp +'">' + data + '</a>';

                                     //}                        
                                 },
                                 { 'data': 'FId', 'visible': false },
                                 {
                                     'data': 'FirmName',
                                     'render': function (data, type, row, meta) {
                                         var idc = row['FId'];
                                         //return '<a target="_blank" href="FirmsDeatils.aspx?type=f&id=' + idc + '">' + data + '</a>';
                                         return '<a  href="FirmsDetails.aspx?type=f&id=' + idc + '">' + data + '</a>';
                                     }
                                 },
                                 { 'data': 'City' },
                                 { 'data': 'Country' },
                    ]
                }
                );
            }
        });
        // Firm Member
       $.ajax({
            //  asmx service to retrive the  data from server side
            url: '<%= ResolveUrl("FirmDetailService.asmx/GetFirmDetails")%>',
            method: 'post',
            dataType: 'json',
            success: function (data) {
                $('#example2').DataTable({
                    searchHighlight: true,
                    lengthChange: false,
                    search: {
                        smart: false
                    },
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
                                         //  return '<a target="_blank" href="FirmsDeatils.aspx?type=f&id=' + idc + '">' + data + '</a>';
                                         return '<a  href="FirmsDetails.aspx?type=f&id=' + idc + '">' + data + '</a>';
                                     }
                                 },
                                 { 'data': 'AUD', 'sortable': false },
                                 { 'data': 'DPB', 'sortable': false },
                                 { 'data': 'IB', 'sortable': false },
                                 { 'data': 'City' },
                                 { 'data': 'Country' },
                    ]
                }
                     );
            }
        });
    
    });
    </script>

<%--<div class="panel panel-default" style="width: 100%; padding: 10px; margin: 10px">--%>
<div class=" text-center">
<h1>Find a Firm/Member</h1>
</div>
    <div id="Tabs" role="tabpanel">
        <!-- Nav tabs -->
        <ul class="nav nav-tabs" role="tablist">
            <li class="active"><a href="#member" aria-controls="member" role="tab" data-toggle="tab">Member Directory</a></li>
            <li><a href="#firm" aria-controls="firm" role="tab" data-toggle="tab">Firm Directory</a></li>
        </ul>
        <!-- Tab panes -->
        <div class="tab-content" style="padding-top: 20px">
            <div role="tabpanel" class="tab-pane active" id="member">
               
                <table id="example1" class="display"  cellspacing="0" width="100%">
                     <thead>
                            <tr>
                                <th>MId</th>
                                <th>MemberName</th>
                                <th>FId</th>
                                <th>FirmName</th>
                                <th>City</th>
                                <th>Country</th>
                            </tr>
                     </thead>
              </table>
        </div>
            <div role="tabpanel" class="tab-pane" id="firm">
                 <div class="row" >
                    <div class="col-sm-4" style ="padding-left :10px" ><b>AUD</b> = Auditor  <b>DPB</b> = Designated Professional Body<b>IB</b> = Investment Business</div>
                </div>
                             <table id="example2" class="display"  cellspacing="0" width="100%">
                                <thead>
                                    <tr>
                                        <th>FId</th>
                                        <th>FirmName</th>
                                        <th>AUD</th>
                                        <th>DPB</th>
                                        <th>IB</th>
                                        <th>City</th>
                                        <th>Country</th>                  
                                    </tr>
                                </thead>
                            </table>
            </div>

        </div>
    </div>
<%--</div>--%>