'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date created/modified               comments
'-----------------------------------------------------------------------------------------------------------------------------------------------------
'Sheela Jarali               04 May 2018                      Enrolment submission Successfull message
'Siddharth Kavitake          14 May 2018                      Added code to send notification CNM-8
'Sheela Jarali               24 May 2018                      Send notification only for lecture courses 
'Sheela Jarali               20 Jul 2018                      Replaced create message run record function with create staging entity record. 
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Data
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Web.eBusiness

Partial Class DisplayEnrollSuccessMsg__c
    Inherits BaseUserControlAdvanced

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            lblEnrollSuccess.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.StudentEnrollment.SuccessMsg")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
            'Added by Sheela as part of CNM batch-1 changes
            Dim isLecture As Boolean = False
            isLecture = checkforClassRoom(Convert.ToInt32(Session("OrderID")))
            'Send notification only for lecture courses
            If isLecture Then
                'Siddharth: Added code for CNM-8
                SaveStagingRecord()
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    'Function to create student enrollment staging record (CNM-8)
    Private Sub SaveStagingRecord()
        Dim sErrorMessage As String = String.Empty
        Try

            Dim iMessageTempID As Integer = AptifyApplication.GetEntityAttribute("Orders", "StudentEnrollMessageTempID__c")
            Dim iReportID As Integer = AptifyApplication.GetEntityAttribute("Orders", "StudentEnrollInvoiceReportID__c")
            Dim oStudentEnrollStageGE As Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase
            oStudentEnrollStageGE = Me.AptifyApplication.GetEntityObject("StudentEnrollStaging__c", -1)
            With oStudentEnrollStageGE
                .SetValue("OrderID", Session("OrderID"))
                .SetValue("MessageTemplateID", iMessageTempID)
                .SetValue("ReportID", iReportID)
                If Not .Save(False, sErrorMessage) Then
                    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(New System.Exception(sErrorMessage))
                End If
            End With
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
        'Try

        '    Dim iMessageTempID As Integer = AptifyApplication.GetEntityAttribute("Orders", "StudentEnrollMessageTempID__c")
        '        Dim iReportID As Integer = AptifyApplication.GetEntityAttribute("Orders", "StudentEnrollInvoiceReportID__c")
        '        Dim oMessageTemplate As Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase = AptifyApplication.GetEntityObject("Message Templates", iMessageTempID)
        '        Dim oMessageRunGE As Aptify.Framework.BusinessLogic.GenericEntity.AptifyGenericEntityBase
        '        oMessageRunGE = Me.AptifyApplication.GetEntityObject("Message Runs", -1)
        '    With oMessageRunGE
        '        .SetValue("MessageSystemID", oMessageTemplate.GetValue("DefaultMessageSystemID"))
        '        .SetValue("MessageSourceID", oMessageTemplate.GetValue("DefaultMessageSourceID"))
        '        .SetValue("MessageTemplateID", iMessageTempID)
        '        .SetValue("ApprovalStatus", "Approved")
        '        .SetValue("SourceType", "ID String")
        '        .SetValue("IDString", Session("OrderID"))
        '        .SetValue("ToType", oMessageTemplate.GetValue("ToType"))
        '        .SetValue("ToValue", oMessageTemplate.GetValue("ToValue"))
        '        .SetValue("ReportID", iReportID)
        '        For i As Integer = 0 To oMessageTemplate.SubTypes("MessageTemplateActions").Count - 1
        '            If CBool(oMessageTemplate.SubTypes("MessageTemplateActions").Item(i).GetValue("MessageActionIsActive")) = True Then
        '                With .SubTypes("MessageRunActions").Add()
        '                    .SetValue("MessageActionID", oMessageTemplate.SubTypes("MessageTemplateActions").Item(i).GetValue("MessageActionID"))
        '                    .SetValue("ProcessFlowID", oMessageTemplate.SubTypes("MessageTemplateActions").Item(i).GetValue("ProcessFlowID"))
        '                    .SetValue("Status", "Success")
        '                    .SetValue("WhenComplete", System.DateTime.Now)
        '                    .SetValue("WhoUpdated", AptifyApplication.UserCredentials.GetUserRelatedRecordID("Employees"))
        '                End With
        '            End If
        '        Next
        '        If Not .Save(False, sErrorMessage) Then
        '            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(New System.Exception(sErrorMessage))
        '        End If
        '    End With
        'Catch ex As Exception
        '    Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        'End Try
    End Sub
    'Added by Sheela as part of CNM batch-1 changes
    Private Function checkforClassRoom(OrderId As Integer) As Boolean
        Try
            Dim oParams(0) As IDataParameter
            oParams(0) = Me.DataAction.GetDataParameter("@OrderID", OrderId)
            Dim sSQL As String = Database & "..spCheckLectureCourse__c"
            Dim iLecture As Integer = Convert.ToInt32(DataAction.ExecuteScalarParametrized(sSQL, CommandType.StoredProcedure, oParams))
            If iLecture > 0 Then
                Return True
            Else
                Return False
            End If
            Return False
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            Return False
        End Try
    End Function
End Class
