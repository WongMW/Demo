'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Pradip Chavhan               09/07/2015                        Six Month Mentor Create Page
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness
Imports Telerik.Web.UI
Namespace Aptify.Framework.Web.eBusiness
    Partial Class SixMonthlyReviewMentor__c
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_MentorDashboardPage As String = "MentorDashboardPage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_SixMonthlySummaryPage As String = "SixMonthlySummaryPage"

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

        Public Overridable Property SixMonthlySummaryPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_SixMonthlySummaryPage) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_SixMonthlySummaryPage))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_SixMonthlySummaryPage) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(LoginPage) Then
                LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage)
            End If
            If String.IsNullOrEmpty(MentorDashboardPage) Then
                MentorDashboardPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_MentorDashboardPage)
            End If

            If String.IsNullOrEmpty(SixMonthlySummaryPage) Then
                SixMonthlySummaryPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_SixMonthlySummaryPage)
            End If
        End Sub

#End Region

#Region "Page Events"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                SetProperties()
                '' Display Static Text On Page From Culture String
                divEvaluationSection.InnerHtml = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.SixMonthlySummaryMentor.EvaluationSection__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                divSuggestedPoint.InnerHtml = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.SixMonthlySummaryMentor.SuggestedPoints__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                divDevelopement1.InnerHtml = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.SixMonthlySummaryMentor.Developement1__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                divDevelopement2.InnerHtml = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.SixMonthlySummaryMentor.Developement2__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                divDevelopement3.InnerHtml = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.SixMonthlySummaryMentor.Developement3__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                divDeclaration.InnerHtml = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.SixMonthlySummaryMentor.Declaration__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                If User1.PersonID <= 0 Then
                    Session("ReturnToPage") = Request.RawUrl
                    Response.Redirect(LoginPage)
                End If
                If Not IsPostBack Then
                    ''"sm" used to load person details when user redirect from Summary page.
                    ''"md" used to load diary details when user redirect from Mentor Dashboard page.
                    If Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("Page")).ToLower = "sm" Then
                        LoadPersonDetails()
                    ElseIf Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("Page")).ToLower = "md" Then
                        hidDiaryID.Value = Convert.ToString(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("ID")))
                        LoadDiaryDetails(Convert.ToInt32(hidDiaryID.Value))
                    End If
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
        Private Sub LoadPersonDetails()
            Try
                Dim sSql As String = Database & "..spGetPersondetailstoCreateSixMonthlySummaryMentor__c @StudentId=" & Convert.ToString(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("ID")))
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    lblStudentName.Text = Convert.ToString(dt.Rows(0)("StudentName"))
                    lblTraineeStudentNumber.Text = Convert.ToString(dt.Rows(0)("TraineeStudentNumber"))
                    lblBueinessUnit.Text = Convert.ToString(dt.Rows(0)("Businessunit"))
                    lblRouteOfEntry.Text = Convert.ToString(dt.Rows(0)("RouteOfEntry"))
                    hidCompanyID.Value = Convert.ToString(dt.Rows(0)("CompanyID"))
                    hidRouteOfEntryID.Value = Convert.ToString(dt.Rows(0)("RouteOfEntryID"))
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Function To Load details of Existing Diary Entry
        ''' </summary>
        ''' <param name="id"></param>
        ''' <remarks></remarks>
        Private Sub LoadDiaryDetails(ByVal id As Integer)
            Try
                Dim sSql As String = Database & "..spGetStudentMentorDetails__c @StudentDairyID=" & Convert.ToString(id)
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    lblStudentName.Text = Convert.ToString(dt.Rows(0)("StudentName"))
                    lblTraineeStudentNumber.Text = Convert.ToString(dt.Rows(0)("TraineeStudentNumber"))
                    lblRouteOfEntry.Text = Convert.ToString(dt.Rows(0)("RouteOfEntry"))
                    If Convert.ToString(dt.Rows(0)("ReviewStartDate")) <> "" Then ' if condition added by GM for redmine #20276
                        txtStartDate.SelectedDate = Convert.ToString(dt.Rows(0)("ReviewStartDate"))
                    End If ' End If redmine #20276
                    txtEvaluationTitle.Text = Convert.ToString(dt.Rows(0)("MentorReviewTitle"))
                    txtEiditorDescription.Content = Convert.ToString(dt.Rows(0)("MentorComments"))
                    hidCompanyID.Value = Convert.ToString(dt.Rows(0)("CompanyID"))
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

#Region "Public Function"
        ''' <summary>
        ''' Function To Add Student Diary Record
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function AddStudentDiaryRecord(ByVal Status As String) As Boolean
            Try
                Dim sErrorMessage As String = String.Empty
                Dim StudentID As Integer = Convert.ToInt32(Convert.ToString(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("ID"))))
                Dim oStudentDiaryGE As AptifyGenericEntityBase
                oStudentDiaryGE = Me.AptifyApplication.GetEntityObject("StudentDiaryEntries__c", -1)
                With oStudentDiaryGE
                    .SetValue("StudentID", StudentID)
                    .SetValue("MentorID", Convert.ToInt32(User1.PersonID))
                    .SetValue("Status", Status)
                    .SetValue("Type", "6 Monthly Review")
                    If Convert.ToInt32(hidCompanyID.Value) > 0 Then
                        .SetValue("CompanyID", Convert.ToInt32(hidCompanyID.Value))
                    End If
                    If txtStartDate.SelectedDate IsNot Nothing Then
                        .SetValue("DateStarted", txtStartDate.SelectedDate)
                        .SetValue("ReviewDate", txtStartDate.SelectedDate)
                    End If
                    If txtEvaluationTitle.Text.Trim <> "" Then
                        .SetValue("MentorReviewTitle", txtEvaluationTitle.Text.Trim)
                    End If
                    If txtEiditorDescription.Text.Trim <> "" Then
                        .SetValue("MentorComments", txtEiditorDescription.Content.Trim)
                    End If
                    If Convert.ToInt32(hidRouteOfEntryID.Value) > 0 Then
                        .SetValue("RouteOfEntry", Convert.ToInt32(hidRouteOfEntryID.Value))
                    End If
                    If oStudentDiaryGE.Save(False, sErrorMessage) Then
                        hidDiaryID.Value = oStudentDiaryGE.RecordID
                        LoadDiaryDetails(Convert.ToInt32(hidDiaryID.Value))
                        Return True
                    Else
                        Return False
                    End If
                End With
                Return True
            Catch ex As System.Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
            Return True
        End Function

        ''' <summary>
        ''' Function To UPdate Student Diary Record
        ''' </summary>
        ''' <param name="DiaryID"></param>
        ''' <param name="Status"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function UpdateDiaryRecord(ByVal DiaryID As Integer, ByVal Status As String) As Boolean
            Try
                Dim sErrorMessage As String = String.Empty
                Dim oStudentDiaryGE As AptifyGenericEntityBase
                oStudentDiaryGE = Me.AptifyApplication.GetEntityObject("StudentDiaryEntries__c", DiaryID)
                With oStudentDiaryGE
                    .SetValue("Status", Status)
                    If txtStartDate.SelectedDate IsNot Nothing Then
                        .SetValue("DateStarted", txtStartDate.SelectedDate)
                        .SetValue("ReviewDate", txtStartDate.SelectedDate)

                    End If
                    If txtEvaluationTitle.Text.Trim <> "" Then
                        .SetValue("MentorReviewTitle", txtEvaluationTitle.Text.Trim)
                    End If
                    If txtEiditorDescription.Text.Trim <> "" Then
                        .SetValue("MentorComments", txtEiditorDescription.Content.Trim)
                    End If
                    If Convert.ToInt32(hidCompanyID.Value) > 0 Then
                        .SetValue("CompanyID", Convert.ToInt32(hidCompanyID.Value))
                    End If
                    If Convert.ToInt32(hidRouteOfEntryID.Value) > 0 Then
                        .SetValue("RouteOfEntry", Convert.ToInt32(hidRouteOfEntryID.Value))
                    End If

                    If oStudentDiaryGE.Save(False, sErrorMessage) Then
                        Return True
                    Else
                        Return False
                    End If
                End With
                Return True
            Catch ex As System.Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
            Return True
        End Function

        ''' <summary>
        ''' Function To Validate User Input
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Validate() As Boolean
            Try
                If txtStartDate.SelectedDate Is Nothing Then
                    rwValidation.VisibleOnPageLoad = True
                    lblValidationMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.SixMonthlySummaryMentor.ValidateStartDate__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    Return False
                End If
                If txtEvaluationTitle.Text.Trim = "" Then
                    rwValidation.VisibleOnPageLoad = True
                    lblValidationMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.SixMonthlySummaryMentor.ValidateMentorTitle__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    Return False
                End If
                If txtEiditorDescription.Text.Trim = "" Then
                    rwValidation.VisibleOnPageLoad = True
                    lblValidationMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.SixMonthlySummaryMentor.ValidateMentorDesc__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    Return False
                End If
                'If Convert.ToInt32(hidCompanyID.Value) <= 0 Then
                '    radWindowValidation.VisibleOnPageLoad = True
                '    lblValidationMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.SixMonthlySummaryMentor.ValidateCompany__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                '    Return False
                'End If
                Return True
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
        End Function

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
                ''"sm" redirect to  Summary page.
                ''"md" redirect to Mentor Dashboard page.
                If Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("Page")).ToLower = "sm" Then
                    Response.Redirect(SixMonthlySummaryPage, False)
                ElseIf Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("Page")).ToLower = "md" Then
                    Response.Redirect(MentorDashboardPage, False)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
            Try
                If Validate() Then
                    ''"sm" created diary entry when user redirect from summary page.
                    ''"md" update diary entry when user redirect from Mentor Dashboard page.
                    If Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("Page")).ToLower = "sm" Then
                        If Convert.ToInt32(hidDiaryID.Value) > 0 Then
                            If UpdateDiaryRecord(Convert.ToInt32(hidDiaryID.Value), "Submitted to Mentor") Then
                                rwValidation.VisibleOnPageLoad = True
                                lblValidationMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.SixMonthlySummaryMentor.RecordSaved__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            End If
                        Else
                            If StudentDairyAlreadySubmitted() Then 'If condition added by GM for Redmine #20168
                                If AddStudentDiaryRecord("Submitted to Mentor") Then
                                    rwValidation.VisibleOnPageLoad = True
                                    lblValidationMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.SixMonthlySummaryMentor.RecordSaved__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                End If
                            Else 'Else part added by GM for Redmine #20168
                                rwValidation.VisibleOnPageLoad = True
                                lblValidationMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.SixMonthlySummaryMentor.AlreadyExists")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            End If
                        End If
                    ElseIf Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("Page")).ToLower = "md" Then
                        If UpdateDiaryRecord(Convert.ToInt32(Convert.ToString(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("ID")))), "Submitted to Mentor") Then
                            rwValidation.VisibleOnPageLoad = True
                            lblValidationMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.SixMonthlySummaryMentor.RecordSaved__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        End If
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

            'Try
            '    If Validate() Then
            '        ''"sm" created diary entry when user redirect from summary page.
            '        ''"md" update diary entry when user redirect from Mentor Dashboard page.
            '        If Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("Page")).ToLower = "sm" Then
            '            If AddStudentDiaryRecord() Then
            '                radWindowValidation.VisibleOnPageLoad = True
            '                lblValidationMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.SixMonthlySummaryMentor.RecordSaved__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            '            End If
            '        ElseIf Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("Page")).ToLower = "md" Then
            '            If UpdateDiaryRecord(Convert.ToInt32(Convert.ToString(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("ID")))), "Submitted to Mentor") Then
            '                radWindowValidation.VisibleOnPageLoad = True
            '                lblValidationMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.SixMonthlySummaryMentor.RecordSaved__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            '            End If
            '        End If
            '    End If
            'Catch ex As Exception
            '    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            'End Try
        End Sub

        Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
            Try
                If Validate() Then
                    ''"sm" redirect to  Summary page.
                    ''"md" redirect to Mentor Dashboard page.
                    If Convert.ToInt32(hidDiaryID.Value) > 0 Then
                        If UpdateDiaryRecord(Convert.ToInt32(hidDiaryID.Value), "Locked") Then
                            If Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("Page")).ToLower = "sm" Then
                                Response.Redirect(SixMonthlySummaryPage, False)
                            ElseIf Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("Page")).ToLower = "md" Then
                                Response.Redirect(MentorDashboardPage, False)
                            End If
                        End If
                    Else
                        If AddStudentDiaryRecord("Locked") Then
                            If Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("Page")).ToLower = "sm" Then
                                Response.Redirect(SixMonthlySummaryPage, False)
                            ElseIf Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("Page")).ToLower = "md" Then
                                Response.Redirect(MentorDashboardPage, False)
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnValidationOK_Click(sender As Object, e As System.EventArgs) Handles btnValidationOK.Click
            rwValidation.VisibleOnPageLoad = False
            lblValidationMsg.Text = ""
        End Sub
        ''' <summary>
        ''' Redmine #20168
        ''' </summary>
        ''' <returns></returns>
        Private Function StudentDairyAlreadySubmitted() As Boolean
            Try
                Dim sSql As String = "..spCheckAlreadyCADairySubmitted__c @StudentID=" & User1.PersonID & ",@Type='6 Monthly Review'"
                Dim iCnt As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSql))
                If iCnt > 0 Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
        End Function
#End Region
    End Class
End Namespace
