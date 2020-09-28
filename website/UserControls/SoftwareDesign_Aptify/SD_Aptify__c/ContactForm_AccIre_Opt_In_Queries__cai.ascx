<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.ContactForm_AccIre_Opt_In_Queries__caiClass" CodeFile="ContactForm_AccIre_Opt_In_Queries__cai.ascx.vb" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>


<div class="content-container clearfix"> 
    <asp:Label runat="server" ID="tblSuccessMessage"></asp:Label>
    <asp:Label id="lblError" ForeColor="Red" runat="server" Visible="False"  />
    <table runat="server" id="tblMain" class="data-form">
      <tr>
           <td class="LeftColumn">
               <div class="required">
                   Full name (required)
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator0" runat="server" ControlToValidate="txtName_of_Person" ErrorMessage="Full name is required" ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;"></asp:RequiredFieldValidator>
               </div>
           </td>
           <td class="RightColumn">
               <asp:TextBox id="txtName_of_Person" runat="server" AptifyDataField="Name_of_Person" Width="100%" ValidationGroup="FormValid" Placeholder="Your first name and last name e.g. John Smith"/>
               <asp:TextBox id="txtForm_Title" runat="server" AptifyDataField="Form_Title" Width="100%" Visible="false"/>
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">
               <span class="label-title">
                   Phone
                   <asp:RegularExpressionValidator ID="regexPhoneValid" runat="server" Display="Dynamic" ControlToValidate="txtPhone_Number" ErrorMessage="Invalid phone number format" ForeColor="Red" ValidationGroup="FormValid" ValidationExpression="^\(?\+?[\d\(\-\s\)]+$" style="float: right;"></asp:RegularExpressionValidator>
               </span>

           </td>
           <td class="RightColumn">
               <asp:TextBox id="txtPhone_Number" runat="server" AptifyDataField="Phone_Number" Width="100%" ValidationGroup="FormValid" Placeholder="Your preferred contact number e.g. +353 87 124567"/>
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">
               <div class="required">
                   Email (required)
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail_Address" ErrorMessage="Email is required" ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;"></asp:RequiredFieldValidator>
                   <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" Display="Dynamic" ControlToValidate="txtEmail_Address" ErrorMessage="Invalid email format" ForeColor="Red" ValidationGroup="FormValid" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" style="float: right;"></asp:RegularExpressionValidator>
               </div>
           </td>
           <td class="RightColumn">
               <asp:TextBox id="txtEmail_Address" runat="server" AptifyDataField="Email_Address" Width="100%" ValidationGroup="FormValid" Placeholder="Your email (for registered user, please use your registered email) e.g. John@gmail.com"/>
           </td>
      </tr>
      <tr>
        <td class="LeftColumn" colspan="2" style="text-align:center"><asp:Button ID="cmdSave" Runat="server" Text="Submit form" CssClass="submitBtn" CausesValidation="true" ValidationGroup="FormValid" Width="50%"></asp:Button></td>
      </tr>
        <tr>
            <td class="LeftColumn"><span class="label-title">Use and protection of your personal information</span>
                The Institute will use the information which you have provided in this form to respond to your request or process your transaction and will hold and protect it 
                    in accordance with the Institute’s <a href="https://www.charteredaccountants.ie/Privacy-statement" target="_blank"><strong>privacy statement</strong></a>, which explains your rights in relation to your personal data.</td>
            <td class="RightColumn">               
                     </td>

        </tr>
    </table>
    <cc3:User id="AptifyEbusinessUser1" runat="server" />
</div> 
