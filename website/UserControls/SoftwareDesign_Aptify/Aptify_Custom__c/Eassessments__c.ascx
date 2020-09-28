<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/Eassessments__c.ascx.vb" Inherits="Eassessments__c" %>
<%@ Register TagPrefix="cc1" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="uc1" TagName="CreditCard" Src="~/UserControls/Aptify_Custom__c/CreditCard__c.ascx" %>


<div class="raDiv" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing">
                <img src="/Images/CAITheme/bx_loader.gif" />
                <span>Loading</span>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<%--<asp:Panel ID="pnlDetails" runat="server">--%>

                    

 <div>
                  
                <div class="field-group">
                    <span class="label-title-inline">Name: </span>
                    <asp:Label ID="lblFirstLast" runat="server" Text=""></asp:Label>
                </div>
                <div class="field-group">
                    <span class="label-title-inline">Student number: </span>
                    <asp:Label ID="lblStudentNumber" runat="server" Text=""></asp:Label>
                </div> 
                 <div class="field-group" id="dvCycle" runat="server">
                    
                    <span class="label-title-inline">Academic Cycle: </span>
                    <asp:Label ID="lblAcademicCycle" runat="server" Text=""></asp:Label>
                    <span class="label-title-inline">&emsp;Session: </span>
                    <asp:Label ID="lblSession" runat="server" Text=""></asp:Label>
                </div>
                           
                <div class="field-group">
                    <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                     <%--<asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>--%>
                </div>
            </div>
<div class="field-group">
                                   
   <asp:UpdatePanel ID="gvPanel" runat="server"  UpdateMode="Always"  ChildrenAsTriggers="true" >
        <ContentTemplate>  
             <asp:Label ID="lblNoRecords" runat="server"></asp:Label>
    
                                    <telerik:RadGrid ID="gvCurriculumCourse" runat="server" PageSize="10" AllowSorting="True"
                                        AllowMultiRowSelection="True" AllowFilteringByColumn="false" AllowPaging="True"
                                        ShowGroupPanel="True" GridLines="none" AutoGenerateColumns="false">
                                        <PagerStyle CssClass="sd-pager" />
					                     <ItemStyle VerticalAlign="Top" />
                                        <MasterTableView Width="75%">
                                            <NoRecordsTemplate>
                                                No record(s)
                                            </NoRecordsTemplate>
                                             <Columns>
                                                  <telerik:GridBoundColumn DataField="Curriculum" HeaderText="Curriculum" SortExpression="Curriculum"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Bottom"/>
                                                <telerik:GridBoundColumn DataField="Subject" HeaderText="Subject" SortExpression="Subject"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Bottom"/>
                                                <telerik:GridBoundColumn DataField="SubjectID" HeaderText="SubjectID" SortExpression="SubjectID"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                    Visible="false" />
                                                 <telerik:GridBoundColumn DataField="Type" HeaderText="Type" SortExpression="Type"
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Bottom"/>
                                               
                                                 <telerik:GridTemplateColumn DataField="SubjectID" HeaderText="Select" 
                                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                                    <ItemTemplate>
                                                        <div  id="idivAssessment" runat="server">
                                                            <asp:Label ID="lblAssessment" runat="server" Text='<%# Eval("SubjectID")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblType" runat="server" Text='<%# Eval("Type")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblCurriculumID" runat="server" Text='<%# Eval("CurriculumID")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblSubject" runat="server" Text='<%# Eval("Subject")%>' Visible="false"></asp:Label>
                                                             <asp:Label ID="lblStudentGroup" runat="server" Text='<%# Eval("StudentGroupID__c")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblAlternativeGroup" runat="server" Text='<%# Eval("AlternativeGroup")%>' Visible="false"></asp:Label>
                                                            <asp:CheckBox ID="chkAssessment" runat="server" AutoPostBack="true" OnCheckedChanged="chkAssessment_CheckedChanged"  />
                                                        </div>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                   </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
       <%-- </ContentTemplate>
       </asp:UpdatePanel>--%>
    <br />
                                    <asp:Label ID="lblEnrollmentMsg" runat="server" Text="" ></asp:Label>
                                </div>

                                
           
    
 
   <%-- </asp:Panel>--%>
