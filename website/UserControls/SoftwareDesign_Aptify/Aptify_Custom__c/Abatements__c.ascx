<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.Abatements__cClass" CodeFile="~/UserControls/Aptify_Custom__c/Abatements__c.ascx.vb" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>


<div class="content-container clearfix cai-form">
    <div runat="server" id="tblMain" class="data-form cai-form-content ">
        <div class="form-section-half">
            <div class="field-group">
                <span class="label-title">Person</span>
                <asp:DropDownList ID="cmbPersonID" runat="server" AptifyDataField="PersonID" AptifyListTextField="NameWCompany" AptifyListValueField="ID" AptifyListSQL="SELECT TOP 100 ID, NameWCompany FROM APTIFY..vwPersons" />
            </div>
        </div>


        <div class="form-section-half">
            <div class="field-group">
                <span class="label-title">Status</span>
                <asp:DropDownList ID="cmbStatus" runat="server" AptifyDataField="Status">
                    <asp:ListItem>Submitted</asp:ListItem>
                    <asp:ListItem>Awaiting Docs</asp:ListItem>
                    <asp:ListItem>Campaign Applied</asp:ListItem>
                    <asp:ListItem>Approved</asp:ListItem>
                    <asp:ListItem>Rejected</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>

        <div class="field-group">
            <span class=" label-title">
                <asp:CheckBox ID="chkIllHealth" runat="server" AptifyDataField="IllHealth" />
                Ill Health
            </span>


            <span class="label-title">
                <asp:CheckBox ID="chkCareerBreak" runat="server" AptifyDataField="CareerBreak" />
                Career Break
            </span>


            <span class="label-title">
                <asp:CheckBox ID="chkRegisteredUnemployed" runat="server" AptifyDataField="RegisteredUnemployed" />
                Registered Unemployed
            </span>

            <span class="label-title">
                <asp:CheckBox ID="chkPartTime" runat="server" AptifyDataField="PartTime" />
                Part Time
            </span>


            <span class="label-title">
                <asp:CheckBox ID="chkEarlyRetirement" runat="server" AptifyDataField="EarlyRetirement" />
                Early Retirement
            </span>
        </div>

        <div class="form-section-half">
            <div class="field-group">
                <span class="label-title">Annual Income Low</span>
                <asp:TextBox ID="txtAnnualIncomeLow" runat="server" AptifyDataField="AnnualIncomeLow" />
            </div>

            <div class="field-group">
                <span class="label-title">Annual Income High</span>
                <asp:TextBox ID="txtAnnualIncomeHigh" runat="server" AptifyDataField="AnnualIncomeHigh" />
            </div>

            <div class="field-group">
                <span class="label-title">Percentage Reduction</span>
                <asp:TextBox ID="txtPercentageReduction" runat="server" AptifyDataField="PercentageReduction" />
            </div>

            <div class="field-group">
                <span class="label-title">Product Category</span>
                <asp:DropDownList ID="cmbProductCategoryID" runat="server" AptifyDataField="ProductCategoryID" AptifyListTextField="NameWRoot" AptifyListValueField="ID" AptifyListSQL="SELECT TOP 100 ID, NameWRoot FROM APTIFY..vwProductCategories UNION SELECT -1 ID, '' NameWRoot" />
            </div>
        </div>

        <div class="form-section-half">
            <div class="field-group">
                <span class="label-title">Product</span>
                <asp:DropDownList ID="cmbProductID" runat="server" AptifyDataField="ProductID" AptifyListTextField="Name" AptifyListValueField="ID" AptifyListSQL="SELECT TOP 100 ID, Name FROM APTIFY..vwProducts UNION SELECT -1 ID, '' Name" />
            </div>

            <div class="field-group">
                <span class="label-title">Campaign</span>
                <asp:DropDownList ID="cmbCampaignID" runat="server" AptifyDataField="CampaignID" AptifyListTextField="Name" AptifyListValueField="ID" AptifyListSQL="SELECT TOP 100 ID, Name FROM APTIFY..vwCampaigns UNION SELECT -1 ID, '' Name" />
            </div>
            
             <div class="field-group">
                <span class="label-title">Currency Type</span>
                <asp:DropDownList ID="cmbCurrencyTypeID__c" runat="server" AptifyDataField="CurrencyTypeID__c" AptifyListTextField="Name" AptifyListValueField="ID" AptifyListSQL="SELECT TOP 100 ID, Name FROM APTIFY..vwCurrencyTypes UNION SELECT -1 ID, '' Name" />
            </div>

            <div class="field-group">
                <span class="label-title ">Reject Message</span>
                <asp:TextBox ID="txtRejectMessage" runat="server" AptifyDataField="RejectMessage" TextMode="MultiLine" />
            </div>
        </div>


        <div class="actions field-group">
            <asp:Button ID="cmdSave" runat="server" CssClass="submitBtn" Text="Save Record"></asp:Button>
        </div>
    </div>
    <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="False" />
    <cc3:User ID="AptifyEbusinessUser1" runat="server" />
</div>
