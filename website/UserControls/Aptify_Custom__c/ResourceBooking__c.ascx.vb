'************************************** Class Summary ***********************************************
'Developer              Date Created/Modified               Summary
'Rajesh Kardile             04/16/2014                  Created user control to display Resource booking 
'****************************************************************************************************

Imports System.Data
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Telerik.Web.UI

Partial Class ResourceBooking__c
    Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

#Region "Property"

#End Region

#Region "Methods"
    ''' <summary>
    ''' Bind Organization in drop down
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub LoadResources()
        Try
            Dim sSQLResource As String = Database & "..spGetAllResourceForPerson__c"
            Dim params(0) As System.Data.IDataParameter
            params(0) = DataAction.GetDataParameter("@personID", SqlDbType.Int, User1.PersonID)
            Dim dtResource As DataTable = DataAction.GetDataTableParametrized(sSQLResource, CommandType.StoredProcedure, params)
            If Not dtResource Is Nothing AndAlso dtResource.Rows.Count > 0 Then
                cmbResource.DataSource = dtResource
                cmbResource.DataTextField = "Name"
                cmbResource.DataValueField = "ID"
                cmbResource.DataBind()
                cmbResource.Items.Insert(0, New ListItem("All", 0))
            Else
                cmbResource.Items.Clear()
                Response.Redirect(Me.GetSecurityErrorPageFromXML & "?Message=" & Server.UrlEncode("You are not authorize to access this page"))
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Bind Rate Cards Resource Booking
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub LoadResourcesDetails()
        Try
            Dim sSQLOrganization As String = Database & "..spGetResourceDetails__c"
            Dim params(2) As System.Data.IDataParameter
            params(0) = DataAction.GetDataParameter("@ResourceID", SqlDbType.Int, cmbResource.SelectedValue)
            params(1) = DataAction.GetDataParameter("@Filter", SqlDbType.VarChar, cmbStatus.SelectedValue)
            params(2) = DataAction.GetDataParameter("@ComapnyID", SqlDbType.Int, CInt(User1.CompanyID))
            Dim dtResourceDetails As DataTable = DataAction.GetDataTableParametrized(sSQLOrganization, CommandType.StoredProcedure, params)
            If Not dtResourceDetails Is Nothing AndAlso dtResourceDetails.Rows.Count > 0 Then
                grdResourceBooking.Visible = True
                grdResourceBooking.DataSource = dtResourceDetails
                grdResourceBooking.DataBind()
                grdResourceBooking.AllowPaging = True
                lblError.Visible = False
            Else
                Dim dtEmptyResourceDetails As New DataTable
                dtEmptyResourceDetails.Columns.Add("ID")
                grdResourceBooking.DataSource = dtEmptyResourceDetails
                grdResourceBooking.DataBind()
                grdResourceBooking.Visible = True
                lblError.Visible = False
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
#End Region

