<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Group_Admin/MemberCertificationDetails.ascx.vb"
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
<div class="ceu-page cai-form clearfix">
    <div class="field-group">
        <asp:Label ID="lblName" runat="server" CssClass="label-title" Text="Name:"></asp:Label>
        <asp:Label ID="lblMemberName" runat="server" Text=""></asp:Label>
    </div>

    <div class="field-group">
        <asp:Label ID="lblTitle" runat="server" CssClass="label-title" Text="Title:"></asp:Label>
        <asp:Label ID="lblMemberTitle" runat="server" Text=""></asp:Label>
    </div>

    <div class="field-group">
        <asp:Label ID="lblCompany" runat="server" CssClass="label-title" Text="Company:"></asp:Label>
        <asp:Label ID="lblMemberCompany" runat="server" Text=""></asp:Label>
    </div>

    <div class="field-group">
        <asp:Label ID="lblTotalUnitCount" runat="server" CssClass="label-title" Text="Total Units:"></asp:Label>
        <asp:Label ID="lblMemberTotalUnitCount" runat="server" Text=""></asp:Label>
    </div>

    <div class="field-group actions">
        <asp:Button ID="btnNewCEUSubmission" UseSubmitBehavior="false" runat="server" Text="Submit New CEU" CssClass="submitBtn" />
        <asp:Button ID="btnPrint" runat="server" UseSubmitBehavior="false" Text="Print Report" CssClass="submitBtn" TabIndex="10000" />
    </div>

    <div class="field-group filters">
        <div id="trSearch" runat="server">
            <span class="label-title">Granted On:</span>
            <telerik:RadDatePicker ID="txtStartDate" CssClass="datePicker" runat="server">
            </telerik:RadDatePicker>

            <span class="label-title">Expires On:</span>
            <telerik:RadDatePicker ID="txtExpiresOn" CssClass="datePicker" runat="server">
            </telerik:RadDatePicker>

            <div class="actions">
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="submitBtn" UseSubmitBehavior="false" />
            </div>
        </div>
    </div>
    <div class="clearfix field-group">
        <asp:Label ID="lblmsg" runat="server" CssClass="label-title" Text=""></asp:Label>
        <asp:Label ID="lblActiveCirtification" runat="server" CssClass="label-title" Text="Active Certifications:"></asp:Label>
        <asp:UpdatePanel ID="upGridPanel" runat="server">
            <ContentTemplate>
                <div id="tblMain" runat="server" class="cai-table">
                    <rad:RadGrid ID="grdMembersActiveCertifications" runat="server" AutoGenerateColumns="False"
                        AllowPaging="true" PagerStyle-PageSizeLabelText="Records Per Page"
                        AllowFilteringByColumn="true" CssClass="no-border certification-table">
                        <PagerStyle CssClass="sd-pager" />
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView AllowFilteringByColumn="true" AllowSorting="true">
                            <Columns>
                                <rad:GridTemplateColumn HeaderText="Certification" DataField="Title" SortExpression="Title"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    ShowFilterIcon="false">
                                    <ItemStyle HorizontalAlign="Left" CssClass="leftAlignCert"></ItemStyle>
                                    <ItemTemplate>

                                        <asp:Label runat="server" Text="Certification:" CssClass="mobile-label"></asp:Label>
                                        <asp:HyperLink ID="lblCertification" CssClass="namelink cai-table-data hlkMeetingSessionGrid" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Title") %>'></asp:HyperLink><br />

                                    </ItemTemplate>
                                </rad:GridTemplateColumn>

                                <rad:GridTemplateColumn HeaderText="Requirement" DataField="Course" SortExpression="Course"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                    <ItemStyle HorizontalAlign="Left" CssClass="leftAlignCert mcCentralAlign"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="Requirement:" CssClass="mobile-label"></asp:Label>
                                        <asp:Label ID="lblRequirement" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Course") %>'></asp:Label>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Units" DataField="Units" SortExpression="Units"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
                                    <ItemStyle HorizontalAlign="Left" CssClass="leftAlignCert mcCentralAlign"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="Units:" CssClass="mobile-label"></asp:Label>
                                        <asp:Label ID="lblUnits" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Units") %>'></asp:Label>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Status" DataField="Status" SortExpression="Status"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                    <ItemStyle HorizontalAlign="Left" CssClass="leftAlignCert mcCentralAlign"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="Status:" CssClass="mobile-label"></asp:Label>
                                        <asp:Label ID="lblStatus" CssClass="cai-table-data" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Status") %>'></asp:Label>
                                        <asp:Label runat="server" Text="Granted On:" CssClass="mobile-label"></asp:Label>
                                        <asp:Label ID="Label1" CssClass="cai-table-data no-desktop" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DateGranted")%>'></asp:Label>
                                        <br />
                                        <asp:Label runat="server" Text="Expires On:" CssClass="mobile-label"></asp:Label>
                                        <asp:Label ID="Label2" CssClass="cai-table-data no-desktop" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ExpirationDate")%>'></asp:Label>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>

                                <rad:GridDateTimeColumn DataField="DateGranted" UniqueName="GridDateTimeColumnACertificationGranted" AllowFiltering="false"
                                    HeaderText="Granted On" HeaderStyle-Width="100px" SortExpression="DateGranted"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" DataType="System.DateTime"
                                    ShowFilterIcon="false" EnableTimeIndependentFiltering="true" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob" />

                                <rad:GridDateTimeColumn DataField="ExpirationDate" UniqueName="GridDateTimeColumnACertificationExpiration" AllowFiltering="false"
                                    HeaderText="Expires On" HeaderStyle-Width="100px" SortExpression="ExpirationDate"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                                    ShowFilterIcon="false" EnableTimeIndependentFiltering="true" HeaderStyle-CssClass="no-mob" ItemStyle-CssClass="no-mob" />
                            </Columns>
                        </MasterTableView>
                    </rad:RadGrid>

                    <asp:Label ID="lblDeActiveCirtification" runat="server" CssClass="grdHeading" Text="Inactive Certifications:"></asp:Label>

                    <rad:RadGrid ID="grdMembersDEeActiveCertifications" runat="server" AutoGenerateColumns="False"
                        AllowPaging="true" PagerStyle-PageSizeLabelText="Records Per Page"
                        AllowFilteringByColumn="true">
                        <PagerStyle CssClass="sd-pager" />
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
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                    <ItemStyle HorizontalAlign="Left" CssClass="leftAlignCert mcCentralAlign"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnits" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Units") %>'></asp:Label>
                                    </ItemTemplate>
                                </rad:GridTemplateColumn>
                                <rad:GridTemplateColumn HeaderText="Status" DataField="Status" SortExpression="Status"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" ShowFilterIcon="false">
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
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <cc1:User runat="server" ID="User1" />
    </div>
</div>
