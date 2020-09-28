<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="SitefinityWebApp.migration._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    Content Migration script (<asp:Label ID="lblStatus" runat="server"></asp:Label>)
    </div>
    <div>
        <input type="radio" id="chkAllItems" name="processType" /> Process All Items <br />
        <input type="radio" id="chkOnlyNews" name="processType" /> Process Only News <br />
        <input type="radio" id="chkOnlyPodcasts" name="processType" /> Create Podcasts Only<br />
        <input type="radio" id="chkOnlyCategories" name="processType" /> Create Categories Only <br />
        <input type="radio" id="chkSpecificTypeID" name="processType" /> Specific ID Only <br />

        <br />

        <a href="#" id="hrefStartMigration">Start Migration</a> ---
        <a href="#" id="hrefStopMigration">Stop Migration</a>
    </div>
    <div>
        <input type="text" id="txtProcessNumber" />
        <a href="#" id="hrefProcessNumber">Process</a>
    </div>
    <div>
        <b>Total Pages in Sitefinity before migration:</b>
        <asp:Label runat="server" ID="lblTotalPages"></asp:Label>
    </div>
    <div>
        <b>Processing:</b>
        <asp:Label runat="server" ID="lblProgress"></asp:Label> of <asp:Label runat="server" ID="lblTotalProgressPages"></asp:Label>
    </div>
    <div>
        <b>Total Pages not imported due to errors:</b>
        <div>
            <b>URLs:</b>
            <asp:Label runat="server" ID="lblUrls"></asp:Label>
        </div>

        <asp:Label runat="server" ID="lblImportErrors"></asp:Label>
    </div>
    <div>
        <b>index errors:</b>
        <asp:Label runat="server" ID="lblindexerrors"></asp:Label>
    </div>
    <div>
        <b>Exception List:</b>
        <asp:Label runat="server" ID="lblExceptionList"></asp:Label>
    </div>
    </form>

    <script src="https://code.jquery.com/jquery-1.11.3.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {

            var lblTotalPages = jQuery("#<%= lblTotalPages.ClientID %>");
            var lblTotalProgressPages = jQuery("#<%= lblTotalProgressPages.ClientID %>");
            var lblProgress = jQuery("#<%= lblProgress.ClientID %>");
            var lblImportErrors = jQuery("#<%= lblImportErrors.ClientID %>");
            var lblExceptionList = jQuery("#<%= lblExceptionList.ClientID %>");
            var lblStatus = jQuery("#<%= lblStatus.ClientID %>");
            var lblindexerrors = jQuery("#<%= lblindexerrors.ClientID %>");
            var lblUrls = jQuery("#<%= lblUrls.ClientID %>");
            var txtProcessNumber = jQuery("#txtProcessNumber");
            var hrefProcessNumber = jQuery("#hrefProcessNumber");
            var processingArray = [];
            var processingArrayIndex = 0;

            var stopMigration = true;
            var totalPages = 0;
            var currentPage = 1;
            var totalErrors = 0;

            setInterval(function () {
                jQuery(lblStatus).html(stopMigration ? "Not Running" : "Running");
            }, 1000);

            var itemNotFoundExceptionCount = 0;

            function updateUIProgress(data) {
                jQuery(lblTotalProgressPages).html(data.totalPages);
                jQuery(lblProgress).html(data.currentPage);

                if (data.error) {
                    totalErrors++;
                }

                if (data.urlError) {
                    jQuery(lblUrls).html(jQuery(lblUrls).html() + "<br/>" + data.urlError);
                }

                jQuery(lblImportErrors).html(totalErrors);

                if (data.ex) {
                    data.ex = data.ex.replace(/\r\n/g, "<br/>");
                    jQuery(lblExceptionList).html(data.ex);
                    //stopMigration = true;
                    if (data.ex.indexOf("already exists") >= 0) {
                        jQuery(lblindexerrors).html(jQuery(lblindexerrors).html() + "-" + data.currentPage);
                    } else {
                        jQuery(lblindexerrors).html(data.currentPage + '|||' + jQuery(lblindexerrors).html());
                    }
                }
            }

            function continueProcessing() {
                var cPage = currentPage;

                if (processingArray.length > 0) {
                    cPage = processingArray[processingArrayIndex];
                }

                var additionalQueryString = "";

                // checking if chkOnlyNews selected
                if (jQuery("#chkOnlyNews").is(":checked")) {
                    additionalQueryString += "&news=1";
                }
                if (jQuery("#chkOnlyPodcasts").is(":checked")) {
                    additionalQueryString += "&podcasts=1";
                }

                if (jQuery("#chkSpecificTypeID").is(":checked")) {
                    additionalQueryString += "&specificTypeID=330";
                }

                if (jQuery("#chkOnlyCategories").is(":checked")) {
                    additionalQueryString += "&createCats=1";
                }

                jQuery.getJSON("processMigration.aspx?item=" + cPage + additionalQueryString, function (data) {
                    updateUIProgress(data);

                    // checking what type of error happened
                    if (data.ex && data.ex.indexOf("Telerik.Sitefinity.SitefinityExceptions.ItemNotFoundException") >= 0 && itemNotFoundExceptionCount < 2) {
                        itemNotFoundExceptionCount++;
                    } else {
                        itemNotFoundExceptionCount = 0;

                        if (processingArray.length > 0) {
                            processingArrayIndex++;
                        } else {
                            currentPage++;
                        }
                    }

                    if (!stopMigration) {
                        if (processingArray.length > 0 && processingArrayIndex < processingArray.length) {
                            continueProcessing();
                        } else if (currentPage < totalPages) {
                            continueProcessing();
                        }
                    }
                });
            }

            jQuery(hrefProcessNumber).click(function () {
                var val = jQuery(txtProcessNumber).val();

                if (val) {
                    if (val.split('-').length > 1) {
                        processingArray = val.split('-');
                        processingArrayIndex = 0;
                        stopMigration = false;
                        continueProcessing();
                    } else {
                        processingArray = [];
                        currentPage = val;
                        stopMigration = true;
                        continueProcessing();
                    }
                }
            });

            jQuery("#hrefStopMigration").click(function () {
                stopMigration = true;
            });

            jQuery("#hrefStartMigration").click(function () {
                if (stopMigration) {
                    stopMigration = false;
                    processingArray = [];
                    continueProcessing();
                }
            });

            jQuery.getJSON("getMigrationDetails.aspx", function (data) {
                totalPages = data.totalPagesToMigrate;
                jQuery(lblTotalPages).html(data.totalPagesInSitefinity);
                updateUIProgress({
                    currentPage: 0,
                    totalPages: data.totalPagesToMigrate,
                    error: false,
                    ex: ""
                });

                //continueProcessing();
            });
        });
    </script>
</body>
</html>
