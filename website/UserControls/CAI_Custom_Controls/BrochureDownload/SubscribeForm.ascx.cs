using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.BrochureDownload
{
    public partial class SubscribeForm : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void btnSave_Click(object sender, EventArgs e)

        {
            SqlConnection con = new SqlConnection("Server=" + ConfigurationManager.AppSettings["AptifyDBServer"] + ";Database=" + ConfigurationManager.AppSettings["AptifyEntitiesDB"] + ";Trusted_Connection=True");

            try
            {

                //sql connection 
                

                //@UpdatedBatchFlag default=0
                Boolean subf = false;
                Boolean spcom = false;
                DateTime myDateTime = DateTime.Now;
                DateTime dtime =  Convert.ToDateTime(myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));



                string sfname = fname.Text.Trim();
                string slname = lname.Text.Trim();
                string semail = email.Text.Trim();
                string sccode = countyrcode.Text.Trim();
                string sacode = mobilearea.Text.Trim();
                string snumber = number.Text.Trim();
                string sinterest = ddi.SelectedItem.Text;
                if (cb1.Checked)
                {
                    spcom = true;
                }
                else
                { spcom = false; }
                // insert resords to DB

                //using (SqlCommand cmd = new SqlCommand("spInsertIntoWebStagingStudentLeads__cai"))
                using (SqlCommand cmd = new SqlCommand("spCreateWebStagingStudentLeads__cai"))
                {
                    cmd.Connection = con;
                    // in parameter
                    cmd.Parameters.Add(new SqlParameter("@FirstName", sfname));
                    cmd.Parameters.Add(new SqlParameter("@LastName", slname));
                    cmd.Parameters.Add(new SqlParameter("@Email", semail));
                    cmd.Parameters.Add(new SqlParameter("@PhoneCountryCode", sccode));
                    cmd.Parameters.Add(new SqlParameter("@PhoneAreaCode", sacode));
                    cmd.Parameters.Add(new SqlParameter("@PhoneNumber", snumber));
                    cmd.Parameters.Add(new SqlParameter("@UpdatedBatchFlag", subf));
                    //@InteractionDetail Default to Brochure Download for Brochure Download 
                    cmd.Parameters.Add(new SqlParameter("@InteractionDetail", "Brochure Download"));
                    //@RouteOfEntry Default to Elevation for Brochure Download 
                    cmd.Parameters.Add(new SqlParameter("@RouteOfEntry", "Elevation"));
                    //@Source Default to Web Lead
                    cmd.Parameters.Add(new SqlParameter("@Source", "Web Lead"));
                    //@Source Default to Lead
                    cmd.Parameters.Add(new SqlParameter("@Status", "Lead"));
                    cmd.Parameters.Add(new SqlParameter("@PreferCommunications", spcom));
                    cmd.Parameters.Add(new SqlParameter("@DateCreated", dtime));
                    cmd.Parameters.Add(new SqlParameter("@AreaOfInterest", ddi.SelectedItem.Text));
                    // insert null into contact text areay
                    cmd.Parameters.Add(new SqlParameter("@ContactQuery", DBNull.Value));


                    // out parameter
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                    cmd.Parameters["@id"].Direction = ParameterDirection.Output;



                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    int s = cmd.ExecuteNonQuery();
                    int ival = Convert.ToInt32(cmd.Parameters["@id"].Value.ToString());
                    if (ival > 0)
                    {


                        Response.Redirect("/Prospective-Students/Apply-and-Join/brochure-download/thank-you");


                    }
                }

            }
            catch( Exception ex)
            { Console.WriteLine(ex.Message); }
            finally
            { con.Close(); }
        }

    


      
    }
}
