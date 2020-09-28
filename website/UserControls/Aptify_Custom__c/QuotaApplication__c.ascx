<%@ Control Language="VB" AutoEventWireup="false" CodeFile="QuotaApplication__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CompanyAdministrator.QuotaApplication__c" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<script language="javascript" type="text/javascript">

    function AllowNumericOnly(evt)//[0..9]
    {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode >= 48 && charCode <= 57)
            return true;
        else
            return false;
    }

</script>
<div class="info-data">
    <asp:Label ID="lblQuataAppError" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
    <div class="row-div clearfix" align="right">
        <%-- <asp:HyperLink ID="lnkHelp" Text="Help" runat="server" Target="_blank"></asp:HyperLink>--%>
    </div>
    <div class="row-div clearfix">
        <div class="label-div w30">
            Firm Name:
        </div>
        <div class="field-div1 w60">
            <asp:Label ID="lblFirmName" runat="server" Text=""></asp:Label>
        </div>
    </div>
    <div class="row-div clearfix">
        <div class="label-div w30">
            Requested By:
        </div>
        <div class="field-div1 w60">
            <asp:Label ID="lblRequestedBy" runat="server" Text=""></asp:Label>
        </div>
    </div>
    <div class="row-div clearfix">
        <div class="label-div w30">
            Current Quota:
        </div>
        <div class="field-div1 w60">
            <asp:Label ID="lblCurrentQuota" runat="server" Text="0"></asp:Label>
        </div>
    </div>
    <div class="row-div clearfix">
        <div class="label-div w30">
            Application Status:
        </div>
        <div class="field-div1 w60">
            <asp:Label ID="lblApplicationStatus" runat="server" Text="In-Progress"></asp:Label>
        </div>
    </div>
    <div class="row-div clearfix">
        <div class="label-div w30">
         <span class="RequiredField">*</span>    Requested Quota:
        </div>
        <div class="field-div1 w60">
            <asp:TextBox ID="txtRequestedQuota" runat="server" Text=""  onkeypress="return AllowNumericOnly(event)"  MaxLength="4"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRequestedQuota"
                                        ErrorMessage="Requested Quota  Required" Display="Dynamic"
                                        ForeColor="Red"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="row-div clearfix">
        <div class="label-div w30">
            &nbsp;
        </div>
        <div class="field-div1 w60">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" Height="25px" />
            <asp:Button ID="btnBack" runat="server" Text="Back" Height="25px" CausesValidation="false" />
        </div>
    </div>
</div>
<cc3:User ID="User1" runat="server" />
<telerik:RadWindow ID="radwindowSubmit" runat="server" VisibleOnPageLoad="false"
    Height="170px" Title="Quota Application" Width="350px" BackColor="#f4f3f1" VisibleStatusbar="false"
    Behaviors="None" ForeColor="#BDA797">
    <ContentTemplate>
        <div class="info-data">
            <div class="row-div clearfix">
                <b>
                    <asp:Label ID="lblSubmitMessage" runat="server" Text=""></asp:Label></b>
                <br />
            </div>
            <div class="row-div clearfix" align="center">
                <asp:Button ID="btnSubmitOk" runat="server" Text="Ok" class="submitBtn" Width="20%" />
             
            </div>
        </div>
    </ContentTemplate>
</telerik:RadWindow>
