<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Customer_Service/TopicCodeControl.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CustomerService.TopicCodeControl" %>
<%@ Register TagPrefix="radTree" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="cc6" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="uc1" TagName="TopicCodeViewer" Src="~/UserControls/SoftwareDesign_Aptify/Aptify_General/TopicCodeViewer.ascx" %>
<%--<script type="text/javascript">
   Telerik.Web.UI.RadTreeView.prototype._onKeyDown = function(e) { } 
</script>--%>
<%--<div style="width=90%; float: left">
    <table id="tblMain" runat="server" class="data-form" style="width=90%; float: left">
        <tr>
            <td>
                <asp:Label runat="server" ID="lblUser"></asp:Label>
                Please Update Your Topics of Interest
            </td>
        </tr>
        <tr>
            <td>
                Topics keep track of what areas you are most interested in. By providing this information
                to the site, the features of each area of the site will reflect your interests more
                directly and allow us to serve you better.
            </td>
        </tr>
        <tr>
            <td class="for-tree-structure">
                <asp:Button ID="cmdSave" runat="server" Text="Save"></asp:Button>
                <asp:Button ID="cmdCheckAll" runat="server" Text="Check All"></asp:Button>
                <asp:Button ID="cmdClearAll" runat="server" Text="Clear"></asp:Button>
                <asp:Label ID="lblDescription" runat="server" Visible="False"></asp:Label><br />
                <radTree:RadTreeView ID="trvTopicCodes" runat="server" Margin="8" CheckBoxes="True"
                    ClientIDMode="Static" ExpandAnimation-Type="None" CausesValidation="false">
                    <NodeTemplate>
                        <asp:Label ID="lblTopicCode" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlTopicCode" runat="server">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtTopicCode" runat="server"></asp:TextBox>
                    </NodeTemplate>
                </radTree:RadTreeView>
                <p>
                    <asp:Label ID="lblEntityID" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblRecordID" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblEntityName" runat="server" Visible="False"></asp:Label></p>
                <p />
            </td>
        </tr>
    </table>
</div>--%>
<%--<cc6:User ID="User1" runat="server" />--%>
<div>
    <uc1:TopicCodeViewer ID="TopicCodeViewer" runat="server">
    </uc1:TopicCodeViewer>
</div>
<cc6:User ID="User1" runat="server" />
