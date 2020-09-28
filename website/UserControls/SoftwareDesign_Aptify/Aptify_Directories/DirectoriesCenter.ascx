<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Directories.DirectoriesCenter" CodeFile="~/UserControls/Aptify_Directories/DirectoriesCenter.ascx.vb" %>

<div class="content-container clearfix">
    <table class="data-form">
        <tr>
            <td>
                <b>
                    <asp:HyperLink runat="server" ID="memberBrowse">Individual Member Directory</asp:HyperLink></b><br />
                Browse and Search Individual Members
            </td>
        </tr>
        <tr>
            <td>
                <b>
                    <asp:HyperLink runat="server" ID="companyBrowse">Corporate Member Directory</asp:HyperLink></b><br />
                Browse and Search Corporate/Organizational Members
            </td>
        </tr>
        <tr>
            <td>
                <b>
                    <asp:HyperLink runat="server" ID="marketBrowse">Marketplace</asp:HyperLink></b><br />
                Marketplace of providers of products and services that are specifically of interest
                to our industry.
            </td>
        </tr>
    </table>
</div>