<%--Aptify e-Business 5.5.1, July 2013--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/WebRemittanceHistory__c.ascx.vb"
    Debug="true" Inherits="Aptify.Framework.Web.eBusiness.CustomerService.WebRemittanceHistory__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<style>
    .RadGrid a {
    background-color: #fff; 
    border: 2px solid #003D51;
    color: #003D51;
    padding: 10px 20px;
    text-align: center;
    text-decoration: none;
    display: inline-block;
}
     .RadGrid a:hover {
      background-color: #003D51;
      color: white;
      border: 2px solid #003D51;
    }

</style>
<div class="content-container clearfix" id="divTop" runat="server">
    <p>
        <%--code Added by Saurabh  21277--%>
        <%-- Change the update mode7--%>
        <%--code Added by Saurabh  21277--%>
        <asp:UpdatePanel ID="updPanelGrid" runat="server" UpdateMode="Always" ChildrenAsTriggers="True">
            <ContentTemplate>
                <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="updPanelGrid">
                    <ProgressTemplate>
                        <div class="dvProcessing">
                            <div class="loading-bg">
                                <img src="/Images/CAITheme/bx_loader.gif" />
                                <span>Please wait...<br />
                                   
                                </span>
                            </div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <div class="cai-table">
                    <div>
                        <asp:HiddenField ID="hfCompanyID" runat="server" Value="0" />
                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="info-data">
                        <div class="row-div clearfix" runat="server">
                            <div class="label-div w10">
                                <asp:Label ID="lblSubsidiaries" runat="server" Text="Subsidiaries"></asp:Label>
                            </div>
                            <div class="field-div1 w50">
                                <asp:DropDownList ID="ddlSubsidiariesList" runat="server" AutoPostBack="true" Width="50%">
                                </asp:DropDownList>
                            </div>
                            <div class="label-div w10">
                            </div>
                        </div>
                    </div>
                    <%--code Added by Saurabh  21277--%>
                    <%-- Added event --%>
                    <%--code Added by Saurabh  21277--%>
                    <rad:RadGrid ID="gvWebRemittanceHistory" runat="server" AutoGenerateColumns="False" ShowStatusBar="true" 
                        AllowPaging="true" AllowFilteringByColumn="false" PageSize="10" AllowSorting="true" OnItemCommand="gvWebRemittanceHistory_ItemCommand">
                        <PagerStyle CssClass="sd-pager" />
                        <MasterTableView>
                            <NoRecordsTemplate>
                                Billing Details are grouped under Head Office.
                            </NoRecordsTemplate>
                            <Columns>
                                <telerik:GridDateTimeColumn DataField="OrderDate" DataFormatString="{0:M/d/yyyy}" HeaderText="Order date" FilterControlWidth="100%"
                                    HeaderStyle-Width="25%" SortExpression="OrderDate" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="EqualTo" ShowFilterIcon="false" EnableTimeIndependentFiltering="true"
                                    ItemStyle-HorizontalAlign="Left" />

                                <rad:GridHyperLinkColumn Text="Pay for" DataNavigateUrlFields="WebRemittanceNo__c" 
                                    DataTextField="WebRemittanceNo__c" FilterControlWidth="80%" HeaderText="Remittance #"
                                    SortExpression="WebRemittanceNo__c" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                    ShowFilterIcon="false" ItemStyle-CssClass="RadGrid"/>
                                <rad:GridBoundColumn DataField="TotalAmount" HeaderText="Total amount" SortExpression="TotalAmount" DataFormatString="{0:0.00}"
                                    FilterControlWidth="80%" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false" />
                                    <%--Govind Mande added 27042016--%>
                                    <rad:GridBoundColumn DataField="OutstandingBalance" HeaderText="Outstanding balance" SortExpression="OutstandingBalance"
                                    FilterControlWidth="80%" DataFormatString="{0:0.00}" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="EqualTo" ShowFilterIcon="false" />
                                
                                <%--column Added by Saurabh  21277--%>
                                <rad:GridBoundColumn DataField="ShipToCompany" HeaderText="Ship To Company" SortExpression="ShipToCompany"
                                    FilterControlWidth="80%" DataFormatString="{0:0.00}" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="EqualTo" ShowFilterIcon="false" />                                
                                <rad:GridTemplateColumn HeaderText="Print Invoice">
                                    <ItemTemplate>
                                        <asp:Button CssClass="submitBtn" ID="btnInvoiceReport" runat="server" Text="Print Invoice" CommandArgument='<%# Eval("WebRemittanceNo__c")%>'
                                            CommandName="InvoiceReport" />
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>

                                <%--code Added by Saurabh  21277--%>
                            </Columns>
                        </MasterTableView>
                        <%--code Added by Saurabh  21277--%>
                        <ClientSettings EnablePostBackOnRowClick="true">
                            <Scrolling AllowScroll="true" UseStaticHeaders="true"></Scrolling>
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                        <%--code Added by Saurabh  21277--%>
                    </rad:RadGrid>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </p>
    <cc1:User runat="server" ID="User1" />
</div>
