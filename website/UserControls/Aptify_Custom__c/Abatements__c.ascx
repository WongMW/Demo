<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.Abatements__cClass" CodeFile="Abatements__c.ascx.vb" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>


<div class="content-container clearfix"> 
    <table runat="server" id="tblMain" class="data-form">
      <tr>
           <td class="LeftColumn">Person</td>
           <td class="RightColumn">
               <asp:DropDownList id="cmbPersonID" runat="server" AptifyDataField="PersonID" AptifyListTextField="NameWCompany" AptifyListValueField="ID" AptifyListSQL="SELECT TOP 100 ID, NameWCompany FROM APTIFY..vwPersons" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Status</td>
           <td class="RightColumn">
               <asp:DropDownList id="cmbStatus" runat="server" AptifyDataField="Status" >
                   <asp:ListItem>Submitted</asp:ListItem>
                   <asp:ListItem>Awaiting Docs</asp:ListItem>
                   <asp:ListItem>Campaign Applied</asp:ListItem>
                   <asp:ListItem>Approved</asp:ListItem>
                   <asp:ListItem>Rejected</asp:ListItem>
               </asp:DropDownList>
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Ill Health</td>
           <td class="RightColumn">
               <asp:CheckBox id="chkIllHealth" runat="server" AptifyDataField="IllHealth" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Career Break</td>
           <td class="RightColumn">
               <asp:CheckBox id="chkCareerBreak" runat="server" AptifyDataField="CareerBreak" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Registered Unemployed</td>
           <td class="RightColumn">
               <asp:CheckBox id="chkRegisteredUnemployed" runat="server" AptifyDataField="RegisteredUnemployed" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Part Time</td>
           <td class="RightColumn">
               <asp:CheckBox id="chkPartTime" runat="server" AptifyDataField="PartTime" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Early Retirement</td>
           <td class="RightColumn">
               <asp:CheckBox id="chkEarlyRetirement" runat="server" AptifyDataField="EarlyRetirement" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Annual Income Low</td>
           <td class="RightColumn">
               <asp:TextBox id="txtAnnualIncomeLow" runat="server" AptifyDataField="AnnualIncomeLow" Width="100%" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Annual Income High</td>
           <td class="RightColumn">
               <asp:TextBox id="txtAnnualIncomeHigh" runat="server" AptifyDataField="AnnualIncomeHigh" Width="100%" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Percentage Reduction</td>
           <td class="RightColumn">
               <asp:TextBox id="txtPercentageReduction" runat="server" AptifyDataField="PercentageReduction" Width="100%" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Product Category</td>
           <td class="RightColumn">
               <asp:DropDownList id="cmbProductCategoryID" runat="server" AptifyDataField="ProductCategoryID" AptifyListTextField="NameWRoot" AptifyListValueField="ID" AptifyListSQL="SELECT TOP 100 ID, NameWRoot FROM APTIFY..vwProductCategories UNION SELECT -1 ID, '' NameWRoot" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Product</td>
           <td class="RightColumn">
               <asp:DropDownList id="cmbProductID" runat="server" AptifyDataField="ProductID" AptifyListTextField="Name" AptifyListValueField="ID" AptifyListSQL="SELECT TOP 100 ID, Name FROM APTIFY..vwProducts UNION SELECT -1 ID, '' Name" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Campaign</td>
           <td class="RightColumn">
               <asp:DropDownList id="cmbCampaignID" runat="server" AptifyDataField="CampaignID" AptifyListTextField="Name" AptifyListValueField="ID" AptifyListSQL="SELECT TOP 100 ID, Name FROM APTIFY..vwCampaigns UNION SELECT -1 ID, '' Name" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Reject Message</td>
           <td class="RightColumn">
               <asp:TextBox id="txtRejectMessage" runat="server" AptifyDataField="RejectMessage" Width="100%"  TextMode="MultiLine" Height="100px" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Currency Type</td>
           <td class="RightColumn">
               <asp:DropDownList id="cmbCurrencyTypeID__c" runat="server" AptifyDataField="CurrencyTypeID__c" AptifyListTextField="Name" AptifyListValueField="ID" AptifyListSQL="SELECT TOP 100 ID, Name FROM APTIFY..vwCurrencyTypes UNION SELECT -1 ID, '' Name" />
           </td>
      </tr>
      <tr>
        <td></td><td><asp:Button ID="cmdSave" Runat="server" Text="Save Record"></asp:Button></td>
      </tr>
    </table>
    <asp:Label id="lblError" ForeColor="Red" runat="server" Visible="False"  />
    <cc3:User id="AptifyEbusinessUser1" runat="server" />
</div> 
