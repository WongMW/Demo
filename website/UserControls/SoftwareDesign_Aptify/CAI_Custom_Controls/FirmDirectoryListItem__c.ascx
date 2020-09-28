<%@ Control Language="C#" className="FirmDirectoryListItem__c" AutoEventWireup="true" CodeFile="~/UserControls/CAI_Custom_Controls/FirmDirectoryListItem__c.ascx.cs" Inherits="UserControls_CAI_Custom_Controls_FirmDirectoryListItem__c" %>

    <tr>
        <td>
           <asp:Label ID="lblFirmName" runat="server" Text="FirmName" /></td>           
        <td>
            <div align="right">
                <asp:Button runat="server" ID="moreInfoBtn" class="submitBtn" type="submit" Text="See More Info"></asp:Button>
            </div>
        </td>
    </tr>

