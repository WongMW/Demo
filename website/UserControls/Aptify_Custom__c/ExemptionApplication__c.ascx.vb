'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Siddharth Kavitake         8/12/2014                            Exemption Application
'Govind Mande               9/9/2014                             Exemption Application functionality
'Siddharth Kavitake         12/5/2014                            Exemption Application functionality changes as per new design
'Siddharth Kavitake         19/1/2016                            Made changes in code for UAT item G3-1
'Sheela Jarali              07/5/2018                            Enrolment submission Successfull message change(CNM-10)
'Sheela Jarali              08/5/2018                            Display My Info Check box access(CNM-12)
'Sheela Jarali              11/5/2018                            Enrolment submission Successfull message change(CNM-10)-QA issue
'Sheela Jarali              14/5/2018                            Exemption Section display logic(CNM-9)
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Explicit On
Option Strict On

Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness
Imports Telerik.Web.UI

Partial Class ExemptionApplication__c
    Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_EXMPAPP As String = "ExemptionApplicationPage"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_REPORT As String = "ReportPage"
    'Added by sheela for Eligibility and Exemption Submisssion Message Change(CNM-10)
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_EEApplicationSubmission As String = "EEApplicationSubmissionURL"
    Dim sDeletedID As String = String.Empty
    Dim bIsSubmit As Boolean

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

    Public Overridable Property ExemptionApplicationPage() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_EXMPAPP) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_EXMPAPP))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_EXMPAPP) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    Public Overridable Property ReportPage() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_REPORT) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_REPORT))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_REPORT) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    Public Property CurrentEducationTable() As DataTable
        Get

            If Not ViewState("CurrentEducationTable") Is Nothing Then
                Return CType(ViewState("CurrentEducationTable"), DataTable)
            Else
                Dim dtCurrentEducationTable As New DataTable
                dtCurrentEducationTable.Columns.Add("ID")
                dtCurrentEducationTable.Columns.Add("EducationLevelID")
                dtCurrentEducationTable.Columns.Add("EducationLevel")
                dtCurrentEducationTable.Columns.Add("Institute")
                dtCurrentEducationTable.Columns.Add("InstituteID")
                dtCurrentEducationTable.Columns.Add("Year")
                dtCurrentEducationTable.Columns.Add("Degree")
                dtCurrentEducationTable.Columns.Add("DegreeID")
                dtCurrentEducationTable.Columns.Add("GPA")
                Return dtCurrentEducationTable
            End If
        End Get
        Set(ByVal value As DataTable)
            ViewState("CurrentEducationTable") = value
        End Set
    End Property

    'Added by sheela for Eligibility and Exemption Submisssion Message Change(CNM-10)
    Public Overridable Property EEApplicationSubmission() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_EEApplicationSubmission) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_EEApplicationSubmission))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_EEApplicationSubmission) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    Protected Overrides Sub SetProperties()
        If String.IsNullOrEmpty(LoginPage) Then
            LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage)
        End If
        If String.IsNullOrEmpty(ExemptionApplicationPage) Then
            ExemptionApplicationPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_EXMPAPP)
        End If
        If String.IsNullOrEmpty(ReportPage) Then
            ReportPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_REPORT)
        End If
        'Added by sheela for Eligibility and Exemption Submisssion Message Change(CNM-10)
        If String.IsNullOrEmpty(EEApplicationSubmission) Then
            EEApplicationSubmission = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_EEApplicationSubmission)
        End If
    End Sub

#End Region

#Region "Page Events"
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim IsExampID As Boolean 'code added by GM for Redmine #20626
        Try
            SetProperties()
            If AptifyEbusinessUser1.PersonID <= 0 Then
                Session("ReturnToPage") = Request.RawUrl
                Response.Redirect(LoginPage)
            End If
            lblMessage.Text = ""
            lblFirmText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectFirm")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            If Not IsPostBack Then
                If Request.QueryString("ExmpID") Is Nothing Then
                    pnlData.Visible = True
                    pnlDetails.Visible = False

                Else
                    PopulateDropDowns()
                    pnlData.Visible = False
                    pnlDetails.Visible = True
                    grdData.Visible = True
                    'Added aa part of #20593
                    Dim iEEId As Integer
                    If Int32.TryParse(Request.QueryString("ExmpID"), iEEId) Then
                        IsExampID = True 'code added by GM for Redmine #20626
                        ViewState("ExemptionApp") = Request.QueryString("ExmpID")
                    Else
                        IsExampID = True 'code added by GM for Redmine #20626
                        ViewState("ExemptionApp") = Convert.ToInt32(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("ExmpID")))
                    End If
                    'End of #20593
                    ''below if condition added by GM for Redmine #20649
                    If CInt(ViewState("ExemptionApp")) > 0 Then
                        ' code added by GM for Redmine #20626
                        Dim sSql As String = "..spCheckExempAppExists__c @PersonID=" & AptifyEbusinessUser1.PersonID & ",@ExempID=" & Convert.ToInt32(ViewState("ExemptionApp"))
                        Dim IsExists As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sSql))
                        If IsExists = False Then
                            Response.Redirect(ExemptionApplicationPage, False)
                        End If
                        ' end code for Redmine #20626
                    End If    ' end code for Redmine #20649
                   
                    CurrentEducationTable = Nothing
                    ''Commented By Pradip  2016-05-16 For Bug #13178
                    'LoadEducationDetails()
                    ''End Here Commented By Pradip  2016-05-16 For Bug #13178
                    LoadPersonalDetails() ' function added by govind M on 22nd July redmine issue 13654
                    LoadYear()
                    LoadRoutes()
                    LoadHeaderText()
                    LoadApplicationDetails()
                    LoadEducationDataTableDetails()
                    LoadExemptionGranted()
                    If (EduDetails.Visible = False) Then 'Added for Hide Remove button when status "Submitted to CIA"
                        btnRemove.Visible = False
                    End If
                End If
                'If Not ViewState("ExemptionApp") Is Nothing Then
                '    btnPrint.Enabled = True
                'Else
                '    btnPrint.Enabled = False
                'End If
                lblStudentEducationDocuments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExemptionApp.StudentDocuments")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblSubmitMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExemptionApp.StudentSubmit")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                'Added by sheela for Eligibility and Exemption Submisssion Message Change(CNM-10)
                lblSubmitWarning.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExemptionApp.SubmitWarningMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            End If
            'Commented by Sheela
            'If Session("IsSubmit") IsNot Nothing Then
            '    If Convert.ToString(Session("IsSubmit")) = "1" Then
            '        Session("IsSubmit") = "0"
            '        Dim ReportPageURL As String = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings("ReportPageURL"))
            '        Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "EE Application Report"))
            '        Dim rptParam As New AptifyCrystalReport__c
            '        rptParam.ReportID = ReportID
            '        rptParam.Param1 = Convert.ToString(Session("ExemptionApp"))
            '        Session("ExemptionApp") = "0"
            '        Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
            '        ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPageURL & "' )", True)
            '    End If
            'End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            ' code added by GM for Redmine #20626
            If IsExampID = True Then
                Response.Redirect(ExemptionApplicationPage, False)
            End If
            ' end code for Redmine #20626
        End Try
    End Sub
#End Region

#Region "Load Drop Downs"
    ''Commented By Pradip  2016-05-16 For Bug #13178
    'Private Sub LoadEducationDetails()
    '    Try
    '        Dim sSql As String = Database & "..spGetEducationLevel__c"
    '        Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
    '        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
    '            ddlEducationLevel.DataSource = dt
    '            ddlEducationLevel.DataTextField = "Name"
    '            ddlEducationLevel.DataValueField = "ID"
    '            ddlEducationLevel.DataBind()
    '            ddlEducationLevel.Items.Insert(0, "Select Education Level")


    '        End If
    '    Catch ex As Exception
    '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
    '    End Try
    'End Sub
    ''End Here Commented By Pradip  2016-05-16 For Bug #13178
    Private Sub LoadRoutes()
        Try
            Dim sSql As String = Database & "..spGetApplicationTypeForCACurriculumRoute__c"
            Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                ddlRoute.DataSource = dt
                ddlRoute.DataTextField = "Name"
                ddlRoute.DataValueField = "IDName"
                ddlRoute.DataBind()
                ddlRoute.Items.Insert(0, New System.Web.UI.WebControls.ListItem("Select one", "Select one|Select one"))
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Private Sub LoadYear()
        Try

            Dim year As Integer = Now.Year - 50 'Get Past 50 Year Data Added by 
            ddlReceivedYear.Items.Clear()
            ''Commented And Added BY Pradip 2016-05-16 For Redmine Bug #13178
            'For i As Integer = 0 To 50
            '    Dim newyear As Integer = year + i
            '    ddlReceivedYear.Items.Add(New WebControls.ListItem(newyear.ToString()))
            'Next
            For i As Integer = 50 To 0 Step -1
                Dim newyear As Integer = year + i
                ddlReceivedYear.Items.Add(New WebControls.ListItem(newyear.ToString()))
            Next
            ''End Here Commented And Added BY Pradip 2016-05-16 For Redmine Bug #13178
            ddlReceivedYear.Items.Insert(0, "Select year")
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
#End Region

