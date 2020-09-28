<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.MeetingRoomTypesClass" CodeFile="~/UserControls/Aptify_Custom__c/MeetingRoomTypes.ascx.vb" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>


<div class="content-container clearfix"> 
    <table runat="server" id="tblMain" class="data-form">
      <tr>
           <td class="LeftColumn">Name</td>
           <td class="RightColumn">
               <asp:TextBox id="txtName" runat="server" AptifyDataField="Name" Width="100%" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Description</td>
           <td class="RightColumn">
               <asp:TextBox id="txtDescription" runat="server" AptifyDataField="Description" Width="100%"  TextMode="MultiLine" Height="100px" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Web Enabled</td>
           <td class="RightColumn">
               <asp:CheckBox id="chkWebEnabled__c" runat="server" AptifyDataField="WebEnabled__c" />
           </td>
      </tr>
      <tr>
        <td></td><td><asp:Button ID="cmdSave" Runat="server" Text="Save Record"></asp:Button></td>
      </tr>
    </table>
    <asp:Label id="lblError" ForeColor="Red" runat="server" Visible="False"  />
    <cc3:User id="AptifyEbusinessUser1" runat="server" />
</div> 
