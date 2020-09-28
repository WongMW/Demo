<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CommitteeTermMeetings__c.ascx.vb"
    Inherits="CommitteeTermMeetings__c" %>
<%@ Register Src="~/UserControls/Aptify_Custom__c/RecordAttachments__c.ascx" TagPrefix="uc2"
    TagName="RecordAttachments__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<style type="text/css">
    .auto-style1
    {
        width: 100%;
    }
</style>
<script language="javascript" type="text/javascript">
    function fnCheckUnCheck1(objId,id) {
        debugger;
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
    <table runat="server" id="tblMain" class="data-form">
        <tr>
            <td>
                <telerik:radgrid id="grdMain" runat="server" autogeneratecolumns="False" allowpaging="true"
                    sortingsettings-sorteddesctooltip="Sorted Descending" sortingsettings-sortedasctooltip="Sorted Ascending"
                    allowfilteringbycolumn="true"> 
               <GroupingSettings CaseSensitive="false" />  
               <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false"  >  
                   <Columns>
                    <telerik:GridTemplateColumn HeaderText="Select" AllowFiltering="false"    >
                                    <ItemTemplate>
                                    <asp:RadioButton  runat="server" id="RadioButton1"  AutoPostBack="true"   onCheckedChanged="rdSelect_CheckedChanged"   />
                                        <asp:Label ID="lblMeetingID" runat="server" Text='<%# Eval("MeetingID") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                    </telerik:GridTemplateColumn>                        
                      <%-- <Telerik:GridHyperLinkColumn Text="MeetingID" DataTextField="MeetingID" HeaderText="Meeting ID" SortExpression= "MeetingID" DataNavigateUrlFields="MeetingID" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />--%>
                       <Telerik:GridBoundColumn DataField="MeetingTitle" HeaderText="Meeting Title"   SortExpression= "MeetingTitle"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="StartDate" HeaderText="Start Date"   SortExpression= "StartDate"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                       <Telerik:GridBoundColumn DataField="EndDate" HeaderText="End Date"   SortExpression= "EndDate"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                   </Columns>
                   </MasterTableView>
               </telerik:radgrid>
            </td>
        </tr>
         <tr id="trRecordAttachment" runat="server" visible="false" >
            <td>
              <b>Documents</b><br />
                <asp:Panel ID="Panel1" runat="Server" Style="border: 1px Solid #000000;">
                    <table runat="server" id="Table2" class="data-form" width="100%">
                        <tr >
                            <td class="LeftColumn">
                                &nbsp;
                            </td>
                            <td class="RightColumn">
                                <uc2:RecordAttachments__c ID="RecordAttachments__c" runat="server" AllowView="True"
                                    AllowAdd="True" AllowDelete="false"    />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:label id="lblError" forecolor="Red" runat="server" visible="False" />
</div>
<cc1:user id="User1" runat="server" />
