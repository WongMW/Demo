<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EC.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.EventCalandar.EC" %>


<link href="https://cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.3.1/fullcalendar.css" rel="stylesheet" />
<link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
<link href="../../../CSS/InHouse/override.css" rel="stylesheet" />
<script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.15.1/moment.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.3.1/fullcalendar.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

<!-- Susan Wong 10-08-2017: Multiselect dropdown files -->
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery.sumoselect/3.0.2/sumoselect.min.css">
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.sumoselect/3.0.2/jquery.sumoselect.min.js"></script>
<%-- This is script code --%>
<script type="text/javascript">
    jQuery(document).ready(function () {
        $.ajax({
            type: "POST",
            contentType: "application/json;charset=utf-8",
            data: "{}",
            url: '<%= ResolveUrl("EventList.aspx/GetEvents")%>',
             dataType: "json",
             success: function (data) {
                 var resultevent = $('#resultDiv');
                 if (data.d == 0) {
                     resultevent.html('<div class="ec-result-normal"><b>There are no events found.<b/></div>')
                 }
                 else {
                     resultevent.html('<div class="ec-result-normal"><b>There is a total of ' + data.d.length + ' events for this year. Please use filters to narrow down your search.<b/></div>')
                 }


                 $('#fullcal').fullCalendar({
                     eventClick: function (calEvent, jsEvent, view) {
                         $('#eid').html(calEvent.id);
                         $('#modalTitle').html(calEvent.title);
                         $('#msDate').html(moment(calEvent.start).format('DD-MM-YYYY HH:mm'));
                         $('#meDate').html(moment(calEvent.end).format('DD-MM-YYYY HH:mm'));
                         //$('#msDate').html(moment.utc(calEvent.start).local().format('DD-MM-YYYY HH:mm'));
                         // $('#meDate').html(moment.utc(calEvent.end).local().format('DD-MM-YYYY HH:mm'));
                         $('#mloc').html(calEvent.loc)
                         $('#mdesc').html(calEvent.des)
                         $('#url').attr('href', 'Meetings/Meeting.aspx?ID=' + calEvent.id)
                         $('#fullCalModal').modal();

                     },

                     header: {
                         left: 'prev,next today',
                         center: 'title',
                         //right: 'month,agendaWeek,agendaDay'
                         right: 'month,basicWeek,basicDay'
                     },
                     views: {
                         month: { // name of view

                             columnFormat: 'ddd',
                             // other view-specific options here
                             //displayEventTime: false
                         },


                         week: { // name of view
                             titleFormat: 'MMMM  D , YYYY',
                             columnFormat: 'ddd D/M',
                             // other view-specific options here
                             // displayEventTime: false,
                             //displayEventEnd : true,
                             //timeFormat: 'H:mm'
                         },
                         day: { // name of view
                             titleFormat: 'MMMM  DD  YYYY',
                             columnFormat: 'ddd D-M-YYYY',
                             // other view-specific options here
                             //displayEventEnd: true,
                             // timeFormat: 'H:mm'
                         }
                     },

                     //editable: true,
                     displayEventTime: false,// hide event time 
                     eventLimit: true, // allow "more" link when too many events
                     events: $.map(data.d, function (item, i) {
                         var event = new Object();
                         event.id = item.EventID;
                         event.title = item.EventName;
                         //event.start = new Date(item.StartDate);
                         //event.end = new Date(item.EndDate);
                         event.start = item.StartDate;
                         event.end = item.EndDate;
                         event.loc = item.Location;
                         event.des = item.Description;
                         return event;

                     }),

                 });

             },
             error: function (XMLHttpRequest, textStatus, errorThrown) {
                 debugger;
             }
         });


    });



</script>

