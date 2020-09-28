<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.ContactForm_Log_Vacancies_Queries__caiClass" CodeFile="ContactForm_Log_Vacancies_Queries__cai.ascx.vb" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>


<div class="content-container clearfix"> 
    <asp:Label runat="server" id="tblSuccessMessage"></asp:Label>
    <asp:Label id="lblError" ForeColor="Red" runat="server" Visible="False"  />
    <table runat="server" id="tblMain" class="data-form">
      <tr>
           <td class="LeftColumn">
               <div class="required">
                   Full name (requested by person) (required)
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator0" runat="server" ControlToValidate="txtName_of_Person" ErrorMessage="Full name of requester is required" ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;"></asp:RequiredFieldValidator>
               </div>
           </td>
           <td class="RightColumn">
               <asp:TextBox id="txtName_of_Person" runat="server" AptifyDataField="Name_of_Person" Width="100%" ValidationGroup="FormValid" Placeholder="Your first name and last name e.g. John Smith" />
               <asp:TextBox id="txtForm_Title" runat="server" AptifyDataField="Form_Title" Width="100%" Visible="false" />
           </td>
      </tr>
        <tr>
            
           <td class="LeftColumn">
              <div class="required"> 
               Phone number (required)
               <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtPhone_Number" ErrorMessage="Phone number is required" ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;"></asp:RequiredFieldValidator>
               <asp:RegularExpressionValidator ID="regexPhoneValid" runat="server" Display="Dynamic" ControlToValidate="txtPhone_Number" ErrorMessage="Invalid phone number format" ForeColor="Red" ValidationGroup="FormValid" ValidationExpression="^\(?\+?[\d\(\-\s\)]+$" style="float: right;"></asp:RegularExpressionValidator>
                </div>
           </td> 
           <td class="RightColumn">
               <asp:TextBox id="txtPhone_number" runat="server" AptifyDataField="Phone_number" Width="100%" ValidationGroup="FormValid" Placeholder="Your phone number e.g. 353 1 2304304"/>
               
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">
               <div class="required">
                    Email (requested by person) (required)
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail_Address" ErrorMessage="Email of requester is required" ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;"></asp:RequiredFieldValidator>
                   <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" Display="Dynamic" ControlToValidate="txtEmail_Address" ErrorMessage="Invalid email format" ForeColor="Red" ValidationGroup="FormValid" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" style="float: right;"></asp:RegularExpressionValidator>
               </div>
           </td>
           <td class="RightColumn">
               <asp:TextBox id="txtEmail_Address" runat="server" AptifyDataField="Email_Address" Width="100%" ValidationGroup="FormValid" Placeholder="Your email (for registered user, please use your registered email) e.g. John@gmail.com"/>
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">
               <div class="required">
                    Position/role to log (required)
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPosition_Title" ErrorMessage="Position/role title is required" ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;"></asp:RequiredFieldValidator>
               </div>
           </td>
           <td class="RightColumn">
               <asp:TextBox id="txtPosition_Title" runat="server" AptifyDataField="Position_Title" Width="100%" ValidationGroup="FormValid" Placeholder="Advertised position e.g. financial controller"/>
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">
               <div class="required">
                   Firm name (required)
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFirm_Name" ErrorMessage="Firm name is required" ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;"></asp:RequiredFieldValidator>
               </div>
           </td>
           <td class="RightColumn">
               <asp:TextBox id="txtFirm_Name" runat="server" AptifyDataField="Firm_Name" Width="100%" ValidationGroup="FormValid" Placeholder="Enter name of firm e.g. ABC Ltd"/>
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">
               <span class="label-title">About the firm (short intro)</span>
           </td>
           <td class="RightColumn">
               <asp:TextBox id="txtAbout_Firm" runat="server" AptifyDataField="About_Firm" Width="100%"  TextMode="MultiLine" Height="100px" Placeholder="Enter a short piece about your firm"/>
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">
               <div class="required">
                   Firm address (required)
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtFirm_Address" ErrorMessage="Firm address is required" ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;"></asp:RequiredFieldValidator>
               </div>
           </td>
           <td class="RightColumn">
               <asp:TextBox id="txtFirm_Address" runat="server" AptifyDataField="Firm_Address" Width="100%"  TextMode="MultiLine" Height="100px" ValidationGroup="FormValid" Placeholder="Enter full address of firm e.g. Chartered Accountants House, 47 Pearse Street, Dublin 2" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">
               <div class="required">
                   Closing date (required)
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtClosing_Date" ErrorMessage="Closing date is required" ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;"></asp:RequiredFieldValidator>
                   <asp:RegularExpressionValidator ID="regexDateValid" runat="server" Display="Dynamic" ControlToValidate="txtClosing_Date" ErrorMessage="Date entered is invalid" ForeColor="Red" ValidationGroup="FormValid" ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]|(?:Jan|Mar|May|Jul|Aug|Oct|Dec)))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2]|(?:Jan|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec))\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)(?:0?2|(?:Feb))\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9]|(?:Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep))|(?:1[0-2]|(?:Oct|Nov|Dec)))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$" style="float: right;"></asp:RegularExpressionValidator>
               </div>
           </td>
           <td class="RightColumn">
               <asp:TextBox id="txtClosing_Date" runat="server" AptifyDataField="Closing_Date" Width="100%" ValidationGroup="FormValid" Placeholder="Closing date for applications e.g. 30/09/2016 or 30-Sep-2016 or 30/Sep/2016" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">
               <span class="label-title">Contact person (apply to)</span>
           </td>
           <td class="RightColumn">
               <asp:TextBox id="txtContact_Person" runat="server" AptifyDataField="Contact_Person" Width="100%" Placeholder="Person to contact first name and last name e.g. John Smith"/>
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">
               <div class="required">
                   Email (apply to) (required)
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEmail_Apply_Address" ErrorMessage="Email to apply to is required" ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;"></asp:RequiredFieldValidator>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ControlToValidate="txtEmail_Apply_Address" ErrorMessage="Invalid email format" ForeColor="Red" ValidationGroup="FormValid" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" style="float: right;"></asp:RegularExpressionValidator>
               </div>
           </td>
           <td class="RightColumn">
               <asp:TextBox id="txtEmail_Apply_Address" runat="server" AptifyDataField="Email_Apply_Address" Width="100%" ValidationGroup="FormValid" Placeholder="Apply to email e.g. John@gmail.com"/>
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">
               <div class="required">
                   Job specifications (required)
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtJob_Spec" ErrorMessage="Job specifications is required" ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;"></asp:RequiredFieldValidator>
               </div>
           </td>
           <td class="RightColumn">
               <asp:TextBox id="txtJob_Spec" runat="server" AptifyDataField="Job_Spec" Width="100%"  TextMode="MultiLine" Height="100px" ValidationGroup="FormValid" Placeholder="List the responsibilities of the position/role seperating each with a comma e.g. prepare accounts, bookkeeping, etc."/>
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">
               <div class="required">
                   Job requirements (required)
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtJob_Requirements" ErrorMessage="Job requirements is required" ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;"></asp:RequiredFieldValidator>
               </div>
           </td>
           <td class="RightColumn">
               <asp:TextBox id="txtJob_Requirements" runat="server" AptifyDataField="Job_Requirements" Width="100%"  TextMode="MultiLine" Height="100px" ValidationGroup="FormValid" Placeholder="List the qualities of the desired candidate seperating each with a comma e.g. a degree of any discipline, computer literate (in Excel), etc"/>
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
