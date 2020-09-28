<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Generated.ContactForm_In_House_Training_Express_Interest__cClass" CodeFile="ContactForm_In_House_Training_Express_Interest__c.ascx.vb" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>


<div class="content-container clearfix"> 
    <asp:Label runat="server" ID="tblSuccessMessage"></asp:Label>
    <asp:Label id="lblError" ForeColor="Red" runat="server" Visible="False"  />
    <table runat="server" id="tblMain" class="data-form">
      <tr>
           <td class="LeftColumn"><div class="required">Full name<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName_of_Person"
            ErrorMessage="Full name is required" ForeColor="Red" Display="Dynamic" ValidationGroup="ExpressInterestQuery" style="float: right;"></asp:RequiredFieldValidator></div></td>
           <td class="RightColumn">
               <asp:TextBox id="txtName_of_Person" runat="server" AptifyDataField="Name_of_Person" ValidationGroup="ExpressInterestQuery" Placeholder="Your first name and last name e.g. John Smith"  Width="100%" />
               <asp:TextBox id="txtForm_Title" Visible="false" runat="server" AptifyDataField="Form_Title" Width="100%" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><span class="label-title">Membership number (if applicable)</span></td>
           <td class="RightColumn">
               <asp:TextBox id="txtMembership_Number" runat="server" AptifyDataField="Membership_Number" Placeholder="Enter your membership number e.g. 0123456" Width="100%" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><div class="required">Phone<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPhone_Number"
            ErrorMessage="Phone number is required" ForeColor="Red" Display="Dynamic" ValidationGroup="ExpressInterestQuery" style="float: right;"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="regexPhoneValid" runat="server" Display="Dynamic" ControlToValidate="txtPhone_Number" ErrorMessage="Invalid phone number format"
            ForeColor="Red" ValidationGroup="ExpressInterestQuery" ValidationExpression="^\(?\+?[\d\(\-\s\)]+$" style="float: right;"></asp:RegularExpressionValidator></div></td>
           <td class="RightColumn">
               <asp:TextBox id="txtPhone_Number" runat="server" AptifyDataField="Phone_Number" ValidationGroup="ExpressInterestQuery" Placeholder="Your preferred contact number e.g. +353 87 124567" Width="100%" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><div class="required">Email<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmail_Address"
            ErrorMessage="Email is required" ForeColor="Red" Display="Dynamic" ValidationGroup="ExpressInterestQuery" style="float: right;"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="regexEmailValid" runat="server" Display="Dynamic" ControlToValidate="txtEmail_Address" ErrorMessage="Invalid email format"
            ForeColor="Red" ValidationGroup="ExpressInterestQuery" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" style="float: right;"></asp:RegularExpressionValidator></div></td>
           <td class="RightColumn">
               <asp:TextBox id="txtEmail_Address" runat="server" AptifyDataField="Email_Address" ValidationGroup="ExpressInterestQuery" Placeholder="Your email (for registered user, please use your registered email) e.g. John@gmail.com" Width="100%" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><div class="required">Subject<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSubject"
            ErrorMessage="Subject title is required" ForeColor="Red" Display="Dynamic" ValidationGroup="ExpressInterestQuery" style="float: right;"></asp:RequiredFieldValidator></div></td>
           <td class="RightColumn">
               <asp:TextBox id="txtSubject" runat="server" AptifyDataField="Subject" ValidationGroup="ExpressInterestQuery" Placeholder="Enter your query / comment subject here" Width="100%" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><div class="required">Training requirements<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtTraining_Required"
            ErrorMessage="Specify training required" ForeColor="Red" Display="Dynamic" ValidationGroup="ExpressInterestQuery" style="float: right;"></asp:RequiredFieldValidator></div></td>
           <td class="RightColumn">
               <asp:TextBox id="txtTraining_Required" runat="server" AptifyDataField="Training_Required" Width="100%" TextMode="MultiLine" ValidationGroup="ExpressInterestQuery" Placeholder="e.g. Audit, Companies Act, Financial Reporting etc." Height="100px" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><span class="label-title">Your query</span></td>
           <td class="RightColumn">
               <asp:TextBox id="txtQuery_Matter" runat="server" AptifyDataField="Query_Matter" Width="100%"  TextMode="MultiLine" Placeholder="Enter you query / comments here" Height="100px" />
           </td>
      </tr>
      <tr>
           <td class="LeftColumn"><span class="label-title">CAPTCHA</span></td>
           <td class="RightColumn">

<div class="g-recaptcha" data-sitekey="6LejaCgTAAAAAH30AlwIKL5fVQMF6vHfyIB7MYF0"></div>
		
           </td>
      </tr>
      <tr>
        <td style="text-align:right"><asp:Button ID="cmdSave" Runat="server" Text="Submit Query" CssClass="submitBtn" ></asp:Button></td><td></td>
      </tr>
    </table>
    
    <cc3:User id="AptifyEbusinessUser1" runat="server" />
</div> 

<script type="text/javascript">
 function IsRecapchaValid()
   {
    var res = grecaptcha.getResponse(widId);
    if (res == "" || res == undefined || res.length == 0)
        {
          return false;
        }
         return true;
    }
 </script>

<script type="text/javascript">
    var widId = "6LejaCgTAAAAAH30AlwIKL5fVQMF6vHfyIB7MYF0";

 </script>
