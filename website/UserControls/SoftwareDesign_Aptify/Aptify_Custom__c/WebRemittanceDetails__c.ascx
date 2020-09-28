<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/WebRemittanceDetails__c.ascx.vb"
    Inherits="WebRemittanceDetails__c" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="ajax" Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" %>
<%@ Register TagPrefix="user" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="card" TagName="CreditCard" Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Custom__c/CreditCard__c.ascx" %>
<%-- Susan Wong, Ticket #18712 - ADD LOADING SCREEN start --%>
<style>
    .dvProcessing {width:50%;}
</style>
<div class="raDiv" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing"><div class="loading-bg">
                <img src="/Images/CAITheme/bx_loader.gif" />
                <span>This process can take a few minutes.<br />
                    WARNING: Please do not leave or close this window while payment is processing.
                </span></div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<%-- Susan Wong, Ticket #18712 - ADD LOADING SCREEN end --%>
<%-- Susan Wong, Ticket #18712 - ADD LOADING SCREEN start --%>
        <asp:UpdatePanel ID="updatePanelButton" runat="server" ChildrenAsTriggers="True">
            <ContentTemplate>
        <%-- Susan Wong, Ticket #18712 - ADD LOADING SCREEN end --%>
<div class="maindiv cai-form">
<div class="form-title"> Web remittance details</div>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadFormDecorator ID="QsfFromDecorator" runat="server" DecoratedControls="All"
        EnableRoundedCorners="false" />
        <%--Govind Mande added CausesValidation false in below button 27042016--%>
    
<div class="field-group">
    <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" CssClass="submitBtn hide-it" 
        Visible="False" CausesValidation="false" Style="height:auto" /><%-- Susan Wong #21274 --%>
 <asp:Button ID="btnPrintInvoice" runat="server" Text="Invoice Print" CssClass="submitBtn"
        CausesValidation="false" Style="height:auto"/></div>

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <asp:HiddenField ID="hfRemittanceNumber" runat="server" />
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
        <div style="overflow-x: auto; width: 99%" class="field-group hide-it"> <%-- Susan Wong #21274 --%>
            <telerik:RadPivotGrid runat="server" ID="gvWebRemittanceDetails" AllowPaging="True"
                PageSize="10" Skin="Default" AccessibilitySettings-OuterTableCaption="" Width="99%"
                AllowFiltering="True" AllowSorting="true" RowHeaderCellStyle-Width="66px" RowHeaderCellStyle-Font-Bold="false"
                ShowFilterHeaderZone="False" ShowColumnHeaderZone="True" ShowDataHeaderZone="false"
                ShowRowHeaderZone="True" ColumnHeaderZoneText="ColumnHeaderZone" ColumnGroupsDefaultExpanded="false"
                Culture="en-US" LocalizationPath="~/App_GlobalResources/">
                <PagerStyle CssClass="sd-pager" />
                <TotalsSettings RowsSubTotalsPosition="None" />
                <PagerStyle Mode="NextPrevAndNumeric" AlwaysVisible="true" ChangePageSizeLabelText="Page size:"></PagerStyle>
                <ClientSettings EnableFieldsDragDrop="true">
                    <Scrolling AllowVerticalScroll="false" SaveScrollPosition="true"></Scrolling>
                </ClientSettings>
                <Fields>
                    <telerik:PivotGridRowField DataField="LastName" Caption="Last name" CellStyle-Width="9%">
                    </telerik:PivotGridRowField>
                    <telerik:PivotGridRowField DataField="FirstName" Caption="First name" CellStyle-Width="9%">
                    </telerik:PivotGridRowField>
                    <telerik:PivotGridRowField DataField="OrderID" Caption="Order ID" CellStyle-Width="9%">
                    </telerik:PivotGridRowField>
                    <telerik:PivotGridColumnField DataField="Curriculum" Caption="Curriculum" UniqueName="Curriculum" CellStyle-Width="43%">
                    </telerik:PivotGridColumnField>
                    <telerik:PivotGridColumnField DataField="Course" Caption="Course" UniqueName="Course" CellStyle-Width="20%">
                    </telerik:PivotGridColumnField>
                    <telerik:PivotGridAggregateField DataField="Amount" CellStyle-Width="9%">
                    </telerik:PivotGridAggregateField>
                </Fields>
            </telerik:RadPivotGrid>
        </div>
        <asp:Label ID="lblNoRecords" runat="server"></asp:Label>
        <user:User ID="loggedInUser" runat="server" />
    </telerik:RadAjaxPanel>
    <div class="info-data">
        <div class="row-div clearfix">
            <div class="label-div w30">
                <asp:Label ID="lblPaymentMessage" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <table valign="top" id="paymentTable" runat="server" width="46%" class="bordercolor" border="0" cellpadding="0" cellspacing="0" visible="false">
        <tr>
            <td class="tdbgcolorpayment infohead" style="padding-left: 8px">
                <strong>Payment information</strong>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 5px;">
                <card:CreditCard ID="CreditCard" runat="server" SetchkSaveforFutureUse="True" />
            </td>
        </tr>
        <tr>
            <td style="padding-left:35px;">
                <asp:Button ID="btnBack" CausesValidation="false" runat="server" CssClass="cai-btn cai-btn-red-inverse" Text="Back" Width="8%" Style="height:auto"/>
                <asp:Button CssClass="cai-btn cai-btn-red" ID="btnReceipt" runat="server" CausesValidation ="false"  Visible ="false" Text="Print Receipt" Style="height:auto"></asp:Button>
                <asp:Button ID="btnMakePayment" runat="server" CssClass="cai-btn cai-btn-red" Text="Make Payment" ValidationGroup="MakePayment" Style="height:auto"/>
            </td>
        </tr>
        <tr>
            <td>
                <br />
            </td>
        </tr>
    </table>
    </div>
				<%-- Added the below Rad window as part of log #19168: Web Remmittance Page - Does not shows payment confirmation message --%>
				<div>
					<telerik:RadWindow ID="radWindowSuccess" runat="server" Width="350px" Height="200px"
						Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
						Title="Web Remittance payment" Behavior="None" VisibleOnPageLoad="false">
						<ContentTemplate>
							<div class="info-data">
								<div class="row-div clearfix">
									<b>
										<asp:Label ID="lblValidation" runat="server" Text=""></asp:Label></b>
									<br />
									<br />
								</div>
								<div class="row-div clearfix" align="center">
									<asp:Button ID="btnSuccessOK" runat="server" Text="Ok" Width="20%" class="submitBtn" OnClick="btnSuccessOK_Click"/>
								</div>
							</div>
						</ContentTemplate>
					</telerik:RadWindow>
				</div>
<%-- Susan Wong, Ticket #18712 - ADD LOADING SCREEN start --%>
    </ContentTemplate>
</asp:UpdatePanel>
<%-- Susan Wong, Ticket #18712 - ADD LOADING SCREEN end --%>
