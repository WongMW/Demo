''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer              Date Created/Modified               Summary
'Pradip Chavhan         05/15/2015                      Display Formal Registration Records.
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Imports System.Data
Imports Telerik.Web.UI
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports System.IO
Imports Aptify.Framework.Application
Imports System.Text.RegularExpressions
Namespace Aptify.Framework.Web.eBusiness.Education
    Partial Class FormalRegistration__c
        Inherits eBusiness.BaseUserControlAdvanced

        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_FORMALREGG As String = "FormalRegistrationPage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_REPORT As String = "ReportPage"
        Dim sDeletedID As String = String.Empty


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

        Public Overridable Property FormalRegistrationPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_FORMALREGG) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_FORMALREGG))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_FORMALREGG) = Me.FixLinkForVirtualPath(value)
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
            If String.IsNullOrEmpty(FormalRegistrationPage) Then
                FormalRegistrationPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_FORMALREGG)
            End If
            If String.IsNullOrEmpty(ReportPage) Then
                ReportPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_REPORT)
            End If
        End Sub

#End Region

#Region "Page Events"

        Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
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
                Me.Page.MaintainScrollPositionOnPostBack = True 'added for mainitaine scroll position for Redmine #20464
                SetProperties()
                If AptifyEbusinessUser1.PersonID <= 0 Then
                    Session("ReturnToPage") = Request.RawUrl
                    Response.Redirect(LoginPage)
                End If
                txtSubmitteddate.Text = New Date(Today.Year, Today.Month, Today.Day).ToShortDateString() 'Date.Now.ToString("dd/MM/yyyy")
                lblMessage.Text = ""
                If Not IsPostBack Then

                    ' divContractDeclaration2.InnerText = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.FormalRegistration.ContractDeclaration1__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) + Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.FormalRegistration.ContractDeclaration2__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)


                    divContractDeclaration2.InnerText = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.FormalRegistration.ContractDeclaration1__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) '+ Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.FormalRegistration.ContractDeclaration2__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    divContractNotice2.InnerText = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.FormalRegistration.DeclarationNotice__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    'message1.InnerText = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "FormalRegistrationTermsAndCondition__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    divRoute.InnerText = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.FormalRegistration.RouteText__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    divEligibility1.InnerText = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.FormalRegistration.Eligibility1__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    divEligibility2.InnerText = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.FormalRegistration.Eligibility2__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)

                    divRoutelevel.InnerHtml = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.FormalRegistration.RouteLevel__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)


                    If Request.QueryString("FormalRegID") Is Nothing Then
                        pnlData.Visible = True
                        pnlDetails.Visible = False
                        LoadGrid()
                        grdFormalReg.Visible = True
                    Else
                        btnBack.Visible = True
                        pnlData.Visible = False
                        pnlDetails.Visible = True
                        LoadPersonalDetails()
                        'RecordAttachments__c.AllowAdd = True
                        'RecordAttachments__c.AllowDelete = False
                        'Dim idFormalReg As Long = CLng(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("FormalRegID")))
                        'Me.RecordAttachments__c.LoadAttachments(AptifyApplication.GetEntityID("EducationContracts__c"), idFormalReg)
                        'Dim sReportName As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt("formalregreport")
                        'Dim strFilePath As String = ReportPage & "?FormalRegID=" & System.Web.HttpUtility.UrlEncode(Request.QueryString("FormalRegID").ToString()) & "&sReportName=" & System.Web.HttpUtility.UrlEncode(sReportName)
                        'hdnReportPath.Value = strFilePath
                        'btnCreate.Visible = False

                    End If
                    If lblStatus.Text.ToLower() = "new" And Request.QueryString("FormalRegID") IsNot Nothing Then
                        btnSubmit.Visible = True
                        'lblpopups.Visible = True
                    Else
                        btnSubmit.Visible = False
                    End If
                    If lblStatus.Text.ToLower() = "new" Then
                        btnPrint.Visible = False
                        btnSubmit.Visible = True
                    Else
                        ''  chkTermsandconditions.Checked = True ' Code commented by GM for Redmine #20464
                    End If
                End If
                ' changes in if condition for Redmine #20464
                If Session("SignedbyStudent") IsNot Nothing AndAlso lblStatus.Text.Trim = "Signed by Student" Or lblStatus.Text.Trim = "Approved" Then 'updated if condition for Redmine #20591/#20640
                    ''if condition added by GM for Redmine #20640
                    If lblStatus.Text.Trim = "Approved" Then
                        btnSubmitContract.Visible = False ' removed eariler text and added code for Redmine  #20464
                        chkTermsandconditions.Enabled = False
                    Else
                        btnSubmitContract.Visible = True
                        chkTermsandconditions.Enabled = True
                    End If
                    'End Redmine #20640

                    chkTermsandconditions.Checked = True ' added code for Redmine  #20464

                ElseIf Session("FormalRegistrationID") IsNot Nothing Then
                    ' btnPrint.Visible = True

                End If
                ' changes in if condition for Redmine #20464
                If Session("CallPrintBtn") IsNot Nothing Then
                    Session("CallPrintBtn") = Nothing
                    Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "Formal Registration Print"))
                    Dim rptParam As New AptifyCrystalReport__c
                    rptParam.ReportID = ReportID 'code added for crystal report issue 20464
                    rptParam.Param1 = Convert.ToInt32(Session("SignedbyStudent"))
                    Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
                    'Dim strFilePath As String = ReportPage
                    ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPage & "' )", True)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

