using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.FirmsSearch
{
    public partial class FirmDirectoryItem : System.Web.UI.UserControl
    {
        private readonly string dbConnection = "Server=" + ConfigurationManager.AppSettings["AptifyDBServer"] + ";Database=" + ConfigurationManager.AppSettings["AptifyEntitiesDB"] + ";Trusted_Connection=True";

        //string ItemID = "";
        public string ItemType = "";
        string itemID = "";
        public string itemType = "";
        public DataTable dt3 = new DataTable();
        
        protected void Page_PreRender(object sender, EventArgs e)
        {
            var scriptManager = ScriptManager.GetCurrent(Page);

            if (scriptManager == null) return;

            scriptManager.Scripts.Add(new ScriptReference { Path = "~/Scripts/InHouse/responsive-calendar.min.js" });
            scriptManager.Scripts.Add(new ScriptReference { Path = "~/Scripts/InHouse/jquery.steps.min.js" });
            scriptManager.Scripts.Add(new ScriptReference { Path = "~/Scripts/InHouse/sweetalert.min.js" });
            scriptManager.Scripts.Add(new ScriptReference { Path = "~/Scripts/InHouse/jquery.pagination.min.js" });
            scriptManager.Scripts.Add(new ScriptReference { Path = "~/Scripts/InHouse/select2.full.min.js" });
            scriptManager.Scripts.Add(new ScriptReference { Path = "~/Scripts/InHouse/ammap.js" });
            scriptManager.Scripts.Add(new ScriptReference { Path = "~/Scripts/InHouse/irelandHigh.js" });
            scriptManager.Scripts.Add(new ScriptReference { Path = "~/Scripts/InHouse/bootstrap.min.js" });
        }

        
           protected void Page_Load(object sender, EventArgs e)
                {

                    if (!this.IsPostBack)
                    {
                        Uri myUri = Request.Url;
                        itemID = HttpContext.Current.Request.QueryString["id"];
                        itemType = HttpContext.Current.Request.QueryString["type"];

                        if ((!string.IsNullOrEmpty(itemID)) && (!string.IsNullOrEmpty(itemType)) && (itemType == "f" || itemType == "m"))
                        {
                            GetData();
                        }

                    }

                }
           public void GetData()
           {
               DataTable dt = new DataTable();
               DataTable dt1 = new DataTable();
               DataTable dt2 = new DataTable();
              // DataTable dt3 = new DataTable();
               using (SqlConnection con = new SqlConnection(dbConnection))
               {
                   con.Open();
                   var command = new SqlCommand("spGetFirmDetailsByFirmId__cai", con);
                   var command1 = new SqlCommand("spGetFirmPrincipalRoles__cai", con);
                   var command2 = new SqlCommand("spGetFirmSubOffices__cai", con);
                   var command3 = new SqlCommand("spGetTradingNamesByCompanyId__cai", con);
                   command.CommandType =  CommandType.StoredProcedure;
                   command1.CommandType = CommandType.StoredProcedure;
                   command2.CommandType = CommandType.StoredProcedure;
                   command3.CommandType = CommandType.StoredProcedure;

                   command.Parameters.AddWithValue("id", itemID);
                   command1.Parameters.AddWithValue("id", itemID);
                   command2.Parameters.AddWithValue("id", itemID);
                   command3.Parameters.AddWithValue("cid", itemID);

                   SqlDataAdapter da = new SqlDataAdapter(command);
                   da.Fill(dt);

                   SqlDataAdapter da1 = new SqlDataAdapter(command1);
                   da1.Fill(dt1);

                   SqlDataAdapter da2 = new SqlDataAdapter(command2);
                   da2.Fill(dt2);

                   SqlDataAdapter da3 = new SqlDataAdapter(command3);
                   da3.Fill(dt3);

                   if (itemType != "f")
                   {

                       SetMemberData(dt);

                   }
                   else
                   {
                       if (dt.Rows.Count > 0)
                       {
                           SetFirmData(dt);
                                                    
                       }

                       // trading names for 
                       if (dt3.Rows.Count > 0)
                       {
                           ltradingname.Visible = true;
                            
                             
                                    R3.DataSource = dt3;
                                    R3.DataBind();
                             
                       }
                       else
                       { ltradingname.Visible = false; }

                       // view principals
                       if (dt1.Rows.Count > 0)
                       {
                           bp.Visible = true;
                           dt1.Columns.Add("c");
                           int c = 0;
                           foreach (DataRow dr1 in dt1.Rows)
                           {
                               dr1["c"] = c = c + 1;
                           }
                           R1.DataSource = dt1;
                           R1.DataBind();
                       }
                       else
                       {
                           bp.Visible = false;
                       }

                       // sub offices 
                       if (dt2.Rows.Count > 0)
                       {
                           dt2.Columns.Add("add");
                           foreach (DataRow dr2 in dt2.Rows)
                           {
                               string stradd2 = string.Empty;


                               //address line1
                               if (!string.IsNullOrWhiteSpace(dr2["line1"].ToString()))
                               {
                                   if (!dr2["line1"].ToString().Equals("n/a"))
                                   {
                                       stradd2 = dr2["line1"].ToString() + ",@";
                                   }

                               }

                               else
                               { stradd2 = string.Empty; }




                               //address lin2
                               if (!string.IsNullOrWhiteSpace(dr2["line2"].ToString().Trim()))
                               {
                                   if (!dr2["line2"].ToString().Trim().Equals("n/a"))
                                   {
                                       stradd2 = stradd2 + dr2["line2"].ToString().Trim() + ",@";
                                   }
                               }
                               else
                               { stradd2 = stradd2 + ""; }



                               //address line3

                               if (!string.IsNullOrWhiteSpace(dr2["line3"].ToString().Trim()))
                               {
                                   if (!dr2["line3"].ToString().Trim().Equals("n/a"))
                                   {
                                       stradd2 = stradd2 + dr2["line3"].ToString().Trim() + ",@";
                                   }
                               }
                               else
                               { stradd2 = stradd2 + ""; }




                               //address line4

                               if (!string.IsNullOrWhiteSpace(dr2["line4"].ToString().Trim()))
                               {
                                   if (!dr2["line4"].ToString().Trim().Equals("n/a"))
                                   {
                                       stradd2 = stradd2 + dr2["line4"].ToString().Trim() + ",@";
                                   }
                               }
                               else
                               { stradd2 = stradd2 + ""; }




                               // address city 
                               if (!string.IsNullOrWhiteSpace(dr2["city"].ToString().Trim()))
                               {
                                   if (!dr2["city"].ToString().Trim().Equals("n/a"))
                                   {
                                       stradd2 = stradd2 + dr2["city"].ToString().Trim() + ",@";
                                   }
                               }
                               else
                               { stradd2 = stradd2 + ""; }



                               // address county
                               if (!string.IsNullOrWhiteSpace(dr2["county"].ToString().Trim()))
                               {
                                   if (!dr2["county"].ToString().Trim().Equals("n/a"))
                                   {
                                       stradd2 = stradd2 + dr2["county"].ToString().Trim() + ",@";
                                   }
                               }
                               else
                               { stradd2 = stradd2 + ""; }



                               // address postcode

                               if (!string.IsNullOrWhiteSpace(dr2["postcode"].ToString().Trim()))
                               {
                                   if (!dr2["postcode"].ToString().Trim().Equals("n/a"))
                                   {
                                       stradd2 = stradd2 + dr2["postcode"].ToString().Trim() + ",@";
                                   }
                               }
                               else
                               { stradd2 = stradd2 + ""; }


                               // address country
                               if (!string.IsNullOrWhiteSpace(dr2["country"].ToString().Trim()))
                               {
                                   if (!dr2["country"].ToString().Trim().Equals("n/a"))
                                   {
                                       stradd2 = stradd2 + dr2["country"].ToString().Trim() + ".";
                                   }
                               }
                               else
                               { stradd2 = stradd2 + ""; }

                               stradd2 = stradd2.Replace("@", Environment.NewLine);
                               dr2["add"] = stradd2;

                           }

                           R2.DataSource = dt2;
                           R2.DataBind();
                       }

                   }
               }
           }


           public void SetFirmData(DataTable dt)
           {


               foreach (DataRow dr in dt.Rows)
               {
                   string stradd = string.Empty;

                   lblFirmName.Text = "Firm Details - " + dr["FirmName"].ToString();


                   if (dr["product"].ToString() == "NO")
                   {
                       liblc.Visible = false;
                   }
                   else
                   {

                       liblc.Visible = true;
                       liblc.Text = "Investment Business License Category : IIA - " + dr["product"].ToString();
                   }

                   //firm name main 
                   lblfirmm.Text = dr["FirmName"].ToString();

                   //address line1
                   if (!string.IsNullOrWhiteSpace(dr["line1"].ToString().Trim()))
                   {
                       if (!string.IsNullOrWhiteSpace(dr["line1"].ToString()))
                       {
                           stradd = dr["line1"].ToString() + ",@";
                       }

                   }

                   else
                   { stradd = string.Empty; }


                   //   lblAddressLine1.Text = dr["line1"].ToString(); 

                   //address lin2
                   if (!string.IsNullOrWhiteSpace(dr["line2"].ToString().Trim()))
                   {
                       if (!string.IsNullOrWhiteSpace(dr["line2"].ToString().Trim()))
                       {
                           stradd = stradd + dr["line2"].ToString().Trim() + ",@";
                       }
                   }
                   else
                   { stradd = stradd + ""; }


                   // lblAddressLine2.Text = dr["line2"].ToString(); 

                   //address line3

                   if (!string.IsNullOrWhiteSpace(dr["line3"].ToString().Trim()))
                   {
                       if (!string.IsNullOrWhiteSpace(dr["line3"].ToString().Trim()))
                       {
                           stradd = stradd + dr["line3"].ToString().Trim() + ",@";
                       }
                   }
                   else
                   { stradd = stradd + ""; }

                   //     lblAddressLine3.Text = dr["line3"].ToString();


                   //address line4

                   if (!string.IsNullOrWhiteSpace(dr["line4"].ToString().Trim()))
                   {
                       if (!string.IsNullOrWhiteSpace(dr["line4"].ToString().Trim()))
                       {
                           stradd = stradd + dr["line4"].ToString().Trim() + ",@";
                       }
                   }
                   else
                   { stradd = stradd + ""; }
                   //     lblAddressLine4.Text = dr["line4"].ToString();



                   // address city 
                   if (!string.IsNullOrWhiteSpace(dr["city"].ToString().Trim()))
                   {
                       if (!string.IsNullOrWhiteSpace(dr["city"].ToString().Trim()))
                       {
                           stradd = stradd + dr["city"].ToString().Trim() + ",@";
                       }
                   }
                   else
                   { stradd = stradd + ""; }

                   //   lblAddressCity.Text = dr.Field<string>("city"); 

                   // address county
                   if (!string.IsNullOrWhiteSpace(dr["county"].ToString().Trim()))
                   {
                       if (!string.IsNullOrWhiteSpace(dr["county"].ToString().Trim()))
                       {
                           stradd = stradd + dr["county"].ToString().Trim() + ",@";
                       }
                   }
                   else
                   { stradd = stradd + ""; }


                   //  lblAddressCounty.Text = dr.Field<string>("county"); 

                   // address postcode

                   if (!string.IsNullOrWhiteSpace(dr["postcode"].ToString().Trim()))
                   {
                       if (!string.IsNullOrWhiteSpace(dr["postcode"].ToString().Trim()))
                       {
                           stradd = stradd + dr["postcode"].ToString().Trim() + ",@";
                       }
                   }
                   else
                   { stradd = stradd + ""; }
                   // lblAddressPostCode.Text = dr.Field<string>("postcode"); 

                   // address country
                   if (!string.IsNullOrWhiteSpace(dr["country"].ToString().Trim()))
                   {
                       if (!string.IsNullOrWhiteSpace(dr["country"].ToString().Trim()))
                       {
                           stradd = stradd + dr["country"].ToString().Trim() + ".";
                       }
                   }
                   else
                   { stradd = stradd + ""; }

                   stradd = stradd.Replace("@", Environment.NewLine);
                   lblAddressLine1.Text = stradd;

                   // phoneno
                   lblPhone.Text = dr["areacode"].ToString() + dr.Field<string>("phoneno");



                   lblFax.Text = dr["faxno"].ToString();
                   lblMainEmail.Text = dr.Field<string>("mainemail");
                  // lblInfoEmail.Text = dr.Field<string>("infoemail");
                   //lblJobsEmail.Text = dr.Field<string>("jobsemail");
                   //lblTrainingEmail.Text = dr.Field<string>("jobsemail");
                   lblWebsite.Text = dr.Field<string>("website");
                   //  lblNumberOfEmployees.Text = dr.Field<Int32>("numberemployees").ToString();
               }
           }

  

           public void SetMemberData(DataTable dt)
           {
               foreach (DataRow dr in dt.Rows)
               {
                   //if (dr.Field<String>("DirectoryStatus") == "Visible")
                   //{
                   lblMemberFullName.Text = dr.Field<string>("FirstName") + " " + dr.Field<string>("LastName");
                   char[] delimiterChars = { '/', };
                   string[] namewcompany = dr.Field<string>("NameWCompany").Split(delimiterChars);
                   if (namewcompany.Length > 1)
                   {
                       lblMemberCompany.Text = namewcompany[1];
                   }
                   else
                   {
                       lblMemberCompany.Text = "Employment information not available.";
                   }

                   lblMemberEmail.Text = dr.Field<string>("Email1");
                   lblMemberJoinDate.Text = dr.Field<DateTime>("JoinDate").ToString("dd/MM/yyyy");
                   //}
               }
           }



        }
}
