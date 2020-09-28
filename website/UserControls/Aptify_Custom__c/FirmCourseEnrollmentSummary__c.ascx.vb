Option Explicit On
Option Strict On
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' 
'Developer                  Date modified               Comments 
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Milind Sutar               15/04/2015                  To show course enrollment summary 
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Data
Imports System.Collections.Generic
Imports Telerik.Web.UI
Imports System.Linq
Imports System.Drawing
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Telerik.Web.UI.PivotGrid
Imports Telerik.Web.UI.PivotGrid.Core
Imports System.Windows.Threading
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Partial Class FirmCourseEnrollmentSummary__c
    Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

    Private Shared _courseEnrollmentSummary As New DataTable()
    Private _orderLines As New DataTable()
    Private _isFirmRTO As Integer
    Private _academicCycleId As Integer

    Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "FirmCourseEnrollmentSummary__c"
    Protected Const ATTRIBUTE_COURSE_ENROLLMENT_PAGE_URL As String = "FirmCourseEnrollmentURL__c"
    'BFP Performance
    Protected Const ATTRIBUTE_ENROLMENT_LANDING_PAGE_URL As String = "FirmCourseEnrollmentLandingURL__c"

    ''' <summary>
    ''' Get sets manage my groups page url
    ''' </summary>
    Public Overridable Property CourseEnrollmentURL() As String
        Get
            If Not ViewState(ATTRIBUTE_COURSE_ENROLLMENT_PAGE_URL) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_COURSE_ENROLLMENT_PAGE_URL))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_COURSE_ENROLLMENT_PAGE_URL) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    ''' <summary>
    ''' To get set academic cycyle id
    ''' </summary>
    Public Property AcademicCycleID() As Integer
        Get
            _academicCycleId = CInt(Request.QueryString("AcademicYear"))
            Return _academicCycleId
        End Get
        Set(ByVal value As Integer)
            _academicCycleId = value
        End Set
    End Property

    Private _webRemittanceNumber As String
    Public Property WebRemittanceNumber() As String
        Get
            _webRemittanceNumber = CStr(ViewState("WebRemittanceNumber"))
            Return _webRemittanceNumber
        End Get
        Set(ByVal value As String)
            _webRemittanceNumber = value
            ViewState("WebRemittanceNumber") = _webRemittanceNumber
        End Set
    End Property

    'BFP Performance: Addded to redirect to landing page
    ''' <summary>
    ''' Get sets manage my groups page url
    ''' </summary>
    Public Overridable Property FirmCourseEnrollmentLandingURL() As String
        Get
            If Not ViewState(ATTRIBUTE_ENROLMENT_LANDING_PAGE_URL) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_ENROLMENT_LANDING_PAGE_URL))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_ENROLMENT_LANDING_PAGE_URL) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    ''' <summary>
    ''' Handles page load event
    ''' </summary>
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ''Commented By Pradip 2015-07-08 For Below Requirement by siddharth RE: CAI: Big firm page changes.
            'Web Remittance Number label and value will only appear on submit and require some text as follows: “Please note this Web Remittance Number and submit with payment”. 
            'LoadWebRemittanceNumber()
            'LoadStudentCourseEnrollmentSummary()                 
            gvFirmCourseEnrollmentSummary.Rebind()
            SetProperties()
            lblMessage.Text = String.Empty
        End If
    End Sub

    ''' <summary>
    ''' Set Properties
    ''' </summary>
    Protected Overrides Sub SetProperties()
        Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
        MyBase.SetProperties()
        If String.IsNullOrEmpty(Me.CourseEnrollmentURL) Then
            Me.CourseEnrollmentURL = Me.GetLinkValueFromXML(ATTRIBUTE_COURSE_ENROLLMENT_PAGE_URL)
        End If
        'BFP Performance
        If String.IsNullOrEmpty(Me.FirmCourseEnrollmentLandingURL) Then
            Me.FirmCourseEnrollmentLandingURL = Me.GetLinkValueFromXML(ATTRIBUTE_ENROLMENT_LANDING_PAGE_URL)
        End If
    End Sub

    ''' <summary>
    ''' Handles pivot grid cell data bound
    ''' </summary>
    Protected Sub gvFirmCourseEnrollmentSummary_CellDataBound(sender As Object, e As Telerik.Web.UI.PivotGridCellDataBoundEventArgs) Handles gvFirmCourseEnrollmentSummary.CellDataBound

        'If gvFirmCourseEnrollmentSummary.CurrentPageIndex = gvFirmCourseEnrollmentSummary.PageCount - 1 Then

        '    gvFirmCourseEnrollmentSummary.TotalsSettings.RowGrandTotalsPosition = TotalsPosition.Last

        '    ' gvFirmCourseEnrollmentSummary.TotalsSettings.GrandTotalsVisibility = PivotGridGrandTotalsVisibility.RowsAndColumns

        '    'gvFirmCourseEnrollmentSummary.RowGrandTotalCellStyle.ForeColor = Color.Red
        'Else
        '    gvFirmCourseEnrollmentSummary.TotalsSettings.RowGrandTotalsPosition = TotalsPosition.None
        '    'gvFirmCourseEnrollmentSummary.TotalsSettings.GrandTotalsVisibility = PivotGridGrandTotalsVisibility.ColumnsOnly
        '    'gvFirmCourseEnrollmentSummary.RowGrandTotalCellStyle.ForeColor = Color.Blue
        'End If

        'If TypeOf e.Cell.CellType Is PivotGridDataCellType.ColumnTotalDataCell OrElse e.Cell.CellType = PivotGridDataCellType.RowTotalDataCell Then

        'End If



       ' If e.Cell.Text.Contains() Then



        ' Dim cell As PivotGridDataCell = TryCast(e.Cell, PivotGridDataCell)

        'Dim parentColIndex As String = cell.ParentColumnIndexes(0).ToString()

        'Dim parentrolIndex As String = cell.ParentRowIndexes(0).ToString()

        'If cell.CellType = PivotGridDataCellType.RowTotalDataCell And Not cell.Text.Contains(parentrolIndex) Then
        '    cell.BackColor = Color.FromArgb(220, 240, 255)
        '    cell.Text = ""
        '    'cell.Style.Add("display", "None")
        '    cell.Attributes.Add("Style", "display:None")
        '    Dim i As Integer = cell.RowIndex
        'End If


        'If cell.CellType = PivotGridDataCellType.RowTotalDataCell Then
        '    cell.BackColor = Color.Gray
        '    'cell.Attributes.Add("Style", "display:None")
        '    Dim myStyle As Style = New Style


        'End If


        'If cell.CellType = PivotGridDataCellType.RowAndColumnTotal Then
        '    cell.BackColor = Color.Blue
        'End If


        'If cell.Text.Contains(parentrolIndex) Then
        '    cell.BackColor = Color.Yellow
        'End If

        'If cell.CellType = PivotGridDataCellType.ColumnGrandTotalRowTotal And cell.Text.Contains(parentrolIndex) Then
        '    cell.BackColor = Color.Yellow
        '    ' cell.Attributes.Add("Style", "display:None")
        'End If

        'If cell.CellType = PivotGridDataCellType.RowTotalDataCell Then
        '    cell.BackColor = Color.FromArgb(220, 240, 255)
        '    cell.Text = ""
        '    cell.Attributes.Add("Style", "display:None")
        'End If



        'If cell.CellType = PivotGridDataCellType.ColumnTotalDataCell Then
        '    cell.BackColor = Color.FromArgb(220, 240, 255)
        'End If



        'If cell.Text = parentrolIndex + " Total" Then
        '    e.Cell.BackColor = Color.FromArgb(220, 240, 255)
        'End If

        ' Dim p1 As String = TryCast(cell.Field, PivotGridRowField).DataField

        'If cell.Text.Contains("Total") Then
        '    e.Cell.BackColor = Color.FromArgb(220, 240, 255)
        'End If


        'If cell.CellType = PivotGridDataCellType.RowTotalDataCell Then
        '    cell.BackColor = Color.FromArgb(220, 240, 255)
        '    Dim p As String = TryCast(cell.Field, PivotGridAggregateField).DataField
        'End If

        'If cell.CellType = PivotGridDataCellType.ColumnGrandTotalDataCell Then
        '    e.Cell.BackColor = Color.Red
        'End If

        'If cell.CellType = PivotGridDataCellType.RowGrandTotalDataCell Then
        '    e.Cell.BackColor = Color.Green
        'End If

        'If e.Cell.Text.Contains("Total") Then
        '    'e.Cell.BackColor = Color.FromArgb(220, 240, 255)
        '    e.Cell.Text = ""
        '    Dim s As String = e.Cell.Field.FieldType

        'End If
        'End If

        If TypeOf e.Cell Is PivotGridRowHeaderCell Then
            If e.Cell.Controls.Count > 0 Then
                If e.Cell.Controls(0) IsNot Nothing Then
                    TryCast(e.Cell.Controls(0), Button).Visible = False
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles course enrollment need data source to bind data 
    ''' </summary>
    Protected Sub gvFirmCourseEnrollmentSummary_NeedDataSource(sender As Object, e As Telerik.Web.UI.PivotGridNeedDataSourceEventArgs) Handles gvFirmCourseEnrollmentSummary.NeedDataSource
        LoadStudentCourseEnrollmentSummary()
        If _courseEnrollmentSummary.Rows.Count > 0 Then
            Dim DistinctDt As DataTable = _courseEnrollmentSummary.DefaultView.ToTable(True, "Curriculum")
            If DistinctDt.Rows.Count = _courseEnrollmentSummary.Rows.Count Then
                gvFirmCourseEnrollmentSummary.ColumnGroupsDefaultExpanded = True
            End If

 Dim query = _courseEnrollmentSummary.AsEnumerable().Select(Function(p) p.Field(Of String)("StudentNumber")).Distinct().ToList()
            If query.Count > 25 Then
                gvFirmCourseEnrollmentSummary.ClientSettings.Scrolling.AllowVerticalScroll = True
                Try
                    gvFirmCourseEnrollmentSummary.ClientSettings.Scrolling.ScrollHeight = 500
                Catch ex As Exception
                    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                End Try
            Else
                gvFirmCourseEnrollmentSummary.ClientSettings.Scrolling.AllowVerticalScroll = False
                gvFirmCourseEnrollmentSummary.ClientSettings.Scrolling.ScrollHeight = 0
            End If

            gvFirmCourseEnrollmentSummary.Visible = True
            gvFirmCourseEnrollmentSummary.DataSource = _courseEnrollmentSummary
            Dim sCurrency As String = AptifyApplication.GetEntityRecordName("Currency Types", loggedInUser.PreferredCurrencyTypeID)
            lblCurrency.Text = String.Format(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                Convert.ToInt32(Me.AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.BigFirm.FirmCurrencyMessage__c")), _
                Convert.ToInt32(Me.DataAction.UserCredentials.CultureID), Me.DataAction.UserCredentials), sCurrency)
        Else
            gvFirmCourseEnrollmentSummary.DataSource = Nothing
            gvFirmCourseEnrollmentSummary.Visible = False
            lblWebRemittaneNumber.Visible = False
            lblCurrency.Text = ""
            lblNoRecords.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                Convert.ToInt32(Me.AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.PivotGrid.NoRecords")), _
                Convert.ToInt32(Me.DataAction.UserCredentials.CultureID), Me.DataAction.UserCredentials)
        End If
    End Sub

    ''' <summary>
    ''' Handles page pre-render event to set default setting
    ''' </summary>
    Protected Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        If Not IsPostBack Then
            LoadDefaultSetting()
        End If
