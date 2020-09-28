using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aptify.Framework.Application;
using Aptify.Framework.DataServices;
using Aptify.Framework.BusinessLogic.Security;
using Aptify.Framework.BusinessLogic.GenericEntity;
using Aptify.Framework;
using Telerik.Web.UI;
using System.Text;
using System.Activities.Expressions;
using Aptify.Framework.Web.Common;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.FirmsSearch
{

    public partial class EnhancedListingEditForm : Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
    {
        private static string spGetEnhancedFirmDeatils__cai => "spGetEnhancedListFirmDeatils__cai";
        private static string spGetFirmsbyFirmAdminByPersonId__cai => "spGetFirmsbyFirmAdminByPersonId__cai";
        private static string spGetFirmNameFullAddressByFirmId__cai => "spGetFirmNameFullAddressByFirmId__cai";
        private static string spGetPerson => "spGetPerson";
        private static string spgetFirmSubOfficesNameAddressByFirmid__cai => "spgetFirmSubOfficesNameAddressByFirmid__cai ";


        public List<FirmDetails> flist = new List<FirmDetails>();
        public List<string> Sites = new List<string> { "StackOverflow", "Super User", "Meta SO" };

        //override protected void OnInit(EventArgs e)
        //{
        //    base.OnInit(e);
        //    R3.ItemCommand += new RepeaterCommandEventHandler(R3_ItemCommand);
        //}
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!this.IsPostBack)
            {
                GetFirmDeatils();
                LoadFirms();

				// test repeater
				createRepeater();


			}


        }

        protected void btnupload_Click(object sender, EventArgs e)
        {
            try
            {
                var allowedExtensions = new[]
                { ".jpg", ".jpeg", ".gif", ".jpe", ".png",".JPG", ".JPEG", ".GIF", ".JPE", ".PNG" };
                string filename = Path.GetFileName(fu.PostedFile.FileName);
                var ext = Path.GetExtension(fu.PostedFile.FileName);
                // Get teh firmId of the Firm
                string firmId = "111";
                string sfilename = firmId;
                string folderPath = Server.MapPath("~/FImages/" + firmId);
                if (!allowedExtensions.Contains(ext))
                {
                    //Check file extension
                    ful.Text = "File Extension Is InValid";


                }
                // file size more than 2MB
                else if (fu.PostedFile.ContentLength > 2 * 1024 * 1024)
                {
                    ful.Text = "File size is too large";


                }


                else
                    if (!Directory.Exists(folderPath))
                {
                    // If Directory(Folder) does not exists.Create it.
                    Directory.CreateDirectory(folderPath);
                }
                // resize the image into 600*600
                Stream strm = fu.PostedFile.InputStream;
                var targetFile = Server.MapPath("~/App_Data/FirmImages/" + firmId + "/" + firmId + ".jpeg");
                //Based on scalefactor image size will vary  
                GenerateThumbnails(0.5, strm, targetFile);


                //Save the File to the Directory (Folder).
                //fu.SaveAs(folderPath + "/" + firmId + ".jpeg");
                //ReduceImageSize(0.5, strm, targetFile);
                //insert reduced size image in db  
            }
            catch (Exception ex)
            {
                ful.Text = ex.Message.ToString();
            }
        }
        private void GenerateThumbnails(double scaleFactor, Stream sourcePath, string targetPath)
        {
            using (var image = System.Drawing.Image.FromStream(sourcePath))
            {

                int ih = 600;
                int iw = 600;
                // can given width of image as we want  
                //var newWidth = (int)(image.Width * scaleFactor);
                // can given height of image as we want  
                // var newHeight = (int)(image.Height * scaleFactor);
                var newWidth = iw;
                var newHeight = ih;
                var thumbnailImg = new Bitmap(newWidth, newHeight);
                var thumbGraph = Graphics.FromImage(thumbnailImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imageRectangle);
                thumbnailImg.Save(targetPath, image.RawFormat);
            }
        }
        protected void BindData()
        { }

        protected void btnSaveListing_Click(object sender, EventArgs e)
        {
            // Save specialisation & industry sector code to save DB
            Boolean btc = false;
            // btc = InsertTopicCode(txtFirstName.Text.Trim(), txtLastName.Text.Trim(), txtEmail.Text.Trim())
        }

        // 
        private bool InsertTopicCode(string fn, string ln, string em)
        {
            bool rvalue = false;
            try
            {
                // call SP to check person Id 

                string sSql = string.Empty;
                string rname = string.Empty;
                string err = "";
                rname = string.Concat(fn, " ", ln);
                System.Data.IDataParameter[] param = new IDataParameter[3];
                sSql = Database + "..spGetPersonIdByFnameLnameEmail__cai";
                param[0] = DataAction.GetDataParameter("@fname", SqlDbType.NVarChar, fn);
                param[1] = DataAction.GetDataParameter("@lname", SqlDbType.NVarChar, ln);
                param[2] = DataAction.GetDataParameter("@email", SqlDbType.NVarChar, em);
                int pid = Convert.ToInt32(DataAction.ExecuteScalarParametrized(sSql, CommandType.StoredProcedure, param));
                if (pid > 0)
                    // Add topic code to link
                    rvalue = AddTopicCodeLink(pid, rname, ref err);
            }
            catch (Exception ex)
            {
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex);
                rvalue = false;
            }
            return rvalue;
        }

        protected virtual bool AddTopicCodeLink(int rid, string rname, ref string ErrorString)
        {
            AptifyGenericEntityBase oLink;
            long TopicCodeID = 1335;
            // string ErrorString = string.Empty;

            try
            {
                oLink = AptifyApplication.GetEntityObject("Topic Code Links", -1);
                oLink.SetValue("TopicCodeID", TopicCodeID);
                oLink.SetValue("RecordID", rid);
                oLink.SetValue("EntityID", AptifyApplication.GetEntityID("Persons"));
                oLink.SetValue("Status", "Active");
                oLink.SetValue("Value", "Yes");
                oLink.SetValue("DateAdded", DateTime.Today);
                return oLink.Save(false, ref ErrorString);
            }

            catch (Exception ex)
            {
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex);
                return false;
            }
        }

        private bool SaveEnhancedList()
        {

            AptifyGenericEntityBase oel;
            oel = AptifyApplication.GetEntityObject("EnhancedListingEditForm__cai", -1);

            return false;
        }


        public void GetFirmDeatils()
        {


            //StringBuilder sqlCheckUser = new StringBuilder();
            //sqlCheckUser.AppendFormat("select FirmId from EnhancedListingEditForm__cai");
            //DataTable result = DataAction.GetDataTable(sqlCheckUser.ToString());
            // DataTable dt1;

            //var parameters = new List<IDataParameter>();
            try
            {
                //var sql = $"{AptifyApplication.GetEntityBaseDatabase("Products")}..{spGetFirmsbyFirmAdminByPersonId__cai}";
                //var param = DataAction.GetDataParameter("@PId", SqlDbType.Int, 55928);
                //parameters.Add(param);
                //DataTable dt;
                //dt = DataAction.GetDataTableParametrized(sql, CommandType.StoredProcedure,
                //    parameters.ToArray());

                //string ssql = "";
                //DataTable dt;
                //ssql = "select FirmId,Firmname from  " + AptifyApplication.GetEntityBaseDatabase("EnhancedListingEditForm__cai") + " where Firmid in ('43973,46224')";
                //dt = DataAction.GetDataTable(ssql, Aptify.Framework.DataServices.IAptifyDataAction.DSLCacheSetting.BypassCache);

                // GetFirmsList(dt);

                DataTable dt = new DataTable();

                // Two columns.
                dt.Columns.Add("FirmId", typeof(string));
                dt.Columns.Add("FirmName", typeof(string));

                // ... Add two rows.
                dt.Rows.Add("43973", "Sheil Kinnear Limited, Sinnottstown Business Park, Drinagh, Ireland");
                dt.Rows.Add("46224", "Quinlivan & Co, Sinnottstown Business Park, Drinagh, Ireland");

                // ... Display first field.

                if (dt.Rows.Count > 0)
                {
                    //g1.DataSource = dt;
                    //g1.DataBind();

                }

                //  return dt;

            }
            catch (Exception ex)
            {
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex);
            }

            //return null;



        }

       

        private string GetFirmDetails(int fid)
        {

            var parameters = new List<IDataParameter>();
            try
            {
                var sql = $"{AptifyApplication.GetEntityBaseDatabase("Firm")}..{spGetFirmNameFullAddressByFirmId__cai}";
                var param = DataAction.GetDataParameter("@fId", SqlDbType.Int, fid);
                parameters.Add(param);

                var dt = DataAction.GetDataTableParametrized(sql, CommandType.StoredProcedure,
                    parameters.ToArray());

                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["fname"].ToString();
                }
            }
            catch (Exception ex)
            {
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex);
            }

            return "null";
        }


       
		

       


        public void LoadFirms()
        {

            DataTable fdt = new DataTable();
            DataTable sodt = new DataTable();
            // devlopment purpose only 
            // User1.PersonID = 55928;
            User1.PersonID = 40063;
            //User1.PersonID = 40063;

            string Loginpage = "/Login.aspx";

            if (User1.PersonID <= 0)
            {
                Response.Redirect(Loginpage);
            }
            else
            {
                // get userdeatils  FirstLast & firmname with address 
                GetUserDetails();

                fdt = GetFirmAdminDetails();
                if (fdt.Rows.Count > 0)
                {
                    // 
                    GetFirms(fdt);


                }
                else
                { }

                // odt = GetFirmSubOffices()
            }
        }

        private void GetUserDetails()
        {
            var parameters = new List<IDataParameter>();
            try
            {
                var sql = $"{AptifyApplication.GetEntityBaseDatabase("Person")}..{spGetPerson}";
                var param = DataAction.GetDataParameter("@ID", SqlDbType.Int, User1.PersonID);
                parameters.Add(param);

                var dt = DataAction.GetDataTableParametrized(sql, CommandType.StoredProcedure,
                    parameters.ToArray());

                if (dt.Rows.Count > 0)
                {
                    //lName.Text = dt.Rows[0]["FirstLast"].ToString();
                    //lFirmName.Text = GetFirmDetails(Convert.ToInt32(dt.Rows[0]["CompanyID"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex);
            }
        }


        private DataTable GetFirmAdminDetails()
        {
            var parameters = new List<IDataParameter>();
            try
            {
                var sql = $"{AptifyApplication.GetEntityBaseDatabase("Products")}..{spGetFirmsbyFirmAdminByPersonId__cai}";
                var param = DataAction.GetDataParameter("@PId", SqlDbType.Int, User1.PersonID);
                parameters.Add(param);

                var dt = DataAction.GetDataTableParametrized(sql, CommandType.StoredProcedure,
                    parameters.ToArray());

                return dt;
            }
            catch (Exception ex)
            {
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex);
            }

            return null;
        }

        public void GetFirms(DataTable fpdt)
        {
            DataTable dty = new DataTable();
            dty.Columns.Add("fid");
            dty.Columns.Add("fname");

            FirmDetails fdet = new FirmDetails();

            foreach (DataRow dr in fpdt.Rows)
            {
                DataRow row = dty.NewRow();
                row["fid"] = dr["fid"];
                row["fname"] = GetFirmDetails(Convert.ToInt32(dr["fid"].ToString()));
                dty.Rows.Add(row);
                //fdet.Fid = Convert.ToInt32(dr["fid"].ToString());
                //fdet.Fname = GetFirmDetails(Convert.ToInt32(dr["fid"].ToString()));
                //Firms.Add(fdet);
            }
            // inject to subofficeswith parent office 
            // GetfirmswithSubOffices(dty);

            if (dty.Rows.Count > 0)
            {
                Session["dof"] = dty;

                R3.DataSource = dty;
                R3.ItemDataBound += new RepeaterItemEventHandler(R3_ItemDataBound);
                R3.DataBind();

            }



        }

        public void GetfirmswithSubOffices(DataTable xdt)
        {

            DataTable dtz = new DataTable();
            dtz.Columns.Add("fid");
            dtz.Columns.Add("fname");
            dtz.Columns.Add("pc");
            DataTable dts = new DataTable();

            FirmDetails fdetx = new FirmDetails();


            foreach (DataRow dr in xdt.Rows)
            {
                DataRow row = dtz.NewRow();
                row["fid"] = dr["fid"];
                row["fname"] = GetFirmDetails(Convert.ToInt32(dr["fid"].ToString()));
                row["pc"] = "Parent office";
                dtz.Rows.Add(row);


                if (dts != null && dts.Rows.Count > 0)
                {
                    dts.Clear();
                }
                dts = GetFirmSubOffices(Convert.ToInt32(dr["fid"].ToString()));
                if (dts != null && dts.Rows.Count > 0)
                {
                    foreach (DataRow sdr in dts.Rows)
                    {
                        DataRow rw = dtz.NewRow();
                        rw["fid"] = sdr["fid"];
                        rw["fname"] = sdr["fname"].ToString();
                        rw["pc"] = "Sub office";
                        dtz.Rows.Add(rw);

                    }
                }
                else
                { //dts.Clear(); 
                }
            }

            if (dtz.Rows.Count > 0)
            {

                Session["dtz"] = dtz;


            }

        }

        public DataTable GetFirmSubOffices(int fid)
        {
            var par = new List<IDataParameter>();
            try
            {
                var sql = $"{AptifyApplication.GetEntityBaseDatabase("Firm")}..{spgetFirmSubOfficesNameAddressByFirmid__cai}";
                var param = DataAction.GetDataParameter("@fId", SqlDbType.Int, fid);
                par.Add(param);

                var dt = DataAction.GetDataTableParametrized(sql, CommandType.StoredProcedure,
                    par.ToArray());

                if (dt.Rows.Count > 0)
                {
                    return dt;
                }
            }
            catch (Exception ex)
            {
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex);
            }

            return null;


        }


        protected void R3_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                var CompanyID = (HiddenField)e.Item.FindControl("pfId");
                var id = Convert.ToInt32(CompanyID.Value);
                var R4 = (Repeater)e.Item.FindControl("R4");

                var btn = e.Item.FindControl("btnSave") as Button;
                if (btn != null)
                {  // adding button event 
                    btn.Click += new EventHandler(Save);
                }


                //  Button ba = (Button)e.Item.FindControl("bpadd");

                //   ba.Visible = false;

                var sdt = GetFirmSubOffices(id);
                if (sdt != null && sdt.Rows.Count > 0)
                {


                    //var dso = GetData(id); 
                    // Get sub companies based on this company id



                    R4.DataSource = sdt;
                    R4.DataBind();
                }
            }
        }

        //protected void bpfsave_Click(object sender, EventArgs e)
        //{

        //    //foreach (RepeaterItem pfri in R3.Items)
        //    //{
        //    //    DropDownList peno = pfri.FindControl("ddpen") as DropDownList;
        //    //    var id = peno.SelectedItem.Text.ToString();
        //    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + id + "');", true);

        //    //}

        //    RepeaterItem item = (sender as Button).NamingContainer as RepeaterItem;
        //    CustomValidator cv1 = item.FindControl("cvp1") as CustomValidator;
        //    DropDownList ddl1 = item.FindControl("ddpen") as DropDownList;

        //    if (ddl1.SelectedValue == "1")
        //    {
        //        cv1.IsValid = false;
        //    }
        //    else
        //    {
        //        cv1.IsValid = true;
        //    }

        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + ddl1.SelectedIndex + "');", true);
        //}

        //protected void bpfsave_Click(object sender, EventArgs e)
        //{
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('hi');", true);
        //}

        //protected void bsfsave_Click(object sender, EventArgs e)
        //{

        //  validate custom validation for parent officess




        //CustomValidator cVal = item.FindControl("cvp1") as CustomValidator;
        //TextBox vtb = item.FindControl("tbName") as TextBox;



        //foreach (RepeaterItem ssri in R4.Items)
        //{
        //    //DropDownList peno = pfri.FindControl("ddpen") as DropDownList;
        //    //var id = peno.SelectedItem.Text.ToString();
        // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('hi');", true);

        //}


        //}

        protected void Save(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('hi');", true);

        }

        protected void Savesub(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('hi');", true);
        }


        public class FirmDetails
        {
            public int FirmId { get; set; }
            public string FirmName { get; set; }
        }


        //protected void R3_ItemCommand(object source, RepeaterCommandEventArgs e)
        //{
        //    if (e.CommandName == "Save")
        //    {
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('hi');", true);
        //    }
        //}


        //protected void R3_ItemCreated(object sender, RepeaterItemEventArgs e)
        //{
        //    //var button = (HtmlButton)e.Item.FindControl("myButton");
        //    //if (button != null)
        //    //{
        //    //    button.Attributes["index"] = e.Item.ItemIndex.ToString();
        //    //    button.ServerClick += new EventHandler(MyButton_Click);
        //    //}
        //}

        //protected void btnSave_Command(object sender, CommandEventArgs e)
        //{

        //    if (e.CommandName == "Save")
        //    {

        //    }


        //}


        public void createRepeater()
        {
			R5.DataSource = createDataTable();
			R5.DataBind();
		}

		public System.Data.DataTable createDataTable()
		{
			DataTable dt = new DataTable();
			DataColumn dc = new DataColumn();


			dc.ColumnName = "id";
			dc.DataType = typeof(int);
			dt.Columns.Add(dc);

			dc = new DataColumn();
			dc.ColumnName = "name";
			dc.DataType = typeof(string);
			dt.Columns.Add(dc);

			dc = new DataColumn();
			dc.ColumnName = "des";
			dc.DataType = typeof(string);
			dt.Columns.Add(dc);


			dt.Rows.Add(new object[] { 1, "aaa" });
			dt.Rows.Add(new object[] { 2, "bbb" });

			return dt;

		}

		public DataTable  createsubDataTable(int id)
		{
			DataTable dt = new DataTable();
			DataColumn dc = new DataColumn();


			dc.ColumnName = "id";
			dc.DataType = typeof(int);
			dt.Columns.Add(dc);

			dc = new DataColumn();
			dc.ColumnName = "name";
			dc.DataType = typeof(string);
			dt.Columns.Add(dc);

			if (id == 1)
			{
				dt.Rows.Add(new object[] { 11, "aabc" });
				dt.Rows.Add(new object[] { 111, "adf" });
				dt.Rows.Add(new object[] { 1111, "aabc" });
				dt.Rows.Add(new object[] { 11111, "adf" });
			}
			else if (id == 2)
			{
				dt.Rows.Add(new object[] { 22, "bcc" });
				dt.Rows.Add(new object[] { 222, "bcfgrdf" });
				dt.Rows.Add(new object[] { 2222, "bgsdr" });
				dt.Rows.Add(new object[] { 22222, "bfrtd" });

			}

			return dt;

		}

		protected void GetValue(object sender, EventArgs e)
		{
			//Reference the Repeater Item using Button.
			RepeaterItem item = (sender as Button).NamingContainer as RepeaterItem;

			//Reference the Label and TextBox.
			string message = "Id: " + (item.FindControl("Id") as Label).Text;
			//   message += "\\nName: " + (item.FindControl("lblName") as Label).Text;
			message += "\\name: " + (item.FindControl("name") as TextBox).Text;

			Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
		}

		protected void GetValueMain(object sender, EventArgs e)
		{
			//Reference the Repeater Item using Button.
			RepeaterItem item = (sender as Button).NamingContainer as RepeaterItem;

			//Reference the Label and TextBox.
			string message = "Id: " + (item.FindControl("Id") as Label).Text;
			//   message += "\\nName: " + (item.FindControl("lblName") as Label).Text;
			message += "\\name: " + (item.FindControl("name") as TextBox).Text;

			Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
		}


		protected void R5_DataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				var xId = (HiddenField)e.Item.FindControl("hid");
				var id = Convert.ToInt32(xId.Value);

				var R66 = (Repeater)e.Item.FindControl("R6");
				var dso = createsubDataTable(id); // Get sub companies based on this company id

				R66.DataSource = dso;
				R66.DataBind();




				//DropDownList ddlC = (DropDownList)e.Item.FindControl("ddl");
				//ddlC.DataSource = CountryDataTable();
				//ddlC.DataBind();
				//ddlC.SelectedValue = ((DataRowView)e.Item.DataItem)["cid"].ToString();
			}
		}




	protected void GetValueSub(object sender, EventArgs e)
	{
		//Reference the Repeater Item using Button.
		RepeaterItem item = (sender as Button).NamingContainer as RepeaterItem;

		//Reference the Label and TextBox.
		string message = "Id: " + (item.FindControl("Id") as Label).Text;
		//   message += "\\nName: " + (item.FindControl("lblName") as Label).Text;
		message += "\\name: " + (item.FindControl("name") as TextBox).Text;

		Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
	}






}


       
  


        




       



    
    }




