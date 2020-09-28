Option Explicit On

Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness

Namespace Aptify.Framework.Web.eBusiness.Generated

   ''' <summary>
   ''' Generated ASP.NET User Control for the ContactForm_Register_School_Visit_Queries__cai entity.
   ''' </summary>
   ''' <remarks></remarks>
   Partial Class ContactForm_Register_School_Visit_Queries__caiClass
       Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

       Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "ContactForm_Register_School_Visit_Queries__caiPage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_NAME As String = "RedirectURL"
        Public Property ThankYouMessage As String
        Public Property FormTitle As String
       Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If Not IsPostBack Then
                tblSuccessMessage.Visible = False
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
               If Me.SetControlRecordIDFromParam() Then
                   LoadDataFromGE(Me.AptifyApplication.GetEntityObject("ContactForm_Register_School_Visit_Queries__cai", ControlRecordID))
               Else
                   LoadDataFromGE(Me.AptifyApplication.GetEntityObject("ContactForm_Register_School_Visit_Queries__cai", -1))
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
                   oGE = AptifyApplication.GetEntityObject("ContactForm_Register_School_Visit_Queries__cai", Me.ControlRecordID)
               Else
                   oGE = AptifyApplication.GetEntityObject("ContactForm_Register_School_Visit_Queries__cai", -1)
               End If

                txtForm_Title.Text = FormTitle

               Me.TransferDataToGE(oGE)
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
                tblSuccessMessage.Text = ThankYouMessage
                tblSuccessMessage.Visible = True
                tblMain.Visible = False
           End If
       End Sub
    End Class
End Namespace
