'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Pradip  Chavhan        10-03-2016                          
Imports System.Data
Imports System.Web
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web
'Imports Microsoft.Reporting.WebForms
Imports System.Reflection

Namespace Aptify.Framework.Web.eBusiness
    Partial Class ReportViewer__c
        Inherits BaseUserControlAdvanced

        '' Protected Const ATTRIBUTE_REPORT_PAGE_REDIRECT_URL As String = "ReportViewerURL"
        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "InvoiceReportViewer"

        Private m_sReportPage As String
        Public Property ReportViewerPageURL() As String
            Get
                Return m_sReportPage
            End Get
            Set(ByVal value As String)
                m_sReportPage = Me.FixLinkForVirtualPath(value)
            End Set
        End Property

        Protected Overrides Sub SetProperties()

            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME

            'If String.IsNullOrEmpty(ReportViewerPageURL) Then
            '    ReportViewerPageURL = Me.GetLinkValueFromXML(ATTRIBUTE_REPORT_PAGE_REDIRECT_URL)
            '    If String.IsNullOrEmpty(ReportViewerPageURL) Then
            '        'Me.lnkReports.Enabled = False
            '        'Me.lnkReports.ToolTip = "ReportRedirectURL property has not been set."
            '    End If
            'End If
        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            SetProperties()
            If Not Page.IsPostBack Then
                Try
                    Dim sServerName As String = System.Configuration.ConfigurationManager.AppSettings("AptifyDBServer").ToString
                    Dim sDatabaseName As String = System.Configuration.ConfigurationManager.AppSettings("AptifyUsersDB").ToUpper
                    Dim sDatabaseUserName As String = System.Configuration.ConfigurationManager.AppSettings("AptifyEBusinessSQLLogin")

                    Dim sDatabaseUserPassword As String = System.Configuration.ConfigurationManager.AppSettings("AptifyEBusinessSQLPWD")
                    Dim myConnectionInfo As CrystalDecisions.Shared.ConnectionInfo = New CrystalDecisions.Shared.ConnectionInfo()
                    myConnectionInfo.ServerName = sServerName
                    myConnectionInfo.DatabaseName = sDatabaseName
                    myConnectionInfo.UserID = sDatabaseUserName
                    myConnectionInfo.Password = sDatabaseUserPassword
                    myConnectionInfo.IntegratedSecurity = True
                    Dim rptdoc As ReportDocument = New ReportDocument()
                    Dim rptDt As DataTable = Nothing
                    Dim oParams(0) As IDataParameter
                    Dim sSql As String
                    Dim _rptParameter As AptifyCrystalReport__c
                    _rptParameter = Session(Me.AptifyApplication.GetEntityAttribute("CrystalReportDetails__c", "ParameterSession").Trim.ToLower())
                    oParams(0) = Me.DataAction.GetDataParameter("@rptID", _rptParameter.ReportID)
                    sSql = Database & "..spGetCrystalReportDetails__c"
                    rptDt = DataAction.GetDataTableParametrized(sSql, CommandType.StoredProcedure, oParams)
                    If rptDt IsNot Nothing AndAlso rptDt.Rows.Count > 0 Then
                        Dim reportPath As String = Server.MapPath("~/Reports__c/" & rptDt.Rows(0)("ReportFileName"))
                        rptdoc.Load(reportPath)
                        For Each dr As DataRow In rptDt.Rows
                            If Convert.ToString(dr("ParameterName")).Trim <> "" Then
                                Dim myPropInfo As PropertyInfo = _rptParameter.GetProperyInfo(Convert.ToString(dr("PropertyName")))
                                Dim ParameterValue As String = CType(myPropInfo.GetValue(_rptParameter, Nothing), String)
                                If Convert.ToString(dr("IsParent")) = "0" Then
                                    rptdoc.SetParameterValue(Convert.ToString(dr("ParameterName")), ParameterValue)
                                Else
                                    rptdoc.SetParameterValue(Convert.ToString(dr("ParameterName")), ParameterValue, Convert.ToString(dr("ReportFileName")))
                                End If
                            End If
                        Next
                    End If
                    Session(Me.AptifyApplication.GetEntityAttribute("ReportDetail__c", "ParameterSession").Trim.ToLower()) = Nothing
                    rptViewerMain.ReportSource = rptdoc
                    Try
                        SetDBLogonForReport(myConnectionInfo, rptdoc)
                        rptdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, HttpContext.Current.Response, False, "")
                        'rptdoc.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath("~/Reports__c/pradip.pdf"))
                    Catch ex As Exception
                        Response.Write(ex.ToString())
                        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                    Finally
                        rptdoc.Close()
                        rptdoc.Dispose()
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
