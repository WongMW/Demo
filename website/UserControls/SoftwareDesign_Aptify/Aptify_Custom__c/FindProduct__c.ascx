<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/FindProduct__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.FindProductControl__c" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<div>
    <%--Nalini Issue 12436 date:01/12/2011--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlmain" runat="server" DefaultButton="cmdSearch">
                <div id="tblMain" runat="server" class="data-form" border="0">
                    <div class="aptify-search-box">
                        <asp:TextBox ID="txtName" runat="server" class="aptify-search-box" />
                        <asp:TextBox ID="txtDescription" runat="server" Visible="false" />
                        <asp:Button CssClass="button button-search fa fa-search fa-lg" ID="cmdSearch" runat="server" Text="&#xf002;"></asp:Button>
                    </div>
                    <div class="error-message">
                        <asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>
                    </div>

                    <div runat="server" id="trNoResults">
                        <p>
                            The system could not locate any matching records. Please try again.
                        </p>
                    </div>
                </div>
            </asp:Panel>
<br />
            <div id="trResults" runat="server" class="cai-table">
                <%--Neha Changes for Issue 14456--%>
                <rad:RadGrid ID="grdResults" runat="server" AutoGenerateColumns="False" AllowPaging="true" AllowFilteringByColumn="false" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending">
                    <GroupingSettings CaseSensitive="false" />
                    <PagerStyle CssClass="sd-pager" />
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView AllowFilteringByColumn="false" AllowSorting="true" AllowNaturalSort="false">
                        <Columns>
                            <rad:GridTemplateColumn HeaderText="Product" DataField="WebName" FilterListOptions="VaryByDataType" SortExpression="WebName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="50%">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkWebName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebName") %>' onclick='<%# GetGtmObject(Container.DataItem)  %>'></asp:Hyperlink> 
                                    <%-- #21000 <asp:HyperLink ID="lnkWebName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebName") %>'
                                        NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"ViewProductPageUrl") %>'
                                                   onclick='<%# GetGtmObject(Container.DataItem)  %>' ></asp:Hyperlink> --%>
                                </ItemTemplate>
                                <ItemStyle Width="50%" />
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Description" DataField="WebDescription" FilterListOptions="VaryByDataType" SortExpression="" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="50%" Visible="false">
                                <ItemTemplate>
                                    <asp:Literal ID="ltDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"WebDescription") %>'></asp:Literal>
                                </ItemTemplate>
                                <ItemStyle Width="50%" />
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Category" DataField="Category" FilterListOptions="VaryByDataType" SortExpression="Category" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="50%" Visible="false">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkCategory" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Category") %>'
                                        NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"ViewProductCatagoryPageUrl") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle Width="50%" />
                            </rad:GridTemplateColumn>
                            <%--added for Redmine #18413 Govind M--%>
                             <rad:GridTemplateColumn HeaderText="StartDate" DataField="StartDate" FilterListOptions="VaryByDataType" SortExpression="StartDate" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="50%" >
                                <ItemTemplate>
                                    <asp:Label ID="lblStartDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StartDate")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="50%" />
                            </rad:GridTemplateColumn>
                              <%--End for Redmine #18413 Govind M--%>
                        </Columns>
                    </MasterTableView>
                </rad:RadGrid>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<cc1:User ID="User1" runat="server" />
<asp:Panel ID="Panel1" runat="server" Visible="false">
    <td valign="middle">
        <asp:DropDownList runat="server" ID="cmbCategory" DataTextField="WebName" DataValueField="ID"
            >
        </asp:DropDownList>
    </td>
</asp:Panel>
