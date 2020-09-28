<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Custom.CommitteeTermControl__c"
    CodeFile="CommitteeTerm__c.ascx.vb" %>
<%@ Register Src="../Aptify_General/RecordAttachments.ascx" TagName="RecordAttachments"
    TagPrefix="uc2" %>
<%@ Register Src="../Aptify_Forums/SingleForum.ascx" TagName="SingleForum" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEbusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="content-container clearfix">
    <table id="tblMain" runat="server" class="data-form">
        <tr>
            <td colspan="2">
                <asp:Label ID="lblCommittee" runat="server" CssClass="CommitteeName"></asp:Label><br />
                <asp:Label ID="lblTerm" runat="server" CssClass="CommitteeTermName"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="CommitteeTermLeft">
                <table>
                    <tr>
                        <td>
                            <img runat="server" id="generalSmall" src="" alt="General Info" />
                            <asp:HyperLink runat="server" ID="lnkGeneral" Text="General" ToolTip="View general information about the committee term" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img runat="server" id="memberSmall" src="" alt="Member List" />
                            <asp:HyperLink runat="server" ID="lnkMembers" Text="Members" ToolTip="View members in this committee term" />
                        </td>
                    </tr>
                    <tr runat="server" id="trForum">
                        <td>
                            <img runat="server" id="forumSmall" src="" alt="Forum" />
                            <asp:HyperLink runat="server" ID="lnkForum" Text="Forum" ToolTip="Discussion forum with committee members" />
                        </td>
                    </tr>
                    <tr runat="server" id="trDocuments">
                        <td>
                            <img runat="server" id="documentsSmall" src="" alt="Documents" />
                            <asp:HyperLink runat="server" ID="lnkDocuments" Text="Documents" ToolTip="Document Library for committee members" />
                        </td>
                    </tr>
                        <tr runat="server" id="trTermMeeting">
                        <td>
                          <img runat="server" id="MeetingSmall" src="" alt="Meetings" />
                           <asp:linkbutton runat="server" id="lnkMeeting">Meetings</asp:linkbutton>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="CommitteeTermRight" runat="server" id="tdExtContent">
                <img runat="server" id="imgTitle" src="" alt="Committee Information" align="absmiddle" />
                <asp:Label runat="server" CssClass="CommitteeTermName" ID="lblTitle" /><br />
                <asp:Label runat="server" ID="lblDetails" /><br />
                <div runat="server" id="pnlGeneral" visible="false">
                    <table>
                        <tr>
                            <td>
                                <b>Director</b>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblDirector" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Start Date</b>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblStartDate" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>End Date</b>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblEndDate" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Goals</b>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblGoals" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Accomplishments</b>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblAccomplishments" />
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:UpdatePanel ID="updPanelGrid" runat="server">
                    <ContentTemplate>
                        <rad:RadGrid ID="grdMembers" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                            AllowFilteringByColumn="false" Visible="false" SortingSettings-SortedDescToolTip ="Sorted Descending" SortingSettings-SortedAscToolTip="Sort Ascending">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AllowSorting="true" AllowNaturalSort="false">
                                <Columns>
                                    <rad:GridBoundColumn DataField="Member" HeaderText="Member" SortExpression="Member"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                    <rad:GridBoundColumn DataField="Title" HeaderText="Title" SortExpression="Title"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                    <rad:GridDateTimeColumn DataField="StartDate" UniqueName="GridDateTimeColumnStartDate" HeaderText="Start" 
                                        SortExpression="StartDate" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                        ShowFilterIcon="false" EnableTimeIndependentFiltering ="true" DataType="System.DateTime" />
                                    <rad:GridDateTimeColumn DataField="EndDate" UniqueName="GridDateTimeColumnEndDate" HeaderText="End" 
                                        SortExpression="EndDate" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                        ShowFilterIcon="false" EnableTimeIndependentFiltering="true" DataType="System.DateTime" />
                                    <rad:GridHyperLinkColumn DataNavigateUrlFields="Email1" DataNavigateUrlFormatString="mailto:{0}"
                                        DataTextField="Email1" HeaderText="Email" SortExpression="Email1" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                    <rad:GridBoundColumn DataField="PrefMobileNumber" HeaderText="Pref. Mobile No." SortExpression="PrefMobileNumber"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                    <rad:GridTemplateColumn HeaderText="Is Council Member" AllowFiltering="false">
                                      <ItemTemplate>
                                         <%--<asp:Label ID="lblIsCouncilMember" runat="server" Text='<%#Bind("IsCouncilMember")  %>'></asp:Label>--%>
                                         <asp:CheckBox ID="chkIsCouncilMember" runat="server" Checked='<%#Bind("IsCouncilMember")  %>'  Enabled="false"/>
                                      </ItemTemplate>
                                    </rad:GridTemplateColumn>  
                                </Columns>
                            </MasterTableView>
                        </rad:RadGrid>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <uc1:SingleForum ID="SingleForum1" runat="server" Visible="false" />
                <uc2:RecordAttachments ID="RecordAttachments" Visible="false" runat="server"></uc2:RecordAttachments>
            </td>
        </tr>
    </table>
    <cc3:User runat="server" ID="User1" />
</div>
