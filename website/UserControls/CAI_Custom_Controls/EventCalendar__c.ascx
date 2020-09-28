<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EventCalendar__c.ascx.cs" Inherits="UserControls_CAI_Custom_Controls_EventCalendar__c" %>

<div class="container"><!-- Container start -->
    <div class="panel panel-default">
        <div class="panel-body">
            <div class="row">
                <!-- Responsive calendar - START -->
                <div class="responsive-calendar col-md-8 col-md-offset-2">
                    <div class="controls">
                        <a class="pull-left">
                            <button id="cal-prev-btn" type="button" class="btn btn-primary col-md-12">Prev</button>
                        </a>
                        <!--<a class="pull-left" data-go="prev">
                            <div class="btn btn-primary">Prev</div>
                        </a>-->
                        <h4><span id="cal-data-month" data-head-month></span> <span id="cal-data-year" data-head-year></span></h4>
                        <!--<a class="pull-right" data-go="next">
                            <div class="btn btn-primary">Next</div>
                        </a>-->
                        <a class="pull-right">
                            <button id="cal-next-btn" type="button" class="btn btn-primary col-md-12">Next</button>
                        </a>
                    </div>
                    <hr />
                    <div class="day-headers">
                        <div class="day header">Mon</div>
                        <div class="day header">Tue</div>
                        <div class="day header">Wed</div>
                        <div class="day header">Thu</div>
                        <div class="day header">Fri</div>
                        <div class="day header">Sat</div>
                        <div class="day header">Sun</div>
                    </div>
                    <div class="days" data-group="days">
                    </div>
                </div>
                <!-- Responsive calendar - END -->
            </div>
            <div class="row">
                <ul id="events-list" class="list-unstyled">
                    <% foreach (CAI_Event ev in events)
                       { %>
                    <li id="event-item-<%= ev.id %>" class="col-md-8 col-md-offset-2 cai-marg-bottom-1" style="display: none;">
                        <div class="col-xs-12 col-md-2 col-md-cai-height-fixed-125 cai-padding-10 cai-bg-standard-grey-dark text-center">
                            <div class="visible-md visible-lg">
                                <span class="col-md-12 cai-font-white cai-font-18-bold"><%= ev.GetFormattedStartMonth() %></span> 
                                <span class="col-md-12 cai-font-white cai-font-36-reg"><%= ev.GetFormattedStartDay() %></span> 
                                <span class="col-md-12 cai-font-white cai-font-18-reg"><%= ev.GetFormattedStartWeekDay() %></span> 
                            </div>
                            <div class="col-xs-6 col-xs-offset-3 visible-xs visible-sm">
                                <span class="cai-font-white cai-font-18-bold"><%= ev.GetFormattedStartMonth() %></span> 
                                <span class="cai-font-white cai-font-36-reg"><%= ev.GetFormattedStartDay() %></span> 
                                <span class="cai-font-white cai-font-18-reg"><%= ev.GetFormattedStartWeekDay() %></span> 
                            </div>


                            <div class="col-xs-3 visible-xs visible-sm">
                                <% if(ev.meetingType == 9) { %>
                                    <div class="cai-bg-primary-wine cai-padding-5 col-md-cai-height-100pc text-center cai-font-white">
                                        <span class="col-xs-12 glyphicon glyphicon-glass cai-font-2em"></span>
                                        <span class="cai-font-10-reg">Conference</span>
                                    </div>
                                <% }else if(ev.meetingType == 8) { %>
                                    <div class="cai-bg-secondary-purple-light cai-padding-5 col-md-cai-height-100pc text-center cai-font-white">
                                        <span class="col-xs-12 glyphicon glyphicon-education cai-font-2em"></span>
                                        <span class="cai-font-10-reg">Conferrings</span>
                                    </div>
                                <% }else if(ev.meetingType == 7) { %>
                                    <div class="cai-bg-secondary-orange-brown cai-padding-5 col-md-cai-height-100pc text-center cai-font-white">
                                        <span class="col-xs-12 glyphicon glyphicon-briefcase cai-font-2em"></span>
                                        <span class="cai-font-10-reg">Committee</span>
                                    </div>
                                <% }else { %>
                                    <div class="cai-bg-secondary-teal cai-padding-5 col-md-cai-height-100pc text-center cai-font-white">
                                        <span class="col-xs-12 glyphicon glyphicon-calendar cai-font-2em"></span>
                                        <span class="cai-font-10-reg">Events</span>
                                    </div>
                                <% } %>
                            </div>

                        </div>
                        <div class="col-xs-12 col-md-8 col-md-cai-height-fixed-125 cai-padding-10 cai-bg-standard-grey-light">
                            <h4 class="cai-marg-top-1 cai-marg-bottom-1 col-md-cai-height-fixed-20"><%= ev.name %></h4>
                            <p class="cai-marg-top-1 cai-marg-bottom-5 cai-font-14-reg col-md-cai-height-fixed-55"><%= ev.description %></p>
                            <p class="cai-marg-top-1 cai-marg-bottom-1 cai-font-14-reg col-md-cai-height-fixed-20"><strong>End Date: </strong><%= ev.GetFormattedEndDate() %></p> <!-- Not a deal breaker, but would be good to get date in format of "26th Apr 2015" -->
                        </div>
                        <div class="col-xs-12 col-md-2 col-md-cai-height-fixed-125 cai-padding-10 cai-bg-standard-grey-light">
                            <div class="col-md-cai-height-fixed-60 cai-marg-5 visible-md visible-lg">
                                <% if(ev.meetingType == 9) { %>
                                    <div class="cai-bg-primary-wine cai-padding-5 col-md-cai-height-100pc text-center cai-font-white">
                                        <span class="col-md-12 glyphicon glyphicon-glass cai-font-2em"></span>
                                        <span class="cai-font-10-reg">Conference</span>
                                    </div>
                                <% }else if(ev.meetingType == 8) { %>
                                    <div class="cai-bg-secondary-purple-light cai-padding-5 col-md-cai-height-100pc text-center cai-font-white">
                                        <span class="col-md-12 glyphicon glyphicon-education cai-font-2em"></span>
                                        <span class="cai-font-10-reg">Conferrings</span>
                                    </div>
                                <% }else if(ev.meetingType == 7) { %>
                                    <div class="cai-bg-secondary-orange-brown cai-padding-5 col-md-cai-height-100pc text-center cai-font-white">
                                        <span class="col-md-12 glyphicon glyphicon-briefcase cai-font-2em"></span>
                                        <span class="cai-font-10-reg">Committee</span>
                                    </div>
                                <% }else { %>
                                    <div class="cai-bg-secondary-teal cai-padding-5 col-md-cai-height-100pc text-center cai-font-white">
                                        <span class="col-md-12 glyphicon glyphicon-calendar cai-font-2em"></span>
                                        <span class="cai-font-10-reg">Events</span>
                                    </div>
                                <% } %>
                            </div>
                            <button type="button" class="btn btn-default col-xs-12 col-md-12">More ..</button><!-- ADD dynamic link to products here -->
                        </div>
                    </li>
                    <%  } %>
                </ul>
                <div class="col-md-12 cai-marg-top-10">
                    <button id="showNextEvents" type="button" class="btn btn-success col-xs-12 col-md-8 col-md-offset-2">Show next 7 days</button>
                </div>
            </div>
        </div>
    </div>
