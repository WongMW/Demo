<%--Aptify e-Business 5.5.1 SR1, June 2014--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/RunRuleEngine__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Education.RunRuleEngine__c" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="table-div">
    <div class="row-div">
        <div class="align-left">
            <asp:Button CssClass="submit-Btn" runat="server" ID="btnRunruleengine" Text="Run Rule Engine"
                CausesValidation="false" />
          
        </div>
    </div>
   
</div>
<cc3:User ID="User1" runat="server" />
