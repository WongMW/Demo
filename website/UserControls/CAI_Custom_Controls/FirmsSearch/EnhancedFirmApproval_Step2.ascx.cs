using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.FirmsSearch
{
    public partial class EnhancedFirmApproval_Step2 : Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
    {
        private readonly string spGetEnhancedListingEditFormByDraftStatus__cai = "spGetEnhancedListingEditFormByDraftStatus__cai";
        private readonly string spGetCompany = "spGetCompany";
        private readonly string spCreateEnhancedListingEditForm__cai = "spCreateEnhancedListingEditForm__cai";
        private readonly string spUpdateEnhancedListingEditForm__cai = "spUpdateEnhancedListingEditForm__cai";
        private readonly string spUpdateEnhancedListingAdmin__cai = "spUpdateEnhancedListingAdmin__cai";
        private readonly string spGetEnhancedListingFirmAdminDetailsByFirmId__cai = "spGetEnhancedListingFirmAdminDetailsByFirmId__cai";
        private static string spUpdateEnhancedListingEmailLog => "spUpdateEnhancedListingEmailLog";
        private static string spCreateEnhancedListingEmailLog => "spCreateEnhancedListingEmailLog";

        public int PersonId
        {
            get
            {
                var val = (int?)ViewState["EnhancedListingAdminApprove"];

                return val.HasValue ? val.Value : 0;
            }
            set
            {
                ViewState["EnhancedListingAdminApprove"] = value;
            }
        }

        #region save button event
        public class SaveEventArgs : EventArgs
        {
            public Dictionary<int, String> ApprovedFirms { get; set; }
            public Dictionary<int, String> RejectedFirms { get; set; }
            public SaveEventArgs(Dictionary<int, String> approvedFirms = null, Dictionary<int, String> rejectedFirms = null)
            {
                ApprovedFirms = approvedFirms;
                RejectedFirms = rejectedFirms;
            }
        }
        public event EventHandler<SaveEventArgs> SaveButtonClicked;

        protected virtual void OnSaveButtonClicked(SaveEventArgs e)
        {
            EventHandler<SaveEventArgs> handler = SaveButtonClicked;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        #endregion

        #region back button event
        public event EventHandler BackButtonClicked;

        protected virtual void OnBackButtonClicked(EventArgs e)
        {
            EventHandler handler = BackButtonClicked;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private List<EnhancedListingAdminFirm> GetDataSource()
        {
            var parameters = new List<IDataParameter>();
            parameters.Add(DataAction.GetDataParameter("@IsDraft", SqlDbType.Int, 1));
            parameters.Add(DataAction.GetDataParameter("@WebStatus", SqlDbType.Int, 2));
            parameters.Add(DataAction.GetDataParameter("@PersonId", SqlDbType.Int, PersonId));

            var sql = $"{AptifyApplication.GetEntityBaseDatabase("Firm")}..{spGetEnhancedListingEditFormByDraftStatus__cai}";
            var dt = DataAction.GetDataTableParametrized(sql, CommandType.StoredProcedure, parameters.ToArray());

            var data = new List<EnhancedListingAdminFirm>();
            foreach (DataRow row in dt.Rows)
            {
                var obj = new EnhancedListingAdminFirm();
                obj.FillData(row);
                obj.IsApproveAllowed = true;

                data.Add(obj);
            }

            // lets sort all firms by parent ID
            Dictionary<int, List<EnhancedListingAdminFirm>> sortedDictionary = new Dictionary<int, List<EnhancedListingAdminFirm>>();
            foreach (var d in data)
            {
                // checking if parent
                if (d.IsParent)
                {
                    // if same firm has already been added, then something is WRONG
                    if (!sortedDictionary.ContainsKey(d.FirmID))
                    {
                        sortedDictionary.Add(d.FirmID, new List<EnhancedListingAdminFirm>());
                    }
                }
            }
            List<EnhancedListingAdminFirm> missingParentFirms = new List<EnhancedListingAdminFirm>();
            List<int> missingParentFirmIds = new List<int>();
            foreach (var d in data)
            {
                // checking if not parent
                if (!d.IsParent)
                {
                    // if parent exists, then 
                    if (!sortedDictionary.ContainsKey(d.ParentID) && !missingParentFirmIds.Contains(d.ParentID))
                    {
                        missingParentFirmIds.Add(d.ParentID);
                        var missingObj = new EnhancedListingAdminFirm();
                        // lets find details for the parent firm
                        parameters = new List<IDataParameter>();
                        parameters.Add(DataAction.GetDataParameter("@ID", SqlDbType.Int, d.ParentID));

                        sql = $"{AptifyApplication.GetEntityBaseDatabase("Firm")}..{spGetCompany}";
                        dt = DataAction.GetDataTableParametrized(sql, CommandType.StoredProcedure, parameters.ToArray());
                        // lets check if found results
                        if (dt.Rows.Count > 0)
                        {
                            DataRow row = dt.Rows[0];
                            missingObj = new EnhancedListingAdminFirm()
                            {
                                FirmID = int.Parse(row["ID"].ToString()),
                                ID = 0,
                                ParentID = 0,
                                FirmName = row["Name"].ToString(),
                                IsApproveAllowed = false
                            };
                        }
                        else
                        {
                            // TODO: REMOVE, only for testing purposes
                            missingObj.FirmName = d.ParentID + " is missing from the database!!!";
                        }

                        missingParentFirms.Add(missingObj);
                    }

                    if (!sortedDictionary.ContainsKey(d.ParentID))
                    {
                        sortedDictionary.Add(d.ParentID, new List<EnhancedListingAdminFirm>());
                    }

                    var lll = sortedDictionary[d.ParentID];
                    lll.Add(d);
                    sortedDictionary[d.ParentID] = lll;
                }
            }

            // checking if found some missing parent ID's
            if (missingParentFirms.Count > 0)
            {
                data.AddRange(missingParentFirms);
            }

            // now lets build correct sorted dictionary as datasource
            var dataSource = new List<EnhancedListingAdminFirm>();
            foreach (int parentFirmId in sortedDictionary.Keys)
            {
                var list = sortedDictionary[parentFirmId];
                // first lets build parent, then every child in sequence
                var pObj = data.Find(a => a.FirmID == parentFirmId);
                dataSource.Add(pObj);
                dataSource.AddRange(list);
            }

            return dataSource;
        }

        public void Bind(int personId)
        {
            errorMessage.Visible = false;
            PersonId = personId;
            var dataSource = GetDataSource();
            
            offices.DataSource = dataSource;
            offices.DataBind();

            String firmIds = String.Empty;
            foreach(var ds in dataSource)
            {
                if(!String.IsNullOrEmpty(firmIds))
                {
                    firmIds += ",";
                }
                firmIds += ds.FirmID;
            }

            var parameters = new List<IDataParameter>();
            parameters.Add(DataAction.GetDataParameter("@FirmId", SqlDbType.NVarChar, firmIds));

            var sql = $"{AptifyApplication.GetEntityBaseDatabase("Firm")}..{spGetEnhancedListingFirmAdminDetailsByFirmId__cai}";
            var dt = DataAction.GetDataTableParametrized(sql, CommandType.StoredProcedure, parameters.ToArray());
            List<String> listOfAdmins = new List<String>();
            if(dt.Rows.Count > 0)
            {
                foreach(DataRow row in dt.Rows)
                {
                    String strPersonId = row["PersonID"].ToString();
                    // only retrieve person that has purchased
                    if(!strPersonId.Equals(personId.ToString()))
                    {
                        continue;
                    }
                    String email = row["email"].ToString();
                    String name = row["name"].ToString();
                    String adminDetails = String.Empty;
                    if(!String.IsNullOrEmpty(name))
                    {
                        if(!String.IsNullOrEmpty(adminDetails))
                        {
                            adminDetails += ", ";
                        }
                        adminDetails += name;
                    }
                    if (!String.IsNullOrEmpty(email))
                    {
                        if (!String.IsNullOrEmpty(adminDetails))
                        {
                            adminDetails += ", ";
                        }
                        adminDetails += email;
                    }
                    if(!listOfAdmins.Contains(adminDetails))
                    {
                        listOfAdmins.Add(adminDetails);
                    }
                }
            }

            if(listOfAdmins.Count > 0)
            {
                contactList.DataSource = listOfAdmins;
                contactList.DataBind();
                contactList.Visible = true;
            } else
            {
                contactList.Visible = false;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            OnBackButtonClicked(e);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var recordsToApprove = new List<int>();
            var recordsToReject = new List<int>();
            var allRecordsToApproveReject = new List<int>();

            // lets see if any firms were checked
            foreach (RepeaterItem item in offices.Items)
            {
                var chkApprove = (CheckBox)item.FindControl("chkApprove");
                if(chkApprove != null)
                {
                    if(chkApprove.Checked)
                    {
                        HtmlAnchor linkPreview = (HtmlAnchor)item.FindControl("linkPreview");
                        var recordId = linkPreview.Attributes["data-id"].ToString();
                        int rId = 0;
                        int.TryParse(recordId, out rId);
                        if(rId > 0)
                        {
                            recordsToApprove.Add(rId);
                        }
                    }
                    // TODO, check if to be not approved (rejected)
                    // 
                    // -------
                }
            }

            // lets verify that records to approve can actually be approved
            var dataSource = GetDataSource();
            String allFirmIds = String.Empty;

            foreach (var ds in dataSource)
            {
                if (!String.IsNullOrEmpty(allFirmIds))
                {
                    allFirmIds += ",";
                }
                allFirmIds += ds.FirmID;
            }

            allRecordsToApproveReject.AddRange(recordsToApprove);
            allRecordsToApproveReject.AddRange(recordsToReject);

            for (var i = 0; i < allRecordsToApproveReject.Count; i++)
            {
                int rId = allRecordsToApproveReject[i];
                var objToCheck = new EnhancedListingAdminFirm()
                {
                    ID = rId
                };

                bool found = false;

                if (dataSource.Contains(objToCheck))
                {
                    objToCheck = dataSource.Find(a => a.ID == objToCheck.ID);
                    if (objToCheck.IsApproveAllowed)
                    {
                        found = true;
                    }
                }

                if (!found)
                {
                    allRecordsToApproveReject.RemoveAt(i);
                    i--;
                }
            }

            if(allRecordsToApproveReject.Count > 0)
            {
                errorMessage.Visible = false;
                // lets approve/reject records
                var parameters = new List<IDataParameter>();
                parameters.Add(DataAction.GetDataParameter("@IsDraft", SqlDbType.Int, 0));
                parameters.Add(DataAction.GetDataParameter("@PersonId", SqlDbType.Int, PersonId));

                var sql = $"{AptifyApplication.GetEntityBaseDatabase("Firm")}..{spGetEnhancedListingEditFormByDraftStatus__cai}";
                var dt = DataAction.GetDataTableParametrized(sql, CommandType.StoredProcedure, parameters.ToArray());
                var dataTarget = new List<EnhancedListingAdminFirm>();
                foreach(DataRow row in dt.Rows)
                {
                    var obj = new EnhancedListingAdminFirm();
                    obj.FillData(row);
                    dataTarget.Add(obj);
                }

                Dictionary<int, String> listOfApprovedFirms = new Dictionary<int, String>();
                Dictionary<int, String> listOfRejectedFirms = new Dictionary<int, String>();


                var trans = DataAction.BeginTransaction();

                try
                {
                    String listOfFirmNamesApproved = String.Empty;

                    // lets go through each record to be approved/rejected and create/update it
                    foreach (int rId in allRecordsToApproveReject)
                    {
                        bool toBeApproved = false;
                        // lets identify if record to be rejected or approved
                        if (recordsToApprove.Contains(rId))
                        {
                            toBeApproved = true;
                        }

                        // lets first find original record
                        EnhancedListingAdminFirm originalRecord = dataSource.Find(a => a.ID == rId);
                        DataRow originalRow = originalRecord.Row;

                        // lets check if record to be rejected, then update original record with this status
                        if (!toBeApproved)
                        {
                            listOfRejectedFirms.Add(rId, originalRow["FirmName"].ToString());
                            // TODO: rejection by updating original record with status rejected
                        }
                        else
                        {
                            listOfApprovedFirms.Add(rId, originalRow["FirmName"].ToString());
                            if(!String.IsNullOrEmpty(listOfFirmNamesApproved))
                            {
                                listOfFirmNamesApproved += "<br/> ";
                            }
                            listOfFirmNamesApproved += originalRow["FirmName"].ToString();

                            // TODO: calculate start and end dates
                            DateTime startDate = DateTime.Now;
                            DateTime endDate = DateTime.Now.AddYears(1);
                            // -----
                            // TODO: get approved by username
                            String approvedBy = User1.FirstName;
                            if (!String.IsNullOrEmpty(approvedBy) && !String.IsNullOrEmpty(User1.LastName))
                            {
                                approvedBy += " ";
                            }

                            if (!String.IsNullOrEmpty(User1.LastName))
                            {
                                approvedBy += User1.LastName;
                            }
                            // ----
                            // lets approve original record
                            List<IDataParameter> param = new List<IDataParameter>();
                            param.Add(DataAction.GetDataParameter("@ID", SqlDbType.Int, originalRow["ID"]));
                            param.Add(DataAction.GetDataParameter("@ApprovedBy", SqlDbType.NVarChar, approvedBy));
                            param.Add(DataAction.GetDataParameter("@StartDate", SqlDbType.DateTime, startDate));
                            param.Add(DataAction.GetDataParameter("@EndDate", SqlDbType.DateTime, endDate));
                            param.Add(DataAction.GetDataParameter("@WebStatus", SqlDbType.Int, 4));

                            var sSql = Database + ".." + spUpdateEnhancedListingEditForm__cai;
                            DataAction.ExecuteNonQueryParametrized(sSql, CommandType.StoredProcedure, param.ToArray(), trans);
                            // ------

                            // Update EnhancedListingAdminID based on originalRow["ELAdminID"] with new start and end date
                            param = new List<IDataParameter>();
                            param.Add(DataAction.GetDataParameter("@ID", SqlDbType.Int, originalRow["ELAdminID"]));
                            param.Add(DataAction.GetDataParameter("@ApprovedStartDate", SqlDbType.DateTime, startDate));
                            param.Add(DataAction.GetDataParameter("@ApprovedEndDate", SqlDbType.DateTime, endDate));
                            param.Add(DataAction.GetDataParameter("@ApproveStatus", SqlDbType.Int, 1));
                            param.Add(DataAction.GetDataParameter("@StatusMessage", SqlDbType.NVarChar, "Approved"));

                            sSql = Database + ".." + spUpdateEnhancedListingAdmin__cai;
                            DataAction.ExecuteNonQueryParametrized(sSql, CommandType.StoredProcedure, param.ToArray(), trans);
                            // ----

                            param = new List<IDataParameter>();

                            param.Add(DataAction.GetDataParameter("@FirmId", SqlDbType.Int, originalRow["FirmId"]));
                            param.Add(DataAction.GetDataParameter("@FirmName", SqlDbType.NVarChar, originalRow["FirmName"]));
                            param.Add(DataAction.GetDataParameter("@NoOfEmployees ", SqlDbType.NVarChar, originalRow["NoOfEmployees"]));
                            param.Add(DataAction.GetDataParameter("@NoOfPartners", SqlDbType.Int, originalRow["NoOfPartners"]));
                            param.Add(DataAction.GetDataParameter("@FirmDescription", SqlDbType.NVarChar, originalRow["FirmDescription"]));
                            param.Add(DataAction.GetDataParameter("@LocationURL", SqlDbType.NVarChar, originalRow["LocationURL"]));
                            param.Add(DataAction.GetDataParameter("@WebStatus", SqlDbType.Int, 4));
                            param.Add(DataAction.GetDataParameter("@ApprovedBy", SqlDbType.NVarChar, approvedBy));
                            param.Add(DataAction.GetDataParameter("@ApprovedDate", SqlDbType.DateTime, DateTime.Now));
                            param.Add(DataAction.GetDataParameter("@LastUpdateDate", SqlDbType.DateTime, DateTime.Now));
                            param.Add(DataAction.GetDataParameter("@ParentId", SqlDbType.Int, originalRow["ParentId"]));
                            param.Add(DataAction.GetDataParameter("@Specialisms", SqlDbType.NVarChar, originalRow["Specialisms"]));
                            param.Add(DataAction.GetDataParameter("@IndustrySector", SqlDbType.NVarChar, originalRow["IndustrySector"]));
                            param.Add(DataAction.GetDataParameter("@LogoURL", SqlDbType.NVarChar, originalRow["LogoURL"]));
                            param.Add(DataAction.GetDataParameter("@IsDraft", SqlDbType.Bit, 0));
                            param.Add(DataAction.GetDataParameter("@IsActive", SqlDbType.Bit, 1));
                            param.Add(DataAction.GetDataParameter("@StartDate", SqlDbType.DateTime, startDate));
                            param.Add(DataAction.GetDataParameter("@EndDate", SqlDbType.DateTime, endDate));
                            param.Add(DataAction.GetDataParameter("@PersonId", SqlDbType.Int, originalRow["PersonId"]));
                            param.Add(DataAction.GetDataParameter("@ELAdminID", SqlDbType.Int, originalRow["ELAdminID"]));
                            param.Add(DataAction.GetDataParameter("@UpdatedBy", SqlDbType.NVarChar, approvedBy));

                            // record to be approved
                            // lets find out if new record already been created
                            if (dataTarget.Contains(new EnhancedListingAdminFirm()
                            {
                                FirmID = originalRecord.FirmID
                            }))
                            {
                                // record exists, therefore we need to update existing record with new data
                                // 
                            }
                            else
                            {
                                // record does not exist, therefore we need to create new record
                                // spCreateEnhancedListingEditForm__cai
                                sSql = Database + ".." + spCreateEnhancedListingEditForm__cai;

                                var idParam = DataAction.GetDataParameter("@ID", System.Data.SqlDbType.Int);
                                idParam.Direction = ParameterDirection.Output;
                                param.Add(idParam);
                                param.Add(DataAction.GetDataParameter("@DateCreated", SqlDbType.DateTime, DateTime.Now));
                                var eeid = Convert.ToInt32(DataAction.ExecuteNonQueryParametrized(sSql, CommandType.StoredProcedure, param.ToArray(), trans));
                            }
                        }
                    }

                    DataAction.CommitTransaction(trans);
                    //DataAction.RollbackTransaction(trans);

                    var messageSubject = "Notification - Premium listing activation";
                    var sendersName = "Senders Name"; // TODO
                    var sendersContact = "Senders Contact"; // TODO
                    var associationName = "Association Name"; // TODO

                    try
                    {
                        var template = GetSDEmailTemplate("EnhancedListingNotificationPremiumListingActivation");

                        if (!String.IsNullOrEmpty(template))
                        {
                            // lets retrieve emails that need to receive notification
                            String firmNamesList = String.Empty;
                            foreach (var key in listOfApprovedFirms.Keys)
                            {
                                if(!String.IsNullOrEmpty(firmNamesList))
                                {
                                    firmNamesList += ",";
                                }
                                firmNamesList += listOfApprovedFirms[key];
                            }

                            parameters = new List<IDataParameter>();
                            parameters.Add(DataAction.GetDataParameter("@FirmId", SqlDbType.NVarChar, allFirmIds));

                            sql = $"{AptifyApplication.GetEntityBaseDatabase("Firm")}..{spGetEnhancedListingFirmAdminDetailsByFirmId__cai}";
                            dt = DataAction.GetDataTableParametrized(sql, CommandType.StoredProcedure, parameters.ToArray());

                            Dictionary<String, String> receipients = new Dictionary<string, string>();

                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow row in dt.Rows)
                                {
                                    String strPersonId = row["PersonID"].ToString();
                                    // only retrieve person that has purchased
                                    if (!strPersonId.Equals(PersonId.ToString()))
                                    {
                                        continue;
                                    }
                                    String email = row["email"].ToString();
                                    String name = row["name"].ToString();
                                    if (!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(email) && !receipients.ContainsKey(email))
                                    {
                                        receipients.Add(email, name);
                                    }
                                }
                            }

                            // checking if there is at least one recipient
                            if(receipients.Keys.Count > 0)
                            {
                                foreach(var email in receipients.Keys)
                                {
                                    var tmpl = template;
                                    tmpl = tmpl.Replace("{Name}", receipients[email])
                                        .Replace("{ListOfFirms}", listOfFirmNamesApproved)
                                        .Replace("{Sender's Name}", sendersName)
                                        .Replace("{Sender's Contact}", sendersContact)
                                        .Replace("{Association's Name}", associationName);
                                    var logId = CreateLogEmail(messageSubject, email, tmpl);
                                    try
                                    {
                                        SoftwareDesign.Helper.SendEmail(messageSubject, tmpl, email);
                                        UpdateLogEmail(logId, "Sent");
                                    } catch(Exception ex)
                                    {
                                        UpdateLogEmail(logId, "Failed - " + ex.Message);
                                        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex);
                                    }
                                }
                            }
                        }
                    } catch(Exception ex)
                    {
                        Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex);
                    }

                    // ----

                    OnSaveButtonClicked(new SaveEventArgs(listOfApprovedFirms, listOfRejectedFirms));
                }
                catch (Exception ex)
                {
                    DataAction.RollbackTransaction(trans);
                    // lets display error message
                    errorMessage.InnerText = "There was an error processing this request! (" + ex.Message + ")";
                    errorMessage.Visible = true;
                }
            }
            else
            {
                // lets display error message
                errorMessage.InnerText = "Please select firms to be approved!";
                errorMessage.Visible = true;
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
            var p1 = DataAction.GetDataParameter("@ID", System.Data.SqlDbType.Int);
            p1.Direction = ParameterDirection.Output;
            param.Add(p1);
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

        protected void offices_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlAnchor linkPreview = (HtmlAnchor)e.Item.FindControl("linkPreview");
                RadWindow previewWindow = (RadWindow)e.Item.FindControl("viewChanges");
                previewWindow.OpenerElementID = linkPreview.ClientID;

                // checking if we have all information
                EnhancedListingAdminFirm data = (EnhancedListingAdminFirm)e.Item.DataItem;
                if (data.Row != null)
                {
                    HtmlGenericControl txtCompanyName = (HtmlGenericControl)previewWindow.ContentContainer.FindControl("txtCompanyName");
                    HtmlGenericControl txtDescription = (HtmlGenericControl)previewWindow.ContentContainer.FindControl("txtDescription");
                    Image img = (Image)previewWindow.ContentContainer.FindControl("img");
                    HtmlGenericControl txtNumberOfEmployees = (HtmlGenericControl)previewWindow.ContentContainer.FindControl("txtNumberOfEmployees");
                    HtmlGenericControl txtNumberOfPartners = (HtmlGenericControl)previewWindow.ContentContainer.FindControl("txtNumberOfPartners");
                    HtmlGenericControl txtSectors = (HtmlGenericControl)previewWindow.ContentContainer.FindControl("txtSectors");
                    HtmlGenericControl txtSpecialisms = (HtmlGenericControl)previewWindow.ContentContainer.FindControl("txtSpecialisms");
                    HyperLink googleLink = (HyperLink)previewWindow.ContentContainer.FindControl("googleLink");

                    String firmName = data.Row["FirmName"].ToString();
                    String firmDescription = data.Row["FirmDescription"].ToString();
                    String noOfEmployees = data.Row["NoOfEmployees"].ToString();
                    String noOfPartners = data.Row["NoOfPartners"].ToString();
                    String specialisms = data.Row["Specialisms"].ToString();
                    String industrySector = data.Row["IndustrySector"].ToString();
                    String locationURL = data.Row["LocationURL"].ToString();
                    String logoURL = data.Row["LogoURL"].ToString();

                    if (!String.IsNullOrEmpty(firmName))
                    {
                        txtCompanyName.InnerText = firmName;
                    } else
                    {
                        txtCompanyName.Visible = false;
                    }

                    if (!String.IsNullOrEmpty(firmDescription))
                    {
                        txtDescription.InnerText = firmDescription;
                    }
                    else
                    {
                        txtDescription.InnerText = "N/A";
                    }

                    if (!String.IsNullOrEmpty(noOfEmployees))
                    {
                        txtNumberOfEmployees.InnerText = noOfEmployees;
                    }
                    else
                    {
                        txtNumberOfEmployees.InnerText = "N/A";
                    }

                    if (!String.IsNullOrEmpty(noOfPartners))
                    {
                        txtNumberOfPartners.InnerText = noOfPartners;
                    }
                    else
                    {
                        txtNumberOfPartners.InnerText = "N/A";
                    }

                    if (!String.IsNullOrEmpty(specialisms))
                    {
                        txtSpecialisms.InnerText = specialisms;
                    }
                    else
                    {
                        txtSpecialisms.InnerText = "N/A";
                    }

                    if (!String.IsNullOrEmpty(industrySector))
                    {
                        txtSectors.InnerText = industrySector;
                    }
                    else
                    {
                        txtSectors.InnerText = "N/A";
                    }

                    if (!String.IsNullOrEmpty(logoURL))
                    {
                        img.ImageUrl = Page.ResolveUrl(logoURL);
                    }
                    else
                    {
                        img.Visible = false;
                    }

                    if (!String.IsNullOrEmpty(locationURL))
                    {
                        googleLink.NavigateUrl = locationURL;
                        googleLink.Target = "_blank";
                    }
                    else
                    {
                        googleLink.Visible = false;
                    }
                }
            }
        }
    }

    public struct EnhancedListingAdminFirm
    {
        public DataRow Row { get; set; }
        public int ID { get; set; }
        public int FirmID { get; set; }
        public int ParentID { get; set; }
        public String FirmName { get; set; }
        public Boolean IsApproveAllowed { get; set; }
        public Boolean IsParent {
            get
            {
                return ParentID == 0;
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return ((EnhancedListingAdminFirm)obj).ID == ID || ((EnhancedListingAdminFirm)obj).FirmID == FirmID;
        }

        public void FillData(DataRow row)
        {
            FirmID = int.Parse(row["FirmId"].ToString());
            ID = int.Parse(row["ID"].ToString());
            ParentID = !String.IsNullOrEmpty(row["ParentId"].ToString()) ? int.Parse(row["ParentId"].ToString()) : 0;
            FirmName = row["FirmName"].ToString();
            Row = row;
        }
    }
}
