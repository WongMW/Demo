Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data
' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
 Public Class GetCompanyDetails__c
    Inherits Aptify.Framework.Web.eBusiness.BaseWebService

    <WebMethod()> _
    Public Function HelloWorld() As String
        Return "Hello World"
    End Function
    <WebMethod()>
 <System.Web.Script.Services.ScriptMethod()> _
    Public Function GetCompanyDetails(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Dim sSQL As String = String.Empty
        Dim dt As New DataTable
        ' sSQL = "select ID, isnull(Name,'') + '/'+ isNull(City ,'')+'/'+ isNull(County,'') As Name   from " & Database & "..vwCompanies where Name like '" & prefixText & "%'"
        sSQL = "..spGetCompanyNameDetails__c @PrefixText='" & prefixText & "'"
        dt = DataAction.GetDataTable(sSQL)
        Dim txtItems As New List(Of String)
        Dim dbValues As String
        For Each row As DataRow In dt.Rows
            Dim sCompany As String = row("Name").ToString()
            Dim lastLetter As Char = sCompany(sCompany.Length - 1)
            Dim sCompanyName As String
            If lastLetter = "/" Then
                sCompanyName = sCompany.Substring(0, sCompany.Length - 1)
                If sCompanyName(sCompanyName.Length - 1) = "/" Then
                    sCompanyName = sCompanyName.Substring(0, sCompany.Length - 2)
                End If
            Else
                sCompanyName = sCompany.Substring(0, sCompany.Length)
            End If

            dbValues = sCompanyName
            dbValues = dbValues
            '  dbValues = dbValues.ToLower()
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(sCompanyName.ToString, row("ID").ToString())
            txtItems.Add(item)
           

            'txtItems.Add(dbValues)
        Next
        Return txtItems
    End Function

    <WebMethod()>
 <System.Web.Script.Services.ScriptMethod()> _
    Public Function GetAwardingBodies(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Dim sSQL As String = String.Empty
        Dim dt As New DataTable
        sSQL = "..spGetAwardingBodiesByName__c @PrefixText='" & prefixText & "'"
        dt = DataAction.GetDataTable(sSQL)
        Dim txtItems As New List(Of String)
        For Each row As DataRow In dt.Rows
            Dim sAwardingBody As String = row("Name").ToString()
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(sAwardingBody, row("ID").ToString())
            txtItems.Add(item)
        Next
        Return txtItems
    End Function

    <WebMethod()>
 <System.Web.Script.Services.ScriptMethod()> _
    Public Function GetQualifications(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)
        Dim txtItems As New List(Of String)
        If Not String.IsNullOrEmpty(contextKey) Then
            Dim sSQL As String = String.Empty
            Dim dt As New DataTable
            sSQL = "..spGetQualificationByNameAndEduLevel__c @PrefixText='" & prefixText & "', @EducationLevelID=" & contextKey
            dt = DataAction.GetDataTable(sSQL)
            For Each row As DataRow In dt.Rows
                Dim sDegree As String = row("Name").ToString()
                Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(sDegree, row("ID").ToString())
                txtItems.Add(item)
            Next
        End If        
        Return txtItems
    End Function

''Added BY PRadip 2016-05-16 For Bug #13178

    <WebMethod()>
<System.Web.Script.Services.ScriptMethod()> _
    Public Function GetQualificationsForEEApp(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Dim txtItems As New List(Of String)

        Dim sSQL As String = String.Empty
        Dim dt As New DataTable
        sSQL = "..spGetQualificationForEEApp__c @PrefixText='" & prefixText & "'"
        dt = DataAction.GetDataTable(sSQL)
        For Each row As DataRow In dt.Rows
            Dim sDegree As String = row("Name").ToString()
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(sDegree, row("ID").ToString())
            txtItems.Add(item)
        Next
        Return txtItems
    End Function
    ''End here Added BY PRadip 2016-05-16 For Bug #13178

    <WebMethod()>
 <System.Web.Script.Services.ScriptMethod()> _
    Public Function GetCompanyDetailsForStudEnroll(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)
        Dim txtItems As New List(Of String)
        If Not String.IsNullOrEmpty(contextKey) Then
            Dim sSQL As String = String.Empty
            Dim dt As New DataTable
            sSQL = "..spGetCompanyNameDetailsStudEnroll__c @PrefixText='" & prefixText & "', @ContextKey='" & contextKey & "'"
            dt = DataAction.GetDataTable(sSQL)
            Dim dbValues As String
            For Each row As DataRow In dt.Rows
                Dim sCompany As String = row("Name").ToString()
                Dim lastLetter As Char = sCompany(sCompany.Length - 1)
                Dim sCompanyName As String
                If lastLetter = "/" Then
                    sCompanyName = sCompany.Substring(0, sCompany.Length - 1)
                    If sCompanyName(sCompanyName.Length - 1) = "/" Then
                        sCompanyName = sCompanyName.Substring(0, sCompany.Length - 2)
                    End If
                Else
                    sCompanyName = sCompany.Substring(0, sCompany.Length)
                End If

                dbValues = sCompanyName
                dbValues = dbValues
                '  dbValues = dbValues.ToLower()
                Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(sCompanyName.ToString, row("ID").ToString())
                txtItems.Add(item)


                'txtItems.Add(dbValues)
            Next
        End If
        Return txtItems
    End Function
 <WebMethod()>
<System.Web.Script.Services.ScriptMethod()> _
    Public Function GetCompanyDetailsForEEApp(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)
        Dim sSQL As String = String.Empty
        Dim dt As New DataTable
        Dim oParams(1) As IDataParameter
        oParams(0) = Me.DataAction.GetDataParameter("@PrefixText", prefixText)
        oParams(1) = Me.DataAction.GetDataParameter("@CheckRTO", contextKey)
        sSQL = Database & "..spGetCompanyNameDetailsForEEApp__c"
        dt = DataAction.GetDataTableParametrized(sSQL, CommandType.StoredProcedure, oParams)

        Dim txtItems As New List(Of String)
        Dim dbValues As String
        For Each row As DataRow In dt.Rows
            Dim sCompany As String = row("Name").ToString()
            Dim lastLetter As Char = sCompany(sCompany.Length - 1)
            Dim sCompanyName As String
            If lastLetter = "/" Then
                sCompanyName = sCompany.Substring(0, sCompany.Length - 1)
                If sCompanyName(sCompanyName.Length - 1) = "/" Then
                    sCompanyName = sCompanyName.Substring(0, sCompany.Length - 2)
                End If
            Else
                sCompanyName = sCompany.Substring(0, sCompany.Length)
            End If

            dbValues = sCompanyName
            dbValues = dbValues
            '  dbValues = dbValues.ToLower()
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(sCompanyName.ToString, row("ID").ToString())
            txtItems.Add(item)


            'txtItems.Add(dbValues)
        Next
        Return txtItems
    End Function

End Class
