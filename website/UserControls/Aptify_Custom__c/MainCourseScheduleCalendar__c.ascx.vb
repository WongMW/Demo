'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Govind Mande                 3/07/2014                        Displaying Course on Calender
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Explicit On
Option Strict On

Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Applications.ProductSetup
Imports System.Data
Imports Aptify.Framework.DataServices

Namespace Aptify.Framework.Web.eBusiness.Meetings
    Partial Class MainCourseScheduleCalendar
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

        Protected Const ATTRIBUTE_MEETING_PAGE As String = "CoursePage"
        Protected Const ATTRIBUTE_MEETINGS_GRIDVIEW_PAGE As String = "MeetingGridViewPage"
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "MainCourseScheduleCalendarPage"
        Protected Const ATTRIBUTE_SHOWMEETINGSLINKTOCLASS_DEFAULT_NAME As String = "ShowMeetingsLinkToClass"
        Dim sAllTimeTable As String = String.Empty
        Dim IsCalenderDate As Boolean = False
        Dim lCurriculumID As Long
        Dim lTimeTableID As Long

#Region "MeetingsCalendar Specific Properties"
        ''' <summary>
        ''' Meeting page url
        ''' </summary>
        Public Overridable Property MeetingPage() As String
            Get
                If Not ViewState(ATTRIBUTE_MEETING_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_MEETING_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_MEETING_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        ''' <summary>
        ''' MeetingGridView page url
        ''' </summary>
        Public Overridable Property MeetingGridViewPage() As String
            Get
                If Not ViewState(ATTRIBUTE_MEETINGS_GRIDVIEW_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_MEETINGS_GRIDVIEW_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_MEETINGS_GRIDVIEW_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
        'Nalini issue 11290
        Protected Overridable ReadOnly Property ShowMeetingsLinkToClass() As Boolean
            Get
                If Not ViewState.Item(ATTRIBUTE_SHOWMEETINGSLINKTOCLASS_DEFAULT_NAME) Is Nothing Then
                    Return CBool(ViewState.Item(ATTRIBUTE_SHOWMEETINGSLINKTOCLASS_DEFAULT_NAME))
                Else
                    Dim value As String = Me.GetGlobalAttributeValue(ATTRIBUTE_SHOWMEETINGSLINKTOCLASS_DEFAULT_NAME)
                    If Not String.IsNullOrEmpty(value) Then
                        Select Case UCase(value)
                            Case "TRUE", "FALSE", "0", "1"
                                ViewState.Item(ATTRIBUTE_SHOWMEETINGSLINKTOCLASS_DEFAULT_NAME) = CBool(value)
                            Case Else
                                ViewState.Item(ATTRIBUTE_SHOWMEETINGSLINKTOCLASS_DEFAULT_NAME) = False
                        End Select
                    Else
                        ViewState.Item(ATTRIBUTE_SHOWMEETINGSLINKTOCLASS_DEFAULT_NAME) = False
                    End If
                End If
            End Get
        End Property
#End Region

        Protected Overrides Sub SetProperties()

            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            'call base method to set parent properties
            MyBase.SetProperties()

            If String.IsNullOrEmpty(MeetingPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                MeetingPage = Me.GetLinkValueFromXML(ATTRIBUTE_MEETING_PAGE)
            End If
            If String.IsNullOrEmpty(MeetingGridViewPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                MeetingGridViewPage = Me.GetLinkValueFromXML(ATTRIBUTE_MEETINGS_GRIDVIEW_PAGE)
                If Not String.IsNullOrEmpty(MeetingGridViewPage) Then
                    '   Me.MeetingGridPage.NavigateUrl = MeetingGridViewPage
                Else
                    ' Me.MeetingGridPage.Enabled = False
                    '  Me.MeetingGridPage.ToolTip = "MeetingGridViewPage property has not been set."
                End If
            End If
            Dim bShowMeeting As Boolean = ShowMeetingsLinkToClass
        End Sub

        'Amruta IssueID 15386,3/4/2013,Revert Meeting Center Calendar View Back to 5.5 Version
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            'set control properties from XML file if needed
            SetProperties()
            If Not IsPostBack Then
                'If Convert.ToInt32(Request.QueryString("CurrID")) > 0 Then
                '    lCurriculumID = Convert.ToInt32(Request.QueryString("CurrID"))
                'End If
                'If Convert.ToInt32(Request.QueryString("TTID")) > 0 Then
                '    lTimeTableID = Convert.ToInt32(Request.QueryString("TTID"))
                'End If
                cmbYear.SelectedValue = DatePart(DateInterval.Year, Today).ToString
                'cmbMonth.SelectedValue = DatePart(DateInterval.Month, Today).ToString
                LoadCurriculumn()
                LoadYear()
                If cmbYear.SelectedIndex = 0 Then
                    If cmbCurriculum.SelectedIndex = 0 Then
                        LoadTimeTable(0, 0)
                    Else
                        LoadTimeTable(0, Convert.ToInt32(cmbCurriculum.SelectedValue))
                    End If
                Else
                    If cmbCurriculum.SelectedIndex = 0 Then
                        LoadTimeTable(Convert.ToInt32(cmbYear.SelectedValue), 0)
                    Else
                        LoadTimeTable(Convert.ToInt32(cmbYear.SelectedValue), Convert.ToInt32(cmbCurriculum.SelectedValue))
                    End If
                End If
                ' Set Curriculumn and Time Table ID
                If Convert.ToInt32(Request.QueryString("CurrID")) > 0 Then
                    cmbCurriculum.SelectedValue = Convert.ToString(Request.QueryString("CurrID"))
                End If
                If Convert.ToInt32(Request.QueryString("TTID")) > 0 Then
                    drpTimeTable.SelectedValue = Convert.ToString(Request.QueryString("TTID"))
                End If
            End If
        End Sub
#Region "Load Data"
        ''' <summary>
        ''' Load Time Table Drop Down
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub LoadTimeTable(ByVal Year As Integer, ByVal CurriculumID As Integer)
            Try
                Dim sSql As String = AptifyApplication.GetEntityBaseDatabase("TimeTable__c") & "..spGetLoadTimeTable__c @Year=" & Year
                Dim dtTimeTable As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dtTimeTable Is Nothing AndAlso dtTimeTable.Rows.Count > 0 Then
                    drpTimeTable.DataSource = dtTimeTable
                    drpTimeTable.DataTextField = "Name"
                    drpTimeTable.DataValueField = "ID"
                    drpTimeTable.DataBind()
                    drpTimeTable.Items.Insert(0, "--All--")
                    ViewState("TimeTableID") = drpTimeTable.SelectedValue
                    ViewState("AllTimeTable") = dtTimeTable

                Else
                    drpTimeTable.Items.Clear()
                    ViewState("TimeTableID") = 0
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub LoadCurriculumn()
            Try
                Dim sSql As String = AptifyApplication.GetEntityBaseDatabase("TimeTable__c") & "..spGetCurriculum__c"
                Dim dtCurriculum As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dtCurriculum Is Nothing AndAlso dtCurriculum.Rows.Count > 0 Then
                    cmbCurriculum.DataSource = dtCurriculum
                    cmbCurriculum.DataTextField = "Name"
                    cmbCurriculum.DataValueField = "ID"
                    cmbCurriculum.DataBind()
                    cmbCurriculum.Items.Insert(0, "--All--")
                    ViewState("AllCurriculum") = dtCurriculum
                Else
                    cmbCurriculum.Items.Clear()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Private Sub LoadYear()
            Try
                Dim year As Integer = 2000
                For i As Integer = 0 To 20
                    Dim newyear As Integer = year + i
                    cmbYear.Items.Add(New ListItem(newyear.ToString()))
                Next
                cmbYear.Items.Insert(0, "--Select Year--")
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region
#Region "Calender"
        'Amruta IssueID 15386,3/4/2013,Revert Meeting Center Calendar View Back to 5.5 Version
        Protected Sub Calendar1_DayRender(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DayRenderEventArgs) Handles Calendar1.DayRender
            Dim sSQL As String
            Dim oDT As DataTable
            Dim dStartDate As Date
            Dim dEndDate As Date
            Dim dCalDate As Date
            Dim sMeetingLink As String
            Dim sStartTime As String, sEndtime As String
            Dim sLinkColor As String
            Dim dHeight As Double = 0
            Dim bEnabled As Boolean = True
            Dim link As HyperLink

            Try
                e.Cell.Height = 50
                e.Day.IsSelectable = False
                If e.Day.IsOtherMonth Then
                    'e.Cell.Controls.Clear()
                Else

                    ''sSQL = "Select 'Red' LinkColor, " & _
                    ''               "c.CourseID,c.Course, p.productcategoryid, m.productid, m.meetingtitle, m.status,'startdate' = CASE WHEN m.startdate > m.EndDate THEN m.EndDate ELSE m.startdate END, 'enddate'= CASE WHEN  LTRIM(RIGHT(CONVERT(VARCHAR(20), m.StartDate, 100), 7))='12:00AM' and LTRIM(RIGHT(CONVERT(VARCHAR(20),m. EndDate, 100), 7))='12:00AM' then Cast(m.EndDate AS varchar(11)) +' 12:00:01AM' Else m.EndDate END   from " & Me.Database & "..vwMeetings m " & _
                    ''               "inner join " & Me.Database & "..vwproducts p on p.id = m.productid " & _
                    ''               "left join " & Me.Database & "..vwmeetingattributes a on a.meetingid = m.id inner Join vwClasses c on c.ProductID=p.ID " & _
                    ''                "inner join vwTimeTables__c TT on TT.ID=c.TimeTableID " & _
                    ''                " inner join vwCurriculumCourses cc on cc.ID=c.CourseID inner join  vwcurriculumDefinitions cd   on cc.CurriculumID=cd.ID " & _
                    ''                "where p.webenabled=1  "
                    ' ''If Not ShowMeetingsLinkToClass Then
                    ' ''    sSQL &= (" AND  ISNULL(p.ClassID ,-1) <=0  ")
                    ' ''End If

                    ''If drpTimeTable.SelectedIndex = 0 Then

                    ''    Dim AllTimeTable As DataTable = CType(ViewState("AllTimeTable"), DataTable)
                    ''    If Not AllTimeTable Is Nothing AndAlso AllTimeTable.Rows.Count > 0 Then

                    ''        For Each dr As DataRow In AllTimeTable.Rows
                    ''            If sAllTimeTable = "" Then
                    ''                sAllTimeTable = Convert.ToString(dr("ID"))
                    ''            Else
                    ''                sAllTimeTable = sAllTimeTable & "," & Convert.ToString(dr("ID"))
                    ''            End If
                    ''        Next
                    ''        ViewState("TimeTableID") = sAllTimeTable
                    ''        sSQL &= (" AND  TT.ID IN (" & Convert.ToString(ViewState("TimeTableID"))) & ")"
                    ''    End If

                    ''Else
                    ''    ViewState("TimeTableID") = drpTimeTable.SelectedValue
                    ''    sSQL &= (" AND  TT.ID IN (" & Convert.ToString(ViewState("TimeTableID"))) & ")"
                    ''End If



                    ' ''If sAllTimeTable <> "" Then
                    ' ''    sSQL &= (" AND  TT.ID IN (" & Convert.ToString(ViewState("TimeTableID"))) & ")"
                    ' ''End If

                    ''If cmbCurriculum.SelectedIndex = 0 Then
                    ''    Dim dtAllCurriculum As DataTable = CType(ViewState("AllCurriculum"), DataTable)
                    ''    Dim sCurriculum As String = String.Empty
                    ''    For Each dr As DataRow In dtAllCurriculum.Rows
                    ''        If sCurriculum = "" Then
                    ''            sCurriculum = Convert.ToString(dr("ID"))
                    ''        Else
                    ''            sCurriculum = sCurriculum & "," & Convert.ToString(dr("ID"))
                    ''        End If
                    ''    Next

                    ''    sSQL &= (" AND cd.ID in(" & sCurriculum & ")")
                    ''End If
                    ''sSQL &= "order by m.startdate"
                    ''oDT = Me.DataAction.GetDataTable(sSQL)
                    oDT = TryCast(ViewState("oDT"), DataTable)
                    If Not oDT Is Nothing AndAlso oDT.Rows.Count > 0 Then
                        For Each oRow As DataRow In oDT.Rows
                            dStartDate = CDate(oRow("StartDate"))
                            dEndDate = CDate(oRow("EndDate"))
                            dCalDate = e.Day.Date
                            sLinkColor = CStr(oRow("LinkColor"))

                            If Trim(CStr(oRow("Status"))) = "Cancelled" Then
                                sLinkColor = System.Drawing.Color.Gainsboro.ToString
                            End If

                          
                            'If CDate(dCalDate.ToString("MM/dd/yyyy")) >= CDate(dStartDate.ToString("MM/dd/yyyy")) AndAlso
                            '    CDate(dCalDate.ToString("MM/dd/yyyy")) <= CDate(dEndDate.ToString("MM/dd/yyyy")) AndAlso bEnabled Then
                            If CDate(dCalDate.ToString("dd/MM/yyyy")) >= CDate(dStartDate.ToString("dd/MM/yyyy")) AndAlso
                              CDate(dCalDate.ToString("dd/MM/yyyy")) <= CDate(dEndDate.ToString("dd/MM/yyyy")) AndAlso bEnabled Then


                                sStartTime = dStartDate.ToString("hh:mm tt")
                                sEndtime = dEndDate.ToString("hh:mm tt")

                                If sEndtime = "12:00 AM" Then
                                    sEndtime = ""
                                Else
                                    sEndtime = " - " & sEndtime
                                End If

                                sMeetingLink = MeetingPage & "?CourseID=" & CStr(oRow("CourseID"))

                                link = New HyperLink
                                With link
                                    If String.IsNullOrEmpty(MeetingPage) Then
                                        .ToolTip = "MeetingPage property has not been set."
                                        .Enabled = False
                                    Else
                                        .Enabled = True
                                    End If
                                    .NavigateUrl = sMeetingLink
                                    '*** Uncomment to include meeting start and end time and Comment out the following line *** 
                                    '.Text = "<br><u>" & sStartTime & sEndtime & ":<br>" & CStr(oRow("meetingtitle")) & "</u><br>"
                                    '***
                                    .Text = "<br><u>" & CStr(oRow("Course")) & "</u><br>"
                                End With

                                e.Cell.Controls.Add(link)
                                dHeight = Math.Max(dHeight, e.Cell.Height.Value)
                            End If
                        Next
                    End If


                   
                End If

            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region
       
#Region "On Click"
        Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
            If cmbYear.SelectedIndex > 0 Then
                Dim sSQL As String
                Dim oDT As DataTable
                ''sSQL = "Select 'Red' LinkColor, " & _
                ''                  "c.CourseID,c.Course, p.productcategoryid, m.productid, m.meetingtitle, m.status,'startdate' = CASE WHEN m.startdate > m.EndDate THEN m.EndDate ELSE m.startdate END, 'enddate'= CASE WHEN  LTRIM(RIGHT(CONVERT(VARCHAR(20), m.StartDate, 100), 7))='12:00AM' and LTRIM(RIGHT(CONVERT(VARCHAR(20),m. EndDate, 100), 7))='12:00AM' then Cast(m.EndDate AS varchar(11)) +' 12:00:01AM' Else m.EndDate END   from " & Me.Database & "..vwMeetings m " & _
                ''                  "inner join " & Me.Database & "..vwproducts p on p.id = m.productid " & _
                ''                  "left join " & Me.Database & "..vwmeetingattributes a on a.meetingid = m.id inner Join vwClasses c on c.ProductID=p.ID " & _
                ''                   "inner join vwTimeTables__c TT on TT.ID=c.TimeTableID " & _
                ''                   " inner join vwCurriculumCourses cc on cc.ID=c.CourseID inner join  vwcurriculumDefinitions cd   on cc.CurriculumID=cd.ID " & _
                ''                   "where p.webenabled=1  "
                ' ''If Not ShowMeetingsLinkToClass Then
                ' ''    sSQL &= (" AND  ISNULL(p.ClassID ,-1) <=0  ")
                ' ''End If

                ''If drpTimeTable.SelectedIndex = 0 Then

                ''    Dim AllTimeTable As DataTable = CType(ViewState("AllTimeTable"), DataTable)
                ''    If Not AllTimeTable Is Nothing AndAlso AllTimeTable.Rows.Count > 0 Then

                ''        For Each dr As DataRow In AllTimeTable.Rows
                ''            If sAllTimeTable = "" Then
                ''                sAllTimeTable = Convert.ToString(dr("ID"))
                ''            Else
                ''                sAllTimeTable = sAllTimeTable & "," & Convert.ToString(dr("ID"))
                ''            End If
                ''        Next
                ''        ViewState("TimeTableID") = sAllTimeTable
                ''        sSQL &= (" AND  TT.ID IN (" & Convert.ToString(ViewState("TimeTableID"))) & ")"
                ''    End If

                ''Else
                ''    ViewState("TimeTableID") = drpTimeTable.SelectedValue
                ''    sSQL &= (" AND  TT.ID IN (" & Convert.ToString(ViewState("TimeTableID"))) & ")"
                ''End If



                ' ''If sAllTimeTable <> "" Then
                ' ''    sSQL &= (" AND  TT.ID IN (" & Convert.ToString(ViewState("TimeTableID"))) & ")"
                ' ''End If

                ''If cmbCurriculum.SelectedIndex = 0 Then
                ''    Dim dtAllCurriculum As DataTable = CType(ViewState("AllCurriculum"), DataTable)
                ''    Dim sCurriculum As String = String.Empty
                ''    For Each dr As DataRow In dtAllCurriculum.Rows
                ''        If sCurriculum = "" Then
                ''            sCurriculum = Convert.ToString(dr("ID"))
                ''        Else
                ''            sCurriculum = sCurriculum & "," & Convert.ToString(dr("ID"))
                ''        End If
                ''    Next

                ''    sSQL &= (" AND cd.ID in(" & sCurriculum & ")")
                ''End If
                ''sSQL &= "order by m.startdate"

                If drpTimeTable.SelectedIndex = 0 Then

                    Dim AllTimeTable As DataTable = CType(ViewState("AllTimeTable"), DataTable)
                    If Not AllTimeTable Is Nothing AndAlso AllTimeTable.Rows.Count > 0 Then

                        For Each dr As DataRow In AllTimeTable.Rows
                            If sAllTimeTable = "" Then
                                sAllTimeTable = Convert.ToString(dr("ID"))
                            Else
                                sAllTimeTable = sAllTimeTable & "," & Convert.ToString(dr("ID"))
                            End If
                        Next
                        ViewState("TimeTableID") = sAllTimeTable
                        'sSQL &= (" AND  TT.ID IN (" & Convert.ToString(ViewState("TimeTableID"))) & ")"
                    End If

                Else
                    ViewState("TimeTableID") = drpTimeTable.SelectedValue
                    '  sSQL &= (" AND  TT.ID IN (" & Convert.ToString(ViewState("TimeTableID"))) & ")"
                End If
                If cmbCurriculum.SelectedIndex = 0 Then
                    Dim dtAllCurriculum As DataTable = CType(ViewState("AllCurriculum"), DataTable)
                    Dim sCurriculum As String = String.Empty
                    For Each dr As DataRow In dtAllCurriculum.Rows
                        If sCurriculum = "" Then
                            sCurriculum = Convert.ToString(dr("ID"))
                        Else
                            sCurriculum = sCurriculum & "," & Convert.ToString(dr("ID"))
                        End If
                    Next
                    ViewState("CurriculumID") = sCurriculum
                    'sSQL &= (" AND cd.ID in(" & sCurriculum & ")")
                Else
                    ViewState("CurriculumID") = cmbCurriculum.SelectedValue
                End If

                If Not ViewState("TimeTableID") Is Nothing AndAlso Not ViewState("CurriculumID") Is Nothing Then
                    sSQL = Database & "..spGetCourseOnCalender__c @CurrID='" & Convert.ToString(ViewState("CurriculumID")) & "',@TTID='" & Convert.ToString(ViewState("TimeTableID")) & "'"
                ElseIf Not ViewState("TimeTableID") Is Nothing Then
                    sSQL = Database & "..spGetCourseOnCalender__c @TTID='" & Convert.ToString(ViewState("TimeTableID")) & "'"
                ElseIf Not ViewState("CurriculumID") Is Nothing Then
                    sSQL = Database & "..spGetCourseOnCalender__c @CurrID='" & Convert.ToString(ViewState("CurriculumID")) & "'"
                End If
                oDT = Me.DataAction.GetDataTable(sSQL)
                If Not oDT Is Nothing AndAlso oDT.Rows.Count > 0 Then
                    ViewState("oDT") = oDT
                    Calendar1.VisibleDate = CDate("1/" & CDate(oDT.Rows(0)("StartDate")).Month & "/" & cmbYear.SelectedValue)
                End If


            End If
                ' cmbBottomYear.SelectedValue = cmbYear.SelectedValue
                '  cmbBottomMonth.SelectedValue = cmbMonth.SelectedValue

                ViewState("TimeTableID") = drpTimeTable.SelectedValue
          

        End Sub
#End Region

#Region "Combo Event"
        ''' <summary>
        ''' Selection of Year displaying Time Table
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Sub cmbYear_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbYear.SelectedIndexChanged
            Try

                If cmbYear.SelectedIndex = 0 Then
                    If cmbCurriculum.SelectedIndex > 0 Then
                        LoadTimeTable(0, Convert.ToInt32(cmbCurriculum.SelectedValue))

                    Else
                        If cmbCurriculum.SelectedIndex > 0 Then
                            LoadTimeTable(0, Convert.ToInt32(cmbCurriculum.SelectedValue))
                        Else
                            LoadTimeTable(0, 0)
                        End If

                    End If
                Else
                    If cmbCurriculum.SelectedIndex > 0 Then
                        LoadTimeTable(Convert.ToInt32(cmbYear.SelectedValue), Convert.ToInt32(cmbCurriculum.SelectedValue))
                    Else
                        LoadTimeTable(Convert.ToInt32(cmbYear.SelectedValue), 0)
                    End If
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region
        

 
    

      
    End Class
End Namespace