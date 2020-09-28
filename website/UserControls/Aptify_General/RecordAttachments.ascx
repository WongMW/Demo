<%@ Control Language="VB" AutoEventWireup="false" CodeFile="RecordAttachments.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.RecordAttachments" Debug="true" %>
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
<table>
    <tr runat="server" id="trGrid">
        <td>
            <%-- Navin Prasad Issue 11032--%>
            <%--Nalini Issue 12436 date:01/12/2011--%>
            <%-- Navin Prasad Issue 12865 --%>
            <asp:UpdatePanel ID="UppanelGrid" runat="server">
                <ContentTemplate>
               <%-- 'Anil B for issues 144499 on 05-04-2013
                        Remove Sorting--%>
                    <rad:RadGrid ID="grdAttachments" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                        AllowSorting="False" AllowFilteringByColumn="false">
                        <MasterTableView>
                            <Columns>
                                <rad:GridTemplateColumn Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <%--  <rad:GridButtonColumn CommandName="Delete" ButtonType="PushButton" />--%>
                                <rad:GridTemplateColumn>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lblFileImage" runat="server" NavigateUrl='<%#  Bind("EncryptedURL")%>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" />
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="File">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lblFile" runat="server" Text='<%# Eval("Name") %>' NavigateUrl='<%#  Bind("EncryptedURL")%>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" />
                                </rad:GridTemplateColumn>
                                <rad:GridBoundColumn DataField="Description" AllowSorting="false" ItemStyle-Wrap="true"
                                    HeaderText="Description" />
                                <rad:GridBoundColumn DataField="DateUpdated" AllowSorting="false" HeaderText="Updated On"
                                    DataFormatString="{0:d}" />
                                <rad:GridBoundColumn DataField="FileSize" AllowSorting="false" HeaderText="Size"
                                    HeaderStyle-HorizontalAlign="right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0} KB" />
                                <rad:GridTemplateColumn ItemStyle-CssClass="grdAttachRecordAlign">
                                    <ItemTemplate>
                                        <rad:RadButton CssClass="submitBtn grdAttachRecordAlign" ID="btn" runat="server"
                                            Text="Delete" CommandName="Delete" CommandArgument='<%# CType(Container, GridDataItem).ItemIndex %>'>
                                        </rad:RadButton>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </rad:RadGrid>
                    <%--  <asp:GridView ID="grdAttachments" runat="server" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:ButtonField CommandName="Delete" ButtonType="Link" CausesValidation="false" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HyperLink ID="lblFileImage" runat="server" NavigateUrl='<%#  Bind("EncryptedURL")%>'></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="File">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lblFile" runat="server" Text='<%# Eval("Name") %>' NavigateUrl='<%#  Bind("EncryptedURL")%>'></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Description" ItemStyle-Wrap="true" HeaderText="Description" />
                            <asp:BoundField DataField="DateUpdated" HeaderText="Updated On" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="FileSize" HeaderText="Size" HeaderStyle-HorizontalAlign="right"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N4} KB" />
                        </Columns>
                        <FooterStyle />
                        <EditRowStyle />
                        <SelectedRowStyle />
                        <PagerStyle />
                        <AlternatingRowStyle />
                        <RowStyle />
                        <HeaderStyle />
                    </asp:GridView>--%>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="grdAttachments" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr runat="server" id="trAdd" visible="false">
        <td>
            To upload a file, fill in the information shown below
            <table>
                <tr>
                    <td>
                        File
                    </td>
                    <td>
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Description
                    </td>
                    <td>
                        <asp:TextBox ID="txtDescription" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button CssClass="submitBtn" runat="server" ID="btnAdd" Text="Upload" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
