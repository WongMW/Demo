﻿<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/StudentEnrollmentPaymentSummery__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.StudentEnrollmentPaymentSummery__c" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="EBusinessGlobal" %>
<%@ Register TagPrefix="cc3" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>
<%@ Register TagPrefix="uc1" TagName="CreditCard" Src="~/UserControls/Aptify_Custom__c/CreditCard__c.ascx" %>
<%--<script type="text/javascript">
    function ShowProgress(obj) {
        
        setTimeout(function () {
            var modal = $('<div />');
            modal.addClass("modal");
            $('body').append(modal);
            var loading = $(".loading");
            loading.show();
            var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
        }, 10);

        __doPostBack(obj);
    }

</script>
<style type="text/css">
        .loading {
        font-family: Arial;
        font-size: 10pt;
        border: 5px solid #67CFF5;
        width: 200px;
        height: 100px;
        display: none;
        position: fixed;
        background-color: White;
        z-index: 999;
    }

    .modal {
        position: fixed;
        top: 0;
        left: 0;
        background-color: black;
        z-index: 99;
        opacity: 0.8;
        filter: alpha(opacity=30);
        -moz-opacity: 0.8;
        min-height: 100%;
        width: 100%;
    }

</style>--%>

<div class="raDiv" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="dvProcessing"><div class="loading-bg">
                <img src="/Images/CAITheme/bx_loader.gif" />
                <span>This process can take a few minutes.<br />
                    WARNING: Please do not close this window while payment is processing.</span></div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>

<div class="info-data cai-form">
    <div class="form-title">Payment summary</div>
     <asp:UpdatePanel ID="UpdatePanel2" runat="server"  UpdateMode="Always">
        <ContentTemplate>  
            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
             <br /><asp:Label ID="lblAutumnMsg" runat="server"  Font-Bold="true" Visible="false"></asp:Label>
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
                                        AllowFilteringByColumn="false" Visible="false" AllowSorting="false" Width="150px">
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
                                                            Visible="false"></asp:Label>
                                                        <asp:Label ID="lblAlternateTimeTableOnOrder" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AlternateTimeTable") %>'
                                                            Visible="false"></asp:Label>--%>
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
                                                                                <asp:Label ID="lblAmt" runat="server" Text='<%# SetStagePaymentAmount(Eval("Percentage"),(Eval("days"))) %>'></asp:Label>
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
            <cc3:User ID="User1" runat="server" />
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
	     <%--Siddharth- Added below control--%>
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
            <%--Siddharth- End--%>        
  </ContentTemplate>
    </asp:UpdatePanel> 
     
</div>

