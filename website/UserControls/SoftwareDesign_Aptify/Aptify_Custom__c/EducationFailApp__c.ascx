<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.EducationFailApp__c"
    CodeFile="~/UserControls/Aptify_Custom__c/EducationFailApp__c.ascx.vb" %>

<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register TagName="CreditCard" TagPrefix="CreditCard" Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Custom__c/CreditCard__c.ascx" %>
<%@ Register Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Custom__c/RecordAttachments__c.ascx" TagPrefix="uc2"
    TagName="RecordAttachments__c" %>

<script type="text/javascript">
    function printDiv() {

        var content = "<html><body>";
        content += document.getElementById('<%= divContent.ClientID %>').innerHTML;
        content += "</body>";
        content += "</html>";


        var printWin = window.open('', '', 'left=0,top=0,width=1000,height=500,toolbar=0,scrollbars=0,status =0');
        printWin.document.write(content);
        printWin.document.close();
        printWin.focus();
        printWin.print();
        printWin.close();
    }
    function NewWindow() {
        document.forms[0].target = "_blank";
    }
</script>

<div class="cai-form">
    <span class="form-title">Appeal application</span>
    <div class="cai-form-content clearfix" id="divContent" runat="server">
        <div runat="server" id="tblMain" class="data-form">

            <div class="field-group">
                <span class="label-title-inline">Status: </span>
                <asp:Label ID="lblStatus" runat="server" Text="In Progress"></asp:Label>
            </div>

            <div class="field-group" style="display:none;">
                <span class="label-title-inline">Exam Number: </span>
                <asp:Label ID="lblExamNumber" runat="server" Text=""></asp:Label>
            </div>

            <div class="field-group">
                <span class="label-title-inline">Course for appeal: </span>
                <asp:TextBox ID="txtCourseAppeal" runat="server" Enabled="false" Width="195px"></asp:TextBox>
            </div>

            <div class="field-group">
                <span class="label-title-inline">Type of appeal: </span>
                <asp:DropDownList ID="drpAppealReason" runat="server" Enabled="false">
                </asp:DropDownList>
            </div>

            <div id="idAppealDescription" runat="server">
                <span>Please Explain Further: </span>
                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"
                    Width="360px" Height="80px"></asp:TextBox>

            </div>
            <div class="field-group">
                <span class="label-title-inline">Total cost: </span>
                <asp:Label ID="lblCurrency" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblTotalCost" runat="server" Text=""></asp:Label>
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            </div>
        </div>

        <div class="field-group" style="display:none;">
            <p>
                <span class="label-title">Please note that you are required to print and sign this form and send it in with
                original documentation supporting your the claim to the following address:</span>
                Examinations Department<br />
                Chartered Accountants Ireland<br />
                47-49 Pearse Street<br />
                Dublin 2
            </p>

        </div>

        <div>
            <CreditCard:CreditCard ID="CreditCard" runat="server" Visible="false" />
        </div>

        <div class="actions field-group">
            <asp:Label ID="lblAlreadyProductAddedError" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
            <asp:Button ID="btnPrint" runat="server" Text="Print" Visible="false" class="submitBtn" />
            <asp:Button ID="btnSave" runat="server" Text="Submit" class="submitBtn" CausesValidation="false" Visible="false" />
            <asp:Button ID="btnPay" runat="server" Text="Save And Pay" class="submitBtn" Visible="false" />
            <asp:Button ID="btnAddToCard" runat="server" Text="Add To Cart" class="submitBtn" />
            <asp:Button ID="btnCancel" runat="server" Text="Back" class="submitBtn" />
        </div>
    </div>
    <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="False" />
    <cc1:User ID="User1" runat="server" />
    <cc2:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="False"></cc2:AptifyShoppingCart>
</div>
