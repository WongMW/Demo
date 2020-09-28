<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="XmlSitemapGenerator.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.XmlSitemapGenerator" %>


<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <asp:HiddenField ID="currItemIndex" runat="server" Value="0" />
        <asp:HiddenField ID="currentStep" runat="server" Value="1" />

        <asp:HiddenField ID="totalNewsHiddenField" runat="server" value=""/>
        <asp:HiddenField ID="entutiesHiddenField" runat="server" value=""/>

        <asp:Label runat="server" ID="lblProgress"></asp:Label><br />
        <asp:Label runat="server" ID="lblProductProgress"></asp:Label><br />
        <asp:Label runat="server" ID="lblFirmProgress"></asp:Label>

        <div style="display: none">
            <asp:Button runat="server" Text="Generate" ID="btnGenerate" OnClick="btnGenerate_Click" />
        </div>
        <div style="display: none">
            <asp:Button runat="server" Text="Generate products sitemap" ID="btnProductGenerate" OnClick="btnGenerateProduct_Click" />
        </div>
         <div style="display: none">
            <asp:Button runat="server" Text="Generate firms sitemap" ID="btnFirmGenerate" OnClick="btnGenerateFirm_Click" />
         </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnGenerate" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="btnProductGenerate" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="btnFirmGenerate" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>

<asp:Button runat="server" Text="Generate" ID="btnClientGenerate" OnClientClick="return StartMapGenerationProcess(event);" />
<asp:Button runat="server" Text="Generate products sitemap" ID="btnClientProductGenerate" OnClientClick="return StartMapGenerationProductProcess(event);" />
<asp:Button runat="server" Text="Generate firms sitemap" ID="btnClientFirmGenerate" OnClientClick="return StartMapGenerationFirmProcess(event);" />

<script type="text/javascript">
    var processInProgress = false;
    var lblProgress = "#<%= lblProgress.ClientID %>";
    var btnGenerate = "#<%= btnGenerate.ClientID %>";
    var btnClientGenerate = "#<%= btnClientGenerate.ClientID %>";

    var currentStep = "#<%= currentStep.ClientID%>";
    var currItemIndex = "#<%= currItemIndex.ClientID%>";

    function StartMapGenerationProcess(e) {
        $(btnClientProductGenerate).attr('disabled', 'disabled');
        $(btnClientFirmGenerate).attr('disabled', 'disabled');
        $(btnClientGenerate).attr('disabled', 'disabled');
        $(btnClientGenerate).text("Generating...");

        if (!processInProgress) {
            $(lblProgress).text("first call...");
            previousLabelText = "";
            processInProgress = true;
            checkProgress();
        }

        return !processInProgress;
    }

    var previousLabelText = "";

    var currentProgressTimeout;

    function checkProgress() {
        if ($(lblProgress).text().indexOf("completed") >= 0) {
            $(btnClientGenerate).removeAttr('disabled');
            $(btnClientFirmGenerate).removeAttr('disabled');
            $(btnClientProductGenerate).removeAttr('disabled');
            processInProgress = false;
            $(currentStep).val("1");
            $(currItemIndex).val("0");
        } else if ($(lblProgress).text() != previousLabelText) {
            previousLabelText = $(lblProgress).text();
            $(btnGenerate).click();
        }

        if (processInProgress)
        currentProgressTimeout = setTimeout(checkProgress, 1000);
    }
</script>


<script type="text/javascript">
    var processInProductProgress = false;
    var lblProductProgress = "#<%= lblProductProgress.ClientID %>";
    var btnProductGenerate = "#<%= btnProductGenerate.ClientID%>";
    var btnClientProductGenerate = "#<%=btnClientProductGenerate.ClientID%>";

    function StartMapGenerationProductProcess(e) {
        $(btnClientGenerate).attr('disabled', 'disabled');
        $(btnClientFirmGenerate).attr('disabled', 'disabled');
        $(btnClientProductGenerate).attr('disabled', 'disabled');
        $(btnClientProductGenerate).text("Generating...");

        if (!processInProductProgress) {
            $(lblProductProgress).text("first call...");
            previousProductLabelText = ""
            processInProductProgress = true;
            checkProductProgress();
        }

        return !processInProductProgress;
    }

    var previousProductLabelText = "";

    var currentProductProgressTimeout;

    function checkProductProgress() {
        if ($(lblProductProgress).text().indexOf("completed") >= 0) {
            $(btnClientProductGenerate).removeAttr('disabled');
            $(btnClientFirmGenerate).removeAttr('disabled');
            $(btnClientGenerate).removeAttr('disabled');
            processInProductProgress = false;
            $(currentStep).val("1");
            $(currItemIndex).val("0");
        } else if ($(lblProductProgress).text() != previousProductLabelText) {
            previousProductLabelText = $(lblProductProgress).text();
            $(btnProductGenerate).click();
        }

        if (processInProductProgress)
            currentProductProgressTimeout = setTimeout(checkProductProgress, 1000);
    }
</script>


<script type="text/javascript">
    var processInFirmProgress = false;
    var lblFirmProgress = "#<%= lblFirmProgress.ClientID %>";
    var btnFirmGenerate = "#<%= btnFirmGenerate.ClientID%>";
    var btnClientFirmGenerate = "#<%=btnClientFirmGenerate.ClientID%>";

    function StartMapGenerationFirmProcess(e) {
        $(btnClientGenerate).attr('disabled', 'disabled');
        $(btnClientProductGenerate).attr('disabled', 'disabled');
        $(btnClientFirmGenerate).attr('disabled', 'disabled');
        $(btnClientFirmGenerate).text("Generating...");

        if (!processInFirmProgress) {
            $(lblFirmProgress).text("first call...");
            previousFirmLabelText = ""
            processInFirmProgress = true;
            checkFirmProgress();
        }

        return !processInFirmProgress;
    }

    var previousFirmLabelText = "";

    var currentFirmProgressTimeout;

    function checkFirmProgress() {
        if ($(lblFirmProgress).text().indexOf("completed") >= 0) {
            $(btnClientFirmGenerate).removeAttr('disabled');
            $(btnClientProductGenerate).removeAttr('disabled');
            $(btnClientGenerate).removeAttr('disabled');
            processInFirmProgress = false;
            $(currentStep).val("1");
            $(currItemIndex).val("0");
        } else if ($(lblFirmProgress).text() != previousFirmLabelText) {
            previousFirmLabelText = $(lblFirmProgress).text();
            $(btnFirmGenerate).click();
        }

        if (processInFirmProgress)
            currentFirmProgressTimeout = setTimeout(checkFirmProgress, 1000);
    }
</script>



<script type="text/javascript">
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function (sender, e) {
            if (sender._postBackSettings.panelsToUpdate != null) {
                if (e.get_error() != null) {
                    clearTimeout(currentProgressTimeout);
                    clearTimeout(currentProductProgressTimeout);
                    clearTimeout(currentFirmProgressTimeout);
                    previousLabelText = "";
                    previousProductLabelText = "";
                    previousFirmLabelText = "";
                    if (processInProgress)
                        checkProgress();
                    if (processInProductProgress)
                        checkProductProgress();
                    if (processInFirmProgress)
                        checkFirmProgress();
                }
            }
        });
    };
</script>
