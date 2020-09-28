<%@ Control Language="VB" AutoEventWireup="false" CodeFile="LLLCourseCatalog__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.LLLCourseCatalog__c" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessShoppingCart" %>
<%@ Register Src="~/UserControls/Aptify_Custom__c/RecordAttachments__c.ascx" TagPrefix="ucRecordAttachment" TagName="RecordAttachments__c" %>
<div class="raDiv" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing"><div class="loading-bg">
                <img src="/Images/CAITheme/bx_loader.gif" />
                <span>LOADING...<br /><br />
                    Please do not leave or close this window while the request is processing.</span></div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<div class="info-data">
    <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
        <ContentTemplate>--%>
    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
    <div class="row-div clearfix">
        <div class="label-div w30">&nbsp;</div>
        <div class="field-div1 w100 cai-form">
            <div class="info-data" id="divIDClassDetails" runat="server">
                <div class="row-div clearfix">
                    <div class="sf_cols">
                        <div class="sf_colsOut sf_2cols_1_33">
                            <div id="baseTemplatePlaceholder_content_ctl01_ctl03_ctl04_C069_Col00" class="sf_colsIn sf_2cols_1in_33">
                                <div class="label-div w100 form-title">
                                    <h4 id="HeadTimeFrame" align="center" style="color:white;">Course details</h4> 
                                </div>
                                <div class="field-div1 w100 cai-table" style="text-align: left;">
                                    <telerik:RadGrid ID="radCourseDetails" runat="server" AutoGenerateColumns="False"
                                        AllowPaging="false" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                        AllowFilteringByColumn="true" AllowSorting="true">
                                        <GroupingSettings CaseSensitive="false" />
                                        <MasterTableView AllowFilteringByColumn="false" AllowSorting="true" AllowNaturalSort="false">
                                            <Columns>
                                                <telerik:GridTemplateColumn HeaderText="Course" AllowFiltering="true" SortExpression="Course"
                                                    AutoPostBackOnFilter="true" DataField="Course" CurrentFilterFunction="Contains"
                                                    ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="100%" HeaderStyle-CssClass="field-group" >
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkCourse" runat="server" ForeColor="Blue" CommandName="Course"
                                                            Font-Underline="true" CommandArgument='<%# Eval("ID")%>' Text='<%# Eval("Course")%>'></asp:LinkButton>
                                                        <asp:Label ID="lblCourseID" runat="server" Text='<%# Eval("ID")%>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="CourseCategory" HeaderText="Course Category"
                                                    SortExpression="CourseCategory" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="100%"
                                                    HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="field-group" Visible="false"/>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </div>
                        </div>
                        <div class="sf_colsOut sf_2cols_2_67">
                            <div id="baseTemplatePlaceholder_content_ctl01_ctl03_ctl04_C069_Col01" class="sf_colsIn sf_2cols_2in_67">
                                <div class="label-div w100 form-title">
                                <h4 runat="server" id="hearderclass" align="center"  Visible="true" style="color:white;">Class details</h4> 
                                </div>
                                <div class="field-div1 w100 cai-table" style="text-align: left;">
                    
                                    <telerik:RadGrid ID="radClassDetails" runat="server" AutoGenerateColumns="False"
                                        AllowPaging="false" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                        AllowFilteringByColumn="false" AllowSorting="true" Visible="false" CssClass="lll-course-seperator">
                                        <GroupingSettings CaseSensitive="false" />
                                        <MasterTableView AllowFilteringByColumn="false" AllowSorting="true" AllowNaturalSort="false">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="WebName" HeaderText="Course" SortExpression="WebName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" 
                                                    ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="30%" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="field-group no-mob" ItemStyle-CssClass="no-mob"/>
                                                <telerik:GridBoundColumn DataField="ClosingDate" HeaderText="Enrol by"
                                                    SortExpression="ClosingDate" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                                    ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="14%" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="field-group no-mob" ItemStyle-CssClass="no-mob"/>   
                                                <telerik:GridBoundColumn DataField="StartDate" HeaderText="Start date" SortExpression="StartDate" 
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                    FilterControlWidth="100%" HeaderStyle-Width="14%" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="field-group no-mob" ItemStyle-CssClass="no-mob"/>
                                                <telerik:GridBoundColumn DataField="ExamDate" HeaderText="Exam date" SortExpression="ExamDate"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                    FilterControlWidth="100%" HeaderStyle-Width="14%" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="field-group no-mob" ItemStyle-CssClass="no-mob"/>
                                                <telerik:GridBoundColumn DataField="DeliveryType" HeaderText="Delivery Type" SortExpression="DeliveryType"
                                                    AutoPostBackOnFilter="true" AllowFiltering="true" CurrentFilterFunction="Contains"
                                                    ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="field-group" Visible="false"/>        
                                               <%-- <telerik:GridTemplateColumn HeaderText="Description Of Course Structure" AllowFiltering="true" SortExpression="DescOfCourseStructure"
                                                    AutoPostBackOnFilter="true" DataField="DescOfCourseStructure" CurrentFilterFunction="Contains"
                                                    ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("NewWebDesc") %>'></asp:Label>
                                                        <asp:LinkButton ID="lnkDescOfCourseStructure" runat="server" ForeColor="Blue" CommandName="DescOfCourseStructure"
                                                            Font-Underline="true" CommandArgument='<%# Eval("DescOfCourseStructure") %>'
                                                            Text="Description" ></asp:LinkButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn> --%>
                                              <%--  <telerik:GridTemplateColumn HeaderText="Document" AllowFiltering="true" SortExpression="Filename"
                                                    AutoPostBackOnFilter="true" DataField="Filename" CurrentFilterFunction="Contains"
                                                    ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="10%">
                                                    <ItemTemplate>                                            
                                                        <asp:LinkButton ID="lnkFileAttachment" runat="server" ForeColor="Blue" CommandName="Attachment" text="Attachment"
                                                            Font-Underline="true" CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn> --%>                            
                                               <%-- <telerik:GridTemplateColumn HeaderText="Entry Criteria" AllowFiltering="true" SortExpression="EntryCriteria"
                                                    AutoPostBackOnFilter="true" DataField="EntryCriteria" CurrentFilterFunction="Contains"
                                                    ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEntryCriteria" runat="server" Text='<%# Eval("NewEntryCriteria") %>'></asp:Label>
                                                        <asp:LinkButton ID="lnkEntryCriteria" runat="server" ForeColor="Blue" CommandName="EntryCriteria"
                                                            Font-Underline="true" CommandArgument='<%# Eval("EntryCriteria") %>'
                                                            Text="Entry Criteria"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn> --%>
                                                <telerik:GridTemplateColumn HeaderText="Course" SortExpression="WebName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" 
                                                    ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-CssClass="field-group no-desktop" ItemStyle-CssClass="no-desktop">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text="Course" CssClass="mobile-label"></asp:Label>
                                                        <asp:Label runat="server" CssClass="cai-table-data no-desktop" Text='<%# Eval("WebName")%>' ></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Enrol by" SortExpression="ClosingDate" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" 
                                                    ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="100%" ItemStyle-Width="100%" HeaderStyle-CssClass="field-group no-desktop" ItemStyle-CssClass="no-desktop">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text="Enrol by" CssClass="mobile-label"></asp:Label>
                                                        <asp:Label runat="server" CssClass="cai-table-data no-desktop" Text='<%# Eval("ClosingDate")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Start date" HeaderStyle-CssClass="mobile-label no-desktop" ItemStyle-CssClass="no-desktop" SortExpression="StartDate" 
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="100%">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" CssClass="mobile-label" Text="Start date"></asp:Label>
                                                        <asp:Label runat="server" CssClass="cai-table-data no-desktop" Text='<%# Eval("StartDate")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Exam date" HeaderStyle-CssClass="mobile-label no-desktop" ItemStyle-CssClass="no-desktop" SortExpression="ExamDate" 
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="100%">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" CssClass="mobile-label" Text="Exam date"></asp:Label>
                                                        <asp:Label runat="server" CssClass="cai-table-data no-desktop" Text='<%# Eval("ExamDate")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Fee" AllowFiltering="false" AutoPostBackOnFilter="false"
                                                    ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-CssClass="field-group">
                                                    <ItemTemplate>
                                                        <asp:Label id="lblFee" runat="server" Text="Fee" CssClass="mobile-label"></asp:Label>
                                                        <span class="cai-table-data mob-align">
                                                            <asp:Label ID="lblCurrency" runat="server" CssClass="cai-table-data mob-currency"></asp:Label>
                                                            <asp:Label ID="lblPrice" runat="server" CssClass="cai-table-data mob-price" Text='<%# GetPrice(Eval("ProductID")) %>'></asp:Label>
                                                        </span>
                                                        
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>  
                                                <telerik:GridTemplateColumn HeaderText=" Class Schedule" AllowFiltering="true" SortExpression="Schedule"
                                                    AutoPostBackOnFilter="true" DataField="Schedule" CurrentFilterFunction="Contains"
                                                    ShowFilterIcon="false" FilterControlWidth="100%" ItemStyle-Width="10%" HeaderStyle-CssClass="field-group" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkSchedule" runat="server" ForeColor="Blue" CommandName="Schedule"
                                                            OnClick="BindOfficcerInfo_click" Font-Underline="true" CommandArgument='<%# Eval("ID")%>'
                                                            Text='<%# Eval("Schedule")%>'></asp:LinkButton>
                                                        <asp:Label ID="lblScheduleClassID" runat="server" Text='<%# Eval("ID")%>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText=" " AllowFiltering="false" AutoPostBackOnFilter="false"
                                                    ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-CssClass="field-group">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEnrol" runat="server" Text="Enrol" CssClass="mobile-label" Visible="false"></asp:Label>
                                                        <asp:LinkButton CommandName="EnrollLink" CommandArgument='<%# Eval("EnrollType") + "," + convert.tostring(Eval("ID")) %>'
                                                            Text='<%# Eval("EnrollType")%>' ID="lnkEnrollment" runat="server" CssClass="cai-table-data cai-btn cai-btn-red-inverse">Enroll</asp:LinkButton>
                                                              <asp:Label ID="lblCTC" runat="server"  Text='<%# Eval("CTC")%>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="label-div w30">&nbsp;</div>
        <telerik:RadWindow ID="radWindow" runat="server" Width="500px" Modal="True" BackColor="#f4f3f1"
            VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797" Title="Life Long Learning enrollment application"
            Behavior="None" Height="300px">
            <ContentTemplate>
                <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                    padding-left: 5px; padding-right: 5px; padding-top: 5px; padding-bottom:5px;">
                    <tr style="height: 210px">
                        <td align="left" valign="top">
                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr valign="bottom">
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
        <%--<telerik:RadWindowManager ID="radMngrCalssParts" runat="server">
                   <Windows>--%>
        <telerik:RadWindow ID="radCalssParts" Width="500px" runat="server" Modal="true" BackColor="#f4f3f1"
            VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797" Title="Class schedule"
            Behavior="None" Height="500px">
            <ContentTemplate>
                <table width="100%" cellpadding="0" cellspacing="0" style="background-color: #f4f3f1;
                    padding-left: 5px; padding-right: 5px; padding-top: 5px;">
                    <tr>
                        <td>
                            <div class="field-div1 w100" style="text-align: left;">
                                <telerik:RadGrid ID="radgridClassPart" runat="server" AutoGenerateColumns="False"
                                    AllowPaging="true" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                    AllowFilteringByColumn="true" AllowSorting="true" PageSize="5" Width="500px">
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView AllowFilteringByColumn="true" AllowSorting="true" AllowNaturalSort="false">
                                        <Columns>
                                            <%--CP.ID,COP.Name,CP.StartDate,CP.EndDate,CP.LecturerStatus__c,CP.Status--%>
                                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" SortExpression="ID" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left"  Visible="false"/>
                                            <telerik:GridBoundColumn DataField="Name" HeaderText="Class Part" SortExpression="Name"
                                                AutoPostBackOnFilter="true" AllowFiltering="true" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn DataField="StartDate" HeaderText="Start date" SortExpression="StartDate"
                                                AutoPostBackOnFilter="true" AllowFiltering="true" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn DataField="EndDate" HeaderText="End date" SortExpression="EndDate"
                                                AutoPostBackOnFilter="true" AllowFiltering="true" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                          <%--  <telerik:GridBoundColumn DataField="LecturerStatus__c" HeaderText="Lecturer Status"
                                                SortExpression="LecturerStatus__c" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" FilterControlWidth="100%"
                                                HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />
                                            <telerik:GridBoundColumn DataField="Status" HeaderText="Status" SortExpression="Status"
                                                AutoPostBackOnFilter="true" AllowFiltering="true" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false" FilterControlWidth="100%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" />--%>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="BtnWinOk" runat="server" Text="Ok" Width="70px" class="submitBtn" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </telerik:RadWindow>
        <%--</Windows>
                </telerik:RadWindowManager>--%>
                
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
    <cc3:User ID="User1" runat="server" />
    <cc2:AptifyShoppingCart ID="ShoppingCart1" runat="server" Visible="False"></cc2:AptifyShoppingCart>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</div>
