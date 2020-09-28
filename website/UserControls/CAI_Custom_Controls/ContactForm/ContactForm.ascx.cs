using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using Telerik.Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using RazorEngine;
using System.IO;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.ContactForm
{
    public partial class ContactForm : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {




        }

        protected void SendEmail(string subject, string htmlbody, string email)
        {

            try
            {

                //build the email message
                MailMessage mail = new MailMessage(ConfigurationManager.AppSettings["MailFrom"], email);
                //  mail.BodyEncoding = Encoding.Default;
                mail.Subject = subject;
                mail.Body = htmlbody;
                mail.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();
                client.Port = int.Parse(ConfigurationManager.AppSettings["MailPort"]);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailPort"], ConfigurationManager.AppSettings["MailPort"]);
                client.UseDefaultCredentials = ConfigurationManager.AppSettings["MailPort"] == "true";
                client.Host = ConfigurationManager.AppSettings["MailServer"];
                client.EnableSsl = ConfigurationManager.AppSettings["Mail.EnableSsl"] == "False";
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)

        {
            SqlConnection con = new SqlConnection("Server=" + ConfigurationManager.AppSettings["AptifyDBServer"] + ";Database=" + ConfigurationManager.AppSettings["AptifyEntitiesDB"] + ";Trusted_Connection=True");

            try
            {

                //Email settings 
                 string emailto = "Info@charteredaccountants.ie";
                string subject = "RE Chartered Accountants Ireland Contact Form Query ";



                //@UpdatedBatchFlag default=0
                Boolean subf = false;
                Boolean spcom = false;
                DateTime myDateTime = DateTime.Now;
                DateTime dtime = Convert.ToDateTime(myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));



                string sfname = fname.Text.Trim();
                string slname = lname.Text.Trim();
                string semail = email.Text.Trim();
                string sccode = countyrcode.Text.Trim();
                string sacode = mobilearea.Text.Trim();
                string snumber = number.Text.Trim();
                string sinterest = ddi.SelectedItem.Text.Trim();
                string squery = txtArea.Text;
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
                    cmd.Parameters.Add(new SqlParameter("@InteractionDetail", "Contact Form"));
                    //@RouteOfEntry Default to Elevation for Brochure Download 
                    cmd.Parameters.Add(new SqlParameter("@RouteOfEntry", "Elevation"));
                    //@Source Default to Direct Lead
                    cmd.Parameters.Add(new SqlParameter("@Source", "Direct Lead"));
                    //@Source Default to Lead
                    cmd.Parameters.Add(new SqlParameter("@Status", "Lead"));
                    cmd.Parameters.Add(new SqlParameter("@PreferCommunications", spcom));
                    cmd.Parameters.Add(new SqlParameter("@DateCreated", dtime));
                    cmd.Parameters.Add(new SqlParameter("@AreaOfInterest", sinterest));
                    cmd.Parameters.Add(new SqlParameter("@ContactQuery", txtArea.Text));

                    // out parameter
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                    cmd.Parameters["@id"].Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    int s = cmd.ExecuteNonQuery();
                    int ival = Convert.ToInt32(cmd.Parameters["@id"].Value.ToString());
                    if (ival > 0)
                    {


                        string emailtemp = EmailTemplate(ival);
                        SendEmail(subject, emailtemp, emailto);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "ShowDiv();", true);

                    }
                }

            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }
            finally
            { con.Close(); }
        }

        public string EmailTemplate(int rid)
        {
            string myString = "";
            StreamReader reader = new StreamReader(Server.MapPath("~/UserControls/CAI_Custom_Controls/ContactForm/EmailQuery.html"));
            string readFile = reader.ReadToEnd();
            string cont = string.Concat(countyrcode.Text.Trim(), mobilearea.Text.Trim(), number.Text.Trim());
            myString = readFile;
            myString = myString.Replace("$$Rid$$", rid.ToString());
            myString = myString.Replace("$$fname$$", fname.Text.Trim());
            myString = myString.Replace("$$lname$$", lname.Text.Trim());
            myString = myString.Replace("$$Email$$", email.Text.Trim());
            myString = myString.Replace("$$contact$$", cont);
            myString = myString.Replace("$$Describe$$", ddi.SelectedItem.Text.Trim());
            myString = myString.Replace("$$MyQuery$$", txtArea.Text.Trim());
            return myString;
        }
    }
}
