<%@ page language="C#" autoeventwireup="true" codebehind="default.aspx.cs" inherits="SitefinityWebApp.UpdateNews._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>

<body>
    <form id="form1" runat="server">
        <div>
            <button runat="server" id="updateNewsBtn">Update News Expiry Dates</button>
        </div>
        <div>
            <b>Status: </b>
            <asp:Label runat="server" ID="statusLabel"></asp:Label>
        </div>
        <div>
            <b>Total News Items: </b>
            <asp:Label runat="server" ID="lblTotalNews"></asp:Label>
        </div>
        <div>
            <b>Total News Items updated expiration date: </b>
            <asp:Label runat="server" ID="lblTotalNewsUpdated"></asp:Label>
        </div>
        <div>
            <b>Total News Items unpublished: </b>
            <asp:Label runat="server" ID="lblTotalNewsUnpublished"></asp:Label>
        </div>
    </form>

    <script src="https://code.jquery.com/jquery-1.11.3.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function ($) {

            var updateNewsBtn = jQuery("#<%= updateNewsBtn.ClientID %>");
            var lblTotalNews = jQuery("#<%= lblTotalNews.ClientID %>");
            var lblTotalNewsUpdated = jQuery("#<%= lblTotalNewsUpdated.ClientID %>");
            var lblTotalNewsUnpublished = jQuery("#<%= lblTotalNewsUnpublished.ClientID %>");
            var statusLabel = jQuery("#<%= statusLabel.ClientID %>");

            statusLabel.html("Not started.");
            lblTotalNews.html(0);
            lblTotalNewsUpdated.html(0);
            lblTotalNewsUnpublished.html(0);

            jQuery(updateNewsBtn).click(function (e) {
                statusLabel.html("Updating News... This can take several minutes.");
                $.getJSON("processUpdateNews.aspx", function (data) {
                    lblTotalNews.html(data.itemsCount);
                    lblTotalNewsUpdated.html(data.itemsUpdated);
                    lblTotalNewsUnpublished.html(data.itemsUnpublished);
                    statusLabel.html("Complete.");
                });
                e.preventDefault();
            });
        });
    </script>
</body>
</html>
