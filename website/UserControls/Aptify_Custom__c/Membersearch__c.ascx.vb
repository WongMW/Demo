Imports Aptify.Framework.BusinessLogic.GenericEntity
Imports Aptify.Framework.Application
Imports Aptify.Framework.BusinessLogic
Imports Aptify.Framework.DataServices
Imports Aptify.Framework.AttributeManagement
Imports System.Data

Namespace Aptify.Framework.Web.eBusiness
    Partial Class Membersearch__c
        Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then
                fillYears()
		resultGrid(0, "", 1, 10)
            End If
        End Sub


        Private Sub fillYears()
            Try
                Dim sYearsList As String = Database & "..spGetYearsList__c"
                Dim dt As DataTable = DataAction.GetDataTable(sYearsList)
                If Not dt Is Nothing Then
                    ddlyear.DataSource = dt
                    ddlyear.DataTextField = "Year"
                    ddlyear.DataValueField = "Year"
                    ddlyear.DataBind()
                End If

            Catch ex As Exception

            End Try
           
        End Sub

        Protected Sub btnsearchmember_Click(sender As Object, e As System.EventArgs) Handles btnsearchmember.Click
            Try
                If ddlyear.SelectedItem.Value <> "---Select---" Then
                    resultGrid(Convert.ToInt32(ddlyear.SelectedItem.Value), "", 1, 10)
                End If

            Catch ex As Exception

            End Try
        End Sub

        Protected Sub btnsearchbyname_Click(sender As Object, e As System.EventArgs) Handles btnsearchbyname.Click
            Try
                resultGrid(0, txtlastname.Text, 1, 10)
            Catch ex As Exception

            End Try
        End Sub

        Private Sub resultGrid(year As Integer, lastname As String, pg As Integer, pgSize As Integer)
            Try
                Dim sProdctsList As String = Database & "..spGetMemberPesons__c"
                Dim params(3) As System.Data.IDataParameter
                params(0) = DataAction.GetDataParameter("@year", SqlDbType.Int, year)
                params(1) = DataAction.GetDataParameter("@surname", SqlDbType.VarChar, lastname)
                params(2) = DataAction.GetDataParameter("@pg", SqlDbType.Int, pg)
                params(3) = DataAction.GetDataParameter("@pgSize", SqlDbType.Int, pgSize)
                Dim dt As DataTable
                dt = DataAction.GetDataTableParametrized(sProdctsList, CommandType.StoredProcedure, params)
                If Not dt Is Nothing Then
                    If dt.Rows.Count > 0 Then
                        rptSearchDetails.DataSource = dt
                        rptSearchDetails.DataBind()
                        pageno(Convert.ToInt32(dt.Rows(0)(0).ToString()))
 		    Else
                        lnkBtnNext.Visible = False
                        lnkBtnPrev.Visible = False
                    End If
		Else
                    lnkBtnNext.Visible = False
                    lnkBtnPrev.Visible = False
                End If
            Catch ex As Exception

            End Try
        End Sub

        Public Sub pageno(totItems As Integer)
            ' Calculate total numbers of pages
            Dim pgCount As Integer = totItems / 5 + totItems Mod 5

            ' Display Next>> button
            If pgCount - 1 > Convert.ToInt16(txtHidden.Value) Then
                lnkBtnNext.Visible = True
            Else
                lnkBtnNext.Visible = False
            End If

            ' Display <<Prev button
            If (Convert.ToInt16(txtHidden.Value)) > 1 Then
                lnkBtnPrev.Visible = True
            Else
                lnkBtnPrev.Visible = False
            End If
        End Sub

        Protected Sub lnkBtnPrev_Click(sender As Object, e As System.EventArgs) Handles lnkBtnPrev.Click
            txtHidden.Value = Convert.ToString(Convert.ToInt16(txtHidden.Value) - 1)
            If ddlyear.SelectedItem.Value <> "---Select---" Then
                resultGrid(Convert.ToInt32(ddlyear.SelectedItem.Value), "", Convert.ToInt16(txtHidden.Value), 10)
            End If

        End Sub

        Protected Sub lnkBtnNext_Click(sender As Object, e As System.EventArgs) Handles lnkBtnNext.Click
            txtHidden.Value = Convert.ToString(Convert.ToInt16(txtHidden.Value) + 1)
            If ddlyear.SelectedItem.Value <> "---Select---" Then
                resultGrid(Convert.ToInt32(ddlyear.SelectedItem.Value), "", Convert.ToInt16(txtHidden.Value), 10)
            End If

        End Sub
    End Class
End Namespace