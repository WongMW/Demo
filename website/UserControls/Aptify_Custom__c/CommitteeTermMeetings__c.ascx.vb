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
Imports Telerik.Web.UI

Partial Class CommitteeTermMeetings__c
    Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
    Dim lTermID As Long
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            lTermID = Convert.ToInt32(Request.QueryString("TermID"))
            If Not IsPostBack Then
                If IsTermMember() Then
                    BindCommitteeMeetings(lTermID) ' Bind Committe Terms Meetings
                    LoadAttachment() ' Load Attachment
                Else
                    Response.Redirect(Me.GetSecurityErrorPageFromXML & "?Message=" & Server.UrlEncode("Unathorised committee term member"))
                End If
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Determines whether [is term member]. added by Govind M on 26 June 2017
    ''' </summary>
    ''' <returns></returns>
    Private Function IsTermMember() As Boolean
        Dim sSQL As String = Database & "..spCheckMemberInTerms__c @TermID=" & Convert.ToInt32(Request.QueryString("TermID")) & ",@MemberID=" & User1.PersonID
        Dim IsMember As Boolean = CBool(DataAction.ExecuteScalar(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache))
        Return IsMember
    End Function
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
                rb = DirectCast(row.FindControl("rdoSelect"), RadioButton)
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
            Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                grdMain.DataSource = dt
                grdMain.DataBind()
            Else
                grdMain.Visible = False
                lblMsg.Visible = True
                lblMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.CommitteeMeetings.NotFound")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
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
            rb = DirectCast(row.FindControl("rdoSelect"), RadioButton)
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
            Dim lEntityId As Long
            lEntityId = CLng(Me.AptifyApplication.GetEntityID("Products"))
            LoadDataFromGE(Me.AptifyApplication.GetEntityObject("Products", MeetingID))
            Dim sSql As String = AptifyApplication.GetEntityBaseDatabase("Products") & "..spGetCommitteeMeetingsEndDate__c @ProductID=" & MeetingID
            Dim MeetingProductID As Long = Convert.ToInt64(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
            If MeetingProductID > 0 Then
                Me.RecordAttachments__c.AllowAdd = True
            Else
                Me.RecordAttachments__c.AllowAdd = False
            End If
            Me.RecordAttachments__c.LoadAttachments(lEntityId, MeetingID)
            Dim grdAttachments As RadGrid = TryCast(Me.RecordAttachments__c.FindControl("grdAttachments"), RadGrid)
            If Not grdAttachments Is Nothing AndAlso grdAttachments.Items.Count > 0 Then
            Else
                If MeetingProductID > 0 Then
                Else
                    trRecordAttachment.Visible = False
                End If
            End If
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
            BindCommitteeMeetings(lTermID)
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
End Class
