Option Explicit On
Option Strict On

Imports System.Data


Namespace SitefinityWebApp
    Public Class ProdNavigatorHelper
        Private ReadOnly _navigateUrlFormatField As String
        Private ReadOnly _viewClassPage As String

        Public Sub New(navigateUrlFormatField As String, viewClassPage As String)
            _navigateUrlFormatField = navigateUrlFormatField
            _viewClassPage = viewClassPage
        End Sub

        Public Function FormatNavigationUrl(dataRow As DataRow) As String

            Dim sUrl As Object
            Dim id As String
            Dim classId As String
            Dim productUrl As String

            id = CStr(dataRow.Item("ID"))
            classId = Nothing
            productUrl = Nothing

            ' Class
            If Not IsDBNull(dataRow.Item("ClassID")) Then
                classId = CStr(dataRow.Item("ClassID"))
            End If

            ' Custom / Meeting
            sUrl = dataRow.Item("ProductURLToUse")
            If Not sUrl Is Nothing AndAlso Len(sUrl) > 0 Then
                productUrl = CStr(sUrl).Trim
            End If

            Return FormatNavigationUrl(id, classId, productUrl)

        End Function

        Public Function FormatNavigationUrl(dataRow As DataRowView) As String

            Dim sUrl As Object
            Dim id As String
            Dim classId As String
            Dim productUrl As String

            id = CStr(dataRow.Item("ID"))
            classId = Nothing
            productUrl = Nothing

            ' Class
            If Not IsDBNull(dataRow.Item("ClassID")) Then
                classId = CStr(dataRow.Item("ClassID"))
            End If

            ' Custom / Meeting
            sUrl = dataRow.Item("ProductURLToUse")
            If Not sUrl Is Nothing AndAlso Len(sUrl) > 0 Then
                productUrl = CStr(sUrl).Trim
            End If

            Return FormatNavigationUrl(id, classId, productUrl)

        End Function

        Public Function FormatNavigationUrl(id As String, classId As String, productUrl As String) As String


            ' Class
            If Not classId Is Nothing AndAlso Len(classId) > 0 Then
                Return Me._viewClassPage & "?ClassID=" & classId
            End If

            ' Custom / Meeting            
            If Not productUrl Is Nothing AndAlso Len(productUrl) > 0 Then
                Return productUrl & "?ID=" & id
            End If

            ' Original
            Dim sUrl As Object
            sUrl = String.Format(Me._navigateUrlFormatField, id)
            Return CStr(sUrl).Trim

        End Function



    End Class



End Namespace
