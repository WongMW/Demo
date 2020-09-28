<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.ContactForm_Technical_Queries__caiClass" CodeFile="ContactForm_Technical_Queries__cai.ascx.vb" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>


<div class="content-container clearfix">
    <asp:Label id="lblError" ForeColor="Red" runat="server" Visible="False"  />
    <asp:Label id="tblSuccessMessage" runat="server" /> 
    <table runat="server" id="tblMain" class="data-form">
        <tr>
            <td class="LeftColumn"><span class="label-title">Please note: all fields are mandatory</span></td><td></td>
        </tr>
      <tr>
           <td class="LeftColumn">
               <div class="required">
                   Full name
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator0" runat="server" ControlToValidate="txtName_of_Person" ErrorMessage="Full name is required" ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;"></asp:RequiredFieldValidator>
               </div>
           </td>
           <td class="RightColumn">
               <asp:TextBox id="txtName_of_Person" runat="server" AptifyDataField="Name_of_Person" Width="100%" ValidationGroup="FormValid" Placeholder="Your first name and last name e.g. John Smith"/>
               <asp:TextBox id="txtForm_Title" runat="server" AptifyDataField="Form_Title" Width="100%" Visible="false" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">
                <div class="required">
                   Membership number
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMember_number" ErrorMessage="Membership number is required" ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;"></asp:RequiredFieldValidator>
               </div>
           </td>
           <td class="RightColumn">
               <asp:TextBox id="txtMember_number" runat="server" AptifyDataField="Member_number" Width="100%" ValidationGroup="FormValid" Placeholder="Your membership number e.g. 001122"/>
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">
               <div class="required">
                   Phone
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPhone_Number" ErrorMessage="Phone number is required" ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;"></asp:RequiredFieldValidator>
                   <asp:RegularExpressionValidator ID="regexPhoneValid" runat="server" Display="Dynamic" ControlToValidate="txtPhone_Number" ErrorMessage="Invalid phone number format" ForeColor="Red" ValidationGroup="FormValid" ValidationExpression="^\(?\+?[\d\(\-\s\)]+$" style="float: right;"></asp:RegularExpressionValidator>
               </div>
           </td>
           <td class="RightColumn">
               <asp:TextBox id="txtPhone_Number" runat="server" AptifyDataField="Phone_Number" Width="100%" ValidationGroup="FormValid" Placeholder="Your preferred contact number e.g. +353 87 124567"/>
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">
               <div class="required">
                   Email
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmail_Address" ErrorMessage="Email is required" ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;"></asp:RequiredFieldValidator>
                   <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" Display="Dynamic" ControlToValidate="txtEmail_Address" ErrorMessage="Invalid email format" ForeColor="Red" ValidationGroup="FormValid" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" style="float: right;"></asp:RegularExpressionValidator>

               </div>
           </td>
           <td class="RightColumn">
               <asp:TextBox id="txtEmail_Address" runat="server" AptifyDataField="Email_Address" Width="100%" ValidationGroup="FormValid" Placeholder="Your email e.g. John.smith@gmail.com"/>
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">
               <div class="required">
                   Subject
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSubject" ErrorMessage="Subject title is required" ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;"></asp:RequiredFieldValidator>
               </div>
           </td>
           <td class="RightColumn">
               <asp:TextBox id="txtSubject" runat="server" AptifyDataField="Subject" Width="100%" ValidationGroup="FormValid" Placeholder="Enter your query/comment subject here"/>
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">
               <div class="required">
                   Your query
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtQuery_Matter" ErrorMessage="Query is required" ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;"></asp:RequiredFieldValidator>
               </div>
           </td>
           <td class="RightColumn">
               <asp:TextBox id="txtQuery_Matter" runat="server" AptifyDataField="Query_Matter" Width="100%"  TextMode="MultiLine" Height="100px" ValidationGroup="FormValid" Placeholder="Enter you query/comments here"/>
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">
               <div class="required">
                   Area
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="Area" ErrorMessage="Area is required" ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;"></asp:RequiredFieldValidator>
               </div>
           </td>
           <td class="RightColumn">
               <asp:DropDownList id="Area" runat="server" AptifyDataField="Area" >
                   <asp:ListItem>Republic of Ireland</asp:ListItem>
                   <asp:ListItem>Northern Ireland</asp:ListItem>
               </asp:DropDownList>
           </td>
      </tr>
      <tr>
        <td colspan="2" style="text-align:center"><asp:Button ID="cmdSave" Runat="server" Text="Submit form" CssClass="submitBtn" CausesValidation="true" ValidationGroup="FormValid" Width="50%"></asp:Button></td>
      </tr>
        <tr>
            <td class="LeftColumn"><span class="label-title">Use and protection of your personal information</span>
                The Institute of Chartered Accountants in Ireland (“the Institute”) will use the information contained in this form together with any other information otherwise furnished by you or by other third 
                parties for the purposes of processing this application; managing and administering your membership; and generally for the performance by the Institute of its regulatory, supervisory and statutory 
                functions, as more fully described in the Institute’s privacy statement, which explains your rights in relation to your personal data. You acknowledge you have read and understand the 
                     <a href="https://www.charteredaccountants.ie/Privacy-statement" target="_blank"><strong>privacy statement</strong></a>.</td>
            <td class="RightColumn">               
                     </td>

        </tr>
    </table>
    
    <cc3:User id="AptifyEbusinessUser1" runat="server" />
</div> 
