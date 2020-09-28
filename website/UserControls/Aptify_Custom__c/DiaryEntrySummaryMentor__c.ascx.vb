''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer            Date Created/Modified               Summary
'Kavita Zinage        09/22/2015                      Student Diary Summary Page
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
    Partial Class DiaryEntrySummaryMentor__c
        Inherits BaseUserControlAdvanced

        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "DiaryEntrySummaryMentor__c"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_DetailedDairyEntryPage As String = "DetailedDiaryEntryPages"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_DiaryEntrySummaryPage As String = "DiaryEntrySummaryPage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_MentorDashboardPage As String = "MentorDashboardPage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_FirmAdminDashboardPage As String = "FirmAdminDashboardPage"


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

        Public Overridable Property DetailedDiaryEntryPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_DetailedDairyEntryPage) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_DetailedDairyEntryPage))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_DetailedDairyEntryPage) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Public Overridable Property DiaryEntrySummaryPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_DiaryEntrySummaryPage) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_DiaryEntrySummaryPage))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_DiaryEntrySummaryPage) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Public Overridable Property MentorDashboardPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_MentorDashboardPage) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_MentorDashboardPage))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_MentorDashboardPage) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        Public Overridable Property FirmAdminDashboardPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_FirmAdminDashboardPage) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_FirmAdminDashboardPage))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_FirmAdminDashboardPage) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(Me.ID) Then
                Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            End If

            MyBase.SetProperties()
            If String.IsNullOrEmpty(LoginPage) Then
                LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage)
            End If
            If String.IsNullOrEmpty(DetailedDiaryEntryPage) Then
                DetailedDiaryEntryPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_DetailedDairyEntryPage)
            End If
            If String.IsNullOrEmpty(DiaryEntrySummaryPage) Then
                DiaryEntrySummaryPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_DiaryEntrySummaryPage)
            End If
            If String.IsNullOrEmpty(MentorDashboardPage) Then
                MentorDashboardPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_MentorDashboardPage)
            End If
            If String.IsNullOrEmpty(FirmAdminDashboardPage) Then
                FirmAdminDashboardPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_FirmAdminDashboardPage)
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
                lblMessage.Text = ""
                If (Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("Status")).ToLower = "lkd") And Request.QueryString("Status") IsNot Nothing Then
                    hdnStatus.Value = "'locked'"
                    TryCast(gvDiarySummary.MasterTableView.GetColumn("SelectAll"), GridTemplateColumn).Display = False
                    btnLocknApprove.Visible = False
                    TryCast(gvDiarySummary.MasterTableView.GetColumn("SelectTitle"), GridTemplateColumn).Display = False
                    btnLocknApprove.Visible = False
                ElseIf (Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("Status")).ToLower = "ip") And Request.QueryString("Status") IsNot Nothing Then
                    hdnStatus.Value = "'unlocked,with student'"
                    TryCast(gvDiarySummary.MasterTableView.GetColumn("SelectAll"), GridTemplateColumn).Display = False
                    btnLocknApprove.Visible = False
                    TryCast(gvDiarySummary.MasterTableView.GetColumn("SelectTitle"), GridTemplateColumn).Display = False
                    btnLocknApprove.Visible = False
                Else
                    hdnStatus.Value = "'submitted to mentor'"
                    TryCast(gvDiarySummary.MasterTableView.GetColumn("SelectAll"), GridTemplateColumn).Display = True
                    btnLocknApprove.Visible = True
                    TryCast(gvDiarySummary.MasterTableView.GetColumn("SelectTitle"), GridTemplateColumn).Display = True
                    btnLocknApprove.Visible = True
                End If
                If Request.QueryString("ID") IsNot Nothing Then
                    hdnStudentID.Value = Convert.ToString(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("ID")))
                End If
                If Request.QueryString("Page") IsNot Nothing Then
                    Session("Page") = Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("Page")).ToLower
                End If
                If Not IsPostBack Then
                    lblMessage.Text = ""
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
                Dim sSql As New StringBuilder()
                If Convert.ToString(Session("Page")) = "md" Then
                    sSql.AppendFormat("{0}..spGetStudentDiarySummary__c @PersonID={1},@Status={2},@Type={3},@MentorID={4}", _
                       Me.Database, Convert.ToInt32(hdnStudentID.Value), Convert.ToString(hdnStatus.Value), "'regular diary entry'", Convert.ToInt32(User1.PersonID))
                ElseIf Convert.ToString(Session("Page")) = "fd" Then
                    If Session("MentorID") IsNot Nothing Then
                        sSql.AppendFormat("{0}..spGetStudentDiarySummary__c @PersonID={1},@Status={2},@Type={3},@MentorID={4}", _
                          Me.Database, Convert.ToInt32(hdnStudentID.Value), Convert.ToString(hdnStatus.Value), "'regular diary entry'", Convert.ToInt32(Convert.ToString(Session("MentorID"))))
                    End If
                End If
                Dim dtDiarySummary As DataTable = DataAction.GetDataTable(sSql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dtDiarySummary Is Nothing Then
                    'AndAlso dtDiarySummary.Rows.Count > 0
                    gvDiarySummary.DataSource = dtDiarySummary
                    gvDiarySummary.DataBind()
                    gvDiarySummary.Visible = True
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

#Region "Grid Events"
        ''' <summary>
        ''' Handles need data source to load/reload grid 
        ''' </summary>
        Protected Sub gvDiarySummary_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvDiarySummary.NeedDataSource
            Try
                LoadGrid()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub


        Protected Sub gvDiarySummary_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles gvDiarySummary.ItemCommand
            Try
                Dim sPageName As String
                If e.CommandName = "Title" Then
                    If hdnStatus.Value = "'locked'" Then
                        sPageName = Aptify.Framework.Web.Common.WebCryptography.Encrypt("LKD")
                    ElseIf hdnStatus.Value = "'submitted to mentor'" Then
                        sPageName = Aptify.Framework.Web.Common.WebCryptography.Encrypt("STM")
                    Else
                        sPageName = Aptify.Framework.Web.Common.WebCryptography.Encrypt("IP")
                    End If

                    Dim sID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(e.CommandArgument.ToString())
                    Dim strFilePath As String = DetailedDiaryEntryPage & "?Page=" & System.Web.HttpUtility.UrlEncode(sPageName) & "&ID=" & System.Web.HttpUtility.UrlEncode(sID)
                    Response.Redirect(strFilePath, False)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub gvDiarySummary_PreRender(sender As Object, e As EventArgs) Handles gvDiarySummary.PreRender
            If (Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("Status")).ToLower = "lkd") And Request.QueryString("Status") IsNot Nothing Then
                TryCast(gvDiarySummary.MasterTableView.GetColumn("SelectAll"), GridTemplateColumn).Display = True
                 btnUnloack.Visible = True
                TryCast(gvDiarySummary.MasterTableView.GetColumn("SelectTitle"), GridTemplateColumn).Display = False
            ElseIf (Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("Status")).ToLower = "ip") And Request.QueryString("Status") IsNot Nothing Then
                TryCast(gvDiarySummary.MasterTableView.GetColumn("SelectAll"), GridTemplateColumn).Display = True
                btnUnloack.Visible = True
                TryCast(gvDiarySummary.MasterTableView.GetColumn("SelectTitle"), GridTemplateColumn).Display = False
            Else
                TryCast(gvDiarySummary.MasterTableView.GetColumn("SelectAll"), GridTemplateColumn).Display = True
                TryCast(gvDiarySummary.MasterTableView.GetColumn("SelectTitle"), GridTemplateColumn).Display = True
            End If

            If gvDiarySummary.MasterTableView.GetItems(GridItemType.NoRecordsItem).Count > 0 Then
                Dim norecordItem As GridNoRecordsItem = CType(gvDiarySummary.MasterTableView.GetItems(GridItemType.NoRecordsItem)(0), GridNoRecordsItem)
                Dim lblnorecordItem As Label = CType(norecordItem.FindControl("lblNoRecord"), Label)
                lblnorecordItem.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentDiaryEntrySummary.NoRecordMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                btnLocknApprove.Visible = False
            End If
        End Sub
#End Region

#Region "Button Click"

        Protected Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
            Try
                If Convert.ToString(Session("Page")) = "md" Then
                    Response.Redirect(MentorDashboardPage, False)
                ElseIf Convert.ToString(Session("Page")) = "fd" Then
                    Response.Redirect(FirmAdminDashboardPage, False)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnLocknApprove_Click(sender As Object, e As System.EventArgs) Handles btnLocknApprove.Click
            Try
                lblMessage.Text = ""
                Dim oStudentDiaryGE As AptifyGenericEntityBase
                Dim iChkCnt As Integer = 0
                Dim iCADiaryID As Integer
                For i As Integer = 0 To gvDiarySummary.Items.Count - 1
                    If CType(gvDiarySummary.Items(i).FindControl("chkSelectDiary"), CheckBox).Checked = True Then
                        iChkCnt = iChkCnt + 1
                        iCADiaryID = Convert.ToInt32(CType(gvDiarySummary.Items(i).FindControl("hdDiaryID"), HiddenField).Value)
                        CType(gvDiarySummary.Items(i).FindControl("chkSelectDiary"), CheckBox).Checked = False
                        oStudentDiaryGE = Me.AptifyApplication.GetEntityObject("StudentDiaryEntries__c", iCADiaryID)
                        Dim sTransID As String = DataAction.BeginTransaction(IsolationLevel.Serializable, True)
                        oStudentDiaryGE.SetValue("Status", "Locked")
                        oStudentDiaryGE.SetValue("ReviewDate", New Date(Today.Year, Today.Month, Today.Day))
                        Dim _error As String = ""
                        Try
                            If oStudentDiaryGE.Save(False, _error) Then
                                DataAction.CommitTransaction(sTransID)
                            Else
                                DataAction.RollbackTransaction(sTransID)
                            End If
                        Catch ex As Exception
                            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)

                        End Try
                    End If
                Next
                'Added uncheck SelectAll Check Box
                Dim ghi As GridHeaderItem = DirectCast(gvDiarySummary.MasterTableView.GetItems(GridItemType.Header)(0), GridHeaderItem)
                If DirectCast(ghi.FindControl("chkAllDiary"), CheckBox).Checked = True Then
                    DirectCast(ghi.FindControl("chkAllDiary"), CheckBox).Checked = False
                End If

                If iChkCnt > 0 Then
                    lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentDiaryEntrySummary.SuccessrMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    LoadGrid()
                Else
                    lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentDiaryEntrySummary.ValidationMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

#Region "CheckBox Event"
        ''' <summary>
        ''' To Select all check box
        ''' </summary>
        Protected Sub ToggleSelectedState(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Dim headerCheckBox As CheckBox = CType(sender, CheckBox)
                For Each dataItem As GridDataItem In gvDiarySummary.MasterTableView.Items
                    If headerCheckBox.Checked Then
                        lblMessage.Text = ""
                        CType(dataItem.FindControl("chkSelectDiary"), CheckBox).Checked = headerCheckBox.Checked
                    Else
                        CType(dataItem.FindControl("chkSelectDiary"), CheckBox).Checked = headerCheckBox.Checked
                    End If
                Next
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

 Protected Sub btnUnloack_Click(sender As Object, e As EventArgs) Handles btnUnloack.Click
            Try
                lblMessage.Text = ""
                Dim oStudentDiaryGE As AptifyGenericEntityBase
                Dim iChkCnt As Integer = 0
                Dim iCADiaryID As Integer
                For i As Integer = 0 To gvDiarySummary.Items.Count - 1
                    If CType(gvDiarySummary.Items(i).FindControl("chkSelectDiary"), CheckBox).Checked = True Then
                        iChkCnt = iChkCnt + 1
                        iCADiaryID = Convert.ToInt32(CType(gvDiarySummary.Items(i).FindControl("hdDiaryID"), HiddenField).Value)
                        CType(gvDiarySummary.Items(i).FindControl("chkSelectDiary"), CheckBox).Checked = False
                        oStudentDiaryGE = Me.AptifyApplication.GetEntityObject("StudentDiaryEntries__c", iCADiaryID)
                        Dim sTransID As String = DataAction.BeginTransaction(IsolationLevel.Serializable, True)
                        oStudentDiaryGE.SetValue("Status", "UnLocked")
                        oStudentDiaryGE.SetValue("ReviewDate", New Date(Today.Year, Today.Month, Today.Day))
                        Dim _error As String = ""
                        Try
                            If oStudentDiaryGE.Save(False, _error) Then
                                DataAction.CommitTransaction(sTransID)
                            Else
                                DataAction.RollbackTransaction(sTransID)
                            End If
                        Catch ex As Exception
                            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)

                        End Try
                    End If
                Next
                'Added uncheck SelectAll Check Box
                Dim ghi As GridHeaderItem = DirectCast(gvDiarySummary.MasterTableView.GetItems(GridItemType.Header)(0), GridHeaderItem)
                If DirectCast(ghi.FindControl("chkAllDiary"), CheckBox).Checked = True Then
                    DirectCast(ghi.FindControl("chkAllDiary"), CheckBox).Checked = False
                End If

                If iChkCnt > 0 Then
                    lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentDiaryEntrySummary.SuccMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    LoadGrid()
                Else
                    lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentDiaryEntrySummary.ValidationMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region
    End Class
End Namespace
