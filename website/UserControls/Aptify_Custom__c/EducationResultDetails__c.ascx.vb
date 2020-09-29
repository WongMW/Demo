'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Govind Mande                08/07/2014                           Display Education Result details as per student on web page
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


Namespace Aptify.Framework.Web.eBusiness.Generated
    Partial Class EducationResultDetails__c
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
        Public RecordId As Integer
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "EducationResultDetails__c"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_NAME As String = "RedirectURL"
        Protected Const ATTRIBUTE_VIEW_CART_PAGE As String = "ViewcartPage"
        Protected Const ATTRIBUTE_WebCourseMaterial_PAGE As String = "WebCourseMaterial"
        Protected Const ATTRIBUTE_CONTROL_eAssessmentURL_NAME As String = "eAssessmentURL" 'Added as part of #20672

        Protected bFailed As Boolean = False
        Dim bIntrimTopScorer As Boolean = False
        Dim bMockTopScorer As Boolean = False
        Dim bExamTopScorer As Boolean = False
        Dim StudentId As Integer = 0

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
        Public Overridable Property ViewcartPage() As String
            Get
                If Not ViewState(ATTRIBUTE_VIEW_CART_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_VIEW_CART_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_VIEW_CART_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        Public Overridable Property WebCourseMaterialPage() As String
            Get
                If Not ViewState(ATTRIBUTE_WebCourseMaterial_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_WebCourseMaterial_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_WebCourseMaterial_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
            'Added as part of #20672
        End Property
        Public Overridable Property eAssessmentURL() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_eAssessmentURL_NAME) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_eAssessmentURL_NAME))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_eAssessmentURL_NAME) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
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
            If Not IsPostBack Then

                ''Added By Pradip 2016-02-25 for G3-37 
                If Request.QueryString("ID") IsNot Nothing Then
                    ViewState("AcademicCycleID") = Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("Year")).Trim
                    StudentId = Convert.ToInt32(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("ID")).Trim)
                Else
                    StudentId = User1.PersonID
                End If

                If Not String.IsNullOrEmpty(Session("ResultDetails.hdnCurrent")) Then
                    hdnCurrent.Value = Session("ResultDetails.hdnCurrent")
                    hdnCurricula.Value = Session("ResultDetails.hdnCurricula")
                    hdnInterim.Value = Session("ResultDetails.hdnInterim")
                    hdnMock.Value = Session("ResultDetails.hdnMock")
                End If
                LoadHeaderText(StudentId)
                'Code Commented by Govind M on 06 April 2016 G3P - UAT-20
                ''If CBool(Session("FirstACCycle")) = True AndAlso CBool(Session("SecondACCycle")) = False Then
                ''    LoadHeaderText(StudentId)
                ''    rdoCurrentAcademicCycle.Checked = True
                ''    rdoNextAcadmicCycle.Checked = False
                ''ElseIf CBool(Session("FirstACCycle")) = False AndAlso CBool(Session("SecondACCycle")) = True The
                ''    LoadHeaderText(StudentId)
                ''    LoadNextAcademicCycle()
                ''    rdoCurrentAcademicCycle.Checked = False
                ''    rdoNextAcadmicCycle.Checked = True
                ''Else
                ''    LoadHeaderText(StudentId)
                ''    rdoCurrentAcademicCycle.Checked = True
                ''    rdoNextAcadmicCycle.Checked = False
                ''End If
                LoadAcademicYear() 'G3P- UAT-20
                If Request.QueryString("ID") IsNot Nothing AndAlso Request.QueryString("Year") IsNot Nothing Then
                    ViewState("AcademicCycleID") = Convert.ToInt32(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("Year")).Trim)
                    ddlAcademicYear.SelectedValue = ViewState("AcademicCycleID")
                    'Code Commented by Govind M on 06 April 2016 for G3P - UAT-20
                    ''GetNextAcademicCycle()
                    ''If ViewState("AcademicCycleID") = ViewState("CurrentAcademicCycleID") Then
                    ''    rdoCurrentAcademicCycle.Checked = True
                    ''ElseIf ViewState("AcademicCycleID") = ViewState("NextAcademicCycleID") Then
                    ''    rdoNextAcadmicCycle.Checked = True
                    ''Else
                    ''    ViewState("AcademicCycleID") = ViewState("CurrentAcademicCycleID")
                    ''End If
                End If
                'G3P- UAT-20
                If Not Session("SelectedAcademicCycle") Is Nothing Then
                    ViewState("AcademicCycleID") = Session("SelectedAcademicCycle")
                    ddlAcademicYear.SelectedValue = ViewState("AcademicCycleID")
                End If
                ' DisplayNextAcademicCycle()
                GetPrefferedCurrency(StudentId)
                LoadEducationResults(StudentId)
                LoadCurrentExamsGrid()
                LoadInterimAssment(StudentId)

                LoadMock(StudentId)
                EducationDetailsOnExamAppeal()
                LoadCurrentExam(StudentId)
                'LoadHeaderText()
                'DisplayNextAcademicCycle()
                'GetPrefferedCurrency()
                'LoadEducationResults()
                'LoadCurrentExamsGrid()
                'LoadInterimAssment()
                ''LoadResitAssment()
                'LoadMock()
                'EducationDetailsOnExamAppeal()
                'LoadCurrentExam()
                lblStatusMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.EducationDetailsPage.RedColorShown")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblCommentsMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.EducationDetailsPage.CommentsMessage")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblDownloadFormatMessage.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.EducationDetailsPage.DownloadLinkFormat")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                'ShowToFirmResult()
                If bFailed = True Then
                    btnSubmitPay.Visible = True
                    'Added By Pradip 2017-02-06 https://redmine.softwaredesign.ie/issues/16383
                    If Convert.ToInt32(ViewState("AppealDayOver")) > 0 Then
                        lblStatusMessage.Visible = False '' lblStatusMessage.Visible = True ' Updated by GM for Redmine #19842
                    Else
                        lblStatusMessage.Visible = False
                    End If

                Else
                    btnSubmitPay.Visible = False
                    lblStatusMessage.Visible = False
                End If
                lbtnCourseMaterial.Visible = False
                If HasWebMaterialAccess() Then
                    lbtnCourseMaterial.Visible = True
                End If

                If Request.QueryString("ID") IsNot Nothing AndAlso Request.QueryString("Year") IsNot Nothing Then
                    lbtnCourseMaterial.Enabled = False
                    'grdCurrentExam.Enabled = False
                    btnSubmitPay.Enabled = False
                    rdoCurrentAcademicCycle.Enabled = False
                    rdoNextAcadmicCycle.Enabled = False
                    'grdCurrentExam.MasterTableView.Enabled = False

                End If
            End If
        End Sub

        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            MyBase.SetProperties()
            If String.IsNullOrEmpty(Me.RedirectURLs) Then
                Me.RedirectURLs = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_NAME)
            End If
            If String.IsNullOrEmpty(ViewcartPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                ViewcartPage = Me.GetLinkValueFromXML(ATTRIBUTE_VIEW_CART_PAGE)
            End If

            If String.IsNullOrEmpty(WebCourseMaterialPage) Then
                WebCourseMaterialPage = Me.GetLinkValueFromXML(ATTRIBUTE_WebCourseMaterial_PAGE)
            End If
            'Added as part of #20672
            If String.IsNullOrEmpty(Me.eAssessmentURL) Then
                Me.eAssessmentURL = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_eAssessmentURL_NAME)
            End If
        End Sub
        Private Sub LoadHeaderText(ByVal StudentId As Integer)
            Try
                ''Added By Pradip 2016-02-25 For G3-37
                Dim sSqlOldPersonID = "..spGetPersonDetail__c @PersonID=" & CStr(StudentId) & ""
                Dim dtPerson As DataTable = DataAction.GetDataTable(sSqlOldPersonID, IAptifyDataAction.DSLCacheSetting.BypassCache)

                lblFirstLast.Text = dtPerson.Rows(0)("FirstLast").Trim
                lblStudentNumber.Text = dtPerson.Rows(0)("OldID").Trim
                ' code commented by govind on 6th April 2016
                ''Dim sSql As String = "..spGetCurrentAcademicYear__c"
                ''Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                ''If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                ''    ' lblAcademicCycle.Text = Convert.ToString(dt.Rows(0)("CalcName"))
                ''    rdoCurrentAcademicCycle.Text = "Current Academic Cycle: " + Convert.ToString(dt.Rows(0)("Name"))
                ''    ViewState("AcademicCycleID") = Convert.ToString(dt.Rows(0)("ID"))

                ''    ''Added By Pradip for G3-37
                ''    ViewState("CurrentAcademicCycleID") = Convert.ToString(dt.Rows(0)("ID"))
                ''End If

                'lblAcademicCycle.Text = Convert.ToString(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Overridable Sub LoadEducationResults(ByVal StudentId As Integer)
            Try
                'Dim sSql As String = Database & "..spGetEducationResultCompOrAttempted__c @PersonID=" & User1.PersonID & ",@AcademicCycleID=" & Convert.ToInt32(ViewState("AcademicYearStartedID"))
                Dim sSql As String = Database & "..spGetEducationResultCompOrAttempted__c @StudentID=" & StudentId '& User1.PersonID
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    rpEducationResult.DataSource = dt
                    rpEducationResult.DataBind()
                    'grdEducationResult.DataSource = dt
                    'grdEducationResult.Visible = True
                    lblErrorCourseCompleted.Text = ""
                    ViewState("EducationAttempt") = dt
                    rpEducationResult.Visible = True
                Else
                    ViewState("EducationAttempt") = Nothing
                    'grdEducationResult.Visible = False
                    rpEducationResult.Visible = False
                    lblErrorCourseCompleted.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.EducationResultDetailsPage.ErrorMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Overridable Sub LoadCurrentExamsGrid()
            Try
                grdCurrentExam.Visible = False
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub


        Protected Sub grdCurrentExam_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles grdCurrentExam.ItemCommand
            Try
                If e.CommandName = "ChangeGroup" Then
                    ' lblMsgClassRegistration.Text = ""
                    ' radMockTrial.VisibleOnPageLoad = True
                    '  LoadGroupDetails(e.CommandArgument)
                    '   ViewState("ClassID") = e.CommandArgument
                    Dim commandArgs As String() = e.CommandArgument.ToString().Split(New Char() {","c})
                    Dim obj(2) As Object
                    obj(0) = commandArgs(0)
                    obj(1) = commandArgs(1)
                    obj(2) = commandArgs(2)
                    Dim lCourseID As Integer = Aptify.Framework.Web.Common.WebCryptography.Encrypt(obj(0))
                    Dim lClassID As Integer = Aptify.Framework.Web.Common.WebCryptography.Encrypt(obj(1))
                    Dim lClassRegistrationID As Integer = Aptify.Framework.Web.Common.WebCryptography.Encrypt(obj(2))
                    Response.Redirect("~\Education\AppealApp.aspx?CourseID=" & System.Web.HttpUtility.UrlEncode(lCourseID) & "&ClassID=" & System.Web.HttpUtility.UrlEncode(lClassID) & "&ClassRegistrationID=" & System.Web.HttpUtility.UrlEncode(lClassRegistrationID))

                    ' Response.Redirect("~\Education\AppealApp.aspx?CourseID=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(obj(0)) & "&ClassID=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(obj(1)) & "&ClassRegistrationID=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(obj(2)))


                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub grdCurrentExam_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles grdCurrentExam.ItemDataBound

            If TypeOf e.Item Is GridDataItem Then

                Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)
                Dim list As DropDownList = TryCast(item.FindControl("drpReports"), DropDownList)
                Dim Status As Label = TryCast(item.FindControl("lblStatus"), Label)
                Dim sSql As String = Database & "..spGetExamReportProduct__c @AttributeName='ExamReport'"
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim dtCurrentReportTable As DataTable = CurrentReportTable
                    For Each dr As DataRow In dt.Rows
                        Dim drCurrentReportTable As DataRow = dtCurrentReportTable.NewRow()
                        drCurrentReportTable("ProductID") = dr("ID")
                        drCurrentReportTable("ProductDetails") = Convert.ToString(dr("Name")) & " " & ViewState("CurrencyTypeID") & " " & GetPrice(dr("ID"))
                        dtCurrentReportTable.Rows.Add(drCurrentReportTable)
                    Next
                    list.DataSource = dtCurrentReportTable
                    list.DataTextField = "ProductDetails"
                    list.DataValueField = "ProductID"
                    list.DataBind()
                End If
                If Convert.ToString(Status.Text).Trim.ToLower <> "failed" Then
                    Dim lnkInterimUpdateGroup As LinkButton = CType(item.FindControl("lnkInterimUpdateGroup"), LinkButton)
                    lnkInterimUpdateGroup.Text = ""
                End If
            End If
        End Sub
        Protected Sub grdCurrentExam_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdCurrentExam.NeedDataSource
            LoadCurrentExamsGrid()
        End Sub
        Protected Function GetPrice(ByVal lProductID As Long) As String
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
                    Return Format(CDec(oOL.Price), "0.00")

                    '  Return Convert.ToString(oOL.Price)
                Else
                    Return Convert.ToString(0)
                End If
            Catch ex As Exception
                Return Convert.ToString(0)
            End Try
        End Function
        ''' <summary>
        ''' Create Company Pay Data table 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CurrentReportTable() As DataTable
            Get
                If Not Session("CurrentReportTable") Is Nothing Then
                    Return CType(Session("CurrentReportTable"), DataTable)
                Else
                    Dim dtCurrentReportTableTable As New DataTable
                    dtCurrentReportTableTable.Columns.Add("ProductID")
                    dtCurrentReportTableTable.Columns.Add("ProductDetails")
                    Return dtCurrentReportTableTable
                End If
            End Get
            Set(ByVal value As DataTable)
                Session("CurrentReportTable") = value
            End Set
        End Property
        Private Sub GetPrefferedCurrency(ByVal StudentId As Integer)
            Try
                Dim sSql As String = Database & "..spGetPrefferedCurencyTypeSymbol__c @PersonID=" & StudentId
                Dim dt As DataTable = DataAction.GetDataTable(sSql)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    ViewState("CurrencyTypeID") = Convert.ToString(dt.Rows(0)("CurrencySymbol"))
                End If


            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub LoadInterimAssment(ByVal StudentID As Integer)
            Try
                Dim sSql As String = Database & "..spGetCurrentAssmntResultCurriculum__c @StudentID=" & StudentID & ",@AcademicCycleID=" & Convert.ToInt32(ViewState("AcademicCycleID"))
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    rptInterimAssessments.DataSource = dt
                    rptInterimAssessments.DataBind()
                    rptInterimAssessments.Visible = True
                    lblInterimErrorMsg.Text = ""
                    CheckIntrimRepeterData()
                    checkIntrimTopScorer()
                Else
                    rptInterimAssessments.Visible = False
                    lblInterimErrorMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.EducationResultDetailsPage.ErrorMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)

                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub CheckIntrimRepeterData()
            Try
                Dim bNotFound As Boolean = False
                For Each row As RepeaterItem In rptInterimAssessments.Items
                    Dim rpInterimDetails As Repeater = DirectCast(row.FindControl("rpInterimDetails"), Repeater)
                    If rpInterimDetails.Items.Count > 0 Then
                        rptInterimAssessments.Visible = True
                        bNotFound = True
                    Else

                    End If
                Next
                If bNotFound = False Then
                    rptInterimAssessments.Visible = False
                    lblInterimErrorMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.EducationResultDetailsPage.ErrorMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub checkIntrimTopScorer()
            Try
                For Each row As RepeaterItem In rptInterimAssessments.Items
                    Dim rpInterimDetails As Repeater = DirectCast(row.FindControl("rpInterimDetails"), Repeater)
                    Dim lCurriculumID As Integer = 0
                    For Each rowdDetails As RepeaterItem In rpInterimDetails.Items
                        If bIntrimTopScorer = True Then
                            Dim lblTopScorere As Label = DirectCast(rowdDetails.FindControl("lblTopScorere"), Label)
                            If lblTopScorere.Text <> "" Then
                                rpInterimDetails.Controls(0).FindControl("topScore").Visible = True
                                rpInterimDetails.Controls(0).FindControl("thIntrimTopScorerBlank").Visible = False
                                rowdDetails.FindControl("tdTopScoreComments").Visible = True
                                rowdDetails.FindControl("tdTopScoreBlank").Visible = False
                                lCurriculumID = DirectCast(row.FindControl("lblCurriculumID"), Label).Text
                                'For i As Integer = 1 To rpInterimDetails.Items.Count
                                '    rpInterimDetails.Controls(i).FindControl("tdTopScoreComments").Visible = True
                                '    rpInterimDetails.Controls(i).FindControl("tdTopScoreBlank").Visible = False
                                'Next
                            Else
                                If lCurriculumID <> Convert.ToInt32(DirectCast(row.FindControl("lblCurriculumID"), Label).Text) Then
                                    rpInterimDetails.Controls(0).FindControl("thIntrimTopScorerBlank").Visible = True
                                    rpInterimDetails.Controls(0).FindControl("topScore").Visible = False
                                    rowdDetails.FindControl("tdTopScoreComments").Visible = False
                                    rowdDetails.FindControl("tdTopScoreBlank").Visible = True
                                End If

                                'rpInterimDetails.Controls(0).FindControl("thIntrimTopScorerBlank").Visible = True
                                'rpInterimDetails.Controls(0).FindControl("topScore").Visible = False
                                'rowdDetails.FindControl("tdTopScoreComments").Visible = False
                                'rowdDetails.FindControl("tdTopScoreBlank").Visible = True
                                'For i As Integer = 1 To rpInterimDetails.Items.Count
                                '    rpInterimDetails.Controls(i).FindControl("tdTopScoreBlank").Visible = True
                                '    rpInterimDetails.Controls(i).FindControl("tdTopScoreComments").Visible = False
                                'Next
                            End If

                        End If
                    Next
                Next
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub rptInterimAssessments_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptInterimAssessments.ItemDataBound
            Try
                If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                    ''Added By Pradip 2016-02-25 for G3-37 
                    If Request.QueryString("ID") IsNot Nothing Then
                        StudentId = Convert.ToInt32(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("ID")).Trim)
                    Else
                        StudentId = User1.PersonID
                    End If

                    Dim lbl As Label = DirectCast(e.Item.FindControl("lblCurriculumID"), Label)
                    Dim rpInterimDetails As Repeater = DirectCast(e.Item.FindControl("rpInterimDetails"), Repeater)
                    ' Dim sSql As String = Database & "..spGetCurrentAssmntResultDetails__c @StudentID=" & User1.PersonID & ",@AcademicCycleID=" & Convert.ToInt32(ViewState("AcademicCycleID")) & ",@CurriculumID=" & lbl.Text
                    Dim sSql As String = Database & "..spGetCurrentAssmntResultDetails__c @StudentID=" & StudentId & ",@AcademicCycleID=" & Convert.ToInt32(ViewState("AcademicCycleID")) & ",@CurriculumID=" & lbl.Text
                    Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                        rpInterimDetails.DataSource = dt
                        rpInterimDetails.DataBind()
                        rpInterimDetails.Visible = True
                        lblInterimErrorMsg.Text = ""
                        ''Changes for 18784, commented below to make same as CAP1
                        'Dim lblH1 As Label = DirectCast(e.Item.FindControl("lblH1"), Label)
                        'If lblH1.Text.ToLower.Contains("fae") Then
                        '    rpInterimDetails.Controls(0).FindControl("FAEResult").Visible = True
                        '    rpInterimDetails.Controls(0).FindControl("ScorResult").Visible = False
                        '    For i As Integer = 1 To rpInterimDetails.Items.Count
                        '        rpInterimDetails.Controls(i).FindControl("tdFAEResult").Visible = True
                        '        rpInterimDetails.Controls(i).FindControl("tdScore").Visible = False
                        '    Next
                        'Else
                        rpInterimDetails.Controls(0).FindControl("FAEResult").Visible = False
                        rpInterimDetails.Controls(0).FindControl("ScorResult").Visible = True
                        For i As Integer = 1 To rpInterimDetails.Items.Count
                            rpInterimDetails.Controls(i).FindControl("tdFAEResult").Visible = False
                            rpInterimDetails.Controls(i).FindControl("tdScore").Visible = True
                        Next
                        'End If
                        Dim bTopScorere As Boolean = False
                        For Each rp As RepeaterItem In rpInterimDetails.Items
                            Dim lblTopScorere As Label = TryCast(rp.FindControl("lblTopScorere"), Label)
                            If lblTopScorere.Text.Trim <> "" Then
                                bTopScorere = True
                                bIntrimTopScorer = True
                            End If
                        Next
                        If bTopScorere Then
                            rpInterimDetails.Controls(0).FindControl("topScore").Visible = True
                            For i As Integer = 1 To rpInterimDetails.Items.Count
                                rpInterimDetails.Controls(i).FindControl("tdTopScoreComments").Visible = True
                            Next
                        Else
                            rpInterimDetails.Controls(0).FindControl("topScore").Visible = False
                            For i As Integer = 1 To rpInterimDetails.Items.Count
                                rpInterimDetails.Controls(i).FindControl("tdTopScoreComments").Visible = False
                            Next
                        End If
                        ''Added by Harish - Redmine 20672 - added column to get purchase re-sit link for FA Interim Assessment if the student failed
                        Dim lnkEassessment As LinkButton = DirectCast(rpInterimDetails.FindControl("lnkEassessment"), LinkButton)
                        Dim lblStatus As Label = DirectCast(rpInterimDetails.FindControl("lblStatus"), Label)
                        Dim lblCourseID As Label = DirectCast(rpInterimDetails.FindControl("lblCourseID"), Label)

                        For Each rp As RepeaterItem In rpInterimDetails.Items
                            lblStatus = TryCast(rp.FindControl("lblStatus"), Label)
                            lblCourseID = TryCast(rp.FindControl("lblCourseID"), Label)
                            lnkEassessment = TryCast(rp.FindControl("lnkEassessment"), LinkButton)
                            If Convert.ToString(lblStatus.Text) = "1" Then
                                lblStatus.Text = "Failed"
                                Dim strCourses As String = Convert.ToString(AptifyApplication.GetEntityAttribute("Curriculum Definitions", "EassessmentCourses__c"))
                                Dim Courses() As String = Split(strCourses, ",")

                                If Courses.Contains(lblCourseID.Text) Then
                                    lnkEassessment.Visible = True
                                    lnkEassessment.Text = "Purchase re-sit"
                                    lnkEassessment.PostBackUrl = eAssessmentURL
                                End If
                            Else
                                lblStatus.Text = "Pass"
                            End If

                        Next
                        ''Code end --Added by Harish - Redmine 20672 - added column to get purchase re-sit link for FA Interim Assessment if the student failed

                    Else
                        lblInterimErrorMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.EducationResultDetailsPage.ErrorMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                        rpInterimDetails.Visible = False
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub LoadMock(ByVal StudentId As Integer)
            Try
                Dim sSql As String = Database & "..spGetCurrentMockExamResultCurriculum__c @StudentID=" & StudentId & ",@AcademicCycleID=" & Convert.ToInt32(ViewState("AcademicCycleID"))
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    rpMockExam.DataSource = dt
                    rpMockExam.DataBind()
                    rpMockExam.Visible = True
                    lblErrorMockExam.Text = ""
                    CheckMockRepeterData()
                    checkrpMockExamTopScorer()
                Else
                    rpMockExam.Visible = False
                    lblErrorMockExam.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.EducationResultDetailsPage.ErrorMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub CheckMockRepeterData()
            Try
                Dim bNotFound As Boolean = False
                For Each row As RepeaterItem In rpMockExam.Items
                    Dim rpMockExamDetails As Repeater = DirectCast(row.FindControl("rpMockExamDetails"), Repeater)
                    If rpMockExamDetails.Items.Count > 0 Then
                        rpMockExam.Visible = True
                        bNotFound = True
                    Else

                    End If
                Next
                If bNotFound = False Then
                    rpMockExam.Visible = False
                    lblErrorMockExam.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.EducationResultDetailsPage.ErrorMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub checkrpMockExamTopScorer()
            Try
                For Each row As RepeaterItem In rpMockExam.Items
                    Dim rpMockExamDetails As Repeater = DirectCast(row.FindControl("rpMockExamDetails"), Repeater)
                    Dim lCurriculumID As Integer = 0
                    For Each rowdDetails As RepeaterItem In rpMockExamDetails.Items
                        If bMockTopScorer = True Then
                            Dim lblTopScorere As Label = DirectCast(rowdDetails.FindControl("lblTopScorere"), Label)
                            If lblTopScorere.Text <> "" Then
                                rpMockExamDetails.Controls(0).FindControl("topScore").Visible = True
                                rpMockExamDetails.Controls(0).FindControl("thMockTopScorerBlank").Visible = False
                                rowdDetails.FindControl("tdTopScoreComments").Visible = True
                                rowdDetails.FindControl("tdMockTopScoreBlank").Visible = False
                                lCurriculumID = DirectCast(row.FindControl("lblCurriculumID"), Label).Text
                                'For i As Integer = 1 To rpInterimDetails.Items.Count
                                '    rpInterimDetails.Controls(i).FindControl("tdTopScoreComments").Visible = True
                                '    rpInterimDetails.Controls(i).FindControl("tdTopScoreBlank").Visible = False
                                'Next
                            Else
                                If lCurriculumID <> Convert.ToInt32(DirectCast(row.FindControl("lblCurriculumID"), Label).Text) Then
                                    rpMockExamDetails.Controls(0).FindControl("thMockTopScorerBlank").Visible = True
                                    rpMockExamDetails.Controls(0).FindControl("topScore").Visible = False
                                    rowdDetails.FindControl("tdTopScoreComments").Visible = False
                                    rowdDetails.FindControl("tdMockTopScoreBlank").Visible = True
                                End If

                            End If

                        End If
                    Next
                Next
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub rpMockExam_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rpMockExam.ItemDataBound
            Try
                If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                    Dim lbl As Label = DirectCast(e.Item.FindControl("lblCurriculumID"), Label)
                    Dim rpMockExamDetails As Repeater = DirectCast(e.Item.FindControl("rpMockExamDetails"), Repeater)
                    'Dim sSql As String = Database & "..spGetCurrentMockExamResultDetails__c @StudentID=" & User1.PersonID & ",@AcademicCycleID=" & Convert.ToInt32(ViewState("AcademicCycleID")) & ",@CurriculumID=" & lbl.Text

                    ''Added By Pradip 2016-02-25 for G3-37 
                    If Request.QueryString("ID") IsNot Nothing Then
                        StudentId = Convert.ToInt32(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("ID")).Trim)
                    Else
                        StudentId = User1.PersonID
                    End If
                    Dim sSql As String = Database & "..spGetCurrentMockExamResultDetails__c @StudentID=" & StudentId & ",@AcademicCycleID=" & Convert.ToInt32(ViewState("AcademicCycleID")) & ",@CurriculumID=" & lbl.Text
                    Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                        rpMockExamDetails.DataSource = dt
                        rpMockExamDetails.DataBind()
                        Dim lblH1 As Label = DirectCast(e.Item.FindControl("lblH1"), Label)
                        ''Changes for 18784, commented below to make same as CAP1
                        'If lblH1.Text.ToLower.Contains("fae") Then
                        '    rpMockExamDetails.Controls(0).FindControl("FAEResult").Visible = True
                        '    rpMockExamDetails.Controls(0).FindControl("ScorResult").Visible = False
                        '    For i As Integer = 1 To rpMockExamDetails.Items.Count
                        '        rpMockExamDetails.Controls(i).FindControl("tdFAEResult").Visible = True
                        '        rpMockExamDetails.Controls(i).FindControl("tdScore").Visible = False
                        '    Next
                        'Else
                        rpMockExamDetails.Controls(0).FindControl("FAEResult").Visible = False
                        rpMockExamDetails.Controls(0).FindControl("ScorResult").Visible = True
                        For i As Integer = 1 To rpMockExamDetails.Items.Count
                            rpMockExamDetails.Controls(i).FindControl("tdFAEResult").Visible = False
                            rpMockExamDetails.Controls(i).FindControl("tdScore").Visible = True
                        Next
                        'End If

                        Dim bTopScorere As Boolean = False
                        For Each rp As RepeaterItem In rpMockExamDetails.Items
                            Dim lblTopScorere As Label = TryCast(rp.FindControl("lblTopScorere"), Label)
                            If lblTopScorere.Text.Trim <> "" Then
                                bTopScorere = True
                                bMockTopScorer = True
                            End If
                        Next
                        If bTopScorere Then
                            rpMockExamDetails.Controls(0).FindControl("topScore").Visible = True
                            For i As Integer = 1 To rpMockExamDetails.Items.Count
                                rpMockExamDetails.Controls(i).FindControl("tdTopScoreComments").Visible = True
                            Next
                        Else
                            rpMockExamDetails.Controls(0).FindControl("topScore").Visible = False
                            For i As Integer = 1 To rpMockExamDetails.Items.Count
                                rpMockExamDetails.Controls(i).FindControl("tdTopScoreComments").Visible = False
                            Next
                        End If
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub LoadCurrentExam(ByVal StudentID As Integer)
            Try
                '  Dim sSql As String = Database & "..spGetCurrentExamResultDetails__c @StudentID=" & User1.PersonID & ",@AcademicCycleID=" & Convert.ToInt32(ViewState("AcademicYearStartedID"))
                Dim sSql As String = Database & "..spGetCurrentExamResultCurriculum__c @StudentID=" & StudentID & ",@AcademicCycleID=" & Convert.ToInt32(ViewState("AcademicCycleID"))

                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    ''Added By Pradip 2017-02-06 for Issue https://redmine.softwaredesign.ie/issues/16383
                    sSql = String.Empty
                    sSql = Database & "..spCheckAppealDayOver__c @StudentID=" & StudentID & ",@AcademicCycleID=" & Convert.ToInt32(ViewState("AcademicCycleID")) & ",@Curriculum=" & "'" & Convert.ToString(dt.Rows(0)("CurrentStage")).Trim & "'"
                    ViewState("AppealDayOver") = DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)

                    rpCurrentExam.DataSource = dt
                    rpCurrentExam.DataBind()
                    rpCurrentExam.Visible = True
                    lblRagisteredInterimAssessments.Text = ""
                    ChecKExamRepeterData()
                    checkrpExamTopScorer()
                    lblDownloadFormatMessage.Visible = True
                Else
                    rpCurrentExam.Visible = False
                    lblRagisteredInterimAssessments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.EducationDetailsPage.ErrorMsgRegisteredExams")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    lblDownloadFormatMessage.Visible = False
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub ChecKExamRepeterData()
            Try
                Dim bNotFound As Boolean = False
                For Each row As RepeaterItem In rpCurrentExam.Items
                    Dim rpCurrentExamDetails As Repeater = DirectCast(row.FindControl("rpCurrentExamDetails"), Repeater)
                    If rpCurrentExamDetails.Items.Count > 0 Then
                        rpCurrentExam.Visible = True
                        bNotFound = True
                    Else
                        '  bNotFound = True
                    End If
                Next
                If bNotFound = False Then
                    rpCurrentExam.Visible = False
                    lblRagisteredInterimAssessments.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.EducationDetailsPage.ErrorMsgRegisteredExams")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub checkrpExamTopScorer()
            Try
                For Each row As RepeaterItem In rpCurrentExam.Items
                    Dim rpCurrentExamDetails As Repeater = DirectCast(row.FindControl("rpCurrentExamDetails"), Repeater)
                    Dim lCurriculumID As Integer = 0
                    For Each rowdDetails As RepeaterItem In rpCurrentExamDetails.Items
                        If bExamTopScorer = True Then
                            Dim lblTopScorere As Label = DirectCast(rowdDetails.FindControl("lblTopScorere"), Label)
                            If lblTopScorere.Text <> "" Then
                                rpCurrentExamDetails.Controls(0).FindControl("topScore").Visible = True
                                rpCurrentExamDetails.Controls(0).FindControl("thExamTopScorerBlank").Visible = False
                                rowdDetails.FindControl("tdTopScoreComments").Visible = True
                                rowdDetails.FindControl("tdExamTopScoreBlank").Visible = False
                                lCurriculumID = DirectCast(row.FindControl("lblCurriculumID"), Label).Text
                                'For i As Integer = 1 To rpInterimDetails.Items.Count
                                '    rpInterimDetails.Controls(i).FindControl("tdTopScoreComments").Visible = True
                                '    rpInterimDetails.Controls(i).FindControl("tdTopScoreBlank").Visible = False
                                'Next
                            Else
                                If lCurriculumID <> Convert.ToInt32(DirectCast(row.FindControl("lblCurriculumID"), Label).Text) Then
                                    rpCurrentExamDetails.Controls(0).FindControl("thExamTopScorerBlank").Visible = True
                                    rpCurrentExamDetails.Controls(0).FindControl("topScore").Visible = False
                                    rowdDetails.FindControl("tdTopScoreComments").Visible = False
                                    rowdDetails.FindControl("tdExamTopScoreBlank").Visible = True
                                End If

                            End If

                        End If
                    Next
                Next
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub rpCurrentExam_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rpCurrentExam.ItemDataBound
            Try
                If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                    Dim lbl As Label = DirectCast(e.Item.FindControl("lblCurriculumID"), Label)
                    Dim rpCurrentExamDetails As Repeater = DirectCast(e.Item.FindControl("rpCurrentExamDetails"), Repeater)
                    'Dim sSql As String = Database & "..spGetCurrentExamResultDetails__c @StudentID=" & User1.PersonID & ",@AcademicCycleID=" & Convert.ToInt32(ViewState("AcademicCycleID")) & ",@CurriculumID=" & lbl.Text
                    ''Added By Pradip 2016-02-25 for G3-37 
                    If Request.QueryString("ID") IsNot Nothing Then
                        StudentId = Convert.ToInt32(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("ID")).Trim)
                    Else
                        StudentId = User1.PersonID
                    End If
                    Dim sSql As String = Database & "..spGetCurrentExamResultDetails__c @StudentID=" & StudentId & ",@AcademicCycleID=" & Convert.ToInt32(ViewState("AcademicCycleID")) & ",@CurriculumID=" & lbl.Text
                    Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                        rpCurrentExamDetails.DataSource = dt
                        rpCurrentExamDetails.DataBind()
                        ' for fae 
                        Dim lblH1 As Label = DirectCast(e.Item.FindControl("lblH1"), Label)
                        If lblH1.Text.ToLower.Contains("fae") Then
                            ViewState("ISFAEResult") = "yes" ' added by GM for report #19143
                            'rpCurrentExamDetails.Controls(0).FindControl("FAEResult").Visible = True
                            ''Changes for 18784, commented below to make same as CAP1
                            'rpCurrentExamDetails.Controls(0).FindControl("ScorResult").Visible = False
                            'rpCurrentExamDetails.Controls(0).FindControl("thBlank").Visible = True
                            'For i As Integer = 1 To rpCurrentExamDetails.Items.Count
                            '    'rpCurrentExamDetails.Controls(i).FindControl("tdFAEResult").Visible = True
                            '    rpCurrentExamDetails.Controls(i).FindControl("tdScore").Visible = False
                            '    rpCurrentExamDetails.Controls(i).FindControl("tdBlank").Visible = True
                            'Next
                        End If
                        ''Changes for 18784, taken out else part to make same as CAP1
                        'Else
                        ' rpCurrentExamDetails.Controls(0).FindControl("FAEResult").Visible = False
                        rpCurrentExamDetails.Controls(0).FindControl("ScorResult").Visible = True
                        rpCurrentExamDetails.Controls(0).FindControl("thBlank").Visible = False
                        For i As Integer = 1 To rpCurrentExamDetails.Items.Count
                            ' rpCurrentExamDetails.Controls(i).FindControl("tdFAEResult").Visible = False
                            rpCurrentExamDetails.Controls(i).FindControl("tdScore").Visible = True
                            rpCurrentExamDetails.Controls(i).FindControl("tdBlank").Visible = False
                        Next
                        'End If
                        Dim bTopScorere As Boolean = False
                        For Each rp As RepeaterItem In rpCurrentExamDetails.Items
                            Dim list As DropDownList = TryCast(rp.FindControl("drpSchemeReport"), DropDownList)
                            Dim lblStatus As Label = TryCast(rp.FindControl("lblStatus"), Label)
                            Dim lnkFAEDEtails As LinkButton = TryCast(rp.FindControl("lnkFAEDEtails"), LinkButton)

                            ''Changes for 18784, commented below to make same as CAP1
                            'If lblH1.Text.ToLower.Contains("fae") Then
                            '    lnkFAEDEtails.Visible = True
                            '    lblStatus.Visible = False
                            'End If
                            Dim lnkCircumstances As LinkButton = TryCast(rp.FindControl("lnkCircumstances"), LinkButton)
                            Dim lnkClericalRecheck As LinkButton = TryCast(rp.FindControl("lnkClericalRecheck"), LinkButton)
                            ' Dim lnkAppealClericalRecheck As LinkButton = TryCast(rp.FindControl("lnkAppealClericalRecheck"), LinkButton)
                            Dim sSqlExamReport As String = Database & "..spGetAppealsProduct__c @Type='Report'" & ",@Appeal=Null,@CurriculumID=" & lbl.Text

                            Dim dtsSqlExamReport As DataTable = DataAction.GetDataTable(sSqlExamReport, IAptifyDataAction.DSLCacheSetting.BypassCache)
                            If Not dtsSqlExamReport Is Nothing AndAlso dtsSqlExamReport.Rows.Count > 0 Then
                                Dim dtCurrentReportTable As DataTable = CurrentReportTable
                                For Each dr As DataRow In dtsSqlExamReport.Rows
                                    Dim drCurrentReportTable As DataRow = dtCurrentReportTable.NewRow()
                                    drCurrentReportTable("ProductID") = dr("ProductID")
                                    drCurrentReportTable("ProductDetails") = Convert.ToString(dr("Name")) & " " & ViewState("CurrencyTypeID") & " " & GetPrice(dr("ProductID"))
                                    dtCurrentReportTable.Rows.Add(drCurrentReportTable)
                                Next
                                list.DataSource = dtCurrentReportTable
                                list.DataTextField = "ProductDetails"
                                list.DataValueField = "ProductID"
                                list.DataBind()
                                list.Items.Insert(0, "--Select--")
                            End If

                            Dim lblCourseID As Label
                            Dim lblClassID As Label
                            Dim lblClassRegistrationID As Label
                            Dim lnkCircumstancess As LinkButton
                            lblCourseID = DirectCast(rp.FindControl("lblCourseID"), Label)
                            lblClassID = DirectCast(rp.FindControl("lblClassID"), Label)
                            lnkCircumstancess = DirectCast(rp.FindControl("lnkCircumstancess"), LinkButton)
                            lnkClericalRecheck = DirectCast(rp.FindControl("lnkClericalRecheck"), LinkButton)
                            '  lnkAppealClericalRecheck = DirectCast(rp.FindControl("lnkAppealClericalRecheck"), LinkButton)
                            lblClassRegistrationID = DirectCast(rp.FindControl("lblClassRegistrationID"), Label)
                            Dim lblSchemeReportStatus As Label = DirectCast(rp.FindControl("lblSchemeReportStatus"), Label)
                            Dim lnkSchemeReportStatus As LinkButton = DirectCast(rp.FindControl("lnkSchemeReportStatus"), LinkButton)
                            Dim lnkEassessment As LinkButton = DirectCast(rp.FindControl("lnkEassessment"), LinkButton) 'Added as part of #20672
                            Dim lnkDownload As LinkButton = DirectCast(rp.FindControl("lnkDownload"), LinkButton)

                            'Dim sAppealReportSQL As String = Database & "..spGetAppealReportAppStatus__c @StudentID=" & User1.PersonID & ",@CourseID=" & lblCourseID.Text & ",@ClassRegistrationID=" & lblClassRegistrationID.Text
                            Dim sAppealReportSQL As String = Database & "..spGetAppealReportAppStatus__c @StudentID=" & StudentId & ",@CourseID=" & lblCourseID.Text & ",@ClassRegistrationID=" & lblClassRegistrationID.Text
                            Dim dtAppealReport As DataTable = DataAction.GetDataTable(sAppealReportSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
                            If Not dtAppealReport Is Nothing AndAlso dtAppealReport.Rows.Count > 0 Then
                                list.Visible = False
                                lblSchemeReportStatus.Visible = False
                                lnkSchemeReportStatus.Visible = True
                                lblSchemeReportStatus.Text = Convert.ToString(dtAppealReport.Rows(0)("Name"))
                                lnkSchemeReportStatus.Text = Convert.ToString(dtAppealReport.Rows(0)("Status"))
                                If Convert.ToString(dtAppealReport.Rows(0)("Status")).Trim.ToLower = "complete" Then
                                    lnkDownload.Visible = True
                                    lnkDownload.Text = "Download " & lblSchemeReportStatus.Text
                                    lnkSchemeReportStatus.Visible = False
                                Else
                                    lnkDownload.Visible = False

                                End If
                            Else
                                list.Visible = True
                                lblSchemeReportStatus.Visible = False
                                lnkDownload.Visible = False
                            End If

                            'Dim sSqlForCircumstanse As String = Database & "..spGetAppealAppStatus__c @PersonID=" & User1.PersonID & ",@TypeOfResonID=" & AptifyApplication.GetEntityRecordIDFromRecordName("AppealReason__c", "Extenuating Circumstances") & ",@CourseID=" & lblCourseID.Text & ",@ClassID=" & lblClassID.Text & ",@ClassRegistrationID=" & lblClassRegistrationID.Text
                            Dim sSqlForCircumstanse As String = Database & "..spGetAppealAppStatus__c @PersonID=" & StudentId & ",@TypeOfResonID=" & AptifyApplication.GetEntityRecordIDFromRecordName("AppealReason__c", "Extenuating Circumstances") & ",@CourseID=" & lblCourseID.Text & ",@ClassID=" & lblClassID.Text & ",@ClassRegistrationID=" & lblClassRegistrationID.Text
                            Dim sCircumstanseStatus As String = Convert.ToString(DataAction.ExecuteScalar(sSqlForCircumstanse, IAptifyDataAction.DSLCacheSetting.BypassCache))
                            If sCircumstanseStatus <> "" Then
                                lnkCircumstancess.Text = sCircumstanseStatus
                            End If
                            'Dim sSqlClericalRecheck As String = Database & "..spGetAppealAppStatus__c @PersonID=" & User1.PersonID & ",@TypeOfResonID=" & AptifyApplication.GetEntityRecordIDFromRecordName("AppealReason__c", "Clerical Recheck") & ",@CourseID=" & lblCourseID.Text & ",@ClassID=" & lblClassID.Text & ",@ClassRegistrationID=" & lblClassRegistrationID.Text
                            Dim sSqlClericalRecheck As String = Database & "..spGetAppealAppStatus__c @PersonID=" & StudentId & ",@TypeOfResonID=" & AptifyApplication.GetEntityRecordIDFromRecordName("AppealReason__c", "Clerical Recheck") & ",@CourseID=" & lblCourseID.Text & ",@ClassID=" & lblClassID.Text & ",@ClassRegistrationID=" & lblClassRegistrationID.Text
                            Dim sClericalRecheckStatus As String = Convert.ToString(DataAction.ExecuteScalar(sSqlClericalRecheck, IAptifyDataAction.DSLCacheSetting.BypassCache))
                            If sClericalRecheckStatus <> "" Then
                                lnkClericalRecheck.Text = sClericalRecheckStatus
                            End If
                            'Dim sSqlAppealClericalRecheck As String = Database & "..spGetAppealAppStatus__c @PersonID=" & User1.PersonID & ",@TypeOfResonID=" & AptifyApplication.GetEntityRecordIDFromRecordName("AppealReason__c", "Appeal Clerical Recheck") & ",@CourseID=" & lblCourseID.Text & ",@ClassID=" & lblClassID.Text & ",@ClassRegistrationID=" & lblClassRegistrationID.Text
                            Dim sSqlAppealClericalRecheck As String = Database & "..spGetAppealAppStatus__c @PersonID=" & StudentId & ",@TypeOfResonID=" & AptifyApplication.GetEntityRecordIDFromRecordName("AppealReason__c", "Appeal Clerical Recheck") & ",@CourseID=" & lblCourseID.Text & ",@ClassID=" & lblClassID.Text & ",@ClassRegistrationID=" & lblClassRegistrationID.Text
                            Dim sAppealClericalRecheckStatus As String = Convert.ToString(DataAction.ExecuteScalar(sSqlAppealClericalRecheck, IAptifyDataAction.DSLCacheSetting.BypassCache))
                            If sAppealClericalRecheckStatus <> "" Then
                                ' lnkAppealClericalRecheck.Text = sAppealClericalRecheckStatus
                            End If

                            Dim lblCircumstancess As Label = DirectCast(rp.FindControl("lblCircumstancess"), Label)
                            Dim lblClericalRecheck As Label = DirectCast(rp.FindControl("lblClericalRecheck"), Label)

                            Dim lblSessionID As Label = DirectCast(rp.FindControl("lblSessionID"), Label)
                            Dim sDisplayExamNumber As String
                            If lblSessionID.Text <> "" Then
                                sDisplayExamNumber = Database & "..spDisplayExamNumberBeforePublishDate__c @AcademicCycleID=" & ViewState("AcademicCycleID") & _
                                                                                                              ",@CurriculumID=" & lbl.Text & ",@SessionID=" & lblSessionID.Text
                            Else
                                sDisplayExamNumber = Database & "..spDisplayExamNumberBeforePublishDate__c @AcademicCycleID=" & ViewState("AcademicCycleID") & _
                                                                                                             ",@CurriculumID=" & lbl.Text & ",@SessionID=0"
                            End If

                            Dim IsDisplayExamNumber As Boolean = Convert.ToBoolean(DataAction.ExecuteScalar(sDisplayExamNumber, IAptifyDataAction.DSLCacheSetting.BypassCache))
                            If IsDisplayExamNumber = True Then
                                lnkCircumstancess.Visible = True
                                lnkClericalRecheck.Visible = True
                                lblCircumstancess.Text = ""
                                lblClericalRecheck.Text = ""
                                list.Enabled = True
                            Else
                                lblCircumstancess.Text = sCircumstanseStatus
                                lnkCircumstancess.Visible = False
                                lblClericalRecheck.Text = sClericalRecheckStatus
                                lnkClericalRecheck.Visible = False
                                list.Enabled = False

                            End If
                            ' If Getdate > =(Publish Date + Appeal End Days ) then not shown Appeal link and report drop down
                            Dim sAppealsDisplaySQL As String = Database & "..spCheckAppealsDateAvailable__c @AcademicCycleID=" & ViewState("AcademicCycleID") & _
                                                                                                              ",@CurriculumID=" & lbl.Text & ",@SessionID=" & lblSessionID.Text
                            Dim bAppealDisplay As Boolean = Convert.ToInt32(DataAction.ExecuteScalar(sAppealsDisplaySQL, IAptifyDataAction.DSLCacheSetting.BypassCache))
                            If bAppealDisplay = True Then
                                lnkCircumstancess.Visible = True
                                lnkClericalRecheck.Visible = True
                                lblCircumstancess.Text = ""
                                lblClericalRecheck.Text = ""
                                list.Enabled = True

                            Else
                                lblCircumstancess.Text = sCircumstanseStatus
                                lnkCircumstancess.Visible = False
                                lblClericalRecheck.Text = sClericalRecheckStatus
                                lnkClericalRecheck.Visible = False
                                list.Visible = False
                                If lnkSchemeReportStatus.Visible = True Then
                                    lnkSchemeReportStatus.Visible = False
                                End If
                                If lnkDownload.Visible = True Then
                                Else
                                    lblSchemeReportStatus.Visible = True

                                End If
                            End If
                            If Convert.ToString(lblStatus.Text).Trim.ToLower <> "failed" Then
                                list.Visible = False
                                lnkCircumstances.Visible = False
                                lnkClericalRecheck.Visible = False
                                lblSchemeReportStatus.Visible = False
                                lnkDownload.Visible = False
                                lnkCircumstancess.Visible = False
                                'lnkAppealClericalRecheck.Visible = False
                            End If


                            Dim lblTopScorere As Label = TryCast(rp.FindControl("lblTopScorere"), Label)
                            If lblTopScorere.Text.Trim <> "" Then
                                bTopScorere = True
                                bExamTopScorer = True
                            End If

                            If Convert.ToString(lblStatus.Text).Trim.ToLower = "failed" Then
                                bFailed = True
                                
                                'Added as part of #20672
                                Dim strCourses As String = Convert.ToString(AptifyApplication.GetEntityAttribute("Curriculum Definitions", "EassessmentCourses__c"))
                                Dim Courses() As String = Split(strCourses, ",")
                                ''Added by Harish - Redmine #21045 - Students who are unsuccessful in either their Law (ROI) or (NI) exams do not have the right of appeal, so they should no longer be able to purchase any of the following products:
                                ''Breakdown of Marks, Clerical Re - Check, Grounds(i) & (ii) Appeal and Tutorial report.

                                Dim StrfailedCourses As String = Convert.ToString(AptifyApplication.GetEntityAttribute("Curriculum Definitions", "InvisibleAppealLink__c"))
                                Dim FailedCourses() As String = Split(StrfailedCourses, ",")
                                ''End Code  - Redmine #21045
                                If Courses.Contains(lblCourseID.Text) Then
                                    lnkEassessment.Visible = True
                                    lnkEassessment.Text = "Purchase re-sit"
                                    lnkEassessment.PostBackUrl = eAssessmentURL

                                    ''Added by Harish - Redmine #21045 - Students who are unsuccessful in either their Law (ROI) or (NI) exams do not have the right of appeal, so they should no longer be able to purchase any of the following products:
                                    ''Breakdown of Marks, Clerical Re - Check, Grounds(i) & (ii) Appeal and Tutorial report.
                                    If FailedCourses.Contains(lblCourseID.Text) Then
                                        list.Visible = False
                                        lnkCircumstancess.Visible = False
                                        lnkClericalRecheck.Visible = False
                                    End If
                                    ''End Code  - Redmine #21045
                                End If
                                'End Of #20672
                            End If




                            If Request.QueryString("ID") IsNot Nothing AndAlso Request.QueryString("Year") IsNot Nothing Then
                                list.Enabled = False
                                lnkCircumstances.Enabled = False
                                lnkClericalRecheck.Enabled = False
                                lnkSchemeReportStatus.Enabled = False
                                lnkDownload.Enabled = False
                                lnkCircumstancess.Enabled = False
                                'lnkClericalRecheck.Visible = False
                                'lnkSchemeReportStatus.Visible = False
                                'lnkDownload.Visible = False
                            End If
                        Next
                        If bTopScorere Then
                            rpCurrentExamDetails.Controls(0).FindControl("topScore").Visible = True
                            For i As Integer = 1 To rpCurrentExamDetails.Items.Count
                                rpCurrentExamDetails.Controls(i).FindControl("tdTopScoreComments").Visible = True
                            Next
                        Else
                            rpCurrentExamDetails.Controls(0).FindControl("topScore").Visible = False
                            For i As Integer = 1 To rpCurrentExamDetails.Items.Count
                                rpCurrentExamDetails.Controls(i).FindControl("tdTopScoreComments").Visible = False
                            Next
                        End If
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub lnkCircumstances_OnClick(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Dim lblCourseID As Label
                Dim lblClassID As Label
                Dim lblClassRegistrationID As Label

                For Each ri As RepeaterItem In rpCurrentExam.Items
                    Dim rpCurrentExamDetails As Repeater = DirectCast(ri.FindControl("rpCurrentExamDetails"), Repeater)
                    For Each ri1 As RepeaterItem In rpCurrentExamDetails.Items
                        lblCourseID = DirectCast(ri1.FindControl("lblCourseID"), Label)
                        lblClassID = DirectCast(ri1.FindControl("lblClassID"), Label)
                        lblClassRegistrationID = DirectCast(ri1.FindControl("lblClassRegistrationID"), Label)
                        If lblCourseID.Text > 0 AndAlso lblClassID.Text > 0 AndAlso lblClassRegistrationID.Text > 0 Then
                            Exit For
                        End If
                    Next
                    If lblCourseID.Text > 0 AndAlso lblClassID.Text > 0 AndAlso lblClassRegistrationID.Text > 0 Then
                        Exit For
                    End If
                Next
                If lblCourseID.Text > 0 AndAlso lblClassID.Text > 0 AndAlso lblClassRegistrationID.Text > 0 Then
                    'Dim lCourseID As Integer = Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblCourseID.Text)
                    'Dim lClassID As Integer = Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblClassID.Text)
                    'Dim lClassRegistrationID As Integer = Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblClassRegistrationID.Text)
                    'Response.Redirect("~\Education\AppealApp.aspx?CourseID=" & System.Web.HttpUtility.UrlEncode(lCourseID) & "&ClassID=" & System.Web.HttpUtility.UrlEncode(lClassID) & "&ClassRegistrationID=" & System.Web.HttpUtility.UrlEncode(lClassRegistrationID))
                    Response.Redirect("~\Education\AppealApp.aspx?CourseID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblCourseID.Text)) & "&ClassID=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblClassID.Text)) & "&CrId=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblClassRegistrationID.Text)) & "&Type=" & System.Web.HttpUtility.UrlEncode(Aptify.Framework.Web.Common.WebCryptography.Encrypt("Extenuating Circumstances")))

                    'Response.Redirect("~\Education\AppealApp.aspx?CourseID=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblCourseID.Text) & "&ClassID=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblClassID.Text) & "&ClassRegistrationID=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblClassRegistrationID.Text) & "&Type=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt("Extenuating Circumstances"))

                End If
                'Dim lblCurriculumnID As Label


            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub rpCurrentExamDetails_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                'code commented by Govind 06 April 2016 for G3-P 20
                ''If rdoCurrentAcademicCycle.Checked = True Then
                ''    Session("FirstACCycle") = True
                ''    Session("SecondACCycle") = False
                ''ElseIf rdoNextAcadmicCycle.Checked = True Then
                ''    Session("FirstACCycle") = False
                ''    Session("SecondACCycle") = True
                ''End If
                Session("SelectedAcademicCycle") = ddlAcademicYear.SelectedValue
                Dim lblCourseID As Label
                Dim lblClassID As Label
                Dim lblClassRegistrationID As Label
                Dim lnkCircumstancess As LinkButton
                Dim lnkClericalRecheck As LinkButton
                'Dim lnkAppealClericalRecheck As LinkButton
                Dim lblRowID As Label
                lblCourseID = DirectCast(e.Item.FindControl("lblCourseID"), Label)
                lblClassID = DirectCast(e.Item.FindControl("lblClassID"), Label)
                lnkCircumstancess = DirectCast(e.Item.FindControl("lnkCircumstancess"), LinkButton)
                lnkClericalRecheck = DirectCast(e.Item.FindControl("lnkClericalRecheck"), LinkButton)
                'lnkAppealClericalRecheck = DirectCast(e.Item.FindControl("lnkAppealClericalRecheck"), LinkButton)
                lblClassRegistrationID = DirectCast(e.Item.FindControl("lblClassRegistrationID"), Label)
                Dim lblExamNumber As Label = DirectCast(e.Item.FindControl("lblExamNumber"), Label)
                lblRowID = DirectCast(e.Item.FindControl("lblRowID"), Label)
                Dim lnkDownload As LinkButton = DirectCast(e.Item.FindControl("lnkDownload"), LinkButton)
                Dim lblOrderID As Label = DirectCast(e.Item.FindControl("lblOrderID"), Label)

                Dim firstlink As Integer
                If lnkCircumstancess.Text = "Request" Then
                    firstlink = 0
                ElseIf lnkCircumstancess.Text = "Requested" Then
                    firstlink = 1
                End If
                Dim secondlink As Integer
                If lnkClericalRecheck.Text = "Request" Then
                    secondlink = 0
                ElseIf lnkClericalRecheck.Text = "Requested" Then
                    secondlink = 1
                End If
                Dim thirdlink As Integer
                'If lnkAppealClericalRecheck.Text = "Request" Then
                '    thirdlink = 0
                'ElseIf lnkAppealClericalRecheck.Text = "Requested" Then
                '    thirdlink = 1
                'End If
                Dim lCourseID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblCourseID.Text)
                Dim lClassID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblClassID.Text)
                Dim lClassRegistrationID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblClassRegistrationID.Text)
                Dim lTypeID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(e.CommandName)
                Dim lRowID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblRowID.Text)
                Dim lfirstlinkID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(firstlink)
                Dim lSecondlinkID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(secondlink)
                Dim lExamNumberID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblExamNumber.Text)
                Dim lOrderID As String = Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblOrderID.Text)
                'If e.CommandName = "Extenuating Circumstances" Then ' commeted by GM for Redmine #19841
                If e.CommandName = "Grounds (i) & (ii)" Then
                    ' lnkCircumstancess.Text = "Requested"
                    lfirstlinkID = 2

                    ''Response.Redirect("~\Education\AppealApp.aspx?CourseID=" & System.Web.HttpUtility.UrlEncode(lCourseID) & "&ClassID=" & System.Web.HttpUtility.UrlEncode(lClassID) & "&ClassRegistrationID=" & System.Web.HttpUtility.UrlEncode(lClassRegistrationID) & "&Type=" & System.Web.HttpUtility.UrlEncode(lTypeID) & "&RID=" & System.Web.HttpUtility.UrlEncode(lRowID) & "&first=" & System.Web.HttpUtility.UrlEncode(lfirstlinkID) & "&second=" & System.Web.HttpUtility.UrlEncode(lSecondlinkID) & "&ExamNumber=" & System.Web.HttpUtility.UrlEncode(lExamNumberID) & "&OrderID=" & System.Web.HttpUtility.UrlEncode(lOrderID))
                    Response.Redirect("~\Education\AppealApp.aspx?CrId=" & System.Web.HttpUtility.UrlEncode(lClassRegistrationID) & "&Type=" & System.Web.HttpUtility.UrlEncode(lTypeID))

                    ' Response.Redirect("~\Education\AppealApp.aspx?CourseID=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblCourseID.Text) & "&ClassID=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblClassID.Text) & "&ClassRegistrationID=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblClassRegistrationID.Text) & "&Type=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(e.CommandName) & "&RID=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblRowID.Text) & "&first=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(firstlink) & "&second=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(secondlink) & "&ExamNumber=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblExamNumber.Text))
                ElseIf e.CommandName = "Clerical Recheck" Then
                    ' lnkClericalRecheck.Text = "Requested"
                    lSecondlinkID = 2
                    'Response.Redirect("~\Education\AppealApp.aspx?CourseID=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblCourseID.Text) & "&ClassID=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblClassID.Text) & "&ClassRegistrationID=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblClassRegistrationID.Text) & "&Type=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(e.CommandName) & "&RID=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblRowID.Text) & "&first=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(firstlink) & "&second=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(secondlink) & "&ExamNumber=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblExamNumber.Text))
                    ''Response.Redirect("~\Education\AppealApp.aspx?CourseID=" & System.Web.HttpUtility.UrlEncode(lCourseID) & "&ClassID=" & System.Web.HttpUtility.UrlEncode(lClassID) & "&ClassRegistrationID=" & System.Web.HttpUtility.UrlEncode(lClassRegistrationID) & "&Type=" & System.Web.HttpUtility.UrlEncode(lTypeID) & "&RID=" & System.Web.HttpUtility.UrlEncode(lRowID) & "&first=" & System.Web.HttpUtility.UrlEncode(lfirstlinkID) & "&second=" & System.Web.HttpUtility.UrlEncode(lSecondlinkID) & "&ExamNumber=" & System.Web.HttpUtility.UrlEncode(lExamNumberID) & "&OrderID=" & System.Web.HttpUtility.UrlEncode(lOrderID))
                    Response.Redirect("~\Education\AppealApp.aspx?CrId=" & System.Web.HttpUtility.UrlEncode(lClassRegistrationID) & "&Type=" & System.Web.HttpUtility.UrlEncode(lTypeID))


                ElseIf e.CommandName = "Appeal Clerical Recheck" Then
                    ' lnkAppealClericalRecheck.Text = "Requested"
                    thirdlink = 2
                    '  Response.Redirect("~\Education\AppealApp.aspx?CourseID=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblCourseID.Text) & "&ClassID=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblClassID.Text) & "&ClassRegistrationID=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblClassRegistrationID.Text) & "&Type=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(e.CommandName) & "&RID=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblRowID.Text) & "&first=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(firstlink) & "&second=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(secondlink) & "&ExamNumber=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblExamNumber.Text) & "&OrderID=" & System.Web.HttpUtility.UrlEncode(lOrderID))
                End If
                If e.CommandName = "Scheme Report Status" Then
                    Dim lblSchemeReportStatus As Label = DirectCast(e.Item.FindControl("lblSchemeReportStatus"), Label)
                    lTypeID = Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblSchemeReportStatus.Text)
                    '' Response.Redirect("~\Education\AppealApp.aspx?CourseID=" & System.Web.HttpUtility.UrlEncode(lCourseID) & "&ClassID=" & System.Web.HttpUtility.UrlEncode(lClassID) & "&ClassRegistrationID=" & System.Web.HttpUtility.UrlEncode(lClassRegistrationID) & "&Type=" & System.Web.HttpUtility.UrlEncode(lTypeID) & "&RID=" & System.Web.HttpUtility.UrlEncode(lRowID) & "&first=" & System.Web.HttpUtility.UrlEncode(lfirstlinkID) & "&second=" & System.Web.HttpUtility.UrlEncode(lSecondlinkID) & "&ExamNumber=" & System.Web.HttpUtility.UrlEncode(lExamNumberID) & "&OrderID=" & System.Web.HttpUtility.UrlEncode(lOrderID))
                    Response.Redirect("~\Education\AppealApp.aspx?CrId=" & System.Web.HttpUtility.UrlEncode(lClassRegistrationID) & "&Type=" & System.Web.HttpUtility.UrlEncode(lTypeID))
                    'Response.Redirect("~\Education\AppealApp.aspx?CourseID=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblCourseID.Text) & "&ClassID=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblClassID.Text) & "&ClassRegistrationID=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblClassRegistrationID.Text) & "&Type=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblSchemeReportStatus.Text) & "&RID=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblRowID.Text) & "&first=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(0) & "&second=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(0) & "&ExamNumber=" & Aptify.Framework.Web.Common.WebCryptography.Encrypt(lblExamNumber.Text))
                End If
                If e.CommandName = "Download" Then
                    Dim sSql As String = Database & "..spGetAppealReportFileName__c @PersonID=" & User1.PersonID & ",@CourseID=" & lblCourseID.Text & ",@ClassRegistrationID=" & lblClassRegistrationID.Text
                    Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                    If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                        Dim fileName As String = Convert.ToString(dt.Rows(0)("Name"))
                        Dim fileExtension As String = "." + fileName.Split(CChar(".")).Last.ToLower
                        Dim dataTable As New DataTable()
                        Dim sql As String = "..spGetBlobData__c @EntityID=" & AptifyApplication.GetEntityID("AppealsApplicationDetail__c") & ",@RecordID=" & Convert.ToString(dt.Rows(0)("ID"))
                        dataTable = DataAction.GetDataTable(sql)

                        Dim FileData() As Byte
                        FileData = CType(dataTable.Rows(0)("BlobData"), Byte())

                        'Dim fs As New System.IO.FileStream(Server.MapPath("\ebusiness\Temp"), IO.FileMode.Create, IO.FileAccess.Write)
                        'fs.Write(FileData, 0, FileData.Length)
                        'fs.Close()               
                        If ContentType(fileExtension) <> "" Then
                            Response.Buffer = True
                            Response.Cache.SetCacheability(HttpCacheability.NoCache)
                            Response.ContentType = ContentType(fileExtension)
                            Response.AddHeader("content-disposition", "attachment;filename=" + fileName)
                            Response.BinaryWrite(FileData)
                            Response.Flush()
                            Response.End()
                        End If

                    End If

                End If

                If e.CommandName = "FAEStatus" Then
                    Response.Redirect("~\Current-Student\Student-Centre\EducationFAEResultDetails.aspx?CrId=" & System.Web.HttpUtility.UrlEncode(lClassRegistrationID))
                End If
            End If

        End Sub
        Private Function ContentType(ByVal fileExtension As String) As String
            Try
                Dim d As New Dictionary(Of String, String)
                'Images'
                d.Add(".bmp", "image/bmp")
                d.Add(".gif", "image/gif")
                d.Add(".jpeg", "image/jpeg")
                d.Add(".jpg", "image/jpeg")
                d.Add(".png", "image/png")
                d.Add(".tif", "image/tiff")
                d.Add(".tiff", "image/tiff")
                'Documents'
                d.Add(".doc", "application/msword")
                d.Add(".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
                d.Add(".pdf", "application/pdf")
                'Slideshows'
                d.Add(".ppt", "application/vnd.ms-powerpoint")
                d.Add(".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation")
                'Data'
                d.Add(".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                d.Add(".xls", "application/vnd.ms-excel")
                d.Add(".csv", "text/csv")
                d.Add(".xml", "text/xml")
                d.Add(".txt", "text/plain")
                'Compressed Folders'
                d.Add(".zip", "application/zip")
                'Audio'
                d.Add(".ogg", "application/ogg")
                d.Add(".mp3", "audio/mpeg")
                d.Add(".wma", "audio/x-ms-wma")
                d.Add(".wav", "audio/x-wav")
                'Video'
                d.Add(".wmv", "audio/x-ms-wmv")
                d.Add(".swf", "application/x-shockwave-flash")
                d.Add(".avi", "video/avi")
                d.Add(".mp4", "video/mp4")
                d.Add(".mpeg", "video/mpeg")
                d.Add(".mpg", "video/mpeg")
                d.Add(".qt", "video/quicktime")
                ' Crystal Report
                d.Add(".rpt", "application/rpt")
                Return d(fileExtension)

            Catch ex As Exception
                Return ""
            End Try

        End Function
        Public Property CurrentAddProductTable() As DataTable
            Get

                If Not Session("CurrentAddProductTable") Is Nothing Then
                    Return CType(Session("CurrentAddProductTable"), DataTable)
                Else
                    Dim dtCurrentAddProductTable As New DataTable
                    dtCurrentAddProductTable.Columns.Add("RID")
                    dtCurrentAddProductTable.Columns.Add("first")
                    dtCurrentAddProductTable.Columns.Add("second")
                    dtCurrentAddProductTable.Columns.Add("third")
                    Return dtCurrentAddProductTable
                End If
            End Get
            Set(ByVal value As DataTable)
                Session("CurrentAddProductTable") = value
            End Set
        End Property
        Private Sub EducationDetailsOnExamAppeal()
            Try
                If IsNothing(Request.QueryString("RID")) Then
                Else
                    Dim RID As Integer = Convert.ToInt32(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("RID")))
                    'Dim Type As String = Convert.ToString(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("Type")))
                    If RID > 0 Then
                        Dim dtCurrentAddProductTable As DataTable
                        If Not Session("AddedRows") Is Nothing Then
                            dtCurrentAddProductTable = DirectCast(Session("AddedRows"), DataTable)
                        Else
                            dtCurrentAddProductTable = CurrentAddProductTable
                        End If
                        Dim drCurrentAddProductTable As DataRow = dtCurrentAddProductTable.NewRow()
                        drCurrentAddProductTable("RID") = RID
                        drCurrentAddProductTable("first") = Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("first"))
                        drCurrentAddProductTable("second") = Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("second"))
                        drCurrentAddProductTable("third") = Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("third"))
                        dtCurrentAddProductTable.Rows.Add(drCurrentAddProductTable)
                        Session("AddedRows") = dtCurrentAddProductTable
                        Dim rpCurrentExamDetails As Repeater
                        If Not dtCurrentAddProductTable Is Nothing AndAlso dtCurrentAddProductTable.Rows.Count > 0 Then
                            For Each ri As RepeaterItem In rpCurrentExam.Items
                                rpCurrentExamDetails = DirectCast(ri.FindControl("rpCurrentExamDetails"), Repeater)
                                If Not rpCurrentExamDetails Is Nothing Then
                                    Exit For
                                End If
                            Next
                            If Not rpCurrentExamDetails Is Nothing Then
                                For Each dr As DataRow In dtCurrentAddProductTable.Rows
                                    If Convert.ToInt32(dr("first")) = 1 OrElse Convert.ToInt32(dr("first")) = 2 Then
                                        Dim lnkCircumstancess As LinkButton = DirectCast(rpCurrentExamDetails.Items(CInt(dr("RID")) - 1).FindControl("lnkCircumstancess"), LinkButton)
                                        lnkCircumstancess.Text = "Requested"
                                    Else
                                        Dim lnkCircumstancess As LinkButton = DirectCast(rpCurrentExamDetails.Items(CInt(dr("RID")) - 1).FindControl("lnkCircumstancess"), LinkButton)
                                        lnkCircumstancess.Text = "Request"
                                    End If
                                    If Convert.ToInt32(dr("second")) = 1 OrElse Convert.ToInt32(dr("second")) = 2 Then
                                        Dim lnkClericalRecheck As LinkButton = DirectCast(rpCurrentExamDetails.Items(CInt(dr("RID")) - 1).FindControl("lnkClericalRecheck"), LinkButton)
                                        lnkClericalRecheck.Text = "Requested"
                                    Else
                                        Dim lnkCircumstancess As LinkButton = DirectCast(rpCurrentExamDetails.Items(CInt(dr("RID")) - 1).FindControl("lnkCircumstancess"), LinkButton)
                                        lnkCircumstancess.Text = "Request"
                                    End If

                                Next
                            End If



                        End If

                    ElseIf Not Session("RID") Is Nothing AndAlso Convert.ToString(Session("RID")) <> "" Then
                        For Each ri As RepeaterItem In rpCurrentExam.Items
                            Dim rpCurrentExamDetails As Repeater = DirectCast(ri.FindControl("rpCurrentExamDetails"), Repeater)

                            Dim sROWID As String() = Session("RID").Split(New Char() {","c})

                            ' Use For Each loop over words and display them
                            Dim word As String
                            For Each word In sROWID


                            Next

                        Next
                    End If
                End If



            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Get Curriculum ID
        ''' </summary>
        ''' <param name="ClassRegistrationID"></param>
        ''' <remarks></remarks>
        Private Sub GetCurriculumID(ByVal ClassRegistrationID As Integer)
            Try
                Dim sSql As String = Database & "..spGetCurriculumIDbyCR__c @ClassRegistrationID=" & ClassRegistrationID
                Dim CurriculumID As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache))
                If CurriculumID > 0 Then
                    ViewState("CurriculumID") = CurriculumID
                Else
                    ViewState("CurriculumID") = 0
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Sub btnSubmitPay_Click(sender As Object, e As System.EventArgs) Handles btnSubmitPay.Click
            Try


                For Each ri As RepeaterItem In rpCurrentExam.Items
                    Dim rpCurrentExamDetails As Repeater = DirectCast(ri.FindControl("rpCurrentExamDetails"), Repeater)
                    For Each rp As RepeaterItem In rpCurrentExamDetails.Items
                        Dim list As DropDownList = TryCast(rp.FindControl("drpSchemeReport"), DropDownList)
                        Dim lblClassRegistrationID As Label = TryCast(rp.FindControl("lblClassRegistrationID"), Label)
                        Dim lblCourse As Label = TryCast(rp.FindControl("lblCourse"), Label)
                        GetCurriculumID(lblClassRegistrationID.Text)
                        If list.SelectedValue <> "--Select--" Then
                            Dim oOrder As Aptify.Applications.OrderEntry.OrdersEntity
                            oOrder = ShoppingCart1.GetOrderObject(Page.Session, Page.User, Page.Application)
                            Dim bAlradyAdded As Boolean = False
                            For i As Integer = 0 To oOrder.SubTypes("OrderLines").Count - 1
                                If Convert.ToInt32(list.SelectedValue) = Convert.ToInt32(oOrder.SubTypes("OrderLines").Item(i).GetValue("ProductID")) AndAlso Convert.ToInt32(oOrder.SubTypes("OrderLines").Item(i).GetValue("ClassRegistrationID__c")) = Convert.ToInt32(lblClassRegistrationID.Text) Then
                                    bAlradyAdded = True
                                    Exit For
                                End If
                            Next
                            If bAlradyAdded = False Then
                                ShoppingCart1.AddToCart(Convert.ToInt32(list.SelectedValue), False, , 1)
                                Dim iOrderLineCount As Integer = -1, ol As OrderLinesEntity = Nothing
                                iOrderLineCount = oOrder.SubTypes("OrderLines").Count - 1
                                ol = oOrder.SubTypes("OrderLines").Item(iOrderLineCount)
                                If ol IsNot Nothing Then
                                    ol.SetValue("ClassRegistrationID__c", lblClassRegistrationID.Text)
                                    Dim sAppealResonIDSQL As String = Database & "..spGetAppealReasonIDbyProductID__c @ProductID=" & list.SelectedValue & ",@CurriculumID=" & ViewState("CurriculumID")
                                    Dim lAppResonID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sAppealResonIDSQL, IAptifyDataAction.DSLCacheSetting.BypassCache))

                                    ol.SetValue("AppealTypeResonID__c", Convert.ToString(lAppResonID))
                                    ol.SetValue("Comments", "")
                                    ol.SetValue("Description", Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.EducationDetailsPage.AppealCourseFor")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & " " & lblCourse.Text)
                                End If
                                ''For Each ol As OrderLinesEntity In oOrder.SubTypes("OrderLines")
                                ''    If CLng(ol.GetValue("ProductID")) = CLng(list.SelectedValue) Then
                                ''        ol.SetValue("ClassRegistrationID__c", lblClassRegistrationID.Text)
                                ''        Dim sAppealResonIDSQL As String = Database & "..spGetAppealReasonIDbyProductID__c @ProductID=" & list.SelectedValue
                                ''        Dim lAppResonID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sAppealResonIDSQL, IAptifyDataAction.DSLCacheSetting.BypassCache))

                                ''        ol.SetValue("AppealTypeResonID__c", Convert.ToString(lAppResonID))
                                ''        ol.SetValue("Comments", "")
                                ''        ol.SetValue("Description", Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.EducationDetailsPage.AppealCourseFor")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials) & " " & lblCourse.Text)
                                ''    End If

                                ''Next
                                ShoppingCart1.SaveCart(Page.Session)
                            End If

                        End If

                    Next
                Next
                'Response.Redirect(ViewcartPage, False)
                Response.Redirect("~\ProductCatalog\ViewCart.aspx", False)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub rdoCurrentAcademicCycle_CheckedChanged(sender As Object, e As System.EventArgs) Handles rdoCurrentAcademicCycle.CheckedChanged
            Try
                Session("FirstACCycle") = True
                Session("SecondACCycle") = False
                LoadHeaderText(User1.PersonID)
                GetPrefferedCurrency(User1.PersonID)
                LoadEducationResults(User1.PersonID)
                LoadCurrentExamsGrid()
                LoadInterimAssment(User1.PersonID)
                'LoadResitAssment()
                LoadMock(User1.PersonID)
                EducationDetailsOnExamAppeal()
                LoadCurrentExam(User1.PersonID)
                If bFailed = True Then
                    btnSubmitPay.Visible = True
                Else
                    btnSubmitPay.Visible = False
                End If
                'ShowToFirmResult()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub DisplayNextAcademicCycle()
            Try
                Dim sSql As String = "..spGetNextAcademicCycleDetails__c"
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    rdoNextAcadmicCycle.Text = "Next Academic Cycle: " + Convert.ToString(dt.Rows(0)("Name"))
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub LoadNextAcademicCycle()
            Try
                Dim sSql As String = "..spGetNextAcademicCycleDetails__c"
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    rdoNextAcadmicCycle.Text = "Next Academic Cycle: " + Convert.ToString(dt.Rows(0)("Name"))
                    ViewState("AcademicCycleID") = Convert.ToString(dt.Rows(0)("ID"))

                    ''Added bY Pradip For G3-37
                    ViewState("NextAcademicCycleID") = Convert.ToString(dt.Rows(0)("ID"))
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''Added BY Pradip 2016-02-29 for G3-37 
        Private Sub GetNextAcademicCycle()
            Try
                Dim sSql As String = "..spGetNextAcademicCycleDetails__c"
                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    'rdoNextAcadmicCycle.Text = "Next Academic Cycle: " + Convert.ToString(dt.Rows(0)("Name"))
                    ' ViewState("AcademicCycleID") = Convert.ToString(dt.Rows(0)("ID"))
                    ''Added bY Pradip For G3-37
                    ViewState("NextAcademicCycleID") = Convert.ToString(dt.Rows(0)("ID"))
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub rdoNextAcadmicCycle_CheckedChanged(sender As Object, e As System.EventArgs) Handles rdoNextAcadmicCycle.CheckedChanged
            Try
                Session("FirstACCycle") = False
                Session("SecondACCycle") = True
                LoadNextAcademicCycle()
                GetPrefferedCurrency(User1.PersonID)
                LoadEducationResults(User1.PersonID)
                LoadCurrentExamsGrid()
                LoadInterimAssment(User1.PersonID)
                'LoadResitAssment()
                LoadMock(User1.PersonID)
                EducationDetailsOnExamAppeal()
                LoadCurrentExam(User1.PersonID)
                If bFailed = True Then
                    btnSubmitPay.Visible = True
                Else
                    btnSubmitPay.Visible = False
                End If
                'ShowToFirmResult()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        'Protected Sub rpEducationResult_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rpEducationResult.ItemDataBound
        '    Try
        '        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
        '            Dim lblCurriculumID As Label = DirectCast(e.Item.FindControl("lblCurriculumID"), Label)
        '            Dim lblAcademicCycleID As Label = DirectCast(e.Item.FindControl("lblAcademicCycleID"), Label)
        '            Dim lblStatus As Label = DirectCast(e.Item.FindControl("lblStatus"), Label)
        '            If lblStatus.Text.Trim.ToLower <> "pass" Then
        '                Dim sSql As String = Database & "..spGetCurriculumWiseResult__c @StudentID=" & User1.PersonID & ",@AcadmicCycleID=" & lblAcademicCycleID.Text & ",@CurriculumID=" & lblCurriculumID.Text
        '                Dim dt As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
        '                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
        '                    Dim bFailed As Boolean = False
        '                    Dim sStatus As String = ""
        '                    For Each dr As DataRow In dt.Rows
        '                        If Convert.ToString(dr("Status")) = "Pass" Then
        '                            bFailed = True
        '                        End If
        '                    Next
        '                    If bFailed = False Then
        '                        lblStatus.Text = "Failed"
        '                    End If
        '                End If
        '            End If


        '        End If
        '    Catch ex As Exception
        '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        '    End Try
        'End Sub
        Protected Sub Page_Unload(sender As Object, e As System.EventArgs) Handles Me.Unload
            Session("ResultDetails.hdnCurrent") = hdnCurrent.Value
            Session("ResultDetails.hdnCurricula") = hdnCurricula.Value
            Session("ResultDetails.hdnInterim") = hdnInterim.Value
            Session("ResultDetails.hdnMock") = hdnMock.Value
            If rdoCurrentAcademicCycle.Checked = True Then
                Session("FirstACCycle") = True
                Session("SecondACCycle") = False
            ElseIf rdoNextAcadmicCycle.Checked = True Then
                Session("FirstACCycle") = False
                Session("SecondACCycle") = True
            End If

        End Sub

        Protected Sub lbtnCourseMaterial_Click(sender As Object, e As System.EventArgs) Handles lbtnCourseMaterial.Click
            Response.Redirect(Me.WebCourseMaterialPage)
        End Sub

        ''' <summary>
        ''' if Web Material Access Status is Approved then only display courses material link
        ''' </summary>
        Private Function HasWebMaterialAccess() As Boolean
            Try
                Dim sql As New StringBuilder()
                sql.AppendFormat("{0}..spGetWebMaterialStatusApproved__c @PersonID={1}", _
                                Me.Database, Me.User1.PersonID)
                Dim lWebMaterialApproved As Long = Convert.ToInt32(DataAction.ExecuteScalar(sql.ToString(), _
                        Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
                If lWebMaterialApproved > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Function
        ''' <summary>
        ''' Load all academic cycles 
        ''' </summary>
        Private Sub LoadAcademicYear()
            Try
                Dim currentAcademicCycleId As Integer
                'commented for redmine 17077

                'Dim sql As New StringBuilder()
                'sql.AppendFormat("{0} ..spGetAllAcademicCycles__c", Database)
                'ddlAcademicYear.DataSource = DataAction.GetDataTable(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                'ddlAcademicYear.DataValueField = "Id"
                'ddlAcademicYear.DataTextField = "Name"
                'ddlAcademicYear.DataBind()

                ''Added BY Pradip 2015-12-28 for Group 3 Tracker G3-35 to set the Accdemic Cycle To Current Academic.
                currentAcademicCycleId = LoadCurrentAcademicCycleID()

                If currentAcademicCycleId <> 0 Then
                    ' redmine 17077 to show past 2 and current academic cycles only.

                    Dim sql As New StringBuilder()
                    sql.AppendFormat("{0} ..spGetCurrentAndPastTwolAcademicCycles__c @CurrentAcademicCycleID=" & currentAcademicCycleId, Database)
                    ddlAcademicYear.DataSource = DataAction.GetDataTable(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                    ddlAcademicYear.DataValueField = "Id"
                    ddlAcademicYear.DataTextField = "Name"
                    ddlAcademicYear.DataBind()

                    ddlAcademicYear.SelectedValue = CStr(currentAcademicCycleId)
                    ViewState("AcademicCycleID") = ddlAcademicYear.SelectedValue
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Function LoadCurrentAcademicCycleID() As Integer
            Dim id As Integer = 0
            Dim sql As New StringBuilder()
            sql.AppendFormat("{0} ..spGetDefaultAcademicCycleToSet__c", Me.Database)
            id = Convert.ToInt32(DataAction.ExecuteScalar(sql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
            Return id
        End Function

        Protected Sub ddlAcademicYear_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlAcademicYear.SelectedIndexChanged
            Try
                ViewState("AcademicCycleID") = ddlAcademicYear.SelectedValue
                GetPrefferedCurrency(User1.PersonID)
                LoadEducationResults(User1.PersonID)
                LoadCurrentExamsGrid()
                LoadInterimAssment(User1.PersonID)
                'LoadResitAssment()
                LoadMock(User1.PersonID)
                EducationDetailsOnExamAppeal()
                LoadCurrentExam(User1.PersonID)
                If bFailed = True Then
                    btnSubmitPay.Visible = True
                    'Added By Pradip 2017-02-06 https://redmine.softwaredesign.ie/issues/16383
                    If Convert.ToInt32(ViewState("AppealDayOver")) > 0 Then
                        lblStatusMessage.Visible = False '' lblStatusMessage.Visible = True ' Updated by GM for Redmine #19842
                    Else
                        lblStatusMessage.Visible = False
                    End If
                Else
                    btnSubmitPay.Visible = False
                    lblStatusMessage.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        'Code added by GM for Redmine  log #19143
        Private Sub lnkReport_Click(sender As Object, e As EventArgs) Handles lnkReport.Click
            Try
                Dim ReportPageURL As String = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings("ReportPageURL"))
                Dim ReportID As Integer
                'below code commented by GM For Redmine #20348
                'If ViewState("ISFAEResult") Is Nothing Then
                '    ReportID = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "ExamResultLetterCAP1ANDCAP2__c"))
                'Else
                '    ReportID = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "ExamResultLetterFAE__c"))
                'End If
                'Added below line For Redmine #20348
                ReportID = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("CrystalReportDetails__c", "ExamResultLetter_WEB__cai"))

                Dim rptParam As New AptifyCrystalReport__c
                rptParam.ReportID = ReportID
                rptParam.Param1 = User1.PersonID
                rptParam.Param2 = ViewState("AcademicCycleID")
                Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower()) = rptParam
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "OpenWindow", "window.open('" & ReportPageURL & "' )", True)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
    End Class
End Namespace