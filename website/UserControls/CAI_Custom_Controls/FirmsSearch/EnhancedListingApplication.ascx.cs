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
using System.Web.UI.HtmlControls;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.FirmsSearch
{
    public partial class EnhancedListingApplication : Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced

    {
        private static string spGetFirmsbyFirmAdminByPersonId__cai => "spGetFirmsbyFirmAdminByPersonId__cai";
        private static string spGetPerson => "spGetPerson";
        private static string spGetFirmNameFullAddressByFirmId__cai => "spGetFirmNameFullAddressByFirmId__cai";
        private static string spgetFirmSubOfficesNameAddressByFirmid__cai => "spgetFirmSubOfficesNameAddressByFirmid__cai";
        //  public List<FirmDetails> Firms = new List<FirmDetails>();

        private List<EnhancedListingApplication.FirmDetails> AllExistingFirmsInAdminTable
        {
            get
            {
                if (ViewState["EnhancedListingAllExistingFirmsInAdminTable"] != null)
                {
                    return (List<EnhancedListingApplication.FirmDetails>)ViewState["EnhancedListingAllExistingFirmsInAdminTable"];
                } else
                {
                    var dbList = new List<FirmDetails>();
                    // lets get firms that belong to the admin user
                    var adminFirms = GetFirmAdminDetails();
                    List<int> adminFirmIds = new List<int>();
                    foreach(DataRow af in adminFirms.Rows)
                    {
                        adminFirmIds.Add(int.Parse(af["fid"].ToString()));
                    }

                    var adminFirmIdsStr = string.Join(",", adminFirmIds);

                    // check if there are no firms that belongs to logged in admin, just return empty
                    if(String.IsNullOrEmpty(adminFirmIdsStr))
                    {
                        AllExistingFirmsInAdminTable = new List<FirmDetails>();
                        return AllExistingFirmsInAdminTable;
                    }

                    var _enhancedListingRenewalDateAllowedAfterDays = ConfigurationManager.AppSettings["EnhancedListingRenewalDateAllowedAfterDays"];
                    if(String.IsNullOrEmpty(_enhancedListingRenewalDateAllowedAfterDays))
                    {
                        _enhancedListingRenewalDateAllowedAfterDays = "30";
                    }

                    var enhancedListingRenewalDateAllowedAfterDays = 30;
                    if(!int.TryParse(_enhancedListingRenewalDateAllowedAfterDays, out enhancedListingRenewalDateAllowedAfterDays))
                    {
                        enhancedListingRenewalDateAllowedAfterDays = 30;
                    }

                    // lets find out details about each purchased instance
                    var sSQL = "SELECT * FROM " + Database + ".." +
                        "vwEnhancedListingAdmin__cai WHERE FirmId IN ( " + adminFirmIdsStr + ") AND ApprovedEndDate > GETDATE()";
                    var dt1 = DataAction.GetDataTable(sSQL);
                    if(dt1.Rows.Count > 0)
                    {
                        foreach(DataRow row in dt1.Rows)
                        {
                            var newFirm = new FirmDetails()
                            {
                                Fid = Int32.Parse(row["FirmId"].ToString()),
                                Fname = row["FirmName"].ToString(),
                                PurchaseAllowed = row["ApproveStatus"].ToString().ToLower().Equals("true"),
                                IsRenewal = row["ApproveStatus"].ToString().ToLower().Equals("true"),
                                ApprovedEndDate = (DateTime)row["ApprovedEndDate"]
                            };

                            bool upForRenewal = DateTime.Now > newFirm.ApprovedEndDate.Value.AddDays(-enhancedListingRenewalDateAllowedAfterDays);
                            newFirm.IsRenewal = newFirm.IsRenewal && upForRenewal; // if approved status is 0, then no point setting renewal status

                            // first lets make sure that we do not already have same firm,
                            // in which case we need to update details in case same firm was purchased more than once (renewals)
                            if (dbList.Find(a => a.Fid == newFirm.Fid) != null)
                            {
                                var existingFirm = dbList.Find(a => a.Fid == newFirm.Fid);
                                if(existingFirm.ApprovedEndDate < newFirm.ApprovedEndDate)
                                {
                                    dbList.Remove(existingFirm);
                                    dbList.Add(newFirm);
                                }
                            } else
                            {
                                dbList.Add(newFirm);
                            }
                        }
                    }
                    // -----

                    AllExistingFirmsInAdminTable = dbList;
                    return AllExistingFirmsInAdminTable;
                }
            }
            set
            {
                ViewState["EnhancedListingAllExistingFirmsInAdminTable"] = value;
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlChooseFirms.Visible = true;
                PaymentStep.Visible = false;
                LoadFirms();
            }
            else
            { }
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

        public void GetFirms(DataTable fdt)
        {
            DataTable dty = new DataTable();
            dty.Columns.Add("fid");
            dty.Columns.Add("fname");
            
            FirmDetails fdet = new FirmDetails();

            var atLeastOneAvailableForSale = false;

            foreach (DataRow dr in fdt.Rows)
            {
                DataRow row = dty.NewRow();
                row["fid"] = dr["fid"];
                row["fname"] = GetFirmDetails(Convert.ToInt32(dr["fid"].ToString()));

                dty.Rows.Add(row);

                var firm = AllExistingFirmsInAdminTable.Find(x => x.Fid == int.Parse(dr["fid"].ToString()));

                if (firm != null)
                {
                    if (firm.PurchaseAllowed && firm.IsRenewal)
                    {
                        atLeastOneAvailableForSale = true;
                    }
                } else
                {
                    atLeastOneAvailableForSale = true;
                }
            }

            if (dty.Rows.Count > 0)
            {
                parentFirmRepeater.DataSource = dty;
                parentFirmRepeater.DataBind();
            }

            if(atLeastOneAvailableForSale)
            {
                btnProceedToPayment.Visible = true;
            } else
            {
                btnProceedToPayment.Visible = false;
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
                    PaymentStep.UserFirmID = Convert.ToInt32(dt.Rows[0]["CompanyID"].ToString());
                    PaymentStep.UserFirmName = lFirmName.Text;
                }
            }
            catch (Exception ex)
            {
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex);
            }
        }


        public void LoadFirms()
        {

            DataTable dt = new DataTable();
            
            string Loginpage = "/Login.aspx";
			string Noaccess = "/securityerror.aspx?Message=Access+to+the+requested+resource+has+been+denied";
			if (User1.PersonID <= 0)
            {
				Session["ReturnToPage"] = Request.RawUrl;
				Response.Redirect(Loginpage);
            }
            else
            {

                dt = GetFirmAdminDetails();
                if (dt.Rows.Count > 0)
                {
                    // get userdeatils  FirstLast & firmname with address 
                    GetUserDetails();
                    // 
                    GetFirms(dt);
                }
                else
                {

					//unauthorized access
					Response.Redirect(Noaccess);

				}
            }
        }

        protected void btnProceedToPayment_Click(object sender, EventArgs e)
        {
            List<FirmDetails> selectedList = new List<FirmDetails>();
            bool atLeastOneParentFirmNotSelected = false;

            // preparing selected firms
            foreach (RepeaterItem pRepeaterItem in parentFirmRepeater.Items)
            {
                CheckBox pfcb = (CheckBox)pRepeaterItem.FindControl("pfcb");
                HiddenField pfcv = (HiddenField)pRepeaterItem.FindControl("pfcv");
                Panel pflblError = (Panel)pRepeaterItem.FindControl("pflblError");
                pflblError.Visible = false;

                var parentFid = int.Parse(pfcv.Value);
                bool parentChecked = false;

                if (pfcb.Checked)
                {
                    parentChecked = true;
                    var firm = AllExistingFirmsInAdminTable.Find(x => x.Fid == parentFid);

                    if(firm == null)
                    {
                        firm = new FirmDetails
                        {
                            Fid = parentFid,
                            Fname = pfcb.Text,
                            IsParent = true,
                            PurchaseAllowed = true
                        };
                    } else
                    {
                        firm.IsParent = true;
                    }

                    firm.Fname = pfcb.Text;

                    selectedList.Add(firm);
                }

                // lets check if there are any items in the children
                Repeater childFirmRepeater = (Repeater)pRepeaterItem.FindControl("childFirmRepeater");

                if(childFirmRepeater.Visible && childFirmRepeater.Items.Count > 0)
                {
                    foreach (RepeaterItem pcRepeaterItem in childFirmRepeater.Items)
                    {
                        pfcb = (CheckBox)pcRepeaterItem.FindControl("pfcb");
                        pfcv = (HiddenField)pcRepeaterItem.FindControl("pfcv");
                        var childFid = int.Parse(pfcv.Value);
                        if (pfcb.Checked && parentChecked)
                        {
                            var firm = AllExistingFirmsInAdminTable.Find(x => x.Fid == childFid);

                            if (firm == null)
                            {
                                firm = new FirmDetails
                                {
                                    Fid = childFid,
                                    Fname = pfcb.Text,
                                    IsParent = false,
                                    ParentId = parentFid,
                                    PurchaseAllowed = true
                                };
                            }
                            else
                            {
                                firm.IsParent = false;
                                firm.ParentId = parentFid;
                            }

                            firm.Fname = pfcb.Text;

                            selectedList.Add(firm);
                        } else if(pfcb.Checked && !parentChecked)
                        {
                            pflblError.Visible = true;
                            atLeastOneParentFirmNotSelected = true;
                        }
                    }
                }
            }

            if(atLeastOneParentFirmNotSelected)
            {
                return;
            }

            // lets verify that there are paid listings available
            PaymentStep.SelectedFirms = selectedList;

            // checking if at least one firm is selected
            if (PaymentStep.PayForFirms.Count > 0)
            {
                PaymentStep.PreparePayment();
                PaymentStep.Visible = true;
                pnlChooseFirms.Visible = false;
                warningSelectFirm.Visible = false;
            } else
            {
                // show error to the user
                warningSelectFirm.Visible = true;
				
			}
        }

        protected void parentFirmRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {
                CheckBox pfcb = (CheckBox)e.Item.FindControl("pfcb");
                HiddenField pfcv = (HiddenField)e.Item.FindControl("pfcv");
                Repeater childFirmRepeater = (Repeater)e.Item.FindControl("childFirmRepeater");

                var id = Convert.ToInt32(pfcv.Value);
                var sdt = GetFirmSubOffices(id);

                if (sdt != null && sdt.Rows.Count > 0)
                {
                    // lets check if all of the kids can not be purchased, then disable parent
                    var allKidsDisabled = true;
                    foreach(DataRow row in sdt.Rows)
                    {
                        var cid = Convert.ToInt32(row["fid"]);
                        allKidsDisabled &= IsCheckboxDisabled(cid);
                        if(!allKidsDisabled)
                        {
                            break;
                        }
                    }

                    if(allKidsDisabled && IsCheckboxDisabled(id))
                    {
                        pfcb.Enabled = false;
                    } else
                    {
                        pfcb.Enabled = true;
                    }

                    childFirmRepeater.DataSource = sdt;
                    childFirmRepeater.DataBind();

                    childFirmRepeater.Visible = true;
                }
                else
                {
                    childFirmRepeater.Visible = false;
                    pfcb.Enabled = !IsCheckboxDisabled(id);
                }
            }
        }

        protected void childFirmRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CheckBox pfcb = (CheckBox)e.Item.FindControl("pfcb");
                HiddenField pfcv = (HiddenField)e.Item.FindControl("pfcv");
                var id = Convert.ToInt32(pfcv.Value);
                pfcb.Enabled = !IsCheckboxDisabled(id);
            }
        }

        private DataTable GetFirmSubOffices(int id)
        {
            var par = new List<IDataParameter>();
            try
            {
                var sql = $"{AptifyApplication.GetEntityBaseDatabase("Firm")}..{spgetFirmSubOfficesNameAddressByFirmid__cai}";
                var param = DataAction.GetDataParameter("@fId", SqlDbType.Int, id);
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

        public bool IsCheckboxDisabled(int _fid)
        {
            var firm = AllExistingFirmsInAdminTable.Find(x => x.Fid == _fid);

            if(firm != null)
            {
                return !firm.PurchaseAllowed || !firm.IsRenewal;
            }

            return false;
        }

        public String GetFirmStatus(String fid)
        {
            int _fid = Int32.Parse(fid);
            String status = "";

            var firm = AllExistingFirmsInAdminTable.Find(x => x.Fid == _fid);

            if (firm != null)
            {
                // checking if firm is in status to be allowed to purchase and up for renewal
                if(firm.PurchaseAllowed && firm.IsRenewal)
                {
                    status = "<span class='firm-status-renewal'>(renewal due)</span>";
                } else if(firm.PurchaseAllowed && !firm.IsRenewal)
                {
                    //status = "<span class='firm-status-active'>(Active until " + firm.ApprovedEndDate.Value.ToString("dd/MM/yyyy H:mm") + ")</span>";
                    status = "<span class='firm-status-active'>(Active until " + firm.ApprovedEndDate.Value.ToString("dd MMMM yyyy h:mm tt") + ")</span>";
                    // TODO: display (pending edit) if !IsDraft is not found
                } else if(!firm.PurchaseAllowed)
                {
                    status ="<span class='firm-status-pending'>(pending approval)</span>";
                }
            }

            return " " + status;
        }


        [Serializable]
        public class FirmDetails
        {
            public int Fid { get; set; }
            public string Fname { get; set; }
            public bool IsParent { get; set; }
            public int ParentId { get; set; }
            public bool PurchaseAllowed { get; set; }
            public bool IsRenewal { get; set; }
            public DateTime? ApprovedEndDate { get; set; }
        }
    }
}
