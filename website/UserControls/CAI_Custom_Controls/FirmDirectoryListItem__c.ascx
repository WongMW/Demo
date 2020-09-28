<%@ Control Language="C#" className="FirmDirectoryListItem__c" AutoEventWireup="true" CodeFile="FirmDirectoryListItem__c.ascx.cs" Inherits="UserControls_CAI_Custom_Controls_FirmDirectoryListItem__c" %>

    <tr>
        <td>
           <asp:Label ID="lblFirmName" runat="server" Text="FirmName" /></td>           
        <td>
            <div align="right">
                <asp:Button runat="server" ID="moreInfoBtn" class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect mdl-button--accent" type="submit" Text="See More Info"></asp:Button>
            </div>
        </td>
    </tr>

