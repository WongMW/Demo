using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.TrainingVacancies
{
    public partial class AddTrainingVacanciesForm : Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced                                        
    {

        private static string spCreateWebTrainingVacancies__cai => "spCreateWebTrainingVacancies__cai";
        private static string spUpdateWebTrainingVacancies__cai => "spUpdateWebTrainingVacancies__cai";
        private static string spGetPersonCompanies => "spGetPersonCompanies";
        private static string spGetCountryByName__cai => "spGetCountryByName__cai";
        private static string spGetWebTrainingVacancies__cai => "spGetWebTrainingVacancies__cai";
        private static string spGetPerson => "spGetPersonTrainingVacancies__cai";

        public int id = -1;

        public delegate void EditStatusDelegate(int TVID);

        public event EditStatusDelegate GetTVID;

        public delegate void ChangeStepDelegate(Step step);

        public event ChangeStepDelegate GetStep;

        public delegate void ErrorDelegate(string errMsg);

        public event ErrorDelegate GetError;        

        public enum Status { Saves = 1, Submitted = 2, Aprroved = 3, Rejected = 4};
        public enum Step { Button = 1, Form = 2, Ok = 3, Error = 4 };
        public enum Modes { Add = 1, Edit = 2, Button = 3 };
        enum TrainInType { Business = 1, Practice = 2, PublicSector = 3 };
        enum VacancyType { TrainingContract = 1, FlexibleRoute = 2, InternshipUp3months = 3, InternshipOver3months = 4 };

        public Step CurrentStep = Step.Button;

        public Modes Mode = Modes.Button;


        private DataTable GetUserDetails()
        {
            var parameters = new List<IDataParameter>();
            try
            {
                var sql = $"{AptifyApplication.GetEntityBaseDatabase("Person")}..{spGetPerson}";
                var param = DataAction.GetDataParameter("@PersonID", SqlDbType.Int, User1.PersonID);
                parameters.Add(param);

                var dt = DataAction.GetDataTableParametrized(sql, CommandType.StoredProcedure,
                    parameters.ToArray());

                return dt;
            }
            catch (Exception ex)
            {
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex);
                return null;
            }
        }

        public void LoadTrainingData()
        {
            var parameters = new List<IDataParameter>();
            try
            {
                var sql = $"{AptifyApplication.GetEntityBaseDatabase("Person")}..{spGetWebTrainingVacancies__cai}";
                var param = DataAction.GetDataParameter("@ID", SqlDbType.Int, id);
                parameters.Add(param);

                var dt = DataAction.GetDataTableParametrized(sql, CommandType.StoredProcedure,
                    parameters.ToArray());

                if (dt.Rows.Count > 0)
                {
                    var item = dt.Rows[0];
                    fid.Value = item["ID"].ToString();
                    ffirmid.Value = item["FirmID"].ToString();
                    fpersonid.Value = item["PersonID"].ToString();
                    fcname.Text = item["CompanyName"].ToString();
                    fdescription.Text = item["CompanyDescription"].ToString();
                    fjobtitle.Text = item["JobTitle"].ToString();
                    fjobtown.Text = item["JobTown"].ToString();
                    fjobcounty.SelectedValue = item["JobCounty"].ToString();
                    ftrainintype.SelectedValue = item["TrainInType"].ToString();
                    IList<string> items = item["VacancyType"].ToString().Split(",".ToCharArray());
                    foreach ( var index in items)
                    {
                        foreach ( ListItem sel in fvacancytype.Items )
                        {
                            if ( sel.Value.ToLower() == index.ToLower() )
                            {
                                sel.Selected = true;
                            }
                        }
                    }

                    items = item["BenefitsRemuneration"].ToString().Split(",".ToCharArray());
                    foreach (var index in items)
                    {
                        if (index.Trim().ToLower() == "pension")
                        { 
                            fcpension.Checked = true;
                        } else if (index.Trim().ToLower() == "health insurance")
                        {
                            fchealth.Checked = true;
                        }
                        else if (index.Trim().ToLower() == "tax savers scheme")
                        {
                            fctex.Checked = true;
                        }
                        else if (index.Trim().ToLower() == "flexi hours")
                        {
                            fcflexi.Checked = true;
                        }
                        else if (index.Trim().ToLower() == "learning & development")
                        {
                            fclearn.Checked = true;
                        }
                        else if (index.Trim().ToLower().StartsWith("others:"))
                        {
                            fcbother.Checked = true;
                            string[] strs = index.Split(':');
                            if ( strs.Length == 2)
                            {
                                fcbothertext.Text = strs[1].Trim();
                            }
                        }                        

                    }

                    fjobspec.Text = item["JobSpec"].ToString();
                    fjobrequirements.Text = item["JobRequirements"].ToString();
                    //  fbenefits.Text = "";  item["BenefitsRemuneration"].ToString();
                    fhowtoapply.Text = item["HowToApply"].ToString();
                    fwebsite.Text = item["WebSite"].ToString();
                    fdatelastupdated.Value = item["DateLastUpdated"].ToString();
                    fdateposted.Value = item["DatePosted"].ToString();
                    fdateclosing.Text = ((DateTime)item["DateClosing"]).ToShortDateString();
                    fapprovedby.Value = item["ApprovedBy"].ToString();
                    fwebstatus.Value = item["WebStatus"].ToString();
                    fisdraft.Value = item["IsDraft"].ToString();
                    fisactive.Value = item["IsActive"].ToString();

                }
            }
            catch (Exception ex)
            {
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex);
            }
        }

        public void LoadData()
        {

            DataTable dt = new DataTable();

            string Loginpage = "/Login.aspx";
            // string Noaccess = "/securityerror.aspx?Message=Access+to+the+requested+resource+has+been+denied";
            if (User1.PersonID <= 0)
            {
                Session["ReturnToPage"] = Request.RawUrl;
                Response.Redirect(Loginpage);
            }
            else
            {

                dt = GetUserDetails();
                if (dt != null && dt.Rows.Count == 1)
                {                
                    ffirmid.Value =dt.Rows[0]["CompanyID"].ToString();
                    fpersonid.Value = "" + User1.PersonID;
                    fcname.Text = dt.Rows[0]["CompanyName"].ToString();
                    fwebsite.Text = dt.Rows[0]["WebSite"].ToString();
                }
                else
                {

                    //unauthorized access
                    // Response.Redirect(Noaccess);

                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {            

            if (!IsPostBack)
            {
                string code = Guid.NewGuid().ToString();
                if (id < 0)
                {                    
                    LoadData();
                } else
                {                                        
                    LoadTrainingData();
                }
            } else
            {
                LoadData();
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.id = 0;
            if ( this.Mode == Modes.Add )
            {
                this.CurrentStep = Step.Button;
            }
            else if ( this.Mode == Modes.Edit && GetTVID != null )
            {
                GetTVID(this.id);
            }
        }

        protected void btnShowForm_Click(object sender, EventArgs e)
        {
            this.CurrentStep = Step.Form;
            this.Mode = Modes.Add;
            if (GetStep != null)
            {
                GetStep(this.CurrentStep);
            }
        }        

        protected void btnUpdate_Click(object sender, EventArgs e)

        {
            SqlConnection con = new SqlConnection("Server=" + ConfigurationManager.AppSettings["AptifyDBServer"] + ";Database=" + ConfigurationManager.AppSettings["AptifyEntitiesDB"] + ";Trusted_Connection=True");

            var sSql = Database + ".." + spUpdateWebTrainingVacancies__cai;
            try
            {

                DateTime myDateTime = DateTime.Now;
                DateTime dtime = Convert.ToDateTime(myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));

                int id = !string.IsNullOrEmpty(fid.Value) ? int.Parse(fid.Value) : -1;
                int spfirmid = !string.IsNullOrEmpty(ffirmid.Value) ? int.Parse(ffirmid.Value) : -1;
                int sppersonid = !string.IsNullOrEmpty(fpersonid.Value) ? int.Parse(fpersonid.Value) : -1;
                string spcompanyname = fcname.Text.Trim();
                string spdescription = fdescription.Text.Trim();
                string spjobtitle = fjobtitle.Text.Trim();
                string spjobtown = fjobtown.Text.Trim();
                string spjobcounty = fjobcounty.SelectedValue.Trim();
                int[] indices = fvacancytype.GetSelectedIndices();
                IList<string> items = new List<string>();
                foreach(int index in indices)
                {
                    items.Add(fvacancytype.Items[index].Value);
                }
                string spvacancytype = String.Join(",",items);
                string sptrainingtype = ftrainintype.SelectedValue.Trim();
                string spjobspec = fjobspec.Text.Trim();
                string spjobrequeriments = fjobrequirements.Text.Trim();
                string spbenefitsremuneration = ""; //fbenefits.Text.Trim();
                string sphowtoapply = fhowtoapply.Text.Trim();
                string spwebsite = fwebsite.Text.Trim();
                DateTime spdatecreated = dtime;
                int spwebstatus = 1;
                Boolean spisdraft = true;
                Boolean spisactive = false;

                // spbenefitsremuneration
                IList<string> benefits = new List<string>();
                if (fcpension.Checked)
                    benefits.Add("Pension");
                if (fchealth.Checked)
                    benefits.Add("Health insurance");
                if (fctex.Checked)
                    benefits.Add("Tax savers scheme");
                if (fcflexi.Checked)
                    benefits.Add("Flexi hours");
                if (fclearn.Checked)
                    benefits.Add("Learning & development");
                if (fcbother.Checked)
                    benefits.Add("Others: " + fcbothertext.Text.Trim());
                spbenefitsremuneration = String.Join(",",benefits);

                var parameters = new List<IDataParameter>();
                parameters.Add(DataAction.GetDataParameter("@ID", SqlDbType.Int, id));
                parameters.Add(DataAction.GetDataParameter("@FirmID", SqlDbType.Int, spfirmid));
                parameters.Add(DataAction.GetDataParameter("@PersonID", SqlDbType.Int, sppersonid));
                parameters.Add(DataAction.GetDataParameter("@CompanyName", SqlDbType.NVarChar, spcompanyname));
                parameters.Add(DataAction.GetDataParameter("@CompanyDescription", SqlDbType.NVarChar, spdescription));
                parameters.Add(DataAction.GetDataParameter("@JobTitle", SqlDbType.NVarChar, spjobtitle));
                parameters.Add(DataAction.GetDataParameter("@JobTown", SqlDbType.NVarChar, spjobtown));
                parameters.Add(DataAction.GetDataParameter("@JobCounty", SqlDbType.NVarChar, spjobcounty));
                parameters.Add(DataAction.GetDataParameter("@VacancyType", SqlDbType.NVarChar, spvacancytype));
                parameters.Add(DataAction.GetDataParameter("@TrainInType", SqlDbType.NVarChar, sptrainingtype));
                parameters.Add(DataAction.GetDataParameter("@JobSpec", SqlDbType.NVarChar, spjobspec));
                parameters.Add(DataAction.GetDataParameter("@JobRequirements", SqlDbType.NVarChar, spjobrequeriments));
                parameters.Add(DataAction.GetDataParameter("@BenefitsRemuneration", SqlDbType.NVarChar, spbenefitsremuneration));
                parameters.Add(DataAction.GetDataParameter("@HowToApply", SqlDbType.NVarChar, sphowtoapply));
                parameters.Add(DataAction.GetDataParameter("@Website", SqlDbType.NVarChar, spwebsite));
                parameters.Add(DataAction.GetDataParameter("@DateLastUpdated", SqlDbType.DateTime, dtime));
                parameters.Add(DataAction.GetDataParameter("@DatePosted", SqlDbType.DateTime, DBNull.Value));
                parameters.Add(DataAction.GetDataParameter("@DateClosing", SqlDbType.DateTime, fdateclosing.Text));
                parameters.Add(DataAction.GetDataParameter("@ApprovedBy", SqlDbType.NVarChar, DBNull.Value));
                parameters.Add(DataAction.GetDataParameter("@WebStatus", SqlDbType.Int, spwebstatus));
                parameters.Add(DataAction.GetDataParameter("@IsDraft", SqlDbType.Bit, spisdraft));
                parameters.Add(DataAction.GetDataParameter("@IsActive", SqlDbType.Bit, spisactive));

                int eeid = Convert.ToInt32(DataAction.ExecuteNonQueryParametrized(sSql, System.Data.CommandType.StoredProcedure, parameters.ToArray()));
                if ( eeid > 0 )
                {                    
                    GetTVID(eeid);                    
                }

            }
            catch (Exception ex)
            {
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)

        {
            SqlConnection con = new SqlConnection("Server=" + ConfigurationManager.AppSettings["AptifyDBServer"] + ";Database=" + ConfigurationManager.AppSettings["AptifyEntitiesDB"] + ";Trusted_Connection=True");
            
            var sSql = Database + ".." + spCreateWebTrainingVacancies__cai;
            try
            {

                DateTime myDateTime = DateTime.Now;
                DateTime dtime = Convert.ToDateTime(myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                
                int id = !string.IsNullOrEmpty(fid.Value) ? int.Parse(fid.Value) : -1;
                int spfirmid = !string.IsNullOrEmpty(ffirmid.Value) ? int.Parse(ffirmid.Value) : -1;
                int sppersonid = !string.IsNullOrEmpty(fpersonid.Value) ? int.Parse(fpersonid.Value) : -1;
                string spcompanyname = fcname.Text.Trim();
                string spdescription = fdescription.Text.Trim();
                string spjobtitle = fjobtitle.Text.Trim();
                string spjobtown = fjobtown.Text.Trim();
                string spjobcounty = fjobcounty.SelectedValue.Trim();
                int[] indices = fvacancytype.GetSelectedIndices();
                IList<string> items = new List<string>();
                foreach (int index in indices)
                {
                    items.Add(fvacancytype.Items[index].Value);
                }
                string spvacancytype = String.Join(",", items);
                string sptrainingtype = ftrainintype.SelectedValue.Trim();
                string spjobspec = fjobspec.Text.Trim();
                string spjobrequeriments = fjobrequirements.Text.Trim();
                string spbenefitsremuneration = ""; // fbenefits.Text.Trim();
                string sphowtoapply = fhowtoapply.Text.Trim();
                string spwebsite = fwebsite.Text.Trim();
                DateTime spdatecreated = dtime;
                Boolean spwebstatus = true;
                Boolean spisdraft = true;
                Boolean spisactive = false;


                // spbenefitsremuneration
                IList<string> benefits = new List<string>();
                if (fcpension.Checked)
                    benefits.Add("Pension");
                if (fchealth.Checked)
                    benefits.Add("Health insurance");
                if (fctex.Checked)
                    benefits.Add("Tax savers scheme");
                if (fcflexi.Checked)
                    benefits.Add("Flexi hours");
                if (fclearn.Checked)
                    benefits.Add("Learning & development");
                if (fcbother.Checked)
                    benefits.Add("Others: " + fcbothertext.Text.Trim());
                spbenefitsremuneration = String.Join(",", benefits);

                var parameters = new List<IDataParameter>();
                parameters.Add(DataAction.GetDataParameter("@FirmID", SqlDbType.Int, spfirmid));
                parameters.Add(DataAction.GetDataParameter("@PersonID", SqlDbType.Int, sppersonid));
                parameters.Add(DataAction.GetDataParameter("@CompanyName", SqlDbType.NVarChar, spcompanyname));
                parameters.Add(DataAction.GetDataParameter("@CompanyDescription", SqlDbType.NVarChar, spdescription));
                parameters.Add(DataAction.GetDataParameter("@JobTitle", SqlDbType.NVarChar, spjobtitle));
                parameters.Add(DataAction.GetDataParameter("@JobTown", SqlDbType.NVarChar, spjobtown));
                parameters.Add(DataAction.GetDataParameter("@JobCounty", SqlDbType.NVarChar, spjobcounty));
                parameters.Add(DataAction.GetDataParameter("@VacancyType", SqlDbType.NVarChar, spvacancytype));
                parameters.Add(DataAction.GetDataParameter("@TrainInType", SqlDbType.NVarChar, sptrainingtype));
                parameters.Add(DataAction.GetDataParameter("@JobSpec", SqlDbType.NVarChar, spjobspec));
                parameters.Add(DataAction.GetDataParameter("@JobRequirements", SqlDbType.NVarChar, spjobrequeriments));
                parameters.Add(DataAction.GetDataParameter("@BenefitsRemuneration", SqlDbType.NVarChar, spbenefitsremuneration));
                parameters.Add(DataAction.GetDataParameter("@HowToApply", SqlDbType.NVarChar, sphowtoapply));
                parameters.Add(DataAction.GetDataParameter("@Website", SqlDbType.NVarChar, spwebsite));                
                parameters.Add(DataAction.GetDataParameter("@DateLastUpdated", SqlDbType.DateTime, DBNull.Value));
                parameters.Add(DataAction.GetDataParameter("@DatePosted", SqlDbType.DateTime, DBNull.Value));
                parameters.Add(DataAction.GetDataParameter("@DateClosing", SqlDbType.DateTime, fdateclosing.Text));
                parameters.Add(DataAction.GetDataParameter("@ApprovedBy", SqlDbType.NVarChar, DBNull.Value));
                parameters.Add(DataAction.GetDataParameter("@WebStatus", SqlDbType.Bit, spwebstatus));
                parameters.Add(DataAction.GetDataParameter("@IsDraft", SqlDbType.Bit, spisdraft));
                parameters.Add(DataAction.GetDataParameter("@IsActive", SqlDbType.Bit, spisactive));

                // out parameter
                var idParam = DataAction.GetDataParameter("@ID", System.Data.SqlDbType.Int);
                idParam.Direction = System.Data.ParameterDirection.Output;

                parameters.Add(idParam);

                if ("" + Session["lastTV"] != "" + spfirmid + "::" + sppersonid + spjobtitle)
                {
                    var eeid = Convert.ToInt32(DataAction.ExecuteNonQueryParametrized(sSql, System.Data.CommandType.StoredProcedure, parameters.ToArray()));

                    if (eeid > 0)
                    {
                        Session["lastTV"] = "" + spfirmid + "::" + sppersonid + spjobtitle;
                        GetTVID(eeid);
                    }
                    else
                    {
                        GetError("Error creating the new Training Vacancy, please try again in some minutes");
                    }
                } else
                {
                    GetError("I can't create the same TV");
                }

            }
            catch (Exception ex)
            {
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex);
            }
        }

        [Serializable]
        public class FirmDetails
        {
            public int Fid { get; set; }
            public string Fname { get; set; }
            public bool IsParent { get; set; }
            public int ParentId { get; set; }
            public bool PurchaseAllowed { get; set; }
            public bool IsRenewal { get; set; }
            public DateTime? ApprovedEndDate { get; set; }
        }
        
    }

}
