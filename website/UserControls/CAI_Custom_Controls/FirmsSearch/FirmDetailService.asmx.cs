using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Configuration;
using System.Web.Script.Services;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.FirmsSearch
{

    /// <summary>
    /// Summary description for FirmDetailService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class FirmDetailService : System.Web.Services.WebService
    {
        //readonly sql connection
        private readonly string dbConnection = "Server=" + ConfigurationManager.AppSettings["AptifyDBServer"] + ";Database=" + ConfigurationManager.AppSettings["AptifyEntitiesDB"] + ";Trusted_Connection=True";

        [WebMethod]
        public void GetMemberDetails()
        {
            List<MemberDetail> mdetails = new List<MemberDetail>();
            // using sql connection
            using (SqlConnection con = new SqlConnection(dbConnection))
            {
                SqlCommand cmd = new SqlCommand("spGetMemberDirectory__cai", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    MemberDetail mdet = new MemberDetail();
                    mdet.MId = rdr["Mid"].ToString();
                    mdet.MemberName = rdr["MemberName"].ToString();
                    mdet.FId = rdr["FId"].ToString();
                    mdet.FirmName = rdr["FirmName"].ToString();
                    //Begin:#19895 also update the SP from UAT to LIVE
                    mdet.AdmittanceDate = rdr["AdmittanceDate"].ToString();
                    //End:#19895
                    mdet.City = rdr["City"].ToString();
                    mdet.Country = rdr["Country"].ToString();
                    mdetails.Add(mdet);
                }
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = 2147483644;
            Context.Response.Write(js.Serialize(mdetails));

        }

        [WebMethod]
        public void GetFirmDetails()
        {
            List<FirmDetail> fdetails = new List<FirmDetail>();
            using (SqlConnection con = new SqlConnection(dbConnection))
            {
                SqlCommand cmd = new SqlCommand("spGetFirmDirectoryLookupTableDaily__cai", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                // testing 
                //SqlCommand cmd = new SqlCommand("select * from FirmDirectoryLookupTableDaily__cai", con);
                //con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    FirmDetail fdet = new FirmDetail();
                    fdet.FId = rdr["FId"].ToString();
                    fdet.FirmName = rdr["FirmName"].ToString();
                    fdet.AUD = rdr["AUD"].ToString();
                    fdet.DPB = rdr["DPB"].ToString();
                    fdet.IB = rdr["IB"].ToString();
                    fdet.City = rdr["City"].ToString();
                    fdet.Country = rdr["Country"].ToString();
                    fdetails.Add(fdet);
                }
            }
             JavaScriptSerializer js = new JavaScriptSerializer();
             js.MaxJsonLength = 2147483644;
             Context.Response.Write(js.Serialize(fdetails));
        }

        
        [WebMethod]
        public void GetMemberPractice()
        {
            List<MembersInPractice> mpractice = new List<MembersInPractice>();
            using (SqlConnection con = new SqlConnection(dbConnection))
            {
                SqlCommand cmd = new SqlCommand("SpGetMembersInPractice__cai", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    MembersInPractice mprac = new MembersInPractice(); ;
                    mprac.MId = rdr["MId"].ToString();
                    mprac.MemberName = rdr["MemberName"].ToString();
                    mprac.FId = rdr["FId"].ToString();
                    mprac.FirmName = rdr["FirmName"].ToString();
                    mprac.PC = rdr["PC"].ToString();
                    mprac.IPC = rdr["IPC"].ToString();
                    mprac.ILC = rdr["ILC"].ToString();
                    mpractice.Add(mprac);
                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = 2147483644;
            Context.Response.Write(js.Serialize(mpractice));

        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public  List<StaffMember> searchnames( )
        {
            var listFirstLast = new List<StaffMember>();
            using (SqlConnection con = new SqlConnection(dbConnection))
            {
                //SqlCommand cmd = new SqlCommand("SpGetMembersInPractice__cai", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT  top 5 (VP.FirstName + ' ' + VP.LastName) AS FirstLast FROM dbo.vwpersons VP WHERE companyid=19738 AND FirstLast like 'a%' AND ISNULL(CONVERT(VARCHAR(12),VP.DuesPaidThru,103),'N/A') <> 'N/A' ORDER BY vp.FirstLast";
                    cmd.Connection = con;
                    con.Open();
                 //   cmd.Parameters.AddWithValue("@SearchEmpName", empname);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr != null)
                        while (rdr.Read())
                        {
                            var meml = new StaffMember();
                            {
                                meml.FirstLast = rdr["FirstLast"].ToString();
                            }

                            listFirstLast.Add(meml);
                        }
                    }
                    return listFirstLast;
                }
            }
            //JavaScriptSerializer js = new JavaScriptSerializer();
            //js.MaxJsonLength = 2147483644;
            //Context.Response.Write(js.Serialize(smn));

        

        [WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]

        public void searchnamesjson(string empname)
        {
            List<StaffMember> listFirstLast = new List<StaffMember>();
            using (SqlConnection con = new SqlConnection(dbConnection))
            {
                //SqlCommand cmd = new SqlCommand("SpGetMembersInPractice__cai", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT  top 5 (VP.FirstName + ' ' + VP.LastName) AS FirstLast FROM dbo.vwpersons VP WHERE companyid=19738 AND FirstLast like 'a%' AND ISNULL(CONVERT(VARCHAR(12),VP.DuesPaidThru,103),'N/A') <> 'N/A' ORDER BY vp.FirstLast";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchEmpName", empname);


                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())

                    {
                        StaffMember sm = new StaffMember();
                        sm.FirstLast =rdr["FirstLast"].ToString();
                        listFirstLast.Add(sm);
                    }
                   // return listFirstLast;

                }
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = 2147483644;
            Context.Response.Clear();
            Context.Response.Flush();
            Context.Response.Write(js.Serialize(listFirstLast));

        }

        [WebMethod]
        public List<string> showcountry(string slookup)
        {
            List<string> lstCountries = new List<string>();



            using (SqlConnection con = new SqlConnection(dbConnection))
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT  (VP.FirstName + ' ' + VP.LastName) AS firstlast FROM dbo.vwpersons VP WHERE companyid=19738 AND FirstLast like '%'+ @sname +'%' AND ISNULL(CONVERT(VARCHAR(12),VP.DuesPaidThru,103),'N/A') <> 'N/A' ORDER BY vp.firstlast";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@sname", slookup);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr != null)
                        while (rdr.Read())
                        {
                           lstCountries.Add(rdr["firstlast"].ToString());
                                                      
                        }
                }
                return lstCountries;
            }
        }
    
    


    public class FirmDetail
        {
            public string FId { get; set; }
            public string FirmName { get; set; }
            public string AUD { get; set; }
            public string DPB { get; set; }
            public string IB { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
        }
        public class MemberDetail
        {
            public string MId { get; set; }
            public string MemberName { get; set; }
            public string FId { get; set; }
            public string FirmName { get; set; }
			//Begin:#19895
            public string AdmittanceDate { get; set; }
			//End:#19895
            public string City { get; set; }
            public string Country { get; set; }
        }
        public class MembersInPractice
        {
            public string MId { get; set; }
            public string MemberName { get; set; }
            public string FId { get; set; }
            public string FirmName { get; set; }
            public string PC { get; set; }
            public string IPC { get; set; }
            public string ILC { get; set; }
        }


        public class StaffMember
        {
    
            public string FirstLast { get; set; }
 
        }
    }
}
