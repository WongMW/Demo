'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Govind Mande              11/19/2011                         create class record 
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Explicit On
Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness

Namespace Aptify.Framework.Web.eBusiness.Generated

    ''' <summary>
    ''' Generated ASP.NET User Control for the Cases entity.
    ''' Description: Tracks individual cases as they go through various stages of resolution from initial entry through final closure
    ''' </summary>
    ''' <remarks></remarks>
    Partial Class Cases__C
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "Cases"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_NAME As String = "RedirectURL"


        Public Overloads Property RedirectURL() As String
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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            SetProperties()
            If Not IsPostBack Then
                LoadRecord()
                'Call function for bind Case Categories
                BindCaseCategory()
                ' Call function for bind Case Type
                BindCaseType()
                ' Call function for bind Case Priorities
                ' BindCasePriorities()
                ' Call function for bind contact
                BindContact()
            End If
        End Sub
        Protected Overrides Sub SetProperties()
            Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            MyBase.SetProperties()
            If String.IsNullOrEmpty(RedirectURL) Then
                Me.RedirectURL = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_NAME)
            End If
        End Sub
        Protected Overridable Sub LoadRecord()
            Try
                If Me.SetControlRecordIDFromParam() Then
                    Me.RecordAttachments__c.Visible = True
                    Dim EntityId As Long
                    LoadDataFromGE(Me.AptifyApplication.GetEntityObject("Cases", ControlRecordID))
                    EntityId = CLng(Me.AptifyApplication.GetEntityID("cases"))
                    If Me.AptifyApplication.GetEntityObject("Cases", ControlRecordID).GetValue("CaseStatus").ToString().ToUpper() <> "REPORTED" Then
                        DisplayReadOnlyMode()
                    Else
                        trRecordAttachment.Visible = True
                        DisplayEditMode()
                    End If
                    Me.RecordAttachments__c.LoadAttachments(EntityId, Me.ControlRecordID)
                    If Me.AptifyApplication.GetEntityObject("Cases", ControlRecordID).GetValue("CaseStatus").ToString().ToUpper() <> "REPORTED" Then
                        If Me.RecordAttachments__c.FoundAttachment Then
                            trRecordAttachment.Visible = True
                        Else
                            trRecordAttachment.Visible = False
                        End If
                    End If
                  
                    lblUploadMsg.Text = ""

                Else
                    LoadDataFromGE(Me.AptifyApplication.GetEntityObject("Cases", -1))
                    DisplayEditMode()
                    lblDateRecorded.Text = System.DateTime.Today
                    ' lblStatus.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal("AptifyEbusiness.NewCaseRecordCaseStatus", "Reported", DataAction.UserCredentials)
                    lblStatus.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.NewCaseRecordCaseStatus")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)

                    trReadOnlytrDateRecorded.Visible = False
                    ' lblUploadMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal("AptifyEbusiness.NewCaseRecordAttachmentMessage", "You need to save the case record first then only you can upload the document against it.", DataAction.UserCredentials)
                    lblUploadMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.NewCaseRecordAttachmentMessage")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)

                End If
                trCategory.Visible = False
                trReadOnlyCategory.Visible = False
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub DisplayReadOnlyMode()
            Try
                trTitle.Visible = False
                trReadOnlyTitle.Visible = True
                trCategory.Visible = False
                trReadOnlyCategory.Visible = True
                trType.Visible = False
                trReadOnlyType.Visible = True
                ' trPriority.Visible = False
                ' trReadOnlyPriority.Visible = True
                trSummary.Visible = False
                trReadOnlySummary.Visible = True
                RecordAttachments__c.AllowAdd = False
                RecordAttachments__c.AllowDelete = False
                cmdSave.Visible = False
                'Added by Govind
                trUploadMsg.Visible = False
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub DisplayEditMode()
            Try
                trTitle.Visible = True
                trReadOnlyTitle.Visible = False
                trCategory.Visible = True
                trReadOnlyCategory.Visible = False
                trType.Visible = True
                trReadOnlyType.Visible = False
                'trPriority.Visible = True
                'trReadOnlyPriority.Visible = False
                trSummary.Visible = True
                trReadOnlySummary.Visible = False
                RecordAttachments__c.AllowAdd = True
                RecordAttachments__c.AllowDelete = True
                cmdSave.Visible = True
                'Added by Govind
                trUploadMsg.Visible = True
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Overridable Sub SaveRecord(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
            Dim oGE As AptifyGenericEntityBase
            Dim bRedirect As Boolean = False
            Try

                If Me.ControlRecordID > 0 Then
                    oGE = AptifyApplication.GetEntityObject("Cases", Me.ControlRecordID)
                Else
                    oGE = AptifyApplication.GetEntityObject("Cases", -1)
                    oGE.SetValue("CaseStatusID", 1)
                End If
                Me.TransferDataToGE(oGE)
                oGE.SetValue("ContactID", User1.PersonID)
                oGE.SetValue("Title", txtTitle.Text.Trim())
                oGE.SetValue("Summary", txtSummary.Text.Trim())
                Dim ManagerID As Long = AptifyApplication.GetEntityAttribute("Cases", "ManagerID")
                If ManagerID > 0 Then
                    oGE.SetValue("ManagerID", ManagerID)
                End If
                oGE.SetValue("RecordedByID", CLng(Me.AptifyApplication.UserCredentials.GetUserRelatedRecordID("Employees")))
                oGE.SetValue("CaseReportMethodID", 1)
                'Added by Govind
                oGE.SetValue("CaseCategoryID", cmbCaseCategoryID.SelectedValue)
                oGE.SetValue("CaseTypeID", cmbCaseTypeID.SelectedValue)
                '  oGE.SetValue("CasePriorityID", cmbCasePriorityID.SelectedValue)
                If oGE.Save(False) Then
                    bRedirect = True
                    lblError.Visible = True
                    '  lblError.Text = "Case is reported successfully."
                    'lblError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal("AptifyEbusiness.NewCaseRecorSaveMessage", "Case is reported successfully.", DataAction.UserCredentials)
                    lblError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.NewCaseRecorSaveMessage")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    trRecordAttachment.Visible = True
                    Dim EntityId As Long
                    EntityId = CLng(Me.AptifyApplication.GetEntityID("cases"))
                    Me.RecordAttachments__c.LoadAttachments(EntityId, oGE.RecordID)
                    lblUploadMsg.Text = ""
                Else
                    lblError.Visible = True
                    lblError.Text = oGE.LastError()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub cmdBack_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdBack.Click
            Response.Redirect(RedirectURL)
        End Sub
        'Code Added by Govind
#Region "Load Data"
        ''' <summary>
        ''' Bind Case Category 
        ''' </summary>
        ''' <remarks></remarks>
        Protected Sub BindCaseCategory()
            Try
                ' Get All Case Categories
                Dim sSQL As String = "..spGetCaseCategories__c"
                Dim dtCaseCategories As DataTable = DataAction.GetDataTable(sSQL)
                If Not dtCaseCategories Is Nothing AndAlso dtCaseCategories.Rows.Count > 0 Then
                    cmbCaseCategoryID.DataSource = dtCaseCategories
                    cmbCaseCategoryID.DataTextField = "Name"
                    cmbCaseCategoryID.DataValueField = "ID"
                    cmbCaseCategoryID.DataBind()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Bind Case Type
        ''' </summary>
        ''' <remarks></remarks>
        Protected Sub BindCaseType()
            Try
                Dim sSQLCaseType As String = "..spGetCaseTypes__c"    ' Get All Case Types
                Dim dtCaseType As DataTable = DataAction.GetDataTable(sSQLCaseType)
                If Not dtCaseType Is Nothing AndAlso dtCaseType.Rows.Count > 0 Then
                    cmbCaseTypeID.DataSource = dtCaseType
                    cmbCaseTypeID.DataTextField = "Name"
                    cmbCaseTypeID.DataValueField = "ID"
                    cmbCaseTypeID.DataBind()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Bind Case Priorities
        ''' </summary>
        ''' <remarks></remarks>
        'Protected Sub BindCasePriorities()
        '    Try
        '        Dim sSQLCasePriorities As String = "..spGetCasePriorities__c"    ' Get All Case Types
        '        Dim dtCasePriorities As DataTable = DataAction.GetDataTable(sSQLCasePriorities)
        '        If Not dtCasePriorities Is Nothing AndAlso dtCasePriorities.Rows.Count > 0 Then
        '            cmbCasePriorityID.DataSource = dtCasePriorities
        '            cmbCasePriorityID.DataTextField = "Name"
        '            cmbCasePriorityID.DataValueField = "ID"
        '            cmbCasePriorityID.DataBind()
        '        End If
        '    Catch ex As Exception
        '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        '    End Try
        'End Sub
        ''' <summary>
        ''' Bind Contact ID
        ''' </summary>
        ''' <remarks></remarks>
        Protected Sub BindContact()
            Try
                'cmbContactID
                Dim sSQLContactID As String = "..spGetContactID__c"    ' Get All Case Types
                Dim dtContactID As DataTable = DataAction.GetDataTable(sSQLContactID)
                If Not dtContactID Is Nothing AndAlso dtContactID.Rows.Count > 0 Then
                    cmbContactID.DataSource = dtContactID
                    cmbContactID.DataTextField = "NameWCompany"
                    cmbContactID.DataValueField = "ID"
                    cmbContactID.DataBind()
                End If
            Catch ex As Exception

            End Try
        End Sub
#End Region
    End Class
End Namespace
