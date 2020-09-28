<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Product_Catalog/ProductGroupingContentsGrid.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.ProductGroupingContentsGrid" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<h3><asp:Label ID="lblTitle" runat="server" Text="ProductNamePlaceHolder's Contents"></asp:Label></h3> <%-- WongS Modified as part of #20438 --%>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <%--Neha Changes for Issue 14456--%>
        <rad:RadGrid ID="grdMain" AutoGenerateColumns="False" runat="server" AllowSorting="false"
            AllowPaging="true" DataKeyNames="ID" AllowFilteringByColumn="false" CssClass="kit-product"> <%-- WongS Modified as part of #20438 --%>
            <PagerStyle CssClass="sd-pager" />
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView AllowFilteringByColumn="false" AllowNaturalSort="false">
                <Columns>
                    <rad:GridTemplateColumn HeaderText="Product" AutoPostBackOnFilter="true" DataField="WebName"
                        CurrentFilterFunction="Contains" ShowFilterIcon="false" SortExpression="WebName"
                        FilterControlWidth="0%" AllowFiltering="false">
                        <ItemTemplate>
                            <asp:HyperLink ID="lnkWebName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebName") %>'
                                NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"WebNameUrl") %>'></asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle/> <%-- WongS Modified as part of #20438 --%>
                    </rad:GridTemplateColumn>
                    <rad:GridTemplateColumn HeaderText="Description" AutoPostBackOnFilter="true" DataField="WebDescription"
                        CurrentFilterFunction="Contains" ShowFilterIcon="false" SortExpression="" FilterControlWidth="0%" AllowFiltering="false">
                        <ItemTemplate>
                            <asp:Label ID="lblWebDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebDescription") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle/> <%-- WongS Modified as part of #20438 --%>
                    </rad:GridTemplateColumn>
                    <rad:GridTemplateColumn HeaderText="Quantity" AllowFiltering="false" Visible="false"> <%-- WongS Modified as part of #20438 --%>
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity","{0:n0}" ) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Right" />
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Right" />
                        <HeaderStyle Font-Bold="true" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False"/> <%-- WongS Modified as part of #20438 --%>
                    </rad:GridTemplateColumn>
                    <rad:GridTemplateColumn HeaderText="Price" AllowFiltering="false">
                        <ItemTemplate>
                            <asp:Label ID="lblPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PriceUrl") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Right" />
                        <%--<HeaderStyle HorizontalAlign="Right" />--%> <%-- WongS Modified as part of #20438 --%>
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Right" />
                        <HeaderStyle Font-Bold="true" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False"/> <%-- WongS Modified as part of #20438 --%>
                    </rad:GridTemplateColumn>
                  	<%--Added as part of #20508--%>
                    <rad:GridTemplateColumn HeaderText="Member price" AllowFiltering="false" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblMemberPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MemberPriceUrl") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Right" />
                        <%--<HeaderStyle HorizontalAlign="Right" />--%> <%-- WongS Modified as part of #20438 --%>
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Right" />
                        <HeaderStyle Font-Bold="true" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False"/> <%-- WongS Modified as part of #20438 --%>
                    </rad:GridTemplateColumn>
                        
                    <rad:GridTemplateColumn HeaderText="ProductID" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblProductID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ProductID") %>'></asp:Label>
                        </ItemTemplate>
                    </rad:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </rad:RadGrid>
    </ContentTemplate>
</asp:UpdatePanel>
