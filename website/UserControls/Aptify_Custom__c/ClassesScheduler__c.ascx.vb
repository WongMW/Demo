Option Explicit On
Option Strict On
Imports System.Data
Imports Telerik.Web.UI
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Applications.ProductSetup
Imports Aptify.Framework.DataServices
Namespace Aptify.Framework.Web.eBusiness.Education
    Partial Class ClassesScheduler__c
        Inherits eBusiness.BaseUserControlAdvanced

        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "ClassesScheduler__c"

        Protected Const ATTRIBUTE_EDUCATIONDETAILS_URL As String = "EducationDetails"
        Dim sAllTimeTable As String = String.Empty
        Dim IsCalenderDate As Boolean = False
        Dim lCurriculumID As Long
        Dim lTimeTableID As Long
        Dim dtClasses As DataTable

#Region "Student Classes Specific Properties"
        Dim GridDataItem As GridItem

        Public Overloads Property EducationDetailURL() As String
            Get
                If Not ViewState(ATTRIBUTE_EDUCATIONDETAILS_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_EDUCATIONDETAILS_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_EDUCATIONDETAILS_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            If String.IsNullOrEmpty(EducationDetailURL) Then
                Me.EducationDetailURL = Me.GetLinkValueFromXML(ATTRIBUTE_EDUCATIONDETAILS_URL)
            End If
            'call base method to set parent properties
            MyBase.SetProperties()
        End Sub
#End Region

#Region "Events"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            'set control properties from XML file if needed
            SetProperties()
            If Not IsPostBack Then
                ''Sachin Sathe Added code
                LoadCurrentAcademicCycle()
                LoadNextAcademicCycle()
                ''End

                GetClassDetails()
                grdClasses.Rebind()
                pnlList.Visible = True
                lnkCalendar.Visible = True
                lnkList.Visible = False
                pnlCalendar.Visible = False
            End If
        End Sub

        Protected Sub lnkCalendar_Click(sender As Object, e As System.EventArgs) Handles lnkCalendar.Click
            pnlCalendar.Visible = True
            pnlList.Visible = False
            lnkCalendar.Visible = False
            lnkList.Visible = True
        End Sub

        Protected Sub lnkList_Click(sender As Object, e As System.EventArgs) Handles lnkList.Click
            pnlCalendar.Visible = False
            pnlList.Visible = True
            lnkCalendar.Visible = True
            lnkList.Visible = False
        End Sub

        Protected Sub grdClasses_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdClasses.NeedDataSource
            GetClassDetails()
            If dtClasses IsNot Nothing Then
                grdClasses.DataSource = dtClasses
            End If
        End Sub

        Protected Sub grdClasses_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles grdClasses.ItemCommand
            Try
                
                If e.CommandName = "Download" Then
                    radDownloadDocuments.VisibleOnPageLoad = True
                    Dim data As String() = e.CommandArgument.ToString().Split(CChar(";"))
                    Dim recordID As Integer = CInt(data(0))
                    Dim entityID As Integer = CInt(Me.AptifyApplication.GetEntityID("Course Parts"))
                    RecordAttachments__c.AttachmentCategory = Me.AptifyApplication.GetEntityRecordIDFromRecordName("Attachment Categories", "Course Material")
                    RecordAttachments__c.LoadAttachments(entityID, recordID, True)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

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

            'sb.Append("<x:Name>Projects 3 </x:Name>")'Commented by GM for Redmine #17192
            sb.Append("<x:Name>Timetable </x:Name>") 'Added by GM for Redmine #17192
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


     Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
            Try
               
               GetClassesDetails()
                Dim dtExport As DataTable = dtClasses.Copy()
                dtExport.Columns.Remove("EndDate")
                dtExport.Columns.Remove("ClassID")
                dtExport.Columns.Remove("CoursePartID")
                dtExport.Columns.Remove("EndDateTime")
                dtExport.Columns.Remove("TYPE")
                dtExport.Columns.Remove("ClassNPartName")
                dtExport.Columns.Remove("InstructorID__c")
                dtExport.Columns.Remove("StudentID")
                dtExport.Columns.Remove("IsAssignment")
		dtExport.Columns.Remove("ClassName")
                dtExport.Columns.Remove("Subject")

                Dim filename As String = "ClassesForStudent.xls"
                Dim tw As New System.IO.StringWriter()
                Dim hw As New System.Web.UI.HtmlTextWriter(tw)
                Dim dgGrid As New DataGrid()
                dgGrid.DataSource = dtExport
                dgGrid.DataBind()
                dgGrid.RenderControl(hw)
                Response.ContentType = "application/vnd.ms-excel"
                ' Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"

                Response.AppendHeader("Pragma", "public")
                Response.AppendHeader("Expires", "0")
                Response.AppendHeader("Content-Transfer-Encoding", "binary")
                Response.AppendHeader("Content-Type", "application/force-download")
                Response.AppendHeader("Content-Type", "application/octet-stream")
                Response.AppendHeader("Content-Type", "application/download")
                Response.AppendHeader("Cache-Control", " must-revalidate, post-check=0, pre-check=0")
                Response.AppendHeader("Content-Disposition", (Convert.ToString("attachment; filename=") & filename) + "")
                Me.EnableViewState = False
                'Sachin added below line
                Response.Write(AddExcelStyling())
                Response.Write(tw.ToString())
                'End
                'Sachin added below line
                Response.Write("</body>")
                Response.Write("</html>")
                'End
		Response.Flush()
                Response.[End]()



                'grdClasses.ExportSettings.Excel.Format = DirectCast([Enum].Parse(GetType(GridExcelExportFormat), "xlsx"), GridExcelExportFormat)
                'grdClasses.ExportSettings.IgnorePaging = True
                'grdClasses.ExportSettings.ExportOnlyData = True
                'grdClasses.ExportSettings.OpenInNewWindow = True
                'grdClasses.MasterTableView.ExportToExcel()


            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
 Private Sub ExportToExcel(table As DataTable)
	
 'Dim filename As String = "ClassesForStudent.xls"
              Dim filename As String = "ClassesForStudent.xls"
            Dim tw As New System.IO.StringWriter()
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            Dim dgGrid As New DataGrid()
            dgGrid.DataSource = dtClasses
            dgGrid.DataBind()
            dgGrid.RenderControl(hw)
            Response.ContentType = "application/vnd.ms-excel"
            Response.AppendHeader("Content-Disposition", (Convert.ToString("attachment; filename=") & filename) + "")
            Me.EnableViewState = False
            Response.Write(tw.ToString())
Response.Flush()
Response.SuppressContent = True

 End Sub
        ''' <summary>
        ''' Handles the cancel button event - download couse materials popup
        ''' </summary>
        Protected Sub btnClose_Click(sender As Object, e As System.EventArgs) Handles btnClose.Click
            radDownloadDocuments.VisibleOnPageLoad = False
        End Sub
#End Region

#Region "Functions"

        Private Sub GetClassDetails()
            Dim sSql As String
            sSql = Database & "..spGetClassesForStudent__c"
            Dim AcademicCycleID As String = "-1"
            If Request.QueryString("ACycle") IsNot Nothing Then
                AcademicCycleID = Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("ACycle"))
            Else
                If rdoCurrentAcademicCycle.Checked Then
                    AcademicCycleID = CStr(ViewState("CurrentAcademicCycleID"))
                Else
                    AcademicCycleID = CStr(ViewState("NextAcademicCycleID"))
                End If

            End If
            Dim param(1) As IDataParameter
            param(0) = DataAction.GetDataParameter("@StudentID", SqlDbType.Int, User1.PersonID)
            param(1) = DataAction.GetDataParameter("@AcademicCyleID", SqlDbType.Int, Convert.ToInt32(AcademicCycleID))
            dtClasses = Me.DataAction.GetDataTableParametrized(sSql, CommandType.StoredProcedure, param)
            If Not dtClasses Is Nothing AndAlso dtClasses.Rows.Count > 0 Then
                ViewState("oDT") = dtClasses
                radSchedulerLecturer.DataSource = dtClasses
                radSchedulerLecturer.DataBind()
                lblGridMsg.Visible = False
                pnlCalendar.Style.Item("Display") = "block"
                pnlList.Style.Item("Display") = "block"
            Else
                lblGridMsg.Visible = True
                pnlList.Style.Item("Display") = "none"
                pnlCalendar.Style.Item("Display") = "none"
            End If
        End Sub
 Private Sub GetClassesDetails()
            Dim sSql As String
            sSql = Database & "..spGetClassesForStudent__c"
            Dim AcademicCycleID As String = "-1"
            If Request.QueryString("ACycle") IsNot Nothing Then
                AcademicCycleID = Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("ACycle"))
            Else
                If rdoCurrentAcademicCycle.Checked Then
                    AcademicCycleID = CStr(ViewState("CurrentAcademicCycleID"))
                Else
                    AcademicCycleID = CStr(ViewState("NextAcademicCycleID"))
                End If
            End If
            Dim param(1) As IDataParameter
            param(0) = DataAction.GetDataParameter("@StudentID", SqlDbType.Int, User1.PersonID)
            param(1) = DataAction.GetDataParameter("@AcademicCyleID", SqlDbType.Int, Convert.ToInt32(AcademicCycleID))
            dtClasses = Me.DataAction.GetDataTableParametrized(sSql, CommandType.StoredProcedure, param)
            
        End Sub

        ''Sachin Sathe code
        Private Sub LoadCurrentAcademicCycle()
            Try
                Dim sSql As String = "..spGetCurrentAcademicYear__c"
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    rdoCurrentAcademicCycle.Text = "Academic Cycle: " + Convert.ToString(dt.Rows(0)("Name"))
                    ViewState("CurrentAcademicCycleID") = Convert.ToString(dt.Rows(0)("ID"))
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub


        Private Sub LoadNextAcademicCycle()
            Try
                Dim sSql As String = "..spGetNextAcademicCycleDetails__c"
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    rdoNextAcadmicCycle.Text = "Next Academic Cycle: " + Convert.ToString(dt.Rows(0)("Name"))
                    ViewState("NextAcademicCycleID") = Convert.ToString(dt.Rows(0)("ID"))
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub rdoNextAcadmicCycle_CheckedChanged(sender As Object, e As System.EventArgs) Handles rdoNextAcadmicCycle.CheckedChanged
            Try
                LoadNextAcademicCycle()
                GetClassDetailsAsPerCycleID(CStr(ViewState("NextAcademicCycleID")))
                grdClasses.Rebind()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub rdoCurrentAcademicCycle_CheckedChanged(sender As Object, e As System.EventArgs) Handles rdoCurrentAcademicCycle.CheckedChanged
            Try
                LoadCurrentAcademicCycle()
                GetClassDetailsAsPerCycleID(CStr(ViewState("CurrentAcademicCycleID")))
                grdClasses.Rebind()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub GetClassDetailsAsPerCycleID(ByVal iAcademicCycleID As String)
            Dim sSql As String
            sSql = Database & "..spGetClassesForStudent__c"
            Dim AcademicCycleID As String = "-1"
            If iAcademicCycleID IsNot Nothing Then
                AcademicCycleID = iAcademicCycleID
           
            End If
            Dim param(1) As IDataParameter
            param(0) = DataAction.GetDataParameter("@StudentID", SqlDbType.Int, User1.PersonID)
            param(1) = DataAction.GetDataParameter("@AcademicCyleID", SqlDbType.Int, Convert.ToInt32(AcademicCycleID))
            dtClasses = Me.DataAction.GetDataTableParametrized(sSql, CommandType.StoredProcedure, param)
            If Not dtClasses Is Nothing AndAlso dtClasses.Rows.Count > 0 Then
                ViewState("oDT") = dtClasses
                radSchedulerLecturer.DataSource = dtClasses
                radSchedulerLecturer.DataBind()
                lblGridMsg.Visible = False
                pnlCalendar.Style.Item("Display") = "block"
                pnlList.Style.Item("Display") = "block"
            Else
                lblGridMsg.Visible = True
                pnlList.Style.Item("Display") = "none"
                pnlCalendar.Style.Item("Display") = "none"
            End If
        End Sub
        ''END

#End Region
        Protected Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
            Response.Redirect(EducationDetailURL, False)
        End Sub
    End Class
End Namespace
