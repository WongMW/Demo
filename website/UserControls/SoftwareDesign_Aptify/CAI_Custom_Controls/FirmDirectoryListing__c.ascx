<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/UserControls/CAI_Custom_Controls/FirmDirectoryListing__c.ascx.cs" Inherits="UserControls_CAI_Custom_Controls_FirmDirectoryListing__c" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<div class="container clearfix"> 

    <div class="row">
        <!-- This Div will be invisible if the type is member-->
        <% if(itemType == "firm") { %>
            <div class="col-xs-12 col-md-6 col-md-push-3">
                <div id="firm-listing">
                    <div class="row">
                        <h1 class="col-xs-12"><asp:Label ID="lblFirmName" runat="server" Text="Firm Name" /></h1>
                    </div>
                    <div class="row">
                        <div class="col-xs-4"><span class="glyphicon glyphicon-home"></span> <strong>Address:</strong></div>
                        <div class="col-xs-8">
                            <asp:Label ID="lblAddressLine1" runat="server" Text="Address 1" /> <asp:Label ID="lblAddressLine2" runat="server" Text="Address 1" /><br /> 
                            <asp:Label ID="lblAddressLine3" runat="server" Text="Address 2" /> <br />
                            <asp:Label ID="lblAddressLine4" runat="server" Text="Address 3" /><br />
                            <asp:Label ID="lblAddressCity" runat="server" Text="Address City" /><br />
                            <asp:Label ID="lblAddressCounty" runat="server" Text="Address County" /> <asp:Label ID="lblAddressPostCode" runat="server" Text="Address Postcode" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4"><span class="glyphicon glyphicon-phone-alt"></span> <strong>Phone:</strong></div>
                        <div class="col-xs-8"><asp:Label ID="lblPhone" runat="server" Text="Phone Number" /></div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4"><span class="glyphicon glyphicon-print"></span> <strong>Fax:</strong></div>
                        <div class="col-xs-8"><asp:Label ID="lblFax" runat="server" Text="Fax Number" /></div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4"><span class="glyphicon glyphicon-envelope"></span> <strong>Main Email:</strong></div>
                        <div class="col-xs-8"><asp:Label ID="lblMainEmail" runat="server" Text="Main Email" /></div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4"><span class="glyphicon glyphicon-envelope"></span> <strong>Info Email:</strong></div>
                        <div class="col-xs-8"><asp:Label ID="lblInfoEmail" runat="server" Text="Info Email" /></div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4"><span class="glyphicon glyphicon-envelope"></span> <strong>Jobs Email:</strong></div>
                        <div class="col-xs-8"><asp:Label ID="lblJobsEmail" runat="server" Text="Jobs EMail" /></div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4"><span class="glyphicon glyphicon-envelope"></span> <strong>Training Email:</strong></div>
                        <div class="col-xs-8"><asp:Label ID="lblTrainingEmail" runat="server" Text="Training Email" /></div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4"><span class="glyphicon glyphicon-link"></span> <strong>Website:</strong></div>
                        <div class="col-xs-8"><asp:Label ID="lblWebsite" runat="server" Text="Website" /></div>
                    </div>
                   <div class="row">
                        <div class="col-xs-4"><span class="glyphicon glyphicon-user"></span> <strong>No. of Employees:</strong></div>
                        <div class="col-xs-8"><span class="btn-primary badge"><asp:Label ID="lblNumberOfEmployees" runat="server" Text="Number of Employees" /></span></div>
                    </div>
                    
                    
                    
                </div>
            </div>
        <% } %>

        <!-- This Div will be invisible if the type is firm-->
        <% if(itemType == "member") { %>
            <div class="col-xs-12 col-md-6 col-md-push-3">
                <div id="member-listing">
                    <div class="row">
                        <h1 class="col-xs-12"><asp:Label ID="lblMemberFullName" runat="server" Text="Member Name" /></h1>
                    </div>
                    <div class="row">
                        <div class="col-xs-4"><span class="glyphicon glyphicon-time"></span> <strong>Member since:</strong></div>
                        <div class="col-xs-8"><asp:Label ID="lblMemberJoinDate" runat="server" Text="Member Join Date" /></div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4"><span class="glyphicon glyphicon-briefcase"></span> <strong>Company:</strong></div>
                        <div class="col-xs-8"><asp:Label ID="lblMemberCompany" runat="server" Text="Member Company" /></div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4"><span class="glyphicon glyphicon-envelope"></span> <strong>Email:</strong></div>
                        <div class="col-xs-8"><asp:Label ID="lblMemberEmail" runat="server" Text="Member Email" /></div>
                    </div>
                </div>
            </div>
        <% } %>

        <asp:Label id="lblError" ForeColor="Red" runat="server" Visible="False"  />
        <cc3:User id="AptifyEbusinessUser1" runat="server" />
    </div>
    <div class="row">
        <div class="cai-marg-top-30">
            <div class="submitBtn col-xs-12 col-md-6 col-md-push-3" ><a href="javascript:history.back(1)"><span class="glyphicon glyphicon-circle-arrow-left"></span> Back to Listings</a></div>
        </div>
    </div>
</div> 

