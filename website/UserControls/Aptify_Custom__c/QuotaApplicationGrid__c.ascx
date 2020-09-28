<%@ Control Language="VB" AutoEventWireup="false" CodeFile="QuotaApplicationGrid__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.CompanyAdministrator.QuotaApplicationGrid__c" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="info-data">
      <div class="row-div clearfix">
        <div class="label-div w10" >
            <asp:Button ID="btnNewQuotaApp" runat="server" Text="New Quota Application" />
        </div>
        </div> 
    <div class="row-div clearfix">
      <%--  <div class="label-div w30">
            <asp:Button ID="btnNewQuotaApp" runat="server" Text="New Quota Application" />
        </div>--%>
    <asp:Label ID="lblQuataAppError" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
       
        <div class="field-div1 w100">
            <telerik:RadGrid ID="radQuotaApplication" runat="server" AutoGenerateColumns="False"
                AllowPaging="false" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                AllowFilteringByColumn="false"  AllowSorting="false" Width="150px">
                <GroupingSettings CaseSensitive="false" />
                <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false">
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="Application ID" AllowFiltering="false" ItemStyle-Width="10%">
                            <ItemTemplate >
                                <asp:LinkButton ID="lnkQuatAppID" runat="server" ForeColor="Blue" 
                                            CommandName="QuotaApp" Font-Underline="true" CommandArgument='<%# Eval("QuotaAppID")%>' Text='<%# Eval("QuotaAppID")%>'></asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Status" HeaderText="Status" SortExpression="Status"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn DataField="StartDate" HeaderText="Start Date" SortExpression="StartDate"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                              <telerik:GridBoundColumn DataField="EndDate" HeaderText="End Date" SortExpression="EndDate"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                              <telerik:GridBoundColumn DataField="ASSIGNEDQUOTA" HeaderText="Quota" SortExpression="ASSIGNEDQUOTA"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                            <telerik:GridBoundColumn DataField="FIRSTLAST" HeaderText="Requested By" SortExpression="FIRSTLAST"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                            FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%" />
                               <telerik:GridTemplateColumn HeaderText="Active" AllowFiltering="false">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkActive" runat="server"   Checked='<%#IsChecked(Eval("Active"))%>' Enabled="false"/>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
    </div>
</div>
<cc3:User ID="User1" runat="server" />
