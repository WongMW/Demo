<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Chapters/Reports.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Chapters.ReportsControl" %>
<%@ Register TagPrefix="cc4" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessHierarchyTree" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %> 
<%@ Register TagPrefix="radTree" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<div class="content-container clearfix">
    <table id="tblMain" runat="server" class="data-form">
        <tr>
            <td colspan="2">
                <asp:LinkButton ID="lnkChapter" runat="server">Go To Chapter</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td class="LeftColumn">
                <radTree:RadTreeView ID="trvReports" runat="server" Margin="8" CheckBoxes="False" OnNodeClick="trvReports_NodeClicked" 
                    ClientIDMode="Static" ExpandAnimation-Type="None" CausesValidation="false" CssClass="for-tree-structure-without-leftpadding" >
                    <NodeTemplate>
                        <asp:Label ID="lblReport" runat="server"></asp:Label> 
                    </NodeTemplate>
                </radTree:RadTreeView> 
            </td>
            <td>
         <%--   Navin Prasad Issue 11032--%>
                <%--<asp:DataGrid ID="grdReports" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:ButtonColumn Text="Report" ItemStyle-ForeColor="Blue" DataTextField="Name" HeaderText="Report"
                            CommandName="Select"></asp:ButtonColumn>
                        <asp:BoundColumn DataField="Description" HeaderText="Description"></asp:BoundColumn>
                        <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID"></asp:BoundColumn>
                    </Columns>
                    <PagerStyle Mode="NumericPages"></PagerStyle>
                </asp:DataGrid>--%>
                <%--Suvarna D IssueID: 12436 on Dec 1, 2011 added Update Panel --%>
                <%--   'Navin Prasad Issue 11145--%>
                <asp:UpdatePanel ID="updPanelGrid" runat="server">
                <ContentTemplate>
                <asp:GridView ID="grdReports" runat="server" AutoGenerateColumns="False">
                    <Columns>
                      <asp:TemplateField HeaderText="Report">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnkReport" runat="server"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--   <asp:ButtonField Text="Report" ItemStyle-ForeColor="Blue" DataTextField="Name" HeaderText="Report"
                            CommandName="Select" CausesValidation="false"  />--%>
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:TemplateField Visible="false"   HeaderText="ID">
                        <ItemTemplate>
                        <asp:Label ID="lblID" runat="server" text= '<%#Eval("ID") %>' ></asp:Label>
                        </ItemTemplate>
                        
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                </ContentTemplate>
                  <Triggers>
                <asp:AsyncPostBackTrigger ControlID = "grdReports" EventName="PageIndexChanging" />
                </Triggers>
                </asp:UpdatePanel>
                <%--End of addition by Suvarna D IssueID: 12436 --%>

                
            </td>
        </tr>
    </table>
    <cc3:User ID="User1" runat="server" />
</div>
