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

Partial Class BusinessUnits__c
    Inherits eBusiness.BaseUserControlAdvanced
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"
    Protected Const ATTRIBUTE_SECURITYERROR_PAGE As String = "securityErrorPage"
    Dim sErrorMessage As String = String.Empty
    Dim sSQL As String = String.Empty
#Region "BU Specific Properties"
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

    Public Overridable ReadOnly Property SecurityErrorPage() As String
        Get
            If Not ViewState(ATTRIBUTE_SECURITYERROR_PAGE) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_SECURITYERROR_PAGE))
            Else
                Dim value As String = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings("SecurityErrorPageURL"))
                If Not String.IsNullOrEmpty(value) Then
                    ViewState(ATTRIBUTE_SECURITYERROR_PAGE) = value
                    Return value
                Else
                    Return String.Empty
                End If
            End If
        End Get
    End Property

    Protected Overrides Sub SetProperties()
        If String.IsNullOrEmpty(LoginPage) Then
            LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage)
        End If

    End Sub
#End Region
#Region "Page Event"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        SetProperties()
        divText.InnerHtml = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.BusinessUnit.Text__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
        divRemoveText.InnerHtml = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.BusinessUnit.RemoveText__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
        radBusinessUnit.AllowSorting = False
        radBusinessUnit.MasterTableView.AllowSorting = False

        If User1.PersonID < 0 Then
            Session("ReturnToPage") = Request.RawUrl
            Response.Redirect(LoginPage)
        Else
            If CheckBUPageAccess() = False Then
                Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("virtualdir") & SecurityErrorPage & "?Message=Access to this Person is unauthorized.")
            End If
        End If
        If Not IsPostBack Then
            LoadAllBusinessUnits()
        End If
    End Sub
#End Region

#Region "Popup Button Events"
    Protected Sub btnValidationOK_Click(sender As Object, e As System.EventArgs) Handles btnValidationOK.Click
        radWindowValidation.VisibleOnPageLoad = False
        Try
            DeleteBusinessUnit(Convert.ToInt32(hidBU.Value))
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As System.EventArgs) Handles btnCancel.Click
        radWindowValidation.VisibleOnPageLoad = False
    End Sub
#End Region

#Region "LoadData"
    ''' <summary>
    ''' Function To Load All Active Business Units
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadAllBusinessUnits()
        Try
            Dim sSql As String = Database & "..spGetAllBusinessUnitsForCreate__c @CompanyID=" & User1.CompanyID
            Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dt Is Nothing Then
                radBusinessUnit.DataSource = dt
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
#End Region

#Region "Private Function"
    ''' <summary>
    ''' Handles Button click event from grid
    ''' </summary>
    Protected Sub btnAddRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim btnRemove As Button = CType(sender, Button)
            Dim rowGridItem As GridDataItem = CType(btnRemove.NamingContainer, GridDataItem)
            Dim iIndex As Integer = rowGridItem.DataSetIndex
            Dim txtBU As TextBox = DirectCast(rowGridItem.FindControl("txtBU"), TextBox)
            Dim hidBusinessUnitgv As HiddenField = DirectCast(rowGridItem.FindControl("hidBusinessUnit"), HiddenField)
            If btnRemove.CommandArgument.ToLower() = "create new" Then
                AddBusinessUnit(txtBU.Text.Trim())
            ElseIf btnRemove.CommandArgument.ToLower() = "remove" Then
                ' DeleteBusinessUnit(Convert.ToInt32(hidBusinessUnitgv.Value))
                hidBU.Value = hidBusinessUnitgv.Value
                lblValidationMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.BusinessUnit.RemoveWarningMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                radWindowValidation.VisibleOnPageLoad = True
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
#End Region

#Region "Grid Events"

    Protected Sub radBusinessUnit_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles radBusinessUnit.NeedDataSource
        Try
            LoadAllBusinessUnits()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Sub radBusinessUnit_DataBound(sender As Object, e As System.EventArgs) Handles radBusinessUnit.DataBound
        Try
            For Each item As Telerik.Web.UI.GridDataItem In radBusinessUnit.MasterTableView.Items
                Dim txtBUgv As TextBox = DirectCast(item.FindControl("txtBU"), TextBox)
                Dim hidBusinessUnitgv As HiddenField = DirectCast(item.FindControl("hidBusinessUnit"), HiddenField)
                Dim lblBU As Label = DirectCast(item.FindControl("lblBU"), Label)
                Dim hidstdAssigngv As HiddenField = DirectCast(item.FindControl("hidstdAssign"), HiddenField)
                Dim btnRemovegv As Button = DirectCast(item.FindControl("btnRemove"), Button)
                If Convert.ToString(hidstdAssigngv.Value).ToLower() = "yes" Then
                    btnRemovegv.Visible = False
                End If
                If lblBU.Text.Trim() <> "" Then
                    lblBU.Visible = True
                    txtBUgv.Visible = False
                    btnRemovegv.ValidationGroup = "p"
                Else
                    lblBU.Visible = False
                    txtBUgv.Visible = True
                End If
            Next
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
#End Region

    ''' <summary>
    ''' Function To Create New Business Unit
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AddBusinessUnit(ByVal Name As String) As Boolean
        Try
            Dim sErrorMessage As String = String.Empty
            Dim oBUGE As AptifyGenericEntityBase
            oBUGE = Me.AptifyApplication.GetEntityObject("BusinessUnits__c", -1)
            With oBUGE
                .SetValue("Name", Name)
                .SetValue("CompanyID", Convert.ToInt32(User1.CompanyID))
                .SetValue("Description", Name)
                .SetValue("IsActive", True)
                If oBUGE.Save(False, sErrorMessage) Then
                    radBusinessUnit.Rebind()
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
    ''' Function TO Make Business Unit InActive / Delete
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteBusinessUnit(ByVal ID As Integer) As Boolean
        Try
            Dim sErrorMessage As String = String.Empty
            Dim oBUGE As AptifyGenericEntityBase
            oBUGE = Me.AptifyApplication.GetEntityObject("BusinessUnits__c", ID)
            With oBUGE
                .SetValue("IsActive", False)
                If oBUGE.Save(False, sErrorMessage) Then
                    radBusinessUnit.Rebind()
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
    ''' Added By Pradip For BUsiness Unit Link / Page Access
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CheckBUPageAccess() As Boolean
        Try
            Dim ssSQL As String = Database & "..spCheckBULinkAccess__c @CompanyID=" & User1.CompanyID.ToString()
            Dim iCheck As Integer = DataAction.ExecuteScalar(ssSQL, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
            If iCheck > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            Return False
        End Try
    End Function
End Class
