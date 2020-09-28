Option Explicit On

Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness

Namespace Aptify.Framework.Web.eBusiness.Generated

    ''' <summary>
    ''' Generated ASP.NET User Control for the Cases entity.
    ''' Description: Tracks individual cases as they go through various stages of resolution from initial entry through final closure
    ''' </summary>
    ''' <remarks></remarks>
    Partial Class CasesClass
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "Cases"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_NAME As String = "RedirectURL"


        Public Overridable Property RedirectURL() As String
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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            SetProperties()
            If Not IsPostBack Then
                LoadRecord()
            End If
        End Sub


        Protected Overrides Sub SetProperties()
            Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME

            MyBase.SetProperties()
            If String.IsNullOrEmpty(RedirectURL) Then
                Me.RedirectURL = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_NAME)
            End If
        
        End Sub
        Protected Overridable Sub LoadRecord()
            Try
                If Me.SetControlRecordIDFromParam() Then
                    LoadDataFromGE(Me.AptifyApplication.GetEntityObject("Cases", ControlRecordID))
                Else
                    LoadDataFromGE(Me.AptifyApplication.GetEntityObject("Cases", -1))
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
                    oGE = AptifyApplication.GetEntityObject("Cases", Me.ControlRecordID)
                Else
                    oGE = AptifyApplication.GetEntityObject("Cases", -1)
                End If

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

            'If bRedirect Then
            '    If Me.AppendRecordIDToRedirectURL Then
            '        If Me.EncryptQueryStringValue Then
            '            Me.RedirectURL &= "?" & Me.RedirectIDParameterName & "=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(oGE.RecordID.ToString))
            '        Else
            '            Me.RedirectURL &= "?" & Me.RedirectIDParameterName & "=" & oGE.RecordID.ToString
            '        End If
            '    End If
            Response.Redirect(Me.RedirectURL)
            'SetProperties()
            'End If
        End Sub
    End Class
End Namespace
