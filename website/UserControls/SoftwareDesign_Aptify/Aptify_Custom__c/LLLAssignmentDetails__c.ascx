<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/LLLAssignmentDetails__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.LLLAssignmentDetails__c" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register Src="~/UserControls/Aptify_Custom__c/LLLRecordAttachments__c.ascx" TagPrefix="ucLLLRecordAttachment"
    TagName="LLLRecordAttachments__c" %>
<%@ Register Src="~/UserControls/Aptify_Custom__c/LLLRecordAttachments__c.ascx" TagPrefix="ucRecordAttachment"
    TagName="LLLRecordAttachments__c" %>
<div class="info-data cai-form">
    <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>--%>
    <div class="form-title">Assignment Details</div>
    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
    <div class="row-div clearfix">
        <div>
            <asp:Label ID="lblAssignmentMessage" runat="server"></asp:Label>
        </div>
        <div class="info-data">
            <div class="row-div clearfix cai-form-content">
                <div class="field-div1 w99">
                    <div class="info-data">
                        <div class="row-div clearfix">
                            <div class="label-align-left-div w50">
                                <asp:Label ID="lblbAssignmentDueDate" runat="server" Text="Assignment Due Date:"
                                    Font-Bold="true"></asp:Label>
                                <asp:Label ID="lblbAssignmentDueDateValue" runat="server" Text="[Date]" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="label-align-left-div w50">
                                <asp:Label ID="lblDaysRemaining" runat="server" Text="Days Remaining To Submission:"
                                    Font-Bold="true"></asp:Label>
                                <asp:Label ID="lblDaysRemainingValue" runat="server" Text="[Days Count]" Font-Bold="true"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="info-data">
                        <div class="row-div clearfix">
                            <div class="label-align-center-div w50">
                                <br />
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div class="label-align-left-div w50">
                                <asp:Label ID="lbldlAssignments" runat="server" Text="Download Assignments Question:"
                                    Font-Bold="true"></asp:Label>
                            </div>
                            <div class="label-align-left-div w90">
                                <%--<ucRecordAttachment:RecordAttachments__c ID="ucAssignmentDownload" runat="server"
                                    AllowView="True" AllowAdd="false" ViewDescription="false" AllowDelete="False" />--%>
                                <ucRecordAttachment:LLLRecordAttachments__c ID="ucAssignmentDownload" runat="server"
                                    AllowView="True" AllowAdd="false" ViewDescription="false" ShowDownload="true"
                                    AllowDelete="false" />
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div class="label-align-left-div
w30">
                                <br />
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div class="label-align-left-div
w50">
                                <asp:Label ID="lblUploadAssignments" runat="server" Text="Upload Assignment Answer:"
                                    Font-Bold="true"></asp:Label>
                            </div>
                            <div class="label-align-left-div w90">
                                <ucLLLRecordAttachment:LLLRecordAttachments__c ID="ucLLLAssignmentUpload" runat="server"
                                    AllowView="True" AllowAdd="True" ViewDescription="false" ShowDownload="false"
                                    AllowDelete="True" />
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div class="label-div w10">
                            </div>
                            <div class="field-div1 w90" align="right">
                                <asp:Button ID="btnCloseAssignment" Text="Back" runat="server" CssClass="submitBtn" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <cc3:User ID="User1" runat="server" />
</div>
