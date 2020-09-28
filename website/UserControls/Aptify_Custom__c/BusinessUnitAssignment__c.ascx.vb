'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Pradip Chavhan              10/05/2015                        Business Unit Assignment Page
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Imports Aptify.Framework.Web
Imports Telerik.Web.UI
Imports System.Data
Imports System.IO
Imports Aptify.Framework.Application
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.DataServices
'Imports Aptify.Framework.Web.Common

Partial Class BusinessUnitAssignment__c
    Inherits eBusiness.BaseUserControlAdvanced
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_FirmAdminDashboardPage As String = "FirmAdminDashboardPage"
    Dim sErrorMessage As String = String.Empty
    Dim sSQL As String = String.Empty
#Region "BU Assignment Specific Properties"
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
        radStudent.Columns(1).CurrentFilterFunction = Telerik.Web.UI.GridKnownFunction.EqualTo
    End Sub
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        SetProperties()
        lblError.Text = ""
        If User1.PersonID < 0 Then
            Session("ReturnToPage") = Request.RawUrl
            Response.Redirect(LoginPage)
        End If
        If Not IsPostBack Then
            ViewState("Flag") = "All"
            LoadBusinessUnitsList()
            'LoadAllBusinessUnits()
            LoadStudentGrid()
        End If
    End Sub
#End Region

#Region "Popup Button Events"
    Protected Sub btnValidationOK_Click(sender As Object, e As System.EventArgs) Handles btnValidationOK.Click
        radWindowValidation.VisibleOnPageLoad = False
    End Sub
#End Region

#Region "LoadData"
    ''' <summary>
    ''' Load Active Business Unit With Parent and Child Company
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadBusinessUnitsList()
        Try
            Dim sSql As String = Database & "..spGetAllBusinessUnitsByFirm__c @CompanyID=" & User1.CompanyID
            Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dt Is Nothing Then
                radBusinessUnit.DataSource = dt
                radBusinessUnit.DataTextField = "Name"
                radBusinessUnit.DataValueField = "ID"
                radBusinessUnit.DataBind()
                radBusinessUnit.SelectedIndex = 0
                If dt.Rows.Count > 0 Then
                    ViewState("BusinessUnit") = dt
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
            sSQL = Database & "..spGetAllStudentForBUAssign__c @CompanyID=" & User1.CompanyID
            Dim dtStudent As DataTable = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dtStudent Is Nothing Then
                radStudent.DataSource = dtStudent
                'radStudent.Rebind()
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Function To Search Business Unit On Text Changes of Search Textbox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SearchBusinessUnits()
        Try
            Dim sSql As String = Database & "..spSearchBusinessUnits__c @CompanyID=" & User1.CompanyID & ",@Name= '" & txtSearch.Text.Trim() & "'"
            ' & txtSearch.Text.Trim()
            Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dt Is Nothing Then
                radBusinessUnit.DataSource = dt
                radBusinessUnit.DataTextField = "Name"
                radBusinessUnit.DataValueField = "ID"
                radBusinessUnit.DataBind()
                radBusinessUnit.SelectedIndex = 0
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Function To Bind Student Grid With Selected Business Unit From Listbox
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Sub LoadStudentGridByBU()
        Try
            sSQL = String.Empty
            sSQL = Database & "..spGetAllStudentByBUAssign__c @CompanyID=" & User1.CompanyID & ",@BusinessID=" & radBusinessUnit.SelectedValue
            Dim dtStudent As DataTable = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dtStudent Is Nothing Then
                radStudent.DataSource = dtStudent
                ' radStudent.DataBind()
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

#End Region

