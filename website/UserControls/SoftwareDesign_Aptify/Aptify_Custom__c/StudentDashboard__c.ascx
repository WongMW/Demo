<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/StudentDashboard__c.ascx.vb"
    Inherits="UserControls_Aptify_Custom__c_StudentDashboard__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<cc1:User runat="server" ID="User1" />

<telerik:RadWindowManager runat="server" ID="RadWindowManager">
</telerik:RadWindowManager>
<div>
    <asp:UpdatePanel ID="UpAreasOfExp" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="main-container clearfix">
                <div class="aptify-category-inner-side-nav">
                    <div id="divMenu" runat="server">
                        <h6>MENU:</h6>
                        <ul>
                            <li>
                                <asp:LinkButton ID="lnkCreateNewDiaryEntry" runat="server">Create a new diary entry</asp:LinkButton>
                            </li>
                            <li>
                                <asp:LinkButton ID="lnkModifyDiaryEntry" runat="server">Modify an existing diary entry</asp:LinkButton>
                            </li>
                            <li>
                                <asp:LinkButton ID="lnkReqMentorReview" runat="server">Request a 6 monthly mentor review</asp:LinkButton>
                            </li>
                            <li>
                                <asp:LinkButton ID="lnkSubmitQuerytoCAI" runat="server" visible="false">Submit query to the Institute</asp:LinkButton>
                            </li>
                            <li>
                                <asp:LinkButton ID="lnkCADiaryReport" runat="server">Generate CA Diary report</asp:LinkButton>
                            </li>
                            <li>
                                <asp:LinkButton ID="lnkAdmissiontoMembership"
                                    runat="server">Admission to membership request</asp:LinkButton>
                            </li>

                            <li>
                                <asp:LinkButton ID="lnkViewMentorReview" runat="server">View mentor review</asp:LinkButton>
                            </li>
                            <li>
                                <asp:LinkButton ID="lnkRequestforFinalReview" runat="server">Request for final review</asp:LinkButton>
                            </li>
                            <li>
                                <asp:LinkButton ID="lnkCADiaryGuidelines" runat="server">CA Diary guidelines</asp:LinkButton>
                            </li>
                            <li>
                                <asp:LinkButton ID="lnkCAIUpdates" runat="server">Institute updates</asp:LinkButton>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="container-left ">
                    <div class="warning-msg-box">
                        <p>
                            Please disable your browser's popup blocker before you download your CA Diary report.
                            <strong>
                                <asp:LinkButton ID="lblPopups" runat="server">
					<a href="/Prospective-Students/browser-popups.aspx" target="_blank" style="text-decoration:underline;">You can find out how to do this here</a></asp:LinkButton>
                            </strong>
                        </p>
                    </div>
                    <div class="cai-form">
                        <span class="form-title">Profile information:</span>

                        <div class="field-group">
                            <asp:LinkButton ID="lnkHistoryProfileInfo" runat="server" Font-Underline="true">History of profile information</asp:LinkButton>
                        </div>

                        <div class="field-group">
                            <span class="label-title-inline">Student#:</span>
                            <asp:Label ID="lblStudentName" runat="server" Text=""></asp:Label>
                        </div>

                        <div class="field-group">
                            <span class="label-title-inline">Mentor:</span>
                            <asp:Label ID="lblMentorName" runat="server" Text=""></asp:Label>
                            <asp:HiddenField ID="hidMetorID" Value="0" runat="server" />
                        </div>

                        <div class="field-group">
                            <span class="label-title-inline">Company:</span>
                            <asp:Label ID="lblCompanyName" Style="font-weight: normal;" runat="server" Text=""></asp:Label>
                            <asp:HiddenField ID="hidCompany" Value="0" runat="server" />
                            <asp:HiddenField ID="hidECCompanyID" Value="0" runat="server" />
							<div><span>Contract trainees should select the 'History of profile information' link above to view contract dates</span></div>
                        </div>

                        <div class="field-group">
                            <span class="label-title-inline">Date of registration:</span>
                            <asp:Label ID="lblRegDate" runat="server" Text=""></asp:Label>
                        </div>

                        <div class="field-group">
                            <span class="label-title-inline">Route of entry:</span>
                            <asp:Label ID="lblRouteOfEntry" Style="font-weight: normal;" runat="server" Text=""></asp:Label>
                            <asp:HiddenField ID="hidRouteOfEntry" Value="0" runat="server" />
                        </div>

                        <div class="field-group">
                            <span class="label-title-inline">Membership requirements must be met by:</span>
                            <asp:Label ID="lblDateOfCompletion" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
            </div>

            <br />

            <div class="main-container clearfix">
                <div class="dashboard-left">
                    <div class="cai-form">
                        <span class="form-title">Mandatory core competencies : </span>

                        <div class="cai-form-content">
                            <asp:Label ID="lblNoMandatoryCoreCompetencies" Visible="false" runat="server" Style="color: Red;"
                                Text=""></asp:Label>
                            <div class="cai-table mobile-table">
                                <asp:GridView ID="grdMandatoryCoreCompetencies" AllowPaging="false"
                                    AutoGenerateColumns="False" runat="server" OnDataBound="OnMandatoryCoreCompetenciesDataBound">
                                    <Columns>
                                        <%--  <asp:TemplateField HeaderText="Category" HeaderStyle-CssClass="rgHeader">
                                            <ItemTemplate>
                                                <span class="mobile-label">Category:</span>
                                                <asp:Label runat="server" CssClass="cai-table-data" Text='<%# Eval("CompetancyCategory")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code" HeaderStyle-CssClass="rgHeader">
                                            <ItemTemplate>
                                                <span class="mobile-label">Code:</span>
                                                <asp:Label runat="server" CssClass="cai-table-data" Text='<%# Eval("CompetencyCode")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="rgHeader">
                                            <ItemTemplate>
                                                <span class="mobile-label">Name:</span>
                                                <asp:Label runat="server" CssClass="cai-table-data" Text='<%# Eval("CompetencyName")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Understand" HeaderStyle-CssClass="rgHeader">
                                            <ItemTemplate>
                                                <span class="mobile-label">Understand:</span>
                                                <asp:Label runat="server" CssClass="cai-table-data" Text='<%# Eval("Understand")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Apply" HeaderStyle-CssClass="rgHeader">
                                            <ItemTemplate>
                                                <span class="mobile-label">Apply:</span>
                                                <asp:Label runat="server" CssClass="cai-table-data" Text='<%# Eval("Apply")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Integrate" HeaderStyle-CssClass="rgHeader">
                                            <ItemTemplate>
                                                <span class="mobile-label">Integrate:</span>
                                                <asp:Label runat="server" CssClass="cai-table-data" Text='<%# Eval("Integrate")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <asp:BoundField HeaderText="Category" ItemStyle-BackColor="White" HeaderStyle-CssClass="rgHeader" ItemStyle-HorizontalAlign="left"
                                            ItemStyle-Font-Bold="true" DataField="CompetancyCategory" />
                                        <asp:BoundField HeaderText="Code" ItemStyle-BackColor="White" HeaderStyle-CssClass="rgHeader" ItemStyle-Width="30px"
                                            DataField="CompetencyCode" />
                                        <asp:BoundField HeaderText="Name" ItemStyle-BackColor="White" HeaderStyle-CssClass="rgHeader" DataField="CompetencyName"
                                            ItemStyle-Width="180px" />
                                        <asp:BoundField HeaderText="Understand" ItemStyle-BackColor="White" HeaderStyle-CssClass="rgHeader" DataField="Understand"
                                            ItemStyle-Width="50px" />
                                        <asp:BoundField HeaderText="Apply" ItemStyle-BackColor="White" HeaderStyle-CssClass="rgHeader" DataField="Apply"
                                            ItemStyle-Width="50px" />
                                        <asp:BoundField HeaderText="Integrate" ItemStyle-BackColor="White" HeaderStyle-CssClass="rgHeader" DataField="Integrate"
                                            ItemStyle-Width="50px" />

                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div>
                                <span class="label-title">Guidance (please refer to the document ‘Guide to Professional Development Requirements on CA Diary’ for details of competency requirements for admission to membership)</span>
                            </div>
                            <ul class="bullet-list">
                                <li runat="server" id="divCompetenciesGuidence1"></li>

                                <li runat="server" id="divCompetenciesGuidence2"></li>

                                <li runat="server" id="divCompetenciesGuidence3"></li>
                            </ul>
                        </div>
                    </div>
                </div>

                <div class="dashboard-right">
                    <div class="cai-form">
                        <span class="form-title">Area of experience :</span>
                        <div class="cai-form-content">
                            <div>
                                <asp:DropDownList ID="ddlAreasOfExp" AutoPostBack="true" runat="server">
                                </asp:DropDownList>
                            </div>
                            <br />

                            <div class="cai-form-data">
                                <asp:Label ID="lblNoAreasofExperienceCompetencies" Visible="false" runat="server"
                                    Style="color: Red;"></asp:Label>

                                <div class="cai-table mobile-table">
                                    <asp:GridView ID="grdAreasofExperience" AllowPaging="false"
                                        AutoGenerateColumns="False" runat="server" OnDataBound="OnAreasofExperienceDataBound">
                                        <Columns>
                                            <%--<asp:TemplateField HeaderText="Code" HeaderStyle-CssClass="no-mob">
                                                <ItemTemplate>
                                                    <span class="mobile-label">Code:</span>
                                                    <asp:Label runat="server" Text='<%# Eval("CompetencyCode")%>' CssClass="cai-table-data"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="no-mob">
                                                <ItemTemplate>
                                                    <span class="mobile-label">Name:</span>
                                                    <asp:Label runat="server" Text='<%# Eval("CompetencyName")%>' CssClass="cai-table-data"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Understand" HeaderStyle-CssClass="no-mob">
                                                <ItemTemplate>
                                                    <span class="mobile-label">Understand:</span>
                                                    <asp:Label runat="server" Text='<%# Eval("Understand")%>' CssClass="cai-table-data"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Apply" HeaderStyle-CssClass="no-mob">
                                                <ItemTemplate>
                                                    <span class="mobile-label">Apply:</span>
                                                    <asp:Label runat="server" Text='<%# Eval("Apply")%>' CssClass="cai-table-data"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Integrate" HeaderStyle-CssClass="no-mob">
                                                <ItemTemplate>
                                                    <span class="mobile-label">Integrate:</span>
                                                    <asp:Label runat="server" Text='<%# Eval("Integrate")%>' CssClass="cai-table-data"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:BoundField HeaderText="" ItemStyle-BackColor="White" ItemStyle-Width="30px"
                                                DataField="CompetencyCode" />
                                            <asp:BoundField HeaderText="" ItemStyle-BackColor="White" DataField="CompetencyName"
                                                ItemStyle-Width="180px" />
                                            <asp:BoundField HeaderText="Understand" ItemStyle-BackColor="White" DataField="Understand"
                                                ItemStyle-Width="40px" />
                                            <asp:BoundField HeaderText="Apply" ItemStyle-BackColor="White" DataField="Apply"
                                                ItemStyle-Width="30px" />
                                            <asp:BoundField HeaderText="Integrate" ItemStyle-BackColor="White" DataField="Integrate"
                                                ItemStyle-Width="40px" />
                                        </Columns>
                                    </asp:GridView>
                                </div>

                                <span class="label-title">Guidance:</span>
                                <div runat="server" id="divAreaofExpGuidance"></div>
                                <div runat="server" id="div2"></div>
                                <div runat="server" id="div3"></div>
                            </div>
                        </div>
                    </div>


                    <div class="actions">
                        <asp:Button ID="btnMainBack" runat="server" Visible="false" Text="Back" class="submitBtn" />
                    </div>
                </div>
            </div>

            <div class="main-container  clearfix">
                <div class="dashboard-left">
                    <div class="cai-form">
                        <span class="form-title">Regulated experience in a practice environment : </span>
                        <div class="cai-form-content">
                            <asp:Panel ID="Panel2" runat="server">
                                <div class="cai-table">
                                    <table class="mobile-table">
                                        <tr>
                                            <td>
												For trainees gaining audit experience in ROI only
                                            </td>
											<%--Changed heading for redmine log #18753--%>
                                            <td class="mobile-table-label">
                                                Number of weeks achieved to date
                                            </td>
											<%--Commented column for redmine log #18753--%>
                                            <%--<td class="mobile-table-label">
                                                Minimum required for auditing requirement (weeks)
                                            </td>--%>
                                        </tr>
										<tr>
                                            <td class="mobile-table-label">
                                                Statutory audit (ROI only) <%--Changed heading for redmine log #20233--%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblStatutoryAuditDay" runat="server" Text="0"></asp:Label>
                                            </td>											
                                        </tr>
                                      
                                        <tr>
                                            <td class="mobile-table-label">
                                                Other audit (ROI only)  <%--Changed heading for redmine log #20233--%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblOtherAuditday" runat="server" Text="0"></asp:Label>
                                            </td>
											<%--Commented column for redmine log #18753--%>
                                            <%--<td>
                                            </td>--%>
                                        </tr>
										<%--tr added for redmine log #20233--%>
                                        <tr>
                                            <td class="mobile-table-label">
                                              <%--  Audit experience total weeks--%>
												Total Audit weeks (ROI)  <%--Changed heading for redmine log #20233--%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTotalAuditday" runat="server" Text="0"></asp:Label>
                                            </td>
											<%--Commented column for redmine log #18753--%>
                                            <%--<td>
                                                <asp:Label ID="lblMinimumOtherAuditday" runat="server" Text=""></asp:Label>
                                            </td>--%>
                                        </tr>
										<tr>
											<td colspan="2">&nbsp;</td>
										</tr>
										<tr>
											<td colspan="2">&nbsp;</td>
										</tr>
										 <tr>
                                            <td>
												For trainees gaining audit experience in NI/UK only   <%--added for redmine log #20233--%>
                                            </td>
											<%--Changed heading for redmine log #18753--%>
                                            <td class="mobile-table-label">
                                                Number of weeks achieved to date
                                            </td>
											
                                        </tr>

										  <tr>
                                            <td class="mobile-table-label">
                                                Company audit (NI/UK only)  <%--added for redmine log #20233--%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCompanyAuditday" runat="server" Text="0"></asp:Label> <%--added for redmine log #20233--%>
                                            </td>
											<%--Commented column for redmine log #18753--%>
                                            <%--<td>
                                                <asp:Label ID="lblMinimumCompanyAuditday" runat="server" Text=""></asp:Label>
                                            </td>--%>
                                        </tr>
										  <tr>
                                            <td class="mobile-table-label">
                                                Other audit (NI/UK only) <%--added for redmine log #20233--%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblOtherAuditNIday" runat="server" Text="0"></asp:Label> <%--added for redmine log #20233--%>
                                            </td>
											<%--Commented column for redmine log #18753--%>
                                            <%--<td>
                                            </td>--%>
                                        </tr>
                                        <tr>
                                            <td class="mobile-table-label">
                                              <%--  Audit experience total weeks--%>
												Total Audit weeks (NI/UK) <%--added for redmine log #20233--%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTotalAuditNIday" runat="server" Text="0"></asp:Label> <%--added for redmine log #20233--%>
                                            </td>
											 
                                        </tr>
										<%--tr end for redmine log #20233--%>
                                    </table>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
                <div class="dashboard-right">
                    <div class="cai-form">
                        <span class="form-title">Required experience:</span>
                        <div class="cai-form-content cai-table">
                            <asp:Panel ID="Panel3" runat="server">
                                <table class="mobile-table">
                                    <tr>
                                        <td></td>
                                        <td class="mobile-table-label">Days recorded to date</td>
                                        <td class="mobile-table-label">Required days</td>
                                    </tr>
                                    <tr>
                                        <td class="mobile-table-label">Prior work experience</td>
                                        <td>
                                            <asp:Label ID="lblPriorWorkExperienceToDate" runat="server" Text="0"></asp:Label></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="mobile-table-label">Required experience</td>
                                        <td>
                                            <asp:Label ID="lblReqExperienceToDate" runat="server" Text="0"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblReqExperienceReqdays" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="mobile-table-label">Total experience</td>
                                        <td>
                                            <asp:Label ID="lblTotalExperienceToDate" runat="server" Text="0"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblTotalExperienceReqdays" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                </table>
                                <!-- Added BY Kavita #18048 -->
                                <span class="label-title">Other</span>
                                <table class="mobile-table">
                                    <tr>
                                        <td class="mobile-table-label">
                                            Overtime Worked
                                        </td>
                                        <td>
                                            <asp:Label ID="lblOverTimeWorked" runat="server" Text="0"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                                <!-- Ended BY Kavita #18048 -->
                                <span class="label-title">Out of office</span>
                                <div>
                                    <asp:GridView ID="grdOutofOffice" ShowHeader="false" ShowFooter="true"
                                        AllowPaging="false" AutoGenerateColumns="False" runat="server" CssClass="grid-style mobile-table">
                                        <Columns>
                                            <%-- <asp:TemplateField FooterText="Total - out of office" FooterStyle-CssClass="mobile-table-label">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" CssClass="mobile-table-label" Text='<%# Eval("Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" CssClass="cai-table-data" Text='<%# Eval("LeaveDay") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:BoundField HeaderText="" ItemStyle-BackColor="White" DataField="Name" FooterText="Total -Out of Office" />
                                            <asp:BoundField HeaderText="" ItemStyle-BackColor="White" DataField="LeaveDay" />
                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

            <div class="main-container clearfix">
                <div class="cai-form">
                    <span class="form-title">Progress summary :</span>
                    <asp:Panel ID="Panel1" runat="server" CssClass="dashboard-split clearfix">
                        <div class="dashboard-section-half">
                            <span class="label-title">Diary entries</span>
                            <div class="cai-table">
                                <table class="mobile-table">
                                    <tr>
                                        <td class="mobile-table-label">In progress:</td>
                                        <td>
                                            <asp:Label CssClass="cai-table-data" ID="lblInProgressDiaryEntry" runat="server" Text="0"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="mobile-table-label">Submitted to mentor for review:</td>
                                        <td>
                                            <asp:Label CssClass="cai-table-data" ID="lblMentorDiaryEntry" runat="server" Text="0"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="mobile-table-label">Reviewed(locked):</td>
                                        <td>
                                            <asp:Label CssClass="cai-table-data" ID="lblReviewDiaryEntry" runat="server" Text="0"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="mobile-table-label">Total diary entries
                                        </td>
                                        <td>
                                            <asp:Label CssClass="cai-table-data" ID="lblTotalDiaryEntry" runat="server" Text="0"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>

                        <div class="dashboard-section-half">
                            <span class="label-title">Mentor review</span>
                            <div class="cai-table">
                                <table class="mobile-table">
                                    <tr>
                                        <td class="mobile-table-label">Number of mentor reviews:</td>
                                        <td>
                                            <asp:Label CssClass="cai-table-data" ID="lblNumbeofMentorReviews" runat="server" Text="0"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="mobile-table-label">Date of last review: </td>
                                        <td>
                                            <asp:Label CssClass="cai-table-data" ID="lblDateofLastReview" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="mobile-table-label">Target next review date:</td>
                                        <td>
                                            <asp:Label CssClass="cai-table-data" ID="lblDateofNextReview" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>

            <telerik:RadWindow ID="radwindowHistory" runat="server" VisibleOnPageLoad="false"
                Height="390px" Title=" History of profile information" Width="550px" BackColor="#f4f3f1"
                VisibleStatusbar="false" Behaviors="None" ForeColor="#BDA797" Skin="Default">
                <ContentTemplate>
                    <div class="info-data">
                        <div>
                            <asp:Label ID="lblHistoryMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </div>
                        <div>
                            <rad:RadGrid ID="grdHistoryProfileInfo" runat="server" AutoGenerateColumns="false"
                                ShowHeader="true" Style="margin-top: 13px; overflow: auto;" CellSpacing="0" GridLines="None"
                                Height="200px">
                                <ClientSettings>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
                                </ClientSettings>
                                <MasterTableView ShowHeadersWhenNoRecords="true">
                                    <Columns>
                                        <rad:GridTemplateColumn HeaderText="Start date" HeaderStyle-Font-Size="Medium" headerstyle-font-bold="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStartDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ContractStartDate","{0:dd/MM/yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridTemplateColumn HeaderText="End date" HeaderStyle-Font-Size="Medium" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEndDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ContractExpireDate","{0:dd/MM/yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridTemplateColumn HeaderText="Route of entry" HeaderStyle-Font-Size="Medium" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRouteofEntry" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"RouteOfEntry")%>'></asp:Label>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                        <rad:GridTemplateColumn HeaderText="Company" HeaderStyle-Font-Size="Medium" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompany" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CompanyName")%>'></asp:Label>
                                            </ItemTemplate>
                                        </rad:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </rad:RadGrid>
                            <div>
                            </div>
                            <div runat="server" visible="false" id="divStartEndDateNote">
                            </div>
                        </div>
                        <div>
                            <asp:Button ID="btnBack" runat="server" Text="Back" class="submitBtn" />
                        </div>
                    </div>
                </ContentTemplate>
            </telerik:radwindow>
            <telerik:radwindow id="radWindowReqMentorReview" runat="server" width="400px" modal="True"
                backcolor="#f4f3f1" visiblestatusbar="False" behaviors="None" forecolor="#BDA797"
                title="Monthly review from mentor" behavior="None" height="190px" skin="Default">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblReviewFromMentor" runat="server" Font-Bold="true" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    &nbsp;
                                </div>
                                <div style="text-align:center;">
                                    <asp:Button ID="btnReviewYes" runat="server" Text="Yes" class="submitBtn" />
                                    <asp:Button ID="btnReviewNo" runat="server" Text="No" class="submitBtn" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </telerik:radwindow>
            <telerik:radwindow id="radWindowSubmitaQuerytoCAI" runat="server" width="450px" height="320px"
                modal="True" backcolor="#f4f3f1" visiblestatusbar="False" behaviors="None" forecolor="#BDA797"
                title="Submit a query to Chartered Accountants Ireland" behavior="None" skin="Default">
                <ContentTemplate>
                    <div>

                        <div>
                            <div class="lable-c w30">
                            </div>
                            <div class=" w175">
                                <div>
                                    <%--<asp:Label ID="lblValidationQuerytoCAI" runat="server" Style="color: Red;" Text=""></asp:Label>--%>
                                    <div>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlQuestionCategory"
                                            ErrorMessage="Please select question category" CssClass="required-label" Operator="NotEqual"
                                            ValidationGroup="S" ValueToCompare="Select" SetFocusOnError="true"></asp:CompareValidator>
                                    </div>
                                    <div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtQuestion"
                                            ErrorMessage="Please enter question" ValidationGroup="S" SetFocusOnError="true"
                                            Display="Dynamic" CssClass="required-label"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div>
                            <div class="lable-c w30">
                                <span style="color: Red;">*</span> Question category:
                            </div>
                            <div class=" w175">
                                <asp:DropDownList ID="ddlQuestionCategory" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div>
                            <div class="lable-c w30">
                                <span style="color: Red;">*</span>Question:
                            </div>
                            <div class=" w375">
                                <asp:TextBox ID="txtQuestion" Style="resize: none;" TextMode="MultiLine" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div>
                        </div>
                        <div class="actions">
                            <asp:Button ID="btnSubmitQuestion" ValidationGroup="S" runat="server" Text="Submit" class="submitBtn" />
                            <asp:Button ID="btnCancelQuestion" runat="server" Text="Cancel" class="submitBtn" />
                        </div>
                        <div>
                        </div>
                    </div>
                </ContentTemplate>
            </telerik:radwindow>
            <telerik:radwindow id="radWindowValidation" runat="server" width="350px" modal="True"
                backcolor="#f4f3f1" visiblestatusbar="False" behaviors="None" forecolor="#BDA797"
                title="Request for final review" behavior="None" height="200px" skin="Default">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblValidationMsg" runat="server" Font-Bold="true" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    &nbsp;
                                </div>
                                <div style="text-align: center;">
                                    <asp:Button ID="btnValidationOK" runat="server" Text="Ok" class="submitBtn" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </telerik:RadWindow>

            <telerik:RadWindow ID="radWindowReqFinalReview" runat="server" Width="350px" Modal="True"
                BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                Title="Request for final review" Behavior="None" Height="150px" Skin="Default">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblReqFinalReview" runat="server" Font-Bold="true" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                </div>
                                <div>
                                    <asp:Button ID="btnReqFinalReviewYes" runat="server" Text="Yes" class="submitBtn" />
                                    <asp:Button ID="btnReqFinalReviewNo" runat="server" Text="No" class="submitBtn" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </telerik:RadWindow>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
