<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StaffPendingTrainingVacancies.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.TrainingVacancies.StaffPendingTrainingVacancies" %>
<%@ Register TagPrefix="uc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
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


<script type="text/javascript">  
      
         function  ShowDiv() {

                 $("#f1").hide();
                 $("#jt2").show();

         };

    </script>  
<style>
    .linkButton {
        color:#861F41 !important;
    }
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

.fvacancytype {
    min-height: 80px;
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


<%--end thank you jumbotron--%>
<div class="container-fluid bootstrap-style" id ="f1">        
                       <div class="row">
                       <h2><span id="baseTemplatePlaceholder_content_C007_ctl00_ctl00_MessageLabel">Training Vacancies pending approval</span></h2>
                        </div>
                   </div>
 
        <div class="row-fluid" id="table-list" >
                <form  id="user-list"  class="form-horizontal" >
                   <div class="row">
                       <% if (this.Success)
                           {
                               this.Success = false; %>
                         <div class="col-lg-12">
                            <div class="alert alert-success alert-dismissible">
                                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                                <strong>Success!</strong> <asp:Label runat="server" ID="lblsuccess">  Changes saved with exit. </asp:Label>
                            </div>
                        </div>
                       <% } %>
                       <% if (this.Error)
                           { 
                               this.Error = false;
                               %>
                        <div class="col-lg-12">
                            <div class="alert alert-danger alert-dismissible">
                                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                                <strong>Error!</strong>  <asp:Label runat="server" ID="lblerror"> We found an error please try it in some minutes. </asp:Label> 
                            </div>
                        </div>
                       <% } %>
                    </div> 
                   <div class="row">
                       <% if (TrainingVacancies.Count == 0) { %>
                       <div class="col-lg-12" style="text-align:center;">
                            <h4> No Traning Vacancies.</h4>
                       </div>
                       <% } else { %>
                        <div class="col-lg-12">
                           <table class="table  table-striped" style="padding-top:10px;padding-bottom:10px;">
                               <thead class="thead-light">
                                   <tr>
                                       <th scope="col"> TV ID </th>                                       
                                       <th scope="col"> Company Name </th>
                                       <th scope="col"> Job Title </th>
                                       <th scope="col"> Creation </th>                                     
                                       <th scope="col"> Last Update </th>
                                       <th scope="col" style="display:none"> Draft </th>
                                       <th scope="col"> Web Status </th>
                                       <th scope="col" style="max-width:60px;"> Actions </th>
                                   </tr>
                               </thead>
                               <tbody>
                                <asp:Repeater runat="server" id="repeaterTV">
                                  <ItemTemplate>
                                   <tr>
                                       <td class="row"><%#Eval("TVID") %> </td>
                                       <td><%#Eval("TVcompanyName") %> </td>
                                       <td> <%#Eval("TVJobTitle") %></td>
                                       <td> <%#Eval("TVCreationDate") %></td>
                                       <td> <%#Eval("TVLastUpdateDate") %></td>
                                       <td style="display:none"> <%#Eval("TVIsDraft") %></td>
                                       <td> <span class="tv-web-status"><%#Eval("TVWebStatus") %></span></td>
                                       <td style="max-width:50px;" class="action-icons">     
                                           <%--<asp:LinkButton runat="server" title="Live" CommandName='TVID' CommandArgument='<%# Eval("TVID") %>' OnClick="btnApprove_Click"><i class="fas fa-smile"></i></asp:LinkButton>--%>                                                                       
                                           <asp:LinkButton runat="server" Visible='<%# (Eval("TVWebStatus").ToString() == "Submitted" ) %>'  title="Approve this listing" CommandName='TVID' CommandArgument='<%# Eval("TVID") %>' OnClick="btnApprove_Click"><i class="fas fa-smile"></i></asp:LinkButton>
                                           <asp:LinkButton runat="server" title="Reject" CommandName='TVID' CommandArgument='<%# Eval("TVID") %>' OnClick="btnReject_Click"><i class="fas fa-frown"></i></asp:LinkButton> 
                                           <%--<asp:LinkButton runat="server" Visible='<%# (Eval("TVWebStatus").ToString() == "Expired" ) %>'  title="Reject this listing" CommandName='TVID' CommandArgument='<%# Eval("TVID") %>' OnClick="btnReject_Click"><i class="fas fa-frown"></i></asp:LinkButton>--%>
                                       </td>
                                   </tr>
                                    </ItemTemplate>
                                </asp:Repeater>   
                               </tbody>
                           </table>
                       </div>
                       <% } %>
                       
                   </div>  
                   <br />   

                </form>


            </div>
    </div>
<uc1:User ID="User1" runat="server"></uc1:User>
<script>
    //Susan Wong, assign colors to status'
    function pageLoad() {
        var delay = 10;
        setTimeout(function () {
            //Susan Wong, assign colors to status'
            $('.tv-web-status').each(function () {
                var webStatus = $(this).text();
                if (webStatus.indexOf('Draft') > -1) {
                    $(this).css('background-color', '#235ed6');
                }
                else if (webStatus.indexOf('Submitted') > -1) {
                    $(this).css('background-color', '#f68e14');
                }
                else if (webStatus.indexOf('Live') > -1) {
                    $(this).css('background-color', '#73ba36');
                }
                else if (webStatus.indexOf('Rejected') > -1) {
                    $(this).css('background-color', '#fa0606');
                }
                else if (webStatus.indexOf('Expired') > -1) {
                    $(this).css('background-color', '#727272');
                }
                else if (webStatus.indexOf('Closed') > -1) {
                    $(this).css('background-color', '#000');
                }
            });
        }, delay);
    };
</script>



