using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class UserControls_CAI_Custom_Controls_FirmDirectory : System.Web.UI.UserControl
{

    public List<CAI_FirmListItem> Firms = new List<CAI_FirmListItem>();
    public List<CAI_FirmListItem> Members = new List<CAI_FirmListItem>();

    public string searchName;
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
        
        searchName = Request.Form["ctl00$contentPlaceholder$C001$TextBoxSearchName"];
        searchLoc = Request.Form["ctl00$contentPlaceholder$C001$TextBoxSearchLoc"];
        if (!searchName.IsNullOrEmpty() || !searchLoc.IsNullOrEmpty())
        {
            Search(searchName, searchLoc);
        }
        
        
    }

    public void SearchButton_Click(object sender, EventArgs e)
    {
        //Search();
    }

    private void Search(string searchName, string searchLoc)
    {
        //Clear any previous results or error messages
        
        using (SqlConnection con = new SqlConnection("data source=DEV-ROSS;UID=sa;PWD=Password1;initial catalog=EBusiness"))
        //using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Sitefinity"].ConnectionString))        
        {
            con.Open();
            if (chkBoxIncFirms.Checked)
            {
                SearchDisplayFirms(con, searchName, searchLoc);
            }
            else
            {
                Label message = new Label();
                message.Text = "Firms not included in search.";
                //plcFirmsSearchResults.Controls.Add(message);
            }
            if (chkBoxIncMembers.Checked)
            {
                SearchDisplayMembers(con, searchName, searchLoc);
            }
            else
            {
                Label message = new Label();
                message.Text = "Members not included in search.";
                //plcMembersSearchResults.Controls.Add(message);
            }
            con.Close();
        }
    }

    private void SearchDisplayFirms(SqlConnection con, string searchName, string searchLoc)
    {

        DataTable dt = new DataTable();
        string sql = createFirmSearchSQLStmt();
        var command = new SqlCommand(sql, con);
        command.Parameters.AddWithValue("name", searchName);
        command.Parameters.AddWithValue("county", searchLoc);
        SqlDataAdapter da = new SqlDataAdapter(command);
        da.Fill(dt);
        if (!(dt.Rows.Count < 1))
        {
            foreach (DataRow dr in dt.Rows)
            {
                //UserControls_CAI_Custom_Controls_FirmDirectoryListItem__c firmListItem = (UserControls_CAI_Custom_Controls_FirmDirectoryListItem__c)LoadControl("~/UserControls/CAI_Custom_Controls/FirmDirectoryListItem__c.ascx");
                //firmListItem.SetData(dr.Field<string>("BusinessName"), dr.Field<int>("FirmID"));
                //plcFirmsSearchResults.Controls.Add(firmListItem);
                CAI_FirmListItem firm = new CAI_FirmListItem(dr.Field<string>("BusinessName"), dr.Field<int>("FirmID"));
                Firms.Add(firm);
            }
        }
        else
        {
            Label message = new Label();
            message.Text = "No firms found.";
            if (!searchName.IsNullOrEmpty())
            {
                message.Text = "No firms found with the name \"" + searchName + "\"";
            }
            if (!searchLoc.IsNullOrEmpty() && !searchName.IsNullOrEmpty())
            {
                message.Text = "No firms found with the name \"" + searchName + "\" in " + searchLoc;
            }
            //plcFirmsSearchResults.Controls.Add(message);
        }
    }

    private bool SearchDisplayMembers(SqlConnection con, string searchName, string searchLoc)
    {
        if (searchName.IsNullOrEmpty())
        {
            return false;
        }
        string firstname = searchName;
        string lastname = searchName;
        if(searchName.Contains(" ")){
            char[] delims = { ' ' };
            string[] names = searchName.Split(delims);
            //Take out the last name
            lastname = names[names.Length - 1];
            firstname = "";
            int count = 0;
            //Loop through the search words to append all of them together to be readable the SQL command
            foreach (string name in names)
            {
                //We don't want to append the last name to our first name string
                if (name != lastname)
                {
                    //We've got than on first name
                    if (count > 0)
                    {
                        firstname = " " + name;
                    }
                    else
                    {
                        firstname = name;
                    }
                }
                count++;
            }
            this.searchName = "FirstName: " + firstname + ", lastName: " + lastname;
        }

        DataTable dt = new DataTable();
        string sql = CreateMemberSearchSQLStmt(firstname, lastname);
        var command = new SqlCommand(sql, con);
        command.Parameters.AddWithValue("name", firstname);
        command.Parameters.AddWithValue("lastname", lastname);
        //command.Parameters.AddWithValue("county", txtboxSearchLocation.Text);
        SqlDataAdapter da = new SqlDataAdapter(command);
        da.Fill(dt);
        if (!(dt.Rows.Count < 1))
        {
            foreach (DataRow dr in dt.Rows)
            {
                //UserControls_CAI_Custom_Controls_FirmDirectoryListItem__c firmListItem = (UserControls_CAI_Custom_Controls_FirmDirectoryListItem__c)LoadControl("~/UserControls/CAI_Custom_Controls/FirmDirectoryListItem__c.ascx");
                //CAI_FirmListItem firmListItem = new CAI_FirmListItem();
                //string name = dr.Field<string>("FirstName") + " " + dr.Field<string>("LastName");
                //firmListItem.SetData(name, dr.Field<int>("ID"));
                //plcMembersSearchResults.Controls.Add(firmListItem);
                string name = dr.Field<string>("FirstName") + " " + dr.Field<string>("LastName");
                CAI_FirmListItem firm = new CAI_FirmListItem(name, dr.Field<int>("ID"));
                Members.Add(firm);
            }
        }
        else
        {
            Label message = new Label();
            message.Text = "No members found with the name \"" + searchName + "\"";
            //plcMembersSearchResults.Controls.Add(message);
        }
        return true;
    }

    private string createFirmSearchSQLStmt(){
        string sql = @"SELECT * FROM [WebsiteData].[dbo].[Firms] WHERE 1=1 ";
        if(!searchName.IsNullOrEmpty()){
            sql = sql + @"AND [BusinessName] LIKE '%'+@name+'%' ";
        }
        if (!searchLoc.IsNullOrEmpty())
        {
            sql = sql + @"AND [AddressCounty] LIKE '%'+@county+'%' ";
        }
        sql = sql + @"AND DirectoryStatus = 'Visible' ";
        return sql;
    }

    private string CreateMemberSearchSQLStmt(string firstname, string lastname)
    {
        string sql = "";
        if (!lastname.IsNullOrEmpty())
        {
            sql = @"SELECT ID, FirstName, LastName FROM [WebsiteData].[dbo].[Members] " +
                       "WHERE MemberTypeID > 1 AND MemberTypeID < 5  " +
                       "AND (LastName LIKE '%'+@lastname+'%') ";
        }
        if (!firstname.IsNullOrEmpty())
        {
            sql = @"SELECT ID, FirstName, LastName FROM [WebsiteData].[dbo].[Members] " +
                       "WHERE MemberTypeID > 1 AND MemberTypeID < 5  " +
                       "AND (FirstName LIKE '%'+@name+'%') ";
        }
        if (!firstname.IsNullOrEmpty() && !lastname.IsNullOrEmpty())
        {
            string opr = "AND";
            if (firstname == lastname)
            {
                opr = "OR";
            }

            sql = @"SELECT ID, FirstName, LastName FROM [WebsiteData].[dbo].[Members] " +
                            "WHERE MemberTypeID > 1 AND MemberTypeID < 5  " +
                            "AND (LastName LIKE '%'+@lastname+'%' " + opr + " FirstName LIKE '%'+@name+'%') ";
        }
        return sql;
    }

}

public class CAI_FirmListItem
{
    public int itemID;
    public string itemName;

    public CAI_FirmListItem(string firmName, int id)
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