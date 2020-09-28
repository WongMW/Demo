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
    public partial class StaffListTrainingVacancies : Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced                                        
    {


        private static string spGetPersonCompanies => "spGetPersonCompanies";
        private static string spGetWebTrainingVacanciesByFirmID__cai => "spGetWebTrainingVacanciesByFirmID__cai";
        private static string spGetPerson => "spGetPersonTrainingVacancies__cai";
        private static string spTrainingVacancySubmit__cai => "spWebTrainingVacancySubmit__cai";
        private static string spTrainingVacancyClose__cai => "spTrainingVacancyClose__cai";
        private static string spDeleteWebTrainingVacancies__cai => "spDeleteWebTrainingVacancies__cai";

        public enum Status { Saved = 1, Submitted = 2, Approved = 3, Rejected = 4, Closed = 5, Expired = 6 };

        private long personID = -1;

        private long firmID = -1;

        public int id = -1;

        public int TVID = -1;

        public bool Success = false;

        public bool Error = false;

        public String CompanyName = string.Empty;

        public int CompanyID = -1;

        public List<TrainingVacancy> trainingVacancies = new List<TrainingVacancy>();

        public List<TrainingVacancy> TrainingVacancies
        {
            get
            {
                return this.trainingVacancies;
            }
        }

        private DataTable GetPersonCompanies()
        {

            var parameters = new List<IDataParameter>();
            try
            {
                var sql = $"{AptifyApplication.GetEntityBaseDatabase("Firm")}..{spGetPersonCompanies}";
                var param = DataAction.GetDataParameter("@ID", SqlDbType.Int, User1.PersonID);
                parameters.Add(param);

                var dt = DataAction.GetDataTableParametrized(sql, CommandType.StoredProcedure,
                    parameters.ToArray());

                return dt;
            }
            catch (Exception ex)
            {
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex);
            }

            return null;
        }


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

        public void LoadData()
        {
            
            if (User1 != null )
            {
                this.personID = User1.PersonID;
                this.firmID = User1.CompanyID;
                this.CompanyName = User1.Company;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            editForm.GetTVID += new StaffAddTrainingVacanciesForm.EditStatusDelegate(ucChild_EditStatusDelegate);
            if (!IsPostBack)
            {                
                if (ViewState["CompanyID"] != null)
                {
                    this.firmID = this.CompanyID = int.Parse(ViewState["CompanyID"].ToString());
                    this.fcompanyid.Value = "" + this.CompanyID;
                }
                if (ViewState["CompanyName"] != null)
                {
                    this.CompanyName = ViewState["CompanyName"].ToString();
                    this.fcompanyname.Text = this.CompanyName;
                }
                if (this.CompanyID != 0)
                {
                    LoadData();
                    if (this.firmID > 0 && this.personID > 0)
                    {
                        GetTraningVacancies();
                    }
                    else
                    {
                        MockData();
                    }
                }
            } else
            {
                if (fcompanyid != null && !String.IsNullOrEmpty(fcompanyid.Value))
                {
                    this.CompanyID = int.Parse(fcompanyid.Value);
                    if (this.CompanyID != 0)
                    {
                        if (ViewState["CompanyID"] != null && ViewState["CompanyID"].ToString() == this.CompanyID.ToString())
                        {
                            this.firmID = this.CompanyID = int.Parse(ViewState["CompanyID"].ToString());
                            this.fcompanyid.Value = "" + this.CompanyID;
                        }
                        if (ViewState["CompanyName"] != null && ViewState["CompanyID"].ToString() == this.CompanyID.ToString())
                        {
                            this.CompanyName = ViewState["CompanyName"].ToString();
                            this.fcompanyname.Text = this.CompanyName;
                        }
                        // LoadData();
                        if (this.firmID > 0 && this.personID > 0)
                        {
                            GetTraningVacancies();
                        }
                        else
                        {
                            MockData();
                        }
                    }
                }
            }
        }

        void ucChild_EditStatusDelegate(int TVID, int companyId, string companyName)
        {
            if (TVID > 0)
            {
                this.Success = true;
                this.lblsuccess.Text = " Changes stored.";                
            }
            else
            {
                this.Error = true;
                this.lblerror.Text = " Please try again in some minutes.";
            }
            this.CompanyID = companyId;
            this.CompanyName = companyName;
             GetTraningVacancies();
        }

        protected void GetTraningVacancies()
        {
            var parameters = new List<IDataParameter>();
            this.trainingVacancies = new List<TrainingVacancy>();
            try
            {
                var sql = $"{AptifyApplication.GetEntityBaseDatabase("Firm")}..{spGetWebTrainingVacanciesByFirmID__cai}";
                var param = DataAction.GetDataParameter("@FirmID", SqlDbType.Int, this.firmID);
                parameters.Add(param);

                var dt = DataAction.GetDataTableParametrized(sql, CommandType.StoredProcedure,
                    parameters.ToArray());

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow item = dt.Rows[i];
                        TrainingVacancy vacancy = new TrainingVacancy();
                        vacancy.TVID = item.Field<int>("ID");
                        vacancy.TVcompanyName = item.Field<string>("CompanyName");
                        vacancy.TVJobTitle = item.Field<string>("JobTitle");
                        vacancy.TVIsDraft = "" + item.Field<bool>("IsDraft");
                        vacancy.TVWebStatus = ((Status)item.Field<int>("WebStatus")).ToString("F");
                        vacancy.TVCreationDate = item.Field<DateTime>("DateCreated").ToShortDateString();
                        vacancy.TVClosingDate = item.Field<DateTime?>("DateClosing").HasValue ? item.Field<DateTime?>("DateClosing").Value.ToShortDateString() : DateTime.Now.ToShortDateString();
                        if (item.Field<DateTime?>("DateClosing").HasValue && item.Field<DateTime?>("DateClosing").Value < DateTime.Now)
                        {
                            vacancy.TVWebStatus = Status.Expired.ToString("F");
                        }
                        vacancy.TVLastUpdateDate = item.Field<DateTime?>("DateLastUpdated").HasValue ? item.Field<DateTime?>("DateLastUpdated").Value.ToShortDateString() : "";
                        this.trainingVacancies.Add(vacancy);
                    }
                }
                repeaterTV.DataSource = this.trainingVacancies;
                repeaterTV.DataBind();
            }
            catch (Exception ex)
            {
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex);
            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {                
                this.CompanyID = int.Parse(fcompanyid.Value);
                this.CompanyName = fcompanyname.Text;
                this.firmID = this.CompanyID;
                GetTraningVacancies();
                this.editForm.SetCompanyDetails(this.CompanyID, this.CompanyName);
                ViewState["CompanyID"] = this.CompanyID;
                ViewState["CompanyName"] = this.CompanyName;
            }
            catch(Exception ex)
            {
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex);
                this.id = -1;
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            this.id = int.Parse(((LinkButton)sender).CommandArgument);
            this.editForm.id = this.id;
            this.editForm.SetCompanyDetails(this.CompanyID, this.CompanyName);
            this.editForm.Mode = StaffAddTrainingVacanciesForm.Modes.Edit;
            this.editForm.CurrentStep = StaffAddTrainingVacanciesForm.Step.Form;
            this.editForm.LoadTrainingData();
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            this.id = int.Parse(((LinkButton)sender).CommandArgument);
            try
            {
                var parameters = new List<IDataParameter>();
                var sql = $"{AptifyApplication.GetEntityBaseDatabase("WebTrainingVacancies")}..{spTrainingVacancySubmit__cai}";
                var param = DataAction.GetDataParameter("@ID", SqlDbType.Int, this.id);
                parameters.Add(param);

                var dt = Convert.ToInt32(DataAction.ExecuteNonQueryParametrized(sql, CommandType.StoredProcedure,
                    parameters.ToArray()));

                if (dt > 0)
                {
                    this.Success = true;
                    this.lblsuccess.Text = " Training Vacancy submitted.";
                    GetTraningVacancies();
                }
                else
                {
                    this.Error = true;
                    this.lblerror.Text = " Please try again in some minutes.";
                }
            }
            catch (Exception ex)
            {
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex);
                this.Error = true;
                this.lblerror.Text = " Please try again in some minutes.";
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            this.id = int.Parse(((LinkButton)sender).CommandArgument);
            try
            {
                var parameters = new List<IDataParameter>();
                var sql = $"{AptifyApplication.GetEntityBaseDatabase("WebTrainingVacancies")}..{spTrainingVacancyClose__cai}";
                var param = DataAction.GetDataParameter("@ID", SqlDbType.Int, this.id);
                parameters.Add(param);

                var dt = Convert.ToInt32(DataAction.ExecuteNonQueryParametrized(sql, CommandType.StoredProcedure,
                    parameters.ToArray()));

                if (dt > 0)
                {
                    this.Success = true;
                    this.lblsuccess.Text = " Training Vacancy closed.";
                    GetTraningVacancies();
                }
                else
                {
                    this.Error = true;
                    this.lblerror.Text = " Please try again in some minutes.";
                }
            }
            catch (Exception ex)
            {
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex);
                this.Error = true;
                this.lblerror.Text = " Please try again in some minutes.";
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            this.id = int.Parse(((LinkButton)sender).CommandArgument);
            try
            {
                var parameters = new List<IDataParameter>();
                var sql = $"{AptifyApplication.GetEntityBaseDatabase("WebTrainingVacancies")}..{spDeleteWebTrainingVacancies__cai}";
                var param = DataAction.GetDataParameter("@ID", SqlDbType.Int, this.id);
                parameters.Add(param);

                var dt = Convert.ToInt32(DataAction.ExecuteNonQueryParametrized(sql, CommandType.StoredProcedure,
                    parameters.ToArray()));

                if (dt > 0)
                {
                    this.Success = true;
                    this.lblsuccess.Text = " Training Vacancy deleted.";
                    GetTraningVacancies();
                }
                else
                {
                    this.Error = true;
                    this.lblerror.Text = " Please try again in some minutes.";
                }
            }
            catch (Exception ex)
            {
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex);
                this.Error = true;
                this.lblerror.Text = " Please try again in some minutes.";
            }
        }

        protected void btnError_Click(object sender, EventArgs e)
        {
            this.CompanyID = int.Parse(((LinkButton)sender).CommandArgument);
            this.CompanyName = fcompanyname.Text;
        }

        protected void MockData()
        {
            for (var i = 0; i < 10; i++)
            {
                TrainingVacancy item = new TrainingVacancy();
                item.TVID = i;
                item.TVcompanyName = "Test Company Name " + i;
                item.TVJobTitle = "Job Title Test " + i;
                this.trainingVacancies.Add(item);
            }
        }



    }

}
