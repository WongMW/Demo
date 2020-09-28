<%@ Control Language="VB" AutoEventWireup="false" CodeFile="OptOutTopicCodes__c.ascx.vb"  Debug="true" Inherits="Aptify.Framework.Web.eBusiness.CustomerService.OptOutTopicCodes"  %>
<%@ Register TagPrefix="radTree" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="cc6" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<script type="text/javascript">
    Telerik.Web.UI.RadTreeView.prototype._onKeyDown = function (e) { } 
</script>
<div style="width=90%; float: left">
    <asp:UpdatePanel ID="upnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="BorderDiv">
                <table id="tblMain" runat="server">
                 
                    <tr>
                        <td>
                            Help us to serve you better by selecting areas of practice or interest below.<%--<font color="red">Please click Save to record your selection.</font>--%></td>
                    </tr>
                    <tr runat="server" id="trTopicCode">
                        <td>
                            <asp:Button ID="cmdSave" runat="server" Text="Save"></asp:Button>
                            <asp:Button ID="cmdCheckAll" runat="server" Text="Check All"></asp:Button>
                            <asp:Button ID="cmdClearAll" runat="server" Text="Clear"></asp:Button>
                            <asp:Label ID="lblDescription" runat="server"></asp:Label><br />
                        </td>
                    </tr>
                    <tr>
                        <td class="for-tree-structure">
                            <radTree:RadTreeView ID="trvTopicCodes" runat="server" Margin="5" CheckBoxes="True" ClientIDMode="Static" ExpandAnimation-Type="None">
                                <NodeTemplate>
                                    <asp:Label ID="lblTopicCode" runat="server"></asp:Label>
                                    <asp:DropDownList ID="ddlTopicCode" runat="server">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtTopicCode" runat="server"></asp:TextBox>
                                </NodeTemplate>
                            </radTree:RadTreeView>
                        </td>
                    </tr>  
                       <tr runat="server">
                        <td>                            
                            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label><br />
                        </td>
                    </tr>                
                </table>
            </div>
        </ContentTemplate>
        <Triggers>
        <asp:AsyncPostBackTrigger ControlID="trvTopicCodes" EventName="NodeCheck"/>
        </Triggers>
         
    </asp:UpdatePanel>
</div>
<cc6:User ID="User1" runat="server" />
