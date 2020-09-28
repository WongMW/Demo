'************************************** Class Summary ***********************************************
'Developer              Date Created/Modified               Summary
'Rajesh Kardile             11-April-2014               Created for to show advertising rate cards
'****************************************************************************************************

Imports System.Data
Imports Aptify.Framework.BusinessLogic.GenericEntity

Partial Class AdvRateCards__c
    Inherits Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

#Region "Property"
    
#End Region

#Region "Methods"
    ''' <summary>
    ''' Bind Organization in drop down
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindAdvertisingDropDown()
        Try
            Dim preferedCurrencyID As Integer
            If User1.PreferredCurrencyTypeID < 0 Then
                preferedCurrencyID = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Currency Types", "Euro"))
            Else
                preferedCurrencyID = User1.PreferredCurrencyTypeID
            End If

            Dim sSQLQuery As String = Database & "..spGetProductForAdvRateCard__c"
            Dim params(0) As System.Data.IDataParameter
            params(0) = DataAction.GetDataParameter("@CurrenctTypeID", SqlDbType.Int, preferedCurrencyID)
            Dim dtAdvertising As DataTable = DataAction.GetDataTableParametrized(sSQLQuery, CommandType.StoredProcedure, params)
            If Not dtAdvertising Is Nothing AndAlso dtAdvertising.Rows.Count > 0 Then
                cmbAdvertise.DataSource = dtAdvertising
                cmbAdvertise.DataTextField = "Name"
                cmbAdvertise.DataValueField = "ID"
                cmbAdvertise.DataBind()
                cmbAdvertise.Visible = True
                cmbAdvertise.Items.Insert(0, Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProduct")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials))
            Else
                cmbAdvertise.Items.Clear()
                lblAdvertise.Text = Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.AdvRateCards.NoRateCardsToDisplay")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials)
                lblAdvertise.Visible = True
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Bind Organization in drop down
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindRateCardsGrid()
        Try
            Dim preferedCurrencyID As Integer
            If User1.PreferredCurrencyTypeID < 0 Then
                preferedCurrencyID = Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Currency Types", "Euro"))
            Else
                preferedCurrencyID = User1.PreferredCurrencyTypeID
            End If

            Dim sSQLQuery As String = Database & "..spGetAdvertisingRateCards__c"
            Dim params(1) As System.Data.IDataParameter
            params(0) = DataAction.GetDataParameter("@productID", SqlDbType.Int, cmbAdvertise.SelectedValue)
            params(1) = DataAction.GetDataParameter("@CurrencyTypeID", SqlDbType.Int, preferedCurrencyID)
            Dim dsProductRates As DataSet = DataAction.GetDataSetParametrized(sSQLQuery, CommandType.StoredProcedure, params)
            dsProductRates.Tables(0).TableName = "Rates"
            dsProductRates.Tables(1).TableName = "RateCards"
            If Not dsProductRates Is Nothing AndAlso dsProductRates.Tables("RateCards").Rows.Count > 0 Then
                If Not dsProductRates.Tables("RateCards").Rows(0)("Description") Is Nothing AndAlso dsProductRates.Tables("RateCards").Rows(0)("Description") <> "" Then
                    lblDescription.Text = dsProductRates.Tables("RateCards").Rows(0)("Description")
                Else
                    lblDescription.Text = "---"
                End If
                lblStartDate.Text = dsProductRates.Tables("RateCards").Rows(0)("StartDate")
                lblEndDate.Text = dsProductRates.Tables("RateCards").Rows(0)("EndDate")
                If dsProductRates.Tables("RateCards").Rows(0)("AgencyDiscount") > 0 Then
                    lblAgencyDiscount.Text = dsProductRates.Tables("RateCards").Rows(0)("AgencyDiscount")
                Else
                    lblAgencyDiscount.Text = "N\A"
                End If
                pnlDetails.Visible = True
            End If

            If Not dsProductRates Is Nothing AndAlso dsProductRates.Tables("Rates").Rows.Count > 0 Then
                grdAdvertising.Visible = True
                grdAdvertising.DataSource = dsProductRates
                grdAdvertising.DataBind()
                grdAdvertising.AllowPaging = True
            Else
                Dim dtAdvertisingTable As New DataTable
                dtAdvertisingTable.Columns.Add("ID")
                grdAdvertising.DataSource = dtAdvertisingTable
                grdAdvertising.DataBind()
                grdAdvertising.Visible = True
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    Protected Function GetFormattedCurrency(ByVal Container As Object, ByVal sField As String) As String
        Dim sCurrencySymbol As String
        Dim iNumDecimals As Integer
        Dim sCurrencyFormat As String
        Try
            ' get the appropriate currency data from the data row
            sCurrencySymbol = Container.DataItem("CurrencySymbol")
            iNumDecimals = 2 'Container.DataItem("NumDigitsAfterDecimal")

            ' build the string we'll use for formatting the currency
            ' it consists of the symbol followed by 0. and the appropriate
            ' number of decimals needed in the final string
            sCurrencyFormat = sCurrencySymbol.Trim & _
                              "{0:" & "0." & _
                              New String("0"c, iNumDecimals) & "}"

            ' format the string using the currency format created
            Return String.Format(sCurrencyFormat, Container.DataItem(sField))
        Catch ex As Exception
            Try
                ' on failure, at least try and return the
                ' data contents
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
                Return Container.DataItem(sField)
            Catch ex2 As Exception
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex2)
                Return "{ERROR}"
            End Try
        End Try
    End Function

#End Region

#Region "Events"

    ''' <summary>
    ''' Page load for initiation
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            Me.Page.MaintainScrollPositionOnPostBack = True
            If Not IsPostBack Then
                BindAdvertisingDropDown()
                pnlDetails.Visible = False
                grdAdvertising.DataSource = Nothing
                grdAdvertising.Visible = False
            End If
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub

    ''' <summary>
    '''  Advertise product Selected IndexChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub cmbAdvertise_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbAdvertise.SelectedIndexChanged
        If Convert.ToString(cmbAdvertise.SelectedValue).Trim.ToLower <> Convert.ToString(Aptify.Framework.BusinessLogic.CultureUtility.GetCultureLocal(Convert.ToInt32(AptifyApplication.GetEntityRecordIDFromRecordName("Culture Strings", "AptifyEbusiness.SelectProduct")), Convert.ToInt32(DataAction.UserCredentials.CultureID), DataAction.UserCredentials).Trim.ToLower) Then
            BindRateCardsGrid()
        Else
            grdAdvertising.Visible = False
            pnlDetails.Visible = False
        End If
    End Sub

    ''' <summary>
    ''' This method use is to calling Teleric filter 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub grdAdvertising_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdAdvertising.NeedDataSource
        Try
            BindRateCardsGrid()
        Catch ex As Exception
            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex)
        End Try
    End Sub
#End Region
End Class