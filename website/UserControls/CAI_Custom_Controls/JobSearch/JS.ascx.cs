using System;
using System.Data;
using System.Data.SqlClient;
using Telerik.Web.UI;
using SoftwareDesign;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.JobSearch
{
    public partial class JS : System.Web.UI.UserControl
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            //var scriptManager = ScriptManager.GetCurrent(Page);

            //if (scriptManager == null) return;

            //scriptManager.Scripts.Add(new ScriptReference { Path = "~/Scripts/InHouse/bootstrap.min.js" });
        }



        protected void Page_Load(object sender, EventArgs e)
        {

            //if(!System.String.IsNullOrEmpty(Request.Form["cbs"]))
            //{
            //    L1.Text = "CBS";
            //}
            //if (!System.String.IsNullOrEmpty(Request.Form["c1"]))
            //{
            //    L1.Text = "c1";
            //}

            //if (!Page.IsPostBack)
            //{
            //    //c1.Checked = true;
            //    //c2.Checked = true;
            //    //c3.Checked = true;
            //}
            //else


            //if (c1.Checked)
            //{ c1.Checked = true; }
            //else
            //{ c1.Checked = false; }
            //if (c2.Checked)
            //{ c2.Checked = true; }
            //else
            //{ c2.Checked = false; }
            //if (c3.Checked)
            //{ c3.Checked = true; }
            //else
            //{ c3.Checked = false; }



            if (!IsPostBack)
            {
                LoadDataForRadGrid1();
            }
        }

        private void LoadDataForRadGrid1()
        {
            RadGrid1.DataSource = GetDataTable();
        }

        protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = GetDataTable();
        }

        public DataTable GetDataTable()


        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection(Helper.GetAptifyEntitiesConnectionString());
            {
                
                using (SqlCommand cmd = new SqlCommand("spGetEventList__cai"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {

                        sda.Fill(dt);
                    }
                }
            }

            return dt;
        }
    }
   
    
}