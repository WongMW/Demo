''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer            Date Created/Modified               Summary
'Kavita Zinage        06/10/2015                      Student Diary Entry Page 
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

Partial Class UserControls_Aptify_Consulting_StudentDiaryEntry__c
    Inherits BaseUserControlAdvanced

    Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "StudentDiaryEntry__c"
    Protected Const ATTRIBUTE_LEAVETYPEPAGE As String = "LeaveTypeDetailsPage"
    Protected Const ATTRIBUTE_COMPETENCYPAGE As String = "CompetencyDetailsPage"
    Protected Const ATTRIBUTE_STUDENTDASHBOARDPAGE As String = "StudentDashboardPage"
    Protected Const ATTRIBUTE_EXISTINGDIARYPAGE As String = "ExistingDiaryPage"

    Public dt As New DataTable()
    Public dIntakeDate As Date
    Public iRouteofEntry As Integer = -1
    Public sRouteofEntry As String = String.Empty
    Public iCompanyID As Integer
    Public bFail As Boolean
    Public dContractExpireDate As Date = Nothing
    Dim iTotalLeaveDays As Integer = 0


#Region "Properties"

    Public Property LeaveTypeDetailsPage() As String
        Get
            If ViewState.Item(ATTRIBUTE_LEAVETYPEPAGE) IsNot Nothing Then
                Return ViewState.Item(ATTRIBUTE_LEAVETYPEPAGE).ToString()
            Else
                Return ""
            End If

        End Get
        Set(ByVal value As String)
            ViewState.Item(ATTRIBUTE_LEAVETYPEPAGE) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    Public Property CompetencyDetailsPage() As String
        Get
            If ViewState.Item(ATTRIBUTE_COMPETENCYPAGE) IsNot Nothing Then
                Return ViewState.Item(ATTRIBUTE_COMPETENCYPAGE).ToString()
            Else
                Return ""
            End If

        End Get
        Set(ByVal value As String)
            ViewState.Item(ATTRIBUTE_COMPETENCYPAGE) = Me.FixLinkForVirtualPath(value)
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

    Public Overridable Property ExistingDiaryPage() As String
        Get
            If Not ViewState(ATTRIBUTE_EXISTINGDIARYPAGE) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_EXISTINGDIARYPAGE))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_EXISTINGDIARYPAGE) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    Protected Overrides Sub SetProperties()
        If String.IsNullOrEmpty(Me.ID) Then
            Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
        End If

        MyBase.SetProperties()
        If String.IsNullOrEmpty(LeaveTypeDetailsPage) Then
            LeaveTypeDetailsPage = Me.GetLinkValueFromXML(ATTRIBUTE_LEAVETYPEPAGE)
        End If
        If String.IsNullOrEmpty(CompetencyDetailsPage) Then
            CompetencyDetailsPage = Me.GetLinkValueFromXML(ATTRIBUTE_COMPETENCYPAGE)
        End If
        If String.IsNullOrEmpty(StudentDashboardPage) Then
            StudentDashboardPage = Me.GetLinkValueFromXML(ATTRIBUTE_STUDENTDASHBOARDPAGE)
        End If
        'Added by Kavita to navigate Existing Diary Page
        If String.IsNullOrEmpty(ExistingDiaryPage) Then
            ExistingDiaryPage = Me.GetLinkValueFromXML(ATTRIBUTE_EXISTINGDIARYPAGE)
        End If
    End Sub


    ''' <summary>
    ''' Create Deleted items Data table 
    ''' </summary>
    Public Property CurrentLeaveDeleteTable() As DataTable
        Get
            If Not Session("LeaveDeleteTable") Is Nothing Then
              Return CType(Session("LeaveDeleteTable"), DataTable)
            Else
                Dim dtLeaveDeleteTable As New DataTable
                dtLeaveDeleteTable.Columns.Add("SubTID")
                dtLeaveDeleteTable.Columns.Add("LeaveID")

                Return dtLeaveDeleteTable
            End If
        End Get
        Set(ByVal value As DataTable)
            Session("LeaveDeleteTable") = value
        End Set
    End Property

    ''' <summary>
    ''' Create Deleted items Data table 
    ''' </summary>
    Public Property CurrentCompetencyDeleteTable() As DataTable
        Get
            If Not Session("CompetencyDeleteTable") Is Nothing Then
                Return CType(Session("CompetencyDeleteTable"), DataTable)
            Else
                Dim dtCompetencyDeleteTable As New DataTable
                dtCompetencyDeleteTable.Columns.Add("SubTCompID")
                dtCompetencyDeleteTable.Columns.Add("CompID")

                Return dtCompetencyDeleteTable
            End If
        End Get
        Set(ByVal value As DataTable)
            Session("CompetencyDeleteTable") = value
        End Set
    End Property
