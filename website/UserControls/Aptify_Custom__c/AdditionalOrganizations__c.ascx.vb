'************************************** Class Summary ***********************************************
'Developer              Date Created/Modified               Summary
'Rajesh Kardile             03/14/2014                  Created user control for display the additional organization
'****************************************************************************************************

Imports System.Data
Imports Aptify.Framework.BusinessLogic.GenericEntity

Partial Class AdditionalOrganizations__c
    Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

#Region "Property"
    ''' <summary>
    ''' Create Organization table 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OrganizationTable() As DataTable
        Get
            If Not ViewState("OrganizationTable") Is Nothing Then
                Return CType(ViewState("OrganizationTable"), DataTable)
            Else
                Dim dtOrganizationTable As New DataTable
                dtOrganizationTable.Columns.Add("ID")
                dtOrganizationTable.Columns.Add("Name")
                dtOrganizationTable.PrimaryKey = New DataColumn() {dtOrganizationTable.Columns("ID")}
                Return dtOrganizationTable
            End If
        End Get
        Set(ByVal value As DataTable)
            ViewState("OrganizationTable") = value
            Dim dtOrganizationTable As DataTable = CType(ViewState("OrganizationTable"), DataTable)
            If dtOrganizationTable IsNot Nothing AndAlso dtOrganizationTable.Rows.Count > 0 Then
                dtOrganizationTable.PrimaryKey = New DataColumn() {dtOrganizationTable.Columns("ID")}
            End If
        End Set
    End Property

    ''' <summary>
    ''' Set Save button visibility
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property SaveButton() As Boolean
        Set(ByVal Value As Boolean)
            btnSave.Visible = Value
        End Set
    End Property

#End Region

#Region "Methods"
    ''' <summary>
    ''' Bind Organization in drop down
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindOrganizationDropDown()
        Try
            ' Get All Organization
            Dim sSQLOrganization As String = Database & "..spGetAllOrganizations__c"
            Dim params(0) As System.Data.IDataParameter
            params(0) = DataAction.GetDataParameter("@PersonID", SqlDbType.VarChar, Convert.ToString(User1.PersonID))
            Dim dsOrganization As DataSet = DataAction.GetDataSetParametrized(sSQLOrganization, CommandType.StoredProcedure, params)
            If Not dsOrganization Is Nothing AndAlso dsOrganization.Tables(0).Rows.Count > 0 AndAlso dsOrganization.Tables(1).Rows.Count > 0 Then
                lblPrimaryDistrictSociety.Text = dsOrganization.Tables(1).Rows(0)(0).ToString()
                cmbOrganization.DataSource = dsOrganization
                cmbOrganization.DataTextField = "Name"
                cmbOrganization.DataValueField = "ID"
                cmbOrganization.DataBind()
                cmbOrganization.Items.Insert(0, Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectOrganization")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
            Else
                cmbOrganization.Items.Clear()
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Bind Organization in drop down
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindOrganizationGrid()
        Try
            ' Get Additional Organization
            Dim sSQLOrganization As String = Database & "..spGetAdditionalOrganizationsForPerson__c"
            Dim params(0) As System.Data.IDataParameter
            params(0) = DataAction.GetDataParameter("@PersonID", SqlDbType.VarChar, Convert.ToString(User1.PersonID))
            Dim dtOrganization As DataTable = DataAction.GetDataTableParametrized(sSQLOrganization, CommandType.StoredProcedure, params)
            If Not dtOrganization Is Nothing AndAlso dtOrganization.Rows.Count > 0 Then
                OrganizationTable = dtOrganization
                grdOrganization.DataSource = OrganizationTable
                grdOrganization.DataBind()
                grdOrganization.AllowPaging = True
                grdOrganization.PageSize = 5
            Else
                grdOrganization.DataSource = OrganizationTable
                grdOrganization.DataBind()
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Save data in database
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SaveData()
        Dim oPersonGE As AptifyGenericEntityBase
        Dim iSequence As Integer
        oPersonGE = AptifyApplication.GetEntityObject("Persons", User1.PersonID)
        If oPersonGE IsNot Nothing Then
            With oPersonGE
                .SubTypes("PersonAdditionalOrgs__c").Clear()
                iSequence = 0
                For Each dr As DataRow In OrganizationTable.Rows
                    With .SubTypes("PersonAdditionalOrgs__c").Add()
                        .SetValue("PersonID", User1.PersonID)
                        .SetValue("Sequence", ++iSequence)
                        .SetValue("OrganizationID", dr("ID"))
                    End With
                Next
                .Save(False)
            End With
        End If
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
            If Not IsPostBack Then
                BindOrganizationDropDown()
                BindOrganizationGrid()
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' add button to add data in grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAdd_Click(sender As Object, e As System.EventArgs) Handles btnAdd.Click
        Try
            If Convert.ToString(cmbOrganization.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectOrganization")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
                lblError.Visible = False
                Dim dtOrganizationTable As DataTable = OrganizationTable
                Dim drCurrentCompanyPayTable As DataRow = dtOrganizationTable.NewRow()
                drCurrentCompanyPayTable("ID") = cmbOrganization.SelectedValue
                drCurrentCompanyPayTable("Name") = cmbOrganization.SelectedItem.Text
                If OrganizationTable.Rows.Find(cmbOrganization.SelectedValue) Is Nothing Then
                    dtOrganizationTable.Rows.Add(drCurrentCompanyPayTable)
                    OrganizationTable = dtOrganizationTable
                    grdOrganization.DataSource = OrganizationTable
                    grdOrganization.DataBind()
                    grdOrganization.AllowPaging = True
                    grdOrganization.PageSize = 5
                Else
                    lblError.Visible = True
                    lblError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectOrganizationExistMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                End If
            Else
                lblError.Visible = True
                lblError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectOrganizationMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' grid item command event to check delete.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub grdOrganization_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles grdOrganization.ItemCommand
        Try
            If e.CommandName.ToLower = "Delete".ToLower Then
                Dim sID As String = Convert.ToString(e.CommandArgument)
                If Not OrganizationTable.Rows.Find(sID) Is Nothing Then
                    OrganizationTable.Rows.Remove(OrganizationTable.Rows.Find(sID))
                End If
                grdOrganization.DataSource = OrganizationTable
                grdOrganization.DataBind()
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Save button click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
        Try
            SaveData()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

#End Region

End Class

