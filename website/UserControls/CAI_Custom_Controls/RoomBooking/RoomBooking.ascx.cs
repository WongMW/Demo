using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.RoomBooking
{
    public struct Room
    {
        public String ID { get; set; }
        public String StartTime { get; set; }
        public String EndTime { get; set; }
        public String StartDate { get; set; }
        public String EndDate { get; set; }
        public String MeetingTitle { get; set; }
        public String AssignedRoom { get; set; }
        public String RoomType { get; set; }
    }

    public partial class RoomBooking : Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
    {
        private const string spVwRoomBookingApplicationsForTodayByVenue = "spVwRoomBookingApplicationsForTodayByVenue__cai";

        public String Venue { get; set; }
        public int RefreshInterval { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Page.IsBackend())
            {
                lblVenueIndicator.Visible = true;
                if(String.IsNullOrEmpty(Venue))
                {
                    lblVenueIndicator.Text = "Please set Venue property in the settings of the widget!";
                } else
                {
                    lblVenueIndicator.Text = "Room Booking Venue: " + Venue;
                }
            } else if(!IsPostBack && !String.IsNullOrEmpty(Venue))
            {
                pnlRoomBooking.Visible = true;

                Bind();
            }
        }

        private void Bind()
        {
            var parameters = new List<IDataParameter>();
            parameters.Add(DataAction.GetDataParameter("@Venue", SqlDbType.NVarChar, Venue));
            var sql = $"{AptifyApplication.GetEntityBaseDatabase("RoomBookingApplications")}..{spVwRoomBookingApplicationsForTodayByVenue}";
            var dt = DataAction.GetDataTableParametrized(sql, CommandType.StoredProcedure, parameters.ToArray());

            List<Room> list = new List<Room>();
            foreach (DataRow r in dt.Rows)
            {
                var obj = new Room()
                {
                    ID = r["ID"].ToString(),
                    StartTime = r["MeetingStartTime"].ToString(),
                    EndTime = r["MeetingEndTime"].ToString(),
                    MeetingTitle = r["MeetingTitle"].ToString(),
                    AssignedRoom = r["AssignedRoom"].ToString(),
                    RoomType = r["RoomType"].ToString(),
                    StartDate = r["StartDate"].ToString(),
                    EndDate = r["EndDate"].ToString()
                };

                // lets check if start time is not passed further than 30 minutes
                var startDate = DateTime.Parse(DateTime.Parse(obj.StartDate).ToString("dd/MM/yyyy") + " " + obj.StartTime).AddMinutes(120);

                if(DateTime.Now < startDate)
                    list.Add(obj);
            }

            roomBookingRepeater.DataSource = list;
            roomBookingRepeater.DataBind();
        }

        protected void roomBookingRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                HtmlTableRow noItems = (HtmlTableRow)e.Item.FindControl("no_items");

                if (roomBookingRepeater.Items.Count < 1)
                {
                    noItems.Visible = true;
                } else
                {
                    noItems.Visible = false;
                }
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Bind();
        }
    }
}
