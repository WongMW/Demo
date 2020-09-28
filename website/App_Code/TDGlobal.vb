Imports System
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Net
Imports System.Web
Imports System.Xml
Imports Microsoft.VisualBasic
Imports ServiceStack
Imports SitefinityWebApp.UserControls.CAI_Custom_Controls.FirmsSearch
Imports SitefinityWebApp.UserControls.CAI_Custom_Controls.FirmsSearch.EnhancedFirmSearchResults
Imports Telerik.Sitefinity.SitemapGenerator.Data

Namespace SitefinityWebApp.App_Code
    Public Class TDGlobal
        Inherits Aptify.Framework.Web.eBusiness.EBusinessGlobal

        Public Overrides Function GetVaryByCustomString(context As HttpContext, custom As String) As String
            'Added this by after dsicussion with sitefinity team
            If (custom.Equals("cms", StringComparison.CurrentCultureIgnoreCase)) Then
                If (IsNothing(HttpContext.Current) And IsNothing(HttpContext.Current.Request)) Then
                    Return HttpContext.Current.Request.Path
                End If
            End If
            Return MyBase.GetVaryByCustomString(context, custom)
        End Function

        Public Overrides Sub Application_Error(sender As Object, e As EventArgs)
            MyBase.Application_Error(sender, e)
            '            // DONE IN WEB.CONFIG
            '            //Exception ex = Server.GetLastError();
            '            //if (ex is HttpException)
            '            //{
            '            //    HttpException httpEx = ex as HttpException;
            '            //    if (httpEx.ErrorCode == 403 || httpEx.Source.StartsWith("Telerik.Cms"))
            '            //    {
            '            //        Response.Redirect("~/Sitefinity/nopermissions.aspx");
            '            //        Server.ClearError();
            '            //    }
            '            //}
            '            //else if (ex is Telerik.Security.SecurityApplicationException)
            '            //{
            '            //    Response.Redirect("~/Sitefinity/nopermissions.aspx");
            '            //    Server.ClearError();
            '            //}
        End Sub

        Public Overrides Sub Session_Start(sender As Object, e As EventArgs)
            MyBase.Session_Start(sender, e)
            Dim SessionId As String = String.Empty

            SessionId = Session.SessionID
            ' Trick to fix sesion ID issues that are not accessed
        End Sub

        Public Overrides Sub Session_End(sender As Object, e As EventArgs)
            MyBase.Session_End(sender, e)
            '            // Code that runs when a session ends. 
            '            // Note: The Session_End event is raised only when the sessionstate mode
            '            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            '            // or SQLServer, the event is not raised.
            'Session.Clear()
        End Sub

        Public Overrides Sub Application_Start(sender As Object, e As EventArgs)
            MyBase.Application_Start(sender, e)
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            AddHandler Telerik.Sitefinity.Abstractions.Bootstrapper.Initialized, AddressOf Me.Bootstrapper_Initialized
        End Sub

        Sub Bootstrapper_Initialized(sender As Object, e As Telerik.Sitefinity.Data.ExecutedEventArgs)
            If (e.CommandName.Equals("Bootstrapped")) Then
                Telerik.Sitefinity.Services.EventHub.Subscribe(Of Telerik.Sitefinity.SitemapGenerator.Abstractions.Events.ISitemapGeneratorBeforeWriting)(AddressOf Before_Writing)
            End If
        End Sub

        Sub Before_Writing(evt As Telerik.Sitefinity.SitemapGenerator.Abstractions.Events.ISitemapGeneratorBeforeWriting)
            Dim entries = evt.Entries.GetResponseDto().ToList()

            ' getting site url to be included in the sitemap
            Dim sysConfig = Telerik.Sitefinity.Configuration.Config.Get(Of Telerik.Sitefinity.Services.SystemConfig)()

            Dim urlHost = String.Empty
            If (sysConfig IsNot Nothing And sysConfig.SiteUrlSettings IsNot Nothing And Not String.Empty.Equals(sysConfig.SiteUrlSettings.Host)) Then
                urlHost = "https://" & sysConfig.SiteUrlSettings.Host
            End If

            ' getting site url to be included in the sitemap
            FillSitemapWithFirmsDirectoryEntries(entries, urlHost)
            FillSitemapWithJobsEntries(entries, urlHost)
            FillSitemapWithProducts(entries, urlHost)
            ' ---------

            ' sets the collection of entries to modified collection
            evt.Entries = entries
        End Sub

        Sub FillSitemapWithProducts(ByRef entries As List(Of SitemapEntry), urlHost As String)
            ' spGetProductsCategory__c - then for each category do below
            ' - SpGetProductForPreferredCurrency__c
            ' - - if ProductURLToUse is specified, then use that instead of default: ~/ProductCatalog/Product.aspx?ID=#
            Dim connectionString = SoftwareDesign.Helper.GetAptifyEntitiesConnectionString()
            Dim categoryIds = New List(Of Integer)()
            Dim con = New SqlConnection(connectionString)

            ' open connection
            con.Open()

            Dim cmd = New SqlCommand("spGetProductsCategory__c", con)
            cmd.CommandType = System.Data.CommandType.StoredProcedure
            cmd.Connection = con

            Dim rdr = cmd.ExecuteReader()
            While (rdr.Read())
                categoryIds.Add(Integer.Parse(rdr("ID").ToString()))
            End While

            rdr.Close()

            ' lets go through each of the category and retrieve its products
            For Each categoryId As Integer In categoryIds
                cmd = New SqlCommand("SpGetProductForPreferredCurrency__c", con)
                cmd.CommandType = System.Data.CommandType.StoredProcedure
                cmd.Connection = con
                cmd.Parameters.AddWithValue("@CurrencyType", "Euro")
                cmd.Parameters.AddWithValue("@ExcludeProductID", "0")
                cmd.Parameters.AddWithValue("@CategoryID", categoryId)
                cmd.Parameters.AddWithValue("@ShowMeetingsLinkToClass", False)

                rdr = cmd.ExecuteReader()
                While (rdr.Read())
                    Dim productURLToUse = rdr("ProductURLToUse").ToString()
                    Dim productId = rdr("ID").ToString()

                    Dim newSitemapEntry As SitemapEntry = New SitemapEntry()
                    newSitemapEntry.Priority = 1
                    newSitemapEntry.Location = urlHost +
                        IIf(String.IsNullOrEmpty(productURLToUse), "/ProductCatalog/Product.aspx", productURLToUse.Replace("~", "")) +
                        "?ID=" + productId

                    entries.Add(newSitemapEntry)
                End While

                rdr.Close()
            Next

            con.Close()
        End Sub

        Sub FillSitemapWithFirmsDirectoryEntries(ByRef entries As List(Of SitemapEntry), urlHost As String)
            ' preparing all entries for the enhanced firms listing
            Dim params As FirmSearchParams = New FirmSearchParams(0)
            params.currentPage = 1
            Dim currIndex = 0
            Dim dt = params.BuildSearchQuery()
            While (dt.Rows.Count > 0 And currIndex < dt.Rows.Count)
                Dim row = dt.Rows(currIndex)
                Dim newSitemapEntry As SitemapEntry = New SitemapEntry()

                Dim obj As EnhancedListingObj = New EnhancedListingObj

                obj.Title = IIf(row("FirmName") IsNot Nothing, row("FirmName").ToString(), String.Empty)
                obj.City = IIf(row("City") IsNot Nothing, row("City").ToString(),
                                IIf(row("County") IsNot Nothing, row("County").ToString(), String.Empty))
                obj.Phone = IIf(dt.Columns.Contains("ContactNo") And row("ContactNo") IsNot Nothing, row("ContactNo").ToString(), String.Empty)
                obj.Url = IIf(row("WebSite") IsNot Nothing, row("WebSite").ToString(), String.Empty)
                obj.ID = IIf(row("FId") IsNot Nothing, row("FId").ToString(), String.Empty)

                newSitemapEntry.Priority = 1
                newSitemapEntry.Location = urlHost + obj.SinglePageUrl
                entries.Add(newSitemapEntry)

                currIndex += 1

                If (dt.Rows.Count <= currIndex) Then
                    currIndex = 0
                    params.currentPage += 1
                    dt = params.BuildSearchQuery()
                End If
            End While
            ' ---------
        End Sub

        Sub FillSitemapWithJobsEntries(ByRef entries As List(Of SitemapEntry), urlHost As String)
            Dim doc As XmlDocument = New XmlDocument()
            doc.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/JSExport.xml"))

            Dim jobUrlTmpl = "/Professional-development/Job-Search/Job-Search-Listing?jobID={0}"

            For Each el As XmlElement In doc.DocumentElement.ChildNodes
                If (el.Name.Equals("Jobs")) Then
                    Dim i = 0
                    For Each e As XmlElement In el.ChildNodes
                        Dim newSitemapEntry As SitemapEntry = New SitemapEntry()

                        Dim formattedJob = e.InnerText
                        Dim replacingText = "\r\n"
                        formattedJob = e.InnerText.Replace(replacingText, "")
                        formattedJob = formattedJob.Replace("#SH", "")
                        formattedJob = formattedJob.Replace("#EH", "")
                        formattedJob = formattedJob.Replace("*SB", "")
                        formattedJob = formattedJob.Replace("*EB", "")
                        formattedJob = formattedJob.Replace("\\SL", " ")
                        formattedJob = formattedJob.Replace("\\EL", " ")

                        newSitemapEntry.Priority = 1
                        newSitemapEntry.Location = urlHost + String.Format(jobUrlTmpl, e.GetAttribute("DBID"))
                        entries.Add(newSitemapEntry)
                    Next
                End If
            Next
        End Sub
    End Class
End Namespace
