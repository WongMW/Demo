<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/GAAddMember__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CustomerService.GAAddMember__c" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<cc2:User ID="User1" runat="server" />
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<script language="javascript" type="text/javascript">
    function Hidelable() {
        window.document.getElementById('<%= lblMessage.ClientID %>').style.display = "none"

    }
</script>
<div class="dvUpdateProgress" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing" style="height: 1200px;">
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

<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
        <ContentTemplate>
<table width="100%">
    <tr>
        <td align="right" colspan="7">
            <asp:Label ID="lblNote" runat="server" ForeColor="Red" Text="Fields marked with * are mandatory."
                Style="text-align: right"></asp:Label>
        </td>
    </tr>
</table>
<asp:DropDownList ID="cmbSubsidarisCompney" runat="server" Width="200px" AutoPostBack="true">
</asp:DropDownList> <br />
<asp:GridView ID="grdAddMember" AutoGenerateColumns="false" runat="server" ShowFooter="false"
    Width="100%" AllowPaging="false">
    <Columns>
        <asp:BoundField DataField="RowNumber" HeaderText="Row Number" Visible="false" />
        <asp:TemplateField>
            <HeaderStyle CssClass="GridViewHeaderStyle" HorizontalAlign="Left" Width="15%" />
            <ItemStyle HorizontalAlign="Left" CssClass="GAAddMembertdalign" />
            <HeaderTemplate>
                <asp:Label ID="lblFname" Text="First Name" runat="server"></asp:Label>
                <asp:Label ID="lblFnameAsterisk" Text="*" CssClass="addNewPeapleMandatoryFields"
                    runat="server"></asp:Label>
            </HeaderTemplate>
            <ItemTemplate>
                <asp:TextBox ID="txtFName" runat="server" CssClass="TextboxStyle"></asp:TextBox>
            </ItemTemplate>
            <%--<FooterStyle CssClass="GridFooter" />
            <FooterTemplate>
                <asp:LinkButton ID="lnkAddRow" runat="server" Text="Add Row" OnClick="lnkAddRow_Click" ForeColor="#d07b0c"></asp:LinkButton>
            </FooterTemplate>--%>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderStyle CssClass="GridViewHeaderStyle" Width="15%" />
            <HeaderTemplate>
                <asp:Label ID="lblLname" Text="Last Name" runat="server"></asp:Label>
                <asp:Label ID="lblLnameAsterisk" Text="*" CssClass="addNewPeapleMandatoryFields"
                    runat="server"></asp:Label>
            </HeaderTemplate>
            <ItemStyle HorizontalAlign="left" CssClass="GAAddMembertdalign" />
            <ItemTemplate>
                <asp:TextBox ID="txtLName" runat="server" CssClass="TextboxStyle"></asp:TextBox>
            </ItemTemplate>
            <%--<FooterStyle CssClass="GridFooter" />
            <FooterTemplate>
                 <asp:Label ID="lblAddMultiple" runat="server" Text="Add Multiple Rows:" ForeColor="Black"></asp:Label>
                <asp:DropDownList ID="drpRows" runat="server">
                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                </asp:DropDownList>
            </FooterTemplate>--%>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderStyle CssClass="GridViewHeaderStyle" Width="15%" />
            <HeaderTemplate>
                <asp:Label ID="lblTitle" Text="Title" runat="server"></asp:Label>
            </HeaderTemplate>
            <ItemStyle HorizontalAlign="Left" CssClass="GAAddMembertdalign" />
            <ItemTemplate>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="TextboxStyle"></asp:TextBox>
            </ItemTemplate>
            <%--<FooterStyle CssClass="GridFooter" />
            <FooterTemplate>
                 <asp:Button ID="btnInsertRows" runat="server" Text="Insert" OnClick="btnInsertRows_Click" />
            </FooterTemplate>--%>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderStyle CssClass="GridViewHeaderStyle" Width="15%" />
            <HeaderTemplate>
                <asp:Label ID="lblEmail" Text="Email" runat="server"></asp:Label>
                <asp:Label ID="lblEmailAsterisk" Text="*" CssClass="addNewPeapleMandatoryFields"
                    runat="server"></asp:Label>
            </HeaderTemplate>
            <ItemStyle HorizontalAlign="Left" CssClass="GAAddMembertdalign" />
            <ItemTemplate>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="TextboxStyle"></asp:TextBox>
            </ItemTemplate>
            <FooterStyle CssClass="GridFooter" />
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderStyle CssClass="GridViewHeaderStyle" Width="20%" />
            <HeaderTemplate>
                <asp:Label ID="lblCompany" Text="Company" runat="server"></asp:Label>
            </HeaderTemplate>
            <ItemStyle HorizontalAlign="Left" CssClass="GAAddMembertdalign" />
            <ItemTemplate>
                <asp:Label ID="lblGACompany" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Company")%>'></asp:Label>
            </ItemTemplate>
            <%--<FooterStyle CssClass="GridFooter" />
            <FooterTemplate>
                <asp:Label ID="lblResult" runat="server" Text='' ForeColor="Red"></asp:Label>
            </FooterTemplate>--%>
        </asp:TemplateField>
        <asp:TemplateField Visible="false">
            <HeaderStyle CssClass="GridViewHeaderStyle" Width="12%" />
            <HeaderTemplate>
                <asp:Label ID="lblCreateWebUser" Text="Create Web User?" runat="server"></asp:Label>
            </HeaderTemplate>
            <ItemStyle HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:CheckBox ID="chkCreateWebUser" runat="server" Checked="true"></asp:CheckBox>
            </ItemTemplate>
            <%-- <FooterStyle CssClass="GridFooter" HorizontalAlign="Right" />
            <FooterTemplate>
                <asp:Button ID="btnDeleteAll" runat="server" Text="Delete All" OnClick="btnDeleteAll_Click" />
            </FooterTemplate>--%>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderStyle CssClass="GridViewHeaderStyle" Width="8%" HorizontalAlign="Center" />
            <HeaderTemplate>
                <asp:Label ID="lblDelete" Text="Delete" runat="server"></asp:Label>
            </HeaderTemplate>
            <ItemStyle CssClass="GAAddMembertdImagealign" />
            <ItemTemplate>
                <asp:ImageButton ID="btndelete" runat="server" ImageUrl="~/Images/Delete.png" CommandName="Delete"
                    CommandArgument='<%#Eval("RowNumber") %>' />
            </ItemTemplate>
            <%--<FooterStyle CssClass="GridFooter" />
            <FooterTemplate>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
            </FooterTemplate>--%>
        </asp:TemplateField>
    </Columns>
    <RowStyle Height="24px" />
