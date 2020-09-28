<%@ Control Language="vb" AutoEventWireup="false" Inherits="Aptify.Framework.Web.eBusiness.Custom.CommitteeTermControl__c"
    CodeFile="~/UserControls/Aptify_Custom__c/CommitteeTerm__c.ascx.vb" %>
<%@ Register Src="../Aptify_General/RecordAttachments.ascx" TagName="RecordAttachments"
    TagPrefix="uc2" %>
<%@ Register Src="../Aptify_Forums/SingleForum.ascx" TagName="SingleForum" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEbusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="content-container clearfix">
    <table id="tblMain" runat="server" class="cai-table">
        <tr>
            <td colspan="2">
              <h2> <asp:Label ID="lblCommittee" runat="server" CssClass="CommitteeName"></asp:Label><br />
                <asp:Label ID="lblTerm" runat="server" CssClass="CommitteeTermName"></asp:Label></h2> 
            </td>
        </tr>
        <tr>
            <td class="CommitteeTermLeft">
                <table width="200px">

                    <tr>
                        <td>
                    <div class="aptify-category-inner-side-nav">
                    <div id="divMenu" runat="server">
                        <h6 style="width:200px">Committees</h6>
                        <ul>
                            <li style="width:200px"><asp:LinkButton runat="server" ID="lnkMeeting">Meetings</asp:LinkButton></li>
                            <li style="width:200px"><asp:HyperLink runat="server" ID="lnkGeneral" Text="General" ToolTip="View general information about the committee term" /></li>
                            <li style="width:200px"> <asp:HyperLink runat="server" ID="lnkMembers" Text="Members" ToolTip="View members in this committee term" /></li>
                            <li style="width:200px; display:none;"><asp:HyperLink runat="server" ID="lnkForum" Text="Forum" ToolTip="Discussion forum with committee members" /></li>
                            <li style="width:200px"><asp:HyperLink runat="server" ID="lnkDocuments" Text="Documents" ToolTip="Document library for committee members" /></li>
                            
                            </ul>
                        </div>
                        </div>

                        </td>

                    </tr>
                    <tr style="display:none;">
                        <td>
                            <img runat="server" id="generalSmall" src="" alt="General info" />
                            <asp:HyperLink runat="server" ID="lnkGeneral1" Text="General" ToolTip="View general information about the committee term" />
                        </td>
                    </tr>
                    <tr style="display:none;">
                        <td>
                            <img runat="server" id="memberSmall" src="" alt="Member list" />
                            <asp:HyperLink runat="server" ID="lnkMembers1" Text="Members" ToolTip="View members in this committee term" />
                        </td>
                    </tr>
                    <tr runat="server" id="trForum" style="display:none;">
                        <td>
                            <img runat="server" id="forumSmall" src="" alt="Forum" />
                            <asp:HyperLink runat="server" ID="lnkForum1" Text="Forum" ToolTip="Discussion forum with committee members" />
                        </td>
                    </tr>
                    <tr runat="server" id="trDocuments" style="display:none;">
                        <td>
                            <img runat="server" id="documentsSmall" src="" alt="Documents" />
                            <asp:HyperLink runat="server" ID="lnkDocuments1" Text="Documents" ToolTip="Document Library for committee members" />
                        </td>
                    </tr>
                    <tr runat="server" id="trTermMeeting" style="display:none;">
                        <td>
                            <img runat="server" id="MeetingSmall" src="" alt="Meetings" />
                            <asp:LinkButton runat="server" ID="lnkMeeting1">Meetings</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="CommitteeTermRight" runat="server" id="tdExtContent">
                <img runat="server" id="imgTitle" src="" alt="Committee information" align="absmiddle" />
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
                                <b>Start date</b>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblStartDate" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>End date</b>
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
                        <tr style="display:none;">
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
                            AllowFilteringByColumn="false" Visible="false" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sort Ascending">
                            <GroupingSettings CaseSensitive="false" />
                            <PagerStyle CssClass="sd-pager" />
                            <MasterTableView AllowSorting="true" AllowNaturalSort="false">
                                <Columns>
                                    <rad:GridBoundColumn DataField="Member" HeaderText="Member" SortExpression="Member"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                    <rad:GridBoundColumn DataField="Title" HeaderText="Title" SortExpression="Title"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                    <rad:GridDateTimeColumn DataField="StartDate" UniqueName="GridDateTimeColumnStartDate" HeaderText="Start"
                                        SortExpression="StartDate" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                        ShowFilterIcon="false" EnableTimeIndependentFiltering="true" DataType="System.Date" />
                                    <rad:GridDateTimeColumn DataField="EndDate" UniqueName="GridDateTimeColumnEndDate" HeaderText="End"
                                        SortExpression="EndDate" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                        ShowFilterIcon="false" EnableTimeIndependentFiltering="true" DataType="System.Date" />
                                    <rad:GridHyperLinkColumn DataNavigateUrlFields="Email1" DataNavigateUrlFormatString="mailto:{0}"
                                        DataTextField="Email1" HeaderText="Email" SortExpression="Email1" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                                    <rad:GridBoundColumn DataField="PrefMobileNumber" HeaderText="Pref. mobile no." SortExpression="PrefMobileNumber"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" visible="false"/>
                                    <rad:GridTemplateColumn HeaderText="Is Council Member" AllowFiltering="false" visible="false">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblIsCouncilMember" runat="server" Text='<%#Bind("IsCouncilMember")  %>'></asp:Label>--%>
                                            <asp:CheckBox ID="chkIsCouncilMember" runat="server" Checked='<%#Bind("IsCouncilMember")  %>' Enabled="false" />
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </rad:RadGrid>
                    </ContentTemplate>
                </asp:UpdatePanel>


                <div class="sfContentBlock">
                    <div class="button-block style-1">
                        <a style="text-decoration: none;" class="btn-full-width btn" href="/Committees/mycommittees.aspx"><em class="fa fa-arrow-left" aria-hidden="true"></em> Committees</a>
                    </div>
                </div>
                <div class="cai-form">
                    <uc1:SingleForum ID="SingleForum1" runat="server" Visible="false" />
                    <uc2:RecordAttachments ID="RecordAttachments" Visible="false" runat="server"></uc2:RecordAttachments>
                </div>
            </td>
        </tr>
    </table>
    <cc3:User runat="server" ID="User1" />
</div>
