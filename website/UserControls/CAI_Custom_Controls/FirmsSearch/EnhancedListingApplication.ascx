<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EnhancedListingApplication.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.FirmsSearch.EnhancedListingApplication" %>
<%@ Register TagPrefix="uc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="uc1" TagName="PaymentStep" Src="~/UserControls/CAI_Custom_Controls/FirmsSearch/EnhancedListingPaymentStep.ascx" %>

<%-- bootstrap  --%>
<!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous"/>
<!-- Optional theme -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css" integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp" crossorigin="anonymous"/>
<!-- Override bootstrap css for corporate styles -->
<link href="../../../CSS/bootstrap-override.min.css" rel="stylesheet" />

<style type="text/css">
	.CheckBoxListCssClass
        {
            font-family:Courier New;
            color:OrangeRed;
            font-style:italic;
            font-weight:bold;
            font-size:large;
            }
	.baseTemplatePlaceholder_content_C007_flcb 
	{
  width: 100%;    
  background-color: #f1f1c1;
	}
	.customtable td label {
	display:inline;
	font-size:large;
	 font-weight: normal;
}
</style>

<div class="raDiv" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="PanelEnhanceListingApp">
        <ProgressTemplate>
            <div class="dvProcessing">
                <div class="loading-bg">
                    <img src="/Images/CAITheme/bx_loader.gif" />
                    <span>LOADING...<br /><br />Please do not leave or close this window while payment is processing.</span>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<asp:UpdatePanel ID="PanelEnhanceListingApp" runat="server" CssClass="sfContentBlock">
    <ContentTemplate>
        <asp:Panel runat="server" ID="pnlChooseFirms">
            <div class="form-section-full-border no-margin">
			    <h2 class="no-margin">User details:</h2>
                <div class="form-section-half-border">
                    <asp:Label runat="server" ID="Label1" class="main-label-title">User name:</asp:Label>
                    <asp:Label ID="lName" runat="server" class="main-label-data"></asp:Label>
                </div>
                <div class="form-section-half-border no-border">
                    <asp:Label runat="server" ID="Label2" class="main-label-title">User firm:</asp:Label>
                    <asp:Label ID="lFirmName" runat="server" class="main-label-data"></asp:Label>
                </div>
            </div>
            <hr />
            <div class="form-section-full-border no-margin">
                <h2 class="no-margin">Select firm(s)</h2>
                <p style="font-size:20px!important">Select the firm(s) you want to purchase <strong>premium listing</strong> for from below.<br />
                    Having problem applying/signing up for premium lising? Please refer to our premium listing <a href="/PremiumListing/Help#HelpApply" target="_blank"><strong><i class="far fa-question-circle"></i> Help</strong></a> page.</p>
                <p class="info-tip" style="font-size:18px!important;">Click on firm names below to <strong>expand panel to see sub-offices</strong> of that firm</p>
                <div runat="server" visible="false" class="info-error" id="warningSelectFirm">
                    Please select at least one firm before proceeding.
                </div>
			    <div class="form-group" >
                    <div style="text-align:right;margin-bottom:10px">
                        <button class="expandBtn submitBtn cai-btn-red" onclick="ExpandAll()" type="button">Expand all</button>
                        <button class="collapseBtn submitBtn cai-btn-red-inverse" onclick="CollapseAll()" type="button">Collapse all</button>
                    </div>
				    <div class="customtable">
				        <%--<asp:CheckBoxList  ID="flcb" runat="server" CssClass ="customtable" DataTextField="fname" DataValueField="fid"  ></asp:CheckBoxList>--%>
                        <asp:Repeater ID="parentFirmRepeater" runat="server" OnItemDataBound="parentFirmRepeater_ItemDataBound">
                            <HeaderTemplate></HeaderTemplate>
                            <ItemTemplate>
                                <div class="list-purchasable-firms">
                                    <div class="item-primary-firm trigger-title">
                                        <span style="display:block">
                                            <asp:CheckBox ID="pfcb" runat="server" Enabled="false" Text='<%# Eval("fname") %>' />
                                            <%# GetFirmStatus(Eval("fid").ToString()) %><span class="plus"></span>
                                        </span>
                                        <asp:HiddenField ID="pfcv" Visible="false" runat="server" Value='<%# Eval("fid") %>'></asp:HiddenField>
                                        <asp:Panel runat="server" ID="pflblError" Visible="false">
                                            <p class="info-error selectfirmError">Please select parent firm in order to purchase sub-office enhanced listing</p>
                                        </asp:Panel>
                                    </div>
                                    <div class="list-sub-office trigger-section">
                                        <asp:Repeater ID="childFirmRepeater" runat="server" OnItemDataBound="childFirmRepeater_ItemDataBound">
                                            <HeaderTemplate></HeaderTemplate>
                                            <ItemTemplate>
                                                <div class="item-sub-office">
                                                    <asp:CheckBox ID="pfcb" runat="server" Enabled="false" Text='<%# Eval("fname") %>' /><%# GetFirmStatus(Eval("fid").ToString()) %>
                                                    <asp:HiddenField ID="pfcv" Visible="false" runat="server" Value='<%# Eval("fid") %>'></asp:HiddenField>
                                                </div>
                                            </ItemTemplate>
                                            <FooterTemplate></FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </ItemTemplate>
                            <FooterTemplate></FooterTemplate>
                        </asp:Repeater>
				    </div>
                </div>
            </div>
            <div class="form-section-full-border">
                <p class="info-error bottom-error hide-it" id="userErr">In order to select a sub-office, you must also select its parent office</p>
                <asp:Button ID="btnProceedToPayment" runat="server" Text="Proceed to Payment" CssClass="cai-btn cai-btn-red"  OnClick="btnProceedToPayment_Click"/>
            </div>
        </asp:Panel>
        <uc1:PaymentStep runat="server" id="PaymentStep" Visible="false"></uc1:PaymentStep>
    </ContentTemplate>
</asp:UpdatePanel>
<uc2:User id="User1" runat="server" />
<script>
function pageLoad() {	
    $('.list-sub-office').each(function () {
        if ($(this).text().trim() === "") {
            $(this).parent().children('.item-primary-firm').removeClass('trigger-title');
            $(this).parent().children('.item-primary-firm').children().children('.plus').addClass("hide-it");
            $(this).remove();
        }
    });
    function ExpandAll() {
        $('.list-sub-office').each(function () {
            $(this).addClass('show-it-transition');
            $(this).parent().children('.item-primary-firm').children().children('.plus')
                .removeClass("plus")
                .addClass("minus");
        });
        $('.expandBtn')
            .addClass('cai-btn-red-inverse')
            .removeClass('cai-btn-red');
        $('.collapseBtn')
            .addClass('cai-btn-red')
            .removeClass('cai-btn-red-inverse');
    }
    function CollapseAll() {
        $('.list-sub-office').each(function () {
            $(this).removeClass('show-it-transition');
            $(this).parent().children('.item-primary-firm').children().children('.minus')
                .removeClass("minus")
                .addClass("plus");
        });
        $('.collapseBtn')
            .addClass('cai-btn-red-inverse')
            .removeClass('cai-btn-red');
        $('.expandBtn')
            .addClass('cai-btn-red')
            .removeClass('cai-btn-red-inverse');
    }
    $('.list-purchasable-firms .aspNetDisabled').each(function () {
        $(this).children('label').addClass('disableBuyFirm');
    });
    if ($('.selectfirmError').is(':visible')) {
        $('.bottom-error')
            .addClass('show-it')
            .removeClass('hide-it');
        location.href = "#userErr";
    }
};
</script>
