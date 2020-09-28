Option Explicit On
Option Strict On
Imports System
Imports System.Configuration
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices

Namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.GDPR

    Partial Class UserTopicPreferences
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not IsPostBack() Then
                FilterTopicCodes()
                Dim message = GetLocalisationString("SitefinityWebApp.UserControls.CAI_Custom_Controls.GDPR.UserTopicPreferences.Message")
                If (Not String.IsNullOrEmpty(message)) Then
                    txtUserTopicPreferences.InnerHtml = message
                End If
            End If
        End Sub

        Public Function GetLocalisationString(ByVal key As String) As String
            Dim txt = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(
                Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", key)),
                Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials
            )
            Return txt
        End Function

        Private Sub FilterTopicCodes()
            Dim filters = ConfigurationManager.AppSettings("UserPreferences.TopicCodeFilter")

            If (String.IsNullOrWhiteSpace(filters)) Then Return
            TopicCodeControl1.TopicFilterCommaList = filters

        End Sub

    End Class

End Namespace