<!-- Susan Wong 10-08-2017: Multiselect dropdown control options -->
<script>
    $(document).ready(function () {

        window.testSelAlld = $('#s1').SumoSelect({
            placeholder: 'Select location(s)',
            okCancelInMulti: true,
            selectAll: true,
            isClickAwayOk: true,
            selectAll: true,
            search: true,
            searchText: 'Type here to search location',
            triggerChangeCombined: false,
            //outputAsCSV:true,
            //csvSepChar: ' ,'
        });

        window.testSelAlld = $('#s2').SumoSelect({
            placeholder: 'Select event type',
        });
        window.testSelAlld = $('#s3').SumoSelect({
            placeholder: 'Select CPD category/categories',
            okCancelInMulti: true,
            selectAll: true,
            search: true,
            searchText: 'Type here to search CPD categories',
        });
        window.testSelAlld = $('#s4').SumoSelect({
            placeholder: 'Select Networking category/categories',
            okCancelInMulti: true,
            selectAll: true,
            search: true,
            searchText: 'Type here to search Networking categories',
        });

        // dropdown change selection 
        $('#s2').on('change', (function () {
            var loc1 = [];
            var evt1 = [];
            $('#s1 option:selected').each(function (i) {
                loc1.push($(this).val());
            });
            evt1.push($(this).val());
            if (loc1.length < 1) {
                //or **if(arrayName.length)**
                //this array is not empty
                loc1 = ['0'];
            }
            InItCal(loc1, evt1, 0, 0);
            $('#fullcal').fullCalendar('destroy');

        }));
        $('.btnOk').on('click', function () {
            var loc = [];
            var evt = [];
            var cat = [];
            var nevt = [];


            $('#s1 option:selected').each(function (i) {
                loc.push($(this).val());
            });
            $('#s2 option:selected').each(function (i) {
                evt.push($(this).val());
            });
            $('#s3 option:selected').each(function (i) {
                cat.push($(this).val());
            });
            $('#s4 option:selected').each(function (i) {
                nevt.push($(this).val());
            });
            if (loc.length < 1) {
                //or **if(arrayName.length)**
                //this array is not empty
                loc = ['0'];
            }
            if (evt.length < 1) {
                //or **if(arrayName.length)**
                //this array is not empty
                evt = ['0'];
            }
            if (cat.length < 1) {
                //or **if(arrayName.length)**
                //this array is not empty
                cat = ['0'];
            }
            if (nevt.length < 1) {
                //or **if(arrayName.length)**
                //this array is not empty
                nevt = ['0'];
            }
            //alert(loc);
            InItCal(loc, evt, cat, nevt);
            $('#fullcal').fullCalendar('destroy');
        });

        function InItCal(location, eventtype, cattype, nevent) {
            $.ajax({
                type: "POST",
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify({
                    location: location.toString(),
                    eventtype: eventtype.toString(),
                    cattype: cattype.toString(),
                    nevent: nevent.toString()
                }),
                url: '<%= ResolveUrl("EventList.aspx/GetEventsfilterloc")%>',
                    dataType: "json",
                    //traditional: true,
                    success: function (data) {
                        //alert(data.d.length);
                        var resultevent = $('#resultDiv');
                        if (data.d == 0) {
                            resultevent.html('<div class="ec-result-fail"><b>Sorry, we could not find any results that match your filter(s). Please try again with different filter option(s).<b/></div>')
                        }
                        else {
                            resultevent.html('<div class="ec-result-success"><b>Found ' + data.d.length + ' events for this year that matches your filter(s). Navigate through the months to see them all.<b/></div>')
                        }
                        $('#fullcal').fullCalendar({
                            eventClick: function (calEvent, jsEvent, view) {
                                $('#eid').html(calEvent.id);
                                $('#modalTitle').html(calEvent.title);
                                $('#msDate').html(moment(calEvent.start).format('DD-MM-YYYY HH:mm'));
                                $('#meDate').html(moment(calEvent.end).format('DD-MM-YYYY HH:mm'));
                                //$('#msDate').html(moment.utc(calEvent.start).local().format('DD-MM-YYYY HH:mm'));
                                // $('#meDate').html(moment.utc(calEvent.end).local().format('DD-MM-YYYY HH:mm'));
                                $('#mloc').html(calEvent.loc)
                                $('#mdesc').html(calEvent.des)
                                $('#url').attr('href', 'Meetings/Meeting.aspx?ID=' + calEvent.id)
                                $('#fullCalModal').modal();
                            },

                            header: {
                                left: 'prev,next today',
                                center: 'title',
                                //right: 'month,agendaWeek,agendaDay'
                                right: 'month,basicWeek,basicDay'
                            },
                            views: {
                                month: { // name of view
                                    default: false,
                                    columnFormat: 'ddd',
                                    // other view-specific options here
                                    //displayEventTime: false
                                },
                                week: { // name of view
                                    titleFormat: 'MMMM  D , YYYY',
                                    columnFormat: 'ddd D/M',
                                    // other view-specific options here
                                    // displayEventTime: false,
                                    //displayEventEnd : true,
                                    //timeFormat: 'H:mm'
                                },
                                day: { // name of view
                                    titleFormat: 'MMMM  DD  YYYY',
                                    columnFormat: 'ddd D-M-YYYY',
                                    // other view-specific options here
                                    //displayEventEnd: true,
                                    // timeFormat: 'H:mm'
                                }
                            },

                            //editable: true,
                            displayEventTime: false,// hide event time 
                            eventLimit: true, // allow "more" link when too many events
                            events: $.map(data.d, function (item, i) {
                                var event = new Object();
                                event.id = item.EventID;
                                event.title = item.EventName;
                                //event.start = new Date(item.StartDate);
                                //event.end = new Date(item.EndDate);
                                event.start = item.StartDate;
                                event.end = item.EndDate;
                                event.loc = item.Location;
                                event.des = item.Description;
                                return event;

                            }),

                        });


                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        debugger;
                    }

                });
            }
    });
        //});
