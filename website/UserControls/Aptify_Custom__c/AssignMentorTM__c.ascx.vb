'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Kavita Zinage             10/05/2015                        Firm Admin’s Mentor Assignment Page
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Imports Aptify.Framework.Web
Imports Telerik.Web.UI
Imports System.Data
Imports System.IO
Imports Aptify.Framework.Application
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.DataServices

Partial Class AssignMentorTM__c
    Inherits eBusiness.BaseUserControlAdvanced

    Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "AssignMentorTM__c"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_FirmAdminDashboardPage As String = "FirmAdminDashboardPage"

    Dim sErrorMessage As String = String.Empty
    Dim sSQL As String = String.Empty

#Region "Firm Admin’s Mentor Assignment Specific Properties"
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
        If String.IsNullOrEmpty(LoginPage) Then
            LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage)
        End If

        If String.IsNullOrEmpty(FirmAdminDashboardPage) Then
            FirmAdminDashboardPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_FirmAdminDashboardPage)
        End If

    End Sub

#End Region

#Region "Page Event"

    Protected Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        Try
            ' radStudent.Columns(1).CurrentFilterFunction = Telerik.Web.UI.GridKnownFunction.EqualTo
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            SetProperties()
            If User1.PersonID < 0 Then
                Session("ReturnToPage") = Request.RawUrl
                Response.Redirect(LoginPage)
            End If
            If Not IsPostBack Then
                ViewState("Flag") = "All"
                LoadCompany()
                ' AutoCompleteExtender11.ContextKey = cmbCompany.SelectedValue

                LoadMentors()
                LoadStudentGrid()
                'For i As Integer = 0 To radStudent.Items.Count - 1

                '    Dim AutoCompleteExtender1 As AjaxControlToolkit.AutoCompleteExtender =
                '        DirectCast(radStudent.Items(i).FindControl("AutoCompleteExtender12"), AjaxControlToolkit.AutoCompleteExtender)

                '    AutoCompleteExtender1.ContextKey = cmbCompany.SelectedValue
                'Next
                'InactivateStudentMentor(2292)
                'CreateStudentMentorRecord(2292, 10, 1, Today, Today)
                'InheritDiary(2292, 10)
            End If
            '  AutoCompleteExtender11.ContextKey = cmbCompany.SelectedValue




        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

#End Region

#Region "Popup Button Events"

    Protected Sub btnValidationOK_Click(sender As Object, e As System.EventArgs) Handles btnValidationOK.Click
        Try
            radWindowValidation.VisibleOnPageLoad = False
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

#End Region

