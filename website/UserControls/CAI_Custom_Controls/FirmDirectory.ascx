<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FirmDirectory.ascx.cs" Inherits="UserControls_CAI_Custom_Controls_FirmDirectory" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!-- Container start -->
<div class="container"><!-- Container start -->
    <div class="row"><!-- Row start-->
        <div class="col-xs-12 col-md-4 col-lg-3"><!-- LHS filter menu start -->
            <h2 class="hidden-xs">Filter By</h2>
            <div class="navbar navbar-default" role="navigation"><!-- .navbar start -->
                <div class="container-fluid"><!-- Containter fluid start -->

                    <!-- Small device menu start -->
                    <div class="navbar-header">
                        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#directory-filter">
                            <span class="sr-only">Toggle navigation</span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                        </button>
                        <span class="navbar-brand visible-xs">Filter By</span>
                    </div>
                    <!-- Small device menu end -->
                    
                    <div class="collapse navbar-collapse" id="directory-filter">
                        <ul class="nav" role="menu">
                            <li class="row cai-drodown-form">
                                <label class="control-label col-xs-9" for="contentPlaceholder_C001_chkBoxIncFirms">Include Firms</label>
                                <span class="col-xs-3"><asp:CheckBox class="pull-right" ID="chkBoxIncFirms" Checked="true" runat="server" /></span>
                            </li>
                            <li class="row cai-drodown-form">
                                <label class="control-label col-xs-9" for="contentPlaceholder_C001_chkBoxIncMembers">Include Members</label>
                                <span class="col-xs-3"><asp:CheckBox class="pull-right" ID="chkBoxIncMembers" Checked="true" runat="server" /></span>
                            </li>
                            <li class="row button-group">
                                <a href="#" class="dropdown-toggle" data-toggle="dropdown">Specialism <div class="pull-right"><b class="caret"></b></div></a>
                                <ul class="dropdown-menu col-xs-12">
                                    <li>
                                        <div class="row">
                                            <label class="control-label col-xs-12" for="contentPlaceholder_C001_checkAUD" data-value="option1" tabindex="-1">
                                                <span class="col-xs-9">Include Auditor Firms</span>
                                                <span class="col-xs-3"><asp:CheckBox class="pull-right" ID="checkAUD" Checked="true" runat="server" /></span>

                                            </label>
                                        </div>
                                    </li>
                                </ul>
                            </li>
                            <li class="row button-group">
                                <a href="#" class="dropdown-toggle" data-toggle="dropdown">Licence <div class="pull-right"><b class="caret"></b></div></a>
                                <ul class="dropdown-menu col-xs-12">
                                    <li>
                                        <div class="row">
                                            <label class="control-label col-xs-12" for="contentPlaceholder_C001_checkINS" data-value="option1" tabindex="-1">
                                                <span class="col-xs-9">Include Insolvency License Holders (GB/NI)</span>
                                                <span class="col-xs-3"><asp:CheckBox class="pull-right" ID="checkINS" Checked="true" runat="server" /></span>
                                            </label>
                                        </div>
                                    </li>
                                    <li class="divider col-md-12"></li>
                                    <li>
                                        <div class="row">
                                            <label class="control-label col-xs-12" for="contentPlaceholder_C001_checkIPC" data-value="option2" tabindex="-1">
                                                <span class="col-xs-9">Include Insolvency Practising Certificate Holders (ROI)</span>
                                                <span class="col-xs-3"><asp:CheckBox class="pull-right" ID="checkIPC" Checked="true" runat="server" /></span>
                                            </label>
                                        </div>
                                    </li>
                                    <li class="divider col-md-12"></li>
                                    <li>
                                        <div class="row">
                                            <label class="control-label col-xs-12" for="contentPlaceholder_C001_checkPC" data-value="option3" tabindex="-1">
                                                <span class="col-xs-9">Include Practising Certificate Holders</span>
                                                <span class="col-xs-3"><asp:CheckBox class="pull-right" ID="checkPC" Checked="true" runat="server" /></span>
                                            </label>
                                        </div>
                                    </li>
                                    <li class="divider col-md-12"></li>
                                    <li>
                                        <div class="row">
                                            <label class="control-label col-xs-12" for="contentPlaceholder_C001_checkIB" data-value="option4" tabindex="-1">
                                                <span class="col-xs-9">Include Investment Businesses</span>
                                                <span class="col-xs-3"><asp:CheckBox class="pull-right" ID="checkIB" Checked="true" runat="server" /></span>
                                            </label>
                                        </div>
                                    </li>
                                    <li class="divider col-md-12"></li>
                                    <li>
                                        <div class="row">
                                            <label class="control-label col-xs-12" for="contentPlaceholder_C001_checkDPB" data-value="option4" tabindex="-1">
                                                <span class="col-xs-9">Include Desginated Professional Bodies</span>
                                                <span class="col-xs-3"><asp:CheckBox class="pull-right" ID="checkDPB" Checked="true" runat="server" /></span>
                                            </label>
                                        </div>
                                    </li>
                                </ul>
                            </li>                   
                        </ul>
                    </div>
                </div><!-- Containter fluid end -->
            </div><!-- .navbar end -->
        </div><!-- LHS filter menu end -->
        
        <!-- RHS search box and results list start -->
        <div class="col-xs-12 col-md-8 col-lg-9">
            <!-- Change the "hide" class to "show" to display alert dialog box so think you'll have to add a bit of logic to code behind -->
            <div class="alert alert-warning hide">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">x</button>
                <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="false" />
            </div>
            <h1>Find a Firm/Member</h1>
            <!--<form role="search">-->
                <div class="form-group">
                    <input class="form-control" type="text" ID="TextBoxSearchName" runat="server" placeholder="Search by Firm/Member name" />
                </div>
                <div id="filters">
                    <div class="form-group">
                        <label for="contentPlaceholder_C001_txtboxSearchLocation" class="control-label">And / Or</label>
                    </div>
                    <div class="form-group btn-group" role="group" aria-label="Location search filter">
                        <button type="button" class="btn btn-primary">Ireland (interactive map)</button>
                        <button type="button" class="btn btn-primary">Worldwide (interactive map)</button>
                        <button type="button" class="btn btn-primary">Search by text</button>
                    </div>
                    <div class="form-group">
                        <div id="searchlocation">
                            <input class="form-control" type="hidden" ID="TextBoxSearchLoc" runat="server" placeholder="Search by County/City" />
                        </div>
                        <div id="mapdiv" style="width: 100%; height: 35em; background-color:#fff; padding-bottom: 2em;"></div>
                    </div>
                    
                </div>
                <div class="form-group text-right">
                     <button type="button" class="btn btn-default" id="toggleFilters">More Filters</button>
                    <input type="submit" class="btn btn-primary" ID="cmdSearchBtn"  value="Search">
                 </div>
            <!--</form>-->
            
            <div class="clearfix"></div>
            <!-- For testing purposes feel free to remove-->
            <!--Search Name: <%= searchName %>, Search Location: <%= searchLoc %>-->

            <% if(Firms.Any()) { %>
            <div id="firmsTable" runat="server">
                <h2>Firms</h2>                
                <table class="table table-hover table-responsive">
                    <%
                        foreach (CAI_FirmListItem firm in Firms)
                        {
                            %>
                                <tr>
                                    <td>
                                        <div class="col-sm-9 cai-font-1em-bold"><%= firm.itemName %></div>
                                        <div class="col-sm-3 text-right"><a href="<%= HttpContext.Current.Request.Url.PathAndQuery %>/firmdirectorylisting?id=<%= firm.itemID %>&type=firm" class="col-xs-12 btn btn-default">More Info</a></div>
                                    </td>
                                </tr>
                            <%
                        }
                         %>

                </table>
            </div>
            <% } %>
            <% if(Members.Any()) { %>
            <div id="membersTable" runat="server">
                <h2>Members</h2>
                <table class="table table-hover table-responsive">
                    <%   
                     foreach (CAI_FirmListItem member in Members)
                        {
                            %>
                                <tr>
                                    <td>
                                        <div class="col-sm-9 cai-font-1em-bold"><%= member.itemName %></div>
                                        <div class="col-sm-3 text-right"><a href="<%= HttpContext.Current.Request.Url.PathAndQuery %>/firmdirectorylisting?id=<%= member.itemID %>&type=member"  class="col-xs-12 btn btn-default">More Info</a></div>
                                    </td>
                                </tr>
                            <%
                        }
                         %>

                </table>
            </div>
            <% } %>
        </div>
        <!-- RHS search box and results list end -->
    </div><!-- Row end -->


    <cc3:User ID="AptifyEbusinessUser1" runat="server" />


    <script>

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

        var options = [];

        $('.dropdown-menu label').on('click', function (event) {

            var $target = $(event.currentTarget),
                val = $target.attr('data-value'),
                $inp = $target.find('input'),
                idx;

            if ((idx = options.indexOf(val)) > -1) {
                options.splice(idx, 1);
                setTimeout(function () { $inp.prop('checked', false) }, 0);
            } else {
                options.push(val);
                setTimeout(function () { $inp.prop('checked', true) }, 0);
            }

            $(event.target).blur();

            console.log(options);
            return false;
        });
    
        

		var map;

        AmCharts.ready(function() {
            map = new AmCharts.AmMap();

            map.theme = AmCharts.themes.dark;
            map.borderColor = "#8C1D40";
            map.balloon.color = "#8C1D40";

            var dataProvider = {
                mapVar: AmCharts.maps.irelandHigh,
                getAreasFromMap:true
            };

            map.dataProvider = dataProvider;

            map.areasSettings = {
                autoZoom: true,
                selectedColor: "#003D51",
                showDescriptionOnHover: true,
                color: "#8C1D40"
            };

            map.smallMap = new AmCharts.SmallMap();

            map.addListener("clickMapObject", function (event) {
                if (event.mapObject.id == "GB-NIR") {
                    //window.location.replace("http://google.com");
                }
                else if (event.mapObject.id == "IE-D") {
                    $('#searchlocation input').val('Dublin');
                    //alert($('#searchlocation input').val());
                    $("#cmdSearchBtn").click();
                }
            });
				
            map.write("mapdiv");

        });
       
        </script>
</div><!-- Container end -->

