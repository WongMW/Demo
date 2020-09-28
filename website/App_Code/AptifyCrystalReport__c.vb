Imports Microsoft.VisualBasic
Imports System
Imports System.Reflection
Public Class AptifyCrystalReport__c
    Private _ReportID As Integer
    Private _Param1 As String
    Private _Param2 As String
    Private _Param3 As String
    Private _Param4 As String
    Private _Param5 As String
    Private _Param6 As String
    Private _Param7 As String
    Private _Param8 As String
    Private _Param9 As String
    Private _Param10 As String


    Private _SubParam1 As String
    Private _SubParam2 As String
    Private _SubParam3 As String
    Private _SubParam4 As String
    Private _SubParam5 As String
    Private _SubParam6 As String
    Private _SubParam7 As String
    Private _SubParam8 As String
    Private _SubParam9 As String
    Private _SubParam10 As String


    Public Property ReportID() As Integer
        Get
            Return _ReportID
        End Get
        Set(ByVal Value As Integer)
            _ReportID = Value
        End Set
    End Property

    Public Property Param1() As String
        Get
            Return _Param1
        End Get
        Set(ByVal Value As String)
            _Param1 = Value
        End Set
    End Property


    Public Property Param2() As String
        Get
            Return _Param2
        End Get
        Set(ByVal Value As String)
            _Param2 = Value
        End Set
    End Property

    Public Property Param3() As String
        Get
            Return _Param3
        End Get
        Set(ByVal Value As String)
            _Param3 = Value
        End Set
    End Property

    Public Property Param4() As String
        Get
            Return _Param4
        End Get
        Set(ByVal Value As String)
            _Param4 = Value
        End Set
    End Property

    Public Property Param5() As String
        Get
            Return _Param5
        End Get
        Set(ByVal Value As String)
            _Param5 = Value
        End Set
    End Property

    Public Property Param6() As String
        Get
            Return _Param6
        End Get
        Set(ByVal Value As String)
            _Param6 = Value
        End Set
    End Property


    Public Property Param7() As String
        Get
            Return _Param7
        End Get
        Set(ByVal Value As String)
            _Param7 = Value
        End Set
    End Property

    Public Property Param8() As String
        Get
            Return _Param8
        End Get
        Set(ByVal Value As String)
            _Param8 = Value
        End Set
    End Property

    Public Property Param9() As String
        Get
            Return _Param9
        End Get
        Set(ByVal Value As String)
            _Param9 = Value
        End Set
    End Property

    Public Property Param10() As String
        Get
            Return _Param10
        End Get
        Set(ByVal Value As String)
            _Param10 = Value
        End Set
    End Property




    Public Property SubParam1() As String
        Get
            Return _SubParam1
        End Get
        Set(ByVal Value As String)
            _SubParam1 = Value
        End Set
    End Property


    Public Property SubParam2() As String
        Get
            Return _SubParam2
        End Get
        Set(ByVal Value As String)
            _SubParam2 = Value
        End Set
    End Property

    Public Property SubParam3() As String
        Get
            Return _SubParam3
        End Get
        Set(ByVal Value As String)
            _SubParam3 = Value
        End Set
    End Property

    Public Property SubParam4() As String
        Get
            Return _SubParam4
        End Get
        Set(ByVal Value As String)
            _SubParam4 = Value
        End Set
    End Property

    Public Property SubParam5() As String
        Get
            Return _SubParam5
        End Get
        Set(ByVal Value As String)
            _SubParam5 = Value
        End Set
    End Property

    Public Property SubParam6() As String
        Get
            Return _SubParam6
        End Get
        Set(ByVal Value As String)
            _SubParam6 = Value
        End Set
    End Property


    Public Property SubParam7() As String
        Get
            Return _SubParam7
        End Get
        Set(ByVal Value As String)
            _Param7 = Value
        End Set
    End Property

    Public Property SubParam8() As String
        Get
            Return _SubParam8
        End Get
        Set(ByVal Value As String)
            _SubParam8 = Value
        End Set
    End Property

    Public Property SubParam9() As String
        Get
            Return _SubParam9
        End Get
        Set(ByVal Value As String)
            _SubParam9 = Value
        End Set
    End Property

    Public Property SubParam10() As String
        Get
            Return _SubParam10
        End Get
        Set(ByVal Value As String)
            _SubParam10 = Value
        End Set
    End Property

    Public Function GetProperyInfo(ByVal PropertyName As String) As PropertyInfo
        Try
            Dim myType As Type = GetType(AptifyCrystalReport__c)
            Dim myPropInfo As PropertyInfo = myType.GetProperty(PropertyName)
            Return myPropInfo
        Catch e As NullReferenceException
        End Try
        Return Nothing
    End Function
End Class


