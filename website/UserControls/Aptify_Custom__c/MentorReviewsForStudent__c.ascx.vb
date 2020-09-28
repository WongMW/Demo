'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Govind Mande               09/2/2015                        Student’s view of Mentor Reviews Page
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness
Imports Telerik.Web.UI
Namespace Aptify.Framework.Web.eBusiness
    Partial Class MentorReviewsForStudent__c
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_StudentDashboardPage As String = "StudentDashboardPage"
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
        Public Overridable Property StudentDashboardPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_StudentDashboardPage) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_StudentDashboardPage))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_StudentDashboardPage) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(LoginPage) Then
                LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage)
            End If
            If String.IsNullOrEmpty(StudentDashboardPage) Then
                StudentDashboardPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_StudentDashboardPage)
            End If
        End Sub

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
                    LoadMentorReview()
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

#Region "LoadData"
        ''' <summary>
        ''' Load Student Mentor review data 
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadMentorReview()
            Try
                Dim sSql As String = Database & "..spGetStudentMentorReviewList__c @StudentID=" & User1.PersonID
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    radStudentReview.DataSource = dt
                    radStudentReview.DataBind()
                    lblErrorMsg.Visible = False
                    radStudentReview.Visible = True
                Else
                    lblErrorMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.MentorReviewsForStudent.ErrorMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    lblErrorMsg.Visible = True
                    radStudentReview.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

#Region "Grid Event"
        ''' <summary>
        ''' Call Popup code
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub radStudentReview_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles radStudentReview.ItemCommand
            Try
                If e.CommandName = "MentorReviewTitle" Then
                    LoadPopupData(e.CommandArgument)
                    radwindow.VisibleOnPageLoad = True
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub radStudentReview_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles radStudentReview.NeedDataSource
            Try
                LoadMentorReview()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

#Region "Popup Event"
        Private Sub LoadPopupData(ByVal StudentDairyID As Integer)
            Try
                Dim sSql As String = Database & "..spGetStudentMentorDetails__c @StudentDairyID=" & StudentDairyID
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    lblStudentName.Text = Convert.ToString(dt.Rows(0)("StudentName"))
                    lblTraineeStudentNumber.Text = Convert.ToString(dt.Rows(0)("TraineeStudentNumber"))
                    lblBueinessUnit.Text = Convert.ToString(dt.Rows(0)("BusinessUnit"))
                    lblRouteOfEntry.Text = Convert.ToString(dt.Rows(0)("RouteOfEntry"))
                    lblMentorName.Text = Convert.ToString(dt.Rows(0)("MentorName"))
                    lblReviewType.Text = Convert.ToString(dt.Rows(0)("Type"))
                    lblReviewStartDate.Text = Convert.ToString(dt.Rows(0)("ReviewStartDate"))
                    lblMentorReviewTitle.Text = Convert.ToString(dt.Rows(0)("MentorReviewTitle"))
                    lblMentorEvaluationDescription.Text = Convert.ToString(dt.Rows(0)("MentorComments"))

                    If lblReviewType.Text.Trim.ToLower = "final review" Then
                        dvReviewPeriodID.Visible = False
                        dvMentorTitle.Visible = False
                    Else
                        dvReviewPeriodID.Visible = True
                        dvMentorTitle.Visible = True
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub btnPopupBack_Click(sender As Object, e As System.EventArgs) Handles btnPopupBack.Click
            Try
                radwindow.VisibleOnPageLoad = False
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

#Region "ButtonEvent"
        ''' <summary>
        ''' CLick on back redirect to the student dashobard page
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
            Try
                Response.Redirect(StudentDashboardPage, False)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

      End Class
End Namespace
