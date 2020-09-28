'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Govind Mande                26/11/2014                          For Firm Admin approve the examption
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Explicit On
Option Strict On

Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic.Security
Imports System.Data
Imports Telerik.Web.UI
Imports Aptify.Framework.BusinessLogic.GenericEntity

Namespace Aptify.Framework.Web.eBusiness.CustomerService
    Partial Class FirmApprovalPortal__c
        Inherits BaseUserControlAdvanced


        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "FirmApprovalPortal__c"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'set control properties from XML file if needed
            SetProperties()
            If Not IsPostBack Then
                LoadStudentDetails()
            End If

        End Sub

        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            'call base method to set parent properties
            MyBase.SetProperties()
        End Sub

        ''' <summary>
        ''' Load student details
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadStudentDetails()
            Try
                Dim sSqlCompanyID As String = "..spCompanyIDForPerson @PersonID=" & User1.PersonID
                Dim lCompanyID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSqlCompanyID, IAptifyDataAction.DSLCacheSetting.BypassCache))
                ViewState("CompanyID") = lCompanyID
                Dim sSql As String = Database & "..spGetStudentApprovalExamption__c @CompanyID=" & lCompanyID
                dtDataMaintained = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dtDataMaintained Is Nothing AndAlso dtDataMaintained.Rows.Count > 0 Then
                    lblSuccessMsg.Text = ""
                    lblErrorMsg.Text = ""
                    radGrdFirmApproval.Visible = True
                    btnApprove.Visible = True

                Else
                    lblErrorMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusinessFirmApprovalPortal.ErrorMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    lblSuccessMsg.Text = ""
                    radGrdFirmApproval.Visible = False
                    btnApprove.Visible = False
                    btnSubmit.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Public Property dtDataMaintained() As DataTable
            Get

                If Not Session("dtDataMaintained") Is Nothing Then
                    Return CType(Session("dtDataMaintained"), DataTable)
                Else
                    Return Nothing
                End If
            End Get
            Set(ByVal value As DataTable)
                Session("dtDataMaintained") = value
            End Set
        End Property

        ''' <summary>
        ''' Set Approval drop down list 
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub radGrdFirmApproval_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles radGrdFirmApproval.ItemDataBound
            Try
                If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
                    Dim ddlApproval As DropDownList = DirectCast(e.Item.FindControl("ddlApproval"), DropDownList)
                    Dim sSql As String = Database & "..spGetFirmApprovalStatus__c"
                    Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                        ddlApproval.DataSource = dt
                        ddlApproval.DataTextField = "Value"
                        ddlApproval.DataValueField = "Value"
                        ddlApproval.DataBind()
                    End If
                    Dim lblApproveStatus As Label = DirectCast(e.Item.FindControl("lblApproveStatus"), Label)
                    If lblApproveStatus.Text <> "" Then
                        ddlApproval.ClearSelection()
                        SetComboValue(ddlApproval, lblApproveStatus.Text)
                    ElseIf ddlApproval.SelectedValue <> "" Then
                        ddlApproval.ClearSelection()
                        SetComboValue(ddlApproval, lblApproveStatus.Text)
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Click on Approved All all rows dropdown select Approved Status 
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub btnApprove_Click(sender As Object, e As System.EventArgs) Handles btnApprove.Click
            Try
                btnSubmit.Visible = True
                ''If dtDataMaintained IsNot Nothing AndAlso dtDataMaintained.Rows.Count > 0 Then
                ''    dtDataMaintained.Columns.Remove("firmapprovalstatus")
                ''    dtDataMaintained.Columns.Add(New DataColumn() With {.ColumnName = "firmapprovalstatus",
                ''                                          .DataType = GetType(String),
                ''                                          .DefaultValue = "Approved"})
                ''    dtDataMaintained.AcceptChanges()
                ''    Session("dtDataMaintained") = dtDataMaintained
                ''    radGrdFirmApproval.Rebind()
                ''End If
                For Each row As Telerik.Web.UI.GridItem In radGrdFirmApproval.Items
                    Dim ddlApproval As DropDownList = TryCast(row.FindControl("ddlApproval"), DropDownList)
                    ddlApproval.ClearSelection()
                    ddlApproval.SelectedValue = "Approved"
                Next

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub SetComboValue(ByRef cmb As System.Web.UI.WebControls.DropDownList, _
                                  ByVal sValue As String)
            Dim i As Integer

            Try
                For i = 0 To cmb.Items.Count - 1
                    If String.Compare(cmb.Items(i).Value, sValue, True) = 0 Then
                        cmb.Items(i).Selected = True
                        Exit Sub
                    End If
                    '11/27/07,Added by Tamasa,Issue 5222.
                    If String.Compare(cmb.Items(i).Text, sValue, True) = 0 Then
                        cmb.Items(i).Selected = True
                        Exit Sub
                    End If
                    'End
                Next
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
            Try
                Dim sApprovalStatus As String = String.Empty
                For Each row As Telerik.Web.UI.GridItem In radGrdFirmApproval.Items
                    Dim ddlApproval As DropDownList = TryCast(row.FindControl("ddlApproval"), DropDownList)
                    sApprovalStatus = Convert.ToString(ddlApproval.SelectedValue)
                    If sApprovalStatus <> "" Then
                        Exit For
                    End If
                Next
                If sApprovalStatus <> "" Then
                    For Each row As Telerik.Web.UI.GridItem In radGrdFirmApproval.Items
                        Dim ddlApproval As DropDownList = TryCast(row.FindControl("ddlApproval"), DropDownList)
                        Dim lblExamptionID As Label = TryCast(row.FindControl("lblExamptionID"), Label)
                        Dim lblPersonID As Label = TryCast(row.FindControl("lblPersonID"), Label)
                        doSave(Convert.ToInt32(lblExamptionID.Text), ddlApproval.SelectedValue, Convert.ToInt32(lblPersonID.Text))
                    Next
                End If
                ''If Not dtDataMaintained Is Nothing AndAlso dtDataMaintained.Rows.Count > 0 Then

                ''    For Each drDataMaintained As DataRow In dtDataMaintained.Rows
                ''        sApprovalStatus = Convert.ToString(drDataMaintained("firmapprovalstatus"))
                ''        If sApprovalStatus <> "" Then
                ''            Exit For
                ''        End If
                ''    Next
                ''    If sApprovalStatus <> "" Then
                ''        For Each drDataMaintained As DataRow In dtDataMaintained.Rows
                ''            doSave(Convert.ToInt32(drDataMaintained("ID")), Convert.ToString(drDataMaintained("firmapprovalstatus")), Convert.ToInt32(drDataMaintained("PersonID")))
                ''        Next

                ''    End If

                ''End If
                Response.Redirect(HttpContext.Current.Request.Url.ToString(), False)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' save ExemptionApplication__c firsm status and Person Company iD
        ''' </summary>
        ''' <param name="ExamptionID"></param>
        ''' <param name="FirmApprovalStatus"></param>
        ''' <param name="personID"></param>
        ''' <remarks></remarks>
        Private Sub doSave(ByVal ExamptionID As Long, ByVal FirmApprovalStatus As String, ByVal personID As Long)
            Try
                Dim oExamptionGE As AptifyGenericEntityBase = AptifyApplication.GetEntityObject("ExemptionApplication__c", ExamptionID)
                oExamptionGE.SetValue("FirmApprovalStatus", FirmApprovalStatus)
                If FirmApprovalStatus.Trim.ToLower = "rejected" Then
                    oExamptionGE.SetValue("CompanyID", -1)
                End If
                If Convert.ToInt32(oExamptionGE.GetValue("StatusID")) = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("ExemptionApplicationStatus__c", "Change Request")) Then
                    If FirmApprovalStatus.Trim.ToLower = "approved" Then
                        oExamptionGE.SetValue("StatusID", AptifyApplication.GetEntityRecordIDFromRecordName("ExemptionApplicationStatus__c", "Approved"))
                    End If
                End If
               
                Dim sError As String = String.Empty
                If oExamptionGE.Save(False, sError) Then
                    lblSuccessMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusinessExamptionFirm.SuccessMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    lblErrorMsg.Text = ""
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub radGrdFirmApproval_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles radGrdFirmApproval.NeedDataSource
            Try
                radGrdFirmApproval.DataSource = dtDataMaintained
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''Public Sub ddlApproval_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        ''    Try
        ''        Dim bApproval As Boolean = False
        ''        Dim ddlRadApproval As DropDownList = DirectCast(sender, DropDownList)
        ''        Dim item As GridDataItem = DirectCast(ddlRadApproval.NamingContainer, GridDataItem)
        ''        Dim lnlExamptionID As Label = DirectCast(item.FindControl("lblExamptionID"), Label)
        ''        If dtDataMaintained.Select("ID=" & lnlExamptionID.Text).Length = 1 Then
        ''            dtDataMaintained.Select("ID=" & lnlExamptionID.Text)(0)("firmapprovalstatus") = ddlRadApproval.SelectedValue
        ''            dtDataMaintained.AcceptChanges()
        ''            Session("dtDataMaintained") = dtDataMaintained
        ''        End If
        ''        If dtDataMaintained.Select("FirmApprovalStatus='Approved'").Length > 0 OrElse dtDataMaintained.Select("FirmApprovalStatus='Rejected'").Length > 0 Then
        ''            bApproval = True
        ''        Else
        ''            bApproval = False
        ''        End If
        ''        If bApproval = True Then
        ''            btnSubmit.Visible = True
        ''        Else
        ''            btnSubmit.Visible = False
        ''        End If

        ''    Catch ex As Exception
        ''        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        ''    End Try
        ''End Sub

    End Class
End Namespace
