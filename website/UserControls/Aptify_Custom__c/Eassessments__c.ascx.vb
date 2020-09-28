'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Sheela Jarali              16-Oct-2019                          Eassessment for DEBK and Law Exam
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness
Imports Telerik.Web.UI
Imports System.Drawing
Imports Aptify.Applications.OrderEntry
Imports Aptify.Applications.Accounting
Imports Aptify.Applications.OrderEntry.Payments
'Imports GlobalPayments.Api
'Imports GlobalPayments.Api.Entities
'Imports GlobalPayments.Api.PaymentMethods
'Imports System.Runtime
'Imports Aptify.Consulting.PaymentGateway.Realex.Icai.Commerce.RealexPaymentProvider
'Imports Aptify.Consulting.PaymentGateway.Realex
'Imports System.Net
'Imports System.IO
'Imports System.Xml
'Imports System.Security.Cryptography
'Imports Dovetail.Realex.Realauth

Partial Class Eassessments__c
    Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_EassessmentConfirmation As String = "EnrollConfirmationURL"
    Dim _studentEnrollmentDetails As New DataTable()
    Dim dtSummerPaymentSummery As DataTable = SummerPaymentSummery
    ''PSD
    'Dim m_payerFName As String, m_payerSName As String, m_payerRef As String, m_cardRef As String
    'Dim m_transSHA1Hash As String, m_normalPassword As String
    'Protected m_MerchantValues As Dictionary(Of String, String)
    'Dim sGatewayLocation As String = ""
    'Dim sProtocolType As String = ""
    'Dim sMerchantId As String, sUsername As String, sPassword As String


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
    Protected Overrides Sub SetProperties()
        If String.IsNullOrEmpty(LoginPage) Then
            LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage)
        End If
        If String.IsNullOrEmpty(EassessmentConfirmation) Then
            EassessmentConfirmation = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_EassessmentConfirmation)
        End If

    End Sub
    Public Overridable Property EassessmentConfirmation() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_EassessmentConfirmation) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_EassessmentConfirmation))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_EassessmentConfirmation) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property
#End Region


#Region "Page Events    "
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            SetProperties()
            If AptifyEbusinessUser1.PersonID <= 0 Then
                Session("ReturnToPage") = Request.RawUrl
                Response.Redirect(LoginPage)
            Else
                If Not IsPostBack Then
                    If Not Session("SummerPaymentSummery") Is Nothing Then
                        Session("SummerPaymentSummery") = Nothing
                    End If

                    Session("PlaceOrder") = False
                    LoadCurrentAcademicCycle()
                    LoadHeaderText()
                    'pnlDetails.Visible = True
                    GetPrefferedCurrency()
                    CreditCard.LoadCreditCardInfo()



                End If
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

