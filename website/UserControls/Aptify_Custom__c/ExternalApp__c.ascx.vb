'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Govind Mande                1/12/2014                      Created External Application
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Explicit On

Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness
Imports Telerik.Web.UI
Imports Aptify.Applications.OrderEntry
Imports Aptify.Applications.Accounting

Namespace Aptify.Framework.Web.eBusiness.Generated
    Partial Class ExternalApp__c
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
        Public RecordId As Integer
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "ExternalApp__c"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_NAME As String = "HomePage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"
        ''Added BY Pradip Chavhan To Print Report.
        Protected Const ATTRIBUTE_CONTROL_ReportPage As String = "ReportPage"
        Public Overridable Property RedirectURLs() As String
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


        Public Overridable Property ReportPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_ReportPage) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_ReportPage))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_ReportPage) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        ''' <summary>
        ''' Load Js file
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        ''' 

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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
            SetProperties()
            'Added below code for Loading panel time out time  from Govind M.
            Dim _ScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
            _ScriptManager.AsyncPostBackTimeout = "1000"

            ' btnPrint.Visible = False
            If User1.PersonID <= 0 Then
                Session("ReturnToPage") = Request.RawUrl
                Response.Redirect(LoginPage)
            End If
            'lblDeclaration.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.FormalRegistration.ContractDeclaration1__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) + Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.FormalRegistration.ContractDeclaration2__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            lblDeclaration.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.FormalRegistration.ContractDeclaration1__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            If Not IsPostBack Then
                If Not String.IsNullOrEmpty(Session("ExternalApp.hdnHeader")) Then
                    hdnHeader.Value = Session("ExternalApp.hdnHeader")
                    hdnSummer.Value = Session("ExternalApp.hdnSummer")
                    hdnAutum.Value = Session("ExternalApp.hdnAutum")
                    hdnCourse.Value = Session("ExternalApp.hdnCourse")
                    hdnCreditCard.Value = Session("ExternalApp.hdnCreditCard")
                End If
                GetPrefferedCurrency()
                If CheckCACurriculumStudentApplied() = True Then
                    LoadHeaderText()
                    GetExternalApplicationType()
                    CreditCard.LoadCreditCardInfo()
                    LoadSummerExamLocation()
                    If Convert.ToString(drpSummerExamLocation.SelectedValue) <> "" Then
                        LoadSummerExamSession(drpSummerExamLocation.SelectedValue)
                    Else
                        LoadSummerExamSession(0)
                    End If
                    LoadAutumExamLocation()
                    If Convert.ToString(drpAutumnExamLocation.SelectedValue) <> "" Then
                        LoadAutumExamSession(drpAutumnExamLocation.SelectedValue)
                    Else
                        LoadAutumExamSession(0)
                    End If
                    LoadRevisionCourseLocation()
                    If Convert.ToString(drpCourseLocation.SelectedValue) <> "" Then
                        LoadRevisionCourseSession(drpCourseLocation.SelectedValue)
                    Else
                        LoadRevisionCourseSession(0)
                    End If
                    CheckHisAlreadyRegister()
                    CheckExternalAppCount()
                    SummerPaymentSummery = Nothing
                    Dim sReportName As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt("externalappreport")
                    Dim strFilePath As String = ReportPage & "?PersonID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(User1.PersonID)) & "&sReportName=" & System.Web.HttpUtility.UrlEncode(sReportName)
                    hdnReportPath.Value = strFilePath

                    'Redmine issue 13601
                    LoadEligibilityOptions()
                    PopulateDropDowns()
                    LoadPersonalDetails()
                Else

                End If
            End If
        End Sub
        ''' <summary>
        ''' Load Eligibility Option for redmine issue 13601
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadEligibilityOptions()
            Try
                Dim sSql As String = Database & "..spGetEligibilityOptions__c "
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    drpEligibilityOption.DataSource = dt
                    drpEligibilityOption.DataTextField = "Name"
                    drpEligibilityOption.DataValueField = "ID"
                    drpEligibilityOption.DataBind()
                    drpEligibilityOption.Items.Insert(0, "Select eligibility option")
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub hlTermsandconditions_Click(sender As Object, e As System.EventArgs) Handles hlTermsandconditions.Click
            Try
                Dim path As String = Server.MapPath("~/Reports__c/External Candidates Regulations.pdf") 'get file object as FileInfo
                Dim file As System.IO.FileInfo = New System.IO.FileInfo(path) '-- if the file exists on the server
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
                Response.AddHeader("Content-Length", file.Length.ToString())
                Response.ContentType = "application/pdf"
                Response.WriteFile(file.FullName)
                Response.End() 'if file does not exist
                'lblWarning.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.FormalRegistration.TermsAndCondition__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                'radMockTrial.VisibleOnPageLoad = True
                'btnOKValidation.Visible = True
                'btnOk.Visible = False
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub btnOKValidation_Click(sender As Object, e As System.EventArgs) Handles btnOKValidation.Click
            Try
                radMockTrial.VisibleOnPageLoad = False
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub DisplayHeading()

        End Sub

        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            '  MyBase.SetProperties()
            If String.IsNullOrEmpty(Me.RedirectURLs) Then
                Me.RedirectURLs = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_NAME)
            End If
            If String.IsNullOrEmpty(LoginPage) Then
                LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage)
            End If
            If String.IsNullOrEmpty(ReportPage) Then
                ReportPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_ReportPage)
            End If
        End Sub

        Private Sub GetExternalApplicationType()
            Try
                Dim sSql As String = Database & "..spGetExternalAppType__c"
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    lblRouteOfEntry.Text = Convert.ToString(dt.Rows(0)("Name"))
                    ViewState("ExternalTypeID") = Convert.ToString(dt.Rows(0)("ID"))
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' check user can apply more than 2 then restict
        ''' </summary>
        ''' <remarks></remarks>
        ''' 

        Private Sub CheckExternalAppCount()
            Try
                Dim sSql As String = Database & "..spCheckCountForExternalApp__c @PersonID=" & User1.PersonID & ",@AcademicCycleID=" & ViewState("AcademicCycleID")
                Dim lCount As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If lCount > 1 Then
                    lblError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPage.AlradyAppliedErrorMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    lblError.Visible = True
                    btnSubmit.Visible = False
                    '  btnPrint.Visible = True
                    ' added 15 April 2015
                    drpSummerExamLocation.Visible = False
                    drpAutumnExamLocation.Visible = False
                    drpCourseLocation.Visible = False
                    grdSummerExamSession.Visible = False
                    grdAutumExamSession.Visible = False
                    grdCourseSelection.Visible = False
                    CreditCard.Visible = False
                    lblSummerExamLocation.Visible = False
                    lblAutumnExamLocation.Visible = False
                    lblCourseLocation.Visible = False
                    lblRoute.Visible = False
                    lblRouteOfEntry.Visible = False
                    ''lblFurtherInformation.Visible = False
                    ''txtFurtherInfo.Visible = False

                    lblFirstLastHeading.Visible = False
                    lblStudentNumberHeading.Visible = False
                    lblStatusHeading.Visible = False
                    lblAcademicCycleHeading.Visible = False
                    lblStatusHeading.Visible = False
                    lblCommentsHeading.Visible = False
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myFunction", "myFunction();", True)
                    idDemodiv.Visible = False
                    HyperLink1.Visible = False
                    lblFirstLast.Visible = False
                    lblAcademicCycle.Visible = False
                    lblStatus.Visible = False
                    lblComments.Visible = False
                    lblStudentNumber.Visible = False

                    lblDeclaration.Visible = False
                    chkTermsandconditions.Visible = False
                    hlTermsandconditions.Visible = False
                Else
                    btnSubmit.Visible = True
                    'btnPrint.Visible = False
                    lblError.Visible = False
                    drpSummerExamLocation.Visible = True
                    drpAutumnExamLocation.Visible = True
                    drpCourseLocation.Visible = True
                    grdSummerExamSession.Visible = True
                    grdAutumExamSession.Visible = True
                    grdCourseSelection.Visible = True
                    CreditCard.Visible = True
                    lblError.Visible = False
                    lblSummerExamLocation.Visible = True
                    lblAutumnExamLocation.Visible = True
                    lblCourseLocation.Visible = True
                    lblRoute.Visible = True
                    lblRouteOfEntry.Visible = True
                    'lblFurtherInformation.Visible = True
                    'txtFurtherInfo.Visible = True
                    lblFirstLastHeading.Visible = True
                    lblStudentNumberHeading.Visible = True
                    lblStatusHeading.Visible = True
                    lblAcademicCycleHeading.Visible = True
                    lblStatusHeading.Visible = True
                    lblCommentsHeading.Visible = True
                    idDemodiv.Visible = True
                    HyperLink1.Visible = True

                    lblFirstLast.Visible = True
                    lblAcademicCycle.Visible = True
                    lblStatus.Visible = True
                    lblComments.Visible = True
                    lblStudentNumber.Visible = True
                    lblDeclaration.Visible = True
                    chkTermsandconditions.Visible = True
                    hlTermsandconditions.Visible = True
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' if user already registered then they can not apply same course
        ''' </summary>
        ''' <remarks></remarks>
        ''' 

        Private Sub CheckHisAlreadyRegister()
            Try
                Dim sSql As String = Database & "..spGetExternalAppDetails__c @PersonID=" & User1.PersonID & ",@AcademicCycleID=" & ViewState("AcademicCycleID")
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        '
                        ViewState("ExamptionID") = dr("ID")
                        lblStatus.Text = Convert.ToString(dr("Status"))
                        lblComments.Text = Convert.ToString(dr("comments"))
                        ' txtFurtherInfo.Text = Convert.ToString(dr("FurtherInformation"))
                        'Redmin issue 13601
                        If Convert.ToInt32(dr("EligibilityOptionsID")) > 0 Then
                            drpEligibilityOption.SelectedValue = dr("EligibilityOptionsID")
                        End If
                        For Each row As Telerik.Web.UI.GridItem In grdSummerExamSession.Items
                            Dim lblProductID As Label = DirectCast(row.FindControl("lblProductID"), Label)
                            Dim chkSummerExam As CheckBox = DirectCast(row.FindControl("chkSummerExam"), CheckBox)
                            Dim lblComments As Label = DirectCast(row.FindControl("lblComments"), Label)
                            Dim lblClassID As Label = DirectCast(row.FindControl("lblClassID"), Label)
                            If Convert.ToInt32(lblClassID.Text) = Convert.ToInt32(dr("classid")) Then
                                'chkSummerExam.Checked = True
                                If Convert.ToString(dr("name")).Trim.ToLower = "summer" Then
                                    chkSummerExam.Enabled = False
                                    lblComments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPage.AlradyAppliedCourseErrorMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                End If

                            End If
                        Next
                        For Each row As Telerik.Web.UI.GridItem In grdAutumExamSession.Items
                            Dim lblAutumInterimProductID As Label = DirectCast(row.FindControl("lblAutumInterimProductID"), Label)
                            Dim chkAutumINtrim As CheckBox = DirectCast(row.FindControl("chkAutumINtrim"), CheckBox)
                            Dim lblComments As Label = DirectCast(row.FindControl("lblComments"), Label)
                            Dim lblProductID As Label = DirectCast(row.FindControl("lblProductID"), Label)
                            Dim chkAutumExam As CheckBox = DirectCast(row.FindControl("chkAutumExam"), CheckBox)
                            Dim lblAutumInterimClassID As Label = DirectCast(row.FindControl("lblAutumInterimClassID"), Label)
                            Dim lblClassID As Label = DirectCast(row.FindControl("lblClassID"), Label)
                            If Convert.ToInt32(lblAutumInterimClassID.Text) = Convert.ToInt32(dr("classid")) Then
                                chkAutumINtrim.Enabled = False
                                lblComments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPage.AlradyAppliedCourseErrorMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            ElseIf Convert.ToInt32(lblClassID.Text) = Convert.ToInt32(dr("classid")) Then
                                '  chkAutumExam.Checked = True
                                If Convert.ToString(dr("name")).Trim.ToLower = "autumn" Then
                                    chkAutumExam.Enabled = False
                                    lblComments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPage.AlradyAppliedCourseErrorMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                End If

                            End If
                            'If Convert.ToInt32(lblAutumInterimProductID.Text) = Convert.ToInt32(dr("ProductID")) Then
                            '    ' chkAutumINtrim.Checked = True
                            '    chkAutumINtrim.Enabled = False
                            '    lblComments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPage.AlradyAppliedCourseErrorMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            'ElseIf Convert.ToInt32(lblProductID.Text) = Convert.ToInt32(dr("ProductID")) Then
                            '    '  chkAutumExam.Checked = True
                            '    chkAutumExam.Enabled = False
                            '    lblComments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPage.AlradyAppliedCourseErrorMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            'End If


                        Next

                        For Each row As Telerik.Web.UI.GridItem In grdCourseSelection.Items
                            Dim lblAutumRevisionProductID As Label = DirectCast(row.FindControl("lblAutumRevisionProductID"), Label)
                            Dim chkAutumRevisionCourse As CheckBox = DirectCast(row.FindControl("chkAutumRevisionCourse"), CheckBox)
                            Dim lblComments As Label = DirectCast(row.FindControl("lblComments"), Label)
                            Dim lblProductID As Label = DirectCast(row.FindControl("lblProductID"), Label)
                            Dim chkSummerRevisionCourse As CheckBox = DirectCast(row.FindControl("chkSummerRevisionCourse"), CheckBox)
                            Dim lblClassID As Label = DirectCast(row.FindControl("lblClassID"), Label)
                            Dim lblAutumRevisionClassID As Label = DirectCast(row.FindControl("lblAutumRevisionClassID"), Label)
                            If Convert.ToInt32(lblClassID.Text) = Convert.ToInt32(dr("classid")) Then
                                If Convert.ToString(dr("name")).Trim.ToLower = "summer" Then
                                    chkSummerRevisionCourse.Enabled = False
                                    lblComments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPage.AlradyAppliedCourseErrorMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                End If

                            ElseIf Convert.ToInt32(lblAutumRevisionClassID.Text) = Convert.ToInt32(dr("classid")) Then
                                '  chkAutumExam.Checked = True
                                If Convert.ToString(dr("name")).Trim.ToLower = "autumn" Then
                                    chkAutumRevisionCourse.Enabled = False
                                    lblComments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPage.AlradyAppliedCourseErrorMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                End If

                            End If

                            ''If Convert.ToInt32(lblAutumRevisionProductID.Text) = Convert.ToInt32(dr("ProductID")) Then
                            ''    ' chkAutumRevisionCourse.Checked = True
                            ''    chkAutumRevisionCourse.Enabled = False
                            ''    lblComments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPage.AlradyAppliedCourseErrorMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            ''ElseIf Convert.ToInt32(lblProductID.Text) = Convert.ToInt32(dr("ProductID")) Then
                            ''    ' chkSummerRevisionCourse.Checked = True
                            ''    chkSummerRevisionCourse.Enabled = False
                            ''    lblComments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPage.AlradyAppliedCourseErrorMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            ''End If
                        Next
                    Next
                Else
                    ViewState("ExamptionID") = -1
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub LoadHeaderText()
            Try
                lblFirstLast.Text = User1.FirstName + " " + User1.LastName
                'lblStudentNumber.Text = CStr(User1.PersonID)
                Dim sSqlOldPersonID = Database & "..spGetPerosnOldIdbyPersonId__c @Id=" & CStr(User1.PersonID) & ""
                Dim lOldPersonID As String = DataAction.ExecuteScalar(sSqlOldPersonID, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If lOldPersonID <> "" Then
                    lblStudentNumber.Text = lOldPersonID
                End If
                Dim sSql As String = Database & "..spGetCurrentAcademicYearExtApp__c"
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    lblAcademicCycle.Text = Convert.ToString(dt.Rows(0)("Name"))
                    ViewState("AcademicCycleID") = Convert.ToString(dt.Rows(0)("ID"))
                End If
                'lblAcademicCycle.Text = Convert.ToString(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Function CheckCACurriculumStudentApplied() As Boolean
            Try
                Dim bApplicabale As Boolean = True
                Dim sSql As String = Database & "..spCheckCAStudentAppliedForExternal__c @PersonID=" & User1.PersonID
                Dim lCAID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If lCAID > 0 Then

                    lblError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPage.ErrorMsgCACurriculumStudent")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    lblError.Visible = True
                    btnSubmit.Visible = False
                    ' btnPrint.Visible = True
                    drpSummerExamLocation.Visible = False
                    drpAutumnExamLocation.Visible = False
                    drpCourseLocation.Visible = False
                    grdSummerExamSession.Visible = False
                    grdAutumExamSession.Visible = False
                    grdCourseSelection.Visible = False
                    CreditCard.Visible = False
                    lblSummerExamLocation.Visible = False
                    lblAutumnExamLocation.Visible = False
                    lblCourseLocation.Visible = False
                    lblRoute.Visible = False
                    lblRouteOfEntry.Visible = False
                    'lblFurtherInformation.Visible = False
                    'txtFurtherInfo.Visible = False
                    bApplicabale = False
                    lblFirstLastHeading.Visible = False
                    lblStudentNumberHeading.Visible = False
                    lblStatusHeading.Visible = False
                    lblAcademicCycleHeading.Visible = False
                    lblStatusHeading.Visible = False
                    lblCommentsHeading.Visible = False
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myFunction", "myFunction();", True)
                    idDemodiv.Visible = False
                    HyperLink1.Visible = False
                    lblDeclaration.Visible = False
                    chkTermsandconditions.Visible = False
                    hlTermsandconditions.Visible = False
                Else
                    bApplicabale = True
                    btnSubmit.Visible = True
                    ' btnPrint.Visible = True
                    drpSummerExamLocation.Visible = True
                    drpAutumnExamLocation.Visible = True
                    drpCourseLocation.Visible = True
                    grdSummerExamSession.Visible = True
                    grdAutumExamSession.Visible = True
                    grdCourseSelection.Visible = True
                    CreditCard.Visible = True
                    lblError.Visible = False
                    lblSummerExamLocation.Visible = True
                    lblAutumnExamLocation.Visible = True
                    lblCourseLocation.Visible = True
                    lblRoute.Visible = True
                    lblRouteOfEntry.Visible = True
                    'lblFurtherInformation.Visible = True
                    'txtFurtherInfo.Visible = True
                    lblFirstLastHeading.Visible = True
                    lblStudentNumberHeading.Visible = True
                    lblStatusHeading.Visible = True
                    lblAcademicCycleHeading.Visible = True
                    lblStatusHeading.Visible = True
                    lblCommentsHeading.Visible = True
                    idDemodiv.Visible = True
                    HyperLink1.Visible = True
                    lblDeclaration.Visible = True
                    chkTermsandconditions.Visible = True
                    hlTermsandconditions.Visible = True
                End If
                Return bApplicabale
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
        End Function
        ''' <summary>
        ''' Load Summer exam location
        ''' </summary>
        ''' <remarks></remarks>
        ''' 

        Private Sub LoadSummerExamLocation()
            Try
                Dim sSql As String = Database & "..spGetExternalStudentExamLocation__c @AcademicCycleID=" & ViewState("AcademicCycleID")
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    drpSummerExamLocation.DataSource = dt
                    drpSummerExamLocation.DataTextField = "Venue"
                    drpSummerExamLocation.DataValueField = "ID"
                    drpSummerExamLocation.DataBind()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' As per location display exams
        ''' </summary>
        ''' <param name="ExamVenueID"></param>
        ''' <remarks></remarks>
        ''' 

        Protected Sub LoadSummerExamSession(ByVal ExamVenueID As Long)
            Try
                Dim sSql As String = Database & "..spGetExternalStudentExamDetails__c @AcademicCycleID=" & ViewState("AcademicCycleID") & ",@VenueID=" & ExamVenueID
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    Session("SummerExamSession") = dt
                    grdSummerExamSession.DataSource = dt
                    grdSummerExamSession.DataBind()
                    grdSummerExamSession.Visible = True
                    drpSummerExamLocation.Visible = True
                    lblSummerExamLocation.Visible = True
                Else
                    grdSummerExamSession.Visible = False
                    drpSummerExamLocation.Visible = False
                    lblSummerExamLocation.Visible = False
                    lblErrorSummerExamSelection.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPage.ErrorMsgSummerExamSelection")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub drpSummerExamLocation_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles drpSummerExamLocation.SelectedIndexChanged
            Try
                LoadSummerExamSession(drpSummerExamLocation.SelectedValue)
                CheckHisAlreadyRegister()
                ClearDataFromPaymentSummery(1)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub ClearDataFromPaymentSummery(ByVal Location As Integer)
            If Not SummerPaymentSummery Is Nothing AndAlso SummerPaymentSummery.Rows.Count > 0 Then
                Dim dr As DataRow() = SummerPaymentSummery.Select("Location=" & Location)
                If dr.Length > 0 Then
                    For i As Integer = 0 To dr.Length - 1
                        SummerPaymentSummery.Rows.Remove(dr(i))
                        SummerPaymentSummery.AcceptChanges()
                    Next
                    radSummerPaymentSummery.DataSource = SummerPaymentSummery
                    radSummerPaymentSummery.DataBind()
                    If Not SummerPaymentSummery Is Nothing AndAlso SummerPaymentSummery.Rows.Count > 0 Then
                        radSummerPaymentSummery.Visible = True
                        lblAmount.Visible = True
                        lblTotalAmount.Visible = True
                        Dim dAmt As Decimal = 0
                        Dim dTaxAmount As Decimal = 0
                        For Each drPaymentRow As DataRow In SummerPaymentSummery.Rows
                            If dAmt = 0 Then
                                dAmt = Convert.ToString(drPaymentRow("Price")).Substring(1, Convert.ToString(drPaymentRow("Price")).Length - 1)
                            Else
                                dAmt = Convert.ToDecimal(dAmt + Convert.ToString(drPaymentRow("Price")).Substring(1, Convert.ToString(drPaymentRow("Price")).Length - 1))
                            End If

                            If dTaxAmount = 0 Then
                                dTaxAmount = CDec(drPaymentRow("Tax"))
                            Else
                                dTaxAmount = dTaxAmount + CDec(drPaymentRow("Tax"))
                            End If
                        Next
                        lblTaxAmount.Text = dTaxAmount
                        dAmt = dAmt + dTaxAmount
                        lblTotalAmount.Text = ViewState("CurrencyTypeID") & Format(CDec(dAmt), "0.00")
                    Else
                        radSummerPaymentSummery.Visible = False
                        lblAmount.Visible = False
                        lblTotalAmount.Visible = False

                    End If

                End If

            End If
        End Sub
        ''' <summary>
        ''' Load Autumn Location
        ''' </summary>
        ''' <remarks></remarks>
        ''' 

        Private Sub LoadAutumExamLocation()
            Try
                Dim sSql As String = Database & "..spGetExternalStudentExamAutumLocation__c @AcademicCycleID=" & ViewState("AcademicCycleID")
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    drpAutumnExamLocation.DataSource = dt
                    drpAutumnExamLocation.DataTextField = "Venue"
                    drpAutumnExamLocation.DataValueField = "ID"
                    drpAutumnExamLocation.DataBind()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' As per Autumn Location display Autumn exam
        ''' </summary>
        ''' <param name="ExamVenueID"></param>
        ''' <remarks></remarks>
        ''' 

        Protected Sub LoadAutumExamSession(ByVal ExamVenueID As Long)
            Try
                Dim sSql As String = Database & "..spGetExternalStudentExamAutumDetails__c @AcademicCycleID=" & ViewState("AcademicCycleID") & ",@VenueID=" & ExamVenueID
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    Session("AutumnDT") = dt
                    grdAutumExamSession.DataSource = dt
                    grdAutumExamSession.DataBind()
                    grdAutumExamSession.Visible = True
                    drpAutumnExamLocation.Visible = True
                    lblAutumnExamLocation.Visible = True
                Else
                    grdAutumExamSession.Visible = False
                    drpAutumnExamLocation.Visible = False
                    lblAutumnExamLocation.Visible = False
                    lblAutumExamSession.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPage.ErrorMsgAutumExamSelection")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub drpAutumnExamLocation_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles drpAutumnExamLocation.SelectedIndexChanged
            Try
                LoadAutumExamSession(drpAutumnExamLocation.SelectedValue)
                CheckHisAlreadyRegister()
                ClearDataFromPaymentSummery(2)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Load Revision Course Location
        ''' </summary>
        ''' <remarks></remarks>
        ''' 

        Private Sub LoadRevisionCourseLocation()
            Try
                Dim sSql As String = Database & "..spGetExternalStudentCourseLocation__c @AcademicCycleID=" & ViewState("AcademicCycleID")
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    drpCourseLocation.DataSource = dt
                    drpCourseLocation.DataTextField = "Name"
                    drpCourseLocation.DataValueField = "ID"
                    drpCourseLocation.DataBind()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Display Revision Courses
        ''' </summary>
        ''' <param name="StudentGroupID"></param>
        ''' <remarks></remarks>
        ''' 

        Protected Overridable Sub LoadRevisionCourseSession(ByVal StudentGroupID As Long)
            Try
                Dim sSql As String = Database & "..spGetExternalStudentRevisionCourseDetails__c @AcademicCycleID=" & ViewState("AcademicCycleID") & ",@StudentGrpID=" & StudentGroupID
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    grdCourseSelection.DataSource = dt
                    grdCourseSelection.DataBind()
                    grdCourseSelection.Visible = True
                Else
                    grdCourseSelection.Visible = False
                    lblRevisionCourse.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPage.ErrorMsgRevisionCourseSelection")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub drpCourseLocation_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles drpCourseLocation.SelectedIndexChanged
            Try
                LoadRevisionCourseSession(drpCourseLocation.SelectedValue)
                CheckHisAlreadyRegister()
                'CheckAlternateLocationForCourse()
                ClearDataFromPaymentSummery(3)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub CheckAlternateLocationForCourse()
            Try
                Dim lblComments As Label
                For Each row As Telerik.Web.UI.GridItem In grdCourseSelection.Items
                    lblComments = DirectCast(row.FindControl("lblComments"), Label)
                    Dim lblClassID As Label = DirectCast(row.FindControl("lblClassID"), Label)
                    Dim sSql As String = Database & "..spGetChangeLocationGroupInExam__c @ClassID=" & lblClassID.Text & ",@AcademicCycleID=" & ViewState("AcademicCycleID") & ",@GroupID=" & drpCourseLocation.SelectedValue
                    Dim iGroupID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    If iGroupID = drpCourseLocation.SelectedValue Then
                        lblComments.Text = ""
                    Else
                        lblComments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPage.FindAlternateLocationOnExam")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    End If
                Next
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''Protected Sub grdSummerExamSession_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles grdSummerExamSession.ItemDataBound
        ''    Try
        ''        If e.Item.ItemType = GridItemType.Item OrElse e.Item.ItemType = GridItemType.AlternatingItem Then
        ''            Dim lblSummerCourseID As Label
        ''            Dim lblComments As Label
        ''            lblSummerCourseID = DirectCast(e.Item.FindControl("lblSummerCourseID"), Label)
        ''            lblComments = DirectCast(e.Item.FindControl("lblComments"), Label)
        ''            Dim lblClassID As Label = DirectCast(e.Item.FindControl("lblClassID"), Label)
        ''            Dim chkSummerExam As CheckBox = DirectCast(e.Item.FindControl("chkSummerExam"), CheckBox)
        ''            Dim sSql As String = Database & "..spGetChangeLocationGroupInExam__c @ClassID=" & lblClassID.Text & ",@AcademicCycleID=" & ViewState("AcademicCycleID") & ",@GroupID=" & drpSummerExamLocation.SelectedValue
        ''            Dim iGroupID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
        ''            If iGroupID = drpSummerExamLocation.SelectedValue Then
        ''                lblComments.Text = ""
        ''            Else
        ''                lblComments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPage.FindAlternateLocationOnExam")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
        ''            End If
        ''        End If
        ''    Catch ex As Exception
        ''        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        ''    End Try
        ''End Sub

        Protected Sub grdCourseSelection_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles grdCourseSelection.ItemDataBound
            Try
                If e.Item.ItemType = GridItemType.Item OrElse e.Item.ItemType = GridItemType.AlternatingItem Then
                    Dim lblSummerRevisionCourseID As Label
                    Dim lblComments As Label
                    lblSummerRevisionCourseID = DirectCast(e.Item.FindControl("lblSummerRevisionCourseID"), Label)
                    lblComments = DirectCast(e.Item.FindControl("lblComments"), Label)
                    Dim lblClassID As Label = DirectCast(e.Item.FindControl("lblClassID"), Label)
                    Dim lblCurriculumID As Label = DirectCast(e.Item.FindControl("lblCurriculumID"), Label)
                    Dim lblAutumRevisionClassID As Label = DirectCast(e.Item.FindControl("lblAutumRevisionClassID"), Label)

                    'Dim sSql As String = Database & "..spGetChangeLocationGroupInExam__c @ClassID=" & lblClassID.Text & ",@AcademicCycleID=" & ViewState("AcademicCycleID") & ",@GroupID=" & drpCourseLocation.SelectedValue
                    'Dim iGroupID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    'If iGroupID = drpCourseLocation.SelectedValue Then
                    '    lblComments.Text = ""
                    'Else
                    '    lblComments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPage.FindAlternateLocationOnExam")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    'End If
                    Dim lblIsDEBK As Label = DirectCast(e.Item.FindControl("lblIsDEBK"), Label)
                    Dim chkSummerRevisionCourse As CheckBox = DirectCast(e.Item.FindControl("chkSummerRevisionCourse"), CheckBox)
                    Dim chkAutumRevisionCourse As CheckBox = DirectCast(e.Item.FindControl("chkAutumRevisionCourse"), CheckBox)
                    ''Dim sDEBKSQL As String = Database & "..spCheckIsDEBKFailed__c @AcademicCycleID=" & Convert.ToInt32(ViewState("AcademicCycleID")) & ",@PersonID=" & User1.PersonID & ",@CourseID=" & lblSummerRevisionCourseID.Text
                    ''Dim lFailed As Long = Convert.ToInt32(DataAction.ExecuteScalar(sDEBKSQL, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    ''If lFailed > 0 Then
                    ''    lblComments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPage.FailedDEBK")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    ''    ' lblComments.Text = "You Failed in DEBK course you not applied agine "
                    ''    chkSummerRevisionCourse.Enabled = False
                    ''    chkAutumRevisionCourse.Enabled = False
                    ''End If

                    ' 
                    ' if Summer Exam Pass then validation '
                    Dim sStudetnPassCourseSql As String = Database & "..spCheckExternalStudentCoursePass__c @StudentID=" & User1.PersonID & ",@AcademicCycleID=" & Convert.ToInt32(ViewState("AcademicCycleID")) & ",@CurriculumID=" & lblCurriculumID.Text & ",@CourseID=" & lblSummerRevisionCourseID.Text & ",@Type='Exam'"
                    Dim lStudentPass As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sStudetnPassCourseSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    If lStudentPass > 0 Then
                        lblComments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SummerExamPass")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        chkSummerRevisionCourse.Enabled = False
                        chkAutumRevisionCourse.Enabled = False
                    End If

                    ' Check Summer Cuttoff date is over then not able to check summer revision exam
                    Dim sRevisionClassesCutOffSql As String = Database & "..spCheckRevisionsEnrollmentCutOffDate__c @ClassID=" & lblClassID.Text & ",@Type='Summer'" & ",@AcademicCycleID=" & Convert.ToInt32(ViewState("AcademicCycleID"))
                    Dim lClassID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sRevisionClassesCutOffSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    If lClassID <= 0 Then
                        'lblComments.Text = ""
                        chkSummerRevisionCourse.Visible = False
                    End If

                    ' Check Autumn Cuttoff date is over then not able to check summer revision exam
                    Dim sRepeatRevisionClassesCutOffSql As String = Database & "..spCheckRevisionsEnrollmentCutOffDate__c @ClassID=" & lblAutumRevisionClassID.Text & ",@Type='Autumn'" & ",@AcademicCycleID=" & Convert.ToInt32(ViewState("AcademicCycleID"))
                    Dim lRepeatClassID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sRepeatRevisionClassesCutOffSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    If lRepeatClassID <= 0 Then
                        ' lblComments.Text = ""
                        chkAutumRevisionCourse.Visible = False
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Public Sub chkAutumINtrim_CheckedChanged(sender As Object, e As EventArgs)
            Try

                Dim chkAutumINtrim As CheckBox = DirectCast(sender, CheckBox)
                '   Dim ddlRadApproval As DropDownList = DirectCast(sender, DropDownList)
                Dim item As GridDataItem = DirectCast(chkAutumINtrim.NamingContainer, GridDataItem)

                Dim lblAutomExamCurriculumID As Label
                Dim lblClassID As Label
                Dim lblComments As Label
                Dim lblAutumCourseID As Label
                'For Each row As Telerik.Web.UI.GridItem In grdAutumExamSession.Items
                chkAutumINtrim = DirectCast(item.FindControl("chkAutumINtrim"), CheckBox)
                lblAutomExamCurriculumID = DirectCast(item.FindControl("lblAutomExamCurriculumID"), Label)
                lblClassID = DirectCast(item.FindControl("lblClassID"), Label)
                lblComments = DirectCast(item.FindControl("lblComments"), Label)
                lblAutumCourseID = DirectCast(item.FindControl("lblAutumCourseID"), Label)
                Dim lblAutumInterimClassID As Label = DirectCast(item.FindControl("lblAutumInterimClassID"), Label)
                Dim lblAutumInterimProductID As Label = DirectCast(item.FindControl("lblAutumInterimProductID"), Label)
                Dim lblAutumInterimClassName As Label = DirectCast(item.FindControl("lblAutumInterimClassName"), Label)
                lblComments.Text = ""
                Dim sSql As String = Database & "..spCheckExamattemptStudent__c @StudentID=" & User1.PersonID & ",@AcademicCycleID=" & Convert.ToInt32(ViewState("AcademicCycleID")) & ",@CurriculumID=" & lblAutomExamCurriculumID.Text & ",@CourseID=" & lblAutumCourseID.Text & ",@Type='Interim Assessment'"
                Dim lClassRegID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If lClassRegID > 0 Then
                    If chkAutumINtrim.Checked = True Then

                        Dim sSqlAutumInterim As String = Database & "..spGetChangeLocationGroupInExam__c @ClassID=" & lblAutumInterimClassID.Text & ",@AcademicCycleID=" & ViewState("AcademicCycleID") & ",@GroupID=" & drpAutumnExamLocation.SelectedValue
                        Dim iGroupID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSqlAutumInterim, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        If iGroupID = drpAutumnExamLocation.SelectedValue Then
                            lblComments.Text = ""
                        Else
                            lblComments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPage.FindAlternateLocationOnExam")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        End If
                        Dim sDEBKSQL As String = Database & "..spCheckIsDEBKFailed__c @AcademicCycleID=" & Convert.ToInt32(ViewState("AcademicCycleID")) & ",@PersonID=" & User1.PersonID & ",@CourseID=" & lblAutumCourseID.Text
                        Dim lFailed As Long = Convert.ToInt32(DataAction.ExecuteScalar(sDEBKSQL, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        If lFailed > 0 Then
                        Else
                            lblComments.Text = lblComments.Text + " " + Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPage.SummerIntrimErrorMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)

                        End If
                        Dim dtSummerPaymentSummery As DataTable = SummerPaymentSummery
                        Dim drSummerPaymentSummery As DataRow = dtSummerPaymentSummery.NewRow()
                        drSummerPaymentSummery("Class") = Convert.ToString(lblAutumInterimClassName.Text)
                        drSummerPaymentSummery("ProductID") = lblAutumInterimProductID.Text
                        Dim dAutumnInterimExamTax As Decimal = 0
                        drSummerPaymentSummery("Price") = GetPrice(Convert.ToInt32(lblAutumInterimProductID.Text), Convert.ToInt32(lblAutumInterimClassID.Text), drpAutumnExamLocation.SelectedValue, dAutumnInterimExamTax)
                        drSummerPaymentSummery("Tax") = dAutumnInterimExamTax
                        drSummerPaymentSummery("Location") = 2
                        dtSummerPaymentSummery.Rows.Add(drSummerPaymentSummery)
                        SummerPaymentSummery = dtSummerPaymentSummery
                    Else
                        lblComments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPage.SummerIntrimscoreMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        If Not SummerPaymentSummery Is Nothing AndAlso SummerPaymentSummery.Rows.Count > 0 Then
                            Dim dr() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(lblAutumInterimProductID.Text))
                            If dr.Length > 0 Then
                                SummerPaymentSummery.Rows.Remove(dr(0))
                                SummerPaymentSummery.AcceptChanges()
                            End If

                        End If
                    End If
                    If Not SummerPaymentSummery Is Nothing AndAlso SummerPaymentSummery.Rows.Count > 0 Then
                        radSummerPaymentSummery.DataSource = SummerPaymentSummery
                        radSummerPaymentSummery.DataBind()
                        radSummerPaymentSummery.Visible = True
                        lblAmount.Visible = True
                        lblTotalAmount.Visible = True
                        Dim dAmt As Decimal = 0
                        Dim dTaxAmount As Decimal = 0
                        For Each dr As DataRow In SummerPaymentSummery.Rows
                            If dAmt = 0 Then
                                dAmt = Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1)
                            Else
                                dAmt = Convert.ToDecimal(dAmt + Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1))
                            End If
                            If dTaxAmount = 0 Then
                                dTaxAmount = CDec(dr("Tax"))
                            Else
                                dTaxAmount = dTaxAmount + CDec(dr("Tax"))
                            End If
                        Next
                        dAmt = dAmt + dTaxAmount
                        lblTotalAmount.Text = ViewState("CurrencyTypeID") & Format(CDec(dAmt), "0.00")
                        lblTaxAmount.Text = dTaxAmount
                    Else
                        radSummerPaymentSummery.Visible = False
                        lblAmount.Visible = False
                        lblTotalAmount.Visible = False
                    End If
                Else
                    'If chkAutumINtrim.Checked = True Then
                    '    lblComments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPageIntrimMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    '    chkAutumINtrim.Checked = False
                    'End If
                End If
                '' is DEBK check
                'Dim lblIsDEBK As Label = DirectCast(item.FindControl("lblIsDEBK"), Label)
                'Dim chkAutumExam As CheckBox = DirectCast(item.FindControl("chkAutumExam"), CheckBox)
                'Dim sDEBKSQL As String = Database & "..spCheckIsDEBKFailed__c @AcademicCycleID=" & Convert.ToInt32(ViewState("AcademicCycleID")) & ",@PersonID=" & User1.PersonID & ",@CourseID=" & lblAutumCourseID.Text
                'Dim lFailed As Long = Convert.ToInt32(DataAction.ExecuteScalar(sDEBKSQL, IAptifyDataAction.DSLCacheSetting.BypassCache))
                'If lFailed > 0 Then
                '    lblComments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPage.FailedDEBK")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                '    chkAutumINtrim.Enabled = False
                '    chkAutumINtrim.Enabled = False
                'End If
                ' Next
                UpdatePanel1.Update()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Public Sub chkAutumExam_CheckedChanged(sender As Object, e As EventArgs)
            Try
                Dim chkAutumExam As CheckBox = DirectCast(sender, CheckBox)
                '   Dim ddlRadApproval As DropDownList = DirectCast(sender, DropDownList)
                Dim item As GridDataItem = DirectCast(chkAutumExam.NamingContainer, GridDataItem)
                Dim lblComments As Label = DirectCast(item.FindControl("lblComments"), Label)
                Dim lblAutomExamCurriculumID As Label = DirectCast(item.FindControl("lblAutomExamCurriculumID"), Label)
                Dim lblClassID As Label = DirectCast(item.FindControl("lblClassID"), Label)
                Dim lblProductID As Label = DirectCast(item.FindControl("lblProductID"), Label)
                Dim lblAutumCourseID As Label = DirectCast(item.FindControl("lblAutumCourseID"), Label)
                Dim chkAutumINtrim As CheckBox = DirectCast(item.FindControl("chkAutumINtrim"), CheckBox)
                Dim lblAutumInterimClassID As Label = DirectCast(item.FindControl("lblAutumInterimClassID"), Label)
                Dim lblAutumInterimProductID As Label = DirectCast(item.FindControl("lblAutumInterimProductID"), Label)
                lblComments.Text = ""
                If chkAutumExam.Checked = True Then

                    ' Check Summer exam is enrolled or not

                    Dim sSql As String = Database & "..spCheckExamattemptStudent__c @StudentID=" & User1.PersonID & ",@AcademicCycleID=" & Convert.ToInt32(ViewState("AcademicCycleID")) & ",@CurriculumID=" & lblAutomExamCurriculumID.Text & ",@CourseID=" & lblAutumCourseID.Text & ",@Type='Exam'"
                    Dim lClassRegID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    If lClassRegID > 0 Then

                        chkAutumExam.Enabled = True
                        'Redmine Issue #16029
                        If Convert.ToInt32(lblAutumInterimProductID.Text) > 0 AndAlso chkAutumINtrim.Visible = True Then
                            chkAutumINtrim.Checked = True
                            chkAutumINtrim.Enabled = True
                        End If
                        If chkAutumINtrim.Checked = False Then
                            sSql = Database & "..spCheckExamattemptStudent__c @StudentID=" & User1.PersonID & ",@AcademicCycleID=" & Convert.ToInt32(ViewState("AcademicCycleID")) & ",@CurriculumID=" & lblAutomExamCurriculumID.Text & ",@CourseID=" & lblAutumCourseID.Text & ",@Type='Interim Assessment'"
                            lClassRegID = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                            If lClassRegID > 0 Then
                                lblComments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPage.SummerIntrimscoreMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            Else
                                lblComments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPage.NotSelectedInterim")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            End If
                        ElseIf chkAutumINtrim.Checked = True Then
                            ' Summer Interim score will be discard and Autumn will be Used
                            Dim sSqlInterim As String = Database & "..spCheckExamattemptStudent__c @StudentID=" & User1.PersonID & ",@AcademicCycleID=" & Convert.ToInt32(ViewState("AcademicCycleID")) & ",@CurriculumID=" & lblAutomExamCurriculumID.Text & ",@CourseID=" & lblAutumCourseID.Text & ",@Type='Interim Assessment'"
                            Dim lClassRegIDForInterim As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSqlInterim, IAptifyDataAction.DSLCacheSetting.BypassCache))
                            If lClassRegIDForInterim > 0 Then
                                lblComments.Text = lblComments.Text + " " + Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPage.SummerIntrimErrorMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            End If

                        End If
                        Dim sSqlAutumnExam As String = Database & "..spGetChangeLocationGroupInExam__c @ClassID=" & lblClassID.Text & ",@AcademicCycleID=" & ViewState("AcademicCycleID") & ",@GroupID=" & drpAutumnExamLocation.SelectedValue
                        Dim iGroupID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSqlAutumnExam, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        If iGroupID = drpAutumnExamLocation.SelectedValue Then
                            lblComments.Text = lblComments.Text + ""
                        Else
                            lblComments.Text = lblComments.Text + "," + Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPage.FindAlternateLocationOnExam")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        End If
                        If Convert.ToInt32(lblAutumInterimProductID.Text) > 0 AndAlso chkAutumINtrim.Visible = True Then
                            ViewState("InterimForceAdded") = True
                        Else
                            ViewState("InterimForceAdded") = False
                        End If
                    Else
                        ' if external students wants comes directly Autumn exam then resticed them add Autumn Interim
                        chkAutumINtrim.Enabled = False
                        If Convert.ToInt32(lblAutumInterimClassID.Text) > 0 Then
                            chkAutumINtrim.Checked = True
                            ViewState("InterimForceAdded") = True
                            lblComments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPage.AutomaticRegistered")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            Dim sSqlAutumnExam As String = Database & "..spGetChangeLocationGroupInExam__c @ClassID=" & lblClassID.Text & ",@AcademicCycleID=" & ViewState("AcademicCycleID") & ",@GroupID=" & drpAutumnExamLocation.SelectedValue
                            Dim iGroupID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSqlAutumnExam, IAptifyDataAction.DSLCacheSetting.BypassCache))
                            If iGroupID = drpAutumnExamLocation.SelectedValue Then
                                lblComments.Text = lblComments.Text + ""
                            Else
                                lblComments.Text = lblComments.Text + "," + Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPage.FindAlternateLocationOnExam")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)

                            End If
                        Else
                            ViewState("InterimForceAdded") = False
                        End If

                        'lblComments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPageExamMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        'chkAutumExam.Enabled = False
                        'chkAutumExam.Checked = False
                    End If
                    Dim SummerExamDt As DataTable = CType(Session("SummerExamSession"), DataTable)
                    If Not SummerExamDt Is Nothing AndAlso SummerExamDt.Rows.Count > 0 Then
                        For Each row1 As Telerik.Web.UI.GridItem In grdSummerExamSession.Items
                            Dim lblSummerCourseID As Label = DirectCast(row1.FindControl("lblSummerCourseID"), Label)
                            Dim chkSummerExam As CheckBox = DirectCast(row1.FindControl("chkSummerExam"), CheckBox)
                            If Convert.ToInt32(lblSummerCourseID.Text) = Convert.ToInt32(lblAutumCourseID.Text) AndAlso chkSummerExam.Checked = True Then
                                lblComments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.AlreadySelectSummerExamMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                chkAutumExam.Checked = False
                                chkAutumINtrim.Checked = False
                                If Not SummerPaymentSummery Is Nothing AndAlso SummerPaymentSummery.Rows.Count > 0 Then
                                    If lblProductID.Text <> "" Then
                                        Dim dr() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(lblProductID.Text))
                                        If dr.Length > 0 Then
                                            SummerPaymentSummery.Rows.Remove(dr(0))
                                            SummerPaymentSummery.AcceptChanges()
                                        End If

                                    End If
                                    If lblAutumInterimProductID.Text <> "" Then
                                        Dim dr1() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(lblAutumInterimProductID.Text))
                                        If dr1.Length > 0 Then
                                            SummerPaymentSummery.Rows.Remove(dr1(0))
                                            SummerPaymentSummery.AcceptChanges()
                                        End If

                                    End If
                                End If
                                Exit For
                            Else
                                'lblComments.Text = ""
                            End If
                        Next
                    End If

                    'code added by GM for Redmine #20187
                    Dim sProductId As String = ""
                    Dim AutumnExamDt As DataTable = CType(Session("AutumnDT"), DataTable)
                    Dim lblPaper As Label = DirectCast(item.FindControl("lblPaper"), Label)
                    If lblPaper.Text = "CAP1 Taxation (ROI)" OrElse lblPaper.Text = "CAP1 Taxation (NI)" OrElse lblPaper.Text = "CAP1 Law (ROI)" OrElse lblPaper.Text = "CAP1 Law (NI)" Then
                        If Not AutumnExamDt Is Nothing AndAlso AutumnExamDt.Rows.Count > 0 Then
                            For Each row1 As Telerik.Web.UI.GridItem In grdAutumExamSession.Items
                                Dim lblAutumnCourseName As Label = DirectCast(row1.FindControl("lblPaper"), Label)
                                Dim chkAutumnCourse As CheckBox = DirectCast(row1.FindControl("chkAutumExam"), CheckBox)
                                Dim chkAutumINtrimSession As CheckBox = DirectCast(row1.FindControl("chkAutumINtrim"), CheckBox)
                                Dim lblAutumInterimProdID As Label = DirectCast(row1.FindControl("lblAutumInterimProductID"), Label)
                                If lblPaper.Text = "CAP1 Taxation (NI)" Then
                                    If (lblAutumnCourseName.Text = "CAP1 Taxation (ROI)" OrElse lblAutumnCourseName.Text = "CAP1 Law (ROI)") AndAlso chkAutumnCourse.Checked = True Then
                                        chkAutumExam.Checked = False
                                        lblComments.Text = String.Format(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(
                                        Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalApp.CourseValidation__c")),
                                        Convert.ToInt32(AptifyApplication.UserCredentials.CultureID), DataAction.UserCredentials), lblAutumnCourseName.Text)
                                        sProductId = lblProductID.Text
                                        Dim dr() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(lblProductID.Text))
                                        If dr.Length > 0 Then
                                            SummerPaymentSummery.Rows.Remove(dr(0))
                                            SummerPaymentSummery.AcceptChanges()
                                        End If
                                        If chkAutumINtrimSession.Checked Then
                                            Dim dr1() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(lblAutumInterimProdID.Text))
                                            If dr1.Length > 0 Then
                                                SummerPaymentSummery.Rows.Remove(dr1(0))
                                                SummerPaymentSummery.AcceptChanges()
                                            End If
                                        End If
                                    End If
                                ElseIf lblPaper.Text = "CAP1 Taxation (ROI)" Then
                                    If (lblAutumnCourseName.Text = "CAP1 Taxation (NI)" OrElse lblAutumnCourseName.Text = "CAP1 Law (NI)") AndAlso chkAutumnCourse.Checked = True Then
                                        chkAutumExam.Checked = False
                                        lblComments.Text = String.Format(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(
                                        Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalApp.CourseValidation__c")),
                                        Convert.ToInt32(AptifyApplication.UserCredentials.CultureID), DataAction.UserCredentials), lblAutumnCourseName.Text)
                                        sProductId = lblProductID.Text
                                        Dim dr() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(lblProductID.Text))
                                        If dr.Length > 0 Then
                                            SummerPaymentSummery.Rows.Remove(dr(0))
                                            SummerPaymentSummery.AcceptChanges()
                                        End If
                                        If chkAutumINtrimSession.Checked Then
                                            Dim dr1() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(lblAutumInterimProdID.Text))
                                            If dr1.Length > 0 Then
                                                SummerPaymentSummery.Rows.Remove(dr1(0))
                                                SummerPaymentSummery.AcceptChanges()
                                            End If
                                        End If
                                    End If
                                ElseIf lblPaper.Text = "CAP1 Law (ROI)" Then
                                    If (lblAutumnCourseName.Text = "CAP1 Law (NI)" OrElse lblAutumnCourseName.Text = "CAP1 Taxation (NI)") AndAlso chkAutumnCourse.Checked = True Then
                                        chkAutumExam.Checked = False
                                        lblComments.Text = String.Format(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(
                                        Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalApp.CourseValidation__c")),
                                        Convert.ToInt32(AptifyApplication.UserCredentials.CultureID), DataAction.UserCredentials), lblAutumnCourseName.Text)
                                        sProductId = lblProductID.Text
                                        Dim dr() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(lblProductID.Text))
                                        If dr.Length > 0 Then
                                            SummerPaymentSummery.Rows.Remove(dr(0))
                                            SummerPaymentSummery.AcceptChanges()
                                        End If
                                        If chkAutumINtrimSession.Checked Then
                                            Dim dr1() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(lblAutumInterimProdID.Text))
                                            If dr1.Length > 0 Then
                                                SummerPaymentSummery.Rows.Remove(dr1(0))
                                                SummerPaymentSummery.AcceptChanges()
                                            End If
                                        End If
                                    End If
                                ElseIf lblPaper.Text = "CAP1 Law (NI)" Then
                                    If (lblAutumnCourseName.Text = "CAP1 Law (ROI)" OrElse lblAutumnCourseName.Text = "CAP1 Taxation (ROI)") AndAlso chkAutumnCourse.Checked = True Then
                                        chkAutumExam.Checked = False
                                        lblComments.Text = String.Format(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(
                                        Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalApp.CourseValidation__c")),
                                        Convert.ToInt32(AptifyApplication.UserCredentials.CultureID), DataAction.UserCredentials), lblAutumnCourseName.Text)
                                        sProductId = lblProductID.Text
                                        Dim dr() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(lblProductID.Text))
                                        If dr.Length > 0 Then
                                            SummerPaymentSummery.Rows.Remove(dr(0))
                                            SummerPaymentSummery.AcceptChanges()
                                        End If
                                        If chkAutumINtrimSession.Checked Then
                                            Dim dr1() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(lblAutumInterimProdID.Text))
                                            If dr1.Length > 0 Then
                                                SummerPaymentSummery.Rows.Remove(dr1(0))
                                                SummerPaymentSummery.AcceptChanges()
                                            End If
                                        End If
                                    End If
                                End If

                            Next

                        End If
                    End If
                    'End Redmine #20187
                    Dim lblAutumExamClassName As Label = DirectCast(item.FindControl("lblAutumExamClassName"), Label)
                    Dim lblAutumInterimClassName As Label = DirectCast(item.FindControl("lblAutumInterimClassName"), Label)

                    Dim dtSummerPaymentSummery As DataTable = SummerPaymentSummery
                    If chkAutumExam.Checked Then
                        If sProductId <> lblProductID.Text Then  ' If condition added for Redmine #20187
                            Dim drSummerPaymentSummery As DataRow = dtSummerPaymentSummery.NewRow()
                            drSummerPaymentSummery("Class") = Convert.ToString(lblAutumExamClassName.Text)
                            drSummerPaymentSummery("ProductID") = lblProductID.Text
                            Dim dAutumnExamTax As Decimal = 0
                            drSummerPaymentSummery("Price") = GetPrice(Convert.ToInt32(lblProductID.Text), Convert.ToInt32(lblClassID.Text), drpAutumnExamLocation.SelectedValue, dAutumnExamTax)
                            drSummerPaymentSummery("Tax") = dAutumnExamTax
                            drSummerPaymentSummery("Location") = 2
                            dtSummerPaymentSummery.Rows.Add(drSummerPaymentSummery)
                            SummerPaymentSummery = dtSummerPaymentSummery
                            If ViewState("InterimForceAdded") = True Then
                                Dim drSummerPaymentSummery1 As DataRow = dtSummerPaymentSummery.NewRow()
                                drSummerPaymentSummery1("Class") = Convert.ToString(lblAutumInterimClassName.Text)
                                drSummerPaymentSummery1("ProductID") = lblAutumInterimProductID.Text
                                Dim dInterimExamTax As Decimal = 0
                                drSummerPaymentSummery1("Price") = GetPrice(Convert.ToInt32(lblAutumInterimProductID.Text), Convert.ToInt32(lblAutumInterimClassID.Text), drpAutumnExamLocation.SelectedValue, dInterimExamTax)
                                drSummerPaymentSummery1("Tax") = dInterimExamTax
                                drSummerPaymentSummery1("Location") = 2
                                dtSummerPaymentSummery.Rows.Add(drSummerPaymentSummery1)
                                SummerPaymentSummery = dtSummerPaymentSummery
                            End If
                        End If 'End if Redmine #20187
                    Else
                        'If Not SummerPaymentSummery Is Nothing AndAlso SummerPaymentSummery.Rows.Count > 0 Then
                        '    Dim dr() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(lblProductID.Text))
                        '    SummerPaymentSummery.Rows.Remove(dr(0))
                        '    SummerPaymentSummery.AcceptChanges()
                        'End If
                    End If
                    If Not SummerPaymentSummery Is Nothing AndAlso SummerPaymentSummery.Rows.Count > 0 Then
                        radSummerPaymentSummery.Visible = True
                        radSummerPaymentSummery.DataSource = SummerPaymentSummery
                        radSummerPaymentSummery.DataBind()
                        lblAmount.Visible = True
                        lblTotalAmount.Visible = True
                        Dim dAmt As Decimal = 0
                        Dim dTaxAmount As Decimal = 0
                        For Each dr As DataRow In SummerPaymentSummery.Rows
                            If dAmt = 0 Then
                                dAmt = Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1)
                            Else
                                dAmt = Convert.ToDecimal(dAmt + Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1))
                            End If
                            If dTaxAmount = 0 Then
                                dTaxAmount = CDec(dr("Tax"))
                            Else
                                dTaxAmount = dTaxAmount + CDec(dr("Tax"))
                            End If
                        Next
                        dAmt = dAmt + dTaxAmount
                        lblTotalAmount.Text = ViewState("CurrencyTypeID") & Format(CDec(dAmt), "0.00")
                        lblTaxAmount.Text = dTaxAmount
                    Else
                        radSummerPaymentSummery.Visible = False
                        lblAmount.Visible = False
                        lblTotalAmount.Visible = False
                    End If
                Else
                    If Not SummerPaymentSummery Is Nothing AndAlso SummerPaymentSummery.Rows.Count > 0 Then
                        Dim dr() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(lblProductID.Text))
                        SummerPaymentSummery.Rows.Remove(dr(0))
                        SummerPaymentSummery.AcceptChanges()
                        If ViewState("InterimForceAdded") = True Then
                            If lblAutumInterimProductID.Text <> "" Then
                                Dim dr1() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(lblAutumInterimProductID.Text))
                                If dr1.Length > 0 Then
                                    SummerPaymentSummery.Rows.Remove(dr1(0))
                                    SummerPaymentSummery.AcceptChanges()
                                End If

                            End If
                        End If

                    End If

                    chkAutumINtrim.Checked = False
                    lblComments.Text = ""
                    'Redmin #16029
                    chkAutumINtrim.Enabled = False
                End If
                If Not SummerPaymentSummery Is Nothing AndAlso SummerPaymentSummery.Rows.Count > 0 Then
                    radSummerPaymentSummery.Visible = True
                    radSummerPaymentSummery.DataSource = SummerPaymentSummery
                    radSummerPaymentSummery.DataBind()
                    lblAmount.Visible = True
                    lblTotalAmount.Visible = True
                    Dim dAmt As Decimal = 0
                    Dim dTaxAmount As Decimal = 0
                    For Each dr As DataRow In SummerPaymentSummery.Rows
                        If dAmt = 0 Then
                            dAmt = Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1)
                        Else
                            dAmt = Convert.ToDecimal(dAmt + Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1))
                        End If
                        If dTaxAmount = 0 Then
                            dTaxAmount = CDec(dr("Tax"))
                        Else
                            dTaxAmount = dTaxAmount + CDec(dr("Tax"))
                        End If
                    Next
                    dAmt = dAmt + dTaxAmount
                    lblTotalAmount.Text = ViewState("CurrencyTypeID") & Format(CDec(dAmt), "0.00")
                    lblTaxAmount.Text = dTaxAmount
                Else
                    radSummerPaymentSummery.Visible = False
                    lblAmount.Visible = False
                    lblTotalAmount.Visible = False
                End If
                UpdatePanel1.Update()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub grdAutumExamSession_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles grdAutumExamSession.ItemDataBound
            Try
                If e.Item.ItemType = GridItemType.Item OrElse e.Item.ItemType = GridItemType.AlternatingItem Then
                    Dim lblAutomExamCurriculumID As Label = DirectCast(e.Item.FindControl("lblAutomExamCurriculumID"), Label)
                    Dim lblAutumCourseID As Label = DirectCast(e.Item.FindControl("lblAutumCourseID"), Label)
                    Dim lblComments As Label = DirectCast(e.Item.FindControl("lblComments"), Label)
                    Dim chkAutumExam As CheckBox = DirectCast(e.Item.FindControl("chkAutumExam"), CheckBox)
                    Dim chkAutumINtrim As CheckBox = DirectCast(e.Item.FindControl("chkAutumINtrim"), CheckBox)
                    Dim lblAutumInterimClassID As Label = DirectCast(e.Item.FindControl("lblAutumInterimClassID"), Label)
                    Dim lblClassID As Label = DirectCast(e.Item.FindControl("lblClassID"), Label)
                    If lblAutumInterimClassID.Text > 0 Then
                        chkAutumINtrim.Visible = True
                        ' Redmine Issue #16029
                        chkAutumINtrim.Enabled = False
                    Else
                        chkAutumINtrim.Visible = False
                    End If
                    ' if Summer Exam Pass then validation '
                    Dim sStudetnPassCourseSql As String = Database & "..spCheckExternalStudentCoursePass__c @StudentID=" & User1.PersonID & ",@AcademicCycleID=" & Convert.ToInt32(ViewState("AcademicCycleID")) & ",@CurriculumID=" & lblAutomExamCurriculumID.Text & ",@CourseID=" & lblAutumCourseID.Text & ",@Type='Exam'"
                    Dim lStudentPass As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sStudetnPassCourseSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    If lStudentPass > 0 Then
                        ' lblComments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SummerExamPass")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        chkAutumExam.Visible = False
                    End If

                    ' if Summer Interim Assessment Pass then validation '
                    ' Code commented for redmine issue 16029
                    ''sStudetnPassCourseSql = Database & "..spCheckExternalStudentCoursePass__c @StudentID=" & User1.PersonID & ",@AcademicCycleID=" & Convert.ToInt32(ViewState("AcademicCycleID")) & ",@CurriculumID=" & lblAutomExamCurriculumID.Text & ",@CourseID=" & lblAutumCourseID.Text & ",@Type='Interim Assessment'"
                    ''Dim lStudentInterimPass As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sStudetnPassCourseSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    ''If lStudentInterimPass > 0 Then
                    ''    '  lblComments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SummerExamPass")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    ''    chkAutumINtrim.Visible = False
                    ''End If
                    ' Addec code for if DEBK passed in Summer then should not have option to sit in Autumn as redmine #16029
                    Dim sDEBKPassSQL As String = Database & "..spCheckIsDEBKPassed__c @PersonID=" & User1.PersonID & ",@CourseID=" & lblAutumCourseID.Text
                    Dim lsDEBKPassSQL As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sDEBKPassSQL, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    If lsDEBKPassSQL > 0 Then
                        '  lblComments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SummerExamPass")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        chkAutumINtrim.Visible = False
                    End If

                    Dim sDEBKSQL As String = Database & "..spCheckIsDEBKFailed__c @AcademicCycleID=" & Convert.ToInt32(ViewState("AcademicCycleID")) & ",@PersonID=" & User1.PersonID & ",@CourseID=" & lblAutumCourseID.Text
                    Dim lFailed As Long = Convert.ToInt32(DataAction.ExecuteScalar(sDEBKSQL, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    If lFailed > 0 Then
                        lblComments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPage.FailedDEBK")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        chkAutumExam.Enabled = False
                    End If


                    If chkAutumExam.Visible = True AndAlso chkAutumINtrim.Visible = True Then
                        ' Check Summer exam is enrolled or not
                        Dim sSql As String = Database & "..spCheckExamattemptStudent__c @StudentID=" & User1.PersonID & ",@AcademicCycleID=" & Convert.ToInt32(ViewState("AcademicCycleID")) & ",@CurriculumID=" & lblAutomExamCurriculumID.Text & ",@CourseID=" & lblAutumCourseID.Text & ",@Type='Exam'"
                        Dim lClassRegID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                        If lClassRegID > 0 Then
                        Else
                            chkAutumINtrim.Enabled = False
                        End If
                    End If

                    ' Check Autumn Cuttoff date is over then not able to check summer revision exam
                    Dim sRepeatRevisionClassesCutOffSql As String = Database & "..spCheckRevisionsEnrollmentCutOffDate__c @ClassID=" & lblClassID.Text & ",@Type='Autumn'" & ",@AcademicCycleID=" & Convert.ToInt32(ViewState("AcademicCycleID"))
                    Dim lRepeatClassID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sRepeatRevisionClassesCutOffSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    If lRepeatClassID <= 0 Then
                        ' lblComments.Text = ""
                        chkAutumExam.Visible = False
                        chkAutumINtrim.Visible = False
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Public Sub chkSummerExam_CheckedChanged(sender As Object, e As EventArgs)
            Try
                Dim chkSummerExam As CheckBox = DirectCast(sender, CheckBox)
                '   Dim ddlRadApproval As DropDownList = DirectCast(sender, DropDownList)
                Dim item As GridDataItem = DirectCast(chkSummerExam.NamingContainer, GridDataItem)
                Dim lblComments As Label = DirectCast(item.FindControl("lblComments"), Label)
                Dim lblSummerCourseID As Label = DirectCast(item.FindControl("lblSummerCourseID"), Label)
                Dim lblProductID As Label = DirectCast(item.FindControl("lblProductID"), Label)
                Dim lblSummerIntrimProductID As Label = DirectCast(item.FindControl("lblSummerIntrimProductID"), Label)
                If chkSummerExam.Checked = True Then
                    ' For Performance
                    Dim AutumnDT As DataTable = CType(Session("AutumnDT"), DataTable)
                    If Not AutumnDT Is Nothing AndAlso AutumnDT.Rows.Count > 0 Then
                        For Each row1 As Telerik.Web.UI.GridItem In grdAutumExamSession.Items
                            Dim lblAutumCourseID As Label = DirectCast(row1.FindControl("lblAutumCourseID"), Label)
                            Dim lblAutumComments As Label = DirectCast(row1.FindControl("lblComments"), Label)
                            Dim chkAutumExam As CheckBox = DirectCast(row1.FindControl("chkAutumExam"), CheckBox)
                            Dim chkAutumINtrim As CheckBox = DirectCast(row1.FindControl("chkAutumINtrim"), CheckBox)
                            Dim lblAutumExamProductID As Label = DirectCast(row1.FindControl("lblProductID"), Label)
                            Dim lblAutumInterimProductID As Label = DirectCast(row1.FindControl("lblAutumInterimProductID"), Label)
                            If Convert.ToInt32(lblSummerCourseID.Text) = Convert.ToInt32(lblAutumCourseID.Text) AndAlso chkAutumExam.Checked = True Then
                                'lblComments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.AlreadySelectSummerExamMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                lblComments.Text = ""
                                chkAutumExam.Checked = False
                                chkAutumINtrim.Checked = False
                                lblAutumComments.Text = ""
                                If Not SummerPaymentSummery Is Nothing AndAlso SummerPaymentSummery.Rows.Count > 0 Then
                                    If lblAutumExamProductID.Text Then
                                        Dim dr() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(lblAutumExamProductID.Text))
                                        If dr.Length > 0 Then
                                            SummerPaymentSummery.Rows.Remove(dr(0))
                                            SummerPaymentSummery.AcceptChanges()
                                        End If
                                    End If

                                    If lblAutumInterimProductID.Text <> "" AndAlso Convert.ToInt32(lblAutumInterimProductID.Text) > 0 Then
                                        Dim dr1() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(lblAutumInterimProductID.Text))
                                        If dr1.Length > 0 Then
                                            SummerPaymentSummery.Rows.Remove(dr1(0))
                                            SummerPaymentSummery.AcceptChanges()
                                        End If

                                    End If
                                End If
                                Exit For
                            Else
                                lblComments.Text = ""
                            End If
                        Next
                    End If
                    ' added by GM for Redmine #20187
                    Dim sProductId As String = ""
                    Dim SummerExamDt As DataTable = CType(Session("SummerExamSession"), DataTable)
                    Dim lblSummerCourse As Label = DirectCast(item.FindControl("Label2"), Label)
                    If lblSummerCourse.Text = "CAP1 Taxation (ROI)" OrElse lblSummerCourse.Text = "CAP1 Taxation (NI)" OrElse lblSummerCourse.Text = "CAP1 Law (ROI)" OrElse lblSummerCourse.Text = "CAP1 Law (NI)" Then
                        If Not SummerExamDt Is Nothing AndAlso SummerExamDt.Rows.Count > 0 Then
                            For Each row1 As Telerik.Web.UI.GridItem In grdSummerExamSession.Items
                                Dim lblSummerCourseName As Label = DirectCast(row1.FindControl("Label2"), Label)
                                Dim chkSummerCourse As CheckBox = DirectCast(row1.FindControl("chkSummerExam"), CheckBox)

                                If lblSummerCourse.Text = "CAP1 Taxation (NI)" Then
                                    If (lblSummerCourseName.Text = "CAP1 Taxation (ROI)" OrElse lblSummerCourseName.Text = "CAP1 Law (ROI)") AndAlso chkSummerCourse.Checked = True Then
                                        chkSummerExam.Checked = False
                                        lblComments.Text = String.Format(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(
                                        Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalApp.CourseValidation__c")),
                                        Convert.ToInt32(AptifyApplication.UserCredentials.CultureID), DataAction.UserCredentials), lblSummerCourseName.Text)
                                        sProductId = lblProductID.Text
                                        Dim dr() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(lblProductID.Text))
                                        If dr.Length > 0 Then
                                            SummerPaymentSummery.Rows.Remove(dr(0))
                                            SummerPaymentSummery.AcceptChanges()
                                        End If
                                    End If
                                ElseIf lblSummerCourse.Text = "CAP1 Taxation (ROI)" Then
                                    If (lblSummerCourseName.Text = "CAP1 Taxation (NI)" OrElse lblSummerCourseName.Text = "CAP1 Law (NI)") AndAlso chkSummerCourse.Checked = True Then
                                        chkSummerExam.Checked = False
                                        lblComments.Text = String.Format(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(
                                        Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalApp.CourseValidation__c")),
                                        Convert.ToInt32(AptifyApplication.UserCredentials.CultureID), DataAction.UserCredentials), lblSummerCourseName.Text)
                                        sProductId = lblProductID.Text
                                        Dim dr() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(lblProductID.Text))
                                        If dr.Length > 0 Then
                                            SummerPaymentSummery.Rows.Remove(dr(0))
                                            SummerPaymentSummery.AcceptChanges()
                                        End If
                                    End If
                                ElseIf lblSummerCourse.Text = "CAP1 Law (ROI)" Then
                                    If (lblSummerCourseName.Text = "CAP1 Law (NI)" OrElse lblSummerCourseName.Text = "CAP1 Taxation (NI)") AndAlso chkSummerCourse.Checked = True Then
                                        chkSummerExam.Checked = False
                                        lblComments.Text = String.Format(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(
                                        Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalApp.CourseValidation__c")),
                                        Convert.ToInt32(AptifyApplication.UserCredentials.CultureID), DataAction.UserCredentials), lblSummerCourseName.Text)
                                        sProductId = lblProductID.Text
                                        Dim dr() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(lblProductID.Text))
                                        If dr.Length > 0 Then
                                            SummerPaymentSummery.Rows.Remove(dr(0))
                                            SummerPaymentSummery.AcceptChanges()
                                        End If
                                    End If
                                ElseIf lblSummerCourse.Text = "CAP1 Law (NI)" Then
                                    If (lblSummerCourseName.Text = "CAP1 Law (ROI)" OrElse lblSummerCourseName.Text = "CAP1 Taxation (ROI)") AndAlso chkSummerCourse.Checked = True Then
                                        chkSummerExam.Checked = False
                                        lblComments.Text = String.Format(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(
                                        Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalApp.CourseValidation__c")),
                                        Convert.ToInt32(AptifyApplication.UserCredentials.CultureID), DataAction.UserCredentials), lblSummerCourseName.Text)
                                        sProductId = lblProductID.Text
                                        Dim dr() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(lblProductID.Text))
                                        If dr.Length > 0 Then
                                            SummerPaymentSummery.Rows.Remove(dr(0))
                                            SummerPaymentSummery.AcceptChanges()
                                        End If
                                    End If
                                End If

                            Next

                        End If
                    End If
                    'End Redmine #20187

                    Dim lblClassID As Label = DirectCast(item.FindControl("lblClassID"), Label)
                    Dim sSql As String = Database & "..spGetChangeLocationGroupInExam__c @ClassID=" & lblClassID.Text & ",@AcademicCycleID=" & ViewState("AcademicCycleID") & ",@GroupID=" & drpSummerExamLocation.SelectedValue
                    Dim iGroupID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    If iGroupID = drpSummerExamLocation.SelectedValue Then
                        ' If condition added for Redmine #20187
                        If lblComments.Text <> "" Then
                        Else
                            lblComments.Text = ""
                        End If
                        'End Redmine #20187
                    Else
                        lblComments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPage.FindAlternateLocationOnExam")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    End If
                    ' Added Summer Exam Payment Summery 
                    ' For below if condition change below text location for redmine #20187
                    Dim dtSummerPaymentSummery As DataTable = SummerPaymentSummery
                    'Dim sProductName As String = AptifyApplication.GetEntityRecordName("Products", Convert.ToInt32(lblProductID.Text))
                    ' If condition added for Redmine #20187
                    If sProductId <> lblProductID.Text Then
                        Dim lblClassName As Label = DirectCast(item.FindControl("lblClassName"), Label)


                        Dim drSummerPaymentSummery As DataRow = dtSummerPaymentSummery.NewRow()
                        drSummerPaymentSummery("Class") = Convert.ToString(lblClassName.Text)
                        drSummerPaymentSummery("ProductID") = lblProductID.Text
                        Dim dSummerExamTax As Decimal = 0
                        drSummerPaymentSummery("Price") = GetPrice(Convert.ToInt32(lblProductID.Text), Convert.ToInt32(lblClassID.Text), drpSummerExamLocation.SelectedValue, dSummerExamTax)
                        drSummerPaymentSummery("Location") = 1
                        drSummerPaymentSummery("Tax") = dSummerExamTax
                        dtSummerPaymentSummery.Rows.Add(drSummerPaymentSummery)
                    End If
                    ' Added Summer Intrim exam Payment Summery 

                    Dim lblSummerIntrimClassID As Label = DirectCast(item.FindControl("lblSummerIntrimClassID"), Label)
                    Dim lblSummerIntrimClassName As Label = DirectCast(item.FindControl("lblSummerIntrimClassName"), Label)

                    If lblSummerIntrimProductID.Text <> "" Then
                        'Dim sSummerIntrimProductName As String = AptifyApplication.GetEntityRecordName("Products", Convert.ToInt32(lblSummerIntrimProductID.Text))
                        Dim drSummerPaymentSummery As DataRow = dtSummerPaymentSummery.NewRow()
                        'drSummerPaymentSummery("Class") = Convert.ToString(AptifyApplication.GetEntityRecordName("Classes", Convert.ToInt32(lblSummerIntrimClassID.Text)))
                        drSummerPaymentSummery("Class") = Convert.ToString(lblSummerIntrimClassName.Text)
                        drSummerPaymentSummery("ProductID") = lblSummerIntrimProductID.Text
                        Dim dInterimExamTax As Decimal = 0
                        drSummerPaymentSummery("Price") = GetPrice(Convert.ToInt32(lblSummerIntrimProductID.Text), Convert.ToInt32(lblSummerIntrimClassID.Text), drpSummerExamLocation.SelectedValue, dInterimExamTax)
                        drSummerPaymentSummery("Tax") = dInterimExamTax
                        drSummerPaymentSummery("Location") = 1
                        dtSummerPaymentSummery.Rows.Add(drSummerPaymentSummery)
                        lblComments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPage.SummerInterimCourseAdded")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    End If
                    SummerPaymentSummery = dtSummerPaymentSummery
                Else
                    If Not SummerPaymentSummery Is Nothing AndAlso SummerPaymentSummery.Rows.Count > 0 Then
                        Dim dr() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(lblProductID.Text))
                        SummerPaymentSummery.Rows.Remove(dr(0))
                        SummerPaymentSummery.AcceptChanges()
                        If lblSummerIntrimProductID.Text <> "" Then
                            Dim dr1() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(lblSummerIntrimProductID.Text))
                            If dr1.Length > 0 Then
                                SummerPaymentSummery.Rows.Remove(dr1(0))
                                SummerPaymentSummery.AcceptChanges()
                            End If

                        End If
                        lblComments.Text = ""
                    End If

                End If
                If Not SummerPaymentSummery Is Nothing AndAlso SummerPaymentSummery.Rows.Count > 0 Then
                    radSummerPaymentSummery.DataSource = SummerPaymentSummery
                    radSummerPaymentSummery.DataBind()
                    radSummerPaymentSummery.Visible = True
                    lblAmount.Visible = True
                    lblTotalAmount.Visible = True
                    Dim dAmt As Decimal = 0
                    Dim dTaxAmount As Decimal = 0

                    For Each dr As DataRow In SummerPaymentSummery.Rows
                        If dAmt = 0 Then
                            dAmt = Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1)
                        Else
                            dAmt = Convert.ToDecimal(dAmt + Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1))
                        End If
                        If dTaxAmount = 0 Then
                            dTaxAmount = CDec(dr("Tax"))
                        Else
                            dTaxAmount = dTaxAmount + CDec(dr("Tax"))
                        End If
                    Next
                    dAmt = dAmt + dTaxAmount
                    lblTotalAmount.Text = ViewState("CurrencyTypeID") & Format(CDec(dAmt), "0.00")
                    lblTaxAmount.Text = dTaxAmount
                Else
                    radSummerPaymentSummery.Visible = False
                    lblAmount.Visible = False
                    lblTotalAmount.Visible = False
                End If
                UpdatePanel1.Update()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Public Sub chkSummerRevisionExam_CheckedChanged(sender As Object, e As EventArgs)
            Try
                ' RevisionExam()
                Dim chkSummerRevisionExam As CheckBox = DirectCast(sender, CheckBox)
                '   Dim ddlRadApproval As DropDownList = DirectCast(sender, DropDownList)
                Dim item As GridDataItem = DirectCast(chkSummerRevisionExam.NamingContainer, GridDataItem)
                Dim lblComments As Label = DirectCast(item.FindControl("lblComments"), Label)
                Dim lblClassID As Label = DirectCast(item.FindControl("lblClassID"), Label)
                Dim lblProductID As Label = DirectCast(item.FindControl("lblProductID"), Label)
                Dim chkAutumRevisionCourse As CheckBox = DirectCast(item.FindControl("chkAutumRevisionCourse"), CheckBox)
                Dim lblAutumRevisionProductID As Label = DirectCast(item.FindControl("lblAutumRevisionProductID"), Label)

                Dim sSql As String = Database & "..spGetChangeLocationGroupInExam__c @ClassID=" & lblClassID.Text & ",@AcademicCycleID=" & ViewState("AcademicCycleID") & ",@GroupID=" & drpCourseLocation.SelectedValue
                Dim iGroupID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If iGroupID = drpCourseLocation.SelectedValue Then
                    lblComments.Text = ""
                Else
                    lblComments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPage.FindAlternateLocationOnExam")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                End If
                If chkSummerRevisionExam.Checked Then
                    Dim dtSummerPaymentSummery As DataTable = SummerPaymentSummery
                    Dim drSummerPaymentSummery As DataRow = dtSummerPaymentSummery.NewRow()
                    drSummerPaymentSummery("Class") = Convert.ToString(AptifyApplication.GetEntityRecordName("Classes", Convert.ToInt32(lblClassID.Text)))
                    drSummerPaymentSummery("ProductID") = lblProductID.Text
                    Dim dSummerRevisionExamTax As Decimal = 0
                    drSummerPaymentSummery("Price") = GetPrice(Convert.ToInt32(lblProductID.Text), Convert.ToInt32(lblClassID.Text), drpCourseLocation.SelectedValue, dSummerRevisionExamTax)
                    drSummerPaymentSummery("Tax") = dSummerRevisionExamTax
                    drSummerPaymentSummery("Location") = 3
                    dtSummerPaymentSummery.Rows.Add(drSummerPaymentSummery)
                    SummerPaymentSummery = dtSummerPaymentSummery

                    ' if autumn already added then remove
                    If chkAutumRevisionCourse.Checked = True Then
                        chkAutumRevisionCourse.Checked = False
                        If Not SummerPaymentSummery Is Nothing AndAlso SummerPaymentSummery.Rows.Count > 0 Then
                            Dim dr() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(lblAutumRevisionProductID.Text))
                            SummerPaymentSummery.Rows.Remove(dr(0))
                            SummerPaymentSummery.AcceptChanges()
                        End If
                    End If
                Else
                    If Not SummerPaymentSummery Is Nothing AndAlso SummerPaymentSummery.Rows.Count > 0 Then
                        Dim dr() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(lblProductID.Text))
                        SummerPaymentSummery.Rows.Remove(dr(0))
                        SummerPaymentSummery.AcceptChanges()
                    End If
                End If
                If Not SummerPaymentSummery Is Nothing AndAlso SummerPaymentSummery.Rows.Count > 0 Then
                    radSummerPaymentSummery.DataSource = SummerPaymentSummery
                    radSummerPaymentSummery.DataBind()
                    radSummerPaymentSummery.Visible = True
                    lblAmount.Visible = True
                    lblTotalAmount.Visible = True
                    Dim dAmt As Decimal = 0
                    Dim dTaxAmount As Decimal = 0
                    For Each dr As DataRow In SummerPaymentSummery.Rows
                        If dAmt = 0 Then
                            dAmt = Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1)
                        Else
                            dAmt = Convert.ToDecimal(dAmt + Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1))
                        End If
                        If dTaxAmount = 0 Then
                            dTaxAmount = CDec(dr("Tax"))
                        Else
                            dTaxAmount = dTaxAmount + CDec(dr("Tax"))
                        End If
                    Next
                    dAmt = dAmt + dTaxAmount
                    lblTotalAmount.Text = ViewState("CurrencyTypeID") & Format(CDec(dAmt), "0.00")
                    lblTaxAmount.Text = dTaxAmount
                Else
                    radSummerPaymentSummery.Visible = False
                    lblAmount.Visible = False
                    lblTotalAmount.Visible = False
                End If
                UpdatePanel1.Update()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub


        Public Sub chkAutumRevisionExam_CheckedChanged(sender As Object, e As EventArgs)
            Try
                ' RevisionExam()
                Dim chkAutumRevisionCourse As CheckBox = DirectCast(sender, CheckBox)
                '   Dim ddlRadApproval As DropDownList = DirectCast(sender, DropDownList)
                Dim item As GridDataItem = DirectCast(chkAutumRevisionCourse.NamingContainer, GridDataItem)
                Dim lblComments As Label = DirectCast(item.FindControl("lblComments"), Label)
                Dim lblSummerRevisionCourseID As Label = DirectCast(item.FindControl("lblSummerRevisionCourseID"), Label)
                Dim lblAutumRevisionProductID As Label = DirectCast(item.FindControl("lblAutumRevisionProductID"), Label)
                Dim lblAutumRevisionClassID As Label = DirectCast(item.FindControl("lblAutumRevisionClassID"), Label)
                Dim lblCurriculumID As Label = DirectCast(item.FindControl("lblCurriculumID"), Label)

                Dim chkSummerRevisionExam As CheckBox = DirectCast(item.FindControl("chkSummerRevisionCourse"), CheckBox)
                Dim lblProductID As Label = DirectCast(item.FindControl("lblProductID"), Label)

                Dim sSql As String = Database & "..spCheckExamattemptRevisionStudent__c @StudentID=" & User1.PersonID & ",@AcademicCycleID=" & Convert.ToInt32(ViewState("AcademicCycleID")) & ",@CurriculumID=" & lblCurriculumID.Text & ",@CourseID=" & lblSummerRevisionCourseID.Text
                Dim lClassRegID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If lClassRegID > 0 Then
                    chkAutumRevisionCourse.Enabled = True
                    Dim sSqlAutumnRevision As String = Database & "..spGetChangeLocationGroupInExam__c @ClassID=" & lblAutumRevisionClassID.Text & ",@AcademicCycleID=" & ViewState("AcademicCycleID") & ",@GroupID=" & drpCourseLocation.SelectedValue
                    Dim iGroupID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSqlAutumnRevision, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    If iGroupID = drpCourseLocation.SelectedValue Then
                        lblComments.Text = ""
                    Else
                        lblComments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPage.FindAlternateLocationOnExam")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    End If
                End If
                If chkAutumRevisionCourse.Checked Then
                    Dim dtSummerPaymentSummery As DataTable = SummerPaymentSummery
                    Dim drSummerPaymentSummery As DataRow = dtSummerPaymentSummery.NewRow()
                    drSummerPaymentSummery("Class") = Convert.ToString(AptifyApplication.GetEntityRecordName("Classes", Convert.ToInt32(lblAutumRevisionClassID.Text)))
                    drSummerPaymentSummery("ProductID") = lblAutumRevisionProductID.Text
                    Dim dAutumRevisionExamTax As Decimal = 0
                    drSummerPaymentSummery("Price") = GetPrice(Convert.ToInt32(lblAutumRevisionProductID.Text), Convert.ToInt32(lblAutumRevisionClassID.Text), drpCourseLocation.SelectedValue, dAutumRevisionExamTax)
                    drSummerPaymentSummery("Tax") = dAutumRevisionExamTax
                    drSummerPaymentSummery("Location") = 3
                    dtSummerPaymentSummery.Rows.Add(drSummerPaymentSummery)
                    SummerPaymentSummery = dtSummerPaymentSummery

                    If chkSummerRevisionExam.Checked = True Then
                        chkSummerRevisionExam.Checked = False
                        If Not SummerPaymentSummery Is Nothing AndAlso SummerPaymentSummery.Rows.Count > 0 Then
                            Dim dr() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(lblProductID.Text))
                            SummerPaymentSummery.Rows.Remove(dr(0))
                            SummerPaymentSummery.AcceptChanges()
                        End If
                    End If
                Else
                    If Not SummerPaymentSummery Is Nothing AndAlso SummerPaymentSummery.Rows.Count > 0 Then
                        Dim dr() As DataRow = SummerPaymentSummery.Select("ProductID=" & Convert.ToInt32(lblAutumRevisionProductID.Text))
                        SummerPaymentSummery.Rows.Remove(dr(0))
                        SummerPaymentSummery.AcceptChanges()
                    End If
                End If
                If Not SummerPaymentSummery Is Nothing AndAlso SummerPaymentSummery.Rows.Count > 0 Then
                    radSummerPaymentSummery.DataSource = SummerPaymentSummery
                    radSummerPaymentSummery.DataBind()
                    radSummerPaymentSummery.Visible = True
                    lblAmount.Visible = True
                    lblTotalAmount.Visible = True
                    Dim dAmt As Decimal = 0
                    Dim dTaxAmount As Decimal = 0
                    For Each dr As DataRow In SummerPaymentSummery.Rows
                        If dAmt = 0 Then
                            dAmt = Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1)
                        Else
                            dAmt = Convert.ToDecimal(dAmt + Convert.ToString(dr("Price")).Substring(1, Convert.ToString(dr("Price")).Length - 1))
                        End If

                        If dTaxAmount = 0 Then
                            dTaxAmount = CDec(dr("Tax"))
                        Else
                            dTaxAmount = dTaxAmount + CDec(dr("Tax"))
                        End If
                    Next
                    dAmt = dAmt + dTaxAmount
                    lblTotalAmount.Text = ViewState("CurrencyTypeID") & Format(CDec(dAmt), "0.00")
                    lblTaxAmount.Text = dTaxAmount
                Else
                    radSummerPaymentSummery.Visible = False
                    lblAmount.Visible = False
                    lblTotalAmount.Visible = False
                End If
                'Else
                '    ' lblComments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPageRevisionMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                '    '  chkAutumRevisionCourse.Enabled = False
                '    ' chkAutumRevisionCourse.Checked = False
                ' End If
                UpdatePanel1.Update()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub RevisionExam()
            Try
                For Each row As Telerik.Web.UI.GridItem In grdCourseSelection.Items
                    Dim chkSummerRevisionCourse As CheckBox = DirectCast(row.FindControl("chkSummerRevisionCourse"), CheckBox)
                    Dim chkAutumRevisionCourse As CheckBox = DirectCast(row.FindControl("chkAutumRevisionCourse"), CheckBox)

                    If chkSummerRevisionCourse.Checked = True AndAlso chkAutumRevisionCourse.Checked = True Then
                        chkAutumRevisionCourse.Checked = False
                    End If
                Next
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
            Try
                If validateBeforeSubmit() Then
                    Dim lOrderID As Long
                    Dim sTransID As String = DataAction.BeginTransaction(IsolationLevel.Serializable, True)
                    Dim sClassId As String = String.Empty
                      DoPersonSave()
                    CreateOrder(lOrderID, sTransID, sClassId) ' create a class product order
                    If lOrderID > 0 Then
                        ' PostPayment(lOrderID, lblTaxAmount.Text)
                        'DoPersonSave()
                        ' create a Extrenal application
                        ExternalAppSave(lOrderID, sTransID, sClassId)

                        ' Response.Redirect(HttpContext.Current.Request.Url.ToString(), False)
                        'Response.Redirect(RedirectURLs, False)
                        lblWarning.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalAppPage.SuccessMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        radMockTrial.VisibleOnPageLoad = True
                        btnOk.Visible = True
                        btnOKValidation.Visible = False
                    End If
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Validates the before submit.redmine issue 13601
        ''' </summary>
        ''' <returns></returns>
        Function validateBeforeSubmit() As Boolean
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
        ''' <summary>
        ''' Create a order 
        ''' </summary>
        ''' <param name="lOrderID"></param>
        ''' <param name="sTransID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function CreateOrder(ByRef lOrderID As Long, ByVal sTransID As String, ByRef sClassID As String) As Long
            Try
                Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
                Dim sError As String = Nothing
                oOrder = TryCast(AptifyApplication.GetEntityObject("Orders", -1), OrdersEntity)
                oOrder.ShipToID = User1.PersonID
                oOrder.BillToID = User1.PersonID
                oOrder.SetValue("OrderSourceID", AptifyApplication.GetEntityRecordIDFromRecordName("Order Sources", "Web")) ' Web
                oOrder.SetValue("BillToSameAsShipTo", 1)
                oOrder.SetValue("EmployeeID", DataAction.UserCredentials.GetUserRelatedRecordID("Employees"))
                Dim sSQlFirmAdmin As String = Database & "..spGetUserFirmAdmin__c @PersonID=" & User1.PersonID
                Dim iFirmAdmin As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSQlFirmAdmin, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If iFirmAdmin > 0 Then
                    oOrder.SetValue("FirmPay__c", 1)
                End If
                Dim oOrderLine As Aptify.Applications.OrderEntry.OrderLinesEntity
                For Each row As Telerik.Web.UI.GridItem In grdSummerExamSession.Items
                    Dim chkSummerExam As CheckBox = DirectCast(row.FindControl("chkSummerExam"), CheckBox)
                    Dim lblProductID As Label = DirectCast(row.FindControl("lblProductID"), Label)
                    Dim lblSummerIntrimProductID As Label = DirectCast(row.FindControl("lblSummerIntrimProductID"), Label)
                    Dim lblSummerIntrimClassID As Label = DirectCast(row.FindControl("lblSummerIntrimClassID"), Label)
                    Dim lblClassID As Label = DirectCast(row.FindControl("lblClassID"), Label)
                    If chkSummerExam.Checked = True Then
                        oOrder.AddProduct(Convert.ToInt32(lblProductID.Text), 1)
                        oOrderLine = oOrder.SubTypes("OrderLines").Item(oOrder.SubTypes("OrderLines").Count - 1)
                        With oOrderLine
                            .ExtendedOrderDetailEntity.SetValue("ClassID", lblClassID.Text)
                            ''Commented BY Pradip 2016-20-01 For Group 3 Tracker G3-20
                            '.ExtendedOrderDetailEntity.SetValue("Status", "Registered")
                            .ExtendedOrderDetailEntity.SetValue("Status", "Pending")
                            .ExtendedOrderDetailEntity.SetValue("AcademicCycleID__c", Convert.ToInt32(ViewState("AcademicCycleID")))
                            .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", drpSummerExamLocation.SelectedValue)
                            .ExtendedOrderDetailEntity.SetValue("RouteOfEntryID__c", Convert.ToInt32(ViewState("ExternalTypeID")))
                            .SetAddValue("__ExtendedAttributeObjectData", .ExtendedOrderDetailEntity.GetObjectData(False))
                        End With
                        oOrderLine.SetProductPrice()
                        If sClassID = "" Then
                            sClassID = lblClassID.Text
                        Else
                            sClassID = sClassID + "," + lblClassID.Text
                        End If
                        '' Added summe intrim product as well 
                        If Convert.ToString(lblSummerIntrimProductID.Text) <> "" Then
                            oOrder.AddProduct(Convert.ToInt32(lblSummerIntrimProductID.Text), 1)
                            oOrderLine = oOrder.SubTypes("OrderLines").Item(oOrder.SubTypes("OrderLines").Count - 1)
                            With oOrderLine
                                .ExtendedOrderDetailEntity.SetValue("ClassID", lblSummerIntrimClassID.Text)
                                ''Commented BY Pradip 2016-20-01 For Group 3 Tracker G3-20
                                '.ExtendedOrderDetailEntity.SetValue("Status", "Registered")
                                .ExtendedOrderDetailEntity.SetValue("Status", "Pending")
                                .ExtendedOrderDetailEntity.SetValue("AcademicCycleID__c", Convert.ToInt32(ViewState("AcademicCycleID")))
                                .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", drpSummerExamLocation.SelectedValue)
                                .ExtendedOrderDetailEntity.SetValue("RouteOfEntryID__c", Convert.ToInt32(ViewState("ExternalTypeID")))
                                .SetAddValue("__ExtendedAttributeObjectData", .ExtendedOrderDetailEntity.GetObjectData(False))
                            End With
                            oOrderLine.SetProductPrice()
                            If sClassID = "" Then
                                sClassID = lblSummerIntrimClassID.Text
                            Else
                                sClassID = sClassID + "," + lblSummerIntrimClassID.Text
                            End If
                        End If

                    End If
                Next
                For Each row As Telerik.Web.UI.GridItem In grdAutumExamSession.Items
                    Dim chkAutumINtrim As CheckBox = DirectCast(row.FindControl("chkAutumINtrim"), CheckBox)
                    Dim chkAutumExam As CheckBox = DirectCast(row.FindControl("chkAutumExam"), CheckBox)
                    Dim lblProductID As Label = DirectCast(row.FindControl("lblProductID"), Label)
                    Dim lblClassID As Label = DirectCast(row.FindControl("lblClassID"), Label)
                    If chkAutumExam.Checked = True Then
                        oOrder.AddProduct(Convert.ToInt32(lblProductID.Text), 1)
                        oOrderLine = oOrder.SubTypes("OrderLines").Item(oOrder.SubTypes("OrderLines").Count - 1)
                        With oOrderLine
                            .ExtendedOrderDetailEntity.SetValue("ClassID", lblClassID.Text)
                            ''Commented BY Pradip 2016-20-01 For Group 3 Tracker G3-20
                            '.ExtendedOrderDetailEntity.SetValue("Status", "Registered")
                            .ExtendedOrderDetailEntity.SetValue("Status", "Pending")
                            .ExtendedOrderDetailEntity.SetValue("AcademicCycleID__c", Convert.ToInt32(ViewState("AcademicCycleID")))
                            .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", drpAutumnExamLocation.SelectedValue)
                            .ExtendedOrderDetailEntity.SetValue("RouteOfEntryID__c", Convert.ToInt32(ViewState("ExternalTypeID")))
                            .SetAddValue("__ExtendedAttributeObjectData", .ExtendedOrderDetailEntity.GetObjectData(False))
                        End With
                        oOrderLine.SetProductPrice()
                        If sClassID = "" Then
                            sClassID = lblClassID.Text
                        Else
                            sClassID = sClassID + "," + lblClassID.Text
                        End If
                    End If
                    If chkAutumINtrim.Checked = True Then
                        Dim lblAutumInterimClassID As Label = DirectCast(row.FindControl("lblAutumInterimClassID"), Label)
                        Dim lblAutumInterimProductID As Label = DirectCast(row.FindControl("lblAutumInterimProductID"), Label)
                        oOrder.AddProduct(Convert.ToInt32(lblAutumInterimProductID.Text), 1)
                        oOrderLine = oOrder.SubTypes("OrderLines").Item(oOrder.SubTypes("OrderLines").Count - 1)
                        With oOrderLine
                            .ExtendedOrderDetailEntity.SetValue("ClassID", lblAutumInterimClassID.Text)
                            ''Commented BY Pradip 2016-20-01 For Group 3 Tracker G3-20
                            '.ExtendedOrderDetailEntity.SetValue("Status", "Registered")
                            .ExtendedOrderDetailEntity.SetValue("Status", "Pending")
                            .ExtendedOrderDetailEntity.SetValue("AcademicCycleID__c", Convert.ToInt32(ViewState("AcademicCycleID")))
                            .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", drpAutumnExamLocation.SelectedValue)
                            .ExtendedOrderDetailEntity.SetValue("RouteOfEntryID__c", Convert.ToInt32(ViewState("ExternalTypeID")))
                            .SetAddValue("__ExtendedAttributeObjectData", .ExtendedOrderDetailEntity.GetObjectData(False))
                        End With
                        oOrderLine.SetProductPrice()
                        If sClassID = "" Then
                            sClassID = lblAutumInterimClassID.Text
                        Else
                            sClassID = sClassID + "," + lblAutumInterimClassID.Text
                        End If
                    End If
                Next

                For Each row As Telerik.Web.UI.GridItem In grdCourseSelection.Items
                    Dim chkSummerRevisionCourse As CheckBox = DirectCast(row.FindControl("chkSummerRevisionCourse"), CheckBox)
                    Dim chkAutumRevisionCourse As CheckBox = DirectCast(row.FindControl("chkAutumRevisionCourse"), CheckBox)
                    Dim lblProductID As Label = DirectCast(row.FindControl("lblProductID"), Label)
                    Dim lblClassID As Label = DirectCast(row.FindControl("lblClassID"), Label)
                    If chkSummerRevisionCourse.Checked = True Then
                        oOrder.AddProduct(Convert.ToInt32(lblProductID.Text), 1)
                        oOrderLine = oOrder.SubTypes("OrderLines").Item(oOrder.SubTypes("OrderLines").Count - 1)
                        With oOrderLine
                            .ExtendedOrderDetailEntity.SetValue("ClassID", lblClassID.Text)
                            ''Commented BY Pradip 2016-20-01 For Group 3 Tracker G3-20
                            '.ExtendedOrderDetailEntity.SetValue("Status", "Registered")
                            .ExtendedOrderDetailEntity.SetValue("Status", "Pending")
                            .ExtendedOrderDetailEntity.SetValue("AcademicCycleID__c", Convert.ToInt32(ViewState("AcademicCycleID")))
                            .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", drpCourseLocation.SelectedValue)
                            .ExtendedOrderDetailEntity.SetValue("RouteOfEntryID__c", Convert.ToInt32(ViewState("ExternalTypeID")))
                            .SetAddValue("__ExtendedAttributeObjectData", .ExtendedOrderDetailEntity.GetObjectData(False))
                        End With
                        oOrderLine.SetProductPrice()
                        If sClassID = "" Then
                            sClassID = lblClassID.Text
                        Else
                            sClassID = sClassID + "," + lblClassID.Text
                        End If
                    ElseIf chkAutumRevisionCourse.Checked = True Then
                        Dim lblAutumRevisionClassID As Label = DirectCast(row.FindControl("lblAutumRevisionClassID"), Label)
                        Dim lblAutumRevisionProductID As Label = DirectCast(row.FindControl("lblAutumRevisionProductID"), Label)
                        oOrder.AddProduct(Convert.ToInt32(lblAutumRevisionProductID.Text), 1)
                        oOrderLine = oOrder.SubTypes("OrderLines").Item(oOrder.SubTypes("OrderLines").Count - 1)
                        With oOrderLine
                            .ExtendedOrderDetailEntity.SetValue("ClassID", lblAutumRevisionClassID.Text)
                            ''Commented BY Pradip 2016-20-01 For Group 3 Tracker G3-20
                            '.ExtendedOrderDetailEntity.SetValue("Status", "Registered")
                            .ExtendedOrderDetailEntity.SetValue("Status", "Pending")
                            .ExtendedOrderDetailEntity.SetValue("AcademicCycleID__c", Convert.ToInt32(ViewState("AcademicCycleID")))
                            .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", drpCourseLocation.SelectedValue)
                            .ExtendedOrderDetailEntity.SetValue("RouteOfEntryID__c", Convert.ToInt32(ViewState("ExternalTypeID")))
                            .SetAddValue("__ExtendedAttributeObjectData", .ExtendedOrderDetailEntity.GetObjectData(False))
                        End With
                        oOrderLine.SetProductPrice()
                        If sClassID = "" Then
                            sClassID = lblAutumRevisionClassID.Text
                        Else
                            sClassID = sClassID + "," + lblAutumRevisionClassID.Text
                        End If
                    End If
                Next

                ''oOrder.SetValue("OrderTypeID", Aptify.Applications.OrderEntry.OrderType.Regular)   ' Regular Order 
                ''oOrder.OrderStatus = OrderStatus.Taken
                ''oOrder.SetValue("PayTypeID", CreditCard.PaymentTypeID.ToString)
                ''oOrder.SetValue("CCAccountNumber", CreditCard.CCNumber.ToString)
                ''oOrder.SetValue("CCExpireDate", CreditCard.CCExpireDate.ToString)
                ''If CreditCard.CCNumber = "-Ref Transaction-" Then
                ''    oOrder.SetValue("ReferenceTransactionNumber", CreditCard.ReferenceTransactionNumber)
                ''    oOrder.SetValue("ReferenceExpiration", CreditCard.ReferenceExpiration)
                ''End If
                ''If Len(CreditCard.CCSecurityNumber) > 0 Then
                ''    oOrder.SetAddValue("_xCCSecurityNumber", CreditCard.CCSecurityNumber) 'Neha changes for Issue 16675, 06/05/2013,Added CCSecurityNumber as a temperory field for not storing in record history.
                ''End If
                If CreditCard.SaveCardforFutureUse Then
                    Dim oPersonGE As Aptify.Applications.Persons.PersonsEntity = DirectCast(Me.AptifyApplication.GetEntityObject("Persons", User1.PersonID), Aptify.Applications.Persons.PersonsEntity)
                    With oPersonGE.SubTypes("PersonSavedPaymentMethods").Add()
                        .SetValue("PersonID", User1.PersonID)
                        .SetValue("StartDate", Today.Date)
                        .SetValue("IsActive", 1)
                        .SetValue("Name", "SPM " & Today.Date)
                        .SetValue("PaymentTypeID", CreditCard.PaymentTypeID)
                        .SetValue("CCAccountNumber", CreditCard.CCNumber)
                        .SetValue("CCExpireDate", CreditCard.CCExpireDate)
                        .SetValue("EndDate", CreditCard.CCExpireDate)
                        If .Fields("PaymentInformationID").EmbeddedObjectExists Then
                            Dim oOrderPayInfo As PaymentInformation = DirectCast(.Fields("PaymentInformationID").EmbeddedObject, PaymentInformation)
                            If oOrderPayInfo IsNot Nothing Then
                                oOrderPayInfo.CreditCardSecurityNumber = CreditCard.CCNumber
                                .SetValue("CCPartial", oOrderPayInfo.GetCCPartial(CStr(CreditCard.CCNumber)))
                            End If
                        End If
                        Dim sPersonSaveError As String = String.Empty
                        If oPersonGE.Save(False, sPersonSaveError) Then

                        End If
                    End With

                End If
                ''If oOrder.Fields("PaymentInformationID").EmbeddedObjectExists Then
                ''    Dim oOrderPayInfo As PaymentInformation = DirectCast(oOrder.Fields("PaymentInformationID").EmbeddedObject, PaymentInformation)
                ''    oOrderPayInfo.CopySavedPaymentMethodToCurrent(oOrder.GetValue("PaymentInformationID"))
                ''    oOrderPayInfo.CreditCardSecurityNumber = CreditCard.CCSecurityNumber
                ''    ''RashmiP, Issue 10254, SPM
                ''    oOrderPayInfo.SetValue("SaveForFutureUse", CreditCard.SaveCardforFutureUse)
                ''    'Ansar Shaikh - Issue 11986 - 12/27/2011
                ''    'Ani B for issue 10254 on 22/04/2013
                ''    'Set CC Partial for credit cart type is reference transaction 
                ''    If CreditCard.CCNumber = "-Ref Transaction-" Then
                ''        oOrderPayInfo.SetValue("CCPartial", CreditCard.CCPartial)
                ''    End If
                ''End If
                oOrder.SetValue("PayTypeID", CreditCard.PaymentTypeID)
                oOrder.SetValue("CCAccountNumber", CreditCard.CCNumber)
                oOrder.SetValue("CCExpireDate", CreditCard.CCExpireDate)
                If CreditCard.CCNumber = "-Ref Transaction-" Then
                    oOrder.SetValue("ReferenceTransactionNumber", CreditCard.ReferenceTransactionNumber)
                    oOrder.SetValue("ReferenceExpiration", CreditCard.ReferenceExpiration)
                End If
                If Len(CreditCard.CCSecurityNumber) > 0 Then
                    oOrder.SetAddValue("_xCCSecurityNumber", CreditCard.CCSecurityNumber) 'Neha changes for Issue 16675, 06/05/2013,Added CCSecurityNumber as a temperory field for not storing in record history.
                End If
                If oOrder.Fields("PaymentInformationID").EmbeddedObjectExists Then
                    Dim oOrderPayInfo As PaymentInformation = DirectCast(oOrder.Fields("PaymentInformationID").EmbeddedObject, PaymentInformation)
                    oOrderPayInfo.CreditCardSecurityNumber = CreditCard.CCSecurityNumber
                    ''RashmiP, Issue 10254, SPM
                    oOrderPayInfo.SetValue("SaveForFutureUse", CreditCard.SaveCardforFutureUse)
                    'Ansar Shaikh - Issue 11986 - 12/27/2011
                    'Ani B for issue 10254 on 22/04/2013
                    'Set CC Partial for credit cart type is reference transaction 
                    If CreditCard.CCNumber = "-Ref Transaction-" Then
                        oOrderPayInfo.SetValue("CCPartial", CreditCard.CCPartial)
                    End If
                End If
                If Not oOrder.Save(False, sError) Then
                    lblError.Text = "<ui><li>" + sError + "</li></ui>"
                    If lblError.Text.Trim.ToLower.Contains("there must be at least one order line per") Then
                        lblError.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ExternalPage.NoSelection")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    End If
                    lblError.Visible = True
                    lblError.Text = sError + "Credit Card Verification Failed"
                    lblError.ForeColor = Drawing.Color.Red
                    lblError.Style.Add("font-size", "medium")
                    lOrderID = -1
                Else
                    lOrderID = oOrder.RecordID
                    'PostPayment(lOrderID, lblTaxAmount.Text)
                    ' oOrder.ShipEntireOrder(False)
                End If
                Return lOrderID
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return -1
            End Try
        End Function
        Private Sub PostPayment(ByVal OrderID As Long, ByVal TaxAmount As Decimal)
            Try
                Dim oPayment As AptifyGenericEntityBase

                Dim bFlag As Boolean = False
                Dim bNotification As Boolean = False
                Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity = Nothing
                Dim sql As String = "spGetPaymentLevelID__c"
                Dim PaymentLevelID As Integer
                PaymentLevelID = Convert.ToInt16(DataAction.ExecuteScalar(sql, IAptifyDataAction.DSLCacheSetting.BypassCache))

                oPayment = AptifyApplication.GetEntityObject("Payments", -1)
                With oPayment
                    .SetValue("PaymentLevelID", PaymentLevelID)
                    .SetValue("EmployeeID", EBusinessGlobal.WebEmployeeID(Page.Application))
                    .SetValue("PersonID", User1.PersonID)
                    .SetValue("PaymentDate", Date.Today)
                    .SetValue("DepositDate", Date.Today)
                    .SetValue("EffectiveDate", Date.Today)
                    .SetValue("PaymentTypeID", CreditCard.PaymentTypeID)
                    If CreditCard.CCNumber = "-Ref Transaction-" Then
                        .SetValue("ReferenceTransactionNumber", CreditCard.ReferenceTransactionNumber)
                        .SetValue("ReferenceExpiration", CreditCard.ReferenceExpiration)
                    End If
                    .SetAddValue("_xCCSecurityNumber", CreditCard.CCSecurityNumber)
                    Dim sSql As String = Database & "..spGetOrderDetailsForPayments__c @OrderID=" & OrderID
                    Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                    Dim bIsFirmPay As Boolean = False
                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                        bIsFirmPay = Convert.ToBoolean(dt.Rows(0)("FirmPay__c"))
                        For Each dr As DataRow In dt.Rows
                            If Convert.ToDecimal(dr("Amount")) > 0 Then
                                With .SubTypes("PaymentLines").Add()
                                    .SetValue("AppliesTo", "Order Line")
                                    .SetValue("OrderID", dr("OrderID"))
                                    .SetValue("OrderDetailID", dr("OrderDetailID"))
                                    .SetValue("LineNumber", dr("Sequence"))
                                    .SetValue("Amount", dr("Amount"))
                                    .SetValue("BillToPersonID", User1.PersonID)
                                End With
                            End If
                        Next
                        ' For Tax Payment 
                        If TaxAmount > 0 Then
                            With .SubTypes("PaymentLines").Add()
                                .SetValue("AppliesTo", "Tax")
                                .SetValue("OrderID", OrderID)
                                .SetValue("Amount", TaxAmount)
                                .SetValue("BillToPersonID", User1.PersonID)
                            End With
                        End If


                    End If
                    'Siddharth: Added to check if Firm is Paying for firm admin order cc payment fail issue.
                    If bIsFirmPay Then
                        .SetValue("CompanyID", User1.CompanyID)
                    End If
                    .SetValue("CCAccountNumber", CreditCard.CCNumber)
                    .SetValue("CCExpireDate", CreditCard.CCExpireDate)
                    ''If .Fields("PaymentInformationID").EmbeddedObjectExists Then
                    ''    Dim payInformation As PaymentInformation = DirectCast(.Fields("PaymentInformationID").EmbeddedObject, PaymentInformation)
                    ''    payInformation.CreditCardSecurityNumber = CreditCard.CCSecurityNumber
                    ''    payInformation.SetValue("SaveForFutureUse", CreditCard.SaveCardforFutureUse)
                    ''    payInformation.SetValue("CCPartial", CreditCard.CCPartial)
                    ''End If
                    Dim errorMessage As String = String.Empty
                    If .Save(False, errorMessage) Then
                        'ViewState("AddedTaxAmount") 
                    End If
                End With


            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Create a external application record
        ''' </summary>
        ''' <param name="OrderID"></param>
        ''' <remarks></remarks>
        ''' 

        Private Sub ExternalAppSave(ByVal OrderID As Long, ByVal sTransID As String, ByVal ClassId As String)
            Try
                Dim oGE As AptifyGenericEntityBase
                If Convert.ToInt32(ViewState("ExamptionID")) > 0 Then
                    oGE = AptifyApplication.GetEntityObject("ExternalApplication__c", Convert.ToInt32(ViewState("ExamptionID")))
                Else
                    oGE = AptifyApplication.GetEntityObject("ExternalApplication__c", -1)
                    oGE.SetValue("PersonID", User1.PersonID)
                    oGE.SetValue("Status", "Pending")
                    oGE.SetValue("AcademicCycleID", Convert.ToInt32(ViewState("AcademicCycleID")))
                    'oGE.SetValue("comments", txtFurtherInfo.Text.Trim)
                    oGE.SetValue("ApplicationTypeID", ViewState("ExternalTypeID"))
                End If
                'oGE.SetValue("FurtherInformation", txtFurtherInfo.Text)
                oGE.SetValue("EligibilityOptionsID", drpEligibilityOption.SelectedValue)
                Dim sClassId As String() = ClassId.Split(New Char() {","c})

                ' Use For Each loop over classes and display them.
                Dim classes As String
                For Each classes In sClassId
                    With oGE.SubTypes("ExternalStudentAppDetails__c").Add()
                        .SetValue("ClassID", classes)
                        .SetValue("OrderID", OrderID)
                    End With
                Next
                Dim sError As String = String.Empty
                If oGE.Save(False, sError) Then
                    'lblSuccessMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.ApplicationSave")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    'lblSuccessMsg.Visible = True
                    DataAction.CommitTransaction(sTransID)
                Else
                    DataAction.RollbackTransaction(sTransID)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
            Session("ExternalApp.hdnSummer") = hdnSummer.Value
            Session("ExternalApp.hdnAutum") = hdnAutum.Value
            Session("ExternalApp.hdnCourse") = hdnCourse.Value
            Session("ExternalApp.hdnCreditCard") = hdnCreditCard.Value
            Session("ExternalApp.hdnHeader") = hdnHeader.Value
        End Sub

        Protected Sub btnOk_Click(sender As Object, e As System.EventArgs) Handles btnOk.Click
            Try
                radMockTrial.VisibleOnPageLoad = False
                Response.Redirect(HttpContext.Current.Request.Url.ToString(), False)
                'btnPrint.Visible = True
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Function GetPrice(ByVal lProductID As Long, ByVal classID As Long, ByVal StudentGroupID As Long, ByRef TaxAmount As Decimal) As String
            Try
                Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
                Dim oOL As Aptify.Applications.OrderEntry.OrderLinesEntity
                'Here get the Top 1 Person ID whose MemberTypeID = 1 
                oOrder = TryCast(AptifyApplication.GetEntityObject("Orders", -1), OrdersEntity)
                oOrder.ShipToID = User1.PersonID
                oOrder.BillToID = User1.PersonID
                oOrder.AddProduct(lProductID, 1)

                If oOrder.SubTypes("OrderLines").Count > 0 Then
                    oOL = TryCast(oOrder.SubTypes("OrderLines").Item(0), OrderLinesEntity)
                    'Return Convert.ToString((Convert.ToDouble(oOL.Price), 2, TriState.True, TriState.True, TriState.True)
                    With oOL
                        .ExtendedOrderDetailEntity.SetValue("ClassID", classID)
                        .ExtendedOrderDetailEntity.SetValue("Status", "Registered")
                        .ExtendedOrderDetailEntity.SetValue("AcademicCycleID__c", 1)
                        .ExtendedOrderDetailEntity.SetValue("StudentGroupID__c", StudentGroupID)
                        .ExtendedOrderDetailEntity.SetValue("RouteOfEntryID__c", Convert.ToInt32(ViewState("ExternalTypeID")))
                        .SetAddValue("__ExtendedAttributeObjectData", .ExtendedOrderDetailEntity.GetObjectData(False))
                    End With
                    oOL.SetProductPrice()
                    Dim sPrice As String
                    Dim dPaymentAmount As Decimal
                    If Convert.ToBoolean(oOL.GetValue("TaxIncludedInPrice")) = True AndAlso oOL.SubTypes("OrderLineTaxes").Count > 0 Then
                        dPaymentAmount = Convert.ToDouble(oOL.SubTypes("OrderLineTaxes").Item(0).GetValue("TaxableAmount"))

                    Else
                        dPaymentAmount = oOL.Extended
                    End If
                    Dim oProductGE As AptifyGenericEntityBase = AptifyApplication.GetEntityObject("Products", lProductID)
                    If CBool(oProductGE.GetValue("Taxable")) Then
                        Dim dTaxAmount As Double = Convert.ToDouble(oOrder.GetValue("CALC_SalesTax"))
                        TaxAmount = dTaxAmount
                    End If

                    sPrice = ViewState("CurrencyTypeID") & Format(CDec(dPaymentAmount), "0.00")
                    Return sPrice

                Else
                    Return Convert.ToString(ViewState("CurrencyTypeID") & 0)
                End If
            Catch ex As Exception
                Return Convert.ToString(0)
            End Try
        End Function
        Private Sub GetPrefferedCurrency()
            Try
                Dim sSql As String = Database & "..spGetPrefferedCurencyTypeSymbol__c @PersonID=" & User1.PersonID
                Dim dt As DataTable = DataAction.GetDataTable(sSql)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    ViewState("CurrencyTypeID") = Convert.ToString(dt.Rows(0)("CurrencySymbol"))
                End If


            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ' ''Added By Pradip To Provide View Report Functionality
        'Protected Sub btnPrint_Click(sender As Object, e As System.EventArgs) Handles btnPrint.Click
        '    Try
        '        Dim sReportName As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt("externalappreport")
        '        Dim strFilePath As String = ReportPage & "?PersonID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(User1.PersonID)) & "&sReportName=" & System.Web.HttpUtility.UrlEncode(sReportName)
        '        '"\externalappreport.aspx"
        '        ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & strFilePath & "')", True)
        '    Catch ex As Exception
        '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        '    End Try
        'End Sub

        Protected Sub btnPrint_Click(sender As Object, e As System.EventArgs) Handles btnPrint.Click
            Try
                Dim ReportID As Integer = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "External Application Report"))
                Dim rptParam As New AptifyCrystalReport__c
                rptParam.ReportID = ReportID
                rptParam.Param1 = User1.PersonID
                rptParam.SubParam1 = User1.PersonID
                Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPage & "' )", True)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub


#Region "Private Functions"
        ''' <summary>
        ''' Create Payment Summery for Summer Exam and Summer Interim Exam
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property SummerPaymentSummery() As DataTable
            Get

                If Not ViewState("SummerPaymentSummery") Is Nothing Then
                    Return CType(ViewState("SummerPaymentSummery"), DataTable)
                Else
                    Dim dtSummerPaymentSummery As New DataTable
                    dtSummerPaymentSummery.Columns.Add("Class")
                    dtSummerPaymentSummery.Columns.Add("ProductID")
                    dtSummerPaymentSummery.Columns.Add("Price")
                    dtSummerPaymentSummery.Columns.Add("Location")
                    dtSummerPaymentSummery.Columns.Add("Tax")
                    Return dtSummerPaymentSummery
                End If
            End Get
            Set(ByVal value As DataTable)
                ViewState("SummerPaymentSummery") = value
            End Set
        End Property
#End Region

        Protected Sub grdSummerExamSession_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles grdSummerExamSession.ItemDataBound
            Try
                If e.Item.ItemType = GridItemType.Item OrElse e.Item.ItemType = GridItemType.AlternatingItem Then
                    Dim lblClassID As Label = DirectCast(e.Item.FindControl("lblClassID"), Label)
                    Dim chkSummerExam As CheckBox = DirectCast(e.Item.FindControl("chkSummerExam"), CheckBox)

                    ' Check Summer Cuttoff date is over then not able to check summer revision exam
                    Dim sRevisionClassesCutOffSql As String = Database & "..spCheckRevisionsEnrollmentCutOffDate__c @ClassID=" & lblClassID.Text & ",@Type='Summer'" & ",@AcademicCycleID=" & Convert.ToInt32(ViewState("AcademicCycleID"))
                    Dim lClassID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sRevisionClassesCutOffSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                    If lClassID <= 0 Then
                        'lblComments.Text = ""
                        chkSummerExam.Visible = False
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ' Redmine #16137
        Protected Sub drpEligibilityOption_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drpEligibilityOption.SelectedIndexChanged
            Try
                If drpEligibilityOption.SelectedItem.Text = "I am currently in the year in which my degree concludes" OrElse drpEligibilityOption.SelectedItem.Text = "I am currently in the year in which my postgraduate programme concludes" Then
                    lblEligibilityText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.ExternalApp.EligibilityOptionOneAndTwo__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    'Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.ExternalApp.EligibilityOptionOneAndTwo__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) + Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.ExternalApp.Degree and postdegree programme")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    lnkDownload.Visible = True
                    radEligibiltyOption.VisibleOnPageLoad = True
                ElseIf drpEligibilityOption.SelectedItem.Text = "I will be commencing a training contract this autumn" Then
                    lblEligibilityText.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.ExternalApp.EligibilityOptionThird__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    'Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.ExternalApp.EligibilityOptionOneAndTwo__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) + Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.ExternalApp.Degree and postdegree programme")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    lnkDownload.Visible = False
                    radEligibiltyOption.VisibleOnPageLoad = True
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ' Redmine #16137
        Protected Sub lnkDownload_Click(sender As Object, e As EventArgs) Handles lnkDownload.Click
            Try

                Dim path As String = Server.MapPath("~/Reports__c/Certificate Of Eligibility.pdf") 'get file object as FileInfo
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
        ' Redmine #16137
        Protected Sub btnEligiblity_Click(sender As Object, e As EventArgs) Handles btnEligiblity.Click
            radEligibiltyOption.VisibleOnPageLoad = False
        End Sub
        ''' <summary>
        ''' 'Added by Govind Mande for redmine issue
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadPersonalDetails()
            Try
                Dim sSQL As String = Database & "..spGetPersonAddressDetails__c @PersonID=" & User1.PersonID
                Dim dt As DataTable = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    cmbGender.SelectedValue = Convert.ToString(dt.Rows(0)("Gender"))
                    '  txtPreferredSalutation.Text = Convert.ToString(dt.Rows(0)("PreferredSalutation__c"))
                    SetComboValue(cmbSalutation, Convert.ToString(dt.Rows(0)("Prefix")).Trim)

                    txtHomeAddressLine1.Text = Convert.ToString(dt.Rows(0)("Line1"))
                    txtHomeAddressLine2.Text = Convert.ToString(dt.Rows(0)("Line2"))
                    txtHomeAddressLine3.Text = Convert.ToString(dt.Rows(0)("Line3"))
                    txtHomeCity.Text = Convert.ToString(dt.Rows(0)("City"))
                    txtHomeZipCode.Text = Convert.ToString(dt.Rows(0)("PostalCode"))
                    txtHomeCounty.Text = Convert.ToString(dt.Rows(0)("Country"))

                    SetComboValue(cmbHomeCountry, IIf(Convert.ToString(dt.Rows(0)("Country")) = "", "Ireland", Convert.ToString(dt.Rows(0)("Country"))).ToString)
                    SetComboValue(cmbCountryofOrigin, IIf(Convert.ToString(dt.Rows(0)("CountryOfOrigin__c")) = "", "Ireland", Convert.ToString(dt.Rows(0)("CountryOfOrigin__c"))).ToString)
                    'PopulateState(cmbHomeState, cmbHomeCountry)
                    '  SetComboValue(cmbHomeState, Convert.ToString(dt.Rows(0)("StateProvince")))
                    txtIntlCode.Text = Convert.ToString(dt.Rows(0)("CellCountryCode"))
                    txtPhoneAreaCode.Text = Convert.ToString(dt.Rows(0)("CellAreaCode"))
                    txtPhone.Text = Convert.ToString(dt.Rows(0)("CellPhone"))

                Else
                    SetComboValue(cmbHomeCountry, "Ireland")
                    SetComboValue(cmbCountryofOrigin, "Ireland")
                End If
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
        Private Sub PopulateDropDowns()
            Dim sSQL As String
            Dim dt As DataTable
            sSQL = AptifyApplication.GetEntityBaseDatabase("Addresses") & _
                          "..spGetCountryList__c"
            dt = DataAction.GetDataTable(sSQL)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                cmbHomeCountry.DataSource = dt
                cmbHomeCountry.DataTextField = "Country"
                cmbHomeCountry.DataValueField = "ID"
                cmbHomeCountry.DataBind()
                cmbHomeCountry.Items.Insert(0, "Select Country")

                cmbCountryofOrigin.DataSource = dt
                cmbCountryofOrigin.DataTextField = "Country"
                cmbCountryofOrigin.DataValueField = "ID"
                cmbCountryofOrigin.DataBind()
                cmbCountryofOrigin.Items.Insert(0, "Select Country")
                ' SetComboValue(cmbHomeCountry, "Ireland")

                sSQL = AptifyApplication.GetEntityBaseDatabase("Prefix") & _
                 "..spGetPrefixes"
                Dim dtPrefix As New DataTable
                dtPrefix = DataAction.GetDataTable(sSQL)
                Dim R As DataRow = dtPrefix.NewRow
                R("Prefix") = ""
                dtPrefix.Rows.InsertAt(R, 0)
                cmbSalutation.DataSource = dtPrefix
                cmbSalutation.DataTextField = "Prefix"
                cmbSalutation.DataValueField = "Prefix"
                cmbSalutation.DataBind()

            End If
        End Sub
        Private Sub PopulateState(ByRef cmbPopulateState As DropDownList, ByRef cmbCurrentCountry As DropDownList)
            Try
                Dim sSQL As String
                sSQL = Database & "..spGetStateList @CountryID=" & cmbCurrentCountry.SelectedValue.ToString
                cmbPopulateState.DataSource = DataAction.GetDataTable(sSQL)
                cmbPopulateState.DataTextField = "State"
                cmbPopulateState.DataValueField = "State"
                cmbPopulateState.DataBind()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub cmbHomeCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbHomeCountry.SelectedIndexChanged
            PopulateState(cmbHomeState, cmbHomeCountry)
        End Sub
        Private Sub DoPersonSave()
            Try
                Dim sCounty As String = ""
                Me.DoPostalCodeLookup(txtHomeZipCode.Text, CStr(IIf(cmbHomeCountry.SelectedIndex >= 0, cmbHomeCountry.SelectedValue, "")), sCounty, txtHomeCity, cmbHomeState)

                'User1.SetValue("HomeAddressLine1", txtHomeAddressLine1.Text)
                'User1.SetValue("HomeAddressLine2", txtHomeAddressLine2.Text)
                'User1.SetValue("HomeAddressLine3", txtHomeAddressLine3.Text)
                'User1.SetValue("HomeCity", txtHomeCity.Text)
                'User1.SetValue("HomeState", CStr(IIf(cmbHomeState.SelectedIndex >= 0, cmbHomeState.SelectedValue, "")))
                'User1.SetValue("HomeZipCode", txtHomeZipCode.Text)
                'User1.SetValue("HomeCountryCodeID", CLng(IIf(cmbHomeCountry.SelectedIndex >= 0, cmbHomeCountry.SelectedItem.Value, ""))) '11/27/07,Added by Tamasa,Issue 5222.
                'User1.SetValue("HomeCountry", CStr(IIf(cmbHomeCountry.SelectedIndex >= 0, cmbHomeCountry.SelectedValue, "")))
                'User1.SetAddValue("HomeCounty", sCounty)
                'User1.SetValue("HomeCounty", txtHomeCounty.Text)
                ''User1.SetValue("PreferredSalutation__c", txtPreferredSalutation.Text)
                ''User1.SetValue("Prefix", txtPreferredSalutation.Text)
                'User1.SetValue("Prefix", CStr(IIf(cmbSalutation.SelectedIndex >= 0, cmbSalutation.SelectedValue, "")))
                'User1.SetValue("Gender", CStr(IIf(cmbGender.SelectedIndex >= 0, cmbGender.SelectedValue, "")))
                'User1.SetValue("CellCountryCode", txtIntlCode.Text)
                'User1.SetValue("CellAreaCode", txtPhoneAreaCode.Text)
                'User1.SetValue("CellPhone", txtPhone.TextWithLiterals)
                'User1.SetValue("CountryOfOrigin__c", CLng(IIf(cmbCountryofOrigin.SelectedIndex >= 0, cmbCountryofOrigin.SelectedItem.Value, "")))
                'User1.Save()

                Dim sError As String = ""
                Dim oPersonGE As AptifyGenericEntityBase = AptifyApplication.GetEntityObject("Persons", User1.PersonID)
                With oPersonGE                    
                    .SetValue("HomeAddressLine1", txtHomeAddressLine1.Text)
                    .SetValue("HomeAddressLine2", txtHomeAddressLine2.Text)
                    .SetValue("HomeAddressLine3", txtHomeAddressLine3.Text)
                    .SetValue("HomeCity", txtHomeCity.Text)
                    .SetValue("HomeState", CStr(IIf(cmbHomeState.SelectedIndex >= 0, cmbHomeState.SelectedValue, "")))
                    .SetValue("HomeZipCode", txtHomeZipCode.Text)
                    .SetValue("HomeCountryCodeID", CLng(IIf(cmbHomeCountry.SelectedIndex >= 0, cmbHomeCountry.SelectedItem.Value, ""))) '11/27/07,Added by Tamasa,Issue 5222.
                    .SetValue("HomeCountry", CStr(IIf(cmbHomeCountry.SelectedIndex >= 0, cmbHomeCountry.SelectedValue, "")))
                    .SetAddValue("HomeCounty", sCounty)
                    .SetValue("HomeCounty", txtHomeCounty.Text)
                    '.SetValue("PreferredSalutation__c", txtPreferredSalutation.Text)
                    '.SetValue("Prefix", txtPreferredSalutation.Text)
                    .SetValue("Prefix", CStr(IIf(cmbSalutation.SelectedIndex >= 0, cmbSalutation.SelectedValue, "")))
                    .SetValue("Gender", CStr(IIf(cmbGender.SelectedIndex >= 0, cmbGender.SelectedValue, "")))
                    .SetValue("CellCountryCode", txtIntlCode.Text)
                    .SetValue("CellAreaCode", txtPhoneAreaCode.Text)
                    .SetValue("CellPhone", txtPhone.TextWithLiterals)
                    .SetValue("CountryOfOrigin__c", CLng(IIf(cmbCountryofOrigin.SelectedIndex >= 0, cmbCountryofOrigin.SelectedItem.Value, "")))
                    If Not .Save(False, sError) Then
                        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(New Exception(sError))
                    End If
                End With
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Overridable Sub DoPostalCodeLookup(ByRef PostalCode As String, ByRef Country As String, ByRef County As String, ByRef txt As TextBox, ByRef cmb As DropDownList)
            Dim sAreaCode As String = Nothing, sCounty As String = Nothing, sCity As String = Nothing
            Dim sState As String = Nothing, sCongDist As String = Nothing, sCountry As String = Nothing
            Dim ISOCountry As String
            Try
                Dim oPostalCode As New Aptify.Framework.BusinessLogic.Address.AptifyPostalCode(Me.AptifyApplication.UserCredentials)
                Dim oAddressInfo As New Aptify.Framework.BusinessLogic.Address.AddressInfo(Me.AptifyApplication)

                ISOCountry = oAddressInfo.GetISOCode(CLng(Country))

                If oPostalCode.GetPostalCodeInfo(PostalCode, ISOCountry, _
                                        sCity, sState, _
                                        sAreaCode, , sCounty, , , , , , , , _
                                        sCongDist, sCountry, AllowGUI:=True) Then
                    If txt IsNot Nothing Then
                        If String.IsNullOrWhiteSpace(txt.Text) Then
                            txt.Text = sCity
                        End If

                    End If
                    If cmb IsNot Nothing And sState.Trim <> "" Then
                        cmb.SelectedValue = sState
                    End If

                    ''RashmiP, removed assigned Area code.
                    County = sCounty

                End If

            Catch ex As Exception

            End Try
        End Sub
    End Class
End Namespace
