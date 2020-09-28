''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer              Date Created/Modified               Summary
'Pradip Chavhan         06/29/2015                      Master Schedule Details Page for Training Manager.
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Imports System.Data
Imports Telerik.Web.UI
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports System.IO
Imports Aptify.Framework.Application
Namespace Aptify.Framework.Web.eBusiness.Education
    Partial Class MasterScheduleDetails__c
        Inherits eBusiness.BaseUserControlAdvanced

        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_MASTERSCHEDULEDETAILS As String = "MasterScheduleDetailsPage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_REPORT As String = "ReportPage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_MASTERSCHEDULE As String = "MasterSchedulePage"


        Dim sDeletedID As String = String.Empty
        Dim sSQL As String
#Region "Property Setting"
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

        Public Overridable Property MasterScheduleDetailsPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_MASTERSCHEDULEDETAILS) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_MASTERSCHEDULEDETAILS))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_MASTERSCHEDULEDETAILS) = Me.FixLinkForVirtualPath(value)
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

        Public Overridable Property ReportPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_REPORT) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_REPORT))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_REPORT) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property



        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(LoginPage) Then
                LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage)
            End If
            If String.IsNullOrEmpty(MasterScheduleDetailsPage) Then
                MasterScheduleDetailsPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_MASTERSCHEDULEDETAILS)
            End If
            If String.IsNullOrEmpty(ReportPage) Then
                ReportPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_REPORT)
            End If
            If String.IsNullOrEmpty(MasterSchedulePage) Then
                MasterSchedulePage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_MASTERSCHEDULE)
            End If
        End Sub

#End Region

#Region "Page Events"
        'FormalRegistrationElevationDeclaration__c
        Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
            ''For Dynamic Script loading By Shiwendra
            Dim js As HtmlGenericControl = New HtmlGenericControl("script")
            js.Attributes.Add("type", "text/javascript")
            js.Attributes.Add("src", ResolveUrl("~/Scripts/jquery-1.7.1.min.js"))
            Me.Page.Header.Controls.Add(js)
            Dim js1 As HtmlGenericControl = New HtmlGenericControl("script")
            js1.Attributes.Add("type", "text/javascript")
            js1.Attributes.Add("src", ResolveUrl("~/Scripts/expand.js"))
            Me.Page.Header.Controls.Add(js1)
            Dim css As HtmlGenericControl = New HtmlGenericControl("style")
            css.Attributes.Add("type", "text/css")
            css.Attributes.Add("src", ResolveUrl("~/CSS/StyleSheet.css"))
            Me.Page.Header.Controls.Add(css)
            Dim js2 As HtmlGenericControl = New HtmlGenericControl("script")
            js2.Attributes.Add("type", "text/javascript")
            js2.Attributes.Add("src", ResolveUrl("~/Scripts/jquery.min.js"))
            Me.Page.Header.Controls.Add(js2)
        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                SetProperties()
                If AptifyEbusinessUser1.PersonID <= 0 Then
                    Session("ReturnToPage") = Request.RawUrl
                    Response.Redirect(LoginPage)
                End If

                'If hdnPersonDetailsState.Value = "1" Then
                '    ' PersonDetails.removeClass("collapse").addClass("active");
                'End If
                lblMessage.Text = ""
                If Not IsPostBack Then
                    txtSubmitteddate.Text = New Date(Today.Year, Today.Month, Today.Day).ToShortDateString()
                    'Date.Now.ToString("dd/MM/yyyy")
                    lblGuidance.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.MasterScheduleDetails.PrintGuidance__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)

                    If Request.QueryString("MasterID") IsNot Nothing Then
                        pnlDetails.Visible = True
                        ' LoadGrid()
                        RecordAttachments__c.AllowAdd = True
                        txtMSNumber.Text = Convert.ToInt32(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("MasterID"))).ToString()
                        divDeclaration.InnerText = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyConsulting.MasterScheduleDetails.Declaration__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        
 divDeclaration2.InnerText = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyConsulting.MasterScheduleDetails.Declaration2__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)

