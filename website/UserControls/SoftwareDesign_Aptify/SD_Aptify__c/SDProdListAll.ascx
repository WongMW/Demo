<%@ control language="C#" autoeventwireup="true" codebehind="SDProdListAll.ascx.cs" inherits="SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c.SDProdListAll" %>
<%@ register tagprefix="rad" namespace="Telerik.Web.UI" assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>

<asp:panel id="productPnl" runat="server" >
 <div class="category-products">
        <asp:UpdatePanel ID="UpdatePanelgrdMain" runat="server">
            <ContentTemplate>
                <rad:RadGrid ID="ProdGrid" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                    ShowFooter="false" EnableTheming="true" EmptyDataText="No Products to display" >
                    <PagerStyle CssClass="sd-pager" />
                    <MasterTableView>
                        <Columns>
                            <rad:GridTemplateColumn ItemStyle-HorizontalAlign="left" ItemStyle-CssClass="product-listing">
                                <ItemTemplate>
                                      <div class="title-link">
                                       <asp:HyperLink CssClass="product-name" ID="lnkProduct" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebName") %>' Font-Bold="true"></asp:HyperLink> 
                                       </div>
                                    <asp:Label ID="lblDescription" runat="server" Visible="True" Text='<%# DataBinder.Eval(Container.DataItem,"WebDescription") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn ItemStyle-CssClass="product-listing">
                                <ItemTemplate>
                                     <asp:HyperLink NavigateUrl='<%# Eval("ID","~/ProductCatalog/Product.aspx?ID={0}") %>' ID="btnAddToCart" CssClass="submitBtn btn-full-width" runat="server" Text="VIEW MORE"></asp:HyperLink>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </rad:RadGrid>
            </ContentTemplate>
        </asp:UpdatePanel>
     
    <cc2:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="False"></cc2:AptifyShoppingCart>
</div>
</asp:panel>
