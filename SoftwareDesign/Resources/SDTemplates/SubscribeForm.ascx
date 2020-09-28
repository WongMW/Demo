<%@ Control Language="C#" %>
<%@ Register TagPrefix="sitefinity" Namespace="Telerik.Sitefinity.Web.UI" Assembly="Telerik.Sitefinity" %>

<asp:Panel ID="errorsPanel" runat="server" CssClass="sfErrorSummary" Visible="false" />
<fieldset id="formFieldset" runat="server" class="sfnewsletterForm sfSubscribe">
    <sitefinity:SitefinityLabel ID="widgetTitle" runat="server" WrapperTagName="h2" HideIfNoText="true" CssClass="sfnewsletterTitle" />
    <sitefinity:SitefinityLabel ID="widgetDescription" runat="server" WrapperTagName="p" HideIfNoText="true" CssClass="sfnewsletterDescription" />
    <sitefinity:Message ID="messageControl" runat="server" FadeDuration="3000" />
    <ol class="sfnewsletterFieldsList">
        <li class="sfnewsletterField">
            <asp:TextBox ID="firstName" runat="server" CssClass="sfTxt name" placeholder="Your Name" /></li>
        <li class="sfnewsletterField hidden">
            <asp:TextBox ID="lastName" runat="server" CssClass="sfTxt name" placeholder="Last Name" /></li>
        <li class="sfnewsletterField">
            <asp:TextBox ID="emailAddress" runat="server" CssClass="sfTxt email" placeholder="Your Email" /></li>
        <li class="sfnewsletterField">
            <asp:Button ID="subscribeButton" runat="server" Text='Sign Up' ValidationGroup="subscribeForm" CssClass="sfnewsletterSubmitBtn" /></li>
    </ol>
    <asp:RequiredFieldValidator ID="emailValidator" runat="server" ControlToValidate="emailAddress" ValidationGroup="subscribeForm" CssClass="sfErrorWrp" Display="Dynamic">
        <strong class="sfError">
            <asp:Literal runat="server" ID="lEmailIsRequired" Text='Please enter a valid email address' /></strong>
    </asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator
        ID="emailRegExp"
        runat="server"
        ControlToValidate="emailAddress"
        ValidationGroup="subscribeForm"
        ValidationExpression="[a-zA-Z0-9._%+-]+@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,4}"
        Display="Dynamic"
        CssClass="sfErrorWrp"
        ErrorMessage="<%$ Resources:ErrorMessages, EmailAddressViolationMessage %>">
        <strong class="sfError">
            <asp:Literal ID="lEmailNotValid" runat="server" Text="<%$ Resources:ErrorMessages, EmailAddressViolationMessage %>" /></strong>
    </asp:RegularExpressionValidator>
</fieldset>

<asp:Panel ID="selectListInstructionPanel" runat="server">
    <asp:Literal ID="pleaseSelectList" runat="server" Text='<%$Resources:NewslettersResources, ClickEditAndSelectList %>' />
</asp:Panel>

<script type="text/javascript">
    $('input.sfnewsletterSubmitBtn').on('click', function () {
        var val = Page_ClientValidate();
        if (!val) {
            var i = 0;
            for (; i < Page_Validators.length; i++) {
                if (!Page_Validators[i].isvalid) {
                    $("#" + Page_Validators[i].controltovalidate)
                     .css("border", "1px solid #bd1622");
                }
            }
        }
        return val;
    });
</script>
