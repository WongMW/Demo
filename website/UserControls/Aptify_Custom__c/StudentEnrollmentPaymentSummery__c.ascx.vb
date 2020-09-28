'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Govind Mande               28 Oct 2015                       Student Enrollment Payment Summery Details
'Sheela Jarali              04 May 2018                       Enrolment submission Successfull message    
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness
Imports Telerik.Web.UI
Imports Aptify.Applications.Accounting
Imports Aptify.Applications.OrderEntry

Namespace Aptify.Framework.Web.eBusiness
    Partial Class StudentEnrollmentPaymentSummery__c
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_StudentEnrollmentPage As String = "RedirectURL"
        'Added by sheela for Enrolment Submission Message Change(CNM-7)
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_StudentEnrollmentConfirmation As String = "EnrollConfirmationURL"
        'Dim SummerPaymentSummery As DataTable '' Commented by Paresh as never used




#Region "Property Setting"
        Public Overridable Property LoginPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        Public Overridable Property StudentEnrollmentPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_StudentEnrollmentPage) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_StudentEnrollmentPage))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_StudentEnrollmentPage) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        'Added by sheela for Enrolment Submission Message Change(CNM-7)
        Public Overridable Property StudentEnrollmentConfirmation() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_StudentEnrollmentConfirmation) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_StudentEnrollmentConfirmation))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_StudentEnrollmentConfirmation) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property


        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(LoginPage) Then
                LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage)
            End If
            If String.IsNullOrEmpty(StudentEnrollmentPage) Then
                StudentEnrollmentPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_StudentEnrollmentPage)
            End If
            'Added by sheela for Enrolment Submission Message Change(CNM-7)
            If String.IsNullOrEmpty(StudentEnrollmentConfirmation) Then
                StudentEnrollmentConfirmation = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_StudentEnrollmentConfirmation)
            End If

        End Sub

#End Region

#Region "Page Events"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                SetProperties()

                Dim _ScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
                _ScriptManager.AsyncPostBackTimeout = "1000"
                lblDPOMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.Data Protection Officer Msg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                If User1.PersonID <= 0 Then
                    Session("ReturnToPage") = Request.RawUrl
                    Response.Redirect(LoginPage)
                End If

                If Not IsPostBack Then
                    Session("PlaceOrder") = False
                    GetPrefferedCurrency()
                    CreditCard.LoadCreditCardInfo()
                    LoadData()
                    LoadEEAppData()
                    ''Added by Paresh for Performance
                    CreateOrderObjectsOnLoad()
                    ''
                    Dim sSQlFirmAdmin As String = Database & "..spGetUserFirmAdmin__c @PersonID=" & User1.PersonID
                    Dim iFirmAdmin As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSQlFirmAdmin, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    If iFirmAdmin > 0 Then
                        ViewState("IsFirmAdmin") = 1
                    End If
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

