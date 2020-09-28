'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Siddharth Kavitake         21/08/2014                        Displaying instructor's Classes on calender and in list view
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Explicit On
Option Strict On

Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Applications.ProductSetup
Imports System.Data
Imports Aptify.Framework.DataServices

Namespace Aptify.Framework.Web.eBusiness.Meetings
    Partial Class ViewMyClasses__c
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

        Protected Const ATTRIBUTE_CLASS_PAGE As String = "ClassPage"
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "ViewMyClassesPage"
        Dim sAllTimeTable As String = String.Empty
        Dim IsCalenderDate As Boolean = False
        Dim lCurriculumID As Long
        Dim lTimeTableID As Long
        Dim dtClasses As DataTable

#Region "MeetingsCalendar Specific Properties"
        ''' <summary>
        ''' Meeting page url
        ''' </summary>
        Public Overridable Property MeetingPage() As String
            Get
                If Not ViewState(ATTRIBUTE_CLASS_PAGE) Is Nothing Then
                    Return CStr(ViewState(ATTRIBUTE_CLASS_PAGE))
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                ViewState(ATTRIBUTE_CLASS_PAGE) = Me.FixLinkForVirtualPath(value)
            End Set
        End Property
#End Region

        Protected Overrides Sub SetProperties()

            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            'call base method to set parent properties
            MyBase.SetProperties()

            If String.IsNullOrEmpty(MeetingPage) Then
                'since value is the 'default' check the XML file for possible custom setting
                MeetingPage = Me.GetLinkValueFromXML(ATTRIBUTE_CLASS_PAGE)
            End If
        End Sub


        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            'set control properties from XML file if needed
            SetProperties()
            If Not IsPostBack Then
                LoadAcademicCycles()
                GetDataAndAssign()
                'grdClasses.Visible = False
                'hypCalendar.Visible = False
                pnlList.Style.Item("Display") = "none"
                hypCalendar.Style.Item("Display") = "none"
            End If
        End Sub
#Region "Load Data"
        Private Sub LoadAcademicCycles()
            Try
                Dim sSql As String = AptifyApplication.GetEntityBaseDatabase("TimeTable__c") & "..spGetAllAcademicYears__c"
                Dim dtCycles As DataTable = DataAction.GetDataTable(sSql, IAptifyDataAction.DSLCacheSetting.BypassCache)
                If Not dtCycles Is Nothing AndAlso dtCycles.Rows.Count > 0 Then
                    cmbAcademicCycle.DataSource = dtCycles
                    cmbAcademicCycle.DataTextField = "Name"
                    cmbAcademicCycle.DataValueField = "ID"
                    cmbAcademicCycle.DataBind()
                    cmbAcademicCycle.Items.Insert(0, New ListItem("--All--", "0"))
                    'ViewState("AllCurriculum") = dtCycles
                Else
                    cmbAcademicCycle.Items.Clear()
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

#Region "Calender"
        Protected Sub Calendar1_DayRender(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DayRenderEventArgs) Handles Calendar1.DayRender
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
                    'oDT = TryCast(ViewState("oDT"), DataTable)
                Else
                    dtClasses = TryCast(ViewState("oDT"), DataTable)
                    If Not dtClasses Is Nothing AndAlso dtClasses.Rows.Count > 0 Then
                        For Each oRow As DataRow In dtClasses.Rows
                            dStartDate = CDate(oRow("StartDate"))
                            dEndDate = CDate(oRow("EndDate"))
                            dCalDate = e.Day.Date
                            'sLinkColor = CStr(oRow("LinkColor"))

                            'If Trim(CStr(oRow("Status"))) = "Cancelled" Then
                            '    sLinkColor = System.Drawing.Color.Gainsboro.ToString
                            'End If


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

                                sMeetingLink = MeetingPage & "?ClassID=" & CStr(oRow("ClassID"))

                                link = New HyperLink
                                With link
                                    If String.IsNullOrEmpty(MeetingPage) Then
                                        .ToolTip = "Class Page property has not been set."
                                        .Enabled = False
                                    Else
                                        .Enabled = True
                                    End If
                                    .NavigateUrl = sMeetingLink
                                    '*** Uncomment to include meeting start and end time and Comment out the following line *** 
                                    '.Text = "<br><u>" & sStartTime & sEndtime & ":<br>" & CStr(oRow("meetingtitle")) & "</u><br>"
                                    '***
                                    .Text = "<br><u>" & CStr(oRow("ClassTitle")) & "</u><br>"
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

#Region "Combo Event"
        Protected Sub cmbAcademicCycle_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbAcademicCycle.SelectedIndexChanged
            Try                
                GetDataAndAssign()
                pnlList.Style.Item("Display") = "none"
                hypCalendar.Style.Item("Display") = "none"
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
#End Region

        Public Function GetNavigationURL(ByVal ClassID As String) As String
            Dim sURL As String = "#"
            sURL = MeetingPage & "?ClassID=" & ClassID
            Return sURL
        End Function


        Private Sub GetDataAndAssign()
            Dim sSql As String
            sSql = Database & "..spGetClassesForInstructor__c @InstructorID=" & User1.PersonID.ToString() & ", @AcademicCycleID=" & cmbAcademicCycle.SelectedValue
            dtClasses = Me.DataAction.GetDataTable(sSql)
            If Not dtClasses Is Nothing AndAlso dtClasses.Rows.Count > 0 Then
                ViewState("oDT") = dtClasses
                Calendar1.VisibleDate = CDate("1/" & CDate(dtClasses.Rows(0)("StartDate")).Month & "/" & CDate(dtClasses.Rows(0)("StartDate")).Year)
                grdClasses.DataSource = dtClasses
                grdClasses.DataBind()
                lblGridMsg.Visible = False
                'Calendar1.Visible = True
                pnlCalendar.Style.Item("Display") = "block"
            Else
                lblGridMsg.Visible = True
                grdClasses.DataSource = Nothing
                grdClasses.DataBind()
                'Calendar1.Visible = False
                pnlList.Style.Item("Display") = "none"
                pnlCalendar.Style.Item("Display") = "none"
            End If
        End Sub
    End Class
End Namespace