'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Siddharth Kavitake         17-Mar-2015                          Added link for Student Enrollment and code to decide visibility of link
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Explicit On
Option Strict On


Namespace Aptify.Framework.Web.eBusiness.Education
    Partial Class EducationCenter__c
        Inherits eBusiness.BaseUserControlAdvanced

        Protected Const ATTRIBUTE_REGISTERED_COURSES_IMAGE_URL As String = "RegisteredCoursesImage"
        Protected Const ATTRIBUTE_REGISTERED_COURSES_PAGE As String = "RegisteredCoursesPage"
        Protected Const ATTRIBUTE_REGISTERED_CERTIFICATIONS_IMAGE_URL As String = "RegisteredCertificationsImage"
        Protected Const ATTRIBUTE_REGISTERED_CERTIFICATIONS_PAGE As String = "RegisteredCertificationsPage"
        Protected Const ATTRIBUTE_COURSE_CATALOG_IMAGE_URL As String = "CourseCatalogImage"
        Protected Const ATTRIBUTE_COURSE_CATALOG_PAGE As String = "CourseCatalogPage"
        Protected Const ATTRIBUTE_CLASS_SCHEDULE_IMAGE_URL As String = "ClassScheduleImage"
        Protected Const ATTRIBUTE_CLASS_SCHEDULE_PAGE As String = "ClassSchedulePage"
        Protected Const ATTRIBUTE_CURRICULA_IMAGE_URL As String = "CurriculaImage"
        Protected Const ATTRIBUTE_CURRICULA_PAGE As String = "CurriculaPage"
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "EducationCenter"
        Protected Const ATTRIBUTE_STUDENTENROLLMENT_URL As String = "StudentEnrollmentPage"
        Protected Const ATTRIBUTE_CONTORL_EducationResult_URL As String = "EducationResultPage"
        Protected Const ATTRIBUTE_CONTORL_EducationResult_Image As String = "EducationResultImage"
        Protected Const ATTRIBUTE_COURSEMATERIAL_URL As String = "CourseMaterialPage"
        Protected Const ATTRIBUTE_COURSEMATERIAL_IMAGE As String = "CourseMaterialImage"
        Protected Const ATTRIBUTE_FORMALREGISTRATION_URL As String = "FormalRegistrationPage"
        Protected Const ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage As String = "LoginPage"

        Protected Const ATTRIBUTE_FIRMCONTRACTREGISTRATION_URL As String = "FirmContractRegistrationPage"
        Protected Const ATTRIBUTE_FORMALREGISTRATION_IMAGE_URL As String = "FormalRegistrationImage"
        ''Added By Pradip
        Protected Const ATTRIBUTE_STUDENTDASHBOARD_URL As String = "StudentDashboardPage"
        Protected Const ATTRIBUTE_STUDENTDASHBOARD_IMAGE_URL As String = "StudentDashboardImage"

        Protected Const ATTRIBUTE_ASSIGNMENTDETAILS_PAGE As String = "AssignmentDetailsPage" 'Added By Kavita
        Protected Const ATTRIBUTE_ASSIGNMENTDETAILS_IMAGE_URL As String = "AssignmentDetailsImage" 'Added By Kavita

        'Added By Kavita For Mentor Dashboard
        Protected Const ATTRIBUTE_MENTORDASHBOARD_URL As String = "MentorDashboardPage"
        Protected Const ATTRIBUTE_MENTORDASHBOARD_IMAGE_URL As String = "MentorDashboardImage"

        'Added BY Pradip 2015-09-18 For Mentor Application Form
        Protected Const ATTRIBUTE_MentorApplication_IMAGE_URL As String = "MentorApplicationImage"
        Protected Const ATTRIBUTE_MENTORAPPLICATION_URL As String = "MentorApplicationPage"
        'Added BY Pradip 2015-10-29 For LLL Qualification Status Page
        Protected Const ATTRIBUTE_QualificationStatus_IMAGE_URL As String = "QualificationStatusImage"
        Protected Const ATTRIBUTE_QualificationStatus_URL As String = "QualificationStatusPage"

        'Added By Kavita For Class Attendance #17454
        Protected Const ATTRIBUTE_ClassAttendance_IMAGE_URL As String = "ClassAttendanceImage"
        Protected Const ATTRIBUTE_ClassAttendancePage_URL As String = "ClassAttendancePage"

        'Added by LH 18-01-18
        Protected Const ATTRIBUTE_RecordingsPage_URL As String = "RecordingsPage"

        'Added by LH 04-04-19 for ticket #20473
        Protected Const ATTRIBUTE_BooksPage_URL As String = "BooksPage"
        Protected Const ATTRIBUTE_CharteredBooksPage_URL As String = "CharteredBooksPage"
        'End Here

        'Added by EM 17-04-20 for ticket #21219
        Protected Const ATTRIBUTE_EBookProQuest_URL As String = "EBookProQuestPage"
        Private lpage As String = "/Login.aspx"

        Private ReadOnly mclient As SoftwareDesign.WEBAPI_PLUGINS.MoodlePlugin = New SoftwareDesign.WEBAPI_PLUGINS.MoodlePlugin()

