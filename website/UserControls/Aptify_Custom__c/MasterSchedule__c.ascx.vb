''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer            Date Created/Modified               Summary
'Kavita Zinage        05/20/2015                      Master Schedule page
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Strict On
Option Explicit On

Imports System.Data
Imports Aptify.Framework.Web.eBusiness
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Telerik.Web.UI
Imports Aptify.Framework.Application


Namespace Aptify.Framework.Web.eBusiness.Education
    Partial Class MasterSchedule
        Inherits BaseUserControlAdvanced
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LOGINPAGE As String = "LoginPage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_MASTERSCHEDULEPAGE As String = "MasterSchedulePage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_MASTERSCHEDULEPAGEDETAILS As String = "MasterScheduleDetailsPage"

        Protected Const ATTRIBUTE_FIRMCONTRACTREGISTRATION_URL As String = "FirmContractRegistrationPage"
#Region "Property Setting"
        Public Overridable Property LoginPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_LOGINPAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_LOGINPAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_LOGINPAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Public Overridable Property MasterSchedulePage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_MASTERSCHEDULEPAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_MASTERSCHEDULEPAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_MASTERSCHEDULEPAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(LoginPage) Then
                LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_LOGINPAGE)
            End If
            If String.IsNullOrEmpty(MasterSchedulePage) Then
                MasterSchedulePage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_MASTERSCHEDULEPAGE)
            End If


            If String.IsNullOrEmpty(MasterSchedulePageDetails) Then
                MasterSchedulePageDetails = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_MASTERSCHEDULEPAGEDETAILS)
            End If
            If String.IsNullOrEmpty(FirmContractRegistrationPage) Then
                FirmContractRegistrationPage = Me.GetLinkValueFromXML(ATTRIBUTE_FIRMCONTRACTREGISTRATION_URL)
            End If

        End Sub


        Public Overridable Property MasterSchedulePageDetails() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_MASTERSCHEDULEPAGEDETAILS) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_MASTERSCHEDULEPAGEDETAILS))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_MASTERSCHEDULEPAGEDETAILS) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property


        Public Overridable Property FirmContractRegistrationPage() As String
            Get
                If Not ViewState(ATTRIBUTE_FIRMCONTRACTREGISTRATION_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_FIRMCONTRACTREGISTRATION_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_FIRMCONTRACTREGISTRATION_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
#End Region
#Region "Page Events"
        ''' <summary>
        ''' Handles page load event
        ''' </summary>
        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            Try
                SetProperties()


                If Request.QueryString("PageName") IsNot Nothing Then
                    btnBack.Visible = True
                End If

                If Not IsPostBack Then
                    LoadTrainingManagerList()
                    LoadGrid()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region
#Region "Private Functions"
        ''' <summary>
        ''' Load Master Schedule Grid
        ''' </summary>
        Public Sub LoadGrid()
            Try
                Dim sSql As String = Database & "..spGetMasterScheduleDetails__c @CompanyID=" & User1.CompanyID
                Dim dt As DataTable = DataAction.GetDataTable(sSql, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    ''Added By Pradip 
                    Dim dcolUrl As DataColumn = New DataColumn()
                    dcolUrl.Caption = "DataNavigateUrl"
                    dcolUrl.ColumnName = "DataNavigateUrl"
                    dt.Columns.Add(dcolUrl)
                    For Each rw As DataRow In dt.Rows
                        Dim navigate As String = MasterSchedulePageDetails & "?MasterID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(rw("ID").ToString()))
                        rw("DataNavigateUrl") = navigate
                    Next
                    ViewState("MasterScheduleDetails") = dt
                    Me.grdMasterSchedule.DataSource = dt
                    Me.grdMasterSchedule.DataBind()
                    Me.grdMasterSchedule.Visible = True
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Load Training Manager dropdown
        ''' </summary>
        Private Sub LoadTrainingManagerList()
            Try
                Dim sSql As String = Database & "..spGetAllTrainingManagers__c @CompanyID=" & User1.CompanyID
                Dim dt As DataTable = DataAction.GetDataTable(sSql, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    ViewState("ManagerDT") = dt
                Else
                    ViewState("ManagerDT") = Nothing
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

        ''' <summary>
        ''' Handles Display button click
        ''' </summary>
        Protected Sub btnAddNewMasterSchedule_Click(sender As Object, e As System.EventArgs) Handles btnAddNewMasterSchedule.Click
            Try
                radWindowConfirmation.VisibleOnPageLoad = True
                btnYes.Visible = True
                btnCancel.Visible = True
                btnOK.Visible = False
                lblConfirmationMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.MasterSchedulePage.ConfirmMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                'Dim oMasterSchedule As AptifyGenericEntityBase
                'oMasterSchedule = AptifyApplication.GetEntityObject("MasterSchedules__c", -1)
                'oMasterSchedule.SetValue("Status", "New")
                'oMasterSchedule.SetValue("FirmID", User1.CompanyID)
                'oMasterSchedule.Save()
                'LoadGrid()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Handles grid Data Bound
        ''' </summary>
        Protected Sub grdMasterSchedule_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdMasterSchedule.DataBound
            Try
                For Each item As Telerik.Web.UI.GridDataItem In grdMasterSchedule.MasterTableView.Items

                    Dim cmbTrainingManager As DropDownList = DirectCast(item.FindControl("cmbTrainingManager"), DropDownList)

                    If Not ViewState("ManagerDT") Is Nothing Then
                        cmbTrainingManager.ClearSelection()
                        cmbTrainingManager.DataSource = CType(ViewState("ManagerDT"), DataTable)
                        cmbTrainingManager.DataTextField = "FirstLast"
                        cmbTrainingManager.DataValueField = "ID"
                        cmbTrainingManager.DataBind()
                        Dim dtManagerdetails As DataTable = CType(ViewState("MasterScheduleDetails"), DataTable)

                        If dtManagerdetails.Rows(item.ItemIndex + (grdMasterSchedule.CurrentPageIndex * grdMasterSchedule.PageSize)).Item("TrainingManagerID").ToString().Trim <> "" Then
                            cmbTrainingManager.SelectedValue = dtManagerdetails.Rows(item.ItemIndex + (grdMasterSchedule.CurrentPageIndex * grdMasterSchedule.PageSize)).Item("TrainingManagerID").ToString()
                        End If
 If dtManagerdetails.Rows(item.ItemIndex + (grdMasterSchedule.CurrentPageIndex * grdMasterSchedule.PageSize)).Item("Status").ToString().ToUpper = "APPROVED" Then
                            cmbTrainingManager.Enabled = False
                        End If
                    End If
                    cmbTrainingManager.Items.Insert(0, New ListItem("Select", "0").ToString)
                Next
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Handles dropdown select changed
        ''' </summary>

        Protected Sub cmbTrainingManager_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
            Try
                Dim TrainingManagerID As DropDownList = CType(sender, DropDownList)
                Dim rowGridItem As GridDataItem
                rowGridItem = CType(TrainingManagerID.Parent.Parent, GridDataItem)
                Dim iIndex As Integer = rowGridItem.DataSetIndex
                'Dim lnkID As HyperLink = CType(grdMasterSchedule.Items(iIndex).FindControl("lnkID"), HyperLink) 'Commented by Kavita

                'Added by Kavita
                Dim iTrainingManagerID As Integer
                Dim dtManagerdetails As DataTable = CType(ViewState("MasterScheduleDetails"), DataTable)
                Dim iMasterScheduleID As Integer = Convert.ToInt32(dtManagerdetails.Rows(iIndex).Item("ID"))

                If TrainingManagerID.SelectedValue = "Select" Then
                    iTrainingManagerID = -1
                Else
                    iTrainingManagerID = Convert.ToInt32(TrainingManagerID.SelectedValue)
                End If

                Dim oMasterSchedule As AptifyGenericEntityBase
                'oMasterSchedule = AptifyApplication.GetEntityObject("MasterSchedules__c", CLng(lnkID.Text)) 'Commneted by Kavita
                oMasterSchedule = AptifyApplication.GetEntityObject("MasterSchedules__c", iMasterScheduleID)
                oMasterSchedule.SetValue("TrainingManagerID", iTrainingManagerID)
                oMasterSchedule.Save()

                radWindowConfirmation.VisibleOnPageLoad = True
                lblConfirmationMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.MasterSchedulePage.SuccessMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                btnOK.Visible = True
                btnYes.Visible = False
                btnCancel.Visible = False
                grdMasterSchedule.Rebind() 'Added by Kavita
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
     
        ''' <summary>
        ''' Handles need data source to load/reload grid 
        ''' </summary>
        Protected Sub grdMasterSchedule_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdMasterSchedule.NeedDataSource
            Try
                'Commented by kavita
                'If ViewState("MasterScheduleDetails") IsNot Nothing Then
                '    grdMasterSchedule.DataSource = CType(ViewState("MasterScheduleDetails"), DataTable)
                'End If
                LoadGrid()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
            Response.Redirect(FirmContractRegistrationPage)
        End Sub

        Protected Sub btnYes_Click(sender As Object, e As System.EventArgs) Handles btnYes.Click
            Try
                radWindowConfirmation.VisibleOnPageLoad = False
                lblConfirmationMsg.Text = ""

                Dim oMasterSchedule As AptifyGenericEntityBase
                oMasterSchedule = AptifyApplication.GetEntityObject("MasterSchedules__c", -1)
                oMasterSchedule.SetValue("Status", "New")
                oMasterSchedule.SetValue("FirmID", User1.CompanyID)
                oMasterSchedule.Save()
                LoadGrid()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnCancel_Click(sender As Object, e As System.EventArgs) Handles btnCancel.Click
            Try
                radWindowConfirmation.VisibleOnPageLoad = False
                lblConfirmationMsg.Text = ""
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Sub

        Protected Sub btnOK_Click(sender As Object, e As System.EventArgs) Handles btnOK.Click
            Try
                radWindowConfirmation.VisibleOnPageLoad = False
                lblConfirmationMsg.Text = ""
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
    End Class
End Namespace