</div><!-- Container end -->

<script src="/EBusiness/Sitefinity/WebsiteTemplates/TestBoostrapTemplate/App_Themes/TestBootstrapTheme1/Bootstrap/js/responsive-calendar.min.js"></script>

<!-- Calendar Setup and Action Logic -->
<script type="text/javascript">

    $(document).ready(function () {
        
        $("#cal-prev-btn").click(function(){
            window.location.href = window.location.href.replace( /[\?#].*|$/, '?caldatem='+$('#cal-data-month').html()+"&caldatey="+$('#cal-data-year').html()+"&caldateaction=prev" );
        })
        $("#cal-next-btn").click(function(){
            window.location.href = window.location.href.replace( /[\?#].*|$/, '?caldatem='+$('#cal-data-month').html()+"&caldatey="+$('#cal-data-year').html()+"&caldateaction=next" );
        })
        //Function called when user clicks "Show Next Seven Days"
        clickedDate = new Date();
        $("#showNextEvents").click(function(){
            clickedDate = new Date(clickedDate);
            clickedDate.setDate(clickedDate.getDate() + 7);
            showEventsForWeek(clickedDate, false)
        });
        
        var eventMap = {
            data: [
                 <% foreach (CAI_Event ev in events)
                    { %>
                    {
                        id: "<%= ev.id %>",
                        date: "<%= ev.GetShortStartDate() %>"
                    },
               <%  } %>

                ]
            };

        //Show all the events for seven days after the date param
            function showEventsForWeek(day, deleteOld) {                
                for (var i = 0, len = eventMap.data.length; i < len; i++) {
                    weekDatesMatch(eventMap.data[i].date, day);
                    if(day == eventMap.data[i].date || weekDatesMatch(eventMap.data[i].date, day)){ 
                        $('#event-item-' + eventMap.data[i].id).fadeIn();
                    } else if(deleteOld) {
                        $('#event-item-' + eventMap.data[i].id).fadeOut();
                    }
                }
               
            }

        //Return true if event date is in the following week of day param
            function weekDatesMatch(event, day){
                var eventDate = new Date(event);
                var eventDateMsg = eventDate.getDate()+'/'+ (eventDate.getMonth()+1) +'/'+eventDate.getFullYear();
                var evDate = new Date(day);
                for (i = 0; i < 7; i++) { 
                    evDate.setDate(evDate.getDate() + 1);
                    var dateMsg = evDate.getDate()+'/'+ (evDate.getMonth()+1) +'/'+evDate.getFullYear();
                    if(eventDateMsg == dateMsg){
                        return true;
                    }
                }
                return false;
            }
            
        //Adds a zero to months less than 10
            function addLeadingZero(num) {
                if (num < 10) {
                    return "0" + num;
                } else {
                    return "" + num;
                }
            }

        //Calendar Setup
            $(".responsive-calendar").responsiveCalendar({
                time: '<%= year+"-"+month %>',
                events: {
                <% 
                
    ArrayList evntdates = new ArrayList();
    foreach (CAI_Event ev in events)
    {
        int count = 1;

        if (evntdates.Count != 0)
        {
            foreach (string evnt in evntdates)
            {
                if (ev.GetShortStartDate() == evnt)
                {
                    count++;
                }
            }
        }
        evntdates.Add(ev.GetShortStartDate());
              %>
                    "<%=ev.GetShortStartDate() %>": {
                        "number": <%=count%>
                        },
      <% } %>

                },
                onDayClick: function (events) {
                    var date = $(this).data('year') + '-' + addLeadingZero($(this).data('month')) + '-' + addLeadingZero($(this).data('day'));
                    clickedDate = date;
                    showEventsForWeek(date, true);
                }
            });
        });
    </script>


