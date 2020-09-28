<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/UserControls/CAI_Custom_Controls/JobSearchListing__c.ascx.cs" Inherits="UserControls_CAI_Custom_Controls_JobSearchListing__c" %>

<%  CAI_JobListItem job = jobs.FirstOrDefault();  %>

<% if(job != null) {%>
<!--<div class="container"> commenting out to match new site nav --> <!-- Container start -->
    <div class="row"><!-- Row start-->
        <!--<div class="col-xs-12 col-md-8 col-md-push-2"> commenting out to match new site nav -->
	<div class="col-xs-12">
            <!--<h1 class="clearfix"><span class="col-xs-12 col-md-9 col-lg-10"><%= job.title %></span><span class="col-xs-12 col-md-3 col-lg-2 cai-font-18-bold">Ref#: <%= job.id %></span></h1> commenting out to rid of REF# --><!-- Job title & refernce number -->
	    <h1 class="clearfix col-xs-12"><%= job.title %></h1>
            <div><!-- Job details start -->
                <div class="col-xs-6 col-md-4">
                    <div id="created"><i class="fa fa-clock-o" aria-hidden="true"></i> Updated <%= job.createdOn %></div><!-- Date -->
                </div>
                <div class="col-xs-6 col-md-4">
                    <div id="contract"><i class="fa fa-briefcase" aria-hidden="true"></i> <%= job.contractType %></div><!-- Contract type -->
                </div>
                <div class="col-xs-6 col-md-4">
                    <div id="location"><i class="fa fa-map-marker" aria-hidden="true"></i> <%= job.location %></div><!-- Location -->
                </div>
                <div class="col-xs-12 cai-marg-top-20">
                    <!-- Decription text goes here -->
                    <%= job.description %>
                </div>
            </div><!-- Job details end -->
        </div>
    </div><!-- Row end -->
<%} %>
    <div class="row"><!-- Row start-->
	<div class="col-xs-12 cai-marg-top-30">
        	<div class="button-block style-1">
            		<a style="text-decoration: none;" class="btn-full-width btn col-xs-12" href="javascript:history.back(1)">Back to Listings</a>
        	</div>
	</div>
    </div><!-- Row end -->
    <div class="row"><!-- Row start-->
        <!--<div class="col-xs-12 col-md-8 col-md-push-2 cai-marg-top-20"> commenting out to match new site nav -->
	<div class="col-xs-12 cai-marg-top-20" style="display:none;">
            <h2>Apply for Job</h2>
        </div>
        <!-- Show below form if logged in, else hide -->
        <!--<div class="col-xs-12 col-md-8 col-md-push-2 cai-marg-top-20"> commenting out to match new site nav -->
	<div class="col-xs-12 cai-marg-top-20" style="display:none;">
            <form>
                <div class="form-group">
                    <label for="covernote">Cover note</label>
                    <textarea class="form-control" id="covernote" rows="4" placeholder="Cover note"></textarea>
                </div>
                <div class="form-group">
                    <label for="name">Name</label>
                    <input type="text" class="form-control" id="name" placeholder="Full name">
                </div>
                <div class="form-group">
                    <label for="phone">Phone</label>
                    <input type="email" class="form-control" id="phone" placeholder="Phone">
                </div>
                <div class="form-group">
                    <label for="exampleInputEmail1">Email</label>
                    <input type="email" class="form-control" id="exampleInputEmail1" placeholder="Email">
                </div>
                <div class="form-group">
                    <label for="cmdChooseCVBtn">CV</label>
		    <span class="button-block style-2">
                    	<input type="submit" class="btn" ID="cmdChooseCVBtn"  value="Choose file">
		    </span> <span>File name displayed here</span>
                </div>
                <div class="form-group text-right button-block style-1">
                    <input type="submit" class="btn-full-width btn" ID="cmdApplyJobBtn"  value="Apply">
                </div>
            </form>
        </div>
        <!-- Show below button if not logged in, else hide -->
        <!--<div class="col-xs-12 col-md-8 col-md-push-2 cai-marg-top-20"> commenting out to match new site nav -->
	<div class="col-xs-12 cai-marg-top-20" style="display:none;">
            <div class="form-group button-block style-1">
                <input type="submit" class="btn-full-width btn" ID="cmdApplyLoginBtn"  value="Login and Apply" />
            </div>
        </div>
    </div><!-- Row end -->
<!--</div> commenting out to match new site nav -->
