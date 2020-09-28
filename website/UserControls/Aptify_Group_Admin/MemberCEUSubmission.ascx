<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MemberCEUSubmission.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.MemberCEUSubmission" %>
<%@ Register Src="../Aptify_General/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc2" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../Aptify_General/RecordAttachments.ascx" TagName="RecordAttachments"
    TagPrefix="uc1" %>
<%@ Register Assembly="AptifyEBusinessUser" Namespace="Aptify.Framework.Web.eBusiness"
    TagPrefix="cc1" %>
<div class="clearfix topPaddingSet">
    <div class="BorderDiv">
        <table id="tblMain" runat="server" class="data-form">
            <tr>
                <td colspan="2" class="tdHeaderInfo">
                    Submit New CEU Record
                </td>
            </tr>
            <tr>
                <td class="leftColumnCEU">
                    Type:
                </td>
                <td>
                    CEU Submitted Via Member Portal
                </td>
            </tr>
            <tr>
                <td class="leftColumnCEU">
                    Member:
                </td>
                <td>
                    <asp:Label ID="txtMember" runat="server" Enabled="False"></asp:Label>
                    <span class="Error">
                        <asp:Label ID="lblErrorMember" runat="server">Error</asp:Label></span>
                </td>
            </tr>
            <tr>
                <td class="leftColumnCEU">
                    Date Started:
                </td>
                <td>
                    <telerik:RadDatePicker ID="dtpDateStarted" CssClass="datePickerCEU" Width="185px"
                        runat="server">
                    </telerik:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="leftColumnCEU">
                    Date Granted:
                </td>
                <td align="left">
                    <telerik:RadDatePicker ID="dtpDateGranted" CssClass="datePickerCEU" Width="185px"
                        runat="server">
                    </telerik:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="leftColumnCEU">
                    Units Earned:
                </td>
                <td>
                    <asp:TextBox ID="txtUnitsEarned" runat="server" CssClass="txtCUESubmission"></asp:TextBox>
                    <span class="Error">
                        <asp:Label ID="lblErrorUnitsEarned" runat="server">Error</asp:Label></span>
                </td>
            </tr>
            <tr>
                <td class="leftColumnCEU">
                    Title:
                </td>
                <td>
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="txtCUESubmission"></asp:TextBox>
                    <span class="Error">
                        <asp:Label ID="lblErrorTitle" runat="server">Error</asp:Label></span>
                </td>
            </tr>
            <tr>
                <td class="leftColumnCEU">
                    CEU Type:
                </td>
                <td>
                    <asp:DropDownList ID="drpTitle" runat="server" DataTextField="Name" CssClass="txtCUESubmission"
                        DataValueField="ID">
                    </asp:DropDownList>
                    <span class="Error">
                        <asp:Label ID="lblErrorCEUType" runat="server">Error</asp:Label></span>
                </td>
            </tr>
            <tr>
                <td class="leftColumnCEU">
                    Status:
                </td>
                <td>
                    <asp:Label ID="lblStatus" runat="server" Text="Declared"></asp:Label>
                    <span class="Error">
                        <asp:Label ID="lblErrorStatus" runat="server">Error</asp:Label></span>
                </td>
            </tr>
            <tr>
                <td class="leftColumnCEU">
                    Expiration Date:
                </td>
                <td>
                    <telerik:RadDatePicker ID="dtpExpirationDate" CssClass="datePickerCEU" Width="185px"
                        runat="server">
                    </telerik:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="leftColumnCEU">
                    Document:
                </td>
                <td>
              <%--   'Anil B For issue 14344 on 28-03-2013
                    'replace file asp upload control in to rad asyncupload control--%>
      
                    <telerik:RadAsyncUpload ID="radCEUDocumentUpload" ViewStateMode="Enabled" Localization-Select="Browse..." runat="server" Localization-Remove="Remove" MaxFileInputsCount="1" CssClass="radFileUploadCEUSubmission"></telerik:RadAsyncUpload>                   
                    <span class="Error">
                        <asp:Label ID="lblErrorFile" runat="server">Error</asp:Label></span>
                </td>
            </tr>
            <tr>               
                <td colspan="2">
                    <asp:Button runat="server" ID="inptSubmit" name="tstButton" Text="Submit" CssClass="submitBtn" />
                    &nbsp;
                    <asp:LinkButton ID="lnkGoBack" runat="server">Return to Member Certifications</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <span class="Error">
                        <asp:Label ID="lblErrorSubmit" runat="server" Visible="false">Error</asp:Label></span>
                    <asp:Label ID="lblSubmitSuccess" runat="server" Visible="False">Success</asp:Label>
                </td>                
            </tr>
        </table>
    </div>
    <cc1:User ID="User1" runat="server" />
</div>
