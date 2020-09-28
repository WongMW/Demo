<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Group_Admin/GAAddMember.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CustomerService.GAAddMember" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<cc2:User ID="User1" runat="server" />
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<script language="javascript" type="text/javascript">
    function Hidelable() {
        window.document.getElementById('<%= lblMessage.ClientID %>').style.display = "none"

    }
</script>
<div class="cai-table">
    <div class="page-message" >      
        <asp:Label ID="lblNote" runat="server" ForeColor="Red" Text="Fields marked with * are mandatory."></asp:Label>    
    </div>
            <asp:GridView ID="grdAddMember" AutoGenerateColumns="false" runat="server" ShowFooter="false"
                Width="100%" CssClass="grid-table" AllowPaging="false">
                <Columns>
                    <asp:BoundField DataField="RowNumber" HeaderText="Row Number" Visible="false" />
                    <asp:TemplateField>
                        <HeaderStyle CssClass="rgHeader" HorizontalAlign="Left"/>
                        <ItemStyle CssClass="GAAddMembertdalign" />
                        <HeaderTemplate>
                            <asp:Label ID="lblFname" CssClass="no-mob" Text="First Name" runat="server"></asp:Label>
                            <asp:Label ID="lblFnameAsterisk"  Text="*" CssClass="addNewPeapleMandatoryFields"  runat="server"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="label4" CssClass="mobile-label no-desktop" Text="First Name:" runat="server"></asp:Label>
                            <asp:TextBox ID="txtFName" runat="server" CssClass="cai-table-data"></asp:TextBox>
                        </ItemTemplate>
                        <%--<FooterStyle CssClass="GridFooter" />
                <FooterTemplate>
                    <asp:LinkButton ID="lnkAddRow" runat="server" Text="Add Row" OnClick="lnkAddRow_Click" ForeColor="#d07b0c"></asp:LinkButton>
                </FooterTemplate>--%>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderStyle CssClass="rgHeader"/>
                        <HeaderTemplate>
                            <asp:Label ID="lblLname" cssClass="no-mob" Text="Last Name" runat="server"></asp:Label>
                            <asp:Label ID="lblLnameAsterisk"  Text="*" CssClass="addNewPeapleMandatoryFields" runat="server"></asp:Label>
                        </HeaderTemplate>
                        <ItemStyle CssClass="GAAddMembertdalign" />
                        <ItemTemplate>
                             <asp:Label ID="label5" CssClass="mobile-label no-desktop" Text="Last Name" runat="server"></asp:Label>
                            <asp:TextBox ID="txtLName" runat="server" CssClass="cai-table-data"></asp:TextBox>
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
                        <HeaderStyle CssClass="rgHeader"/>
                        <HeaderTemplate>
                            <asp:Label ID="lblTitle" Text="Title" runat="server"></asp:Label>
                        </HeaderTemplate>
                        <ItemStyle CssClass="GAAddMembertdalign" />
                        <ItemTemplate>
                             <asp:Label ID="lblLname" CssClass="mobile-label no-desktop" Text="Title" runat="server"></asp:Label>
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="cai-table-data"></asp:TextBox>
                        </ItemTemplate>
                        <%--<FooterStyle CssClass="GridFooter" />
                <FooterTemplate>
                     <asp:Button ID="btnInsertRows" runat="server" Text="Insert" OnClick="btnInsertRows_Click" />
                </FooterTemplate>--%>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderStyle CssClass="rgHeader" />
                        <HeaderTemplate>
                            <asp:Label ID="lblEmail" Text="Email" CssClass="no-mob" runat="server"></asp:Label>
                            <asp:Label ID="lblEmailAsterisk" Text="*" CssClass="addNewPeapleMandatoryFields" runat="server"></asp:Label>
                        </HeaderTemplate>
                        <ItemStyle HorizontalAlign="Left" CssClass="GAAddMembertdalign" />
                        <ItemTemplate>
                             <asp:Label ID="label1" CssClass="mobile-label no-desktop" Text="Email:" runat="server"></asp:Label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="cai-table-data"></asp:TextBox>
                        </ItemTemplate>
                        <FooterStyle CssClass="GridFooter" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderStyle CssClass="rgHeader" Width="20%" />
                        <HeaderTemplate>
                            <asp:Label ID="lblCompany" Text="Company" runat="server"></asp:Label>
                        </HeaderTemplate>
                        <ItemStyle HorizontalAlign="Left" CssClass="GAAddMembertdalign" />
                        <ItemTemplate>
                            <asp:Label ID="lab" CssClass="mobile-label no-desktop" Text="Company:" runat="server"></asp:Label>
                            <asp:Label ID="lblGACompany" runat="server" CssClass="cai-table-data" Text='<%#DataBinder.Eval(Container.DataItem,"Company")%>'></asp:Label>
                        </ItemTemplate>
                        <%--<FooterStyle CssClass="GridFooter" />
                <FooterTemplate>
                    <asp:Label ID="lblResult" runat="server" Text='' ForeColor="Red"></asp:Label>
                </FooterTemplate>--%>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderStyle CssClass="rgHeader"/>
                        <HeaderTemplate>
                            <asp:Label ID="lblCreateWebUser" Text="Create Web User?" runat="server"></asp:Label>
                        </HeaderTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="label2" CssClass="mobile-label no-desktop" Text="Create New User?:" runat="server"></asp:Label>
                            <asp:CheckBox ID="chkCreateWebUser" CssClass="cai-table-data" runat="server" Checked="true"></asp:CheckBox>
                        </ItemTemplate>
                        <%-- <FooterStyle CssClass="GridFooter" HorizontalAlign="Right" />
                <FooterTemplate>
                    <asp:Button ID="btnDeleteAll" runat="server" Text="Delete All" OnClick="btnDeleteAll_Click" />
                </FooterTemplate>--%>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderStyle CssClass="rgHeader" Width="8%" HorizontalAlign="Center" />
                        <HeaderTemplate>
                            <asp:Label ID="lblDelete" Text="Delete" runat="server"></asp:Label>
                        </HeaderTemplate>
                        <ItemStyle CssClass="GAAddMembertdImagealign" />
                        <ItemTemplate>
                            <asp:Label ID="label3" CssClass="mobile-label no-desktop" Text="Delete User:" runat="server"></asp:Label>
                            <asp:ImageButton ID="btndelete" CssClass="delete-img cai-table-data" runat="server" ImageUrl="~/Images/Delete.png" CommandName="Delete"
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
            <div class="submit-options">               
                        <div class="drop-down-row ">
                            <%--<asp:LinkButton ID="lnkAddRow" runat="server" Text="Add Row" OnClick="lnkAddRow_Click" ForeColor="#d07b0c"></asp:LinkButton>--%>
                            <asp:Label ID="lblAddMultiple" runat="server" CssClass="label " Text="Add Rows:" ForeColor="Black"></asp:Label>
                            <asp:DropDownList ID="drpRows" CssClass="" runat="server">
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
                        </div>                        
                        <div class="actions">
                            <div class="label">
                            <asp:Label ID="lblResult" runat="server" Text='' ForeColor="Red"></asp:Label>
                            </div>
                            <asp:Button ID="btnDeleteAll" runat="server" Text="Delete All" OnClick="btnDeleteAll_Click"
                                CssClass="submitBtn" />
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="submitBtn" />
                        </div>               
            </div>
  
    <%--  Aparna 14317 Excel upload--%>
 <asp:Label runat="server" Text="OR" class="label center-mob"></asp:Label>
    <div class="excel-upload-section">
            <div class="page-message">
                <asp:Label ID="lblExceltemplate" Font-Bold="true" Text="Upload Records using Excel Template" runat="server"></asp:Label>
                 <div class="actions">                      
                    <div class="excelTdWidthexport"></div>
                    <div class="excellblmsg">
                        <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </div>
                    <div class="excelTdWidth"></div>
                </div>             
            </div>
            <div class="page-message labels">                
                 <div class="upload-label">
                      <asp:Label ID="lblUpload" Font-Bold="true" Text="Upload your Excel file" runat="server"></asp:Label>                    
                </div> 
                <div class="upload-image">
                    <rad:RadBinaryImage ID="imgExcelTemplet" ImageUrl="~/Images/Excel_ICON.png" runat="server" />
                </div>              
                <asp:Label ID="lblUploadedExcel" Visible="false" Font-Bold="true" Text="Uploaded Excel file" runat="server"></asp:Label>
               <br />
                <div class="templetLink">
                    <asp:LinkButton ID="Download" CssClass="namelink" runat="server" Text="Download Template" OnClick="Download_Click" />
                </div>
            </div>
            <div class="upload-file">              
                <div class="uploader-input"> 
                <telerik:RadAsyncUpload ID="xlsUpload" CssClass="radButtonPadding RadUploadWidth"   Localization-Select="Browse..." runat="server"  Localization-Remove="Remove" ControlObjectsVisibility="None" MaxFileInputsCount="1" OnClientFileSelected="Hidelable"  >
                    <FileFilters>
                        <telerik:FileFilter Extensions="xls,xlsx" />
                    </FileFilters>
                </telerik:RadAsyncUpload>
                </div>
                <div class="actions">
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" 
                    CssClass="submitBtn uploadBtnHeight" OnClick="btnUpload_Click"/>                    
                </div>
                <div class="excelTdWidthexport ExportExcelPadding" > 
                    <div  class="exportExcelLink" > <asp:LinkButton CssClass="namelink" ID="ExportExcel" runat="server" Text="AddPerson.xlsx" OnClick="ExportExcel_Click" Visible="false" /></div>
                    <div class="excelImgDiv" >
                            <rad:RadBinaryImage ID="radImgSmallExcel"  ImageUrl="~/Images/Excel_ICO.png"  runat="server" Visible="false" />
                    </div>
                </div>                 
            </div>
        </div>
    </div>