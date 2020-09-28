<%@ Control Language="VB" AutoEventWireup="false" CodeFile="MemberCertificationDetails.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.MemberCertificationDetails" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<script language="javascript" type="text/javascript">
// 'Anil B For issue 14344 on 17-04-2013
//            'Open print report on a new tab
    function openNewWin(url) {

        var x = window.open(url);

        x.focus();

    }
</script>
<div>
    <table width="100%">
        <tr>
            <td class="mcLeftTD">
                <asp:Label ID="lblName" runat="server" Font-Bold="true" Text="Name:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblMemberName" runat="server" Text=""></asp:Label>
            </td>          
            <td class="mcLastTD">
          <%--   'Anil B for 14344
            UseSubmitBehavior property to avoid button event call--%>
                <asp:Button ID="btnNewCEUSubmission" UseSubmitBehavior="false"  runat="server" Text="Submit New CEU" CssClass="submitBtn" />&nbsp;
                <asp:Button ID="btnPrint" runat="server" UseSubmitBehavior="false" Text="Print Report" CssClass="submitBtn" TabIndex="10000" />
            </td>
        </tr>
        <tr>
            <td class="mcLeftTD">
                <asp:Label ID="lblTitle" runat="server" Font-Bold="true" Text="Title:"></asp:Label>
            </td>
            <td colspan="2">
                <asp:Label ID="lblMemberTitle" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="mcLeftTD">
                <asp:Label ID="lblCompany" runat="server" Font-Bold="true" Text="Company:"></asp:Label>
            </td>
            <td colspan="2">
                <asp:Label ID="lblMemberCompany" runat="server" Text=""></asp:Label>
            </td> 
        </tr>
        <tr>
            <td class="mcLeftTD">
                <asp:Label ID="lblTotalUnitCount" runat="server" Font-Bold="true" Text="Total Units:"></asp:Label>
            </td>
            <td colspan="2">
                <asp:Label ID="lblMemberTotalUnitCount" runat="server" Text=""></asp:Label>
            </td> 
        </tr>
    </table>
</div>
<div>
    <table>
        <tr>
            <td colspan="5">
                &nbsp;
            </td>            
        </tr>
        <tr id="trSearch" runat="server">
            <td class="mcSearchLeftTD">
               <%-- Anil B For issue 14344 on 28-03-2013
                    Change label text  --%>
                Granted On:
            </td>
            <td class="mcSearchRigthTD">
                <telerik:RadDatePicker ID="txtStartDate" CssClass="datePicker" runat="server" Width="100px">
                </telerik:RadDatePicker>
            </td>
            <td class="mcSearchLeftTD">
                Expires On:
            </td>
            <td class="mcSearchRigthTD">
                <telerik:RadDatePicker ID="txtExpiresOn" CssClass="datePicker" runat="server" Width="100px">
                </telerik:RadDatePicker>
            </td>
            <td>
            <%--   'Anil B for 14344
            UseSubmitBehavior property to avoid button event call--%>
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="submitBtn" UseSubmitBehavior="false" />
            </td>
        </tr>
    </table>
