﻿''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'Developer                  Date Created/Modified               Summary
'Asmita                     11/12/2013                          Display and save the topic codes
'Rajesh Kardile             04/10/2014                          Change inline Query to Store procedure spGetPersonTopicCodes__c
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Option Explicit On
Option Strict On
Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework
Imports Aptify.Framework.DataServices
Imports System.Data
Imports Telerik.Web.UI


Namespace Aptify.Framework.Web.eBusiness.CustomerService
    Partial Class TopicCodeControl
        Inherits BaseUserControlAdvanced

        Protected Const ATTRIBUTE_CONTORL_DEFAULT_NAME As String = "TopicCodeControl"

        Protected Overrides Sub SetProperties()
            If String.IsNullOrEmpty(Me.ID) Then Me.ID = ATTRIBUTE_CONTORL_DEFAULT_NAME
            'call base method to set parent properties
            MyBase.SetProperties()
        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Me.Page.MaintainScrollPositionOnPostBack = True
            SetProperties()
            If Not IsPostBack Then
                LoadTree()

            End If
        End Sub
        Public Overridable Sub LoadTree()
            Try
               
                'Rajesh K - 04/10/2014
                '************************** Start ************************************

                'Dim sSQL As String = "SELECT ID,ParentID,Name,WebEnabled, Description FROM " + AptifyApplication.GetEntityBaseDatabase("Topic Codes") + ".." + AptifyApplication.GetEntityBaseView("Topic Codes") + " WHERE ISNULL(ParentID,-1)=-1 AND ( Status='Active' AND WebEnabled = 'Global' OR ( WebEnabled = 'By Service' AND (ID IN (SELECT TopicCodeID FROM " + AptifyApplication.GetEntityBaseDatabase("TopicCodeEntities") + ".." + AptifyApplication.GetEntityBaseView("TopicCodeEntities") + " WHERE EntityID_Name = 'Persons' AND Status = 'Active' AND WebEnabled =1)))) ORDER By Name ASC"
                'Dim odt As DataTable = DataAction.GetDataTable(sSQL, IAptifyDataAction.DSLCacheSetting.BypassCache)
                Dim sPersonTopicCode As String = Database & "..spGetPersonTopicCodes__c"
                Dim odt As DataTable = DataAction.GetDataTable(sPersonTopicCode, CommandType.StoredProcedure)
                '************************************* End ********************************************
                If odt IsNot Nothing AndAlso odt.Rows.Count > 0 Then
                    For Each rw As DataRow In odt.Rows
                        Dim oNode As RadTreeNode = New RadTreeNode()
                        oNode.NodeTemplate = trvTopicCodes.NodeTemplate
                        oNode.Value = rw("ID").ToString
                        trvTopicCodes.Nodes.Add(oNode)
                        Dim lbl As Label = CType(oNode.Controls.Item(1), Label)
                        lbl.Text = rw("Name").ToString
                        SetNodeCheckedStatus(oNode)
                        oNode = CustomNode(oNode)
                        LoadChildrenNodes(oNode)
                    Next
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Overridable Sub LoadChildrenNodes(ByVal oNode As RadTreeNode)
            Dim sSQL As String = "SELECT ID,ParentID,Name,WebEnabled FROM " + AptifyApplication.GetEntityBaseDatabase("Topic Codes") + ".." + AptifyApplication.GetEntityBaseView("Topic Codes") + " WHERE ( Status='Active' AND WebEnabled = 'Global' OR ( WebEnabled = 'By Service' AND (ID IN (SELECT TopicCodeID FROM " + AptifyApplication.GetEntityBaseDatabase("TopicCodeEntities") + ".." + AptifyApplication.GetEntityBaseView("TopicCodeEntities") + " WHERE EntityID_Name = 'Persons' AND Status = 'Active' AND WebEnabled =1)))) and ParentID='" + oNode.Value + "'" + "order by Name"
            Dim oDA As DataAction = New DataAction(AptifyApplication.UserCredentials), iCount As Integer = -1, bIsOther As Boolean = False, iOtherIndex As Integer = -1
            Dim odt As DataTable = oDA.GetDataTable(sSQL)
            If odt IsNot Nothing AndAlso odt.Rows.Count > 0 Then

                For Each rw As DataRow In odt.Rows
                    iCount = iCount + 1                     '10/21/2012
                    If rw("Name").ToString.ToUpper() = "OTHER" Then
                        iOtherIndex = iCount
                        bIsOther = True
                    Else
                        Dim oChildNode As RadTreeNode = New RadTreeNode()
                        oNode.NodeTemplate = trvTopicCodes.NodeTemplate
                        oChildNode.Value = rw("ID").ToString
                        oNode.Nodes.Add(oChildNode)
                        Dim lbl As Label = CType(oChildNode.Controls.Item(1), Label)
                        lbl.Text = rw("Name").ToString
                        SetNodeCheckedStatus(oChildNode)
                        oChildNode = CustomNode(oChildNode)
                        LoadChildrenNodes(oChildNode)
                    End If
                Next
                '10/21/2012
                If bIsOther = True Then
                    LoadOtherChilderenNodes(odt, iOtherIndex, oNode)
                End If
            End If
        End Sub
        '10/21/2012
        Private Sub LoadOtherChilderenNodes(ByVal dt As DataTable, ByVal iOtherIndex As Integer, ByVal oNode As RadTreeNode)
            Try
                Dim oChildNode As RadTreeNode = New RadTreeNode()
                oNode.NodeTemplate = trvTopicCodes.NodeTemplate
                oChildNode.Value = dt.Rows(iOtherIndex)("ID").ToString
                oNode.Nodes.Add(oChildNode)
                Dim lbl As Label = CType(oChildNode.Controls.Item(1), Label)
                lbl.Text = dt.Rows(iOtherIndex)("Name").ToString
                SetNodeCheckedStatus(oChildNode)
                oChildNode = CustomNode(oChildNode)
                LoadChildrenNodes(oChildNode)
            Catch ex As Exception
                ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub
        Protected Overridable Function SetNodeCheckedStatus(ByRef oNode As RadTreeNode) As Boolean
            Dim sSQL As String

            If User1.PersonID > 0 Then
                Dim oDA As DataAction = New DataAction(AptifyApplication.UserCredentials)
                sSQL = "SELECT Status from  " + AptifyApplication.GetEntityBaseDatabase("Topic Code Links") + ".." + AptifyApplication.GetEntityBaseView("Topic Code Links") + " WHERE TopicCodeID='" + oNode.Value + "' and EntityID='" + Convert.ToString(AptifyApplication.GetEntityID("Persons")) + "' and RecordID='" + Convert.ToString(User1.PersonID) + "'"
                Dim Status As Object = ""
                Status = oDA.ExecuteScalar(sSQL)

                If Not String.IsNullOrEmpty(CStr(Status)) Then
                    If CStr(Status) = "Active" Then
                        oNode.Checked = True
                        Return True
                    Else
                        oNode.Checked = False
                        Return False
                    End If
                End If

            End If
            oNode.Checked = False
            Return False
        End Function


        Protected Overridable Function CustomNode(ByRef oNode As RadTreeNode) As RadTreeNode
            Try
                Dim ddl As DropDownList = CType(oNode.Controls.Item(3), DropDownList)
                Dim txt As TextBox = CType(oNode.Controls.Item(5), TextBox)
                Dim sSQL As String = "SELECT ID,ValueType from  " + AptifyApplication.GetEntityBaseDatabase("Topic Codes") + ".." + AptifyApplication.GetEntityBaseView("Topic Codes") + " WHERE ID='" + oNode.Value + "'"
                Dim oDA As DataAction = New DataAction(AptifyApplication.UserCredentials)
                Dim odt As DataTable = oDA.GetDataTable(sSQL)
                If odt IsNot Nothing AndAlso odt.Rows.Count > 0 Then
                    If odt.Rows(0)("ValueType") IsNot Nothing Then
                        If ddl IsNot Nothing Then
                            ddl.Items.Clear()
                            ddl.Visible = False
                        End If
                        If txt IsNot Nothing Then
                            txt.Text = String.Empty
                            txt.Visible = False
                        End If
                        If oNode.Checked = True Then
                            Select Case Convert.ToString(odt.Rows(0)("ValueType"))
                                Case "Yes/No"
                                    Dim sVal As String = GetSavedValue(oNode.Value, Convert.ToString(AptifyApplication.GetEntityID("Persons")), Convert.ToString(User1.PersonID))
                                    If sVal = "Yes" Then
                                        oNode.Checked = True
                                    ElseIf sVal = "No" Then
                                        oNode.Checked = False
                                    End If
                                Case "Multiple Choice"
                                    If ddl IsNot Nothing Then
                                        RenderDropDownloadList(oNode.Value, ddl)
                                        Dim sVal As String = GetSavedValue(oNode.Value, Convert.ToString(AptifyApplication.GetEntityID("Persons")), Convert.ToString(User1.PersonID))
                                        If sVal IsNot Nothing And Not String.IsNullOrEmpty(sVal) AndAlso ddl.Items.Contains(New ListItem(sVal)) Then
                                            ddl.SelectedValue = sVal
                                        End If
                                        ddl.Visible = True
                                    End If
                                Case "Numeric"
                                    If txt IsNot Nothing Then
                                        txt.Text = GetSavedValue(oNode.Value, Convert.ToString(AptifyApplication.GetEntityID("Persons")), Convert.ToString(User1.PersonID))
                                        txt.Visible = True
                                    End If
                                Case "Text"
                                    If txt IsNot Nothing Then
                                        txt.Text = GetSavedValue(oNode.Value, Convert.ToString(AptifyApplication.GetEntityID("Persons")), Convert.ToString(User1.PersonID))
                                        txt.Visible = True
                                    End If
                            End Select
                        End If
                    End If
                End If
            Catch ex As Exception
                ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
            Return oNode
        End Function

        Protected Overridable Function GetInputValue(ByRef oNode As RadTreeNode) As String
            Dim sSQL As String = "SELECT ID,ValueType from  " + AptifyApplication.GetEntityBaseDatabase("Topic Codes") + ".." + AptifyApplication.GetEntityBaseView("Topic Codes") + " WHERE ID='" + oNode.Value + "'"
            Dim oDA As DataAction = New DataAction(AptifyApplication.UserCredentials)
            Dim odt As DataTable = oDA.GetDataTable(sSQL)
            If odt IsNot Nothing And odt.Rows.Count > 0 Then
                Select Case odt.Rows(0)("ValueType").ToString
                    Case "Yes/No"
                        If oNode.Checked = True Then
                            Return "Yes"
                        Else
                            Return "No"
                        End If
                    Case "Multiple Choice"
                        ' ddl exists
                        Dim ddl As DropDownList = CType(oNode.Controls.Item(3), DropDownList)
                        Return ddl.SelectedValue
                    Case "Numeric"
                        Dim txt As TextBox = CType(oNode.Controls.Item(5), TextBox)
                        Return txt.Text
                    Case "Text"
                        Dim txt As TextBox = CType(oNode.Controls.Item(5), TextBox)
                        Return txt.Text
                End Select
            End If
            Return String.Empty
        End Function

        Protected Overridable Function RenderDropDownloadList(ByVal sTopicCodeID As String, ByRef ddl As DropDownList) As Boolean
            Dim sSQL As String = "SELECT ID,Value from  " + AptifyApplication.GetEntityBaseDatabase("TopicCodePossibleValues") + ".." + AptifyApplication.GetEntityBaseView("TopicCodePossibleValues") + " WHERE TopicCodeID='" + sTopicCodeID + "'"
            Dim oDA As DataAction = New DataAction(AptifyApplication.UserCredentials)
            Dim odt As DataTable = oDA.GetDataTable(sSQL)
            If odt IsNot Nothing And odt.Rows.Count > 0 Then
                For Each rw As DataRow In odt.Rows
                    ddl.Items.Add(rw("Value").ToString)
                Next
            End If
            Return True
        End Function

        Protected Overridable Function GetSavedValue(ByVal sTopicCodeID As String, ByVal sEntityID As String, ByVal sRecordID As String) As String
            Dim sSQL As String = "SELECT ID,Value from  " + AptifyApplication.GetEntityBaseDatabase("Topic Code Links") + ".." + AptifyApplication.GetEntityBaseView("Topic Code Links") + " WHERE TopicCodeID='" + sTopicCodeID + "' and EntityID='" + sEntityID + "' and RecordID='" + sRecordID + "'"
            Dim oDA As DataAction = New DataAction(AptifyApplication.UserCredentials)
            Dim odt As DataTable = oDA.GetDataTable(sSQL)
            If odt IsNot Nothing AndAlso odt.Rows.Count > 0 Then
                If odt.Rows(0)("Value") IsNot Nothing AndAlso odt.Rows(0)("Value") IsNot DBNull.Value Then
                    Return odt.Rows(0)("Value").ToString
                End If
            End If
            Return String.Empty
        End Function