sSQL = Database & "..spGetMasterScheduleDetailsByID__c"
                        Dim param(0) As IDataParameter
                        param(0) = DataAction.GetDataParameter("@MasterID", SqlDbType.Int, Convert.ToInt32(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("MasterID"))))
                        Dim dtMasterSchedule = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param)
                        If dtMasterSchedule.Rows.Count > 0 Then
                            txtFirmName.Text = dtMasterSchedule.Rows(0)("FirmName").ToString()
                            txtStatus.Text = dtMasterSchedule.Rows(0)("Status").ToString()
                            txtStatusReason.Text = dtMasterSchedule.Rows(0)("Description").ToString()
                            If txtStatusReason.Text.Trim() <> "" Then
                                pnlStatusReason.Visible = True
                            End If
                            txtTrainingManager.Text = dtMasterSchedule.Rows(0)("FirstLast").ToString()
                            Dim signeddate As DateTime = Convert.ToDateTime(dtMasterSchedule.Rows(0)("SignedDate").ToString())
                            txtSubmitteddate.Text = New Date(signeddate.Year, signeddate.Month, signeddate.Day).ToShortDateString()
                        End If

                        'Dim sReportName As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt("msreport")
                        'Dim strParentCompany As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(AptifyEbusinessUser1.CompanyID.ToString())
                        'Dim strFilePath As String = ReportPage & "?sMasterID=" & System.Web.HttpUtility.UrlEncode(Request.QueryString("MasterID").ToString()) & "&sReportName=" & System.Web.HttpUtility.UrlEncode(sReportName) & "&sParentCompanyID=" & System.Web.HttpUtility.UrlEncode(strParentCompany)
                        'hdnReportPath.Value = strFilePath

                        If txtStatus.Text.Trim().ToLower() = "new" Then
                            RecordAttachments__c.AllowAdd = True
                            'btnSubmit.Visible = True ' code commented for Redmine #20665
                        Else
                            RecordAttachments__c.AllowAdd = False
                            btnSubmit.Visible = False
                            chkTermsandconditions.Checked = True
                            chkTermsandconditions.Enabled = False
                            btnSubmit.Visible = True ' code added for Redmine #20665
                            btnSubmit.Focus() ' code added for Redmine #20665
                        End If
                        LoadGrid()
                        Me.RecordAttachments__c.LoadAttachments(AptifyApplication.GetEntityID("MasterSchedules__c"), CLng(txtMSNumber.Text.Trim()))
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

#Region "Private Functions"
        Private Sub LoadGrid()
            Dim dtContractDetails As Data.DataTable
            Try
                sSQL = Database & "..spContractRegistrationByMasterSchedule__c"
                Dim param(1) As IDataParameter
                param(0) = DataAction.GetDataParameter("@ParentCompanyId", SqlDbType.Int, AptifyEbusinessUser1.CompanyID)
                param(1) = DataAction.GetDataParameter("@MasterID", SqlDbType.Int, Convert.ToInt32(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("MasterID"))))
                dtContractDetails = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param)
                'Dim dcolUrl As DataColumn = New DataColumn()
                'dcolUrl.Caption = "DataNavigateUrl"
                'dcolUrl.ColumnName = "DataNavigateUrl"
                'dtContractDetails.Columns.Add(dcolUrl)
                lblMessage.Text = ""
                If dtContractDetails IsNot Nothing And dtContractDetails.Rows.Count > 0 Then
                    If dtContractDetails.Rows.Count = 1 And dtContractDetails.Rows(0)("StudentNo").ToString().Trim() = "" Then
                        lblEmptyMsg.Visible = True
                    Else
                        ViewState("ContractDetails") = dtContractDetails
                        grdContractDetails.DataSource = dtContractDetails
                        grdContractDetails.Rebind()
                        ' code commented by GM for Redmine #20645
                        'If txtStatus.Text.Trim.ToUpper = "NEW" Then
                        '    grdContractDetails.Columns(9).Visible = True
                        'Else
                        '    grdContractDetails.Columns(9).Visible = False
                        'End If
                        lblEmptyMsg.Visible = False
                    End If
                Else
                    lblEmptyMsg.Visible = True
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Function validateBeforeSubmit() As Boolean
            If chkTermsandconditions.Checked = False Then
                lblWarningTermandCon.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.MasterScheduleDetails.TermsAndConValidation__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                radMockTrialTermandCon.VisibleOnPageLoad = True
                chkTermsandconditions.Focus()
                Return False
            End If
            Return True
        End Function