#Region "Button Click"
    Protected Sub btnAddQualification_Click(sender As Object, e As System.EventArgs) Handles btnAddQualification.Click
        Try
            If ValidateQualificationData() Then
                lblErrorQualification.Text = ""
                Dim dtCurrentEducationTable As DataTable = CurrentEducationTable
                Dim drCurrentEducationTable As DataRow = dtCurrentEducationTable.NewRow()
                drCurrentEducationTable("ID") = -1
                ''Commented By Pradip  2016-05-16 For Bug #13178
                'drCurrentEducationTable("EducationLevelID") = ddlEducationLevel.SelectedValue
                'drCurrentEducationTable("EducationLevel") = ddlEducationLevel.SelectedItem.Text
                ''End Here Commented By Pradip  2016-05-16 For Bug #13178
                drCurrentEducationTable("Institute") = txtAwardingBody.Text
                If hdnAwardingBody.Value <> String.Empty AndAlso hdnAwardingBody.Value <> "0" Then
                    drCurrentEducationTable("InstituteID") = hdnAwardingBody.Value
                    hdnAwardingBody.Value = "0"
                Else
                    drCurrentEducationTable("InstituteID") = "-1"
                End If
                If Convert.ToString(ddlReceivedYear.SelectedItem).Trim.ToLower <> "Select year" Then
                    drCurrentEducationTable("Year") = Convert.ToString(ddlReceivedYear.SelectedItem)
                End If
                If hdnQualification.Value <> String.Empty AndAlso hdnQualification.Value <> "0" Then
                    drCurrentEducationTable("DegreeID") = hdnQualification.Value
                    hdnQualification.Value = "0"
                Else
                    drCurrentEducationTable("DegreeID") = "-1"
                End If
                drCurrentEducationTable("Degree") = txtQualifications.Text
                drCurrentEducationTable("GPA") = txtGrade.Text
                dtCurrentEducationTable.Rows.Add(drCurrentEducationTable)
                CurrentEducationTable = dtCurrentEducationTable
                LoadEducationDataTableDetails()
                lblErrorQualification.Text = ""
                lblErrorQualification.Text = "Education details saved successfully"
                lblErrorQualification.ForeColor = Drawing.Color.Green
                ClearFields()
                radWindowAddQualification.VisibleOnPageLoad = True 'Added by Abhishek 2-3-2015
            Else
                lblErrorQualification.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExemptionApp.IncompleteEducationalDetails")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblErrorQualification.ForeColor = Drawing.Color.Red
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub btnSaveExit_Click(sender As Object, e As System.EventArgs) Handles btnSaveExit.Click
        Try
            If Page.IsValid Then ' if condition added for redmine #17553
                If ViewState("SaveExitClick") Is Nothing Then
                    ViewState("SaveExitClick") = "Yes"
                    If DoSave() Then

                    End If
                End If
                Response.Redirect(ExemptionApplicationPage, False)
            End If



        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    'Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
    '    Try
    '        If DoSave() Then
    '            If Not ViewState("ExemptionApp") Is Nothing Then
    '                Dim oGE As AptifyGenericEntityBase
    '                oGE = AptifyApplication.GetEntityObject("ExemptionApplication__c", Convert.ToInt32(ViewState("ExemptionApp")))
    '                oGE.SetValue("StatusID", AptifyApplication.GetEntityRecordIDFromRecordName("ExemptionApplicationStatus__c", "Submitted to CAI"))
    '                oGE.Save()
    '                lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExemptionApp.Submitted")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
    '            Else
    '                lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExemptionApp.NotSubmitted")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
    '            End If
    '        End If
    '        'Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
    '    Catch ex As Exception
    '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
    '    End Try
    'End Sub

    Protected Sub btnSubmitOk_Click(sender As Object, e As System.EventArgs) Handles btnSubmitOk.Click
        Try
            radwindowSubmit.VisibleOnPageLoad = False
            btnSubmitOk.Enabled = False
            If Session("SubmitOKClick") Is Nothing Then
                Session("SubmitOKClick") = "Yes"
                If DoSave() Then
                    If Not ViewState("ExemptionApp") Is Nothing Then
                        Dim oGEExmpApp As AptifyGenericEntityBase
                        oGEExmpApp = AptifyApplication.GetEntityObject("ExemptionApplication__c", Convert.ToInt32(ViewState("ExemptionApp")))
                        oGEExmpApp.SetValue("SubmissionDate", "GetDate()")
                        oGEExmpApp.SetValue("StatusID", AptifyApplication.GetEntityRecordIDFromRecordName("ExemptionApplicationStatus__c", "Submitted to CAI"))
                        If oGEExmpApp.Save() Then
                            lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExemptionApp.Submitted")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            LoadHeaderText()
                            LoadApplicationDetails()
                            Session("IsSubmit") = "1"
                            Session("ExemptionApp") = ViewState("ExemptionApp")
                            bIsSubmit = True
                            'UpdateWebUserDetails()
                            Response.Redirect(ExemptionApplicationPage, False) 'Redirect to Exemption List Page
                        Else
                            lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExemptionApp.NotSubmitted")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        End If
                    Else
                        lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExemptionApp.NotSubmitted")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    End If
                End If
            Else
                Response.Redirect(ExemptionApplicationPage, False) 'Redirect to Exemption List Page
            End If
            If bIsSubmit Then
                Response.Redirect(ExemptionApplicationPage, False) 'Redirect to Exemption List Page
            End If

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Sub UpdateWebUserDetails()
        Try
            Dim sSQL As String = Database & "..spGetPersonWebUserDetails__c @PersonID=" & AptifyEbusinessUser1.PersonID
            Dim dt As DataTable = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                Dim oGEWebUser As AptifyGenericEntityBase
                oGEWebUser = AptifyApplication.GetEntityObject("Web Users", Convert.ToInt32(dt.Rows(0)("UserID")))
                oGEWebUser.SetValue("UserID", Convert.ToInt32(dt.Rows(0)("OldID")))
                oGEWebUser.Save()

            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Protected Sub btnPrint_Click(sender As Object, e As System.EventArgs) Handles btnPrint.Click
        If Not ViewState("ExemptionApp") Is Nothing Then
            'Dim sReportName As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt("printgrantedexemptions")
            'Dim sExempAppID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(ViewState("ExemptionApp").ToString())
            'ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPage & "?sReportName=" & System.Web.HttpUtility.UrlEncode(sReportName) & "&ExmAplID=" & System.Web.HttpUtility.UrlEncode(sExempAppID) & "')", True)

            Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "Granted Exemptions Report"))
            Dim rptParam As New AptifyCrystalReport__c
            rptParam.ReportID = ReportID
            rptParam.Param1 = Convert.ToString(AptifyEbusinessUser1.PersonID)
            'Convert.ToString(ViewState("ExemptionApp").ToString)
            Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPage & "' )", True)
        End If

    End Sub

    Protected Sub btnRemove_Click(sender As Object, e As System.EventArgs) Handles btnRemove.Click
        DeleteQualificationDetails()
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        Response.Redirect(ExemptionApplicationPage, False)
    End Sub

    Protected Sub btnNew_Click(sender As Object, e As System.EventArgs) Handles btnNew.Click
        Response.Redirect(Me.ExemptionApplicationPage & "?ExmpID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt("-1")), False)
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
        Try
            'Added by Deepika on 06/12/2017 for log #17563
            'To validate that all compulsory fields are entered or not.
            If Page.IsValid Then
                'End here
                Dim SelectedValue As String = ddlRoute.SelectedValue.Substring(ddlRoute.SelectedValue.IndexOf("|") + 1, ddlRoute.SelectedValue.Length - ddlRoute.SelectedValue.IndexOf("|") - 1).Trim()
                If txtFirm.Text.Trim = "" AndAlso String.Compare(SelectedValue, "ele", True) <> 0 AndAlso ddlRoute.SelectedValue.Trim <> "Select one|Select one" Then
                    '  lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExemptionApp.IncompleteFirmDetails")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    lblValidation.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExemptionApp.IncompleteFirmDetails")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    radWindowValidation.VisibleOnPageLoad = True
                ElseIf ddlRoute.SelectedValue.Trim = "Select one|Select one" Then
                    ' lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExemptionApp.IncompleteRouteDetails")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    lblValidation.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExemptionApp.IncompleteRouteDetails")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    radWindowValidation.VisibleOnPageLoad = True
                ElseIf grdEducationDetails.Rows.Count = 0 Then
                    lblValidation.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExemptionApp.EducationDetails")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    radWindowValidation.VisibleOnPageLoad = True
                Else
                    'Added by sheela for Eligibility and Exemption Submisssion Message Change(CNM-10)
                    radwindowSubmit.VisibleOnPageLoad = False
                    btnSubmitOk.Enabled = False
                    If DoSave() Then
                        If Not ViewState("ExemptionApp") Is Nothing Then
                            Dim oGEExmpApp As AptifyGenericEntityBase
                            oGEExmpApp = AptifyApplication.GetEntityObject("ExemptionApplication__c", Convert.ToInt32(ViewState("ExemptionApp")))
                            oGEExmpApp.SetValue("SubmissionDate", "GetDate()")
                            oGEExmpApp.SetValue("StatusID", AptifyApplication.GetEntityRecordIDFromRecordName("ExemptionApplicationStatus__c", "Submitted to CAI"))
                            If oGEExmpApp.Save() Then
                                lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExemptionApp.SuccessMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                'Commented by sheela for Eligibility and Exemption Submisssion Message Change(CNM-10)
                                'LoadHeaderText()
                                'LoadApplicationDetails()
                                Session("IsSubmit") = "1"
                                Session("ExemptionApp") = ViewState("ExemptionApp")
                                bIsSubmit = True
                                'UpdateWebUserDetails()
                                Response.Redirect(EEApplicationSubmission, False) 'Redirect to EE application confirmation page
                            Else
                                lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExemptionApp.NotSubmitted")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            End If
                        Else
                            lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExemptionApp.NotSubmitted")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        End If
                    End If
                    If bIsSubmit Then
                        Response.Redirect(EEApplicationSubmission, False) 'Redirect to EE application confirmation page
                    End If

                End If
                'Added by Deepika on 06/12/2017 for log #17563
            End If
            'End here
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub btnOk_Click(sender As Object, e As System.EventArgs) Handles btnOk.Click
        radWindowAddQualification.VisibleOnPageLoad = False
    End Sub

    Protected Sub btnNo_Click(sender As Object, e As System.EventArgs) Handles btnNo.Click
        radwindowSubmit.VisibleOnPageLoad = False
    End Sub
#End Region

#Region "Events"
    Protected Sub ddlRoute_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlRoute.SelectedIndexChanged
        Try
            RoutesInformation()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub txtFirm_TextChanged(sender As Object, e As System.EventArgs) Handles txtFirm.TextChanged
        Try
            If hdnCompanyId.Value <> "" AndAlso CDbl(hdnCompanyId.Value) > 0 Then
                Dim sSql As String = Database & "..spGetCompanyAddress__c @CompanyID=" & hdnCompanyId.Value
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    ViewState("AddressID") = dt.Rows(0)("AddressID").ToString
                    'Dim sCompanyAddress As String = ""
                    'sCompanyAddress = dt.Rows(0)("Line1").ToString()
                    'If dt.Rows(0)("Line2").ToString() <> "" Then
                    '    sCompanyAddress = sCompanyAddress + ", " + dt.Rows(0)("Line2").ToString()
                    'End If
                    'If dt.Rows(0)("Line3").ToString() <> "" Then
                    '    sCompanyAddress = sCompanyAddress + ", " + dt.Rows(0)("Line3").ToString()
                    'End If
                    'If dt.Rows(0)("Line4").ToString() <> "" Then
                    '    sCompanyAddress = sCompanyAddress + ", " + dt.Rows(0)("Line4").ToString()
                    'End If                    
                    ''Dim sCompanyAddress As String = dt.Rows(0)("StreetAddress").ToString
                    'Dim lastLetter As Char = sCompanyAddress(sCompanyAddress.Length - 1)
                    'Dim sCompanyName As String
                    'If lastLetter = "/" Then
                    '    sCompanyName = sCompanyAddress.Substring(0, sCompanyAddress.Length - 1)
                    '    If sCompanyName(sCompanyName.Length - 1) = "," Then
                    '        sCompanyName = sCompanyName.Substring(0, sCompanyName.Length - 2)
                    '    End If
                    'Else
                    '    sCompanyName = sCompanyAddress.Substring(0, sCompanyAddress.Length)
                    'End If
                    txtAddrsLine1.Text = dt.Rows(0)("Line1").ToString()
                    txtAddrsLine2.Text = dt.Rows(0)("Line2").ToString()
                    txtAddrsLine3.Text = dt.Rows(0)("Line3").ToString()
                    ViewState("AddressLine1") = dt.Rows(0)("Line1").ToString()
                    ViewState("AddressLine2") = dt.Rows(0)("Line2").ToString()
                    ViewState("AddressLine3") = dt.Rows(0)("Line3").ToString()
                    'txtFirmAddress.Text = sCompanyName
                    txtCity.Text = dt.Rows(0)("City").ToString
                    'txtFirmAddress.Enabled = False
                    txtAddrsLine1.Enabled = False
                    txtAddrsLine2.Enabled = False
                    txtAddrsLine3.Enabled = False
                    txtCity.Enabled = False
                Else
                    ' txtFirmAddress.Enabled = True
                    txtAddrsLine1.Enabled = True
                    txtAddrsLine2.Enabled = True
                    txtAddrsLine3.Enabled = True
                    txtCity.Enabled = True
                    'txtFirmAddress.Text = ""
                    txtAddrsLine1.Text = ""
                    txtAddrsLine2.Text = ""
                    txtAddrsLine3.Text = ""
                    txtCity.Text = ""
                End If
            Else
                ' txtFirmAddress.Enabled = True
                txtAddrsLine1.Enabled = True
                txtAddrsLine2.Enabled = True
                txtAddrsLine3.Enabled = True
                txtCity.Enabled = True
                'txtFirmAddress.Text = ""
                txtAddrsLine1.Text = ""
                txtAddrsLine2.Text = ""
                txtAddrsLine3.Text = ""
                txtCity.Text = ""
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub grdGrantedExempts_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdGrantedExempts.NeedDataSource
        Try
            LoadExemptionGranted()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub grdData_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdData.NeedDataSource
        Me.LoadGrid()
    End Sub
#End Region

#Region "Private Functions"
    Private Sub LoadGrid()
        Dim sSql As String = Database & "..spGetExemptionAppsForStudent__c @PersonID=" & AptifyEbusinessUser1.PersonID
        Dim dtExmpApp As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)

        If Not dtExmpApp Is Nothing AndAlso dtExmpApp.Rows.Count > 0 Then
            Dim dcolUrl As DataColumn = New DataColumn()
            dcolUrl.Caption = "DataNavigateUrl"
            dcolUrl.ColumnName = "DataNavigateUrl"
            dtExmpApp.Columns.Add(dcolUrl)
            Dim sNavigateURL As String = ""
            For Each rw As DataRow In dtExmpApp.Rows
                sNavigateURL = Me.ExemptionApplicationPage & "?ExmpID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(rw("ID").ToString()))
                rw("DataNavigateUrl") = sNavigateURL
            Next
            grdData.DataSource = dtExmpApp
            'lblExemptionsNotFound.Text = ""
            sSql = Database & "..spCheckAllowNewExemptionApp__c @PersonID=" & AptifyEbusinessUser1.PersonID
            Dim iAllow As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
            If iAllow > 0 Then
                btnNew.Visible = True
                lblaccessID.Visible = False
            Else
                btnNew.Visible = False
                lblaccessID.Visible = True
            End If
        Else
            grdData.Visible = False 'Hide unusefull Grid Line if Grid was blank.
            'lblExemptionsNotFound.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.PivotGrid.NoRecords")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            btnNew.Visible = True
            lblaccessID.Visible = False
        End If

        'Added by Abhishek 2-3-2015
        'Dim drDistinctRowsValue() As DataRow
        'drDistinctRowsValue = dtExmpApp.Select("StatusID IN(6,7)")
        'If drDistinctRowsValue.Length > 0 Then
        '    btnNew.Visible = True
        'Else
        '    btnNew.Visible = False
        'End If
    End Sub
    Private Sub PopulateDropDowns()
        Dim sSQL As String
        Dim dt As DataTable
        sSQL = AptifyApplication.GetEntityBaseDatabase("Addresses") &
                      "..spGetCountryListProfile__c"
        dt = DataAction.GetDataTable(sSQL)
        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
            cmbHomeCountry.DataSource = dt
            cmbHomeCountry.DataTextField = "Country"
            cmbHomeCountry.DataValueField = "ID"
            cmbHomeCountry.DataBind()
            cmbHomeCountry.Items.Insert(0, "Select Country")

            cmbCountryofOrigin.DataSource = dt
            cmbCountryofOrigin.DataTextField = "Country"
            cmbCountryofOrigin.DataValueField = "ID"
            cmbCountryofOrigin.DataBind()
            cmbCountryofOrigin.Items.Insert(0, "Select Country")
            ' SetComboValue(cmbHomeCountry, "Ireland")

        End If

        sSQL = AptifyApplication.GetEntityBaseDatabase("Prefix") & _
            "..spGetPrefixes"
        Dim dtPrefix As New DataTable
        dtPrefix = DataAction.GetDataTable(sSQL)
        Dim R As DataRow = dtPrefix.NewRow
        R("Prefix") = ""
        dtPrefix.Rows.InsertAt(R, 0)
        cmbSalutation.DataSource = dtPrefix
        cmbSalutation.DataTextField = "Prefix"
        cmbSalutation.DataValueField = "Prefix"
        cmbSalutation.DataBind()
    End Sub
    Private Sub PopulateState(ByRef cmbPopulateState As DropDownList, ByRef cmbCurrentCountry As DropDownList)
        Try
            Dim sSQL As String
            sSQL = Database & "..spGetStateList @CountryID=" & cmbCurrentCountry.SelectedValue.ToString
            cmbPopulateState.DataSource = DataAction.GetDataTable(sSQL)
            cmbPopulateState.DataTextField = "State"
            cmbPopulateState.DataValueField = "State"
            cmbPopulateState.DataBind()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Protected Sub cmbHomeCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbHomeCountry.SelectedIndexChanged
        PopulateState(cmbHomeState, cmbHomeCountry)
    End Sub
    ''' <summary>
    ''' 'Added by Govind Mande for redmine issue
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadPersonalDetails()
        Try
            Dim sSQL As String = Database & "..spGetPersonAddressDetails__c @PersonID=" & AptifyEbusinessUser1.PersonID
            Dim dt As DataTable = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                cmbGender.SelectedValue = Convert.ToString(dt.Rows(0)("Gender"))
                'txtPreferredSalutation.Text = Convert.ToString(dt.Rows(0)("PreferredSalutation__c"))
                SetComboValue(cmbSalutation, Convert.ToString(dt.Rows(0)("Prefix")).Trim)
                txtHomeAddressLine1.Text = Convert.ToString(dt.Rows(0)("Line1"))
                txtHomeAddressLine2.Text = Convert.ToString(dt.Rows(0)("Line2"))
                txtHomeAddressLine3.Text = Convert.ToString(dt.Rows(0)("Line3"))
                txtHomeCity.Text = Convert.ToString(dt.Rows(0)("City"))
                txtHomeZipCode.Text = Convert.ToString(dt.Rows(0)("PostalCode"))
                txtHomeCounty.Text = Convert.ToString(dt.Rows(0)("County"))

                SetComboValue(cmbHomeCountry, IIf(Convert.ToString(dt.Rows(0)("Country")) = "", "Ireland", Convert.ToString(dt.Rows(0)("Country"))).ToString)
                SetComboValue(cmbCountryofOrigin, IIf(Convert.ToString(dt.Rows(0)("CountryOfOrigin__c")) = "", "Select Country", Convert.ToString(dt.Rows(0)("CountryOfOrigin__c"))).ToString) ' upated line for Redmine #17553
                'PopulateState(cmbHomeState, cmbHomeCountry)
                '  SetComboValue(cmbHomeState, Convert.ToString(dt.Rows(0)("StateProvince")))
                txtIntlCode.Text = Convert.ToString(dt.Rows(0)("CellCountryCode"))
                txtPhoneAreaCode.Text = Convert.ToString(dt.Rows(0)("CellAreaCode"))
                txtPhone.Text = Convert.ToString(dt.Rows(0)("CellPhone"))
                'SetComboValue(cmbGender, Convert.ToString(dt.Rows(0)("Gender")))
                'lblSalutation.Text = "Dear " & Convert.ToString(dt.Rows(0)("FirstLast"))
                'lblGender.Text = Convert.ToString(dt.Rows(0)("Gender"))
                'lblHomeAddressline1.Text = Convert.ToString(dt.Rows(0)("Line1"))
                'lblHomeAddressline2.Text = Convert.ToString(dt.Rows(0)("Line2"))
                'lblHomeAddressline3.Text = Convert.ToString(dt.Rows(0)("Line3"))
                'lblHomeAddressCity.Text = Convert.ToString(dt.Rows(0)("City"))
                'lblHomeAddressState.Text = Convert.ToString(dt.Rows(0)("StateProvince"))
                'lblHomeAddressCountry.Text = Convert.ToString(dt.Rows(0)("Country"))
            Else
                SetComboValue(cmbHomeCountry, "Ireland")
                SetComboValue(cmbCountryofOrigin, "Select Country") ' updated line for #17553
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Sub SetComboValue(ByRef cmb As System.Web.UI.WebControls.DropDownList, _
                                  ByVal sValue As String)
        Dim i As Integer

        Try
            For i = 0 To cmb.Items.Count - 1
                If String.Compare(cmb.Items(i).Value, sValue, True) = 0 Then
                    cmb.Items(i).Selected = True
                    Exit Sub
                End If
                '11/27/07,Added by Tamasa,Issue 5222.
                If String.Compare(cmb.Items(i).Text, sValue, True) = 0 Then
                    cmb.Items(i).Selected = True
                    Exit Sub
                End If
                'End
            Next
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Private Sub LoadHeaderText()
        Try
            lblFirstLast.Text = AptifyEbusinessUser1.FirstName + " " + AptifyEbusinessUser1.LastName
            Dim OldId As String = String.Empty
            Dim sSql As String = Database & "..spGetPerosnOldIdbyPersonId__c"
            Dim oParams1(0) As IDataParameter
            oParams1(0) = Me.DataAction.GetDataParameter("@Id", SqlDbType.BigInt, AptifyEbusinessUser1.PersonID)
            Dim dtoldID As DataTable = Me.DataAction.GetDataTableParametrized(sSql, CommandType.StoredProcedure, oParams1)
            If Not dtoldID Is Nothing AndAlso dtoldID.Rows.Count > 0 Then
                lblStudentNumber.Text = dtoldID.Rows(0)("OldID").ToString()
            End If

            ' lblStudentNumber.Text = CStr(AptifyEbusinessUser1.PersonID)
            ''Commented By Pradip 2016-01-07 For Group 3 Tracker G3-9
            'Dim sSqlAcademicCycle As String = Database & "..spGetAcademicYearForEEApp__c"
            'Dim dt As DataTable = DataAction.GetDataTable(sSqlAcademicCycle, IAptifyDataAction.DSLCacheSetting.BypassCache)
            'If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
            '    lblAcademicCycle.Text = dt.Rows(0)("Name").ToString()
            'End If
            ''Added By Pradip 2016-01-07 For Group 3 Tracker G3-9
            Dim oParams(0) As IDataParameter
            oParams(0) = Me.DataAction.GetDataParameter("@StudentID", SqlDbType.BigInt, AptifyEbusinessUser1.PersonID)
            Dim sSqlAcademicCycle As String = Database & "..spGetNextAcademicYearForEEApp__c"
            Dim dt As DataTable = Me.DataAction.GetDataTableParametrized(sSqlAcademicCycle, CommandType.StoredProcedure, oParams)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                lblAcademicCycle.Text = dt.Rows(0)("Name").ToString()
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Private Sub LoadApplicationDetails()
        Try
            txtFirm.Text = "Not known"
            Dim sSql As String = Database & "..spGetExemptionAppDetailsByID__c @ID=" & ViewState("ExemptionApp").ToString()

            Dim dtExmpDetails As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dtExmpDetails Is Nothing AndAlso dtExmpDetails.Rows.Count > 0 Then
                'ViewState("ExemptionApp") = Convert.ToInt32(dt.Rows(0)("ID"))   

                lblStudentNumber.Text = Convert.ToString(dtExmpDetails.Rows(0)("OLDID"))
                lblAcademicCycle.Text = Convert.ToString(dtExmpDetails.Rows(0)("AcademicCycle"))
                ViewState("AcademicCycle") = Convert.ToString(dtExmpDetails.Rows(0)("AcademicCycle"))
                lblStatus.Text = Convert.ToString(dtExmpDetails.Rows(0)("Status"))
                lblComments.Text = Convert.ToString(dtExmpDetails.Rows(0)("Comments"))
                If Convert.ToString(dtExmpDetails.Rows(0)("RejectReason")) <> String.Empty Then
                    lblRejectReason.Text = "Reject Reason: " & Convert.ToString(dtExmpDetails.Rows(0)("RejectReason"))
                Else
                    lblRejectReason.Text = String.Empty
                End If
                ddlRoute.SelectedValue = Convert.ToString(dtExmpDetails.Rows(0)("RouteOfEntry"))
                RoutesInformation()
                hdnCompanyId.Value = Convert.ToString(dtExmpDetails.Rows(0)("CompanyID"))
                If Not dtExmpDetails.Rows(0)("CompanyName") Is Nothing AndAlso dtExmpDetails.Rows(0)("CompanyName").ToString() <> String.Empty Then
                    txtFirm.Text = Convert.ToString(dtExmpDetails.Rows(0)("CompanyName"))
                Else
                    txtFirm.Text = "Not known"
                End If
                'Dim sStreetAddress As String = ""
                'sStreetAddress = dtExmpDetails.Rows(0)("Line1").ToString()
                'If dtExmpDetails.Rows(0)("Line2").ToString() <> "" Then
                '    sStreetAddress = sStreetAddress + ", " + dtExmpDetails.Rows(0)("Line2").ToString()
                'End If
                'If dtExmpDetails.Rows(0)("Line3").ToString() <> "" Then
                '    sStreetAddress = sStreetAddress + ", " + dtExmpDetails.Rows(0)("Line3").ToString()
                'End If
                'If dtExmpDetails.Rows(0)("Line4").ToString() <> "" Then
                '    sStreetAddress = sStreetAddress + ", " + dtExmpDetails.Rows(0)("Line4").ToString()
                'End If
                'txtFirmAddress.Text = Convert.ToString(dtExmpDetails.Rows(0)("StreetAddress"))
                'ViewState("Address") = sStreetAddress
                'txtFirmAddress.Text = sStreetAddress
                ViewState("AddressLine1") = dtExmpDetails.Rows(0)("Line1").ToString()
                ViewState("AddressLine2") = dtExmpDetails.Rows(0)("Line2").ToString()
                ViewState("AddressLine3") = dtExmpDetails.Rows(0)("Line3").ToString()
                txtAddrsLine1.Text = dtExmpDetails.Rows(0)("Line1").ToString()
                txtAddrsLine2.Text = dtExmpDetails.Rows(0)("Line2").ToString()
                txtAddrsLine3.Text = dtExmpDetails.Rows(0)("Line3").ToString()
                txtCity.Text = Convert.ToString(dtExmpDetails.Rows(0)("City"))
                If Convert.ToBoolean(dtExmpDetails.Rows(0)("ShareMyInfoWithFirm")) = True Then
                    chkInfoToFirm.Checked = True
                Else
                    chkInfoToFirm.Checked = False
                End If
                If Convert.ToInt32(dtExmpDetails.Rows(0)("StatusID")) >= 0 AndAlso Convert.ToInt32(dtExmpDetails.Rows(0)("StatusID")) <> AptifyApplication.GetEntityRecordIDFromRecordName("ExemptionApplicationStatus__c", "With Student") Then
                    EnabledAllFileds()
                End If
                If (Convert.ToString(dtExmpDetails.Rows(0)("Status__c")).ToUpper() = "APPROVED" Or Convert.ToString(dtExmpDetails.Rows(0)("Status__c")).ToUpper() = "REJECTED") Then
                    'txtFirmAddress.Enabled = False
                    txtAddrsLine1.Enabled = False
                    txtAddrsLine2.Enabled = False
                    txtAddrsLine3.Enabled = False
                    txtCity.Enabled = False
                End If
                ''Added BY Pradip 2016-05-27 For Issue ID https://redmine.softwaredesign.ie/issues/13701
                If (Convert.ToString(dtExmpDetails.Rows(0)("StatusName")).ToUpper() = "APPROVED") Then
                    grdEducationDetails.Columns(4).Visible = False
                    btnRemove.Visible = False
                    btnPrintForm.Visible = True
                    lblPopups.Visible = True
                End If
                If Convert.ToInt32(dtExmpDetails.Rows(0)("StatusID")) = AptifyApplication.GetEntityRecordIDFromRecordName("ExemptionApplicationStatus__c", "Approved") OrElse Convert.ToInt32(dtExmpDetails.Rows(0)("StatusID")) = AptifyApplication.GetEntityRecordIDFromRecordName("ExemptionApplicationStatus__c", "Rejected ") Then
                    btnPrintForm.Visible = False
                    lblPopups.Visible = False
                    ViewState("ApprovedRejectedStatus") = "yes"
                    'btnPrint.Visible = True
                    ' Below code commented for Redmine #19800 and changes added in design file
                    ' below if condition Code added by GM for this Redmine #19800
                    'If Convert.ToInt32(dtExmpDetails.Rows(0)("StatusID")) = AptifyApplication.GetEntityRecordIDFromRecordName("ExemptionApplicationStatus__c", "Approved") Then
                    'btnPrintForm.Visible = True
                    'End If
                    'End #19800
                End If
                If Convert.ToInt32(dtExmpDetails.Rows(0)("StatusID")) = AptifyApplication.GetEntityRecordIDFromRecordName("ExemptionApplicationStatus__c", "With Student") Then
                    btnPrintForm.Visible = False
                    lblPopups.Visible = False
                End If
                '' Added by Swati
                If Convert.ToInt32(dtExmpDetails.Rows(0)("PersonID")) = AptifyEbusinessUser1.PersonID Then

                Else
                    Response.Redirect(ExemptionApplicationPage, True)
                End If
                'End Swati 
            Else
                btnPrintForm.Visible = False
                lblPopups.Visible = False
            End If
            If txtFirm.Enabled = True AndAlso txtFirm.Text = "Not known" Then
                lblFirmText.Visible = True
                ' Redmine #16179
                Dim sSQLCompanyDetails As String = Database & "..spGetEEApprovedCompanyDetails__c @PersonID=" & AptifyEbusinessUser1.PersonID
                Dim dt As DataTable = DataAction.GetDataTable(sSQLCompanyDetails, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    hdnCompanyId.Value = Convert.ToString(dt.Rows(0)("CompanyID"))
                    If Not dt.Rows(0)("CompanyName") Is Nothing AndAlso dt.Rows(0)("CompanyName").ToString() <> String.Empty Then
                        txtFirm.Text = Convert.ToString(dt.Rows(0)("CompanyName"))
                    Else
                        txtFirm.Text = "Not known"
                    End If

                    ViewState("AddressLine1") = dt.Rows(0)("Line1").ToString()
                    ViewState("AddressLine2") = dt.Rows(0)("Line2").ToString()
                    ViewState("AddressLine3") = dt.Rows(0)("Line3").ToString()
                    txtAddrsLine1.Text = dt.Rows(0)("Line1").ToString()
                    txtAddrsLine2.Text = dt.Rows(0)("Line2").ToString()
                    txtAddrsLine3.Text = dt.Rows(0)("Line3").ToString()
                    txtCity.Text = Convert.ToString(dt.Rows(0)("City"))
                End If
            Else
                lblFirmText.Visible = False
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Private Sub LoadExemptionGranted()
        Try
            lblGrantExemptedMsg.Text = ""
            'Added by Deepika on 09/11/2017 for Bug #16901 
            lblExternalCertificateMsg.Text = ""
            Dim ExemptionCount As Int32 = 0
            Dim ExternalCount As Int32 = 0
            'End Here
            'Dim sSql As String = Database & "..spGetExemptionGrantedDetails__c @ExmpAppID=" & Convert.ToInt32(ViewState("ExemptionApp"))
            Dim sSql As String = Database & "..spGetCertificatesForStudEnroll__c @StudentID=" & AptifyEbusinessUser1.PersonID
            Dim dtExmpDetails As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)

            If Not dtExmpDetails Is Nothing AndAlso dtExmpDetails.Rows.Count > 0 Then
                grdGrantedExempts.DataSource = dtExmpDetails.Select("Type='Exemption'") ' Added by Govind M for Redmine #14335
                grdGrantedExempts.DataBind() ' Added by Govind M for Redmine #14335
                grdGrantedExempts.Visible = True ' Added by Govind M for Redmine #14335
                Dim drExternal() As DataRow = dtExmpDetails.Select("Type='External'") ' Added by Govind M for Redmine #14335
                If Not drExternal Is Nothing AndAlso drExternal.Length > 0 Then ' Added by Govind M for Redmine #14335
                    grdExternalPassed.DataSource = dtExmpDetails.Select("Type='External'") ' Added by Govind M for Redmine #14335
                    grdExternalPassed.DataBind() ' Added by Govind M for Redmine #14335
                    grdExternalPassed.Visible = True ' Added by Govind M for Redmine #14335
                End If
                'Added by Deepika on 09/11/2017 for Bug #16901 
                For i = 0 To dtExmpDetails.Rows.Count - 1
                    If dtExmpDetails.Rows(i).Item("Type").ToString().Trim() = "Exemption" Then
                        ExemptionCount += 1
                    End If
                    If dtExmpDetails.Rows(i).Item("Type").ToString().Trim() = "External" Then
                        ExternalCount += 1
                    End If
                Next i

                If dtExmpDetails.Rows.Count = ExternalCount Then
                    lblGrantExemptedMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExemptionApp.ExemptionGranted")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                End If
                If dtExmpDetails.Rows.Count = ExemptionCount Then
                    lblExternalCertificateMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExemptionApp.ExternalCertificate")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                End If
                'End Here
                '' grdGrantedExempts.DataSource = dtExmpDetails ' Commented by Govind M for Redmine #14335
                ''grdGrantedExempts.DataBind() ' Commented by Govind M for Redmine #14335
                '' grdGrantedExempts.Visible = True ' Commented by Govind M for Redmine #14335

                hdnExemptionsGrantedState.Value = "1"
                'btnPrintForm.Visible = False
            Else
                grdGrantedExempts.Visible = False
                'Added by Sheela as part of CNM-9:Exemption Section display logic 
                dvExceptionGranted.Visible = False
                lblGrantExemptedMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExemptionApp.ExemptionGranted")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                'Added by Deepika on 09/11/2017 for Bug #16901 
                lblExternalCertificateMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExemptionApp.ExternalCertificate")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                'End Here
            End If
            If Convert.ToString(ViewState("ApprovedRejectedStatus")) = "yes" Then
                btnPrint.Visible = True
            Else
                btnPrint.Visible = False
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Private Sub ClearFields()
        Try
            LoadYear()
            ''Commented By Pradip  2016-05-16 For Bug #13178
            'LoadEducationDetails()
            ''End Here Commented By Pradip  2016-05-16 For Bug #13178
            txtAwardingBody.Text = ""
            txtQualifications.Text = ""
            txtGrade.Text = ""
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Private Sub EnabledAllFileds()
        Try
            EduDetails.Visible = False

            ''Commented By Pradip  2016-05-16 For Bug #13178
            'ddlEducationLevel.Enabled = False
            ''End Here Commented By Pradip  2016-05-16 For Bug #13178
            ddlReceivedYear.Enabled = False
            ddlRoute.Enabled = False
            txtGrade.Enabled = False
            txtFirm.Enabled = False
            'txtFirmAddress.Enabled = False
            txtAddrsLine1.Enabled = False
            txtAddrsLine2.Enabled = False
            txtAddrsLine3.Enabled = False
            txtCity.Enabled = False
            chkInfoToFirm.Enabled = False
            btnAddQualification.Enabled = False
            btnSubmit.Visible = False
            'Added by Sheela
            lblSubmitWarning.Visible = False
            btnSaveExit.Visible = False
            btnRemove.Enabled = False
            grdEducationDetails.Enabled = False
            lblPopups2.Visible = False

            ' txtPreferredSalutation.Enabled = False
            cmbSalutation.Enabled = False
            cmbGender.Enabled = False
            txtHomeAddressLine1.Enabled = False
            txtHomeAddressLine2.Enabled = False
            txtHomeAddressLine3.Enabled = False
            txtHomeCity.Enabled = False
            cmbHomeState.Enabled = False
            txtHomeCounty.Enabled = False
            txtHomeZipCode.Enabled = False
            cmbHomeCountry.Enabled = False
            txtIntlCode.Enabled = False
            txtPhoneAreaCode.Enabled = False
            txtPhone.Enabled = False
            cmbCountryofOrigin.Enabled = False
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Private Sub RoutesInformation()
        Try

            Dim SelectedValue As String = ddlRoute.SelectedValue.Substring(ddlRoute.SelectedValue.IndexOf("|") + 1, ddlRoute.SelectedValue.Length - ddlRoute.SelectedValue.IndexOf("|") - 1).Trim()
            autoCompany.ContextKey = "0"
            If String.Compare(SelectedValue, "con", True) = 0 OrElse String.Compare(SelectedValue, "mas", True) = 0 Then
                chkInfoToFirm.Checked = True
                chkInfoToFirm.Enabled = False
                lblRoutesMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExemptionApp.ContractOrMasterRoutes")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                '  AddedControl Byte pradip 2016-05-27 for E&E application - Contract and masters route is selected then need to display RTO firms only
                autoCompany.ContextKey = "1"
            ElseIf String.Compare(SelectedValue, "ele", True) <> 0 Then
                lblRoutesMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExemptionApp.OtherrRoutes")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                chkInfoToFirm.Checked = False
                chkInfoToFirm.Enabled = True
            ElseIf String.Compare(SelectedValue, "ele", True) = 0 Then
                lblRoutesMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExemptionApp.ElevationRoutes")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                chkInfoToFirm.Checked = False
                'Modified by Sheela for Display My Info Check box access(CNM-12)
                chkInfoToFirm.Enabled = False
            Else
                lblRoutesMessage.Text = ""
                chkInfoToFirm.Checked = False
                chkInfoToFirm.Enabled = True
            End If
            If String.Compare(SelectedValue, "und", True) = 0 OrElse Convert.ToString(ddlRoute.SelectedItem).Trim.ToLower = "select one" Then
                lblRoutesMessage.Text = ""
                chkInfoToFirm.Checked = False
                chkInfoToFirm.Enabled = True
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Private Sub FieldsVisible(ByVal IsDisplay As Boolean)
        Try
            If IsDisplay Then
                'idInstitute.Visible = True
                'idYear.Visible = True
                'idDegree.Visible = True
                ''idOther.Visible = True
                'idGrade.Visible = True
                'idAddQuali.Visible = True
            Else
                'idInstitute.Visible = False
                'idYear.Visible = False
                'idDegree.Visible = False
                ''idOther.Visible = False
                'idGrade.Visible = False
                'idAddQuali.Visible = False
                ''grdEducationDetails.Visible = False
            End If

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub


    Private Function ValidateQualificationData() As Boolean
        Dim bResult As Boolean = True
        'ddlEducationLevel.SelectedValue = "Select Education Level" Or 
        If String.IsNullOrEmpty(txtAwardingBody.Text) Or String.IsNullOrEmpty(txtQualifications.Text) Or ddlReceivedYear.SelectedValue = "Select year" Or String.IsNullOrEmpty(txtGrade.Text) Then
            bResult = False
        End If
        Return bResult
    End Function
    Private Sub DoPersonSave()
        Try
            Dim sCounty As String = ""
            Me.DoPostalCodeLookup(txtHomeZipCode.Text, CStr(IIf(cmbHomeCountry.SelectedIndex >= 0, cmbHomeCountry.SelectedValue, "")), sCounty, txtHomeCity, cmbHomeState)

            'Added by Deepika on 06/12/2017 for log #17563
            ' To save all the person's details 
            Dim oGEPerson As AptifyGenericEntityBase
            oGEPerson = AptifyApplication.GetEntityObject("Persons", AptifyEbusinessUser1.PersonID)
            With oGEPerson
                .SetValue("HomeAddressLine1", txtHomeAddressLine1.Text)
                .SetValue("HomeAddressLine2", txtHomeAddressLine2.Text)
                .SetValue("HomeAddressLine3", txtHomeAddressLine3.Text)
                .SetValue("HomeCity", txtHomeCity.Text)

                .SetValue("HomeState", CStr(IIf(cmbHomeState.SelectedIndex >= 0, cmbHomeState.SelectedValue, "")))
                .SetValue("HomeZipCode", txtHomeZipCode.Text)
                .SetValue("HomeCountryCodeID", CLng(IIf(cmbHomeCountry.SelectedIndex >= 0, cmbHomeCountry.SelectedItem.Value, ""))) '11/27/07,Added by Tamasa,Issue 5222.
                .SetValue("HomeCountry", CStr(IIf(cmbHomeCountry.SelectedIndex >= 0, cmbHomeCountry.SelectedValue, "")))
                .SetAddValue("HomeCounty", sCounty)
                .SetValue("HomeCounty", txtHomeCounty.Text)


                ' AptifyEbusinessUser1.SetValue("PreferredSalutation__c", txtPreferredSalutation.Text)
                'AptifyEbusinessUser1.SetValue("Prefix", txtPreferredSalutation.Text)
                .SetValue("Prefix", CStr(IIf(cmbSalutation.SelectedIndex >= 0, cmbSalutation.SelectedValue, "")))
                .SetValue("Gender", CStr(IIf(cmbGender.SelectedIndex >= 0, cmbGender.SelectedValue, "")))
                .SetValue("CellCountryCode", txtIntlCode.Text)
                .SetValue("CellAreaCode", txtPhoneAreaCode.Text)
                .SetValue("CellPhone", txtPhone.TextWithLiterals)
                .SetValue("CountryOfOrigin__c", CLng(IIf(cmbCountryofOrigin.SelectedIndex >= 0, cmbCountryofOrigin.SelectedItem.Value, "")))
                oGEPerson.Save()
            End With
            'End here 
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Protected Overridable Sub DoPostalCodeLookup(ByRef PostalCode As String, ByRef Country As String, ByRef County As String, ByRef txt As TextBox, ByRef cmb As DropDownList)
        Dim sAreaCode As String = Nothing, sCounty As String = Nothing, sCity As String = Nothing
        Dim sState As String = Nothing, sCongDist As String = Nothing, sCountry As String = Nothing
        Dim ISOCountry As String
        Try
            Dim oPostalCode As New Aptify.Framework.BusinessLogic.Address.AptifyPostalCode(Me.AptifyApplication.UserCredentials)
            Dim oAddressInfo As New Aptify.Framework.BusinessLogic.Address.AddressInfo(Me.AptifyApplication)

            ISOCountry = oAddressInfo.GetISOCode(CLng(Country))

            If oPostalCode.GetPostalCodeInfo(PostalCode, ISOCountry, _
                                    sCity, sState, _
                                    sAreaCode, , sCounty, , , , , , , , _
                                    sCongDist, sCountry, AllowGUI:=True) Then
                If txt IsNot Nothing Then
                    If String.IsNullOrWhitespace(txt.Text) Then
                        txt.Text = sCity
                    End If

                End If
                If cmb IsNot Nothing And sState.Trim <> "" Then
                    cmb.SelectedValue = sState
                End If

                ''RashmiP, removed assigned Area code.
                County = sCounty

            End If

        Catch ex As Exception

        End Try
    End Sub
    'End Here Commented By Pradip  2016-05-16 For Bug #13178
    Private Function DoSave() As Boolean
        Try
            Dim oGEExmpApp As AptifyGenericEntityBase
            Dim bRedirect As Boolean = False
            Dim bRouteDetails As Boolean = True
            If Convert.ToInt32(ViewState("ExemptionApp")) > 0 Then
                oGEExmpApp = AptifyApplication.GetEntityObject("ExemptionApplication__c", Convert.ToInt32(ViewState("ExemptionApp")))
            Else
                oGEExmpApp = AptifyApplication.GetEntityObject("ExemptionApplication__c", -1)
            End If
            With oGEExmpApp
                Dim SelectedValue As String = ddlRoute.SelectedValue.Substring(ddlRoute.SelectedValue.IndexOf("|") + 1, ddlRoute.SelectedValue.Length - ddlRoute.SelectedValue.IndexOf("|") - 1).Trim()

                .SetValue("PersonID", AptifyEbusinessUser1.PersonID)
                If oGEExmpApp.RecordID <= 0 Then
                    .SetValue("StatusID", AptifyApplication.GetEntityRecordIDFromRecordName("ExemptionApplicationStatus__c", "With Student"))
                End If
                If ddlRoute.SelectedValue.Substring(0, ddlRoute.SelectedValue.IndexOf("|")) = "Select One" Then
                    'ddlRoute.SelectedValue = "Select One" Then
                    bRouteDetails = False
                Else
                    .SetValue("RouteOfEntry", ddlRoute.SelectedValue.Substring(0, ddlRoute.SelectedValue.IndexOf("|")))
                End If
                If hdnCompanyId.Value <> "" AndAlso hdnCompanyId.Value <> "0" Then
                    .SetValue("CompanyID", hdnCompanyId.Value)

                    If ViewState("AddressLine1").ToString().Trim() <> txtAddrsLine1.Text.Trim() Or _
                        ViewState("AddressLine2").ToString().Trim() <> txtAddrsLine2.Text.Trim() Or _
                        ViewState("AddressLine3").ToString().Trim() <> txtAddrsLine3.Text.Trim() Then
                        Dim oAddressGE As AptifyGenericEntityBase
                        oAddressGE = AptifyApplication.GetEntityObject("Addresses", Convert.ToInt32(ViewState("AddressID")))
                        oAddressGE.SetValue("Line1", txtAddrsLine1.Text)
                        oAddressGE.SetValue("Line2", txtAddrsLine2.Text)
                        oAddressGE.SetValue("Line3", txtAddrsLine3.Text)
                        oAddressGE.Save()
                    End If
                    If Not ViewState("AddressID") Is Nothing Then
                        '' .SetValue("CompanyAddressID", Convert.ToInt32(ViewState("AddressID"))) ''Code commented by GM for Redmine #20054
                    End If

                    'Added by abhishek tapkir 02-27-2015
                    'Dim sAddressID As String = String.Empty
                    'Dim sAddress(3) As String
                    'If Not txtFirmAddress.Text = String.Empty Then
                    '    sAddress = txtFirmAddress.Text.Split(CChar(","))
                    'End If
                    'Dim sSql As String = Database & "..spGetCompanyAddress__c @CompanyID=" & hdnCompanyId.Value
                    'Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                    'If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    '    sAddressID = dt.Rows(0)("AddressID").ToString
                    'End If
                    'Dim oCompanyGE As AptifyGenericEntityBase
                    'oCompanyGE = AptifyApplication.GetEntityObject("Companies", CLng(hdnCompanyId.Value))
                    'If oCompanyGE.Save() Then
                    '    Dim oAddressGE As AptifyGenericEntityBase
                    '    oAddressGE = AptifyApplication.GetEntityObject("Addresses", CLng(sAddressID))
                    '    oAddressGE.SetValue("Line1", sAddress(0))
                    '    oAddressGE.SetValue("Line2", sAddress(1))
                    '    oAddressGE.SetValue("Line3", sAddress(2))
                    '    oAddressGE.SetValue("Line4", sAddress(3))
                    '    If oAddressGE.Save() Then
                    '        oCompanyGE.Save()
                    '    End If
                    'End If

                ElseIf txtFirm.Text.Trim.ToLower = "not known" OrElse txtFirm.Text.Trim.ToLower = "not listed" Then ' added by GM for Redmine #16744 on 22/06/2018
                    'Commented below part as part of #20005
                    'ElseIf txtFirm.Text <> String.Empty Then
                    '    ' Create a new company with and address

                    '    Dim oCompanyGE As AptifyGenericEntityBase
                    '    oCompanyGE = AptifyApplication.GetEntityObject("Companies", -1)
                    '    oCompanyGE.SetValue("Name", txtFirm.Text)
                    '    oCompanyGE.SetValue("Status__c", "Pending")
                    '    If oCompanyGE.Save() Then
                    '        Dim oAddressGE As AptifyGenericEntityBase
                    '        oAddressGE = AptifyApplication.GetEntityObject("Addresses", -1)
                    '        oAddressGE.SetValue("Line1", txtAddrsLine1.Text)
                    '        oAddressGE.SetValue("Line2", txtAddrsLine2.Text)
                    '        oAddressGE.SetValue("Line3", txtAddrsLine3.Text)
                    '        oAddressGE.SetValue("City", txtCity.Text)
                    '        If oAddressGE.Save() Then
                    '            oCompanyGE.SetValue("AddressID", oAddressGE.RecordID)
                    '            oCompanyGE.Save()
                    '        End If
                    '        .SetValue("CompanyID", oCompanyGE.RecordID)
                    '        ''.SetValue("CompanyAddressID", oCompanyGE.GetValue("AddressID")) ''Code commented by GM for Redmine #20054
                    '    End If
                ElseIf txtFirm.Text = String.Empty AndAlso String.Compare(SelectedValue, "ele", True) <> 0 Then
                    ' commented by govind for redmine issue #16094
                    'bRouteDetails = False
                End If
                If oGEExmpApp.RecordID <= 0 Then
                    .SetValue("FirmApprovalStatus", "")
                End If

                If chkInfoToFirm.Checked Then
                    .SetValue("ShareMyInfoWithFirm", 1)
                Else
                    .SetValue("ShareMyInfoWithFirm", 0)
                End If
                If ViewState("AcademicCycle") Is Nothing Then
                    ''Commented By Pradip 2016-01-07 For Group 3 Tracker G3-9
                    'Dim sSqlAcademicCycle As String = Database & "..spGetAcademicYearForEEApp__c"
                    'Dim dt As DataTable = DataAction.GetDataTable(sSqlAcademicCycle, IAptifyDataAction.DSLCacheSetting.BypassCache)
                    'If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    '    .SetValue("AcademicCycleID", Convert.ToInt32(dt.Rows(0)("ID")))
                    'End If
                    ''Added By Pradip 2016-01-07 For Group 3 Tracker G3-9
                    Dim oParams(0) As IDataParameter
                    oParams(0) = Me.DataAction.GetDataParameter("@StudentID", SqlDbType.BigInt, AptifyEbusinessUser1.PersonID)
                    Dim sSqlAcademicCycle As String = Database & "..spGetNextAcademicYearForEEApp__c"
                    Dim dt As DataTable = Me.DataAction.GetDataTableParametrized(sSqlAcademicCycle, CommandType.StoredProcedure, oParams)
                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                        .SetValue("AcademicCycleID", Convert.ToInt32(dt.Rows(0)("ID")))
                    End If
                End If

                If bRouteDetails Then
                    If .Save() Then
                        ' If txtPreferredSalutation.Enabled = True Then
                        If cmbSalutation.Enabled = True Then
                            DoPersonSave()
                        End If

                        ViewState("ExemptionApp") = .RecordID
                        bRedirect = True
                        ' Call Delete Qaulification details                    
                        'to delete already existing details, get in datatable
                        Dim dtQualificatioDetails As DataTable = TryCast(ViewState("dtQualificationDetails"), DataTable)
                        Dim oQualificationGE As AptifyGenericEntityBase
                        For Each drDetails As DataRow In dtQualificatioDetails.Rows
                            oQualificationGE = AptifyApplication.GetEntityObject("QualificationDocuments__c", Convert.ToInt32(drDetails("ID")))
                            oQualificationGE.Delete()
                        Next
                        ' Add new Qualification details record           
                        If Not CurrentEducationTable Is Nothing AndAlso CurrentEducationTable.Rows.Count > 0 Then
                            Dim oAwardingBodyGE As AptifyGenericEntityBase
                            Dim oDegreeGE As AptifyGenericEntityBase
                            For Each row As DataRow In CurrentEducationTable.Rows
                                oQualificationGE = AptifyApplication.GetEntityObject("QualificationDocuments__c", -1)
                                oQualificationGE.SetValue("ExemptionApplicationID", .RecordID)
                                oQualificationGE.SetValue("EducationLevel", row("EducationLevelID"))
                                If Convert.ToInt32(row("InstituteID")) <= 0 Then
                                    oAwardingBodyGE = AptifyApplication.GetEntityObject("AwardingBodies__c", -1)
                                    oAwardingBodyGE.SetValue("Name", row("Institute"))
                                    oAwardingBodyGE.SetValue("Status", "Pending")
                                    If oAwardingBodyGE.Save() Then
                                        oQualificationGE.SetValue("AwardingBodyID", oAwardingBodyGE.RecordID)
                                    End If
                                Else
                                    oQualificationGE.SetValue("AwardingBodyID", row("InstituteID"))
                                End If

                                oQualificationGE.SetValue("Year", row("Year"))
                                If Convert.ToInt32(row("DegreeID")) <= 0 Then
                                    oDegreeGE = AptifyApplication.GetEntityObject("Qualifications__c", -1)
                                    oDegreeGE.SetValue("Name", row("Degree"))
                                    oDegreeGE.SetValue("Status", "Pending")
                                    oDegreeGE.SetValue("EducationLevelID", row("EducationLevelID"))
                                    If oDegreeGE.Save() Then
                                        oQualificationGE.SetValue("DegreeID", oDegreeGE.RecordID)
                                    End If
                                Else
                                    oQualificationGE.SetValue("DegreeID", row("DegreeID"))
                                End If
                                oQualificationGE.SetValue("Grade", row("GPA"))
                                If row("EducationLevel").ToString().Trim.ToLower = "gce" Then
                                    Dim sSql As String = Database & "..spGetSubjectsByEducationLevelID__c @EducationLevelID=" & row("EducationLevelID").ToString()
                                    Dim dtSubjects As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                                    If Not dtSubjects Is Nothing AndAlso dtSubjects.Rows.Count > 0 Then
                                        For Each drSubject As DataRow In dtSubjects.Rows
                                            With oQualificationGE.SubTypes("QualificationSubjects__c").Add()
                                                .SetValue("SubjectID", drSubject("ID").ToString())
                                            End With
                                        Next
                                    End If
                                End If
                                If row("EducationLevel").ToString().Trim.ToLower = "leaving certificate" Then
                                    Dim sSql As String = Database & "..spGetSubjectsByEducationLevelID__c @EducationLevelID=" & row("EducationLevelID").ToString()
                                    Dim dtSubjects As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                                    If Not dtSubjects Is Nothing AndAlso dtSubjects.Rows.Count > 0 Then
                                        For Each drSubject As DataRow In dtSubjects.Rows
                                            With oQualificationGE.SubTypes("QualificationSubjects__c").Add()
                                                .SetValue("SubjectID", drSubject("ID").ToString())
                                            End With
                                        Next
                                    End If
                                End If
                                oQualificationGE.Save()
                            Next
                            ViewState("dtQualificationDetails") = CurrentEducationTable
                        End If
                    End If
                ElseIf ddlRoute.SelectedValue.Trim = "Select One|Select One" Then
                    ' lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExemptionApp.IncompleteRouteDetails")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    lblValidation.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExemptionApp.IncompleteRouteDetails")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    radWindowValidation.VisibleOnPageLoad = True

                Else
                    ' lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExemptionApp.IncompleteRouteDetails")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    'lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExemptionApp.IncompleteFirmDetails")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    lblValidation.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExemptionApp.IncompleteFirmDetails")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    radWindowValidation.VisibleOnPageLoad = True
                End If
            End With
            If bRedirect = True Then
                lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExemptionApp.ApplicationSaved")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)

            End If
            Return bRedirect
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            Return False
        End Try
    End Function

    Private Sub DeleteQualificationDetails()
        Try
            'for each row in grid check value of checkbox, and remove row from dt. dt=CurrentEducationTable
            Dim chkRemove As CheckBox
            Dim iRemoveIndex As Integer = 0
            For Each grdItem As GridViewRow In grdEducationDetails.Rows
                chkRemove = TryCast(grdItem.FindControl("chkRemove"), CheckBox)
                If Not chkRemove Is Nothing AndAlso chkRemove.Checked Then
                    If CurrentEducationTable.Rows.Count = grdEducationDetails.Rows.Count Then
                        iRemoveIndex = grdItem.RowIndex
                    Else
                        iRemoveIndex = grdItem.RowIndex - (grdEducationDetails.Rows.Count - CurrentEducationTable.Rows.Count)
                    End If
                    CurrentEducationTable.Rows.RemoveAt(iRemoveIndex)
                End If
            Next
            grdEducationDetails.DataSource = CurrentEducationTable
            grdEducationDetails.DataBind()
            If CurrentEducationTable.Rows.Count > 0 Then
                btnRemove.Visible = True
            Else
                btnRemove.Visible = False
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Private Sub LoadEducationDataTableDetails()
        Try
            If Not CurrentEducationTable Is Nothing And CurrentEducationTable.Rows.Count > 0 Then
                grdEducationDetails.DataSource = CurrentEducationTable
                grdEducationDetails.DataBind()
                btnRemove.Visible = True
            Else
                Dim sSql As String = "..spGetQualificationDocumentDetails__c @ExempAppID=" & Convert.ToInt32(ViewState("ExemptionApp"))
                Dim dt As DataTable = DataAction.GetDataTable(sSql)
                ViewState("dtQualificationDetails") = dt
                grdEducationDetails.DataSource = dt
                grdEducationDetails.DataBind()
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    CurrentEducationTable = dt
                    btnRemove.Visible = True

                End If

                'Added by Abhishek 2-3-2015
                If Not ViewState("ExemptionApp") Is Nothing Then
                    Dim iStatusID As Integer
                    Dim oGEExmpApp As AptifyGenericEntityBase
                    oGEExmpApp = AptifyApplication.GetEntityObject("ExemptionApplication__c", Convert.ToInt32(ViewState("ExemptionApp")))
                    iStatusID = CInt(oGEExmpApp.GetValue("StatusID"))
                    If iStatusID = 2 Then
                        grdEducationDetails.Columns(5).Visible = False
                        lblRoutesMessage.Visible = False
                    End If
                End If
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
#End Region

    Protected Sub btnValidationOK_Click(sender As Object, e As EventArgs) Handles btnValidationOK.Click
        radWindowValidation.VisibleOnPageLoad = False
    End Sub

    Protected Sub lnkUpdate_Click(sender As Object, e As EventArgs) Handles lnkUpdate.Click
        'txtPreferredSalutation.Enabled = True
        cmbSalutation.Enabled = True
        cmbGender.Enabled = True
        txtHomeAddressLine1.Enabled = True
        txtHomeAddressLine2.Enabled = True
        txtHomeAddressLine3.Enabled = True
        txtHomeCity.Enabled = True
        cmbHomeState.Enabled = True
        txtHomeCounty.Enabled = True
        txtHomeZipCode.Enabled = True
        cmbHomeCountry.Enabled = True
        txtIntlCode.Enabled = True
        txtPhoneAreaCode.Enabled = True
        txtPhone.Enabled = True
    End Sub

    Protected Sub btnPrintForm_Click(sender As Object, e As EventArgs) Handles btnPrintForm.Click
        Try
            Dim ReportPageURL As String = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings("ReportPageURL"))
            Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "EE Application Report"))
            Dim rptParam As New AptifyCrystalReport__c
            rptParam.ReportID = ReportID
            rptParam.Param1 = Convert.ToString(ViewState("ExemptionApp"))
            Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPageURL & "' )", True)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

End Class