#End Region
    ''Page Load Event
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            lblMessage.Text = ""
            If Request.QueryString("ID") IsNot Nothing Then
                hdnCADiaryRecordID.Value = Convert.ToString(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("ID")))
            End If
            SetProperties()
            lnkleavetype.Attributes.Add("onClick", "javascript:window.open('" + LeaveTypeDetailsPage + "');return false;")
            lnkAreaExp.Attributes.Add("onClick", "javascript:window.open('" + CompetencyDetailsPage + "');return false;")
            ValidateData()
            If Not IsPostBack Then

                lblEngagement.Text = CStr(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentDiaryEntryPage.EngagementNumberMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
                AddColumn()
                LoadLeaveTypes()
                LoadCompetencyCategories()
                LoadCompetencies()
                If Convert.ToInt32(hdnCADiaryRecordID.Value) > 0 Then
                    LoadDiaryDetails()
                End If
                CurrentLeaveDeleteTable = Nothing
                CurrentCompetencyDeleteTable = Nothing
                ViewState("bFail") = True

                'Added By kavita Zinage 15/03/2016
                If Not txtStartDate.SelectedDate Is Nothing And Not txtEndDate.SelectedDate Is Nothing Then
                    TxtTotalDays.Text = Convert.ToString(CalculateDays(Convert.ToDateTime(txtStartDate.SelectedDate), Convert.ToDateTime(txtEndDate.SelectedDate), iTotalLeaveDays))
                End If
            End If
            'PanelRegExp.Visible = False
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
    ''' Load Leave Types
    ''' </summary>
    Private Sub LoadLeaveTypes()
        Try
            Dim sSql As String = Database & "..spGetAllLeaveTypes__c"
            Dim dtLeaveType As DataTable = DataAction.GetDataTable(sSql, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            ddlLeaveType.DataSource = dtLeaveType
            ddlLeaveType.DataTextField = "Name"
            ddlLeaveType.DataValueField = "ID"
            ddlLeaveType.DataBind()
            ddlLeaveType.Items.Insert(0, "Select")
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Load Competencies
    ''' </summary>
    Private Sub LoadCompetencyCategories()
        Try
            Dim sSql As New StringBuilder()
            sSql.AppendFormat("{0}..spGetAllCompetancyCategory__c", Me.Database)
            Dim dtCompetencyCategory As DataTable = DataAction.GetDataTable(sSql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            ddlExpCategory.DataSource = dtCompetencyCategory
            ddlExpCategory.DataTextField = "Name"
            ddlExpCategory.DataValueField = "ID"
            ddlExpCategory.DataBind()
            ddlExpCategory.Items.Insert(0, "Select")
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Load Competencies
    ''' </summary>
    Private Sub LoadCompetencies()
        Try
            Dim CompetencyCategoryID As Integer = 0
            If ddlExpCategory.SelectedValue <> "Select" Then
                CompetencyCategoryID = Convert.ToInt32(ddlExpCategory.SelectedValue)
            End If
            Dim sSql As New StringBuilder()
            sSql.AppendFormat("{0}..spGetAllFunctionalCompetencies__c @CompetencyCategoryID={1}", Me.Database, CompetencyCategoryID)
            Dim dtFunctionalCompetency As DataTable = DataAction.GetDataTable(sSql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            ddlAreaofExperience.DataSource = dtFunctionalCompetency
            ddlAreaofExperience.DataTextField = "Name"
            ddlAreaofExperience.DataValueField = "ID"
            ddlAreaofExperience.DataBind()
            ddlAreaofExperience.Items.Insert(0, "Select")

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Private Sub AddColumn()
        Try
            Dim dtLeave As New DataTable()
            dtLeave.Columns.Add(New DataColumn("LeaveType", GetType(String)))
            dtLeave.Columns.Add(New DataColumn("Days", GetType(Double)))
            dtLeave.Columns.Add(New DataColumn("LeaveTypeID", GetType(Integer)))
            dtLeave.Columns.Add(New DataColumn("lID", GetType(Integer)))
            dtLeave.Columns.Add(New DataColumn("sdlID", GetType(Integer)))
            dtLeave.Columns.Add(New DataColumn("dlID", GetType(Integer)))


            ViewState("dtLeave") = dtLeave

            Dim dtCompetency As New DataTable()
            dtCompetency.Columns.Add(New DataColumn("ComptencyCategory", GetType(String)))
            dtCompetency.Columns.Add(New DataColumn("Experience", GetType(String)))
            dtCompetency.Columns.Add(New DataColumn("ComptencyID", GetType(Integer)))
            dtCompetency.Columns.Add(New DataColumn("cID", GetType(Integer)))
            dtCompetency.Columns.Add(New DataColumn("sdcID", GetType(Integer)))
            dtCompetency.Columns.Add(New DataColumn("dcID", GetType(Integer)))
            ViewState("dtCompetency") = dtCompetency
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try

    End Sub
    ''' <summary>
    ''' Add CA Diary Record
    ''' </summary>
    Public Function AddCADiaryRecord(ByVal Status As String) As Boolean
        Try
            Dim oStudentDiaryGE As AptifyGenericEntityBase
            oStudentDiaryGE = Me.AptifyApplication.GetEntityObject("StudentDiaryEntries__c", Convert.ToInt32(hdnCADiaryRecordID.Value))
            Dim sTransID As String = DataAction.BeginTransaction(IsolationLevel.Serializable, True)

            oStudentDiaryGE.SetValue("StudentID", Convert.ToInt32(AptifyEbusinessUser1.PersonID))
            oStudentDiaryGE.SetValue("DateStarted", txtStartDate.SelectedDate)
            oStudentDiaryGE.SetValue("DateCompleted", txtEndDate.SelectedDate)
            oStudentDiaryGE.SetValue("LearningLevel", Convert.ToString(ddlLearningLevel.SelectedValue))
            oStudentDiaryGE.SetValue("Days", Convert.ToDouble(txtExperience.Text.ToString()))

            oStudentDiaryGE.SetValue("MentorID", Convert.ToInt32(HFMentorID.Value))
            oStudentDiaryGE.SetValue("ShortWorkActivity", txtTitle.Text)
            oStudentDiaryGE.SetValue("WorkActivity", txtEiditorDescription.Content)

            If Convert.ToInt32(hdnCADiaryRecordID.Value) > 0 Then
                oStudentDiaryGE.SetValue("Status", Convert.ToString(lblStatus.Text))
            Else
                oStudentDiaryGE.SetValue("Status", Status)
            End If

            If Convert.ToInt32(iCompanyID) > 0 Then
                oStudentDiaryGE.SetValue("CompanyID", Convert.ToInt32(iCompanyID))
            End If

            If Convert.ToInt32(hdnCADiaryRecordID.Value) < 0 Then
                oStudentDiaryGE.SetValue("Type", "Regular Diary Entry")
            End If

            oStudentDiaryGE.SetValue("EngagementNumber", txtEngagmentNumber.Text)

            If Convert.ToInt32(hdnCADiaryRecordID.Value) > 0 Then
                If Convert.ToString(Session("OldRouteofEntry")) = sRouteofEntry Or Convert.ToString(Session("OldRouteofEntry")) = "" Then
                    oStudentDiaryGE.SetValue("RouteOfEntry", iRouteofEntry)
                    Session("OldRouteofEntry") = ""
                End If
            Else
                oStudentDiaryGE.SetValue("RouteOfEntry", iRouteofEntry)
            End If

            oStudentDiaryGE.SetValue("CompanyAuditDays", txtCompanyDays.Text)
            oStudentDiaryGE.SetValue("OtherAuditDays", txtOtherDays.Text)

            For i As Integer = 0 To gvCompetency.Items.Count - 1
                Dim hdnCompetencyID As HiddenField = CType(gvCompetency.Items(i).FindControl("hdnCompetencyID"), HiddenField)
                Dim hdnCID As HiddenField = CType(gvCompetency.Items(i).FindControl("hdnCID"), HiddenField)
                If Convert.ToInt32(hdnCID.Value) = 0 Then
                    With oStudentDiaryGE.SubTypes("StudentDiaryCompentencies__c").Add()
                        .SetValue("FunctionalCompetencyID", Convert.ToInt32(hdnCompetencyID.Value))
                    End With
                End If
            Next

            For i As Integer = 0 To grdLeave.Items.Count - 1
                Dim hdnLeaveTypeID As HiddenField = CType(grdLeave.Items(i).FindControl("hdnLeaveTypeID"), HiddenField)
                Dim lblDays As Label = CType(grdLeave.Items(i).FindControl("lblDays"), Label)
                Dim hdnLID As HiddenField = CType(grdLeave.Items(i).FindControl("hdnLID"), HiddenField)

                If Convert.ToInt32(hdnLID.Value) = 0 Then
                    With oStudentDiaryGE.SubTypes("StudentDiaryLeaves__c").Add()
                        .SetValue("LeaveType", Convert.ToInt32(hdnLeaveTypeID.Value))
                        .SetValue("Days", Convert.ToDouble(lblDays.Text))
                    End With
                End If
            Next
            DeleteCompetencyRecord()
            DeleteLeaveRecord()

            Dim _error As String = ""
            Try
                If oStudentDiaryGE.Save(False, _error) Then
                    DataAction.CommitTransaction(sTransID)
                    ViewState("dtLeave") = Nothing
                    ViewState("dtCompetency") = Nothing
                    Session("LeaveDeleteTable") = Nothing
                    Session("CompetencyDeleteTable") = Nothing
                    Return True
                Else
                    DataAction.RollbackTransaction(sTransID)
                    Return False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' Clear Data
    ''' </summary>
    Private Sub clearData()
        Try
            txtDays.Text = String.Empty
            txtEiditorDescription.Content = String.Empty
            txtEndDate.Clear()
            txtEngagmentNumber.Text = String.Empty
            txtExperience.Text = String.Empty
            txtStartDate.Clear()
            txtTitle.Text = String.Empty

            ddlExpCategory.SelectedIndex = 0
            ddlAreaofExperience.SelectedIndex = 0
            ddlLearningLevel.SelectedIndex = 0
            ddlLeaveType.SelectedIndex = 0
            txtCompanyDays.Text = String.Empty
            txtOtherDays.Text = String.Empty

            ViewState("dtLeave") = Nothing
            ViewState("dtCompetency") = Nothing

            AddColumn()
            grdLeave.Rebind()
            gvCompetency.Rebind()
            TxtTotalDays.Text = String.Empty
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ''' <summary>
    '''  Set Control ReadOnly/Disabled
    ''' </summary>
    Private Sub DisableControl()
        Try
            txtDays.ReadOnly = True
            txtEiditorDescription.Visible = False
            lblDesc.Visible = True
            txtEndDate.Enabled = False
            txtEngagmentNumber.ReadOnly = True
            txtExperience.ReadOnly = True
            txtStartDate.Enabled = False
            txtTitle.ReadOnly = True

            txtCompanyDays.ReadOnly = True
            txtOtherDays.ReadOnly = True

            ddlExpCategory.Enabled = False
            ddlAreaofExperience.Enabled = False
            ddlLearningLevel.Enabled = False
            ddlLeaveType.Enabled = False

            btnAdd.Enabled = False
            btnAddExperience.Enabled = False

            btnSavenExit.Visible = False
            btnSubmitToMentor.Visible = False

            RecordAttachments__c.AllowAdd = False
            RecordAttachments__c.AllowView = True
            RecordAttachments__c.AllowDelete = False

            grdLeave.MasterTableView.GetColumn("ColRemove").Display = False
            gvCompetency.MasterTableView.GetColumn("ColRemove").Display = False
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    'Added Function to enable control after click on "Submittomentor" button for new record
    Private Sub EnableControl()
        Try
            txtDays.ReadOnly = False
            txtEiditorDescription.Visible = True
            lblDesc.Visible = False
            txtEndDate.Enabled = True
            txtEngagmentNumber.ReadOnly = False
            txtExperience.ReadOnly = False
            txtStartDate.Enabled = True
            txtTitle.ReadOnly = False

            txtCompanyDays.ReadOnly = False
            txtOtherDays.ReadOnly = False

            ddlExpCategory.Enabled = True
            ddlAreaofExperience.Enabled = True
            ddlLearningLevel.Enabled = True
            ddlLeaveType.Enabled = True

            btnAdd.Enabled = True
            btnAddExperience.Enabled = True

            btnSavenExit.Visible = True
            btnSubmitToMentor.Visible = True

            RecordAttachments__c.AllowAdd = True
            RecordAttachments__c.AllowView = True
            RecordAttachments__c.AllowDelete = True

            grdLeave.MasterTableView.GetColumn("ColRemove").Display = True
            gvCompetency.MasterTableView.GetColumn("ColRemove").Display = True
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Clear/Reset Grid with Nothing
    ''' </summary>
    Private Sub ClearGrid()
        Try
            'grdLeave.DataSource = Nothing
            'grdLeave.DataBind()
            'grdLeave.Visible = False
            'PanelLeave.Visible = True

            'gvCompetency.DataSource = Nothing
            'gvCompetency.DataBind()
            'gvCompetency.Visible = False
            'PanelCompetency.Visible = True
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ' Leave Details
    Private Sub LoadLeavesGrid()
        Try
            Dim sSQLLeave As New StringBuilder()

            sSQLLeave.AppendFormat("{0}..spGetLeavesByDiaryID__c  @DiaryID={1}", Me.Database, Convert.ToInt32(hdnCADiaryRecordID.Value))
            Dim dtDiaryLeaves As DataTable = DataAction.GetDataTable(sSQLLeave.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)

            If Not dtDiaryLeaves Is Nothing Then 'AndAlso dtDiaryLeaves.Rows.Count > 0 
                PanelLeave.Visible = True
                ViewState("dtLeave") = dtDiaryLeaves
                grdLeave.DataBind()
                grdLeave.Visible = True
                divLeave.Visible = True

                'Else
                '    PanelLeave.Visible = False


            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    'Competency Details
    Private Sub LoadCompetencyGrid()
        Try
            Dim sSQLCompetency As New StringBuilder()
            sSQLCompetency.AppendFormat("{0}..spGetCompetenciesByDiaryID__c @DiaryID={1}", Me.Database, Convert.ToInt32(hdnCADiaryRecordID.Value))
            Dim dtDiaryCompetencies As DataTable = DataAction.GetDataTable(sSQLCompetency.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dtDiaryCompetencies Is Nothing Then 'AndAlso dtDiaryCompetencies.Rows.Count > 0
                PanelCompetency.Visible = True
                ViewState("dtCompetency") = dtDiaryCompetencies
                gvCompetency.DataBind()
                gvCompetency.Visible = True
                divcmp.Visible = True
                'Else
                '    PanelCompetency.Visible = False
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    'Existing Diary Details
    Private Sub LoadDiaryDetails()
        Try
            Dim sSQLDetails As New StringBuilder()
            sSQLDetails.AppendFormat("{0}..spGetAllStudentDiaryEntriesDetailsByID__c @ID={1}", Me.Database, Convert.ToInt32(hdnCADiaryRecordID.Value))
            Dim dtDiaryDetails As DataTable = DataAction.GetDataTable(sSQLDetails.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)

            If Not dtDiaryDetails Is Nothing AndAlso dtDiaryDetails.Rows.Count > 0 Then
                If Not IsDBNull(dtDiaryDetails.Rows(0).Item("StartDate")) Then
                    txtStartDate.SelectedDate = Convert.ToDateTime(dtDiaryDetails.Rows(0)("StartDate"))
                    'Added to Enable/Disable Date Control based on Route of Entry
                    If Not IsDBNull(dtDiaryDetails.Rows(0).Item("RouteOfEntry_Name")) Then
                        Session("OldRouteofEntry") = Convert.ToString(dtDiaryDetails.Rows(0).Item("RouteOfEntry_Name"))
                        If Convert.ToString(dtDiaryDetails.Rows(0).Item("RouteOfEntry_Name")).ToLower() = "contract" Then
                            txtStartDate.Enabled = False
                        Else
                            txtStartDate.Enabled = True
                        End If
                    End If
                End If
                If Not IsDBNull(dtDiaryDetails.Rows(0).Item("EndDate")) Then
                    txtEndDate.SelectedDate = Convert.ToDateTime(dtDiaryDetails.Rows(0).Item("EndDate"))
                End If
                txtExperience.Text = Convert.ToString(dtDiaryDetails.Rows(0).Item("Days"))
                txtTitle.Text = Convert.ToString(dtDiaryDetails.Rows(0).Item("Title"))
                txtEiditorDescription.Content = Convert.ToString(dtDiaryDetails.Rows(0).Item("Description"))
                ddlLearningLevel.SelectedValue = Convert.ToString(dtDiaryDetails.Rows(0).Item("LearningLevel"))
                txtEngagmentNumber.Text = Convert.ToString(dtDiaryDetails.Rows(0).Item("EngagementNumber"))
                lblDesc.Text = Convert.ToString(dtDiaryDetails.Rows(0).Item("Description"))
                lblStatus.Text = Convert.ToString(dtDiaryDetails.Rows(0).Item("Status"))
                txtCompanyDays.Text = Convert.ToString(dtDiaryDetails.Rows(0).Item("CompanyAuditDays"))
                txtOtherDays.Text = Convert.ToString(dtDiaryDetails.Rows(0).Item("OtherAuditDays"))
            End If
            LoadLeavesGrid()
            LoadCompetencyGrid()
            'Added by Kavita Zinage 15/03/2016
            'If Not txtStartDate.SelectedDate Is Nothing And Not txtEndDate.SelectedDate Is Nothing Then
            '    TxtTotalDays.Text = Convert.ToString(CalculateDays(CDate(txtEndDate.SelectedDate), CDate(txtStartDate.SelectedDate), iTotalLeaveDays))
            'End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Private Sub invisiblePanel()
        Try
            lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentDiaryEntryPage.ValidationMsg")), _
Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)

            pnlMain.Visible = False
            pnlDetails.Visible = False

            pnlDiaryEntry.Visible = False
            pnlEligibility.Visible = False
            PriorExp.Visible = False
            btnSavenExit.Visible = False
            btnSubmitToMentor.Visible = False
            lblsts.Visible = False
            lblStatus.Visible = False
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try


    End Sub

    Private Sub ValidateData()
        Try
            'To check Active Contract Transfer
            Dim sSQL, sSQLType, sSQLExistingDiary, sSQLPerson As New StringBuilder()
            sSQL.AppendFormat("{0}..spCheckStudentDiaryEntrypage__c @StudentID={1}", _
                       Me.Database, Convert.ToInt32(AptifyEbusinessUser1.PersonID))
            Dim dt As DataTable = DataAction.GetDataTable(sSQL.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)

            'To Check Diary Type
            sSQLType.AppendFormat("{0}..spGetStudentDiaryType__c @ID={1}", Me.Database, Convert.ToInt32(hdnCADiaryRecordID.Value))
            Dim dtDiary As DataTable = DataAction.GetDataTable(sSQLType.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)

            sSQLExistingDiary.AppendFormat("{0}..spGetExistingDiaryEntryByStudentID__c @StudentID={1}", Me.Database, Convert.ToInt32(AptifyEbusinessUser1.PersonID))
            Dim dtExistingDiary As DataTable = DataAction.GetDataTable(sSQLExistingDiary.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)

            'To get Intake Date
            Dim dPesronIntakeDate As DateTime
            Dim sIntakedateSQL As String = Me.Database & "..spGetPersonIntakeDateByStudentID__c @StudentID=" & Convert.ToInt32(AptifyEbusinessUser1.PersonID) & ""
            If Convert.ToString(DataAction.ExecuteScalar(sIntakedateSQL)) <> "" Then
                dPesronIntakeDate = Convert.ToDateTime(DataAction.ExecuteScalar(sIntakedateSQL))
            End If
            If Convert.ToString(dPesronIntakeDate) <> "" Then
                dIntakeDate = dPesronIntakeDate
            End If
            'To get Active logged in Student Mentor ID
            Dim sSqlMentorID As New StringBuilder()
            sSqlMentorID.AppendFormat("{0}..spGetActiveMentorByStudentID__c @StudentID={1}", Me.Database, Convert.ToInt32(AptifyEbusinessUser1.PersonID))
            Dim MentorID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSqlMentorID.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
            If MentorID > 0 Then
                HFMentorID.Value = Convert.ToString(MentorID)
            End If

            'Validation Student Diary entry creation page
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                If Convert.ToInt32(hdnCADiaryRecordID.Value) < 0 Then
                    invisiblePanel()
                Else
                    If Not dtDiary Is Nothing AndAlso dtDiary.Rows.Count > 0 Then
                        If Convert.ToString(dtDiary.Rows(0).Item("Status")).ToLower() = "locked" Or Convert.ToString(dtDiary.Rows(0).Item("Status")).ToLower() = "submitted to mentor" Then
                            DisableControl()
                        End If
                    End If
                End If
            Else
                If Convert.ToInt32(hdnCADiaryRecordID.Value) > 0 Then
                    If Not dtDiary Is Nothing AndAlso dtDiary.Rows.Count > 0 Then
                        If Convert.ToString(dtDiary.Rows(0).Item("Status")).ToLower() = "locked" Or Convert.ToString(dtDiary.Rows(0).Item("Status")).ToLower() = "submitted to mentor" Then
                            DisableControl()
                        End If
                    End If
                End If
            End If

            'To invisible Prior Experience Section
            If Not dtDiary Is Nothing AndAlso dtDiary.Rows.Count > 0 Then
                If Convert.ToInt32(hdnCADiaryRecordID.Value) > 0 And Convert.ToString(dtDiary.Rows(0).Item("Type")).ToLower() = "prior work experience" Then
                    PriorExp.Visible = True
                    Me.RecordAttachments__c.LoadAttachments(AptifyApplication.GetEntityID("StudentDiaryEntries__c"), Convert.ToInt32(hdnCADiaryRecordID.Value))
                End If
            End If


            'To set Latest Route of Entry 
            Dim sSQLRouteofEntry As New StringBuilder()
            sSQLRouteofEntry.AppendFormat("{0}..spGetLatestRouteofEntryFRbyStudentID__c @StudentID={1}", Me.Database, Convert.ToInt32(AptifyEbusinessUser1.PersonID))
            Dim dtRouteofEntry As DataTable = DataAction.GetDataTable(sSQLRouteofEntry.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dtRouteofEntry Is Nothing AndAlso dtRouteofEntry.Rows.Count > 0 Then

                If Not IsDBNull(dtRouteofEntry.Rows(0).Item("ContractExpireDate")) Then
                    dContractExpireDate = Convert.ToDateTime(dtRouteofEntry.Rows(0).Item("ContractExpireDate"))
                End If
                If Not IsDBNull(dtRouteofEntry.Rows(0).Item("CompanyID")) Then
                    iCompanyID = Convert.ToInt32(dtRouteofEntry.Rows(0).Item("CompanyID"))
                End If
                If Not IsDBNull(dtRouteofEntry.Rows(0).Item("RouteOfEntryID")) Then
                    iRouteofEntry = Convert.ToInt32(dtRouteofEntry.Rows(0).Item("RouteOfEntryID"))
                    sRouteofEntry = Convert.ToString(dtRouteofEntry.Rows(0).Item("RouteOfEntry"))
                Else
                    sRouteofEntry = ""
                End If

                'To set Start Date Based on Existing "Contract" Student Entry
                If Convert.ToInt32(hdnCADiaryRecordID.Value) < 0 Then
                    If Not dtExistingDiary Is Nothing AndAlso dtExistingDiary.Rows.Count > 0 Then
                        If Not IsDBNull(dtRouteofEntry.Rows(0).Item("RouteOfEntryID")) Then
                            If Convert.ToString(dtRouteofEntry.Rows(0).Item("RouteOfEntry")).ToLower() = "contract" Then
                                If Not IsDBNull(dtExistingDiary.Rows(0).Item("EndDate")) Then
                                    txtStartDate.SelectedDate = Convert.ToDateTime(dtExistingDiary.Rows(0).Item("EndDate")).AddDays(1)
                                    txtStartDate.Enabled = False
                                    txtEndDate.MinDate = Convert.ToDateTime(txtStartDate.SelectedDate)
                                End If
                            Else
                                txtStartDate.Enabled = True
                            End If
                        End If
                    End If
                End If

            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try

    End Sub

    Public Function BeforeSaveValidate() As Boolean

        If Convert.ToInt32(hdnCADiaryRecordID.Value) > 0 Then
            If Convert.ToString(Session("OldRouteofEntry")) <> sRouteofEntry Then
                If Convert.ToString(Session("OldRouteofEntry")).ToLower = "contract" AndAlso Convert.ToString(txtStartDate.SelectedDate) <> "" AndAlso Convert.ToString(txtEndDate.SelectedDate) <> "" AndAlso txtExperience.Text <> "" AndAlso txtTitle.Text <> "" AndAlso txtEiditorDescription.Text <> "" AndAlso gvCompetency.Items.Count > 0 Then
                    Return True
                ElseIf Convert.ToString(Session("OldRouteofEntry")).ToLower = "elevation" AndAlso txtExperience.Text <> "" AndAlso txtTitle.Text <> "" AndAlso txtEiditorDescription.Text <> "" AndAlso gvCompetency.Items.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            ElseIf sRouteofEntry.ToLower = "contract" AndAlso Convert.ToString(txtStartDate.SelectedDate) <> "" AndAlso Convert.ToString(txtEndDate.SelectedDate) <> "" AndAlso txtExperience.Text <> "" AndAlso txtTitle.Text <> "" AndAlso txtEiditorDescription.Text <> "" AndAlso gvCompetency.Items.Count > 0 Then
                Return True
            ElseIf sRouteofEntry.ToLower = "elevation" AndAlso txtExperience.Text <> "" AndAlso txtTitle.Text <> "" AndAlso txtEiditorDescription.Text <> "" AndAlso gvCompetency.Items.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Else
            If sRouteofEntry.ToLower = "contract" AndAlso Convert.ToString(txtStartDate.SelectedDate) <> "" AndAlso Convert.ToString(txtEndDate.SelectedDate) <> "" AndAlso txtExperience.Text <> "" AndAlso txtTitle.Text <> "" AndAlso txtEiditorDescription.Text <> "" AndAlso gvCompetency.Items.Count > 0 Then
                Return True
            ElseIf sRouteofEntry.ToLower = "elevation" AndAlso txtExperience.Text <> "" AndAlso txtTitle.Text <> "" AndAlso txtEiditorDescription.Text <> "" AndAlso gvCompetency.Items.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End If
    End Function

    Protected Sub BindDeleteLeaveData(ByVal SubTID As Integer, ByVal LeaveID As Integer)
        Try
            Dim dtCurrentLeaveDeleteTable As DataTable = CurrentLeaveDeleteTable
            Dim dr As DataRow = dtCurrentLeaveDeleteTable.NewRow()

            dr("SubTID") = SubTID
            dr("LeaveID") = LeaveID

            dtCurrentLeaveDeleteTable.Rows.Add(dr)
            CurrentLeaveDeleteTable = dtCurrentLeaveDeleteTable
            Session("LeaveDeleteTable") = dtCurrentLeaveDeleteTable
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try

    End Sub

    Protected Sub BindDeleteCompetencyData(ByVal SubTCompID As Integer, ByVal CompID As Integer)
        Try
            Dim dtCurrentCompetencyDeleteTable As DataTable = CurrentCompetencyDeleteTable
            Dim dr As DataRow = dtCurrentCompetencyDeleteTable.NewRow()

            dr("SubTCompID") = SubTCompID
            dr("CompID") = CompID

            dtCurrentCompetencyDeleteTable.Rows.Add(dr)
            CurrentCompetencyDeleteTable = dtCurrentCompetencyDeleteTable
            Session("CompetencyDeleteTable") = dtCurrentCompetencyDeleteTable
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try

    End Sub

    Protected Sub DeleteLeaveRecord()
        Try
            If Not CurrentLeaveDeleteTable Is Nothing AndAlso CurrentLeaveDeleteTable.Rows.Count > 0 Then
                For Each dr As DataRow In CurrentLeaveDeleteTable.Rows
                    If Convert.ToInt32(dr("LeaveID")) > 0 Then
                        Dim oLeaveDiaryGE As AptifyGenericEntityBase
                        oLeaveDiaryGE = Me.AptifyApplication.GetEntityObject("StudentDiaryEntries__c", Convert.ToInt32(dr("SubTID")))
                        oLeaveDiaryGE.SubTypes("StudentDiaryLeaves__c").Find("ID", Convert.ToInt32(dr("LeaveID"))).Delete()

                        Dim sError As String = String.Empty
                        If oLeaveDiaryGE.Save(False, sError) Then
                        Else
                            Dim sErr As String = sError
                        End If
                    End If
                Next
            End If
            CurrentLeaveDeleteTable = Nothing
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub DeleteCompetencyRecord()
        Try
            If Not CurrentCompetencyDeleteTable Is Nothing AndAlso CurrentCompetencyDeleteTable.Rows.Count > 0 Then
                For Each dr As DataRow In CurrentCompetencyDeleteTable.Rows
                    If Convert.ToInt32(dr("CompID")) > 0 Then
                        Dim oCompetencyDiaryGE As AptifyGenericEntityBase
                        oCompetencyDiaryGE = Me.AptifyApplication.GetEntityObject("StudentDiaryEntries__c", Convert.ToInt32(dr("SubTCompID")))
                        oCompetencyDiaryGE.SubTypes("StudentDiaryCompentencies__c").Find("ID", dr("CompID")).Delete()
                        Dim sError As String = String.Empty
                        If oCompetencyDiaryGE.Save(False, sError) Then
                        Else
                            Dim sErr As String = sError
                        End If
                    End If
                Next
            End If
            CurrentCompetencyDeleteTable = Nothing
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Calculate date difference from get End date and start date
    ''' </summary>
    ''' <param name="Enddate"></param>
    ''' <param name="Startdate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CalculateDays(ByVal Startdate As DateTime, ByVal Enddate As DateTime, ByVal LeaveDays As Integer) As Double
        Dim iDays As Integer = 0

        Dim oParams(1) As IDataParameter
        Dim sSQL As String
        oParams(0) = DataAction.GetDataParameter("@StartDate", SqlDbType.DateTime, Startdate)
        oParams(1) = DataAction.GetDataParameter("@EndDate", SqlDbType.DateTime, Enddate)
        sSQL = Me.Database & "..spGetDiaryExperienceDaysbyDates__c"
        iDays = Convert.ToInt32(DataAction.ExecuteScalarParametrized(sSQL, CommandType.StoredProcedure, oParams))

        iDays = iDays - LeaveDays
        Return iDays
    End Function

#End Region

#Region "Button Click"
    ''' <summary>
    ''' Add Leave in Grid
    ''' </summary>
    Protected Sub btnAdd_Click(sender As Object, e As System.EventArgs) Handles btnAdd.Click
        Try
            If (ddlLeaveType.SelectedItem.Text <> "" Or ddlLeaveType.SelectedItem.Text <> "Select") AndAlso txtDays.Text <> "" Then
                dt = DirectCast(ViewState("dtLeave"), DataTable)
                Dim checkCount As Integer = 0
                For i As Integer = 0 To grdLeave.Items.Count - 1
                    If ddlLeaveType.SelectedItem.Text = CType(grdLeave.Items(i).FindControl("lblLeaveType"), Label).Text.ToString() Then
                        checkCount = checkCount + 1
                    End If
                Next
                If checkCount = 0 Then
                    Dim dr As DataRow = dt.NewRow()
                    dr("lID") = 0
                    dr("sdlID") = 0
                    dr("dlID") = 0
                    dr("LeaveType") = ddlLeaveType.SelectedItem.Text
                    dr("Days") = Convert.ToDouble(txtDays.Text)
                    dr("LeaveTypeID") = Convert.ToInt32(ddlLeaveType.SelectedValue)
                    dt.Rows.Add(dr)
                Else
                    lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
           Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentDiaryEntryPage.ErrorMsg")), _
                       Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    checkCount = 0
                End If

                ViewState("dtLeave") = dt
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    Me.grdLeave.Visible = True
                    Me.grdLeave.DataSource = dt
                    Me.grdLeave.Rebind()
                    divLeave.Visible = True

                    'Added By kavita Zinage 15/03/2016
                    If Not txtStartDate.SelectedDate Is Nothing And Not txtEndDate.SelectedDate Is Nothing Then
                        TxtTotalDays.Text = Convert.ToString(CalculateDays(Convert.ToDateTime(txtStartDate.SelectedDate), Convert.ToDateTime(txtEndDate.SelectedDate), iTotalLeaveDays))
                    End If
                End If
            Else
                lblMessage.Text = CStr(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                        Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentDiaryEntryPage.RequiredFieldMsg")), _
                        Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Add Compentency in Grid
    ''' </summary>
    Protected Sub btnAddExperience_Click(sender As Object, e As System.EventArgs) Handles btnAddExperience.Click
        Try
            If (ddlExpCategory.SelectedItem.Text <> "" AndAlso ddlExpCategory.SelectedItem.Text <> "Select") AndAlso (ddlAreaofExperience.SelectedItem.Text <> "" Or ddlAreaofExperience.SelectedItem.Text <> "Select") Then
                dt = DirectCast(ViewState("dtCompetency"), DataTable)
                Dim checkCount As Integer = 0
                For i As Integer = 0 To gvCompetency.Items.Count - 1
                    If ddlExpCategory.SelectedItem.Text = CType(gvCompetency.Items(i).FindControl("lblExpCategory"), Label).Text.ToString() And ddlAreaofExperience.SelectedItem.Text = CType(gvCompetency.Items(i).FindControl("lblExperience"), Label).Text.ToString() Then
                        checkCount = checkCount + 1
                    End If
                Next
                If checkCount = 0 Then
                    lblMessage.Text = ""
                    Dim dr As DataRow = dt.NewRow()
                    dr("cID") = 0
                    dr("sdcID") = 0
                    dr("dcID") = 0
                    dr("ComptencyCategory") = ddlExpCategory.SelectedItem.Text
                    dr("Experience") = ddlAreaofExperience.SelectedItem.Text
                    dr("ComptencyID") = Convert.ToInt32(ddlAreaofExperience.SelectedValue)
                    dt.Rows.Add(dr)
                Else
                    lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
           Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentDiaryEntryPage.ErrorMsg1")), _
                       Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    checkCount = 0
                End If
                ViewState("dtCompetency") = dt
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    Me.gvCompetency.Visible = True
                    divcmp.Visible = True
                    Me.gvCompetency.DataSource = dt
                    Me.gvCompetency.Rebind()

                End If
            Else
                lblMessage.Text = CStr(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                        Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentDiaryEntryPage.RequiredFieldMsg")), _
                        Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Handles remove click
    ''' </summary>
    Protected Sub btnLeaveRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim btnRemove As Button = CType(sender, Button)
            Dim rowGridItem As GridDataItem = CType(btnRemove.NamingContainer, GridDataItem)
            Dim iIndex As Integer = rowGridItem.DataSetIndex

            Dim hdnsdlID As HiddenField = CType(grdLeave.Items(iIndex).FindControl("hdnsdlID"), HiddenField)
            Dim hdndlID As HiddenField = CType(grdLeave.Items(iIndex).FindControl("hdndlID"), HiddenField)

            dt = CType(ViewState("dtLeave"), DataTable)
            dt.Rows.RemoveAt(iIndex)
            grdLeave.DataSource = dt
            grdLeave.DataBind()
            ViewState("dtLeave") = dt

            BindDeleteLeaveData(Convert.ToInt32(hdnsdlID.Value), Convert.ToInt32(hdndlID.Value))

            'Added By kavita Zinage 15/03/2016
            If Not txtStartDate.SelectedDate Is Nothing And Not txtEndDate.SelectedDate Is Nothing Then
                TxtTotalDays.Text = Convert.ToString(CalculateDays(Convert.ToDateTime(txtStartDate.SelectedDate), Convert.ToDateTime(txtEndDate.SelectedDate), iTotalLeaveDays))
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Handles remove click
    ''' </summary>
    Protected Sub btnCompetencyRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim btnRemove As Button = CType(sender, Button)
            Dim rowGridItem As GridDataItem = CType(btnRemove.NamingContainer, GridDataItem)
            Dim iIndex As Integer = rowGridItem.DataSetIndex

            Dim hdnsdcID As HiddenField = CType(gvCompetency.Items(iIndex).FindControl("hdnsdcID"), HiddenField)
            Dim hdndcID As HiddenField = CType(gvCompetency.Items(iIndex).FindControl("hdndcID"), HiddenField)

            dt = CType(ViewState("dtCompetency"), DataTable)
            dt.Rows.RemoveAt(iIndex)
            ViewState("dtCompetency") = dt

            gvCompetency.DataSource = dt
            gvCompetency.DataBind()

            BindDeleteCompetencyData(Convert.ToInt32(hdnsdcID.Value), Convert.ToInt32(hdndcID.Value))
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Save and Exit Student Diary Entry
    ''' </summary>
    Protected Sub btnSavenExit_Click(sender As Object, e As System.EventArgs) Handles btnSavenExit.Click
        If BeforeSaveValidate() And Convert.ToBoolean(ViewState("bFail")) = True Then
            If Convert.ToInt32(HFMentorID.Value) > 0 Then
                If Convert.ToInt32(hdnCADiaryRecordID.Value) > 0 Then
                    If AddCADiaryRecord("With Student") Then
                        Response.Redirect(ExistingDiaryPage)
                    End If
                Else
                    If AddCADiaryRecord("With Student") Then
                        clearData()
                        ValidateData()
                        lblMessage.ForeColor = Drawing.Color.Blue
                        If Convert.ToInt32(hdnCADiaryRecordID.Value) < 0 Then
                            lblMessage.Text = CStr(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                                Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.StudentDiaryEntryPage.SuccessMsg__c")), _
                                Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
                        Else
                            lblMessage.Text = CStr(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                                Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.StudentDiaryEntryPage.SuccMsg__c")), _
                                Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
                        End If

                    Else
                        lblMessage.Text = CStr(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                              Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentDiaryEntryPage.validmsg3")), _
                                            Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
                    End If

                End If

            Else
                lblMessage.Text = CStr(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                         Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentDiaryEntryPage.validmsg2")), _
                                  Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
            End If
        Else
            lblMessage.Text = CStr(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                        Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentDiaryEntryPage.RequiredFieldMsg1")), _
                            Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
        End If
    End Sub
    ''' <summary>
    ''' Submit to Mentor Student Diary Entry
    ''' </summary>
    Protected Sub btnSubmitToMentor_Click(sender As Object, e As System.EventArgs) Handles btnSubmitToMentor.Click
        Try
            If Convert.ToInt32(hdnCADiaryRecordID.Value) > 0 Then
                lblMentor.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.StudentDiaryEntryPage.SubmitToMentorWarning__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            Else
                lblMentor.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.StudentDiaryEntryPage.SubmitWarning__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            End If
            radWindowSubmiToMentor.VisibleOnPageLoad = True
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try

    End Sub

    Protected Sub btnReviewYes_Click(sender As Object, e As System.EventArgs) Handles btnReviewYes.Click
        Try

            If Convert.ToInt32(hdnCADiaryRecordID.Value) > 0 Then
                lblStatus.Text = "Submitted to Mentor"
                If BeforeSaveValidate() And Convert.ToBoolean(ViewState("bFail")) = True Then
                    If AddCADiaryRecord("Submitted to Mentor") Then
                        'Commented by Kavita Zinage 06/04/2016
                        ''clearData()
                        ''EnableControl()
                        ''ValidateData()
                        'LoadDiaryDetails()
                        'lblMessage.ForeColor = Drawing.Color.Blue
                        'lblMessage.Text = CStr(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                        '    Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.StudentDiaryEntryPage.SuccMsg__c")), _
                        '    Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
                        Response.Redirect(ExistingDiaryPage) 'Added by Kavita Zinage 06/04/2016
                    End If
                Else
                    lblMessage.Text = CStr(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                            Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentDiaryEntryPage.RequiredFieldMsg1")), _
                            Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
                End If
            Else
                'If Convert.ToString(txtStartDate.SelectedDate) <> "" And Convert.ToString(txtEndDate.SelectedDate) <> "" And txtExperience.Text <> "" And txtTitle.Text <> "" And txtEiditorDescription.Text <> "" And gvCompetency.Items.Count > 0 Then
                If BeforeSaveValidate() And Convert.ToBoolean(ViewState("bFail")) = True Then
                    If AddCADiaryRecord("Submitted to Mentor") Then
                        clearData()
                        ValidateData()
                        EnableControl()
                        lblMessage.ForeColor = Drawing.Color.Blue
                        lblMessage.Text = CStr(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                            Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.StudentDiaryEntryPage.SuccessMsg__c")), _
                            Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
                    End If
                Else
                    lblMessage.Text = CStr(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                            Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentDiaryEntryPage.RequiredFieldMsg1")), _
                            Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
                End If
            End If
            radWindowSubmiToMentor.VisibleOnPageLoad = False
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub btnReviewNo_Click(sender As Object, e As System.EventArgs) Handles btnReviewNo.Click
        Try
            radWindowSubmiToMentor.VisibleOnPageLoad = False
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try

    End Sub

    Protected Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        Try
            If Request.QueryString("Page") IsNot Nothing Then
                If Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("Page")).ToLower = "ede" Then
                    Response.Redirect(Me.ExistingDiaryPage)
                End If
            Else
                Response.Redirect(Me.StudentDashboardPage)
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try

    End Sub
#End Region

#Region "Change Events"
    ''' <summary>
    ''' Load Functional Competencies as per Competency Category
    ''' </summary>
    Protected Sub ddlExpCategory_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlExpCategory.SelectedIndexChanged
        Try
            If ddlExpCategory.SelectedValue = "Select" Then
                ddlAreaofExperience.Items.Clear()
            Else
                LoadCompetencies()
            End If

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    'Handles End date Change
    Protected Sub txtEndDate_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles txtEndDate.SelectedDateChanged
        Try
            Dim dDate As DateTime = Convert.ToDateTime(txtEndDate.SelectedDate)
            Dim sSql As String = Database & "..spCheckDiaryStartEndDateExists__c @StudentID=" & Convert.ToInt32(AptifyEbusinessUser1.PersonID) & ",@Date='" & New DateTime(dDate.Year, dDate.Month, dDate.Day) & "',@DiaryID = " & Convert.ToInt32(hdnCADiaryRecordID.Value) & ""
            Dim iCount As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSql, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
            lblMessage.Text = ""

            If Not txtStartDate.SelectedDate Is Nothing Then
                If dDate < txtStartDate.SelectedDate Then
                    ViewState("bFail") = False
                    lblMessage.Text = CStr(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                                Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentDiaryEntryPage.datevalidmsg1__c")), _
                                Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
                End If
            End If

            If iCount > 0 Or dDate <= dIntakeDate Then
                ViewState("bFail") = False
                lblMessage.Text = CStr(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                            Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentDiaryEntryPage.validmsg1")), _
                            Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
            ElseIf dDate >= dContractExpireDate AndAlso dContractExpireDate <> Nothing Then
                ViewState("bFail") = False
                lblMessage.Text = CStr(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                            Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentDiaryEntryPage.datevalidmsg_c")), _
                            Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
            Else
                ViewState("bFail") = True
            End If
            If Not txtStartDate.SelectedDate Is Nothing And Not txtEndDate.SelectedDate Is Nothing Then
                TxtTotalDays.Text = Convert.ToString(CalculateDays(Convert.ToDateTime(txtStartDate.SelectedDate), Convert.ToDateTime(txtEndDate.SelectedDate), iTotalLeaveDays))
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try

    End Sub
    'Handles Start date Change
    Protected Sub txtStartDate_SelectedDateChanged(sender As Object, e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles txtStartDate.SelectedDateChanged
        Try
            Dim dDate As DateTime = Convert.ToDateTime(txtStartDate.SelectedDate)
            Dim sSql As String = Database & "..spCheckDiaryStartEndDateExists__c @StudentID=" & Convert.ToInt32(AptifyEbusinessUser1.PersonID) & ",@Date='" & New DateTime(dDate.Year, dDate.Month, dDate.Day) & "',@DiaryID = " & Convert.ToInt32(hdnCADiaryRecordID.Value) & " "
            Dim iCount As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSql, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
            lblMessage.Text = ""
            If iCount > 0 Or dDate <= dIntakeDate Then
                ViewState("bFail") = False
                lblMessage.Text = CStr(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                            Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentDiaryEntryPage.validmsg1")), _
                            Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
                'ElseIf dDate <= dContractExpireDate Then
                '    bFail = True
                '    lblMessage.Text = CStr(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                '                Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentDiaryEntryPage.validmsg1")), _
                '                Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
            Else
                ViewState("bFail") = True
            End If
            If Not txtStartDate.SelectedDate Is Nothing And Not txtEndDate.SelectedDate Is Nothing Then
                TxtTotalDays.Text = Convert.ToString(CalculateDays(Convert.ToDateTime(txtStartDate.SelectedDate), Convert.ToDateTime(txtEndDate.SelectedDate), iTotalLeaveDays))
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try

    End Sub

    Protected Sub txtExperience_TextChanged(sender As Object, e As System.EventArgs) Handles txtExperience.TextChanged
        'Added By kavita Zinage 15/03/2016
        If Not txtStartDate.SelectedDate Is Nothing And Not txtEndDate.SelectedDate Is Nothing Then

            Dim iTotalDiffDays As Integer = 0
            Dim oParams(1) As IDataParameter
            Dim sSQL As String
            oParams(0) = DataAction.GetDataParameter("@StartDate", SqlDbType.DateTime, Convert.ToDateTime(txtStartDate.SelectedDate))
            oParams(1) = DataAction.GetDataParameter("@EndDate", SqlDbType.DateTime, Convert.ToDateTime(txtEndDate.SelectedDate))
            sSQL = Me.Database & "..spGetDiaryExperienceDaysbyDates__c"
            iTotalDiffDays = Convert.ToInt32(DataAction.ExecuteScalarParametrized(sSQL, CommandType.StoredProcedure, oParams))

            If Convert.ToInt32(txtExperience.Text) > iTotalDiffDays Then
                ViewState("bFail") = False
                'lblMessage.Text = CStr(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                '            Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentDiaryEntryPage.DayValidation")), _
                '            Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
                lblexperr.Visible = True
                lblexperr.Text = CStr(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                            Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentDiaryEntryPage.DayValidation")), _
                            Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
            Else
                lblexperr.Visible = False
                ViewState("bFail") = True
                lblMessage.Text = ""
                lblexperr.Text = ""
            End If
        End If
    End Sub
#End Region

#Region "Grid Events"

    Protected Sub grdLeave_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles grdLeave.ItemDataBound

        If (TypeOf e.Item Is GridDataItem) Then
            Dim LblDays As Label = DirectCast(e.Item.FindControl("lblDays"), Label)
            If LblDays.Text.Trim <> "" Then
                iTotalLeaveDays = iTotalLeaveDays + Convert.ToInt32(LblDays.Text)
                If Not txtStartDate.SelectedDate Is Nothing And Not txtEndDate.SelectedDate Is Nothing Then
                    TxtTotalDays.Text = Convert.ToString(CalculateDays(Convert.ToDateTime(txtStartDate.SelectedDate), Convert.ToDateTime(txtEndDate.SelectedDate), iTotalLeaveDays))
                End If
            End If
        End If

    End Sub

    Protected Sub grdLeave_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdLeave.NeedDataSource
        Try

            If ViewState("dtLeave") IsNot Nothing Then
                grdLeave.DataSource = CType(ViewState("dtLeave"), DataTable)
                'Else
                '    PanelLeave.Visible = False
            End If

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub gvCompetency_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvCompetency.NeedDataSource
        Try

            If ViewState("dtCompetency") IsNot Nothing Then
                gvCompetency.DataSource = CType(ViewState("dtCompetency"), DataTable)
                'Else
                '    PanelCompetency.Visible = False
            End If

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

#End Region

End Class