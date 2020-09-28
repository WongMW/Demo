<%@ Control Language="vb" AutoEventWireup="false" Debug="true" Inherits="Aptify.Framework.Web.eBusiness.Generated.OnlineRefundRequests__cClass" CodeFile="~/UserControls/Aptify_Custom__c/OnlineRefundRequests__c.ascx.vb" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>


<div class="content-container clearfix"> 
    <table runat="server" id="tblMain" class="data-form">
        <tr id="trID" runat="server">
           <td class="LeftColumn">ID</td>
           <td class="RightColumn">
               <asp:TextBox id="TextBox1" runat="server" AptifyDataField="ID" Width="20%" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Order</td>
           <td class="RightColumn">
               <asp:Label ID="lblOrderID" runat="server" Text=""  > </asp:Label>
           </td>
      </tr>

      <tr>
           <td class="LeftColumn">Order Total</td>
           <td class="RightColumn">
               <asp:Label ID="lblOrderTotal" runat="server" Text=""  > </asp:Label>
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Order Balance</td>
           <td class="RightColumn">
               <asp:Label ID="lblOrderBalance" runat="server" Text=""  > </asp:Label>
           </td>
      </tr>
      <tr id="trRequest" runat="server">
           <td class="LeftColumn">Request</td>
           <td class="RightColumn">
               <asp:TextBox id="txtRequest" runat="server" AptifyDataField="Request" Width="100%"  TextMode="MultiLine" Height="100px" />
           </td>
      </tr>
        <tr id="trReadOnlyRequest" runat="server">
           <td class="LeftColumn">Request</td>
           <td class="RightColumn">
              <asp:Label ID="lblRequest" runat="server"  AptifyDataField="Request" TextMode="MultiLine" > </asp:Label>
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">Status</td>
           <td class="RightColumn">
               <asp:Label ID="lblStatus" runat="server" AptifyDataField="Status"> </asp:Label>
           </td>
      </tr>
      <tr id="trAmount" runat="server">
           <td class="LeftColumn">Amount</td>
           <td class="RightColumn">
               <asp:TextBox id="txtAmount" runat="server" AptifyDataField="Amount" Width="20%" />
           </td>
      </tr>
      <tr id="trReadOnlyAmount" runat="server">
           <td class="LeftColumn">Amount</td>
           <td class="RightColumn">
               <asp:Label ID="lblAmount" runat="server" AptifyDataField="Amount" > </asp:Label>
           </td>
      </tr>

      <tr id="trResponse" runat="server">
           <td class="LeftColumn">Response</td>
           <td class="RightColumn">
              <asp:Label ID="lblResponse" runat="server" AptifyDataField="Response" Width="100%" TextMode="MultiLine" Height="100px" > </asp:Label>
           </td>
      </tr>
      
          
      <tr>
        <td></td><td><asp:Button ID="cmdSave" Runat="server" Text="Save Record"></asp:Button></td>
      </tr>
    </table>
    <asp:Label id="lblError" ForeColor="Red" runat="server" Visible="False"  />
    <cc3:User id="AptifyEbusinessUser1" runat="server" />
</div> 
