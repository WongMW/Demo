
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Pradip Chavhan             29/10/2015                         For LLL Qualification Status Page
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Data
Imports System.Collections.Generic
Imports Telerik.Web.UI
Imports System.Linq
Imports System.Drawing
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports System
Imports System.Reflection
Imports Aptify.Framework.DataServices

Partial Class LLLQualificationStatus__c
    Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

    Private dtcourseDetails As DataTable
    Private dtHearderDetails As DataTable
    Private classRegId As Integer
    Private lstudentId As Integer
    Private classRegPartStatusId As Integer
    Private dtAttendanceStatus As DataTable
    Private dtAssignmentStatus As DataTable
    Private dtClassRegAllStatus As DataTable
    Private dtExamStatus As DataTable
    Private dtQualStatus As DataTable
    Private dtAttachement As DataTable
    Private CRclassId As Integer = 0
    Private AssignmentRowCount As Integer = 0
    'Private PerOverAllScore As Double = 0
    ' Private OverAllPerTotal As Double = 0
    'Private PerTotalWeighting As Double = 0

    Private OverallAssignPer As Double = 0
    Private OverallAssignWeightPer As Double = 0

    Private OverallQualPer As Double = 0
    Private OverallQaulWeightPer As Double = 0
    Protected bFailed As Boolean = False
    Dim sSQL As String = String.Empty
    Dim bExamTopScorer As Boolean = False
    Private courseName As String
    Private SubjectTopScorer__c As String = String.Empty
    Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "LLLQualificationStatus__c"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_PostEnrollReqPage = "PostEnrollReqPage"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_AssignmentDetailsPage = "AssignmentDetailsPage"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_AppealAppPage = "AppealAppPage"
    Protected Const ATTRIBUTE_QualificationStatus_URL As String = "QualificationStatusPage"
    Protected Const ATTRIBUTE_SECURITYERROR_PAGE As String = "securityErrorPage"
    Protected Const ATTRIBUTE_ViewCart_PAGE As String = "ViewCartPage"

#Region "Page Property"


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

    Public Property ClassRegistrationID() As Integer
        Get
            Dim CRId As String = Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("CRID"))
            classRegId = Convert.ToInt32(CRId)
            Return classRegId
        End Get
        Set(ByVal value As Integer)
            classRegId = value
            hfClassRegId.Value = Convert.ToString(classRegId)
        End Set
    End Property
    Public Property StudentID() As Integer
        Get
            Return lstudentId
        End Get
        Set(ByVal value As Integer)
            lstudentId = value
        End Set
    End Property

    Public ReadOnly Property ClassID() As Integer
        Get
            Dim CRclassId As String = Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("ClassID"))
            If Not String.IsNullOrEmpty(CRclassId) Then
                CRclassId = Convert.ToInt32(CRclassId)
            End If
            Return CRclassId
        End Get
    End Property

    Public Property PartStatusID() As Integer
        Get
            Return Convert.ToInt32(hfPartStatusID.Value)
        End Get
        Set(ByVal value As Integer)
            hfPartStatusID.Value = value
        End Set
    End Property

    ''' <summary>
    ''' Get sets manage my groups page url
    ''' </summary>
    Public Overridable Property PostEnrollReqPage() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_PostEnrollReqPage) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_PostEnrollReqPage))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_PostEnrollReqPage) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    Public Overridable Property AssignmentDetailsPage() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_AssignmentDetailsPage) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_AssignmentDetailsPage))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_AssignmentDetailsPage) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    Public Overridable Property AppealAppPage() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_AppealAppPage) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_AppealAppPage))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_AppealAppPage) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    Public Overridable Property QualificationStatusPage() As String
        Get
            If Not ViewState(ATTRIBUTE_QualificationStatus_URL) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_QualificationStatus_URL))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_QualificationStatus_URL) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property
    Public Overridable ReadOnly Property SecurityErrorPage() As String
        Get
            If Not ViewState(ATTRIBUTE_SECURITYERROR_PAGE) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_SECURITYERROR_PAGE))
            Else
                Dim value As String = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings("SecurityErrorPageURL"))
                If Not String.IsNullOrEmpty(value) Then
                    ViewState(ATTRIBUTE_SECURITYERROR_PAGE) = value
                    Return value
                Else
                    Return String.Empty
                End If
            End If
        End Get
    End Property


    Public Overridable Property ViewCartPage() As String
        Get
            If Not ViewState(ATTRIBUTE_ViewCart_PAGE) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_ViewCart_PAGE))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_ViewCart_PAGE) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    ''' <summary>
    ''' To set the default properties
    ''' </summary>
    Protected Overrides Sub SetProperties()
        Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
        MyBase.SetProperties()
        If String.IsNullOrEmpty(LoginPage) Then
            LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage)
        End If
        If String.IsNullOrEmpty(Me.PostEnrollReqPage) Then
            Me.PostEnrollReqPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_PostEnrollReqPage)
        End If
        If String.IsNullOrEmpty(Me.AssignmentDetailsPage) Then
            Me.AssignmentDetailsPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_AssignmentDetailsPage)
        End If
        If String.IsNullOrEmpty(Me.AppealAppPage) Then
            Me.AppealAppPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_AppealAppPage)
        End If

        If String.IsNullOrEmpty(QualificationStatusPage) Then
            QualificationStatusPage = Me.GetLinkValueFromXML(ATTRIBUTE_QualificationStatus_URL)
        End If

        If String.IsNullOrEmpty(ViewCartPage) Then
            ViewCartPage = Me.GetLinkValueFromXML(ATTRIBUTE_ViewCart_PAGE)
        End If
    End Sub
#End Region

