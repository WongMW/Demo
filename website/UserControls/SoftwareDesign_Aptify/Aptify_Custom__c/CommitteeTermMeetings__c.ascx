<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/CommitteeTermMeetings__c.ascx.vb"
    Inherits="CommitteeTermMeetings__c" %>
<%@ Register Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Custom__c/RecordAttachments__c.ascx" TagPrefix="uc2"
    TagName="RecordAttachments__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<script type="text/javascript">
    function fnCheckUnCheck1(objId, id) {
        var grd = $get("<%= grdMain.ClientID %>");
        var rdoArray = grd.getElementsByTagName("input");
        for (i = 0; i <= rdoArray.length - 1; i++) {
            if (rdoArray[i].type == 'radio') {
                if (rdoArray[i].id != objId) {
                    rdoArray[i].checked = false;
                }
            }
        }
    }
</script>
<div class="content-container clearfix">
    <div runat="server" id="tblMain" class="cai-table mobile-table">
        <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
        <telerik:RadGrid ID="grdMain" runat="server" AutoGenerateColumns="False" AllowPaging="true"
            SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
            AllowFilteringByColumn="true">
            <GroupingSettings CaseSensitive="false" />
            <PagerStyle CssClass="sd-pager" />
            <MasterTableView AllowFilteringByColumn="false" AllowSorting="true" AllowNaturalSort="false">
                <Columns>
                    <telerik:GridTemplateColumn HeaderText="Select" AllowFiltering="false">
                        <ItemTemplate>
                            <span class="mobile-label">Select:</span>
                            <asp:RadioButton runat="server" CssClass="cai-table-data" ID="rdoSelect" AutoPostBack="true" OnCheckedChanged="rdSelect_CheckedChanged" />
                            <asp:Label ID="lblMeetingID" runat="server" Text='<%# Eval("MeetingID") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="MeetingTitle" HeaderText="Meeting title" SortExpression="MeetingTitle" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <ItemTemplate>
                            <span class="mobile-label">Meeting title:</span>
                            <asp:Label  CssClass="cai-table-data" runat="server" Text='<%# Eval("MeetingTitle")%>'></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="CommitteeTerm" HeaderText="Committee term" SortExpression="CommitteeTerm" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <ItemTemplate>
                            <span class="mobile-label">Committee term:</span>
                            <asp:Label CssClass="cai-table-data" runat="server" Text='<%# Eval("CommitteeTerm")%>'></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="StartDate" HeaderText="Start date" SortExpression="StartDate" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <ItemTemplate>
                            <span class="mobile-label">Start date:</span>
                            <asp:Label  CssClass="cai-table-data" runat="server" Text='<%# Eval("StartDate")%>'></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="EndDate" HeaderText="End date" SortExpression="EndDate" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="Contains" ShowFilterIcon="false">
                        <ItemTemplate>
                            <span class="mobile-label">End date:</span>
                            <asp:Label  CssClass="cai-table-data" runat="server" Text='<%# Eval("EndDate")%>'></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
        <br />

        <div id="trRecordAttachment" runat="server" visible="false" class="cai-form">

            <span class="form-title">Documents</span>
            <asp:Panel ID="Panel1" runat="Server" CssClass="cai-form-content">
                <div runat="server" id="Table2">
                    <uc2:RecordAttachments__c ID="RecordAttachments__c" runat="server" AllowView="True"
                        AllowAdd="True" AllowDelete="false" />
                </div>
            </asp:Panel>
        </div>
        <div class="sfContentBlock"><div class="button-block style-1">
            <a style="text-decoration: none;" class="btn-full-width btn" href="/Committees/mycommittees.aspx"><em class="fa fa-arrow-left" aria-hidden="true"></em> Committees</a>
        </div>
    </div>
    <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="False" />
</div>
</div>
<cc1:User ID="User1" runat="server" />
