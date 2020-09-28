<%@ control language="vb" autoeventwireup="false" inherits="Aptify.Framework.Web.eBusiness.Generated.ContactForm_Prospective_Students_Queries__cClass" codefile="ContactForm_Prospective_Students_Queries__c.ascx.vb" %>
<%@ register tagprefix="cc2" namespace="Aptify.Framework.Web.eBusiness" assembly="EBusinessGlobal" %>
<%@ register tagprefix="cc3" namespace="Aptify.Framework.Web.eBusiness" assembly="AptifyEBusinessUser" %>


<div class="content-container clearfix web-form">
    <asp:Label runat="server" ID="tblSuccessMessage"></asp:Label>
    <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="False" />
    <table runat="server" id="tblMain" class="data-form">
        
        <tr>
            <td class="LeftColumn">
                <div class="required">
                    Full name
               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName_of_Person"
                   ErrorMessage="Full name is required" ForeColor="Red" Display="Dynamic" ValidationGroup="PSQuery" Style="float: right;">
               </asp:RequiredFieldValidator>

                </div>
            </td>
            <td class="RightColumn">
                <asp:TextBox ID="txtName_of_Person" runat="server" AptifyDataField="Name_of_Person" ValidationGroup="PSQuery" Placeholder="Your first name and last name e.g. John Smith" Width="100%" />
                <asp:TextBox ID="txtForm_Title" runat="server" AptifyDataField="Form_Title" Width="100%" Visible="false" />
            </td>
        </tr>
        <tr>
            <td class="LeftColumn">
                <div class="required">
                    Phone<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPhone_Number"
                        ErrorMessage="Phone number is required" ForeColor="Red" Display="Dynamic" ValidationGroup="PSQuery" Style="float: right;"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="regexPhoneValid" runat="server" Display="Dynamic" ControlToValidate="txtPhone_Number" ErrorMessage="Invalid phone number format"
                            ForeColor="Red" ValidationGroup="PSQuery" ValidationExpression="^\(?\+?[\d\(\-\s\)]+$" Style="float: right;"></asp:RegularExpressionValidator>
                </div>
            </td>
            <td class="RightColumn">
                <asp:TextBox ID="txtPhone_Number" runat="server" AptifyDataField="Phone_Number" ValidationGroup="PSQuery" Placeholder="Your preferred contact number e.g. +353 87 124567" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="LeftColumn">
                <div class="required">
                    Email<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmail_Address"
                        ErrorMessage="Email is required" ForeColor="Red" Display="Dynamic" ValidationGroup="PSQuery" Style="float: right;"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="regexEmailValid" runat="server" Display="Dynamic" ControlToValidate="txtEmail_Address" ErrorMessage="Invalid email format"
                            ForeColor="Red" ValidationGroup="PSQuery" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Style="float: right;"></asp:RegularExpressionValidator>
                </div>
            </td>
            <td class="RightColumn">
                <asp:TextBox ID="txtEmail_Address" runat="server" AptifyDataField="Email_Address" ValidationGroup="PSQuery" Placeholder="Your email (for registered user, please use your registered email) e.g. John@gmail.com" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="LeftColumn">
                <div class="required">
                    Subject<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSubject"
                        ErrorMessage="Subject title is required" ForeColor="Red" Display="Dynamic" ValidationGroup="PSQuery" Style="float: right;"></asp:RequiredFieldValidator>
                </div>
            </td>
            <td class="RightColumn">
                <asp:TextBox ID="txtSubject" runat="server" ValidationGroup="PSQuery" Placeholder="Enter your query / comment subject here" AptifyDataField="Subject" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="LeftColumn">
                <div class="required">
                    Your query<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtQuery_Matter"
                        ErrorMessage="Query is required" ForeColor="Red" Display="Dynamic" ValidationGroup="PSQuery" Style="float: right;"></asp:RequiredFieldValidator>
                </div>
            </td>
            <td class="RightColumn">
                <asp:TextBox ID="txtQuery_Matter" runat="server" AptifyDataField="Query_Matter" Width="100%" TextMode="MultiLine" ValidationGroup="PSQuery" Placeholder="Enter you query / comments here" Height="100px" />
            </td>
        </tr>
        <tr>
            <td class="LeftColumn"><span class="label-title">Your current status</span></td>
            <td class="RightColumn">
                <%--<asp:TextBox id="txtCurrent_Status" runat="server" AptifyDataField="Current_Status" Width="100%" />--%>
                <asp:DropDownList ID="txtCurrent_Status" runat="server" AptifyDataField="Current_Status">
                    <asp:ListItem Text="---Select---" Value="Not answered" />
                    <asp:ListItem Text="Working" Value="Working" />
                    <asp:ListItem Text="Studying" Value="Studying" />
                    <asp:ListItem Text="Job Seeking" Value="Job Seeking" />
                    <asp:ListItem Text="Other" Value="Other" />
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="LeftColumn"><span class="label-title">How did you hear about us?</span></td>
            <td class="RightColumn">
                <%--<asp:TextBox id="txtHow_did_you_hear_about_us" runat="server" AptifyDataField="How_did_you_hear_about_us" Width="100%" />--%>
                <asp:DropDownList ID="txtHow_did_you_hear_about_us" runat="server" AptifyDataField="How_did_you_hear_about_us">
                    <asp:ListItem Text="---Select---" Value="Not answered" />
                    <asp:ListItem Text="Friend / Relative / Colleague" Value="Friend / Relative / Colleague" />
                    <asp:ListItem Text="Careers Officer" Value="Careers Officer" />
                    <asp:ListItem Text="Google Search" Value="Google Search" />
                    <asp:ListItem Text="Outdoor Advertisement" Value="Outdoor Advertisement" />
                    <asp:ListItem Text="Press Advertisement" Value="Press Advertisement" />
                    <asp:ListItem Text="Radio Advertisement" Value="Radio Advertisement" />
                    <asp:ListItem Text="Online Advertisement" Value="Online Advertisement" />
                    <asp:ListItem Text="Other" Value="Other" />
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:center">
                <asp:Button ID="cmdSave" runat="server" CssClass="submitBtn" OnClick="buttonSubmit_Click" Text="Submit form"  
                     CausesValidation="true" ValidationGroup="PSQuery"></asp:Button></td>
        </tr>
        <tr>
            <td class="LeftColumn"><span class="label-title">Use and protection of your personal information</span>
                The Institute will use the information which you have provided in this form to respond to your request or process your transaction and will hold and protect it 
                    in accordance with the Institute’s <a href="https://www.charteredaccountants.ie/Privacy-statement" target="_blank"><strong>privacy statement</strong></a>, which explains your rights in relation to your personal data.</td>
            <td class="RightColumn">               
                     </td>

        </tr>
    </table>
    <cc3:user id="AptifyEbusinessUser1" runat="server" />
</div>

<!--
<style>
    .submitBtn:disabled {
        background-color: #c0c0c0;
        color: #ffffff;
        cursor: not-allowed;
    }
</style>
    -->
