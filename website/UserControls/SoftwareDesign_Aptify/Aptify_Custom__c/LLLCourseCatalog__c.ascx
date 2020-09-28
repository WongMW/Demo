<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/LLLCourseCatalog__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.LLLCourseCatalog__c" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc4" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%--Added by Pradip 2016-05-24--%>
<%@ Register Src="~/UserControls/Aptify_Custom__c/RecordAttachments__c.ascx" TagPrefix="ucRecordAttachment" TagName="RecordAttachments__c" %>

<script type="text/javascript">
    function openClassPartWin() {
        debugger;
        window.radopen(null, "radCalssParts");
    }
</script>

<div class="dvUpdateProgress" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing" style="height: 700px;">
                <table class="tblFullHeightWidth">
                    <tr>
                        <td class="tdProcessing" style="vertical-align: middle">
                            Please wait...
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<div class="info-data" style="height: 450px;">
    <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
        <ContentTemplate>--%>
            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
            <div class="row-div clearfix">
                <div class="label-div w30">
                    &nbsp;
                </div>
                <div class="field-div1 w100">
                    <div class="info-data" style="margin: 0 5% 0 6% !important;" id="divIDClassDetails"
                        runat="server">
                        <div class="row-div clearfix">
                            <div class="field-div1 w80 cai-table" style="text-align: left;">
                                <telerik:RadGrid ID="radCourseDetails" runat="server" AutoGenerateColumns="False"
                                    AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                    AllowFilteringByColumn="true" AllowSorting="true" Width="100%" PageSize="5">
                                    <GroupingSettings CaseSensitive="false" />
                                    <PagerStyle CssClass="sd-pager" />
                                    <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="Course" AllowFiltering="true" SortExpression="Course"
                                                AutoPostBackOnFilter="true" DataField="Course" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="10%" >
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkCourse" runat="server" ForeColor="Blue" CommandName="Course"
                                                        Font-Underline="true" CommandArgument='<%# Eval("ID")%>' Text='<%# Eval("Course")%>'></asp:LinkButton>
                                                    <asp:Label ID="lblCourseID" runat="server" Text='<%# Eval("ID")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="CourseCategory" HeaderText="Course Category"
                                                SortExpression="CourseCategory" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="100%"
                                                HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                            <div class="label-div w30">
                                &nbsp;
                            </div>
                            <div class="label-div w30">
                                &nbsp;
                            </div>
                    <%--Added BY Pradip 2016-05-24--%>
                         <div class="label-div w100">
                            <h2 runat="server" id="hearderclass" align="center" visible="false">Class Details</h2>
                         </div>
                         <div class="label-div w30">
                           &nbsp;
                         </div>
                     <%--End Here Added BY Pradip 2016-05-24--%>
                            <div class="field-div1 w80" style="text-align: left;">
                                <telerik:RadGrid ID="radClassDetails" runat="server" AutoGenerateColumns="False"
                                    AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                    AllowFilteringByColumn="true" AllowSorting="true" Width="150px" PageSize="5"
                                    Visible="false">
                                    <GroupingSettings CaseSensitive="false" />
				     <PagerStyle CssClass="sd-pager" />
                                    <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                                        <Columns>
                                            <%--<telerik:GridTemplateColumn HeaderText="Course" AllowFiltering="true" SortExpression="WebName"
                                                AutoPostBackOnFilter="true" DataField="WebName" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkClassTitle" runat="server" ForeColor="Blue" CommandName="ClassTitle" 
                                                        Font-Underline="true" CommandArgument='<%# Eval("ID")%>' Text='<%# Eval("WebName")%>'></asp:LinkButton>
                                                    <asp:Label ID="lblClassID" runat="server" Text='<%# Eval("ID")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                    </telerik:GridTemplateColumn>--%>
                                    <telerik:GridBoundColumn DataField="WebName" HeaderText="Course" SortExpression="WebName"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                        FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn DataField="StartDate" HeaderText="Start Date" SortExpression="StartDate"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridTemplateColumn HeaderText=" Class Schedule" AllowFiltering="true" SortExpression="Schedule"
                                                AutoPostBackOnFilter="true" DataField="Schedule" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkSchedule" runat="server" ForeColor="Blue" CommandName="Schedule" OnClick="BindOfficcerInfo_click"
                            Font-Underline="true" CommandArgument='<%# Eval("ID")%>' Text='<%# Eval("Schedule")%>'></asp:LinkButton>
                                                    <asp:Label ID="lblScheduleClassID" runat="server" Text='<%# Eval("ID")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="ExamDate" HeaderText="Exam Date" SortExpression="ExamDate"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn DataField="DeliveryType" HeaderText="Delivery Type" SortExpression="DeliveryType"
                                                AutoPostBackOnFilter="true" AllowFiltering="true" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridTemplateColumn HeaderText="Fee" AllowFiltering="false" AutoPostBackOnFilter="false"
                                                ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrice" runat="server" Text='<%# GetPrice(Eval("ProductID")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="DescOfCourseStructure" HeaderText="Description Of Course Structure"
                                                SortExpression="DescOfCourseStructure" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="100%"
                                                HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridTemplateColumn HeaderText="Document" AllowFiltering="true" SortExpression="Filename"
                                                AutoPostBackOnFilter="true" DataField="Filename" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                     <%--Commented By Pradip 2016-05-24--%>
                                                     <%--<asp:LinkButton ID="lnkFilePath" runat="server" ForeColor="Blue" CommandName="Attachment" 
                                                        Font-Underline="true" CommandArgument='<%# Eval("FilePath") + "," + convert.tostring(Eval("ID")) %>' Text='<%# Eval("Filename")%>'></asp:LinkButton>
                                                      <asp:Label ID="lblFilePath" runat="server" Text='<%# Eval("FilePath")%>' Visible="false"></asp:Label>--%>
                                                      <%--Added By Pradip 2016-05-24--%>
                                                      <asp:LinkButton ID="lnkFileAttachment" runat="server" ForeColor="Blue" CommandName="Attachment" Text="Attachment"
                                                       Font-Underline="true" CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>  
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="ClosingDate" HeaderText="Closing Date for Enrollment"
                                                SortExpression="ClosingDate" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                    <telerik:GridTemplateColumn HeaderText="Entry Criteria" AllowFiltering="true" SortExpression="EntryCriteria"
                                        AutoPostBackOnFilter="true" DataField="EntryCriteria" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEntryCriteria" runat="server" Text='<%# Eval("NewEntryCriteria") %>'></asp:Label>
                                            <asp:LinkButton ID="lnkEntryCriteria" runat="server" ForeColor="Blue" CommandName="EntryCriteria"
                                                Font-Underline="true" CommandArgument='<%# Eval("EntryCriteria") %>'
                                                Text="Entry Criteria"></asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn> 
                                           <telerik:GridTemplateColumn HeaderText="Enroll" AllowFiltering="false" AutoPostBackOnFilter="false"
                                                ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:LinkButton CommandName="EnrollLink" Font-Underline="true" CommandArgument='<%# Eval("EnrollType") + "," + convert.tostring(Eval("ID")) %>'
                                                        Text='<%# Eval("EnrollType")%>' ID="lnkEnrollment" runat="server" ForeColor="Blue">Enroll</asp:LinkButton>
                                           <asp:Label ID="lblCTC" runat="server" Text='<%# Eval("CTC")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="label-div w30">
                    &nbsp;
                </div>                
                <telerik:RadWindow ID="radWindow" runat="server" Width="350px" Modal="True" BackColor="#f4f3f1"
                    VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797" Title="Life Long Learning Enrollment Application"
                    Behavior="None" Height="150px">
                    <ContentTemplate>
                        <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                            padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <div>
                                        <br />
                                    </div>
                                    <div>
                                        <asp:Button ID="btnOK" runat="server" Text="Ok" Width="70px" class="submitBtn" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </telerik:RadWindow>

               
               <%--Commented BY Pradip 2016-05-24--%>
               <%--<telerik:RadWindowManager ID="radMngrCalssParts" runat="server">
                   <Windows>--%>
                     <telerik:RadWindow ID="radCalssParts" runat="server" Modal="true">
                        <ContentTemplate>
                           <div class="field-div1 w80" style="text-align: left;">
                                <telerik:RadGrid ID="radgridClassPart" runat="server" AutoGenerateColumns="False"
                                    AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                    AllowFilteringByColumn="true" AllowSorting="true" Width="150px" PageSize="5">
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                                        <Columns>
                                        <%--CP.ID,COP.Name,CP.StartDate,CP.EndDate,CP.LecturerStatus__c,CP.Status--%>
                                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID"
                                                SortExpression="ID" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="100%"
                                                HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn DataField="Name" HeaderText="Class Part"
                                                SortExpression="Name" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="100%"
                                                HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn DataField="StartDate" HeaderText="Start Date"
                                                SortExpression="StartDate" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="100%"
                                                HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn DataField="EndDate" HeaderText="End Date"
                                                SortExpression="EndDate" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="100%"
                                                HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                            <%--Commented BY Pradip 2016-05-24--%>
                                           <%-- <telerik:GridBoundColumn DataField="LecturerStatus__c" HeaderText="Lecturer Status"
                                                SortExpression="LecturerStatus__c" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="100%"
                                                HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn DataField="Status" HeaderText="Status"
                                                SortExpression="Status" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="100%"
                                                HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />--%>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                <%--Added By Pradip 2016-05-24--%>
                <div>
                    <asp:Button ID="BtnWinOk" runat="server" Text="Ok" Width="70px" class="submitBtn" />
                </div> 
                        </ContentTemplate>
                     </telerik:RadWindow>
             <%-- </Windows>
                </telerik:RadWindowManager>--%>

        <%--Added BY Pradip 2016-05-24--%>
        <telerik:RadWindow ID="radDownloadDocuments" runat="server" Width="500px" Height="350px"
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
                            <td width="5%"></td>
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
                            <td width="5%"></td>
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
                            <td></td>
                            <td align="right">
                                <asp:Button ID="btnClose" runat="server" Text="Cancel" CssClass="submitBtn" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
            </div>
            <cc3:User ID="User1" runat="server" />
            <cc4:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="False"></cc4:AptifyShoppingCart>
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</div>
