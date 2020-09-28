Option Explicit On

Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness
Imports Telerik.Web.UI

Namespace Aptify.Framework.Web.eBusiness.Generated

   ''' <summary>
   ''' Generated ASP.NET Grid User Control for the Conferring__c entity.
   ''' </summary>
   ''' <remarks></remarks>
    Partial Class Conferring__c
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "Conferring__c"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_NAME As String = "RedirectURL"
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If Not IsPostBack Then
                'SetProperties()
                LoadGrid()
            End If
        End Sub

        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            MyBase.SetProperties()
            If String.IsNullOrEmpty(Me.RedirectURL) Then
                Me.RedirectURL = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_NAME)
            End If
            If String.IsNullOrEmpty(Me.RedirectURL) Then
                'Me.grdMain.Enabled = False
                Me.grdMain.ToolTip = "property has not been set."
            End If
            If String.IsNullOrEmpty(Me.RedirectIDParameterName) Then Me.RedirectIDParameterName = "ReportID"
        End Sub
        Protected Overridable Sub LoadGrid()
            Try
                Dim sSQL As String, dt As DataTable
                'sSQL = "SELECT *  FROM APTIFY..vwConferring__c"
                sSQL = Database & "..spConferringRecord__c @PersonID=" & AptifyEbusinessUser1.PersonID.ToString()
                dt = DataAction.GetDataTable(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)


                grdMain.DataSource = dt
                grdMain.DataBind()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub


        Protected Sub Submit_Click(sender As Object, e As EventArgs) Handles Submit.Click
            Dim oConferrings As AptifyGenericEntityBase

            For Each gvr As GridViewRow In grdMain.Rows
                Dim lblid As Label = DirectCast(gvr.FindControl("Lblid"), Label)
                Dim CID As Long
                CID = CLng(lblid.Text)

                Dim tb As TextBox = DirectCast(gvr.FindControl("txtcert"), TextBox)
                Dim txt As String = tb.Text
                If tb.Text = "" Then
                Else

                    oConferrings = AptifyApplication.GetEntityObject("Conferring__c", CID)
                    oConferrings.SetValue("CertificateName", tb.Text)
                  
                    oConferrings.Save()
                End If

            Next

        End Sub
    End Class
End Namespace
