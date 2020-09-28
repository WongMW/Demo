<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EnhancedFirmApproval_Step2.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.FirmsSearch.EnhancedFirmApproval_Step2" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="uc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<asp:Repeater ID="contactList" runat="server">
    <HeaderTemplate>
        <h2>Admin Contact List:</h2>
    </HeaderTemplate>
    <ItemTemplate>
        <div>
            <%# Container.DataItem.ToString() %>
        </div>
    </ItemTemplate>
</asp:Repeater>

<div id="errorMessage" runat="server" visible="false">
    
</div>

<asp:Repeater ID="offices" runat="server" OnItemDataBound="offices_ItemDataBound">
    <HeaderTemplate>
        <table>
            <thead>
                <tr>
                    <td></td>
                    <td>Preview</td>
                    <td>Approve</td>
                </tr>
            </thead>
            <tbody>
    </HeaderTemplate>
    <ItemTemplate>
                <tr>
                    <td style="<%# (Boolean)Eval("IsParent") ? "font-weight: bold" : "padding-left: 30px;" %>"><%# Eval("FirmName").ToString() %></td>
                    <td>
                        <a id="linkPreview" href="#" visible='<%# Eval("IsApproveAllowed") %>' data-id='<%# Eval("ID").ToString() %>' class="enhancedListingPreview" runat="server">View</a>
                        <rad:radwindow id="viewChanges" runat="server" width="400px" height="360px"
                            modal="True" skin="Default" backcolor="#f4f3f1" visiblestatusbar="False"
                            forecolor="#BDA797" iconurl="~/Images/Alert.png" title="Preview Changes" behavior="Close" OpenerElementID="linkPreview">
                            <ContentTemplate>
                                <div class="password-reset-popup forgot-modal">    
                                    <div><b runat="server" id="txtCompanyName">Company Name</b></div>
                                    <div><b>Description</b></div>
                                    <p runat="server" id="txtDescription">Company Description</p>
                                    <asp:Image ImageUrl="~/Images/Alert.png" runat="server" ID="img" />
                                    <p>Number of Employees: <span id="txtNumberOfEmployees" runat="server">N/A</span></p>
                                    <p>Number of Partners: <span id="txtNumberOfPartners" runat="server">N/A</span></p>
                                    <p>Sectors: <span id="txtSectors" runat="server">N/A</span></p>
                                    <p>Specialisms: <span id="txtSpecialisms" runat="server">N/A</span></p>
                                    <asp:HyperLink runat="server" ID="googleLink">Google Maps</asp:HyperLink>
                                </div>
                            </ContentTemplate>
                        </rad:radwindow>
                    </td>
                    <td><asp:CheckBox Visible='<%# Eval("IsApproveAllowed") %>' runat="server" ID="chkApprove" /></td>
                </tr>
    </ItemTemplate>
    <FooterTemplate>
            </tbody>
        </table>
    </FooterTemplate>
</asp:Repeater>

<uc2:User id="User1" runat="server" />

<asp:Button ID="btnSave" runat="server" Text="Submit" OnClick="btnSave_Click" />
<asp:Button ID="btnBack" runat="server" Text="Back to Step 1" OnClick="btnBack_Click" />
