<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Product_Catalog/ProductTopicCodesGrid.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.ProductTopicCodesGrid" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="productTopicCodesGrid skills-topiccodes" style="margin-top:18px">
    <%--<h4 runat="server" class="textfont">Related content</h4>--%>
    <div class="content-container clearfix plain-table">
        <%--Navin Prasad Issue 11032--%>
        <%--Nalini Issue 12436 date:01/12/2011--%>
        <h4 style="margin-top:18px">How this relates to your career pathway:</h4>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <%--Neha Changes for Issue 14456--%>
                <rad:RadGrid ID="grdMain" AutoGenerateColumns="False" runat="server" AllowPaging="false" AllowFilteringByColumn="false" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending">
                    <GroupingSettings CaseSensitive="false" />
                    <PagerStyle CssClass="sd-pager" />
                    <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false" CssClass="topic-code-control-table" Style="border:none!important">
                        <Columns>                            
                            <rad:GridBoundColumn DataField="Name" HeaderText=" " SortExpression="Name" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" HeaderStyle-Width="20%"  FilterControlWidth="80%" ItemStyle-CssClass="topic-code-control-data" />

                            <rad:GridBoundColumn DataField="Description" HeaderText=" " SortExpression="" AutoPostBackOnFilter="true" HeaderStyle-Width="250px"  CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowSorting="false" ItemStyle-CssClass="topic-code-control-data" FilterControlWidth="80%" />
                            <rad:GridBoundColumn Visible="False" DataField="ID" />
                        </Columns>
                    </MasterTableView>
                </rad:RadGrid>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
