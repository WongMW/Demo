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
    public partial class StaffPendingTrainingVacancies : Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced                                        
    {

        private static string spGetWebTrainingVacanciesPending__cai => "spGetWebTrainingVacanciesPending__cai";
        private static string spTrainingVacancyApprove__cai => "spWebTrainingVacancyApprove__cai";
        private static string spTrainingVacancyReject__cai => "spWebTrainingVacancyReject__cai";
       
        public enum Status { Draft = 1, Submitted = 2, Live = 3, Rejected = 4 , Closed = 5, Expired = 6};

        public string PersonName;

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

            if ( User1 != null )
            {
                this.PersonName = User1.FirstName + ' ' + User1.LastName;
                GetTraningVacancies();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
        }



        protected void GetTraningVacancies()
        {
            var parameters = new List<IDataParameter>();
            this.trainingVacancies = new List<TrainingVacancy>();
            try
            {
                var sql = $"{AptifyApplication.GetEntityBaseDatabase("Firm")}..{spGetWebTrainingVacanciesPending__cai}";
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

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            this.id = int.Parse(((LinkButton)sender).CommandArgument);
            try
            {
                var parameters = new List<IDataParameter>();
                var sql = $"{AptifyApplication.GetEntityBaseDatabase("WebTrainingVacancies")}..{spTrainingVacancyApprove__cai}";
                parameters.Add(DataAction.GetDataParameter("@ID", SqlDbType.Int, this.id));
                parameters.Add(DataAction.GetDataParameter("@ApprovedBy", SqlDbType.NVarChar, User1.FirstName + ' ' + User1.LastName));

                var dt = Convert.ToInt32(DataAction.ExecuteNonQueryParametrized(sql, CommandType.StoredProcedure,
                    parameters.ToArray()));

                if (dt > 0)
                {
                    this.Success = true;
                    this.lblsuccess.Text = " Training Vacancy approved.";
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

        protected void btnView_Click(object sender, EventArgs e)
        {
            this.id = int.Parse(((LinkButton)sender).CommandArgument);
            
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            this.id = int.Parse(((LinkButton)sender).CommandArgument);
            try
            {
                var parameters = new List<IDataParameter>();
                var sql = $"{AptifyApplication.GetEntityBaseDatabase("WebTrainingVacancies")}..{spTrainingVacancyReject__cai}";
                parameters.Add(DataAction.GetDataParameter("@ID", SqlDbType.Int, this.id));
                parameters.Add(DataAction.GetDataParameter("@ApprovedBy", SqlDbType.NVarChar, "Unknown User"));

                var dt = Convert.ToInt32(DataAction.ExecuteNonQueryParametrized(sql, CommandType.StoredProcedure,
                    parameters.ToArray()));

                if (dt > 0)
                {
                    this.Success = true;
                    this.lblsuccess.Text = " Training Vacancy rejected.";
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
