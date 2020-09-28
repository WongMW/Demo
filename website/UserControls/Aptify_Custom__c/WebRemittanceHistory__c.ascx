<%--Aptify e-Business 5.5.1, July 2013--%>
<%@ Control Language="VB" AutoEventWireup="false" CodeFile="WebRemittanceHistory__c.ascx.vb"
    Debug="true" Inherits="Aptify.Framework.Web.eBusiness.CustomerService.WebRemittanceHistory__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="content-container clearfix" id="divTop" runat="server">
    <p>
        <asp:UpdatePanel ID="updPanelGrid" runat="server">
            <ContentTemplate>
                <div>
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
                    <rad:RadGrid ID="gvWebRemittanceHistory" runat="server" AutoGenerateColumns="False"
                        AllowPaging="true" AllowFilteringByColumn="true" PageSize="10" AllowSorting="true">
                        <MasterTableView>
                            <NoRecordsTemplate>
                                No History Available.
                            </NoRecordsTemplate>
                            <Columns>
                                <telerik:GridDateTimeColumn DataField="OrderDate" HeaderText="Order Date" FilterControlWidth="100%"
                                    HeaderStyle-Width="25%" DataFormatString="{0:d}" SortExpression="OrderDate" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="EqualTo" ShowFilterIcon="false" EnableTimeIndependentFiltering="true"
                                    ItemStyle-HorizontalAlign="Left" />
                                <rad:GridHyperLinkColumn Text="WebRemittanceNo" DataNavigateUrlFields="WebRemittanceNo__c"
                                    DataTextField="WebRemittanceNo__c" FilterControlWidth="80%" HeaderText="Remittance #"
                                    SortExpression="WebRemittanceNo__c" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                    ShowFilterIcon="false" />
                                <rad:GridBoundColumn DataField="TotalAmount" HeaderText="Total Amount" SortExpression="TotalAmount"
                                    FilterControlWidth="80%" DataFormatString="{0:0.00}" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="EqualTo" ShowFilterIcon="false" />
                                    <%--Govind Mande added 27042016--%>
                                    <rad:GridBoundColumn DataField="OutstandingBalance" HeaderText="Outstanding Balance" SortExpression="OutstandingBalance"
                                    FilterControlWidth="80%" DataFormatString="{0:0.00}" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="EqualTo" ShowFilterIcon="false" />
                            </Columns>
                        </MasterTableView>
                    </rad:RadGrid>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </p>
    <cc1:User runat="server" ID="User1" />
</div>