<div id="dvPaymentSummary" class="info-data cai-form" >
    <asp:Panel ID="UpdatePanel2" runat="server" Visible="false">
    <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server"  UpdateMode="Always" Visible="false" ChildrenAsTriggers="true" >
        <ContentTemplate>  --%>
            <div class="form-title">Payment summary</div>
            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label>
             <br />
            <div class="cai-form-content" >
                                    <asp:Label ID="lblPaymentSummery" runat="server" Text="Payment summary" Visible="false"
                                        Font-Bold="true"></asp:Label>
                                    <asp:GridView ID="GV" runat="server" AutoGenerateColumns="false" CssClass="cai-table">
                                        <Columns>
                                            <asp:BoundField DataField="Key" HeaderText="Subject" />
                                            <asp:BoundField DataField="Value" HeaderText="Price" />
                                        </Columns>
                                    </asp:GridView>
                                    <telerik:RadGrid ID="radSummerPaymentSummery" runat="server" AutoGenerateColumns="False"
                                        AllowPaging="false" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                        AllowFilteringByColumn="false" AllowSorting="false" Width="150px" Visible="false">
                                        <GroupingSettings CaseSensitive="false" />
                                        <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false">
                                            <Columns>
                                                <telerik:GridTemplateColumn HeaderText="Subject" AllowFiltering="false">
                                                    <ItemTemplate>
                                                        <%-- Commented by Paresh for Performance --%>
                                                        <%--<asp:Label ID="lblCurriculumID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CurriculumID") %>'
                                                            Visible="false"></asp:Label>--%>
                                                        <asp:Label ID="lblSubject" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Subject") %>'></asp:Label>
                                                        <%--<asp:Label ID="lblPaymentProductID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ProductID") %>'
                                                            Visible="false"></asp:Label>
                                                        <asp:Label ID="lblIsProductPaymentPlan" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IsProductPaymentPlan") %>'
                                                            Visible="false"></asp:Label>
                                                        <asp:Label ID="lblClassID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ClassID") %>'
                                                            Visible="false"></asp:Label>--%>
                                                        <asp:Label ID="lblAlternateTimeTableOnOrder" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AlternateTimeTable") %>'
                                                            Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Academic cycle ID" AllowFiltering="false"
                                                    Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAcademicCycleID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AcademicCycleID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Type" AllowFiltering="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Type") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Who pays" AllowFiltering="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWhoPay" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WhoPay") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Price" AllowFiltering="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPaymentPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Price") %>'></asp:Label>
                                                        &nbsp;
                                                        <asp:Label ID="lblTaxAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TaxAmount") %>'
                                                            Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                    <div class="info-data">
                                        <div class="row-div clearfix">
                                            &nbsp;</div>
                                        <div class="row-div clearfix">
                                            <div class="label-div-left-align w40">
                                                <div class="info-data">
                                                    <div class="row-div clearfix">
                                                        <div class="label-div w55">
                                                            <b>
                                                                <asp:Label ID="lblAmount" runat="server" Text="Total amount:" Visible="false"></asp:Label></b>
                                                        </div>
                                                        <div class="field-div1 w75">
                                                            <b>
                                                                <asp:Label ID="lblTotalAmount" runat="server" Text="" Visible="false"></asp:Label></b>
                                                            <asp:Label ID="lblStagePaymentTotal" runat="server" Text="" Visible="false"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row-div clearfix">
                                                        <div class="label-div w55">
                                                            <b>
                                                                <asp:Label ID="lblStudentPaidLabel" runat="server" Text="Amount to be paid by student:"
                                                                    Visible="false"></asp:Label></b>
                                                        </div>
                                                        <div class="field-div1 w75">
                                                            <b>
                                                                <asp:Label ID="lblAmountPaidStudent" runat="server" Text="" Visible="false"></asp:Label></b>
                                                        </div>
                                                    </div>
                                                    <div class="row-div clearfix">
                                                        <div class="label-div w55">
                                                            <b>
                                                                <asp:Label ID="lblFirmPaidLabel" runat="server" Text="Amount to be paid by firm:"
                                                                    Visible="false"></asp:Label></b>
                                                        </div>
                                                        <div class="field-div1 w75">
                                                            <b>
                                                                <asp:Label ID="lblAmountPaidFirm" runat="server" Text="" Visible="false"></asp:Label></b>
                                                        </div>
                                                    </div>
                                                    <div class="row-div clearfix">
                                                        <div class="label-div w55">
                                                            <b>
                                                                <asp:Label ID="lblTax" runat="server" Text="Tax amount:" Visible="false"></asp:Label></b>
                                                        </div>
                                                        <div class="field-div1 w75">
                                                            <b>
                                                                <asp:Label ID="lblTaxAmount" runat="server" Text="" Visible="false"></asp:Label></b>
                                                        </div>
                                                    </div>
                                                    
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                &nbsp;
                                    <div class="info-data">
                                        <div class="row-div clearfix">
                                            <div class="label-div-left-align w50">
                                                <div class="info-data">
                                                    <div class="row-div clearfix">
                                                        <div class="label-div w50">
                                                            <asp:Label ID="lblPaymentPlan" runat="server" Text="Payment plan:" Visible="false"></asp:Label></b>
                                                        </div>
                                                        <div class="field-div1 w40">
                                                            <asp:DropDownList ID="ddlPaymentPlan" runat="server" AutoPostBack="true" Width="320px">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="row-div clearfix">
                                                        <div class="label-div w25">
                                                            &nbsp;
                                                        </div>
                                                   <div class="row-div clearfix">
                                                        <div class="label-div w55">
                                                           <b> <asp:Label ID="lblIntialAmt" runat="server" Text="Initial payment:"></asp:Label></b>
                                                        </div>
                                                        <div class="field-div1 w75">
                                                           <b> <asp:Label ID="lblCurrency" runat="server" Text="" Visible="false"></asp:Label>
                                                            <asp:Label ID="txtIntialAmount" runat="server" Enabled="false" Width="320px"></asp:Label></b>
                                                        </div>
                                                    </div>
                                                        <div class="field-div1 w70">
                                                            <telerik:RadGrid ID="radPaymentPlanDetails" runat="server" AutoGenerateColumns="False"
                                                                AllowPaging="false" SortingSettings-SortedDescToolTip="Sorted Descending" SortingSettings-SortedAscToolTip="Sorted Ascending"
                                                                AllowFilteringByColumn="false" Visible="false" AllowSorting="false" Width="200px">
                                                                <GroupingSettings CaseSensitive="false" />
                                                                <MasterTableView AllowFilteringByColumn="false" AllowSorting="false" AllowNaturalSort="false">
                                                                    <Columns>
                                                                        <telerik:GridTemplateColumn HeaderText="Days" AllowFiltering="false" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbldays" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "days") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn HeaderText="Schedule date" AllowFiltering="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblScheduleDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ScheduleDate") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                      							<%--Siddharth- Commented by to remove column--%>
                                                                        <%--<telerik:GridTemplateColumn HeaderText="Percentage" AllowFiltering="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPercentage" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Percentage") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>--%>
									<%--Siddharth- End--%>
                                                                        <telerik:GridTemplateColumn HeaderText="Amount" AllowFiltering="false">
                                                                            <ItemTemplate>
                                                                                <%--<asp:Label ID="lblAmt" runat="server" Text='<%# SetStagePaymentAmount(Eval("Percentage"), (Eval("days"))) %>'></asp:Label>--%>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                            </telerik:RadGrid>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            
					    <div class="field-div1 w200">
                                                <b>
                                                    <uc1:CreditCard ID="CreditCard" runat="server" />
                                                </b>
                                            </div>                                        
				         </div>
                                       
                                    </div>
                                    <p>
                                    </p>
                                </div>
                                <div class="info-data"  >
                 
                                <div class="row-div clearfix">
                                </div>
                                <div class="row-div clearfix">
                                    <div class="label-div-left-align w80 cai-form-content">

                                        <asp:Label ID="lblDPOMsg" runat="server" Text="" Font-Bold="true" ></asp:Label><br />
                                        <%--<asp:Button ID="btnSaveExit" runat="server" Text="Save and Exit" />&nbsp;--%>
                                        <asp:Button ID="btnBack" runat="server" Text="Back"  CausesValidation="false" CssClass="submitBtn cai-btn-red-inverse" />
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="submitBtn"/>
                                   </div>
                                </div>
                               
                    
                    </div>
            <telerik:RadWindow ID="radwindowSubmit" runat="server" VisibleOnPageLoad="false"
                                    Height="170px" Title="Student enrolment" Width="350px" BackColor="#f4f3f1" VisibleStatusbar="false"
                                    Behaviors="None" ForeColor="#BDA797">
                                    <ContentTemplate>
                                        <div class="info-data">
                                            <div class="row-div clearfix">
                                                <b>
                                                    <asp:Label ID="lblSubmitMessage" runat="server" Text=""></asp:Label></b>
                                                <br />
                                            </div>
                                            <div class="row-div clearfix" align="center">
                                                <asp:Button ID="btnSubmitOk" runat="server" Text="Yes" class="submitBtn" Width="20%" />
                                                <asp:Button ID="btnNo" runat="server" Text="No" class="submitBtn" Width="20%" Visible="false" />
                                                <asp:Button ID="btnSuccess" runat="server" Text="Ok" class="submitBtn" Width="20%"
                                                    Visible="false" />
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </telerik:RadWindow>
            <telerik:RadWindow ID="radwindowPaymentFail" runat="server" VisibleOnPageLoad="false"
                Height="170px" Title="Student enrolment" Width="350px" BackColor="#f4f3f1" VisibleStatusbar="false"
                Behaviors="None" ForeColor="#BDA797">
                <ContentTemplate>
                    <div class="info-data">
                        <div class="row-div clearfix">
                            <b>
                                <asp:Label ID="lblPaymentFailed" runat="server" Text=""></asp:Label></b>
                            <br />
                        </div>
                        <div class="row-div clearfix" align="center">
                            <asp:Button ID="btnOk" runat="server" Text="Ok" class="submitBtn" Width="20%" />                            
                        </div>
                    </div>
                </ContentTemplate>
            </telerik:RadWindow> 
         </asp:Panel>
 </ContentTemplate>
    </asp:UpdatePanel>
     
</div>
<div>


                        <telerik:RadWindow ID="radWindowValidation" runat="server" Width="350px" Height="200px"
                                    Modal="True" BackColor="#f4f3f1" VisibleStatusbar="False" Behaviors="None" ForeColor="#BDA797"
                                    Title="Student Enrolment" Behavior="None" VisibleOnPageLoad ="false">
                                    <ContentTemplate>
                                        <div class="info-data">
                                            <div class="row-div clearfix">
                                                <b>
                                                    <asp:Label ID="lblValidation" runat="server" Text=""></asp:Label></b>
                                                <br />
                                                <br />
                                            </div>
                                            <div class="row-div clearfix" align="center">
                                                <asp:Button ID="btnValidationOK" runat="server" Text="Ok" Width="20%" class="submitBtn" CausesValidation="false" />
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </telerik:RadWindow>
                    </div>
<div>
    <cc1:User ID="AptifyEbusinessUser1" runat="server" />
</div>
   
