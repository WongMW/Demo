'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Shital Jadhav                23/11/2015                        Created to show script marking related to corrector.
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Explicit On
Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness
Imports Telerik.Web.UI


Namespace Aptify.Framework.Web.eBusiness
    Partial Class LLLCorrectorWebPortal__c
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
#Region "Page Load"
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Try
                If Not IsPostBack Then
                    'Call function to bind script details.
                    BindScriptDetails()
                    ucUpload.AllowAdd = True
                    ucUpload.AllowDelete = True
                End If
                'If a file is uploaded for the script marking then user can not upload another file so disable the upload control.
                Dim grd As RadGrid
                Dim pnlupload As Panel
                grd = TryCast(ucUpload.FindControl("grdAttachments"), RadGrid)
                pnlupload = TryCast(ucUpload.FindControl("pnlUpload"), Panel)
                If grd.MasterTableView.Items.Count > 0 Then
                    pnlupload.Visible = False
                Else
                    pnlupload.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region


#Region "Grid Events"
        ''' <summary>
        ''' Function to bind the grid.
        ''' </summary>
        ''' <remarks></remarks>

        Protected Sub BindScriptDetails()
            Try

                Dim oParams(0) As IDataParameter
                oParams(0) = DataAction.GetDataParameter("@CorrectorID", SqlDbType.BigInt, User1.PersonID)
                Dim sSql As String = Database & "..spGetCorrectorWebPageScript__c"
                Dim dt As DataTable = DataAction.GetDataTableParametrized(sSql, CommandType.StoredProcedure, oParams)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    radgrdScriptDetails.DataSource = dt
                    radgrdScriptDetails.DataBind()
                    radgrdScriptDetails.Visible = True
                    hearderscript.Visible = True
                    ucUpload.AllowAdd = True
                    ucUpload.AllowDelete = True
                    lblmsg.Visible = False
                Else
                    radgrdScriptDetails.Visible = False
                    hearderscript.Visible = False
                    ucUpload.AllowAdd = False
                    ucUpload.AllowDelete = False
                    lblmsg.Visible = True
                    lblmsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.LLLCorrectorWebPortal.CorrectorAssignment__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Function is used to show popup on attacment click.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub BindOfficcerInfo_click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                radDownloadDocuments.VisibleOnPageLoad = True
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Function is used to close the popup for attachment.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub btnClose_Click(sender As Object, e As System.EventArgs) Handles btnClose.Click
            Try
                radDownloadDocuments.VisibleOnPageLoad = False
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Function is used to change the status of script marking if user upload the updated document.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
            Try
                Dim grd As RadGrid
                grd = TryCast(ucUpload.FindControl("grdAttachments"), RadGrid)
                If grd.MasterTableView.Items.Count > 0 Then
                    Dim oScriptMarking As AptifyGenericEntityBase
                    oScriptMarking = AptifyApplication.GetEntityObject("ScriptMarking__c", ucUpload.RecordID)
                    If Not oScriptMarking Is Nothing AndAlso _
                       oScriptMarking.RecordID = ucUpload.RecordID Then
                        oScriptMarking.SetValue("Status", "Script Scoring Sheet Uploaded")
                        oScriptMarking.Save(False)
                    End If

                End If
                BindScriptDetails()

                radDownloadDocuments.VisibleOnPageLoad = False
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' To check the click of grid to show popup and also attachment.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub radgrdScriptDetails_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles radgrdScriptDetails.ItemCommand
            Try
                If e.CommandName.ToUpper = "Attachment".ToUpper Then
                    radDownloadDocuments.VisibleOnPageLoad = True

                    Dim data As String() = e.CommandArgument.ToString().Split(CChar(";"))
                    Dim intrecordID As Integer = Convert.ToInt64(data(0))
                    Dim intentityID As Integer = Convert.ToInt64(Me.AptifyApplication.GetEntityID("ScriptMarking__c"))
                    ucDownload.AttachmentCategory = Me.AptifyApplication.GetEntityRecordIDFromRecordName("Attachment Categories", "Assigned Scripts")
                    ucDownload.LoadAttachments(intentityID, intrecordID, True)

                    ucUpload.AllowAdd = True
                    ucUpload.AllowDelete = True
                    ucUpload.AttachmentCategory = Me.AptifyApplication.GetEntityRecordIDFromRecordName("Attachment Categories", "Uploaded Scripts")
                    ucUpload.LoadAttachments(intentityID, intrecordID, True)
                    'If a file is uploaded for the script marking then user can not upload another file so disable the upload control.
                    Dim grd As RadGrid
                    Dim pnlupload As Panel
                    grd = TryCast(ucUpload.FindControl("grdAttachments"), RadGrid)
                    pnlupload = TryCast(ucUpload.FindControl("pnlUpload"), Panel)
                    If grd.MasterTableView.Items.Count > 0 Then
                        pnlupload.Visible = False
                    Else
                        pnlupload.Visible = True
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Need data source event for grid.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub radgrdScriptDetails_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles radgrdScriptDetails.NeedDataSource
            Try
                BindScriptDetails()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region
    End Class
End Namespace
