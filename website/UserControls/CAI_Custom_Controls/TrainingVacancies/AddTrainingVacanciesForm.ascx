<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddTrainingVacanciesForm.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.TrainingVacancies.AddTrainingVacanciesForm" %>
<%@ Register TagPrefix="uc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<!-- Bootstrap CSS -->
<link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
<!-- BootstrapValidator CSS -->
<link rel="stylesheet" href="//cdn.jsdelivr.net/jquery.bootstrapvalidator/0.5.0/css/bootstrapValidator.min.css" />
<link rel="stylesheet" href="https://code.jquery.com/ui/1.12.0/themes/base/jquery-ui.css">
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery.sumoselect/3.0.2/sumoselect.min.css">
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.sumoselect/3.0.2/jquery.sumoselect.min.js"></script>
<!-- jQuery and Bootstrap JS -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
<!-- BootstrapValidator JS -->
<script type="text/javascript" src="https://cdn.jsdelivr.net/jquery.bootstrapvalidator/0.5.0/js/bootstrapValidator.min.js"></script>
<script src="https://unpkg.com/gijgo@1.9.13/js/gijgo.min.js" type="text/javascript"></script>
<link href="https://unpkg.com/gijgo@1.9.13/css/gijgo.min.css" rel="stylesheet" type="text/css" />


