<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/NonDuesFirmbillingPaymentControl__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.NonDuesFirmbillingPaymentControl__c"
    Debug="true" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%--<script type="text/javascript" >
    function ShowHideControls() {
        var chkBillMeLater = document.getElementById('<%= chkBillMeLater.ClientID %>');
        var Validator1 = document.getElementById('<%= RequiredFieldValidator1.ClientID %>');
        var Validator2 = document.getElementById('<%= RequiredFieldValidator2.ClientID %>');
       
        var tblMain = document.getElementById('<%= tblMain.ClientID %>');
        var tblPO = document.getElementById('<%= tblPONum.ClientID %>');


        if (chkBillMeLater.checked == true) {
            ValidatorEnable(Validator1, false);
            ValidatorEnable(Validator2, false); 
            tblMain.style.display = "none";
            tblPO.style.display = "block";

        }
        else {
            tblMain.style.display = "block";
            tblPO.style.display = "none";
            ValidatorEnable(Validator1, true);
            ValidatorEnable(Validator2, true);   
        }
    }
</script>--%>
<%--Nalini issue#12578--%>
<table runat="server" class="data-form">
    <tr>
        <td style="width: 117px; padding-left: 5px;">
            <b>
                <asp:Label ID="lblBillMelater" runat="server"></asp:Label></b>
        </td>
        <td valign="middle">
            <asp:CheckBox ID="chkBillMeLater" runat="server" Text="" TextAlign="Left" Onclick="javascript:ShowHideControls();" />
        </td>
    </tr>
