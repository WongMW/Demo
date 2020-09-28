<%--Aptify e-Business 5.5.1, July 2013--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/RecordAttachments__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.RecordAttachments" Debug="true" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<cc2:User runat="server" ID="User1" />
<style>
    .RadButton_Sunset.rbSkinnedButton, .RadButton_Sunset .rbDecorated, .RadButton_Sunset.rbVerticalButton, .RadButton_Sunset.rbVerticalButton .rbDecorated, .RadButton_Sunset .rbSplitRight, .RadButton_Sunset .rbSplitLeft {
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
    <asp:Label ID="lblMessage" runat="server"></asp:Label>
</div>
<div runat="server" id="trGrid">
    <asp:UpdatePanel ID="UppanelGrid" runat="server">
        <ContentTemplate>
            <rad:RadGrid ID="grdAttachments" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                AllowSorting="False" AllowFilteringByColumn="false" ShowHeadersWhenNoRecords="true" CssClass="cai-table mobile-table">
                <PagerStyle CssClass="sd-pager" />
                <MasterTableView>
                    <Columns>
                        <rad:GridTemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn>
                            <ItemTemplate>
                                <asp:HyperLink ID="lblFileImage" runat="server" NavigateUrl='<%#  Bind("EncryptedURL")%>'></asp:HyperLink>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn HeaderText="File name">
                            <ItemTemplate>
                                <span class="mobile-label">File name:</span>
                                <asp:LinkButton CssClass="cai-table-data" ID="lbtnFile" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("Name")&";"&Eval("FileSize") %>'
                                    CommandName="Download" runat="server"></asp:LinkButton>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn DataField="Description" AllowSorting="false" ItemStyle-Wrap="true"
                            HeaderText="Description">
                            <ItemTemplate>
                                <span class="mobile-label">Description:</span>
                                <asp:Label CssClass="cai-table-data" Text='<%# Eval("Description")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn DataField="DateUpdated" AllowSorting="false" HeaderText="Updated on">
                            <ItemTemplate>
                                <span class="mobile-label">Updated on:</span>
                                <asp:Label CssClass="cai-table-data" Text='<%# Eval("DateUpdated", "{0:d}")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn DataField="FileSize" AllowSorting="false" HeaderText="Size">
                            <ItemTemplate>
                                <span class="mobile-label">Size:</span>
                                <asp:Label CssClass="cai-table-data" Text='<%# Eval("FileSize", "{0:N0} KB")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                        <rad:GridTemplateColumn HeaderText="Delete">
                            <ItemTemplate>
                                <rad:RadButton CssClass="submitBtn" ID="btn" runat="server"
                                    Text="Delete" CommandName="Delete" CommandArgument='<%# CType(Container, GridDataItem).ItemIndex %>'>
                                </rad:RadButton>
                            </ItemTemplate>
                        </rad:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </rad:RadGrid>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="grdAttachments" />
        </Triggers>
    </asp:UpdatePanel>
</div>

<div runat="server" id="trAdd" visible="false">
    <asp:Panel ID="pnlUpload" runat="Server">
        <div class="field-group">
            <span class="label">To upload a file, fill in the information shown below</span>
        </div>
        <div class="field-group">
            <span class="label-title">File:</span>
            <asp:FileUpload ID="FileUpload1" runat="server" />
        </div>
        <div class="field-group description">
            <span class="label-title">Description : </span>
            <asp:TextBox ID="txtDescription" CssClass="description-input" runat="server" />
        </div>
        <div class="actions field-group">
            <asp:Button CssClass="submitBtn" runat="server" ID="btnAdd" Text="Upload" />
            <asp:Button CssClass="submitBtn" runat="server" ID="btnCancel" Text="Cancel" Visible="false" />
        </div>
    </asp:Panel>
</div>
