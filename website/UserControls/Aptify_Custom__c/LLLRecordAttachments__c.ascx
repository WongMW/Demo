<%--Aptify e-Business 5.5.1, July 2013--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="LLLRecordAttachments__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.LLLRecordAttachments__c" Debug="true" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<cc2:User runat="server" ID="User1" />
<style>
    .RadButton_Sunset.rbSkinnedButton, .RadButton_Sunset .rbDecorated, .RadButton_Sunset.rbVerticalButton, .RadButton_Sunset.rbVerticalButton .rbDecorated, .RadButton_Sunset .rbSplitRight, .RadButton_Sunset .rbSplitLeft
    {
        background-image: none !important;
    }
</style>
<asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>
<script type="text/javascript">
    function _do_window_open(url) {
        window.open(url, '_aptify_attachment_content', 'toolbar=no,menubar=yes,location=no,directories=no,status=no,resizable=yes,scrollbars=yes');
    }
</script>
<div>
    <asp:Label ID="lblMessage" Style="font-weight: bold;" runat="server"></asp:Label>
</div>
<table>
    <tr runat="server" id="trAdd" visible="false">
        <td>
            <asp:Panel ID="pnlUpload" runat="Server">
                <%--Style="border: 1px Solid #000000;"--%>
                <%--<b>To upload a file, fill in the information shown below</b>--%>
                <table width="100%" class="data-form">
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <b>Upload :</b> &nbsp;<asp:FileUpload ID="FileUpload1" runat="server" />
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td align="right">
                            Description :
                        </td>
                        <td class="RightColumn">
                            <asp:TextBox ID="txtDescription" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:CheckBox ID="chkCheck" TextAlign="Right" Text="I attest that the uploaded documents are authentic and non plagiarized"
                                runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button CssClass="submitBtn" runat="server" ID="btnAdd" Text="Upload" />
                            <asp:Button CssClass="submitBtn" runat="server" ID="btnCancel" Text="Cancel" Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td>
            <asp:UpdatePanel ID="UppanelGrid" runat="server">
                <ContentTemplate>
                    <rad:RadGrid ID="grdAttachments" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                        AllowSorting="False" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true"
                        Width="100%">
                        <MasterTableView>
                            <Columns>
                                <rad:GridTemplateColumn HeaderText="1" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="2" Visible="false">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lblFileImage" runat="server" NavigateUrl='<%#  Bind("EncryptedURL")%>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" />
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Name" HeaderStyle-Width="45%" ItemStyle-Width="45%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnFile" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("Name")&";"&Eval("FileSize") %>'
                                            CommandName="Download" runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" />
                                </rad:GridTemplateColumn>
                                <rad:GridBoundColumn DataField="Description" Visible="false" AllowSorting="false"
                                    ItemStyle-Wrap="true" HeaderText="Description" HeaderStyle-Width="45%" ItemStyle-Width="45%" />
                                <rad:GridBoundColumn DataField="DateUpdated" Visible="false" AllowSorting="false"
                                    HeaderText="Updated On" DataFormatString="{0:d}" HeaderStyle-Width="20%" ItemStyle-Width="20%" />
                                <rad:GridBoundColumn DataField="FileSize" Visible="false" AllowSorting="false" HeaderText="Size"
                                    HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0} KB"
                                    HeaderStyle-Width="20%" ItemStyle-Width="20%" />
                                <rad:GridTemplateColumn ItemStyle-CssClass="grdAttachRecordAlign" HeaderStyle-Width="20%"
                                    HeaderText="Delete">
                                    <ItemTemplate>
                                        <%--  <rad:RadButton CssClass="submitBtn" ID="btn" runat="server" Text="Delete" CommandName="Delete"
                                            CommandArgument='<%# CType(Container, GridDataItem).ItemIndex %>'>
                                        </rad:RadButton>--%>
                                        <asp:Button CssClass="submitBtn" runat="server" ID="btn" CommandName="Delete" CommandArgument='<%# CType(Container, GridDataItem).ItemIndex %>'
                                            Text="Delete" />
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Download" HeaderStyle-Width="45%" ItemStyle-Width="45%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnDownload" Text="Download" CommandArgument='<%# Eval("Name")&";"&Eval("FileSize") %>'
                                            CommandName="DownloadFile" runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" />
                                </rad:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </rad:RadGrid>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="grdAttachments" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
