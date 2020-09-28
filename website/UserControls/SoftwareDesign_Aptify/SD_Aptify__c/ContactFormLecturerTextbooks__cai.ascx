<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.ContactFormLecturerTextbooks__caiClass" CodeFile="ContactFormLecturerTextbooks__cai.ascx.vb" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<style>
    .textbox {
        font-family: 'Source Sans Pro';
        font-size: 16px; 
        font-weight:400; 
        line-height: 1.4em;
    }
</style>

<div class="content-container clearfix">
    <asp:Label id="lblError" ForeColor="Red" runat="server" Visible="False"  />
    <asp:Label id="tblSuccessMessage" runat="server" Visible="False"  /> 
    <table runat="server" id="tblMain" class="data-form">
     
      <tr>
           <td class="LeftColumn">
               <div class="required">Name (required)
                <asp:RequiredFieldValidator ID="RequiredFieldValidator0" runat="server" ControlToValidate="txtName" ErrorMessage="Full name is required" ForeColor="Red" 
                    Display="Dynamic" ValidationGroup="FormValid" style="float:right"></asp:RequiredFieldValidator>
                   </div></td>
           <td class="RightColumn">
               <asp:TextBox id="txtName" runat="server" AptifyDataField="Name" Width="100%" ValidationGroup="FormValid" Placeholder="Your full name" CssClass="textbox" />
               <asp:TextBox id="txtForm_Title" runat="server" AptifyDataField="Form_Title" Width="100%" Visible="false" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><div class="required">Higher learning institution (required)
               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtInstitution" ErrorMessage="Higher learning institution is required" ForeColor="Red" 
                    Display="Dynamic" ValidationGroup="FormValid" style="float:right"></asp:RequiredFieldValidator>
           </div></td>
           <td class="RightColumn">
               <asp:TextBox id="txtInstitution" runat="server" AptifyDataField="Institution" Width="100%" CssClass="textbox" ValidationGroup="FormValid" Placeholder="Your university/higher learning institution" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><div class="required">Address (required)
               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAddress" ErrorMessage="Address is required" ForeColor="Red" 
                    Display="Dynamic" ValidationGroup="FormValid" style="float:right"></asp:RequiredFieldValidator>
           </div></td>
           <td class="RightColumn">
               <asp:TextBox id="txtAddress" runat="server" AptifyDataField="Address" Width="100%" CssClass="textbox" TextMode="MultiLine" Height="100px" ValidationGroup="FormValid" Placeholder="Your address" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><div class="required">Phone number (required)
               <asp:RegularExpressionValidator ID="regexPhoneValid" runat="server" Display="Dynamic" ControlToValidate="txtPhone" ErrorMessage="Invalid phone number format"
                   ForeColor="Red" ValidationGroup="FormValid" ValidationExpression="^\(?\+?[\d\(\-\s\)]+$" style="float:right;"></asp:RegularExpressionValidator>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPhone" ErrorMessage="Phone number is required" ForeColor="Red" 
                    Display="Dynamic" ValidationGroup="FormValid" style="float:right"></asp:RequiredFieldValidator>
           </div></td>
           <td class="RightColumn">
               <asp:TextBox id="txtPhone" runat="server" AptifyDataField="Phone" Width="100%" CssClass="textbox" ValidationGroup="FormValid" Placeholder="Your phone number" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><div class="required">Email (required)
               <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEmail_Address" ErrorMessage="Email is required" ForeColor="Red" 
                    Display="Dynamic" ValidationGroup="FormValid" style="float:right"></asp:RequiredFieldValidator>
               <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" Display="Dynamic" ControlToValidate="txtEmail_Address" ErrorMessage="Invalid email format"
                   ForeColor="Red" ValidationGroup="FormValid" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" style="float:right;"></asp:RegularExpressionValidator>
               </div></td>
           <td class="RightColumn">
               <asp:TextBox id="txtEmail_Address" runat="server" AptifyDataField="Email_Address" Width="100%" CssClass="textbox" ValidationGroup="FormValid" Placeholder="Your email address" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><span class="label-title">Subject(s) taught</span></td>
           <td class="RightColumn">
               <asp:TextBox id="txtSubjects" runat="server" AptifyDataField="Subjects" Width="100%" CssClass="textbox" TextMode="MultiLine" Height="100px" ValidationGroup="FormValid" Placeholder="Subjects you teach" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><span class="label-title">Course title(s)</span></td>
           <td class="RightColumn">
               <asp:TextBox id="txtCourseTitle" runat="server" AptifyDataField="CourseTitle" Width="100%" CssClass="textbox" TextMode="MultiLine" Height="100px" ValidationGroup="FormValid" Placeholder="Course titles" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><span class="label-title">Year(s) (inc. post-grad)</span></td>
           <td class="RightColumn">
               <asp:TextBox id="txtYears" runat="server" AptifyDataField="Years" Width="100%" CssClass="textbox" ValidationGroup="FormValid" Placeholder="Level(s) you teach at e.g. BComm/MA, etc." />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><span class="label-title">Class size(s)</span></td>
           <td class="RightColumn">
               <asp:TextBox id="txtClassSize" runat="server" AptifyDataField="ClassSize" Width="100%" CssClass="textbox" ValidationGroup="FormValid" Placeholder="The size of your class(es)" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><div class="required">Chartered Accountants Ireland textbook(s) requested (required)
               <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtTextbooksRequested" ErrorMessage="This field is required" ForeColor="Red" 
                    Display="Dynamic" ValidationGroup="FormValid" style="float:right"></asp:RequiredFieldValidator>
               </div></td>
           <td class="RightColumn">
               <asp:TextBox id="txtTextbooksRequested" runat="server" AptifyDataField="TextbooksRequested" Width="100%" CssClass="textbox" TextMode="MultiLine" Height="100px" ValidationGroup="FormValid" Placeholder="The textbook(s) for which you are requesting inspection copies" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><span class="label-title">What textbooks are you currently using?</span></td>
           <td class="RightColumn">
               <asp:TextBox id="txtCurrentTextbooks" runat="server" AptifyDataField="CurrentTextbooks" Width="100%" CssClass="textbox" TextMode="MultiLine" Height="100px" Placeholder="The textbook(s) you are currently using" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><span class="label-title">Are you a member of Chartered Accountants Ireland?</span></td>
           <td class="RightColumn">
               <asp:DropDownList id="cmbMembership" runat="server" AptifyDataField="Membership" CssClass="textbox" >
                   <asp:ListItem>No</asp:ListItem>
                   <asp:ListItem>Yes</asp:ListItem>
               </asp:DropDownList>
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><span class="label-title">Membership number (if applicable)</span></td>
           <td class="RightColumn">
               <asp:TextBox id="txtMemberNumber" runat="server" AptifyDataField="MemberNumber" Width="100%" CssClass="textbox" Placeholder="Your membership number. Type 'N/A' if you are not a member " />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><span class="label-title">Please specify any other professional membership</span></td>
           <td class="RightColumn">
               <asp:TextBox id="txtOtherMembership" runat="server" AptifyDataField="OtherMembership" Width="100%" CssClass="textbox" Placeholder="Other professional memberships" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><span class="label-title">Would you like to be contacted about related products or services? </span></td>
           <td class="RightColumn"> <asp:CheckBox id="chkConsentBox" runat="server" AptifyDataField="ConsentBox" />Yes, please contact me about related products and services of the Institute
               
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