<script type="text/javascript">  

    //WongS, Ticket #20924
    //Remove small pill and selected option when you "x" on small pills
    function removeFunction(ab) {
        var c = ab.getAttribute('title');
        ab.remove();
        $('.SumoSelect ul.options li.selected label').each(function () {
            var d = $(this).text()
            if (d == c) {
                $(this).parent().click();
            }
        });
    };
      
    $(document).ready(function () {
      
        $('.cancelBtn').click(function(event){                   
                 var bootstrapValidator = $("#form1").data('bootstrapValidator');
                 bootstrapValidator.destroy();
                 $("#form1").data('bootstrapValidator',null);
                 $('#form1').submit();
             })
             $('#<%= btnSave.ClientID %>').click(function(event){
                 $('#form1').bootstrapValidator({
                     // To use feedback icons, ensure that you use Bootstrap v3.1.0 or later
                     feedbackIcons: {
                         valid: 'glyphicon glyphicon-ok',
                         invalid: 'glyphicon glyphicon-remove',
                         validating: 'glyphicon glyphicon-refresh',
                         submitButtons: 'button[class="submitBtn"]',
                     },
                     fields: {
                         <%=fcname.UniqueID%>: {
                             validators: {
                                 notEmpty: {
                                     message: 'Company name is required'
                                 },                             
                             }
                         },
                         <%=fdescription.UniqueID%>: {
                             validators: {
                                 notEmpty: {
                                     message: 'Company description is required'
                                 },
                                 stringLength: {
                                     max: 500,
                                     message: 'The Company description should be less than 500 characters'
                                 }
                             }
                         },
                         <%=fjobtitle.UniqueID%>: {
                             validators: {
                                 notEmpty: {
                                     message: 'Job Title is required'
                                 },
                                 stringLength: {
                                     max: 200,
                                     message: 'Job Title should be less than 200 characters'
                                 }
                             }
                         },
                         <%=fjobtown.UniqueID%>: {
                             validators: {
                                 notEmpty: {
                                     message: 'Job Title is required'
                                 },
                                 stringLength: {
                                     max: 50,
                                     message: 'Job Title should be less than 50 characters'
                                 }
                             }
                         },
                         <%=fjobcounty.UniqueID%>: {
                             validators: {
                                 notEmpty: {
                                     message: 'Job County is required'
                                 },
                                 stringLength: {
                                     max: 50,
                                     message: 'Job county should be less than 50 characters'
                                 }
                             }
                         },
                         <%=fvacancytype.UniqueID%>: {
                             validators: {
                                 notEmpty: {
                                     message: 'Company name is required'
                                 },                             
                             }
                         },
                         <%=ftrainintype.UniqueID%>: {
                             validators: {
                                 notEmpty: {
                                     message: 'Company name is required'
                                 },                             
                             }
                         },
                         <%=fdateclosing.UniqueID%>: {
                             validators: {
                                 notEmpty: {                                                   
                                     message: 'Date Closing is required'
                                 },                             
                             }
                         },
                         <%=fjobspec.UniqueID%>: {
                             validators: {
                                 notEmpty: {
                                     message: 'Job Spec is required'
                                 },
                                 stringLength: {
                                     max: 500,
                                     message: 'Job spec should be less than 500 characters'
                                 }
                             }
                         },
                         <%=fjobrequirements.UniqueID%>: {
                             validators: {
                                 notEmpty: {
                                     message: 'Job Requirements is required'
                                 },
                                 stringLength: {
                                     max: 500,
                                     message: 'Job Requirements should be less than 500 characters'
                                 }
                             }
                         },
                         <%=fhowtoapply.UniqueID%>: {
                             validators: {
                                 notEmpty: {
                                     message: 'How To Apply is required'
                                 },
                                 stringLength: {
                                     max: 300,
                                     message: 'How To Apply should be less than 300 characters'
                                 }
                             }
                         },
                     }
                 }).on('status.field.bv', function(e, data) {
                     if (data.bv.getSubmitButton()) {
                         data.bv.disableSubmitButtons(false);
                     }                     
                 });
             })

             $('#<%= btnEditSave.ClientID %>').click(function(event){
                 $('#form1').bootstrapValidator({
                     // To use feedback icons, ensure that you use Bootstrap v3.1.0 or later
                     feedbackIcons: {
                         valid: 'glyphicon glyphicon-ok',
                         invalid: 'glyphicon glyphicon-remove',
                         validating: 'glyphicon glyphicon-refresh',
                         submitButtons: 'button[class="submitBtn"]',
                     },
                     fields: {
                         <%=fcname.UniqueID%>: {
                             validators: {
                                 notEmpty: {
                                     message: 'Company name is required'
                                 },                             
                             }
                         },
                         <%=fdescription.UniqueID%>: {
                             validators: {
                                 notEmpty: {
                                     message: 'Company description is required'
                                 },
                                 stringLength: {
                                     max: 500,
                                     message: 'The Company description should be less than 500 characters'
                                 }
                             }
                         },
                         <%=fjobtitle.UniqueID%>: {
                             validators: {
                                 notEmpty: {
                                     message: 'Job Title is required'
                                 },
                                 stringLength: {
                                     max: 200,
                                     message: 'Job Title should be less than 200 characters'
                                 }
                             }
                         },
                         <%=fjobtown.UniqueID%>: {
                             validators: {
                                 notEmpty: {
                                     message: 'Job Title is required'
                                 },
                                 stringLength: {
                                     max: 50,
                                     message: 'Job Title should be less than 50 characters'
                                 }
                             }
                         },
                         <%=fjobcounty.UniqueID%>: {
                             validators: {
                                 notEmpty: {
                                     message: 'Job County is required'
                                 },
                                 stringLength: {
                                     max: 50,
                                     message: 'Job county should be less than 50 characters'
                                 }
                             }
                         },
                         <%=fvacancytype.UniqueID%>: {
                             validators: {
                                 notEmpty: {
                                     message: 'Company name is required'
                                 },                             
                             }
                         },
                         <%=ftrainintype.UniqueID%>: {
                             validators: {
                                 notEmpty: {
                                     message: 'Company name is required'
                                 },                             
                             }
                         },
                         <%=fdateclosing.UniqueID%>: {
                             validators: {
                                 notEmpty: {                                                   
                                     message: 'Date Closing is required'
                                 },                             
                             }
                         },
                         <%=fjobspec.UniqueID%>: {
                             validators: {
                                 notEmpty: {
                                     message: 'Job Spec is required'
                                 },
                                 stringLength: {
                                     max: 500,
                                     message: 'Job spec should be less than 500 characters'
                                 }
                             }
                         },
                         <%=fjobrequirements.UniqueID%>: {
                             validators: {
                                 notEmpty: {
                                     message: 'Job Requirements is required'
                                 },
                                 stringLength: {
                                     max: 500,
                                     message: 'Job Requirements should be less than 500 characters'
                                 }
                             }
                         },
                         <%=fhowtoapply.UniqueID%>: {
                             validators: {
                                 notEmpty: {
                                     message: 'How To Apply is required'
                                 },
                                 stringLength: {
                                     max: 300,
                                     message: 'How To Apply should be less than 300 characters'
                                 }
                             }
                         },
                     }
                 }).on('status.field.bv', function(e, data) {
                     if (data.bv.getSubmitButton()) {
                         data.bv.disableSubmitButtons(false);
                     }                     
                 });
             })
    
             $("#<%= fdateclosing.ClientID %>").datepicker({
                 format: 'dd/mm/yy',
                 uiLibrary: 'bootstrap',
                 select: function (e, type) {
                     if ($('#form1') && $('#form1').data('bootstrapValidator'))
                        $('#form1').data('bootstrapValidator').updateStatus('<%=fdateclosing.UniqueID%>', 'VALID', null)
                 }
             });
             $('#fcbothertext').attr('disabled','disabled');
             $(document).click(function () {
                 $(".SumoSelect").each(function () {
                     if ($(this).hasClass("show-it open")) {
                         $(this)
                             .removeClass("show-it open")
                             .addClass("hide-it");
                         $(this).siblings('.multiselect-pill')
                             .removeClass("hide-it")
                             .addClass("show-it");
                     }
                 });
             });
             $('.SlectBox').SumoSelect();
             $('.SumoSelect').addClass("hide-it");
        //WongS, Ticket #20924
        // Show multiselect when pill is clicked
             $('.multiselect-pill').click(function (event) {
                 $(this)
                     .addClass("hide-it")
                     .removeClass("show-it");
                 $(this).siblings('.SumoSelect')
                     .removeClass("hide-it")
                     .addClass("show-it open");
                 event.stopPropagation();
             });
        //WongS, Ticket #20924
        // Create small pill for each selected option
             $(".SlectBox").change(function () {
                 var arr = [];
                 i = 0;
                 $(this).closest('.row').siblings('.row').children('.pill-list').html("");
                 var chkSelectedItem = $(this).closest('.row').children('.col-lg-12').children('.SumoSelect').children('.multiple').children('.options').children('.selected'); //WongS, Ticket #20924
                 chkSelectedItem.each(function () {
                     var textValue = $(this).text();
                     arr[i++] = $(this).text();
                     $(this).parent('.options').parent('.multiple').parent('.SumoSelect').parent('.col-lg-12').parent('.row').siblings('.row').children('.pill-list').append('<span class="grey-pill" onclick="removeFunction(this);" title="' + textValue + '">' + textValue + '</span>'); //WongS, Ticket #20924
                 });
             });

             $(".SlectBox").change();

         });
