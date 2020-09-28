<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Directories.SearchControl"
    CodeFile="Search.ascx.vb" EnableViewState="true" ViewStateMode="Enabled" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEbusinessUser" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessLogin" %>
<%@ Register TagPrefix="uc1" TagName="CompanyDirectoryGrid" Src="CompanyDirectoryGrid.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PersonDirectoryGrid" Src="PersonDirectoryGrid.ascx" %>
<div class="content-container clearfix">
        <%--Suraj Issue 14451 3/11/13 , if the user serching any data and type any text in textbox and after click on enter button button Search event gets fired   --%>
    <asp:Panel ID="pnlsearch" runat="server" DefaultButton="cmdSearch">
        <table id="tblMain" runat="server" class="data-form">
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblHeader" CssClass="MeetingDates" runat="server" Text="Search Directory Page"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="LeftColumn">
                    Search by Name:
                </td>
                <td>
                    <asp:TextBox ID="txtSearch" Width="300px" runat="server" CssClass="txtfontfamily"
                        EnableViewState="true"></asp:TextBox>
                    <asp:Button ID="cmdSearch" runat="server" Text="Search" CssClass="submitBtn" OnClientClick="GetSearch()"
                        OnClick="cmdSearch_Click"></asp:Button>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <uc1:CompanyDirectoryGrid ID="CompanyDirectoryGrid" runat="server"></uc1:CompanyDirectoryGrid>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <uc1:PersonDirectoryGrid ID="PersonDirectoryGrid" runat="server"></uc1:PersonDirectoryGrid>
                </td>
            </tr>
        </table>
    </asp:Panel>
</div>
<cc3:User ID="User1" runat="server"></cc3:User>
<cc1:AptifyWebUserLogin ID="WebUserLogin1" runat="server"></cc1:AptifyWebUserLogin>