#End Region
#Region "Private Functions"
    Private Sub LoadCurrentAcademicCycle()
        Try
            Dim sSql As String = Database & "..spCommonCurrentAcadmicCycle__c"
            'spCommonCurrentAcadmicCycle__c
            Dim iCurrentAcademicCycleID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
            If iCurrentAcademicCycleID > 0 Then
                ViewState("AcademicCycleID") = iCurrentAcademicCycleID
                ViewState("CurrentAcademicCycle") = AptifyApplication.GetEntityRecordName("AcademicCycles__c", iCurrentAcademicCycleID)
            End If
            'Dim sNextAcademicCycle As String = Database & "..spGetNextAcademicCycleStudentEnrollment__c @AcademicCycleID=" & iCurrentAcademicCycleID
            'Dim iNextAcademicCycleID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sNextAcademicCycle, IAptifyDataAction.DSLCacheSetting.BypassCache))
            'lblNextAcademicCycleName.Text = AptifyApplication.GetEntityRecordName("AcademicCycles__c", iNextAcademicCycleID)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Sub GetPrefferedCurrency()
        Try
            Dim sSql As String = Database & "..spGetPrefferedCurencyTypeSymbol__c @PersonID=" & AptifyEbusinessUser1.PersonID
            Dim dt As DataTable = DataAction.GetDataTable(sSql)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                ViewState("CurrencyTypeID") = Convert.ToString(dt.Rows(0)("CurrencySymbol"))
                lblCurrency.Text = ViewState("CurrencyTypeID")
            End If


        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Sub LoadHeaderText()
        Try
            lblFirstLast.Text = AptifyEbusinessUser1.FirstName + " " + AptifyEbusinessUser1.LastName
            lblStudentNumber.Text = CStr(AptifyEbusinessUser1.PersonID)
            Dim dt As New DataTable()
            Dim sSql As String = Database & "..spGetCurrentExamSessionID__c"
            dt = Me.DataAction.GetDataTable(sSql, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            Dim iSessionId As Int32
            iSessionId = Convert.ToInt32(dt.Rows(0)("SessionID"))
            If iSessionId > 0 Then
                ViewState("SessionID") = iSessionId
                lblSession.Text = Convert.ToString(AptifyApplication.GetEntityRecordName("ExamSessions__C", iSessionId))
                lblAcademicCycle.Text = Convert.ToString(ViewState("CurrentAcademicCycle"))
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
#End Region
    Protected Sub gvCurriculumCourse_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gvCurriculumCourse.NeedDataSource
        Dim SessionId As Integer = Convert.ToInt32(ViewState("SessionID"))
        If SessionId > 0 Then


            LoadEassessmentDataByStudent()
        Else
            lblEnrollmentMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Eassessments.NoEnrollments__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            lblEnrollmentMsg.Visible = True
            lblEnrollmentMsg.Font.Bold = True
            UpdatePanel2.Visible = False
            dvCycle.Visible = False
        End If

    End Sub

    Public Property SummerPaymentSummery() As DataTable
        Get
            'Dim dt As New DataTable

            If Not ViewState("SummerPaymentSummery") Is Nothing Then
                Return CType(ViewState("SummerPaymentSummery"), DataTable)
            Else
                Dim dtSummerPaymentSummery As New DataTable
                dtSummerPaymentSummery.Columns.Add("Subject")
                dtSummerPaymentSummery.Columns.Add("ClassID")
                dtSummerPaymentSummery.Columns.Add("Type")
                dtSummerPaymentSummery.Columns.Add("ProductID")
                dtSummerPaymentSummery.Columns.Add("WhoPay")
                dtSummerPaymentSummery.Columns.Add("Price")
                dtSummerPaymentSummery.Columns.Add("CurriculumID")
                dtSummerPaymentSummery.Columns.Add("IsProductPaymentPlan")
                dtSummerPaymentSummery.Columns.Add("AcademicCycleID")
                dtSummerPaymentSummery.Columns.Add("TaxAmount")
                dtSummerPaymentSummery.Columns.Add("SessionType")
                dtSummerPaymentSummery.Columns.Add("AlternateTimeTable")

                Return dtSummerPaymentSummery
            End If
        End Get
        Set(ByVal value As DataTable)
            ViewState("SummerPaymentSummery") = value
        End Set
    End Property
    Private Sub LoadEassessmentDataByStudent()
        Try
            lblNoRecords.Text = String.Empty

            Dim Courses As String = Convert.ToString(AptifyApplication.GetEntityAttribute("Curriculum Definitions", "EassessmentCourses__c"))
            Dim sSql As String = Database & "..spGetStudentEassessmentsforEnrol__c @StudentID=" & AptifyEbusinessUser1.PersonID & ", @Courses='" & Courses & "'"
            _studentEnrollmentDetails = Me.DataAction.GetDataTable(sSql, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)


            If Not _studentEnrollmentDetails Is Nothing AndAlso _studentEnrollmentDetails.Rows.Count > 0 Then
                gvCurriculumCourse.DataSource = _studentEnrollmentDetails
                ViewState("_studentEnrollmentDetails") = _studentEnrollmentDetails
                ViewState("StudentGroupID") = Convert.ToInt32(_studentEnrollmentDetails.Rows(0)("StudentGroupID__c"))
                ViewState("RouteOfEntry") = Convert.ToInt32(_studentEnrollmentDetails.Rows(0)("RouteOfEntry"))

            Else
                lblEnrollmentMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.Eassessments.NoEnrollments__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblEnrollmentMsg.Visible = True
                lblEnrollmentMsg.Font.Bold = True
                UpdatePanel2.Visible = False
            End If

        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
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
    Protected Sub chkAssessment_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim chkAssessment As CheckBox = DirectCast(sender, CheckBox)
            chkAssessment.Enabled = False
            Dim item As GridDataItem = DirectCast(chkAssessment.NamingContainer, GridDataItem)
            Dim lblAssessment As Label = DirectCast(item.FindControl("lblAssessment"), Label)
            Dim lblType As Label = DirectCast(item.FindControl("lblType"), Label)
            Dim lblCurriculumID As Label = DirectCast(item.FindControl("lblCurriculumID"), Label)
            Dim lblSubject As Label = DirectCast(item.FindControl("lblSubject"), Label)
            Dim lblStudentGroup As Label = DirectCast(item.FindControl("lblStudentGroup"), Label)
            Dim lblAlternativeGroup As Label = DirectCast(item.FindControl("lblAlternativeGroup"), Label)
            ViewState("StudentGroupID") = lblStudentGroup.Text.Trim
            Dim dt1 As DataTable = dtSummerPaymentSummery
            If Not Session("SummerPaymentSummery") Is Nothing Then
                dt1 = DirectCast(Session("SummerPaymentSummery"), DataTable)
            End If
            Dim CourseID As Integer = Convert.ToInt32(lblAssessment.Text) ''18 ''Convert.ToInt32(DataBinder.Eval(gvrow.DataItem, "SubjectID"))
            Dim SessionId As Integer = Convert.ToInt32(ViewState("SessionID"))
            Dim Type As String = Convert.ToString(lblType.Text)
            Dim sSql As String = Database & "..spGetNewClassDetails__c @CourseID=" & CourseID & ", @AcademicCycleID=" & ViewState("AcademicCycleID") & ", @SessionID=" & SessionId & ",@type='" & Type & "'" ''@CourseID INT, @AcademicCycleID INT, @SessionID INT, @type
            Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)


            If chkAssessment.Checked Then
                UpdatePanel2.Visible = True


                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

                    Dim drSummerPaymentSummery As DataRow = dt1.NewRow()


                    drSummerPaymentSummery("Subject") = Convert.ToString(lblSubject.Text)
                    drSummerPaymentSummery("Type") = Type ''"Interim Assessment" 'Convert.ToInt32(DataBinder.Eval(gvrow.DataItem, "Type"))
                    drSummerPaymentSummery("ProductID") = Convert.ToInt32(dt.Rows(0)("ProductID"))
                    If WhoPaysForElevationStudent(CInt(dt.Rows(0)("ProductID")), 2, dt.Rows(0)("classid")) Then
                        drSummerPaymentSummery("WhoPay") = "Firm Pay"
                        drSummerPaymentSummery("IsProductPaymentPlan") = 0
                    Else
                        drSummerPaymentSummery("WhoPay") = "Member Pay"
                        If Convert.ToInt32(dt.Rows(0)("IsProductPaymentPlan")) > 0 Then
                            drSummerPaymentSummery("IsProductPaymentPlan") = 1
                        Else
                            drSummerPaymentSummery("IsProductPaymentPlan") = 0
                        End If

                    End If
                    drSummerPaymentSummery("ClassID") = Convert.ToInt32(dt.Rows(0)("classid"))
                    Dim dInterimAssTax As Decimal
                    drSummerPaymentSummery("Price") = GetPrice(Convert.ToInt32(dt.Rows(0)("ProductID")), Convert.ToInt32(dt.Rows(0)("classid")), ViewState("StudentGroupID"), dInterimAssTax)
                    drSummerPaymentSummery("TaxAmount") = dInterimAssTax

                    drSummerPaymentSummery("CurriculumID") = Convert.ToString(lblCurriculumID.Text)
                    drSummerPaymentSummery("AcademicCycleID") = ViewState("AcademicCycleID")
                    If lblAlternativeGroup.Text.Trim <> "" Then
                        drSummerPaymentSummery("AlternateTimeTable") = lblAlternativeGroup.Text.Trim
                    End If
                    drSummerPaymentSummery("SessionType") = Convert.ToString(dt.Rows(0)("Session"))

                    dt1.Rows.Add(drSummerPaymentSummery)
                    Session("SummerPaymentSummery") = dt1
                    '' _enrollmentList.Add(New Enrollment(0, Convert.ToInt32(lblCurriculumID.Text), Convert.ToInt32(CourseID), chkClassRoomTxt(0), chkClassRoomTxt(1), 0, lblCutOffUnits.Text, Convert.ToDouble(lblMinimumUnits.Text), Convert.ToString(drSummerPaymentSummery("Type")), False, isFailed, FailedUnits, FirstAttempt, Convert.ToDecimal(lblUnits.Text), "Summer"))

                Else
                    UpdatePanel2.Visible = False
                    Session("SummerPaymentSummery") = Nothing
                End If
            Else
                dt1 = DirectCast(Session("SummerPaymentSummery"), DataTable)
                If Not dt1 Is Nothing AndAlso dt1.Rows.Count > 0 Then
                    Dim dr() As DataRow = dt1.Select("ProductID=" & Convert.ToInt32(dt.Rows(0)("ProductID")))
                    dt1.Rows.Remove(dr(0))
                    dt1.AcceptChanges()
                    Session("SummerPaymentSummery") = dt1
                Else
                    Session("SummerPaymentSummery") = Nothing
                End If

            End If

            LoadData()
            chkAssessment.Enabled = True
        Catch ex As Exception

            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Protected Function GetPrice(ByVal lProductID As Long, ByVal classID As Long, ByVal StudentGroupID As Long, ByRef TaxAmount As Decimal) As String
        Try
            Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
            Dim oOL As Aptify.Applications.OrderEntry.OrderLinesEntity
            'Here get the Top 1 Person ID whose MemberTypeID = 1 
            oOrder = TryCast(AptifyApplication.GetEntityObject("Orders", -1), OrdersEntity)
            oOrder.ShipToID = AptifyEbusinessUser1.PersonID
            oOrder.BillToID = AptifyEbusinessUser1.PersonID
            oOrder.AddProduct(lProductID, 1)
            Dim sPrice As String
            If oOrder.SubTypes("OrderLines").Count > 0 Then
                oOL = TryCast(oOrder.SubTypes("OrderLines").Item(0), OrderLinesEntity)
                With oOL
                    .ExtendedOrderDetailEntity.SetValue("ClassID", classID)
                    .ExtendedOrderDetailEntity.SetValue("Status", "Registered")
                    .ExtendedOrderDetailEntity.SetValue("AcademicCycleID__c", 1)
                    .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", StudentGroupID)
                    .ExtendedOrderDetailEntity.SetValue("RouteOfEntryID__c", Convert.ToInt32(ViewState("ExternalTypeID")))
                    .SetAddValue("__ExtendedAttributeObjectData", .ExtendedOrderDetailEntity.GetObjectData(False))
                End With
                oOL.SetProductPrice()

                Dim dPaymentAmount As Decimal
                If Convert.ToBoolean(oOL.GetValue("TaxIncludedInPrice")) = True AndAlso oOL.SubTypes("OrderLineTaxes").Count > 0 Then
                    'Dim dTaxableAmt As Decimal = Convert.ToDouble(oOL.SubTypes("OrderLineTaxes").Item(0).GetValue("TaxableAmount"))
                    'dPaymentAmount = dTaxableAmt
                    dPaymentAmount = Convert.ToDouble(oOL.SubTypes("OrderLineTaxes").Item(0).GetValue("TaxableAmount"))
                    '  dPaymentAmount = ViewState("CurrencyTypeID") & Format(CDec(dTaxableAmt), "0.00")

                Else
                    dPaymentAmount = oOL.Extended
                    'If ViewState("AddedTaxAmount") Is Nothing Then
                    '    ViewState("AddedTaxAmount") = dTaxAmount
                    '    dPaymentAmount = oOL.Extended + dTaxAmount
                    'Else
                    '    dPaymentAmount = oOL.Extended
                    'End If

                    'If dTaxAmount > 0 Then

                    '    If ViewState("AddedTaxAmount") Is Nothing Then
                    '        ViewState("AddedTaxAmount") = dTaxAmount
                    '        dPaymentAmount = oOL.Extended + dTaxAmount
                    '    Else

                    '        dPaymentAmount = oOL.Extended
                    '    End If
                    'Else
                    '    dPaymentAmount = oOL.Extended
                    'End If

                End If

                'Dim dShippingAmount As Double = Convert.ToDouble(oOrder.GetValue("CALC_ShippingCharge"))
                'Dim dHandlingAmount As Double = Convert.ToDouble(oOrder.GetValue("CALC_HandlingCharge"))
                Dim dTaxAmount As Double = Convert.ToDouble(oOrder.GetValue("CALC_SalesTax"))
                TaxAmount = dTaxAmount
                Dim dTaxamt As Decimal
                If dTaxAmount > 0 Then
                    lblTaxAmount.Visible = True
                    lblTax.Visible = True
                    ViewState("dTaxamt") = CDec(ViewState("dTaxamt")) + dTaxAmount

                    lblTaxAmount.Text = ViewState("CurrencyTypeID") & Format(CDec(ViewState("dTaxamt")), "0.00")
                End If
                sPrice = ViewState("CurrencyTypeID") & Format(CDec(dPaymentAmount), "0.00")
                Return sPrice

                '  Return Convert.ToString(oOL.Price)
            Else
                Return Convert.ToString(ViewState("CurrencyTypeID") & 0)
            End If
        Catch ex As Exception
            Return Convert.ToString(0)
        End Try
    End Function
    Protected Function WhoPaysForElevationStudent(ByVal ProductID As Integer, ByVal RouteOfEntryID As Integer, ByVal NewClassID As Integer) As Boolean
        Try
            'Dim sEducationContactCompanySQl As String = "..spGetCompanyInEducaionContract__c @PersonID=" & AptifyEbusinessUser1.PersonID & ",@RouteOfEntryID=" & RouteOfEntryID
            ''Change by pradip 2016-02-12 for Tracker G3-48 and Issue no 5417
            Dim sEducationContactCompanySQl As String = "..spGetCompanyInEEApplication__c @PersonID=" & AptifyEbusinessUser1.PersonID & ",@RouteOfEntryID=" & RouteOfEntryID
            Dim iCompanyID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sEducationContactCompanySQl, IAptifyDataAction.DSLCacheSetting.BypassCache))
            Dim oProductGE As AptifyGenericEntityBase = AptifyApplication.GetEntityObject("Products", ProductID)
            ' First Find this product who pay in persons who pay sub entity and if not found in person then check in Company who pay entity records
            'Dim sSqlPersonWhoPay As String = "..spGetPersonWhoPay__c @PersonID=" & BillToID & ",@OrderlineProduct=" & ProductID & ",@ProductCategoryID=" & Convert.ToInt32(oProductGE.GetValue("CategoryID")) & ",@OrderDate='" & Me.OrderDate & "'"
            Dim sSqlPersonWhoPay As String = "..spGetPersonWhoPayForElevationStudent__c @PersonID=" & Me.AptifyEbusinessUser1.PersonID & ",@OrderlineProduct=" & ProductID & ",@ProductCategoryID=" & Convert.ToInt32(oProductGE.GetValue("CategoryID")) & ",@OrderDate='" & New DateTime(Year(Today), Month(Today), Day(Today)) & "',@RouteOfEntryID=" & RouteOfEntryID & ",@NewClassID=" & NewClassID

            Dim dtWhopayonPerson As DataTable = DataAction.GetDataTable(sSqlPersonWhoPay, IAptifyDataAction.DSLCacheSetting.BypassCache)

            Dim WhoPay As String = Convert.ToString(dtWhopayonPerson.Rows(0)("WhoPay"))
            If Not String.IsNullOrEmpty(WhoPay) AndAlso WhoPay <> "" Then
                If WhoPay.Trim.ToLower = "firm pays" Then
                    Return True
                ElseIf WhoPay.Trim = "" Then
                    Dim sSqlCompanyParent As String = Database & "..spCheckCompanyRTOOrCentrallyManaged__c @CompanyID=" & iCompanyID
                    Dim dtCompanyParent As DataTable = DataAction.GetDataTable(sSqlCompanyParent, IAptifyDataAction.DSLCacheSetting.BypassCache)
                    If Not dtCompanyParent Is Nothing AndAlso dtCompanyParent.Rows.Count > 0 Then
                        If dtCompanyParent.Rows(0)("ParentCompanyID") > 0 AndAlso CBool(dtCompanyParent.Rows(0)("CentrallyManaged__c")) Then
                            iCompanyID = dtCompanyParent.Rows(0)("ParentCompanyID")
                        End If
                    End If
                    Dim ssqlCompanyWhoPay As String = "..spGetCompanyWhoPayForElevationStudent__c @CompanyID=" & iCompanyID & ",@OrderlineProduct=" & ProductID & ",@ProductCategoryID=" & Convert.ToInt32(oProductGE.GetValue("CategoryID")) & ",@OrderDate='" & New DateTime(Year(Today), Month(Today), Day(Today)) & "',@PersonID=" & AptifyEbusinessUser1.PersonID & ",@RouteOfEntryID=" & RouteOfEntryID & ",@NewClassID=" & NewClassID
                    Dim CompayWhoPayID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(ssqlCompanyWhoPay, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    If CompayWhoPayID > 0 Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Else
                Dim sSqlCompanyParent As String = Database & "..spCheckCompanyRTOOrCentrallyManaged__c @CompanyID=" & iCompanyID
                Dim dtCompanyParent As DataTable = DataAction.GetDataTable(sSqlCompanyParent, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dtCompanyParent Is Nothing AndAlso dtCompanyParent.Rows.Count > 0 Then
                    If dtCompanyParent.Rows(0)("ParentCompanyID") > 0 AndAlso CBool(dtCompanyParent.Rows(0)("CentrallyManaged__c")) Then
                        iCompanyID = dtCompanyParent.Rows(0)("ParentCompanyID")
                    End If
                End If
                Dim ssqlCompanyWhoPay As String = "..spGetCompanyWhoPayForElevationStudent__c @CompanyID=" & iCompanyID & ",@OrderlineProduct=" & ProductID & ",@ProductCategoryID=" & Convert.ToInt32(oProductGE.GetValue("CategoryID")) & ",@OrderDate='" & New DateTime(Year(Today), Month(Today), Day(Today)) & "',@PersonID=" & AptifyEbusinessUser1.PersonID & ",@RouteOfEntryID=" & RouteOfEntryID & ",@NewClassID=" & NewClassID
                Dim CompayWhoPayID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(ssqlCompanyWhoPay, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If CompayWhoPayID > 0 Then
                    Return True
                Else
                    Return False
                End If
            End If

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Function
    Private Sub LoadData()
        Try
            If Not Session("SummerPaymentSummery") Is Nothing Then
                Dim SummerPaymentSummery As DataTable = CType(Session("SummerPaymentSummery"), DataTable)
                If Not SummerPaymentSummery Is Nothing AndAlso SummerPaymentSummery.Rows.Count > 0 Then
                    GV.DataSource = Nothing
                    GV.DataBind()
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
                        ''radSummerPaymentSummery.Visible = True
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

                            'Dim sType As String = dr("Type").ToString()
                            'If sType = "Revision" OrElse sType = "Autumn Exam" OrElse sType = "Repeat Revision" Then
                            '    lblAutumnMsg.Visible = True
                            '    lblAutumnMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.AutumRevisionMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            'End If
                            Dim group As String = dr("Subject").ToString()
                            Dim rate As Decimal = CDec(Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1))
                            'If dicSum.ContainsKey(group) Then
                            '    dicSum(group) += rate
                            'Else
                            If Not dicSum.ContainsKey(group) Then
                                dicSum.Add(group, rate)
                            End If

                            'End If
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
                        If Not Session("SummerPaymentSummery") Is Nothing Then
                            Session("SummerPaymentSummery") = Nothing
                        End If
                        UpdatePanel2.Visible = False
                        GV.DataSource = Nothing
                        GV.DataBind()
                        radSummerPaymentSummery.Visible = False
                        lblAmount.Visible = False
                        lblTotalAmount.Visible = False
                        lblTotalAmount.Text = ""
                        lblAmountPaidStudent.Text = ""
                        lblStudentPaidLabel.Visible = False
                        lblAmountPaidStudent.Visible = False
                        lblFirmPaidLabel.Visible = False
                        lblAmountPaidFirm.Visible = False
                        lblAmountPaidFirm.Text = ""
                        lblPaymentSummery.Visible = False
                        ddlPaymentPlan.Visible = False
                        CreditCard.Visible = False
                        txtIntialAmount.Visible = False
                        txtIntialAmount.Text = ""
                        lblCurrency.Visible = False
                        lblIntialAmt.Visible = False
                        lblTaxAmount.Visible = False
                        lblTax.Visible = False
                        ddlPaymentPlan.Visible = False
                        lblPaymentPlan.Visible = False
                    End If
                Else
                    radSummerPaymentSummery.Visible = False
                    lblTotalAmount.Text = ""
                    lblAmountPaidStudent.Text = ""
                    lblAmountPaidFirm.Text = ""
                    txtIntialAmount.Text = ""

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
                    UpdatePanel2.Visible = False
                    GV.DataSource = Nothing
                    GV.DataBind()

                End If



            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
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
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
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
                Response.Redirect(EassessmentConfirmation, False)
            Else
                ' check person credit limit added by Govind for redmine issue #13672
                Dim sCreditSql As String = "..spGetCreditLimitAndStatus__c @PersonID=" & AptifyEbusinessUser1.PersonID
                Dim isCreditLimitApproved As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sCreditSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If isCreditLimitApproved = False Then
                    lblSubmitMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment..CreditLimit")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    radwindowSubmit.VisibleOnPageLoad = True
                    btnSubmitOk.Visible = False
                    btnNo.Visible = False
                    btnSuccess.Visible = True
                Else
                    CreateOrderObjectsOnLoad()

                    CreateOrderAndSaveData()
                    'Createorder code here
                End If

            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Sub CreateOrderObjectsOnLoad()
        Try
            If Not Session("SummerPaymentSummery") Is Nothing Then
                Dim dtpmtInfoTable As DataTable = TryCast(Session("SummerPaymentSummery"), DataTable)
                If Not dtpmtInfoTable Is Nothing AndAlso dtpmtInfoTable.Rows.Count > 0 Then
                    Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
                    oOrder = TryCast(AptifyApplication.GetEntityObject("Orders", -1), OrdersEntity)
                    oOrder.ShipToID = AptifyEbusinessUser1.PersonID
                    oOrder.BillToID = AptifyEbusinessUser1.PersonID
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

                                If row("AlternateTimeTable").ToString().Trim <> "" Then
                                    .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", AptifyApplication.GetEntityRecordIDFromRecordName("StudentGroups__c", row("AlternateTimeTable").ToString().Trim))

                                Else
                                    .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", ViewState("StudentGroupID"))
                                End If

                                .ExtendedOrderDetailEntity.SetValue("RouteOfEntryID__c", ViewState("RouteOfEntry"))
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
                    oFirmOrder.ShipToID = AptifyEbusinessUser1.PersonID
                    oFirmOrder.BillToID = AptifyEbusinessUser1.PersonID
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

                            .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", ViewState("StudentGroupID"))
                            If Convert.ToString(dr("AlternateTimeTable")) <> "" Then
                                .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", AptifyApplication.GetEntityRecordIDFromRecordName("StudentGroups__c", Convert.ToString(dr("AlternateTimeTable"))))

                            Else
                                .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", ViewState("StudentGroupID"))
                            End If

                            .ExtendedOrderDetailEntity.SetValue("RouteOfEntryID__c", ViewState("RouteOfEntry"))
                            .SetAddValue("__ExtendedAttributeObjectData", .ExtendedOrderDetailEntity.GetObjectData(False))
                            '.SetValue("BillToCompanyID__c", AptifyApplication.GetEntityRecordIDFromRecordName("Companies", ViewState("CompanyID")))
                            'Added BY Pradip 2016-01-10 For Group 3 Tracker G3-48
                            '.SetValue("BillToCompanyID__c", AptifyApplication.GetEntityRecordIDFromRecordName("Companies", ViewState("CompanyID")))
                            'If CheckApprovedEE() Then
                            '    ' .SetValue("BillToCompanyID__c", AptifyApplication.GetEntityRecordIDFromRecordName("Companies", ViewState("CompanyID")))
                            '    .SetValue("BillToCompanyID__c", Convert.ToString(ViewState("CompanyID")))
                            'Else
                            '    If Convert.ToInt32(ViewState("IsFirmAdmin")) = 1 Then
                            '    Else
                            '        .SetValue("BillToCompanyID__c", -1)
                            '    End If
                            'End If
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
                Session("PlaceOrder") = True
                Response.Redirect(EassessmentConfirmation, False)
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
                '    oOrder.ShipToID = AptifyEbusinessUser1.PersonID
                '    oOrder.BillToID = AptifyEbusinessUser1.PersonID
                '    oOrder.SetValue("OrderSourceID", AptifyApplication.GetEntityRecordIDFromRecordName("Order Sources", "Web")) ' Web
                '    oOrder.SetValue("BillToSameAsShipTo", 1)
                '    oOrder.SetValue("EmployeeID", DataAction.UserCredentials.GetUserRelatedRecordID("Employees"))
                '    Dim oOrderLine As Aptify.Applications.OrderEntry.OrderLinesEntity
                '    'Dim sSQlFirmAdmin As String = Database & "..spGetUserFirmAdmin__c @PersonID=" & AptifyEbusinessUser1.PersonID
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
                'oOrder.ShipToID = AptifyEbusinessUser1.PersonID
                'oOrder.BillToID = AptifyEbusinessUser1.PersonID
                'oOrder.SetValue("OrderSourceID", AptifyApplication.GetEntityRecordIDFromRecordName("Order Sources", "Web")) ' Web
                'oOrder.SetValue("BillToSameAsShipTo", 1)
                'oOrder.SetValue("EmployeeID", DataAction.UserCredentials.GetUserRelatedRecordID("Employees"))
                'Dim oOrderLine As Aptify.Applications.OrderEntry.OrderLinesEntity
                'Dim dTaxAmount As Decimal = 0
                ''Dim sSQlFirmAdmin As String = Database & "..spGetUserFirmAdmin__c @PersonID=" & AptifyEbusinessUser1.PersonID
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
                    ''psd
                    'Dim IsVerified As Boolean = True
                    'Dim str As String = String.Empty
                    'Dim Resposecode As String = "00"
                    ''PSD end
                    If ddlPaymentPlan.SelectedValue <> "Select Payment Plan" AndAlso Not ViewState("SelectedPaymentPlan") Is Nothing Then
                        ''Dim oOrderPayInfo As PaymentInformation
                        Dim sErrorString As String = ""
                        Dim RefTranse As String = ""

                        Dim sLastError As String = ""
                        Dim oPersonGE As Aptify.Applications.Persons.PersonsEntity = DirectCast(Me.AptifyApplication.GetEntityObject("Persons", AptifyEbusinessUser1.PersonID), Aptify.Applications.Persons.PersonsEntity)

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
                            ''    .SetValue("PersonID", AptifyEbusinessUser1.PersonID)
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

                        'PSD2 Starts here
                        'code added by GM for PSD2

                        'Dim transData As RealexHelper.TransactionData = New RealexHelper.TransactionData()
                        'transData.CreditCard = New RealexHelper.CreditCard()
                        'transData.CreditCard.CardType = "Visa"
                        'transData.CreditCard.CreditCardNumber = CreditCard.CCNumber 'oPayment.GetValue("CCAccountNumber").ToString()        
                        'Dim dExpiresDate As Date = CreditCard.CCExpireDate
                        'transData.CreditCard.ExpiresDate = RealexHelper.GenerateDate(dExpiresDate.Month, dExpiresDate.Year)
                        'transData.Currency = "EUR"
                        'transData.OrderId = "1159366"
                        'transData.CreditCard.CSC = CreditCard.CCSecurityNumber


                        'Dim arrNames() As String = (AptifyEbusinessUser1.FirstName + "" + AptifyEbusinessUser1.LastName).ToString().Split(New [Char]() {" "c})
                        'm_payerFName = arrNames(0) 'First name
                        'm_payerSName = ""
                        'For i As Integer = 1 To arrNames.Length - 1
                        '    m_payerSName = m_payerSName + arrNames(i) 'Last name
                        'Next
                        ''Siddharth: Reducing length of names as per RealEx - RealVault Developer Guide v1.5
                        'If m_payerFName.Length > 30 Then
                        '    m_payerFName = m_payerFName.Substring(0, 30)
                        'End If
                        'If m_payerSName.Length > 50 Then
                        '    m_payerSName = m_payerSName.Substring(0, 50)
                        'End If
                        ''Siddharth: Used regular expression to remove special characters as Realex gives exception if there is special character in PayerRef.
                        'm_payerRef = System.Text.RegularExpressions.Regex.Replace(m_payerFName.Trim().ToLower(), "[^\w\.@-]", "") + transData.OrderId.ToString()
                        ''Siddharth: Added code to remove Fadas from payerref as Realex does not support fadas 
                        'Dim iso As Encoding = Encoding.GetEncoding("iso-8859-1")
                        'Dim utf8 As Encoding = Encoding.UTF8
                        'Dim arrBytes As Byte() = iso.GetBytes(m_payerRef)
                        'Dim lstBytes As List(Of Byte) = New List(Of Byte)
                        'For i As Int32 = 0 To arrBytes.Length - 1
                        '    If Convert.ToInt32(arrBytes(i)) <= 127 Then
                        '        lstBytes.Add(arrBytes(i))
                        '    End If
                        'Next
                        'Dim arrNewBytes As Byte() = lstBytes.ToArray()
                        'm_payerRef = utf8.GetString(arrNewBytes)
                        'm_cardRef = transData.CreditCard.CardType.ToString().ToLower() + transData.OrderId.ToString()


                        'transData.CreditCard.CardHolderName = m_payerFName + " " + m_payerSName

                        ''Dim response As RealexResponse = Nothing
                        ''Dim rResponse As RealexTransaction.Response = Nothing
                        ''PSD2 start
                        'Dim sSql As String = Database & "..spGetGatewayAndPrototype__c"
                        'Dim dt1 As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache) ', sTrans)

                        'If Not dt1 Is Nothing AndAlso dt1.Rows.Count > 0 Then
                        '    'bIsFirmPay = Convert.ToBoolean(dt.Rows(0)("FirmPay__c"))

                        '    For Each dr As DataRow In dt1.Rows
                        '        If Convert.ToString(dr("Attribute")) = "GatewayLocation" Then
                        '            sGatewayLocation = dr("Value")
                        '        End If
                        '        If Convert.ToString(dr("Attribute")) = "ProtocolType" Then
                        '            sProtocolType = dr("Value")
                        '        End If
                        '        If Convert.ToString(dr("Attribute")) = "MerchantID" Then
                        '            sMerchantId = dr("Value")
                        '        End If
                        '        If Convert.ToString(dr("Attribute")) = "Username" Then
                        '            sUsername = dr("Value")
                        '        End If
                        '        If Convert.ToString(dr("Attribute")) = "Password" Then
                        '            sPassword = dr("Value")
                        '        End If

                        '    Next
                        'End If
                        ''Dim transData As RealexHelper.TransactionData = New RealexHelper.TransactionData()
                        ''transData = getTransaData()
                        'Dim rResponse As RealexTransaction.Response = Nothing
                        'rResponse = ProcessReferenceTransaction(transData)

                        'ServicesContainer.ConfigureService(New GatewayConfig With {
                        '        .MerchantId = sMerchantId,
                        '        .AccountId = sUsername,
                        '        .SharedSecret = sPassword,
                        '        .ServiceUrl = sGatewayLocation
                        '    })

                        'Dim card = New CreditCardData With {
                        '        .Number = CreditCard.CCNumber,
                        '        .ExpMonth = CDate(CreditCard.CCExpireDate).Month,
                        '        .ExpYear = CDate(CreditCard.CCExpireDate).Year,
                        '        .Cvn = CreditCard.CCSecurityNumber,
                        '        .CardHolderName = AptifyEbusinessUser1.FirstName + " " + AptifyEbusinessUser1.LastName
                        '    }
                        'Try
                        '    'Dim lblttl As Label = DirectCast(OrderSummary__c.FindControl("lblTotal"), Label)
                        '    'Dim lAmount As Decimal = Convert.ToDecimal(Trim((lblttl.Text)).Substring(1))
                        '    Dim lAmount As Decimal = lblTotalAmount.Text.Substring(1).Trim
                        '    'send the Verify-Enrolled request to the gateway
                        '    Dim enrolled As Boolean
                        '    Try
                        '        enrolled = card.VerifyEnrolled(lAmount, transData.Currency, Nothing)
                        '    Catch ex As ApiException
                        '        lblErrorMsg.Text = ex.Message
                        '        lblErrorMsg.ForeColor = Drawing.Color.Red
                        '        lblErrorMsg.Style.Add("font-size", "medium")
                        '        lblErrorMsg.Visible = True
                        '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                        '        Throw New Exception(ex.Message)
                        '    End Try
                        '    If card.ThreeDSecure.Enrolled = "Y" Then
                        '        Dim threeDsEnrollmentDetails = card.ThreeDSecure

                        '        'get the details necessary to redirect the customer to the ACS page
                        '        Dim pareq = threeDsEnrollmentDetails.PayerAuthenticationRequest
                        '        Dim acsUrl = threeDsEnrollmentDetails.IssuerAcsUrl

                        '        Dim request As WebRequest = WebRequest.Create(acsUrl & "&PaReq=" & HttpUtility.UrlEncode(pareq) & "&TermUrl=https://www.prod-01.test/ProductCatalog/BillingPage.aspx")
                        '        request.Method = "POST"
                        '        Dim postData As String = "This is a test that posts this string to a Web server."
                        '        Dim byteArray As Byte() = Encoding.UTF8.GetBytes(postData)
                        '        request.ContentType = "application/x-www-form-urlencoded"
                        '        request.ContentLength = byteArray.Length
                        '        Dim dataStream As Stream = request.GetRequestStream()
                        '        dataStream.Write(byteArray, 0, byteArray.Length)
                        '        dataStream.Close()
                        '        Dim response1 As WebResponse = request.GetResponse()
                        '        'Console.WriteLine((CType(response1, HttpWebResponse)).StatusDescription)
                        '        Dim mid2 As String
                        '        Using __Assign(dataStream, response1.GetResponseStream())
                        '            Dim reader As StreamReader = New StreamReader(dataStream)
                        '            Dim responseFromServer As String = reader.ReadToEnd()
                        '            'divradACS.InnerHtml = (responseFromServer)
                        '            'radACS.VisibleOnPageLoad = True
                        '            'Step-3
                        '            Dim index1 As Integer = responseFromServer.LastIndexOf("PaRes") + 14
                        '            Dim index2 As Integer = responseFromServer.LastIndexOf("escapeXml") - 1
                        '            mid2 = responseFromServer.Substring(index1, index2 - index1)
                        '        End Using
                        '        response1.Close()

                        '        'Dim paRes As String = "eNrVWNey47qx/ZWp8aPqHEaJ5JRmu8AcRErM4Y1ZYhZJieHrTe09yePjKttP9+JFRKvRaDRWrwZw/PtcV5+eaT/c2ubrZ+RP+POnv78drWufpqyZxo8+fTuq6TCEefrplnz9nIQkiezjA7ynUjwjyAgLYRQhopjYJySRRp/fjhdgpMO7MopgJE6Rm+zbDG/bBH+iR+h7dzPdx9ewGd+OYXynJe0NJ3CEIo/Qt+6xTnuJfaO+tSP00T9CPwdeHq+vYfNzviVvXNvCea7oILxriNJQyGwIp+AqdAr59Qi9NI5JOKZvKIxQMAWjn2DyC45+2W9TvsuP3cscqNvHZnsPv9oR+lV23GLSp028vFHENuhH75jOXdukm8a2vh/fR+ine13YvMG/NARBNtWX9Gh5b8fxVv/qFvEFx74ghyP0Lj8OYzg+hjf/CH37Osbh8/kGAGAYIcF405kczGSMJna4Cbzattx3lWMa397g/ebU9vs+ClR529/Ga/1y9Z8FR+jlCvS+hW9H85Y322R9+mlDSTN8/Xwdx+4LBE3T9OeE/dn2OYRuC4FgCtoUkuGW/+3zx6g0kZqs/a+GMWHTNrc4rG5rOG7gUNPx2iaffvj2V2Ys42UJgQyO+WMz9UeM4M0fLwmMIfvNJvTXRn9Z2X8yy+/O9kP4x3ANkdcEvxl6Oxpplr4QkX6yDenr57/9zAH2lqfD+L9M+H2yXy18t+eE1SN9E2ZKVS7sjte6WRkc5YGpp7pIivKifv0+7kPzCP3w8Jv7H3v1S0w+FLNLWfR8nT4f3G7iQ9yfEkrvIXrHsEKG3eixuTC5EJ3rsGsgKhwV/XpmNK25BQmhDNGeSy8YuJbwcvelIrDyp4NWxMHorftzqctr5HQDCZeqhGZaplb1jGcOVI2KT9e6Kj345gazcLPv99nie+rmZdVBcni3hPNKTI77PCm30ify0tVSMzywNOYs8qyXWGAZDCCuWDE+ifYUIZFREuRJnXwO4w4oaeEOBQ9YhxGxmJzHvtXiXkeoeL1XuKpVT5XWjBvZSsvjLFn6ToE4Cn0gfXZ3gFUFdYpHF+g+eKHGSJ19qtaxxh6tSwt9HpkBdNpfMvyh1uTUTm09aFbwyPTzffX6rN15lMaAr19/wcy3HVHS5WMHvD1MseEYfnwxaT/esg28Gy2pksQGLMOAcsnBJNEgl2xJSA2br1ZI89rFMaRHaxsyVVYGZk+s7stKG0jXZ6wBnTvROphClruoABYAYnNgVgWbq2wP1mjVsCdu8llH10/cNLt6FdiBp8E25nSJYM+sBU50rjk0aC2aD2Qb5mZ2BdqHLLbo0qiiJqjigjNUQL7bZ2hV0lFqiDD5GgjkzKxA/tD3LVA5lkrjHmtxuGqBRbMAqrEcovLtJpOmdxn7QzbFLOeptP3h9zSdo5p6BA61hm7y8FCtiht95i1gfdgfVJYPGKcyqhgzrMSt4MDiNBUMH37lE+eUlaVbP2NBT5NgCM4jqZ1FNbiJ/YiFxIIrG6HzM675Mlq5kwrKD/2ryjiOOuU5d3vZYMy7YEoRxuocDXQbAFyi2Qm8/ldAu+2VziiaUZ0jdIApkyOL+3RLmkrxu2rugieNtDup1S2MLPXLWdnbJTYwgm3ZldtFWaHp6ZQqGVEJvOoxrdYiceec1VyHBMKfzqde4GsGuRdQ5PpS1120xFNmA6CkspQ0b+1STkaeieUheH7TAdFnqWdCWDe0c5NZsXvgfG7GmyXJ13t7bij9kvCJNsvjqEkjEccn1fRZrBO3qu+fTUQunqRYmUIZ4XYsQAd2J1731LQvS4yG/YNrMmtyTu/Z9Qwp2gQddDWM/GggOujWGhaGtiZVXUtH7ZgCvZYVT66PSR3uEpfl4DZRLVueFbh58k3pGzOt0Hwie7OazLSvqCeNtGLwoO8o2iZVVoFcpQHg/grr7BZ7GggEoaTTUEJPjq3Oa8Je5h3NZlc3UrOqYO6RXbbqhJOCu+CQWnhyAJNABosVaq7t6JjmkKtpuBVqFMXegPvQZeRSlWsgK2zupNlA9tZkF4f4RCWPXcFcx8nmb7Atd1OUOPkzM+B6qioyjstFjup4VL2sMBke4cL7oamshUMafsngk5UF9DNaaihi6kxQ7her5bQ+SsFZUae4E5O9rtROk2rdyDsp4ec30ze8mBl3hdwssHXN/NyKJtbtSjpieQk3zLpX72HzGEIekDhrEuOM1/YuFJRKs9F5WEZtxZJtQZenOPaLofltcN13BEGTi0bT5skWiRChbi6lswLLcjm/k6kZFGOLbwT2Ozv9FV1xgrXR1SR8pysDnPyY8vY6MZqzdbgJO66+1c6/oSrrX9LtdxrqYpSbTysYv6W9JVfvstx093DgyQ/fM7oI3V8jhra2Phq6WiVx/BqjVBG6PBy61EM12kkA7+nOcjPvhp52lQSniFBk3MYUgUkbugVSfoJnrQB71SoXlVVXjU3CTbZo60akFjdprIq/ZKqQ/0KNqkVXtOzwFLvRjaPS6gcFzarqlLLrCM6a8BoSekal6vDEfNCOwk2GazobLevkKzYvmchNARe4ezTwpJmzwOXDfm4xnPaMBGrxXeOp6sPEfOgL3CQ79rrF35R+jy/9LT0u4FVO9JZ5TxWlTwJIt4sJZ9eSKPl9YwXE2b/XJUJgOR49c9zYKSKOrOMaT/ZDuxzaftgFQ4xCJm9kE6pm6oScpwuqqc31SrVkQl8dQm5TznZiaio02BU3uhjvQIwK2Azv5+v9SpMPqAork5rFqHsuJcTtV6EQrPPUFmCWvK5MyYd5QK3HXlQUWpif1wCSLfDIEvp5wm47W71npWbNnr9xrZLA4u0giv3zXFb3iSN37CB1+Br0nc9CxiNh8V4XhckzdTgkaW3W16Vu3aK9nQfVkXwlR56VLgUThfca5TTRaLRwgvTVEHeAaw9aeUlo1Bdq1pUCYpqiw8CzhM63gpxYpHcraTEe2cMlSMmkwbpx2PDPARCe6SfL0A8LmK99E42NtjKSY1ggAF2EQM4BVdRf2EgkYIyqMEyh7vMBzrHbfm2oAAWd5z2dczytxwwwfC8UDThm2y0ECZYse/SF21O9YcGkphAtp7PuS8rkb3tti1vuTScL+PyVNwOPHvxXHvBGG26nqg2DeMICdvOBFv79/+d3v3Xy3e8N1xtu/El8Yc2AzzTtc7za5mkJioCnYykHS5mZJiRvS5Xp/vcSyUwfJXIri/3A3gAa79y+RqwdZ+1hLREolC1Z4VYqyG1Y78yki7ifPvdJMcEO/SQoJjGXsVft/ArFcAr5K1J3LLKeJe2S3px2bzYVQhfLo1T1A9eilWdgOY+KLbNe/KHV0NYbzUmIya1222MpGVKOBCh5wqS6dUQivsTWzTWufU+C9iHZ067i825YWm9vi2IHq1a94tzDO2gD0+0WwMqyj/XDngGFtVSqrxuYPBwA21tEvpwnMUPH5+2kNCPCmKK47PbMYcHJ8MxlclsTFjjneFVUZWnwZ4V1bTuuEJkuxTGd7VMLeQHiSLhnnFIFjZUgPFw9wpHnUqPhE2330BnHb8RzJwRubqlCRyoFN/+H9MwXW/o/D9/pWU/HB2Kqc9dwGmia84pcdpfRelFZeb+WN4Ga4Nfphgcs3f0r1fxywrqqnFNXq29xT5WBv1Ne/S4zaTlqtpNZvb8mQvWMan6Q+NcJLuh81M51T1sjVOs+qBufhQL436mUcz7oPXA3mLvIZuMFU57+dpJc1Y3A1QKs6hrDZ+d1klR/l/0fKyv/BT1rpZ1VSt01e3T0JSwYae3iXnx8wBW5gUTHjXl2Zzq38GzT7pUU0iV5RBrUMpMgjWWmzE/zxhpGbWaMLcn0HXYrTsceMThF24VOOvfGpOe34Y5FGiwFJxDDqyh1i+s96rAtB2IIjSc7hOjURyvEjci9VOECakXoee63sqeIlEzt7yVzx7eyMXDxsm+UKWPLjkeztgxG2FYUfpwdMTa5i8khq6YhG4pnU+uIZ+ct8/NkDgzkJT0pEBaToxGyzJ7LF/0FRh4bTLlGVqwmkHAGtdPrNBcbmzKZOVgQEJ4ze83cS99zIzhYTMxS/bmj2G7ytJspC5e1jG3bzcTtKuPlVLqdniyupVA8/kHPD4uhx+KjrH6nZ9raaHGj5//31EzPpLiEiQ7cCqcfNGazM7WuvFZK6+/UTOs/qDlFBPR6GQDYGdudA46dBtlZaUyafqLTgCfuTHOi+PQaPnAixQ6ZI2i39VwukkUBUWj7g4GcpPCqeDvuUN91yx/WRjb3V08mBTKGL3A9F44ZR8RSTvp+ov2756po5Cnd7WSlvuNmCOh5994hCyUcDBicw+ZU1JNm0KDPl+ehQvQgImwVozPK3Ffwo+fVE7wBhaQJqqrSvYeh0pMqskf8OMSh1NOn8mLOB/++u2/Fd8BJzTDP2g663GtKnCdExKBlRe8nAV8tiZhlh+XMQOC8MDlNjOyxh7OReigP0aOdkHcUSwoviBz/JmUclUY9nmPIHqUrhxEDVFguSXGVHqdURBEnsqBk4UQR/AU1Qz/v/NCPd4CfLwTvL53vr6+vp7lfX2X/AVYeJgI="
                        '        Dim paRes As String = mid2
                        '        '// use the same Order ID from the Verify-Enrolled

                        '        '// TODO populate a card object with the data returned by the ACS URL MD field
                        '        Dim sigVerification As Boolean
                        '        Try
                        '            sigVerification = card.VerifySignature(paRes, Nothing)
                        '        Catch ex As ApiException
                        '            lblErrorMsg.Text = ex.Message
                        '            lblErrorMsg.ForeColor = Drawing.Color.Red
                        '            lblErrorMsg.Style.Add("font-size", "medium")
                        '            lblErrorMsg.Visible = True
                        '            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                        '            Throw New Exception(ex.Message)
                        '        End Try

                        '        If sigVerification Then

                        '            Dim threeDsigVerificationDetails As ThreeDSecure = card.ThreeDSecure

                        '            '// if the signature verifcation was successful grab the 3D Secure data
                        '            Dim lEci As String = threeDsigVerificationDetails.Eci
                        '            Dim lxid As String = threeDsigVerificationDetails.Xid
                        '            Dim lcavv As String = threeDsigVerificationDetails.Cavv
                        '            Dim Status As String = threeDsigVerificationDetails.Status

                        '            'TODO determine Action basePd On Verify-Signature response, see further example
                        '            'Step-4
                        '            Dim threeDSecureInfo = New ThreeDSecure With {
                        '                .Cavv = lcavv,
                        '                .Xid = lxid,
                        '                .Eci = lEci
                        '                 }
                        '            card.ThreeDSecure = threeDSecureInfo
                        '            'Dim orderId = "ezJDQjhENTZBLTdCNzNDQw"

                        '            Try
                        '                Dim response2 = card.Charge(lAmount).WithCurrency("EUR").Execute()
                        '                Resposecode = response2.ResponseCode
                        '                If Not Resposecode = "00" Then
                        '                    str = str + "Card charge failed:"
                        '                End If
                        '            Catch exce As BuilderException
                        '                lblErrorMsg.Text = exce.Message
                        '                lblErrorMsg.ForeColor = Drawing.Color.Red
                        '                lblErrorMsg.Style.Add("font-size", "medium")
                        '                lblErrorMsg.Visible = True
                        '                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(exce)
                        '                Throw New Exception(exce.Message)
                        '            End Try
                        '        Else
                        '            IsVerified = False
                        '            str = str + "Verify Signature failed:"
                        '        End If
                        '    ElseIf card.ThreeDSecure.Enrolled = "U" Then
                        '        IsVerified = False
                        '        str = str + "Unable to Verify Enrolment:"
                        '    End If
                        'Catch ex As Exception
                        '    lblErrorMsg.Text = ex.Message
                        '    lblErrorMsg.ForeColor = Drawing.Color.Red
                        '    lblErrorMsg.Style.Add("font-size", "medium")
                        '    lblErrorMsg.Visible = True
                        '    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                        '    Throw New Exception(ex.Message)
                        'End Try
                        ''PSD2 ends here
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
                    'If IsVerified And Resposecode = "00" Then
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
                End If
            Else
                ' for firm pay retun true
                lOrderID = -1
            End If
            'Else
            '    lOrderID = -1
            'End If
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
                .SetValue("PersonID", AptifyEbusinessUser1.PersonID)
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
                                .SetValue("BillToPersonID", AptifyEbusinessUser1.PersonID)
                            End With
                        End If
                    Next
                    ' For Tax Payment 
                    If TaxAmount > 0 Then
                        With .SubTypes("PaymentLines").Add()
                            .SetValue("AppliesTo", "Tax")
                            .SetValue("OrderID", OrderID)
                            .SetValue("Amount", TaxAmount)
                            .SetValue("BillToPersonID", AptifyEbusinessUser1.PersonID)
                        End With
                    End If


                End If
                'Siddharth: Added to check if Firm is Paying for firm admin order cc payment fail issue.
                If bIsFirmPay Then
                    .SetValue("CompanyID", AptifyEbusinessUser1.CompanyID)
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
    ''PSD2 Starts
    'Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
    '    target = value
    '    Return value
    'End Function

    'Private Function ProcessReferenceTransaction(ByVal transData As RealexHelper.TransactionData) As RealexTransaction.Response
    '    Try
    '        Dim m_transType As String = "payer-new"
    '        Dim sRequestXML As String = Me.ToXML(transData, m_transType)
    '        Dim response As New RealexTransaction.Response()
    '        response = Me.SubmitTransaction(sRequestXML)

    '        If response.Result = "00" Then
    '            m_transType = "card-new"
    '            sRequestXML = Me.ToXML(transData, m_transType)
    '            response = Me.SubmitTransaction(sRequestXML)
    '        End If
    '        Return response
    '    Catch ex As Exception
    '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
    '    End Try
    'End Function

    'Protected Function SubmitTransaction(ByVal sRequestXML As String) As RealexTransaction.Response


    '    ServicePointManager.Expect100Continue = True
    '    ServicePointManager.SecurityProtocol = CType([Enum].Parse(GetType(SecurityProtocolType), sProtocolType), SecurityProtocolType)

    '    'Dim wReq As HttpWebRequest = DirectCast(WebRequest.Create("https://epage.payandshop.com/epage-remote-plugins.cgi"), HttpWebRequest)
    '    Dim wReq As HttpWebRequest = DirectCast(WebRequest.Create(sGatewayLocation), HttpWebRequest)
    '    wReq.ContentType = "text/xml"
    '    wReq.UserAgent = "Realex Payments RealVault"
    '    wReq.Timeout = 45 * 1000
    '    ' milliseconds
    '    wReq.AllowAutoRedirect = False
    '    'Siddharth: Changed below code get byte count for fada's
    '    'wReq.ContentLength = sRequestXML.Length        
    '    wReq.ContentLength = System.Text.ASCIIEncoding.UTF8.GetByteCount(sRequestXML)
    '    wReq.Method = "POST"

    '    Try
    '        Dim sReq As New StreamWriter(wReq.GetRequestStream())
    '        sReq.Write(sRequestXML)
    '        sReq.Flush()
    '        sReq.Close()

    '        Dim wResp As HttpWebResponse = DirectCast(wReq.GetResponse(), HttpWebResponse)
    '        Dim sResp As New StreamReader(wResp.GetResponseStream())
    '        Dim responseXML As [String] = sResp.ReadToEnd()
    '        Dim document As New XmlDocument()
    '        document.LoadXml(responseXML)
    '        sResp.Close()

    '        Dim sPasRef As String = ""
    '        Dim sTimeStamp As String = document.GetElementsByTagName("response").Item(0).Attributes.Item(0).InnerText
    '        Dim sResult As String = document.GetElementsByTagName("result").Item(0).InnerText
    '        Dim sMessage As String = document.GetElementsByTagName("message").Item(0).InnerText
    '        Dim sAuthCode As String = ""
    '        If document.GetElementsByTagName("authcode").Count > 0 Then
    '            sAuthCode = document.GetElementsByTagName("authcode").Item(0).InnerText
    '        End If
    '        If document.GetElementsByTagName("pasref").Count > 0 Then
    '            sPasRef = document.GetElementsByTagName("pasref").Item(0).InnerText
    '        End If
    '        'Added below code because for real vault service that is for reference transaction we do not get authcode in response
    '        If sAuthCode = "" Then
    '            sAuthCode = sResult
    '        End If

    '        Dim response As New RealexTransaction.Response()
    '        response.TimeStamp = sTimeStamp
    '        response.Result = sResult
    '        response.Message = sMessage
    '        response.PasRef = sPasRef
    '        response.AuthCode = sAuthCode
    '        Return response

    '    Catch ex As Exception
    '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
    '        Return Nothing
    '    End Try
    'End Function
    'Protected Function ToXML(ByVal transData As RealexHelper.TransactionData, ByVal sRequestType As String, Optional ByVal bIsCapture As Boolean = True) As String
    '    Dim sGatewayLocation As String = ""
    '    Dim sAccountName As String = ""
    '    Dim sMerchantID As String = ""
    '    Dim sSharedSecret As String = ""



    '    Dim m_transTimestamp = DateTime.Now.ToString("yyyyMMddHHmmss")
    '    Dim m_transMerchantName = "icaitest"
    '    m_normalPassword = "secret"

    '    Dim xmlSettings As New XmlWriterSettings()
    '    xmlSettings.Indent = True
    '    xmlSettings.NewLineOnAttributes = False
    '    xmlSettings.NewLineChars = vbCr & vbLf
    '    xmlSettings.CloseOutput = True

    '    Dim strBuilder As New StringBuilder()
    '    Dim xml As XmlWriter = XmlWriter.Create(strBuilder, xmlSettings)
    '    xml.WriteStartDocument()
    '    xml.WriteStartElement("request")
    '    If True Then
    '        xml.WriteAttributeString("timestamp", m_transTimestamp)
    '        xml.WriteAttributeString("type", "payer-new")

    '        xml.WriteElementString("merchantid", "icaitest")
    '        xml.WriteElementString("orderid", transData.OrderId)
    '        Dim sSHA1Hash As String = ""
    '        Select Case sRequestType
    '            Case "payer-new"
    '                'payer tag
    '                xml.WriteStartElement("payer")
    '                xml.WriteAttributeString("ref", m_payerRef)
    '                xml.WriteAttributeString("type", "Business")
    '                xml.WriteElementString("firstname", m_payerFName)
    '                xml.WriteElementString("surname", AptifyEbusinessUser1.LastName)
    '                xml.WriteEndElement()
    '                sSHA1Hash = m_transTimestamp & "." & m_transMerchantName & "." & transData.OrderId & "..." & m_payerRef
    '            Case "card-new"
    '                xml.WriteStartElement("card")
    '                xml.WriteElementString("ref", m_cardRef)
    '                xml.WriteElementString("payerref", m_payerRef)
    '                xml.WriteElementString("number", transData.CreditCard.CreditCardNumber)
    '                xml.WriteElementString("expdate", transData.CreditCard.ExpiresDate)
    '                xml.WriteElementString("chname", m_payerFName & " " & m_payerSName)
    '                xml.WriteElementString("type", transData.CreditCard.CardType)
    '                xml.WriteEndElement()
    '                sSHA1Hash = m_transTimestamp & "." & m_transMerchantName & "." & transData.OrderId & "..." & m_payerRef & "." & m_payerFName & " " & m_payerSName & "." & transData.CreditCard.CreditCardNumber
    '            Case "receipt-in"
    '                xml.WriteElementString("account", sAccountName)
    '                xml.WriteStartElement("autosettle")
    '                If bIsCapture Then
    '                    xml.WriteAttributeString("flag", "1")
    '                Else
    '                    xml.WriteAttributeString("flag", "0")
    '                End If
    '                xml.WriteEndElement()
    '                'If CSC is required then pass in XML
    '                If transData.CreditCard.CSC.Length > 0 Then
    '                    xml.WriteStartElement("paymentdata")
    '                    xml.WriteStartElement("cvn")
    '                    xml.WriteElementString("number", transData.CreditCard.CSC)
    '                    xml.WriteEndElement()
    '                    xml.WriteEndElement()
    '                End If
    '                xml.WriteStartElement("amount")
    '                xml.WriteAttributeString("currency", transData.Currency.Trim())
    '                xml.WriteValue(transData.TotalAmount)
    '                xml.WriteEndElement()
    '                xml.WriteElementString("payerref", m_payerRef)
    '                xml.WriteElementString("paymentmethod", m_cardRef)
    '                sSHA1Hash = m_transTimestamp & "." & m_transMerchantName & "." & transData.OrderId & "." & transData.TotalAmount & "." & transData.Currency.Trim() & "." & m_payerRef
    '            Case "authorization"
    '                xml.WriteElementString("account", sAccountName)
    '                xml.WriteStartElement("amount")
    '                xml.WriteAttributeString("currency", transData.Currency.Trim())
    '                xml.WriteValue(transData.TotalAmount)
    '                xml.WriteEndElement()
    '                xml.WriteStartElement("card")
    '                xml.WriteElementString("number", transData.CreditCard.CreditCardNumber)
    '                xml.WriteElementString("expdate", transData.CreditCard.ExpiresDate)
    '                xml.WriteElementString("chname", transData.CreditCard.CardHolderName)
    '                xml.WriteElementString("type", transData.CreditCard.CardType)
    '                'If CSC is required then pass in XML
    '                If transData.CreditCard.CSC.Length > 0 Then
    '                    xml.WriteStartElement("cvn")
    '                    xml.WriteElementString("number", transData.CreditCard.CSC)
    '                    xml.WriteEndElement()
    '                End If
    '                xml.WriteEndElement()
    '                xml.WriteStartElement("autosettle")
    '                If bIsCapture Then
    '                    xml.WriteAttributeString("flag", "1")
    '                Else
    '                    xml.WriteAttributeString("flag", "0")
    '                End If
    '                xml.WriteEndElement()
    '                sSHA1Hash = m_transTimestamp & "." & m_transMerchantName & "." & transData.OrderId & "." & transData.TotalAmount & "." & transData.Currency.Trim() & "." & transData.CreditCard.CreditCardNumber
    '        End Select

    '        generateSHA1Hash(sSHA1Hash)
    '        xml.WriteElementString("sha1hash", m_transSHA1Hash)
    '    End If

    '    xml.WriteEndElement()
    '    xml.Flush()
    '    xml.Close()
    '    Return (strBuilder.ToString())
    'End Function
    'Private Sub generateSHA1Hash(ByVal hashInput As String)
    '    Try
    '        Dim sha As SHA1 = New SHA1Managed()
    '        Dim hashStage1 As [String] = Convert.ToString(hexEncode(sha.ComputeHash(Encoding.UTF8.GetBytes(hashInput)))) & "." & m_normalPassword
    '        Dim hashStage2 As [String] = hexEncode(sha.ComputeHash(Encoding.UTF8.GetBytes(hashStage1)))
    '        m_transSHA1Hash = hashStage2
    '    Catch ex As Exception
    '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
    '    End Try
    'End Sub
    'Private Function hexEncode(ByVal data As Byte()) As String
    '    Dim result As [String] = ""
    '    For Each b As Byte In data
    '        result += b.ToString("X2")
    '    Next
    '    result = result.ToLower()
    '    Return (result)
    'End Function
    ''PSD2 Ends
End Class
