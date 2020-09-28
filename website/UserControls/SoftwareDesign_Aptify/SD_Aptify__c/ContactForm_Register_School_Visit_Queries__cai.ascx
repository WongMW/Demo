<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.ContactForm_Register_School_Visit_Queries__caiClass" CodeFile="ContactForm_Register_School_Visit_Queries__cai.ascx.vb" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>


<div class="content-container clearfix"> 
    <asp:Label runat="server" id="tblSuccessMessage" />
    <asp:Label id="lblError" ForeColor="Red" runat="server" Visible="False"  />
    <table runat="server" id="tblMain" class="data-form">
      <tr>
           <td class="LeftColumn">
               <div class="required">
                   Full name of contact person (required)
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator0" runat="server" ControlToValidate="txtName_of_Person" ErrorMessage="Full name is required" ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;"></asp:RequiredFieldValidator>
               </div>
           </td>
           <td class="RightColumn">
               <asp:TextBox id="txtName_of_Person" runat="server" AptifyDataField="Name_of_Person" Width="100%" ValidationGroup="FormValid" Placeholder="Your first name and last name e.g. John Smith"/>
               <asp:TextBox id="txtForm_Title" runat="server" AptifyDataField="Form_Title" Width="100%" Visible="False"/>
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">
               <div class="required">
                   Phone (required)
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPhone_Number" ErrorMessage="Phone number is required" ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;"></asp:RequiredFieldValidator>
                   <asp:RegularExpressionValidator ID="regexPhoneValid" runat="server" Display="Dynamic" ControlToValidate="txtPhone_Number" ErrorMessage="Invalid phone number format" ForeColor="Red" ValidationGroup="FormValid" ValidationExpression="^\(?\+?[\d\(\-\s\)]+$" style="float: right;"></asp:RegularExpressionValidator>
               </div>
           </td>
           <td class="RightColumn">
               <asp:TextBox id="txtPhone_Number" runat="server" AptifyDataField="Phone_Number" Width="100%" ValidationGroup="FormValid" Placeholder="Your preferred contact number e.g. +353 87 124567" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">
               <div class="required">
                   Email (required)
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmail_Address" ErrorMessage="Email is required" ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;"></asp:RequiredFieldValidator>
                   <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" Display="Dynamic" ControlToValidate="txtEmail_Address" ErrorMessage="Invalid email format" ForeColor="Red" ValidationGroup="FormValid" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" style="float: right;"></asp:RegularExpressionValidator>
               </div>
           </td>
           <td class="RightColumn">
               <asp:TextBox id="txtEmail_Address" runat="server" AptifyDataField="Email_Address" Width="100%" ValidationGroup="FormValid" Placeholder="Your preferred email address e.g. John@gmail.com" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">
               <div class="required">
                   School name (required)
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSchool_Name" ErrorMessage="School name is required" ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;"></asp:RequiredFieldValidator>
               </div>
           </td>
           <td class="RightColumn">
               <asp:TextBox id="txtSchool_Name" runat="server" AptifyDataField="School_Name" Width="100%" ValidationGroup="FormValid" Placeholder="Enter name of School e.g. ABC College" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">
               <div class="required">
                   School address (required)
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSchool_Address" ErrorMessage="School address is required" ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;"></asp:RequiredFieldValidator>
               </div>
           </td>
           <td class="RightColumn">
               <asp:TextBox id="txtSchool_Address" runat="server" AptifyDataField="School_Address" Width="100%"  TextMode="MultiLine" Height="100px" ValidationGroup="FormValid" Placeholder="Enter address of school e.g. ABC College, 47 Pearse Street, Dublin 2" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">
               <span class="label-title">
                   Approx # of students
                   <asp:RegularExpressionValidator ID="regexIntValid" runat="server" Display="Dynamic" ControlToValidate="txtNumber_Students" ErrorMessage="Invalid number format" ForeColor="Red" ValidationGroup="FormValid" ValidationExpression="^\d{0,9}$" style="float: right;"></asp:RegularExpressionValidator>
               </span>
           </td>
           <td class="RightColumn">
               <asp:TextBox id="txtNumber_Students" runat="server" AptifyDataField="Number_Students" Width="100%" ValidationGroup="FormValid" Placeholder="Enter the approximate number of students in the school e.g. 450" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><span class="label-title">Preferred time</span></td>
           <td class="RightColumn">
               <asp:TextBox id="txtPreferred_Time" runat="server" AptifyDataField="Preferred_Time" Width="100%" Placeholder="Enter your preferred times e.g. 2:00pm or 12pm-3pm" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><span class="label-title">Preferred dates/days</span></td>
           <td class="RightColumn">
               <asp:TextBox id="txtPreferred_Dates" runat="server" AptifyDataField="Preferred_Dates" Width="100%" Placeholder="Enter your preferred dates/days e.g. 31-Dec-2016 or Every Monday" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><span class="label-title">Subject</span></td>
           <td class="RightColumn">
               <asp:TextBox id="txtSubject" runat="server" AptifyDataField="Subject" Width="100%" Placeholder="Enter your query / comment subject here" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><span class="label-title">Your query</span></td>
           <td class="RightColumn">
               <asp:TextBox id="txtQuery_Matter" runat="server" AptifyDataField="Query_Matter" Width="100%"  TextMode="MultiLine" Height="100px" Placeholder="Enter you query / comments here" />
           </td>
      </tr>
      <tr>
        <td  class="LeftColumn" colspan="2" style="text-align:center"><asp:Button ID="cmdSave" Runat="server" Text="Submit form" CssClass="submitBtn" CausesValidation="true" ValidationGroup="FormValid" Width="50%"></asp:Button></td>
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