ScriptManager.RegisterStartupScript(Page, GetType(Page), "script", "$(function () { HideCells(); });", True)
    End Sub

    ''' <summary>
    ''' Handles generate button click
    ''' </summary>
    Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
        ''Added By Pradip 2015-07-08 Web Remittance Number label and value will only appear on submit and require some text as follows: “Please note this Web Remittance Number and submit with payment”. 
        LoadWebRemittanceNumber()
        AttachWebRemittanceNumber()
        ''Added BY Pradip 2016-04-11 for UAT Tracker Item G3-P 30
        btnSubmit.Enabled = False
        btnSubmit.BackColor = System.Drawing.Color.Gray
btnSubmit.visible = false
    End Sub

    ''' <summary>
    ''' Handles back button click to redirect to the big firm page
    ''' </summary>
    Protected Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        ' Response.Redirect(Me.CourseEnrollmentURL)
        ''Added BY Pradip 2015-07-27 
        Dim eventName As String = ""
        If lblWebRemittaneNumber.Text = "" Then
            eventName = "Back"
            'Response.Redirect(Me.CourseEnrollmentURL + "?EventName=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(eventName)))
            Dim events As String = System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(eventName))
            Dim url As New StringBuilder()
            url.AppendFormat("{0}?AcademicYear={1}&EnrollMentType={2}&RouteOfEntry={3}&CurrentStage={4}&FilterBy={5}&SubjectFailed={6}&EventName={7}&Loc={8}", Me.CourseEnrollmentURL, Request.QueryString("AcademicYear").ToString(), Request.QueryString("EnrollMentType").ToString(), Request.QueryString("RouteOfEntry").ToString(), Request.QueryString("CurrentStage").ToString(), Request.QueryString("FilterBy").ToString(), Request.QueryString("SubjectFailed").ToString(), events, Request.QueryString("Loc").ToString())
            Response.Redirect(url.ToString())
        Else
            'BFP Performance: As now we have disabled current stage drop downs from enrolment page, after generating web remittance number redirect back to landing page
            Response.Redirect(Me.FirmCourseEnrollmentLandingURL)
        End If

        ' Response.Redirect(HttpContext.Current.Request.Url.ToString() + "?FormalRegID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(hidFormalID.Value.ToString())), False)

    End Sub

    Private Sub AttachWebRemittanceNumber()
        Try
            lblMessage.Text = String.Empty
            Dim errorMessage As String = String.Empty
            'BFP Performance: Initialize result to false
            Dim result As Boolean = False
            If IsFirmRTO() Then
                LoadOrderLines()
                For Each row As DataRow In _orderLines.Rows
                    Dim orderLinesIds As String() = row("OrderLines").ToString.Split(CChar(","))
                    Dim orderEntity As Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase = _
                                      Me.AptifyApplication.GetEntityObject("Orders", CInt(row("OrderID")))
                    For Each orderDetailId As Integer In orderLinesIds
                        Dim oOrderLine As Aptify.Applications.OrderEntry.OrderLinesEntity
                        oOrderLine = CType(orderEntity.SubTypes("OrderLines").Find("ID", orderDetailId), Aptify.Applications.OrderEntry.OrderLinesEntity)
                        With oOrderLine
                            .SetValue("WebRemittanceNo__c", Me.WebRemittanceNumber)
                        End With
                    Next
                    result = orderEntity.Save(errorMessage)
                Next
            Else
                'BFP Performance: Commented below loop code and now calling a SP which will update staging table
                'LoadStudentCourseEnrollmentSummary()
                'For Each row As DataRow In _courseEnrollmentSummary.Rows
                '    Dim enrollmentStaging As Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase = _
                '                      Me.AptifyApplication.GetEntityObject("EnrollmentStaging__c", CInt(row("RecordID")))
                '    enrollmentStaging.SetValue("OrderStatus", "Enroll")
                '    enrollmentStaging.SetValue("WebRemittanceNumber", Me.WebRemittanceNumber)
                '    enrollmentStaging.SetValue("IsSelected", 0)
                '    result = enrollmentStaging.Save(errorMessage)
                'Next spUpdateStudentCourseEnrollmentWebRem__c
                Dim sSQL As String = Me.Database + "..spUpdateStudentCourseEnrollmentWebRem__c"
                Dim oParams(3) As IDataParameter
                oParams(0) = DataAction.GetDataParameter("@CompanyID", SqlDbType.Int, loggedInUser.CompanyID)
                oParams(1) = DataAction.GetDataParameter("@AcademicCycleID", SqlDbType.Int, Me.AcademicCycleID)
                oParams(2) = DataAction.GetDataParameter("@WebRemittanceNumber", SqlDbType.VarChar, Me.WebRemittanceNumber)
                'Added by Govind for redmine log #19936
                oParams(3) = DataAction.GetDataParameter("@BillToPersonID", SqlDbType.Int, loggedInUser.PersonID)
                Dim iUpdateCount As Integer = DataAction.ExecuteNonQueryParametrized(sSQL, CommandType.StoredProcedure, oParams)
                If iUpdateCount > 0 Then
                    result = True
                End If
            End If
            If result Then
                btnSubmit.Enabled = False
                lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                    Convert.ToInt32(Me.AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.FirmCourseEnrollmentSummary.Success")), _
                    Convert.ToInt32(Me.DataAction.UserCredentials.CultureID), Me.DataAction.UserCredentials)
                lblMessage.ForeColor = Color.Green
            End If
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' To load order/order lines to attach web remittance number
    ''' </summary>
    Private Sub LoadOrderLines()
        Try
            lblNoRecords.Text = String.Empty
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0} ..spGetCourseEnrollmentOrderLinesForRTO__c @CompanyID={1}", _
                             Me.Database, loggedInUser.CompanyID.ToString())
            _orderLines = Me.DataAction.GetDataTable(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load student course enrollment data
    ''' </summary>
    Private Sub LoadStudentCourseEnrollmentSummary()
        Try
            lblNoRecords.Text = String.Empty
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0} ..spGetStudentCourseEnrollmentSummary__c @AcademicCycleID={1},@CompanyID={2}", _
                             Me.Database, Me.AcademicCycleID, loggedInUser.CompanyID.ToString())
            _courseEnrollmentSummary = Me.DataAction.GetDataTable(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Private Sub LoadWebRemittanceNumber()
        Try
            Dim sql As New StringBuilder()
            Dim webRemittancNumber As New StringBuilder()
            sql.AppendFormat("{0} ..spGetUniqueWebRemittance__c @UserID={1}", _
                                Me.Database, loggedInUser.UserID.ToString())
            Dim number As String = CStr(Me.DataAction.ExecuteScalar(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
            Me.WebRemittanceNumber = webRemittancNumber.AppendFormat("WEB{0}", number).ToString()
            webRemittancNumber.AppendFormat("Web Remitttance Number :{0}", Me.WebRemittanceNumber)
            lblWebRemittaneNumber.Text = "Web Remittance Number : " + Me.WebRemittanceNumber
            ''Added BY Pradip 2015-07-08
            lblWebRemittaneNumber.Visible = True
            lblWebRemittaneNumberText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.BigFirm.RemittanceNumberText__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            lblWebRemittaneNumberText.Visible = True
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' to do the default settings on reload
    ''' </summary>
    Private Sub LoadDefaultSetting()
        Try
            If _courseEnrollmentSummary.Rows.Count > 0 Then
                gvFirmCourseEnrollmentSummary.ColumnGroupsDefaultExpanded = True
                Dim curriculumTable As DataTable = _courseEnrollmentSummary.DefaultView.ToTable(True, "Curriculum")
                Dim curriculums As New List(Of String)()
                Dim cout As Integer = 0
                For Each row As DataRow In curriculumTable.Rows
                    If cout <> 0 Then
                        gvFirmCourseEnrollmentSummary.CollapsedColumnIndexes.Add(New String() {row("Curriculum").ToString()})
                    End If
                    cout = cout + 1
                Next
            End If
            gvFirmCourseEnrollmentSummary.Rebind()
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' To check if firm is RTO
    ''' </summary>
    Private Function IsFirmRTO() As Boolean
        Try
            Dim result As Boolean = False
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0} ..spCommonCheckCompanyRTO__c @CompanyID={1}", _
                             Me.Database, loggedInUser.CompanyID.ToString())
            result = CBool(Me.DataAction.ExecuteScalar(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
            Return result
        Catch ex As Exception
            Return True
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Function
End Class
