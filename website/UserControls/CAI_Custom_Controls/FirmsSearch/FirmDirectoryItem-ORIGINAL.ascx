<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FirmDirectoryItem.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.Jim_UserControls.FirmDirectory.FirmDirectoryItem" %>

 <link rel='stylesheet' href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />

    <script type="text/javascript">
        $(document).ready(function () {
            $("#demo").on("hide.bs.collapse", function () {
                $("#bp").html('<span class="glyphicon glyphicon-collapse-down"></span> View Principals');
            });
            $("#demo").on("show.bs.collapse", function () {
                $("#bp").html('<span class="glyphicon glyphicon-collapse-up"></span> Hide Principals ');
            });
        });
  </script>
<%--<div class="container clearfix"> 
    <div class="row">
        <!-- This Div will be invisible if the type is member-->
        <% if(ItemType == "firm") { %>
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
        <% if( ItemType== "member") { %>
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

    </div>
    <div class="row">
        <div class="cai-marg-top-30">
            <a class="col-xs-12 col-md-6 col-md-push-3 btn btn-default" href="javascript:history.back(1)"><span class="glyphicon glyphicon-circle-arrow-left"></span> Back to Listings</a>
        </div>
    </div>
</div> --%>
<div class="container clearfix"> 
    <div class="row">
        <!-- This Div will be invisible if the type is member-->
        <% if(itemType == "f") { %>
   
            <div class="col-xs-12 col-md-6 col-md-push-3">
                <div id="firm-listing">
                    <div class="row">
                        <h3 class="col-xs-12"><asp:Label ID="lblFirmName" runat="server" Text="" /></h3>
                    </div>
                    <div class="row">
                        <div class="col-xs-12"> <asp:Label ID="liblc" runat="server" Text="" /></div> 
                                         
                    </div>
  <div class ="row">   
          
      <div class="col-xs-12">
        <button id="bp" type="button" class="btn btn-info" data-toggle="collapse" runat="server" data-target="#demo">
            <span class="glyphicon glyphicon-collapse-down"></span> View Principals
        </button>
        </div>
       <div class="col-xs-12">
          <div id="demo" class="collapse">
                            <asp:Repeater ID="R1" runat="server">
                                            <ItemTemplate>                                                                    
                                            <%# Eval("c") +". "+ Eval("pname") %> <br />
                                            </ItemTemplate>
                                        </asp:Repeater> 
          </div>

          </div>  
  </div>                       
                    <div class="row">
                        <div class="col-xs-4"> <strong><asp:Label ID="ltradingname" runat="server" Text="Trading Names : "></asp:Label></strong></div>
                        <div class="col-xs-8"><asp:Label ID="rtradingname" runat="server" Text="" /></div>
                    </div>

                    <div class="row">
                        <div class="col-xs-4"><span class="glyphicon glyphicon-home"></span> <strong>Address :</strong></div>
                        <div class="col-xs-8">
                            <asp:Label ID="lblAddressLine1" runat="server" Text="" />
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
                        <div class="col-xs-8"><a href="mailto:#"><asp:Label ID="lblMainEmail" runat="server" Text="" /></a></div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4"><span class="glyphicon glyphicon-envelope"></span> <strong>Info Email:</strong></div>
                        <div class="col-xs-8"><a href="mailto:#"><asp:Label ID="lblInfoEmail" runat="server" Text="" /></a></div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4"><span class="glyphicon glyphicon-envelope"></span> <strong>Jobs Email:</strong></div>
                        <div class="col-xs-8"><a href="mailto:#"><asp:Label ID="lblJobsEmail" runat="server" Text="" /></a></a></div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4"><span class="glyphicon glyphicon-envelope"></span> <strong>Training Email:</strong></div>
                        <div class="col-xs-8"><a href="mailto:#"><asp:Label ID="lblTrainingEmail" runat="server" Text="" /></a></div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4"><span class="glyphicon glyphicon-link"></span> <strong>Website:</strong></div>
                        <div class="col-xs-8"><abbr title="Website"><asp:Label ID="lblWebsite" runat="server" Text="" /></abbr></div>
                    </div>                               
                </div>
                <br />
                    <asp:Repeater ID="R2" runat="server">                 
                        <ItemTemplate>
                            <div class ="row">
                                <div class=" text-center">
                                     <b class="col-xs-12" ><%# Eval("firmName") %></b> <br />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-4"><span class="glyphicon glyphicon-home"></span> <strong>Address :</strong></div>
                                <div class="col-xs-8">
                                     <%# Eval("add") %> <br />
                                </div>
                            </div>                                                              
                    <div class="row">
                        <div class="col-xs-4"><span class="glyphicon glyphicon-phone-alt"></span> <strong>Phone:</strong></div>
                        <div class="col-xs-8">  <%# Eval("phoneext") %> +    <%# Eval("phoneno") %> <br /> </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4"><span class="glyphicon glyphicon-print"></span> <strong>Fax:</strong></div>
                        <div class="col-xs-8"> <%# Eval("faxno") %> <br /></div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4"><span class="glyphicon glyphicon-envelope"></span> <strong>Email:</strong></div>
                        <div class="col-xs-8"><a href="mailto:#"> <%# Eval("mainemail") %> <br /></a></div>
                    </div>
                    <div class ="row">
                                <div class=" text-center">
                                    <div class="col-xs-12"><span class="glyphicon glyphicon-option-horizontal"></span> </div>
                                </div>
                    </div>
                            <br />
                        </ItemTemplate>
                        </asp:Repeater> 
            </div>



        <% } %>

        <!-- This Div will be invisible if the type is firm-->
        <% if(itemType == "m") { %>
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

    </div>
    <div class="row">
        <div class="cai-marg-top-30">
            <a  id ="jb1" class ="col-xs-12 col-md-6 col-md-push-3 btn btn-default" href="javascript:history.back(1)"><span class="glyphicon glyphicon-circle-arrow-left"></span> Back to Listings</a>
        </div>
    </div>
</div> 