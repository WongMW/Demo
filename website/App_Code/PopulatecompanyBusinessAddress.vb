Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Script.Serialization
Imports System.Data
Imports System.Web.Script.Services

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
<ScriptService()> _
Public Class PopulatecompanyBusinessAddress
    Inherits Aptify.Framework.Web.eBusiness.BaseWebService
    <WebMethod()> _
      <ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=False, XmlSerializeString:=False)> _
    Public Function PopulateCompanyBusinessAddress(ByVal Company As String) As String
        Dim js As JavaScriptSerializer = New JavaScriptSerializer()
        Dim sSQL As String = String.Empty
        Dim dt As New DataTable
        Dim param(0) As IDataParameter
        Dim BusinessAddress As New List(Of BusinessAddress)()
        Dim strCompany As String() = Company.Split("\")
        Dim sCompanyName As String = strCompany(0)
        If Not String.IsNullOrEmpty(Company.Trim()) Then
            sSQL = Database & "..spGetBusinessAddress__c"
            param(0) = DataAction.GetDataParameter("@Company", SqlDbType.NVarChar, sCompanyName)
            dt = DataAction.GetDataTableParametrized(sSQL, Data.CommandType.StoredProcedure, param)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    Dim Address As New List(Of BusinessAddress)() From { _
                    New BusinessAddress() With { _
                        .AddressLine1 = Convert.ToString(row("AddressLine1")),
                        .AddressLine2 = Convert.ToString(row("AddressLine2")),
                        .AddressLine3 = Convert.ToString(row("AddressLine3")),
                        .AddressLine4 = Convert.ToString(row("AddressLine4")),
                        .City = Convert.ToString(row("City")),
                        .State = Convert.ToString(row("State")),
                        .ZipCode = Convert.ToString(row("ZipCode")),
                        .CountryCodeID = Convert.ToString(row("CountryCodeID")),
                        .MainAreaCode = Convert.ToString(row("MainAreaCode")),
                        .MainPhone = Convert.ToString(row("MainPhone")),
                        .MainFaxAreaCode = Convert.ToString(row("MainFaxAreaCode")),
                        .MainFaxNumber = Convert.ToString(row("MainFaxNumber"))}}
                    BusinessAddress = Address
                Next
            End If
        End If
        Return js.Serialize(BusinessAddress)
    End Function

<WebMethod()> _
      <ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=False, XmlSerializeString:=False)> _
    Public Function PopulateCompanyStreetAddress(ByVal CompanyID As String) As String
        Dim js As JavaScriptSerializer = New JavaScriptSerializer()
        Dim sSQL As String = String.Empty
        Dim dt As New DataTable
        Dim param(0) As IDataParameter
        Dim BusinessAddress As New List(Of BusinessAddress)()
        'Dim strCompany As String() = CompanyID.Split("\")
        Dim sCompanyName As Integer = CompanyID
        If IsNumeric(CompanyID) Then
            sSQL = Database & "..spGetCompanyStreetAddress__c"
            param(0) = DataAction.GetDataParameter("@CompanyID", SqlDbType.BigInt, sCompanyName)
            dt = DataAction.GetDataTableParametrized(sSQL, Data.CommandType.StoredProcedure, param)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    Dim Address As New List(Of BusinessAddress)() From { _
                    New BusinessAddress() With { _
                        .AddressLine1 = Convert.ToString(row("AddressLine1")),
                        .AddressLine2 = Convert.ToString(row("AddressLine2")),
                        .AddressLine3 = Convert.ToString(row("AddressLine3")),
                        .AddressLine4 = Convert.ToString(row("AddressLine4")),
                        .City = Convert.ToString(row("City")),
                        .State = Convert.ToString(row("State")),
                        .ZipCode = Convert.ToString(row("ZipCode")),
                        .CountryCodeID = Convert.ToString(row("CountryCodeID")),
                        .MainAreaCode = Convert.ToString(row("MainAreaCode")),
                        .MainPhone = Convert.ToString(row("MainPhone")),
                        .MainFaxAreaCode = Convert.ToString(row("MainFaxAreaCode")),
                        .MainFaxNumber = Convert.ToString(row("MainFaxNumber")),
 			.County = Convert.ToString(row("County"))}}
                    BusinessAddress = Address
                Next
            End If
        End If
        Return js.Serialize(BusinessAddress)
    End Function



End Class
Public Class BusinessAddress
    Public AddressLine1 As String
    Public AddressLine2 As String
    Public AddressLine3 As String
    Public AddressLine4 As String
    Public CountryCodeID As String
    Public City As String
    Public State As String
    Public ZipCode As String
    Public MainAreaCode As String
    Public MainPhone As String
    Public MainFaxAreaCode As String
    Public MainFaxNumber As String
 Public County As String
End Class