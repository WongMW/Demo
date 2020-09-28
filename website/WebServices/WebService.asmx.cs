using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Data;
using Newtonsoft.Json;
using static SitefinityWebApp.UserControls.CAI_Custom_Controls.FirmsSearch.EnhancedFirmSearchResults;
using System.Web.Http;
using SitefinityWebApp.UserControls.CAI_Custom_Controls.TrainingVacancies;
using System.Threading.Tasks;

namespace SitefinityWebApp.WebServices
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        //readonly sql connection
        private readonly string dbConnection = SoftwareDesign.Helper.GetAptifyEntitiesConnectionString();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public List<CompanyList> GetCountryCompanyName(string countryId, string city, string firmName, List<String> sectors, List<String> specialisms, List<String> authorisations)
        {
            FirmSearchParams prms = new FirmSearchParams(countryId,
                city,
                firmName,
                sectors != null && sectors.Count > 0 ? String.Join(",", sectors) : "",
                specialisms != null && specialisms.Count > 0 ? String.Join(",", specialisms) : "",
                authorisations != null && authorisations.Count > 0 ? String.Join(",", authorisations) : "",
                String.Empty
            );

            List<CompanyList> comdet = new List<CompanyList>();

            var dt = prms.BuildNameFIdSelectQuery();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    CompanyList cdet = new CompanyList();
                    cdet.Id = Convert.ToInt32(row["FId"].ToString());
                    cdet.Name = row["FirmName"].ToString();
                    comdet.Add(cdet);
                }

                // lets sort by firmname
                comdet.Sort((a, b) => a.Name.CompareTo(b.Name));
            }

            return comdet;
        }

        [WebMethod]
        public string EnhancedListingFirmSearchCount(String countryId, String city, String firmName, List<String> sectors, List<String> specialisms, List<String> authorisations)
        {
            String output = String.Empty;

            FirmSearchParams prms = new FirmSearchParams(countryId, 
                city, 
                firmName, 
                sectors != null && sectors.Count > 0 ? String.Join(",", sectors) : "",
                specialisms != null && specialisms.Count > 0 ? String.Join(",", specialisms) : "",
                authorisations != null && authorisations.Count > 0 ? String.Join(",", authorisations) : "",
                String.Empty
            );

            if(!prms.HasOneValue())
            {
                return "-1";
            }

            var dt = prms.BuildCountQuery();
            if(dt != null && dt.Rows.Count > 0)
            {
                int totalRecords = 0;
                foreach(DataRow row in dt.Rows)
                {
                    totalRecords = int.Parse(row["TotalRecords"].ToString());
                }

                output = totalRecords.ToString();
            }

            return output;
        }

        [WebMethod]
        public List<string> GetMemberNames(string slookup, string cid)
        {
            List<string> lstnames = new List<string>();
            using (SqlConnection con = new SqlConnection(dbConnection))
            {

                    SqlCommand cmd = new SqlCommand("spGetMemberNamesByCompanyID__cai", con);
                    // cmd.CommandText = "SELECT  (VP.FirstName + ' ' + VP.LastName) AS firstlast FROM dbo.vwpersons VP WHERE companyid=@cid AND FirstLast like '%'+ @sname +'%' AND ISNULL(CONVERT(VARCHAR(12),VP.DuesPaidThru,103),'N/A') <> 'N/A' ORDER BY vp.firstlast";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@sname", slookup);
                    cmd.Parameters.AddWithValue("@cid", cid);
                   SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr != null)
                        while (rdr.Read())
                        {
                            lstnames.Add(rdr["firstlast"].ToString());
                        }
                
                return lstnames;
            }
        }

        // return json filter 
        [WebMethod(Description = "Gets Country names ")]
        // [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public   string GetResultset()
        {
            DataTable cdt = new DataTable();
            using (SqlConnection con = new SqlConnection(dbConnection))
            {
                SqlCommand cmd = new SqlCommand("spGetCountryByName__cai", con);
                cmd.CommandType = CommandType.StoredProcedure;
               // cmd.Parameters.AddWithValue("@search", search);
                cmd.Connection = con;
                con.Open();


                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                {
                    dataAdapter.SelectCommand = cmd;
                    dataAdapter.Fill(cdt);
                }

            }
            return JsonConvert.SerializeObject(cdt);
        }


        //test mmethod
        [WebMethod]
        public List<CountryList> GetCountryNames()
        {
            List<CountryList> lst = new List<CountryList>();
            lst.Add(new CountryList() { CountryId = 1, CountryName = "India" });
            lst.Add(new CountryList() { CountryId = 2, CountryName = "Nepal" });
            lst.Add(new CountryList() { CountryId = 3, CountryName = "America" });
            return lst;

        }
        [WebMethod]
        public List<CountryList> GetCountryNamesById()
        {
            //List<CountryList> lst = new List<CountryList>();
            //lst.Add(new CountryList() { CountryId = 1, CountryName = "India" });
            //lst.Add(new CountryList() { CountryId = 2, CountryName = "Nepal" });
            //lst.Add(new CountryList() { CountryId = 3, CountryName = "America" });
            //return lst;
            List<CountryList> clst = new List<CountryList>();
            using (SqlConnection con = new SqlConnection(dbConnection))
            {

                SqlCommand cmd = new SqlCommand("spGetCountryByName__cai", con);             
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                con.Open();
                //cmd.Parameters.AddWithValue("@sname", slookup);
                //cmd.Parameters.AddWithValue("@cid", cid);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr != null)
                    while (rdr.Read())
                    {
                        //clst.Add(new CountryList() { CountryId = rdr["firstlast"].ToString(), CountryName = "India" });

                        //lstnames.Add(rdr["firstlast"].ToString());
                    }

                return clst;
            }
        }


        //GetCountryCompany   webservice profile.ascx #3 #20072
        [WebMethod]
        public List<CountryList> GetCountryDetails()
        {
            List<CountryList> cdetails = new List<CountryList>();
            // using sql connection
            using (SqlConnection con = new SqlConnection(dbConnection))
            {
                SqlCommand cmd = new SqlCommand("spGetCountryByName__cai", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    CountryList cdet = new CountryList();
                    cdet.CountryId = Convert.ToInt32(rdr["CountryId"].ToString());
                    cdet.CountryName = rdr["CountryName"].ToString();
                    cdetails.Add(cdet);
                }
            }

            //JavaScriptSerializer js = new JavaScriptSerializer();
            //js.MaxJsonLength = 2147483644;
            //Context.Response.Write(js.Serialize(cdetails));
            return cdetails;

        }

        [WebMethod]
        public void GetCountryByName()
        {
            List<CountryList> cdetails = new List<CountryList>();
            // using sql connection
            using (SqlConnection con = new SqlConnection(dbConnection))
            {
                SqlCommand cmd = new SqlCommand("spGetCountryByName__cai", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    CountryList cdet = new CountryList();
                    cdet.CountryId = Convert.ToInt32(rdr["CountryId"].ToString());
                    cdet.CountryName = rdr["CountryName"].ToString();
                    cdetails.Add(cdet);
                }
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = 2147483644;
            Context.Response.Write(js.Serialize(cdetails));
            // return cdetails;

        }

        //GetCountryCompany   webservice profile.ascx #2 #20072
        [WebMethod]
        public List<CompanyList> GetCountryCompany(string country, string city, string name)
        
        {
            List<CompanyList> comdet = new List<CompanyList>();
            // using sql connection
            using (SqlConnection con = new SqlConnection(dbConnection))
            {
                //if (string.IsNullOrEmpty(name))
                //    {

                //    SqlCommand cmd1 = new SqlCommand("spGetCompanyDetailsByCountryDefault__cai", con);
                //    cmd1.CommandType = CommandType.StoredProcedure;
                //    con.Open();
                //    cmd1.Parameters.AddWithValue("@country", country);
                //    SqlDataReader rdr1 = cmd1.ExecuteReader();
                //    while (rdr1.Read())
                //    {
                //        CompanyList comdet = new CompanyList();
                //        comdet.Id = Convert.ToInt32(rdr1["Id"].ToString());
                //        comdet.Name = rdr1["Name"].ToString();
                //        comlst.Add(comdet);
                //    }


                //}
                //else
                //{

                SqlCommand cmd2 = new SqlCommand("spGetCompanyDetailsByCountryCountyName__cai", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd2.Parameters.AddWithValue("@country", country);
                cmd2.Parameters.AddWithValue("@city", city);
                cmd2.Parameters.AddWithValue("@name", name);
                SqlDataReader rdr2 = cmd2.ExecuteReader();
                while (rdr2.Read())
                {
                    CompanyList cdet = new CompanyList();
                    cdet.Id = Convert.ToInt32(rdr2["Id"].ToString());
                    cdet.Name = rdr2["Name"].ToString();
                    comdet.Add(cdet);
                }

                return comdet;
            }


            }



        //Get company name webservice profile.ascx #1 #20072
        [WebMethod]
        public List<CompanyList> GetCompany(string country)    
        {

            List<CompanyList> clst = new List<CompanyList>();
            // using sql connection
            using (SqlConnection con = new SqlConnection(dbConnection))
            {
                SqlCommand cmd1 = new SqlCommand("spGetCompanyDetailsByCountryDefault__cai", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd1.Parameters.AddWithValue("@country", country);
                SqlDataReader rdr1 = cmd1.ExecuteReader();
                while (rdr1.Read())
                {
                    CompanyList comdet = new CompanyList();
                    comdet.Id = Convert.ToInt32(rdr1["Id"].ToString());
                    comdet.Name = rdr1["Name"].ToString();
                    clst.Add(comdet);
                }

                return clst;
            }

        }


        [WebMethod]
        public List<CompanyList> GetCompanyByCountryCity(string country,string city)
        {
 
            List<CompanyList> clst = new List<CompanyList>();
            // using sql connection
            using (SqlConnection con = new SqlConnection(dbConnection))
            {
                SqlCommand cmd1 = new SqlCommand("spGetCompanyDetailsByCountryCity__cai", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd1.Parameters.AddWithValue("@country", country);
                cmd1.Parameters.AddWithValue("@city", city);
                SqlDataReader rdr1 = cmd1.ExecuteReader();
                while (rdr1.Read())
                {
                    CompanyList comdet = new CompanyList();
                    comdet.Id = Convert.ToInt32(rdr1["Id"].ToString());
                    comdet.Name = rdr1["Name"].ToString();
                    clst.Add(comdet);
                }

                return clst;
            }

        }
        // Filetr countryCity names by country #4 #20072
        [WebMethod]   
        public List<CityList> GetCountyCityByCountry(string country, string city)
        {

            List<CityList> citylst = new List<CityList>();
            // using sql connection
            using (SqlConnection con = new SqlConnection(dbConnection))
            {
                SqlCommand cmd = new SqlCommand("spGetCityCountyByCountry__cai", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@country", country);
                cmd.Parameters.AddWithValue("@city", city);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    CityList citydet = new CityList();
                    citydet.Name = rdr["Name"].ToString();
                    citylst.Add(citydet);
                }

                return citylst;
            }

        }

		// Filetr memebr names by country city EnhancedListing Member directory list result
		[WebMethod]
		public List<MemDirList> GetCountyCityByCountryMemebersDirectory(string country, string city, string name)
		{

			List<MemDirList> Memlst = new List<MemDirList>();
            // using sql connection
            using (SqlConnection con = new SqlConnection(dbConnection))
            {
                if ((string.IsNullOrEmpty(country) || (String.Compare(country, "Country") == 0)))
                { country = null; }
                if (string.IsNullOrEmpty(city))
                { city = null; }
                if (string.IsNullOrEmpty(name))
                { name = null; }

                try
                {
                    SqlCommand cmd = new SqlCommand("spGetELMDByCountryCityMemberName__cai", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@country", country);
                    cmd.Parameters.AddWithValue("@city", city);
                    cmd.Parameters.AddWithValue("@mname", name);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        MemDirList mdet = new MemDirList();
                        mdet.mid = Convert.ToInt32(rdr["mid"].ToString());
                        mdet.mname = rdr["mname"].ToString();
                        Memlst.Add(mdet);
                    }
                    return Memlst;
                }
                catch (Exception e)
                {

                    if (e.Source != null)
                        Console.WriteLine("IOException source: {0}", e.Source);
                }

                return null;
            }

		}

		// Filetr city names by country city EnhancedListing Member directory
		[WebMethod]
		public List<MemListCity> GetCountyCityByCountryForMemebersDirectory(string country, string city)
		{

			List<MemListCity> MLcity = new List<MemListCity>();
            // using sql connection
            using (SqlConnection con = new SqlConnection(dbConnection))
            {
                string name = null;
                if (string.IsNullOrEmpty(country))
                { country = null; }
                if (string.IsNullOrEmpty(city))
                { city = null; }
                //if (string.IsNullOrEmpty(name))
                //{ name = null; }

                try
                {
                    SqlCommand cmd = new SqlCommand("spGetELMDByCountryCityMemberName__cai", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@country", country);
                    cmd.Parameters.AddWithValue("@city", city);
                    cmd.Parameters.AddWithValue("@mname", name);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        MemListCity mcitydet = new MemListCity();
                        mcitydet.Id = Convert.ToInt32(rdr["mid"].ToString());
                        mcitydet.Name = rdr["city"].ToString();
                        MLcity.Add(mcitydet);
                    }
                    return MLcity;
                }
                catch (Exception e)
                {

                    if (e.Source != null)
                        Console.WriteLine("IOException source: {0}", e.Source);
                }

                return null;
            }

		}

		// Filetr city names by country city EnhancedListing Member directory
		[WebMethod]
		public void GetELMemebersDirectory(string country, string city, string mname)
		{

			List<MemList>mlist = new List<MemList>();
            // using sql connection
            using (SqlConnection con = new SqlConnection(dbConnection))
            {

                if ((string.IsNullOrEmpty(country) || (String.Compare(country, "Country") == 0)))
                { country = null; }
                if (string.IsNullOrEmpty(city))
                { city = null; }
                if (string.IsNullOrEmpty(mname))
                { mname = null; }
                var ad = string.Empty;

                try
                {
                    SqlCommand cmd = new SqlCommand("spGetELMDByCountryCityMemberName__cai", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@country", country);
                    cmd.Parameters.AddWithValue("@city", city);
                    cmd.Parameters.AddWithValue("@mname", mname);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        MemList ml = new MemList();
                        ml.mid = Convert.ToInt32(rdr["mid"].ToString());
                        ml.mname = rdr["mname"].ToString();

                        if (String.IsNullOrEmpty(rdr["fid"].ToString()))
                        { ml.fid = 0; }
                        else
                        { ml.fid = Convert.ToInt32(rdr["fid"].ToString()); }


                        if (String.IsNullOrEmpty(rdr["fname"].ToString()))
                        { ml.fname = "--"; }
                        else
                        { ml.fname = rdr["fname"].ToString(); }
                        if (String.IsNullOrEmpty(rdr["city"].ToString()))
                        { ml.city = "--"; }
                        else
                        { ml.city = rdr["city"].ToString(); }
                        ml.country = rdr["country"].ToString();
                        if (String.IsNullOrEmpty(rdr["admittancedate"].ToString()))
                        { ml.adate = "--"; }
                        else

                        {
                            ad = string.Empty;
                            ad = rdr["admittancedate"].ToString();
                            ml.adate = ad.Substring(0, 10);
                        }
                        mlist.Add(ml);
                    }
                    //return MLcity;
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    //js.MaxJsonLength = 2147483644;
                    Context.Response.Write(js.Serialize(mlist));
                }
                catch (Exception e)
                {

                    if (e.Source != null)
                        Console.WriteLine("IOException source: {0}", e.Source);
                }


            }

		}


		// Filetr countryCity names by country #4 #20072
		[WebMethod]
		public List<CityList> GetCountyCityByCountryMembersInPracticeDirectory(string country, string city)
		{

			List<CityList> citylst = new List<CityList>();
            // using sql connection
            using (SqlConnection con = new SqlConnection(dbConnection))
            {
                SqlCommand cmd = new SqlCommand("spGetCityCountyByCountryMembersInPracticeDirectoryLookupTableDaily__cai", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@country", country);
                cmd.Parameters.AddWithValue("@city", city);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    CityList citydet = new CityList();
                    citydet.Name = rdr["Name"].ToString();
                    citylst.Add(citydet);
                }

                return citylst;
            }

		}


		[WebMethod]
		public async Task<List<MemInPracDirList>> GetELMIPDirectory(string country, string city, string mn , string authorisations)
		{

			string ilc = string.Empty;
			string ipc = string.Empty;
			string pc = string.Empty;

			List<MemInPracDirList> miplist = new List<MemInPracDirList>();
            // using sql connection
            using (SqlConnection con = new SqlConnection(dbConnection))
            {

                if ((string.IsNullOrEmpty(country) || (String.Compare(country, "Country") == 0)))
                { country = null; }
                if (string.IsNullOrEmpty(city))
                { city = null; }
                if (string.IsNullOrEmpty(mn))
                { mn = null; }

                if (string.IsNullOrEmpty(authorisations))
                {
                    ilc = null;
                    ipc = null;
                    pc = null;
                }
                else
                {
                    if (authorisations.Contains("ILC"))
                    { ilc = "yes"; }
                    else
                    { ilc = null; }
                    if (authorisations.Contains("IPC"))
                    { ipc = "yes"; }
                    else
                    { ipc = null; }
                    if (authorisations.Contains("PC"))
                    { pc = "yes"; }
                    else
                    { pc = null; }
                }


                try
                {
                    SqlCommand cmd = new SqlCommand("spGetMembersInPracticeDirectoryLookupTableDailyByMemberName__cai", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@country", country);
                    cmd.Parameters.AddWithValue("@city", city);
                    cmd.Parameters.AddWithValue("@mn", mn);
                    cmd.Parameters.AddWithValue("@ilc", ilc);
                    cmd.Parameters.AddWithValue("@ipc", ipc);
                    cmd.Parameters.AddWithValue("@pc", pc);


                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        MemInPracDirList mipl = new MemInPracDirList();
                        mipl.mipid = Convert.ToInt32(rdr["mid"].ToString());
                        mipl.mipname = rdr["mname"].ToString();


                        miplist.Add(mipl);
                    }
                    return miplist;
                    //JavaScriptSerializer js = new JavaScriptSerializer();
                    //js.MaxJsonLength = 2147483644;
                    //Context.Response.Write(js.Serialize(miplist));
                }
                catch (Exception e)
                {

                    if (e.Source != null)
                        Console.WriteLine("IOException source: {0}", e.Source);
                }
                return null;


            }

		}

        [HttpPost]
		[WebMethod]
		public void GetELMIPDirectoryTable(string country, string city, string mn, string authorisations)
        {
            try
            {
                string ilc = string.Empty;
                string ipc = string.Empty;
                string pc = string.Empty;

                List<MemInPracList> miplist = new List<MemInPracList>();
                // using sql connection
                using (SqlConnection con = new SqlConnection(dbConnection))
                {

                    if ((string.IsNullOrEmpty(country) || (String.Compare(country, "Country") == 0)))
                    { country = null; }
                    if (string.IsNullOrEmpty(city))
                    { city = null; }
                    if (string.IsNullOrEmpty(mn))
                    { mn = null; }
                    if (string.IsNullOrEmpty(authorisations))
                    {
                        ilc = null;
                        ipc = null;
                        pc = null;
                    }
                    else
                    {
                        if (authorisations.Contains("ILC"))
                        { ilc = "yes"; }
                        else
                        { ilc = null; }
                        if (authorisations.Contains("IPC"))
                        { ipc = "yes"; }
                        else
                        { ipc = null; }
                        if (authorisations.Contains("PC"))
                        { pc = "yes"; }
                        else
                        { pc = null; }
                    }


                    try
                    {
                        SqlCommand cmd = new SqlCommand("spGetMembersInPracticeDirectoryLookupTableDaily__cai", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.AddWithValue("@country", country);
                        cmd.Parameters.AddWithValue("@city", city);
                        cmd.Parameters.AddWithValue("@mn", mn);
                        cmd.Parameters.AddWithValue("@ilc", ilc);
                        cmd.Parameters.AddWithValue("@ipc", ipc);
                        cmd.Parameters.AddWithValue("@pc", pc);



                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            MemInPracList mipl = new MemInPracList();
                            mipl.mid = Convert.ToInt32(rdr["mid"].ToString());
                            mipl.mname = rdr["mname"].ToString();
                            mipl.fname = rdr["fname"].ToString();

                            if (rdr["ILC"].ToString() == "Yes")
                            {
                                mipl.ilc = "ILC";
                            }
                            else
                            { mipl.ilc = "--"; }


                            if (rdr["IPC"].ToString() == "Yes")
                            {
                                mipl.ipc = "IPC";
                            }
                            else
                            { mipl.ipc = "--"; }

                            if (rdr["PC"].ToString() == "Yes")
                            {
                                mipl.pc = "PC";
                            }
                            else
                            { mipl.pc = "--"; }
                            //   mipl.ilc = rdr["ILC"].ToString();
                            //mipl.ipc = rdr["IPC"].ToString();
                            //mipl.pc = rdr["PC"].ToString();
                            if (String.IsNullOrEmpty(rdr["city"].ToString()))
                            {
                                mipl.city = rdr["county"].ToString();
                            }
                            else
                            { mipl.city = rdr["city"].ToString(); }
                            if (String.IsNullOrEmpty(rdr["county"].ToString()))
                            {
                                mipl.city = rdr["city"].ToString();
                            }
                            mipl.country = rdr["country"].ToString();
                            miplist.Add(mipl);
                        }

                        JavaScriptSerializer js = new JavaScriptSerializer();
                        js.MaxJsonLength = 2147483644;
                        Context.Response.Write(js.Serialize(miplist));
                    }
                    catch (Exception e)
                    {

                        if (e.Source != null)
                            Console.WriteLine("IOException source: {0}", e.Source);
                    }
                }
            }
            catch (Exception e)
            {
                    Console.WriteLine("{0}", e.Message);
            }
		}

		// Webservice for ThirdPartyToken
		[WebMethod]
		public void ValidateToken(String token, String vendor_key)
		{
			Dictionary<string, object> response = new Dictionary<string, object>();

			var statusKey = "Status";
			var successResponse = "OK";
			var failureResponse = "AUTH_FAIL";
			var isValid = false;
			var dbToken = new Dictionary<string, object>();

			var cleanDaysStr = ConfigurationManager.AppSettings["thirdPartyTokenApiTokenCleanInterval"];
			if (String.IsNullOrEmpty(cleanDaysStr))
			{
				cleanDaysStr = "30";
			}

			int cleanDays = 30;
			int.TryParse(cleanDaysStr, out cleanDays);

			// stored procedures
			// spDeleteThirdPartyAPITokens(@ID)
			// spUpdateThirdPartyAPITokens(@ID)
			// spCreateThirdPartyAPITokens(@ID-out, @Token(guid), @ThirdPartySecretKey, @PersonId, @DateCreated, @ExpiresAt)
			// spVerifyThirdPartyAPITokenByTokenAndKey(@token, @key)
			// -----

			// checking if token is GUID
			Guid _token = Guid.Empty;
			Guid.TryParse(token, out _token);

			if (!Guid.Empty.Equals(_token))
			{
				using (SqlConnection con = new SqlConnection(dbConnection))
				{
					SqlCommand cmd = new SqlCommand("spVerifyThirdPartyAPITokenByTokenAndKey__cai", con);
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Connection = con;
					con.Open();
					cmd.Parameters.AddWithValue("@token", _token);
					cmd.Parameters.AddWithValue("@key", vendor_key);
					SqlDataReader rdr = cmd.ExecuteReader();
					if (rdr != null)
					{
						while (!isValid && rdr.Read())
						{
							var isValidStr = rdr["IsValid"].ToString();
							isValid |= "1".Equals(isValidStr);
							if (isValid)
							{
								dbToken.Add("ID", rdr["ID"]);
								dbToken.Add("Token", rdr["Token"]);
								dbToken.Add("ThirdPartySecretKey", rdr["ThirdPartySecretKey"]);
								dbToken.Add("PersonId", rdr["PersonId"]);
								dbToken.Add("DateCreated", rdr["DateCreated"]);
								dbToken.Add("ExpiresAt", rdr["ExpiresAt"]);
							}
						}

						if (!rdr.IsClosed)
						{
							rdr.Close();
						}
					}

					// lets delete all expired tokens
					cmd = new SqlCommand("spDeleteExpiredThirdPartyAPIToken__cai", con);
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@CleanDays", cleanDays);
					cmd.Connection = con;
					cmd.ExecuteNonQuery();
				}
			}

			if (dbToken.ContainsKey("ExpiresAt"))
			{
				DateTime expiresAt = (DateTime)dbToken["ExpiresAt"];
				var expiresIn = (int)(expiresAt - DateTime.Now).TotalSeconds;
				if (expiresIn > 0)
				{
					response.Add("ExpiresIn", (int)(expiresAt - DateTime.Now).TotalSeconds);
				}
				else
				{
					isValid = false;
				}
			}

			response.Add(statusKey, isValid ? successResponse : failureResponse);

			HttpContext.Current.Response.Clear();
			HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
			HttpContext.Current.Response.Write(new JavaScriptSerializer().Serialize(response));
			HttpContext.Current.Response.Flush();
			HttpContext.Current.Response.End();
		}

        // Training Vacancies
        [WebMethod]
        public async Task<SearchTrainingVacancyResponse> SearchTrainingVacancies(string location, string vacancyType, string trainingType,int pageSize = 20, int page = 0)
        {
            SearchTrainingVacancyResponse response = new SearchTrainingVacancyResponse();
            response.page = page;
            response.pageSize = pageSize;
            List<TrainingVacancy> trainingVacancies = new List<TrainingVacancy>();
            string filter = string.Empty;

            if (!string.IsNullOrEmpty(location))
            {
                filter += " AND ( JobTown like '%" + location + "%' or JobCounty like '%" + location + "%') ";
            }

            if (!string.IsNullOrEmpty(vacancyType))
            {
                filter += " AND ( ";
                string[] strs = vacancyType.Split(',');
                for (int i = 0; i < strs.Length; i++)
                    filter += i == 0 ? " VacancyType like '%" + strs[i] + "%'" : " OR VacancyType like '%" + strs[i] + "%' ";
                filter += " ) ";
            }

            if (!string.IsNullOrEmpty(trainingType))
            {
                filter += " AND ( ";
                string[] strs = trainingType.Split(',');
                for (int i = 0; i < strs.Length; i++)
                    filter += i == 0 ? " TrainInType like '%" + strs[i] + "%'" : " OR TrainInType like '%" + strs[i] + "%' ";
                filter += " ) ";
            }


            //  " AND ( JobTown like '%" + location + "%' or JobCounty like '%" + location +"%') AND (TrainInType like '%%') AND (VacancyType like '%%' OR VacancyType like '%%') ";
            // N'( JobTown like ''%%'' or JobCounty like ''%%'') AND (TrainInType like ''%%'') AND (VacancyType like ''%Training%'' OR VacancyType like ''%%'')'
            // using sql connection

            SqlConnection con = new SqlConnection(dbConnection);
            {
                SqlCommand cmd = new SqlCommand("spEnhancedTrainingVacanciesSearchCount__cai", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@filter", filter);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    response.count = int.Parse(rdr[0].ToString());
                }
                con.Close();
            }

            con = new SqlConnection(dbConnection);
            {
                SqlCommand cmd = new SqlCommand("spEnhancedTrainingVacanciesSearch__cai", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@filter", filter);
                cmd.Parameters.AddWithValue("@offset", page * pageSize);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    TrainingVacancy item = new TrainingVacancy();
                    item.TVID = Convert.ToInt32(rdr["ID"].ToString());
                    item.TVCompanyName = rdr["CompanyName"].ToString();
                    item.TVCompanyDescription = rdr["CompanyDescription"].ToString().Replace(Environment.NewLine, "<br />"); ;
                    item.TVJobTitle = rdr["JobTitle"].ToString();
                    item.TVJobTown = rdr["JobTown"].ToString();
                    item.TVJobCounty = rdr["JobCounty"].ToString();
                    item.TVHowToApply = rdr["HowToApply"].ToString().Replace(Environment.NewLine, "<br />"); ;
                    item.TVJobRequirements = rdr["JobRequirements"].ToString();
                    item.TVVacancyType = rdr["VacancyType"].ToString();
                    item.TVTrainInType = rdr["TrainInType"].ToString();
                    item.TVJobSpec = rdr["JobSpec"].ToString().Replace(Environment.NewLine, "<br />"); ;
                    item.TVBenefits = rdr["BenefitsRemuneration"].ToString();
                    item.TVJobDatePosted = ((DateTime)rdr["DatePosted"]).ToShortDateString();
                    item.TVJobClosingDate = ((DateTime)rdr["DateClosing"]).ToShortDateString();
                    item.TVWebSite = rdr["WebSite"].ToString();

                    trainingVacancies.Add(item);
                }
                con.Close();
            }

            //JavaScriptSerializer js = new JavaScriptSerializer();
            //js.MaxJsonLength = 2147483644;
            //Context.Response.Write(js.Serialize(cdetails));
            response.result = trainingVacancies;
            return response;

        }

        [WebMethod]
        public async Task<List<Company>> SearchCompanies(string name)
        {            
            List<Company> companies = new List<Company>();            

            SqlConnection con = new SqlConnection(dbConnection);
            {
                SqlCommand cmd = new SqlCommand("spGetCompaniesSearch__cai", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Name", name);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Company item = new Company();
                    item.id = Convert.ToInt32(rdr["ID"].ToString());
                    item.label = rdr["NAME"].ToString();
                    item.value = rdr["NAME"].ToString();
                    companies.Add(item);
                }
                con.Close();
            }
                       
            return companies;

        }


        //#20072 
        public class CountryList
        {
            public int CountryId { get; set; }
            public string CountryName { get; set; }
        }

        //#20072
        public class CompanyList
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        //#20072
        public class CityList
        {
           
            public string Name { get; set; }
        }

		// member directory

		public class MemList
		{

			public int mid { get; set; }
			public string mname { get; set; }
			public int fid { get; set; }
			public string fname { get; set; }
			public string city { get; set; }
			public string country { get; set; }
			public string adate { get; set; }
		}


		public class MemDirList
		{

			public int mid { get; set; }
			public string mname { get; set; }
		}

		public class MemInPracDirList
		{

			public int mipid { get; set; }
			public string mipname { get; set; }
		}
		public class MemListCity
		{

			public int Id { get; set; }
			public string Name { get; set; }
		}

		public class MemInPracList
		{

			public int mid { get; set; }
			public string mname { get; set; }
			public int fid { get; set; }
			public string fname { get; set; }
			public string city { get; set; }
			public string country { get; set; }
			public string pc { get; set; }
			public string ilc { get; set; }
			public string ipc { get; set; }

		}

        public class SearchTrainingVacancyResponse
        {
            public List<TrainingVacancy> result { get; set; }
            public int count { get; set; }
            public int page { get; set; }
            public int pageSize { get; set; }
        }


        [Serializable]
        public class TrainingVacancy
        {
            public int TVID { get; set; }
            public string TVCompanyName { get; set; }
            public string TVCompanyDescription { get; set; }
            public string TVJobTitle { get; set; }
            public string TVJobTown { get; set; }
            public string TVJobCounty { get; set; }
            public string TVHowToApply { get; set; }
            public string TVJobRequirements { get; set; }
            public string TVVacancyType { get; set; }
            public string TVTrainInType { get; set; }
            public string TVJobSpec { get; set; }
            public string TVBenefits { get; set; }
            public string TVJobClosingDate { get; set; }
            public string TVJobDatePosted { get; set; }
            public string TVWebSite { get; set; }
        }

    }
   }

