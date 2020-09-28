using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_CAI_Custom_Controls_FirmDirectoryListing__c : System.Web.UI.UserControl
{
    string itemID;
    public string itemType;


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
        Uri myUri = Request.Url;
        this.itemID = HttpUtility.ParseQueryString(myUri.Query).Get("id");
        this.itemType = HttpUtility.ParseQueryString(myUri.Query).Get("type");
        if (!itemID.IsNullOrEmpty() && !itemType.IsNullOrEmpty() && (itemType == "firm" || itemType == "member"))
        {
            GetData();
        }
    }

    public void GetData()
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection("data source=DEV-ROSS;UID=sa;PWD=Password1;initial catalog=EBusiness"))
        //using (var con = new SqlConnection("Data Source=local;Initial Catalog=OnlineDirectory;Integrated Security=True"))
        {
            con.Open();
            string sql = @"SELECT TOP 1 * FROM [WebsiteData].[dbo].[Firms] WHERE FirmID = @id";
            if (itemType != "firm")
            {
                sql = @"SELECT TOP 1 * FROM [WebsiteData].[dbo].[Members] WHERE ID = @id";
            }
            var command = new SqlCommand(sql, con);
            command.Parameters.AddWithValue("id", itemID);
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);

            if (itemType != "firm")
            {
                SetMemberData(dt);
            }
            else
            {
                SetFirmData(dt);
            }
        }
    }


    public void SetFirmData(DataTable dt)
    {
        foreach (DataRow dr in dt.Rows)
        {
            lblFirmName.Text = dr.Field<string>("BusinessName");
            lblAddressLine1.Text = dr.Field<string>("AddressLine1");
            lblAddressLine2.Text = dr.Field<string>("AddressLine2");
            lblAddressLine3.Text = dr.Field<string>("AddressLine3");
            lblAddressLine4.Text = dr.Field<string>("AddressLine4");
            lblAddressCity.Text = dr.Field<string>("AddressCity");
            lblAddressCounty.Text = dr.Field<string>("AddressCounty");
            lblAddressPostCode.Text = dr.Field<string>("AddressPostCode");
            lblPhone.Text = dr.Field<string>("PhoneNumber");
            lblFax.Text = dr.Field<string>("FaxNumber");
            lblMainEmail.Text = dr.Field<string>("MainEmail");
            lblInfoEmail.Text = dr.Field<string>("InfoEmail");
            lblJobsEmail.Text = dr.Field<string>("JobsEmail");
            lblTrainingEmail.Text = dr.Field<string>("TrainingEmail");
            lblWebsite.Text = dr.Field<string>("Website");
            lblNumberOfEmployees.Text = dr.Field<Int32>("NumberEmployees").ToString();
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
                if(namewcompany.Length > 1){
                    lblMemberCompany.Text = namewcompany[1];
                }else{
                    lblMemberCompany.Text = "Employment information not available.";
                }
                
                lblMemberEmail.Text = dr.Field<string>("Email1");
                lblMemberJoinDate.Text = dr.Field<DateTime>("JoinDate").ToString("dd/MM/yyyy");
            //}
        }
    }

}