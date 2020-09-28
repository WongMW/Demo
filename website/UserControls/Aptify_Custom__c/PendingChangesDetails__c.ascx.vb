
'-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
'DEVELOPER                              DATE                                        Comments
'Ganesh I                               23/03/2014                                 Creating new PendingChangeDetails  control for showing pending changes on ebiz

'Kavita Zinage                          01/02/2016                                 Updated code for showing pending changes on ebiz
'----------------------------------------------------------------------------------------------------------------------------------------------------------------------

'Aptify e-Business 5.5.1, July 2013

Option Explicit On
Option Strict On

Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic.Security
Imports System.Data
Imports Telerik.Web.UI

Namespace Aptify.Framework.Web.eBusiness.CustomerService
    Partial Class PendingChangesDetails__c
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

        'Changes by Ganesh I on 31/03/2014
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "PendingChangesDetails__c"
        Protected Const ATTRIBUTE_LOGIN_PAGE As String = "LoginPage"


#Region "Pending Changes Specific Properties"

        Public Overridable Property EntityName() As String
            Get
                Return Convert.ToString(ViewState("P_EntityName"))
            End Get
            Set(ByVal value As String)
                ViewState("P_EntityName") = value
            End Set
        End Property

        Public Overridable Property RecordID() As Long
            Get
                Return Convert.ToInt32(ViewState("P_RecordID"))
            End Get
            Set(ByVal value As Long)
                ViewState("P_RecordID") = value
            End Set
        End Property

        Public Overridable Property PendingChangeType() As String
            Get
                Return Convert.ToString(ViewState("P_PendingChangeType"))
            End Get
            Set(ByVal value As String)
                ViewState("P_PendingChangeType") = value
            End Set
        End Property

#End Region

        'Loading PendingChanges details on Ebiz page

        Public Sub LoadPendingChanges(ByVal EntityName As String, ByVal RecordID As Long, ByVal PendingChangeType As String)
            Dim sSql As String = String.Empty
            Dim dtPendingChanges As DataTable = Nothing
            Dim param(2) As IDataParameter
            Try
                'Added By Kavita Zinage - To Load Details per Entity
                If EntityName.ToLower = "persons" Then
                    sSql = Database & "..spGetPendingChanges__c"
                ElseIf EntityName.ToLower = "companies" Then
                    sSql = Database & "..spGetPendingChangesforCompanyRec__c"  'Added BY Kavita Zinage
                End If

                Dim EntityID As Long = AptifyApplication.GetEntityID(EntityName)

                param(0) = DataAction.GetDataParameter("@EntityID ", SqlDbType.BigInt, EntityID)
                param(1) = DataAction.GetDataParameter("@RecordID ", SqlDbType.BigInt, RecordID)
                param(2) = DataAction.GetDataParameter("@PendingChangeType ", SqlDbType.VarChar, PendingChangeType)

                dtPendingChanges = DataAction.GetDataTableParametrized(sSql, CommandType.StoredProcedure, param)

                If dtPendingChanges IsNot Nothing AndAlso dtPendingChanges.Rows.Count > 0 Then
                    'for getting current value in pendingchanges grid
                    'Added By Kavita Zinage 28/01/2016
                    Dim result As String

                    For Each dr As DataRow In dtPendingChanges.Rows
                        result = String.Empty
                        Dim end1 As Integer
                        Dim NewValue As String = String.Empty
                        'Dim Value As String = dr("Changes").ToString()
                        If Convert.ToString(dr("Changes")).IndexOf("from") > 0 Then
                            Dim start As Integer = Convert.ToString(dr("Changes")).IndexOf("from") + 4
                            end1 = Convert.ToString(dr("Changes")).IndexOf("to", start)
                            result = Convert.ToString(dr("Changes")).Substring(start, end1 - start)
                        Else
                            result = "(Blank)"
                        End If
                        If Convert.ToString(dr("Changes")).IndexOf("to") > 0 Then
                            Dim iNewValueIndex As Integer = Convert.ToString(dr("Changes")).IndexOf(Environment.NewLine, end1 + 2)

                            If iNewValueIndex = -1 Then
                                NewValue = Convert.ToString(dr("Changes")).Substring(end1 + 2, Convert.ToString(dr("Changes")).Length - (end1 + 2))
                            Else
                                NewValue = Convert.ToString(dr("Changes")).Substring(end1 + 2, iNewValueIndex - (end1 + 2))
                            End If
                        Else
                            NewValue = Convert.ToString(dr("NewValue"))
                        End If
                        dr("Changes") = Convert.ToString(result)
                        dr("NewValue") = NewValue.ToString()
                    Next

                    rgvPendingchange.Visible = True
                    rgvPendingchange.DataSource = dtPendingChanges
                    rgvPendingchange.DataBind()
                Else
                    rgvPendingchange.DataSource = dtPendingChanges
                    rgvPendingchange.DataBind()
                End If
                ViewState("PendingChanges") = dtPendingChanges
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        'For paging on radgrid
        'Protected Sub rgvPendingchange_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgvPendingchange.NeedDataSource
        '    LoadPendingChanges(EntityName, RecordID, PendingChangeType)
        'End Sub

        Protected Sub rgvPendingchange_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles rgvPendingchange.PageIndexChanging
            Try
                rgvPendingchange.PageIndex = e.NewPageIndex
                rgvPendingchange.DataSource = TryCast(ViewState("PendingChanges"), DataTable)
                rgvPendingchange.DataBind()
            Catch ex As Exception

            End Try
        End Sub
    End Class
End Namespace