</script>
<style>
    .btnShowForm {
        padding-top: 20px;
        padding-bottom: 20px;
    }

    .icon-circle .fa {
        font-size: 25px;
        color: #e84700;
        margin: 0 auto;
        height: 80px;
        width: 80px;
        border-radius: 50%;
        border: 2px solid #e84700;
        line-height: 80px;
        cursor: pointer;
        -webkit-transition: all ease-in-out 0.35s;
        -moz-transition: all ease-in-out 0.35s;
        -o-transition: all ease-in-out 0.35s;
        -ms-transition: all ease-in-out 0.35s;
        transition: all ease-in-out 0.20s,background-color ease-in-out 0.05s;
    }

        .icon-circle .fa:hover {
            background-color: #e84700;
            color: #fff;
            border: 1px solid #e84711;
            -moz-box-shadow: inset 0px 0px 0px 5px #ffffff;
            -o-box-shadow: inset 0px 0px 0px 5px #ffffff;
            -ms-box-shadow: inset 0px 0px 0px 5px #ffffff;
            -webkit-box-shadow: inset 0px 0px 0px 5px #ffffff;
            box-shadow: inset 0px 0px 0px 5px #ffffff;
            -ms-transform: scale(1.2,1.2);
            -webkit-transform: scale(1.2,1.2);
            -moz-transform: scale(1.2,1.2);
            -o-transform: scale(1.2,1.2);
            transform: scale(1.2,1.2);
        }

    .icon-circle i:before {
        margin-left: 0px;
        font-size: 40px;
    }

    @media (min-width:320px) and (max-width:768px) {


        .icon-circle .fa {
            font-size: 15px;
            color: #e84700;
            margin: 0 auto;
            height: 40px;
            width: 40px;
            border-radius: 50%;
            border: 2px solid #e84700;
            line-height: 40px;
            cursor: pointer;
            -webkit-transition: all ease-in-out 0.35s;
            -moz-transition: all ease-in-out 0.35s;
            -o-transition: all ease-in-out 0.35s;
            -ms-transition: all ease-in-out 0.35s;
            transition: all ease-in-out 0.20s,background-color ease-in-out 0.05s;
        }

            .icon-circle .fa:hover {
                background-color: #e84700;
                color: #fff;
                border: 1px solid #e84711;
                -moz-box-shadow: inset 0px 0px 0px 5px #ffffff;
                -o-box-shadow: inset 0px 0px 0px 5px #ffffff;
                -ms-box-shadow: inset 0px 0px 0px 5px #ffffff;
                -webkit-box-shadow: inset 0px 0px 0px 5px #ffffff;
                box-shadow: inset 0px 0px 0px 5px #ffffff;
                -ms-transform: scale(1.2,1.2);
                -webkit-transform: scale(1.2,1.2);
                -moz-transform: scale(1.2,1.2);
                -o-transform: scale(1.2,1.2);
                transform: scale(1.2,1.2);
            }

        .icon-circle i:before {
            margin-left: 0px;
            font-size: 20px;
        }
    }

    .ifacebook .fa {
        color: #3B5998;
        border: 2px solid #3B5998;
    }

        .ifacebook .fa:hover {
            background-color: #3B5998;
            color: #fff;
            border: 1px solid #3B5998;
        }

    .itwittter .fa {
        color: #33ccff;
        border: 2px solid #33ccff;
    }

        .itwittter .fa:hover {
            background-color: #33ccff;
            color: #fff;
            border: 1px solid #33ccff;
        }

    .iFlickr .fa {
        color: #ff0281;
        border: 2px solid #ff0281;
    }

        .iFlickr .fa:hover {
            background-color: #ff0281;
            color: #fff;
            border: 1px solid #ff0281;
        }

    .iLinkedin .fa {
        color: #007bb7;
        border: 2px solid #007bb7;
    }

        .iLinkedin .fa:hover {
            background-color: #007bb7;
            color: #fff;
            border: 1px solid #007bb7;
        }

    .cancelBtn {
        padding: 8px 20px;
        height: 40px;
        display: inline-block;
        text-transform: uppercase;
        background: #8C1D40;
        color: #fff;
        border: 2px solid transparent;
        margin-right: 5px;
    }

    .cb-item {
        min-height: 42px !important;
    }

    .cb-item-cb {
        margin-top: 5px;
    }

    .other-item {
        margin-top: 8px;
    }

    .cb-other {
        min-height: 42px !important;
        margin-top: 8px;
    }

    .cancelBtn:hover {
        background: #fff;
        color: #8C1D40;
        border: 2px solid #8C1D40;
        transition: background-color 1s;
        -moz-transition: background-color 1s;
        -webkit-transition: background-color 1s;
    }

    .fvacancytype {
        min-height: 80px;
    }
    <!-- ADDING STYLES OVERWRITTEN BY BOOTSTRAP STYLES -->
    .jumbotron {
        background-color: transparent !important;
    }

    body, .office-link {
        font-size: 16px !important;
        line-height: 1.4em;
    }

    .title-holder {
        padding: 10px 0px;
        margin-top: 14px;
    }

    h1, h2, #Tabs > ul.nav-tabs > li > a {
        font-family: 'Source Sans Pro', sans-serif;
        line-height: 1.4em;
        font-weight: 700;
        color: #003D51;
    }

    h1 {
        font-size: 32px;
    }

    h2, #Tabs > ul.nav-tabs > li > a {
        font-size: 24px;
    }

    a, a:link, a:visited, a:hover, a:active {
        color: #003D51;
    }

    .main-nav li a, .link-holder a, .sfNavHorizontalDropDown .k-item > a.k-link, .sfNavHorizontal li.loginli a {
        font-size: 16px;
    }

        .main-nav li a:hover, .link-holder a:hover, .sfNavHorizontal li.loginli a:hover {
            text-decoration: none !important;
        }

    .link-holder a, .sfNavHorizontal li.loginli a {
        color: #fff;
    }

    .footer-wrapper-main li, .footer-wrapper-main li > a, .footer-wrapper-main p, .tel-number {
        line-height: 1.4em;
    }

    .sfnewsletterForm.sfSubscribe ol.sfnewsletterFieldsList li.sfnewsletterField input, .dataTables_filter input[type=search] {
        font-weight: normal;
    }

    .sfnewsletterForm.sfSubscribe ol.sfnewsletterFieldsList {
        margin-bottom: 0px;
    }

    .footer-wrapper-main h2, .footer-wrapper-main li > a, .office-link {
        font-family: 'Source Sans Pro', sans-serif;
    }

    .footer-navs .link-holder a {
        color: #8C1D40;
        font-size: 12px;
    }

    .nav-tabs > li > a {
        background-color: #eee;
    }

    .nav-tabs > li {
        float: left;
        margin-bottom: -1px;
    }

    .main-title-banner > h2 {
        margin: 0pc !important;
    }
    /*WongS, Ticket #20924*/
    .SumoSelect {width:100%}
