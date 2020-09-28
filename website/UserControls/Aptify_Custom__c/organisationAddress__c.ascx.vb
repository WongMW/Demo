Option Explicit On

Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness



Partial Class organisationAddress__c
    Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
    Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "organisationAddress__c"
    Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_NAME As String = "RedirectURLs"


    Public Overridable Property RedirectURLs() As String
        Get
            If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_NAME) Is Nothing Then
                Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_NAME))
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_NAME) = Me.FixLinkForVirtualPath(value)
        End Set
    End Property

    Protected Overrides Sub SetProperties()

        Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME

        MyBase.SetProperties()
        If String.IsNullOrEmpty(RedirectURLs) Then
            Me.RedirectURLs = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_NAME)
        End If

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SetProperties()
            'LoadCompany()
            LoadRecord()
        End If
    End Sub

    Protected Overridable Sub LoadRecord()
        Try
            Dim sqlstr As String = String.Empty
            Dim dtOrganisationDetails As System.Data.DataTable = New DataTable()
            sqlstr = Database & "..SpGetOrganisationDetails__c"
            dtOrganisationDetails = Me.DataAction.GetDataTable(sqlstr)
            dtOrganisationDetails = DataAction.GetDataTable(sqlstr)
           If Not dtOrganisationDetails Is Nothing AndAlso dtOrganisationDetails.Rows.Count > 0 Then
            lblAddress.Text = ((dtOrganisationDetails.Rows(0)("Line1")) + " " + (dtOrganisationDetails.Rows(0)("Line2")) + " " + (dtOrganisationDetails.Rows(0)("Line3")) + " " + (dtOrganisationDetails.Rows(0)("Line4")) + " " + (dtOrganisationDetails.Rows(0)("City")) + " " + (dtOrganisationDetails.Rows(0)("PostalCode")) + " " + (dtOrganisationDetails.Rows(0)("Expr1")))
            email.Text = dtOrganisationDetails.Rows(0)("MainEmail")
            email.NavigateUrl = "mailTo:" & dtOrganisationDetails.Rows(0)("MainEmail")
           End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
End Class
