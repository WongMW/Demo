using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

public partial class UserControls_CAI_Custom_Controls_EventCalendar__c : System.Web.UI.UserControl
{

    public List<CAI_Event> events = new List<CAI_Event>();
    
    public int month;
    public int year;


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
        
        //If the month/year is passed then set the defualt calendar date as those else set to today
        if (!Request.QueryString["caldatem"].IsNullOrEmpty() && !Request.QueryString["caldatey"].IsNullOrEmpty() && !Request.QueryString["caldateaction"].IsNullOrEmpty())
        {       
            string monthFull = Request.QueryString["caldatem"];
            string yearFull = Request.QueryString["caldatey"];
            string action = Request.QueryString["caldateaction"];
            month = DateTime.ParseExact(monthFull, "MMMM", CultureInfo.InvariantCulture).Month;
            year = Convert.ToInt32(yearFull);
            if (action == "prev")
            {
                month--;
            }
            else if (action == "next")
            {
                month++;
            }
        }
        else
        {
            year = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
            month = Convert.ToInt32(DateTime.Now.ToString("MM").Trim()); 
        }
        using (SqlConnection con = new SqlConnection("data source=DEV-ROSS;UID=sa;PWD=Password1;initial catalog=EBusiness"))
        //using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Sitefinity"].ConnectionString))        
        {
            con.Open();

            DataTable dt = new DataTable();
            string sql = "SELECT [APTIFY].[dbo].[Meeting].ID, ProductID, MeetingTypeID, StartDate, EndDate, MeetingTitle, VerboseDescription, [APTIFY].[dbo].[Venue].Name as Venue" 
                          +" FROM [APTIFY].[dbo].[Meeting]"
                          +" FULL JOIN [APTIFY].[dbo].[Venue] ON [APTIFY].[dbo].[Meeting].VenueID = [APTIFY].[dbo].[Venue].ID"
                          +" WHERE MeetingTypeID IN (4, 7, 8, 9)"
                          + " AND StartDate > DATEADD(month, -1,  @date)"
                          + "AND StartDate < DATEADD(month, 2, @date)"
                          +" ORDER BY StartDate";
            
            var command = new SqlCommand(sql, con);
            string today = year + "-" + month + "-01";
            command.Parameters.Add("@date", SqlDbType.DateTime);
            command.Parameters["@date"].Value = today;

            SqlDataAdapter da = new SqlDataAdapter(command);
            try
            {
                da.Fill(dt);
            }
            catch(Exception exception)
            {
                //No events
            }
            if (!(dt.Rows.Count < 1))
            {
                foreach (DataRow dr in dt.Rows)
                {
                   //firmListItem.SetData(dr.Field<string>("BusinessName"), dr.Field<int>("FirmID"));
                    events.Add(new CAI_Event(
                        dr.Field<int>("ID").ToString(),
                        dr.Field<int>("ProductID").ToString(),
                        dr.Field<string>("MeetingTitle"),
                        dr.Field<DateTime>("StartDate"),
                        dr.Field<string>("VerboseDescription"), 
                        "no url",
                        dr.Field<DateTime>("EndDate"),
                        dr.Field<int>("MeetingTypeID").ToString(), 
                        dr.Field<string>("Venue"),
                        dr.Field<int>("MeetingTypeID")));
                        
                }
            }

            con.Close();
        }

    }


}

public class CAI_Event
{
    public string id;
    public string ProductID;
    public string name;
    DateTime date;
    public string description;
    public string url;
    DateTime endDate;
    public string eventType;
    public string venue;
    public int meetingType;

    public string GetFormattedStartDay()
    {
        return date.ToString("dd",
                  CultureInfo.InvariantCulture);
    }

    public string GetFormattedStartMonth()
    {
        return date.ToString("MMM",
                  CultureInfo.InvariantCulture);
    }

    public string GetFormattedStartWeekDay()
    {
        return date.ToString("ddd",
                  CultureInfo.InvariantCulture);
    }

    public string GetShortStartDate()
    {
        string fdate = date.ToString("yyyy-MM-dd", 
                  CultureInfo.InvariantCulture);
        return fdate;
    }

    public string GetFormattedEndDate()
    {
        string fdate = endDate.ToString("MMM d yyyy",
                 CultureInfo.InvariantCulture);
        return fdate;
    }


    public CAI_Event(string id, string ProductID, string name, DateTime date, string desc, string url, DateTime endDate, string type, string venue, int meetingType)
    {
        this.id = id;
        this.ProductID = ProductID;
        this.name = name;
        this.date = date;
        this.description = desc;
        this.url = url;
        this.endDate = endDate;
        this.eventType = type;
        this.venue = venue;
        this.meetingType = meetingType;
    }

}