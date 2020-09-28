<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ForumMessageGrid.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Forums.ForumMessageGrid" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%--Dilip changes --%>
<table>
    <tr>
        <td colspan="2" class="tdForumPost">
            <asp:Panel DefaultButton="cmdNewPost" ID="PnlTextSearch" runat="server">
                <asp:Button ID="cmdNewPost" Text="New Post" runat="server" CssClass="submitBtn"></asp:Button>
                <asp:Button ID="cmdReply" Text="Reply" runat="server" Visible="false"  CssClass="submitBtn"></asp:Button> 
                <asp:DropDownList ID="cmbViewType" runat="server" AutoPostBack="True">
                    <asp:ListItem Value="Threaded View">Threaded View</asp:ListItem>
                    <asp:ListItem Value="Standard View">Standard View</asp:ListItem>
                </asp:DropDownList>
            </asp:Panel>
        </td>
    </tr>
    </table>
    <table>
    <tr>
        <td colspan="2">
      <%--  Navin Prasad Issue 11032--%>
            <%-- Nalini Issue 12458--%>
            <asp:GridView ID="grdMain" Width="99%" SkinID="ForumsDataGrid" runat="server" AllowSorting="True"
                AutoGenerateColumns="False"  GridLines="Horizontal" CssClass="gridForumAlign"
                BorderColor="#CCCCCC" BorderWidth="1px" AlternatingRowStyle-BackColor="White">
                <HeaderStyle CssClass="GridViewHeader" Height="28px" HorizontalAlign="Center" Font-Bold="true" />
                <FooterStyle CssClass="GridFooter" />
                <RowStyle CssClass="GridItemStyle" BackColor="#e5e2dd" />
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
                        <HeaderStyle Width="295px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="From">
                    <ItemStyle Width="200px" />
                        <ItemTemplate>
                            <asp:Label ID="lblWebUserNameWCompany" runat="server" Text='<%# Eval("WebUserNameWCompany") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="150px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sent">
                    <ItemStyle Width="200px" />
                        <ItemTemplate>
                            <asp:Label ID="lblDateEntered" runat="server" Text='<%# Eval("DateEntered") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ChildCount" HeaderText="# Replies" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="60px" />
                    <asp:TemplateField  Visible="false"  HeaderText="ID" >
                    <ItemTemplate>
                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField  Visible="false"   HeaderText="ParentPos" >
                    <ItemTemplate>
                    <asp:Label ID="lblParentPos" runat="server" Text='<%# Eval("ParentPos") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    
                    
                     <asp:TemplateField  Visible="false"   HeaderText="Expanded" >
                    <ItemTemplate>
                    <asp:Label ID="lblExpanded" runat="server" Text='<%# Eval("Expanded") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    
                   
                    
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
        </td>
    </tr>
</table>
<cc2:User runat="server" ID="User1" />
