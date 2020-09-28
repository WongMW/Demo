using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Aptify.Framework.BusinessLogic.GenericEntity;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.FirmsSearch
{
    

    public partial class EnhancedListingConfirm : Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
    {
        public List<FirmDetails> sflist = new List<FirmDetails>();
        private static string spGetFirmsbyFirmAdminByPersonId__cai => "spGetFirmsbyFirmAdminByPersonId__cai";
        private static string spGetPerson => "spGetPerson";
        private static string spGetFirmNameFullAddressByFirmId__cai => "spGetFirmNameFullAddressByFirmId__cai";
        private static string spgetFirmSubOfficesNameAddressByFirmid__cai => "spgetFirmSubOfficesNameAddressByFirmid__cai";
		private static string spGetEnhancedListingPendingParentEditFirms__cai => "spGetEnhancedListingPendingParentEditFirms__cai";
		private static string spGetEnhancedListingPendingSubOfficesEditFirms__cai => "spGetEnhancedListingPendingSubOfficesEditFirms__cai";
		private static string spGetEnhancedListingEditFormByFirmId__cai => "spGetEnhancedListingEditFormByFirmId__cai";
		private static string spGetEnhancedListingEditFormByElAdminIDFirmId__cai => "spGetEnhancedListingEditFormByElAdminIDFirmId__cai";
		private static string spCreateEnhancedListingEditForm__cai => "spCreateEnhancedListingEditForm__cai";
		private static string spGetCompanyParentID__c => "spGetCompanyParentID__c";
		private static string spGetTopicCodeByParentID__c => "spGetTopicCodeByParentID__c";
        private static string spUpdateEnhancedListingEmailLog => "spUpdateEnhancedListingEmailLog";
        private static string spCreateEnhancedListingEmailLog => "spCreateEnhancedListingEmailLog";

		protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
				// load firms 
				LoadFirms();
            }
            else
            { }

        }




        public void LoadFirms()
        {

            DataTable fdt = new DataTable();
			DataTable pdt = new DataTable();
			DataTable sodt = new DataTable();
			string fadminlist = string.Empty;
            string Loginpage = "/Login.aspx";
			string Noaccess = "/500.html";

			if (User1.PersonID <= 0)
            {
				Session["ReturnToPage"] = Request.RawUrl;
				Response.Redirect(Loginpage);
            }
            else
            {
							
				// get userdeatils  FirstLast & firmname with address 
				GetUserDetails();
				// Get firm Admin details 
                fdt = GetFirmAdminDetails();
                if (fdt.Rows.Count > 0)
                {
					// Get  list of parent firms  by admin 
					fadminlist = GetCommaSeparatedString(fdt);
					pdt = GetParentFirmsbyAdminRole(fadminlist);
					if( pdt.Rows.Count > 0 )
					{
						R3.DataSource = pdt;
						R3.DataBind();
					}              
				}
                else
                {
					//unauthorized access
					Response.Redirect(Noaccess);
				}

                
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
                    lName.Text = dt.Rows[0]["FirstLast"].ToString();
                    lFirmName.Text = GetFirmDetails(Convert.ToInt32(dt.Rows[0]["CompanyID"].ToString()));
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

		private DataTable GetParentFirmsbyAdminRole(string adminfid)
		{
			var parameters = new List<IDataParameter>();
			try
			{
				var sql = $"{AptifyApplication.GetEntityBaseDatabase("Products")}..{spGetEnhancedListingPendingParentEditFirms__cai}";
				
				var param = DataAction.GetDataParameter("@adminfid", adminfid);
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

		private DataTable GetEnhancedListingEditFormByFirmId(int fid)
		{
			var parameters = new List<IDataParameter>();
			try
			{
				var sql = $"{AptifyApplication.GetEntityBaseDatabase("Products")}..{spGetEnhancedListingEditFormByFirmId__cai}";

				var param = DataAction.GetDataParameter("@fid", fid);
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

		private DataTable GetEnhancedListingEditFormByELAidFirmId(int fid, int eladminid)
		{
			var parameters = new List<IDataParameter>();

			//var parameters = new List<IDataParameter>();
			//var sql = $"{AptifyApplication.GetEntityBaseDatabase("TopicCode")}..{spGetTopicCodeByParentID__c}";
			//parameters.Add(DataAction.GetDataParameter("@ParentID", SqlDbType.Int, 1376));
			//parameters.Add(DataAction.GetDataParameter("@OrganizationID", SqlDbType.Int, -1));

			//var dt = DataAction.GetDataTableParametrized(sql, CommandType.StoredProcedure, parameters.ToArray());
			//if (dt.Rows.Count > 0)


				try
			{
				var sql = $"{AptifyApplication.GetEntityBaseDatabase("Products")}..{spGetEnhancedListingEditFormByElAdminIDFirmId__cai}";

				parameters.Add(DataAction.GetDataParameter("@fid", SqlDbType.Int, fid));
				parameters.Add(DataAction.GetDataParameter("@elaid", SqlDbType.Int, eladminid));
				var dt = DataAction.GetDataTableParametrized(sql, CommandType.StoredProcedure,parameters.ToArray());

				return dt;
			}
			catch (Exception ex)
			{
				Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex);
			}

			return null;
		}


		// get the suboffices by parentid
		private DataTable GetSubofficesEnhancedListingEditFormByFirmId(int pid)
		{
			var parameters = new List<IDataParameter>();
			try
			{
				var sql = $"{AptifyApplication.GetEntityBaseDatabase("Products")}..{spGetEnhancedListingPendingSubOfficesEditFirms__cai}";

				var param = DataAction.GetDataParameter("@pid", pid);
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



		//Get  Firm Parent Id 

		private int GetFirmParentId(int fid)
		{
			//exec spGetCompanyParentID__c 41321

			var parameters = new List<IDataParameter>();
			try
			{
				var sql = $"{AptifyApplication.GetEntityBaseDatabase("Firm")}..{spGetCompanyParentID__c}";
				var param = DataAction.GetDataParameter("@CompanyID", SqlDbType.Int, fid);
				parameters.Add(param);

				var dt = DataAction.GetDataTableParametrized(sql, CommandType.StoredProcedure,
					parameters.ToArray());

				if (dt.Rows.Count > 0)
				{
					return int.Parse(dt.Rows[0]["ParentID"].ToString());
				}
			}
			catch (Exception ex)
			{
				Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			return 0;
			
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
            GetfirmswithSubOffices(dty);

            if (dty.Rows.Count > 0)
            {
                

                R3.DataSource = dty;
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
            // Get companyId from hidden field 
            // or you could get it from e.Item.DataItem which should have the data for this row of data
            var CompanyID = (HiddenField)e.Item.FindControl("pfId");
            var fid = Convert.ToInt32(CompanyID.Value);

			var rid = (HiddenField)e.Item.FindControl("pid");
			var pelaid = Convert.ToInt32(rid.Value);
			// bind   parent firms by fid
			DataTable edt = new DataTable();
			DataTable sdt = new DataTable();
			//int tid = 43973;
			// get single firm details  to put conditins  while dispplaying
			edt = GetEnhancedListingEditFormByELAidFirmId(fid, pelaid); 
		
			// bind R4
			  var R4 = (Repeater)e.Item.FindControl("R4");
			//  var sdt = GetFirmSubOffices(id);

			sdt = GetSubofficesEnhancedListingEditFormByFirmId(fid);			
				   if ( sdt.Rows.Count > 0)
					 {

						  R4.DataSource = sdt;
							 R4.DataBind();
					  }

				if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				//Reference the Repeater Item.
				RepeaterItem item = e.Item;

				CheckBox cbp = (item.FindControl("pfcb") as CheckBox);
				Panel spanel = item.FindControl("ps") as Panel;
				LinkButton sb = item.FindControl("btnSave") as LinkButton;
				//firm status label 
				 Label fs = (item.FindControl("fis") as Label);

				FileUpload fup = item.FindControl("fu") as FileUpload;

				System.Web.UI.WebControls.Image im = item.FindControl("ImgPrv") as System.Web.UI.WebControls.Image;

				//multi select specilism
				ListBox lb = item.FindControl("lbpspec") as ListBox;
				lb.DataSource = BindSpecialisms();
				lb.DataBind();

				// multi selct Industry sector
				ListBox lb1 = item.FindControl("lbpis") as ListBox;
				lb1.DataSource = BindIndustrySectors();
				lb1.DataBind();

				im.Visible = false;

				cbp.Enabled = false;
				if (edt.Rows.Count > 0)
				{
					
					//Reference the Controls.
					string firmId = Convert.ToString((HiddenField)e.Item.FindControl("pfId"));

					// checkbox enable false and  submit  button false  only view  details   for status = 2 or 4  submit for approval & approval granted
				
					 // submit deatils and and approved 
					 if (((edt.Rows[0]["WebStatus"].ToString()) == "2"))					
					{
						fs.Text = "Pending approval";
						//fs.ForeColor = System.Drawing.Color.Red;
						//cbp.Visible = false;
						cbp.Enabled = false;
						sb.Visible = false;
						fup.Visible = false;
					}
					 // save details & rject deatails
					else if(((edt.Rows[0]["WebStatus"].ToString()) == "1" ))
					{

						//fs.Text = "<span class='firm-status-renewal'>(Ready to submit)</span>";
						fs.Text = "Ready to submit";
						//fs.ForeColor = System.Drawing.Color.DarkGreen;


						//cbp.Enabled = true;
						cbp.Checked = true;
						spanel.Visible = true;
						sb.Visible = true;
						sb.Text = "Update Deatils";
						//SubmitApproval.Visible = true;
					}else if (((edt.Rows[0]["WebStatus"].ToString()) == "4"))
					{
						fs.Text = "Approved & active";
						cbp.Enabled = false;
						sb.Visible = false;
						fup.Visible = false;

					}
					 else
					{
						fs.Text = "Pending edit";
						cbp.Enabled = false;
						//	fs.ForeColor = System.Drawing.Color.DarkOrange;
					}




					// no of employees 
					DropDownList ddl1 = item.FindControl("ddpen") as DropDownList;

					// bind  dropdownlais 
					ddl1.DataSource = BindNoOfEmployees();
					ddl1.DataBind();
					ddl1.SelectedIndex = ddl1.Items.IndexOf(ddl1.Items.FindByText(edt.Rows[0]["NoOfEmployees"].ToString()));



					// no of partners 
					TextBox tb = item.FindControl("tbpnop") as TextBox;
					tb.Text = edt.Rows[0]["NoOfPartners"].ToString();
					//  description
					TextBox tbd = item.FindControl("tbpdes") as TextBox;
					tbd.Text = edt.Rows[0]["FirmDescription"].ToString();
					//image 

					if (String.IsNullOrEmpty(edt.Rows[0]["LogoURL"].ToString()))
					{


					}

					else
					{
						//System.Web.UI.WebControls.Image im = item.FindControl("ImgPrv") as System.Web.UI.WebControls.Image;
						im.Visible = true;
						string url = edt.Rows[0]["LogoURL"].ToString();
						im.ImageUrl = url;

					}



					//multi select specilism
					//ListBox lb = item.FindControl("lbpspec") as ListBox;
					//lb.DataSource = BindSpecialisms();
					//lb.DataBind();

					foreach (ListItem sitem in lb.Items)
					{
						if (edt.Rows[0]["Specialisms"].ToString().Contains(sitem.Text.ToString()))
						//if (sitem.Text.Contains(edt.Rows[0]["Specialisms"].ToString()) == true)
						{
							sitem.Selected = true;
						}

					}

					//// multi selct Industry sector
					//ListBox lb1 = item.FindControl("lbpis") as ListBox;
					//lb1.DataSource = BindIndustrySectors();
					//lb1.DataBind();

					foreach (ListItem sitem in lb1.Items)
					{
						if (edt.Rows[0]["IndustrySector"].ToString().Contains(sitem.Text.ToString()))
						{
							sitem.Selected = true;
						}

					}

					// google map textbox 

					if (String.IsNullOrEmpty(edt.Rows[0]["LocationURL"].ToString()))
					{ }
				
					else
					{
					TextBox tbg = item.FindControl("tblocgmap") as TextBox;

					tbg.Text = edt.Rows[0]["LocationURL"].ToString();


					}


				}
				else
				{
					fs.Text = "Pending edit";
					cbp.Enabled = false;

				}
					
			}
				

				

			}


		protected void R4_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
		{

			var CompanyID = (HiddenField)e.Item.FindControl("sfId");
			var fid = Convert.ToInt32(CompanyID.Value);
			 var elaid = (HiddenField)e.Item.FindControl("sid");
			var selaid = Convert.ToInt32(elaid.Value);




			DataTable sdt = new DataTable();
			//int tid = 43973;
			// get single firm details  to put conditins  while dispplaying
			sdt = GetEnhancedListingEditFormByELAidFirmId(fid,selaid);


			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				//Reference the Repeater Item.
				RepeaterItem item = e.Item;
				CheckBox cbs = (item.FindControl("sfcb") as CheckBox);
				Panel spanel = item.FindControl("pss") as Panel;

				//multi select specilism
				ListBox lb = item.FindControl("lbsspec") as ListBox;
				lb.DataSource = BindSpecialisms();
				lb.DataBind();

				// multi selct Industry sector
				ListBox lb1 = item.FindControl("lbsis") as ListBox;
				lb1.DataSource = BindIndustrySectors();
				lb1.DataBind();


				// save button 
				LinkButton sb = item.FindControl("btnSavesub") as LinkButton;
				cbs.Enabled = false;

				//firm  sub office status label 
				Label sos = (item.FindControl("sis") as Label);
				if (sdt.Rows.Count > 0)
				{

					

					if ((sdt.Rows[0]["WebStatus"].ToString()) == "2")
					{
						sos.Text = "Pending approval";
						cbs.Enabled = false;
						sb.Visible = false;
					}
					else if ((sdt.Rows[0]["WebStatus"].ToString()) == "1")
					{
						spanel.Visible = true;
						cbs.Checked = true;
						sb.Text = "Update details";
						sos.Text = "Ready to submit"; 

					}
					else if ((sdt.Rows[0]["WebStatus"].ToString()) == "4")
					{
						sb.Visible = false;
						sos.Text = "Approved & active";

					}
					//	//multi select specilism
					//	ListBox lb = item.FindControl("lbsspec") as ListBox;
					//lb.DataSource = BindSpecialisms();
					//lb.DataBind();

					foreach (ListItem sitem in lb.Items)
					{
						if (sdt.Rows[0]["Specialisms"].ToString().Contains(sitem.Text.ToString()))
						{
							sitem.Selected = true;
						}

					}

					//// multi selct Industry sector
					//ListBox lb1 = item.FindControl("lbsis") as ListBox;
					//lb1.DataSource = BindIndustrySectors();
					//lb1.DataBind();

					foreach (ListItem sitem in lb1.Items)
					{
						if (sdt.Rows[0]["IndustrySector"].ToString().Contains(sitem.Text.ToString()))
						{
							sitem.Selected = true;
						}

					}

					// google map textbox 

					if (String.IsNullOrEmpty(sdt.Rows[0]["LocationURL"].ToString()))
					{ }

					else
					{
						TextBox tbg = item.FindControl("tblocgmaps") as TextBox;

						tbg.Text = sdt.Rows[0]["LocationURL"].ToString();


					}
				}

				else
				{
					cbs.Enabled = false;
					sos.Text = "Pending edit";
				}

			}
		}


		

		public DataTable BindNoOfEmployees()
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

			dt.Rows.Add(new object[] { 1, "Self-employed"});
			dt.Rows.Add(new object[] { 2, "1-10 employees"});
			dt.Rows.Add(new object[] { 3, "11-50 employees"});
			dt.Rows.Add(new object[] { 4, "51-200 employees"});
			dt.Rows.Add(new object[] { 5, "201-500 employees"});
			dt.Rows.Add(new object[] { 6, "501-1000 employees"});
			dt.Rows.Add(new object[] { 7, "1001-5000 employees"});
			dt.Rows.Add(new object[] { 8, "5001-10000 employees"});
			dt.Rows.Add(new object[] { 9, "10000-15000 employees"});
			return dt;
		}

		// save and update  parent office

		protected void btnSave_Command(object sender, CommandEventArgs e)
		{

		  
			var firmId = int.Parse(e.CommandArgument.ToString());
			RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;



			int ensccess = 0;
			string lurl = string.Empty;
			  DataTable pfirmexist = new DataTable();
			if (e.CommandName.Equals("btnSave"))
			{
				//	TextBox tb = item.FindControl("tb2") as TextBox;
				//	TextBox fdtb = item.FindControl("txtDescription") as TextBox;

				//get firmid from hiddenfield
				HiddenField hfp = item.FindControl("pid") as HiddenField;

				//get ID from admin id 
				//HiddenField paid = item.FindControl("pid") as HiddenField;

				//var paid = (HiddenField)e.Item.FindControl("pfId");

				// no of employees 
				DropDownList ddl1 = item.FindControl("ddpen") as DropDownList;
				CustomValidator cv1 = item.FindControl("cvp1") as CustomValidator;
				var text = ddl1.SelectedItem.Text;
				if (ddl1.SelectedItem.Text == "No of employees")
				{ cv1.IsValid = false; }
				else
				{ cv1.IsValid = true; }


				// no of partners 
				TextBox tb = item.FindControl("tbpnop") as TextBox;
				CustomValidator cv2 = item.FindControl("cvp2") as CustomValidator;

				if (tb.Text == "")
				{ cv2.IsValid = false; }
				else 
				{ cv2.IsValid = true; }


				//  description
				TextBox tbd = item.FindControl("tbpdes") as TextBox;
				CustomValidator cv3 = item.FindControl("cvp3") as CustomValidator;
				if (tbd.Text.Trim() == "")
				{ cv3.IsValid = false; }
				else
				{ cv3.IsValid = true; }


				// fileupload 
				Label fl = item.FindControl("ful") as Label;
				FileUpload fu1 = item.FindControl("fu") as FileUpload;
				System.Web.UI.WebControls.Image im1 = item.FindControl("ImgPrv") as System.Web.UI.WebControls.Image;
				CustomValidator cv4 = item.FindControl("cvp4") as CustomValidator;
				RequiredFieldValidator rffu = item.FindControl("furf") as RequiredFieldValidator;
				if (fu1.HasFile)
				{

					try
					{
						var allowedExtensions = new[]
						{ ".jpg", ".jpeg", ".gif", ".jpe", ".png",".JPG", ".JPEG", ".GIF", ".JPE", ".PNG" };

						string filename = System.IO.Path.GetFileName(fu1.PostedFile.FileName);
						var ext = Path.GetExtension(fu1.PostedFile.FileName);

						if (!allowedExtensions.Contains(ext))
						{
							//Check file extension
							//	fl.Text = "File Extension Is InValid";
							fl.Text = "";
							cv4.IsValid = false;
						//	cv4.Text = "Only Image file formats/extensions are valid";

						}
						// file size more than 2MB
						else if (fu1.PostedFile.ContentLength > 2 * 1024 * 1024)
						{
							//fl.Text = "File size is too large (2MB MAX)";
							fl.Text = "";
							cv4.IsValid = false;
							cv4.Text = "Only File size valid (2MB MAX)";
						}
						else
						//    if (!Directory.Exists(folderPath))
						{
							//    // If Directory(Folder) does not exists.Create it.
							//fl.Text = "";

						}

					}
					catch (Exception ex)
					{
						fl.Text = ex.Message.ToString();
					}


				}
				else
				{
					if (im1.Visible)
					{
						fl.Text = "";
						cv4.IsValid = true;

					}
					else
					{
						cv4.Text = "*Firm logo required";
						cv4.IsValid = false;
					}
				}

				// multi selct Specialisms
				ListBox lb = item.FindControl("lbpspec") as ListBox;
				CustomValidator cv5 = item.FindControl("cvp5") as CustomValidator;
			 //  string sval = lb.se
				//  Specialisms is not mandatory for time bieng 
				string smes = "";
				int icount = 0;
				//innt scount = l 
				foreach (ListItem sitem in lb.Items)
				{

					if (sitem.Selected)
					{
						icount = icount + 1;
						// if (icount == 1 ||  icount == lb.Items.Count)
						//{ smes += sitem.Text; }
						// else
						{
							smes += sitem.Text + " ,";
						}
						//message += sitem.Text + " " + sitem.Value + "\\n";
					}
				}
				smes = smes.TrimEnd(',');
				//if (String.IsNullOrEmpty(message))
				//{ cv5.IsValid = false; }
				//else
				//{ cv5.IsValid = true; }
				if ((icount > 3 ) || (icount == 0))
				{ cv5.IsValid = false; }
				else
				{ cv5.IsValid = true; }
				//if (icount < 1 )
				//{ cv5.IsValid = false; }
				//else
				//{ cv5.IsValid = true; }




				// multi selct Industry sector
				ListBox lb1 = item.FindControl("lbpis") as ListBox;
				CustomValidator cv6 = item.FindControl("cvp6") as CustomValidator;
				//  Industry sector is not mandatory for time bieng 
				string imes = "";
				int scount = 0;
				foreach (ListItem iitem in lb1.Items)
				{
					if (iitem.Selected)
					{
						scount = scount + 1;
						//mes += sitem.Text + " " + sitem.Value + "\\n";
						imes += iitem.Text + " ,";
					}
				}
				imes = imes.TrimEnd(',');
				//if (String.IsNullOrEmpty(imes))
				//{ cv6.IsValid = false; }
				//else
			 //   { cv6.IsValid = true; }

				if ((scount > 3) || (scount == 0))
				{ cv6.IsValid = false; }
				else
				{ cv6.IsValid = true; }


				// google location url 
				TextBox tbgl = item.FindControl("tblocgmap") as TextBox;
				//if (String.IsNullOrEmpty(tbgl.Text.Trim()))
				//{ cv6.IsValid = false; }
				//else
				//{ cv6.IsValid = true; }



				// if page is valid insert the records
				if (cv1.IsValid && cv2.IsValid  && cv3.IsValid &&  cv4.IsValid && cv5.IsValid && cv6.IsValid)
				{
					if (fu1.HasFile)
					{
						// validate image  if file is changed after   
						try
						{
							var allowedExtensions = new[]
							{ ".jpg", ".jpeg", ".gif", ".jpe", ".png",".JPG", ".JPEG", ".GIF", ".JPE", ".PNG" };

						//	string fname = System.IO.Path.GetFileName(fu1.PostedFile.FileName);
							var ext = Path.GetExtension(fu1.PostedFile.FileName);

							if (!allowedExtensions.Contains(ext))
							{
								//Check file extension
								//fl.Text = "Only Image file formats/extensions are valid";

							}
							// file size more than 2MB
							else if (fu1.PostedFile.ContentLength > 2 * 1024 * 1024)
							{
								fl.Text = "File size is too large (2MB MAX)";

							}
							else
							//    if (!Directory.Exists(folderPath))
							{
								//    // If Directory(Folder) does not exists.Create it.
								fl.Text = "";

							}

						}
						catch (Exception ex)
						{
							fl.Text = ex.Message.ToString();
						}




						string filename = Path.GetFileName(fu1.PostedFile.FileName);
						string folderPath = Server.MapPath("~/Content/Images/FirmImages/" + firmId);
						if (!Directory.Exists(folderPath))
						{
					  // If Directory(Folder) does not exists.Create it.
							Directory.CreateDirectory(folderPath);
					    }
						string targetPath = Server.MapPath("~/Content/Images/FirmImages/" + firmId + "/" + firmId + ".jpeg");
						Stream strm = fu1.PostedFile.InputStream;
						var targetFile = targetPath;
						 lurl = GenerateThumbnails(0.5, strm, targetFile);

						// check the logourl and image is exist inside teh folder 
						if (!String.IsNullOrEmpty(lurl) && File.Exists(lurl))
						{
							lurl = "~/Content/Images/FirmImages/" + firmId + "/" + firmId + ".jpeg";

							// validate firm for updat eor insert 
							//pfirmexist = GetEnhancedListingEditFormByFirmId(firmId);
							pfirmexist = GetEnhancedListingEditFormByELAidFirmId(firmId, int.Parse(hfp.Value));
							if (pfirmexist.Rows.Count > 0)
							{
								// update into table if records already exist  into suboffices
								//(int fid, string noe, int nop, string fdes, string fspec, string fisec, string fgmaploc, string logourl)
								int upsuc = UpdateEnhancedEditParentFirms(firmId, ddl1.SelectedItem.Text.Trim(), int.Parse(tb.Text.Trim()), tbd.Text.Trim(), smes, imes, tbgl.Text.Trim(),lurl, int.Parse(hfp.Value));


							}
							else
							{

								ensccess = InsertEnhncedEditFirmDetails(firmId, GetFirmDetails(firmId), ddl1.SelectedItem.Text.Trim(), int.Parse(tb.Text.Trim()), tbd.Text.Trim(), tbgl.Text.Trim(), smes, imes, lurl, int.Parse(hfp.Value) );
								if (ensccess > 0)
								{
									Panel spanel = item.FindControl("ps") as Panel;
									spanel.Visible = true;
								}
							}

							// refresh after submission

							LoadFirms();

						}
					}
					else
					{

						lurl = "~/Content/Images/FirmImages/" + firmId + "/" + firmId + ".jpeg";

						// validate firm for updat eor insert 
						pfirmexist = GetEnhancedListingEditFormByELAidFirmId(firmId, int.Parse(hfp.Value));
						if (pfirmexist.Rows.Count > 0)
						{
							// update into table if records already exist  into suboffices
							//(int fid, string noe, int nop, string fdes, string fspec, string fisec, string fgmaploc, string logourl)
							int upsuc = UpdateEnhancedEditParentFirms(firmId, ddl1.SelectedItem.Text.Trim(), int.Parse(tb.Text.Trim()), tbd.Text.Trim(), smes, imes, tbgl.Text.Trim(), lurl, int.Parse(hfp.Value));


						}
						else
						{

							ensccess = InsertEnhncedEditFirmDetails(firmId, GetFirmDetails(firmId), ddl1.SelectedItem.Text.Trim(), int.Parse(tb.Text.Trim()), tbd.Text.Trim(), tbgl.Text.Trim(), smes, imes, lurl, int.Parse(hfp.Value));
							if (ensccess > 0)
							{
								Panel spanel = item.FindControl("ps") as Panel;
								spanel.Visible = true;
							}
						}


						// refresh after submission

						LoadFirms();

					}
				}
				else
				{
				
				}

			

			}
			Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "keepopenaftersave('" + firmId + "');", true);
	
		}
		private int UpdateEnhancedEditParentFirms(int firmId, string v, string smes, string imes, string text)
		{
			throw new NotImplementedException();
		}

		// private function to insert enhancedlisting records

		private int InsertEnhncedEditFirmDetails(int fid, string fname, string noe , int nop ,string fdes, string locurl,  string spec, string indsec, string logourl, int eladminid )
		{

			int eeid = 0;


			try
			{
				// call SP to check person Id 

				string sSql = string.Empty;
				string rname = string.Empty;
				int pid = 0;
				IDataParameter[] param = new IDataParameter[21];
				sSql = Database + "..spCreateEnhancedListingEditForm__cai";
				param[0] = DataAction.GetDataParameter("@ID", System.Data.SqlDbType.Int);
				param[0].Direction = ParameterDirection.Output;
				// ID should not be there, but by some reason required....
				param[1] = DataAction.GetDataParameter("@FirmId", SqlDbType.Int, fid);
				param[2] = DataAction.GetDataParameter("@FirmName", SqlDbType.NVarChar, fname);
				param[3] = DataAction.GetDataParameter("@NoOfEmployees ", SqlDbType.NVarChar, noe);
				if (nop > 0 )
				{ param[4] = DataAction.GetDataParameter("@NoOfPartners", SqlDbType.Int, nop); }
				else
				{ param[4] = DataAction.GetDataParameter("@NoOfPartners", SqlDbType.Int, DBNull.Value); }
				
				param[5] = DataAction.GetDataParameter("@FirmDescription", SqlDbType.NVarChar, fdes);
				param[6] = DataAction.GetDataParameter("@LocationURL", SqlDbType.NVarChar, locurl);
				param[7] = DataAction.GetDataParameter("@WebStatus", SqlDbType.Int, 1);
				param[8] = DataAction.GetDataParameter("@StartDate", SqlDbType.DateTime,DBNull.Value);
				param[9] = DataAction.GetDataParameter("@EndDate", SqlDbType.DateTime,DBNull.Value);
				param[10] = DataAction.GetDataParameter("@ApprovedBy", SqlDbType.DateTime,DBNull.Value);
				param[11] = DataAction.GetDataParameter("@ApprovedDate", SqlDbType.DateTime, DBNull.Value);
				param[12] = DataAction.GetDataParameter("@LastUpdateDate", SqlDbType.DateTime,DateTime.Now);
				param[13] = DataAction.GetDataParameter("@DateCreated", SqlDbType.DateTime, DateTime.Now);


				// insert parentid
				pid = GetFirmParentId(fid);
				 if (pid > 0 )
				{ param[14] = DataAction.GetDataParameter("@ParentId", SqlDbType.Int, pid); }
				 else
				{ param[14] = DataAction.GetDataParameter("@ParentId", SqlDbType.Int, DBNull.Value); }

				
				param[15] = DataAction.GetDataParameter("@Specialisms", SqlDbType.NVarChar, spec);
				param[16] = DataAction.GetDataParameter("@IndustrySector", SqlDbType.NVarChar, indsec);
				param[17] = DataAction.GetDataParameter("@LogoURL", SqlDbType.NVarChar, logourl);
				param[18] = DataAction.GetDataParameter("@UpdatedBy", System.Data.SqlDbType.NVarChar, User1.FirstName + " " + User1.LastName); 
				param[19] = DataAction.GetDataParameter("@PersonId", SqlDbType.Int, User1.PersonID);
				param[20] = DataAction.GetDataParameter("@ELAdminID", SqlDbType.Int, eladminid);
				eeid = Convert.ToInt32(DataAction.ExecuteNonQueryParametrized(sSql, CommandType.StoredProcedure, param));

				//Convert.ToInt32(cmd.Parameters["@id"].Value.ToString());
				//int contractID = Convert.ToInt32(DataAction.GetDataParameter["@ID"].Value);
				if (eeid > 0)
				{
					return eeid;
				}
				

			}
			catch (Exception ex)
			{
				Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex);
			
			}

			return eeid;
		}

		private string GenerateThumbnails(double scaleFactor, Stream sourcePath, string targetPath)
		{



			using (var image = System.Drawing.Image.FromStream(sourcePath))
			{
				try
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
				catch (Exception ex)
				{
					Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex);

				}
				return targetPath;
			}
		}

		// sub office save deatils 
		protected void btnSavesub_Command(object sender, CommandEventArgs e)
		{

			var firmId = int.Parse(e.CommandArgument.ToString());
			RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;

			int subsucins = 0;
			DataTable firmexist = new DataTable();

			string lurl = string.Empty;

			if (e.CommandName.Equals("btnSave"))
			{

				// multi selct Specialisms
				HiddenField hfs = item.FindControl("sid") as HiddenField;
				ListBox lb1 = item.FindControl("lbsspec") as ListBox;
				CustomValidator cvs11 = item.FindControl("cvs1") as CustomValidator;
				//  string sval = lb.se
				//  Specialisms is not mandatory for time bieng 
				string smes = "";
				int icount = 0;
				//innt scount = l 
				foreach (ListItem sitem in lb1.Items)
				{

					if (sitem.Selected)
					{
						icount = icount + 1;
						// if (icount == 1 ||  icount == lb.Items.Count)
						//{ smes += sitem.Text; }
						// else
						{
							smes += sitem.Text + " ,";
						}
						//message += sitem.Text + " " + sitem.Value + "\\n";
					}
				}
				smes = smes.TrimEnd(',');
				//if (String.IsNullOrEmpty(message))
				//{ cv5.IsValid = false; }
				//else
				//{ cv5.IsValid = true; }
				if((icount > 3) || (icount == 0))
				{ cvs11.IsValid = false; }
				else
				{ cvs11.IsValid = true; }


				// multi selct Industry sector
				ListBox lb2 = item.FindControl("lbsis") as ListBox;
				CustomValidator cvs22 = item.FindControl("cvs2") as CustomValidator;
				//  Industry sector is not mandatory for time bieng 
				string imes = "";
				int scount = 0;
				foreach (ListItem iitem in lb2.Items)
				{
					if (iitem.Selected)
					{
						scount = scount + 1;
						//mes += sitem.Text + " " + sitem.Value + "\\n";
						imes += iitem.Text + " ,";
					}
				}
				imes = imes.TrimEnd(',');
				//if (String.IsNullOrEmpty(mes))
				//{ cv6.IsValid = false; }
				//else
				//{ cv6.IsValid = true; }

				if ((scount > 3) || (scount == 0))
				{ cvs22.IsValid = false; }
				else
				{ cvs22.IsValid = true; }
				

				// google location url 
				TextBox tbgl = item.FindControl("tblocgmaps") as TextBox;
				//if (String.IsNullOrEmpty(tbgl.Text.Trim()))
				//{ cv6.IsValid = false; }
				//else
				//{ cv6.IsValid = true; }



				// if page is valid insert the records
				if (cvs11.IsValid  && cvs22.IsValid )
				{
					firmexist = GetEnhancedListingEditFormByELAidFirmId(firmId, int.Parse(hfs.Value));
					if (firmexist.Rows.Count > 0)
					{
						// update into table if records already exist  into suboffices
						int upsuc = UpdateEnhancedEditSubFirms(firmId, smes, imes, tbgl.Text, int.Parse(hfs.Value));


					}
					else
					{
						// insert into table first time for sub offices 
						subsucins = InsertEnhncedEditFirmDetails(firmId, GetFirmDetails(firmId), string.Empty, 0, string.Empty, tbgl.Text.Trim(), smes, imes, String.Empty,int.Parse(hfs.Value));
				
							if (subsucins > 0)
							{

								Panel spanel = item.FindControl("pss") as Panel;
								spanel.Visible = true;
							}
					}

					// refresh after submission

					LoadFirms();
				}
			}

			

			Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "keepopenaftersave('" + firmId + "');", true);
		}
		// update sub office  firms
		public int UpdateEnhancedEditSubFirms(int fid , string fspec,   string fisec, string fgmaploc, int sID )
		{
			try
			{
				string sSql = string.Empty;
			
				//int pid = 0;
				IDataParameter[] param = new IDataParameter[7];

				
				param[0] = DataAction.GetDataParameter("@FirmId", SqlDbType.Int, fid);
				param[1] = DataAction.GetDataParameter("@Specialisms", SqlDbType.NVarChar, fspec);
				param[2] = DataAction.GetDataParameter("@IndustrySector", SqlDbType.NVarChar, fisec);
				param[3] = DataAction.GetDataParameter("@LocationURL", SqlDbType.NVarChar, fgmaploc);
				param[4] = DataAction.GetDataParameter("@UpdatedBy", System.Data.SqlDbType.NVarChar, User1.FirstName + " " + User1.LastName);
				param[5] = DataAction.GetDataParameter("@PersonId", SqlDbType.Int, User1.PersonID);
				param[6] = DataAction.GetDataParameter("@ELAdminID", SqlDbType.Int, sID);

				sSql = Database + "..spUpdateEnhancedListingEditFormByFirmIdELAdminID__cai";
				int recordupdate = this.DataAction.ExecuteNonQueryParametrized(sSql, CommandType.StoredProcedure, param, 180);

				return recordupdate;
			}
			catch (Exception ex)
			{
				Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex);
				return 0;
			}
		}
		// update parent firms 
		public int UpdateEnhancedEditParentFirms(int fid, string noe, int nop, string fdes, string fspec, string fisec, string fgmaploc, string logourl, int pID)
		{
			//	string noe, int nop,string fdes, string locurl,  string spec, string indsec, string logourl )
			{
				try
				{
					string sSql = string.Empty;

					//int pid = 0;
					IDataParameter[] param = new IDataParameter[10];


					param[0] = DataAction.GetDataParameter("@FirmId", SqlDbType.Int, fid);
					param[1] = DataAction.GetDataParameter("@NoOfEmployees", SqlDbType.NVarChar, noe);
					param[2] = DataAction.GetDataParameter("@NoOfPartners", SqlDbType.Int, nop);
					param[3] = DataAction.GetDataParameter("@FirmDescription", SqlDbType.NVarChar, fdes);
					param[4] = DataAction.GetDataParameter("@Specialisms", SqlDbType.NVarChar, fspec);
					param[5] = DataAction.GetDataParameter("@IndustrySector", SqlDbType.NVarChar, fisec);
					param[6] = DataAction.GetDataParameter("@LocationURL", SqlDbType.NVarChar, fgmaploc);
					param[7] = DataAction.GetDataParameter("@LogoURL", SqlDbType.NVarChar, logourl);
					param[8] = DataAction.GetDataParameter("@UpdatedBy", System.Data.SqlDbType.NVarChar, User1.FirstName + " " + User1.LastName);
					param[9] = DataAction.GetDataParameter("@ELAdminID", SqlDbType.Int,pID );

					sSql = Database + "..spUpdateEnhancedListingEditFormByFirmIdELAdminID__cai";
					int recordupdate = this.DataAction.ExecuteNonQueryParametrized(sSql, CommandType.StoredProcedure, param, 180);

					return recordupdate;
				}
				catch (Exception ex)
				{
					Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex);
					return 0;
				}
			}

        }

        private void UpdateLogEmail(int logId, String status)
        {
            string sSql = string.Empty;
            List<IDataParameter> param = new List<IDataParameter>();
            sSql = Database + ".." + spUpdateEnhancedListingEmailLog;
            param.Add(DataAction.GetDataParameter("@ID", System.Data.SqlDbType.Int, logId));
            param.Add(DataAction.GetDataParameter("@Status ", SqlDbType.VarChar, status));
            param.Add(DataAction.GetDataParameter("@DateUpdated", SqlDbType.DateTime, DateTime.Now));

            DataAction.ExecuteNonQueryParametrized(sSql, CommandType.StoredProcedure, param.ToArray());
        }
        private int CreateLogEmail(String subject, String email, String body)
        {
            string sSql = string.Empty;
            List<IDataParameter> param = new List<IDataParameter>();
            sSql = Database + ".." + spCreateEnhancedListingEmailLog;
            param.Add(DataAction.GetDataParameter("@ID", System.Data.SqlDbType.Int));
            param.First().Direction = ParameterDirection.Output;
            param.Add(DataAction.GetDataParameter("@Subject", SqlDbType.VarChar, subject));
            param.Add(DataAction.GetDataParameter("@EmailTo", SqlDbType.VarChar, email));
            param.Add(DataAction.GetDataParameter("@Status ", SqlDbType.VarChar, "To Be Sent"));
            param.Add(DataAction.GetDataParameter("@Body", SqlDbType.Text, body));
            param.Add(DataAction.GetDataParameter("@DateCreated", SqlDbType.DateTime, DateTime.Now));
            param.Add(DataAction.GetDataParameter("@DateUpdated", SqlDbType.DateTime, DateTime.Now));
            
            var arr = param.ToArray();

            DataAction.ExecuteNonQueryParametrized(sSql, CommandType.StoredProcedure, arr);

            var p = arr[0];
            var v = p.Value;
            return Convert.ToInt32(v);
        }

        private String GetSDEmailTemplate(String templateName)
        {
            var resource = "SitefinityWebApp.SDEmailTemplates." + templateName + ".html";
            string data = "";
            using (System.IO.Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException(
                        "Cannot get the email template, as the file is missing. This file should be marked as 'Embedded Resource' and be located at '{resource}'."
                    );
                }

                using (var reader = new System.IO.StreamReader(stream))
                {
                    data = reader.ReadToEnd();
                }
            }

            return data;
        }

        public int UpdateEnhancedEditFirmsforSubmitApproval(int fid, int pid)
		{
			//	string noe, int nop,string fdes, string locurl,  string spec, string indsec, string logourl )
			{
				try
				{
					string sSql = string.Empty;

					//int pid = 0;
					IDataParameter[] param = new IDataParameter[5];

					param[0] = DataAction.GetDataParameter("@FirmId", SqlDbType.Int, fid);
					param[1] = DataAction.GetDataParameter("@WebStatus", SqlDbType.Int, 2);
					param[2] = DataAction.GetDataParameter("@UpdatedBy", System.Data.SqlDbType.NVarChar, User1.FirstName + " " + User1.LastName);
					param[3] = DataAction.GetDataParameter("@PersonId", SqlDbType.Int, User1.PersonID);
					param[4] = DataAction.GetDataParameter("@ELAdminID", SqlDbType.Int, pid);


					sSql = Database + "..spUpdateEnhancedListingEditFormByFirmIdELAdminID__cai";
					int recordupdate = this.DataAction.ExecuteNonQueryParametrized(sSql, CommandType.StoredProcedure, param, 180);

                    return recordupdate;
				}
				catch (Exception ex)
				{
					Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex);
					return 0;
				}
			}

			

		}

		

		

		public string GetCommaSeparatedString( DataTable dt)
		{
			string strresult = string.Empty;
			try
			{
				
				 strresult = String.Join(",", dt.AsEnumerable().Select(x => x.Field<int>("fid").ToString()).ToArray());

			}
			catch(Exception ex)
			{
				
			}

			return strresult;

		}


		protected void SubmitApproval_Click(object sender, EventArgs e)
		{

		 	
			 int fcount = 1;

            String submittedFirmNames = String.Empty;
            foreach (RepeaterItem pRepeaterItem in R3.Items)
			{

				CheckBox pfchkbox = (CheckBox)pRepeaterItem.FindControl("pfcb");
				HiddenField pfirmid = (HiddenField)pRepeaterItem.FindControl("pfid");
				HiddenField puid = (HiddenField)pRepeaterItem.FindControl("pid");
				// lets check if there are any items in the children
				Repeater R44 = (Repeater)pRepeaterItem.FindControl("R4");

				if (pfchkbox.Checked)
				{

					try {
						FirmDetails spfd = new FirmDetails();
						spfd.Fid = fcount;
						spfd.Fname = pfchkbox.Text;
						


				      	int psuc = UpdateEnhancedEditFirmsforSubmitApproval(int.Parse(pfirmid.Value), int.Parse(puid.Value));
						sflist.Add(spfd);
						fcount = fcount + 1;

                        submittedFirmNames += "<br/>" + GetFirmDetails(int.Parse(pfirmid.Value));
					}
					catch (Exception ex)
					{
						
							Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex );
					
					}

					

					


				}

				foreach (RepeaterItem sRepeaterItem in R44.Items)
				{

					CheckBox sfchkbox = (CheckBox)sRepeaterItem.FindControl("sfcb");
					HiddenField sfirmid = (HiddenField)sRepeaterItem.FindControl("sfid");
					HiddenField suid = (HiddenField)sRepeaterItem.FindControl("sid");

					if (sfchkbox.Checked)
					{
						try
						{
							FirmDetails ssfd = new FirmDetails();
							ssfd.Fid = fcount;
							ssfd.Fname = sfchkbox.Text;

							int ssuc = UpdateEnhancedEditFirmsforSubmitApproval(int.Parse(sfirmid.Value), int.Parse(suid.Value));
                            sflist.Add(ssfd);
							fcount = fcount + 1;

                            submittedFirmNames += "<br/>" + GetFirmDetails(int.Parse(sfirmid.Value));
                        }
						catch (Exception ex)
						{

							Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex);

						}

					}



				}

                // send out email to notify that submission is completed (Aleksei - 17/04/2019)
                if(!String.IsNullOrEmpty(submittedFirmNames))
                {
                    var name = User1.FirstName + " " + User1.LastName;
                    var customerEmail = User1.Email;
                    var messageSubject = "Notification - Premium listing submitted";
                    var sendersName = "Senders Name"; // TODO
                    var sendersContact = "Senders Contact"; // TODO
                    var associationName = "Association Name"; // TODO

                    var enhancedListingAdminNotificationEmailRecipient = ConfigurationManager.AppSettings["enhancedListingAdminNotificationEmailRecipient"];

                    // sending email to the customer
                    try
                    {
                        var tmpl = GetSDEmailTemplate("EnhancedListingNotificationPremiumListingSubmitted");
                        tmpl = tmpl.Replace("{Name}", name)
                                        .Replace("{Sender's Name}", sendersName)
                                        .Replace("{Sender's Contact}", sendersContact)
                                        .Replace("{Association's Name}", associationName);
                        var logId = CreateLogEmail(messageSubject, customerEmail, tmpl);
                        try
                        {
                            SoftwareDesign.Helper.SendEmail(messageSubject, tmpl, customerEmail);
                            UpdateLogEmail(logId, "Sent");
                        }
                        catch (Exception ex)
                        {
                            UpdateLogEmail(logId, "Failed - " + ex.Message);
                            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex);
                        }
                    }
                    catch (Exception ex)
                    {
                        var logId = CreateLogEmail(messageSubject, customerEmail, "");
                        UpdateLogEmail(logId, "Failed to load email template: EnhancedListingNotificationPremiumListingSubmitted");
                        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex);
                    }

                    // sending email to the administrator if necessary
                    if (!String.IsNullOrEmpty(enhancedListingAdminNotificationEmailRecipient))
                    {
                        try
                        {
                            var tmpl = GetSDEmailTemplate("EnhancedListingNotificationPremiumListingSubmittedAdmin");
                            tmpl = tmpl.Replace("{FirmName}", submittedFirmNames);
                            var logId = CreateLogEmail(messageSubject, enhancedListingAdminNotificationEmailRecipient, tmpl);
                            try
                            {
                                SoftwareDesign.Helper.SendEmail(messageSubject, tmpl, enhancedListingAdminNotificationEmailRecipient);
                                UpdateLogEmail(logId, "Sent");
                            }
                            catch (Exception ex)
                            {
                                UpdateLogEmail(logId, "Failed - " + ex.Message);
                                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex);
                            }
                        }
                        catch (Exception ex)
                        {
                            var logId = CreateLogEmail(messageSubject, enhancedListingAdminNotificationEmailRecipient, "");
                            UpdateLogEmail(logId, "Failed to load email template: EnhancedListingNotificationPremiumListingSubmittedAdmin");
                            Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex);
                        }
                    }
                    // ----
                }
            }

            // modal popup for submit confirmation 
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "showmodal();", true);


			// refresh after submission

			LoadFirms();

			
		}

		private DataTable BindIndustrySectors()
		{
			try
			{

				var parameters = new List<IDataParameter>();
				var sql = $"{AptifyApplication.GetEntityBaseDatabase("TopicCode")}..{spGetTopicCodeByParentID__c}";
				//parameters.Add(DataAction.GetDataParameter("@ParentID", SqlDbType.Int, 1393));
				parameters.Add(DataAction.GetDataParameter("@ParentID", SqlDbType.Int, (ConfigurationManager.AppSettings["enhancedListingIndustryTopicCodeID"])));
				parameters.Add(DataAction.GetDataParameter("@OrganizationID", SqlDbType.Int, -1));

				var dt = DataAction.GetDataTableParametrized(sql, CommandType.StoredProcedure, parameters.ToArray());
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
		private DataTable BindSpecialisms()
		{
			try
			{

				var parameters = new List<IDataParameter>();
				var sql = $"{AptifyApplication.GetEntityBaseDatabase("TopicCode")}..{spGetTopicCodeByParentID__c}";
				//	parameters.Add(DataAction.GetDataParameter("@ParentID", SqlDbType.Int, 1381));
				parameters.Add(DataAction.GetDataParameter("@ParentID", SqlDbType.Int, (ConfigurationManager.AppSettings["enhancedListingSpecialismsTopicCodeID"])));
				
				parameters.Add(DataAction.GetDataParameter("@OrganizationID", SqlDbType.Int, -1));

				var dt = DataAction.GetDataTableParametrized(sql, CommandType.StoredProcedure, parameters.ToArray());
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

		



		public class FirmDetails
        {
            public int Fid { get; set; }
            public string Fname { get; set; }

        }




    }
}
