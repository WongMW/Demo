<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ForgotUID.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.ForgotUIDControl"  %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="WebUserActivity" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessLogin" %>

<div class="content-container clearfix">
    <table id="tblMain" runat="server" class="data-form">
    <tr>
    <td>
    If you have forgotten your 
        password, please enter your user ID and click submit.&nbsp; You will then be 
        presented with&nbsp;password hint question corresponding to your user ID. 
        &nbsp;If you are able to answer the question correctly, you will be permitted 
        to change your password online and proceed with logging into the web 
        site.&nbsp; If you have forgotten your user ID, please contact 
            <asp:HyperLink ID="mailAddress" runat="server"></asp:HyperLink> for assistance.&nbsp;

        <hr /> <%--Neha issue Id:15163 Add CssClass for ErrorMessage--%>
        <asp:label id="lblError" CssClass="lblForgotUID" runat="server" ></asp:label>
        <table id="tblUserName" runat="server">
        <tr>
          <td >
            <asp:label id="Label1" runat="server" >User ID:</asp:label>
          </td>
          <td><asp:textbox id="txtUID" runat="server"  />
              <asp:requiredfieldvalidator id="valUID" runat="server" CssClass="lblForgotUID " ControlToValidate="txtUID" ErrorMessage=" Required Field" />
          </td>
        </tr>
        </table>
        <table id="tblPWDHint" runat="server">
        <tr>
          <td ><asp:label id="Label2" runat="server" >Password Hint:</asp:label></td>
          <td><asp:label id="lblHint" runat="server" ></asp:label></td>
        </tr>
        <tr>
          <td ><asp:label id="Label3" runat="server" >Answer:</asp:label></td>
          <td><asp:textbox id="txtAnswer" runat="server" ></asp:textbox><asp:requiredfieldvalidator id="valAnswer" runat="server"  ControlToValidate="txtAnswer" CssClass="lblForgotUID" ErrorMessage=" Required Field" ></asp:requiredfieldvalidator></td>
        </tr>
        </table>
        <table id="tblPasswordChange" runat="server">
        <tr>
          <td ><asp:label id="Label6" runat="server" >User ID:</asp:label></td>
          <td><asp:label id="lblUID" runat="server" ></asp:label></td>
        </tr>
        <tr>
          <td ><asp:label id="Label4" runat="server">Password:</asp:label></td>
          <td><asp:textbox id="txtPWD" runat="server"  TextMode="Password" ></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtPWD" CssClass="lblForgotUID" ErrorMessage=" Required Field" ></asp:requiredfieldvalidator></td>
        </tr>
        <tr><td><asp:label id="Label5" runat="server" >Repeat Password:</asp:label></td>
          <td><asp:textbox id="txtRepeatPWD" runat="server"  TextMode="Password" ></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server"  ControlToValidate="txtRepeatPWD" CssClass="lblForgotUID" ErrorMessage=" Required Field" ></asp:requiredfieldvalidator></td>
        </tr>
        </table>
    </td>
    </tr>
    <tr>
    <td>
        <p><asp:button id="cmdSubmit" runat="server" Text="Submit" CssClass="submitBtn"  /></p>
    </td>
    </tr>
    </table>
    <cc2:webuseractivity id="WebUserActivity1" runat="server" />
    <cc1:AptifyWebUserLogin id="WebUserLogin1" runat="server" /> 
</div>