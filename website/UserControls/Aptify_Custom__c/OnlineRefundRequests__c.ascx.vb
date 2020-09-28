Option Explicit On

Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness

Namespace Aptify.Framework.Web.eBusiness.Generated

   ''' <summary>
   ''' Generated ASP.NET User Control for the OnlineRefundRequests__c entity.
   ''' </summary>
   ''' <remarks></remarks>
   Partial Class OnlineRefundRequests__cClass
       Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

       Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "OnlineRefundRequests__cPage"
       Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_NAME As String = "RedirectURL"
       Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
           If Not IsPostBack Then
            SetProperties()
               LoadRecord()
           End If
       End Sub
       Protected Overrides Sub SetProperties()
       If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
        MyBase.SetProperties()
         If String.IsNullOrEmpty(Me.RedirectURL) Then
        Me.RedirectURL = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_NAME)
        End If
        If String.IsNullOrEmpty(Me.RedirectURL) Then
         End If
         If String.IsNullOrEmpty(Me.RedirectIDParameterName) Then Me.RedirectIDParameterName = "ID"
        If String.IsNullOrEmpty(Me.AppendRecordIDToRedirectURL) Then Me.AppendRecordIDToRedirectURL = "true"
        If String.IsNullOrEmpty(Me.EncryptQueryStringValue) Then Me.EncryptQueryStringValue = "true"
       If String.IsNullOrEmpty(Me.QueryStringRecordIDParameter) Then Me.QueryStringRecordIDParameter = "ID"
        End Sub
       Protected Overridable Sub LoadRecord()
            Try

                If Request.QueryString("OrderID") IsNot Nothing AndAlso IsNumeric(Request.QueryString("OrderID")) AndAlso CLng(Request.QueryString("OrderID")) > 0 Then
                    SetOnlineRefundInformation(CLng(Request.QueryString("OrderID")))
                End If


                If Me.SetControlRecordIDFromParam() Then
                    Dim oGE As AptifyGenericEntityBase = Nothing
                    oGE = Me.AptifyApplication.GetEntityObject("OnlineRefundRequests__c", Me.ControlRecordID)

                    SetOnlineRefundInformation(CLng(oGE.GetValue("OrderID")))

                    LoadDataFromGE(Me.AptifyApplication.GetEntityObject("OnlineRefundRequests__c", Me.ControlRecordID))

                    trResponse.Visible = True
                    cmdSave.Visible = False

                    trAmount.Visible = False
                    trReadOnlyAmount.Visible = True

                    trRequest.Visible = False
                    trReadOnlyRequest.Visible = True
                Else
                    LoadDataFromGE(Me.AptifyApplication.GetEntityObject("OnlineRefundRequests__c", -1))

                    trResponse.Visible = False
                    lblStatus.Text = "Pending"
                    cmdSave.Visible = True

                    trAmount.Visible = True
                    trReadOnlyAmount.Visible = False

                    trRequest.Visible = True
                    trReadOnlyRequest.Visible = False
                End If

                trID.Visible = False
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
       End Sub
        Private Sub SetOnlineRefundInformation(ByVal lOrderID As Long)
            Dim sSQL As String, dt As DataTable
            Try
                lblOrderID.Text = Convert.ToString(lOrderID)

                sSQL = "SELECT Balance, Calc_GrandTotal FROM APTIFY..vwOrders WHERE ID = " & lOrderID
                dt = DataAction.GetDataTable(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    lblOrderBalance.Text = Convert.ToString(dt.Rows(0)("Balance"))
                    lblOrderTotal.Text = Convert.ToString(dt.Rows(0)("Calc_GrandTotal"))
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Overridable Sub SaveRecord(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
            Dim oGE As AptifyGenericEntityBase
            Dim bRedirect As Boolean = False

            Try
                If Me.ControlRecordID > 0 Then
                    oGE = AptifyApplication.GetEntityObject("OnlineRefundRequests__c", Me.ControlRecordID)
                Else
                    oGE = AptifyApplication.GetEntityObject("OnlineRefundRequests__c", -1)
                    oGE.SetValue("Request", txtRequest.Text.Trim())
                    oGE.SetValue("Amount", txtAmount.Text.Trim())
                    oGE.SetValue("OrderID", CLng(Request.QueryString("OrderID")))
                End If

                'Me.TransferDataToGE(oGE)
                If oGE.Save(False) Then
                    bRedirect = True
                Else
                    lblError.Visible = True
                    lblError.Text = oGE.LastError()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

            If bRedirect Then
                'If Me.AppendRecordIDToRedirectURL Then
                '    If Me.EncryptQueryStringValue Then
                '        Me.RedirectURL &= "?" & Me.RedirectIDParameterName & "=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(oGE.RecordID.ToString))
                '    Else
                '        Me.RedirectURL &= "?" & Me.RedirectIDParameterName & "=" & oGE.RecordID.ToString
                '    End If
                'End If
                'Response.Redirect(Me.RedirectURL)

                Response.Redirect("~/CustomerService/orderhistory")
            End If
        End Sub
    End Class
End Namespace
