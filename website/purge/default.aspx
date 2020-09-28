<%@ page language="C#" autoeventwireup="true" codebehind="default.aspx.cs" inherits="SitefinityWebApp.purge._default" enableeventvalidation="false" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input type="text" id="txtPagetitle" runat="server" placeholder="Page URL" />
            <button runat="server" id="purgePagesBtn">Purge Pages</button>
        </div>
        <div><span>To find the url, go to pages in the sitefinity backend and select "Actions -> Title & Properties" on the desired page.</span></div>
        <div>
            <b>Status: </b>
             <asp:label runat="server" id="statusLabel"></asp:label>
        </div>
        <div>
            <b>Total Pages purged: </b>
            <asp:label runat="server" id="lblTotalPages"></asp:label>
        </div>
    </form>

    <script src="https://code.jquery.com/jquery-1.11.3.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function ($) {

            var purgePagesBtn = jQuery("#<%= purgePagesBtn.ClientID %>");
            var txtPagetitle = jQuery("#<%= txtPagetitle.ClientID %>");
            var lblTotalPages = jQuery("#<%= lblTotalPages.ClientID %>");
            var statusLabel = jQuery("#<%= statusLabel.ClientID %>");

            statusLabel.html("Not started.");
            lblTotalPages.html(0);

            jQuery(purgePagesBtn).click(function (e) {
                statusLabel.html("Purging... This can take several minutes.");
                var val = jQuery(txtPagetitle).val();

                if (val) {
                    $.getJSON("processPurge.aspx?item=" + val, function (data) {
                        lblTotalPages.html(data.pageCount);
                        statusLabel.html("Complete.");
                    });
                }

                e.preventDefault();
            });
        });
    </script>
</body>
</html>
