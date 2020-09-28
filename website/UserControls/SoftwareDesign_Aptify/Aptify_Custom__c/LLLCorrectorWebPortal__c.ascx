<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/LLLCorrectorWebPortal__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.LLLCorrectorWebPortal__c" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register Src="~/UserControls/Aptify_Custom__c/RecordAttachments__c.ascx" TagPrefix="ucRecordAttachment"
    TagName="RecordAttachments__c" %>
<div class="dvUpdateProgress" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing" style="height: 700px;">
                <table class="tblFullHeightWidth">
                    <tr>
                        <td class="tdProcessing" style="vertical-align: middle">
                            Please wait...
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<div class="info-data" style="height: 450px;">
    <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
        <ContentTemplate>--%>
    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
    <div class="row-div clearfix">
        <div class="label-div w30">
            &nbsp;
        </div>
        <div class="field-div1 w100">
            <div class="info-data" id="divIDClassDetails" runat="server">
                <div class="row-div clearfix">
                    <div class="label-div w30">
                        &nbsp;
                    </div>
                    <div class="label-div w100">
                        <h2 runat="server" id="hearderscript" align="center" visible="false">
                            Script Marking Details</h2>
                    </div>
                    <div class="label-div w30">
                        &nbsp;
                    </div>
                    <div class="label-div w100" style="text-align: left;">
                        <asp:Label ID="lblmsg" runat="server" Visible="false" Text="There is no current assignment assigned to the corrector."></asp:Label>
                    </div>
                    <div class="field-div1 w100" style="text-align: left;">
                        <telerik:RadGrid ID="radgrdScriptDetails" runat="server" AutoGenerateColumns="False"
                            AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                            AllowFilteringByColumn="true" AllowSorting="true" PageSize="3" Visible="false">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="Class" HeaderText="Class" SortExpression="Class"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn DataField="CoursePart" HeaderText="Course Part" SortExpression="CoursePart"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn DataField="Status" HeaderText="Status" SortExpression="Status"
                                        AllowFiltering="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Download and Upload"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        FilterControlWidth="100%" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkFileAttachment" runat="server" ForeColor="Blue" CommandName="Attachment"
                                                Text="Download/Upload Script Marking Spreadsheets" Font-Underline="true" CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                </div>
            </div>
        </div>
        <div class="label-div w30">
            &nbsp;
        </div>
        <telerik:RadWindow ID="radDownloadDocuments" runat="server" Width="500px" Height="600px"
            Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
            Title="Download/Upload Script Marking Spreadsheet" Behavior="None">
            <ContentTemplate>
                <div>
                    <table width="100%">
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td width="5%">
                            </td>
                            <td width="90%">
                                <b>Download Documents</b><br />
                                <asp:Panel ID="pnlDownloadDocuments" runat="Server" Style="border: 1px Solid #000000;">
                                    <table class="data-form" width="100%">
                                        <tr>
                                            <td class="RightColumn">
                                                <ucRecordAttachment:RecordAttachments__c ID="ucDownload" runat="server" AllowView="True"
                                                    AllowAdd="false" AllowDelete="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td width="5%">
                            </td>
                        </tr>
                        <tr>
                            <td width="5%">
                            </td>
                            <td width="90%">
                                <b>Upload Documents</b><br />
                                <asp:Panel ID="Panel1" runat="Server" Style="border: 1px Solid #000000;">
                                    <table class="data-form" width="100%">
                                        <tr>
                                            <td class="RightColumn">
                                                <ucRecordAttachment:RecordAttachments__c ID="ucUpload" runat="server" AllowView="True"
                                                    AllowAdd="true" AllowDelete="true" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td width="5%">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="right">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="submitBtn" />
                                <asp:Button ID="btnClose" runat="server" Text="Cancel" CssClass="submitBtn" />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
    </div>
    <cc3:User ID="User1" runat="server" />
    <cc2:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="False"></cc2:AptifyShoppingCart>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</div>
