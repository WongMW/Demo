using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.FirmsSearch
{
    public partial class EnhancedListingPaymentStep : Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
    {
        // stored procedure names
        private String spGetEnhancedListingAdmin__cai = "spGetEnhancedListingAdmin__cai";
        private String spCreateEnhancedListingAdmin__cai = "spCreateEnhancedListingAdmin__cai";
        private String spGetCampaignCodeID = "spGetCampaignCodeID";
        private static string spGetFirmNameFullAddressByFirmId__cai => "spGetFirmNameFullAddressByFirmId__cai";

        public List<EnhancedListingApplication.FirmDetails> ParentSelectedFirms
        {
            get
            {
                if (ViewState["EnhancedListingParentSelectedFirms"] != null)
                {
                    return (List<EnhancedListingApplication.FirmDetails>)ViewState["EnhancedListingParentSelectedFirms"];
                }

                return new List<EnhancedListingApplication.FirmDetails>();
            }
            set
            {
                ViewState["EnhancedListingParentSelectedFirms"] = value;
            }
        }


        public int UserFirmID
        {
            get
            {
                if (ViewState["EnhancedListingUserFirmID"] != null)
                {
                    return (int)ViewState["EnhancedListingUserFirmID"];
                }

                return -1;
            }
            set
            {
                ViewState["EnhancedListingUserFirmID"] = value;
            }
        }
        public String UserFirmName
        {
            get
            {
                if (ViewState["EnhancedListingUserFirmName"] != null)
                {
                    return (String)ViewState["EnhancedListingUserFirmName"];
                }

                return String.Empty;
            }
            set
            {
                ViewState["EnhancedListingUserFirmName"] = value;
                lFirmName.Text = value;
            }
        }

        public List<EnhancedListingApplication.FirmDetails> PayForFirms
        {
            get
            {
                if (SelectedFirms != null)
                {
                    List<EnhancedListingApplication.FirmDetails> list = new List<EnhancedListingApplication.FirmDetails>();

                    foreach(var firm in SelectedFirms)
                    {
                        if(firm.PurchaseAllowed)
                        {
                            // checking if approve date exist, then it is a renewal
                            if(firm.ApprovedEndDate != null && firm.IsRenewal)
                            {
                                list.Add(firm);
                            } else if(firm.ApprovedEndDate == null)
                            {
                                // it is new purchase
                                list.Add(firm);
                            }
                        }
                    }

                    return list;
                }

                return new List<EnhancedListingApplication.FirmDetails>();
            }
        }

        public List<EnhancedListingApplication.FirmDetails> SelectedFirms
        {
            get
            {
                if(ViewState["EnhancedListingSelectedFirms"] != null)
                {
                    return (List<EnhancedListingApplication.FirmDetails>)ViewState["EnhancedListingSelectedFirms"];
                }

                return new List<EnhancedListingApplication.FirmDetails>();
            }
            set
            {
                ViewState["EnhancedListingSelectedFirms"] = value;

                // lets assign parent values
                List<EnhancedListingApplication.FirmDetails> _parentSelectedFirms = new List<EnhancedListingApplication.FirmDetails>();
                foreach(var val in value)
                {
                    if(val.IsParent)
                    {
                        _parentSelectedFirms.Add(val);
                    }
                }

                ParentSelectedFirms = _parentSelectedFirms;
            }
        }

        private Control CreditCard
        {
            get
            {
                return this.FindControl("CreditCard");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindPaymentTypeDropdown();
                LoadControls();
            }
        }

        private void LoadControls()
        {
            // initialise credit card control
            ExecuteCreditCardMethod("LoadCreditCardInfo", new Object[0]);
            SetCreditCardProperty("DisableBillMeLater", true);
        }

        private int GetFirmsProductID()
        {
            var val = System.Configuration.ConfigurationManager.AppSettings.Get("Firms.EnhancedListing.ProductID");
            if (string.IsNullOrEmpty(val))
            {
                return -1;
            }

            int _val = -1;
            int.TryParse(val, out _val);

            return _val;
        }

        private Aptify.Applications.OrderEntry.OrdersEntity _order;

        private Aptify.Applications.OrderEntry.OrdersEntity Order
        {
            get
            {
                if (_order == null)
                {
                    _order = (Aptify.Applications.OrderEntry.OrdersEntity)ShoppingCart1.GetNewOrderObject();
                    // lets add products based on the quantity
                    if (PayForFirms.Count > 0)
                    {
                        var firmProductId = GetFirmsProductID();
                        
                        if (firmProductId > 0)
                        {
                            int i = 0;
                            foreach (var f in PayForFirms)
                            {
                                _order.AddProduct(firmProductId, 1);
                                _order.OrderLines(i).SetValue("Description", "Enhanced Listing for: " + f.Fname);
                                i++;
                            }
                        }
                    }
                }

                _order.CalculateOrderTotals();

                return _order;
            }
        }

        private void bindPaymentTypeDropdown()
        {
            Dictionary<String, String> paymentTypeOptions = new Dictionary<string, string>();

            //paymentTypeOptions.Add("1", "Bill to firm");
            paymentTypeOptions.Add("2", "Pay with Credit Card");
            //paymentTypeOptions.Add("3", "Pay now with firm credit card");

            ListItemCollection collection = new ListItemCollection();
            foreach (var _id in paymentTypeOptions.Keys)
            {
                collection.Add(new ListItem(paymentTypeOptions[_id], _id));
            }
            dropdownPaymentType.DataSource = collection;
            dropdownPaymentType.DataBind();
        }

        public void PreparePayment()
        {
            // TODO: identify what firms are not required to be paid for, and what firms to be renewed

            if (ParentSelectedFirms.Count > 0)
            {
                dropdownFirmList.DataSource = ParentSelectedFirms;
                dropdownFirmList.DataValueField = "Fid";
                dropdownFirmList.DataTextField = "Fname";

                // pre-select first firm
                dropdownFirmList.DataBind();

                dropdownFirmList.SelectedIndex = 0;
            }

            // hide selection dropdown if only one firm selected
            if (ParentSelectedFirms.Count == 1)
            {
                pnlBillTo.Visible = false;
            }
            else
            {
                // commented out to keep hidden all the time, as default bill to will be UserFirmID
                // pnlBillTo.Visible = true;
                pnlBillTo.Visible = false;
            }

            BindOrderTotals();
        }

        private void BindOrderTotals()
        {
            // lets prepare total payment amount
            lblPaymentAmount.InnerHtml = GetTotalAmountToBePaidFormatted(GetTotalAmountToBePaid());
            lblVAT.InnerHtml = GetTotalAmountToBePaidFormatted(GetVATAmount());
            lblSubtotal.InnerHtml = GetTotalAmountToBePaidFormatted(GetSubTotal());
            if (GetDiscountAmount() > 0)
            {
                discountHolder.Visible = true;
                lblDiscount.InnerHtml = GetTotalAmountToBePaidFormatted(GetDiscountAmount()) + "(discount applied)";
            }
            else
            {
                discountHolder.Visible = false;
            }
        }


        private string GetTotalAmountToBePaidFormatted(decimal amount)
        {
            return Microsoft.VisualBasic.Strings.Format(amount, User1.PreferredCurrencyFormat);
        }

        public decimal GetSubTotal()
        {
            return Order.CALC_SubTotal;
        }

        private decimal GetVATAmount()
        {
            return Order.CALC_SalesTax;
        }

        private decimal GetDiscountAmount()
        {
            return Order.CALC_Discount;
        }

        private decimal GetTotalAmountToBePaid()
        {
            return Order.GetOrderTotal();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var sTrans = DataAction.BeginTransaction(System.Data.IsolationLevel.ReadCommitted, true);

            String ccNumber = (String)GetCreditCardProperty("CCNumber");
            String ccExpireDate = (String)GetCreditCardProperty("CCExpireDate");
            String ccSecurityNumber = (String)GetCreditCardProperty("CCSecurityNumber");
            var paymentTypeId = (long)GetCreditCardProperty("PaymentTypeID");

            Order.SetValue("BillToCompanyID", UserFirmID);// dropdownFirmList.SelectedValue);

            // lets process payment
            Order.SetValue("OrderSourceID", AptifyApplication.GetEntityRecordIDFromRecordName("Order Sources", "Web"));
            Order.SetValue("InitialPaymentAmount", Order.GetOrderTotal());
            Order.SetValue("PayTypeID", paymentTypeId);
            Order.SetValue("CCAccountNumber", ccNumber);
            Order.SetValue("CCExpireDate", ccExpireDate);
            Order.SetAddValue("_xCCSecurityNumber", ccSecurityNumber);
            Order.SetValue("Comments", String.IsNullOrEmpty(txtComments.Text) ? "" : txtComments.Text);

          //  Order.SetValue("BillToCompanyID", dropdownFirmList.SelectedValue);

            String errorMessage = String.Empty;
            
            if (Order.Save(false, ref errorMessage, sTrans))
            {
                DataAction.CommitTransaction(sTrans);

                paymentOrderNumber.InnerText = Order.RecordID.ToString();
                thankYouStep.Visible = true;
                paymentStep.Visible = false;

                lblError.Visible = false;

                // we need to create new enhanced listing entry for each selected firm
                foreach (var firm in PayForFirms)
                {
                    try
                    {
                        var _params = new IDataParameter[15];
                        _params[0] = DataAction.GetDataParameter("@ID", System.Data.SqlDbType.Int, 0); // ID should not be there, but by some reason required....
                        _params[1] = DataAction.GetDataParameter("@FirmId", System.Data.SqlDbType.Int, firm.Fid);
                        _params[2] = DataAction.GetDataParameter("@FirmName", System.Data.SqlDbType.NVarChar, firm.Fname);
                        _params[3] = DataAction.GetDataParameter("@PersonId", System.Data.SqlDbType.Int, User1.PersonID);
                        _params[4] = DataAction.GetDataParameter("@PersonName", System.Data.SqlDbType.NVarChar, User1.FirstName + " " + User1.LastName);
                        _params[5] = DataAction.GetDataParameter("@PaymentId", System.Data.SqlDbType.Int, Order.PaymentInformationID);
                        _params[6] = DataAction.GetDataParameter("@PaymentDate", System.Data.SqlDbType.DateTime, DateTime.Now);
                        _params[7] = DataAction.GetDataParameter("@PaymentStatus", System.Data.SqlDbType.NVarChar, "Success");
                        _params[8] = DataAction.GetDataParameter("@PaymentBy", System.Data.SqlDbType.NVarChar, User1.FirstName + " " + User1.LastName); // name of person
                        _params[9] = DataAction.GetDataParameter("@OrderNumber", System.Data.SqlDbType.NVarChar, Order.RecordID);
                        // checking if this is a renewal
                        DateTime approvedStartDate = DateTime.Now;
                        if (firm.IsRenewal && firm.ApprovedEndDate != null)
                        {
                            approvedStartDate = firm.ApprovedEndDate.Value;
                        }
                        _params[10] = DataAction.GetDataParameter("@ApprovedStartDate", System.Data.SqlDbType.DateTime, approvedStartDate);
                        // -----
                        _params[11] = DataAction.GetDataParameter("@ApprovedEndDate", System.Data.SqlDbType.DateTime, approvedStartDate.AddYears(1));
                        _params[12] = DataAction.GetDataParameter("@StatusMessage", System.Data.SqlDbType.NVarChar, "");
                        _params[13] = DataAction.GetDataParameter("@DateCreated", System.Data.SqlDbType.DateTime, DateTime.Now);
                        if (firm.IsParent)
                        { _params[14] = DataAction.GetDataParameter("@ParentId", System.Data.SqlDbType.Int, DBNull.Value); }
                        else
                        { _params[14] = DataAction.GetDataParameter("@ParentId", System.Data.SqlDbType.Int, firm.ParentId); }
                        var sSQL = String.Empty;
                        sSQL = Database + ".." + spCreateEnhancedListingAdmin__cai;
                        DataAction.ExecuteNonQueryParametrized(sSQL, System.Data.CommandType.StoredProcedure, _params, 180);
                    } catch(Exception ex)
                    {
                        SoftwareDesign.Helper.LogApplicationLevelException("EnhancedListingPaymentStep.ascx.cs", "btnSave_Click", ex);
                        try
                        {
                            var tmpl = GetSDEmailTemplate("EnhancedListingNotificationPremiumListingEnhancedAdminFailure");
                            tmpl = tmpl.Replace("{OrderID}", paymentOrderNumber.InnerText)
                                .Replace("{ErrorMessage}", ex.Message);
                            SoftwareDesign.Helper.SendEmail("Enhanced Listing Admin Creation Failed", tmpl, "webmaster@charteredaccountants.ie");
                        }
                        catch (Exception ex2)
                        {
                            SoftwareDesign.Helper.LogApplicationLevelException("EnhancedListingPaymentStep.ascx.cs", "btnSave_Click-SendEmail", ex2); 
                        }
                    }
                }
            } else
            {
                lblError.InnerHtml = errorMessage;
                lblError.Visible = true;
            }
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

        private object ExecuteCreditCardMethod(String methodName, Object[] args)
        {
            var ccCntrl = CreditCard;
            if (ccCntrl != null)
            {
                Type t = ccCntrl.GetType();

                System.Reflection.MethodInfo theMethod = t.GetMethod(methodName);
                if (theMethod != null)
                {
                    return theMethod.Invoke(ccCntrl, args);
                }
            }

            return null;
        }

        private void SetCreditCardProperty(String propertyName, object val)
        {
            var prop = GetCreditCardPropertyInfo(propertyName);
            if (prop != null)
            {
                prop.SetValue(CreditCard, val);
            }
        }

        private object GetCreditCardProperty(String propertyName)
        {
            var prop = GetCreditCardPropertyInfo(propertyName);
            if (prop != null)
            {
                return prop.GetValue(CreditCard);
            }

            return null;
        }

        private System.Reflection.PropertyInfo GetCreditCardPropertyInfo(String propertyName)
        {
            var ccCntrl = CreditCard;
            if (ccCntrl != null)
            {
                Type t = ccCntrl.GetType();
                System.Reflection.PropertyInfo[] props = t.GetProperties();
                Dictionary<String, System.Reflection.PropertyInfo> properties = new Dictionary<String, System.Reflection.PropertyInfo>();
                foreach (var prop in props)
                {
                    properties.Add(prop.Name, prop);
                }

                // checking if necessary property exist
                if (properties.ContainsKey(propertyName))
                {
                    return properties[propertyName];
                }
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

            return "";
        }

        protected void txtDiscountcode_TextChanged(object sender, EventArgs e)
        {
            var code = txtDiscountcode.Text;

            if(!String.IsNullOrEmpty(code))
            {
                var parameters = new List<IDataParameter>();
                parameters.Add(DataAction.GetDataParameter("@CampaignName", SqlDbType.NVarChar, code));

                var sql = $"{AptifyApplication.GetEntityBaseDatabase("Firm")}..{spGetCampaignCodeID}";
                var dt = DataAction.GetDataTableParametrized(sql, CommandType.StoredProcedure, parameters.ToArray());
                if(dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    var campaignId = int.Parse(row["ID"].ToString());
                    Order.CampaignCodeID = campaignId;
                    Order.CalculateOrderTotals();
                    lblIncorrectCode.Visible = false;
                } else
                {
                    lblIncorrectCode.Visible = true;
                }
            } else
            {
                lblIncorrectCode.Visible = false;
            }

            BindOrderTotals();
        }
    }
}
