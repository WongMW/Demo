using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.EAssessment
{
    public partial class ModalContent : Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
    {
        public string SkipSessionKey { get { return "eassessment_skip"; } }

        private static String spCreateEassessmentConsent__cai = "spCreateEassessmentConsent__cai";
        private static String spUpdateEassessmentConsent__cai = "spUpdateEassessmentConsent__cai";
        private static String spGetEassessmentConsentByPersonStudentID__cai = "spGetEassessmentConsentByPersonStudentID__cai";
        private static String spGetPerosnOldIdbyPersonId__c = "spGetPerosnOldIdbyPersonId__c";
        private static String spGetPersonCellPhone = "spGetPersonCellPhone";
        private string personPhone;

        public Telerik.Web.UI.RadWindow Modal { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var stringsKey = "lblEAssessment_str{0}";
                personPhone = GetPersonPhoneNumber();

                var stringsControls = new System.Collections.ArrayList()
                {
                    lblEAssessment_str1,
                    lblEAssessment_str2,
                    null,//lblEAssessment_str3,
                    null,//lblEAssessment_str4,
                    null,//lblEAssessment_str5,
                    lblEAssessment_str6,
                    lblEAssessment_str7,
                    lblEAssessment_str8,
                    lblError_str9,
                    lblEAssessment_str10
                };
                for(var i = 1; i <= stringsControls.Count; i++)
                {
                    if (stringsControls[i - 1] == null) continue;

                    var stringKey = String.Format(stringsKey, "" + i);
                    var strVal = GetStringText(stringKey);

                    // checking if string is set then update conrol
                    if(!String.IsNullOrEmpty(strVal))
                    {
                        System.Web.UI.HtmlControls.HtmlGenericControl cntrl = (System.Web.UI.HtmlControls.HtmlGenericControl)stringsControls[i - 1];
                        cntrl.InnerHtml = strVal;
                    }
                }

                for (var i = 1; i <= stringsControls.Count; i++)
                {
                    if (stringsControls[i - 1] == null) continue;

                    System.Web.UI.HtmlControls.HtmlGenericControl cntrl = (System.Web.UI.HtmlControls.HtmlGenericControl)stringsControls[i - 1];
                    cntrl.InnerHtml = cntrl.InnerHtml.Replace("[Display Name]", User1.FirstName + " " + User1.LastName)
                                                     .Replace("[Phone]", personPhone);

                }

                if (string.IsNullOrEmpty(personPhone))
                    chkRule4.Enabled = false;
            }
        }

        private String GetStringText(String key)
        {
            String val = "";

            if (key == "lblEAssessment_str10")
            {
                key += (string.IsNullOrEmpty(personPhone)) ? "_without_phone" : "_with_phone";
            }

            Object cntrlWrapper = FindControl("wrapper");
            if(cntrlWrapper != null)
            {
                System.Reflection.MethodInfo clickMethodInfo = cntrlWrapper.GetType().GetMethod("GetLocalisationString");

                var response = clickMethodInfo.Invoke(cntrlWrapper, new object[] {
                    key
                });

                val = (String)response;
            }

            return val;
        }

        protected void btnDone_Click(object sender, EventArgs e)
        {
            // checking if all rules are ticked
            if(chkRule1.Checked /*&& chkRule2.Checked*/ && chkRule3.Checked && chkRule4.Checked)
            {
                lblError_str9.Visible = false;

                var param = new List<System.Data.IDataParameter>();
                param.Add(DataAction.GetDataParameter("@PersonID", System.Data.SqlDbType.Int, GetPersonID()));
                param.Add(DataAction.GetDataParameter("@StudentID", System.Data.SqlDbType.NVarChar, GetStudentID()));
                param.Add(DataAction.GetDataParameter("@AcceptRulesRegs", System.Data.SqlDbType.Bit, true));
                param.Add(DataAction.GetDataParameter("@ConfirmNames", System.Data.SqlDbType.Bit, true));
                param.Add(DataAction.GetDataParameter("@ConfirmPhones", System.Data.SqlDbType.Bit, true));
                param.Add(DataAction.GetDataParameter("@ConsentRecording", System.Data.SqlDbType.Bit, false)); // set to FALSE because user no longer asked this question.
                param.Add(DataAction.GetDataParameter("@DateSkipped", System.Data.SqlDbType.DateTime, DateTime.Now));

                // saving the control
                System.Data.DataRow consentRow = GetConsent();
                if (consentRow != null)
                {
                    // lets update existing consent with new data
                    param.Add(DataAction.GetDataParameter("@ID", System.Data.SqlDbType.Int, consentRow["ID"]));

                    var sSql = Database + ".." + spUpdateEassessmentConsent__cai;
                    DataAction.ExecuteNonQueryParametrized(sSql, System.Data.CommandType.StoredProcedure, param.ToArray());
                }
                else
                {
                    // lets create new consent with new data
                    var sSql = Database + ".." + spCreateEassessmentConsent__cai;
                    var idParam = DataAction.GetDataParameter("@ID", System.Data.SqlDbType.Int);
                    idParam.Direction = System.Data.ParameterDirection.Output;
                    param.Add(idParam);
                    var eeid = Convert.ToInt32(DataAction.ExecuteNonQueryParametrized(sSql, System.Data.CommandType.StoredProcedure, param.ToArray()));
                }
                // ----


                // closing modal
                if (Modal != null)
                {
                    Modal.VisibleOnPageLoad = false;
                }
            } else
            {
                // Presenting a friendly error message
                lblError_str9.Visible = true;
            }
        }

        protected void btnSkip_Click(object sender, EventArgs e)
        {
            // record skip event
            System.Data.DataRow consentRow = GetConsent();
            if(consentRow != null)
            {
                // lets update existing consent with skip action
                List<System.Data.IDataParameter> param = new List<System.Data.IDataParameter>();
                param.Add(DataAction.GetDataParameter("@ID", System.Data.SqlDbType.Int, consentRow["ID"]));
                param.Add(DataAction.GetDataParameter("@DateSkipped", System.Data.SqlDbType.DateTime, DateTime.Now));

                var sSql = Database + ".." + spUpdateEassessmentConsent__cai;
                DataAction.ExecuteNonQueryParametrized(sSql, System.Data.CommandType.StoredProcedure, param.ToArray());

            } else
            {
                // lets create new consent with skip action
                var param = new List<System.Data.IDataParameter>();
                var sSql = Database + ".." + spCreateEassessmentConsent__cai;

                param.Add(DataAction.GetDataParameter("@PersonID", System.Data.SqlDbType.Int, GetPersonID()));
                param.Add(DataAction.GetDataParameter("@StudentID", System.Data.SqlDbType.NVarChar, GetStudentID()));
                param.Add(DataAction.GetDataParameter("@AcceptRulesRegs", System.Data.SqlDbType.Bit, false));
                param.Add(DataAction.GetDataParameter("@ConfirmNames", System.Data.SqlDbType.Bit, false));
                param.Add(DataAction.GetDataParameter("@ConfirmPhones", System.Data.SqlDbType.Bit, false));
                param.Add(DataAction.GetDataParameter("@ConsentRecording", System.Data.SqlDbType.Bit, false));
                param.Add(DataAction.GetDataParameter("@DateSkipped", System.Data.SqlDbType.DateTime, DateTime.Now));

                var idParam = DataAction.GetDataParameter("@ID", System.Data.SqlDbType.Int);
                idParam.Direction = System.Data.ParameterDirection.Output;
                param.Add(idParam);
                var eeid = Convert.ToInt32(DataAction.ExecuteNonQueryParametrized(sSql, System.Data.CommandType.StoredProcedure, param.ToArray()));
            }
            // ----

            Page.Session[SkipSessionKey] = "1";

            if (Modal != null)
            {
                Modal.VisibleOnPageLoad = false;
            }
        }

        public System.Data.DataRow GetConsent()
        {
            System.Data.DataRow row = null;

            var parameters = new List<System.Data.IDataParameter>();
            parameters.Add(DataAction.GetDataParameter("@PersonID", System.Data.SqlDbType.Int, GetPersonID()));
            parameters.Add(DataAction.GetDataParameter("@StudentID", System.Data.SqlDbType.NVarChar, GetStudentID()));

            var sql = $"{AptifyApplication.GetEntityBaseDatabase("EassessmentConsent")}..{spGetEassessmentConsentByPersonStudentID__cai}";
            var dt = DataAction.GetDataTableParametrized(sql, System.Data.CommandType.StoredProcedure, parameters.ToArray());

            // if found at least one row, then 
            if(dt.Rows.Count > 0)
            {
                row = dt.Rows[0];

                chkRule1.Checked = Boolean.Parse(row["AcceptRulesRegs"].ToString());
                chkRule3.Checked = Boolean.Parse(row["ConfirmNames"].ToString());
                chkRule4.Checked = Boolean.Parse(row["ConfirmPhones"].ToString());
            }
            else
            {
                chkRule1.Checked = false;
                chkRule3.Checked = false;
                chkRule4.Checked = false;
            }

            return row;
        }

        private int GetPersonID() { return Convert.ToInt32(User1.PersonID); }
        private string OldId { get; set; }
        private String GetStudentID() {
            if (OldId == null)
            {
                var parameters = new List<System.Data.IDataParameter>();
                parameters.Add(DataAction.GetDataParameter("@Id", System.Data.SqlDbType.BigInt, GetPersonID()));

                var sql = $"{AptifyApplication.GetEntityBaseDatabase("Person")}..{spGetPerosnOldIdbyPersonId__c}";
                var dt = DataAction.GetDataTableParametrized(sql, System.Data.CommandType.StoredProcedure, parameters.ToArray());

                if(dt.Rows.Count > 0)
                {
                    OldId = dt.Rows[0]["OldID"].ToString();
                }
            }

            return OldId;
        }

        private string GetPersonPhoneNumber()
        {
            var parameters = new List<System.Data.IDataParameter>();
            parameters.Add(DataAction.GetDataParameter("@Id", System.Data.SqlDbType.BigInt, GetPersonID()));

            var sql = $"{AptifyApplication.GetEntityBaseDatabase("vwPersonCellPhones")}..{spGetPersonCellPhone}";
            var dt = DataAction.GetDataTableParametrized(sql, System.Data.CommandType.StoredProcedure, parameters.ToArray());

            if (dt.Rows.Count <= 0)
                return null;

            var countruCode = dt.Rows[0]["CountryCode"].ToString().Replace(" ", "");
            var areaCode = dt.Rows[0]["AreaCode"].ToString().Replace(" ", "");
            var phone = dt.Rows[0]["Phone"].ToString().Replace(" ", "");
            
            return string.Format("{0}{1}{2}", countruCode, areaCode, phone);
        }
    }
}
