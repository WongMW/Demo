''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer            Date Created/Modified               Summary
'Kavita Zinage        06/10/2015                      Assignment details page for Corrector
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Strict On
Option Explicit On

Imports System.Data
Imports Telerik.Web.UI
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness
Imports Aptify.Framework.Application
Imports System.IO
Namespace Aptify.Framework.Web.eBusiness.Education
    Partial Class AssignmentDetails__c
        Inherits BaseUserControlAdvanced

        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_ASSIGNMENTS As String = "AssignmentsPage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_NAVURL As String = "NavUrl"

        Dim dtNew As New DataTable


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

        Public Overridable Property AssignmentsPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_ASSIGNMENTS) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_ASSIGNMENTS))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_ASSIGNMENTS) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Public Overridable Property NavUrl() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_NAVURL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_NAVURL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_NAVURL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(LoginPage) Then
                LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_LOGINPAGE)
            End If
            If String.IsNullOrEmpty(AssignmentsPage) Then
                AssignmentsPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_ASSIGNMENTS)
            End If
            If String.IsNullOrEmpty(NavUrl) Then
                NavUrl = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_NAVURL)
            End If
        End Sub
#End Region

#Region "Page Events"
        ''' <summary>
        ''' Handles page load event
        ''' </summary>
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                SetProperties()
                If Not IsPostBack Then
                    ReLoadGrid()
                    LoadCurriculum()
                    LoadCourses()
                    LoadAssignments()
                    lnkHelp.NavigateUrl = NavUrl
                    lblExemptionsNotFound.Text = ""
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Handles page PreRender event
        ''' </summary>
        Protected Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
            gvAssignmentDetails.Columns(1).CurrentFilterFunction = Telerik.Web.UI.GridKnownFunction.EqualTo
        End Sub
#End Region

