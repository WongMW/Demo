'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Siddharth Kavitake         8/12/2014                           Added conditions to show granted exemptions report 
Imports System.Data
Imports System.Web
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web
Imports Microsoft.Reporting.WebForms

Namespace Aptify.Framework.Web.eBusiness
    Partial Class AppealReportViewer__c
        Inherits BaseUserControlAdvanced

        Protected Const ATTRIBUTE_REPORT_PAGE_REDIRECT_URL As String = "ReportRedirectURL"
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "InvoiceReportViewer"

        Private m_sReportPage As String
        Public Property ReportPageRedirectURL() As String
            Get
                Return m_sReportPage
            End Get
            Set(ByVal value As String)
                m_sReportPage = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Protected Overrides Sub SetProperties()

            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME

            If String.IsNullOrEmpty(ReportPageRedirectURL) Then
                ReportPageRedirectURL = Me.GetLinkValueFromXML(ATTRIBUTE_REPORT_PAGE_REDIRECT_URL)
                If String.IsNullOrEmpty(ReportPageRedirectURL) Then
                    'Me.lnkReports.Enabled = False
                    'Me.lnkReports.ToolTip = "ReportRedirectURL property has not been set."
                End If
            End If
        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            SetProperties()
            If Not Page.IsPostBack Then
                Try
                    Dim sServerName As String = System.Configuration.ConfigurationManager.AppSettings("AptifyDBServer").ToString
                    Dim sDatabaseName As String = System.Configuration.ConfigurationManager.AppSettings("AptifyUsersDB").ToUpper
                    Dim sDatabaseUserName As String = System.Configuration.ConfigurationManager.AppSettings("AptifyEBusinessSQLLogin")
                    Dim myConnectionInfo As CrystalDecisions.Shared.ConnectionInfo = New CrystalDecisions.Shared.ConnectionInfo()

                    myConnectionInfo.ServerName = sServerName
                    myConnectionInfo.DatabaseName = sDatabaseName
                    myConnectionInfo.IntegratedSecurity = True

                    Dim rptdoc As ReportDocument = New ReportDocument()
                    Dim crParameterFieldDefinitions As ParameterFieldDefinitions
                    Dim crParameterFieldDefinition As ParameterFieldDefinition
                    Dim crParameterValues As New ParameterValues
                    Dim crParameterDiscreteValue As New ParameterDiscreteValue

                    If Not Request.QueryString("sReportName") Is Nothing AndAlso Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("sReportName")).ToLower = "printgrantedexemptions" Then
                        Dim reportPath As String = Server.MapPath("~/Reports__c/GrantedExemptions__c.rpt")
                        rptdoc.Load(reportPath)

                        'crParameterDiscreteValue.Value = CInt(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("ExmAplID")))
                        'crParameterFieldDefinitions = rptdoc.DataDefinition.ParameterFields()
                        'crParameterFieldDefinition = crParameterFieldDefinitions.Item("@ExmpAppID")
                        'crParameterValues = crParameterFieldDefinition.CurrentValues
                        'crParameterValues.Clear()
                        'crParameterValues.Add(crParameterDiscreteValue)
                        'crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)
                        rptdoc.SetParameterValue("@ExmpAppID", Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("ExmAplID")))
                        ''Added By Pradip 2015-05-04
                    ElseIf Not Request.QueryString("sReportName") Is Nothing AndAlso Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("sReportName")).ToLower = "externalappreport" Then
                        Dim reportPath As String = Server.MapPath("~/Reports__c/ExtStudentExamReport__c.rpt")
                        rptdoc.Load(reportPath)
                        rptdoc.SetParameterValue("@PersonID", Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("PersonID")))
                        rptdoc.SetParameterValue("@PersonID", Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("PersonID")), rptdoc.Subreports(0).Name.ToString())

                    ElseIf Not Request.QueryString("sReportName") Is Nothing AndAlso Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("sReportName")).ToLower = "formalregreport" Then
                        Dim reportPath As String = Server.MapPath("~/Reports__c/FormalRegistrationPrint__c.rpt")
                        rptdoc.Load(reportPath)
                        rptdoc.SetParameterValue("@ID", Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("FormalRegID")))

                    ElseIf Not Request.QueryString("sReportName") Is Nothing AndAlso Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("sReportName")).ToLower = "msreport" Then
                        Dim reportPath As String = Server.MapPath("~/Reports__c/MasterScheduleDetailsReport__c.rpt")
                        rptdoc.Load(reportPath)
                        rptdoc.SetParameterValue("@ParentCompanyId", Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("sParentCompanyID")))
                        rptdoc.SetParameterValue("@MasterID", Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("sMasterID")))

                    ElseIf Not Request.QueryString("sReportName") Is Nothing AndAlso Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("sReportName")).ToLower = "cadiaryreport" Then
                        Dim reportPath As String = Server.MapPath("~/Reports__c/CADiaryReport.rpt")
                        rptdoc.Load(reportPath)
                        rptdoc.SetParameterValue("@StudentID", Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("PersonID")))
                        rptdoc.SetParameterValue("@StudentID", Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("PersonID")), rptdoc.Subreports(0).Name.ToString())
                        rptdoc.SetParameterValue("@StudentID", Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("PersonID")), rptdoc.Subreports(1).Name.ToString())
                        rptdoc.SetParameterValue("@StudentID", Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("PersonID")), rptdoc.Subreports(2).Name.ToString())
                        rptdoc.SetParameterValue("@StudentID", Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("PersonID")), rptdoc.Subreports(3).Name.ToString())
                        rptdoc.SetParameterValue("@StudentID", Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("PersonID")), rptdoc.Subreports(4).Name.ToString())
                        rptdoc.SetParameterValue("@StudentID", Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("PersonID")), rptdoc.Subreports(5).Name.ToString())
                        rptdoc.SetParameterValue("@StudentID", Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("PersonID")), rptdoc.Subreports(6).Name.ToString())
                        rptdoc.SetParameterValue("@StudentID", Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("PersonID")), rptdoc.Subreports(7).Name.ToString())
                    ElseIf Not Request.QueryString("sReportName") Is Nothing AndAlso Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("sReportName")).ToLower = "pd1" Then
                        Dim reportPath As String = Server.MapPath("~/Reports__c/CADiaryReport.rpt")
                        rptdoc.Load(reportPath)
                        rptdoc.SetParameterValue("@StudentID", Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("PersonID")))
                        rptdoc.SetParameterValue("@StudentID", Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("PersonID")), rptdoc.Subreports(0).Name.ToString())
                        rptdoc.SetParameterValue("@StudentID", Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("PersonID")), rptdoc.Subreports(1).Name.ToString())
                        rptdoc.SetParameterValue("@StudentID", Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("PersonID")), rptdoc.Subreports(2).Name.ToString())
                        rptdoc.SetParameterValue("@StudentID", Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("PersonID")), rptdoc.Subreports(3).Name.ToString())
                        rptdoc.SetParameterValue("@StudentID", Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("PersonID")), rptdoc.Subreports(4).Name.ToString())
                        rptdoc.SetParameterValue("@StudentID", Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("PersonID")), rptdoc.Subreports(5).Name.ToString())
                        rptdoc.SetParameterValue("@StudentID", Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("PersonID")), rptdoc.Subreports(6).Name.ToString())
                        rptdoc.SetParameterValue("@StudentID", Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("PersonID")), rptdoc.Subreports(7).Name.ToString())

                    ElseIf Not Request.QueryString("sReportName") Is Nothing AndAlso Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("sReportName")).ToLower = "pd2" Then
                        Dim reportPath As String = Server.MapPath("~/Reports__c/CAP2Report.rpt")
                        rptdoc.Load(reportPath)
                        rptdoc.SetParameterValue("@StudentID", Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("PersonID")))
                        rptdoc.SetParameterValue("@StudentID", Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("PersonID")), rptdoc.Subreports(1).Name.ToString())
                        'rptdoc.SetParameterValue("@StudentID", Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("PersonID")), rptdoc.Subreports(1).Name.ToString())
                        rptdoc.SetParameterValue("@StudentID", Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("PersonID")), rptdoc.Subreports(2).Name.ToString())
 ElseIf Not Request.QueryString("sReportName") Is Nothing AndAlso Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("sReportName")).ToLower = "auth" Then
                        Dim reportPath As String = Server.MapPath("~/Reports__c/AuthorisationCertificate__c.rpt")
                        rptdoc.Load(reportPath)
                        rptdoc.SetParameterValue("@ID", Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("ID")))                    

