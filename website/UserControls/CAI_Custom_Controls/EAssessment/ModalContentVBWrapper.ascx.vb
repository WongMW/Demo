Option Explicit On
Option Strict On
Imports Aptify.Framework.DataServices
Imports System.Data
Imports Aptify.Framework.Web.eBusiness

Namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.EAssessment
    Public Class ModalContentVBWrapper
        Inherits BaseUserControlAdvanced

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        End Sub

        Public Function GetLocalisationString(ByVal key As String) As String
            Dim txt = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(
                Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "SitefinityWebApp.UserControls.CAI_Custom_Controls.EAssessment." & key)),
                Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials
            )

            Return txt
        End Function
    End Class
End Namespace