#Region "Events"

    ''' <summary>
    ''' Page load for initiation
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            Me.Page.MaintainScrollPositionOnPostBack = True
            If User1.PersonID <= 0 Then
                Session("ReturnToPage") = Request.RawUrl
                Response.Redirect("~/Login.aspx", True)
            End If
            If Not IsPostBack Then
                LoadResources()
                LoadResourcesDetails()
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    '''  Bind All status in drop down list
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub cmbStatus_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbStatus.SelectedIndexChanged
        LoadResourcesDetails()
    End Sub

    ''' <summary>
    ''' This method use is to calling Teleric filter 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub grdResourceBooking_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdResourceBooking.NeedDataSource
        Try
            LoadResourcesDetails()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub grdResourceBooking_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles grdResourceBooking.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim dataItem As GridDataItem = e.Item
            Dim resource As Label = CType(dataItem.FindControl("lblRecordStatus"), Label)
            If Not IsNothing(resource) Then
                resource.Font.Bold = True
                If resource.Text.ToLower() = "new" Then
                    resource.ForeColor = Drawing.Color.Green
                ElseIf resource.Text.ToLower() = "updated" Then
                    resource.ForeColor = Drawing.Color.Blue
                ElseIf resource.Text.ToLower() = "cancelled" Then
                    resource.ForeColor = Drawing.Color.Orange
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Bins resource in drop down list
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub cmbResource_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbResource.SelectedIndexChanged
        LoadResourcesDetails()
    End Sub

#End Region

    Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
        Try
            Dim dtResourceDetails As DataTable = New DataTable()
            If Not String.IsNullOrEmpty(txtstartdate.SelectedDate.ToString().Trim()) Then

                If Not String.IsNullOrEmpty(txtenddate.SelectedDate.ToString().Trim()) Then
                    Dim sSQLOrganization As String = Database & "..spGetResourceDetailsNew__c"
                    Dim params(4) As System.Data.IDataParameter
                    params(0) = DataAction.GetDataParameter("@ResourceID", SqlDbType.Int, cmbResource.SelectedValue)
                    params(1) = DataAction.GetDataParameter("@Filter", SqlDbType.VarChar, cmbStatus.SelectedValue)
                    params(2) = DataAction.GetDataParameter("@ComapnyID", SqlDbType.Int, CInt(User1.CompanyID))
                    params(3) = DataAction.GetDataParameter("@StartDate", SqlDbType.Date, CDate(txtstartdate.SelectedDate.ToString().Trim()))
                    params(4) = DataAction.GetDataParameter("@EndDate", SqlDbType.Date, CDate(txtenddate.SelectedDate.ToString().Trim()))
                    dtResourceDetails = DataAction.GetDataTableParametrized(sSQLOrganization, CommandType.StoredProcedure, params)
                Else
                    Dim sSQLOrganization As String = Database & "..spGetResourceDetails__c"
                    Dim params(2) As System.Data.IDataParameter
                    params(0) = DataAction.GetDataParameter("@ResourceID", SqlDbType.Int, cmbResource.SelectedValue)
                    params(1) = DataAction.GetDataParameter("@Filter", SqlDbType.VarChar, cmbStatus.SelectedValue)
                    params(2) = DataAction.GetDataParameter("@ComapnyID", SqlDbType.Int, CInt(User1.CompanyID))
                    dtResourceDetails = DataAction.GetDataTableParametrized(sSQLOrganization, CommandType.StoredProcedure, params)

                End If
            Else
                Dim sSQLOrganization As String = Database & "..spGetResourceDetails__c"
                Dim params(2) As System.Data.IDataParameter
                params(0) = DataAction.GetDataParameter("@ResourceID", SqlDbType.Int, cmbResource.SelectedValue)
                params(1) = DataAction.GetDataParameter("@Filter", SqlDbType.VarChar, cmbStatus.SelectedValue)
                params(2) = DataAction.GetDataParameter("@ComapnyID", SqlDbType.Int, CInt(User1.CompanyID))
                dtResourceDetails = DataAction.GetDataTableParametrized(sSQLOrganization, CommandType.StoredProcedure, params)
            End If


           
            If Not dtResourceDetails Is Nothing AndAlso dtResourceDetails.Rows.Count > 0 Then
                Dim filename As String = "ResourceBooking.xls"
                Dim tw As New System.IO.StringWriter()
                Dim hw As New System.Web.UI.HtmlTextWriter(tw)
                Dim dgGrid As New DataGrid()
                dgGrid.DataSource = dtResourceDetails
                dgGrid.DataBind()
                dgGrid.RenderControl(hw)
                Response.ContentType = "application/vnd.ms-excel"
                Response.AppendHeader("Content-Disposition", (Convert.ToString("attachment; filename=") & filename) + "")
                Me.EnableViewState = False
                Response.Write(tw.ToString())
                Response.Flush()
                Response.[End]()
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub btnGo_Click(sender As Object, e As System.EventArgs) Handles btnGo.Click
        Try
            If Not String.IsNullOrEmpty(txtstartdate.SelectedDate.ToString().Trim()) Then

                If Not String.IsNullOrEmpty(txtenddate.SelectedDate.ToString().Trim()) Then
                    Dim sSQLOrganization As String = Database & "..spGetResourceDetailsNew__c"
                    Dim params(4) As System.Data.IDataParameter
                    params(0) = DataAction.GetDataParameter("@ResourceID", SqlDbType.Int, cmbResource.SelectedValue)
                    params(1) = DataAction.GetDataParameter("@Filter", SqlDbType.VarChar, cmbStatus.SelectedValue)
                    params(2) = DataAction.GetDataParameter("@ComapnyID", SqlDbType.Int, CInt(User1.CompanyID))
                    params(3) = DataAction.GetDataParameter("@StartDate", SqlDbType.Date, CDate(txtstartdate.SelectedDate.ToString().Trim()))
                    params(4) = DataAction.GetDataParameter("@EndDate", SqlDbType.Date, CDate(txtenddate.SelectedDate.ToString().Trim()))
                    Dim dtResourceDetails As DataTable = DataAction.GetDataTableParametrized(sSQLOrganization, CommandType.StoredProcedure, params)
                    If Not dtResourceDetails Is Nothing AndAlso dtResourceDetails.Rows.Count > 0 Then
                        grdResourceBooking.Visible = True
                        grdResourceBooking.DataSource = dtResourceDetails
                        grdResourceBooking.DataBind()
                        grdResourceBooking.AllowPaging = True
                        lblError.Visible = False
                    Else
                        Dim dtEmptyResourceDetails As New DataTable
                        dtEmptyResourceDetails.Columns.Add("ID")
                        grdResourceBooking.DataSource = dtEmptyResourceDetails
                        grdResourceBooking.DataBind()
                        grdResourceBooking.Visible = True
                        lblError.Visible = False
                    End If
                Else
                    LoadResourcesDetails()
                End If
            Else
                LoadResourcesDetails()
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
End Class