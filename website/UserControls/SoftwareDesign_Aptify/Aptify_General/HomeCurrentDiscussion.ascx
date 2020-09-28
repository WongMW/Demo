<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_General/HomeCurrentDiscussion.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.Aptify_General.HomeCurrentDiscussion" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
    <%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<div>
    <asp:ListView ID="lstCurrentDiscussion" runat="server">
        <LayoutTemplate>
            <table border="0">
                <tr class="tableHeader">
              <%--  Dilip issue 12717 For change padding --%>
              <%--Dilip Issue 13144 Remove folder Aptify_UC_Img from image path--%>
                    <td class="tableHeaderFont" colspan="4" style="width: 36%;">
                        <asp:Image runat="server" ID="img2" ImageUrl="~/Images/discussion-icon.png"
                            CssClass="MiddleImage" />
                        <asp:Label runat="server" ID="Label1" Text="Current Discussion" />
                    </td>
                </tr>
                <tr id="itemPlaceholder" runat="server">
                </tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
           
                <table style="width:100%;">
                    <tr style="padding-top: 50px;">
                        <td style="padding-top: 8px; vertical-align: top;">
                        <%-- Neha changes Issue 16001,05/07/13--%>
                         <div class="imgmember">
                        <rad:RadBinaryImage ID="imgProfileRad" runat="server" AutoAdjustImageControlSize="false" ResizeMode="Fill" CssClass="PeopleImage" />
                        </div>
                        </td>
                    <td class="tablecontrolsfont" style="width: 200px; height: auto;">
                        <asp:LinkButton ID="lnkTopic" runat="server" CommandName="disscussion" CommandArgument='<% #Eval("ForumID") %>'
                            Style="cursor: pointer;" ToolTip='<% #Eval("Subject") %>'></asp:LinkButton>
                         <%--   <asp:HyperLink ID="lnkTopic"  runat="server" Text='<% #Eval("Subject") %>'  style="cursor:pointer;"></asp:HyperLink>--%>
                            <br />
                            <div id="StarterDiv" class="tablelightfont">
                                Started By:
                                <asp:Label ID="lblStarter" runat="server" Text='<% #eval("webUserName") %>'></asp:Label>
                            </div>
                           <%-- <div id="LastReplyDiv" class="tablelightfont" style="width:206px;">
                                <asp:Label ID="lblLastReply" runat="server" Text='<% #Eval("Body") %>'></asp:Label>
                            </div>--%>
                        </td>
                        <%--Dilip changes--%>
                        <td class="tablecontrolsfont" style="padding-right: 1px; float:right; text-align:right; padding-top:5px;">
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/reply-icon.png" />
                            <div>
                                <asp:Label ID="lblReplyCount" runat="server" Text='<% #Eval("ChildCount") %>'></asp:Label>
                                 Replies
                            </div>
                        </td>
                        <%--<td class="tablecontrolsfont">
                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Aptify_UC_Img/like-icon.png" />
                            <div>
                                <asp:Label ID="lblLikesCount" runat="server" Text="1 likes"></asp:Label>
                            </div>
                        </td>--%>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <hr class="GrayLine" />
                        </td>
                    </tr>
                </table>
          
        </ItemTemplate>
    </asp:ListView>
</div>
<asp:Label ID="lblsfMessage" runat="server" ></asp:Label>
   <cc2:User runat="Server" ID="User1" />