#Region "Private Functions"
        Private Sub LoadGrid()
            Dim sSQL As String, dt As Data.DataTable
            Try
                sSQL = Database & "..spStudentFormalRegistrationDetails__c"
                Dim param(0) As IDataParameter
                param(0) = DataAction.GetDataParameter("@PersonID", SqlDbType.Int, AptifyEbusinessUser1.PersonID)
                dt = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param)
                Dim dcolUrl As DataColumn = New DataColumn()
                dcolUrl.Caption = "DataNavigateUrl"
                dcolUrl.ColumnName = "DataNavigateUrl"
                dt.Columns.Add(dcolUrl)
                lblMessage.Text = ""
                If dt.Rows.Count > 0 Then
                    For Each rw As DataRow In dt.Rows
                        Dim navigate As String = FormalRegistrationPage & "?FormalRegID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(rw("ID").ToString()))
                        rw("DataNavigateUrl") = navigate
                    Next
                    grdFormalReg.DataSource = dt
                Else
                    sSQL = Database & "..spStudentEEApplicationDetails__c"
                    Dim param1(0) As IDataParameter
                    param1(0) = DataAction.GetDataParameter("@PersonID", SqlDbType.Int, AptifyEbusinessUser1.PersonID)
                    dt = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param)
                    If dt.Rows.Count > 0 Then
                        If dt.Rows(0)("RouteOfEntryName").ToString().ToLower = "contract" Then
                            'FormalRegistrationTermsNoRecord__c
                            lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.FormalRegistration.NoRecord__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) + Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "FormalRegistrationContractDeclaration2__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        ElseIf dt.Rows(0)("RouteOfEntryName").ToString().ToLower = "elevation" Then
                            pnlDetails.Visible = True
                            grdFormalReg.Visible = False
                            divRecognizedexperience.Visible = True
                            pnlEmploymentDetails.Visible = False
                            divWitnessName.Style.Add("display", "none")
                            divWitnessSign.Style.Add("display", "none")
                            divFlexibleDeclaration.Visible = True
                            lblSubmittedDate.Text = "Signature Date"
                            'Siddharth: 7-Jun-16 For Redmine issue #13898
                            lblRouteOfEntry.Text = dt.Rows(0)("RouteOfEntryWebName").ToString()
                            hidEERouteOfEntry.Value = dt.Rows(0)("RouteOfEntryName").ToString().ToLower
                            hidRouteOfEntry.Value = dt.Rows(0)("RouteOfEntry").ToString()
                            lblStatus.Text = dt.Rows(0)("Status").ToString()
                            lblType.Text = dt.Rows(0)("Type").ToString()
                            txtStudentNo.Text = dt.Rows(0)("oldid").ToString()
                            'dt.Rows(0)("StudentNo").ToString()
                            txtStudentName.Text = dt.Rows(0)("FirstLast").ToString()
                            divFlexibleDeclarationText.InnerText = String.Format(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                   Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "FormalRegistrationElevationDeclaration__c")), _
                              Convert.ToInt32(AptifyApplication.UserCredentials.CultureID), DataAction.UserCredentials), dt.Rows(0)("FirstLast").ToString().ToUpper()).ToString()
                            txtGender.Text = dt.Rows(0)("Gender").ToString()
                            txtAddress1.Text = dt.Rows(0)("Line1").ToString()
                            txtAddress2.Text = dt.Rows(0)("Line2").ToString()
                            txtAddress3.Text = dt.Rows(0)("Line3").ToString()
                            txtCity.Text = dt.Rows(0)("City").ToString()
                            txtPostalCode.Text = dt.Rows(0)("PostalCode").ToString()
                            txtCountryOrigin.Text = dt.Rows(0)("Country").ToString()
                            txtEmail.Text = dt.Rows(0)("Email1").ToString()
                            txtMobile.Text = dt.Rows(0)("CellPhone").ToString()
                            txtRecognizedexperience.Text = Convert.ToString(dt.Rows(0)("YearsOfExperience"))
                            If (txtMobile.Text <> "") Then


                                '-----------------To remove symbols when data is nt present'
                                Dim s2, s, d, g As String
                                s2 = txtMobile.Text

                                Dim i As Integer = txtMobile.Text.IndexOf("(")
                                Dim f As String = txtMobile.Text.Substring(i + 1, txtMobile.Text.IndexOf(")", i + 1) - i - 1)
                                If (f = "") Then
                                    s = InStr(1, s2, " (")
                                    d = InStr(1, s2, ") ")
                                    g = s2.Remove(s, d + 1 - s)
                                    txtMobile.Text = g

                                End If
                                Dim wordArr As String() = txtMobile.Text.Split("-")
                                Dim result As String = wordArr(1)
                                result = result.Replace(" ", "")
                                If (result.Length = 0 Or result = "") Then
                                    Regex.Replace(txtMobile.Text, "[^\w\\-]", "")
                                    txtMobile.Text = wordArr(0)
                                End If
                                '-----------------------------------------------------------------------------------
                            End If

                            txtFirmName.Text = dt.Rows(0)("CompanyName").ToString()
                            txtfirmAddress1.Text = dt.Rows(0)("CLine1").ToString()
                            txtfirmAddress2.Text = dt.Rows(0)("CLine2").ToString()
                            txtfirmAddress3.Text = dt.Rows(0)("CLine3").ToString()
                            txtfirmCity.Text = dt.Rows(0)("CCity").ToString()
                            txtfirmCountry.Text = dt.Rows(0)("CCountry").ToString()
                            txtfirmPostalCode.Text = dt.Rows(0)("CPostalCode").ToString()
                            txtStatusReason.Text = dt.Rows(0)("Reason").ToString().Trim()

                            If (dt.Rows(0)("Term").ToString() = "1") Then
                                chkTermsandconditions.Checked = True
                            End If
                            'grdFormalReg.Visible = False
                            pnlData.Visible = False
                        End If
                    Else
                        lblMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.FormalRegistration.NoRecord__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) + Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "FormalRegistrationContractDeclaration2__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub LoadPersonalDetails()
            Dim sSQL As String, dt As Data.DataTable

            Try
                sSQL = Database & "..spGetFormalRegistrationDetails__c"
                Dim param(0) As IDataParameter
                param(0) = DataAction.GetDataParameter("@ID", SqlDbType.Int, Convert.ToInt32(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("FormalRegID"))))
                dt = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param)
                If dt.Rows.Count > 0 Then
                    lblStatus.Text = dt.Rows(0)("Status").ToString()
                    lblType.Text = dt.Rows(0)("Type").ToString()
                    If dt.Rows(0)("RouteOfEntryName").ToString().ToLower() <> "elevation" Then
                        ''Or dt.Rows(0)("EERouteOfEntryName").ToString().ToLower() = "" Then
                        divContractDeclaration.Visible = True
                        divWitnessName.Style.Add("display", "block")
                        divWitnessSign.Style.Add("display", "block")
                        divContractNotice.Visible = True
                        pnlEligibility.Style.Add("display", "none")
                    ElseIf (dt.Rows(0)("RouteOfEntryName").ToString().ToLower() = "elevation") Then
                        hidEERouteOfEntry.Value = dt.Rows(0)("RouteOfEntryName").ToString().ToLower()
                        pnlEmploymentDetails.Visible = False
                        pnlEmploymentDetails.Style.Add("display", "none")
                        divWitnessName.Style.Add("display", "none")
                        divWitnessSign.Style.Add("display", "none")
                        divFlexibleDeclaration.Visible = True
                        lblSubmittedDate.Text = "Signature Date"
                        divRecognizedexperience.Visible = True
                    End If
                    'Siddharth: 7-Jun-16 For Redmine issue #13898
                    lblRouteOfEntry.Text = dt.Rows(0)("RouteOfEntryWebName").ToString()
                    txtStudentNo.Text = dt.Rows(0)("oldid").ToString()
                    'dt.Rows(0)("StudentNo").ToString()
                    txtStudentName.Text = dt.Rows(0)("FirstLast").ToString()
                    divFlexibleDeclarationText.InnerText = String.Format(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                 Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "FormalRegistrationElevationDeclaration__c")), _
                            Convert.ToInt32(AptifyApplication.UserCredentials.CultureID), DataAction.UserCredentials), dt.Rows(0)("FirstLast").ToString().ToUpper()).ToString()
                    txtGender.Text = dt.Rows(0)("Gender").ToString()
                    txtAddress1.Text = dt.Rows(0)("Line1").ToString()
                    txtAddress2.Text = dt.Rows(0)("Line2").ToString()
                    txtAddress3.Text = dt.Rows(0)("Line3").ToString()
                    txtCity.Text = dt.Rows(0)("City").ToString()
                    txtPostalCode.Text = dt.Rows(0)("PostalCode").ToString()
                    txtCountryOrigin.Text = dt.Rows(0)("Country").ToString()
                    txtCounty.Text = dt.Rows(0)("County").ToString()
                    txtEmail.Text = dt.Rows(0)("Email1").ToString()
                    txtMobile.Text = dt.Rows(0)("CellPhone").ToString()
                    txtRecognizedexperience.Text = Convert.ToString(dt.Rows(0)("YearsOfExperience"))
                    '-----------------To remove symbols when data is nt present'
                    If (txtMobile.Text <> "") Then

                        Dim s2, s, d, g As String
                        s2 = txtMobile.Text
                        Dim i As Integer = txtMobile.Text.IndexOf("(")
                        Dim f As String = txtMobile.Text.Substring(i + 1, txtMobile.Text.IndexOf(")", i + 1) - i - 1)
                        If (f = "") Then
                            s = InStr(1, s2, " (")
                            d = InStr(1, s2, ") ")
                            g = s2.Remove(s, d + 1 - s)
                            txtMobile.Text = g

                        End If
                        Dim wordArr As String() = txtMobile.Text.Split("-")
                        Dim result As String = wordArr(1)
                        result = result.Replace(" ", "")
                        If (result.Length = 0 Or result = "") Then
                            Regex.Replace(txtMobile.Text, "[^\w\\-]", "")
                            txtMobile.Text = wordArr(0)
                        End If

                    End If
                    '-----------------------------------------------------------------------------------
                    txtFirmName.Text = dt.Rows(0)("CompanyName").ToString()
                    txtfirmAddress1.Text = dt.Rows(0)("CLine1").ToString()
                    txtfirmAddress2.Text = dt.Rows(0)("CLine2").ToString()
                    txtfirmAddress3.Text = dt.Rows(0)("CLine3").ToString()
                    txtfirmCity.Text = dt.Rows(0)("CCity").ToString()
                    txtfirmCountry.Text = dt.Rows(0)("CCountry").ToString()
                    txtfirmCounty.Text = dt.Rows(0)("CCounty").ToString()
                    txtfirmPostalCode.Text = dt.Rows(0)("CPostalCode").ToString()
                    txtStatusReason.Text = dt.Rows(0)("Reason").ToString().Trim()
                    hidRouteOfEntry.Value = dt.Rows(0)("RouteOfEntry").ToString().Trim()
                    If dt.Rows(0)("ContractStartDate").ToString().Trim() <> "" Then
                        Dim d1 As DateTime = Convert.ToDateTime(dt.Rows(0)("ContractStartDate").ToString().Trim())
                        txtfirmStartDate.Text = (New Date(d1.Year, d1.Month, d1.Day).ToShortDateString())
                        txtfirmContractDur.Text = dt.Rows(0)("ContractDuration").ToString().Trim()
                    End If
                    If dt.Rows(0)("ContractExpireDate").ToString().Trim() <> "" Then
                        Dim d2 As DateTime = Convert.ToDateTime(dt.Rows(0)("ContractExpireDate").ToString().Trim())
                        txtfirmEndDate.Text = (New Date(d2.Year, d2.Month, d2.Day).ToShortDateString())
                    End If

                    If txtStatusReason.Text.Trim <> "" Then
                        pnlStatusReason.Visible = True
                    End If

                    If (dt.Rows(0)("Term").ToString() = "1") Then
                        chkTermsandconditions.Checked = True
                    End If
                    If (dt.Rows(0)("RouteA").ToString() = "True") Then
                        chkRouteA.Checked = True
                    End If
                    If (dt.Rows(0)("RouteB").ToString() = "True") Then
                        chkRouteB.Checked = True
                    End If
                    'If dt.Rows(0)("DateRegistered").ToString.Trim() <> "" Then
                    '    hidDateRegistered.Value = dt.Rows(0)("DateRegistered").ToString()
                    'End If

                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            Finally
            End Try
        End Sub

        Private Sub AddExpression()
            Dim ExpSyllabusSort As New GridSortExpression
            Dim ExpStudentSort As New GridSortExpression
            ExpStudentSort.FieldName = "Type"
            ExpSyllabusSort.SetSortOrder("Ascending")
            grdFormalReg.MasterTableView.SortExpressions.AddSortExpression(ExpStudentSort)
        End Sub

        Function validateBeforeSubmit() As Boolean

            If (hidEERouteOfEntry.Value = "elevation") And chkRouteA.Checked = False And chkRouteB.Checked = False Then
                lblWarning.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.FormalRegistration.RouteValidation__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                radMockTrial.VisibleOnPageLoad = True
                btnOKValidation.Visible = True
                btnOk.Visible = False
                Return False
            End If

            If chkTermsandconditions.Checked = False Then
                lblWarning.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.FormalRegistration.TermsAndConValidation__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                chkTermsandconditions.Focus()
                radMockTrial.VisibleOnPageLoad = True
                btnOKValidation.Visible = True
                btnOk.Visible = False
                Return False
            End If
            Return True
        End Function

