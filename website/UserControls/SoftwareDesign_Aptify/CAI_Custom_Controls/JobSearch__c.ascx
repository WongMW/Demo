<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/UserControls/CAI_Custom_Controls/JobSearch__c.ascx.cs" Inherits="UserControls_CAI_Custom_Controls_JobSearch__c" %>

<style>
    .checkbox-inline {
        line-height: 2.5em;
    }
</style>

<!-- Container start -->
<!--<div class="container"> commenting out to match new site nav -->
    <div class="row"><!-- Row start-->
        <!--<div class="col-xs-12 col-md-8 col-md-push-2"> commenting out to match new site nav -->
        <div class="col-xs-12">
            <div class="form-group" style="display:none;">
                <input class="form-control" type="text" id="TextBoxSearchTitle" runat="server" placeholder="Search by Job Title" />
            </div>
            <div>
                <div class="form-group">
                 <!--<label for="contentPlaceholder_C001_txtboxSearchLocation" class="control-label">And / Or</label><br/>-->
                    <select class="form-control js-example-basic-multiple" name="location-select[]" multiple="multiple">
                      <% foreach (CAI_Location entry in locations)
                         { %>
                             <option value="<%=entry.code %>"  <%= (searchLocations.Contains(entry.code))?"selected=\"selected\"" : "" %> ><%=entry.descrption %></option>
                      <% }  %>
                    </select>
                </div>
                <div class="form-group">
                    <asp:CheckBox ID="contractCheckBox" name="contract_type" class="checkbox-inline" runat="server" value="contract" Text="Contract" AutoPostBack="false" Checked="true"></asp:CheckBox>
                    <asp:CheckBox ID="permanentCheckBox" name="permanent_type" class="checkbox-inline" runat="server" value="permanent"  Text="Permanent" AutoPostBack="false" Checked="true"></asp:CheckBox>
                </div>  
            </div>
            <!-- Don't remove this hidden input -->
            <input type="hidden" name="search" value="search">
            <br />
            <div class="form-group text-right">
                <!--<button type="button" class="submitBtn" id="toggleFilters">More Filters</button>-->
                <input type="submit" class="submitBtn" ID="cmdSearchBtn"  value="Search">
            </div>
        </div>
      <!--<div class="col-xs-12 col-md-8 col-md-push-2"> commenting out to match new site nav -->
	<div class="col-xs-12">
            <!-- Results list start -->
            <%
                if ( searchLocations.Any() || contractSearch || permanentSearch)
                {
                    if (!jobs.Any())
                    { %>
                        <p>There are no jobs available.</p>   
                 <% }
                    else
                    {
                        foreach (CAI_JobItem job in jobs)
                        { %>
                    <div class="jobs-box panel panel-default"><!-- Panel start -->
                        <div id="job-<%= job.id %>"><!-- Job div start -->
                            <div class="panel-heading cai-bg-primary-wine cai-font-white col-xs-12 cai-marg-bottom-15">
                                <div class="panel-title col-xs-12" id="job-title"><%= job.title %></div>
                                <!--<div class="col-xs-3 text-right">Ref#: <%= job.id %></div> commenting out to rid of REF# and changed div above from col-xs-9-->
                            </div>
                            <div class="panel-body"><!-- Panel body start -->
                                <div class="col-xs-4">
                                   <div id="contract"><i class="fa fa-briefcase" aria-hidden="true"></i> <%= job.contractType %></div>
                                </div>
                                <div class="col-xs-4">
                                   <div id="created"><i class="fa fa-clock-o" aria-hidden="true"></i> Updated <%= job.createdOn %></div>
                                </div>
                                <div class="col-xs-4">
                                    <div id="location"><i class="fa fa-map-marker" aria-hidden="true"></i> <%= job.location %></div>
                                </div>
                                <div class="col-xs-12 cai-marg-top-15">
                                    <!-- Don't change the style attr on this one -->
                                    <div id="job-description-<%=job.id %>">
                                        <p class="cai-height-fixed-45"><%= job.description %></p>
                                    </div>
                                </div>
                                <div class="col-xs-12">
                                    <div class="button-block style-1" style="text-align: center;"><a  style="text-decoration: none;" class="btn-full-width btn" href="<%= HttpContext.Current.Request.Url.PathAndQuery %>/Job-Search-Listing?jobID=<%= job.id %>" id="job-read-btn-<%=job.id %>">More Info</a></div>
                                </div>
                            </div><!-- Panel body end -->

                    </div><!-- Job div end -->
                </div>
                      <% }
                     }
                } %>
    
        </div>
    </div><!-- Row end -->

<script>
 

</script>
<!--</div> commenting out to match new site nav --><!-- Container end -->






