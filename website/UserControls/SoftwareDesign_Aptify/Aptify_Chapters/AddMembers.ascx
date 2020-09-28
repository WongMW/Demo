<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Chapters/AddMembers.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Chapters.AddMembersControl" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="uc1" TagName="ChapterMember" Src="ChapterMember.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NameAddressBlock" Src="../Aptify_General/NameAddressBlock.ascx" %>
<%@ Register TagPrefix="cc4" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="content-container clearfix">
    <asp:Label ID="lblErrorMain" runat="server" Visible="False" ForeColor="Red"></asp:Label>
    <table id="tblMain" runat="server" class="data-form">
        <tr>
            <td>
                <asp:Label ID="lblChapterName" runat="server" Font-Size="Larger" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr runat="server" id="trSuccess">
            <td>
                <p>
                    Your submission was accepted. Please press Continue to return to the main chapter
                    screen.</p>
                <p align="center">
                    <asp:Button ID="btnSuccess" runat="server" Text="Continue" CssClass="submitBtn">
                    </asp:Button></p>
            </td>
        </tr>
        <tr runat="server" id="trAddMembers">
            <td>
                <asp:Button ID="cmdAddRow" AccessKey="A" CssClass="submitBtn" runat="server" Text="Add Row"
                    DESIGNTIMEDRAGDROP="120"></asp:Button>
                <asp:Button ID="cmdSubmit" runat="server" Text="Submit New Members" CssClass="submitBtn">
                </asp:Button>
                <%--Suraj issue 15154 apply style--%>
                <asp:Label ID="lblError" runat="server" Style="color: Red" Visible="False"></asp:Label>
                <%--Navin Prasad issue 11032--%>
                <%--Suvarna D IssueID: 12436 on Dec 1, 2011 added Update Panel --%>
                <asp:UpdatePanel ID="updPanelGrid" runat="server">
                    <ContentTemplate>
                        <%--RashmiP, IssueID: 14448 on Jan 14, 2013 Chaged to Rad Grid --%>
                        <%-- Suraj issue 15154 ,2/28/12 Add CssClass="data-form" and  Remove HeaderStyle-CssClass="GridViewHeaderStyle from GridTemplateColumn".--%>
                        <rad:RadGrid ID="grdMembers" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                            CssClass="data-form" AllowFilteringByColumn="false">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AllowSorting="false">
                                <Columns>
                                    <rad:GridTemplateColumn HeaderText="First Name" DataField="FirstName" SortExpression="FirstName"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtFirstName" Width="100px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FirstName") %>'>
                                            </asp:TextBox>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Last Name" DataField="LastName" SortExpression="LastName"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtLastName" Width="100px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LastName") %>'>
                                            </asp:TextBox>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Title" DataField="Title" SortExpression="Title"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtTitle" Width="100px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Title") %>'>
                                            </asp:TextBox>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Email" DataField="Email" SortExpression="Email"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtEmail" runat="server" Width="200px" Text='<%# DataBinder.Eval(Container.DataItem,"Email") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Delete" AllowFiltering="false">
                                        <ItemTemplate>
                                                 <asp:ImageButton ID="Button1" runat="server" ImageUrl="~/Images/Delete.png" CommandName="Delete"
                            CommandArgument="<%# CType(Container, GridDataItem).RowIndex %>" />
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </rad:RadGrid>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <%--  <asp:GridView ID="grdMembers" runat="server" AutoGenerateColumns="false" SkinID="test"
                            Width="100%" AlternatingRowStyle-BackColor="White" GridLines="Horizontal" BorderColor="#CCCCCC"
                            BorderWidth="1px">
                            <HeaderStyle CssClass="GridViewHeader" Height="28px" HorizontalAlign="Left" Font-Bold="true" />
                            <FooterStyle CssClass="GridFooter" />
                            <RowStyle CssClass="GridItemStyle" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                        <asp:Button ID="Button1" CssClass="submitBtn"  runat="server" CommandName="Delete" Text="Delete" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="First Name">
                        <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtFirstName" Width="100px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FirstName") %>'>
                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Last Name">
                         <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtLastName" Width="100px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LastName") %>'>
                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Title">
                         <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtTitle" Width="100px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Title") %>'>
                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email">
                         <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtEmail" Width="150px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Email") %>'>
                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings Mode="Numeric" />
                </asp:GridView>--%>
                <%--End of addition by Suvarna D IssueID: 12436 on Dec 1, 2011--%>
                <%--<asp:DataGrid ID="grdMembers" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:ButtonColumn CommandName="Delete" Text="Delete" ButtonType="PushButton" HeaderText="Delete">
                        </asp:ButtonColumn>
                        <asp:TemplateColumn HeaderText="First Name">
                            <ItemTemplate>
                                <asp:TextBox ID="txtFirstName" Width="100px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FirstName") %>'>
                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Last Name">
                            <ItemTemplate>
                                <asp:TextBox ID="txtLastName" Width="100px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LastName") %>'>
                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Title">
                            <ItemTemplate>
                                <asp:TextBox ID="txtTitle" Width="100px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Title") %>'>
                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Email">
                            <ItemTemplate>
                                <asp:TextBox ID="txtEmail" Width="150px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Email") %>'>
                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                    <PagerStyle Mode="NumericPages"></PagerStyle>
                </asp:DataGrid>--%>
            </td>
        </tr>
    </table>
    <cc4:AptifyShoppingCart runat="Server" ID="ShoppingCart1" />
    <cc3:User ID="User1" runat="server" />
</div>
