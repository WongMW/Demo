<%@ Control Language="VB" AutoEventWireup="false" CodeFile="StudentWebCourseDetails__c.ascx.vb"
    Inherits="StudentWebCourseDetails__c" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/UserControls/Aptify_Custom__c/RecordAttachments__c.ascx" TagPrefix="ucRecordAttachment"
    TagName="RecordAttachments__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<link href="../../CSS/StyleSheet.css" rel="stylesheet" type="text/css" />
<link href="../../CSS/DivTag.css" rel="stylesheet" type="text/css" />
<link href="../../CSS/Div_Data_form.css" rel="stylesheet" type="text/css" />
<div class="content-container clearfix">
    <div>
        <asp:Label ID="lblErrorMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
    </div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlCourseDetails" runat="server">
                <div>
                    <div>
                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                    </div>
                    <div>
                        <asp:Label ID="lblNote" runat="server" Text="Note:" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="info-data">
                        <br />
                        <div class="row-div clearfix" runat="server">
                            <div class="label-div w20">
                                <asp:Label ID="lblCurriculum" runat="server">Curriculum:</asp:Label>
                            </div>
                            <div class="field-div1 w70">
                                <asp:DropDownList ID="ddlCurriculumList" runat="server" AutoPostBack="true" Width="30%">
                                </asp:DropDownList>
                            </div>
                            <div class="label-div w10">
                            </div>
                        </div>
                        <div class="row-div clearfix" runat="server">
                            <div class="label-div w20">
                                <asp:Label ID="lblCourse" runat="server">Course:</asp:Label>
                            </div>
                            <div class="field-div1 w70">
                                <asp:DropDownList ID="ddlCourseList" runat="server" AutoPostBack="true" Width="30%">
                                </asp:DropDownList>
                            </div>
                            <div class="label-div w10">
                            </div>
                        </div>
                        <div class="row-div clearfix">
                            <div class="label-div w20">
                                <asp:Label ID="Label1" runat="server">Student Group:</asp:Label>
                            </div>
                            <div class="field-div1 w70">
                                <asp:DropDownList ID="ddlGroupList" runat="server" AutoPostBack="true" Width="30%">
                                </asp:DropDownList>
                            </div>
                            <div class="label-div w10">
                            </div>
                        </div>
                    </div>
                    <div>
                        <asp:HiddenField ID="hfPartStatusID" runat="server" Value="0" />
                        <telerik:RadGrid ID="gvCourseDetails" runat="server" AllowPaging="True" AllowSorting="True"
                            PageSize="10" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None"
                            AutoGenerateColumns="false" Width="99%" Visible="true">
                            <MasterTableView ShowHeadersWhenNoRecords="true">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="Lesson" HeaderText="Lesson" SortExpression="Lesson"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        FilterControlWidth="100%" HeaderStyle-Width="30%" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridDateTimeColumn DataField="Schedule" HeaderText="Schedule" FilterControlWidth="100%"
                                        HeaderStyle-Width="25%" SortExpression="Schedule" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="EqualTo" ShowFilterIcon="false" EnableTimeIndependentFiltering="true"
                                        ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn DataField="Duration" HeaderText="Duration" FilterControlWidth="100%"
                                        HeaderStyle-Width="10%" SortExpression="Duration" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="EqualTo" ShowFilterIcon="false" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridBoundColumn DataField="Instructor" HeaderText="Instructor" FilterControlWidth="100%"
                                        HeaderStyle-Width="20%" SortExpression="Instructor" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridTemplateColumn HeaderText="Course Material" ShowFilterIcon="false" HeaderStyle-Width="20%"
                                        ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" CommandName="Download"
                                                CommandArgument='<%# Eval("CoursePartID")%>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid><br />
                        <div>
                            <asp:Label ID="lblDownloadFormat" runat="server" Text="" Font-Bold="false"></asp:Label>
                        </div>
                        <asp:Label ID="lblCourseDetailMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                        <telerik:RadWindow ID="radDownloadDocuments" runat="server" Width="500px" Height="300px"
                            Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                            Title="Download Documents" Behavior="None">
                            <ContentTemplate>
                                <div>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="5%">
                                            </td>
                                            <td width="90%">
                                                <b>Documents</b><br />
                                                <asp:Panel ID="pnlDownloadDocuments" runat="Server" Style="border: 1px Solid #000000;">
                                                    <table class="data-form" width="100%">
                                                        <tr>
                                                            <td class="RightColumn">
                                                                <ucRecordAttachment:RecordAttachments__c ID="ucDownload" runat="server" AllowView="True"
                                                                    AllowAdd="false" AllowDelete="False" ViewDescription="false" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                            <td width="5%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="btnClose" runat="server" Text="Cancel" CssClass="submitBtn" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
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
