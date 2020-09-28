'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Abhishek Tapkir            1/20/2015                           Created control for pivot
'Siddharth Kavitake         1/23/2015                           Made changes as per configuration requirements 
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Data
Imports System.Collections.Generic
Imports Telerik.Web.UI
Imports System.Linq
Imports System.Drawing
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Telerik.Web.UI.PivotGrid
Imports Telerik.Web.UI.PivotGrid.Core
Imports Aptify.Framework.BusinessLogic.ProcessPipeline

Partial Class UserControls_Aptify_Custom__c_FirmChangeSessionToAutumn__c
    Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

#Region "Properties"
    Private _dataTable As DataTable
    Private _enrollmentTable As DataTable
    Private _studentsTable As DataTable
    Private _studentsDetailsTable As DataTable
    Private _sClassRegID As String = ""

    Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "FirmChangeSessionToAutumn__c"
    Protected Const ATTRIBUTE_MANAGE_MYGROUP_URL_NAME As String = "ManageMyGroupURL"

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

#End Region

#Region "Protected Methods"
    Protected Overrides Sub SetProperties()
        Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
        MyBase.SetProperties()
        If String.IsNullOrEmpty(Me.ManageMyGroupURL) Then
            Me.ManageMyGroupURL = Me.GetLinkValueFromXML(ATTRIBUTE_MANAGE_MYGROUP_URL_NAME)
        End If
    End Sub
#End Region

#Region "Private Methods"
    Private Sub LoadAcademicYear()
        Dim sql As String
        sql = Database & "..spGetParallelAcademicYears__c"
        ddlAcademicYear.DataSource = DataAction.GetDataTable(sql)
        ddlAcademicYear.DataValueField = "Id"
        ddlAcademicYear.DataTextField = "Name"
        ddlAcademicYear.DataBind()
    End Sub

    Private Sub Getdata()
        gvStudentResult.FilterWindow.Localization.OK = "OK"
        gvStudentResult.FilterWindow.Localization.Cancel = "Cancel"
        Dim sql As String = String.Empty
        sql = Database & "..spGetStudentsForSessionToAutumn__c @AcademicCycleID=" & ddlAcademicYear.SelectedValue.ToString() & ", @CompanyID=" & loggedInUser.CompanyID
        _enrollmentTable = Me.DataAction.GetDataTable(sql)
        If _enrollmentTable.Rows.Count > 0 Then
            Dim dtDistinctStuds As DataTable = _enrollmentTable.DefaultView.ToTable(True, "StudentID")
            lblStudCount.Text = "Total student count: " & dtDistinctStuds.Rows.Count.ToString()
            btnSubmit.Visible = True
            lblMsg.Visible = True
        Else
            lblStudCount.Text = "Total student count: 0"
            btnSubmit.Visible = False
            lblMsg.Visible = False
        End If
    End Sub

    Private Sub LoadDefaultSetting()
        Try
            If _enrollmentTable.Rows.Count > 0 Then
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

#End Region

