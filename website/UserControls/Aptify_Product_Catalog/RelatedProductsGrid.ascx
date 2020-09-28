<%@ Control Language="VB" AutoEventWireup="false" CodeFile="RelatedProductsGrid.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.RelatedProductsGrid" %>
    <div style="padding-bottom:10px;">
<h6 runat="server" class="textfontsub">Related Products</h6>
</div>
<div id="divGrid" runat="server" class="DivGrid clearfix" >
    <%--Suvarna Deshmukh IssueID-12433,12430 and 12434 On Dec 13,2011 commented and added to implement new designs for ebusiness--%>
       <asp:DataList ID="grdMain" runat="server" HorizontalAlign="Left">
                <ItemTemplate>
                    <table width="100%">
                        <tr>
                            <td valign="middle" align="center" style="width:20%; height:100px;">
                                <asp:Image ID="ImgProd" runat="server" CssClass="Image" />
                             </td>
                             <%--<td style="width:5%;"></td>--%>
                             <td align="left" valign="top" style="width:50%; font-size:12px; ">
                           <div>
                        <asp:HyperLink ID="lnkProduct" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebName") %>'
                            Font-Bold="true"></asp:HyperLink>
                    </div>
                    <asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"GridDescription") %>' Font-Size="12px" ></asp:Label>
                    <%--Rashmi P, Issue 13150,8/21/12, The prompt text is not displayed for related product  --%>
                                <div>
                                <asp:Label ID="lblWebPrompttext" runat = "server" Text ='<%# databinder.eval(container.dataitem,"PromptText") %>'></asp:Label>
                             </div>
                             </td>
                        </tr>
                        <tr><td style="width:35%;"></td></tr>
                         
                    </table>
                   
                         </ItemTemplate>
                           <%-- Commemted by Suvarna To remove View All link from page
                           <FooterTemplate>
                         <asp:Label ID = "lblViewAll" runat="server" Text="View All" ></asp:Label>
                         <a id="A1" href="#" runat="server"><img id="imgViewAll" alt="" src="~/Images/view-all-icon-hover.png" runat="server" /></a>
                         </FooterTemplate>
                         <FooterStyle HorizontalAlign="Right" ForeColor="#71582d" />--%>

            </asp:DataList>
        <%--End of Addtion by Suvarna Deshmukh IssueID-12433,12430 and 12434 On Dec 13,2011 commented and added to implement new designs for ebusiness--%>

<%-- Navin Prasad Issue 11032--%>
     <%--Nalini Issue 12436 date:01/12/2011--%>
  <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
     <asp:GridView ID="grdMain" AutoGenerateColumns="False" runat="server">
        <SelectedRowStyle />
        <AlternatingRowStyle />
        <RowStyle />
        <HeaderStyle />
        <FooterStyle />
        <Columns>
            <asp:HyperLinkField DataNavigateUrlFields="ProductID" DataTextField="WebName" HeaderText="Product" />
            <asp:BoundField DataField="PromptText" HeaderText="Description" />
            <asp:BoundField DataField="Relationship" HeaderText="Relationship" />
            <asp:BoundField Visible="False" DataField="ProductID" HeaderText="ID" />
        </Columns>
    </asp:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="grdMain" EventName="PageIndexChanging" />
        </Triggers>
    </asp:UpdatePanel>


<%--<asp:DataGrid id="grdMain1" AutoGenerateColumns="False" runat="server" >
	<SelectedItemStyle></SelectedItemStyle>
	<AlternatingItemStyle></AlternatingItemStyle>
	<ItemStyle></ItemStyle>
	<HeaderStyle></HeaderStyle>
	<FooterStyle></FooterStyle>
	<Columns>
		<asp:HyperLinkColumn DataNavigateUrlField="ProductID" DataTextField="WebName" HeaderText="Product"></asp:HyperLinkColumn>
		<%--HP Issue#8706: replacing web description to the WebPrompt text entered for the relationship-%>
		<%--<asp:BoundColumn DataField="GridDescription" HeaderText="Description"></asp:BoundColumn>--%>
		<%--<asp:BoundColumn DataField="PromptText" HeaderText="Description"></asp:BoundColumn>--%>
		<%--<%--HP Issue#8793: replacing promtText to actual Relationship type--%>
		<%--<asp:BoundColumn DataField="PromptText" HeaderText="Relationship"></asp:BoundColumn>--%>
		<%--<asp:BoundColumn DataField="Relationship" HeaderText="Relationship"></asp:BoundColumn>
		<asp:BoundColumn Visible="False" DataField="ProductID" HeaderText="ID"></asp:BoundColumn>
	</Columns>
	<PagerStyle Mode="NumericPages"></PagerStyle>
</asp:DataGrid>--%>

</div>
