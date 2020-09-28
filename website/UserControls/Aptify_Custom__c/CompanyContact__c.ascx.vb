
Imports Aptify.Framework.Web.eBusiness
Imports Aptify.Framework.Application
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports System
Imports Aptify.Framework.DataServices
Imports System.Data
Imports System.Data.SqlClient
Imports System.Net
Imports System.IO
Imports System.Collections
Namespace Aptify.Framework.Web.eBusiness.CustomerService
    Partial Class CompanyContact__c
        Inherits BaseUserControlAdvanced

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then
                If User1.CompanyID > 0 Then
                    txtCompany.Text = User1.Company
                    If txtCompany.Text <> "" Then
                        LoadaddressPhone()
                    End If

                End If
            End If
        End Sub
        Private Sub LoadaddressPhone()

            Try

                Dim sSQL As String
                Dim dtstate As DataTable = New DataTable()
                If txtCompany.Text <> "" Then
                    ' sSQL = Database & "..SpGetAddressofCompany__c @CompanyName='" & txtCompany.Text & "'"
                    ' sSQL = "SELECT  Top 1    ID, Name, AddressLine1, MainPhone, MainAreaCode, StateSenate FROM vwCompanies where Name ='" & txtCompany.Text & "'"
                    sSQL = "SELECT  Top 1    ID, Name, AddressLine1, MainPhone, MainAreaCode, StateSenate FROM vwCompanies where Name ='" & txtCompany.Text & "'"
                    dtstate = Me.DataAction.GetDataTable(sSQL)
                    If dtstate.Rows.Count > 0 Then
                        txtAddressLine1.Text = Convert.ToString(dtstate.Rows(0)("AddressLine1"))
                        txtcode.Text = Convert.ToString(dtstate.Rows(0)("MainAreaCode"))
                        txtphone.Text = Convert.ToString(dtstate.Rows(0)("MainPhone"))

                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub txtCompany_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCompany.TextChanged

            Try
                LoadaddressPhone()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Sub
        Public Function SaveWebUsercompany() As Boolean
            Dim bFlag As Boolean = False
            Dim strError As String = String.Empty
            Try
                'Dim chkstring As Boolean
                Dim oWebuserGE As AptifyGenericEntityBase
                Dim WebId As Integer
                Dim sSQLs As String
                Dim sSQL As String
                Dim dtstate As DataTable = New DataTable()
                If txtCompany.Text <> "" Then
                    ' sSQL = Database & "..SpGetAddressofCompany__c @CompanyName='" & txtCompany.Text & "'"
                    ' sSQL = "SELECT  Top 1    ID, Name, AddressLine1, MainPhone, MainAreaCode, StateSenate FROM vwCompanies where Name ='" & txtCompany.Text & "'"
                    sSQL = "SELECT  Top 1    ID, Name, AddressLine1, MainPhone, MainAreaCode, StateSenate FROM vwCompanies where Name ='" & txtCompany.Text & "'"
                    dtstate = Me.DataAction.GetDataTable(sSQL)
                    If dtstate.Rows.Count = 0 Then

                        Dim dt As DataTable = New DataTable()
                        sSQLs = Database & "..SpGetWebUserID__c @PersonID=" & User1.PersonID
                        dt = Me.DataAction.GetDataTable(sSQLs)
                        If dt.Rows.Count > 0 Then
                            WebId = Convert.ToInt32(dt.Rows(0)("ID"))
                        End If
                        oWebuserGE = AptifyApplication.GetEntityObject("Web Users", CLng(WebId))
                        If oWebuserGE IsNot Nothing Then
                            With oWebuserGE
                                .SetValue("WebCompanyName__c", txtCompany.Text)

                                .SetValue("WebCompanyAddress__c", txtAddressLine1.Text)

                                .SetValue("WebCompanyPhone__c", txtcode.Text + "-" + txtphone.Text)
                                If .Save(False, strError) Then
                                    bFlag = True
                                Else
                                    bFlag = False
                                End If
                            End With
                        End If
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return bFlag
            End Try
            Return bFlag
        End Function

    End Class
End Namespace