</table>
<table id="tblMain" runat="server" class="data-form">
    <tr>
        <td colspan="2" style="width: 137px; padding-left: 5px;">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 122px; padding-left: 5px;">
                        <b>Payment Method:</b>
                    </td>
                    <td>
                        <asp:DropDownList ID="cmbCreditCard" runat="server" AppendDataBoundItems="True" AutoPostBack="True">
                            <asp:ListItem>---Select---</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </td>
        <%--runat="server" id="PaymentTypeSelection">--%>
        <td>
        </td>
    </tr>
    <tr runat="server" id="PaymentTypeSelection">
        <td colspan="2" style="width: 137px; padding-left: 5px;">
            <%--  <asp:Panel ID="Panel1" runat="server">--%>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 122px; padding-left: 5px;">
                        <b>
                           <span class="RequiredField">*</span> <asp:Label ID="lblCardNo" runat="server" Text="Card Number:"></asp:Label>
                        </b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCCNumber" runat="server" AutoComplete="Off" EnableViewState="False"
                            Width="150px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCCNumber"
                            Enabled="True" ErrorMessage="Credit Card # Required" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                       <span class="RequiredField">*</span> <asp:Label ID="lblSecurity" runat="server" Font-Bold="True" Text="Security #:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCCSecurityNumber" runat="server" AutoComplete="Off" EnableViewState="false"
                            Width="35px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCCSecurityNumber"
                            Enabled="True" ErrorMessage="Security # Required" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblExpirationDate" runat="server" Font-Bold="True" Text="Expiration Date"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="dropdownMonth" runat="server" Width="90px">
                            <asp:ListItem Value="1">January</asp:ListItem>
                            <asp:ListItem Value="2">February</asp:ListItem>
                            <asp:ListItem Value="3">March</asp:ListItem>
                            <asp:ListItem Value="4">April</asp:ListItem>
                            <asp:ListItem Value="5">May</asp:ListItem>
                            <asp:ListItem Value="6">June</asp:ListItem>
                            <asp:ListItem Value="7">July</asp:ListItem>
                            <asp:ListItem Value="8">August</asp:ListItem>
                            <asp:ListItem Value="9">September</asp:ListItem>
                            <asp:ListItem Value="10">October</asp:ListItem>
                            <asp:ListItem Value="11">November</asp:ListItem>
                            <asp:ListItem Value="12">December</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="dropdownDay" runat="server" Visible="False">
                        </asp:DropDownList>
                        <asp:DropDownList ID="dropdownYear" runat="server" Width="63px">
                        </asp:DropDownList>
                        <asp:CustomValidator ID="vldExpirationDate" runat="server" ControlToValidate="dropdownDay"
                            Display="Dynamic" ErrorMessage="The date selected is not valid." ForeColor="Red"></asp:CustomValidator>
                    </td>
                </tr>
            </table>
            <%-- </asp:Panel>--%>
        </td>
    </tr>
    <tr runat="server" id="PaymentTypeSelection1">
        <%--runat="server" id="PaymentTypeSelection">--%>
        <td colspan="2">
            <%--   <asp:Panel ID="Panel2" runat="server">--%>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr style="display:none;">
                    <td >
                        <b>
                             <span class="RequiredField">*</span><asp:Label ID="lblTransactionNumber" runat="server" Text="Transaction Number Colin"></asp:Label>
                        </b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTransactionNo" runat="server" AutoComplete="Off" EnableViewState="False" Text="TranNo" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtTransactionNo"
                            Display="Dynamic" ForeColor="Red" ErrorMessage="Transaction Number Required"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 122px; padding-left: 5px;">
                        <b>
                             <span class="RequiredField">*</span><asp:Label ID="lblBank" runat="server" Text="Bank"></asp:Label>
                        </b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtBank" runat="server" AutoComplete="Off" EnableViewState="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtBank"
                            Display="Dynamic" ForeColor="Red" ErrorMessage="Bank Required."></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>
                             <span class="RequiredField">*</span><asp:Label ID="lblNameOfAccount" runat="server" Text="Account Name"></asp:Label>
                        </b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNameOfAccount" runat="server" AutoComplete="Off" EnableViewState="false"></asp:TextBox><asp:RequiredFieldValidator
                            ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtNameOfAccount"
                            Display="Dynamic" ForeColor="Red" ErrorMessage="Account Name Required."></asp:RequiredFieldValidator>
                    </td>
                </tr>
              <%--  <tr>
                    <td>
                        <b>
                            <span class="RequiredField">*</span> <asp:Label ID="lblRoutingNo" runat="server" Text="Routing Number"></asp:Label>
                        </b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtRoutingNo" runat="server" AutoComplete="Off" EnableViewState="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtRoutingNo"
                            Display="Dynamic" ForeColor="Red" ErrorMessage="Routing Number Required."></asp:RequiredFieldValidator>
                    </td>
                </tr>--%>
                <tr>
                    <td>
                        <b>
                             <span class="RequiredField">*</span><asp:Label ID="lblAccountNo" runat="server" Text="Account Number"></asp:Label>
                        </b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAccountNo" runat="server" AutoComplete="Off" EnableViewState="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtAccountNo"
                            Display="Dynamic" ForeColor="Red" ErrorMessage="Account Number Required."></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>&nbsp;
                            <asp:Label ID="lblBranchName" runat="server" Text="Branch Name"></asp:Label>
                        </b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtBranchName" runat="server" AutoComplete="Off" EnableViewState="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>&nbsp;
                            <asp:Label ID="lblABA" runat="server" Text="ABA/Routing Number"></asp:Label>
                        </b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtABA" runat="server" AutoComplete="Off" EnableViewState="false"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <%-- </asp:Panel>--%>
        </td>
    </tr>
    <!-- Changes made to add Credit Card security number feature on e-business site.
        Change made by Vijay Sitlani for Issue 5369 -->
</table>
<table id="tblPONum" runat="server" style="display: none;">
    <tr id="trPONum" runat="server">
        <td style="padding-left: 5px;">
            <b>PO Number:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b>
        </td>
        <td>
            <asp:TextBox ID="txtPONumber" runat="server" EnableViewState="False" AutoComplete="Off"
                MaxLength="25"></asp:TextBox>
        </td>
    </tr>
</table>
<cc2:User ID="User1" runat="server"></cc2:User>