#Region "Private Functions"
        ''' <summary>
        ''' Load gvAssignmentDetails
        ''' </summary>
        Private Sub LoadGrid()
            Try
                Dim sSql As New StringBuilder()
                sSql.AppendFormat("{0}..spGetAssignmentDetailsData__c @CurriculumID={1},@CourseID={2},@AssignmentID={3}", _
                       Me.Database, Convert.ToInt32(ddlCurriculumList.SelectedValue), Convert.ToInt32(ddlCourseList.SelectedValue), Convert.ToInt32(ddlAssignmentList.SelectedValue))
                Dim dt As DataTable = DataAction.GetDataTable(sSql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    ViewState("AssignmentDetails") = dt

                    ' dtNew = dt.Clone
                    'dtNew = dt.cl
                    For Each c As DataColumn In dt.Columns
                        dtNew.Columns.Add(c.ColumnName)
                    Next
                    ViewState("dtNew") = dtNew
                    Me.gvAssignmentDetails.Visible = True
                    Me.gvAssignmentDetails.Rebind()

                    Dim i As Integer = 0
                    For Each rw As DataRow In dt.Rows
                        If ddlCurriculumList.SelectedItem.Text.Contains("FAE-") Then
                            gvAssignmentDetails.Items.Item(i).FindControl("ddlscore").Visible = True
                            gvAssignmentDetails.Items.Item(i).FindControl("txtScore").Visible = False
                        Else
                            gvAssignmentDetails.Items.Item(i).FindControl("txtScore").Visible = True
                            gvAssignmentDetails.Items.Item(i).FindControl("ddlscore").Visible = False
                        End If
                        i = i + 1
                    Next
                Else
                    ReLoadGrid()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub LoadGridAgain()
            Try
                Dim sSql As New StringBuilder()
                sSql.AppendFormat("{0}..spGetAssignmentDetailsData__c @CurriculumID={1},@CourseID={2},@AssignmentID={3}", _
                       Me.Database, Convert.ToInt32(ddlCurriculumList.SelectedValue), Convert.ToInt32(ddlCourseList.SelectedValue), Convert.ToInt32(ddlAssignmentList.SelectedValue))
                Dim dt As DataTable = DataAction.GetDataTable(sSql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                    ViewState("AssignmentDetails") = dt
                    dtNew = dt.Clone
                    'dtNew = dt.cl
                    'For Each c As DataColumn In dt.Columns
                    '    dtNew.Columns.Add(c.ColumnName)
                    'Next
                    ViewState("dtNew") = dtNew
                    Me.gvAssignmentDetails.Visible = True
                    Me.gvAssignmentDetails.Rebind()

                    Dim i As Integer = 0
                    For Each rw As DataRow In dt.Rows
                        If ddlCurriculumList.SelectedItem.Text.Contains("FAE-") Then
                            gvAssignmentDetails.Items.Item(i).FindControl("ddlscore").Visible = True
                            gvAssignmentDetails.Items.Item(i).FindControl("txtScore").Visible = False
                        Else
                            gvAssignmentDetails.Items.Item(i).FindControl("txtScore").Visible = True
                            gvAssignmentDetails.Items.Item(i).FindControl("ddlscore").Visible = False
                        End If
                        i = i + 1
                    Next
                Else
                    ReLoadGrid()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Load Curriculum
        ''' </summary>
        Private Sub LoadCurriculum()
            Try
                Dim sSql As String = Database & "..spGetCurriculumDefinationDetails__c"
                Dim dt As DataTable = DataAction.GetDataTable(sSql, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                ddlCurriculumList.DataSource = dt
                ddlCurriculumList.DataTextField = "Name"
                ddlCurriculumList.DataValueField = "ID"
                ddlCurriculumList.DataBind()
                ddlCurriculumList.Items.Insert(0, "Select Curriculum")
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Load drop down
        ''' </summary>
        Private Sub LoadCourses()
            Try
                Dim sSql As New StringBuilder()
                sSql.AppendFormat("{0}..spGetCoursebyCurriculumID__c @CurriculumID={1},@CorrectorID={2}", _
                       Me.Database, Convert.ToInt32(ddlCurriculumList.SelectedValue), User1.PersonID)
                Dim dt As DataTable = DataAction.GetDataTable(sSql.ToString(), Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                ddlCourseList.DataSource = dt
                ddlCourseList.DataTextField = "CourseName"
                ddlCourseList.DataValueField = "CourseID"
                ddlCourseList.DataBind()
                ddlCourseList.Items.Insert(0, "Select Course")
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Load drop down
        ''' </summary>
        Private Sub LoadAssignments()
            Try
                Dim sql As New StringBuilder()
                sql.AppendFormat("{0}..spGetAssignmentDetails__c @InstructorID={1},@CourseID={2}", Me.Database, Me.User1.PersonID, Convert.ToInt32(ddlCourseList.SelectedValue))
                Dim dt As DataTable = DataAction.GetDataTable(sql.ToString, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache)
                ddlAssignmentList.DataSource = dt
                ddlAssignmentList.DataTextField = "Name"
                ddlAssignmentList.DataValueField = "ID"
                ddlAssignmentList.DataBind()
                ddlAssignmentList.Items.Insert(0, "Select Assignment")
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Load Courses as per curriculum
        ''' </summary>
        Protected Sub ddlCurriculumList_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCurriculumList.SelectedIndexChanged
            Try
                LoadCourses()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Load Assignment Name as per Course
        ''' </summary>
        Protected Sub ddlCourseList_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCourseList.SelectedIndexChanged
            Try
                LoadAssignments()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

#End Region
        ''' <summary>
        ''' Handles Display button click
        ''' </summary>
        Protected Sub btnDisplay_Click(sender As Object, e As System.EventArgs) Handles btnDisplay.Click
            Try
                If ddlCurriculumList.SelectedValue = "Select Curriculum" Or ddlCourseList.SelectedValue = "Select Course" Or ddlAssignmentList.SelectedValue = "Select Assignment" Then
                    lblExemptionsNotFound.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                    Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.AssignmentDetailsPage.RequiredFieldMsg")), _
                    Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                Else
                    lblExemptionsNotFound.Text = String.Empty
                    LoadGrid()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Handles Add button click
        ''' </summary>
        Protected Sub cmdAdd_Click(sender As Object, e As System.EventArgs) Handles cmdAdd.Click
            Try
                AddSelectedRowToGrid()
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Handles CheckBox Checked Changed
        ''' </summary>
        Protected Sub chkSelect_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
            Try
                Dim chkSelect As CheckBox = CType(sender, CheckBox)
                Dim rowGridItem As GridDataItem = CType(chkSelect.NamingContainer, GridDataItem)
                Dim iIndex As Integer = rowGridItem.DataSetIndex
                If chkSelect.Checked = True Then
                    If ddlCurriculumList.SelectedItem.Text.Contains("FAE-") Then
                        CType(gvAssignmentDetails.Items(iIndex).FindControl("ddlscore"), DropDownList).Enabled = True
                    Else
                        CType(gvAssignmentDetails.Items(iIndex).FindControl("txtScore"), TextBox).ReadOnly = False
                    End If
                Else
                    If ddlCurriculumList.SelectedItem.Text.Contains("FAE-") Then
                        CType(gvAssignmentDetails.Items(iIndex).FindControl("ddlscore"), DropDownList).Enabled = False
                    Else
                        CType(gvAssignmentDetails.Items(iIndex).FindControl("txtScore"), TextBox).ReadOnly = True
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Function to Add selected Row to Grid
        ''' </summary>
        Private Sub AddSelectedRowToGrid()
            Try


                Dim dtAll As DataTable = CType(ViewState("AssignmentDetails"), DataTable)
                dtNew = CType(ViewState("dtNew"), DataTable)
                Dim checkCount As Integer = 0
                Dim ischeckCount As Integer = 0
                lblExemptionsNotFound.Text = ""
                If ddlCurriculumList.SelectedItem.Text.Contains("FAE-") Then
                    For i As Integer = 0 To gvAssignmentDetails.Items.Count - 1
                        checkCount = 0
                        If CType(gvAssignmentDetails.Items(i).FindControl("chkSelect"), CheckBox).Checked = True Then
                            Dim hdnPartStatusID As HiddenField = CType(gvAssignmentDetails.Items(i).FindControl("hdnPartStatusID"), HiddenField)
                            ischeckCount = ischeckCount + 1
                            If CType(gvAssignmentDetails.Items(i).FindControl("ddlscore"), DropDownList).SelectedValue <> "" Then

                                CType(gvAssignmentDetails.Items(i).FindControl("chkSelect"), CheckBox).Checked = False

                                CType(gvAssignmentDetails.Items(i).FindControl("ddlscore"), DropDownList).Enabled = False

                                Dim datarow As DataRow = dtAll.Rows(i)

                                '-------Added below Data to row--------------------------------
                                datarow("StudentID") = gvAssignmentDetails.Items(i)("StudentID").Text
                                datarow("FirstName") = gvAssignmentDetails.Items(i)("FirstName").Text
                                datarow("LastName") = gvAssignmentDetails.Items(i)("LastName").Text
                                datarow("Lesson") = gvAssignmentDetails.Items(i)("Lesson").Text
                                datarow("Type") = gvAssignmentDetails.Items(i)("Type").Text
                                '-----------------------------------------------------------------------
                                datarow("score") = CType(gvAssignmentDetails.Items(i).FindControl("ddlscore"), DropDownList).SelectedValue
                                CType(gvAssignmentDetails.Items(i).FindControl("ddlscore"), DropDownList).SelectedIndex = 0
                                If gvAssignment.Items.Count > 0 Then
                                    For j As Integer = 0 To gvAssignment.Items.Count - 1
                                        Dim hd As HiddenField = CType(gvAssignment.Items(j).FindControl("hdnStudent"), HiddenField)

                                        If Convert.ToString(hdnPartStatusID.Value) = Convert.ToString(hd.Value) Then
                                            lblExemptionsNotFound.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                               Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.AssignmentDetailsPage.ErrorMsg")), _
                                           Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                            checkCount = checkCount + 1
                                        End If
                                    Next
                                End If

                                If checkCount = 0 Then
                                    lblExemptionsNotFound.Text = ""
                                    dtNew.ImportRow(datarow)
                                End If

                                ViewState("dtNew") = dtNew
                            Else
                                CType(gvAssignmentDetails.Items(i).FindControl("ddlscore"), DropDownList).Enabled = True
                                lblExemptionsNotFound.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                    Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.AssignmentDetailsPage.ValidationMsg")), _
                    Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            End If
                        End If
                    Next
                    If ischeckCount = 0 Then
                        lblExemptionsNotFound.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                            Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.AssignmentDetailsPage.NoRecordErrorMsg")), _
                        Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    End If

                Else
                    For i As Integer = 0 To gvAssignmentDetails.Items.Count - 1
                        checkCount = 0
                        If CType(gvAssignmentDetails.Items(i).FindControl("chkSelect"), CheckBox).Checked = True Then
                            Dim hdnPartStatusID As HiddenField = CType(gvAssignmentDetails.Items(i).FindControl("hdnPartStatusID"), HiddenField)
                            ischeckCount = ischeckCount + 1
                            If CType(gvAssignmentDetails.Items(i).FindControl("txtScore"), TextBox).Text <> "" Then

                                CType(gvAssignmentDetails.Items(i).FindControl("chkSelect"), CheckBox).Checked = False

                                CType(gvAssignmentDetails.Items(i).FindControl("txtScore"), TextBox).ReadOnly = True
                                Dim datarow As DataRow = dtAll.Rows(i)


                                '-------Added below Data to row--------------------------------
                                datarow("StudentID") = gvAssignmentDetails.Items(i)("StudentID").Text
                                datarow("FirstName") = gvAssignmentDetails.Items(i)("FirstName").Text
                                datarow("LastName") = gvAssignmentDetails.Items(i)("LastName").Text
                                datarow("Lesson") = gvAssignmentDetails.Items(i)("Lesson").Text
                                datarow("Type") = gvAssignmentDetails.Items(i)("Type").Text
                                datarow("Type") = gvAssignmentDetails.Items(i)("Type").Text

                                datarow("score") = CType(gvAssignmentDetails.Items(i).FindControl("txtScore"), TextBox).Text
                                CType(gvAssignmentDetails.Items(i).FindControl("txtScore"), TextBox).Text = ""
                                If gvAssignment.Items.Count > 0 Then
                                    For j As Integer = 0 To gvAssignment.Items.Count - 1
                                        Dim hd As HiddenField = CType(gvAssignment.Items(j).FindControl("hdnStudent"), HiddenField)

                                        If Convert.ToString(hdnPartStatusID.Value) = Convert.ToString(hd.Value) Then

                                            lblExemptionsNotFound.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                               Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.AssignmentDetailsPage.ErrorMsg")), _
                                           Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                                            checkCount = checkCount + 1
                                        End If
                                    Next
                                End If

                                If checkCount = 0 Then
                                    lblExemptionsNotFound.Text = ""
                                    dtNew.ImportRow(datarow)
                                End If

                                ViewState("dtNew") = dtNew
                            Else
                                CType(gvAssignmentDetails.Items(i).FindControl("txtScore"), TextBox).ReadOnly = False
                                lblExemptionsNotFound.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                    Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.AssignmentDetailsPage.ValidationMsg")), _
                    Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            End If
                        End If
                    Next
                    If ischeckCount = 0 Then
                        lblExemptionsNotFound.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                            Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.AssignmentDetailsPage.NoRecordErrorMsg")), _
                        Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                    End If

                End If
                'Added uncheck SelectAll Check Box
                Dim ghi As GridHeaderItem = DirectCast(gvAssignmentDetails.MasterTableView.GetItems(GridItemType.Header)(0), GridHeaderItem)
                If DirectCast(ghi.FindControl("chkAllStudent"), CheckBox).Checked = True Then
                    DirectCast(ghi.FindControl("chkAllStudent"), CheckBox).Checked = False
                End If

                gvAssignment.DataSource = dtNew
                gvAssignment.DataBind()
                LoadGrid()
                'gvAssignmentDetails.Rebind()
                If dtNew.Rows.Count > 0 Then
                    pnlAssignmnetDetails.Visible = True
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Handles remove click
        ''' </summary>
        Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Try
                Dim btnRemove As LinkButton = CType(sender, LinkButton)
                Dim rowGridItem As GridDataItem = CType(btnRemove.NamingContainer, GridDataItem)
                Dim iIndex As Integer = rowGridItem.DataSetIndex
                dtNew = CType(ViewState("dtNew"), DataTable)
                If dtNew.Rows.Count > 0 Then
                    dtNew.Rows.RemoveAt(iIndex)
                    ViewState("dtNew") = dtNew
                    gvAssignment.DataSource = dtNew
                Else
                    Dim dtNew As New DataTable
                    dtNew.Columns.Add("StudentID")
                    gvAssignment.DataSource = dtNew
                End If
                gvAssignment.DataBind()

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Handles Submit Button click
        ''' </summary>
        Protected Sub cmdSubmit_Click(sender As Object, e As System.EventArgs) Handles cmdSubmit.Click
            Try
                dtNew = CType(ViewState("dtNew"), DataTable)
                If Not dtNew Is Nothing AndAlso dtNew.Rows.Count > 0 Then
                    lblExemptionsNotFound.Text = String.Empty
                    For Each row As DataRow In dtNew.Rows
                        Dim oClassRegistrationGE As Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase
                        oClassRegistrationGE = AptifyApplication.GetEntityObject("Class Registrations", Convert.ToInt32(row("ClassRegistrationID")))
                        With oClassRegistrationGE.SubTypes("ClassRegistrationPartStatus").Find("ID", Convert.ToInt32(row("ClassRegPartStatusID")))
                            If ddlCurriculumList.SelectedItem.Text.Contains("FAE-") Then
                                .SetValue("FAEPreliminaryScore__c", row("score"))
                            Else
                                .SetValue("PreliminaryScore__c", row("score"))
                            End If
                        End With
                        If oClassRegistrationGE.Save() Then
                            lblExemptionsNotFound.Text = String.Empty
                            ClearGrid()
                        Else
                            lblExemptionsNotFound.Text = oClassRegistrationGE.LastUserError
                        End If
                    Next
                Else
                    lblExemptionsNotFound.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal( _
                    Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.AssignmentDetailsPage.NoRecordMsg")), _
                    Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                End If
            Catch ex As Exception
                lblExemptionsNotFound.Text = ex.Message
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Handles need data source to load/reload grid 
        ''' </summary>
        Protected Sub gvAssignmentDetails_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles gvAssignmentDetails.NeedDataSource
            Try
                gvAssignmentDetails.DataSource = Nothing
                If ViewState("AssignmentDetails") IsNot Nothing Then
                    gvAssignmentDetails.DataSource = CType(ViewState("AssignmentDetails"), DataTable)
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' To Select all check box
        ''' </summary>
        Protected Sub ToggleSelectedState(ByVal sender As Object, ByVal e As EventArgs)
            Try
                Dim headerCheckBox As CheckBox = CType(sender, CheckBox)
                For Each dataItem As GridDataItem In gvAssignmentDetails.MasterTableView.Items
                    CType(dataItem.FindControl("chkSelect"), CheckBox).Checked = headerCheckBox.Checked

                    If CType(dataItem.FindControl("chkSelect"), CheckBox).Checked = True Then
                        If ddlCurriculumList.SelectedItem.Text.Contains("FAE-") Then
                            CType(dataItem.FindControl("ddlscore"), DropDownList).Enabled = True
                        Else
                            CType(dataItem.FindControl("txtScore"), TextBox).ReadOnly = False
                        End If
                    Else
                        If ddlCurriculumList.SelectedItem.Text.Contains("FAE-") Then
                            CType(dataItem.FindControl("ddlscore"), DropDownList).Enabled = False
                        Else
                            CType(dataItem.FindControl("txtScore"), TextBox).ReadOnly = True
                        End If
                    End If
                Next
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Clear/Reset Grid with Nothing
        ''' </summary>
        Private Sub ClearGrid()
            Try
                Dim dtAssignmentdetails As New DataTable
                dtAssignmentdetails.Columns.Add("StudentID")
                gvAssignment.DataSource = dtAssignmentdetails
                gvAssignment.DataBind()
                gvAssignment.Visible = True
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' ReLoad Grid with No Records Message
        ''' </summary>
        Private Sub ReLoadGrid()
            Try
                Dim dtAssignment As New DataTable
                dtAssignment.Columns.Add("StudentID")
                gvAssignmentDetails.DataSource = dtAssignment
                gvAssignmentDetails.Rebind()
                gvAssignmentDetails.Visible = True
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub gvAssignmentDetails_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvAssignmentDetails.DataBound

            For Each dataItem As GridDataItem In gvAssignmentDetails.MasterTableView.Items
                If ddlCurriculumList.SelectedItem.Text.Contains("FAE-") Then
                    CType(dataItem.FindControl("ddlscore"), DropDownList).Visible = True
                    CType(dataItem.FindControl("txtScore"), TextBox).Visible = False
                Else
                    CType(dataItem.FindControl("ddlscore"), DropDownList).Visible = False
                    CType(dataItem.FindControl("txtScore"), TextBox).Visible = True
                End If
            Next
            'ViewState("AssignmentDetails") = gvAssignmentDetails.DataSource

        End Sub
    End Class
End Namespace