#Region "Load Event"
        Private Sub GetPrefferedCurrency()
            Try
                Dim sSql As String = Database & "..spGetPrefferedCurencyTypeSymbol__c @PersonID=" & User1.PersonID
                Dim dt As DataTable = DataAction.GetDataTable(sSql)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    ViewState("CurrencyTypeID") = Convert.ToString(dt.Rows(0)("CurrencySymbol"))
                    lblCurrency.Text = ViewState("CurrencyTypeID")
                End If


            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub LoadData()
            Try
                If Not Session("SummerPaymentSummery") Is Nothing Then
                    Dim SummerPaymentSummery As DataTable = CType(Session("SummerPaymentSummery"), DataTable)
                    If Not SummerPaymentSummery Is Nothing AndAlso SummerPaymentSummery.Rows.Count > 0 Then
                        Dim dicSum As New Dictionary(Of String, Decimal)()
                        ''For Each row As DataRow In SummerPaymentSummery.Rows
                        ''    Dim sType As String = row("Type").ToString()
                        ''    If sType = "Revision" OrElse sType = "Autumn Exam" OrElse sType = "Repeat Revision" Then
                        ''        lblAutumnMsg.Visible = True
                        ''        lblAutumnMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.AutumRevisionMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        ''    End If
                        ''    Dim group As String = row("Subject").ToString()
                        ''    Dim rate As Decimal = CDec(Convert.ToString(row("Price")).Substring(1, Convert.ToString(row("Price")).Length - 1))
                        ''    If dicSum.ContainsKey(group) Then
                        ''        dicSum(group) += rate
                        ''    Else
                        ''        dicSum.Add(group, rate)
                        ''    End If
                        ''Next
                        ''GV.DataSource = dicSum
                        ''GV.DataBind()
                        Dim sProductID As String = String.Empty
                        If Not SummerPaymentSummery Is Nothing AndAlso SummerPaymentSummery.Rows.Count > 0 Then
                            radSummerPaymentSummery.DataSource = SummerPaymentSummery
                            radSummerPaymentSummery.DataBind()
                            ' 'radSummerPaymentSummery.Visible = True
                            lblAmount.Visible = True
                            lblTotalAmount.Visible = True
                            Dim dAmt As Decimal = 0
                            lblStudentPaidLabel.Visible = True
                            lblAmountPaidStudent.Visible = True
                            lblFirmPaidLabel.Visible = True
                            lblAmountPaidFirm.Visible = True
                            lblCurrency.Visible = True
                            txtIntialAmount.Visible = True
                            lblCurrency.Visible = True
                            lblIntialAmt.Visible = True
                            Dim dAmountPaidByStudent As Decimal = 0
                            Dim dAmountPaidByFirm As Decimal = 0
                            Dim dPaymentPlanAmount As Decimal = 0
                            Dim dTaxAmount As Decimal = 0
                            For Each dr As DataRow In SummerPaymentSummery.Rows
                                If dAmt = 0 Then
                                    dAmt = CDec(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1))
                                    If Convert.ToString(dr("WhoPay")).Trim.ToLower = "member pay" Then
                                        dAmountPaidByStudent = CDec(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1))
                                    ElseIf Convert.ToString(dr("WhoPay")).Trim.ToLower = "firm pay" Then
                                        dAmountPaidByFirm = CDec(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1))
                                    End If

                                Else
                                    dAmt = Convert.ToDecimal(dAmt + CDbl(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1)))
                                    If Convert.ToString(dr("WhoPay")).Trim.ToLower = "member pay" Then
                                        dAmountPaidByStudent = Convert.ToDecimal(dAmountPaidByStudent + CDbl(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1)))
                                    ElseIf Convert.ToString(dr("WhoPay")).Trim.ToLower = "firm pay" Then
                                        dAmountPaidByFirm = Convert.ToDecimal(dAmountPaidByFirm + CDbl(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1)))
                                    End If
                                End If
                                If dr("IsProductPaymentPlan") = 1 Then
                                    If sProductID = "" Then
                                        sProductID = dr("ProductID")
                                    Else
                                        sProductID = sProductID + "," + dr("ProductID")
                                    End If
                                    If dPaymentPlanAmount = 0 Then

                                        dPaymentPlanAmount = CDec(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1))
                                    Else
                                        dPaymentPlanAmount = Convert.ToDecimal(dPaymentPlanAmount + CDbl(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1)))
                                    End If
                                End If
                                If dTaxAmount = 0 Then
                                    dTaxAmount = CDec(dr("TaxAmount"))
                                Else
                                    dTaxAmount = dTaxAmount + CDec(dr("TaxAmount"))
                                End If

                                Dim sType As String = dr("Type").ToString()
                                If sType = "Revision" OrElse sType = "Autumn Exam" OrElse sType = "Repeat Revision" Then
                                    lblAutumnMsg.Visible = True
                                    lblAutumnMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.AutumRevisionMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                End If
                                Dim group As String = dr("Subject").ToString()
                                Dim rate As Decimal = CDec(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1))
                                If dicSum.ContainsKey(group) Then
                                    dicSum(group) += rate
                                Else
                                    dicSum.Add(group, rate)
                                End If
                                ''Added BY Pradip 2016-01-22
                                ' ''Dim group As String = dr("Subject").ToString()
                                ' ''Dim rate As Decimal = CDec(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1))
                                ' ''If dicSum.ContainsKey(group) Then
                                ' ''    dicSum(group) += rate
                                ' ''Else
                                ' ''    dicSum.Add(group, rate)
                                ' ''End If

                                ''End here Added BY Pradip 2016-01-22
                            Next

                            GV.DataSource = dicSum
                            GV.DataBind()
                            ''End here Added BY Pradip 2016-01-22
                            lblStagePaymentTotal.Text = ViewState("CurrencyTypeID") & Format(CDec(dPaymentPlanAmount), "0.00")
                            lblTotalAmount.Text = ViewState("CurrencyTypeID") & Format(CDec(dAmt), "0.00")
                            lblAmountPaidStudent.Text = ViewState("CurrencyTypeID") & Format(CDec(dAmountPaidByStudent), "0.00")
                            lblAmountPaidFirm.Text = ViewState("CurrencyTypeID") & Format(CDec(dAmountPaidByFirm), "0.00")
                            'If dPaymentPlanAmount > 0 Then
                            '    txtIntialAmount.Text = ""
                            'Else
                            Dim dTotalAmtPaidWithTax As Decimal = (dAmountPaidByStudent + dTaxAmount)
                            txtIntialAmount.Text = Format(dTotalAmtPaidWithTax, "0.00")

                            'txtIntialAmount.Text = Convert.ToString(lblAmountPaidStudent.Text).Substring(1, Convert.ToString(lblAmountPaidStudent.Text).Length - 1).Trim
                            'End If
                            lblPaymentSummery.Visible = True
                            lblTaxAmount.Text = ViewState("CurrencyTypeID") & Format(CDec(dTaxAmount), "0.00")
                            CreditCard.Visible = True
                            If sProductID <> "" Then
                                LoadPaymentPlan(sProductID)
                                ddlPaymentPlan.Visible = True
                                lblPaymentPlan.Visible = True
                            Else
                                ddlPaymentPlan.Visible = False
                                lblPaymentPlan.Visible = False
                            End If
                            If dAmountPaidByStudent > 0 Then
                                CreditCard.Visible = True
                            Else
                                CreditCard.Visible = False
                                ddlPaymentPlan.Visible = False
                                lblPaymentPlan.Visible = False
                            End If
                        Else
                            radSummerPaymentSummery.Visible = False
                            lblAmount.Visible = False
                            lblTotalAmount.Visible = False
                            lblStudentPaidLabel.Visible = False
                            lblAmountPaidStudent.Visible = False
                            lblFirmPaidLabel.Visible = False
                            lblAmountPaidFirm.Visible = False
                            lblPaymentSummery.Visible = False
                            ddlPaymentPlan.Visible = False
                            CreditCard.Visible = False
                            txtIntialAmount.Visible = False
                            lblCurrency.Visible = False
                            lblIntialAmt.Visible = False
                            lblTaxAmount.Visible = False
                            lblTax.Visible = False
                            ddlPaymentPlan.Visible = False
                            lblPaymentPlan.Visible = False
                        End If
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub LoadPaymentPlan(ByVal sProduct As String)
            Try
                Dim sSql As String = Database & "..spGetPaymentPlanForStudentEnrollment__c @ProductIds='" & sProduct & "'"
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    ddlPaymentPlan.DataSource = dt
                    ddlPaymentPlan.DataTextField = "Name"
                    ddlPaymentPlan.DataValueField = "ID"
                    ddlPaymentPlan.DataBind()
                    ' radPaymentPlanDetails.Visible = True
                Else
                    ' radPaymentPlanDetails.Visible = False
                End If
                ddlPaymentPlan.Items.Insert(0, "Select Payment Plan")
                radPaymentPlanDetails.Visible = False

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Public Function SetStagePaymentAmount(ByVal Percentage As Decimal, ByVal days As Integer) As String
            Try
                Dim dStageAmt As Decimal = CDec(Convert.ToString(lblStagePaymentTotal.Text).Substring(1, Convert.ToString(lblStagePaymentTotal.Text).Length - 1))
                Dim dStudentPayAmt As Decimal = CDec(Convert.ToString(lblAmountPaidStudent.Text).Substring(1, Convert.ToString(lblAmountPaidStudent.Text).Length - 1))
                Dim dPercentageAmt As Decimal = (dStageAmt * Percentage) / 100
                If days = 0 Then
                    Dim dintialStageAmount As Decimal = dStudentPayAmt - dStageAmt
                    txtIntialAmount.Text = Format(CDec(dintialStageAmount + dPercentageAmt), "0.00")
                Else
                    Dim dintialStageAmount As Decimal = dStudentPayAmt - dStageAmt
                    txtIntialAmount.Text = Format(CDec(dintialStageAmount), "0.00")
                End If
                Return ViewState("CurrencyTypeID") & Format(CDec(dPercentageAmt), "0.00")
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return ""
            End Try
        End Function
        Private Sub LoadEEAppData()
            Try
                Dim dtEEAppData As DataTable = CType(Session("UpdateEEAppData"), DataTable)
                If Not dtEEAppData Is Nothing AndAlso dtEEAppData.Rows.Count > 0 Then
                    ViewState("RouteChanged") = dtEEAppData.Rows(0)("RouteChanged")
                    ViewState("ExemptionAppID") = dtEEAppData.Rows(0)("ExemptionAppID")
                    ViewState("RouteOfEntryID") = dtEEAppData.Rows(0)("RouteOfEntryID")
                    ViewState("CompanyID") = dtEEAppData.Rows(0)("CompanyID")
                    ViewState("ShareMyInfoWithFirm") = dtEEAppData.Rows(0)("ShareMyInfoWithFirm")
                    ViewState("StudentGroupID") = dtEEAppData.Rows(0)("StudentGroupID")
                    ViewState("ExamInterimStudentGrp") = dtEEAppData.Rows(0)("ExamInterimStudentGrp")
                    ViewState("Eid") = dtEEAppData.Rows(0)("EID")
                    ''Added By Pardip 2016-002-16
                    ViewState("OldCompanyID") = dtEEAppData.Rows(0)("OldCompanyID")
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' This method will get call on Page load to reduce time om submit click 
        ''' </summary>
        Private Sub CreateOrderObjectsOnLoad()
            Try
                If Not Session("SummerPaymentSummery") Is Nothing Then
                    Dim dtpmtInfoTable As DataTable = TryCast(Session("SummerPaymentSummery"), DataTable)
                    If Not dtpmtInfoTable Is Nothing AndAlso dtpmtInfoTable.Rows.Count > 0 Then
                        Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
                        oOrder = TryCast(AptifyApplication.GetEntityObject("Orders", -1), OrdersEntity)
                        oOrder.ShipToID = User1.PersonID
                        oOrder.BillToID = User1.PersonID
                        oOrder.SetValue("OrderSourceID", AptifyApplication.GetEntityRecordIDFromRecordName("Order Sources", "Web")) ' Web
                        oOrder.SetValue("BillToSameAsShipTo", 1)
                        oOrder.SetValue("EmployeeID", DataAction.UserCredentials.GetUserRelatedRecordID("Employees"))
                        Dim oOrderLine As Aptify.Applications.OrderEntry.OrderLinesEntity
                        Dim dTaxAmount As Decimal = 0
                        If Convert.ToInt32(ViewState("IsFirmAdmin")) = 1 Then
                            oOrder.SetValue("FirmPay__c", 1)
                        End If
                        For Each row As DataRow In dtpmtInfoTable.Rows
                            If row("AlternateTimeTable").ToString().Trim <> "" Then
                                Dim sStudentGrp As String = Database & "..spCheckStudentGroupOnClassPart__c @ClassID=" & row("ClassID") & ",@StudentGroupID=" & ViewState("StudentGroupID")
                                Dim iIsStudentGrp As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sStudentGrp, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                If iIsStudentGrp > 0 Then
                                    row("AlternateTimeTable") = String.Empty
                                End If
                            End If

                            If dTaxAmount = 0 Then
                                dTaxAmount = CDec(row("TaxAmount"))
                            Else
                                dTaxAmount = dTaxAmount + CDec(row("TaxAmount"))
                            End If
                            ' Check Is Firm Pay 
                            If row("WhoPay").ToString().Trim.ToLower = "firm pay" Then
                                ' if firm pay then add data in new data table
                                Dim dtFirmPaySummery As DataTable = FirmPaySummery

                                Dim drFirmPaySummery As DataRow = dtFirmPaySummery.NewRow()
                                drFirmPaySummery("Subject") = row("Subject")
                                drFirmPaySummery("Type") = row("Type")
                                drFirmPaySummery("ProductID") = row("ProductID")
                                drFirmPaySummery("WhoPay") = row("WhoPay")
                                drFirmPaySummery("ClassID") = row("ClassID")
                                drFirmPaySummery("Price") = row("Price")
                                drFirmPaySummery("IsProductPaymentPlan") = 1
                                drFirmPaySummery("AcademicCycleID") = row("AcademicCycleID")
                                If row("AlternateTimeTable").ToString().Trim <> "" Then
                                    drFirmPaySummery("AlternateTimeTable") = row("AlternateTimeTable").Trim
                                End If
                                dtFirmPaySummery.Rows.Add(drFirmPaySummery)
                                FirmPaySummery = dtFirmPaySummery
                            Else
                                oOrder.AddProduct(Convert.ToInt32(row("ProductID")), 1)
                                oOrderLine = oOrder.SubTypes("OrderLines").Item(oOrder.SubTypes("OrderLines").Count - 1)
                                With oOrderLine
                                    .ExtendedOrderDetailEntity.SetValue("ClassID", row("ClassID"))
                                    .ExtendedOrderDetailEntity.SetValue("Status", "Registered")
                                    .ExtendedOrderDetailEntity.SetValue("AcademicCycleID__c", Convert.ToInt32(row("AcademicCycleID")))
                                    If (row("Type").ToString().Trim.ToLower = "summer exam" OrElse row("Type").ToString().Trim.ToLower = "autumn exam") OrElse (row("Type").ToString().Trim.ToLower = "interim assessment" OrElse row("Type").ToString().Trim.ToLower = "resit interim assessment") Then
                                        '  .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", ViewState("ExamInterimStudentGrp"))
                                        If row("AlternateTimeTable").ToString().Trim <> "" Then
                                            Dim sSqlStudentGrp As String = Database & "..spGetExamInterimStudentGrp__c @StudentGrpID=" & AptifyApplication.GetEntityRecordIDFromRecordName("StudentGroups__c", row("AlternateTimeTable").ToString().Trim)
                                            Dim iExamIterimStudentGrp As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSqlStudentGrp, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                            If iExamIterimStudentGrp > 0 Then
                                                .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", iExamIterimStudentGrp)
                                            Else
                                                .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", ViewState("ExamInterimStudentGrp"))
                                            End If
                                        Else
                                            .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", ViewState("ExamInterimStudentGrp"))
                                        End If

                                    Else
                                        If row("AlternateTimeTable").ToString().Trim <> "" Then
                                            .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", AptifyApplication.GetEntityRecordIDFromRecordName("StudentGroups__c", row("AlternateTimeTable").ToString().Trim))

                                        Else
                                            .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", ViewState("StudentGroupID"))
                                        End If
                                    End If
                                    .ExtendedOrderDetailEntity.SetValue("RouteOfEntryID__c", ViewState("RouteOfEntryID"))
                                    .SetAddValue("__ExtendedAttributeObjectData", .ExtendedOrderDetailEntity.GetObjectData(False))
                                End With
                                oOrderLine.SetProductPrice()
                            End If
                        Next
                        Session("oOrder") = oOrder
                    End If

                    If Not FirmPaySummery Is Nothing AndAlso FirmPaySummery.Rows.Count > 0 Then
                        Dim oFirmOrder As Aptify.Applications.OrderEntry.OrdersEntity
                        oFirmOrder = TryCast(AptifyApplication.GetEntityObject("Orders", -1), OrdersEntity)
                        oFirmOrder.ShipToID = User1.PersonID
                        oFirmOrder.BillToID = User1.PersonID
                        oFirmOrder.SetValue("OrderSourceID", AptifyApplication.GetEntityRecordIDFromRecordName("Order Sources", "Web")) ' Web
                        oFirmOrder.SetValue("BillToSameAsShipTo", 1)
                        oFirmOrder.SetValue("EmployeeID", DataAction.UserCredentials.GetUserRelatedRecordID("Employees"))
                        Dim oFirmOrderLine As Aptify.Applications.OrderEntry.OrderLinesEntity
                        'Dim sSQlFirmAdmin As String = Database & "..spGetUserFirmAdmin__c @PersonID=" & User1.PersonID
                        'Dim iFirmAdmin As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSQlFirmAdmin, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        If Convert.ToInt32(ViewState("IsFirmAdmin")) = 1 Then
                            oFirmOrder.SetValue("FirmPay__c", 1)
                        End If
                        For Each dr As DataRow In FirmPaySummery.Rows
                            oFirmOrder.AddProduct(Convert.ToInt32(dr("ProductID")))
                            oFirmOrderLine = oFirmOrder.SubTypes("OrderLines").Item(oFirmOrder.SubTypes("OrderLines").Count - 1)
                            With oFirmOrderLine
                                .ExtendedOrderDetailEntity.SetValue("ClassID", dr("ClassID"))
                                .ExtendedOrderDetailEntity.SetValue("Status", "Registered")
                                .ExtendedOrderDetailEntity.SetValue("AcademicCycleID__c", Convert.ToInt32(dr("AcademicCycleID")))
                                If (Convert.ToString(dr("Type")).Trim.ToLower = "summer exam" OrElse Convert.ToString(dr("Type")).Trim.ToLower = "autumn exam") OrElse (Convert.ToString(dr("Type")).Trim.ToLower = "interim assessment" OrElse Convert.ToString(dr("Type")).Trim.ToLower = "resit interim assessment") Then
                                    ' .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", ViewState("ExamInterimStudentGrp"))
                                    If Convert.ToString(dr("AlternateTimeTable")) <> "" Then
                                        Dim sSqlStudentGrp As String = Database & "..spGetExamInterimStudentGrp__c @StudentGrpID=" & AptifyApplication.GetEntityRecordIDFromRecordName("StudentGroups__c", Convert.ToString(dr("AlternateTimeTable")))
                                        Dim iExamIterimStudentGrp As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSqlStudentGrp, IAptifyDataAction.DSLCacheSetting.BypassCache))
                                        If iExamIterimStudentGrp > 0 Then
                                            .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", iExamIterimStudentGrp)
                                        Else
                                            .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", ViewState("ExamInterimStudentGrp"))
                                        End If
                                    Else
                                        .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", ViewState("ExamInterimStudentGrp"))
                                    End If

                                Else
                                    '  .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", ddlCAP1StudGrp.SelectedValue)
                                    If Convert.ToString(dr("AlternateTimeTable")) <> "" Then
                                        .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", AptifyApplication.GetEntityRecordIDFromRecordName("StudentGroups__c", Convert.ToString(dr("AlternateTimeTable"))))

                                    Else
                                        .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", ViewState("StudentGroupID"))
                                    End If
                                End If
                                .ExtendedOrderDetailEntity.SetValue("RouteOfEntryID__c", ViewState("RouteOfEntryID"))
                                .SetAddValue("__ExtendedAttributeObjectData", .ExtendedOrderDetailEntity.GetObjectData(False))
                                '.SetValue("BillToCompanyID__c", AptifyApplication.GetEntityRecordIDFromRecordName("Companies", ViewState("CompanyID")))
                                'Added BY Pradip 2016-01-10 For Group 3 Tracker G3-48
                                '.SetValue("BillToCompanyID__c", AptifyApplication.GetEntityRecordIDFromRecordName("Companies", ViewState("CompanyID")))
                                If CheckApprovedEE() Then
                                    ' .SetValue("BillToCompanyID__c", AptifyApplication.GetEntityRecordIDFromRecordName("Companies", ViewState("CompanyID")))
                                    .SetValue("BillToCompanyID__c", Convert.ToString(ViewState("CompanyID")))
                                Else
                                    If Convert.ToInt32(ViewState("IsFirmAdmin")) = 1 Then
                                    Else
                                        .SetValue("BillToCompanyID__c", -1)
                                    End If
                                End If
                            End With
                            oFirmOrderLine.SetProductPrice()
                        Next
                        oFirmOrder.SetValue("OrderTypeID", Aptify.Applications.OrderEntry.OrderType.Regular)   ' Regular Order 
                        oFirmOrder.OrderStatus = OrderStatus.Taken
                        oFirmOrder.PayTypeID = AptifyApplication.GetEntityRecordIDFromRecordName("Payment Types", "Purchase Order")
                        oFirmOrder.PONumber = "Firm Pay Order"
                        'oFirmOrder.SetAddValue("_xBypassCreditCheck", 1) 'bypass credit check
                        'redmine 16921
                        'If ViewState("Eid") IsNot Nothing AndAlso Convert.ToInt32(ViewState("Eid")) > 0 Then
                        '    oFirmOrder.SetValue("OnBehalfEmployeeID__c", Convert.ToInt32(ViewState("Eid")))
                        'End If

                        If Session("EID") IsNot Nothing AndAlso Convert.ToInt32(Session("EID")) > 0 Then
                            oFirmOrder.SetValue("OnBehalfEmployeeID__c", Convert.ToInt32(Session("EID")))
                        End If

                        'If oFirmOrder.Save(False, sError) Then
                        '    lFirmOrderID = oFirmOrder.RecordID
                        '    'oFirmOrder.ShipEntireOrder()
                        'End If
                        Session("oFirmOrder") = oFirmOrder
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region
        Public Property FirmPaySummery() As DataTable
            Get

                If Not ViewState("FirmPaySummery") Is Nothing Then
                    Return CType(ViewState("FirmPaySummery"), DataTable)
                Else
                    Dim dtFirmPaySummery As New DataTable
                    dtFirmPaySummery.Columns.Add("Subject")
                    dtFirmPaySummery.Columns.Add("ClassID")
                    dtFirmPaySummery.Columns.Add("Type")
                    dtFirmPaySummery.Columns.Add("ProductID")
                    dtFirmPaySummery.Columns.Add("WhoPay")
                    dtFirmPaySummery.Columns.Add("Price")
                    dtFirmPaySummery.Columns.Add("IsProductPaymentPlan")
                    dtFirmPaySummery.Columns.Add("AcademicCycleID")
                    dtFirmPaySummery.Columns.Add("AlternateTimeTable")
                    Return dtFirmPaySummery
                End If
            End Get
            Set(ByVal value As DataTable)
                ViewState("FirmPaySummery") = value
            End Set
        End Property

        Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
            Try
                If Convert.ToDecimal(txtIntialAmount.Text.Trim) > 0 Then
                    If CreditCard.PaymentTypeID <= 0 Then
                        Exit Sub
                    End If
                End If
                If Session("PlaceOrder") = True Then
                    lblSubmitMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.SuccessMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    'Modified by sheela for Enrolment Submission Message Change(CNM-7)
                    'radwindowSubmit.VisibleOnPageLoad = True

                    btnSubmitOk.Visible = False
                    btnNo.Visible = False
                    btnSuccess.Visible = True
                    'Added by sheela for Enrolment Submission Message Change(CNM-7)
                    Response.Redirect(StudentEnrollmentConfirmation, False)
                Else
                    ' check person credit limit added by Govind for redmine issue #13672
                    Dim sCreditSql As String = "..spGetCreditLimitAndStatus__c @PersonID=" & User1.PersonID
                    Dim isCreditLimitApproved As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sCreditSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    If isCreditLimitApproved = False Then
                        lblSubmitMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment..CreditLimit")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        radwindowSubmit.VisibleOnPageLoad = True
                        btnSubmitOk.Visible = False
                        btnNo.Visible = False
                        btnSuccess.Visible = True
                    Else
                        CreateOrderAndSaveData()
                    End If

                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub CreateOrderAndSaveData()
            Try
                Dim lOrderID As Long
                'Dim sTransID As String = DataAction.BeginTransaction(IsolationLevel.Serializable, True)
                'If CreateOrder(lOrderID) > 0 Then
                CreateOrder(lOrderID)
                ' create a firm order 
                Dim lFirmOrderID As Long = -1
                FirmPayOrder(lFirmOrderID)
                If lOrderID <= 0 AndAlso lFirmOrderID > 0 Then
                    lOrderID = lFirmOrderID
                End If
                If lOrderID > 0 Then
                    If Not Session("UpdateEEAppData") Is Nothing Then
                        ''Commented BY Pradip 2016-02-16 for Issue No 5437
                        'If (ViewState("RouteChanged") IsNot Nothing AndAlso Convert.ToBoolean(ViewState("RouteChanged"))) _
                        '  Or (ViewState("CompanyID") <> "" AndAlso CLng(ViewState("CompanyID")) > 0) Then
                        If (ViewState("RouteChanged") IsNot Nothing AndAlso Convert.ToBoolean(ViewState("RouteChanged"))) Or (ViewState("CompanyID") <> ViewState("OldCompanyID")) Then
                            ' Created by Govind M for Performance
                            Dim ssqlEEUpdate As String = Database & "..spUpdateEEApp__c @ExempAppID=" & Convert.ToInt32(ViewState("ExemptionAppID")) & ",@RouteOfEntryID=" & ViewState("RouteOfEntryID") & ",@CompanyID=" & ViewState("CompanyID") & ",@ShareMyInfoWithFirm=" & ViewState("ShareMyInfoWithFirm") & ",@StatusID=" & AptifyApplication.GetEntityRecordIDFromRecordName("ExemptionApplicationStatus__c", "Change Request")
                            Dim updateEEID As Integer = DataAction.ExecuteNonQuery(ssqlEEUpdate)
                            '' ''Dim oGEExmpApp As AptifyGenericEntityBase
                            '' ''oGEExmpApp = AptifyApplication.GetEntityObject("ExemptionApplication__c", Convert.ToInt32(ViewState("ExemptionAppID")))
                            '' ''With oGEExmpApp
                            '' ''    .SetValue("RouteOfEntry", ViewState("RouteOfEntryID"))
                            '' ''    .SetValue("CompanyID", ViewState("CompanyID"))
                            '' ''    .SetValue("ShareMyInfoWithFirm", ViewState("ShareMyInfoWithFirm"))
                            '' ''    .SetValue("StatusID", AptifyApplication.GetEntityRecordIDFromRecordName("ExemptionApplicationStatus__c", "Change Request"))
                            '' ''    Dim sError As String = String.Empty
                            '' ''    If .Save(False, sError) Then

                            '' ''    End If
                            '' ''End With



                        End If

                    End If



                    CreateCurriculumApplication()
                    Session("SummerPaymentSummery") = Nothing
                    'Modified by sheela for Enrolment Submission Message Change(CNM-7)
                    lblSubmitMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.SuccessMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    'radwindowSubmit.VisibleOnPageLoad = True
                    btnSubmitOk.Visible = False
                    btnNo.Visible = False
                    btnSuccess.Visible = True
                    Session("UpdateEEAppData") = Nothing
                    Session("SummerPaymentSummery") = Nothing

                    'updatePnlPopup.Update()
                    ViewState("AlreadySubmit") = False
                    Session("PlaceOrder") = True

                    Session("OrderID") = lOrderID
                    'Added by sheela for Enrolment Submission Message Change(CNM-7)
                    Response.Redirect(StudentEnrollmentConfirmation, False)

                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ' Create as Firm Pay Order
        Private Sub FirmPayOrder(ByRef lFirmOrderID As Long)
            Try
                Dim oFirmOrder As Aptify.Applications.OrderEntry.OrdersEntity
                If Not Session("oFirmOrder") Is Nothing Then
                    oFirmOrder = CType(Session("oFirmOrder"), Aptify.Applications.OrderEntry.OrdersEntity)

                    Dim sError As String = Nothing
                    ''Çommented by Paresh as moved on form load
                    'If Not FirmPaySummery Is Nothing AndAlso FirmPaySummery.Rows.Count > 0 Then
                    '    Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
                    '    Dim sError As String = Nothing
                    '    oOrder = TryCast(AptifyApplication.GetEntityObject("Orders", -1), OrdersEntity)
                    '    oOrder.ShipToID = User1.PersonID
                    '    oOrder.BillToID = User1.PersonID
                    '    oOrder.SetValue("OrderSourceID", AptifyApplication.GetEntityRecordIDFromRecordName("Order Sources", "Web")) ' Web
                    '    oOrder.SetValue("BillToSameAsShipTo", 1)
                    '    oOrder.SetValue("EmployeeID", DataAction.UserCredentials.GetUserRelatedRecordID("Employees"))
                    '    Dim oOrderLine As Aptify.Applications.OrderEntry.OrderLinesEntity
                    '    'Dim sSQlFirmAdmin As String = Database & "..spGetUserFirmAdmin__c @PersonID=" & User1.PersonID
                    '    'Dim iFirmAdmin As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSQlFirmAdmin, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    '    If Convert.ToInt32(ViewState("IsFirmAdmin")) = 1 Then
                    '        oOrder.SetValue("FirmPay__c", 1)
                    '    End If
                    '    For Each dr As DataRow In FirmPaySummery.Rows
                    '        oOrder.AddProduct(Convert.ToInt32(dr("ProductID")))
                    '        oOrderLine = oOrder.SubTypes("OrderLines").Item(oOrder.SubTypes("OrderLines").Count - 1)
                    '        With oOrderLine
                    '            .ExtendedOrderDetailEntity.SetValue("ClassID", dr("ClassID"))
                    '            .ExtendedOrderDetailEntity.SetValue("Status", "Registered")
                    '            .ExtendedOrderDetailEntity.SetValue("AcademicCycleID__c", Convert.ToInt32(dr("AcademicCycleID")))
                    '            If (Convert.ToString(dr("Type")).Trim.ToLower = "summer exam" OrElse Convert.ToString(dr("Type")).Trim.ToLower = "autumn exam") OrElse (Convert.ToString(dr("Type")).Trim.ToLower = "interim assessment" OrElse Convert.ToString(dr("Type")).Trim.ToLower = "resit interim assessment") Then
                    '                ' .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", ViewState("ExamInterimStudentGrp"))
                    '                If Convert.ToString(dr("AlternateTimeTable")) <> "" Then
                    '                    Dim sSqlStudentGrp As String = Database & "..spGetExamInterimStudentGrp__c @StudentGrpID=" & AptifyApplication.GetEntityRecordIDFromRecordName("StudentGroups__c", Convert.ToString(dr("AlternateTimeTable")))
                    '                    Dim iExamIterimStudentGrp As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSqlStudentGrp, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    '                    If iExamIterimStudentGrp > 0 Then
                    '                        .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", iExamIterimStudentGrp)
                    '                    Else
                    '                        .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", ViewState("ExamInterimStudentGrp"))
                    '                    End If
                    '                Else
                    '                    .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", ViewState("ExamInterimStudentGrp"))
                    '                End If

                    '            Else
                    '                '  .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", ddlCAP1StudGrp.SelectedValue)
                    '                If Convert.ToString(dr("AlternateTimeTable")) <> "" Then
                    '                    .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", AptifyApplication.GetEntityRecordIDFromRecordName("StudentGroups__c", Convert.ToString(dr("AlternateTimeTable"))))

                    '                Else
                    '                    .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", ViewState("StudentGroupID"))
                    '                End If
                    '            End If
                    '            .ExtendedOrderDetailEntity.SetValue("RouteOfEntryID__c", ViewState("RouteOfEntryID"))
                    '            .SetAddValue("__ExtendedAttributeObjectData", .ExtendedOrderDetailEntity.GetObjectData(False))
                    '            '.SetValue("BillToCompanyID__c", AptifyApplication.GetEntityRecordIDFromRecordName("Companies", ViewState("CompanyID")))
                    '            'Added BY Pradip 2016-01-10 For Group 3 Tracker G3-48
                    '            '.SetValue("BillToCompanyID__c", AptifyApplication.GetEntityRecordIDFromRecordName("Companies", ViewState("CompanyID")))
                    '            If CheckApprovedEE() Then
                    '                ' .SetValue("BillToCompanyID__c", AptifyApplication.GetEntityRecordIDFromRecordName("Companies", ViewState("CompanyID")))
                    '                .SetValue("BillToCompanyID__c", Convert.ToString(ViewState("CompanyID")))
                    '            Else
                    '                If Convert.ToInt32(ViewState("IsFirmAdmin")) = 1 Then
                    '                Else
                    '                    .SetValue("BillToCompanyID__c", -1)
                    '                End If
                    '            End If
                    '        End With
                    '        oOrderLine.SetProductPrice()
                    '    Next
                    '    oOrder.SetValue("OrderTypeID", Aptify.Applications.OrderEntry.OrderType.Regular)   ' Regular Order 
                    '    oOrder.OrderStatus = OrderStatus.Taken
                    '    oOrder.PayTypeID = AptifyApplication.GetEntityRecordIDFromRecordName("Payment Types", "Purchase Order")
                    '    oOrder.PONumber = "Firm Pay Order"
                    '    'oOrder.SetAddValue("_xBypassCreditCheck", 1) 'bypass credit check
                    '    'redmine 16921
                    '    'If ViewState("Eid") IsNot Nothing AndAlso Convert.ToInt32(ViewState("Eid")) > 0 Then
                    '    '    oOrder.SetValue("OnBehalfEmployeeID__c", Convert.ToInt32(ViewState("Eid")))
                    '    'End If

                    '    If Session("EID") IsNot Nothing AndAlso Convert.ToInt32(Session("EID")) > 0 Then
                    '        oOrder.SetValue("OnBehalfEmployeeID__c", Convert.ToInt32(Session("EID")))
                    '    End If

                    '    If oOrder.Save(False, sError) Then
                    '        lFirmOrderID = oOrder.RecordID
                    '        'oOrder.ShipEntireOrder()
                    '    End If
                    'End If

                    If oFirmOrder.Save(False, sError) Then
                        lFirmOrderID = oFirmOrder.RecordID
                        'oOrder.ShipEntireOrder()
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            Finally
                Session.Remove("oFirmOrder")
            End Try
        End Sub
        Private Sub CreateCurriculumApplication()
            Try
                If Not Session("SummerPaymentSummery") Is Nothing Then
                    Dim dt As DataTable = CType(Session("SummerPaymentSummery"), DataTable)
                    Dim drCurriculumWiseUnits() As DataRow = dt.Select("CurriculumID > '0'")
                    Dim view As New DataView(dt)
                    Dim distinctValues As DataTable = view.ToTable(True, "CurriculumID", "AcademicCycleID")
                    If Not distinctValues Is Nothing AndAlso distinctValues.Rows.Count > 0 Then
                        For Each dr As DataRow In distinctValues.Rows
                            ' Create Curriculum Application
                            ' First Check For this already curriculum Application or not for that person
                            Dim sSql As String = Database & "..spCheckCurriculumAppExists__c @StudentID=" & User1.PersonID & ",@CurriculumID=" & Convert.ToInt32(dr("CurriculumID"))
                            Dim iCurriculumAppID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                            If iCurriculumAppID <= 0 Then
                                ' CREATE APP
                                '' Dim sSQLInsert As String = Database & "..spSaveInsertCurriculumnApplications__c @PersonID=" & User1.PersonID & ",@CurriculumID=" & Convert.ToInt32(dr("CurriculumID")) & ",@SubGroupID=" & ViewState("StudentGroupID") & ",@RounteOfEntryID=" & ViewState("RouteOfEntryID") & ",@AcademicCycleID=" & dr("AcademicCycleID")
                                '' Dim insertApp As Integer = DataAction.ExecuteNonQuery(sSQLInsert)
                                ' CREATE APP
                                Dim oCurriculumGE As AptifyGenericEntityBase = AptifyApplication.GetEntityObject("Curriculum Applications", -1)
                                With oCurriculumGE
                                    .SetValue("ApplicantID", User1.PersonID)
                                    .SetValue("CurriculumCategoryID", AptifyApplication.GetEntityRecordIDFromRecordName("Curriculum Definition Categories", "CA-Chartered Accountant"))
                                    .SetValue("CurriculumID", Convert.ToInt32(dr("CurriculumID")))
                                    .SetValue("ApplicationDate", Today.Date)
                                    .SetValue("Status", "Approved")
                                    .SetValue("StartDate__c", Today.Date)
                                    .SetValue("SubGroupID__c", ViewState("StudentGroupID"))
                                    .SetValue("RounteOfEntryID__c", ViewState("RouteOfEntryID"))
                                    .SetValue("AcademicCycleID__c", dr("AcademicCycleID"))
                                    Dim sError As String = String.Empty
                                    If .Save(False, sError) Then

                                    Else

                                    End If
                                End With
                            Else
                                Dim ssqlUpdate As String = Database & "..spUpdateCurriculumnApplications__c @CurriculumAppID=" & iCurriculumAppID & ",@SubGroupID=" & ViewState("StudentGroupID")
                                Dim updateCurrID As Integer = DataAction.ExecuteNonQuery(ssqlUpdate)
                                ''Dim oCurriculumGE As AptifyGenericEntityBase = AptifyApplication.GetEntityObject("Curriculum Applications", iCurriculumAppID)
                                ''With oCurriculumGE
                                ''    .SetValue("SubGroupID__c", ViewState("StudentGroupID"))
                                ''    Dim sError As String = String.Empty
                                ''    If .Save(False, sError) Then

                                ''    Else

                                ''    End If
                                ''End With

                            End If

                        Next
                    End If

                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Create a order 
        ''' </summary>
        ''' <param name="lOrderID"></param>
        ''' <param name="sTransID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' 
        Private Function CreateOrder(ByRef lOrderID As Long) As Long
            Try
                Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
                If Not Session("oOrder") Is Nothing Then
                    oOrder = CType(Session("oOrder"), Aptify.Applications.OrderEntry.OrdersEntity)
                    Dim sError As String = Nothing
                    ''Paresh Commented as moved on Page Load
                    'Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
                    'Dim sError As String = Nothing
                    'oOrder = TryCast(AptifyApplication.GetEntityObject("Orders", -1), OrdersEntity)
                    'oOrder.ShipToID = User1.PersonID
                    'oOrder.BillToID = User1.PersonID
                    'oOrder.SetValue("OrderSourceID", AptifyApplication.GetEntityRecordIDFromRecordName("Order Sources", "Web")) ' Web
                    'oOrder.SetValue("BillToSameAsShipTo", 1)
                    'oOrder.SetValue("EmployeeID", DataAction.UserCredentials.GetUserRelatedRecordID("Employees"))
                    'Dim oOrderLine As Aptify.Applications.OrderEntry.OrderLinesEntity
                    'Dim dTaxAmount As Decimal = 0
                    ''Dim sSQlFirmAdmin As String = Database & "..spGetUserFirmAdmin__c @PersonID=" & User1.PersonID
                    ''Dim iFirmAdmin As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSQlFirmAdmin, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    'If Convert.ToInt32(ViewState("IsFirmAdmin")) = 1 Then
                    '    oOrder.SetValue("FirmPay__c", 1)
                    'End If
                    'For Each row As Telerik.Web.UI.GridItem In radSummerPaymentSummery.Items

                    '    Dim lblSubject As Label = DirectCast(row.FindControl("lblSubject"), Label)
                    '    Dim lblPaymentProductID As Label = DirectCast(row.FindControl("lblPaymentProductID"), Label)
                    '    Dim lblClassID As Label = DirectCast(row.FindControl("lblClassID"), Label)
                    '    Dim lblType As Label = DirectCast(row.FindControl("lblType"), Label)
                    '    Dim lblPaymentPrice As Label = DirectCast(row.FindControl("lblPaymentPrice"), Label)
                    '    Dim lblAcademicCycleID As Label = DirectCast(row.FindControl("lblAcademicCycleID"), Label)
                    '    Dim lblTaxAmount As Label = DirectCast(row.FindControl("lblTaxAmount"), Label)
                    '    Dim lblAlternateTimeTableOnOrder As Label = DirectCast(row.FindControl("lblAlternateTimeTableOnOrder"), Label)


                    '    If lblAlternateTimeTableOnOrder.Text.Trim <> "" Then
                    '        Dim sStudentGrp As String = Database & "..spCheckStudentGroupOnClassPart__c @ClassID=" & lblClassID.Text & ",@StudentGroupID=" & ViewState("StudentGroupID")
                    '        Dim iIsStudentGrp As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sStudentGrp, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    '        If iIsStudentGrp > 0 Then
                    '            lblAlternateTimeTableOnOrder.Text = String.Empty
                    '        End If
                    '    End If
                    '    If dTaxAmount = 0 Then
                    '        dTaxAmount = CDec(lblTaxAmount.Text)
                    '    Else
                    '        dTaxAmount = dTaxAmount + CDec(lblTaxAmount.Text)
                    '    End If
                    '    ' Check Is Firm Pay 
                    '    Dim lblWhoPay As Label = DirectCast(row.FindControl("lblWhoPay"), Label)
                    '    If lblWhoPay.Text.Trim.ToLower = "firm pay" Then
                    '        ' if firm pay then add data in new data table
                    '        Dim dtFirmPaySummery As DataTable = FirmPaySummery

                    '        Dim drFirmPaySummery As DataRow = dtFirmPaySummery.NewRow()
                    '        drFirmPaySummery("Subject") = lblSubject.Text
                    '        drFirmPaySummery("Type") = lblType.Text
                    '        drFirmPaySummery("ProductID") = lblPaymentProductID.Text
                    '        drFirmPaySummery("WhoPay") = lblWhoPay.Text
                    '        drFirmPaySummery("ClassID") = lblClassID.Text
                    '        drFirmPaySummery("Price") = lblPaymentPrice.Text
                    '        drFirmPaySummery("IsProductPaymentPlan") = 1
                    '        drFirmPaySummery("AcademicCycleID") = lblAcademicCycleID.Text
                    '        If lblAlternateTimeTableOnOrder.Text.Trim <> "" Then
                    '            drFirmPaySummery("AlternateTimeTable") = lblAlternateTimeTableOnOrder.Text.Trim
                    '        End If
                    '        dtFirmPaySummery.Rows.Add(drFirmPaySummery)
                    '        FirmPaySummery = dtFirmPaySummery
                    '    Else
                    '        oOrder.AddProduct(Convert.ToInt32(lblPaymentProductID.Text), 1)
                    '        oOrderLine = oOrder.SubTypes("OrderLines").Item(oOrder.SubTypes("OrderLines").Count - 1)
                    '        With oOrderLine
                    '            .ExtendedOrderDetailEntity.SetValue("ClassID", lblClassID.Text)
                    '            .ExtendedOrderDetailEntity.SetValue("Status", "Registered")
                    '            .ExtendedOrderDetailEntity.SetValue("AcademicCycleID__c", Convert.ToInt32(lblAcademicCycleID.Text))
                    '            If (lblType.Text.Trim.ToLower = "summer exam" OrElse lblType.Text.Trim.ToLower = "autumn exam") OrElse (lblType.Text.Trim.ToLower = "interim assessment" OrElse lblType.Text.Trim.ToLower = "resit interim assessment") Then
                    '                '  .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", ViewState("ExamInterimStudentGrp"))
                    '                If lblAlternateTimeTableOnOrder.Text.Trim <> "" Then
                    '                    Dim sSqlStudentGrp As String = Database & "..spGetExamInterimStudentGrp__c @StudentGrpID=" & AptifyApplication.GetEntityRecordIDFromRecordName("StudentGroups__c", lblAlternateTimeTableOnOrder.Text.Trim)
                    '                    Dim iExamIterimStudentGrp As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSqlStudentGrp, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    '                    If iExamIterimStudentGrp > 0 Then
                    '                        .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", iExamIterimStudentGrp)
                    '                    Else
                    '                        .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", ViewState("ExamInterimStudentGrp"))
                    '                    End If
                    '                Else
                    '                    .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", ViewState("ExamInterimStudentGrp"))
                    '                End If

                    '            Else
                    '                If lblAlternateTimeTableOnOrder.Text.Trim <> "" Then
                    '                    .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", AptifyApplication.GetEntityRecordIDFromRecordName("StudentGroups__c", lblAlternateTimeTableOnOrder.Text.Trim))

                    '                Else
                    '                    .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", ViewState("StudentGroupID"))
                    '                End If
                    '            End If
                    '            .ExtendedOrderDetailEntity.SetValue("RouteOfEntryID__c", ViewState("RouteOfEntryID"))
                    '            .SetAddValue("__ExtendedAttributeObjectData", .ExtendedOrderDetailEntity.GetObjectData(False))
                    '        End With
                    '        oOrderLine.SetProductPrice()
                    '    End If
                    'Next
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    ' check stage Payment
                    If oOrder.SubTypes("OrderLines").Count > 0 Then
                        If ddlPaymentPlan.SelectedValue <> "Select Payment Plan" AndAlso Not ViewState("SelectedPaymentPlan") Is Nothing Then
                            ''Dim oOrderPayInfo As PaymentInformation
                            Dim sErrorString As String = ""
                            Dim RefTranse As String = ""

                            Dim sLastError As String = ""
                            Dim oPersonGE As Aptify.Applications.Persons.PersonsEntity = DirectCast(Me.AptifyApplication.GetEntityObject("Persons", User1.PersonID), Aptify.Applications.Persons.PersonsEntity)

                            Dim cmbSavedPaymentMethod As DropDownList = DirectCast(CreditCard.FindControl("cmbSavedPaymentMethod"), DropDownList)
                            If cmbSavedPaymentMethod.SelectedValue <> "" Then
                                With oPersonGE.SubTypes("PersonSavedPaymentMethods").Find("ID", cmbSavedPaymentMethod.SelectedValue)
                                    oOrder.SetValue("SavedPaymentMethodID__c", cmbSavedPaymentMethod.SelectedValue)
                                    oOrder.SetValue("PaymentPlans__c", ddlPaymentPlan.SelectedValue)
                                    oOrder.SetValue("InitialPaymentAmount", txtIntialAmount.Text)
                                End With
                            Else
                                ' Create new Save payment Method
                                'Commented by Govind: For decliened staged payments issue
                                ''With oPersonGE.SubTypes("PersonSavedPaymentMethods").Add()
                                ''    .SetValue("PersonID", User1.PersonID)
                                ''    .SetValue("StartDate", Today.Date)
                                ''    .SetValue("IsActive", 1)
                                ''    .SetValue("Name", "SPM " & Today.Date)
                                ''    .SetValue("PaymentTypeID", CreditCard.PaymentTypeID)
                                ''    .SetValue("CCAccountNumber", CreditCard.CCNumber)
                                ''    .SetValue("CCExpireDate", CreditCard.CCExpireDate)
                                ''    .SetValue("EndDate", CreditCard.CCExpireDate)
                                ''    If .Fields("PaymentInformationID").EmbeddedObjectExists Then
                                ''        oOrderPayInfo = DirectCast(.Fields("PaymentInformationID").EmbeddedObject, PaymentInformation)
                                ''        If oOrderPayInfo IsNot Nothing Then
                                ''            oOrderPayInfo.CreditCardSecurityNumber = CreditCard.CCNumber
                                ''            .SetValue("CCPartial", oOrderPayInfo.GetCCPartial(CStr(CreditCard.CCNumber)))
                                ''        End If
                                ''    End If

                                ''End With
                            End If
                            'Commented by Govind: For decliened staged payments issue
                            ''Dim sPersonSaveError As String = String.Empty
                            ''If oPersonGE.Save(False, sPersonSaveError) Then
                            ''    oOrder.SetValue("SavedPaymentMethodID__c", oPersonGE.SubTypes("PersonSavedPaymentMethods").Item(oPersonGE.SubTypes("PersonSavedPaymentMethods").Count - 1).RecordID)
                            ''    oOrder.SetValue("PaymentPlans__c", ddlPaymentPlan.SelectedValue)
                            ''    oOrder.SetValue("InitialPaymentAmount", (txtIntialAmount.Text))
                            ''    oOrder.SetValue("OrderTypeID", Aptify.Applications.OrderEntry.OrderType.Regular)   ' Regular Order 
                            ''    oOrder.OrderStatus = OrderStatus.Taken
                            ''    oOrder.PayTypeID = AptifyApplication.GetEntityRecordIDFromRecordName("Payment Types", "Purchase Order")
                            ''    oOrder.PONumber = "Stage Payment"
                            ''End If

                            'Added by Govind: For decliened staged payments issue
                            'Added for Redmine #20327
                            oOrder.SetValue("BillToSameAsShipTo", 0)
                            oOrder.SetValue("BillToCompanyID", -1)
                            'End Redmine #20327
                            oOrder.SetValue("PaymentPlans__c", ddlPaymentPlan.SelectedValue)
                            oOrder.SetValue("InitialPaymentAmount", (txtIntialAmount.Text))
                            oOrder.SetValue("PayTypeID", CreditCard.PaymentTypeID)
                            oOrder.SetValue("CCAccountNumber", CreditCard.CCNumber)
                            oOrder.SetValue("CCExpireDate", CreditCard.CCExpireDate)
                            If CreditCard.CCNumber = "-Ref Transaction-" Then
                                oOrder.SetValue("ReferenceTransactionNumber", CreditCard.ReferenceTransactionNumber)
                                oOrder.SetValue("ReferenceExpiration", CreditCard.ReferenceExpiration)
                            End If
                            If Len(CreditCard.CCSecurityNumber) > 0 Then
                                oOrder.SetAddValue("_xCCSecurityNumber", CreditCard.CCSecurityNumber) 'Neha changes for Issue 16675, 06/05/2013,Added CCSecurityNumber as a temperory field for not storing in record history.
                            End If
                            If oOrder.Fields("PaymentInformationID").EmbeddedObjectExists Then
                                Dim oOrderPayInfo As PaymentInformation = DirectCast(oOrder.Fields("PaymentInformationID").EmbeddedObject, PaymentInformation)
                                oOrderPayInfo.CreditCardSecurityNumber = CreditCard.CCSecurityNumber
                                ''RashmiP, Issue 10254, SPM
                                oOrderPayInfo.SetValue("SaveForFutureUse", 1)
                                'Ansar Shaikh - Issue 11986 - 12/27/2011
                                'Ani B for issue 10254 on 22/04/2013
                                'Set CC Partial for credit cart type is reference transaction 
                                If CreditCard.CCNumber = "-Ref Transaction-" Then
                                    oOrderPayInfo.SetValue("CCPartial", CreditCard.CCPartial)
                                End If
                            End If
                        Else
                            oOrder.SetValue("PayTypeID", CreditCard.PaymentTypeID)
                            oOrder.SetValue("CCAccountNumber", CreditCard.CCNumber)
                            oOrder.SetValue("CCExpireDate", CreditCard.CCExpireDate)
                            If CreditCard.CCNumber = "-Ref Transaction-" Then
                                oOrder.SetValue("ReferenceTransactionNumber", CreditCard.ReferenceTransactionNumber)
                                oOrder.SetValue("ReferenceExpiration", CreditCard.ReferenceExpiration)
                            End If
                            If Len(CreditCard.CCSecurityNumber) > 0 Then
                                oOrder.SetAddValue("_xCCSecurityNumber", CreditCard.CCSecurityNumber) 'Neha changes for Issue 16675, 06/05/2013,Added CCSecurityNumber as a temperory field for not storing in record history.
                            End If
                            If oOrder.Fields("PaymentInformationID").EmbeddedObjectExists Then
                                Dim oOrderPayInfo As PaymentInformation = DirectCast(oOrder.Fields("PaymentInformationID").EmbeddedObject, PaymentInformation)
                                oOrderPayInfo.CreditCardSecurityNumber = CreditCard.CCSecurityNumber
                                ''RashmiP, Issue 10254, SPM
                                oOrderPayInfo.SetValue("SaveForFutureUse", CreditCard.SaveCardforFutureUse)
                                'Ansar Shaikh - Issue 11986 - 12/27/2011
                                'Ani B for issue 10254 on 22/04/2013
                                'Set CC Partial for credit cart type is reference transaction 
                                If CreditCard.CCNumber = "-Ref Transaction-" Then
                                    oOrderPayInfo.SetValue("CCPartial", CreditCard.CCPartial)
                                End If
                            End If
                        End If
                        ' oOrder.SetAddValue("_xBypassCreditCheck", 1) 'bypass credit check

                        'redmine 16921
                        'If ViewState("Eid") IsNot Nothing AndAlso Convert.ToInt32(ViewState("Eid")) > 0 Then
                        '    oOrder.SetValue("OnBehalfEmployeeID__c", Convert.ToInt32(ViewState("Eid")))
                        'End If
                        If Session("EID") IsNot Nothing AndAlso Convert.ToInt32(Session("EID")) > 0 Then
                            oOrder.SetValue("OnBehalfEmployeeID__c", Convert.ToInt32(Session("EID")))
                        End If

                        If Convert.ToInt32(ViewState("IsFirmAdmin")) = 1 Then
                        Else
                            oOrder.SetAddValue("_xBypassCreditCheck", 1) 'bypass credit check
                        End If
                        If Not oOrder.Save(False, sError) Then
                            lblErrorMsg.Visible = True
                            lblErrorMsg.Text = "<ui><li>" + sError + "</li></ui>"
                            If lblErrorMsg.Text.Trim.ToLower.Contains("there must be at least one order line per") Then
                                lblErrorMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalPage.NoSelection")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            ElseIf lblErrorMsg.Text.Trim.ToLower.Contains("this company has a credit limit") Then
                                lblErrorMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.CreditLimitModify")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            End If

                            lblErrorMsg.Text = sError + "Credit Card Verification Failed"
                            lblErrorMsg.ForeColor = Drawing.Color.Red
                            lblErrorMsg.Style.Add("font-size", "medium")
                            lblErrorMsg.Visible = True
                            lOrderID = -1
                        Else
                            lOrderID = oOrder.RecordID


                            ''If ddlPaymentPlan.SelectedValue <> "Select Payment Plan" AndAlso Not ViewState("SelectedPaymentPlan") Is Nothing Then
                            ''Else
                            ''    If Not PostPayment(lOrderID, dTaxAmount, oOrder) Then
                            ''        lblPaymentFailed.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.PaymentFailed")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            ''        radwindowPaymentFail.VisibleOnPageLoad = True
                            ''        Return -1
                            ''    End If
                            ''End If
                            ''If oOrder.RecordID > 0 Then

                            ''    'oOrder.ShipEntireOrder()

                            ''Else
                            ''    lOrderID = -1
                            ''End If
                        End If
                    Else
                        ' for firm pay retun true
                        lOrderID = -1
                    End If
                Else
                    lOrderID = -1
                End If
                Return lOrderID
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return -1
            Finally
                Session.Remove("oOrder")
            End Try
        End Function

        Private Function PostPayment(ByVal OrderID As Long, ByVal TaxAmount As Decimal, ByRef OrderGE As OrdersEntity) As Boolean
            Try
                Dim oPayment As AptifyGenericEntityBase

                Dim bFlag As Boolean = False
                Dim bNotification As Boolean = False
                Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity = Nothing
                Dim sql As String = "spGetPaymentLevelID__c"
                Dim PaymentLevelID As Integer
                PaymentLevelID = Convert.ToInt16(DataAction.ExecuteScalar(sql, IAptifyDataAction.DSLCacheSetting.BypassCache))

                oPayment = AptifyApplication.GetEntityObject("Payments", -1)
                With oPayment
                    .SetValue("PaymentLevelID", PaymentLevelID)
                    .SetValue("EmployeeID", EBusinessGlobal.WebEmployeeID(Page.Application))
                    .SetValue("PersonID", User1.PersonID)
                    .SetValue("PaymentDate", Date.Today)
                    .SetValue("DepositDate", Date.Today)
                    .SetValue("EffectiveDate", Date.Today)
                    .SetValue("PaymentTypeID", CreditCard.PaymentTypeID)
                    If CreditCard.CCNumber = "-Ref Transaction-" Then
                        .SetValue("ReferenceTransactionNumber", CreditCard.ReferenceTransactionNumber)
                        .SetValue("ReferenceExpiration", CreditCard.ReferenceExpiration)
                    End If
                    .SetAddValue("_xCCSecurityNumber", CreditCard.CCSecurityNumber)
                    Dim sSql As String = Database & "..spGetOrderDetailsForPayments__c @OrderID=" & OrderID
                    Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                    Dim bIsFirmPay As Boolean = False
                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                        bIsFirmPay = Convert.ToBoolean(dt.Rows(0)("FirmPay__c"))
                        For Each dr As DataRow In dt.Rows
                            If Convert.ToDecimal(dr("Amount")) > 0 Then
                                With .SubTypes("PaymentLines").Add()
                                    .SetValue("AppliesTo", "Order Line")
                                    .SetValue("OrderID", dr("OrderID"))
                                    .SetValue("OrderDetailID", dr("OrderDetailID"))
                                    .SetValue("LineNumber", dr("Sequence"))
                                    .SetValue("Amount", Math.Round(Convert.ToDecimal(dr("Amount")), 2))
                                    .SetValue("BillToPersonID", User1.PersonID)
                                End With
                            End If
                        Next
                        ' For Tax Payment 
                        If TaxAmount > 0 Then
                            With .SubTypes("PaymentLines").Add()
                                .SetValue("AppliesTo", "Tax")
                                .SetValue("OrderID", OrderID)
                                .SetValue("Amount", TaxAmount)
                                .SetValue("BillToPersonID", User1.PersonID)
                            End With
                        End If


                    End If
                    'Siddharth: Added to check if Firm is Paying for firm admin order cc payment fail issue.
                    If bIsFirmPay Then
                        .SetValue("CompanyID", User1.CompanyID)
                    End If
                    .SetValue("CCAccountNumber", CreditCard.CCNumber)
                    .SetValue("CCExpireDate", CreditCard.CCExpireDate)
                    If .Fields("PaymentInformationID").EmbeddedObjectExists Then
                        Dim payInformation As PaymentInformation = DirectCast(.Fields("PaymentInformationID").EmbeddedObject, PaymentInformation)
                        payInformation.CreditCardSecurityNumber = CreditCard.CCSecurityNumber
                        payInformation.SetValue("SaveForFutureUse", CreditCard.SaveCardforFutureUse)
                        payInformation.SetValue("CCPartial", CreditCard.CCPartial)
                    End If
                    Dim errorMessage As String = String.Empty
                    If .Save(False, errorMessage) Then
                        'ViewState("AddedTaxAmount")
                        Return True
                    Else
                        OrderGE.Delete()
                    End If
                End With

                Return False

            Catch ex As Exception
                OrderGE.Delete()
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Added By Pradip 2016-01-10 to Check Approve EE For Person (For G3 Tracker G3-48)
        ''' </summary>
        ''' <remarks></remarks>
        Private Function CheckApprovedEE() As Boolean
            Try
                Dim oParams(0) As IDataParameter
                oParams(0) = Me.DataAction.GetDataParameter("@PersonId", SqlDbType.BigInt, User1.PersonID)
                Dim sSQL As String = Database & "..spCheckApprovedEE__c"
                Dim iCheck As Integer = Convert.ToInt32(DataAction.ExecuteScalarParametrized(sSQL, CommandType.StoredProcedure, oParams))
                If iCheck > 0 Then
                    Return True
                Else
                    Return False
                End If
                Return False
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Function

