''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer            Date Created/Modified               Summary
'Kavita Zinage        08/28/2015                      CA Diary Entries
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Strict On
Option Explicit On

Imports System.Data
Imports Telerik.Web.UI
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness
Imports Aptify.Framework.Application
Imports System.IO
Namespace Aptify.Framework.Web.eBusiness.Education
    Partial Class CADiaryEntries__c
        Inherits BaseUserControlAdvanced

        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "StudentDiaryEntry__c"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_STUDENTDIARYPAGE As String = "StudentDiaryEntryPage"
        Protected Const ATTRIBUTE_STUDENTDASHBOARDPAGE As String = "StudentDashboardPage"


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
        Public Overridable Property StudentDiaryEntryPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_STUDENTDIARYPAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_STUDENTDIARYPAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_STUDENTDIARYPAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Public Overridable Property StudentDashboardPage() As String
            Get
                If Not ViewState(ATTRIBUTE_STUDENTDASHBOARDPAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_STUDENTDASHBOARDPAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_STUDENTDASHBOARDPAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(LoginPage) Then
                LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage)
            End If
            If String.IsNullOrEmpty(StudentDiaryEntryPage) Then
                StudentDiaryEntryPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_STUDENTDIARYPAGE)
            End If
            If String.IsNullOrEmpty(StudentDashboardPage) Then
                StudentDashboardPage = Me.GetLinkValueFromXML(ATTRIBUTE_STUDENTDASHBOARDPAGE)
            End If
        End Sub
#End Region

#Region "Page Events"
        ''' <summary>
        ''' Handles page load event
        ''' </summary>
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                SetProperties()
                If User1.PersonID <= 0 Then
                    Application("ReturnToPage") = Request.RawUrl
                    Response.Redirect(LoginPage)
                End If
                If Not IsPostBack Then
                    LoadGrid()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

#Region "Private Functions"
        ''' <summary>
        ''' Load gvAssignmentDetails
        ''' </summary>
        Private Sub LoadGrid()
            Try
                Dim sSQL As New StringBuilder()
                sSQL.AppendFormat("{0}..spGetAllStudentDiaryEntriesDetails__c @StudentID={1}", Me.Database, Convert.ToInt32(User1.PersonID))
                Dim dt As DataTable = DataAction.GetDataTable(sSQL.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    ViewState("DiaryEntries") = dt
                    Me.gvDiaryEntries.DataSource = dt
                    Me.gvDiaryEntries.Visible = True
                    Me.gvDiaryEntries.Rebind()
                Else
                    lblErrorMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExistingDiaryEntries.ErrorMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    lblErrorMsg.Visible = True
                    Me.gvDiaryEntries.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

#End Region
        ''' <summary>
        ''' Handles Grid Data Bound Event
        ''' </summary>
        Protected Sub gvDiaryEntries_DataBound(sender As Object, e As System.EventArgs) Handles gvDiaryEntries.DataBound
            For Each item As Telerik.Web.UI.GridDataItem In gvDiaryEntries.MasterTableView.Items
                Dim lnkEdit As LinkButton = DirectCast(item.FindControl("lnkEdit"), LinkButton)
                'If item.Cells(5).Text.ToLower = "locked" Or item.Cells(5).Text.ToLower = "submitted to mentor" Then
                If item.Cells(5).Text.ToLower = "locked" Then 'Modified by Harish Redmine Enhancement #20870 - Student not able to modify or do changes in records if status is locked, while in other cases user can able to edit the data
                    lnkEdit.Text = "View"
                Else
                    lnkEdit.Text = "Edit"
                End If
            Next
        End Sub

        ''' <summary>
        ''' Handles need data source to load/reload grid 
        ''' </summary>
        Protected Sub gvDiaryEntries_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvDiaryEntries.NeedDataSource
            Try
                If ViewState("DiaryEntries") IsNot Nothing Then
                    gvDiaryEntries.DataSource = CType(ViewState("DiaryEntries"), DataTable)
                Else
                    gvDiaryEntries.DataSource = Nothing
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Handles Back button click
        ''' </summary>
        Protected Sub cmdBack_Click(sender As Object, e As System.EventArgs) Handles cmdBack.Click
            Try
                Response.Redirect(StudentDashboardPage)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Handles Edit click
        ''' </summary>
        Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Try
                Dim btnEdit As LinkButton = CType(sender, LinkButton)
                Response.Redirect(Me.StudentDiaryEntryPage & "?ID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(Convert.ToUInt32(btnEdit.CommandArgument).ToString())))
           
' 17476
 Catch taex As System.Threading.ThreadAbortException
' 17476
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
    End Class
End Namespace
