<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CEUSubmission.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Education.CEUSubmissionControl" %>
<%@ Register Src="../Aptify_General/DatePicker.ascx" TagName="DatePicker" TagPrefix="uc2" %>
<%@ Register Src="../Aptify_General/RecordAttachments.ascx" TagName="RecordAttachments" TagPrefix="uc1" %>
<%@ Register Assembly="AptifyEBusinessUser" Namespace="Aptify.Framework.Web.eBusiness" TagPrefix="cc1" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<script type="text/javascript" language="javascript">
    function disp_alert() {
        alert("I am an alert box!!");
    }
    function inptSubmit_onclick() {

    }
    <%--Amruta IssueID 14903 method to hide label --%>
    function HideLabel(sender, args) {
        var input = args.get_fileName();  
        var n = input.indexOf(".");
        var fileExtension = input.substr(n + 1);
        var extensionArrayList = new Array("png", "jpg", "txt", "doc", "docx", "pdf", "bmp","gif");
        for (var i = 0; i < extensionArrayList.length; i++) {
        if(fileExtension == extensionArrayList[i]) {        
                if (document.getElementById('<%=lblErrorFile.ClientID%>'))
                    document.getElementById('<%=lblErrorFile.ClientID%>').style.display = 'none';
                return false;
            }
        }    
    }
</script>
<%--Amruta IssueID 14903 Page alignment and upload contol--%>
<div class="BorderDiv CEUMargin">
    <table id="tblMain" runat="server" class="data-form">
        <tr>
            <td colspan="2" class="tdHeaderInfo">
                Submit New CEU Record
            </td>
        </tr>
        <tr>
            <td class="tdRightAlignCEU">
                Type:
            </td>
            <td>
                <asp:Label ID="lbltype" runat="server" Text="CEU Submitted Via Member Portal" CssClass="txtfontfamily"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tdRightAlignCEU">
                Member:
            </td>
            <td>
                <asp:TextBox ID="txtMember" runat="server" Enabled="False" CssClass="txtfontfamily" Width="180px" Height="22px"></asp:TextBox>
                <span class="Error"><asp:Label ID="lblErrorMember" runat="server">Error</asp:Label></span>
           </td>
        </tr>
        <tr>
            <td class="tdRightAlignCEU">Date Started:</td>
            <td>
                <telerik:RadDatePicker ID="dtpDateStarted" CssClass="datePickerCEU" Width="185px" runat="server"></telerik:RadDatePicker>
            </td>
        </tr>
        <tr>
            <td class="tdRightAlignCEU">Date Granted:</td>
            <td>
                <telerik:RadDatePicker ID="dtpDateGranted" CssClass="datePickerCEU" Width="185px" runat="server"></telerik:RadDatePicker>
            </td>
        </tr>
        <tr>
            <td class="tdRightAlignCEU">Units Earned:</td>
            <td>
                <asp:TextBox ID="txtUnitsEarned" runat="server" CssClass="txtfontfamily" Width="180px" Height="22px"></asp:TextBox>
                <span class="Error"><asp:Label ID="lblErrorUnitsEarned" runat="server" Width="20%">Error</asp:Label></span>
            </td>
        </tr>
        <tr>
            <td class="tdRightAlignCEU">Title:</td>
            <td>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="txtfontfamily" Width="180px" Height="22px"></asp:TextBox>
                <span class="Error"><asp:Label ID="lblErrorTitle" runat="server">Error</asp:Label></span>
            </td>
        </tr>
        <tr>
            <td class="tdRightAlignCEU">CEU Type:</td>
            <td>
                <asp:DropDownList ID="drpTitle" runat="server" DataTextField="Name" DataValueField="ID" CssClass="txtfontfamily" Width="180px" Height="22px"></asp:DropDownList>
                <span class="Error"><asp:Label ID="lblErrorCEUType" runat="server">Error</asp:Label></span>
            </td>
        </tr>
        <tr>
            <td class="tdRightAlignCEU">Status:</td>
            <td>
                <asp:Label ID="lblStatus" runat="server" Text="Declared" CssClass="txtfontfamily"></asp:Label>
                <span class="Error"><asp:Label ID="lblErrorStatus" runat="server">Error</asp:Label></span>
            </td>
        </tr>
        <tr>
            <td class="tdRightAlignCEU">Expiration Date:</td>
            <td>
               <telerik:RadDatePicker ID="dtpExpirationDate" CssClass="datePickerCEU" Width="185px" runat="server"></telerik:RadDatePicker>
            </td>
        </tr>
        <%--Amruta Issue 14903,20/03/2013,Message for valid upload file type--%>
        <tr>
            <td colspan="2">
                Optional: Provide a document from the education content provider that shows proof
                of completion of this CEU. <br />
                Your document format :TXT,JPG,DOC,DOCX,PNG,GIF,BMP,PDF.
            </td>            
        </tr>
        <tr>
            <td class="tdRightAlignCEU">Document:</td>
            <td>
                <telerik:RadAsyncUpload ID="radCEUDocumentUpload" Localization-Select="Browse..." runat="server" Localization-Remove="Remove" MaxFileInputsCount="1" CssClass="radFileUploadCEUSubmission" OnClientFileSelected="HideLabel"></telerik:RadAsyncUpload>
                <asp:Image ID="tooptip" runat="server" ImageUrl="~/Images/Help.png" ToolTip="If you want to replace the file you uploaded, remove the existing file and then specify a new file." />
                <span class="Error"><asp:Label ID="lblErrorFile" runat="server">Error</asp:Label></span>        
            </td>
        </tr>
        <tr>
            <td>
                <span class="Error"><asp:Label ID="lblErrorSubmit" runat="server" Visible="false">Error</asp:Label></span>
                <asp:Label ID="lblSubmitSuccess" runat="server" Visible="False">Success</asp:Label>
            </td>
            <td>
                <asp:LinkButton ID="lnkGoBack" runat="server">Return to My Certifications</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button runat="server" ID="inptSubmit" name="tstButton" Text="Submit" CssClass="submitBtn"/>
            </td>
        </tr>
    </table>
    <cc1:User ID="User1" runat="server" />
</div>
