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
    public partial class ListTrainingVacancies : Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced                                        
    {

        private static string spGetWebTrainingVacanciesByFirmID__cai => "spGetWebTrainingVacanciesByFirmID__cai";        
        private static string spTrainingVacancySubmit__cai => "spWebTrainingVacancySubmit__cai";
        private static string spTrainingVacancyClose__cai => "spTrainingVacancyClose__cai";
        private static string spDeleteWebTrainingVacancies__cai => "spDeleteWebTrainingVacancies__cai";

        public enum Status { Draft = 1, Submitted = 2, Live = 3, Rejected = 4 , Closed = 5, Expired = 6};

        public long personID = -1;

        public long firmID = -1;        

        public int id = -1;
        
        public int TVID = -1;

        public bool Success = false;

        public bool Error = false;    

        public String CompanyName { get; set; }

        public List<TrainingVacancy> trainingVacancies = new List<TrainingVacancy>();

        public List<TrainingVacancy> TrainingVacancies
        {
            get
            {
                return this.trainingVacancies;
            }
        }






        public void LoadData()
        {

            DataTable dt = new DataTable();

            string Loginpage = "/Login.aspx";
            //string Noaccess = "/securityerror.aspx?Message=Access+to+the+requested+resource+has+been+denied";            
            if (User1.PersonID <= 0)
            {
                Session["ReturnToPage"] = Request.RawUrl;
                Response.Redirect(Loginpage);
            }
            else
            {
                    firmID = User1.CompanyID;                    
                    CompanyName = User1.Company;
                    personID = User1.PersonID;                                   
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            editForm.GetTVID += new AddTrainingVacanciesForm.EditStatusDelegate(ucChild_EditStatusDelegate);
            if (this.id != 0)
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
        }

        void ucChild_EditStatusDelegate(int TVID)
        {            
            if (TVID > 0)
            {
                this.Success = true;
                LoadData();
                if (this.firmID > 0 && this.personID > 0)
                {
                    GetTraningVacancies();
                }
            } else
            {
                this.Error = true;
            }
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
                    for(int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow item = dt.Rows[i];
                        TrainingVacancy vacancy = new TrainingVacancy();
                        vacancy.TVID = item.Field<int>("ID");
                        vacancy.TVcompanyName = item.Field<string>("CompanyName");
                        vacancy.TVJobTitle = item.Field<string>("JobTitle");
                        vacancy.TVIsDraft = "" + item.Field<bool>("IsDraft");
                        vacancy.TVWebStatus = ((Status)item.Field<int>("WebStatus")).ToString("F");
                        vacancy.TVCreationDate = item.Field<DateTime>("DateCreated").ToShortDateString();
                        vacancy.TVClosingDate = item.Field<DateTime?>("DateClosing").HasValue ? item.Field<DateTime?>("DateClosing").Value.ToShortDateString() : DateTime.Now.ToShortDateString() ;
                        if (item.Field<DateTime?>("DateClosing").HasValue  && item.Field<DateTime?>("DateClosing").Value < DateTime.Now )
                        {
                            vacancy.TVWebStatus = Status.Expired.ToString("F");
                        }
                        vacancy.TVLastUpdateDate = item.Field<DateTime?>("DateLastUpdated").HasValue ? item.Field<DateTime?>("DateLastUpdated").Value.ToShortDateString() : "" ;
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

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            this.id = int.Parse(((LinkButton)sender).CommandArgument);
            this.editForm.id = this.id;
            this.editForm.CurrentStep = AddTrainingVacanciesForm.Step.Form;
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
                } else
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
            this.id = int.Parse(((LinkButton)sender).CommandArgument);

        }

        protected void MockData()
        {
            for ( var i = 0; i < 10; i ++ )
            {
                TrainingVacancy item = new TrainingVacancy();
                item.TVID = i;
                item.TVcompanyName = "Test Company Name " + i ;
                item.TVJobTitle = "Job Title Test " + i;
                this.trainingVacancies.Add(item);
            }
        }



    }

}
