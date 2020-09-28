'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Govind Mande                08/07/2014                           Display Education FAE Result details as per student on web page
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Explicit On

Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness
Imports Telerik.Web.UI
Imports Aptify.Applications.OrderEntry


Namespace Aptify.Framework.Web.eBusiness.Generated
    Partial Class EducationFAEResultDetails__c
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
         
       Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "EducationFAEResultDetails__c"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_NAME As String = "EducationResultDetails__c"

         

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
         
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
            SetProperties()
            If Not IsPostBack Then
                ViewState("ClassRegistrationID") = Convert.ToInt32(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("CrId")))
                LoadFAEStatusDetails()
            End If
        End Sub

        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            MyBase.SetProperties()
            If String.IsNullOrEmpty(Me.RedirectURLs) Then
                Me.RedirectURLs = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_NAME)
            End If
             
        End Sub

        Private Sub LoadFAEStatusDetails()
            Try
                Dim sSQL As String = Database & "..spGetFAEResultBreakDown__c @CRID=" & ViewState("ClassRegistrationID")
                Dim dt As DataTable = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    lblHeading.Text = Convert.ToString(dt.Rows(0)("COURSE")) + " " + Convert.ToString(dt.Rows(0)("SESSION")) + " " + Convert.ToString(dt.Rows(0)("RESULT"))
                    lblStatus.Text = Convert.ToString(dt.Rows(0)("RESULT"))
                    lblDecile.Text = Convert.ToString(dt.Rows(0)("Decile__c"))
                    lblSufficiency.Text = Convert.ToString(dt.Rows(0)("SUFFICIENCY__C"))
                    If Convert.ToBoolean(dt.Rows(0)("ISCORE__C")) Then
                        If Convert.ToString(dt.Rows(0)("SUPERSIX1__C")).Trim.ToUpper = "G" Then
                            Div11.Attributes("class") = "GreenColor"
                            'divClass.Attributes.Add("background-color", "RED")
                            'idivAutumnRevision.Attributes("class") = "not_available"
                            'lblSS1.ForeColor = Drawing.Color.Green
                        ElseIf Convert.ToString(dt.Rows(0)("SUPERSIX1__C")).Trim.ToUpper = "R" Then
                            Div11.Attributes("class") = "RedColor"
                            'lblSS1.ForeColor = Drawing.Color.Red
                        ElseIf Convert.ToString(dt.Rows(0)("SUPERSIX1__C")).Trim.ToUpper = "Y" Then
                            ' lblSS1.ForeColor = Drawing.Color.Yellow
                            Div11.Attributes("class") = "YellowColor"
                        End If

                        If Convert.ToString(dt.Rows(0)("SUPERSIX2__C")).Trim.ToUpper = "G" Then
                            'lblSS2.ForeColor = Drawing.Color.Green
                            Div22.Attributes("class") = "GreenColor"
                        ElseIf Convert.ToString(dt.Rows(0)("SUPERSIX2__C")).Trim.ToUpper = "R" Then
                            'blSS2.ForeColor = Drawing.Color.Red
                            Div22.Attributes("class") = "RedColor"
                        ElseIf Convert.ToString(dt.Rows(0)("SUPERSIX2__C")).Trim.ToUpper = "Y" Then
                            'lblSS2.ForeColor = Drawing.Color.Yellow
                            Div22.Attributes("class") = "YellowColor"
                        End If

                        If Convert.ToString(dt.Rows(0)("SUPERSIX3__C")).Trim.ToUpper = "G" Then
                            'lblSS3.ForeColor = Drawing.Color.Green
                            Div33.Attributes("class") = "GreenColor"
                        ElseIf Convert.ToString(dt.Rows(0)("SUPERSIX3__C")).Trim.ToUpper = "R" Then
                            ' lblSS3.ForeColor = Drawing.Color.Red
                            Div33.Attributes("class") = "RedColor"
                        ElseIf Convert.ToString(dt.Rows(0)("SUPERSIX3__C")).Trim.ToUpper = "Y" Then
                            'lblSS3.ForeColor = Drawing.Color.Yellow
                            Div33.Attributes("class") = "YellowColor"
                        End If

                        If Convert.ToString(dt.Rows(0)("SUPERSIX4__C")).Trim.ToUpper = "G" Then
                            Div44.Attributes("class") = "GreenColor"
                            ' lblSS4.ForeColor = Drawing.Color.Green
                        ElseIf Convert.ToString(dt.Rows(0)("SUPERSIX4__C")).Trim.ToUpper = "R" Then
                            'lblSS4.ForeColor = Drawing.Color.Red
                            Div44.Attributes("class") = "RedColor"
                        ElseIf Convert.ToString(dt.Rows(0)("SUPERSIX4__C")).Trim.ToUpper = "Y" Then
                            ' lblSS4.ForeColor = Drawing.Color.Yellow
                            Div44.Attributes("class") = "YellowColor"
                        End If

                        If Convert.ToString(dt.Rows(0)("SUPERSIX5__C")).Trim.ToUpper = "G" Then
                            'lblSS5.ForeColor = Drawing.Color.Green
                            Div55.Attributes("class") = "GreenColor"
                        ElseIf Convert.ToString(dt.Rows(0)("SUPERSIX5__C")).Trim.ToUpper = "R" Then
                            ' lblSS5.ForeColor = Drawing.Color.Red
                            Div55.Attributes("class") = "RedColor"
                        ElseIf Convert.ToString(dt.Rows(0)("SUPERSIX5__C")).Trim.ToUpper = "Y" Then
                            ' lblSS5.ForeColor = Drawing.Color.Yellow
                            Div55.Attributes("class") = "YellowColor"
                        End If

                        If Convert.ToString(dt.Rows(0)("SUPERSIX6__C")).Trim.ToUpper = "G" Then
                            'lblSS6.ForeColor = Drawing.Color.Green
                            Div66.Attributes("class") = "GreenColor"
                        ElseIf Convert.ToString(dt.Rows(0)("SUPERSIX6__C")).Trim.ToUpper = "R" Then
                            Div66.Attributes("class") = "RedColor"
                            ' lblSS6.ForeColor = Drawing.Color.Red
                        ElseIf Convert.ToString(dt.Rows(0)("SUPERSIX6__C")).Trim.ToUpper = "Y" Then
                            ' lblSS6.ForeColor = Drawing.Color.Yellow
                            Div66.Attributes("class") = "YellowColor"
                        End If
                    Else
                        Div1.Visible = False
                        Div2.Visible = False
                        Div3.Visible = False
                        Div4.Visible = False
                        Div5.Visible = False
                        Div6.Visible = False
                    End If
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
  Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Response.Redirect(RedirectURLs, False)
        End Sub
    End Class
End Namespace
