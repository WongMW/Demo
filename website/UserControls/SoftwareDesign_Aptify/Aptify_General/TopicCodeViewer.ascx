<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_General/TopicCodeViewer.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.TopicCodeViewer" %>
<%@ Register TagPrefix="cc4" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessHierarchyTree" %>
<%@ Register TagPrefix="cc6" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="radTree" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<script type="text/javascript">
    Telerik.Web.UI.RadTreeView.prototype._onKeyDown = function (e) { }
</script>

<div>
    <table id="tblMain" runat="server" class="data-form">
        <tr>
            <td id="tdtopic" runat="server">
                <asp:Label runat="server" ID="lblUser" Visible="false"></asp:Label><asp:Label runat="server" ID="lblinterest" CssClass="label-title" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td id="tdtopiccode" runat="server">
                <asp:Label ID="Topicskeep" runat="server"><p style="text-align:left;"></p></asp:Label>
            </td>
            <td>
                <asp:Label ID="MsgBlankTopiccode" runat="server" Text="There are no preferences available for selection at this time."></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="for-tree-structure">
                        
                <asp:Label ID="lblDescription" runat="server" Visible="False"></asp:Label><br />
                <div class="topic-code-headers-holder" runat="server" id="trvTopicCodesRepeaterDiv">
                    <asp:Repeater ID="trvTopicCodesRepeater" runat="server">
                        <ItemTemplate>
                            <a data-index='<%# Container.ItemIndex %>' href="#" class="trvTopcCodesRepeaterLink">
                                <%# Container.DataItem.ToString()  %>
                            </a>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

                <style>
                    .rtUL.rtLines > .rtLI > div {
                        display: none;
                    }
                    #trvTopicCodes > .rtUL.rtLines > .rtLI {
                        border: none;
                    }
                    .rtUL.rtLines > .rtLI > ul {
                        display: none!important;
                    }
                    .global-opt-out-panel {
                        padding-left: 20px;
                        padding-right: 70%;
                    }
                </style>
                <style id="tabSwitchingStyle">.rtUL.rtLines > .rtLI:nth-child(1) > ul { display: block!important; }</style>
                <asp:UpdatePanel ID="update1" runat="server">
                    <ContentTemplate>
                        <radTree:RadTreeView ID="trvTopicCodes" runat="server" Margin="8" CheckBoxes="True"
                            ClientIDMode="Static" ExpandAnimation-Type="None" CausesValidation="false">
                            <NodeTemplate>
                                <asp:Label ID="lblTopicCode" runat="server"></asp:Label>
                                <asp:DropDownList ID="ddlTopicCode" runat="server">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtTopicCode" runat="server"></asp:TextBox>
                            </NodeTemplate>
                        </radTree:RadTreeView>
                        <p />
                        <div class="global-opt-out-panel">
                            <b>
                            <asp:CheckBox runat="server" ID="chkGlobalOptOut" AutoPostBack="true" OnCheckedChanged="chkGlobalOptOut_CheckedChanged" /> <span class="checkboxText">Global opt out</span>
                            <p>
                                (Select this option if you wish to opt out of all newsletters and marketing emails.
                                If you are a member or student, you will still receive regulatory and student information emails from the Institute.)
                            </p>
                            </b>
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="trvTopicCodes" EventName="" />
                        <asp:AsyncPostBackTrigger ControlID="chkGlobalOptOut" EventName="" />
                    </Triggers>
                </asp:UpdatePanel>
                <p class="actions">
                    <asp:Label ID="lblEntityID" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblRecordID" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblEntityName" runat="server" Visible="False"></asp:Label>
                </p>
                <p />
                <div class="actions">
                    <asp:Button ID="cmdSave" CssClass="submitBtn" runat="server" Text="Save"></asp:Button>
                    <asp:Button ID="cmdCheckAll" runat="server" CssClass="submitBtn" Text="Check All" Visible="false"></asp:Button>
                    <asp:Button ID="cmdClearAll" runat="server" CssClass="submitBtn" Text="Clear" Visible="false"></asp:Button>
                </div>
            </td>
        </tr>
    </table>
</div>

<script type="text/javascript">
    $(function () {
        var topicCodesRepeater = "#<%= trvTopicCodesRepeaterDiv.ClientID %>";
        var trvTopicCodes = "#<%= trvTopicCodes.ClientID %>";

        $(topicCodesRepeater).find(".trvTopcCodesRepeaterLink").click(function (e) {
            e.preventDefault();
            e.stopPropagation();

            $(this).data('opened', true);

            // checking if already current
            if ($(this).hasClass("current")) {
                return;
            }

            $(topicCodesRepeater).find(".trvTopcCodesRepeaterLink").removeClass("current");
            $(this).addClass("current");
            var indx = $(this).data("index");
            $("#tabSwitchingStyle").html(
                ".rtUL.rtLines > .rtLI:nth-child("+(parseInt(indx)+1)+") > ul { display: block!important; }"
            );
        });
        $(trvTopicCodes).children(".rtUL.rtLines .rtPlus").each(function () {
            $(this).click();
        });
        if ($(topicCodesRepeater).find(".trvTopcCodesRepeaterLink").length > 0) {
            $($(topicCodesRepeater).find(".trvTopcCodesRepeaterLink")[0]).click();
        }
    });
</script>
<cc6:User ID="User1" runat="server" />
