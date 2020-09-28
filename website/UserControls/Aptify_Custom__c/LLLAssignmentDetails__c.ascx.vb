'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Pradip Chavhan               05/11/2015                        LLL Assignment Details
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness
Imports Telerik.Web.UI
Namespace Aptify.Framework.Web.eBusiness
    Partial Class LLLAssignmentDetails__c
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"
        Protected Const ATTRIBUTE_SECURITYERROR_PAGE As String = "securityErrorPage"

        Protected Const ATTRIBUTE_QualificationStatusPage As String = "QualificationStatusPage"
        Dim sErrorMessage As String = String.Empty
        Dim dtCRPartStatus As DataTable
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

        Public ReadOnly Property ClassRegPartStatusID() As Integer
            Get
                Dim CRPId As String = Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("CRPId"))
                If Not String.IsNullOrEmpty(CRPId) Then
                    CRPId = Convert.ToInt32(CRPId)
                End If
                Return CRPId
            End Get
        End Property


        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(LoginPage) Then
                LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage)
            End If
            If String.IsNullOrEmpty(QualificationStatusPage) Then
                QualificationStatusPage = Me.GetLinkValueFromXML(ATTRIBUTE_QualificationStatusPage)
            End If
        End Sub

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

        Public Overridable Property QualificationStatusPage() As String
            Get
                If Not ViewState(ATTRIBUTE_QualificationStatusPage) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_QualificationStatusPage))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_QualificationStatusPage) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
#End Region

#Region "Page Events"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                SetProperties()
                If User1.PersonID <= 0 Then
                    Session("ReturnToPage") = Request.RawUrl
                    Response.Redirect(LoginPage)
                End If
                If Not IsPostBack Then
                    Dim sql As New StringBuilder()
                    sql.AppendFormat("{0}..spGetClassRegPartDetailsForLLL__c @ClassRegPartStatusID={1}", Me.Database, Me.ClassRegPartStatusID)
                    dtCRPartStatus = Me.DataAction.GetDataTable(Convert.ToString(sql))
                    If dtCRPartStatus IsNot Nothing AndAlso dtCRPartStatus.Rows.Count > 0 Then
                        If Convert.ToString(dtCRPartStatus.Rows(0)("Status")).Trim.ToLower = "success" Or Convert.ToString(dtCRPartStatus.Rows(0)("Status")).Trim.ToLower = "failed" Or Convert.ToString(dtCRPartStatus.Rows(0)("Status")).Trim.ToLower = "with corrector" Then
                            ucLLLAssignmentUpload.AllowAdd = False
                            ucLLLAssignmentUpload.AllowDelete = False
                        End If
                        lblDaysRemainingValue.Text = (Convert.ToDateTime(dtCRPartStatus.Rows(0)("EndDate")) - DateTime.Now.Date).TotalDays.ToString()
                        lblbAssignmentDueDateValue.Text = Convert.ToDateTime(dtCRPartStatus.Rows(0)("EndDate"))
                        Dim uploadRecordId As Integer = Convert.ToInt32(dtCRPartStatus.Rows(0)("ClassRegPartStatusID"))
                        Dim uploadEntityId As Integer = CInt(Me.AptifyApplication.GetEntityID("ClassRegistrationPartStatus"))
                        ucLLLAssignmentUpload.RecordID = uploadRecordId
                        ucLLLAssignmentUpload.EntityID = uploadEntityId
                        ucLLLAssignmentUpload.AttachmentCategory = Me.AptifyApplication.GetEntityRecordIDFromRecordName("Attachment Categories", "Assignments Upload")
                        ucLLLAssignmentUpload.LoadAttachments(uploadEntityId, uploadRecordId, True)
                        Dim downloadRecordId As Integer = Convert.ToInt32(dtCRPartStatus.Rows(0)("CoursePartID"))
                        Dim downloadEntityId As Integer = CInt(Me.AptifyApplication.GetEntityID("Course Parts"))
                        ucAssignmentDownload.RecordID = downloadRecordId
                        ucAssignmentDownload.EntityID = downloadEntityId
                        ucAssignmentDownload.AttachmentCategory = Me.AptifyApplication.GetEntityRecordIDFromRecordName("Attachment Categories", "Assignments")
                        ucAssignmentDownload.LoadAttachments(downloadEntityId, downloadRecordId, True)
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region
#Region "Public Function"
       

#End Region

#Region "ButtonEvent"
        Protected Sub btnCloseAssignment_Click(sender As Object, e As System.EventArgs) Handles btnCloseAssignment.Click
            Try
                Response.Redirect(QualificationStatusPage & "?ClassID=" & Request.QueryString("ClassID") & "&CRID=" & Request.QueryString("CRID"))
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

       
    End Class
End Namespace
