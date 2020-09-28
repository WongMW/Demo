using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

public partial class UserControls_CAI_Custom_Controls_JobSearch__c : System.Web.UI.UserControl
    {
        public List<CAI_JobItem> jobs = new List<CAI_JobItem>();
        //public Dictionary<string, string> locations = new Dictionary<string, string>();
        public List<CAI_Location> locations = new List<CAI_Location>();

        public string searchTitle;
        public bool contractSearch = true;
        public bool permanentSearch = true;
        public bool search = false;
        public List<string> searchLocations = new List<string>();

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
            string searchForm = Request.Form["search"];

            if (!searchForm.IsNullOrEmpty())
            {
                search = true;
            }
            if (search)
            {
               searchTitle = Request.Form["ctl00$headerPlaceholder$C001$TextBoxSearchTitle"];
               contractSearch = !Request.Form["ctl00$ctl00$baseTemplatePlaceholder$content$JobSearch__c$contractCheckBox"].IsNullOrEmpty();
               permanentSearch = !Request.Form["ctl00$ctl00$baseTemplatePlaceholder$content$JobSearch__c$permanentCheckBox"].IsNullOrEmpty();
                
                if (!Request.Form["location-select[]"].IsNullOrEmpty())
                {
                    searchLocations = Request.Form["location-select[]"].Split(',').ToList<string>();
                }
            }
            LoadXML("~/App_Data/JSExport.xml");
        }

        public void LoadXML(string xmlLocation)
        {
            XmlDocument doc = new XmlDocument();
            //doc.Load(xmlLocation); change xml file location  to APP_Data 
            doc.Load(Server.MapPath("~/App_Data/JSExport.xml"));
            foreach (XmlElement el in doc.DocumentElement.ChildNodes)
            {
                if (el.Name == "Jobs")
                {
                    int i = 0;
                    foreach (XmlElement e in el.ChildNodes)
                    {
                        string formattedJob = e.InnerText;
                        
                        string replacingText = "\r\n";
                       // string formattedJob = string.Empty;
                        formattedJob = e.InnerText.Replace(replacingText,"");
                        formattedJob = formattedJob.Replace("#SH", "");
                        formattedJob = formattedJob.Replace("#EH", "");
                        formattedJob = formattedJob.Replace("*SB", "");
                        formattedJob = formattedJob.Replace("*EB", "");
                        formattedJob = formattedJob.Replace("\\SL", " ");
                        formattedJob = formattedJob.Replace("\\EL", " ");
                        
                        CAI_JobItem job = new CAI_JobItem(e.GetAttribute("DBID"), e.GetAttribute("Title"), e.GetAttribute("CreatedOn"), e.GetAttribute("Loc"), e.GetAttribute("Term"), formattedJob);
                        jobs.Add(job);

                        i++;
                    }
                }
                if (el.Name == "Locations")
                {
                    int i = 0;
                    foreach (XmlElement e in el.ChildNodes)
                    {
                        locations.Add(new CAI_Location(i, e.GetAttribute("Desc"), e.GetAttribute("DBID")));
                        i++;
                    }
                }

            }
            jobs = initJobs();
        }

        public List<CAI_JobItem> initJobs()
        {
            List<CAI_JobItem> jobList = new List<CAI_JobItem>();
            foreach (CAI_JobItem job in jobs)
            {
                //Setup Location string
                string loc = job.locationID;
                foreach (CAI_Location cai_location in locations)
                {
                    if (loc == cai_location.code)
                    {
                        job.location = cai_location.descrption;
                    }
                }

                bool addToList = true;
                if (search)
                {
                    //If contract is not ticked and the job type is contract don't add
                    if (!contractSearch && job.contractType == "Contract")
                    {
                        addToList = false;
                    }
                    //If permanent is not ticked and the job type is contract don't add
                    if (!permanentSearch && job.contractType == "Permanent")
                    {
                        addToList = false;
                    }
                    //If the search title is set and the job title doesn't contain the search string then don't add
                    if (!searchTitle.IsNullOrEmpty() && !job.title.Contains(searchTitle))
                    {
                        addToList = false;
                    }

                }
                if (addToList)
                {
                    //If the search locations are not empty
                    if (search && searchLocations.Any())
                    {
                        foreach (string searchLocation in searchLocations)
                        {
                            if (searchLocation == job.locationID)
                            {
                                jobList.Add(job);
                            }
                        }
                    }
                    else
                    {
                        jobList.Add(job);
                    }

                }
            }
            return jobList;
        }
    }

    public class CAI_JobItem
    {
        public string id;
        public string title { get; set; }
        public string createdOn { get; set; }
        public string locationID { get; set; }
        public string location { get; set; }
        public string contractTypeID { get; set; }
        public string contractType { get; set; }
        public string description { get; set; }

        public CAI_JobItem(string id, string title, string createdOn, string location, string contractType, string description)
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

    public class CAI_Location
    {

        public int id;
        public string code;
        public string descrption;

        public CAI_Location(int i, string p1, string p2)
        {
            this.id = i;
            this.code = p2;
            this.descrption = p1;
        }
    }

