Option Explicit On
Option Strict On
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' 
'Developer                  Date modified               Comments 
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Milind Sutar               23/01/2015                  Student result page
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Imports System.Data
Imports System.Collections.Generic
Imports Telerik.Web.UI
Imports System.Linq
Imports System.Drawing
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Telerik.Web.UI.PivotGrid
Imports Telerik.Web.UI.PivotGrid.Core
Imports System.Windows.Threading


Partial Class UserControls_Aptify_Custom__c_StudentResults__c
    Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
    Dim dts As DataTable 'added by Ashwini for #17426
#Region "Properties"

    Private _dataTable As DataTable
    Private _enrollmentTable As DataTable
    Private _studentsTable As DataTable
    Private _studentsDetailsTable As DataTable

    Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "StudentResult__c"
    Protected Const ATTRIBUTE_MANAGE_MYGROUP_URL_NAME As String = "ManageMyGroupURL"
    Protected Const ATTRIBUTE_MANAGE_MYGROUP_URL_FIRMPORTALPAGE As String = "FirmPortalPage" 'Added by Kavita to Navigate Firm Admin Student Assignment Details
    ''adeed by Pradip 2016-02-25 for G3-37 Tracker Item
    Protected Const ATTRIBUTE_EDUCATIONRESULT_URL_NAME As String = "EducationResulePage"
    ''' <summary>
    ''' Get sets manage my groups page url
    ''' </summary>
    Public Overridable Property ManageMyGroupURL() As String
        Get
            If Not ViewState(ATTRIBUTE_MANAGE_MYGROUP_URL_NAME) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_MANAGE_MYGROUP_URL_NAME))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_MANAGE_MYGROUP_URL_NAME) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    'Added by Kavita to Navigate Firm Admin Student Assignment Details
    Public Overridable Property FirmPortalPage() As String
        Get
            If Not ViewState(ATTRIBUTE_MANAGE_MYGROUP_URL_FIRMPORTALPAGE) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_MANAGE_MYGROUP_URL_FIRMPORTALPAGE))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_MANAGE_MYGROUP_URL_FIRMPORTALPAGE) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property


    ''Added by Pradip 2016-02-25 for G3-37
    Public Overridable Property EducationresultURL() As String
        Get
            If Not ViewState(ATTRIBUTE_EDUCATIONRESULT_URL_NAME) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_EDUCATIONRESULT_URL_NAME))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_EDUCATIONRESULT_URL_NAME) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property
#End Region

#Region "Protected Methods"

    ''' <summary>
    ''' Hanndles page load event 
    ''' </summary>
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblMessage.Text = String.Empty
            LoadAcademicYear()
            ''Added BY Pradip 2016-02-19 for G3-37
            FillCurriculum()
            FillSession()
            SetProperties()
        End If
        LoadStudentResults()

    End Sub

    Protected Overrides Sub SetProperties()
        Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
        MyBase.SetProperties()
        If String.IsNullOrEmpty(Me.ManageMyGroupURL) Then
            Me.ManageMyGroupURL = Me.GetLinkValueFromXML(ATTRIBUTE_MANAGE_MYGROUP_URL_NAME)
        End If

        'Added by Kavita to Navigate Firm Admin Student Assignment Details
        If String.IsNullOrEmpty(FirmPortalPage) Then
            FirmPortalPage = Me.GetLinkValueFromXML(ATTRIBUTE_MANAGE_MYGROUP_URL_FIRMPORTALPAGE)
        End If
        ''Added by Pradip 2016-02-25 For G3-37 Tracker Item
        If String.IsNullOrEmpty(EducationresultURL) Then
            EducationresultURL = Me.GetLinkValueFromXML(ATTRIBUTE_EDUCATIONRESULT_URL_NAME)
        End If
    End Sub

    ''' <summary>
    ''' Handles need data source to bind data to pivot grid
    ''' </summary>
    Protected Sub gvStudentResult_NeedDataSource(sender As Object, e As Telerik.Web.UI.PivotGridNeedDataSourceEventArgs) Handles gvStudentResult.NeedDataSource
        Try
            If _enrollmentTable IsNot Nothing AndAlso _enrollmentTable.Rows.Count > 0 Then
                Dim DistinctDt As DataTable = _enrollmentTable.DefaultView.ToTable(True, "Curriculum")
                If DistinctDt.Rows.Count = _enrollmentTable.Rows.Count Then
                    gvStudentResult.ColumnGroupsDefaultExpanded = True
                End If

 Dim query = _enrollmentTable.AsEnumerable().Select(Function(p) p.Field(Of String)("StudentID")).Distinct().ToList()
                ''Added By Pradip 2016-06-15 For Vertical Scroll Bar Issue No-13856
                If query.Count > 10 Then
                    gvStudentResult.ClientSettings.Scrolling.AllowVerticalScroll = True
                    Try
                        gvStudentResult.ClientSettings.Scrolling.ScrollHeight = 500
                    Catch ex As Exception
                        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                    End Try
                Else
                    gvStudentResult.ClientSettings.Scrolling.AllowVerticalScroll = False
                    gvStudentResult.ClientSettings.Scrolling.ScrollHeight = 0
                End If
                gvStudentResult.Visible = True
                gvStudentResult.DataSource = _enrollmentTable
            Else
                gvStudentResult.DataSource = Nothing
                gvStudentResult.Visible = False
                lblNoRecords.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                    Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.PivotGrid.NoRecords")), _
                    Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            End If
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Handles cell data bound event to set cell color according to score value
    ''' </summary>
    Protected Sub gvStudentResult_CellDataBound(sender As Object, e As Telerik.Web.UI.PivotGridCellDataBoundEventArgs) Handles gvStudentResult.CellDataBound
        Try
            If TypeOf e.Cell Is PivotGridRowHeaderCell Then
                If e.Cell.Controls.Count > 0 Then
                    If e.Cell.Controls(0) IsNot Nothing Then
                        TryCast(e.Cell.Controls(0), Button).Visible = False
                    End If
                End If
            ElseIf TypeOf e.Cell Is PivotGridColumnHeaderCell Then
                If e.Cell.Controls.Count > 0 Then
                    If e.Cell.Controls(0) IsNot Nothing Then
                        TryCast(e.Cell.Controls(0), Button).CssClass.Remove(1)
                    End If
                End If
            End If

            If TypeOf e.Cell Is PivotGridDataCell Then
                Dim cell As PivotGridDataCell = TryCast(e.Cell, PivotGridDataCell)
                If cell.CellType = PivotGridDataCellType.DataCell And
                    cell.CellType <> PivotGridDataCellType.RowTotalDataCell And
                    cell.CellType <> PivotGridDataCellType.RowAndColumnTotal And
                    cell.CellType <> PivotGridDataCellType.ColumnTotalDataCell And
                    cell.CellType <> PivotGridDataCellType.ColumnGrandTotalRowTotal Then
                    If Not e.Cell.DataItem Is Nothing Then
                        If e.Cell.DataItem.ToString().Length > 0 Then
                            Dim label As Label = TryCast(e.Cell.FindControl("lblScore"), Label)

                            'Dim faeResult As Integer = Convert.ToInt32(e.Cell.DataItem)
                            Dim result As Decimal = Convert.ToDecimal(label.Text)

                            Select Case result
                                Case -1
                                    e.Cell.BackColor = Color.FromName("Gray")
                                    Exit Select
                                Case -100
                                    label.Text = String.Empty
                                    Exit Select
                                Case -1111
                                    'e.Cell.BackColor = Color.FromName("Yellow")
                                    'label.Text = "Passed"
                                    ' label.ForeColor = Color.Green
                                    label.Text = "Appeal Exits"
                                    'label.ForeColor = Color.Green
                                    label.Visible = True
                                    Exit Select
                                Case -111
                                    ''Commented By Pradip 2016-02-18 For G3-36
                                    ' e.Cell.BackColor = Color.FromName("Green")
                                    label.Text = "Passed"
                                    'label.ForeColor = Color.Green
                                    label.Visible = True
                                    Exit Select
                                Case -2222
                                    'e.Cell.BackColor = Color.FromName("Yellow")
                                    ' label.Text = "Failed"
                                    label.Text = "Appeal Exits"
                                    'label.ForeColor = Color.Red
                                    label.Visible = True
                                    Exit Select
                                Case -222
                                    ''Commented By Pradip 2016-02-18 For G3-36
                                    'e.Cell.BackColor = Color.FromName("Red")
                                    label.Text = "Failed"
                                    'label.ForeColor = Color.Red
                                    label.Visible = True
                                    Exit Select
                                Case -333
                                    e.Cell.BackColor = Color.FromName("White")
                                    Exit Select
                                Case Is >= 1000
                                    e.Cell.BackColor = Color.FromName("Yellow")
                                    label.Text = (result / 1000).ToString()
                                    Exit Select
                                Case -6666
                                    label.Text = "NA (Not addressed)"
                                    label.Visible = True
                                    Exit Select
                                Case -6667
                                    label.Text = "NC (Nominal Competence)"
                                    label.Visible = True
                                    Exit Select
                                Case -6668
                                    label.Text = "RC (Reaching Competence)"
                                    label.Visible = True
                                    Exit Select
                                Case -6669
                                    label.Text = "C (Competent)"
                                    label.Visible = True
                                    Exit Select
                                Case -6670
                                    label.Text = "HC (Highly Competent)"
                                    label.Visible = True
                                    Exit Select
                                 Case -777
                                    ''Commented By Pradip 2016-02-18 For G3-36
                                    'e.Cell.BackColor = Color.FromName("Red")
                                    label.Text = "Absent"
                                    'label.ForeColor = Color.Red
                                    label.Visible = True
                                    Exit Select
                            End Select
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Handles academic cycle dropdown change event to load results
    ''' based on academic cycle selection
    ''' </summary>
    Protected Sub ddlAcademicYear_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlAcademicYear.SelectedIndexChanged
        LoadDefaultSetting()
        grdTopScore.Rebind()
    End Sub

    ''' <summary>
    ''' Handles the back button event
    ''' </summary>
    Protected Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        Response.Redirect(Me.ManageMyGroupURL)
    End Sub

    Protected Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        If Not IsPostBack Then
            LoadDefaultSetting()
        End If
    End Sub



    ''Added By Pradip 
    Protected Sub grdTopScore_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdTopScore.NeedDataSource
        Try
            LoadTopScore()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub grdTopScore_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles grdTopScore.ItemCommand
        Try
            Dim lnkButton As LinkButton = DirectCast(e.Item.FindControl("lbtnStudent"), LinkButton)
            Select Case e.CommandName
                Case "Student"
                    Dim sYear As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(ddlAcademicYear.SelectedValue)
                    Dim sID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(e.CommandArgument.ToString())
                    Dim strFilePath As String = EducationresultURL & "?Year=" & System.Web.HttpUtility.UrlEncode(sYear) & "&ID=" & System.Web.HttpUtility.UrlEncode(sID)
                    Response.Redirect(strFilePath, False)
            End Select
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub grdTopScore_PreRender(sender As Object, e As System.EventArgs) Handles grdTopScore.PreRender
        If grdTopScore.MasterTableView.GetItems(GridItemType.NoRecordsItem).Count > 0 Then
            Dim norecordItem As GridNoRecordsItem = DirectCast(grdTopScore.MasterTableView.GetItems(GridItemType.NoRecordsItem)(0), GridNoRecordsItem)
            Dim lblnorecordItem As Label = DirectCast(norecordItem.FindControl("lblNoGainingExp"), Label)
            lblnorecordItem.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.TopScorer.NoRecord__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
        End If
    End Sub


#End Region

#Region "Private Methods"

    ''' <summary>
    ''' Load all academic cycles 
    ''' </summary>
    Private Sub LoadAcademicYear()
        Try
            Dim currentAcademicCycleId As Integer
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0} ..spGetAllAcademicYears__c", Database)
            ddlAcademicYear.DataSource = DataAction.GetDataTable(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            ddlAcademicYear.DataValueField = "Id"
            ddlAcademicYear.DataTextField = "Name"
            ddlAcademicYear.DataBind()
            ''Added BY Pradip 2015-12-28 for Group 3 Tracker G3-35 to set the Accdemic Cycle To Current Academic.
            currentAcademicCycleId = LoadCurrentAcademicCycleID()
            If currentAcademicCycleId <> 0 Then
                ddlAcademicYear.SelectedValue = CStr(currentAcademicCycleId)
            End If
        Catch ex As Exception
            lblMessage.Text = ex.Message
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    '''Added By Pradip 2015-12-28 To load current academic cycle id by current date
    ''' </summary>

    Private Function LoadCurrentAcademicCycleID() As Integer
        Dim id As Integer = 0
        Dim sql As New StringBuilder()
        sql.AppendFormat("{0} ..spGetDefaultAcademicCycleToSet__c", Me.Database)
        id = CInt(DataAction.ExecuteScalar(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
        Return id
    End Function

    ''' <summary>
    ''' Load studnent result
    ''' </summary>
    Private Sub LoadStudentResults()
        Try
            lblNoRecords.Text = String.Empty
            gvStudentResult.FilterWindow.Localization.OK = "OK"
            gvStudentResult.FilterWindow.Localization.Cancel = "Cancel"
            ' Dim sql As New StringBuilder()
            'sql.AppendFormat("{0} ..spGetStudentResult__c @AcademicCycleID={1},@CompanyID={2}", Me.Database, ddlAcademicYear.SelectedValue.ToString(), loggedInUser.CompanyID)
            Dim sSQL As String = Database & "..spGetStudentResult__c"
            Dim param(3) As IDataParameter
            param(0) = DataAction.GetDataParameter("@AcademicCycleID", SqlDbType.Int, ddlAcademicYear.SelectedValue.ToString())
            param(1) = DataAction.GetDataParameter("@CompanyID", SqlDbType.Int, loggedInUser.CompanyID)
            param(2) = DataAction.GetDataParameter("@CurrentStage", SqlDbType.NVarChar, ddlCurrentStage.SelectedItem.Text)
			 'Added by Dipali for session filters 3/17/2017
            param(3) = DataAction.GetDataParameter("@SessionName", SqlDbType.NVarChar, ddlSession.SelectedItem.Text.Trim)
			
            _enrollmentTable = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param, 600)

            ' _enrollmentTable = Me.DataAction.GetDataTable(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' to do the default settings on reload
    ''' </summary>
    Private Sub LoadDefaultSetting()
        Try
            If _enrollmentTable IsNot Nothing AndAlso _enrollmentTable.Rows.Count > 0 Then
                gvStudentResult.ColumnGroupsDefaultExpanded = True
                Dim curriculumTable As DataTable = _enrollmentTable.DefaultView.ToTable(True, "Curriculum")
                Dim curriculums As New List(Of String)()
                Dim cout As Integer = 0
                For Each row As DataRow In curriculumTable.Rows
                    If cout <> 0 Then
                        gvStudentResult.CollapsedColumnIndexes.Add(New String() {row("Curriculum").ToString()})
                    End If
                    cout = cout + 1
                Next
            End If
            gvStudentResult.Rebind()
        Catch ex As Exception
            lblMessage.Text = ex.Message
            lblMessage.ForeColor = Color.Red
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub


    ' ''Added By Pradip 2016-02-19 To Fill Curriculum
    Private Sub FillCurriculum()
        Try
            ' Dim sSql As String = Database & "..spGetCurriculumDefinationDetailsWithBridging__c"
            ''Added BY Pradip 2017-02-06 https://redmine.softwaredesign.ie/issues/16518
            Dim sSql As String = Database & "..spGetCurriculumForStudentResult__c"
            Dim dt As DataTable = DataAction.GetDataTable(sSql, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            ddlCurrentStage.DataSource = dt
            ddlCurrentStage.DataTextField = "Name"
            ddlCurrentStage.DataValueField = "Name"
            ddlCurrentStage.DataBind()
            ddlCurrentStage.Items.Insert(0, "All")
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try

    End Sub

    ' ''Added By Pradip 2016-02-19 To Fill Session
    Private Sub FillSession()
        Try
            Dim sSql As String = Database & "..spGetAllSessionDetail__c"
            Dim dt As DataTable = DataAction.GetDataTable(sSql, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            ddlSession.DataSource = dt
            ddlSession.DataTextField = "Name"
            ddlSession.DataValueField = "ID"
            ddlSession.DataBind()
            ddlSession.Items.Insert(0, "All")
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Added BY Pradip 2016-02-19 to fill Student To Score
    ''' </summary>
    Public Sub LoadTopScore()
        Try

            Dim sSql As String = Database & "..spGetStudentResultTopScorer__c"
            Dim param(3) As IDataParameter
            param(0) = DataAction.GetDataParameter("@AcademicCycleID", SqlDbType.Int, ddlAcademicYear.SelectedValue)
            param(1) = DataAction.GetDataParameter("@CompanyID", SqlDbType.Int, loggedInUser.CompanyID)
            param(2) = DataAction.GetDataParameter("@CurrentStage", SqlDbType.NVarChar, ddlCurrentStage.SelectedItem.Text.Trim)
            param(3) = DataAction.GetDataParameter("@SessionName", SqlDbType.NVarChar, ddlSession.SelectedItem.Text.Trim)
            Dim dtsubscription As DataTable = DataAction.GetDataTableParametrized(sSql, CommandType.StoredProcedure, param,600)
            If Not dtsubscription Is Nothing Then
                Me.grdTopScore.DataSource = dtsubscription
                'Me.grdTopScore.DataBind()
                Me.grdTopScore.Visible = True
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

#End Region

    'Added by Kavita to Navigate Firm Admin Student Assignment Details
    Protected Sub lnkAssignment_Click(sender As Object, e As System.EventArgs) Handles lnkAssignment.Click
        Response.Redirect(FirmPortalPage)
    End Sub

    ''Added BY Pradip 2016-02-19 for G3-37 Tracker
    Protected Sub ddlCurrentStage_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCurrentStage.SelectedIndexChanged
        Try
            LoadDefaultSetting()
            grdTopScore.Rebind()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try

    End Sub

    Protected Sub ddlSession_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSession.SelectedIndexChanged
        Try
		'Added by Dipali for session filters 3/17/2017
            LoadDefaultSetting()
            grdTopScore.Rebind()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ' Added by  Ashwini for redmine log #17426
    Dim dtClasses As DataTable
    Private Function AddExcelStyling() As String
        Dim sb As New StringBuilder()
        sb.Append("<html xmlns:o='urn:schemas-microsoft-com:office:office'" +
            "xmlns:x='urn:schemas-microsoft-com:office:excel'" +
            "xmlns='http://www.w3.org/TR/REC-html40'>" +
            "<head>")

        sb.Append("<style>")

        sb.Append("@page")
        sb.Append("{margin:.5in .75in .5in .75in;")

        sb.Append("mso-header-margin:.5in;")
        sb.Append("mso-footer-margin:.5in;")

        sb.Append("mso-page-orientation:landscape;}")
        sb.Append("</style>")

        sb.Append("<!--[if gte mso 9]><xml>")
        sb.Append("<x:ExcelWorkbook>")

        sb.Append("<x:ExcelWorksheets>")
        sb.Append("<x:ExcelWorksheet>")

        sb.Append("<x:Name>Projects 3 </x:Name>")
        sb.Append("<x:WorksheetOptions>")

        sb.Append("<x:Print>")
        sb.Append("<x:ValidPrinterInfo/>")

        sb.Append("<x:PaperSizeIndex>9</x:PaperSizeIndex>")
        sb.Append("<x:HorizontalResolution>600</x:HorizontalResolution")

        sb.Append("<x:VerticalResolution>600</x:VerticalResolution")
        sb.Append("</x:Print>")

        sb.Append("<x:Selected/>")
        sb.Append("<x:DoNotDisplayGridlines/>")

        sb.Append("<x:ProtectContents>False</x:ProtectContents>")
        sb.Append("<x:ProtectObjects>False</x:ProtectObjects>")

        sb.Append("<x:ProtectScenarios>False</x:ProtectScenarios>")
        sb.Append("</x:WorksheetOptions>")

        sb.Append("</x:ExcelWorksheet>")
        sb.Append("</x:ExcelWorksheets>")

        sb.Append("<x:WindowHeight>12780</x:WindowHeight>")
        sb.Append("<x:WindowWidth>19035</x:WindowWidth>")

        sb.Append("<x:WindowTopX>0</x:WindowTopX>")
        sb.Append("<x:WindowTopY>15</x:WindowTopY>")

        sb.Append("<x:ProtectStructure>False</x:ProtectStructure>")
        sb.Append("<x:ProtectWindows>False</x:ProtectWindows>")

        sb.Append("</x:ExcelWorkbook>")
        sb.Append("</xml><![endif]-->")

        sb.Append("</head>")
        sb.Append("<body>")

        Return sb.ToString()

    End Function

    ''' <summary>
    ''' Below code added by Ashwini for #17426
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Protected Sub lnkCAP1_Click(sender As Object, e As EventArgs) Handles lnkCAP1.Click
        Try
            Dim sSql As String
            'Dim ida As Long
            'ida = loggedInUser.CompanyID
            sSql = Database & "..spGetExamDetailsasperCurriculumID__c"
            'dtClasses = Me.DataAction.GetDataTable(sSql, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            Dim param(1) As IDataParameter
            param(0) = DataAction.GetDataParameter("@CAP", SqlDbType.Int, 2)
            param(1) = DataAction.GetDataParameter("@ida", SqlDbType.Int, loggedInUser.CompanyID)
            dts = DataAction.GetDataTableParametrized(sSql, CommandType.StoredProcedure, param)
            DownloadExcel()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Downloads the excel. Below code added by Ashwini for #17426
    ''' </summary>
    Protected Sub DownloadExcel()
        Try
            'Dim dtExport As DataTable = dtClasses.Copy()
            Dim dtExport As DataTable = dts
            Dim filename As String = "ClassesForStudent.xls"
            Dim tw As New System.IO.StringWriter()
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            Dim dgGrid As New DataGrid()
            dgGrid.DataSource = dtExport
            dgGrid.DataBind()
            dgGrid.RenderControl(hw)
            Response.ContentType = "application/vnd.ms-excel"
            Response.AppendHeader("Pragma", "public")
            Response.AppendHeader("Expires", "0")
            Response.AppendHeader("Content-Transfer-Encoding", "binary")
            Response.AppendHeader("Content-Type", "application/force-download")
            Response.AppendHeader("Content-Type", "application/octet-stream")
            Response.AppendHeader("Content-Type", "application/download")
            Response.AppendHeader("Cache-Control", " must-revalidate, post-check=0, pre-check=0")
            Response.AppendHeader("Content-Disposition", (Convert.ToString("attachment; filename=") & filename) + "")
            Me.EnableViewState = False
            Response.Write(AddExcelStyling())
            Response.Write(tw.ToString())
            Response.Write("</body>")
            Response.Write("</html>")
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Exports to excel. Below code added by Ashwini for #17426
    ''' </summary>
    ''' <param name="table">The table.</param>
    Private Sub ExportToExcel(table As DataTable)
        Try
            Dim filename As String = "ClassesForStudent.xls"
            Dim tw As New System.IO.StringWriter()
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            Dim dgGrid As New DataGrid()
            dgGrid.DataSource = dts
            dgGrid.DataBind()
            dgGrid.RenderControl(hw)
            Response.ContentType = "application/vnd.ms-excel"
            Response.AppendHeader("Content-Disposition", (Convert.ToString("attachment; filename=") & filename) + "")
            Me.EnableViewState = False
            Response.Write(tw.ToString())
            Response.Flush()
            Response.SuppressContent = True
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Handles the Click event of the lnkCAP2 control. Below code added by Ashwini for #17426
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Protected Sub lnkCAP2_Click(sender As Object, e As EventArgs) Handles lnkCAP2.Click
        Try
            Dim sSql As String
            sSql = Database & "..spGetExamDetailsasperCurriculumID__c"
            'dtClasses = Me.DataAction.GetDataTable(sSql, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            Dim param(1) As IDataParameter
            param(0) = DataAction.GetDataParameter("@CAP", SqlDbType.Int, 3)
            param(1) = DataAction.GetDataParameter("@ida", SqlDbType.Int, loggedInUser.CompanyID)
            dts = DataAction.GetDataTableParametrized(sSql, CommandType.StoredProcedure, param)
            DownloadExcel()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try

    End Sub
    ''' <summary>
    ''' Handles the Click event of the lnkFAE control. Below code added by Ashwini for #17426
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Protected Sub lnkFAE_Click(sender As Object, e As EventArgs) Handles lnkFAE.Click
        Try
            Dim sSql As String
            sSql = Database & "..spGetExamDetailsasperCurriculumID__c"
            'dtClasses = Me.DataAction.GetDataTable(sSql, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            Dim param(1) As IDataParameter
            param(0) = DataAction.GetDataParameter("@CAP", SqlDbType.Int, 5)
            param(1) = DataAction.GetDataParameter("@ida", SqlDbType.Int, loggedInUser.CompanyID)
            dts = DataAction.GetDataTableParametrized(sSql, CommandType.StoredProcedure, param)
            DownloadExcel()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try

    End Sub
    'End redmine log #17426
End Class
