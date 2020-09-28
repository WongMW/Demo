<%@ Control Language="C#" AutoEventWireup="true" CodeFile="JobSearchListing__c.ascx.cs" Inherits="UserControls_CAI_Custom_Controls_JobSearchListing__c" %>

<%  CAI_JobListItem job = jobs.First();  %>

<div class="container"><!-- Container start -->
    <div class="row"><!-- Row start-->
        <div class="col-xs-12 col-md-8 col-md-push-2">
            <h1 class="clearfix"><span class="col-xs-12 col-md-9 col-lg-10"><%= job.title %></span><span class="col-xs-12 col-md-3 col-lg-2 cai-font-18-bold">Ref#: <%= job.id %></span></h1><!-- Job title & refernce number -->
            <div><!-- Job details start -->
                <div class="col-xs-6 col-md-4">
                    <div id="created"><span class="glyphicon glyphicon-time"></span> Updated <%= job.createdOn %></div><!-- Date -->
                </div>
                <div class="col-xs-6 col-md-4">
                    <div id="contract"><span class="glyphicon glyphicon-briefcase"></span> <%= job.contractType %></div><!-- Contract type -->
                </div>
                <div class="col-xs-6 col-md-4">
                    <div id="location"><span class="glyphicon glyphicon-map-marker"></span> <%= job.location %></div><!-- Location -->
                </div>
                <div class="col-xs-12 cai-marg-top-20">
                    <!-- Decription text goes here -->
                    <%= job.description %>
                </div>
            </div><!-- Job details end -->
        </div>
    </div><!-- Row end -->
    <div class="row"><!-- Row start-->
        <div class="cai-marg-top-30">
            <a class="col-xs-12 col-md-8 col-md-push-2 btn btn-default" href="javascript:history.back(1)"><span class="glyphicon glyphicon-circle-arrow-left"></span> Back to Listings</a>
        </div>
    </div><!-- Row end -->
    <div class="row"><!-- Row start-->
        <div class="col-xs-12 col-md-8 col-md-push-2 cai-marg-top-20">
            <h2>Apply for Job</h2>
        </div>
        <!-- Show below button if not logged in, else hide -->
        <div class="col-xs-12 col-md-8 col-md-push-2 cai-marg-top-20">
            <div class="form-group">
                <input type="submit" class="btn btn-primary" ID="cmdApplyLoginBtn"  value="Login and Apply" />
            </div>
        </div>
        <!-- Show below form if logged in, else hide -->
        <div class="col-xs-12 col-md-8 col-md-push-2 cai-marg-top-20">
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
                    <input type="submit" class="btn btn-default" ID="cmdChooseCVBtn"  value="Choose file"> <span>File name displayed here</span>
                </div>
                <div class="form-group text-right">
                    <input type="submit" class="btn btn-primary" ID="cmdApplyJobBtn"  value="Apply">
                </div>
            </form>
        </div>
    </div><!-- Row end -->
</div>