#Region "LoadData"
    ''' <summary>
    ''' Load Active Business Unit With Parent and Child Company
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadMentors()
        Try

            Dim sSql As String = Database & "..spGetMentorDetailsForFirmAdminDashboard__c @CompanyID=" & cmbCompany.SelectedValue
            Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dt Is Nothing Then
                radMentors.DataSource = dt
                radMentors.DataTextField = "Name"
                radMentors.DataValueField = "ID"
                radMentors.DataBind()
                radMentors.SelectedIndex = 0
                If dt.Rows.Count > 0 Then
                    ViewState("Mentors") = dt
                End If

            End If

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Function To Bind Student Grid With All Students Which are Satisfying conditions
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Sub LoadStudentGrid()
        Try
            sSQL = String.Empty
            sSQL = Database & "..spGetAllStudentForMentorAssign__c @CompanyID=" & cmbCompany.SelectedValue & ",@From=" & Convert.ToInt32(txtFrom.Text) & ",@To=" & Convert.ToInt32(txtTo.Text)
            Dim dtStudent As DataTable = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dtStudent Is Nothing Then
                'radStudent.DataSource = dtStudent
                ' radStudent.DataBind()
                grdStudent.DataSource = dtStudent
                grdStudent.DataBind()
                ' ViewState("Selected") = dtStudent

            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Function To Search Mentors by name
    ''' </summary>
    Private Sub SearchMentors()
        Try
            'Dim sSql As String = Database & "..spSearchMentors__c @CompanyID=" & User1.CompanyID & ",@Name=" & txtSearch.Text.Trim()
            Dim sSql As String = Database & "..spSearchMentors__c @CompanyID=" & cmbCompany.SelectedValue & ",@Name= '" & txtSearch.Text.Trim() & "'"
            Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dt Is Nothing Then
                radMentors.DataSource = dt
                radMentors.DataTextField = "Name"
                radMentors.DataValueField = "ID"
                radMentors.DataBind()
                radMentors.SelectedIndex = 0
                If dt.Rows.Count > 0 Then
                    ViewState("Mentors") = dt
                End If
            End If

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Function To Bind Student Grid With Selected Mentor From Listbox
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Sub LoadStudentGridByMentor()
        Try
            sSQL = String.Empty
            sSQL = Database & "..spGetAllStudentForMentorAssignMentorBy__c @CompanyID=" & cmbCompany.SelectedValue & ",@MentorID=" & radMentors.SelectedValue & " ,@From=" & Convert.ToInt32(txtFrom.Text) & ",@To=" & Convert.ToInt32(txtTo.Text)
            Dim dtStudent As DataTable = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dtStudent Is Nothing Then
                ' radStudent.DataSource = dtStudent
                grdStudent.DataSource = dtStudent
                grdStudent.DataBind()


            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    'Added by Kavita Zinage #18180
    ''' <summary>
    ''' Function To Bind Student Grid With Additional Search Criteria
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Sub LoadStudentGridByOpt(ByVal iNoneAssigne As Integer, ByVal sStudentID As String, ByVal iMentorID As Integer, ByVal iOption As Integer)
        Try

            sSQL = String.Empty
            sSQL = Database & "..spGetStudentDetailForMentorAssign__c @CompanyID=" & cmbCompany.SelectedValue & ",@From=" & Convert.ToInt32(txtFrom.Text) & ",@To=" & Convert.ToInt32(txtTo.Text) & ",@NoneAssigne =" & Convert.ToInt32(iNoneAssigne) & ",@Option=" & Convert.ToInt32(iOption) & ",@StudentID='" & Convert.ToString(sStudentID) & "',@MentorID=" & iMentorID

            Dim dtStudent As DataTable = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dtStudent Is Nothing Then
                grdStudent.DataSource = dtStudent
                grdStudent.DataBind()
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    'Till Here Log #18180
#End Region

#Region "Private Function"

    Function validateBeforeSave() As Boolean
        Dim CheckCount As Integer = 0
        lblError.Text = ""
        lblError.Visible = False
        Try
            For i As Integer = 0 To grdStudent.Rows.Count - 1
                If CType(grdStudent.Rows(i).FindControl("chkStudent"), CheckBox).Checked = True Then
                    Dim ddlMentor As DropDownList = DirectCast(grdStudent.Rows(i).FindControl("ddlMentor"), DropDownList)
                    Dim dStartDate As TextBox = DirectCast(grdStudent.Rows(i).FindControl("txtGvStartDate"), TextBox)
                    Dim dEndDate As TextBox = DirectCast(grdStudent.Rows(i).FindControl("txtGvEndDate"), TextBox)
                    If ddlMentor.SelectedIndex = 0 Or dStartDate.Text = String.Empty Or dEndDate.Text = String.Empty Then
                        lblError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.MentorAssignment.CheckGridRecordValidationMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        lblError.Visible = True
                        Return False
                    End If
                    CheckCount = CheckCount + 1
                    ' Return True
                End If
            Next

            If CheckCount = 0 Then
                lblError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.MentorAssignment.CheckGridRecordValidationMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblError.Visible = True
                Return False
            End If
            Return True
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
        Return True
    End Function
    Public Function InactivateStudentMentor(StudentID As Integer) As Boolean
        Try
            Dim sSQL As String, dtStudentMentor As Data.DataTable
            Dim oStudentMentorGE As AptifyGenericEntityBase
            sSQL = Me.AptifyApplication.GetEntityBaseDatabase("StudentMentors__c") & "..spUpdateInactiveStudentMentor__c @StudentId=" & Convert.ToString(StudentID)
            Me.DataAction.ExecuteNonQuery(sSQL)
            Me.DataAction.CommitTransaction()

            Return True
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            Return False
        End Try
    End Function

    Public Function CreateStudentMentorRecord(StudentID As Integer, MentorID As Integer, CompanyID As Integer, StartDate As Date, EndDate As Date) As Boolean
        Dim sErrorMessage As String = String.Empty
        Dim StudentMentorGE As AptifyGenericEntityBase
        Try


            StudentMentorGE = Me.AptifyApplication.GetEntityObject("StudentMentors__c", -1)
            With StudentMentorGE
                .SetValue("StudentID", Convert.ToInt32(StudentID))
                .SetValue("MentorID", Convert.ToInt32(MentorID))
                .SetValue("CompanyID", Convert.ToInt32(CompanyID))
                If Convert.ToString(StartDate) <> "" Then
                    .SetValue("StartDate", Convert.ToDateTime(StartDate))
                End If
                If Convert.ToString(EndDate) <> "" And Convert.ToString(EndDate) <> "1900-01-01" Then
                    .SetValue("EndDate", Convert.ToDateTime(EndDate))
                End If
                '.SetValue("Active", True)
                sErrorMessage = String.Empty
                If StudentMentorGE.Save(False, sErrorMessage) Then
                Else
                    lblError.Text = StudentMentorGE.LastUserError
                    lblError.Visible = True
                End If

            End With

            Return True
        Catch ex As System.Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            Return False
        Finally

        End Try
        Return True
    End Function

    Public Function InheritDiary(StudentID As Integer, MentorID As Integer) As Boolean
        Try

            Dim sSQL As String, dtStudentDiary As Data.DataTable
            Dim StudentDiaryGE As AptifyGenericEntityBase
            sSQL = Me.AptifyApplication.GetEntityBaseDatabase("StudentDiaryEntries__c") & "..spGetAllOpenStudentDiariesByStudent__c @StudentID=" & Convert.ToString(StudentID)
            dtStudentDiary = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If dtStudentDiary IsNot Nothing Then
                If dtStudentDiary.Rows.Count > 0 Then
                    For Each dr As DataRow In dtStudentDiary.Rows
                        StudentDiaryGE = Me.AptifyApplication.GetEntityObject("StudentDiaryEntries__c", Convert.ToInt32(Convert.ToString(dr("ID"))))
                        With StudentDiaryGE
                            .SetValue("MentorID", Convert.ToInt32(MentorID))
                            Try
                                sErrorMessage = String.Empty
                                StudentDiaryGE.Save(False, sErrorMessage)
                            Catch ex As Exception
                                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                            End Try
                        End With
                    Next
                End If
            End If
            Return True
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            Return False
        End Try
    End Function
#End Region

#Region "Grid Events"

    
#End Region

#Region "Button Click"

    Protected Sub btnMentor_Click(sender As Object, e As System.EventArgs) Handles btnMentor.Click
        Try
            lblmsg.Visible = False
            ViewState("Flag") = "ByMentor"
            'Added By Kavita ZInage #18180
            chknoneassign.Checked = False
            txtStudent.Text = ""
            'Till Here
            grdStudentLoad()

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try

    End Sub

    Protected Sub btnShowAll_Click(sender As Object, e As System.EventArgs) Handles btnShowAll.Click
        Try
            lblmsg.Visible = False
            ViewState("Flag") = "All"
            'Added By Kavita ZInage #18180
            chknoneassign.Checked = False
            txtStudent.Text = ""
            'Till Here
            grdStudentLoad()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub btnMainBack_Click(sender As Object, e As System.EventArgs) Handles btnMainBack.Click
        Try
            lblmsg.Visible = False
            Response.Redirect(FirmAdminDashboardPage, False)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
        Try
            Dim iSuccCount As Integer = 0
            Dim iSuccCnt As Integer = 0
            Dim iSucCnt As Integer = 0

            If validateBeforeSave() Then
                For i As Integer = 0 To grdStudent.Rows.Count - 1
                    If DirectCast(grdStudent.Rows(i).FindControl("chkStudent"), CheckBox).Checked = True Then
                        Dim sTransID As String = DataAction.BeginTransaction(IsolationLevel.Serializable, True)
                        DirectCast(grdStudent.Rows(i).FindControl("chkStudent"), CheckBox).Checked = False
                        Dim ddlMentorIDgv As DropDownList = DirectCast(grdStudent.Rows(i).FindControl("ddlMentor"), DropDownList)
                        Dim hdnCompanyIDgv As HiddenField = DirectCast(grdStudent.Rows(i).FindControl("hdnCompanyID"), HiddenField)
                        Dim hdnStudentIDgv As HiddenField = DirectCast(grdStudent.Rows(i).FindControl("hdnStudentID"), HiddenField)
                        Dim StartDate As TextBox = DirectCast(grdStudent.Rows(i).FindControl("txtGvStartDate"), TextBox)
                        Dim EndDate As TextBox = DirectCast(grdStudent.Rows(i).FindControl("txtGvEndDate"), TextBox)
                        Dim hdnMentorID As HiddenField = DirectCast(grdStudent.Rows(i).FindControl("hdnMentorID"), HiddenField)
                        Dim hdnStartDate As HiddenField = DirectCast(grdStudent.Rows(i).FindControl("hdnGvStartDate"), HiddenField)
                        Dim hdnEndDate As HiddenField = DirectCast(grdStudent.Rows(i).FindControl("hdnGvEndDate"), HiddenField)
                        If hdnStartDate.Value.Trim() = "" Then
                            hdnStartDate.Value = Convert.ToDateTime("01/01/1900")
                        End If
                        If hdnEndDate.Value.Trim() = "" Then
                            hdnEndDate.Value = Convert.ToDateTime("01/01/1900")
                        End If



                        If StartDate.Text.Trim() <> Convert.ToDateTime(hdnStartDate.Value).ToShortDateString() Or EndDate.Text.Trim() <> Convert.ToDateTime(hdnEndDate.Value).ToShortDateString() Or ddlMentorIDgv.SelectedValue <> hdnMentorID.Value Then

                            If InactivateStudentMentor(Convert.ToInt32(hdnStudentIDgv.Value)) Then
                                iSucCnt = iSucCnt + 1
                            End If

                            If CreateStudentMentorRecord(Convert.ToInt32(hdnStudentIDgv.Value), Convert.ToInt32(ddlMentorIDgv.SelectedValue), Convert.ToInt32(hdnCompanyIDgv.Value), StartDate.Text, EndDate.Text) Then
                                iSuccCount = iSuccCount + 1
                            End If

                            If InheritDiary(Convert.ToInt32(hdnStudentIDgv.Value), Convert.ToInt32(ddlMentorIDgv.SelectedValue)) Then
                                iSuccCnt = iSuccCnt + 1
                            End If
                        End If


                    End If
                Next

                grdStudentLoad()
                If iSuccCount > 0 Or iSucCnt > 0 Then 'Or iSuccCnt > 0
                    lblmsg.Visible = True
                    lblmsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.MentorAssignmentPage.SuccessMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                End If

            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            lblmsg.Visible = False
            If txtSearch.Text.Trim <> "" And txtSearch.Text.Trim <> "Search..." Then
                SearchMentors()
            Else
                LoadMentors()
                txtSearch.Text = "Search..."
            End If

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
#End Region

    Protected Sub lnkShowAll_Click(sender As Object, e As System.EventArgs) Handles lnkShowAll.Click
        Try
            LoadMentors()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    'Protected Sub txtSearch_TextChanged(sender As Object, e As System.EventArgs) Handles txtSearch.TextChanged
    '    Try
    '        If txtSearch.Text.Trim <> "" Then
    '            SearchMentors()
    '        Else
    '            LoadMentors()
    '        End If
    '    Catch ex As Exception
    '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
    '    End Try
    'End Sub

    Protected Sub ToggleSelectedState(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim headerCheckBox As CheckBox = CType(sender, CheckBox)
            
            For Each item As GridViewRow In grdStudent.Rows
                If CType(item.FindControl("chkStudent"), CheckBox).Enabled = True Then
                    CType(item.FindControl("chkStudent"), CheckBox).Checked = headerCheckBox.Checked

                    'item.Selected = headerCheckBox.Checked
                End If
            Next
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try

    End Sub


    Protected Sub cmbCompany_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCompany.SelectedIndexChanged
        lblmsg.Visible = False
        
        ViewState("Flag") = "All"
        LoadMentors()
        grdStudentLoad()
        
    End Sub


    ''' <summary>
    ''' Load Active Business Unit With Parent and Child Company
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadCompany()
        Try
            'Added by Swati
            Dim sSql1 As String = Database & "..spGetAllSubsidiariesWithCity__c @ParentCompanyId=" & User1.CompanyID
            Dim dtCompany As DataTable = DataAction.GetDataTable(sSql1, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dtCompany Is Nothing AndAlso dtCompany.Rows.Count > 0 Then
                cmbCompany.ClearSelection()
                cmbCompany.DataSource = dtCompany
                'cmbOfficeLocation.DataTextField = "Name"
                cmbCompany.DataTextField = "FirmCity"
                cmbCompany.DataValueField = "ID"
                cmbCompany.DataBind()
                cmbCompany.SelectedValue = User1.CompanyID
                'cmbCompany.Items.Insert(0, "Select")
                ViewState("SubsidariesCompanyDT") = dtCompany
            Else
                ViewState("SubsidariesCompanyDT") = Nothing
            End If
            'End Swati

        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub



    Protected Sub grdStudent_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdStudent.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim ddlMentor As DropDownList = CType(e.Row.FindControl("ddlMentor"), DropDownList)
                Dim hdnMentor As HiddenField = DirectCast(e.Row.FindControl("hdnMentor"), HiddenField)

                If Not ViewState("Mentors") Is Nothing Then
                    ddlMentor.ClearSelection()
                    ddlMentor.DataSource = CType(ViewState("Mentors"), DataTable)
                    ddlMentor.DataTextField = "Name"
                    ddlMentor.DataValueField = "ID"
                    ddlMentor.DataBind()
                    ddlMentor.Items.Insert(0, "Select")
                    ddlMentor.SelectedValue = hdnMentor.Value
                Else
                    ddlMentor.Items.Insert(0, "Select")
                End If

                Dim dStartDate As TextBox = DirectCast(e.Row.FindControl("txtGvStartDate"), TextBox)
                Dim dEndDate As TextBox = DirectCast(e.Row.FindControl("txtGvEndDate"), TextBox)
                If dStartDate.Text.Trim() = String.Empty Then
                    'dStartDate.MinDate = Today
                    dStartDate.Text = New Date(Today.Year, Today.Month, Today.Day)
                    dEndDate.Text = Today().AddDays(1)
                Else
                    Dim dStartDate1 As DateTime = Convert.ToDateTime(dStartDate.Text)
                    dStartDate.Text = New Date(dStartDate1.Year, dStartDate1.Month, dStartDate1.Day)

                End If

                If dEndDate.Text.Trim() <> String.Empty Then
                    Dim dEndDate1 As DateTime = Convert.ToDateTime(dEndDate.Text)
                    dEndDate.Text = New Date(dEndDate1.Year, dEndDate1.Month, dEndDate1.Day)
                End If

            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub


    Private Sub grdStudentLoad()
        Try
            If Convert.ToString(ViewState("Flag")) = "All" Then
                LoadStudentGrid()

            ElseIf Convert.ToString(ViewState("Flag")) = "ByMentor" Then
                LoadStudentGridByMentor()
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click

        lblmsg.Visible = False
        If Not ViewState("Flag") Is Nothing Then
            If ViewState("Flag") = "All" Then
                ViewState("Flag") = "All"
            Else
                ViewState("Flag") = "ByMentor"
            End If
        Else
            ViewState("Flag") = "All"
        End If
        grdStudentLoad()
    End Sub
    'Added by Kavita Zinage - #18180
    ''' <summary>
    ''' Handles the Click event of the btnfind control. #18180
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Protected Sub btnfind_Click(sender As Object, e As EventArgs) Handles btnfind.Click
        Try

            Dim iNonAssignee As Integer = 0
            Dim iOption As Integer
            If chknoneassign.Checked Then
                iNonAssignee = 1
                iOption = 3
            Else
                iOption = 3
            End If
            If txtStudent.Text <> "" Then
                LoadStudentGridByOpt(iNonAssignee, Convert.ToString(txtStudent.Text), -1, iOption)
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Loads the none assigned trainee. #18180
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Protected Sub LoadNoneAssignedTrainee(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim iNonAssignee As Integer = 0
            Dim iOption As Integer
            If chknoneassign.Checked Then
                iNonAssignee = 1
                iOption = 2
            Else
                iOption = 1
            End If
            LoadStudentGridByOpt(iNonAssignee, Convert.ToString(txtStudent.Text), -1, iOption)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    'Till Here
End Class
