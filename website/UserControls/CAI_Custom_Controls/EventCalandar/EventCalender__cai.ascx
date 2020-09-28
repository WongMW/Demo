<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventCalender__cai.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.Jim_UserControls.EventCalender__cai" %>




<%--<link href="https://cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.0.1/fullcalendar.css" rel="stylesheet" />
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"/>

<script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.15.1/moment.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.0.1/fullcalendar.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>--%>


<%--problem --%>

<%--<link href="https://fullcalendar.io/js/fullcalendar-3.1.0/fullcalendar.min.css" rel="stylesheet" />
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"/>

<script src="https://fullcalendar.io/js/fullcalendar-3.1.0/lib/moment.min.js"></script>
<%--<script src="https://fullcalendar.io/js/fullcalendar-3.1.0/lib/jquery.min.js"></script>--%>
<%--<script src="https://fullcalendar.io/js/fullcalendar-3.1.0/fullcalendar.min.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>--%>



<%--<script src="https://fullcalendar.io/js/fullcalendar-3.1.0/lib/jquery.min.js"></script>--%>
<%--<script src="https://fullcalendar.io/js/fullcalendar-3.1.0/lib/moment.min.js"></script>
<script src="https://fullcalendar.io/js/fullcalendar-3.1.0/fullcalendar.min.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>--%>



<%--direct link from website folder --%>

<link href ="../../../Scripts/InHouse/FullEventCalender/fc3.1.0/fullcalendar.css" rel="stylesheet" />
<link href="../../../Scripts/InHouse/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />

<script src ="../../../Scripts/InHouse/FullEventCalender/fc3.1.0/lib/moment.min.js"></script>
<script src="../../../Scripts/InHouse/FullEventCalender/fc3.1.0/fullcalendar.js"></script>
<script src="../../../Scripts/InHouse/bootstrap/3.3.7/js/bootstrap.min.js"></script>




<%--production setup --%>
<%--<link href="https://cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.0.1/fullcalendar.css" rel="stylesheet" />
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"/>
<!--<link rel="stylesheet" type="text/css" href="../../../CSS/InHouse/override.css"/>-->



<script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.15.1/moment.js"></script>
<script>window.jQuery || document.write('<script src="~/Scripts/InHouse/FullEventCalender/moment.min.js"><\/script>')</script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.0.1/fullcalendar.js"></script>
<script>window.jQuery || document.write('<script src="~/Scripts/InHouse/FullEventCalender/fc3.1.0/fullcalendar.js"><\/script>')</script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
<script>window.jQuery || document.write('<script src="~/Scripts/InHouse/bootstrap/3.3.7/js/bootstrap.min.js"><\/script>')</script>--%>

<script type="text/javascript">
    $(document).ready(function () {
        $.ajax({
            type: "POST",
            contentType: "application/json",
            data: "{}",
            url: '<%= ResolveUrl("EventList.aspx/GetEvents")%>',
            dataType: "json",
            success: function (data) {
                $('#fullcal').fullCalendar({
                    eventClick: function (calEvent, jsEvent, view) {
                        $('#eid').html(calEvent.id);
                        $('#modalTitle').html(calEvent.title);
                        $('#msDate').html(moment(calEvent.start).format('DD-MM-YYYY HH:mm'));
                        $('#meDate').html(moment(calEvent.end).format('DD-MM-YYYY HH:mm'));
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

                            columnFormat: 'dddd',
                            // other view-specific options here
                            //displayEventTime: false
                        },


                        week: { // name of view
                            titleFormat: 'MMMM  D , YYYY',
                            columnFormat: 'dddd D/M',
                            // other view-specific options here
                            // displayEventTime: false,
                            //displayEventEnd : true,
                            //timeFormat: 'H:mm'
                        },
                        day: { // name of view
                            titleFormat: 'MMMM  DD  YYYY',
                            columnFormat: 'dddd D-M-YYYY',
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
                        event.start = new Date(item.StartDate);
                        event.end = new Date(item.EndDate);
                        event.loc = item.Location;
                        event.des = item.Description;
                        return event;
                    }),

                });

                $("div[id=fullcal]").show();

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                debugger;
            }
        });
    });
</script>




<style>
	#fullcal {
		max-width: 900px;
		margin: 0 auto;
	}
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
    .modal-content {
        padding: 0px;
    }

    /*.submitBtn {
    padding: 8px 20px;
    height: 40px;
    display: inline-block;
    text-transform: uppercase;
    background: #8C1D40;
    color: #fff;
    border: 2px solid transparent;
    margin-right: 5px;
}*/

    a:link {
    color: #fff;
}

/* visited link */
a:visited {
    color: blue;
}

/* mouse over link */
a:hover {
    color: #8C1D40;
}

</style>
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
                <table id ="t1" class="table">
                <thead>
                  <tr>
                    <th><b>Start Date</b></th>
                    <th><b>End Date</b></th>
                    <th><b>Location</b></th>
                  </tr>
                </thead>
                    <tbody>
                      <tr>
                        <td id ="msDate"></td>
                        <td id ="meDate"></td>
                        <td id ="mloc"></td>
                      </tr>
                    </tbody>
                 </table>
                 <table id ="t2" class="table">
                <thead>
                  <tr>
                    <th><b>Description :</b></th>
                  </tr>
                </thead>
                    <tbody>
                      <tr>
                        <td id ="mdesc"></td>
                      </tr>
                    </tbody>
                 </table>
            </div>
            <div class="modal-footer">
               <button class="submitBtn"><a id="url" target="_blank">VIEW MORE</a></button>
                <button type="button" class="submitBtn" data-dismiss="modal">CLOSE</button>        
            </div>
        </div>
    </div>
</div>
