using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_CAI_Custom_Controls_MentorsSearch : System.Web.UI.UserControl
{
public List<CAI_MentorListItem> Mentors = new List<CAI_MentorListItem>();


    public string searchLoc;

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
        searchLoc = Request.Form["ctl00$contentPlaceholder$C001$TextBoxSearchLoc"];
        if (!searchLoc.IsNullOrEmpty())
        {
            Search(searchLoc);
        }
    }

    public void SearchButton_Click(object sender, EventArgs e)
    {
        //Search();
    }

    private void Search(string searchLoc)
    {
        //Clear any previous results or error messages
        
        using (SqlConnection con = new SqlConnection("data source=DEV-ROSS;UID=sa;PWD=Password1;initial catalog=EBusiness"))
        //using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Sitefinity"].ConnectionString))        
        {
            con.Open();

            SearchDisplayMembers(con, searchLoc);

            con.Close();
        }
    }

    private bool SearchDisplayMembers(SqlConnection con, string searchLoc)
    {
        if (searchLoc.IsNullOrEmpty())
        {
            return false;
        }
        
        DataTable dt = new DataTable();
        string sql = createMentorSearchSQLStmt(searchLoc);
        var command = new SqlCommand(sql, con);
        command.Parameters.AddWithValue("location", searchLoc);
        SqlDataAdapter da = new SqlDataAdapter(command);
        da.Fill(dt);
        if (!(dt.Rows.Count < 1))
        {
            foreach (DataRow dr in dt.Rows)
            {
                string name = "Mentor(" + dr.Field<int>("ID") + ") in  " + dr.Field<string>("City") + ", " + dr.Field<string>("County") + ", " + dr.Field<string>("Country");
                CAI_MentorListItem mentor = new CAI_MentorListItem(name, dr.Field<int>("ID"));
                Mentors.Add(mentor);
            }
        }
        else
        {
            Label message = new Label();
            message.Text = "No mentors found in \"" + searchLoc + "\"";
            //plcMembersSearchResults.Controls.Add(message);
        }
        return true;
    }

    private string createMentorSearchSQLStmt(string location)
    {
        string sql = sql = @"SELECT m.ID, adr.City, adr.County, adr.Country FROM [WebsiteData].[dbo].Members m" +
                    "FULL JOIN [WebsiteData].[dbo].[MembersAddress] adr on adr.ID = m.AddressID" +
                    "WHERE  isMentor = 1";
        if (!searchLoc.IsNullOrEmpty())
        {
            sql = sql + @"AND adr.County LIKE '%'+@location+'%' ";

        }
        return sql;
    }
}

public class CAI_MentorListItem
{
    public int itemID;
    public string itemName;

    public CAI_MentorListItem(string firmName, int id)
    {
        this.itemName = firmName;
        this.itemID = id;
    }

    public void SetData(string firmName, int id)
    {
        this.itemName = firmName;
        this.itemID = id;
    }
}