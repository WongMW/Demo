Option Explicit On

Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness

Namespace Aptify.Framework.Web.eBusiness.Generated

   ''' <summary>
   ''' Generated ASP.NET User Control for the RoomBookingApplications__c entity.
   ''' </summary>
   ''' <remarks></remarks>
   Partial Class RoomBookingApplications__cClass
       Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

       Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "RoomBookingApplications__cPage"
       Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_NAME As String = "RedirectURL"
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
            If AptifyEbusinessUser1.UserID > 0 Then
                If Not IsPostBack Then
                    SetProperties()
                    LoadRecord()
                End If
            Else
                Session("ReturnToPage") = Request.RawUrl
                Response.Redirect("~/Home.aspx" + "?ReturnURL=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(Request.RawUrl)))
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
                If Me.SetControlRecordIDFromParam() Then
                    Dim oRoomBookGE As AptifyGenericEntityBase = Nothing
                    oRoomBookGE = Me.AptifyApplication.GetEntityObject("RoomBookingApplications__c", ControlRecordID)
                    LoadDataFromGE(Me.AptifyApplication.GetEntityObject("RoomBookingApplications__c", ControlRecordID))
                    txtRequesterName.Text = oRoomBookGE.GetValue("Requester").ToString()
                Else
                    LoadDataFromGE(Me.AptifyApplication.GetEntityObject("RoomBookingApplications__c", -1))
                    txtRequesterName.Text = AptifyEbusinessUser1.FirstName & " " & AptifyEbusinessUser1.LastName
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
                   oGE = AptifyApplication.GetEntityObject("RoomBookingApplications__c", Me.ControlRecordID)
               Else
                   oGE = AptifyApplication.GetEntityObject("RoomBookingApplications__c", -1)
               End If

                Me.TransferDataToGE(oGE)
                oGE.SetValue("RequesterID", AptifyEbusinessUser1.PersonID)
               If oGE.Save(False) Then
                    bRedirect = True
                    Response.Redirect("~/Meetings/room-booking")
               Else
                   lblError.Visible = True
                   lblError.Text = oGE.LastError()
               End If
           Catch ex As Exception
               Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
           End Try



           'If bRedirect Then
           '    If Me.AppendRecordIDToRedirectURL Then
           '        If Me.EncryptQueryStringValue Then
           '           Me.RedirectURL &= "?" & Me.RedirectIDParameterName & "=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(oGE.RecordID.ToString))
           '        Else
           '            Me.RedirectURL &= "?" & Me.RedirectIDParameterName & "=" & oGE.RecordID.ToString
           '        End If
           '    End If
           '    Response.Redirect(Me.RedirectURL)
           'End If
       End Sub
    End Class
End Namespace
