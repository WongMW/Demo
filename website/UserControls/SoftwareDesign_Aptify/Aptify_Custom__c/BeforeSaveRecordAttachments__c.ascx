<%--Aptify e-Business 5.5.1, July 2013--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/BeforeSaveRecordAttachments__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.BeforeSaveRecordAttachments__c" Debug="true" %>
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
<table width="80%">
    <tr runat="server" id="trAdd" visible="false">
        <td>
            <table>
               
                <tr>
                    <td>
                        File
                    </td>
                    <td>
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                        <asp:Label ID="lblFileExist" runat="server" Text="" ForeColor="Red" Visible="False"></asp:Label>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please upload file"
                            ControlToValidate="FileUpload1"></asp:RequiredFieldValidator>--%>
                        <asp:CustomValidator ID="cvUpload" runat="server" ControlToValidate="FileUpload1"
                            Display="Dynamic" Text="*" ValidateEmptyText="true" ErrorMessage="File Required"
                            OnServerValidate="FileUpload1_Validate"></asp:CustomValidator>
                    </td>
                </tr>
                <%--<tr>
                    <td>
                        Description
                    </td>
                    <td>
                        <asp:TextBox ID="txtDescription" runat="server"/>
                    </td>
                </tr>--%>
                <tr>
                    <td colspan="2">
                        <asp:Button CssClass="submitBtn" runat="server" ID="btnAdd" Text="Upload" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr runat="server" id="trGrid">
        <td>
            <%-- Navin Prasad Issue 11032--%>
            <%--Nalini Issue 12436 date:01/12/2011--%>
            <%-- Navin Prasad Issue 12865 --%>
            <asp:UpdatePanel ID="UppanelGrid" runat="server">
                <contenttemplate>
                    <asp:GridView ID="grdAttachments" runat="server" AutoGenerateColumns="false" Width="340px">
                        <Columns>
                            <asp:TemplateField HeaderText="SNo" >
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                  <%--   <%# Container.DataItemIndex + 1%>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:ButtonField CommandName="Delete" ButtonType="Link" ControlStyle-Width="40px" HeaderText="Delete" />
                          <%--  <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HyperLink ID="lblFileImage" runat="server" NavigateUrl='<%#  Bind("EncryptedURL")%>'></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" />
                            </asp:TemplateField>--%>
                            <%--<asp:TemplateField HeaderText="File" ControlStyle-Width="250px">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lblFile" runat="server" Text='<%# Eval("Name") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle Wrap="true" />--%>
                                
                          <%--  </asp:TemplateField>--%>
                          <asp:BoundField DataField="Name" ItemStyle-Wrap="true" HeaderText="File" />
                            <asp:BoundField DataField="Description" ItemStyle-Wrap="true" HeaderText="Description" visible="False" />
                            <asp:BoundField DataField="DateUpdated" HeaderText="Uploaded On" DataFormatString="{0:d}"  ControlStyle-Width="50px" />
                            <%--<asp:BoundField DataField="FileSize" HeaderText="Size" HeaderStyle-HorizontalAlign="right"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N4} KB" />--%>
                        </Columns>
                        <FooterStyle />
                        <EditRowStyle />
                        <SelectedRowStyle />
                        <PagerStyle />
                        <AlternatingRowStyle />
                        <RowStyle />
                        <HeaderStyle />
                    </asp:GridView>
                </contenttemplate>
                <triggers>
                    <%--<asp:AsyncPostBackTrigger ControlID="grdAttachments" EventName="PageIndexChanging" />--%>
                    <asp:PostBackTrigger ControlID="grdAttachments" />
                </triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <%-- <tr id="trNewGrid" runat="server">
        <td>
            <asp:GridView ID="grdAttachData" runat="server" AutoGenerateColumns="false" AutoGenerateDeleteButton="True"
                EnableModelValidation="True" Width="99%">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:BoundField DataField="Date" HeaderText="Date" />
                    <asp:BoundField DataField="File" HeaderText="File" visible="false"/>
                </Columns>
            </asp:GridView>
        </td>
    </tr>--%>
</table>