</style>


<div class="container-fluid bootstrap-style" id="f1" style="padding-bottom: 150px;">
    <% if (CurrentStep == Step.Button)
        { %>
    <div class="row-fluid btnShowForm">
        <asp:Button ID="btnShowForm" runat="server" Text="Add new Training Vacancy" class="submitBtn" OnClick="btnShowForm_Click" />
    </div>
    <% }
        else if (CurrentStep == Step.Form)
        {  %>
    <div class="row-fluid">
        <div class="container-fluid">
            <div class="row">
                <div class="title-holder" style="text-align: center;">
                    <h1>
                        <span id="baseTemplatePlaceholder_content_C007_ctl00_ctl00_MessageLabel"><%= Mode == Modes.Add ? "Add new Training Vacancy" : "Edit Training Vacancy" %>  </span>
                    </h1>
                </div>
            </div>
        </div>
        <form class="form-horizontal" method="post" id="form1">
            <fieldset>


                <!-- Hidden fields -->
                <asp:HiddenField ID="action" runat="server" />
                <asp:HiddenField ID="fid" runat="server" />
                <asp:HiddenField ID="ffirmid" runat="server" />
                <asp:HiddenField ID="fpersonid" runat="server" />
                <asp:HiddenField ID="fdatelastupdated" runat="server" />
                <asp:HiddenField ID="fdateposted" runat="server" />
                <asp:HiddenField ID="fapprovedby" runat="server" />
                <asp:HiddenField ID="fwebstatus" runat="server" />
                <asp:HiddenField ID="fisdraft" runat="server" />
                <asp:HiddenField ID="fisactive" runat="server" />
                <!--  Company Name -->
                <div class="form-group row">
                    <label class="col-sm-2 col-form-label required" for="fcname">Company name</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="fcname" runat="server" class="form-control disabled" placeholder="Company name" disabled="disabled" name="fcname"></asp:TextBox>
                    </div>
                </div>
                <!--  Company Description -->
                <div class="form-group row">

                    <label class="col-sm-2 col-form-label required" for="ddi">Company description</label>
                    <div class="col-md-10">
                        <div class="">
                            <asp:TextBox ID="fdescription" runat="server" class="form-control char-max-500" placeholder="Company description goes here (500 characters max)" TextMode="multiline" Rows="7" name="fdescription"></asp:TextBox><%-- Wongs, Ticket #20924 --%>
                            <div class="char-count"><span class="chars-num" style="font-weight:bold;">500</span> characters remaining</div><%-- WongS, Ticket #20924 --%>
                        </div>
                    </div>
                </div>
                <!--  Job Title -->
                <div class="form-group row">
                    <label class="col-sm-2 col-form-label required" for="fcname">Job title</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="fjobtitle" runat="server" class="form-control " placeholder="Job title" name="fjobtitle"></asp:TextBox>
                    </div>
                </div>
                <!--  Job Title -->
                <div class="form-group row">
                    <label class="col-sm-2 col-form-label required" for="fcname">Job town</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="fjobtown" runat="server" class="form-control " placeholder="Job town" name="fjobtown"></asp:TextBox>
                    </div>
                </div>
                <!--  Job County -->
                <div class="form-group row">
                    <label class="col-sm-2 col-form-label required" for="email">Job county</label>
                    <div class="col-sm-10">
                        <asp:DropDownList ID="fjobcounty" runat="server" class="form-control" name="fjobcounty">
                            <asp:ListItem Text="Select one" Value=""></asp:ListItem>
                            <asp:ListItem Text="Antrim" Value="Antrim"></asp:ListItem>
                            <asp:ListItem Text="Armagh" Value="Armagh"></asp:ListItem>
                            <asp:ListItem Text="Carlow" Value="Carlow"></asp:ListItem>
                            <asp:ListItem Text="Cavan" Value="Cavan"></asp:ListItem>
                            <asp:ListItem Text="Clare" Value="Clare"></asp:ListItem>
                            <asp:ListItem Text="Cork" Value="Cork"></asp:ListItem>
                            <asp:ListItem Text="Derry" Value="Derry"></asp:ListItem>
                            <asp:ListItem Text="Donegal" Value="Donegal"></asp:ListItem>
                            <asp:ListItem Text="Down" Value="Down"></asp:ListItem>
                            <asp:ListItem Text="County Dublin" Value="County Dublin"></asp:ListItem>
                            <asp:ListItem Text="Dublin 1" Value="Dublin 1"></asp:ListItem>
                            <asp:ListItem Text="Dublin 2" Value="Dublin 2"></asp:ListItem>
                            <asp:ListItem Text="Dublin 3" Value="Dublin 3"></asp:ListItem>
                            <asp:ListItem Text="Dublin 4" Value="Dublin 4"></asp:ListItem>
                            <asp:ListItem Text="Dublin 5" Value="Dublin 5"></asp:ListItem>
                            <asp:ListItem Text="Dublin 6" Value="Dublin 6"></asp:ListItem>
                            <asp:ListItem Text="Dublin 7" Value="Dublin 7"></asp:ListItem>
                            <asp:ListItem Text="Dublin 8" Value="Dublin 8"></asp:ListItem>
                            <asp:ListItem Text="Dublin 9" Value="Dublin 9"></asp:ListItem>
                            <asp:ListItem Text="Dublin 10" Value="Dublin 10"></asp:ListItem>
                            <asp:ListItem Text="Dublin 11" Value="Dublin 11"></asp:ListItem>
                            <asp:ListItem Text="Dublin 12" Value="Dublin 12"></asp:ListItem>
                            <asp:ListItem Text="Dublin 13" Value="Dublin 13"></asp:ListItem>
                            <asp:ListItem Text="Dublin 14" Value="Dublin 14"></asp:ListItem>
                            <asp:ListItem Text="Dublin 15" Value="Dublin 15"></asp:ListItem>
                            <asp:ListItem Text="Dublin 16" Value="Dublin 16"></asp:ListItem>
                            <asp:ListItem Text="Dublin 17" Value="Dublin 17"></asp:ListItem>
                            <asp:ListItem Text="Dublin 18" Value="Dublin 18"></asp:ListItem>
                            <asp:ListItem Text="Dublin 19" Value="Dublin 19"></asp:ListItem>
                            <asp:ListItem Text="Dublin 20" Value="Dublin 20"></asp:ListItem>
                            <asp:ListItem Text="Dublin 21" Value="Dublin 21"></asp:ListItem>
                            <asp:ListItem Text="Dublin 22" Value="Dublin 22"></asp:ListItem>
                            <asp:ListItem Text="Dublin 23" Value="Dublin 23"></asp:ListItem>
                            <asp:ListItem Text="Dublin 24" Value="Dublin 24"></asp:ListItem>
                            <asp:ListItem Text="Fermanagh" Value="Fermanagh"></asp:ListItem>
                            <asp:ListItem Text="Galway" Value="Galway"></asp:ListItem>
                            <asp:ListItem Text="Kerry" Value="Kerry"></asp:ListItem>
                            <asp:ListItem Text="Kildare" Value="Kildare"></asp:ListItem>
                            <asp:ListItem Text="Kilkenny" Value="Kilkenny"></asp:ListItem>
                            <asp:ListItem Text="Laois" Value="Laois"></asp:ListItem>
                            <asp:ListItem Text="Leitrim" Value="Leitrim"></asp:ListItem>
                            <asp:ListItem Text="Limerick" Value="Limerick"></asp:ListItem>
                            <asp:ListItem Text="Longford" Value="Longford"></asp:ListItem>
                            <asp:ListItem Text="Louth" Value="Louth"></asp:ListItem>
                            <asp:ListItem Text="Mayo" Value="Mayo"></asp:ListItem>
                            <asp:ListItem Text="Meath" Value="Meath"></asp:ListItem>
                            <asp:ListItem Text="Monaghan" Value="Monaghan"></asp:ListItem>
                            <asp:ListItem Text="Offaly" Value="Offaly"></asp:ListItem>
                            <asp:ListItem Text="Roscommon" Value="Roscommon"></asp:ListItem>
                            <asp:ListItem Text="Sligo" Value="Sligo"></asp:ListItem>
                            <asp:ListItem Text="Tipperary" Value="Tipperary"></asp:ListItem>
                            <asp:ListItem Text="Tyrone" Value="Tyrone"></asp:ListItem>
                            <asp:ListItem Text="Waterford" Value="Waterford"></asp:ListItem>
                            <asp:ListItem Text="Westmeath" Value="Westmeath"></asp:ListItem>
                            <asp:ListItem Text="Wexford" Value="Wexford"></asp:ListItem>
                            <asp:ListItem Text="Wicklow" Value="Wicklow"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <!--  Vacancy Type -->
                <div class="form-group row">
                    <label class="col-sm-2 col-form-label required" for="email">Vacancy type</label>
                    <div class="col-sm-10">
                        <div class="row">
                            <div class="col-lg-12">
                                <span class="multiselect-pill authorisation-pill">Press to select vacancy types</span>
                                <asp:ListBox ID="fvacancytype" runat="server" SelectionMode="Multiple" CssClass="SlectBox form-control">
                                    <asp:ListItem Text="Training Contract" Value="Training Contract"></asp:ListItem>
                                    <asp:ListItem Text="Flexible Route" Value="Flexible Route"></asp:ListItem>
                                    <asp:ListItem Text="Internship up to 3 months" Value="Internship up to 3 months"></asp:ListItem>
                                    <asp:ListItem Text="Internship over 3 months" Value="Internship over 3 months"></asp:ListItem>
                                </asp:ListBox>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 pill-list extra-space"><%--WongS, Ticket #20924--%>
                            </div>
                        </div>
                    </div>
                </div>
                <!--  Train In Type -->
                <div class="form-group row">
                    <label class="col-sm-2 col-form-label required" for="email">Train in type</label>
                    <div class="col-sm-10">
                        <asp:DropDownList ID="ftrainintype" runat="server" class="form-control" name="ftrainintype">
                            <asp:ListItem Enabled="true" Text="Please select" Value=""></asp:ListItem>
                            <asp:ListItem Text="Business" Value="Business"></asp:ListItem>
                            <asp:ListItem Text="Practice" Value="Practice"></asp:ListItem>
                            <asp:ListItem Text="Public sector" Value="Public sector"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <!--  Job Spec -->
                <div class="form-group row">

                    <label class="col-sm-2 col-form-label required" for="ddi">Job spec</label>
                    <div class="col-md-10">
                        <div class="">
                            <asp:TextBox ID="fjobspec" runat="server" class="form-control char-max-500" placeholder="Job specification goes here (500 characters max)" TextMode="multiline" Rows="7" name="fjobspec"></asp:TextBox><%-- WongS, Ticket #20924 --%>
                            <div class="char-count"><span class="chars-num" style="font-weight:bold;">500</span> characters remaining</div><%-- WongS, Ticket #20924 --%>
                        </div>
                    </div>
                </div>
                <!--  Job Requeriments -->
                <div class="form-group row">
                    <label class="col-sm-2 col-form-label required" for="ddi">Job requirement</label>
                    <div class="col-md-10">
                        <div class="">
                            <asp:TextBox ID="fjobrequirements" runat="server" class="form-control char-max-500" placeholder="Job requirements goes here (500 characters max)" TextMode="multiline" Rows="7" name="fjobrequirements"></asp:TextBox><%-- WongS, Ticket #20924 --%>
                            <div class="char-count"><span class="chars-num" style="font-weight:bold;">500</span> characters remaining</div><%-- WongS, Ticket #20924 --%>
                        </div>
                    </div>
                </div>
                <!--   Benefits Remuneration [ |  |  |  |  | Others] -->
                <div class="form-group row">
                    <label class="col-md-2 col-sm-2 col-form-label" for="ddi" style="padding: 10px 0; font-weight: 600; font-size: 16px;">Benefits remuneration</label>
                    <div class="col-md-10">
                        <div class="row">
                            <div class="col-lg-4 cb-item">
                                <asp:CheckBox ID="fcpension" runat="server" CssClass="cb-item-cb" Text="Pension" TextAlign="Right" />
                            </div>
                            <div class="col-lg-4 cb-item">
                                <asp:CheckBox ID="fchealth" runat="server" CssClass="cb-item-cb" Text="Health insurance" TextAlign="Right" />
                            </div>
                            <div class="col-lg-4 cb-item">
                                <asp:CheckBox ID="fctex" runat="server" CssClass="cb-item-cb" Text="Tax savers scheme" TextAlign="Right" />
                            </div>
                            <div class="col-lg-4 cb-item">
                                <asp:CheckBox ID="fcflexi" runat="server" CssClass="cb-item-cb" Text="Flexi hours" TextAlign="Right" />
                            </div>
                            <div class="col-lg-4 cb-item">
                                <asp:CheckBox ID="fclearn" runat="server" CssClass="cb-item-cb" Text="Learning &amp; development" TextAlign="Right" />
                            </div>
                            <div class="col-lg-12 cb-item">
                                <div class="row">
                                    <asp:CheckBox ID="fcbother" runat="server" Text="Others" TextAlign="Right" CssClass="col-lg-2 other-item" />
                                    <asp:TextBox ID="fcbothertext" runat="server" class="form-control" placeholder="Add other benefits here here" CssClass="col-lg-8" name="fbenotherstext"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!--  How To Apply -->
                <div class="form-group row">
                    <label class="col-sm-2 col-form-label required" for="ddi">How to apply</label>
                    <div class="col-md-10">
                        <div class="">
                            <asp:TextBox ID="fhowtoapply" runat="server" class="form-control char-max-300" placeholder="Instructions on how to apply goes here (300 characters max)" TextMode="multiline" Rows="3" name="fhowtoapply"></asp:TextBox> <%-- Wongs, Ticket #20924 --%>
                            <div class="char-count"><span class="chars-num" style="font-weight:bold;">300</span> characters remaining</div><%-- WongS, Ticket #20924 --%>
                        </div>
                    </div>
                </div>
                <!--  Website -->
                <div class="form-group row hide-it"><%-- WongS, Ticket #20924 --%>
                    <label class="col-sm-2 col-form-label required" for="fcname">Website</label>
                    <div class="col-sm-10">
                        <asp:Label ID="fwebsite" runat="server" class="form-control " placeholder="Website" name="fwebsite"></asp:Label>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-sm-2 col-form-label required" for="fcname">Date closing</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="fdateclosing" runat="server" class="form-control" placeholder="Date closing" name="fdateclosing"></asp:TextBox>
                    </div>
                </div>
				<!-- PATCH FIX CHECKBOXES #21366 -->
                <div class="form-group row">
                    <div class="col-sm-2"></div>
                        <asp:CheckBox ID="cb1" CssClass="checkbox-inline col-xs-12 col-sm-10" runat="server" Checked="false" Text=" Please tick this box if you'd like to receive communications from us about how you can study and train to become a Chartered Accountant for the upcoming intake" />
                </div>
                <%-- WongS, Ticket #20924 --%>
                <div class="form-group row">
                    <p class="info-note">After you've added a training vacancy listing, you must then submit it for approval to staff before your listing goes up on the website.</p>
                </div>
                <div class="form-group row">
                    <% if (id < 0)
                        { %>
                    <div class="text-right">
                        <asp:Button ID="btnSave" runat="server" Text="Add Training Vacancy" class="submitBtn" OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="cancelBtn" OnClientClick="history.back();" />
                    </div>
                    <% }
                        else
                        { %>
                    <div class="text-right">
                        <asp:Button ID="btnEditSave" runat="server" Text="Save changes" class="submitBtn" OnClick="btnUpdate_Click" />
                        <asp:Button ID="btnEditCancel" runat="server" Text="Cancel" class="cancelBtn" OnClientClick="history.back();" />
                    </div>
                    <% }  %>
                </div>
            </fieldset>
        </form>
    </div>
    <% } %>
</div>
<uc1:User ID="User1" runat="server"></uc1:User>
<script>
    //WongS, Ticket #20924
    //Susan Wong, Max character red text warning display
    var maxLength = 500;
    $('.char-max-500').each(function () {
        $(this).keyup(function () {
            var length = $(this).val().length;
            var length = maxLength - length;
            $(this).siblings('.char-count').children('.chars-num').text(length);
            if (length < 15)
            { $(this).siblings('.char-count').children('.chars-num').css("color", "red"); }
        });
    });
    var maxLength2 = 300;
    $('.char-max-300').each(function () {
        $(this).keyup(function () {
            var length = $(this).val().length;
            var length = maxLength2 - length;
            $(this).siblings('.char-count').children('.chars-num').text(length);
            if (length < 15)
            { $(this).siblings('.char-count').children('.chars-num').css("color", "red"); }
        });
    });
</script>

