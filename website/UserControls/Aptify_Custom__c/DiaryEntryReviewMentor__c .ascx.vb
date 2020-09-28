''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer            Date Created/Modified               Summary
'Kavita Zinage        06/10/2015                      Mentor’s detailed Student Diary Review Page 
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Strict On
Option Explicit On

Imports System.Data
Imports Aptify.Framework.Web.eBusiness
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.Application
Imports System.Web.Security
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Data.SqlClient
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.BusinessLogic.Security
Imports Telerik.Web.UI
Namespace Aptify.Framework.Web.eBusiness.Education
    Partial Class DiaryEntryReviewMentor__c
        Inherits BaseUserControlAdvanced

        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "DiaryEntryReviewMentor__c"
        Protected Const ATTRIBUTE_DIARYENTRYSUMMARYPAGE As String = "DiaryEntrySummaryPage"

#Region "Properties"

        Public Overridable Property DiaryEntrySummaryPage() As String
            Get
                If Not ViewState(ATTRIBUTE_DIARYENTRYSUMMARYPAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_DIARYENTRYSUMMARYPAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_DIARYENTRYSUMMARYPAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(Me.ID) Then
                Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            End If

            MyBase.SetProperties()

            If String.IsNullOrEmpty(DiaryEntrySummaryPage) Then
                DiaryEntrySummaryPage = Me.GetLinkValueFromXML(ATTRIBUTE_DIARYENTRYSUMMARYPAGE)
            End If
        End Sub

#End Region
        ''Page Load Event
        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            Try

                SetProperties()
                If Request.QueryString("Page") IsNot Nothing Then
                    hdnCADiaryRecordID.Value = Convert.ToString(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("ID")))
                End If
                If Not IsPostBack Then
                    lblMessage.Text = ""
                    DisableControl()
                    LoadDiaryDetails()
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
            ''For Dynamic Script loading By Shiwendra
            Dim js As HtmlGenericControl = New HtmlGenericControl("script")
            js.Attributes.Add("type", "text/javascript")
            js.Attributes.Add("src", ResolveUrl("~/Scripts/jquery-1.7.1.min.js"))
            Me.Page.Header.Controls.Add(js)
            Dim js1 As HtmlGenericControl = New HtmlGenericControl("script")
            js1.Attributes.Add("type", "text/javascript")
            js1.Attributes.Add("src", ResolveUrl("~/Scripts/expand.js"))
            Me.Page.Header.Controls.Add(js1)
            Dim css As HtmlGenericControl = New HtmlGenericControl("style")
            css.Attributes.Add("type", "text/css")
            css.Attributes.Add("src", ResolveUrl("~/CSS/StyleSheet.css"))
            Me.Page.Header.Controls.Add(css)
            Dim js2 As HtmlGenericControl = New HtmlGenericControl("script")
            js2.Attributes.Add("type", "text/javascript")
            js2.Attributes.Add("src", ResolveUrl("~/Scripts/jquery.min.js"))
            Me.Page.Header.Controls.Add(js2)
        End Sub