</script>


<style>
    #fullcal {
        max-width: 900px;
        margin: 0 auto;
    }

    .modal-header-primary {
        color: #fff;
        padding: 9px 15px;
        border-bottom: 1px solid #eee;
        background-color: #8C1D40;
        -webkit-border-top-left-radius: 5px;
        -webkit-border-top-right-radius: 5px;
        -moz-border-radius-topleft: 5px;
        -moz-border-radius-topright: 5px;
        border-top-left-radius: 5px;
        border-top-right-radius: 5px;
    }

    .modal-content {
        padding: 0px;
    }

    a:link {
        color: #fff;
    }
    /* mouse over link */
    a:hover {
        color: #8C1D40;
    }
    /* Susan Wong 10-08-2017: Fix spacing for select all on multiselect dropdown*/
    p.select-all {
        padding-top: 10px !important;
        height: 2.5em !important;
    }

    .SumoSelect {
        width: 100%;
    }

    .label-div {
        font-weight: bold;
    }

    p.select-all > label {
        margin: 0px;
        font-weight: bold;
    }
</style>

<!-- Susan Wong 10-08-2017: Multiselect dropdown -->
<div class="sf_cols">
    <div class="sf_colsOut sf_3cols_1_33">
        <div id="baseTemplatePlaceholder_content_ctl01_ctl03_ctl05_C012_Col00" class="sf_colsIn sf_3cols_1in_33">
            <form method="get">
                <div class="label-div">Filter by location</div>
                <select id="s1" multiple="multiple" onchange="console.log('changed', this)" placeholder="Select location(s)" class="SlectBox-grp">
                    <option value="Dublin">Dublin</option>
                    <option value="Belfast">Belfast</option>
                    <option value="London">London</option>
                    <optgroup label="Select by province">
                        <option value="Leinster">Leinster (ROI)</option>
                        <option value="Ulster">Ulster (NI)</option>
                        <option value="Munster">Munster (ROI)</option>
                        <option value="Connacht">Connacht (ROI)</option>
                    </optgroup>
                </select>
            </form>
        </div>
    </div>
    <div class="sf_colsOut sf_3cols_2_34">
        <div id="baseTemplatePlaceholder_content_ctl01_ctl03_ctl05_C012_Col01" class="sf_colsIn sf_3cols_2in_34">
            <form method="get">
                <div id="eventType">
                    <div class="label-div">Filter by event type</div>
                    <select id="s2" onchange="console.log('changed', this)" placeholder="Select an event type" class="SlectBox-grp">
                        <option selected disabled>Only select 1 below</option>
                        <option value="CPD courses ALL A-Z">CPD courses</option>
                        <option value="Conferences">Conferences</option>
                        <option value="Networking Events">Networking events</option>
                    </select>
                </div>
            </form>
        </div>
    </div>
    <div class="sf_colsOut sf_3cols_3_33">
        <div id="baseTemplatePlaceholder_content_ctl01_ctl03_ctl05_C012_Col02" class="sf_colsIn sf_3cols_3in_33">
            <form method="get">
                <div id="categoryType1">
                    <div class="label-div">Filter by category</div>
                    <select id="s3" multiple="multiple" onchange="console.log('changed', this)" placeholder="Select CPD category/categories" class="SlectBox-grp">
                        <option value="Accounting">Accounting</option>
                        <option value="Audit and assurance">Audit and assurance</option>
                        <option value="Ethics">Ethics</option>
                        <option value="Finance, management reporting and analysis">Finance, management reporting and analysis</option>
                        <option value="Financial reporting">Financial reporting</option>
                        <option value="Financial services">Financial services</option>
                        <option value="Governance, risk and legal">Governance, risk and legal</option>
                        <option value="Information technology">Information technology</option>
                        <option value="Insolvency and corporate recovery">Insolvency and corporate recovery</option>
                        <option value="Investment business">Investment business</option>
                        <option value="Leadership, management and personal impact">Leadership, management and personal impact</option>
                        <option value="Practice and business improvement">Practice and business improvement</option>
                        <option value="Research & analysis">Research & analysis</option>
                        <option value="Student texts">Student texts</option>
                        <option value="Tax">Tax</option>
                    </select>
                </div>
            </form>
            <form method="get">
                <div id="categoryType2">
                    <div class="label-div">Filter by category</div>
                    <select id="s4" multiple="multiple" onchange="console.log('changed', this)" placeholder="Select Networking category/categories" class="SlectBox-grp">
                        <option value="Cork Society events">Cork Society events</option>
                        <option value="Institute events">Institute events</option>
                        <option value="Leinster Society events">Leinster Society events</option>
                        <option value="London Society events">London Society events</option>
                        <option value="Mid Western Society events">Mid Western Society events</option>
                        <option value="NorthWest Society events">NorthWest Society events</option>
                        <option value="Ulster Society events">Ulster Society events</option>
                        <option value="Western Society events">Western Society events</option>
                        <option value="Young Professionals">Young Professionals events</option>
                    </select>
                </div>
            </form>
        </div>
    </div>