#Region "Private Function"
    Function validateBeforeSave() As Boolean
        Dim CheckCount As Integer = 0
        lblError.Text = ""
        lblError.Visible = False
        Try
            For i As Integer = 0 To radStudent.Items.Count - 1
                If CType(radStudent.Items(i).FindControl("chkStudent"), CheckBox).Checked = True Then
                    Dim cmbBusinessUnitgv As DropDownList = DirectCast(radStudent.Items(i).FindControl("cmbBusinessUnit"), DropDownList)
                    If cmbBusinessUnitgv.SelectedIndex = 0 Then
                        lblError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.BusinessUnit.CheckGridRecordValidationMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        lblError.Visible = True
                        Return False
                    End If
                    CheckCount = CheckCount + 1
                    ' Return True
                End If
            Next
            If CheckCount = 0 Then
                lblError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.BusinessUnit.CheckGridRecordValidationMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblError.Visible = True
                Return False
            End If
            Return True
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
        Return True
    End Function
    ''' <summary>
    ''' Event For Grid Header Checkbox Check/Uncheck Event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ToggleSelectedState(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim headerCheckBox As CheckBox = CType(sender, CheckBox)
            For Each dataItem As GridDataItem In radStudent.MasterTableView.Items
                If CType(dataItem.FindControl("chkStudent"), CheckBox).Enabled = True Then
                    CType(dataItem.FindControl("chkStudent"), CheckBox).Checked = headerCheckBox.Checked
                    dataItem.Selected = headerCheckBox.Checked
                End If
            Next
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try

    End Sub
    ''' <summary>
    ''' Event For Grid Row Checkbox Check/Uncheck
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ToggleRowSelection(ByVal sender As Object, ByVal e As EventArgs)
        TryCast(TryCast(sender, CheckBox).NamingContainer, GridItem).Selected = TryCast(sender, CheckBox).Checked
        Dim checkHeader As Boolean = True
        For Each dataItem As GridDataItem In radStudent.MasterTableView.Items
            If Not TryCast(dataItem.FindControl("chkStudent"), CheckBox).Checked Then
                checkHeader = False
                Exit For
            End If
        Next
        Dim headerItem As GridHeaderItem = TryCast(radStudent.MasterTableView.GetItems(GridItemType.Header)(0), GridHeaderItem)
        TryCast(headerItem.FindControl("chkAllStudent"), CheckBox).Checked = checkHeader
    End Sub
#End Region


#Region "Grid Events"

    Protected Sub radStudent_DataBound(sender As Object, e As System.EventArgs) Handles radStudent.DataBound
        Dim checkHeader As Boolean = True
        For Each item As Telerik.Web.UI.GridDataItem In radStudent.MasterTableView.Items
            Dim cmbBusinessUnitGV As DropDownList = DirectCast(item.FindControl("cmbBusinessUnit"), DropDownList)
            Dim hidBusinessUnitGV As HiddenField = DirectCast(item.FindControl("hidBusinessUnit"), HiddenField)
            Dim chkRow As CheckBox = DirectCast(item.FindControl("chkStudent"), CheckBox)
            If Not ViewState("BusinessUnit") Is Nothing Then
                cmbBusinessUnitGV.ClearSelection()
                cmbBusinessUnitGV.DataSource = CType(ViewState("BusinessUnit"), DataTable)
                cmbBusinessUnitGV.DataTextField = "Name"
                cmbBusinessUnitGV.DataValueField = "ID"
                cmbBusinessUnitGV.DataBind()
                cmbBusinessUnitGV.Items.Insert(0, "Select")
                cmbBusinessUnitGV.SelectedValue = hidBusinessUnitGV.Value
            Else
                cmbBusinessUnitGV.Items.Insert(0, "Select")
            End If
            If chkRow.Checked = False Then
                checkHeader = False
            End If
        Next
        Dim headerItem As GridHeaderItem = TryCast(radStudent.MasterTableView.GetItems(GridItemType.Header)(0), GridHeaderItem)
        TryCast(headerItem.FindControl("chkAllStudent"), CheckBox).Checked = checkHeader
    End Sub
    
    Protected Sub radStudent_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles radStudent.NeedDataSource
        Try
            If Convert.ToString(ViewState("Flag")) = "All" Then
                LoadStudentGrid()
            ElseIf Convert.ToString(ViewState("Flag")) = "ByBU" Then
                LoadStudentGridByBU()
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
#End Region
#Region "Button Events"
    Protected Sub btnThisBU_Click(sender As Object, e As System.EventArgs) Handles btnThisBU.Click
        Try
            ViewState("Flag") = "ByBU"
            radStudent.MasterTableView.FilterExpression = String.Empty
            For Each column As GridColumn In radStudent.MasterTableView.OwnerGrid.Columns
                column.CurrentFilterFunction = GridKnownFunction.NoFilter
                column.CurrentFilterValue = String.Empty
            Next
            radStudent.Rebind()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try

    End Sub

    Protected Sub lnkShowAll_Click(sender As Object, e As System.EventArgs) Handles lnkShowAll.Click
        Try
            txtSearch.Text = "Search..."
            LoadBusinessUnitsList()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub btnShowAll_Click(sender As Object, e As System.EventArgs) Handles btnShowAll.Click
        Try
            ViewState("Flag") = "All"
            radStudent.MasterTableView.FilterExpression = String.Empty
            For Each column As GridColumn In radStudent.MasterTableView.OwnerGrid.Columns
                column.CurrentFilterFunction = GridKnownFunction.NoFilter
                column.CurrentFilterValue = String.Empty
            Next
            radStudent.Rebind()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub txtSearch_TextChanged(sender As Object, e As System.EventArgs) Handles txtSearch.TextChanged
        Try
            If txtSearch.Text.Trim <> "" And txtSearch.Text.Trim <> "Search..." Then
                SearchBusinessUnits()
            Else
                LoadBusinessUnitsList()
                txtSearch.Text = "Search..."
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
        Try
            If validateBeforeSave() Then
                For i As Integer = 0 To radStudent.Items.Count - 1
                    If DirectCast(radStudent.Items(i).FindControl("chkStudent"), CheckBox).Checked = True Then
                        Dim sTransID As String = DataAction.BeginTransaction(IsolationLevel.Serializable, True)
                        Dim hdBLIDgv As HiddenField = DirectCast(radStudent.Items(i).FindControl("hdBLID"), HiddenField)
                        Dim hidCompanyIDgv As HiddenField = DirectCast(radStudent.Items(i).FindControl("hidCompanyID"), HiddenField)
                        Dim hidStudentIDgv As HiddenField = DirectCast(radStudent.Items(i).FindControl("hidStudentID"), HiddenField)
                        Dim cmbBusinessUnitgv As DropDownList = DirectCast(radStudent.Items(i).FindControl("cmbBusinessUnit"), DropDownList)
                        Dim idBL As Long = CLng(hdBLIDgv.Value)
                        Dim oBLGE As AptifyGenericEntityBase
                        oBLGE = Me.AptifyApplication.GetEntityObject("BusinessUnitLinks__c", idBL)
                        oBLGE.SetValue("PersonID", Convert.ToInt32(hidStudentIDgv.Value))
                        oBLGE.SetValue("BusinessUnitID", Convert.ToInt32(cmbBusinessUnitgv.SelectedValue))
                        oBLGE.SetValue("CompanyID", Convert.ToInt32(hidCompanyIDgv.Value))
                        oBLGE.SetValue("IsActive", True)
                        Dim _error As String = ""
                        Try
                            If oBLGE.Save(False, _error) Then
                                DataAction.CommitTransaction(sTransID)
                            Else
                                DataAction.RollbackTransaction(sTransID)
                            End If
                        Catch ex As Exception
                            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                            Continue For
                        End Try
                    End If
                Next
                radStudent.Rebind()

                lblValidationMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.BusinessUnit.SaveMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                radWindowValidation.VisibleOnPageLoad = True
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub btnMainBack_Click(sender As Object, e As System.EventArgs) Handles btnMainBack.Click
        If Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("Page")).ToLower = "fd" Then
            Response.Redirect(FirmAdminDashboardPage, False)
        End If
    End Sub


    Protected Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            If txtSearch.Text.Trim <> "" And txtSearch.Text.Trim <> "Search..." Then
                SearchBusinessUnits()
            Else
                LoadBusinessUnitsList()
                txtSearch.Text = "Search..."
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

#End Region

    
End Class
