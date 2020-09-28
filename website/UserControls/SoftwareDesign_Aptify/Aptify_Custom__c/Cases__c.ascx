<%@ Control Language="vb" AutoEventWireup="false" Debug="true" Inherits="Aptify.Framework.Web.eBusiness.Generated.Cases__C"
    CodeFile="~/UserControls/Aptify_Custom__c/Cases__c.ascx.vb" %>
<%@ Register Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Custom__c/RecordAttachments__c.ascx" TagPrefix="uc2"
    TagName="RecordAttachments__c" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<style type="text/css">
    .style1
    {
        width: 338px;
    }
</style>
<div class="content-container clearfix">
    <table runat="server" id="tblMain" class="data-form" style="width: 550px">
        <tr>
            <td colspan="2">
                <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="False" />
            </td>
        </tr>
        <tr id="trUploadMsg" runat="server">
            <td colspan="2">
                <b>
                    <asp:Label ID="lblUploadMsg" runat="server" Text=""></asp:Label>
                </b>
            </td>
        </tr>
        <tr>
            <td>
            <b>Case Information</b><br />
                <asp:Panel ID="pnlCaseInfo" runat="Server" Style="border: 1px Solid #000000;" Width="530px">
                    <table runat="server" id="Table1" class="data-form" width="100%">
                        <tr id="trTitle" runat="server">
                            <td align="right">
                                Title :</td>
                            <td class="style1">
                                <asp:TextBox ID="txtTitle" runat="server" AptifyDataField="Title" Width="90%" />
                            </td>
                        </tr>
                        <tr id="trReadOnlyTitle" runat="server">
                            <td  align="right">
                                Title :</td>
                            <td class="style1">
                                <asp:Label ID="lblTitle" runat="server" AptifyDataField="Title"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trCategory" runat="server">
                            <td  align="right">
                                Category :</td>
                            <td class="style1">
                                <asp:DropDownList ID="cmbCaseCategoryID" runat="server" />
                            </td>
                        </tr>
                        <tr id="trReadOnlyCategory" runat="server">
                            <td  align="right">
                                Category :</td>
                            <td class="style1">
                                <asp:Label ID="lblCategory" runat="server" AptifyDataField="CaseCategory"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trType" runat="server">
                            <td  align="right">
                                Type :</td>
                            <td class="style1">
                                <asp:DropDownList ID="cmbCaseTypeID" runat="server" />
                            </td>
                        </tr>
                        <tr id="trReadOnlyType" runat="server">
                            <td  align="right">
                                Type :</td>
                            <td class="style1">
                                <asp:Label ID="lblType" runat="server" AptifyDataField="CaseType"></asp:Label>
                            </td>
                        </tr>
                     <%--   <tr id="trPriority" runat="server">
                            <td  align="right">
                                Priority :</td>
                            <td class="style1">
                                <asp:DropDownList ID="cmbCasePriorityID" runat="server" />
                            </td>
                        </tr>--%>
                        <%--<tr id="trReadOnlyPriority" runat="server">
                            <td  align="right">
                                Priority :</td>
                            <td class="style1">
                                <asp:Label ID="lblPriority" runat="server" AptifyDataField="CasePriority"></asp:Label>
                            </td>
                        </tr>--%>
                        <tr>
                            <td  align="right">
                                Status :</td>
                            <td class="style1">
                                <asp:Label ID="lblStatus" runat="server" AptifyDataField="CaseStatus"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trSummary" runat="server">
                            <td  align="right">
                                Summary :</td>
                            <td class="style1">
                                <asp:TextBox ID="txtSummary" runat="server" AptifyDataField="Summary" Width="90%"
                                    TextMode="MultiLine" Height="100px" />
                            </td>
                        </tr>
                        <tr id="trReadOnlySummary" runat="server">
                            <td  align="right">
                                Summary :</td>
                            <td class="style1">
                                <asp:Label ID="lblSummary" runat="server" AptifyDataField="Summary"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trReadOnlytrDateRecorded" runat="server">
                            <td  align="right">
                                Date Recorded :</td>
                            <td class="style1">
                                <asp:Label ID="lblDateRecorded" runat="server" AptifyDataField="DateRecorded"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td  align="right">
                                Contact :</td>
                            <td class="RightColumn">
                                <asp:DropDownList ID="cmbContactID" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="LeftColumn">
                                &nbsp;
                            </td>
                            <td class="RightColumn">
                                <asp:Button ID="cmdSave" runat="server" Text="Save Record" Style="height: 26px">
                                </asp:Button>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr id="trRecordAttachment" runat="server" visible="false">
            <td>
              <b>Documents</b><br />
                <asp:Panel ID="Panel1" runat="Server" Style="border: 1px Solid #000000;">
                    <table runat="server" id="Table2" class="data-form" width="100%">
                        <tr >
                            <td class="LeftColumn">
                                &nbsp;
                            </td>
                            <td class="RightColumn">
                                <uc2:RecordAttachments__c ID="RecordAttachments__c" runat="server" AllowView="True"
                                    AllowAdd="True" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td  align="Right">
            <asp:Button ID="cmdBack" runat="server" Text="Back"></asp:Button>
            </td>
            <td>
                
            </td>
        </tr>
    </table>
    <cc3:User ID="AptifyEbusinessUser1" runat="server" />
    <cc1:User ID="User1" runat="server"></cc1:User>
</div>