#Region "Functions"

        ''' <summary>
        '''  Set Control ReadOnly/Disabled
        ''' </summary>
        Private Sub DisableControl()
            Try

                txtStartDate.Enabled = False
                txtEndDate.Enabled = False
                txtExperience.ReadOnly = True
                txtTitle.ReadOnly = True
                txtEngagmentNumber.ReadOnly = True
                txtCompanyDays.ReadOnly = True
                txtOtherDays.ReadOnly = True
                'Added as part of #20530
                txtOtherNIDays.ReadOnly = True
                txtStatutoryDays.ReadOnly = True
                txtToilDays.ReadOnly = True
                'End of #20530
                RecordAttachments__c.AllowAdd = False
                RecordAttachments__c.AllowView = True
                RecordAttachments__c.AllowDelete = False

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ' Load Leave Details
        Private Sub LoadLeavesGrid()
            Try
                Dim sSQLLeave As New StringBuilder()

                sSQLLeave.AppendFormat("{0}..spGetLeavesByDiaryID__c  @DiaryID={1}", Me.Database, Convert.ToInt32(hdnCADiaryRecordID.Value))
                Dim dtDiaryLeaves As DataTable = DataAction.GetDataTable(sSQLLeave.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)

                If Not dtDiaryLeaves Is Nothing Then 'AndAlso dtDiaryLeaves.Rows.Count > 0
                    grdLeave.Visible = True
                    grdLeave.DataSource = dtDiaryLeaves
                    grdLeave.DataBind()
                    ViewState("dtLeave") = dtDiaryLeaves
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        'Load Competency Details
        Private Sub LoadCompetencyGrid()
            Try
                Dim sSQLCompetency As New StringBuilder()
                sSQLCompetency.AppendFormat("{0}..spGetCompetenciesByDiaryID__c @DiaryID={1}", Me.Database, Convert.ToInt32(hdnCADiaryRecordID.Value))
                Dim dtDiaryCompetencies As DataTable = DataAction.GetDataTable(sSQLCompetency.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dtDiaryCompetencies Is Nothing Then 'AndAlso dtDiaryCompetencies.Rows.Count > 0
                    gvCompetency.Visible = true
                    gvCompetency.DataSource = dtDiaryCompetencies
                    gvCompetency.DataBind()
                    ViewState("dtCompetency") = dtDiaryCompetencies
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        'Load Detailed Diary Entry Details
        Private Sub LoadDiaryDetails()
            Try
                Dim sSQLDetails As New StringBuilder()
                sSQLDetails.AppendFormat("{0}..spGetDetailedDiaryEntryDataByID__c @ID={1}", Me.Database, Convert.ToInt32(hdnCADiaryRecordID.Value))
                Dim dtDiaryDetails As DataTable = DataAction.GetDataTable(sSQLDetails.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)

                If Not dtDiaryDetails Is Nothing AndAlso dtDiaryDetails.Rows.Count > 0 Then
                    lblStudentName.Text = Convert.ToString(dtDiaryDetails.Rows(0).Item("StudentName"))
                    lblCompany.Text = Convert.ToString(dtDiaryDetails.Rows(0).Item("CompanyName"))
                    lblBusiness.Text = Convert.ToString(dtDiaryDetails.Rows(0).Item("BusinessUnit"))
                    lblRouteofEntry.Text = Convert.ToString(dtDiaryDetails.Rows(0).Item("RouteOfEntry"))
                    lblMentorName.Text = Convert.ToString(dtDiaryDetails.Rows(0).Item("MentorName"))
                    lblEntryType.Text = Convert.ToString(dtDiaryDetails.Rows(0).Item("EntryType"))

                    If Not IsDBNull(dtDiaryDetails.Rows(0).Item("StartDate")) Then
                        txtStartDate.SelectedDate = Convert.ToDateTime(dtDiaryDetails.Rows(0).Item("StartDate"))
                    End If
                    If Not IsDBNull(dtDiaryDetails.Rows(0).Item("EndDate")) Then
                        txtEndDate.SelectedDate = Convert.ToDateTime(dtDiaryDetails.Rows(0).Item("EndDate"))
                    End If
                    txtExperience.Text = Convert.ToString(dtDiaryDetails.Rows(0).Item("Days"))
                    txtToilDays.Text = Convert.ToString(dtDiaryDetails.Rows(0).Item("TOILDays")) ''Added as part of #20530
                    txtTitle.Text = Convert.ToString(dtDiaryDetails.Rows(0).Item("Title"))
                    lblDesc.Text = Convert.ToString(dtDiaryDetails.Rows(0).Item("Description"))
                    lblLearningLevel.Text = Convert.ToString(dtDiaryDetails.Rows(0).Item("LearningLevel"))

                    txtEngagmentNumber.Text = Convert.ToString(dtDiaryDetails.Rows(0).Item("EngagementNumber"))

                    lblStatus.Text = Convert.ToString(dtDiaryDetails.Rows(0).Item("Status"))
                    ''added by dipali 28/04/2017
                    If (lblStatus.Text = "With Student") Then
                        btnLocknApprove.Visible = False
                        btnLocknApprove.Style.Add("display", "none")
                    Else
                        btnLocknApprove.Visible = True
                        btnLocknApprove.Style.Add("display", "inline-block")
                    End If
                    txtCompanyDays.Text = Convert.ToString(dtDiaryDetails.Rows(0).Item("CompanyAuditDays"))
                    txtOtherDays.Text = Convert.ToString(dtDiaryDetails.Rows(0).Item("OtherAuditDays"))
                    'Added for redmine log #20530
                    txtStatutoryDays.Text = Convert.ToString(dtDiaryDetails.Rows(0).Item("StatutoryAuditDays"))
                    txtOtherNIDays.Text = Convert.ToString(dtDiaryDetails.Rows(0).Item("OtherAuditNIDays"))
                    'Set Button Text based on Diary Entry Status 
                    If Convert.ToString(dtDiaryDetails.Rows(0).Item("Status")).ToLower() = "locked" Then
                        btnUnlockEntry.Visible = True
                        btnLocknApprove.Visible = False
                    Else
                        btnLocknApprove.Visible = True
                        btnUnlockEntry.Visible = False
                    End If
                    hdStudentID.Value = Convert.ToString(dtDiaryDetails.Rows(0).Item("StudentID"))
                End If
                LoadLeavesGrid()
                LoadCompetencyGrid()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

#End Region

#Region "Grid Events"
        'Handles Leave Grid Need Data Source
        Protected Sub grdLeave_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdLeave.NeedDataSource
            Try
                LoadLeavesGrid()
                'If ViewState("dtLeave") IsNot Nothing Then
                '    grdLeave.DataSource = CType(ViewState("dtLeave"), DataTable)
                '    'Else
                '    '    PanelLeave.Visible = False
                'End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        'Protected Sub gvCompetency_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles gvCompetency.ItemDataBound
        '    e.Item.CssClass = "MyGridNew"
        'End Sub

        'Protected Sub gvCompetency_DataBound(sender As Object, e As System.EventArgs) Handles gvCompetency.DataBound
        '    If e.Row.RowType = DataControlRowType.DataRow Then

        '    End If
        'End Sub


        'Handles Competency Grid Need Data Source
        Protected Sub gvCompetency_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvCompetency.NeedDataSource
            Try
                LoadCompetencyGrid()
                'If ViewState("dtCompetency") IsNot Nothing Then
                '    gvCompetency.DataSource = CType(ViewState("dtCompetency"), DataTable)
                '    'Else
                '    '    PanelCompetency.Visible = False
                'End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

#Region "Button Events"
        'Handles Cancel/Back Button Click
        Protected Sub btnCancelnBack_Click(sender As Object, e As System.EventArgs) Handles btnCancelnBack.Click
            Try
                Dim sPage As String = Request.QueryString("Page")
                Dim sID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(Convert.ToString(hdStudentID.Value))
                Dim strFilePath As String = DiaryEntrySummaryPage & "?Status=" & System.Web.HttpUtility.UrlEncode(sPage) & "&ID=" & System.Web.HttpUtility.UrlEncode(sID)
                Response.Redirect(strFilePath, False)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        'Handles Lock and Approve Button Click
        Protected Sub btnLocknApprove_Click(sender As Object, e As System.EventArgs) Handles btnLocknApprove.Click
            Try
                lblMessage.Text = ""
                Dim oStudentDiaryGE As AptifyGenericEntityBase
                oStudentDiaryGE = Me.AptifyApplication.GetEntityObject("StudentDiaryEntries__c", Convert.ToInt32(hdnCADiaryRecordID.Value))
                Dim sTransID As String = DataAction.BeginTransaction(IsolationLevel.Serializable, True)
                oStudentDiaryGE.SetValue("Status", "Locked")
                oStudentDiaryGE.SetValue("ReviewDate", New Date(Today.Year, Today.Month, Today.Day))
                Dim _error As String = ""
                Try
                    If oStudentDiaryGE.Save(False, _error) Then
                        DataAction.CommitTransaction(sTransID)
                        'lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentDiaryEntrySummary.SuccessrMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        'DisableControl()
                        'LoadDiaryDetails()
                        radWindowValidation.VisibleOnPageLoad = True
                        lblValidationMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentDiaryEntrySummary.SuccessrMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    Else
                        DataAction.RollbackTransaction(sTransID)
                    End If

                Catch ex As Exception
                    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)

                End Try
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        'Handles UnLock Entry Button Click
        Protected Sub btnUnlockEntry_Click(sender As Object, e As System.EventArgs) Handles btnUnlockEntry.Click
            Try
                Dim oStudentDiaryGE As AptifyGenericEntityBase
                oStudentDiaryGE = Me.AptifyApplication.GetEntityObject("StudentDiaryEntries__c", Convert.ToInt32(hdnCADiaryRecordID.Value))
                Dim sTransID As String = DataAction.BeginTransaction(IsolationLevel.Serializable, True)
                oStudentDiaryGE.SetValue("Status", "UnLocked")

                Dim _error As String = ""
                Try
                    If oStudentDiaryGE.Save(False, _error) Then
                        DataAction.CommitTransaction(sTransID)
                        'lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentDiaryEntrySummary.SuccMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        'DisableControl()
                        'LoadDiaryDetails()
                        radWindowValidation.VisibleOnPageLoad = True
                        lblValidationMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentDiaryEntrySummary.SuccMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    Else
                        DataAction.RollbackTransaction(sTransID)

                    End If
                Catch ex As Exception
                    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)

                End Try
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnValidationOK_Click(sender As Object, e As System.EventArgs) Handles btnValidationOK.Click
            radWindowValidation.VisibleOnPageLoad = False
            lblValidationMsg.Text = ""

            Dim sPage As String = Request.QueryString("Page")
            Dim sID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(Convert.ToString(hdStudentID.Value))
            Dim strFilePath As String = DiaryEntrySummaryPage & "?Status=" & System.Web.HttpUtility.UrlEncode(sPage) & "&ID=" & System.Web.HttpUtility.UrlEncode(sID)
            Response.Redirect(strFilePath, False)
        End Sub
#End Region

    End Class
End Namespace
