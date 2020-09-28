<%@ Control Language="C#" AutoEventWireup="true" CodeFile="JobSearch__c.ascx.cs" Inherits="UserControls_CAI_Custom_Controls_JobSearch__c" %>


<div class="container"><!-- Container start -->
    <div class="row"><!-- Row start-->
        <div class="col-xs-12 col-md-8 col-md-push-2">
            <h1>Job Listing</h1>

            <div class="form-group">
                <input class="form-control" type="text" id="TextBoxSearchTitle" runat="server" placeholder="Search by Job Title" />
            </div>
            <div id="filters">
                <div class="form-group">
                    <label for="contentPlaceholder_C001_txtboxSearchLocation" class="control-label">And / Or</label>
                    <select class="js-example-basic-multiple form-control" name="location-select[]" multiple="multiple">
                      <% foreach (CAI_Location entry in locations)
                         { %>
                             <option value="<%=entry.code %>"><%=entry.descrption %></option>
                      <% }  %>
                    </select>
                </div>
                <div class="form-group"> 
                    <asp:CheckBox ID="contractCheckBox" name="contract_type" class="checkbox-inline" runat="server" value="contract"  Text="Contract" AutoPostBack="false"></asp:CheckBox>
                    <asp:CheckBox ID="permanentCheckBox" name="permanent_type" class="checkbox-inline" runat="server" value="permanent"  Text="Permanent" AutoPostBack="false"></asp:CheckBox>
                </div>  
            </div>
            <!-- Don't remove this hidden input -->
            <input type="hidden" name="search" value="search">
            <br />
            <div class="form-group text-right">
                <button type="button" class="btn btn-default" id="toggleFilters">More Filters</button>
                <input type="submit" class="btn btn-primary" ID="cmdSearchBtn"  value="Search">
            </div>
        </div>
        <div class="col-xs-12 col-md-8 col-md-push-2">
            <!-- Results list start -->
            <% if (!jobs.Any() && (contractSearch || permanentSearch) )
                { %>
                    <p>There are no jobs available.</p>   
                <% } else { 
                    foreach(CAI_JobItem job in jobs){ %>
                    <div class="panel panel-default"><!-- Panel start -->
                        <div id="job-<%= job.id %>"><!-- Job div start -->
                            <div class="panel-heading cai-bg-primary-wine cai-font-white col-xs-12 cai-marg-bottom-15">
                                <div class="panel-title col-xs-9" id="job-title"><%= job.title %></div>
                                <div class="col-xs-3 text-right">Ref#: <%= job.id %></div>
                            </div>
                            <div class="panel-body"><!-- Panel body start -->
                                <div class="col-xs-4">
                                   <div id="contract"><span class="glyphicon glyphicon-briefcase"></span> <%= job.contractType %></div>
                                </div>
                                <div class="col-xs-4">
                                   <div id="created"><span class="glyphicon glyphicon-time"></span> Updated <%= job.createdOn %></div>
                                </div>
                                <div class="col-xs-4">
                                    <div id="location"><span class="glyphicon glyphicon-map-marker"></span> <%= job.location %></div>
                                </div>
                                <div class="col-xs-12 cai-marg-top-15">
                                    <!-- Don't change the style attr on this one -->
                                    <div id="job-description-<%=job.id %>">
                                        <p class="cai-height-fixed-45"><%= job.description %></p>
                                    </div>
                                </div>
                                <div class="col-xs-12">
                                    <a href="<%= HttpContext.Current.Request.Url.PathAndQuery %>/Job-Search-Listing?jobID=<%= job.id %>" class="btn btn-default col-xs-3 col-xs-push-9" id="job-read-btn-<%=job.id %>">More info</a>
                                </div>
                            </div><!-- Panel body end -->

                    </div><!-- Job div end -->
                </div>
                <% }
            } %>
    
        </div>
    </div><!-- Row end -->
</div><!-- Container end -->


<script type="text/javascript">
    $(".js-example-basic-multiple").select2({
        placeholder: "Select a County/City",
        allowClear: true
    });
    
    $(document).ready(function () {
        var filtersOn = false;
        $("#filters").hide();
        $("#toggleFilters").click(function () {
            if (!filtersOn) {
                $("#filters").fadeIn();
                $("#toggleFilters").html("Hide Filters");
                filtersOn = true;
            } else {
                $("#filters").fadeOut();
                $("#toggleFilters").html("Show Filters");
                filtersOn = false;
            }

        });
    });
       // $(".select2-basic-multiple").select2();

       /* $(document).ready(function () {
            $('a#cmdSearchBtn').on('click', function () {
                
                return false;
            });
        });*/
        $(document).ready(function () {
            
        });

</script>