</div>
<div id="resultDiv">
</div>
<div id='fullcal'>
</div>
<div id="fullCalModal" class="modal fade">
    <div class="modal-dialog" id="primary" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-content">

            <div class="modal-header modal-header-primary">
                <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">X</span> <span class="sr-only">close</span></button>
                <h4 id="modalTitle" class="modal-title"></h4>
            </div>
            <div id="modalBody" class="modal-body">
                <table id="t1" class="table">
                    <thead>
                        <tr>
                            <th><b>Start Date</b></th>
                            <th><b>End Date</b></th>
                            <th><b>Location</b></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td id="msDate"></td>
                            <td id="meDate"></td>
                            <td id="mloc"></td>
                        </tr>
                    </tbody>
                </table>
                <table id="t2" class="table">
                    <thead>
                        <tr>
                            <th><b>Description :</b></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td id="mdesc"></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="modal-footer sfContentBlock">
                <a id="url" target="_blank" class="cai-btn cai-btn-red">VIEW MORE</a>
                <button type="button" class="cai-btn cai-btn-red" data-dismiss="modal">CLOSE</button>
            </div>
        </div>
    </div>
</div>
<script>
    $(document).ready(function () {
        $("#categoryType1").hide();
        $("#categoryType2").hide();
    });
    $("#s2").change(function () {
        if ($("#eventType p span").text().trim() === "CPD courses") {
            $("#categoryType1").show();
            $('#s4')[0].sumo.unSelectAll();
            $("#categoryType2").hide();
        }
        else if ($("#eventType p span").text().trim() === "Networking events") {
            $('#s3')[0].sumo.unSelectAll();
            $("#categoryType1").hide();
            $("#categoryType2").show();
        }
        else {
            $("#categoryType1").hide();
            $("#categoryType2").hide();
            $('#s4')[0].sumo.unSelectAll();
            $('#s3')[0].sumo.unSelectAll();
        }

    });
</script>
