<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/TopicCodeControl__c.ascx.vb" Debug="true" Inherits="Aptify.Framework.Web.eBusiness.CustomerService.TopicCodeControl" %>
<%@ Register TagPrefix="radTree" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="cc6" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<script type="text/javascript">
    Telerik.Web.UI.RadTreeView.prototype._onKeyDown = function (e) { }
</script>
<div>
    <asp:UpdatePanel ID="upnl" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="tblMain" runat="server">
                <span class="label-title">Help us to serve you better by selecting areas of practice or interest below.<%--<font color="red">Please click Save to record your selection.</font>--%></span>

                <div class="for-tree-structure">
                    <radTree:RadTreeView ID="trvTopicCodes" runat="server" Margin="5" CheckBoxes="True" ClientIDMode="Static" ExpandAnimation-Type="None" CausesValidation="false">
                        <NodeTemplate>
                            <asp:Label ID="lblTopicCode" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlTopicCode" runat="server">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtTopicCode" runat="server"></asp:TextBox>
                        </NodeTemplate>
                    </radTree:RadTreeView>
                </div>

                <div runat="server" visible="false" id="trTopicCode">
                    <asp:Button ID="cmdSave" runat="server" Text="Save" Visible="false"></asp:Button>
                    <asp:Button ID="cmdCheckAll" runat="server" Text="Check All" Visible="false"></asp:Button>
                    <asp:Button ID="cmdClearAll" runat="server" Text="Clear" Visible="false"></asp:Button>
                    <asp:Label ID="lblDescription" runat="server" Visible="False"></asp:Label><br />
                </div>
            </div>
        </ContentTemplate>
        <%-- <Triggers>
        <asp:AsyncPostBackTrigger ControlID="trvTopicCodes" EventName="NodeCheck"/>
        </Triggers>--%>
    </asp:UpdatePanel>
</div>
<cc6:User ID="User1" runat="server" />


