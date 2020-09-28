<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/StudentWebCourseDetails__c.ascx.vb"
    Inherits="StudentWebCourseDetails__c" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Custom__c/RecordAttachments__c.ascx" TagPrefix="ucRecordAttachment"
    TagName="RecordAttachments__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<div class="content-container clearfix cai-table">
    <div>
        <asp:Label ID="lblErrorMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
    </div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlCourseDetails" runat="server">
                <div>
                    <div>
                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblNote" runat="server" Text="Note:" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="info-data">
                        <div class="row-div clearfix" runat="server">
                            <asp:Label ID="lblCurriculum" runat="server">Curriculum:</asp:Label>
                            <asp:DropDownList ID="ddlCurriculumList" runat="server" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="row-div clearfix" runat="server">
                            <asp:Label ID="lblCourse" runat="server">Course:</asp:Label>
                            <asp:DropDownList ID="ddlCourseList" runat="server" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="row-div clearfix">
                            <asp:Label ID="Label1" runat="server">Student Group:</asp:Label>
                            <asp:DropDownList ID="ddlGroupList" runat="server" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div>
                        <asp:HiddenField ID="hfPartStatusID" runat="server" Value="0" />
                        <telerik:RadGrid ID="gvCourseDetails" runat="server" AllowPaging="True" AllowSorting="True"
                            PageSize="10" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None"
                            AutoGenerateColumns="false" Visible="true" CssClass="cai-table mobile-table">
                            <PagerStyle CssClass="sd-pager" />
                            <MasterTableView ShowHeadersWhenNoRecords="true">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="Lesson" HeaderText="Lesson" SortExpression="Lesson"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob" />
                                    <telerik:GridDateTimeColumn DataField="Schedule" HeaderText="Schedule"
                                        SortExpression="Schedule" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="EqualTo"  ShowFilterIcon="false" EnableTimeIndependentFiltering="true"
                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob" />
                                    <telerik:GridBoundColumn DataField="Duration" HeaderText="Duration"
                                        SortExpression="Duration" AutoPostBackOnFilter="true" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob" 
                                        CurrentFilterFunction="EqualTo" ShowFilterIcon="false" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn DataField="Instructor" HeaderText="Instructor"
                                        SortExpression="Instructor" AutoPostBackOnFilter="true" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob" 
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridTemplateColumn HeaderText="Course Material" ShowFilterIcon="false"
                                        ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>

                                            <asp:Label runat="server" Text="Lesson:" CssClass="mobile-label"></asp:Label>
                                            <asp:Label ID="lblMemberTitle" CssClass="cai-table-data no-desktop" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Lesson") %>'></asp:Label>
                                            <asp:Label runat="server" Text="Schedule:" CssClass="mobile-label"></asp:Label>
                                            <asp:Label ID="Label2" CssClass="cai-table-data no-desktop" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Schedule") %>'></asp:Label>
                                            <asp:Label runat="server" Text="Duration:" CssClass="mobile-label"></asp:Label>
                                            <asp:Label ID="Label3" CssClass="cai-table-data no-desktop" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Duration")%>'></asp:Label>
                                             <asp:Label runat="server" Text="Instructor:" CssClass="mobile-label"></asp:Label>
                                            <asp:Label ID="Label4" CssClass="cai-table-data no-desktop" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Instructor")%>'></asp:Label>
                                            <asp:Label runat="server" Text="Materials:" CssClass="mobile-label"></asp:Label>
                                            <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" CssClass="cai-table-data" CommandName="Download"
                                                CommandArgument='<%# Eval("CoursePartID")%>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <div class="actions">
                            <asp:Label ID="lblDownloadFormat" runat="server" Text="" Font-Bold="false"></asp:Label>
                        </div>
                        <asp:Label ID="lblCourseDetailMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                        <telerik:RadWindow ID="radDownloadDocuments" runat="server" Width="500px" height="400px"
                            Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" CssClass="pop-up" Behaviors="None" ForeColor="#BDA797"
                            Title="Download Documents" Behavior="None">
                            <ContentTemplate>
                                    <asp:Panel ID="pnlDownloadDocuments" runat="Server">
                                        <table class="data-form">
                                            <tr>
                                                <td class="RightColumn">
                                                    <ucRecordAttachment:RecordAttachments__c ID="ucDownload" runat="server" AllowView="True"
                                                        AllowAdd="false" AllowDelete="False" ViewDescription="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>                                         
                                    <div class="actions">
                                          <asp:Button ID="btnClose" runat="server" Text="Cancel" CssClass="submitBtn" />
                                    </div>
                            </ContentTemplate>
                        </telerik:RadWindow>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<cc1:User ID="User1" runat="server"></cc1:User>
