Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class GetCompanyList__c
    Inherits Aptify.Framework.Web.eBusiness.BaseWebService

    <WebMethod()> _
     <System.Web.Script.Services.ScriptMethod()> _
    Public Function GetCompanyList(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim sSQL As String = String.Empty
        Dim dt As New DataTable
        'sSQL = "select ID,Name + '\'+ City +'\'+ Country As Name  from " & Database & "..vwCompanies where Name like '" & prefixText & "%'"
 sSQL = Database & "..spGetCompaniesDetails__c @PrefixText='" & prefixText & "'"
        dt = DataAction.GetDataTable(sSQL)
        Dim txtItems As New List(Of String)
        Dim dbValues As String
        For Each row As DataRow In dt.Rows
            'dbValues = row("Name").ToString()
            'dbValues = dbValues
            ''  dbValues = dbValues.ToLower()
            'txtItems.Add(dbValues)

            dbValues = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row("Name").ToString, row("ID").ToString)
            txtItems.Add(dbValues)
        Next
        Return txtItems.ToArray()
    End Function
    <WebMethod()> _
     <System.Web.Script.Services.ScriptMethod()> _
    Public Function GetCompanyList1(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim sSQL As String = String.Empty
        Dim dt As New DataTable
        'sSQL = "select ID,Name + '\'+ City +'\'+ Country As Name  from " & Database & "..vwCompanies where Name like '" & prefixText & "%'"
        sSQL = Database & "..spGetCompaniesDetails__sd_c @SearchableText='" & prefixText & "'"
        dt = DataAction.GetDataTable(sSQL)
        Dim txtItems As New List(Of String)
        Dim dbValues As String
        For Each row As DataRow In dt.Rows
            'dbValues = row("Name").ToString()
            'dbValues = dbValues
            ''  dbValues = dbValues.ToLower()
            'txtItems.Add(dbValues)

            dbValues = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row("Name").ToString, row("ID").ToString)
            txtItems.Add(dbValues)
        Next
        Return txtItems.ToArray()
    End Function
End Class