#End Region

        Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
            Dim idFormalReg As Long
            Try
                If Session("Value") = "" Then
                    If validateBeforeSubmit() Then
                        Dim sTransID As String = DataAction.BeginTransaction(IsolationLevel.Serializable, True)
                        If Request.QueryString("FormalRegID") Is Nothing Then
                            idFormalReg = -1
                        Else
                            idFormalReg = CLng(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("FormalRegID")))
                        End If
                        Dim oCertGE As AptifyGenericEntityBase
                        oCertGE = Me.AptifyApplication.GetEntityObject("EducationContracts__c", idFormalReg)
                        'Me.RecordAttachments__c.LoadAttachments(AptifyApplication.GetEntityID("EducationContracts__c"), idFormalReg)
                        'Commented by Kavita
                        'If chkTermsandconditions.Checked Then
                        '    oCertGE.SetValue("Term", 1)
                        'Else
                        '    oCertGE.SetValue("Term", 0)
                        'End If
                        oCertGE.SetValue("Status", "Signed by Student")
                        oCertGE.SetValue("StudentID", AptifyEbusinessUser1.PersonID)
                        If chkRouteA.Checked Then
                            oCertGE.SetValue("RouteA", 1)
                        End If
                        If chkRouteB.Checked Then
                            oCertGE.SetValue("RouteB", 1)
                        End If
                        oCertGE.SetValue("RouteOfEntry", hidRouteOfEntry.Value)
                        oCertGE.SetValue("Type", lblType.Text)

                        Try
                            ''Added If Condition By Pradip 2016-09-13 for redmine issue https://redmine.softwaredesign.ie/issues/15151 
                            If txtfirmStartDate.Text.Trim = "" Then
                                Dim sSQL As String = Database & "..spGetEarlierRegistrationDate__c"
                                Dim param(0) As IDataParameter
                                param(0) = DataAction.GetDataParameter("@PersonID", SqlDbType.Int, AptifyEbusinessUser1.PersonID)
                                Dim dt As DataTable = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param)
                                If dt.Rows.Count > 0 Then
                                    If dt.Rows(0)("StartDate__c").ToString().Trim() <> "" Then
                                        Dim d1 As DateTime = Convert.ToDateTime(dt.Rows(0)("StartDate__c").ToString().Trim())
                                        oCertGE.SetValue("ContractStartDate", New Date(d1.Year, d1.Month, d1.Day).ToShortDateString())

                                    Else
                                        oCertGE.SetValue("ContractStartDate", New Date(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToShortDateString())
                                    End If
                                Else
                                    oCertGE.SetValue("ContractStartDate", New Date(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToShortDateString())
                                End If
                            End If
                        Catch ex As Exception
                        End Try
                        Dim _error As String = ""
                        Try
                            If oCertGE.Save(False, _error) Then
                                Session("Value") = "1"
                                DataAction.CommitTransaction(sTransID)
                                Session("FormalRegistrationID") = oCertGE.RecordID 'line added by GM for Redmine 20464
                                Session("SignedbyStudent") = oCertGE.RecordID 'line added by GM for Redmine 20591
                                'code uncommented by GM for Redmine #20464
                                Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "Formal Registration Print"))
                                Dim rptParam As New AptifyCrystalReport__c
                                rptParam.ReportID = ReportID
                                rptParam.Param1 = oCertGE.RecordID
                                'Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("FormalRegID"))
                                Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
                                ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPage & "' )", True)

                                lblWarning.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusinessFormalRegistrationPage.SuccessMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                radMockTrial.VisibleOnPageLoad = True
                                btnOk.Visible = True
                                btnOKValidation.Visible = False

                                ' btnPrint.Visible = True ' comment line for  Redmine #20464
                                'End Redmine #20464
                            Else
                                DataAction.RollbackTransaction(sTransID)
                            End If
                        Catch ex As Exception
                            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                        End Try
                    End If

                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnOk_Click(sender As Object, e As System.EventArgs) Handles btnOk.Click
            Try
                radMockTrial.VisibleOnPageLoad = False
                ' Response.Redirect(FormalRegistrationPage, False) ' commented code for Redmine #20464
                'updated response.redirect string for Redmine #20591
                Response.Redirect(HttpContext.Current.Request.Url.OriginalString.ToString() & "?FormalRegID=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(Session("FormalRegistrationID")))
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        'Protected Sub btnCreate_Click(sender As Object, e As System.EventArgs) Handles btnCreate.Click
        '    Dim idFormalReg As Long
        '    Try
        '        'Added By kavita Zinage 11-03-2016
        '        Dim oCertGE As AptifyGenericEntityBase
        '        If Session("Value") = "" Then
        '            Session("Value") = "1"
        '            If validateBeforeSubmit() Then
        '                Dim sTransID As String = DataAction.BeginTransaction(IsolationLevel.Serializable, True)
        '                If Request.QueryString("FormalRegID") Is Nothing Then
        '                    idFormalReg = -1
        '                Else
        '                    idFormalReg = CLng(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("FormalRegID")))
        '                End If
        '                'Dim oCertGE As AptifyGenericEntityBase
        '                oCertGE = Me.AptifyApplication.GetEntityObject("EducationContracts__c", idFormalReg)
        '                ' Me.RecordAttachments__c.LoadAttachments(AptifyApplication.GetEntityID("EducationContracts__c"), idFormalReg)
        '                'Commented By kavita
        '                'If chkTermsandconditions.Checked Then
        '                '    oCertGE.SetValue("Term", 1)
        '                'Else
        '                '    oCertGE.SetValue("Term", 0)
        '                'End If
        '                oCertGE.SetValue("Status", lblStatus.Text)
        '                oCertGE.SetValue("StudentID", AptifyEbusinessUser1.PersonID)
        '                If chkRouteA.Checked Then
        '                    oCertGE.SetValue("RouteA", 1)
        '                End If
        '                If chkRouteB.Checked Then
        '                    oCertGE.SetValue("RouteB", 1)
        '                End If
        '                oCertGE.SetValue("RouteOfEntry", hidRouteOfEntry.Value)
        '                oCertGE.SetValue("Type", lblType.Text)
        '                Try
        '                    Dim sSQL As String = Database & "..spGetEarlierRegistrationDate__c"
        '                    Dim param(0) As IDataParameter
        '                    param(0) = DataAction.GetDataParameter("@PersonID", SqlDbType.Int, AptifyEbusinessUser1.PersonID)
        '                    Dim dt As DataTable = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, param)
        '                    If dt.Rows.Count > 0 Then
        '                        If dt.Rows(0)("StartDate__c").ToString().Trim() <> "" Then
        '                            Dim d1 As DateTime = Convert.ToDateTime(dt.Rows(0)("StartDate__c").ToString().Trim())
        '                            oCertGE.SetValue("ContractStartDate", New Date(d1.Year, d1.Month, d1.Day).ToShortDateString())

        '                        Else
        '                            oCertGE.SetValue("ContractStartDate", New Date(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToShortDateString())
        '                        End If
        '                    Else
        '                        oCertGE.SetValue("ContractStartDate", New Date(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToShortDateString())
        '                    End If
        '                Catch ex As Exception

        '                End Try
        '                Dim _error As String = ""
        '                Try
        '                    If oCertGE.Save(False, _error) Then

        '                        DataAction.CommitTransaction(sTransID)
        '                        hidFormalID.Value = oCertGE.RecordID.ToString()
        '                        Session("Value") = oCertGE.RecordID.ToString()
        '                        radCreateMsg.VisibleOnPageLoad = True
        '                        lblCreate.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.FormalRegistration.AfterCreateMSG__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
        '                    Else
        '                        DataAction.RollbackTransaction(sTransID)
        '                    End If
        '                Catch ex As Exception
        '                    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        '                End Try
        '            End If
        '        Else
        '            hidFormalID.Value = Convert.ToString(Session("Value"))
        '            radCreateMsg.VisibleOnPageLoad = True
        '            lblCreate.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.FormalRegistration.AfterCreateMSG__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
        '        End If
        '    Catch ex As Exception
        '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        '    End Try
        'End Sub

        Protected Sub btnPrint_Click(sender As Object, e As System.EventArgs) Handles btnPrint.Click
            Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "Formal Registration Print"))
            Dim rptParam As New AptifyCrystalReport__c
            rptParam.ReportID = ReportID
            ' If condition added by GM for Redmine #20464
            If Session("FormalRegistrationID") Is Nothing Then
                rptParam.Param1 = Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("FormalRegID"))
            Else
                rptParam.Param1 = Session("FormalRegistrationID")
            End If
            'rptParam.Param1 = Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("FormalRegID"))
            Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
            'Dim strFilePath As String = ReportPage
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPage & "' )", True)
        End Sub

        'Protected Sub hlTermsandconditions_Click(sender As Object, e As System.EventArgs) Handles hlTermsandconditions.Click //changed by LH for ticket #20390
        '    Try
        '        lblWarning.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.FormalRegistration.TermsAndCondition__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
        '        radMockTrial.VisibleOnPageLoad = True
        '        btnOKValidation.Visible = True
        '        btnOk.Visible = False
        '    Catch ex As Exception
        '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        '    End Try
        'End Sub

        Protected Sub btnOKValidation_Click(sender As Object, e As System.EventArgs) Handles btnOKValidation.Click
            Try
                radMockTrial.VisibleOnPageLoad = False
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
            Try

                Dim path As String = Server.MapPath("~/Reports__c/Flexible_Route_Registration_Form.pdf") 'get file object as FileInfo
                Dim file As System.IO.FileInfo = New System.IO.FileInfo(path) '-- if the file exists on the server
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
                Response.AddHeader("Content-Length", file.Length.ToString())
                Response.ContentType = "application/pdf"
                Response.WriteFile(file.FullName)
                Response.End() 'if file does not exist
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
            Response.Redirect(FormalRegistrationPage)
        End Sub

        Protected Sub btnCreateOK_Click(sender As Object, e As System.EventArgs) Handles btnCreateOK.Click
            radCreateMsg.VisibleOnPageLoad = False
            Response.Redirect(HttpContext.Current.Request.Url.ToString() + "?FormalRegID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(hidFormalID.Value.ToString())), False)
        End Sub
        ''' <summary>
        ''' Code added by GM for Redmine #20464
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub btnSubmitContract_Click(sender As Object, e As EventArgs) Handles btnSubmitContract.Click
            Try
                ' radSumbitWin.VisibleOnPageLoad = True ' code commented by GM for Redmine #20464
                'code added for changes in Redmine #20464
                Dim oEducationContractGE As AptifyGenericEntityBase
                oEducationContractGE = Me.AptifyApplication.GetEntityObject("EducationContracts__c", Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("FormalRegID")))
                oEducationContractGE.SetValue("Status", "Signed by Student")
                oEducationContractGE.Save(False)
                radSumbitWin.VisibleOnPageLoad = False
                Session("SignedbyStudent") = oEducationContractGE.RecordID
                btnUpdate.Visible = False
                btnPrint.Visible = False 'Redmine #20464
                Session("CallPrintBtn") = True 'added for Redmine #20464
                Response.Redirect(HttpContext.Current.Request.Url.OriginalString.ToString()) 'Updated URL for Redmine #20464
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        '''  Code added/updated by GM for Redmine #20464
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
            Try
                Dim oEducationContractGE As AptifyGenericEntityBase
                oEducationContractGE = Me.AptifyApplication.GetEntityObject("EducationContracts__c", Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("FormalRegID")))
                oEducationContractGE.SetValue("Status", "Signed by Student")
                oEducationContractGE.Save(False)
                radSumbitWin.VisibleOnPageLoad = False
                Session("SignedbyStudent") = oEducationContractGE.RecordID
                btnUpdate.Visible = False
                btnPrint.Visible = False 'Redmine #20464
                Session("CallPrintBtn") = True 'added for Redmine #20464
                Response.Redirect(HttpContext.Current.Request.Url.OriginalString.ToString()) 'Updated URL for Redmine #20464
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        '''  Code added by GM for Redmine #20464
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub chkTermsandconditions_CheckedChanged(sender As Object, e As EventArgs) Handles chkTermsandconditions.CheckedChanged
            Try
                If chkTermsandconditions.Checked = True Then
                    'added if condition for Redmine #20464
                    If Request.QueryString("FormalRegID") Is Nothing Then
                        btnSubmit.Visible = True
                    Else
                        btnSubmitContract.Visible = True
                    End If

                Else
                    btnSubmitContract.Visible = False 'added for Redmine #20464
                    btnSubmit.Visible = False 'added for Redmine #20464
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
    End Class
End Namespace