#Region "Page Event"
    ''' <summary>
    ''' Handles page load event
    ''' </summary>
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        SetProperties()
        If LoggedInUser.PersonID < 0 Then
            Session("ReturnToPage") = QualificationStatusPage
            'Request.RawUrl
            Response.Redirect(LoginPage)
        Else
            If CheckDisplayQualStatLink() = False Then
                Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("virtualdir") & SecurityErrorPage & "?Message=Access to this Person is unauthorized.")
            End If
        End If
       
        If Not IsPostBack Then
            Me.StudentID = LoggedInUser.PersonID
            ''Query String Value is used to check that user click on Griview or load page first time 
            If Request.QueryString("CRID") Is Nothing Then
                pnlData.Visible = True
                pnlDetails.Visible = False
                LoadGrid()
            Else
                btnBack.Visible = True
                pnlData.Visible = False
                pnlDetails.Visible = True
                LoadHeaderDetails()
                LoadStudentCourseDetails()
                LoadExamStatus()
                LoadAssignmentStatus()
                LoadAttendanceStatus()
                Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
                oOrder = ShoppingCart1.GetOrderObject(Page.Session, Page.User, Page.Application)
                If oOrder.SubTypes("OrderLines").Count > 0 Then
                    btnSubmitPay.Visible = True
                End If
                'Dim myt As TestPC = New TestPC
                'myt.MyProperty = 101
                'Dim myPropInfo As PropertyInfo = myt.GetProperyInfo("MyProperty")
                'Dim n As String = myPropInfo.Name
                'Dim na As Int32 = CType(myPropInfo.GetValue(myt, Nothing), Integer)
            End If
        End If
    End Sub
#End Region

#Region "Grid Event"
    Protected Sub gvAttendanceStatus_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvAttendanceStatus.NeedDataSource
        Try
            LoadAttendanceStatus()
            If dtAttendanceStatus IsNot Nothing Then
                gvAttendanceStatus.DataSource = dtAttendanceStatus
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub gvCourseDetails_DataBound(sender As Object, e As System.EventArgs) Handles gvCourseDetails.DataBound
        Try
            For Each item As Telerik.Web.UI.GridDataItem In gvCourseDetails.MasterTableView.Items
                Dim hidIsExam As HiddenField = DirectCast(item.FindControl("hidIsExam"), HiddenField)
                Dim hidIsAssignment As HiddenField = DirectCast(item.FindControl("hidIsAssignment"), HiddenField)
                Dim lnkGvDownload As LinkButton = DirectCast(item.FindControl("lnkGvDownload"), LinkButton)
                Dim lnkAssignment As LinkButton = DirectCast(item.FindControl("lnkAssignment"), LinkButton)
                Dim lnkAdd As LinkButton = DirectCast(item.FindControl("lnkAdd"), LinkButton)
                If hidIsExam.Value = True Then
                    lnkGvDownload.Visible = False
                    lnkAssignment.Visible = False
                    lnkAdd.Visible = False
                End If
                If hidIsAssignment.Value = True Then
                    lnkGvDownload.Visible = False
                    lnkAdd.Visible = False
                End If
            Next
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' To handle link button events to open the specific popups
    ''' </summary>
    Protected Sub gvCourseDetails_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles gvCourseDetails.ItemCommand
        Try
            lblMessage.Text = ""
            lblAddNoteMessage.Text = ""
            If e.CommandName = "AddNotes" Then
                Dim sql As New StringBuilder()
                hfPartStatusID.Value = Convert.ToString(e.CommandArgument)
                sql.AppendFormat("{0}..spGetStudentCourseNotes__c @ClassRegPartStatusID={1}", _
                                 Me.Database, Convert.ToString(e.CommandArgument))
                txtNotes.Text = Me.DataAction.ExecuteScalar(Convert.ToString(sql))
                radAddNotes.VisibleOnPageLoad = True
            End If
            If e.CommandName = "Download" Then
                radDownloadDocuments.VisibleOnPageLoad = True
                Dim data As String() = Convert.ToString(e.CommandArgument).Split(CChar(";"))
                Dim recordID As Integer = Convert.ToInt32(data(0))
                Dim entityID As Integer = Convert.ToInt32(Me.AptifyApplication.GetEntityID("Course Parts"))
                ucDownload.AttachmentCategory = Me.AptifyApplication.GetEntityRecordIDFromRecordName("Attachment Categories", "Course Material")
                ucDownload.LoadAttachments(entityID, recordID, True)
            End If
            If e.CommandName = "Assignment" Then
                Dim data As String() = Convert.ToString(e.CommandArgument).Split(CChar(";"))
                Dim sID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(Convert.ToString(data(1)))
                Dim strFilePath As String = AssignmentDetailsPage & "?ClassID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(Convert.ToString(ClassID))) & "&CRPId=" & System.Web.HttpUtility.UrlEncode(sID) & "&CRID=" & Request.QueryString("CRID")
                Response.Redirect(strFilePath, False)
            End If
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' To bind the data source
    ''' </summary>
    Protected Sub gvCourseDetails_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvCourseDetails.NeedDataSource
        Try
            LoadStudentCourseDetails()
            If dtcourseDetails IsNot Nothing Then
                gvCourseDetails.DataSource = dtcourseDetails
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub gvAssignmentStatus_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles gvAssignmentStatus.ItemDataBound
        Try
            If (TypeOf e.Item Is GridDataItem) Then
                Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
                If Convert.ToDouble(dataItem("AssignWeightPer__c").Text.Trim) < 0 Then
                    dataItem("AssignWeightPer__c").Text = ""
                End If
            End If
            If (TypeOf e.Item Is GridFooterItem) Then
                Dim footerItem As GridFooterItem = CType(e.Item, GridFooterItem)
                footerItem("Module").Text = "Overall"
                footerItem("AssignWeightPer__c").Text = Convert.ToString(Math.Round(OverallAssignWeightPer, 2))
                'If AssignmentRowCount > 0 Then
                '    footerItem("Percentage").Text = Convert.ToString(Math.Round((OverAllPerTotal / AssignmentRowCount), 2))
                'Else
                '    footerItem("Percentage").Text = OverAllPerTotal
                'End If
                footerItem("Percentage").Text = Convert.ToString(Math.Round(OverallAssignPer, 2))
                footerItem("Status").Text = lblAssignmentStatus.Text
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub gvAssignmentStatus_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvAssignmentStatus.NeedDataSource
        Try
            LoadAssignmentStatus()
            If dtAssignmentStatus IsNot Nothing AndAlso dtAssignmentStatus.Rows.Count > 0 Then
                gvAssignmentStatus.DataSource = dtAssignmentStatus
                gvAssignmentStatus.Visible = True

            Else
                gvAssignmentStatus.Visible = False
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub gvQualificationStatus_DataBound(sender As Object, e As EventArgs) Handles gvQualificationStatus.DataBound
        Try
            Dim lblClassRegistrationID As Label
            Dim lnkCircumstancess As LinkButton
            Dim lblStatus As Label
            Dim lnkCircumstances As LinkButton
            Dim lnkClericalRecheck As LinkButton
            Dim lnkBRERequest As LinkButton
            Dim lblCourseID As Label
            Dim lblClassID As Label
            For Each item As Telerik.Web.UI.GridDataItem In gvQualificationStatus.MasterTableView.Items
                lblStatus = TryCast(item.FindControl("lblStatus"), Label)
                lnkCircumstances = TryCast(item.FindControl("lnkCircumstances"), LinkButton)
                lnkClericalRecheck = TryCast(item.FindControl("lnkClericalRecheck"), LinkButton)
                lnkBRERequest = TryCast(item.FindControl("lnkBRERequest"), LinkButton)

                lblCourseID = DirectCast(item.FindControl("lblCourseID"), Label)
                lblClassID = DirectCast(item.FindControl("lblClassID"), Label)
                lnkCircumstancess = DirectCast(item.FindControl("lnkCircumstancess"), LinkButton)
                lnkClericalRecheck = DirectCast(item.FindControl("lnkClericalRecheck"), LinkButton)
                lblClassRegistrationID = DirectCast(item.FindControl("lblClassRegistrationID"), Label)
                Dim sAppealReportSQL As String = Database & "..spGetAppealReportAppStatus__c @StudentID=" & LoggedInUser.PersonID & ",@CourseID=" & lblCourseID.Text & ",@ClassRegistrationID=" & lblClassRegistrationID.Text
                Dim dtAppealReport As DataTable = DataAction.GetDataTable(sAppealReportSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)

                Dim sSqlForCircumstanse As String = Database & "..spGetLLLAppealAppStatus__c @PersonID=" & LoggedInUser.PersonID & ",@TypeOfReson=" & "'Extenuating Circumstances'" & ",@CourseID=" & lblCourseID.Text & ",@ClassID=" & lblClassID.Text & ",@ClassRegistrationID=" & lblClassRegistrationID.Text
                Dim sCircumstanseStatus As String = Convert.ToString(DataAction.ExecuteScalar(sSqlForCircumstanse, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If sCircumstanseStatus <> "" Then
                    lnkCircumstancess.Text = sCircumstanseStatus
                End If
                Dim sSqlClericalRecheck As String = Database & "..spGetLLLAppealAppStatus__c @PersonID=" & LoggedInUser.PersonID & ",@TypeOfReson=" & "'Clerical Recheck'" & ",@CourseID=" & lblCourseID.Text & ",@ClassID=" & lblClassID.Text & ",@ClassRegistrationID=" & lblClassRegistrationID.Text
                Dim sClericalRecheckStatus As String = Convert.ToString(DataAction.ExecuteScalar(sSqlClericalRecheck, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If sClericalRecheckStatus <> "" Then
                    lnkClericalRecheck.Text = sClericalRecheckStatus
                End If
                Dim sSqlBookRepeatExam As String = Database & "..spGetLLLAppealAppStatus__c @PersonID=" & LoggedInUser.PersonID & ",@TypeOfReson=" & "'Book Repeat Exam'" & ",@CourseID=" & lblCourseID.Text & ",@ClassID=" & lblClassID.Text & ",@ClassRegistrationID=" & lblClassRegistrationID.Text
                Dim ssSqlBookRepeatExamStatus As String = Convert.ToString(DataAction.ExecuteScalar(sSqlBookRepeatExam, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If ssSqlBookRepeatExamStatus <> "" Then
                    lnkBRERequest.Text = ssSqlBookRepeatExamStatus
                End If
                Dim sCheckPublishedDate As String
                sCheckPublishedDate = Database & "..spCheckLLLClassPublishDate__c @ClassID=" & lblClassID.Text
                Dim IsDisplayExamNumber As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sCheckPublishedDate, IAptifyDataAction.DSLCacheSetting.BypassCache))

                If item.Cells(2).Text.Trim = "Exam" Then
                    If IsDisplayExamNumber = True And lblStatus.Text.Trim.ToLower = "failed" Then
                        lnkCircumstancess.Visible = True
                        lnkClericalRecheck.Visible = True
                        lnkBRERequest.Visible = True
                    Else
                        lnkCircumstancess.Visible = False
                        lnkClericalRecheck.Visible = False
                        lnkBRERequest.Visible = False
                    End If
                End If
                
            Next
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub gvQualificationStatus_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles gvQualificationStatus.ItemCommand
        Try
            Dim lblCourseID As Label
            Dim lblClassID As Label
            Dim lblClassRegistrationID As Label
            Dim lnkCircumstancess As LinkButton
            Dim lnkClericalRecheck As LinkButton
            lblCourseID = DirectCast(e.Item.FindControl("lblCourseID"), Label)
            lblClassID = DirectCast(e.Item.FindControl("lblClassID"), Label)
            lnkCircumstancess = DirectCast(e.Item.FindControl("lnkCircumstancess"), LinkButton)
            lnkClericalRecheck = DirectCast(e.Item.FindControl("lnkClericalRecheck"), LinkButton)
            lblClassRegistrationID = DirectCast(e.Item.FindControl("lblClassRegistrationID"), Label)
            Dim lCourseID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblCourseID.Text)
            Dim lClassID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblClassID.Text)
            Dim lClassRegistrationID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblClassRegistrationID.Text)
            Dim lTypeID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(e.CommandName)
            Dim isLLL As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt("Y")
            If e.CommandName = "Extenuating Circumstances" Then
                Response.Redirect(AppealAppPage & "?CrId=" & System.Web.HttpUtility.UrlEncode(lClassRegistrationID) & "&Type=" & System.Web.HttpUtility.UrlEncode(lTypeID) & "&IsLLL=" & System.Web.HttpUtility.UrlEncode(isLLL))
            ElseIf e.CommandName = "Clerical Recheck" Then
                Response.Redirect(AppealAppPage & "?CrId=" & System.Web.HttpUtility.UrlEncode(lClassRegistrationID) & "&Type=" & System.Web.HttpUtility.UrlEncode(lTypeID) & "&IsLLL=" & System.Web.HttpUtility.UrlEncode(isLLL))
            ElseIf e.CommandName = "Book Repeat Exam" Then
                Response.Redirect(AppealAppPage & "?CrId=" & System.Web.HttpUtility.UrlEncode(lClassRegistrationID) & "&Type=" & System.Web.HttpUtility.UrlEncode(lTypeID) & "&IsLLL=" & System.Web.HttpUtility.UrlEncode(isLLL))
            End If
            If e.CommandName = "Tutorial Report" Then
                Response.Redirect(AppealAppPage & "?CrId=" & System.Web.HttpUtility.UrlEncode(lClassRegistrationID) & "&Type=" & System.Web.HttpUtility.UrlEncode(lTypeID) & "&IsLLL=" & System.Web.HttpUtility.UrlEncode(isLLL))
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub gvQualificationStatus_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles gvQualificationStatus.ItemDataBound
        Try
            If (TypeOf e.Item Is GridDataItem) Then
                Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
                'dataItem("PerOverallScore").Text = PerOverAllScore / AssignmentRowCount
                If Convert.ToDouble(dataItem("Percentage").Text.Trim) < 0 Then
                    dataItem("Percentage").Text = ""
                End If
                If Convert.ToDouble(dataItem("AttendanceWeight__c").Text.Trim) > 0 Then
                    AssignmentRowCount = AssignmentRowCount + 1
                End If
                'If dataItem("Percentage").Text.Trim <> "" Then
                '    OverallAssignWeightPer = OverallAssignWeightPer + Convert.ToDouble(dataItem("Percentage").Text)
                'End If
                If dataItem("AttendanceWeight__c").Text.Trim <> "" Then
                    OverallAssignWeightPer = OverallAssignWeightPer + Convert.ToDouble(dataItem("AttendanceWeight__c").Text)
                End If
            End If
            If (TypeOf e.Item Is GridFooterItem) Then
                Dim footerItem As GridFooterItem = CType(e.Item, GridFooterItem)
                footerItem("Module").Text = "Overall"
                'If AssignmentRowCount > 0 Then
                '    footerItem("AttendanceWeight__c").Text = Convert.ToString(Math.Round((PerTotalWeighting / AssignmentRowCount), 2))
                'Else
                '    footerItem("AttendanceWeight__c").Text = Convert.ToString(Math.Round((PerTotalWeighting), 2))
                'End If
                footerItem("AttendanceWeight__c").Text = Convert.ToString(Math.Round((OverallAssignWeightPer), 2))
                footerItem("Percentage").Text = Convert.ToString(Math.Round(OverallQualPer, 2))
                footerItem("Status").Text = lblQualStatus.Text
                footerItem("Placed").Text = SubjectTopScorer__c
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try

    End Sub

    Protected Sub gvQualificationStatus_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvQualificationStatus.NeedDataSource
        Try
            AssignmentRowCount = 0
            LoadQualificationStatus()
            If dtQualStatus IsNot Nothing Then
                gvQualificationStatus.DataSource = dtQualStatus
                If dtQualStatus.Rows.Count > 0 Then
                    SubjectTopScorer__c = Convert.ToString(dtQualStatus.Rows(0)("SubjectTopScorer__c"))
                    OverallQualPer = Convert.ToDouble(dtQualStatus.Rows(0)("OverallPercentage__c"))
If OverallQualPer > 0 Then
                        divQualificationMain.Visible = True
                    Else
                        divQualificationMain.Visible = False
                    End If
                End If
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub gvExamStatus_DataBound(sender As Object, e As System.EventArgs) Handles gvExamStatus.DataBound
        Try
            Dim lblClassRegistrationID As Label
            Dim lnkCircumstancess As LinkButton
            Dim lblStatus As Label
            Dim lnkCircumstances As LinkButton
            Dim lnkClericalRecheck As LinkButton
            Dim lnkBRERequest As LinkButton
            Dim lblCourseID As Label
            Dim lblClassID As Label
            For Each item As Telerik.Web.UI.GridDataItem In gvExamStatus.MasterTableView.Items
                lblStatus = TryCast(item.FindControl("lblStatus"), Label)
                lnkCircumstances = TryCast(item.FindControl("lnkCircumstances"), LinkButton)
                lnkClericalRecheck = TryCast(item.FindControl("lnkClericalRecheck"), LinkButton)
                lnkBRERequest = TryCast(item.FindControl("lnkBRERequest"), LinkButton)

                lblCourseID = DirectCast(item.FindControl("lblCourseID"), Label)
                lblClassID = DirectCast(item.FindControl("lblClassID"), Label)
                lnkCircumstancess = DirectCast(item.FindControl("lnkCircumstancess"), LinkButton)
                lnkClericalRecheck = DirectCast(item.FindControl("lnkClericalRecheck"), LinkButton)
                lblClassRegistrationID = DirectCast(item.FindControl("lblClassRegistrationID"), Label)
                Dim lblSchemeReportStatus As Label = DirectCast(item.FindControl("lblSchemeReportStatus"), Label)
                Dim lnkSchemeReportStatus As LinkButton = DirectCast(item.FindControl("lnkSchemeReportStatus"), LinkButton)
                Dim lnkDownload As LinkButton = DirectCast(item.FindControl("lnkDownload"), LinkButton)
                Dim sAppealReportSQL As String = Database & "..spGetAppealReportAppStatus__c @StudentID=" & LoggedInUser.PersonID & ",@CourseID=" & lblCourseID.Text & ",@ClassRegistrationID=" & lblClassRegistrationID.Text
                Dim dtAppealReport As DataTable = DataAction.GetDataTable(sAppealReportSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dtAppealReport Is Nothing AndAlso dtAppealReport.Rows.Count > 0 Then
                    lblSchemeReportStatus.Visible = False
                    lnkSchemeReportStatus.Visible = True
                    lblSchemeReportStatus.Text = Convert.ToString(dtAppealReport.Rows(0)("Name"))
                    lnkSchemeReportStatus.Text = Convert.ToString(dtAppealReport.Rows(0)("Status"))
                    If Convert.ToString(dtAppealReport.Rows(0)("Status")).Trim.ToLower = "complete" Then
                        lnkDownload.Visible = True
                        lnkSchemeReportStatus.Visible = False
                    Else
                        lnkDownload.Visible = False
                    End If
                Else
                    lblSchemeReportStatus.Visible = False
                    lnkDownload.Visible = False
                End If

                Dim sSqlForCircumstanse As String = Database & "..spGetLLLAppealAppStatus__c @PersonID=" & LoggedInUser.PersonID & ",@TypeOfReson=" & "'Extenuating Circumstances'" & ",@CourseID=" & lblCourseID.Text & ",@ClassID=" & lblClassID.Text & ",@ClassRegistrationID=" & lblClassRegistrationID.Text
                Dim sCircumstanseStatus As String = Convert.ToString(DataAction.ExecuteScalar(sSqlForCircumstanse, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If sCircumstanseStatus <> "" Then
                    lnkCircumstancess.Text = sCircumstanseStatus
                End If
                Dim sSqlClericalRecheck As String = Database & "..spGetLLLAppealAppStatus__c @PersonID=" & LoggedInUser.PersonID & ",@TypeOfReson=" & "'Clerical Recheck'" & ",@CourseID=" & lblCourseID.Text & ",@ClassID=" & lblClassID.Text & ",@ClassRegistrationID=" & lblClassRegistrationID.Text
                Dim sClericalRecheckStatus As String = Convert.ToString(DataAction.ExecuteScalar(sSqlClericalRecheck, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If sClericalRecheckStatus <> "" Then
                    lnkClericalRecheck.Text = sClericalRecheckStatus
                End If
                Dim sSqlBookRepeatExam As String = Database & "..spGetLLLAppealAppStatus__c @PersonID=" & LoggedInUser.PersonID & ",@TypeOfReson=" & "'Book Repeat Exam'" & ",@CourseID=" & lblCourseID.Text & ",@ClassID=" & lblClassID.Text & ",@ClassRegistrationID=" & lblClassRegistrationID.Text
                Dim ssSqlBookRepeatExamStatus As String = Convert.ToString(DataAction.ExecuteScalar(sSqlBookRepeatExam, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If ssSqlBookRepeatExamStatus <> "" Then
                    lnkBRERequest.Text = ssSqlBookRepeatExamStatus
                End If
                Dim lblSessionID As Label = DirectCast(item.FindControl("lblSessionID"), Label)
                Dim sCheckPublishedDate As String
                sCheckPublishedDate = Database & "..spCheckLLLClassPublishDate__c @ClassID=" & lblClassID.Text
                Dim IsDisplayExamNumber As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sCheckPublishedDate, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If IsDisplayExamNumber = True And lblStatus.Text.Trim.ToLower = "failed" Then
                    If lnkDownload.Visible = False Then
                        lnkSchemeReportStatus.Visible = True
                    End If
                    lnkCircumstancess.Visible = True
                    lnkClericalRecheck.Visible = True
                    lnkBRERequest.Visible = True
                Else
                    lnkCircumstancess.Visible = False
                    lnkClericalRecheck.Visible = False
                    lnkSchemeReportStatus.Visible = False
                    lnkDownload.Visible = False
                    lnkBRERequest.Visible = False
                End If
            Next
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub gvExamStatus_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles gvExamStatus.ItemCommand
        Try
            Dim lblCourseID As Label
            Dim lblClassID As Label
            Dim lblClassRegistrationID As Label
            Dim lnkCircumstancess As LinkButton
            Dim lnkClericalRecheck As LinkButton
            Dim lblRowID As Label
            lblCourseID = DirectCast(e.Item.FindControl("lblCourseID"), Label)
            lblClassID = DirectCast(e.Item.FindControl("lblClassID"), Label)
            lnkCircumstancess = DirectCast(e.Item.FindControl("lnkCircumstancess"), LinkButton)
            lnkClericalRecheck = DirectCast(e.Item.FindControl("lnkClericalRecheck"), LinkButton)
            lblClassRegistrationID = DirectCast(e.Item.FindControl("lblClassRegistrationID"), Label)
            Dim lblExamNumber As Label = DirectCast(e.Item.FindControl("lblExamNumber"), Label)
            lblRowID = DirectCast(e.Item.FindControl("lblRowID"), Label)
            Dim lnkDownload As LinkButton = DirectCast(e.Item.FindControl("lnkDownload"), LinkButton)
            Dim lCourseID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblCourseID.Text)
            Dim lClassID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblClassID.Text)
            Dim lClassRegistrationID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblClassRegistrationID.Text)
            Dim lTypeID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(e.CommandName)
            Dim lRowID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblRowID.Text)
            Dim lExamNumberID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblExamNumber.Text)
            Dim isLLL As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt("Y")
            If e.CommandName = "Extenuating Circumstances" Then
                Response.Redirect(AppealAppPage & "?CrId=" & System.Web.HttpUtility.UrlEncode(lClassRegistrationID) & "&Type=" & System.Web.HttpUtility.UrlEncode(lTypeID) & "&IsLLL=" & System.Web.HttpUtility.UrlEncode(isLLL))
            ElseIf e.CommandName = "Clerical Recheck" Then
                Response.Redirect(AppealAppPage & "?CrId=" & System.Web.HttpUtility.UrlEncode(lClassRegistrationID) & "&Type=" & System.Web.HttpUtility.UrlEncode(lTypeID) & "&IsLLL=" & System.Web.HttpUtility.UrlEncode(isLLL))
            ElseIf e.CommandName = "Book Repeat Exam" Then
                Response.Redirect(AppealAppPage & "?CrId=" & System.Web.HttpUtility.UrlEncode(lClassRegistrationID) & "&Type=" & System.Web.HttpUtility.UrlEncode(lTypeID) & "&IsLLL=" & System.Web.HttpUtility.UrlEncode(isLLL))
            End If
            If e.CommandName = "Tutorial Report" Then
                Response.Redirect(AppealAppPage & "?CrId=" & System.Web.HttpUtility.UrlEncode(lClassRegistrationID) & "&Type=" & System.Web.HttpUtility.UrlEncode(lTypeID) & "&IsLLL=" & System.Web.HttpUtility.UrlEncode(isLLL))
            End If
            If e.CommandName = "Download" Then
                Dim sSql As String = Database & "..spGetAppealReportFileName__c @PersonID=" & LoggedInUser.PersonID & ",@CourseID=" & lblCourseID.Text & ",@ClassRegistrationID=" & lblClassRegistrationID.Text
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim fileName As String = Convert.ToString(dt.Rows(0)("Name"))
                    Dim fileExtension As String = "." + fileName.Split(CChar(".")).Last.ToLower
                    Dim dataTable As New DataTable()
                    Dim sql As String = "..spGetBlobData__c @EntityID=" & AptifyApplication.GetEntityID("AppealsApplicationDetail__c") & ",@RecordID=" & Convert.ToString(dt.Rows(0)("ID"))
                    dataTable = DataAction.GetDataTable(sql)
                    Dim FileData() As Byte
                    FileData = CType(dataTable.Rows(0)("BlobData"), Byte())
                    If ContentType(fileExtension) <> "" Then
                        Response.Buffer = True
                        Response.Cache.SetCacheability(HttpCacheability.NoCache)
                        Response.ContentType = ContentType(fileExtension)
                        Response.AddHeader("content-disposition", "attachment;filename=" + fileName)
                        Response.BinaryWrite(FileData)
                        Response.Flush()
                        Response.End()
                    End If
                End If
            End If

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub









    'Protected Sub gvExamStatus_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles gvExamStatus.ItemDataBound
    '    Try
    '        Dim lblStatus As Label = TryCast(e.Item.FindControl("lblStatus"), Label)
    '        Dim lnkCircumstances As LinkButton = TryCast(e.Item.FindControl("lnkCircumstances"), LinkButton)
    '        Dim lnkClericalRecheck As LinkButton = TryCast(e.Item.FindControl("lnkClericalRecheck"), LinkButton)
    '        Dim lnkBRERequest As LinkButton = TryCast(e.Item.FindControl("lnkBRERequest"), LinkButton)
    '        Dim lblClassRegistrationID As Label
    '        Dim lnkCircumstancess As LinkButton
    '        Dim lblCourseID As Label = DirectCast(e.Item.FindControl("lblCourseID"), Label)
    '        Dim lblClassID As Label = DirectCast(e.Item.FindControl("lblClassID"), Label)
    '        lnkCircumstancess = DirectCast(e.Item.FindControl("lnkCircumstancess"), LinkButton)
    '        lnkClericalRecheck = DirectCast(e.Item.FindControl("lnkClericalRecheck"), LinkButton)
    '        lblClassRegistrationID = DirectCast(e.Item.FindControl("lblClassRegistrationID"), Label)
    '        Dim lblSchemeReportStatus As Label = DirectCast(e.Item.FindControl("lblSchemeReportStatus"), Label)
    '        Dim lnkSchemeReportStatus As LinkButton = DirectCast(e.Item.FindControl("lnkSchemeReportStatus"), LinkButton)
    '        Dim lnkDownload As LinkButton = DirectCast(e.Item.FindControl("lnkDownload"), LinkButton)
    '        Dim sAppealReportSQL As String = Database & "..spGetAppealReportAppStatus__c @StudentID=" & LoggedInUser.PersonID & ",@CourseID=" & lblCourseID.Text & ",@ClassRegistrationID=" & lblClassRegistrationID.Text
    '        Dim dtAppealReport As DataTable = DataAction.GetDataTable(sAppealReportSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
    '        If Not dtAppealReport Is Nothing AndAlso dtAppealReport.Rows.Count > 0 Then
    '            lblSchemeReportStatus.Visible = False
    '            lnkSchemeReportStatus.Visible = True
    '            lblSchemeReportStatus.Text = Convert.ToString(dtAppealReport.Rows(0)("Name"))
    '            lnkSchemeReportStatus.Text = Convert.ToString(dtAppealReport.Rows(0)("Status"))
    '            If Convert.ToString(dtAppealReport.Rows(0)("Status")).Trim.ToLower = "complete" Then
    '                lnkDownload.Visible = True
    '                lnkSchemeReportStatus.Visible = False
    '            Else
    '                lnkDownload.Visible = False
    '            End If
    '        Else
    '            lblSchemeReportStatus.Visible = False
    '            lnkDownload.Visible = False
    '        End If

    '        Dim sSqlForCircumstanse As String = Database & "..spGetAppealAppStatus__c @PersonID=" & LoggedInUser.PersonID & ",@TypeOfResonID=" & AptifyApplication.GetEntityRecordIDFromRecordName("AppealReason__c", "Extenuating Circumstances") & ",@CourseID=" & lblCourseID.Text & ",@ClassID=" & lblClassID.Text & ",@ClassRegistrationID=" & lblClassRegistrationID.Text
    '        Dim sCircumstanseStatus As String = Convert.ToString(DataAction.ExecuteScalar(sSqlForCircumstanse, IAptifyDataAction.DSLCacheSetting.BypassCache))
    '        If sCircumstanseStatus <> "" Then
    '            lnkCircumstancess.Text = sCircumstanseStatus
    '        End If
    '        Dim sSqlClericalRecheck As String = Database & "..spGetAppealAppStatus__c @PersonID=" & LoggedInUser.PersonID & ",@TypeOfResonID=" & AptifyApplication.GetEntityRecordIDFromRecordName("AppealReason__c", "Clerical Recheck") & ",@CourseID=" & lblCourseID.Text & ",@ClassID=" & lblClassID.Text & ",@ClassRegistrationID=" & lblClassRegistrationID.Text
    '        Dim sClericalRecheckStatus As String = Convert.ToString(DataAction.ExecuteScalar(sSqlClericalRecheck, IAptifyDataAction.DSLCacheSetting.BypassCache))
    '        If sClericalRecheckStatus <> "" Then
    '            lnkClericalRecheck.Text = sClericalRecheckStatus
    '        End If
    '        Dim sSqlBookRepeatExam As String = Database & "..spGetAppealAppStatus__c @PersonID=" & LoggedInUser.PersonID & ",@TypeOfResonID=" & AptifyApplication.GetEntityRecordIDFromRecordName("AppealReason__c", "Book Repeat Exam") & ",@CourseID=" & lblCourseID.Text & ",@ClassID=" & lblClassID.Text & ",@ClassRegistrationID=" & lblClassRegistrationID.Text
    '        Dim ssSqlBookRepeatExamStatus As String = Convert.ToString(DataAction.ExecuteScalar(sSqlBookRepeatExam, IAptifyDataAction.DSLCacheSetting.BypassCache))
    '        If ssSqlBookRepeatExamStatus <> "" Then
    '            lnkBRERequest.Text = ssSqlBookRepeatExamStatus
    '        End If

    '        Dim lblCircumstancess As Label = DirectCast(e.Item.FindControl("lblCircumstancess"), Label)
    '        Dim lblClericalRecheck As Label = DirectCast(e.Item.FindControl("lblClericalRecheck"), Label)
    '        Dim lblSessionID As Label = DirectCast(e.Item.FindControl("lblSessionID"), Label)
    '        Dim sCheckPublishedDate As String
    '        sCheckPublishedDate = Database & "..spCheckLLLClassPublishDate__c @ClassID=" & lblClassID.Text
    '        Dim IsDisplayExamNumber As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sCheckPublishedDate, IAptifyDataAction.DSLCacheSetting.BypassCache))
    '        If IsDisplayExamNumber = True And lblStatus.Text.Trim.ToLower = "failed" Then
    '            lnkCircumstancess.Visible = True
    '            lnkClericalRecheck.Visible = True
    '            lnkSchemeReportStatus.Visible = True
    '            lblCircumstancess.Text = ""
    '            lblClericalRecheck.Text = ""
    '        Else
    '            lblCircumstancess.Text = sCircumstanseStatus
    '            lnkCircumstancess.Visible = False
    '            lblClericalRecheck.Text = sClericalRecheckStatus
    '            lnkClericalRecheck.Visible = False
    '            lnkSchemeReportStatus.Visible = False
    '            lnkDownload.Visible = False
    '        End If
    '    Catch ex As Exception
    '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
    '    End Try
    'End Sub

    Protected Sub gvExamStatus_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvExamStatus.NeedDataSource
        Try
            LoadExamStatus()
            If dtExamStatus IsNot Nothing Then
                gvExamStatus.DataSource = dtExamStatus
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

#End Region

#Region "Button Events"
    ''' <summary>
    ''' Handles the cancel button event - download couse materials popup
    ''' </summary>
    Protected Sub btnClose_Click(sender As Object, e As System.EventArgs) Handles btnClose.Click
        radDownloadDocuments.VisibleOnPageLoad = False
    End Sub

    ''' <summary>
    ''' Handles the add note button event to add notes to the Student Comments -  tab
    ''' </summary>
    Protected Sub btnAddNotes_Click(sender As Object, e As System.EventArgs) Handles btnAddNotes.Click
        Try
            lblAddNoteMessage.Text = ""
            If txtNotes.Text.Trim <> "" Then
                Dim oClassRegistrationGE As Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase
                oClassRegistrationGE = AptifyApplication.GetEntityObject("Class Registrations", Me.ClassRegistrationID)
                With oClassRegistrationGE.SubTypes("ClassRegistrationPartStatus").Find("ID", Me.PartStatusID)
                    .SetValue("StudentComments", txtNotes.Text)
                End With
                Dim result As Boolean = oClassRegistrationGE.Save(lblAddNoteMessage.Text)
                If result = True Then
                    radAddNotes.VisibleOnPageLoad = False
                Else
                    lblAddNoteMessage.ForeColor = Color.Red
                End If
            Else
                lblAddNoteMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLQualStatus.NotesTextValidate__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblAddNoteMessage.ForeColor = Color.Red
            End If
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Handles the cancel button event - Add notes popup
    ''' </summary>
    Protected Sub btnCloseAddNotes_Click(sender As Object, e As System.EventArgs) Handles btnCloseAddNotes.Click
        radAddNotes.VisibleOnPageLoad = False
    End Sub


    Protected Sub lnkEnrollChangesReq_Click(sender As Object, e As System.EventArgs) Handles lnkEnrollChangesReq.Click
        Dim strFilePath As String = PostEnrollReqPage & "?ClassID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(Convert.ToString(ClassID))) & "&CRID=" & System.Web.HttpUtility.UrlEncode(Request.QueryString("CRID"))
        Response.Redirect(strFilePath, False)
    End Sub

    Protected Sub btnSubmitPay_Click(sender As Object, e As System.EventArgs) Handles btnSubmitPay.Click
        Try
            'Response.Redirect("~\ProductCatalog\ViewCart.aspx", False)
            Response.Redirect(ViewCartPage, False)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub lnkAssignmenthelp_Click(sender As Object, e As System.EventArgs) Handles lnkAssignmenthelp.Click
        Try
            OpenHelpDocuments("AssignmentHelp")
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub lnkExamStatushelp_Click(sender As Object, e As System.EventArgs) Handles lnkExamStatushelp.Click
        Try
            OpenHelpDocuments("FinalExamHelp")
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub lnkQualStatushelp_Click(sender As Object, e As System.EventArgs) Handles lnkQualStatushelp.Click
        Try
            OpenHelpDocuments("QualificationHelp")
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub lnkAttendenceStatushelp_Click(sender As Object, e As System.EventArgs) Handles lnkAttendenceStatushelp.Click
        Try
            OpenHelpDocuments("AttendanceHelp")
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        Try
            Response.Redirect(QualificationStatusPage, True)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try

    End Sub

