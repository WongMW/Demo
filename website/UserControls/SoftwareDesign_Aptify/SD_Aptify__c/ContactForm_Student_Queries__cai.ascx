<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.ContactForm_Student_Queries__caiClass" CodeFile="ContactForm_Student_Queries__cai.ascx.vb" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>


<div class="content-container clearfix"> 
    <asp:Label runat="server" ID="tblSuccessMessage"></asp:Label>
    <asp:Label id="lblError" ForeColor="Red" runat="server" Visible="False"  />
    <table runat="server" id="tblMain" class="data-form">
      <tr>
           <td class="LeftColumn">
               <div class="required">Full name
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator0" runat="server" ControlToValidate="txtName_of_Person"
            ErrorMessage="Subject title is required" ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;">
                   </asp:RequiredFieldValidator>
               </div>
           </td>
           <td class="RightColumn">
               <asp:TextBox id="txtName_of_Person" runat="server" ValidationGroup="FormValid" Placeholder="Your first name and last name e.g. John Smith" AptifyDataField="Name_of_Person" Width="100%" />
               <asp:TextBox id="txtForm_Title" runat="server" AptifyDataField="Form_Title" Width="100%" Visible="false" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><span class="label-title">Student number (if applicable)</span></td>
           <td class="RightColumn">
               <asp:TextBox id="txtStudent_Number" runat="server" AptifyDataField="Student_Number" Placeholder="Enter your student number e.g. 0123456" Width="100%" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">
               <span class="label-title">
                   Phone
                   <asp:RegularExpressionValidator ID="regexPhoneValid" runat="server" Display="Dynamic" ControlToValidate="txtPhone_Number" ErrorMessage="Invalid phone number format"
            ForeColor="Red" ValidationGroup="FormValid" ValidationExpression="^\(?\+?[\d\(\-\s\)]+$" style="float: right;"></asp:RegularExpressionValidator>
               </span>
           </td>
           <td class="RightColumn">
               <asp:TextBox id="txtPhone_Number" runat="server" AptifyDataField="Phone_Number" ValidationGroup="FormValid" Placeholder="Your preferred contact number e.g. +353 87 124567" Width="100%" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">
               <div class="required">
                   Email
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmail_Address"
            ErrorMessage="Email is required" ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;"></asp:RequiredFieldValidator>
                   <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" Display="Dynamic" ControlToValidate="txtEmail_Address" ErrorMessage="Invalid email format"
            ForeColor="Red" ValidationGroup="FormValid" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" style="float: right;"></asp:RegularExpressionValidator>
               </div>
           </td>
           <td class="RightColumn">
               <asp:TextBox id="txtEmail_Address" runat="server" AptifyDataField="Email_Address" ValidationGroup="FormValid" Placeholder="Your email (for registered user, please use your registered email) e.g. John@gmail.com" Width="100%" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">
               <div class="required">
                   Subject
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSubject"
            ErrorMessage="Subject title is required" ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;"></asp:RequiredFieldValidator>
               </div>

           </td>
           <td class="RightColumn">
               <asp:TextBox id="txtSubject" runat="server" ValidationGroup="FormValid" Placeholder="Enter your query / comment subject here" AptifyDataField="Subject" Width="100%" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">
               <div class="required">
                   Your query
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQuery_Matter"
            ErrorMessage="Query is required" ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;"></asp:RequiredFieldValidator>
               </div>
           </td>
           <td class="RightColumn">
               <asp:TextBox id="txtQuery_Matter" runat="server" AptifyDataField="Query_Matter" Width="100%"  TextMode="MultiLine" ValidationGroup="FormValid" Placeholder="Enter you query / comments here" Height="100px" />
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
    <cc3:User id="AptifyEbusinessUser1" runat="server" />
</div> 
