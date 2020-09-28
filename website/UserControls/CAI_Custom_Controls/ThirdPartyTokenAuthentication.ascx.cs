using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls
{
    public partial class ThirdPartyTokenAuthentication : Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
    {
        public String VendorKey { get; set; }
        public int TokenTimeoutInMinutes { get; set; }
        public String VendorURL { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            // checking if not in design mode
            if(this.Page.IsDesignMode() || String.IsNullOrEmpty(VendorKey) || String.IsNullOrEmpty(VendorURL))
            {
                pnlSettings.Visible = true;
                lblVendorKey.Text = String.IsNullOrEmpty(VendorKey) ? "NOT SET" : VendorKey;
                lblVendorUrl.Text = String.IsNullOrEmpty(VendorURL) ? "NOT SET" : VendorURL;
                lblTokenTimeout.Text = (TokenTimeoutInMinutes > 0 ? TokenTimeoutInMinutes.ToString() : "20") + " minutes";
            } else
            {
                if(TokenTimeoutInMinutes <= 0)
                {
                    TokenTimeoutInMinutes = 20;
                }

                pnlSettings.Visible = false;

                // checking if logged in
                if (User1 != null && User1.PersonID >= 1)
                {
                    // lets generate token if not already generated for this person for same vendor in the past half of the timeout time
                    var tokenTimeoutToCheck = TokenTimeoutInMinutes / 2;
                    var spVerifyThirdPartyAPITokenByPersonIdAndKey__cai = "spVerifyThirdPartyAPITokenByPersonIdAndKey__cai";
                    var parameters = new List<IDataParameter>();
                    parameters.Add(DataAction.GetDataParameter("@Timeout", SqlDbType.Int, tokenTimeoutToCheck));
                    parameters.Add(DataAction.GetDataParameter("@PersonID", SqlDbType.Int, User1.PersonID));
                    parameters.Add(DataAction.GetDataParameter("@Key", SqlDbType.VarChar, VendorKey));

                    var sql = $"{AptifyApplication.GetEntityBaseDatabase("ThirdPartyAPIToken")}..{spVerifyThirdPartyAPITokenByPersonIdAndKey__cai}";
                    var dt = DataAction.GetDataTableParametrized(sql, CommandType.StoredProcedure, parameters.ToArray());

                    var token = Guid.Empty;

                    if(dt != null && dt.Rows.Count > 0)
                    {
                        // lets get first token to be used
                        DataRow row = dt.Rows[0];
                        token = (Guid)row["Token"];
                    }

                    // lets check if token is not found, then lets generate
                    if(Guid.Empty.Equals(token))
                    {
                        token = Guid.NewGuid();
                        //spCreateThirdPartyAPITokens(@ID-out, @Token(guid), @ThirdPartySecretKey, @PersonId, @DateCreated, @ExpiresAt)
                        sql = Database + ".." + "spCreateThirdPartyAPITokens";
                        parameters.Clear();

                        var idParam = DataAction.GetDataParameter("@ID", System.Data.SqlDbType.Int);
                        idParam.Direction = ParameterDirection.Output;
                        parameters.Add(idParam);

                        parameters.Add(DataAction.GetDataParameter("@Token", SqlDbType.UniqueIdentifier, token));
                        parameters.Add(DataAction.GetDataParameter("@ThirdPartySecretKey", SqlDbType.VarChar, VendorKey));
                        parameters.Add(DataAction.GetDataParameter("@PersonId", SqlDbType.Int, User1.PersonID));
                        parameters.Add(DataAction.GetDataParameter("@DateCreated", SqlDbType.DateTime, DateTime.Now));
                        parameters.Add(DataAction.GetDataParameter("@ExpiresAt", SqlDbType.DateTime, DateTime.Now.AddMinutes(TokenTimeoutInMinutes)));

                        int rowsAffected = DataAction.ExecuteNonQueryParametrized(sql, CommandType.StoredProcedure, parameters.ToArray());

                        if(rowsAffected <= 0)
                        {
                            token = Guid.Empty;
                        }
                    }

                    if(Guid.Empty.Equals(token))
                    {
                        Response.Write("error generating token");
                    } else
                    {
                        Response.Redirect(VendorURL + (VendorURL.Contains("?") ? "&" : "?") + "token=" + token.ToString());
                    }
                    // -----
                } else
                {
                    // redirect to login page
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
    }
}