#End Region

#Region "Functions"


    Private Function CheckDisplayQualStatLink() As Boolean
        Try
            Dim sSQL As String = Database & "..spCheckPersonIsRegisterForLLL__c @PersonId=" & LoggedInUser.PersonID
            Dim iCheck As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSQL, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
            If iCheck > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            Return False
        End Try
    End Function

    Private Sub LoadGrid()
        Dim sSQL As String, dt As Data.DataTable
        Dim ClassID As String
        Dim CRID As String
        Try
            sSQL = Database & "..spGetPersonClassRegForLLL__c @PersonId=" & LoggedInUser.PersonID
            dt = DataAction.GetDataTable(sSQL)
            Dim dcolUrl As DataColumn = New DataColumn()
            dcolUrl.Caption = "DataNavigateUrl"
            dcolUrl.ColumnName = "DataNavigateUrl"
            dt.Columns.Add(dcolUrl)
            lblMessage.Text = ""
            If dt.Rows.Count > 0 AndAlso dt IsNot Nothing Then
                For Each rw As DataRow In dt.Rows
                    ClassID = Aptify.Framework.Web.Common.WebCryptography.Encrypt(Convert.ToString(rw("ClassID")))
                    CRID = Aptify.Framework.Web.Common.WebCryptography.Encrypt(Convert.ToString(rw("CRID")))
                    Dim navigate As String = QualificationStatusPage & "?ClassID=" & System.Web.HttpUtility.UrlEncode(ClassID) & "&CRID=" & System.Web.HttpUtility.UrlEncode(CRID)
                    rw("DataNavigateUrl") = navigate
                Next
                grdClassReg.DataSource = dt
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load the student course details 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadStudentCourseDetails()
        Try
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0}..spGetStudentCourseDetailsForLLL__c @PersonID={1},@ClassID={2}", Me.Database, Me.StudentID, Me.ClassID)
            dtcourseDetails = Me.DataAction.GetDataTable(Convert.ToString(sql))
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load the Attendance Status details 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadAttendanceStatus()
        Try
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0}..spGetAttendanceStatusDetails__c @ClassRegID={1}", Me.Database, Me.ClassRegistrationID)
            dtAttendanceStatus = Me.DataAction.GetDataTable(Convert.ToString(sql))
            If dtAttendanceStatus IsNot Nothing AndAlso dtAttendanceStatus.Rows.Count > 0 Then
                divAttendenceReq.InnerHtml = String.Format(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                                               Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLQualStatus.AttendanceStatus__c")), _
                                                Convert.ToInt32(AptifyApplication.UserCredentials.CultureID), DataAction.UserCredentials), Convert.ToString(dtAttendanceStatus.Rows(0)("Target")), Convert.ToString(dtAttendanceStatus.Rows(0)("TotalAttendance")))
                ' lblAttendenceStatus.Text = dtAttendanceStatus.Rows(0)("AttendanceStatus__c")
                If Convert.ToInt32(dtAttendanceStatus.Rows(0)("exempt")) > 0 Or Convert.ToString(dtAttendanceStatus.Rows(0)("ClassType")).trim.ToLower = "distance" Then
                    divAttendanceStatus.Visible = False
                End If
            End If
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Set header details of the page
    ''' </summary>
    Private Sub LoadHeaderDetails()
        Try
            Dim d1 As DateTime
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0}..spGetHeaderDtlForLLLQualStatus__c @PersonID={1},@ClassID={2}", _
                             Me.Database, Me.StudentID, Me.ClassID)
            dtHearderDetails = Me.DataAction.GetDataTable(Convert.ToString(sql))
            If dtHearderDetails IsNot Nothing AndAlso dtHearderDetails.Rows.Count > 0 Then
                lblFirstLastValue.Text = dtHearderDetails.Rows(0)("FirstLast")
                lblStudentNumberValue.Text = dtHearderDetails.Rows(0)("StudentNumber")
                lblCourseNameValue.Text = dtHearderDetails.Rows(0)("Course")
                 ' Added below condtion by Govind M
                If Convert.ToString(dtHearderDetails.Rows(0)("StartDate")) <> "" Then
                    d1 = Convert.ToDateTime(dtHearderDetails.Rows(0)("StartDate"))
                    lblStartDateValue.Text = (New Date(d1.Year, d1.Month, d1.Day).ToShortDateString())
                End If
                If Convert.ToString(dtHearderDetails.Rows(0)("EndDate")) <> "" Then
                    d1 = Convert.ToDateTime(dtHearderDetails.Rows(0)("EndDate"))
                    lblEndDateValue.Text = (New Date(d1.Year, d1.Month, d1.Day).ToShortDateString())
                End If
                lblTypeName.Text = dtHearderDetails.Rows(0)("Type")
                lblAttendenceStatus.Text = Convert.ToString(dtHearderDetails.Rows(0)("AttendanceStatus__c"))
                lblAssignmentStatus.Text = Convert.ToString(dtHearderDetails.Rows(0)("AssignmentStatus__c"))
                lblExamStatus.Text = Convert.ToString(dtHearderDetails.Rows(0)("FinalExamStatus__c"))
                lblQualStatus.Text = Convert.ToString(dtHearderDetails.Rows(0)("Status"))
                'If Convert.ToString(lblExamStatus.Text).Trim.ToLower = "failed" Then
                '    btnSubmitPay.Visible = True
                'Else
                '    btnSubmitPay.Visible = False
                'End If
                If Convert.ToString(dtHearderDetails.Rows(0)("Status")).Trim.ToLower = "passed" Or Convert.ToString(dtHearderDetails.Rows(0)("Status")).Trim.ToLower = "withdrawal" Or Convert.ToString(dtHearderDetails.Rows(0)("Status")).Trim.ToLower = "failed" Then
                    lnkEnrollChangesReq.Visible = False
                End If
                If Convert.ToInt32(dtHearderDetails.Rows(0)("IsPublished")) = 1 Then
                    divQualificationMain.Visible = True
                End If
            End If
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load the Assignment Status details 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadAssignmentStatus()
        Try
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0}..spGetAssignmentReqDetails__c @ClassRegID={1}", Me.Database, Me.ClassRegistrationID)
            dtAssignmentStatus = Me.DataAction.GetDataTable(Convert.ToString(sql))
            If dtAssignmentStatus IsNot Nothing AndAlso dtAssignmentStatus.Rows.Count > 0 Then
                AssignmentRowCount = dtAssignmentStatus.Rows.Count
                OverallAssignWeightPer = Convert.ToDouble(dtAssignmentStatus.Rows(0)("PerOverallScore"))
                OverallAssignPer = Convert.ToDouble(dtAssignmentStatus.Rows(0)("AssignmentPercent__c"))
                divAssignmentMain.Visible = True
            Else
                divAssignmentMain.Visible = False
            End If
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load the Exam Status details 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadExamStatus()
        Try
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0}..spGetExamStatusDetails__c @ClassRegID={1}", Me.Database, Me.ClassRegistrationID)
            dtExamStatus = Me.DataAction.GetDataTable(Convert.ToString(sql))
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub


    ''' <summary>
    ''' Load the Qualification Status details 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadQualificationStatus()
        Try
            OverallQualPer = 0
            OverallAssignWeightPer = 0
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0}..spGetQualificationStatusDetails__c @ClassRegID={1}", Me.Database, Me.ClassRegistrationID)
            dtQualStatus = Me.DataAction.GetDataTable(Convert.ToString(sql))
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub


    ''' <summary>
    ''' Method To Open Help Document From Curriculum Defination Entity.
    ''' </summary>
    ''' <param name="Category"></param>
    ''' <remarks></remarks>
    Private Sub OpenHelpDocuments(ByVal Category As String)
        Try
            Dim entityID As Integer = Convert.ToInt32(Me.AptifyApplication.GetEntityID("Curriculum Definitions"))
            Dim categoryID As Integer = Me.AptifyApplication.GetEntityRecordIDFromRecordName("Attachment Categories", Category)
            Dim sSql As String = Database & "..spGetEntityAttachmentsByCategory__c @EntityID=" & entityID & ",@CategoryID=" & categoryID
            Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                Dim fileName As String = Convert.ToString(dt.Rows(0)("Name"))
                Dim fileExtension As String = "." + fileName.Split(CChar(".")).Last.ToLower
                Dim dataTable As New DataTable()
                Dim sql As String = "..spGetBlobDataLLLHelp__c @EntityID=" & entityID & ",@RecordID=" & Convert.ToString(dt.Rows(0)("ID"))
                dataTable = DataAction.GetDataTable(sql)
                Dim FileData() As Byte
                FileData = CType(dataTable.Rows(0)("BlobData"), Byte())
                If ContentType(fileExtension) <> "" Then
                    Response.Buffer = True
                    Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    Response.ContentType = ContentType(fileExtension)
                    Response.AddHeader("content-disposition", "attachment;filename=" + fileName)
                    Response.BinaryWrite(FileData)
                    Response.Flush()
                    Response.End()
                End If
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Private Function ContentType(ByVal fileExtension As String) As String
        Try
            Dim d As New Dictionary(Of String, String)
            'Images'
            d.Add(".bmp", "image/bmp")
            d.Add(".gif", "image/gif")
            d.Add(".jpeg", "image/jpeg")
            d.Add(".jpg", "image/jpeg")
            d.Add(".png", "image/png")
            d.Add(".tif", "image/tiff")
            d.Add(".tiff", "image/tiff")
            'Documents'
            d.Add(".doc", "application/msword")
            d.Add(".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
            d.Add(".pdf", "application/pdf")
            'Slideshows'
            d.Add(".ppt", "application/vnd.ms-powerpoint")
            d.Add(".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation")
            'Data'
            d.Add(".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            d.Add(".xls", "application/vnd.ms-excel")
            d.Add(".csv", "text/csv")
            d.Add(".xml", "text/xml")
            d.Add(".txt", "text/plain")
            'Compressed Folders'
            d.Add(".zip", "application/zip")
            'Audio'
            d.Add(".ogg", "application/ogg")
            d.Add(".mp3", "audio/mpeg")
            d.Add(".wma", "audio/x-ms-wma")
            d.Add(".wav", "audio/x-wav")
            'Video'
            d.Add(".wmv", "audio/x-ms-wmv")
            d.Add(".swf", "application/x-shockwave-flash")
            d.Add(".avi", "video/avi")
            d.Add(".mp4", "video/mp4")
            d.Add(".mpeg", "video/mpeg")
            d.Add(".mpg", "video/mpeg")
            d.Add(".qt", "video/quicktime")
            ' Crystal Report
            d.Add(".rpt", "application/rpt")
            Return d(fileExtension)
        Catch ex As Exception
            Return String.Empty
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Function

#End Region


End Class