#End Region

        Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
            Try
                If validateBeforeSubmit() Then
                    radMockTrialSubmit.VisibleOnPageLoad = True
                    lblWarningSubmit.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.MasterScheduleDetails.WarningMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnOk_Click(sender As Object, e As System.EventArgs) Handles btnOk.Click
            Try
                radMockTrial.VisibleOnPageLoad = False
                Response.Redirect(MasterScheduleDetailsPage & "?MasterID=" & System.Web.HttpUtility.UrlEncode(Request.QueryString("MasterID")), False)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnPrint_Click(sender As Object, e As System.EventArgs) Handles btnPrint.Click
            'ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & hdnReportPath.Value & "' )", True)
            Try
                Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "Master Schedule Detail Report"))
                Dim rptParam As New AptifyCrystalReport__c
                rptParam.ReportID = ReportID
                rptParam.Param1 = Convert.ToString(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("MasterID")))
                rptParam.Param2 = Convert.ToString(AptifyEbusinessUser1.CompanyID)
                Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPage & "' )", True)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnOKSubmit_Click(sender As Object, e As System.EventArgs) Handles btnOKSubmit.Click
            Try
                radMockTrialSubmit.VisibleOnPageLoad = False
                Dim oCertGE As AptifyGenericEntityBase
                oCertGE = Me.AptifyApplication.GetEntityObject("MasterSchedules__c", CLng(txtMSNumber.Text.Trim()))
                Me.RecordAttachments__c.LoadAttachments(AptifyApplication.GetEntityID("MasterSchedules__c"), CLng(txtMSNumber.Text.Trim()))
                oCertGE.SetValue("Status", "Signed by Manager")
                oCertGE.SetValue("SignedDate", Convert.ToDateTime(txtSubmitteddate.Text))
                Dim _error As String = ""
                Try
                    If oCertGE.Save(False, _error) Then
                        For i As Integer = 0 To grdContractDetails.Items.Count - 1
                            Dim sTransID As String = DataAction.BeginTransaction(IsolationLevel.Serializable, True)
                            Dim hidECIDgv As HiddenField = DirectCast(grdContractDetails.Items(i).FindControl("hidECID"), HiddenField)
                            Dim oECGE As AptifyGenericEntityBase
                            oECGE = Me.AptifyApplication.GetEntityObject("EducationContracts__c", CLng(hidECIDgv.Value))
                            oECGE.SetValue("Status", "Signed by Firm")
                            _error = ""
                            Try
                                If oECGE.Save(False, _error) Then
                                    DataAction.CommitTransaction(sTransID)
                                Else
                                    DataAction.RollbackTransaction(sTransID)
                                End If
                            Catch ex As Exception
                                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                                Continue For
                            End Try
                        Next
                        If txtStatusReason.Text.Trim() <> "" Then
                            pnlStatusReason.Visible = True
                        End If
                        'code added by GM for Redmine #20665
                        Try
                            Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "Master Schedule Detail Report"))
                            Dim rptParam As New AptifyCrystalReport__c
                            rptParam.ReportID = ReportID
                            rptParam.Param1 = Convert.ToString(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("MasterID")))
                            rptParam.Param2 = Convert.ToString(AptifyEbusinessUser1.CompanyID)
                            Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
                            ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPage & "' )", True)
                        Catch ex As Exception
                            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                        End Try
                        lblWarning.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.MasterScheduleDetails.SuccessMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        radMockTrial.VisibleOnPageLoad = True
                    End If
                Catch ex As Exception
                    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                End Try
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnCancleSubmit_Click(sender As Object, e As System.EventArgs) Handles btnCancleSubmit.Click
            Try
                radMockTrialSubmit.VisibleOnPageLoad = False
                Response.Redirect(MasterScheduleDetailsPage & "?MasterID=" & System.Web.HttpUtility.UrlEncode(Request.QueryString("MasterID")), False)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
            Try
                Response.Redirect(MasterSchedulePage, False)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        'Protected Sub hlTermsandconditions_Click(sender As Object, e As System.EventArgs) Handles hlTermsandconditions.Click
        '    Try
        '        lblWarningTermandCon.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.MasterScheduleDetails.TermsAndCon__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
        '        radMockTrialTermandCon.VisibleOnPageLoad = True
        '    Catch ex As Exception
        '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        '    End Try
        'End Sub

        Protected Sub btnOKTermandCon_Click(sender As Object, e As System.EventArgs) Handles btnOKTermandCon.Click
            radMockTrialTermandCon.VisibleOnPageLoad = False
        End Sub

        Protected Sub grdContractDetails_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles grdContractDetails.ItemCommand
            Try
                Dim lnkButton As LinkButton = e.Item.FindControl("lnkRemoveAssociation")
                Dim hidECID As HiddenField = e.Item.FindControl("hidECID")
                Select Case e.CommandName
                    Case "RemoveAssociation"
                        Dim oFormalGE As AptifyGenericEntityBase
                        oFormalGE = Me.AptifyApplication.GetEntityObject("EducationContracts__c", hidECID.Value)
                        oFormalGE.SetValue("Status", "Signed By Student")
                        oFormalGE.SetValue("MasterSchedule", -1)
                        Dim _error As String = ""
                        Try
                            If oFormalGE.Save(False, _error) Then
                                LoadGrid()
                            End If
                        Catch ex As Exception
                            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                        End Try
                End Select
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub



        Protected Sub grdContractDetails_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdContractDetails.NeedDataSource
            Try
                If ViewState("ContractDetails") IsNot Nothing Then
                    grdContractDetails.DataSource = CType(ViewState("ContractDetails"), DataTable)
                    If txtStatus.Text.Trim.ToUpper = "NEW" Then
                        grdContractDetails.Columns(9).Visible = True
                    Else
                        grdContractDetails.Columns(9).Visible = False
                    End If
                Else
                    btnSubmit.Visible = False 'added by GM for Redmine #20665S
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Code added for Redmine #20665
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub chkTermsandconditions_CheckedChanged(sender As Object, e As EventArgs) Handles chkTermsandconditions.CheckedChanged
            Try
                If chkTermsandconditions.Checked Then
                    btnSubmit.Visible = True
                    btnSubmit.Focus()
                Else
                    btnSubmit.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
    End Class
End Namespace