#Region "Save Methods"
        Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
            Try
                'Navin Prasad Issue 6448
                Dim sErrorString As String = ""
                If RecursiveUpdateTopicCodes(trvTopicCodes.Nodes, sErrorString) Then
                    lblDescription.Text = "Topic Codes Updated Successfully!"
                    lblDescription.ForeColor = System.Drawing.Color.Blue
                    lblDescription.Visible = False
                Else
                    lblDescription.Text = "Error updating topic codes." + Environment.NewLine + sErrorString
                    lblDescription.ForeColor = System.Drawing.Color.Red
                    lblDescription.Visible = False
                End If
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try

        End Sub

        Public Overridable Function RecursiveUpdateTopicCodes(ByVal Nodes As Telerik.Web.UI.RadTreeNodeCollection, ByRef ErrorString As String, Optional ByRef lPersonID As Long = -1) As Boolean
            Dim i As Integer
            Dim sSQL As String
            Dim iCount As Long
            Dim bOK As Boolean
            Try
                bOK = True
                For i = 0 To Nodes.Count - 1
                    With Nodes(i)
                        sSQL = GetNodeCheckSQL(CLng(.Value), lPersonID)
                        iCount = CInt(DataAction.ExecuteScalar(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
                        If (.Checked And iCount = 0) Then
                            ' This executes only if the node is checked and the db
                            ' does not have this turned on
                            If User1.PersonID > 0 Then
                                bOK = bOK And AddTopicCodeLink(CLng(.Value), GetInputValue(Nodes(i)), ErrorString)
                            Else
                                bOK = bOK And AddTopicCodeLink(CLng(.Value), GetInputValue(Nodes(i)), ErrorString, lPersonID)
                            End If

                        ElseIf (iCount <> 0 AndAlso (Not .Checked Or .Checked)) Then
                            ' if the node is not checked or checked and the DB does have the topic code 
                            ' turned on - update an existing topic code link.
                            bOK = bOK And UpdateTopicCodeLink(CLng(.Value), .Checked, GetInputValue(Nodes(i)), ErrorString)
                        End If
                    End With
                    bOK = bOK And RecursiveUpdateTopicCodes(Nodes(i).Nodes, ErrorString, lPersonID)
                Next
                Return bOK
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Function

        Private Function GetNodeCheckSQL(ByVal lTopicCodeID As Long, Optional ByRef lPersonID As Long = -1) As String
            Return "SELECT COUNT(*) FROM " & _
                   GetNodeBaseSQL(lTopicCodeID, lPersonID)
        End Function
        Private Function GetNodeLinkSQL(ByVal lTopicCodeID As Long, Optional ByRef lPersonID As Long = -1) As String
            Return "SELECT ID FROM " & _
                   GetNodeBaseSQL(lTopicCodeID, lPersonID)
        End Function
        Protected Overridable Function GetNodeBaseSQL(ByVal lTopicCodeID As Long, Optional ByRef lPersonID As Long = -1) As String
            Dim sSQL As String

            If User1.PersonID > 0 Then
                sSQL = Database & ".." & _
                  AptifyApplication.GetEntityBaseView("Topic Code Links") + " WHERE TopicCodeID=" & lTopicCodeID & _
                  " AND EntityID=(SELECT ID FROM " & _
                  Database & ".." & _
                  AptifyApplication.GetEntityBaseView("Entities") + " WHERE Name='Persons') " & _
                  " AND RecordID=" & User1.PersonID

                Return sSQL
            Else
                sSQL = Database & ".." & _
                  AptifyApplication.GetEntityBaseView("Topic Code Links") + " WHERE TopicCodeID=" & lTopicCodeID & _
                  " AND EntityID=(SELECT ID FROM " & _
                  Database & ".." & _
                  AptifyApplication.GetEntityBaseView("Entities") + " WHERE Name='Persons') " & _
                  " AND RecordID=" & lPersonID

                Return sSQL
            End If

        End Function

        Protected Overridable Function AddTopicCodeLink(ByVal TopicCodeID As Long, ByVal Value As String, ByRef ErrorString As String, Optional ByRef lPersonID As Long = -1) As Boolean
            Dim oLink As AptifyGenericEntityBase
            Try
                oLink = AptifyApplication.GetEntityObject("Topic Code Links", -1)
                oLink.SetValue("TopicCodeID", TopicCodeID)

                If User1.PersonID > 0 Then
                    oLink.SetValue("RecordID", User1.PersonID)
                Else
                    oLink.SetValue("RecordID", lPersonID)
                End If

                oLink.SetValue("EntityID", AptifyApplication.GetEntityID("Persons"))
                oLink.SetValue("Status", "Active")
                oLink.SetValue("Value", Value)
                oLink.SetValue("DateAdded", Date.Today)
                Return oLink.Save(ErrorString)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Function
        Protected Overridable Function UpdateTopicCodeLink(ByVal TopicCodeID As Long, _
                                             ByVal Active As Boolean, ByVal Value As String, ByRef ErrorString As String) As Boolean
            Dim sSQL As String
            Dim lLinkID As Long
            Dim oLink As AptifyGenericEntityBase
            Try
                sSQL = GetNodeLinkSQL(TopicCodeID)
                lLinkID = CLng(DataAction.ExecuteScalar(sSQL, DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache))
                oLink = AptifyApplication.GetEntityObject("Topic Code Links", lLinkID)
                oLink.SetValue("Value", Value)
                If Active Then
                    oLink.SetValue("Status", "Active")
                Else
                    oLink.SetValue("Status", "Inactive")
                End If
                Return oLink.Save(ErrorString)
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Function

#End Region

        Private Sub cmdClearAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClearAll.Click
            ' clear all topic codes from this tree
            RecursiveSetCheckBoxes(trvTopicCodes.Nodes, False)
        End Sub
        Private Sub cmdCheckAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCheckAll.Click
            ' check off all topic codes from this tree
            RecursiveSetCheckBoxes(trvTopicCodes.Nodes, True)
        End Sub

        Protected Overridable Sub RecursiveSetCheckBoxes(ByRef oNodes As Telerik.Web.UI.RadTreeNodeCollection, ByVal bChecked As Boolean)
            Dim l As Integer
            Try
                For l = 0 To oNodes.Count - 1
                    oNodes(l).Checked = bChecked
                    CustomNode(oNodes(l))
                    RecursiveSetCheckBoxes(oNodes(l).Nodes, bChecked)
                Next
            Catch ex As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
            End Try
        End Sub

        Protected Sub trvTopicCodes_NodeCheck(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadTreeNodeEventArgs) Handles trvTopicCodes.NodeCheck
            Me.Page.MaintainScrollPositionOnPostBack = True
            If User1.PersonID > 0 Then
                CustomNode(e.Node)
            Else
                ' e.Node.Checked = True
            End If
        End Sub

    End Class


End Namespace
