
Namespace SitefinityWebApp.BusinessFacade.Services.VB

    Public Class CultureUtility

        Public Function GetCultureLocal(ByVal key As String, ByVal id As String) As String
            Dim txt = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(
                Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", key)),
                Convert.ToInt32(id), DataAction.UserCredentials
            )
            Return txt
        End Function
    End Class

End Namespace