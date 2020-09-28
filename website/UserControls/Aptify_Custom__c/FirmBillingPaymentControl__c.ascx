<%@ Control Language="VB" Debug ="true" AutoEventWireup="false" CodeFile="FirmBillingPaymentControl__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.FirmBillingPaymentControl__c" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<!--RashmiP issue 6781 -->
<%--<style type="text/css">
    .style1
    {
        width: 162px;
    }
    #tblMain
    {
        width: 784px;
        position: relative;
    }
    .style2
    {
        width: 100%;
    }
</style>--%>
<%--<style type="text/css">
    .style2
    {
    }
    .style3
    {
        width: 100%;
        position: relative;
    }
    .style5
    {
        width: 207px;
    }
    #tblMain
    {
        width: 847px;
        position: absolute;
        z-index: 1;
        left: 10px;
        top: 15px;
        height: 413px;
    }
    .style9
    {
        width: 208px;
    }
    .style11
    {
        width: 206px;
    }
    .style12
    {
        width: 209px;
    }
    .style13
    {
        width: 210px;
    }
    .style14
    {
        width: 215px;
    }
    </style>--%>
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
<%--script type ="text/javascript" >
</script>--%>
&nbsp;<cc2:User ID="User1" runat="server"></cc2:User>
<table runat="server" class="data-form">
    <tr>
        <td width="158px">
            &nbsp;<asp:Label ID="lblFirmBillingPayment" runat="server" 
                Font-Bold="True" Text="Payment Method"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="cmbFirmBillingPayment" runat="server" Width="154px" AutoPostBack="True"
                OnSelectedIndexChanged="cmbFirmBillingPayment_SelectedIndexChanged" EnableViewState="true"
                AppendDataBoundItems="True">
                <asp:ListItem>---Select---</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr id="trTranctions" runat="server" visible="false" style="display:none;">
        <td>
            <b><span class="RequiredField">*</span><asp:Label ID="lblTransactionNumber" runat="server"
                Text="Transaction Number"></asp:Label>
            </b>
        </td>
        <td>
            <asp:TextBox ID="txtTransactionNo" runat="server" AutoComplete="Off" EnableViewState="False" Text="TranNo"
                Width="150px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator9"
                    Display="Dynamic" runat="server" ControlToValidate="txtTransactionNo" ForeColor="Red"
                    ErrorMessage="Transaction Number Required"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr id="trCheckDetails" runat="server" visible="false">
        <td>
            <b><span class="RequiredField">*</span>
                <asp:Label ID="lblCheckNo" runat="server" Text="Check Number" Width="150px"></asp:Label>
            </b>
        </td>
        <td>
            <asp:TextBox ID="txtCheckNo" runat="server" AutoComplete="Off" EnableViewState="False"
                Width="150px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator10"
                    runat="server" ControlToValidate="txtCheckNo" Display="Dynamic" ForeColor="Red"
                    ErrorMessage="Check Number Required"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr id="trBnak" runat="server" visible="false">
        <td>
            <b><span class="RequiredField">*</span><asp:Label ID="lblBank" runat="server" Text="Bank"></asp:Label>
            </b>
        </td>
        <td>
            <asp:TextBox ID="txtBank" runat="server" AutoComplete="Off" EnableViewState="false"
                 Width="150px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator11"
                    runat="server" ControlToValidate="txtBank" Display="Dynamic" ForeColor="Red"
                    ErrorMessage="Bank Required."></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr id="trAccName" runat="server" visible="false">
        <td>
            <b><span class="RequiredField">*</span><asp:Label ID="lblNameOfAccount" runat="server"
                Text="Account Name" Width="150px"></asp:Label>
            </b>
        </td>
        <td>
            <asp:TextBox ID="txtNameOfAccount" runat="server" AutoComplete="Off" EnableViewState="false"
                Width="150px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator14"
                    runat="server" ControlToValidate="txtNameOfAccount" Display="Dynamic" ForeColor="Red"
                    ErrorMessage="Account Name Required."></asp:RequiredFieldValidator>
        </td>
    </tr>
   <%-- <tr id="trRouteDetails" runat="server" visible="false">
        <td>
            <b><span class="RequiredField">*</span><asp:Label ID="lblRoutingNo" runat="server"
                Text="Routing Number" Width="150px"></asp:Label>
            </b>
        </td>
        <td>
            <asp:TextBox ID="txtRoutingNo" runat="server" AutoComplete="Off" EnableViewState="false"
                Width="150px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtRoutingNo"
                Display="Dynamic" ForeColor="Red" ErrorMessage="Routing Number Required."></asp:RequiredFieldValidator>
        </td>
    </tr>--%>
    <tr id="trAccountInfo" runat="server" visible="false">
        <td>
            <b><span class="RequiredField">*</span>
                <asp:Label ID="lblAccountNo" runat="server" Text="Account Number" Width="150px"></asp:Label>
            </b>
        </td>
        <td>
            <asp:TextBox ID="txtAccountNo" runat="server" AutoComplete="Off" EnableViewState="false"
                Width="150px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtAccountNo"
                Display="Dynamic" ForeColor="Red" ErrorMessage="Account Number Required."></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr id="trBranchDetails" runat="server" visible="false">
        <td>
            <b>&nbsp;
                <asp:Label ID="lblBranchName" runat="server" Text="Branch Name" Width="150px"></asp:Label>
            </b>
        </td>
        <td>
            <asp:TextBox ID="txtBranchName" runat="server" AutoComplete="Off" EnableViewState="false"
                Width="150px"></asp:TextBox>
        </td>
    </tr>
    <tr id="trABA" runat="server" visible="false">
        <td>
            <b>&nbsp;
                <asp:Label ID="lblABA" runat="server" Text="ABA/Routing Number" Width="150px"></asp:Label>
            </b>
        </td>
        <td>
            <asp:TextBox ID="txtABA" runat="server" AutoComplete="Off" EnableViewState="false"
                Width="150px"></asp:TextBox>
        </td>
    </tr>
</table>
