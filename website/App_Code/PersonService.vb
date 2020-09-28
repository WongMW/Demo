Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness
Imports System.Collections.Generic
Imports System

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class PersonService
    Inherits BaseWebService
    Private _dataAction As New DataAction()

    <WebMethod()> _
    Public Function HelloWorld() As String
        Return "Hello World"
    End Function

    <WebMethod()> _
    Public Function SearchPersonsByCompany(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)
        Dim sql As String = String.Empty
        Dim Products As List(Of String) = New List(Of String)
        Dim strArr() As String
        strArr = contextKey.Split(";")
        sql = Database & "..spGetPersonByCompany__c @prefixText='" & prefixText & "',@Count=" & count & ",@PersonID=" & strArr(0).ToString() & ",@CompanyID=" & strArr(1).ToString()
        Dim customers As List(Of String) = New List(Of String)
        Try
            Dim personList As DataTable = _dataAction.GetDataTable(sql)
            For Each row As DataRow In personList.Rows
                Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row("FirstLast").ToString, row("ID").ToString)
                customers.Add(item)
            Next
            Return customers
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            Return Nothing
        End Try
    End Function

    <WebMethod()> _
    Public Function SearchPersons(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)
        Dim sSQL As String
        Dim sdr As System.Data.IDataReader
        Dim Products As List(Of String) = New List(Of String)
        sSQL = Database & "..spGetPersonAutoComplete__c"
        Dim pName As Data.IDataParameter = Nothing
        Dim pCount As Data.IDataParameter = Nothing
        Dim pCompanyID As Data.IDataParameter = Nothing
        Dim pCategory As Data.IDataParameter = Nothing
        Dim objDA As New DataAction
        Dim colParams(2) As Data.IDataParameter
        Try
            pName = objDA.GetDataParameter("@prefixText", SqlDbType.VarChar, prefixText)
            pCount = objDA.GetDataParameter("@Count", SqlDbType.Int, count)
            pCompanyID = objDA.GetDataParameter("CompanyID", SqlDbType.Int, CInt(contextKey))
            colParams(0) = pName
            colParams(1) = pCount
            colParams(2) = pCompanyID
            Dim customers As List(Of String) = New List(Of String)
            sdr = objDA.ExecuteDataReaderParametrized(sSQL, CommandType.StoredProcedure, colParams)

            While sdr.Read
                Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(sdr("FirstLast").ToString, sdr("ID").ToString)
                customers.Add(item)
            End While
            Return customers
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            Return Nothing
        End Try
    End Function

End Class