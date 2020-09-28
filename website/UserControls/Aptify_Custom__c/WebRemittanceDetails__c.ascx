<%@ Control Language="VB" AutoEventWireup="false" CodeFile="WebRemittanceDetails__c.ascx.vb"
    Inherits="WebRemittanceDetails__c" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="ajax" Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" %>
<%@ Register TagPrefix="user" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="card" TagName="CreditCard" Src="~/UserControls/Aptify_Custom__c/CreditCard__c.ascx" %>
<div class="maindiv">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadFormDecorator ID="QsfFromDecorator" runat="server" DecoratedControls="All"
        EnableRoundedCorners="false" />
        <%--Govind Mande added CausesValidation false in below button 27042016--%>
    <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" CssClass="submit-Btn"
        Visible="False" CausesValidation="false" />
 <asp:Button ID="btnPrintInvoice" runat="server" Text="Invoice Print" CssClass="submit-Btn"
        CausesValidation="false" />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
        <asp:HiddenField ID="hfRemittanceNumber" runat="server" />
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
        <div style="overflow-x: auto; width: 100%">
            <telerik:RadPivotGrid runat="server" ID="gvWebRemittanceDetails" AllowPaging="True"
                PageSize="10" Skin="Default" AccessibilitySettings-OuterTableCaption="" Width="120%"
                AllowFiltering="True" AllowSorting="true" RowHeaderCellStyle-Width="66px" RowHeaderCellStyle-Font-Bold="false"
                ShowFilterHeaderZone="False" ShowColumnHeaderZone="True" ShowDataHeaderZone="false"
                ShowRowHeaderZone="True" ColumnHeaderZoneText="ColumnHeaderZone" ColumnGroupsDefaultExpanded="false"
                Culture="en-US" LocalizationPath="~/App_GlobalResources/">
                <TotalsSettings RowsSubTotalsPosition="None" />
                <PagerStyle Mode="NextPrevAndNumeric" AlwaysVisible="true" ChangePageSizeLabelText="Page Size:">
                </PagerStyle>
                <ClientSettings EnableFieldsDragDrop="true">
                    <Scrolling AllowVerticalScroll="false" SaveScrollPosition="true"></Scrolling>
                </ClientSettings>
                <Fields>
                    <telerik:PivotGridRowField DataField="StudentNumber" Caption="Student Number" CellStyle-Width="11%">
                    </telerik:PivotGridRowField>
                    <telerik:PivotGridRowField DataField="LastName" Caption="Last Name" CellStyle-Width="9%">
                    </telerik:PivotGridRowField>
                    <telerik:PivotGridRowField DataField="FirstName" Caption="First Name" CellStyle-Width="9%">
                    </telerik:PivotGridRowField>
                    <telerik:PivotGridRowField DataField="OrderID" Caption="Order ID" CellStyle-Width="9%">
                    </telerik:PivotGridRowField>
                    <telerik:PivotGridColumnField DataField="Curriculum" Caption="Curriculum" UniqueName="Curriculum">
                    </telerik:PivotGridColumnField>
                    <telerik:PivotGridColumnField DataField="Course" Caption="Course" UniqueName="Course">
                    </telerik:PivotGridColumnField>
                    <telerik:PivotGridAggregateField DataField="Amount">
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
    <table valign="top" id="paymentTable" runat="server" width="46%" class="bordercolor"
        border="0" cellpadding="0" cellspacing="0" visible="false">
        <tr>
            <td class="tdbgcolorpayment infohead" style="padding-left: 8px">
                <strong><font size="2">Payment Information</font></strong>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 5px;">
                <card:CreditCard ID="CreditCard" runat="server" SetchkSaveforFutureUse="True" />
            </td>
        </tr>
        <tr>
            <td style="padding-left: 320px">
                <asp:Button ID="btnMakePayment" runat="server" CssClass="submit-Btn" Text="Make Payment"
                    ValidationGroup="MakePayment" />
<asp:Button CssClass="submitBtn" ID="btnReceipt" runat="server" CausesValidation ="false"  Visible ="false" Text="Print Receipt"></asp:Button>
            </td>
        </tr>
        <tr>
            <td>
                <br />
            </td>
        </tr>
    </table>
    <div class="info-data">
        <div class="row-div clearfix">
            <div class="label-div w90">
                <asp:Button ID="btnBack" runat="server" CssClass="submit-Btn" Text="Back" CausesValidation="false"
                    ValidationGroup="MakePayment2" Width="8%" />
            </div>
        </div>
    </div>
</div>
