<%@ Control Language="VB" AutoEventWireup="False" CodeFile="~/UserControls/Aptify_Custom__c/CompanyContact__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CustomerService.CompanyContact__c" %>

<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<style type="text/css">
    .style3
    {
        width: 131px;
    }
</style>
<div id="Container" class="ProfileMainDiv">
    <asp:Panel ID="pnlProfile" runat="server">
        <div class="BorderDiv">
            <table id="tblMain" runat="server" width="100%" class="data-form">
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td class="style3">
                                    <asp:Label ID="lblcompanyname" runat="server" Text="Company Name" 
                                        style="text-align: left"></asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:TextBox ID="txtCompany" CssClass="txtBoxEditProfile" runat="server" AutoPostBack="true"></asp:TextBox>
                                          <%-- <Ajax:AutoCompleteExtender ID="AutoCompleteExtender" runat="server" BehaviorID="autoComplete"
                                            CompletionInterval="10" CompletionListElementID="divwidth" 
                                           CompletionSetCount="12" EnableCaching="true" MinimumPrefixLength="1" ServiceMethod="GetCompanyList"
                                            ServicePath="~/GetCompanyList__c.asmx" TargetControlID="txtCompany">
                                        </Ajax:AutoCompleteExtender>--%>
                                </td>
                            </tr>
                           
                            <tr>
                                <td colspan="2">
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                    <asp:Label ID="lblAddress" runat="server" style="text-align: left">Address:</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:TextBox ID="txtAddressLine1" runat="server" CssClass="txtBoxEditProfile"></asp:TextBox>
                                   <%-- <asp:TextBox ID="txtCompany" runat="server" CssClass="txtBoxEditProfile"></asp:TextBox>--%>
                                  
                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                    <asp:Label ID="Label1" runat="server" style="text-align: left">(Area Code) Phone:</asp:Label>
                                </td>
                                <td class="RightColumn">
                                    <asp:TextBox ID="txtcode" runat="server" 
                                        CssClass="txtUserProfileAreaCodeSmall" style="margin-left: 0px"></asp:TextBox>
                                    <asp:TextBox ID="txtphone" runat="server" CssClass="txtUserProfileAreaCode"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <cc1:User runat="server" ID="User1" />
</div>
