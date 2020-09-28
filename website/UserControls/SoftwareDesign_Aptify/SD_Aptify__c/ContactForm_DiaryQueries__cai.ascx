<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.ContactForm_DiaryQueries__caiClass" CodeFile="ContactForm_DiaryQueries__cai.ascx.vb" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>


<div class="content-container clearfix"> 
    <asp:Label id="lblError" ForeColor="Red" runat="server" Visible="False"  />
    <asp:Label id="tblSuccessMessage" runat="server" />
    <table runat="server" id="tblMain" class="data-form">
        <tr>
            <td class="LeftColumn"><p class="info-note">Please ensure you click the submit button at the bottom of the form to send your diary request. Any fields marked as 
                required <strong>must</strong> be completed. Diaries will be posted within 3-5 working days.</p></td>
        </tr>
      <tr>
           <td class="LeftColumn"><div class="required">
                   Full name (required)
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator0" runat="server" ControlToValidate="txtName_of_Person" ErrorMessage="Full name is required" 
                       ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;"></asp:RequiredFieldValidator>
               </div></td>
           <td class="RightColumn">
               <asp:TextBox id="txtName_of_Person" runat="server" AptifyDataField="Name_of_Person" Width="100%" Placeholder="Please type your name here" />
               <asp:TextBox id="txtForm_Title" runat="server" AptifyDataField="Form_Title" Width="100%" Visible="false" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><div class="required">
                   Email (required)
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmail_Address" ErrorMessage="Email is required" ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;"></asp:RequiredFieldValidator>
                   <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" Display="Dynamic" ControlToValidate="txtEmail_Address" ErrorMessage="Invalid email format" ForeColor="Red" ValidationGroup="FormValid" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" style="float: right;"></asp:RegularExpressionValidator>

               </div></td>
           <td class="RightColumn">
               <asp:TextBox id="txtEmail_Address" runat="server" AptifyDataField="Email_Address" Width="100%" ValidationGroup="FormValid" Placeholder="Your email (for registered user, please use your registered email) e.g. John@gmail.com" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><span class="label-title">
                   Member/student number                  
               </span></td>
           <td class="RightColumn">
               <asp:TextBox id="txtMemberNumber" runat="server" AptifyDataField="MemberNumber" Width="100%" ValidationGroup="FormValid" Placeholder="Member or student number" />
           </td>
      </tr>

<%--      <tr>
           <td class="LeftColumn"><span class="label-title">Phone number
               <asp:RegularExpressionValidator ID="regexPhoneValid" runat="server" Display="Dynamic" ControlToValidate="txtPhone_Number" ErrorMessage="Invalid phone number format" 
                   ForeColor="Red" ValidationGroup="FormValid" ValidationExpression="^\(?\+?[\d\(\-\s\)]+$" style="float: right;"></asp:RegularExpressionValidator>
                </span>
           </td>
           <td class="RightColumn">
               <asp:TextBox id="txtPhone_number" runat="server" AptifyDataField="Phone_number" Width="100%" ValidationGroup="FormValid" Placeholder="Your preferred contact number" />
           </td>
      </tr>--%>
      <tr>
           <td class="LeftColumn"><div class="required">
                   Address line 1 (required)
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAddress_1" ErrorMessage="First line of address is required" 
                       ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;"></asp:RequiredFieldValidator>
               </div></td>
           <td class="RightColumn">
               <asp:TextBox id="txtAddress_1" runat="server" AptifyDataField="Address_1" Width="100%" ValidationGroup="FormValid" Placeholder="First line of your address" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn">
               <span class="label-title">Address line 2
                </span>
           </td>
           <td class="RightColumn">
               <asp:TextBox id="txtAddress_2" runat="server" AptifyDataField="Address_2" Width="100%" Placeholder="Second line of your address" />
           </td>
      </tr>
<%--      <tr>
           <td class="LeftColumn"><span class="label-title">Address line 3
                </span></td>
           <td class="RightColumn">
               <asp:TextBox id="txtAddress_3" runat="server" AptifyDataField="Address_3" Width="100%" Placeholder="Third line of your address" />
           </td>
      </tr>--%>
      <tr>
           <td class="LeftColumn"><div class="required">
                   Town/city (required)
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtTown" ErrorMessage="Town/city is required" 
                       ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;"></asp:RequiredFieldValidator>
               </div></td>
           <td class="RightColumn">
               <asp:TextBox id="txtTown" runat="server" AptifyDataField="Town" Width="100%" ValidationGroup="FormValid" Placeholder="Town or city" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><span class="label-title">County</span></td>
           <td class="RightColumn">
               <asp:TextBox id="txtCounty" runat="server" AptifyDataField="County" Width="100%" Placeholder="Enter your county here" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><span class="label-title">Postcode (if applicable)</span></td>
           <td class="RightColumn">
               <asp:TextBox id="txtPostcode" runat="server" AptifyDataField="Postcode" Width="100%" Placeholder="Enter your postcode" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><span class="label-title">Country (if based outside of Ireland)</span></td>
           <td class="RightColumn">
               <asp:TextBox id="txtCountry" runat="server" AptifyDataField="Country" Width="100%" Placeholder="Enter your country" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><span class="label-title">Number of diaries (if more than one)</span></td>
           <td class="RightColumn">
               <asp:TextBox id="txtNumber_diaries" runat="server" AptifyDataField="Number_diaries" Width="100%" Placeholder="Number of diaries" />
           </td>
      </tr>
      <tr>
        <td colspan="2" class="LeftColumn" style="text-align:center;"><asp:Button ID="cmdSave" Runat="server" Text="Send diary request" CssClass="submitBtn" CausesValidation="true" ValidationGroup="FormValid" Width="50%"></asp:Button></td>
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
