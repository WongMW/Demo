''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer              Date Created/Modified               Summary
'Pradip Chavhan         04/21/2016                      Control For Chariot Index Page
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Imports System.Data
Imports Telerik.Web.UI
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports System.IO
Imports Aptify.Framework.Application
Imports System.Text.RegularExpressions
Namespace Aptify.Framework.Web.eBusiness.Education
    Partial Class Chariot__c
        Inherits eBusiness.BaseUserControlAdvanced

        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_CHARIOT As String = "ChariotIndex"
        
        Dim sDeletedID As String = String.Empty


#Region "Property Setting"
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

        Public Overridable Property ChariotPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_CHARIOT) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_CHARIOT))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_CHARIOT) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

       

        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(LoginPage) Then
                LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage)
            End If
            If String.IsNullOrEmpty(ChariotPage) Then
                ChariotPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_CHARIOT)
            End If
            
        End Sub

#End Region

#Region "Page Events"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                SetProperties()
                If AptifyEbusinessUser1.PersonID <= 0 Then
                    Session("ReturnToPage") = Request.RawUrl
                    Response.Redirect(LoginPage)
                End If
                lblMessage.Text = ""
                If Not IsPostBack Then
                    CheckChariotLink()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region
        'Adde by Pradip 2016-04-21 to check access for Chariot Index Page
        Private Sub CheckChariotLink()
            Try
                'Dim sSQL As String = Database & "..spCheckChariotPageAccess__c @StudentId=" & AptifyEbusinessUser1.PersonID
                'Dim lID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSQL))
                'If lID > 0 Then
                Dim Cookie As New HttpCookie("Chariot")
                Cookie.Value = "Yes"
                Response.Cookies.Add(Cookie)
               Response.Redirect(ChariotPage)

 'Response.Redirect(LoginPage)
                'Else

                'End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        'Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        '    Dim aCookie As New HttpCookie("Pradip")
        '    aCookie.Value = "Yes"
        '    Response.Cookies.Add(aCookie)
        '    Response.Redirect("http://localhost/ebusiness/CHARIOT/lindexNew.html")
        'End Sub
    End Class
End Namespace
