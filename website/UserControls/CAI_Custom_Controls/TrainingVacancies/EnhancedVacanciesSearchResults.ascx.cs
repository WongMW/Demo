using Aptify.Framework.DataServices;
using SitefinityWebApp.UserControls.CAI_Custom_Controls.FirmsSearch;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.TrainingVacancies
{
    public partial class EnhancedVacanciesSearchResults : Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
    {
        private static int pageSize = 50;

        public IList<TrainingVacancy> TrainingVacancies = new List<TrainingVacancy>();

        public class TrainingVacancy
        {
            public int ID;
            public string JobName;
        }

               private void doSearch()
        {
            mockupData();
        }

        private void mockupData()
        {
            TrainingVacancy item;
            for ( var i = 0; i < 10; i++)
            {
                item = new TrainingVacancy();
                item.ID = i;
                item.JobName = "Job Name Test Item " + i;
                this.TrainingVacancies.Add(item);
            }
        }
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.EnableViewState = true;                
            }
        }

        protected void btnSearchFirms_click(object sender, EventArgs e)
        {
            doSearch();
            fLocation.Text = fLocation.Text;
        }

    }
}