</asp:GridView>
<div class="GAFooterDivStyle">
    <table style="width: 100%;">
        <tr>
            <td style="width: 20%;">
                <%--<asp:LinkButton ID="lnkAddRow" runat="server" Text="Add Row" OnClick="lnkAddRow_Click" ForeColor="#d07b0c"></asp:LinkButton>--%>
                <asp:Label ID="lblAddMultiple" runat="server" Text="Add Rows:" ForeColor="Black"></asp:Label>
                <asp:DropDownList ID="drpRows" runat="server">
                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnInsertRows" runat="server" Text="Insert" OnClick="btnInsertRows_Click"
                    CssClass="submitBtn" />
            </td>
            <td style="width: 55%; text-align: right;">
                <asp:Label ID="lblResult" runat="server" Text='' ForeColor="Red"></asp:Label>
            </td>
            <td style="text-align: right; width: 25%;">
                <asp:Button ID="btnBack" runat="server" Text="Back"  
                    CssClass="submitBtn" />
                <asp:Button ID="btnDeleteAll" runat="server" Text="Delete All" OnClick="btnDeleteAll_Click"
                    CssClass="submitBtn" />
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                    CssClass="submitBtn" />
            </td>
        </tr>
    </table>
</div>
</ContentTemplate>
</asp:UpdatePanel>
<%--  Aparna 14317 Excel upload--%>
<br />
<br />
<asp:Label runat="server" Text="OR" Font-Bold="true" Visible="false"></asp:Label><br />
<br />
<br />
<table class="excelFormatTable" id="iDExcelTemp" runat="server" visible="false">
    <tr>
        <td class="excelTdWidth">
            <asp:Label ID="lblExceltemplate" Font-Bold="true" Text="Upload Records using Excel Template"
                runat="server"></asp:Label>
        </td>
        <td class="excelLblUpload">
            <asp:Label ID="lblUpload" Font-Bold="true" Text="Upload your Excel file" runat="server"></asp:Label><br />
        </td>
        <td class="excelTdWidthexport">
            <asp:Label ID="lblUploadedExcel" Visible="false" Font-Bold="true" Text="Uploaded Excel file"
                runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="excelTdWidth">
            <div class="templetImge">
                <rad:RadBinaryImage ID="imgExcelTemplet" ImageUrl="~/Images/Excel_ICON.png" runat="server" />
            </div>
            <div class="templetLink">
                <asp:LinkButton ID="Download" CssClass="namelink" runat="server" Text="Download Template"
                    OnClick="Download_Click" />
            </div>
        </td>
        <td class="RadAsyncUploadTd">
            <table>
                <tr>
                    <td>
                        <telerik:RadAsyncUpload ID="xlsUpload" CssClass="radButtonPadding RadUploadWidth"
                            Localization-Select="Browse..." runat="server" Localization-Remove="Remove" ControlObjectsVisibility="None"
                            MaxFileInputsCount="1" OnClientFileSelected="Hidelable">
                            <FileFilters>
                                <telerik:FileFilter Extensions="xls,xlsx" />
                            </FileFilters>
                        </telerik:RadAsyncUpload>
                    </td>
                    <td class="exceBtnUpload">
                        <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="submitBtn uploadBtnHeight"
                            OnClick="btnUpload_Click" Height="22px" />
                    </td>
                </tr>
            </table>
        </td>
        <td class="excelTdWidthexport ExportExcelPadding">
            <div class="exportExcelLink">
                <asp:LinkButton CssClass="namelink" ID="ExportExcel" runat="server" Text="AddPerson.xlsx"
                    OnClick="ExportExcel_Click" Visible="false" /></div>
            <div class="excelImgDiv">
                <rad:RadBinaryImage ID="radImgSmallExcel" ImageUrl="~/Images/Excel_ICO.png" runat="server"
                    Visible="false" /></div>
        </td>
    </tr>
    <tr>
        <td class="excelTdWidthexport">
        </td>
        <td class="excellblmsg">
            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
        <td class="excelTdWidth">
        </td>
    </tr>
</table>
