'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Govind Mande                03/5/2014                            Displaying Commitee Tearms Meeting and document
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Explicit On

Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness
Imports System.Web.UI
Partial Class CommitteeTermMeetings__c
    Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
    Dim TermID As Long
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            TermID = Convert.ToInt32(Request.QueryString("TermID"))
            If Not IsPostBack Then
                BindCommitteeMeetings(TermID) ' Bind Committe Terms Meetings
                LoadAttachment() ' Load Attachment
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Load Attachment
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadAttachment()
        Try
            Dim rb As RadioButton
            Dim lMeetinID As Label
            For Each row As Telerik.Web.UI.GridItem In grdMain.Items
                lMeetinID = DirectCast(row.FindControl("lblMeetingID"), Label)
                rb = DirectCast(row.FindControl("RadioButton1"), RadioButton)
                rb.Checked = True
                displayRecordAttachment(Convert.ToInt32(lMeetinID.Text))
                Exit For
            Next
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Bind Meetings as per Terms 
    ''' </summary>
    ''' <param name="TermID"></param>
    ''' <remarks></remarks>
    Private Sub BindCommitteeMeetings(ByVal TermID As Long)
        Try
            Dim sSql As String = Database & "..spGetCommiteeTermMeetings__c @CommitteeTermID=" & TermID
            Dim dt As DataTable = DataAction.GetDataTable(sSql)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                grdMain.DataSource = dt
                grdMain.DataBind()
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ''' <summary>
    ''' on changed meeting displaying Attachment on the selected meeting
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rdSelect_CheckedChanged(sender As Object, e As EventArgs)
        Dim selectButton As RadioButton = DirectCast(sender, RadioButton)
        Dim rb As RadioButton
        For Each row As Telerik.Web.UI.GridItem In grdMain.Items
            rb = DirectCast(row.FindControl("RadioButton1"), RadioButton)
            rb.Checked = False
        Next
        selectButton.Checked = True
        Dim drRow As Telerik.Web.UI.GridDataItem = DirectCast(selectButton.NamingContainer, Telerik.Web.UI.GridDataItem)
        Dim lblMeetingID As Label = DirectCast(drRow.FindControl("lblMeetingID"), Label)
        displayRecordAttachment(Convert.ToInt32(lblMeetingID.Text))
    End Sub
    ''' <summary>
    ''' Display Attachment as per Meeting
    ''' </summary>
    ''' <param name="MeetingID"></param>
    ''' <remarks></remarks>
    Protected Sub displayRecordAttachment(ByVal MeetingID As Integer)
        Try
            trRecordAttachment.Visible = True
            Me.RecordAttachments__c.Visible = True
            Dim EntityId As Long
            EntityId = CLng(Me.AptifyApplication.GetEntityID("Products"))
            LoadDataFromGE(Me.AptifyApplication.GetEntityObject("Products", MeetingID))
            Dim sSql As String = AptifyApplication.GetEntityBaseDatabase("Products") & "..spGetCommitteeMeetingsEndDate__c @ProductID=" & MeetingID
            Dim MeetingProductID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSql))
            If MeetingProductID > 0 Then
                Me.RecordAttachments__c.AllowAdd = True
            Else
                Me.RecordAttachments__c.AllowAdd = False
            End If
            Me.RecordAttachments__c.LoadAttachments(EntityId, MeetingID)
           
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ''' <summary>
    ''' This method use is to calling Teleric filter 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub grdMain_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdMain.NeedDataSource
        Try
            BindCommitteeMeetings(TermID)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
End Class
