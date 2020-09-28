<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.CasesClass" CodeFile="Cases.ascx.vb" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>

<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<%@ Register src="UserControls/Aptify_General/RecordAttachments__c.ascx" tagname="RecordAttachments" tagprefix="uc2" %>





<style type="text/css">
    .style1
    {
        width: 338px;
    }
</style>


<div class="content-container clearfix"> 
    <table runat="server" id="tblMain" class="data-form" style="width: 524px">
      <tr>
           <td class="LeftColumn">Title</td>
           <td class="style1">
               <asp:TextBox id="txtTitle" runat="server" AptifyDataField="Title" Width="100%" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Category</td>
           <td class="style1">
               <asp:DropDownList id="cmbCaseCategoryID" runat="server" AptifyDataField="CaseCategoryID" AptifyListTextField="Name" AptifyListValueField="ID" AptifyListSQL="SELECT TOP 100 ID, Name FROM APTIFY..vwCaseCategories" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Type</td>
           <td class="style1">
               <asp:DropDownList id="cmbCaseTypeID" runat="server" AptifyDataField="CaseTypeID" AptifyListTextField="Name" AptifyListValueField="ID" AptifyListSQL="SELECT TOP 100 ID, Name FROM APTIFY..vwCaseTypes" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Priority</td>
           <td class="style1">
               <asp:DropDownList id="cmbCasePriorityID" runat="server" AptifyDataField="CasePriorityID" AptifyListTextField="Name" AptifyListValueField="ID" AptifyListSQL="SELECT TOP 100 ID, Name FROM APTIFY..vwCasePriorities" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Status</td>
           <td class="style1">
               <asp:DropDownList id="cmbCaseStatusID" runat="server" AptifyDataField="CaseStatusID" AptifyListTextField="Name" AptifyListValueField="ID" AptifyListSQL="SELECT TOP 100 ID, Name FROM APTIFY..vwCaseStatuses" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Summary</td>
           <td class="style1">
               <asp:TextBox id="txtSummary" runat="server" AptifyDataField="Summary" Width="100%"  TextMode="MultiLine" Height="100px" />
           </td>
      </tr>
     
      
      <tr>
           <td class="LeftColumn">Case Report Method</td>
           <td class="style1">
               <asp:DropDownList id="cmbCaseReportMethodID" runat="server" AptifyDataField="CaseReportMethodID" AptifyListTextField="Name" AptifyListValueField="ID" AptifyListSQL="SELECT TOP 100 ID, Name FROM APTIFY..vwCaseReportMethods" />
           </td>
      </tr>
      
      <tr>
           <td class="LeftColumn">Date Recorded</td>
           <td class="style1">
               <asp:TextBox id="txtDateRecorded" runat="server" AptifyDataField="DateRecorded" Width="100%" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Recorded By</td>
           <td class="style1">
               <asp:DropDownList id="cmbRecordedByID" runat="server" AptifyDataField="RecordedByID" AptifyListTextField="FirstLast" AptifyListValueField="ID" AptifyListSQL="SELECT TOP 100 ID, FirstLast FROM APTIFY..vwEmployees" />
           </td>
      </tr>

       <tr>
           <td class="LeftColumn">Manager</td>
           <td class="RightColumn">
               <asp:DropDownList id="cmbManagerID" runat="server" AptifyDataField="ManagerID" AptifyListTextField="FirstLast" AptifyListValueField="ID" AptifyListSQL="SELECT TOP 100 ID, FirstLast FROM APTIFY..vwEmployees" />
           </td>
      </tr>

       <tr>
           <td class="LeftColumn">&nbsp;</td>
           <td class="RightColumn">
              
               <uc2:RecordAttachments ID="RecordAttachments1" runat="server" />
              
           </td>
      </tr>

      <tr>
        <td></td><td class="style1"><asp:Button ID="cmdSave" Runat="server" Text="Save Record"></asp:Button></td>
      </tr>
    </table>
    
    <asp:Label id="lblError" ForeColor="Red" runat="server" Visible="False"  />
    <cc3:User id="AptifyEbusinessUser1" runat="server" />
    <cc1:User ID="User1" runat="server"></cc1:User>
</div> 
