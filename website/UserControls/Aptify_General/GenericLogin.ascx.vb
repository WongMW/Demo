
Partial Class UserControls_Aptify_General_GenericLogin
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If User1.UserID > 0 Then
            lblstat.Visible = False
        Else
            lblstat.Visible = True
        End If
    End Sub
End Class
