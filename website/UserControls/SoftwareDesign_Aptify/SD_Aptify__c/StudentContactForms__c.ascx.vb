Option Explicit On

Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness

Namespace Aptify.Framework.Web.eBusiness.Generated

   ''' <summary>
   ''' Generated ASP.NET User Control for the StudentContactForms__c entity.
   ''' </summary>
   ''' <remarks></remarks>
   Partial Class StudentContactForms__cClass
       Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

       Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "StudentContactForms__cPage"
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
                   LoadDataFromGE(Me.AptifyApplication.GetEntityObject("StudentContactForms__c", ControlRecordID))
               Else
                   LoadDataFromGE(Me.AptifyApplication.GetEntityObject("StudentContactForms__c", -1))
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
                   oGE = AptifyApplication.GetEntityObject("StudentContactForms__c", Me.ControlRecordID)
               Else
                   oGE = AptifyApplication.GetEntityObject("StudentContactForms__c", -1)
                End If

                txtepiServerPageName.Text = FormTitle

                Dim isError As Boolean

                ' verifying data
                If String.IsNullOrEmpty(txtname.Text) Then
                    ' show error for name
                    lblname.Visible = True
                    isError = True
                Else
                    lblname.Visible = False
                End If

                If String.IsNullOrEmpty(txtemail.Text) Then
                    ' show error for email
                    lblemail.Visible = True
                    lblemail.Text = "Email is required"
                    isError = True
                Else
                    Dim regUtil As New SoftwareDesign.RegexUtilities()

                    If Not regUtil.IsValidEmail(txtemail.text) Then
                        lblemail.Visible = True
                        isError = True
                        lblemail.Text = "Email is not valid"
                    Else
                        lblemail.Visible = False
                    End If
                End If

                If String.IsNullOrEmpty(txtquery.Text) Then
                    ' show error for email
                    lblquery.Visible = True
                    isError = True
                Else
                    lblquery.Visible = False
                End If

                If Not isError Then
                    Me.TransferDataToGE(oGE)
                    If oGE.Save(False) Then
                        bRedirect = True
                        lblError.Visible = False
                    Else
                        lblError.Visible = True
                        lblError.Text = oGE.LastError()
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

            If bRedirect Then
                tblSuccessMessage.Text = ThankYouMessage
                tblSuccessMessage.Visible = True
                tblMain.Visible = False
            End If
       End Sub
    End Class
End Namespace
