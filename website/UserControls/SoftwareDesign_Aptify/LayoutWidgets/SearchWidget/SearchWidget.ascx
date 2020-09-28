<%@ Control Language="C#" %>
<%@ Register TagPrefix="sd" TagName="IncludeScript" Src="~/UserControls/SoftwareDesign_Aptify/LayoutWidgets/IncludeScriptUsingJavascript.ascx" %>
<asp:Label ID="MessageLabel" Text="Text" runat="server" Visible="false" />


<div runat="server" class="sf_cols">
    <div runat="server" class="sf_colsOut sf_1cols_1_100">
        <div runat="server" class="sf_colsIn sf_1cols_1in_100">
            <div class="search-widget">
                <a href="#" runat="server" id="searchBarIcon">
                    <i class="fa fa-search" id="searchIcon" runat="server"></i>
                </a>
                <div class="search-wrapper" runat="server" id="headerSearchInput">
                    <input id="txtSearchBox" runat="server" type="text" placeholder="Search..." name="search" value="" class="search-box" />
                    <input id="UrlHolder" name="UrlHolder" runat="server" value="" type="hidden" />
                    <a id="searchButton" runat="server" href="#" class="btn btn-search">Search</a>
                </div>
            </div>
        </div>
    </div>
</div>

<script type="text/javascript">
    jQuery(document).ready(function () {

        jQuery("#<%= txtSearchBox.ClientID %>").keypress(function (e) {
            if (e.which == 13) {
                var query = jQuery('#<%= headerSearchInput.ClientID %> input[name]').val();
                var url = jQuery('#<%= headerSearchInput.ClientID %> input[type=hidden]').val();
                submitSearchForm(query, url);
                return false; 
            }
        });

        jQuery("#<%= searchBarIcon.ClientID %>").click(function () {
            jQuery("#<%= headerSearchInput.ClientID %>").toggleClass("display");
            jQuery("#<%= searchIcon.ClientID %>").toggleClass("active");
            return false;
        });

        jQuery("#<%= searchButton.ClientID %>").click(function () {
            var query = jQuery('#<%= headerSearchInput.ClientID %> input[name]').val();
            var url = jQuery('#<%= headerSearchInput.ClientID %> input[type=hidden]').val();
            submitSearchForm(query, url);
            return false;
        });

    });

    function submitSearchForm(query, url) {
        var form = document.createElement("form");
        var queryElement = document.createElement("input");

        var hiddenElement1 = document.createElement("input");
        var hiddenElement2 = document.createElement("input");

        form.method = "GET";
        form.action = url;

        query = encodeURI(query);
        queryElement.value = query.trim();
        queryElement.name = "searchQuery";
        form.appendChild(queryElement);

        hiddenElement1.name = "indexCatalogue";
        hiddenElement1.value = "_all";
        form.appendChild(hiddenElement1);

//        hiddenElement2.name = "wordsMode";
//        hiddenElement2.value = "0";
//       form.appendChild(hiddenElement2);


        document.body.appendChild(form);

        form.submit();
    }


</script>