#Region "EducationCenter Specific Properties"
        ''' <summary>
        ''' Redirect login page 
        ''' </summary>
        ''' <returns></returns>
        Public Overridable Property LoginPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                'ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage) = Me.FixLinkForVirtualPath(value)
                ViewState(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage) = lpage

            End Set
        End Property
        ''' <summary>
        ''' RegisteredCoursesImage url
        ''' </summary>
        Public Overridable Property RegisteredCoursesImage() As String
            Get
                If Not ViewState(ATTRIBUTE_REGISTERED_COURSES_IMAGE_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_REGISTERED_COURSES_IMAGE_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_REGISTERED_COURSES_IMAGE_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' RegisteredCourses page url
        ''' </summary>
        Public Overridable Property RegisteredCoursesPage() As String
            Get
                If Not ViewState(ATTRIBUTE_REGISTERED_COURSES_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_REGISTERED_COURSES_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_REGISTERED_COURSES_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' RegisteredCertificationsImage url
        ''' </summary>
        Public Overridable Property RegisteredCertificationsImage() As String
            Get
                If Not ViewState(ATTRIBUTE_REGISTERED_CERTIFICATIONS_IMAGE_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_REGISTERED_CERTIFICATIONS_IMAGE_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_REGISTERED_CERTIFICATIONS_IMAGE_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' RegisteredCertifications page url
        ''' </summary>
        Public Overridable Property RegisteredCertificationsPage() As String
            Get
                If Not ViewState(ATTRIBUTE_REGISTERED_CERTIFICATIONS_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_REGISTERED_CERTIFICATIONS_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_REGISTERED_CERTIFICATIONS_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' CourseCatalogImage url
        ''' </summary>
        Public Overridable Property CourseCatalogImage() As String
            Get
                If Not ViewState(ATTRIBUTE_COURSE_CATALOG_IMAGE_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_COURSE_CATALOG_IMAGE_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_COURSE_CATALOG_IMAGE_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property


        ''' <summary> Added BY Pradip 2015-07-20
        ''' FormalRegistrationImage url
        ''' </summary>
        Public Overridable Property FormalRegistrationImage() As String
            Get
                If Not ViewState(ATTRIBUTE_FORMALREGISTRATION_IMAGE_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_FORMALREGISTRATION_IMAGE_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_FORMALREGISTRATION_IMAGE_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        ''' <summary>
        ''' CourseCatalog page url
        ''' </summary>
        Public Overridable Property CourseCatalogPage() As String
            Get
                If Not ViewState(ATTRIBUTE_COURSE_CATALOG_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_COURSE_CATALOG_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_COURSE_CATALOG_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' ClassScheduleImage url
        ''' </summary>
        Public Overridable Property ClassScheduleImage() As String
            Get
                If Not ViewState(ATTRIBUTE_CLASS_SCHEDULE_IMAGE_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CLASS_SCHEDULE_IMAGE_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CLASS_SCHEDULE_IMAGE_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' ClassSchedule page url
        ''' </summary>
        Public Overridable Property ClassSchedulePage() As String
            Get
                If Not ViewState(ATTRIBUTE_CLASS_SCHEDULE_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CLASS_SCHEDULE_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CLASS_SCHEDULE_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' CurriculaImage url
        ''' </summary>
        Public Overridable Property CurriculaImage() As String
            Get
                If Not ViewState(ATTRIBUTE_CURRICULA_IMAGE_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CURRICULA_IMAGE_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CURRICULA_IMAGE_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' Curricula page url
        ''' </summary>
        Public Overridable Property CurriculaPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CURRICULA_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CURRICULA_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CURRICULA_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        ''' <summary>
        ''' Curricula page url
        ''' </summary>
        Public Overridable Property EducationResultPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTORL_EducationResult_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTORL_EducationResult_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTORL_EducationResult_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property


        ''' <summary>
        ''' Education result page image
        ''' </summary>
        Public Overridable Property EducationResultImage() As String
            Get
                If Not ViewState(ATTRIBUTE_CONTORL_EducationResult_Image) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CONTORL_EducationResult_Image))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CONTORL_EducationResult_Image) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        ''' <summary>
        ''' Student enrollment page url
        ''' </summary>
        Public Overridable Property StudentEnrollmentPage() As String
            Get
                If Not ViewState(ATTRIBUTE_STUDENTENROLLMENT_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_STUDENTENROLLMENT_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_STUDENTENROLLMENT_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        ''' <summary>
        ''' Course material page url
        ''' </summary>
        Public Overridable Property CourseMaterialPage() As String
            Get
                If Not ViewState(ATTRIBUTE_COURSEMATERIAL_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_COURSEMATERIAL_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_COURSEMATERIAL_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        ''' <summary>
        ''' Course material Image
        ''' </summary>
        Public Overridable Property CourseMaterialImage() As String
            Get
                If Not ViewState(ATTRIBUTE_COURSEMATERIAL_IMAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_COURSEMATERIAL_IMAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_COURSEMATERIAL_IMAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        Public Overridable Property FormalRegistrationPage() As String
            Get
                If Not ViewState(ATTRIBUTE_FORMALREGISTRATION_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_FORMALREGISTRATION_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_FORMALREGISTRATION_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''Added By Pradip For FirmContract Registration Page.
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
        ''Added By Pradip For Assignment Details Page for Corrector.
        ''' <summary>
        ''' Assignment Details page url
        ''' </summary>
        Public Overridable Property AssignmentDetailsPage() As String
            Get
                If Not ViewState(ATTRIBUTE_ASSIGNMENTDETAILS_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_ASSIGNMENTDETAILS_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_ASSIGNMENTDETAILS_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        ''' <summary> Added BY Kavita 2015-07-23
        ''' Assignment Details Image url
        ''' </summary>
        Public Overridable Property AssignmentDetailsImage() As String
            Get
                If Not ViewState(ATTRIBUTE_ASSIGNMENTDETAILS_IMAGE_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_ASSIGNMENTDETAILS_IMAGE_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_ASSIGNMENTDETAILS_IMAGE_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property


        ''Added BY Pradip For Student Dashboard Page
        Public Overridable Property StudentDashboardImage() As String
            Get
                If Not ViewState(ATTRIBUTE_STUDENTDASHBOARD_IMAGE_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_STUDENTDASHBOARD_IMAGE_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_STUDENTDASHBOARD_IMAGE_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property


        Public Overridable Property StudentDashboardPage() As String
            Get
                If Not ViewState(ATTRIBUTE_STUDENTDASHBOARD_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_STUDENTDASHBOARD_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_STUDENTDASHBOARD_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        ''Added BY Kavita For Mentor Dashboard Page
        Public Overridable Property MentorDashboardImage() As String
            Get
                If Not ViewState(ATTRIBUTE_MENTORDASHBOARD_IMAGE_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_MENTORDASHBOARD_IMAGE_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_MENTORDASHBOARD_IMAGE_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Public Overridable Property MentorDashboardPage() As String
            Get
                If Not ViewState(ATTRIBUTE_MENTORDASHBOARD_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_MENTORDASHBOARD_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_MENTORDASHBOARD_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        ''Added BY Pradip For Mentor Application Page

        Public Overridable Property MentorApplicationImage() As String
            Get
                If Not ViewState(ATTRIBUTE_MentorApplication_IMAGE_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_MentorApplication_IMAGE_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_MentorApplication_IMAGE_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Public Overridable Property MentorApplicationPage() As String
            Get
                If Not ViewState(ATTRIBUTE_MENTORAPPLICATION_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_MENTORAPPLICATION_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_MENTORAPPLICATION_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property


        ''Added BY Pradip For LLL Qualification Status Page

        Public Overridable Property QualificationStatusImage() As String
            Get
                If Not ViewState(ATTRIBUTE_QualificationStatus_IMAGE_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_QualificationStatus_IMAGE_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_QualificationStatus_IMAGE_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Public Overridable Property QualificationStatusPage() As String
            Get
                If Not ViewState(ATTRIBUTE_QualificationStatus_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_QualificationStatus_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_QualificationStatus_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        ''Added BY Kavita Zinage #17454 For Class Attendance Page

        Public Overridable Property ClassAttendanceImage() As String
            Get
                If Not ViewState(ATTRIBUTE_ClassAttendance_IMAGE_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_ClassAttendance_IMAGE_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_ClassAttendance_IMAGE_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Public Overridable Property ClassAttendancePage() As String
            Get
                If Not ViewState(ATTRIBUTE_ClassAttendancePage_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_ClassAttendancePage_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_ClassAttendancePage_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Public Overridable Property RecordingsPage() As String
            Get
                If Not ViewState(ATTRIBUTE_RecordingsPage_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_RecordingsPage_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_RecordingsPage_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Public Overridable Property BooksPage() As String
            Get
                If Not ViewState(ATTRIBUTE_BooksPage_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_BooksPage_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_BooksPage_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Public Overridable Property CharteredBooksPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CharteredBooksPage_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CharteredBooksPage_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CharteredBooksPage_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Public Overridable Property EBookProQuestPage() As String
            Get
                If Not ViewState(ATTRIBUTE_EBookProQuest_URL) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_EBookProQuest_URL))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_EBookProQuest_URL) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property


        'End Here

#End Region

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'set control properties from XML file if needed

            SetProperties()
            If User1.PersonID <= 0 Then
                Session("ReturnToPage") = Request.RawUrl
                Response.Redirect(LoginPage)

            End If

            If Not IsPostBack Then
                SetupPage()

                Dim jtwCheck = mclient.ConsumeJWTTokenEducation(User1.WebUserStringID)

                If Not String.IsNullOrEmpty(jtwCheck) Then
                    MOODLELinkLi.Visible = True
                    MOODLELinkA.NavigateUrl = jtwCheck
                    MOODLELinkA.Target = "_blank"
                Else
                    MOODLELinkLi.Visible = False
                End If

                CheckDisplayEducationResultPage()
                CheckDisplayCourseEnrollLink()
                CheckDisplayCourseMaterialPage()

                CheckDisplayAssignmentDetailsLink()
                ''Added BY Pradip 2015-04-09 For CA Diary/ Student Dashboard link access
                CheckDisplayCADiaryLink()

                ''Added by Kavita for Mentor Dashboard (CA Diary) Link
                CheckDisplayMentorDashboardLink()
                ''Added BY PRadip for Mentor Request Application Link Access
                CheckDisplayMentorReqAppLink()
                ''Added BY PRadip for Qualification Status Link Access
                CheckDisplayQualStatLink()
                ''Added by LH for Chartered Books Link #20473
                CheckDisplayBooksLink()
                ''NO PREVIOUS COMMENT
                CheckDisplayCharteredBooksLink()
                ''Added by Kavita for Class Attendance Link #17454
                CheckDisplayClassAttendanceLink()

            End If
        End Sub

        Protected Overrides Sub SetProperties()

            If String.IsNullOrEmpty(LoginPage) Then
                'LoginPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTROL_REDIRECTURL_LoginPage)
                LoginPage = lpage
            End If


            'Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            'call base method to set parent properties
            MyBase.SetProperties()

            If String.IsNullOrEmpty(RegisteredCoursesImage) Then
                'since value is the 'default' check the XML file for possible custom setting
                RegisteredCoursesImage = Me.GetLinkValueFromXML(ATTRIBUTE_REGISTERED_COURSES_IMAGE_URL)
                Me.imgMyCouses.Src = RegisteredCoursesImage
            End If
            If String.IsNullOrEmpty(RegisteredCoursesPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                RegisteredCoursesPage = Me.GetLinkValueFromXML(ATTRIBUTE_REGISTERED_COURSES_PAGE)
                If String.IsNullOrEmpty(RegisteredCoursesPage) Then
                    Me.lnkMyCourses.Enabled = False
                    Me.lnkMyCourses.ToolTip = "RegisteredCoursesPage property has not been set."
                Else
                    Me.lnkMyCourses.NavigateUrl = RegisteredCoursesPage
                End If
            Else
                Me.lnkMyCourses.NavigateUrl = RegisteredCoursesPage
            End If

            If String.IsNullOrEmpty(RegisteredCertificationsImage) Then
                'since value is the 'default' check the XML file for possible custom setting
                RegisteredCertificationsImage = Me.GetLinkValueFromXML(ATTRIBUTE_REGISTERED_CERTIFICATIONS_IMAGE_URL)
                Me.imgMyCerts.Src = RegisteredCertificationsImage
            End If
            If String.IsNullOrEmpty(RegisteredCertificationsPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                RegisteredCertificationsPage = Me.GetLinkValueFromXML(ATTRIBUTE_REGISTERED_CERTIFICATIONS_PAGE)
                If String.IsNullOrEmpty(RegisteredCertificationsPage) Then
                    Me.lnkMyCerts.Enabled = False
                    Me.lnkMyCerts.ToolTip = "RegisteredCertificationsPage property has not been set."
                Else
                    Me.lnkMyCerts.NavigateUrl = RegisteredCertificationsPage
                End If
            Else
                Me.lnkMyCerts.NavigateUrl = RegisteredCertificationsPage
            End If

            If String.IsNullOrEmpty(CourseCatalogImage) Then
                'since value is the 'default' check the XML file for possible custom setting
                CourseCatalogImage = Me.GetLinkValueFromXML(ATTRIBUTE_COURSE_CATALOG_IMAGE_URL)
                Me.imgCatalog.Src = CourseCatalogImage
            End If
            If String.IsNullOrEmpty(CourseCatalogPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                CourseCatalogPage = Me.GetLinkValueFromXML(ATTRIBUTE_COURSE_CATALOG_PAGE)
                If String.IsNullOrEmpty(CourseCatalogPage) Then
                    Me.lnkCatalog.Enabled = False
                    Me.lnkCatalog.ToolTip = "CourseCatalogPage property has not been set."
                Else
                    Me.lnkCatalog.NavigateUrl = CourseCatalogPage
                End If
            Else
                Me.lnkCatalog.NavigateUrl = CourseCatalogPage
            End If

            If String.IsNullOrEmpty(ClassScheduleImage) Then
                'since value is the 'default' check the XML file for possible custom setting
                ClassScheduleImage = Me.GetLinkValueFromXML(ATTRIBUTE_CLASS_SCHEDULE_IMAGE_URL)
                Me.imgClassSchedule.Src = ClassScheduleImage
            End If
            If String.IsNullOrEmpty(ClassSchedulePage) Then
                'since value is the 'default' check the XML file for possible custom setting
                ClassSchedulePage = Me.GetLinkValueFromXML(ATTRIBUTE_CLASS_SCHEDULE_PAGE)
                If String.IsNullOrEmpty(ClassSchedulePage) Then
                    Me.lnkClassSchedule.Enabled = False
                    Me.lnkClassSchedule.ToolTip = "ClassSchedulePage property has not been set."
                Else
                    Me.lnkClassSchedule.NavigateUrl = ClassSchedulePage
                End If
            Else
                Me.lnkClassSchedule.NavigateUrl = ClassSchedulePage
            End If

            If String.IsNullOrEmpty(CurriculaImage) Then
                'since value is the 'default' check the XML file for possible custom setting
                CurriculaImage = Me.GetLinkValueFromXML(ATTRIBUTE_CURRICULA_IMAGE_URL)
                Me.imgCurricula.Src = CurriculaImage
            End If
            If String.IsNullOrEmpty(CurriculaPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                CurriculaPage = Me.GetLinkValueFromXML(ATTRIBUTE_CURRICULA_PAGE)
                If String.IsNullOrEmpty(CurriculaPage) Then
                    Me.lnkCurricula.Enabled = False
                    Me.lnkCurricula.ToolTip = "CurriculaPage property has not been set."
                Else
                    Me.lnkCurricula.NavigateUrl = CurriculaPage
                End If
            Else
                Me.lnkCurricula.NavigateUrl = CurriculaPage
            End If

            If String.IsNullOrEmpty(EducationResultImage) Then
                EducationResultImage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTORL_EducationResult_Image)
                Me.imgEducationResult.Src = EducationResultImage
            Else
                Me.imgEducationResult.Src = EducationResultImage
            End If

            If String.IsNullOrEmpty(EducationResultPage) Then
                EducationResultPage = Me.GetLinkValueFromXML(ATTRIBUTE_CONTORL_EducationResult_URL)
                Me.lnkEducationResult.NavigateUrl = EducationResultPage
            Else
                Me.lnkEducationResult.NavigateUrl = EducationResultPage
            End If

            If String.IsNullOrEmpty(StudentEnrollmentPage) Then
                StudentEnrollmentPage = Me.GetLinkValueFromXML(ATTRIBUTE_STUDENTENROLLMENT_URL)
                Me.lnkCourseEnrollment.NavigateUrl = StudentEnrollmentPage
            Else
                Me.lnkCourseEnrollment.NavigateUrl = StudentEnrollmentPage
            End If

            If String.IsNullOrEmpty(CourseMaterialImage) Then
                CourseMaterialImage = Me.GetLinkValueFromXML(ATTRIBUTE_COURSEMATERIAL_IMAGE)
                Me.imgCourseMaterial.Src = CourseMaterialImage
            Else
                Me.imgCourseMaterial.Src = CourseMaterialImage
            End If

            If String.IsNullOrEmpty(CourseMaterialPage) Then
                CourseMaterialPage = Me.GetLinkValueFromXML(ATTRIBUTE_COURSEMATERIAL_URL)
                Me.lnkCourseMaterial.NavigateUrl = CourseMaterialPage
            Else
                Me.lnkCourseMaterial.NavigateUrl = CourseMaterialPage
            End If

            ''Added BY Pradip To Navigate To Formal Registration Page.
            If String.IsNullOrEmpty(FormalRegistrationPage) Then
                FormalRegistrationPage = Me.GetLinkValueFromXML(ATTRIBUTE_FORMALREGISTRATION_URL)
                Me.lnkFormalRegistration.NavigateUrl = FormalRegistrationPage
            Else
                Me.lnkFormalRegistration.NavigateUrl = FormalRegistrationPage
            End If

            If String.IsNullOrEmpty(FormalRegistrationImage) Then
                'since value is the 'default' check the XML file for possible custom setting
                FormalRegistrationImage = Me.GetLinkValueFromXML(ATTRIBUTE_FORMALREGISTRATION_IMAGE_URL)
                Me.imgFormalRegistration.Src = FormalRegistrationImage
            End If



            ''Added BY Pradip To Navigate To Firm Contract Registration Page.
            If String.IsNullOrEmpty(FirmContractRegistrationPage) Then
                FirmContractRegistrationPage = Me.GetLinkValueFromXML(ATTRIBUTE_FIRMCONTRACTREGISTRATION_URL)
                Me.lnkFirmContractRegistration.NavigateUrl = FirmContractRegistrationPage
            Else
                Me.lnkFirmContractRegistration.NavigateUrl = FirmContractRegistrationPage
            End If

            'Added by Kavita 18/06/2015
            If String.IsNullOrEmpty(AssignmentDetailsImage) Then
                'since value is the 'default' check the XML file for possible custom setting
                AssignmentDetailsImage = Me.GetLinkValueFromXML(ATTRIBUTE_ASSIGNMENTDETAILS_IMAGE_URL)
                Me.imgAssignment.Src = AssignmentDetailsImage
            End If
            If String.IsNullOrEmpty(AssignmentDetailsPage) Then
                AssignmentDetailsPage = Me.GetLinkValueFromXML(ATTRIBUTE_ASSIGNMENTDETAILS_PAGE)
                Me.lnkAssignmentDetails.NavigateUrl = AssignmentDetailsPage
            Else
                Me.lnkAssignmentDetails.NavigateUrl = AssignmentDetailsPage
            End If

            ''Added BY Pradip To Navigate To Student Dashboard Page.
            If String.IsNullOrEmpty(StudentDashboardImage) Then
                StudentDashboardImage = Me.GetLinkValueFromXML(ATTRIBUTE_STUDENTDASHBOARD_IMAGE_URL)
                Me.imgStudentDashboard.Src = StudentDashboardImage
            End If

            If String.IsNullOrEmpty(StudentDashboardPage) Then
                StudentDashboardPage = Me.GetLinkValueFromXML(ATTRIBUTE_STUDENTDASHBOARD_URL)
                ' Me.lnkStudentDashboard.NavigateUrl = StudentDashboardPage
            Else
                'Me.lnkStudentDashboard.NavigateUrl = StudentDashboardPage
            End If

            ''Added BY Kavita To Navigate To Mentor Dashboard Page.
            If String.IsNullOrEmpty(MentorDashboardImage) Then
                MentorDashboardImage = Me.GetLinkValueFromXML(ATTRIBUTE_MENTORDASHBOARD_IMAGE_URL)
                Me.imgMentorDashboard.Src = MentorDashboardImage
            End If

            If String.IsNullOrEmpty(MentorDashboardPage) Then
                MentorDashboardPage = Me.GetLinkValueFromXML(ATTRIBUTE_MENTORDASHBOARD_URL)
                Me.lnkMentorDashboard.NavigateUrl = MentorDashboardPage
            Else
                Me.lnkMentorDashboard.NavigateUrl = MentorDashboardPage
            End If

            ''Added BY Pradip To Navigate To Mentor Application Page
            If String.IsNullOrEmpty(MentorApplicationImage) Then
                MentorApplicationImage = Me.GetLinkValueFromXML(ATTRIBUTE_MentorApplication_IMAGE_URL)
                Me.imgMentorApplication.Src = MentorApplicationImage
            End If
            If String.IsNullOrEmpty(MentorApplicationPage) Then
                MentorApplicationPage = Me.GetLinkValueFromXML(ATTRIBUTE_MENTORAPPLICATION_URL)
                Me.lnkMentorApplication.NavigateUrl = MentorApplicationPage
            Else
                Me.lnkMentorApplication.NavigateUrl = MentorApplicationPage
            End If



            ''Added BY Pradip To Navigate To LLL Qualification Status Page
            If String.IsNullOrEmpty(QualificationStatusImage) Then
                QualificationStatusImage = Me.GetLinkValueFromXML(ATTRIBUTE_QualificationStatus_IMAGE_URL)
                Me.imgQualificationStatus.Src = QualificationStatusImage
            End If
            If String.IsNullOrEmpty(QualificationStatusPage) Then
                QualificationStatusPage = Me.GetLinkValueFromXML(ATTRIBUTE_QualificationStatus_URL)
                Me.lnkQualificationStatus.NavigateUrl = QualificationStatusPage
            Else
                Me.lnkQualificationStatus.NavigateUrl = QualificationStatusPage
            End If

            ''Added BY Kavita Zinage #17454 To Navigate To Class Attendance Page
            If String.IsNullOrEmpty(ClassAttendanceImage) Then
                ClassAttendanceImage = Me.GetLinkValueFromXML(ATTRIBUTE_ClassAttendance_IMAGE_URL)
                Me.imgClassAttd.Src = ClassAttendanceImage
            End If
            If String.IsNullOrEmpty(ClassAttendancePage) Then
                ClassAttendancePage = Me.GetLinkValueFromXML(ATTRIBUTE_ClassAttendancePage_URL)
            End If


            If String.IsNullOrEmpty(BooksPage) Then
                BooksPage = Me.GetLinkValueFromXML(ATTRIBUTE_BooksPage_URL)
                Me.lnkBooksPage.NavigateUrl = BooksPage
            Else
                Me.lnkBooksPage.NavigateUrl = BooksPage
            End If
            If Not String.IsNullOrEmpty(Me.lnkBooksPage.NavigateUrl) Then
                Me.lnkBooksPage.NavigateUrl = Me.lnkBooksPage.NavigateUrl.Replace("/http", "http")
            End If

            If String.IsNullOrEmpty(CharteredBooksPage) Then
                CharteredBooksPage = Me.GetLinkValueFromXML(ATTRIBUTE_CharteredBooksPage_URL)
                Me.lnkCharteredBooks.NavigateUrl = CharteredBooksPage
            Else
                Me.lnkCharteredBooks.NavigateUrl = CharteredBooksPage
            End If
            If Not String.IsNullOrEmpty(Me.lnkCharteredBooks.NavigateUrl) Then
                Me.lnkCharteredBooks.NavigateUrl = Me.lnkCharteredBooks.NavigateUrl.Replace("/http", "http")
            End If

            '###EM 17-04-20
            If String.IsNullOrEmpty(EBookProQuestPage) Then
                EBookProQuestPage = Me.GetLinkValueFromXML(ATTRIBUTE_EBookProQuest_URL)
                Me.lnkProQuestEBook.NavigateUrl = EBookProQuestPage
            Else
                Me.lnkProQuestEBook.NavigateUrl = EBookProQuestPage
            End If
            If Not String.IsNullOrEmpty(Me.lnkProQuestEBook.NavigateUrl) Then
                Me.lnkProQuestEBook.NavigateUrl = Me.lnkProQuestEBook.NavigateUrl.Replace("/http", "http")
            End If

        End Sub

        Private Sub SetupPage()
            ' determine if the individual logged in is an active instructor
            ' on any course. If so, show the InstructorCenter link.
            If InstructorValidator1.IsCurrentUserInstructor() Then
                trInstructorCenter.Visible = True
            Else
                trInstructorCenter.Visible = False
            End If
        End Sub

#Region "Consulting Code"

        Private Sub CheckDisplayEducationResultPage()
            Try
                Dim sSQLPersonEligible As String = "..spGetAccessResultPage__c @StudentID=" & User1.PersonID
                Dim lStudentID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSQLPersonEligible))
                If lStudentID > 0 Then
                    trEducationResult.Visible = True
                Else
                    trEducationResult.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Private Sub CheckDisplayCourseMaterialPage()
            Try
                If HasWebMaterialAccess() Then
                    trCourseMaterial.Visible = True
                Else
                    trCourseMaterial.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Check if user has an access to web materials
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

        'Added by Siddharth - to deside visibility of student enrollment link
        Private Sub CheckDisplayCourseEnrollLink()
            Try
                Dim sSQL As String = "..spCheckAccessForStudEnrollPage__c @StudentID=" & User1.PersonID
                Dim lStudentID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSQL))
                If lStudentID > 0 Then
                    trCourseEnrollment.Visible = True
                    ''Added by Pradip - to deside visibility of Formal Registration Link
                    trFormalRegistration.Visible = True
                    'trFirmContractRegistration.Visible = True
                Else
                    trCourseEnrollment.Visible = False
                    ''Added by Pradip - to deside visibility of Formal Registration Link
                    trFormalRegistration.Visible = False
                    trFirmContractRegistration.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        'Adde by kavita - to check accessibility of Assignment Details link
        Private Sub CheckDisplayAssignmentDetailsLink()
            Try
                Dim sSQL As String = "..spCheckAccessForAssignmentDetailsPage__c @InsructorID=" & User1.PersonID
                Dim lInstructorID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSQL))
                If lInstructorID > 0 Then
                    trAssignmentDetails.Visible = True
                Else
                    trAssignmentDetails.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        'Adde by Pradip 2015-09-02 to check access for CA Diary link 
        Private Sub CheckDisplayCADiaryLink()
            Try
                Dim sSQL As String = Database & "..spCheckCADiaryLinkAccess__c @StudentId=" & User1.PersonID
                Dim lID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSQL))
                If lID > 0 Then
                    trStudentDashboard.Visible = True
                Else
                    trStudentDashboard.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        'Added by Kavita 2015-09-09 to check access for Mentor Dashboard link 
        Private Sub CheckDisplayMentorDashboardLink()
            Try
                Dim sSQL As String = Database & "..spCheckMentorDashboardLinkAccess__c @MentorID=" & Convert.ToInt32(User1.PersonID)
                Dim lID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSQL))
                If lID > 0 Then
                    trMentorDashboard.Visible = True
                Else
                    trMentorDashboard.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        'Adde by Pradip 2015-09-02 to check access for Mentor Application Request 
        Private Sub CheckDisplayMentorReqAppLink()
            Try
                Dim sSQL As String = Database & "..spCheckIsMentor__c @PersonId=" & User1.PersonID
                Dim lID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSQL))
                If lID > 0 Then
                    trMentorApplication.Visible = True
                Else
                    trMentorApplication.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        'Added by Pradip 2015-10-29 to check access for Qualification Status Link

        Private Sub CheckDisplayQualStatLink()
            Try
                Dim sSQL As String = Database & "..spCheckPersonIsRegisterForLLL__c @PersonId=" & User1.PersonID
                Dim iCheck As Integer = Convert.ToInt32(DataAction.ExecuteScalar(sSQL, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
                If iCheck > 0 Then
                    trQualificationStatus.Visible = True
                    Me.lnkQualificationStatus.NavigateUrl = QualificationStatusPage
                Else
                    trQualificationStatus.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub


        'Added by kavita - to check accessibility of Class Attendance link on 2017-09-11
        Private Sub CheckDisplayClassAttendanceLink()
            Try
                Dim sSQL As String = "..spCheckAccessForClassAttendancePage__c @PersonID=" & User1.PersonID
                Dim lIsLecturer As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSQL))
                If lIsLecturer = 1 Then
                    trClassAttendance.Visible = True
                    trProQuestEBook.Visible = True
                Else
                    trClassAttendance.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub


        'Added by LH 04-04-2019 to check access for BooksPage link for lecturer
        Private Sub CheckDisplayBooksLink()
            Try
                Dim sSQL As String = Database & "..spCheckAccessForClassAttendancePage__c @PersonID=" & User1.PersonID
                Dim lID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSQL))
                If lID > 0 Then
                    trBooksLink.Visible = True
                Else
                    trBooksLink.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Sub

        'Added by LH 04-04-2019 to check access for Chartered Books link for student
        Private Sub CheckDisplayCharteredBooksLink()
            Try
                Dim sSQL As String = Database & "..spCheckCADiaryLinkAccess__c @StudentID=" & User1.PersonID
                Dim lID As Long = Convert.ToInt32(DataAction.ExecuteScalar(sSQL))
                If lID > 0 Then
                    trCharteredBooks.Visible = True
                    trProQuestEBook.Visible = True
                Else
                    trProQuestEBook.Visible = False
                    trCharteredBooks.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        'Ends Here

        '<Begin: added by Govind on 2016-05-11>
        ''' <summary>
        ''' Added by Govind M to set cookie 
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub lnkCADairy_Click(sender As Object, e As System.EventArgs) Handles lnkCADairy.Click
            Try
                Dim aCookie As New HttpCookie("CADairyOnCenter")
                aCookie.Value = "Yes"
                Response.Cookies.Add(aCookie)
                Response.Redirect(StudentDashboardPage, False)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        ' <End :added by Govind on 2016-05-11>


        '<Begin: added by Kavita #17454 on 2017-09-11>
        Protected Sub lnkclassAttd_Click(sender As Object, e As EventArgs) Handles lnkclassAttd.Click
            Try
                Response.Redirect(ClassAttendancePage, False)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub



        '<Begin: added by LH On 04-04-2019 For ticket #20473>
        'Protected Sub lnkBooksPage_Click(sender As Object, e As EventArgs) Handles lnkBooksPage.Click
        '    Try
        '        Response.Redirect(BooksPage, False)
        '    Catch ex As Exception
        '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        '    End Try
        'End Sub
        'Protected Sub lnkCharteredBooks_Click(sender As Object, e As EventArgs) Handles lnkCharteredBooks.Click
        '    Try
        '        Response.Redirect(CharteredBooksPage, False)
        '    Catch ex As Exception
        '        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        '    End Try
        'End Sub


#End Region

    End Class
End Namespace
