<%@ Control Language="VB" AutoEventWireup="true" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.GDPR.UserTopicPreferences"
    CodeFile="UserTopicPreferences.ascx.vb" %>
<%@ Register TagPrefix="topics" TagName="TopicCodeControl"
    Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Customer_Service/TopicCodeControl.ascx" %>

<p id="txtUserTopicPreferences" runat="server" style="margin-top: 20px;">
  From time to time we will use this information to communicate with you about an upcoming events, publications or activities which are relevant to you and tailored to your personal profile. 
</p>

<%--TODO: Move to common stylesheet--%>
<style type="text/css" media="all">
    #trvTopicCodes ul {
        float: none;
    }

    /*.user-preferences {
        margin-left: 4%;
    }*/

    .user-preferences input[type=submit] {
        width: 100%;
        font-size: 16px;
        height: 52px;
    }
    
</style>
<div class="user-preferences">
    <topics:TopicCodeControl runat="server" ID="TopicCodeControl1" ClientIDMode="AutoID" CheckChildNodes="True" />
</div>
