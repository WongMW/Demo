<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetControls_c.aspx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.GetControls.GetControls_c" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:Button runat="server" ID="getControlsButton" Text="Get used controls list" OnClick="GetControls_Click" />
        <br /><br />
        <label>Please enter control name to get pages where this control is used</label>
        <br/>
        <asp:TextBox runat="server" ID="searchPagesTextBox"></asp:TextBox>
        <asp:Button runat="server" ID="getPagesButton" Text="Search" OnClick="GetPagesButton_Click" />
        <br />
        <asp:UpdatePanel ID="searchPagesUpdatePane" runat="server">
            <ContentTemplate>
                <asp:Label ID="pagesListLabel" runat="server"/>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="getPagesButton" />
            </Triggers>
        </asp:UpdatePanel>

        
        <br /><br />
        <label>Please enter page name to get controls placed on this page  </label>
        <br/>
        <asp:TextBox runat="server" ID="searchPageControlsTextBox"></asp:TextBox>
        <asp:Button runat="server" ID="getPageControlsButton" Text="Search" OnClick="GetPageControlButton_Click" />
        <br />

           <asp:UpdatePanel ID="searchPageControlsUpdatePane" runat="server">
            <ContentTemplate>
                <asp:Label ID="controlsListLabel" runat="server"/>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="getPageControlsButton" />
            </Triggers>
        </asp:UpdatePanel>
     

    </form>
</body>
</html>