#Region "Events"
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            SetProperties()
            LoadAcademicYear()
            lblMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.WebPortalForSessionChange.UserMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.PivotGrid.NoRecords")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            lblGroupMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.WebPortalForSessionChange.GroupNotAvailableNote")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            lblAutumnMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.WebPortalForSessionChange.AutumnRegistrationNote")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
        End If
        Getdata()
    End Sub

    Protected Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        If Not IsPostBack Then
            LoadDefaultSetting()
        End If
    End Sub

    Protected Sub gvStudentResult_CellDataBound(sender As Object, e As Telerik.Web.UI.PivotGridCellDataBoundEventArgs) Handles gvStudentResult.CellDataBound        

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
                If Not e.Cell.FindControl("chkToAutumn") Is Nothing Then
                    Dim chkToAutumn As CheckBox = CType(e.Cell.FindControl("chkToAutumn"), CheckBox)
                    chkToAutumn.Visible = False
                End If
            End If
        ElseIf TypeOf e.Cell Is PivotGridDataCell Then
            Dim cell As PivotGridDataCell = TryCast(e.Cell, PivotGridDataCell)
            If cell.CellType = PivotGridDataCellType.DataCell And cell.CellType <> PivotGridDataCellType.ColumnTotalDataCell Then
                If e.Cell.DataItem = -1 Then
                    e.Cell.BackColor = Color.LightGray
                ElseIf e.Cell.DataItem = -2 Then
                    e.Cell.BackColor = Color.LightBlue
                ElseIf Not e.Cell.DataItem Is Nothing Then
                    Dim hdnValue As String = hdnCheckedBoxes.Value
                    If Not e.Cell.FindControl("chkToAutumn") Is Nothing Then
                        Dim chkToAutumn As CheckBox = CType(e.Cell.FindControl("chkToAutumn"), CheckBox)
                        chkToAutumn.ToolTip = e.Cell.DataItem.ToString()
                        If hdnValue.Contains(e.Cell.DataItem.ToString()) Then
                            chkToAutumn.Checked = True
                        Else
                            chkToAutumn.Checked = False
                        End If
                    End If
                End If
            End If
            If cell.CellType = PivotGridDataCellType.ColumnTotalDataCell Then
                If Not cell.FindControl("chkToAutumn") Is Nothing Then
                    Dim chkToAutumn As CheckBox = CType(cell.FindControl("chkToAutumn"), CheckBox)
                    chkToAutumn.Visible = False
                End If
            End If
        End If
    End Sub

    Protected Sub gvStudentResult_NeedDataSource(sender As Object, e As Telerik.Web.UI.PivotGridNeedDataSourceEventArgs) Handles gvStudentResult.NeedDataSource
        If _enrollmentTable.Rows.Count > 0 Then
            gvStudentResult.DataSource = _enrollmentTable
            gvStudentResult.Visible = True
            lblMessage.Visible = False
            Dim DistinctDt As DataTable = _enrollmentTable.DefaultView.ToTable(True, "Curriculum")
            If DistinctDt.Rows.Count = _enrollmentTable.Rows.Count Then
                gvStudentResult.ColumnGroupsDefaultExpanded = True
                'Else
                '    gvStudentResult.ColumnGroupsDefaultExpanded = False
            End If
        Else
            gvStudentResult.DataSource = Nothing
            gvStudentResult.Visible = False
            lblMessage.Visible = True
        End If
    End Sub

    Protected Sub ddlAcademicYear_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlAcademicYear.SelectedIndexChanged
        LoadDefaultSetting()
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
        Dim sClassRegID As String = ""
        Dim oStagingGE As Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase
        'For Each item As PivotGridItem In gvStudentResult.Items
        '    For Each cell In item.Cells
        '        If TypeOf cell Is PivotGridDataCell Then
        '            If cell.FindControl("chkToAutumn") IsNot Nothing Then
        '                Dim chkToAutumn As CheckBox = CType(cell.FindControl("chkToAutumn"), CheckBox)
        '                If chkToAutumn.Checked AndAlso chkToAutumn.Enabled Then
        '                    If cell.FindControl("lblClassRegID") IsNot Nothing Then
        '                        Dim lblClassRegID As Label = CType(cell.FindControl("lblClassRegID"), Label)
        '                        _sClassRegID = _sClassRegID + lblClassRegID.Text + ","
        '                        oStagingGE = AptifyApplication.GetEntityObject("FirmChangeSessionAutumnStaging__c", -1)
        '                        oStagingGE.SetValue("ClassRegistrationID", lblClassRegID.Text)
        '                        oStagingGE.Save()
        '                    End If
        '                End If
        '            End If
        '        Else
        '            Exit For
        '        End If
        '    Next
        'Next
        'Dim hdnValue As String = hdnCheckedBoxes.Value
        sClassRegID = hdnCheckedBoxes.Value

        If sClassRegID = "" Then
            lblSubmitMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.WebPortalForSessionChange.SubmitDataNotSelected")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            radWindowSubmit.VisibleOnPageLoad = True
        Else
            Dim arrClassRegID As String() = sClassRegID.Split(",")
            For iCnt As Integer = 0 To arrClassRegID.Length - 2
                oStagingGE = AptifyApplication.GetEntityObject("FirmChangeSessionAutumnStaging__c", -1)
                oStagingGE.SetValue("ClassRegistrationID", arrClassRegID(iCnt))
                oStagingGE.Save()
            Next
            lblSubmitMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.WebPortalForSessionChange.SubmitClick")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            radWindowSubmit.VisibleOnPageLoad = True
            Dim sSQL As String = Database & "..spGetProcessFlowIDByName__c @Name='Change Exam Session To Autumn Through Web Portal__c'"
            Dim iProcessFlowID As Integer = DataAction.ExecuteScalar(sSQL, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            If iProcessFlowID > 0 Then
                Dim pfResult As ProcessFlowResult
                pfResult = ProcessFlowEngine.ExecuteProcessFlow(Me.AptifyApplication, iProcessFlowID)
            End If
            Getdata()
            gvStudentResult.Rebind()
        End If
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        Response.Redirect(Me.ManageMyGroupURL, False)
    End Sub

    Protected Sub btnSubmitOk_Click(sender As Object, e As System.EventArgs) Handles btnSubmitOk.Click
        Try
            radWindowSubmit.VisibleOnPageLoad = False
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
#End Region

End Class
