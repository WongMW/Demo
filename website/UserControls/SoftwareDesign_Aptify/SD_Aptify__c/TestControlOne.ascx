<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TestControlOne.ascx.cs" Inherits="SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c.TestControlOne" %>
<%@ register tagprefix="cc1" namespace="Aptify.Framework.Web.eBusiness" assembly="EBusinessLogin" %>

<asp:Button runat="server" ID="btnTest" OnClick="btnTest_OnClick"/>


<cc1:AptifyWebUserLogin runat="server" Visible="False" ID="webUserControl1"></cc1:AptifyWebUserLogin>