Else
                        'AppealFailReport__c.rpt()
                        'Dim reportPath As String = Server.MapPath("~/UserControls/Aptify_Custom__c/Appeal Application.rpt") '
                        Dim reportPath As String = Server.MapPath("~/Reports__c/AppealFailReport__c.rpt")
                        rptdoc.Load(reportPath)

                        crParameterDiscreteValue.Value = CInt(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("sID")))
                        crParameterFieldDefinitions = rptdoc.DataDefinition.ParameterFields()
                        crParameterFieldDefinition = crParameterFieldDefinitions.Item("@StudentID")
                        crParameterValues = crParameterFieldDefinition.CurrentValues
                        crParameterValues.Clear()
                        crParameterValues.Add(crParameterDiscreteValue)
                        crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                        crParameterDiscreteValue = Nothing
                        crParameterDiscreteValue = New ParameterDiscreteValue()
                        crParameterDiscreteValue.Value = CInt(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("CourseID"))) '2nd current value
                        crParameterFieldDefinitions = rptdoc.DataDefinition.ParameterFields()
                        crParameterFieldDefinition = crParameterFieldDefinitions.Item("@CourseID")
                        'Add the second current value for the parameter field
                        crParameterValues.Add(crParameterDiscreteValue)
                        crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                        crParameterDiscreteValue = Nothing
                        crParameterDiscreteValue = New ParameterDiscreteValue()
                        crParameterDiscreteValue.Value = CInt(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("ClassID")))
                        crParameterFieldDefinitions = rptdoc.DataDefinition.ParameterFields()
                        crParameterFieldDefinition = crParameterFieldDefinitions.Item("@ClassID")
                        'Add the second current value for the parameter field
                        crParameterValues.Add(crParameterDiscreteValue)
                        crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                        crParameterDiscreteValue = Nothing
                        crParameterDiscreteValue = New ParameterDiscreteValue()
                        crParameterDiscreteValue.Value = CInt(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("CRID")))
                        crParameterFieldDefinitions = rptdoc.DataDefinition.ParameterFields()
                        crParameterFieldDefinition = crParameterFieldDefinitions.Item("@ClassRegistrationID")
                        'Add the second current value for the parameter field
                        crParameterValues.Add(crParameterDiscreteValue)
                        crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                        crParameterDiscreteValue = Nothing
                        crParameterDiscreteValue = New ParameterDiscreteValue()
                        crParameterDiscreteValue.Value = CInt(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("AppealTypeID")))
                        crParameterFieldDefinitions = rptdoc.DataDefinition.ParameterFields()
                        crParameterFieldDefinition = crParameterFieldDefinitions.Item("@AppealReasonID")
                        'Add the second current value for the parameter field
                        crParameterValues.Add(crParameterDiscreteValue)
                        crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                        crParameterDiscreteValue = Nothing
                        crParameterDiscreteValue = New ParameterDiscreteValue()
                        crParameterDiscreteValue.Value = CInt(Aptify.Framework.Web.Common.WebCryptography.Decrypt(Request.QueryString("OrderID")))
                        crParameterFieldDefinitions = rptdoc.DataDefinition.ParameterFields()
                        crParameterFieldDefinition = crParameterFieldDefinitions.Item("@OrderID")
                        'Add the second current value for the parameter field
                        crParameterValues.Add(crParameterDiscreteValue)
                        crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)
                    End If
                    rptViewerMain.ReportSource = rptdoc
                    Try
                        SetDBLogonForReport(myConnectionInfo, rptdoc)
                        rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, HttpContext.Current.Response, False, "")

                    Catch ex As Exception
                        Response.Write(ex.ToString())
                    Finally
                        rptdoc.Close()
                    End Try
                Catch ex As Exception
                    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                End Try
            End If
        End Sub
        Private Sub SetDBLogonForReport(ByVal myConnectionInfo As ConnectionInfo, ByVal myReportDocument As ReportDocument)
            Dim myTables As Tables = myReportDocument.Database.Tables
            For Each myTable As CrystalDecisions.CrystalReports.Engine.Table In myTables
                Dim myTableLogonInfo As TableLogOnInfo = myTable.LogOnInfo
                myTableLogonInfo.ConnectionInfo = myConnectionInfo
                myTable.ApplyLogOnInfo(myTableLogonInfo)
            Next
        End Sub
    End Class
End Namespace
