<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_General/CurrentDiscussion.ascx.vb"
    Inherits="UserControls_Aptify_UC_CurrentDiscussion" %>

<div>
    <table border = 0>
        <tr class="tableHeader">
            <td class="tableHeaderFont"
                colspan="4">
                 <%--Dilip Issue 13144 Remove folder Aptify_UC_Img from image path--%>
                <asp:Image runat="server" ID="img2" ImageUrl="~/Images/discussion-icon.png" CssClass="MiddleImage" />
                <asp:Label runat="server" ID="Label1" Text="Current Discussion" />
            </td>
        </tr>
      
        <tr style = "padding-top:50px;">
            <td style=" padding:5px 8px; vertical-align:top;">
                <asp:Image ID="imgMember" runat="server" ImageUrl="~/Images/mem_img.JPG" />
            </td>
            <td class = "tablecontrolsfont"  >
                <asp:HyperLink ID="lnkTopic" runat="server" Text="New Features in Aptify v5.0 are usable" ></asp:HyperLink>
                <br />
                <div id="StarterDiv" class= "tablelightfont">
                    <asp:Label ID="lblStarter" runat="server" Text="Started by Erin Faulkner in Discussion."></asp:Label>
                </div>
                <div id="LastReplyDiv" class= "tablelightfont">
                    <asp:Label ID="lblLastReply" runat="server" Text="Last reply by Sven on Aug 18,2011"></asp:Label>
                </div>
            </td>
            <td class="tablecontrolsfont" style = "padding-left:1px;">
                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/reply-icon.png" />
                <div>
                    <asp:Label ID="lblReplyCount" runat="server" Text="2 Replies"></asp:Label>
                </div>
            </td>
            <td class="tablecontrolsfont">
                <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/like-icon.png" />
                <div>
                    <asp:Label ID="lblLikesCount" runat="server" Text="1 likes"></asp:Label>
                </div>
            </td>
        </tr>
      <tr>
      <td colspan="4"><hr class="GrayLine" /></td>
      </tr>
        <tr>
            <td style="font-weight: bold; font-size: 11px; color: #2B86B0; padding:5px 8px; vertical-align:top;">
                <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/mem_img.JPG" />
            </td>
            <td class = "tablecontrolsfont">
                <asp:HyperLink ID="HyperLink1" runat="server" Text="New Features in Aptify v5.0 are usable"></asp:HyperLink>
                <br />
                <div id="Div1" class= "tablelightfont">
                    <asp:Label ID="Label2" runat="server" Text="Started by Erin Faulkner in Discussion."></asp:Label>
                </div>
                <div id="Div2" class= "tablelightfont">
                    <asp:Label ID="Label3" runat="server" Text="Last reply by Sven on Aug 18,2011"></asp:Label>
                </div>
            </td>
            <td class="tablecontrolsfont" style = "padding-left:1px;">
                <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/reply-icon.png" />
                <div>
                    <asp:Label ID="Label4" runat="server" Text="2 Replies"></asp:Label>
                </div>
            </td>
            <td class="tablecontrolsfont">
                <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/like-icon.png" />
                <div>
                    <asp:Label ID="Label5" runat="server" Text="1 likes"></asp:Label>
                </div>
            </td>
        </tr>
    <tr>
      <td colspan="4"><hr class="GrayLine" /></td>
          </tr>
        <tr>
            <td style="font-weight: bold; font-size: 11px; color: #2B86B0; padding:5px 8px; vertical-align:top;">
                <asp:Image ID="Image7" runat="server" ImageUrl="~/Images/mem_img.JPG" />
            </td>
            <td class = "tablecontrolsfont">
                <asp:HyperLink ID="HyperLink2" runat="server" Text="New Features in Aptify v5.0 are usable" ></asp:HyperLink>
                <br />
                <div id="Div3" class= "tablelightfont">
                    <asp:Label ID="Label6" runat="server" Text="Started by Erin Faulkner in Discussion."></asp:Label>
                </div>
                <div id="Div4" class= "tablelightfont">
                    <asp:Label ID="Label7" runat="server" Text="Last reply by Sven on Aug 18,2011"></asp:Label>
                </div>
            </td>
            <td class="tablecontrolsfont" style = "padding-left:1px;">
                <asp:Image ID="Image8" runat="server" ImageUrl="~/Images/reply-icon.png" />
                <div>
                    <asp:Label ID="Label8" runat="server" Text="2 Replies"></asp:Label>
                </div>
            </td>
            <td class="tablecontrolsfont">
                <asp:Image ID="Image9" runat="server" ImageUrl="~/Images/like-icon.png" />
                <div>
                    <asp:Label ID="Label9" runat="server" Text="1 likes"></asp:Label>
                </div>
            </td>
        </tr>
       
        <tr >
            <td colspan="2" 
                style="padding-top:10px;" 
                align="left" class = "tablecontrolsfont" >
               
                  <asp:HyperLink id="lnkAddDisc" runat="server" >
                   <div class="AddLink">Add Discussion</div>
                  </asp:HyperLink>

            </td>
           
            <td colspan="2" class = "tablecontrolsfont" >                
                <asp:HyperLink ID="linkViewAll" runat="server"><div class="ViewAllLink">View All</div></asp:HyperLink>
            </td>
         
        </tr>
    </table>
</div>
