'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Govind Mande                 13/10/2015                         create Expression of Interest Page and contact log record
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Explicit On
Imports System.Data
Imports Aptify.Framework.Application
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness

Namespace Aptify.Framework.Web.eBusiness
    Partial Class LLLExpressInterest__c
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "LLLExpressInterest__c"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_NAME As String = "RedirectURL"
#Region "Page Load"
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
            'Code added by Govind Mande on 12 May 2016
            regexEmailValid.ValidationExpression = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.EmailRegularExpressionValidator__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            ' End Code
            If Not IsPostBack Then
                If User1.PersonID > 0 Then
                    LoadRecord()
                End If

                'Call function for bind LLL Categories Courses
                BindLLLCourseCategory()
                CheckPersonNotLoggedIN()
            End If
        End Sub
        Protected Overrides Sub SetProperties()
            Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            MyBase.SetProperties()
            If String.IsNullOrEmpty(RedirectURL) Then
                Me.RedirectURL = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_NAME)
            End If
        End Sub
        Private Sub CheckPersonNotLoggedIN()
            Try
                If User1.PersonID <= 0 Then
                    txtFirstName.Enabled = True
                    txtLastName.Enabled = True
                    txtEmail.Enabled = True
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Overridable Sub LoadRecord()
            Try
                txtFirstName.Text = User1.FirstName
                txtLastName.Text = User1.LastName
                txtEmail.Text = User1.Email
               
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

#End Region

#Region "Load Data"
        ''' <summary>
        ''' LLL Category Course
        ''' </summary>
        ''' <remarks></remarks>
        Protected Sub BindLLLCourseCategory()
            Try
                ' Get All Case Categories
                Dim sSQL As String = Database & "..spGetLLLCourseCategory__c"
                Dim dtLLLCategories As DataTable = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dtLLLCategories Is Nothing AndAlso dtLLLCategories.Rows.Count > 0 Then
                    cmbLLLCourseCategory.DataSource = dtLLLCategories
                    cmbLLLCourseCategory.DataTextField = "Name"
                    cmbLLLCourseCategory.DataValueField = "ID"
                    cmbLLLCourseCategory.DataBind()
                    cmbLLLCourseCategory.Items.Insert(0, "Select qualification")
                Else
                    cmbLLLCourseCategory.ClearSelection()
                    cmbLLLCourseCategory.Items.Clear()
                    cmbLLLCourseCategory.Items.Insert(0, "Select qualification")
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub


#End Region


#Region "Button Click"
        ''' <summary>
        ''' Create Expression Of Interest record
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
            Try
                ' On submission an Expression of Interest record will capture this information
                Dim oGE As AptifyGenericEntityBase = AptifyApplication.GetEntityObject("ExpressionOfInterest__c", -1)
                With oGE
                    .SetValue("PersonID", User1.PersonID)
                    .SetValue("FirstName", txtFirstName.Text.Trim)
                    .SetValue("LastName", txtLastName.Text.Trim)
                    .SetValue("Email", txtEmail.Text.Trim)
                    .SetValue("QualificationID", cmbLLLCourseCategory.SelectedValue)

                    .SetValue("Comments", txtComments.Text.Trim)
                    Dim sError As String = String.Empty
                    If .Save(False, sError) Then
                        ' Create a contact log record
                        If CreateContactLog(.RecordID) Then
                            lblMsg.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "Aptify.Ebusiness.Expression of Interest Page.SuccessMsg__c")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                            radWindow.VisibleOnPageLoad = True
                        End If
                    End If
                End With
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Create Contact log record
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function CreateContactLog(ByVal ExpressionRecordID As Integer) As Boolean
            Try
                Dim sError As String = String.Empty
                Dim bIsSuccess As Boolean = False
                ' Get Course Details
                Dim CourseName As String = String.Empty
                Dim CourseCat As String = String.Empty
                Dim Description As String
                'Dim sSql As String = Database & "..spGetCourseDetails__c @CourseID=" & cmbLLLCourseCategory.SelectedValue
                'Dim dt As DataTable = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
                'If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                '    CourseName = Convert.ToString(dt.Rows(0)("CourseName"))
                '    CourseCat = Convert.ToString(dt.Rows(0)("Category"))
                '    Description = CourseCat & "," & CourseName
                'End If
                Description = cmbLLLCourseCategory.SelectedItem.Text
                If txtComments.Text.Trim <> "" Then
                    Description = Description & "," & txtComments.Text.Trim
                End If
                Dim oGE As AptifyGenericEntityBase = AptifyApplication.GetEntityObject("Contact Log", -1)
                oGE.SetValue("Description", Description)
                oGE.SetValue("TypeID", AptifyApplication.GetEntityRecordIDFromRecordName("Contact Log Types", "Planned Contact"))
                oGE.SetValue("CategoryID", AptifyApplication.GetEntityRecordIDFromRecordName("Contact Log Categories", "Expression of Interest"))
                oGE.SetValue("Direction", "Inbound")
                oGE.SetValue("NextContactDate", New Date(Today.Year, Today.Month, Today.Day))
                If User1.PersonID > 0 Then
                    With oGE.SubTypes("ContactLogLinks").Add()
                        .SetValue("EntityID", AptifyApplication.GetEntityRecordIDFromRecordName("Entities", "Persons"))
                        .SetValue("AltID", User1.PersonID)
                    End With
                End If
                With oGE.SubTypes("ContactLogLinks").Add()
                    .SetValue("EntityID", AptifyApplication.GetEntityRecordIDFromRecordName("Entities", "ExpressionOfInterest__c"))
                    .SetValue("AltID", ExpressionRecordID)
                End With
                If oGE.Save(False, sError) Then
                    bIsSuccess = True
                End If
                Return bIsSuccess
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return False
            End Try
        End Function
        ''' <summary>
        ''' popup ok button close popup and redirect home page
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub btnOK_Click(sender As Object, e As System.EventArgs) Handles btnOK.Click
            Try
                radWindow.VisibleOnPageLoad = False
                lblMsg.Text = ""
                Response.Redirect(RedirectURL, False)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

    End Class
End Namespace
