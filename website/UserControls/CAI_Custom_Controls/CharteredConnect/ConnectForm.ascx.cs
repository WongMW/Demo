using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.CharteredConnect
{
    public partial class ConnectForm : Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
    {
        // stored procedure names
        private const string spGetCharteredConnectSignUpByEmail__cai = "spGetCharteredConnectSignUpByEmail__cai";
        private const string spCreateCharteredConnectSignUp__cai = "spCreateCharteredConnectSignUp__cai";
        // ---

        /// <summary>
        /// Secured URL that will be protected by this form
        /// </summary>
        public String SecuredURL { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack && !this.IsBackend())
            {
                // checking if security passed, then redirect to landing page
                if (ConnectFormAuthCheck.CheckSecurity(Session))
                {
                    CompleteFormAuth(false);
                }
            }
        }

        /// <summary>
        /// Method that will set secure cookie for the other form
        /// </summary>
        private void CompleteFormAuth(Boolean generateSecurityToken = true)
        {
            if(generateSecurityToken)
            {
                Session[ConnectFormAuthCheck.SECURED_COOKIE_NAME] = Guid.NewGuid().ToString();
            }
            
            if (!String.IsNullOrEmpty(SecuredURL))
            {
                Response.Redirect(SecuredURL);
            }
        }

        private void markAsError(HtmlGenericControl el, Boolean hasError)
        {
            if (hasError && !el.Attributes["class"].Contains("has-error"))
            {
                el.Attributes["class"] = el.Attributes["class"] + " has-error";
            }
            else if (!hasError)
            {
                el.Attributes["class"] = el.Attributes["class"].Replace("has-error", "");
            }
        }

        private Boolean isFieldValid(String txt, Regex reg, int min = 0, int max = 0)
        {
            var matches = reg.Match(txt.Trim());

            if (min > 0 && max > 0)
            {
                if (txt.Trim().Length > max || txt.Trim().Length < min)
                {
                    return false;
                }
            }

            return matches.Success;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblErrorMessage.Visible = false;

            var regexFirstName = new Regex(@"^[a-zA-Z\s]+$");
            var regexLastName = new Regex(@"^[a-zA-Z\s]+$");
            var regexCollegeName = new Regex(@"^[a-zA-Z\s\d\,\.\+]+$");
            var regexEmail = new Regex(@"^([\w\.\-\+]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var regexGraduationYear = new Regex(@"^\d{4}$"); // min 2 - max 3

            var firstName = txtFirstName.Text;
            var lastName = txtLastName.Text;
            var email = txtEmail.Text;
            var allowCommunication = chkAllowCommunication.Checked;
            var graduationYear = txtGraduationYear.Text;
            var collegeName = hdnAwardingBody.Value;

            var isFormValid = true;

            if (!isFieldValid(firstName, regexFirstName))
            {
                isFormValid = false;
                // firstname is invalid
                lblFirstNameError.InnerText = "First name is required";
                markAsError(holderTxtFirstName, true);
            }
            else
            {
                markAsError(holderTxtFirstName, false);
            }

            if (!isFieldValid(lastName, regexLastName))
            {
                isFormValid = false;
                // lastname is invalid
                lblLastNameError.InnerText = "Last name is required";
                markAsError(holderTxtLastName, true);
            }
            else
            {
                markAsError(holderTxtLastName, false);
            }

            if (!isFieldValid(email, regexEmail))
            {
                isFormValid = false;
                // email is invalid
                lblEmailError.InnerText = "Not a valid email address";
                markAsError(holderTxtEmail, true);
            }
            else
            {
                markAsError(holderTxtEmail, false);
            }

            if (!isFieldValid(graduationYear, regexGraduationYear))
            {
                isFormValid = false;
                // graduation year is invalid
                lblGraduationYearError.InnerText = "Graduation Year must be in format YYYY (e.g. 2019)";
                markAsError(holderTxtGraduationYear, true);
            }
            else
            {
                markAsError(holderTxtGraduationYear, false);
            }

            if (!isFieldValid(collegeName, regexCollegeName))
            {
                isFormValid = false;
                // college name is not specified
                lblAwardingBodyError.InnerText = "College Name must be specified";
                markAsError(holderTxtCollegeName, true);
            }
            else
            {
                markAsError(holderTxtCollegeName, false);
            }

            if (!isFormValid)
            {
                // at least one field is invalid 
            }
            else
            {
                var success = false;

                using (SqlConnection con = new SqlConnection(SoftwareDesign.Helper.GetAptifyEntitiesConnectionString()))
                {
                    // lets check if submitted email already exists
                    using (SqlCommand cmd = new SqlCommand(spGetCharteredConnectSignUpByEmail__cai))
                    {
                        cmd.Connection = con;
                        // in parameter
                        cmd.Parameters.Add(new SqlParameter("@EMAIL", email));
                        DataTable dt = new DataTable();
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);

                            if(dt.Rows.Count > 0)
                            {
                                success = true;
                            }
                        }
                    }

                    // user does not exist with the given email address
                    if(!success)
                    {
                        using (SqlCommand cmd = new SqlCommand(spCreateCharteredConnectSignUp__cai))
                        {
                            cmd.Connection = con;
                            // in parameter
                            cmd.Parameters.Add(new SqlParameter("@FirstName", firstName));
                            cmd.Parameters.Add(new SqlParameter("@LastName", lastName));
                            cmd.Parameters.Add(new SqlParameter("@Email", email));
                            cmd.Parameters.Add(new SqlParameter("@CollegeName", collegeName));
                            cmd.Parameters.Add(new SqlParameter("@GraduationYear", graduationYear));
                            cmd.Parameters.Add(new SqlParameter("@DateSubmitted", DateTime.Now));
                            cmd.Parameters.Add(new SqlParameter("@AllowCommunication", allowCommunication));

                            // out parameter
                            cmd.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                            cmd.Parameters["@ID"].Direction = System.Data.ParameterDirection.Output;

                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            con.Open();
                            int s = cmd.ExecuteNonQuery();
                            int ival = Convert.ToInt32(cmd.Parameters["@ID"].Value.ToString());
                            if (ival > 0)
                            {
                                success = true;
                            }
                            else
                            {
                                // failure
                                lblErrorMessage.InnerText = "There was an error submitting the form at this time.";
                                lblErrorMessage.Visible = true;
                            }
                        }
                    }

                    if(success)
                    {
                        CompleteFormAuth();
                    }
                }
            }
        }
    }
}