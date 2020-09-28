<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.ContactForm_Roombooking_Queries__caiClass" CodeFile="ContactForm_Roombooking_Queries__cai.ascx.vb" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>


<div class="content-container clearfix"> 
    <asp:Label id="tblSuccessMessage" runat="server"></asp:Label>
    <asp:Label id="lblError" ForeColor="Red" runat="server" Visible="False"  />
    <table runat="server" id="tblMain" class="data-form">
        <tr>
            <td class="LeftColumn" colspan="2"><span class="label-title">Please note: all fields are mandatory</span></td>
            
        </tr>
      <tr>
           <td class="LeftColumn">
               <div class="required">Name<asp:RequiredFieldValidator ID="RequiredFieldValidator0" runat="server" ControlToValidate="txtName_of_Person" 
                   ErrorMessage="Name is required" ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;">
                   </asp:RequiredFieldValidator>
        </div></td>
           <td class="RightColumn">
               <asp:TextBox id="txtName_of_person" runat="server" AptifyDataField="Name_of_person" Width="100%" ValidationGroup="FormValid" Placeholder="Please enter your full name" />
               <asp:TextBox id="txtForm_title" runat="server" AptifyDataField="Form_title" Width="100%" Visible ="false"/>
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><div class="required">Phone number<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPhone_Number" 
               ErrorMessage="A phone number is required" ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;"></asp:RequiredFieldValidator>
               <asp:RegularExpressionValidator ID="regexPhoneValid" runat="server" Display="Dynamic" ControlToValidate="txtPhone_Number" ErrorMessage="Invalid phone number format"
            ForeColor="Red" ValidationGroup="FormValid" ValidationExpression="^\(?\+?[\d\(\-\s\)]+$" style="float: right;"></asp:RegularExpressionValidator>
                </div>
            </td>
           <td class="RightColumn">
               <asp:TextBox id="txtPhone_number" runat="server" AptifyDataField="Phone_number" Width="100%" ValidationGroup="FormValid" Placeholder="Please enter your phone number" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><div class="required">Email address<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmail_Address" 
               ErrorMessage="Email is required" ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;"></asp:RequiredFieldValidator>
                   <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" Display="Dynamic" ControlToValidate="txtEmail_Address" ErrorMessage="Invalid email format"
            ForeColor="Red" ValidationGroup="FormValid" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" style="float: right;"></asp:RegularExpressionValidator>
            </div></td>
           <td class="RightColumn">
               <asp:TextBox id="txtEmail_address" runat="server" AptifyDataField="Email_address" Width="100%" ValidationGroup="FormValid" Placeholder="Please enter your email address" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><div class="required">Event details<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEvent_details" 
               ErrorMessage="Event information is required" ForeColor="Red" Display="Dynamic" ValidationGroup="FormValid" style="float: right;"></asp:RequiredFieldValidator>
            </div></td>
           <td class="RightColumn">
               <asp:TextBox id="txtEvent_details" runat="server" AptifyDataField="Event_details" Width="100%"  TextMode="MultiLine" Height="100px" Placeholder="Please enter any other event details, comments or queries here." />
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
