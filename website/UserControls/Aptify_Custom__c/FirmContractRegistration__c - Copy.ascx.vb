'Aptify e-Business 5.5.1, July 2013
Option Explicit On
Option Strict On
Imports Telerik.Web.UI
Imports System.Data
Imports Aptify.Framework.BusinessLogic.GenericEntity

Namespace Aptify.Framework.Web.eBusiness.Education
    Partial Class FirmContractRegistration__c
        Inherits eBusiness.BaseUserControlAdvanced
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_FIRMCON As String = "FirmContractRegistrationPage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_MASTERSCHEDULE As String = "MasterSchedulePage"
        Protected Const ATTRIBUTE_DATATABLE_STUDENT As String = "dtStudent"
        Private _frDetails As New DataTable()
#Region "Specific Properties"
        Public Overridable Property LoginPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Public Overridable Property FirmContractRegistrationPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_FIRMCON) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_FIRMCON))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_FIRMCON) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Public Overridable Property MasterSchedulePage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_MASTERSCHEDULE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_MASTERSCHEDULE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_MASTERSCHEDULE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(LoginPage) Then
                LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage)
            End If
            If String.IsNullOrEmpty(FirmContractRegistrationPage) Then
                FirmContractRegistrationPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_FIRMCON)
            End If
            If String.IsNullOrEmpty(MasterSchedulePage) Then
                MasterSchedulePage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_MASTERSCHEDULE)
                Dim sPageName As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt("firmcontractpage")
                Me.lnkMasterSchedule.NavigateUrl = MasterSchedulePage + "?PageName=" & System.Web.HttpUtility.UrlEncode(sPageName)
            End If
        End Sub
