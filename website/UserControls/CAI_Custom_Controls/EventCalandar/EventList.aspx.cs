using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;
using Aptify.Framework.Application;
using Aptify.Framework.DataServices;
using Aptify.Framework.BusinessLogic;
using Aptify.Framework.BusinessLogic.GenericEntity;
using Aptify.Framework.Web.eBusiness;
using System.Configuration;
using System.Web.Script.Serialization;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.EventCalandar
{
    public partial class EventList : System.Web.UI.Page 
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {

        }

            public class Event
            {
                public int EventID { get; set; }
                public string EventName { get; set; }
                public string StartDate { get; set; }
                public string EndDate { get; set; }
                public string Location { get; set; }
                public string Description { get; set; }
            }


        
        [WebMethod]
        public static List<Event> GetEvents()
        {
            DataTable dt = new DataTable();
        
            //sql connection 
            SqlConnection con = new SqlConnection("Server="+ ConfigurationManager.AppSettings["AptifyDBServer"] +";Database=" + ConfigurationManager.AppSettings["AptifyEntitiesDB"] +";Trusted_Connection=True");
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


            List<Event> events = new List<Event>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Event ev = new Event();

                ev.EventID = Convert.ToInt32(dt.Rows[i]["EventID"]);
                ev.EventName = dt.Rows[i]["EventName"].ToString();
                ev.StartDate = dt.Rows[i]["StartDate"].ToString();
                ev.EndDate = dt.Rows[i]["EndDate"].ToString();
               

                if (String.IsNullOrEmpty(dt.Rows[i]["Location"].ToString()))
                {
                    ev.Location = "--";
                }
                else
                {
                    ev.Location = dt.Rows[i]["Location"].ToString();
                }
                ev.Description = dt.Rows[i]["Description"].ToString();
                events.Add(ev);
            }
              return events  ;
           
        }


        [WebMethod]
        public static List<Event> GetEventsfilter(string location)
        {
            DataTable dt = new DataTable();

            //sql connection 
            SqlConnection con = new SqlConnection("Server=" + ConfigurationManager.AppSettings["AptifyDBServer"] + ";Database=" + ConfigurationManager.AppSettings["AptifyEntitiesDB"] + ";Trusted_Connection=True");
            {
                using (SqlCommand cmd = new SqlCommand("spGetEventListByLocationByEventTypeByCatagory__cai"))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@loc", location));
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {

                        sda.Fill(dt);
                    }
                }
            }


            List<Event> events = new List<Event>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Event ev = new Event();

                ev.EventID = Convert.ToInt32(dt.Rows[i]["EventID"]);
                ev.EventName = dt.Rows[i]["EventName"].ToString();
                ev.StartDate = dt.Rows[i]["StartDate"].ToString();
                ev.EndDate = dt.Rows[i]["EndDate"].ToString();


                if (String.IsNullOrEmpty(dt.Rows[i]["Location"].ToString()))
                {
                    ev.Location = "--";
                }
                else
                {
                    ev.Location = dt.Rows[i]["Location"].ToString();
                }
                ev.Description = dt.Rows[i]["Description"].ToString();
                events.Add(ev);
            }
            return events;

        }
        [WebMethod]
        public static List<Event> GetEventsfilterloc(string  location, string eventtype, string cattype, string nevent)
        {

            string ostring = "";
            string ustring = "";
            //--" fflag --> Finance, management reporting and analysis "
            //--" gflag --> Governance, risk and legal"
            //--" lflag --> Leadership, management and personal impact"
            //
            string fflag= "0";
            string gflag= "0";
            string lflag ="0";

            ostring = cattype;
            if ((string.Equals(eventtype,"CPD courses ALL A-Z")) && cattype.Contains("Finance, management reporting and analysis"))
            {
                fflag="1";
                //int  indx1 = cattype .IndexOf("Finance, management reporting and analysis");
                //if (indx1!=-1)
                //{ ostring = cattype.Remove(indx1); }
                if (ostring.Contains("Finance, management reporting and analysis,"))
                { ostring = ostring.Replace("Finance, management reporting and analysis,", ""); }
                if (ostring.Contains(",Finance, management reporting and analysis"))
                {ostring = ostring.Replace(",Finance, management reporting and analysis", ""); }
                else if (ostring.Contains("Finance, management reporting and analysis"))
                { ostring = ostring.Replace("Finance, management reporting and analysis", ""); }

               // ostring = cattype.Replace(",Finance, management reporting and analysis", "");
            }
            if ((string.Equals(eventtype,"CPD courses ALL A-Z")) && cattype.Contains("Governance, risk and legal"))
            { 
                gflag ="1";
                if (ostring.Contains("Governance, risk and legal,"))
                { ostring = ostring.Replace("Governance, risk and legal,", ""); }
                if (ostring.Contains(",Governance, risk and legal"))
                { ostring = ostring.Replace(",Governance, risk and legal", ""); }
                else if (ostring.Contains("Governance, risk and legal"))
                { ostring = ostring.Replace("Governance, risk and legal", ""); }

                //ostring = cattype.Replace(",Finance, management reporting and analysis", "");
            }
            if ((string.Equals(eventtype,"CPD courses ALL A-Z")) && cattype.Contains("Leadership, management and personal impact"))
            {
                 lflag ="1";
                 if (ostring.Contains("Leadership, management and personal impact,"))
                 { ostring = ostring.Replace("Leadership, management and personal impact,", ""); }
                 if (ostring.Contains(",Leadership, management and personal impact"))
                 { ostring = ostring.Replace(",Leadership, management and personal impact", ""); }
                 else if (ostring.Contains("Leadership, management and personal impact"))
                 { ostring = ostring.Replace("Leadership, management and personal impact", ""); }
            }
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection("Server=" + ConfigurationManager.AppSettings["AptifyDBServer"] + ";Database=" + ConfigurationManager.AppSettings["AptifyEntitiesDB"] + ";Trusted_Connection=True");
            {
               // using (SqlCommand cmd = new SqlCommand("spGetEventListByLocationByEventTypeByCatagory__cai"))
                using (SqlCommand cmd = new SqlCommand("spGetEventFilter__cai"))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@loc", location));
                    cmd.Parameters.Add(new SqlParameter("@evt", eventtype));
                   // cmd.Parameters.Add(new SqlParameter("@cat", cattype));
                    cmd.Parameters.Add(new SqlParameter("@cat", ostring));
                    cmd.Parameters.Add(new SqlParameter("@nevt", nevent));
                    cmd.Parameters.Add(new SqlParameter("@fflag", fflag));
                    cmd.Parameters.Add(new SqlParameter("@lflag", lflag));
                    cmd.Parameters.Add(new SqlParameter("@gflag", gflag));



                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        
                       
                            sda.Fill(dt);
                       

                    }
                }
            }


            List<Event> events = new List<Event>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Event ev = new Event();

                ev.EventID = Convert.ToInt32(dt.Rows[i]["EventID"]);
                ev.EventName = dt.Rows[i]["EventName"].ToString();
                ev.StartDate = dt.Rows[i]["StartDate"].ToString();
                ev.EndDate = dt.Rows[i]["EndDate"].ToString();


                if (String.IsNullOrEmpty(dt.Rows[i]["Location"].ToString()))
                {
                    ev.Location = "--";
                }
                else
                {
                    ev.Location = dt.Rows[i]["Location"].ToString();
                }
                ev.Description = dt.Rows[i]["Description"].ToString();
                events.Add(ev);
            }
            return events;

        }


       
  }

 }
