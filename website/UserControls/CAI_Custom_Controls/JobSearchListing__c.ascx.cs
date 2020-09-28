using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Xml;

public partial class UserControls_CAI_Custom_Controls_JobSearchListing__c : System.Web.UI.UserControl
{
    public List<CAI_JobListItem> jobs = new List<CAI_JobListItem>();
    //public Dictionary<string, string> locations = new Dictionary<string, string>();
    public List<CAI_LocationListItem> locations = new List<CAI_LocationListItem>();

    public string jobId = "0";


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
        this.jobId = HttpUtility.ParseQueryString(myUri.Query).Get("jobID");
        if (!jobId.IsNullOrEmpty())
        {
            LoadXML("~/App_Data/JSExport.xml");
            //doc.Load(Server.MapPath("~/App_Data/JSExport.xml"));
        }

        if (this.jobs.Count <= 0)
        {
            throw new HttpException(404, "Sorry, the page you are looking for cannot be found.");
        }
    }

    public void LoadXML(string xmlLocation)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(Server.MapPath(xmlLocation));
        foreach (XmlElement el in doc.DocumentElement.ChildNodes)
        {
            if (el.Name == "Jobs")
            {
                int i = 0;
                foreach (XmlElement e in el.ChildNodes)
                {
                    if (e.GetAttribute("DBID") == jobId)
                    {
                        string desc = e.InnerText;
                        string replacingText = "\r\n";
                        string formattedJob = string.Empty;
                        formattedJob = e.InnerText.Replace(replacingText, "<br />") + "<br />";
                        formattedJob = formattedJob.Replace("#SH", "<h2>");
                        formattedJob = formattedJob.Replace("#EH", "</h2>");
                        formattedJob = formattedJob.Replace("*SB", "<b>");
                        formattedJob = formattedJob.Replace("*EB", "</b>");
                        formattedJob = formattedJob.Replace("\\SL", "<list>");
                        formattedJob = formattedJob.Replace("\\EL", "</list>");

                        CAI_JobListItem job = new CAI_JobListItem(e.GetAttribute("DBID"), e.GetAttribute("Title"), e.GetAttribute("CreatedOn"), e.GetAttribute("Loc"), e.GetAttribute("Term"), formattedJob);
                        jobs.Add(job);

                        i++;
                    }
                }
            }
            if (el.Name == "Locations")
            {
                int i = 0;
                foreach (XmlElement e in el.ChildNodes)
                {
                    locations.Add(new CAI_LocationListItem(i, e.GetAttribute("Desc"), e.GetAttribute("DBID")));
                    i++;
                }
            }

        }
        jobs = initJobs();

        // now lets initialise title of the page
        if (jobs != null && jobs.Count > 0)
        {
            var job = jobs.First();
            Page.Title = job.title;
        }
    }

    public List<CAI_JobListItem> initJobs()
    {
        List<CAI_JobListItem> jobList = new List<CAI_JobListItem>();
        foreach (CAI_JobListItem job in jobs)
        {
            //Setup Location string
            string loc = job.locationID;
            foreach (CAI_LocationListItem CAI_LocationListItem in locations)
            {
                if (loc == CAI_LocationListItem.code)
                {
                    job.location = CAI_LocationListItem.descrption;
                }
            }
            jobList.Add(job);
        }
        return jobList;
    }
}

public class CAI_JobListItem
{
    public string id;
    public string title { get; set; }
    public string createdOn { get; set; }
    public string locationID { get; set; }
    public string location { get; set; }
    public string contractTypeID { get; set; }
    public string contractType { get; set; }
    public string description { get; set; }

    public CAI_JobListItem(string id, string title, string createdOn, string location, string contractType, string description)
    {
        this.id = id;
        this.title = title;
        this.createdOn = createdOn;
        this.locationID = location;
        this.contractTypeID = contractType;
        if (contractType == "4")
        {
            this.contractType = "Permanent";
        }
        else if (contractType == "6")
        {
            this.contractType = "Contract";
        }
        else
        {
            this.contractType = "Not specified";
        }
        this.description = description;
    }
}

public class CAI_LocationListItem
{

    public int id;
    public string code;
    public string descrption;

    public CAI_LocationListItem(int i, string p1, string p2)
    {
        this.id = i;
        this.code = p2;
        this.descrption = p1;
    }
}