#End Region

        Protected Overridable Sub OnPageLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            SetProperties()
            If User1.PersonID <= 0 Then
                Session("ReturnToPage") = Request.RawUrl
                Response.Redirect(LoginPage)
            End If
            If Not IsPostBack Then
                LoadDropDown()
                grdStudentDetails.Rebind()
            End If
        End Sub

        Private Sub LoadDropDown()
            Try
                Dim sSql As String = Database & "..spGetAllSubsidiariesWithCity__c @ParentCompanyId=" & User1.CompanyID
                Dim dtCompany As DataTable = DataAction.GetDataTable(sSql, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dtCompany Is Nothing AndAlso dtCompany.Rows.Count > 0 Then
                    cmbOfficeLocation.ClearSelection()
                    cmbOfficeLocation.DataSource = dtCompany
                    'cmbOfficeLocation.DataTextField = "Name"
                    cmbOfficeLocation.DataTextField = "City"
                    cmbOfficeLocation.DataValueField = "ID"
                    cmbOfficeLocation.DataBind()
                    cmbOfficeLocation.Items.Insert(0, "Select")
                    ViewState("SubsidariesCompanyDT") = dtCompany
                Else
                    ViewState("SubsidariesCompanyDT") = Nothing
                End If
                sSql = Database & "..spGetNewMasterScheduleDetails__c @CompanyID=" & User1.CompanyID
                Dim dtMasterSchedule As DataTable = DataAction.GetDataTable(sSql, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dtMasterSchedule Is Nothing AndAlso dtMasterSchedule.Rows.Count > 0 Then
                    cmbMasterSchedule.ClearSelection()
                    cmbMasterSchedule.DataSource = dtMasterSchedule
                    cmbMasterSchedule.DataTextField = "ID"
                    cmbMasterSchedule.DataValueField = "ID"
                    cmbMasterSchedule.DataBind()
                    ViewState("MasterSchedule") = dtMasterSchedule
                Else
                    ViewState("MasterSchedule") = Nothing
                End If
                cmbMasterSchedule.Items.Insert(0, "Select")
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Overridable Sub LoadGrid()
            Dim sSQL As String
            Try
                If chkActive.Checked Then
                    sSQL = Database & "..spFirmActiveContractRegistrationDetails__c @ParentCompanyId=" & User1.CompanyID
                Else
                    sSQL = Database & "..spFirmContractRegistrationDetails__c @ParentCompanyId=" & User1.CompanyID
                End If
                _frDetails = DataAction.GetDataTable(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                If _frDetails.Rows.Count > 0 Then
                    Me.grdStudentDetails.DataSource = _frDetails
                    divDetails.Visible = True
                    btnSave.Visible = True
                Else
                    lblError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.FirmContractPage.NoRecordErrorMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    lblError.Visible = True
                    divDetails.Visible = False
                    btnSave.Visible = False
                End If
                ViewState(ATTRIBUTE_DATATABLE_STUDENT) = _frDetails
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Function validateBeforeSet() As Boolean
            Dim CheckCount As Integer = 0
            lblError.Text = ""
            lblError.Visible = False
            Try

                If txtStartDate.SelectedDate Is Nothing And cmbOfficeLocation.SelectedIndex = 0 And cmbMasterSchedule.SelectedIndex = 0 Then
                    lblError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.FirmContractPage.ValidationErrorMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    lblError.Visible = True
                    Return False
                End If

                For i As Integer = 0 To grdStudentDetails.Items.Count - 1
                    If CType(grdStudentDetails.Items(i).FindControl("chkStudent"), CheckBox).Checked = True Then
                        CheckCount = CheckCount + 1
                        Return True
                    End If
                Next
                If CheckCount = 0 Then
                    lblError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.FirmContractPage.RecordCheckErrorMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    lblError.Visible = True
                    Return False
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
            Return True
        End Function




        Function validateBeforeSave() As Boolean
            Dim CheckCount As Integer = 0
            lblError.Text = ""
            lblError.Visible = False
            Try
                For i As Integer = 0 To grdStudentDetails.Items.Count - 1
                    If CType(grdStudentDetails.Items(i).FindControl("chkStudent"), CheckBox).Checked = True Then
                        Dim txtGvStartDateGV As RadDatePicker = DirectCast(grdStudentDetails.Items(i).FindControl("txtGvStartDate"), RadDatePicker)
                        Dim lblTypegv As Label = DirectCast(grdStudentDetails.Items(i).FindControl("lblType"), Label)
                        Dim cmbTrainOffice As DropDownList = DirectCast(grdStudentDetails.Items(i).FindControl("cmbTrainOffice"), DropDownList)
                        Dim cmbMasterSchedulegv As DropDownList = DirectCast(grdStudentDetails.Items(i).FindControl("cmbGvMasterSchedule"), DropDownList)
                        If txtGvStartDateGV.SelectedDate Is Nothing Then
                            lblError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.FirmContractPage.StartDateValidationErrorMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) + " for selected record in Grid"
                            lblError.Visible = True
                            Return False
                        End If
                        If txtGvStartDateGV.SelectedDate Is Nothing And cmbTrainOffice.SelectedIndex = 0 And cmbMasterSchedulegv.SelectedIndex = 0 Then
                            lblError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.FirmContractPage.ValidationErrorMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) + " for selected record in Grid"
                            lblError.Visible = True
                            Return False
                        End If
                        CheckCount = CheckCount + 1
                        Return True
                    End If
                Next
                If CheckCount = 0 Then
                    lblError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.FirmContractPage.RecordCheckErrorMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    lblError.Visible = True
                    Return False
                End If

                Return True
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
            Return True
        End Function

#Region "Control Events"
        Protected Sub gvCourseDetails_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdStudentDetails.NeedDataSource
            LoadGrid()
            If Not _frDetails Is Nothing Then
                grdStudentDetails.DataSource = _frDetails
            End If
        End Sub
        Protected Sub grdStudentDetails_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdStudentDetails.DataBound
            For Each item As Telerik.Web.UI.GridDataItem In grdStudentDetails.MasterTableView.Items
                Dim cmbTrainOffice As DropDownList = DirectCast(item.FindControl("cmbTrainOffice"), DropDownList)
                Dim cmbMasterSchedulegv As DropDownList = DirectCast(item.FindControl("cmbGvMasterSchedule"), DropDownList)
                Dim lblStatusgv As Label = DirectCast(item.FindControl("lblStatus"), Label)
                Dim hidCompanygv As HiddenField = DirectCast(item.FindControl("hidCompany"), HiddenField)
                Dim lblMasterSchedulegv As Label = DirectCast(item.FindControl("lblMasterSchedule"), Label)
                Dim txtGvStartDateGV As RadDatePicker = DirectCast(item.FindControl("txtGvStartDate"), RadDatePicker)

                Dim hidECID As HiddenField = DirectCast(item.FindControl("hidECID"), HiddenField)

                Dim chkStudentgv As CheckBox = DirectCast(item.FindControl("chkStudent"), CheckBox)
                If Not ViewState("SubsidariesCompanyDT") Is Nothing Then
                    cmbTrainOffice.ClearSelection()
                    cmbTrainOffice.DataSource = CType(ViewState("SubsidariesCompanyDT"), DataTable)
                    cmbTrainOffice.DataTextField = "City"
                    cmbTrainOffice.DataValueField = "ID"
                    cmbTrainOffice.DataBind()
                    cmbTrainOffice.Items.Insert(0, "Select")
                    cmbTrainOffice.SelectedValue = hidCompanygv.Value
                Else
                    cmbTrainOffice.Items.Insert(0, "Select")
                End If
                If Not ViewState("MasterSchedule") Is Nothing Then
                    cmbMasterSchedulegv.ClearSelection()
                    cmbMasterSchedulegv.DataSource = CType(ViewState("MasterSchedule"), DataTable)
                    cmbMasterSchedulegv.DataTextField = "ID"
                    cmbMasterSchedulegv.DataValueField = "ID"
                    cmbMasterSchedulegv.DataBind()
                    cmbMasterSchedulegv.Items.Insert(0, "Select")
                Else
                    cmbMasterSchedulegv.Items.Insert(0, "Select")
                End If
                If (lblStatusgv.Text.ToUpper = "SIGNED BY STUDENT") And txtGvStartDateGV.SelectedDate IsNot Nothing Then
                    cmbMasterSchedulegv.Visible = True
                    lblMasterSchedulegv.Visible = False
                End If

                If lblMasterSchedulegv.Text.Trim() <> "" Then
                    cmbMasterSchedulegv.SelectedValue = lblMasterSchedulegv.Text
                End If

                If lblStatusgv.Text.ToUpper = "SIGNED BY STUDENT" Or lblStatusgv.Text.ToUpper = "NEW" Or lblStatusgv.Text.ToUpper.Trim = "" Then
                    chkStudentgv.Enabled = True
                Else
                    chkStudentgv.Enabled = False
                    cmbMasterSchedulegv.Enabled = False
                    cmbTrainOffice.Enabled = False
                    txtGvStartDateGV.Enabled = False
                End If

                If CInt(hidECID.Value) > 0 AndAlso (txtGvStartDateGV.SelectedDate IsNot Nothing Or (lblStatusgv.Text.ToUpper.Trim <> "NEW" And lblStatusgv.Text.Trim <> "")) Then
                    txtGvStartDateGV.Enabled = False
                End If


            Next
        End Sub

        Protected Sub chkActive_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkActive.CheckedChanged
            grdStudentDetails.Rebind()
        End Sub

        Protected Sub btnSet_Click(sender As Object, e As System.EventArgs) Handles btnSet.Click
            Dim d1 As DateTime
            If validateBeforeSet() Then
                For i As Integer = 0 To grdStudentDetails.Items.Count - 1
                    If CType(grdStudentDetails.Items(i).FindControl("chkStudent"), CheckBox).Checked = True Then
                        Dim txtGvStartDateGV As RadDatePicker = DirectCast(grdStudentDetails.Items(i).FindControl("txtGvStartDate"), RadDatePicker)

                        Dim lblyrsOfExp As Label = CType(grdStudentDetails.Items(i).FindControl("lblyrsOfExp"), Label)
                        Dim lblContractExpireDate As Label = CType(grdStudentDetails.Items(i).FindControl("lblContractExpireDate"), Label)
                        Dim lblTypegv As Label = DirectCast(grdStudentDetails.Items(i).FindControl("lblType"), Label)
                        Dim hidAttributeValuegv As HiddenField = DirectCast(grdStudentDetails.Items(i).FindControl("hidAttributeValue"), HiddenField)
                        Dim hidECID As HiddenField = DirectCast(grdStudentDetails.Items(i).FindControl("hidECID"), HiddenField)

                        If txtStartDate.SelectedDate IsNot Nothing Then
                            If CInt(hidECID.Value) <= 0 Then
                                txtGvStartDateGV.SelectedDate = txtStartDate.SelectedDate

                                If lblTypegv.Text.ToLower = "flexible option" And txtGvStartDateGV.SelectedDate IsNot Nothing Then
                                    Dim months As Integer = Convert.ToInt32(Convert.ToDecimal(hidAttributeValuegv.Value.ToString()) * 12)
                                    d1 = Convert.ToDateTime(txtGvStartDateGV.SelectedDate).AddMonths(months)
                                    d1 = d1.AddDays(-1)
                                    lblContractExpireDate.Text = New Date(d1.Year, d1.Month, d1.Day).ToShortDateString()
                                Else
                                    If lblyrsOfExp.Text <> "" And txtGvStartDateGV.SelectedDate IsNot Nothing Then
                                        Dim months As Integer = Convert.ToInt32(Convert.ToDecimal(lblyrsOfExp.Text.ToString()) * 12)
                                        d1 = Convert.ToDateTime(txtGvStartDateGV.SelectedDate).AddMonths(months)
                                        d1 = d1.AddDays(-1)
                                        lblContractExpireDate.Text = New Date(d1.Year, d1.Month, d1.Day).ToShortDateString()
                                    End If
                                End If
                            End If
                            'If lblTypegv.Text.ToLower = "flexible option" And txtGvStartDateGV.SelectedDate IsNot Nothing Then
                            '    d1 = Convert.ToDateTime(txtGvStartDateGV.SelectedDate).AddYears(CInt(hidAttributeValuegv.Value))
                            '    lblContractExpireDate.Text = New Date(d1.Year, d1.Month, d1.Day).ToShortDateString()
                            'Else
                            '    If lblyrsOfExp.Text <> "" And txtGvStartDateGV.SelectedDate IsNot Nothing Then
                            '        d1 = Convert.ToDateTime(txtGvStartDateGV.SelectedDate).AddYears(CInt(lblyrsOfExp.Text))
                            '        lblContractExpireDate.Text = New Date(d1.Year, d1.Month, d1.Day).ToShortDateString()
                            '    End If
                            'End If
                        End If
                        If cmbOfficeLocation.SelectedIndex <> 0 Then
                            Dim cmbTrainOffice As DropDownList = DirectCast(grdStudentDetails.Items(i).FindControl("cmbTrainOffice"), DropDownList)
                            cmbTrainOffice.SelectedValue = cmbOfficeLocation.SelectedValue
                        End If
                        If cmbMasterSchedule.SelectedIndex <> 0 Then
                            Dim cmbMasterSchedulegv As DropDownList = DirectCast(grdStudentDetails.Items(i).FindControl("cmbGvMasterSchedule"), DropDownList)
                            cmbMasterSchedulegv.SelectedValue = cmbMasterSchedule.SelectedValue
                        End If

                    End If

                Next
                lblWarning.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.FirmContractPage.SetMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                radMockTrial.VisibleOnPageLoad = True
                btnOkSet.Visible = True
                btnOk.Visible = False

                txtStartDate.SelectedDate = Nothing
                cmbMasterSchedule.SelectedIndex = 0
                cmbOfficeLocation.SelectedIndex = 0

            End If
        End Sub

        Protected Sub ToggleSelectedState(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Dim headerCheckBox As CheckBox = CType(sender, CheckBox)
                For Each dataItem As GridDataItem In grdStudentDetails.MasterTableView.Items
                    If CType(dataItem.FindControl("chkStudent"), CheckBox).Enabled = True Then
                        CType(dataItem.FindControl("chkStudent"), CheckBox).Checked = headerCheckBox.Checked
                        dataItem.Selected = headerCheckBox.Checked
                    End If
                Next
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Sub

        Protected Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
            Try

                If validateBeforeSave() Then
                    For i As Integer = 0 To grdStudentDetails.Items.Count - 1
                        If DirectCast(grdStudentDetails.Items(i).FindControl("chkStudent"), CheckBox).Checked = True Then
                            Dim sTransID As String = DataAction.BeginTransaction(IsolationLevel.Serializable, True)
                            Dim hidECIDgv As HiddenField = DirectCast(grdStudentDetails.Items(i).FindControl("hidECID"), HiddenField)
                            Dim hidAttributeValuegv As HiddenField = DirectCast(grdStudentDetails.Items(i).FindControl("hidAttributeValue"), HiddenField)
                            Dim lblTypegv As Label = DirectCast(grdStudentDetails.Items(i).FindControl("lblType"), Label)
                            Dim lblyrsOfExpgv As Label = DirectCast(grdStudentDetails.Items(i).FindControl("lblyrsOfExp"), Label)
                            Dim cmbGvMasterSchedulegv As DropDownList = DirectCast(grdStudentDetails.Items(i).FindControl("cmbGvMasterSchedule"), DropDownList)
                            Dim cmbTrainOfficegv As DropDownList = DirectCast(grdStudentDetails.Items(i).FindControl("cmbTrainOffice"), DropDownList)
                            Dim idEducationCon As Long = CLng(hidECIDgv.Value)
                            Dim txtGvStartDateGV As RadDatePicker = DirectCast(grdStudentDetails.Items(i).FindControl("txtGvStartDate"), RadDatePicker)
                            'Dim lnkStudentNogv As HyperLink = DirectCast(grdStudentDetails.Items(i).FindControl("lnkStudentNo"), HyperLink)
                            Dim hidStudentNogv As HiddenField = DirectCast(grdStudentDetails.Items(i).FindControl("hidStudentNo"), HiddenField)
                            Dim lblStatusgv As Label = DirectCast(grdStudentDetails.Items(i).FindControl("lblStatus"), Label)


                            Dim lblContractExpireDate As Label = CType(grdStudentDetails.Items(i).FindControl("lblContractExpireDate"), Label)

                            Dim oCertGE As AptifyGenericEntityBase
                            oCertGE = Me.AptifyApplication.GetEntityObject("EducationContracts__c", idEducationCon)
                            If (idEducationCon < 0) Then
                                oCertGE.SetValue("Type", "Contract")
                                oCertGE.SetValue("RouteOfEntry", Convert.ToInt32(Me.AptifyApplication.GetEntityRecordIDFromRecordName("ApplicationTypes__c", "Contract")))
                                oCertGE.SetValue("Status", "New")
                            End If
                            oCertGE.SetValue("StudentID", Convert.ToInt32(hidStudentNogv.Value))
                            If txtGvStartDateGV.SelectedDate IsNot Nothing Then
                                oCertGE.SetValue("ContractStartDate", txtGvStartDateGV.SelectedDate)
                            Else
                                oCertGE.SetValue("ContractStartDate", Nothing)
                                oCertGE.SetValue("ContractExpireDate", Nothing)
                            End If
                            If lblContractExpireDate.Text.Trim <> "" Then
                                oCertGE.SetValue("ContractExpireDate", Convert.ToDateTime(lblContractExpireDate.Text.Trim()))
                            End If

                            'If lblTypegv.Text.ToLower = "flexible option" And txtGvStartDateGV.SelectedDate IsNot Nothing Then
                            '    oCertGE.SetValue("ContractExpireDate", Convert.ToDateTime(txtGvStartDateGV.SelectedDate).AddYears(CInt(hidAttributeValuegv.Value)))
                            'Else
                            '    If lblyrsOfExpgv.Text <> "" And txtGvStartDateGV.SelectedDate IsNot Nothing Then
                            '        oCertGE.SetValue("ContractExpireDate", Convert.ToDateTime(txtGvStartDateGV.SelectedDate).AddYears(CInt(lblyrsOfExpgv.Text)))
                            '    End If
                            'End If
                            If cmbGvMasterSchedulegv.SelectedIndex <> 0 And idEducationCon > 0 And lblStatusgv.Text.ToUpper = "SIGNED BY STUDENT" Then
                                oCertGE.SetValue("MasterSchedule", cmbGvMasterSchedulegv.SelectedValue)
                                oCertGE.SetValue("Status", "Assigned to a Master Schedule")
                                'Else
                                '    oCertGE.SetValue("MasterSchedule", Nothing)
                            End If

                            If cmbTrainOfficegv.SelectedIndex <> 0 Then
                                oCertGE.SetValue("CompanyID", cmbTrainOfficegv.SelectedValue)
                            Else
                                oCertGE.SetValue("CompanyID", Nothing)
                            End If

                            Dim _error As String = ""
                            Try
                                If oCertGE.Save(False, _error) Then
                                    DataAction.CommitTransaction(sTransID)
                                Else
                                    DataAction.RollbackTransaction(sTransID)
                                End If
                            Catch ex As Exception
                                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                                Continue For
                            End Try
                        End If
                    Next
                    lblWarning.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.FirmContractPage.SuccessMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    radMockTrial.VisibleOnPageLoad = True
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnOk_Click(sender As Object, e As System.EventArgs) Handles btnOk.Click
            Try
                radMockTrial.VisibleOnPageLoad = False
                Response.Redirect(FirmContractRegistrationPage, False)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub txtGvStartDate_OnSelectedDateChanged(ByVal sender As Object, ByVal e As System.EventArgs)
            Try
                Dim txtGvStartDateGV As RadDatePicker = CType(sender, RadDatePicker)
                Dim d1 As DateTime
                Dim rowGridItem As GridDataItem
                rowGridItem = CType(txtGvStartDateGV.Parent.Parent, GridDataItem)
                Dim iIndex As Integer = rowGridItem.DataSetIndex
                Dim lblyrsOfExp As Label = CType(grdStudentDetails.Items(iIndex).FindControl("lblyrsOfExp"), Label)
                Dim lblContractExpireDate As Label = CType(grdStudentDetails.Items(iIndex).FindControl("lblContractExpireDate"), Label)
                Dim lblTypegv As Label = DirectCast(grdStudentDetails.Items(iIndex).FindControl("lblType"), Label)
                Dim hidAttributeValuegv As HiddenField = DirectCast(grdStudentDetails.Items(iIndex).FindControl("hidAttributeValue"), HiddenField)
                lblContractExpireDate.Text = ""
                If lblTypegv.Text.ToLower = "flexible option" And txtGvStartDateGV.SelectedDate IsNot Nothing Then
                    Dim months As Integer = Convert.ToInt32(Convert.ToDecimal(hidAttributeValuegv.Value.ToString()) * 12)
                    d1 = Convert.ToDateTime(txtGvStartDateGV.SelectedDate).AddMonths(months)
                    d1 = d1.AddDays(-1)
                    lblContractExpireDate.Text = New Date(d1.Year, d1.Month, d1.Day).ToShortDateString()
                Else
                    If lblyrsOfExp.Text <> "" And txtGvStartDateGV.SelectedDate IsNot Nothing Then
                        Dim months As Integer = Convert.ToInt32(Convert.ToDecimal(lblyrsOfExp.Text.ToString()) * 12)
                        d1 = Convert.ToDateTime(txtGvStartDateGV.SelectedDate).AddMonths(months)
                        '---------------------------Substarct 1 day-----------------
                        d1 = d1.AddDays(-1)
                        lblContractExpireDate.Text = New Date(d1.Year, d1.Month, d1.Day).ToShortDateString()

                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub


#End Region


        Protected Sub btnOkSet_Click(sender As Object, e As System.EventArgs) Handles btnOkSet.Click
            Try
                btnOk.Visible = True
                btnOkSet.Visible = False
                radMockTrial.VisibleOnPageLoad = False
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
            grdStudentDetails.Columns(1).CurrentFilterFunction = Telerik.Web.UI.GridKnownFunction.EqualTo
            ' .Columns[0].CurrentFilterFunction = Telerik.Web.UI.GridKnownFunction.StartsWith
        End Sub
    End Class
End Namespace