</div>
<div class="clearfix topPaddingSet">
    <div>
        <asp:Label ID="lblmsg" runat="server" Font-Bold="true" Text=""></asp:Label>
    </div>
    <div class="ceuSubmit">
    </div>
     <asp:UpdatePanel ID="upGridPanel" runat="server">
                    <ContentTemplate>
    <table id="tblMain" runat="server" class="data-form">
        <tr>
            <td colspan="2" valign="bottom">
                <asp:Label ID="lblActiveCirtification" runat="server" CssClass="grdHeading" Text="Active Certifications:"></asp:Label>
            </td>           
        </tr>
        <tr>
            <td colspan="2">
                <%-- Anil B For issue 14344 on 28-03-2013
                    Remove updatepanel for refresh the grid  --%>
                        <rad:RadGrid ID="grdMembersActiveCertifications" runat="server" AutoGenerateColumns="False"
                            AllowPaging="true"  PagerStyle-PageSizeLabelText="Records Per Page"
                            AllowFilteringByColumn="true">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AllowFilteringByColumn="true" AllowSorting="true">
                                <Columns>
                                    <rad:GridTemplateColumn HeaderText="Certification" DataField="Title" SortExpression="Title"
                                        AutoPostBackOnFilter="true" FilterControlWidth="150px" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false">
                                        <ItemStyle HorizontalAlign="Left" CssClass="leftAlignCert"></ItemStyle>
                                        <ItemTemplate>
                                         <%-- Anil B For issue 14344 on 28-03-2013
                                            Remove text decoration for hyperlink  --%>
                                            <asp:HyperLink ID="lblCertification" CssClass="namelink hlkMeetingSessionGrid" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Title") %>'></asp:HyperLink><br />
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                   <%-- Anil B For issue 14344 on 28-03-2013
                                            Remove the curriculam column  --%>
                                    <rad:GridTemplateColumn HeaderText="Requirement" DataField="Course" SortExpression="Course"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        FilterControlWidth="120px">
                                        <ItemStyle HorizontalAlign="Left" CssClass="leftAlignCert mcCentralAlign"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRequirement" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Course") %>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Units" DataField="Units" SortExpression="Units"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                        FilterControlWidth="80px">
                                        <ItemStyle HorizontalAlign="Left" CssClass="leftAlignCert mcCentralAlign"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnits" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Units") %>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <rad:GridTemplateColumn HeaderText="Status" DataField="Status" SortExpression="Status"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        FilterControlWidth="100px">
                                        <ItemStyle HorizontalAlign="Left" CssClass="leftAlignCert mcCentralAlign"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </rad:GridTemplateColumn>
                                    <%-- Anil B For issue 14344 on 28-03-2013
                                            Remove filtering to date column  --%>
                                   <rad:GridDateTimeColumn DataField="DateGranted" UniqueName="GridDateTimeColumnACertificationGranted" AllowFiltering="false"
                                    HeaderText="Granted On" FilterControlWidth="100px" HeaderStyle-Width="100px" SortExpression="DateGranted"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" DataType="System.DateTime"
                                    ShowFilterIcon="false" EnableTimeIndependentFiltering="true" />

                                    <rad:GridDateTimeColumn DataField="ExpirationDate" UniqueName="GridDateTimeColumnACertificationExpiration" AllowFiltering="false"
                                     HeaderText="Expires On" FilterControlWidth="100px" HeaderStyle-Width="100px" SortExpression="ExpirationDate"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" 
                                    ShowFilterIcon="false" EnableTimeIndependentFiltering="true" />
                                </Columns>
                            </MasterTableView>
                        </rad:RadGrid>
                  
            </td>
        </tr>       
        <tr>
            <td colspan="2" style="padding-top:10px;">
             <%-- Anil B For issue 14344 on 28-03-2013
               change heading of grid  --%>
                <asp:Label ID="lblDeActiveCirtification" runat="server" CssClass="grdHeading" Text="Inactive Certifications:"></asp:Label>
            </td>          
        </tr>
        <tr>
            <td colspan="2">
                <rad:RadGrid ID="grdMembersDEeActiveCertifications" runat="server" AutoGenerateColumns="False"
                    AllowPaging="true" PagerStyle-PageSizeLabelText="Records Per Page"
                    AllowFilteringByColumn="true">
                    <GroupingSettings CaseSensitive="false" />
                    <MasterTableView AllowFilteringByColumn="true" AllowSorting="true">
                        <Columns>
                            <rad:GridTemplateColumn HeaderText="Certification" DataField="Title" SortExpression="Title"
                                AutoPostBackOnFilter="true" FilterControlWidth="150px" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false">
                                <ItemStyle HorizontalAlign="Left" CssClass="leftAlignCert"></ItemStyle>
                                <ItemTemplate>
                                    <asp:HyperLink ID="lblCertification" CssClass="namelink hlkMeetingSessionGrid" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Title") %>'></asp:HyperLink><br />
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                          
                            <rad:GridTemplateColumn HeaderText="Requirement" DataField="Course" SortExpression="Course"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                FilterControlWidth="120px">
                                <ItemStyle HorizontalAlign="Left" CssClass="leftAlignCert mcCentralAlign"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblRequirement" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Course") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Units" DataField="Units" SortExpression="Units"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                FilterControlWidth="80px">
                                <ItemStyle HorizontalAlign="Left" CssClass="leftAlignCert mcCentralAlign"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblUnits" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Units") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>
                            <rad:GridTemplateColumn HeaderText="Status" DataField="Status"  SortExpression="Status"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                                FilterControlWidth="100px">
                                <ItemStyle HorizontalAlign="Left" CssClass="leftAlignCert mcCentralAlign"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Status") %>'></asp:Label>
                                </ItemTemplate>
                            </rad:GridTemplateColumn>                        
                             <%-- Anil B For issue 14344 on 28-03-2013
                                            Remove filtering to date column  --%>
                            <rad:GridDateTimeColumn DataField="DateGranted" UniqueName="GridDateTimeColumnDACertificationGranted"
                            HeaderText="Granted On" FilterControlWidth="100px" HeaderStyle-Width="100px" SortExpression="DateGranted"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" DataType="System.DateTime" AllowFiltering="false"
                            ShowFilterIcon="false" EnableTimeIndependentFiltering="true" />

                             <rad:GridDateTimeColumn DataField="ExpirationDate" UniqueName="GridDateTimeColumnDACertificationExpiration"
                            HeaderText="Expires On" FilterControlWidth="100px" HeaderStyle-Width="100px" SortExpression="ExpirationDate"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" DataType="System.DateTime" AllowFiltering="false"
                            ShowFilterIcon="false" EnableTimeIndependentFiltering="true" />

                        </Columns>
                    </MasterTableView>
                </rad:RadGrid>
            </td>
        </tr>
    </table>
     </ContentTemplate>
                </asp:UpdatePanel>
    <cc1:User runat="server" ID="User1" />   
</div>
