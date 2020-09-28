<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Web_Articles/WebArticles.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.WebArticles" Debug="true" %>
<%@ Register Src="~/UserControls/SoftwareDesign_Aptify/Aptify_General/RSSFeed.ascx" TagName="RSSFeed" TagPrefix="uc1" %>

<table width="100%" border="0" runat="server" style = "margin-top:2em;">
    <tr class = "tableHeader">
        <td class="tableHeaderFont">
            <asp:Image runat="server" ID="img2" ImageUrl="~/Images/news-updates-icon.png" CssClass="MiddleImage" />
            News Updates &nbsp;
        </td>
        <td>
       <%-- Nalini 12429 13/12/2011--%>
         <%--   <asp:Image runat="server" ID="Image1" ImageUrl="~/Images/rss-icon.png"
                CssClass="MiddleImage" />--%>
                <uc1:RSSFeed ID="RSSFeed" runat="server" ChannelID='<%# GetChannelID() %>' RSSTitle="Web Articles"
                Visible='<%#IsRSSVisible() %>' />

        </td>
    </tr>

    <tr>
        <td >
            <asp:Repeater ID="lstArticles" runat="server">
                <ItemTemplate>
                    <table  width="100%">
                        <tr >
                            <td class="tablecontrolsfont" >
                                <asp:HyperLink ID="articleLink" runat="server"   ></asp:HyperLink>
                                <br />
                                <div id="StarterDiv" class="tablelightfont" >
                                    <asp:Label ID="lblDate" runat="server"></asp:Label>
                                   <hr class="GrayLine" />
                                </div>
                            </td>

                        </tr>
                   
                    </table>
                
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>
        </td>
    </tr>

    <tr>  
        <%--Dilip changes remove View link --%>
           <td colspan="2" class = "tablecontrolsfont" >                
              <%--  <asp:HyperLink ID="linkViewAll" runat="server"><div class="ViewAllLink">View All</div></asp:HyperLink>--%>
            </td>
         
  </tr>
</table>