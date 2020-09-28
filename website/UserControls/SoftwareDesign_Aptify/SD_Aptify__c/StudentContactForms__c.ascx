<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.StudentContactForms__cClass" CodeFile="StudentContactForms__c.ascx.vb" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>


<div class="content-container clearfix"> 
    <asp:Label runat="server" id="tblSuccessMessage"></asp:Label>

    <table runat="server" id="tblMain" class="data-form">
      <tr>
           <td class="LeftColumn required label-title">Name</td>
           <td class="RightColumn">
               <asp:TextBox id="txtname" runat="server" AptifyDataField="name" Width="100%" />
               <asp:Label CssClass="error-message" ID="lblname" Visible="false" Text="Name is required" runat="server" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn label-title">Student Number</td>
           <td class="RightColumn">
               <asp:TextBox id="txtstudent_number" runat="server" AptifyDataField="student_number" Width="100%" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn label-title">Telephone</td>
           <td class="RightColumn">
               <asp:TextBox id="txttelephone" runat="server" AptifyDataField="telephone" Width="100%" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn required label-title">Email</td>
           <td class="RightColumn">
               <asp:TextBox id="txtemail" runat="server" AptifyDataField="email" Width="100%" />
               <asp:Label CssClass="error-message" ID="lblemail" Visible="false" Text="Email is required" runat="server" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn required label-title">Query</td>
           <td class="RightColumn">
               <asp:TextBox id="txtquery" runat="server" AptifyDataField="query" Width="100%"  TextMode="MultiLine" Height="100px" />
               <asp:Label CssClass="error-message" ID="lblquery" Visible="false" Text="Query is required" runat="server" />
            <asp:TextBox Visible="false" id="txtepiServerPageName" runat="server" AptifyDataField="epiServerPageName" Width="100%" />
           </td>
      </tr>
      <tr>
        <td colspan="2" style="text-align:center"><asp:Button ID="cmdSave" Runat="server" Text="Submit form" CssClass="submitBtn" CausesValidation="true" ValidationGroup="FormValid" Width="50%"></asp:Button></td>
      </tr>
        <tr>
            <td class="LeftColumn"><span class="label-title">Use and protection of your personal information</span>
                The Institute will use the information which you have provided in this form to respond to your request or process your transaction and will hold and protect it 
                    in accordance with the Institute’s <a href="https://www.charteredaccountants.ie/Privacy-statement" target="_blank"><strong>privacy statement</strong></a>, which explains your rights in relation to your personal data.</td>
            <td class="RightColumn">               
                     </td>

        </tr>
    </table>
    <asp:Label id="lblError" ForeColor="Red" runat="server" Visible="False"  />
    <cc3:User id="AptifyEbusinessUser1" runat="server" />
</div> 
