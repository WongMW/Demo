using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.FirmsSearch
{
    public partial class EnhancedFirmApproval_Step1 : Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
    {
        private readonly string spGetEnhancedListingEditFormByDraftStatus__cai = "spGetEnhancedListingEditFormByDraftStatus__cai";
        protected void Page_Load(object sender, EventArgs e)
        {
            step2.BackButtonClicked += Step2_BackButtonClicked;
            step2.SaveButtonClicked += Step2_SaveButtonClicked;
            Page.EnableViewState = true;

            if (!IsPostBack)
            {
                // bind list of companies
                BindListOfApprovalsAwaiting();
            }
        }

        private void BindListOfApprovalsAwaiting()
        {
            var parameters = new List<IDataParameter>();
            parameters.Add(DataAction.GetDataParameter("@IsDraft", SqlDbType.Int, 1));
            parameters.Add(DataAction.GetDataParameter("@WebStatus", SqlDbType.Int, 2));

            var sql = $"{AptifyApplication.GetEntityBaseDatabase("Firm")}..{spGetEnhancedListingEditFormByDraftStatus__cai}";
            var dt = DataAction.GetDataTableParametrized(sql, CommandType.StoredProcedure, parameters.ToArray());

            if(dt.Rows.Count > 0)
            {
                noApprovalNeeded.Visible = false;
                approvalNeeded.Visible = true;
                
                var data = new List<ApprovalUser>();
                foreach (DataRow row in dt.Rows)
                {
                    var obj = new ApprovalUser()
                    {
                        AdminName = row["AdminName"].ToString(),
                        FirstSubmittedDate = DateTime.Parse(row["LastUpdateDate"].ToString()),
                        PendingCount = 1,
                        PersonId = int.Parse(row["PersonId"].ToString())
                    };

                    // lets check if already exists, then need to check pending date
                    if(data.Contains(obj))
                    {
                        var addedIndex = data.IndexOf(obj);
                        // lets check if first submitted date is earlier
                        if(obj.FirstSubmittedDate < data[addedIndex].FirstSubmittedDate)
                        {
                            obj.PendingCount = data[addedIndex].PendingCount + 1;
                        } else
                        {
                            obj = data[addedIndex];
                            obj.PendingCount++;
                        }

                        data[addedIndex] = obj;
                    } else
                    {
                        data.Add(obj);
                    }
                }

                // lets sort by Admin Name
                data.Sort((a, b) => a.AdminName.CompareTo(b.AdminName));

                approvalNeeded.DataSource = data;
                approvalNeeded.DataBind();

            } else
            {
                noApprovalNeeded.Visible = true;
                approvalNeeded.Visible = false;
            }
        }

        public struct FirmStatusMessage
        {
            public Boolean IsRejected { get; set; }
            public String FirmName { get; set; }
            public int ID { get; set; }
        }
        
        private void Step2_SaveButtonClicked(object sender, EnhancedFirmApproval_Step2.SaveEventArgs e)
        {
            List<FirmStatusMessage> messages = new List<FirmStatusMessage>();
            if(e.ApprovedFirms.Count > 0)
            {
                foreach(var key in e.ApprovedFirms.Keys)
                {
                    messages.Add(new FirmStatusMessage()
                    {
                        ID = key,
                        FirmName = e.ApprovedFirms[key],
                        IsRejected = false 
                    });
                }
            }

            if (e.RejectedFirms.Count > 0)
            {
                foreach (var key in e.RejectedFirms.Keys)
                {
                    messages.Add(new FirmStatusMessage()
                    {
                        ID = key,
                        FirmName = e.RejectedFirms[key],
                        IsRejected = true
                    });
                }
            }

            if(messages.Count > 0 )
            {
                statusMessages.Visible = true;
                statusMessages.DataSource = messages;
                statusMessages.DataBind();
            }

            step2.Visible = false;
            step1.Visible = true;

            // lets rebind the data in case it was changed
            BindListOfApprovalsAwaiting();
        }

        private void Step2_BackButtonClicked(object sender, EventArgs e)
        {
            step2.Visible = false;
            step1.Visible = true;
            statusMessages.Visible = false;

            // lets rebind the data in case it was changed
            BindListOfApprovalsAwaiting();
        }

        protected void btnView_Command(object sender, CommandEventArgs e)
        {
            if(e.CommandName.Equals("view"))
            {
                int personId = int.Parse(e.CommandArgument.ToString());
                // lets pass this person ID to the step 2 of the approval process
                step1.Visible = false;
                step2.Visible = true;
                step2.Bind(personId);
                statusMessages.Visible = false;
            }
        }
    }

    public struct ApprovalUser
    {
        public int PersonId { get; set; }
        public String AdminName { get; set; }
        public int PendingCount { get; set; }
        public DateTime FirstSubmittedDate { get; set; }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            ApprovalUser usr = (ApprovalUser)obj;
            
            return usr.PersonId.Equals(PersonId);
        }

    }
}
