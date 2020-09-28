<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MentorsSearch.ascx.cs" Inherits="UserControls_CAI_Custom_Controls_MentorsSearch" %>

<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>



<!-- Container start -->
<div class="container"><!-- Container start -->
    <div class="row"><!-- Row start-->
        <div class="col-xs-12 col-md-8 col-md-push-2">
            <h1>Find a Mentor</h1>
            <!-- Change the "hide" class to "show" to display alert dialog box so think you'll have to add a bit of logic to code behind -->
            <div class="alert alert-warning hide">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">x</button>
                <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="false" />
            </div>
            <div class="form-group">
                <input class="form-control" type="text" ID="TextBoxSearchLocation" runat="server" placeholder="Search by County/City" />
            </div>
            <div id="filters">
                <div class="form-group">
                    <label class="control-label">And / Or</label>
                    <select class="form-control">
                        <option value="" selected disabled>-- Please select --</option>
                        <option value="All">All Catergories</option>
                        <option value="Business">In Business</option>
                        <option value="Practice">In Practice</option>
                    </select>
                </div>
                <div class="form-group">
                    <label class="checkbox-inline">
                      <input type="checkbox" name="pref_male_type" value="Male" checked> Male
                    </label>
                    <label class="checkbox-inline">
                      <input type="checkbox" name="pref_female_type" value="Female" checked> Female
                    </label>
                </div>
            </div>
            <div class="form-group text-right">
                <button type="button" class="btn btn-default" id="toggleFilters">More Filters</button>
                <input type="submit" class="btn btn-primary" ID="cmdSearchBtn"  value="Search">
            </div>
        </div>
        <div class="col-xs-12 col-md-8 col-md-push-2">
            <!-- Results list start -->
             <div id="membersTable" runat="server">                
                <table class="table table-hover table-responsive">
                    <%   
                        foreach (CAI_MentorListItem mentor in Mentors)
                        {
                            %>
                                <tr>
                                    <td>
                                        <div class="col-sm-9 cai-font-1em-bold"><%= mentor.itemName %></div>
                                        <div class="col-sm-3 text-right"><a href="<%= HttpContext.Current.Request.Url.PathAndQuery %>/firmdirectorylisting?id=<%= mentor.itemID %>&type=member"  class="col-xs-12 btn btn-default">More Info</a></div>
                                    </td>
                                </tr>
                            <%
                        }
                         %>

                </table>
            </div>
            <!-- Results list end -->
        </div>
    </div><!-- Row end -->
    <cc3:User ID="AptifyEbusinessUser1" runat="server" />
</div><!-- Container end -->

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
    </script>



