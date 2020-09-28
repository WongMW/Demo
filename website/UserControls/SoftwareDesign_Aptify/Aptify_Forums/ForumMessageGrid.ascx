<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Forums/ForumMessageGrid.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Forums.ForumMessageGrid" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%--Dilip changes --%>
<div>
    <asp:Panel DefaultButton="cmdNewPost" ID="PnlTextSearch" runat="server">
        <div class="actions">
            <asp:Button ID="cmdNewPost" Text="New Post" runat="server" CssClass="submitBtn"></asp:Button>
            <asp:Button ID="cmdReply" Text="Reply" runat="server" Visible="false" CssClass="submitBtn"></asp:Button>
        </div>
        <asp:DropDownList ID="cmbViewType" runat="server" AutoPostBack="True">
            <asp:ListItem Value="Threaded View">Threaded View</asp:ListItem>
            <asp:ListItem Value="Standard View">Standard View</asp:ListItem>
        </asp:DropDownList>
    </asp:Panel>

    <br />

    <div class="cai-table">
        <asp:GridView ID="grdMain" runat="server" AllowSorting="True"
            AutoGenerateColumns="False" GridLines="Horizontal">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton runat="server" ImageUrl="~/Images/plus.gif" CommandName="Expand"
                            CommandArgument='<%# Container.DataItemIndex %>' Visible='<%# Eval("ChildCount")>0  AND Eval("Expanded")=0%>'
                            ID="imgPlus"></asp:ImageButton>
                        <asp:ImageButton runat="server" ImageUrl="~/Images/minus.gif" CommandName="Collapse"
                            CommandArgument='<%# Container.DataItemIndex %>' Visible='<%# Eval("ChildCount")>0 AND Eval("Expanded")>0 %>'
                            ID="imgMinus"></asp:ImageButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Subject">
                    <ItemTemplate>
                        <%#Eval("Spaces")%>
                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("Subject") %>' CommandName="Select"
                            CausesValidation="false">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="From">
                    <ItemTemplate>
                        <asp:Label ID="lblWebUserNameWCompany" runat="server" Text='<%# Eval("WebUserNameWCompany") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sent">
                    <ItemTemplate>
                        <asp:Label ID="lblDateEntered" runat="server" Text='<%# Eval("DateEntered") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ChildCount" HeaderText="# Replies" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField Visible="false" HeaderText="ID">
                    <ItemTemplate>
                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="false" HeaderText="ParentPos">
                    <ItemTemplate>
                        <asp:Label ID="lblParentPos" runat="server" Text='<%# Eval("ParentPos") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="false" HeaderText="Expanded">
                    <ItemTemplate>
                        <asp:Label ID="lblExpanded" runat="server" Text='<%# Eval("Expanded") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</div>

<cc2:User runat="server" ID="User1" />