#Region "DropDown"
        Protected Sub ddlPaymentPlan_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlPaymentPlan.SelectedIndexChanged
            Try
                Dim iPaymentPlan As Integer = 0
                If ddlPaymentPlan.SelectedValue <> "Select Payment Plan" Then
                    iPaymentPlan = ddlPaymentPlan.SelectedValue
                End If
                DisplayPaymentSchedule(iPaymentPlan)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub DisplayPaymentSchedule(ByVal PaymentPlanID As Integer)
            Try


                Dim sSql As String = Database & "..spGetPaymentPlansDetailsAsPerPlan__c @PaymentPlanID=" & PaymentPlanID
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    radPaymentPlanDetails.Visible = True
                    radPaymentPlanDetails.DataSource = dt
                    radPaymentPlanDetails.DataBind()
                    ViewState("SelectedPaymentPlan") = dt
                Else
                    radPaymentPlanDetails.Visible = False
                    ViewState("SelectedPaymentPlan") = Nothing
                    'UpdatePanelPayment.Update()
                End If

                Dim dtPaymentPlan As DataTable = DirectCast(ViewState("SelectedPaymentPlan"), DataTable)
                If Not dtPaymentPlan Is Nothing AndAlso dtPaymentPlan.Rows.Count > 0 Then
                    For Each drP As DataRow In dtPaymentPlan.Rows
                        Dim dStageAmt As Decimal = CDec(Convert.ToString(lblStagePaymentTotal.Text).Substring(1, Convert.ToString(lblStagePaymentTotal.Text).Length - 1))
                        Dim dStudentPayAmt As Decimal = CDec(Convert.ToString(lblAmountPaidStudent.Text).Substring(1, Convert.ToString(lblAmountPaidStudent.Text).Length - 1))
                        Dim dPercentageAmt As Decimal = (dStageAmt * CDec(drP("Percentage"))) / 100
                        If CInt(drP("days")) = 0 Then
                            Dim dintialStageAmount As Decimal = dStudentPayAmt - dStageAmt
                            txtIntialAmount.Text = Format(CDec(dintialStageAmount + dPercentageAmt), "0.00")
                        Else
                            Dim dintialStageAmount As Decimal = dStudentPayAmt - dStageAmt
                            txtIntialAmount.Text = Format(CDec(dintialStageAmount), "0.00")
                        End If
                        Exit For
                    Next

                    Dim taxAmt As Decimal = Convert.ToString(lblTaxAmount.Text).Substring(1, Convert.ToString(lblTaxAmount.Text).Length - 1).Trim
                    txtIntialAmount.Text = Format((CDec(txtIntialAmount.Text) + taxAmt), "0.00")
                Else
                    Dim dAmtPaidByStudent As Decimal = CDec(Convert.ToString(lblAmountPaidStudent.Text).Substring(1, Convert.ToString(lblAmountPaidStudent.Text).Length - 1).Trim)
                    Dim taxAmt As Decimal = Convert.ToString(lblTaxAmount.Text).Substring(1, Convert.ToString(lblTaxAmount.Text).Length - 1).Trim

                    txtIntialAmount.Text = dAmtPaidByStudent + taxAmt

                    'Dim taxAmt As Decimal = Convert.ToString(lblTaxAmount.Text).Substring(1, Convert.ToString(lblTaxAmount.Text).Length - 1).Trim
                    'txtIntialAmount.Text = Format((CDec(txtIntialAmount.Text) + taxAmt), "0.00")
                    ' txtIntialAmount.Text = Convert.ToString(lblAmountPaidStudent.Text).Substring(1, Convert.ToString(lblAmountPaidStudent.Text).Length - 1).Trim
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

        Protected Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
            Try
                Response.Redirect(StudentEnrollmentPage, False)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub



        Protected Sub btnSuccess_Click(sender As Object, e As System.EventArgs) Handles btnSuccess.Click
            radwindowSubmit.VisibleOnPageLoad = False
            btnSubmitOk.Enabled = False
            Response.Redirect(StudentEnrollmentPage, False)
        End Sub

        Protected Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
            radwindowPaymentFail.VisibleOnPageLoad = False
        End Sub
        'Redmine #13814
        Protected Sub radPaymentPlanDetails_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles radPaymentPlanDetails.ItemDataBound
            For Each row As Telerik.Web.UI.GridItem In radPaymentPlanDetails.Items
                Dim lbldays As Label = DirectCast(row.FindControl("lbldays"), Label)
                Dim lblScheduleDate As Label = DirectCast(row.FindControl("lblScheduleDate"), Label)
                Dim lblAmt As Label = DirectCast(row.FindControl("lblAmt"), Label)
                If lbldays.Text = "0" Then
                    lblScheduleDate.Visible = False
                    lblAmt.Visible = False
                End If
            Next
        End Sub
    End Class
End Namespace
