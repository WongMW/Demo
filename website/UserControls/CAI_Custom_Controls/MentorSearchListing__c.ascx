<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MentorSearchListing__c.ascx.cs" Inherits="UserControls_CAI_Custom_Controls_MentorSearchListing__c" %>



<div class="container"><!-- Container start -->
    <div class="row"><!-- Row start-->
        <div class="col-xs-12 col-md-8 col-md-push-2">
            <h1>Mentor Details</h1>
            <div class="col-xs-12 col-sm-6">
                <div class="col-xs-6"><strong>Mentor ID:</strong></div>
                <div class="col-xs-6"><p>F1</p></div><!-- Mentor ID -->
            </div>
            <div class="col-xs-12 col-sm-6">
                <div class="col-xs-6"><strong>Gender:</strong></div>
                <div class="col-xs-6"><p>Female</p></div><!-- Mentor Gender -->
            </div>
            <div class="col-xs-12 col-sm-6">
                <div class="col-xs-6"><strong>Yrs of experience:</strong></div>
                <div class="col-xs-6"><p><span class="btn-success badge">10+</span></p></div><!-- Years experience -->
            </div>
            <div class="col-xs-12 col-sm-6">
                <div class="col-xs-6"><strong>Yrs as Sole Practioner:</strong></div>
                <div class="col-xs-6"><p><span class="btn-info badge">21+ years</span></p></div><!-- Years as sole practioner -->
            </div>
            <div class="col-xs-12 col-sm-6">
                <div class="col-xs-6"><strong>Current role:</strong></div>
                <div class="col-xs-6"><p><span class="btn-primary badge">Accountant</span></p></div><!-- Current position -->
            </div>
            <div class="col-xs-12 col-sm-6">
                <div class="col-xs-6"><strong>Trained in:</strong></div>
                <div class="col-xs-6"><p><span class="btn-danger badge">Industry</span></p></div><!-- Area trained in -->
            </div>
            <div class="col-xs-12 col-sm-6">
                <div class="col-xs-6"><strong>Expertise:</strong></div>
                <div class="col-xs-6"><p>Accounting & Financial Reporting, Taxation, Budgeting/Forecasting, Teaching/Lecturing, Not for profit/Charity Accounting</p></div><!-- Areas of expertise -->
            </div>
            <div class="col-xs-12 col-sm-6">
                <div class="col-xs-6"><strong>Positions held:</strong></div>
                <div class="col-xs-6"><p>Global Operational Risk Manager, Financial Controller, Internal Audit, Operational Risk</p></div><!-- Current position -->
            </div>
            <div class="col-xs-12">
                <div class="col-xs-12 col-md-6"><strong>Structure of firms worked in post qualification:</strong></div>
                <div class="col-xs-12 col-md-6"><p>6 months PLC, 6 months education (not teaching,) 6 months practice, 1 year Not for Profit, 5 years Government/Public Sector, Part-time Education (teaching/academe) 5 years.</p></div><!-- Sturcture of companies worked for -->
                <div class="col-xs-12 col-md-6"><strong>Industry sector & areas of professional expertise:</strong></div>
                <div class="col-xs-12 col-md-6"><p>6 months each in Banking/FS/Insurance, Manufacturing and Education/Academe 1 year Voluntary, 5 years in Media/Communications/Publishing</p></div><!-- Industry sector and areas of expertise -->
                <div class="col-xs-12 col-md-6"><strong>Non-Exec directorships:</strong></div>
                <div class="col-xs-12 col-md-6"><p>Corporate Banking/Iinternational, Relationship and Portfolio Management</p></div><!-- Non-Exec directorships -->

            </div>
        </div>
        
    </div><!-- Row end -->
    <div class="row"><!-- Row start-->
        <div class="cai-marg-top-30">
            <a class="col-xs-12 col-md-8 col-md-push-2 btn btn-default" href="javascript:history.back(1)"><span class="glyphicon glyphicon-circle-arrow-left"></span> Back to Listings</a>
        </div>
    </div><!-- Row end -->
    <div class="row"><!-- Row start-->
        <div class="col-xs-12 col-md-8 col-md-push-2 cai-marg-top-20">
            <h2>Apply for this Mentor</h2>
            <form>
                <div class="form-group">
                    <label for="name">Name</label>
                    <input type="text" class="form-control" id="name" placeholder="Full name">
                </div>
                <div class="form-group">
                    <label for="exampleInputEmail1">Email</label>
                    <input type="email" class="form-control" id="exampleInputEmail1" placeholder="Email">
                </div>
                <div class="form-group">
                    <label for="covernote">Comment</label>
                    <textarea class="form-control" id="covernote" rows="4" placeholder="Cover note"></textarea>
                </div>
                <div class="form-group text-right">
                    <input type="submit" class="btn btn-primary btn-lg" ID="cmdApplyJobBtn"  value="Apply">
                </div>
            </form>
        </div>
    </div><!-- Row end -->
</div>