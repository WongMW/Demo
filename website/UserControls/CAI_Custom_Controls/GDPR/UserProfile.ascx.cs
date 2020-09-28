using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Aptify.Framework.ExceptionManagement;
using Aptify.Framework.Web.eBusiness;
using SoftwareDesign.Infrastructure;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.GDPR
{
    public partial class UserProfile : BaseUserControlAdvanced
    {
        private static string StoredProcedureName => "spGetPersonFullProfile__sd";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    BindData();
                }
                catch (Exception exception)
                {
                    ExceptionManager.Publish(exception);
                    lblMsg.Text = "Error loading products:" + exception.Message;
                }
                
            }
        }

        private void BindData()
        {
            var model = GetDatasource();
            BindSource(model);
            String message = System.Configuration.ConfigurationManager.AppSettings.Get("Gdpr.Message.Second.Step");

            if (String.IsNullOrEmpty(message)) {
                message = "Please ensure your profile information, job title and job function is correct. This will enable us to support you with more relevant content.";
            }
            txtUserProfileText.InnerText = message;
        }

        private UserProfileViewModel GetDatasource()
        {
            var ret = new UserProfileViewModel();

            var dt = GetItemsDt();

            if (dt == null || dt.Rows.Count == 0)
                return ret;

            var row = dt.Rows[0];

            ret.FullName = Convert.ToString(row["FirstLast"]);
            ret.DateOfBirth = GetFormattedDate(row); 
            ret.HomeAddress = FormatHomeAddress(row);
            ret.FirmAddress = FormatFirmAddress(row);
            ret.JobFunction = Convert.ToString(row["Jobfunction"]);
            ret.JobTitle = Convert.ToString(row["Title"]);
            ret.EmploymentStatus = Convert.ToString(row["EmploymentStatus"]);



            return  ret;
        }

        private static string GetFormattedDate(DataRow row)
        {
           var date = (DateTime?) row["BirthDay"];
            return date != null ? 
                date.Value.ToString("dd MMM yyyy") : 
                string.Empty;
        }

        private DataTable GetItemsDt()
        {
            var parameters = new List<IDataParameter>(); 
            try
            {
                
                var sql = $"{AptifyApplication.GetEntityBaseDatabase("Products")}..{StoredProcedureName}";

                parameters.Add(GetPersonIdParam());
                

                var dt = DataAction.GetDataTableParametrized(sql, CommandType.StoredProcedure,
                    parameters.ToArray());

                return dt;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return null;
        }

        private IDataParameter GetPersonIdParam()
        {
            var param = DataAction.GetDataParameter("@PersonID", SqlDbType.Int, User1.PersonID);
            return param;
        }

        private static string FormatHomeAddress(DataRow row)
        {
            var lines = new List<string>
            {
                Convert.ToString(row["HomeAddressLine1"]),
                Convert.ToString(row["HomeAddressLine2"]),
                Convert.ToString(row["HomeAddressLine3"]),
                Convert.ToString(row["HomeAddressCity"]),
                Convert.ToString(row["HomeAddressCounty"]),
                Convert.ToString(row["HomeAddressPostalCode"]),
                //Convert.ToString(row["HomeAddressProvince"]),
                Convert.ToString(row["HomeAddressCountry"])
            };

            return ReturnFormattedLines(lines);
        }

        private static string ReturnFormattedLines(IEnumerable<string> lines)
        {
            return string.Join(", ", lines.Where(s => !string.IsNullOrWhiteSpace(s)));
        }


        private static NotAvailableString FormatFirmAddress(DataRow row)
        {
            var lines = new List<string>
            {
                //Convert.ToString(row["CompanyName"] + "<br/>"),
                Convert.ToString(row["CompanyName"]),
                Convert.ToString(row["CompanyAddressLine1"]),
                Convert.ToString(row["CompanyAddressLine2"]),
                Convert.ToString(row["CompanyAddressLine3"]),
                Convert.ToString(row["CompanyAddressCity"]),
                Convert.ToString(row["CompanyAddressCounty"]),
                Convert.ToString(row["CompanyAddressPostalCode"]),
                //Convert.ToString(row["CompanyAddressProvince"]),
                Convert.ToString(row["CompanyAddressCountry"])
            };

            return ReturnFormattedLines(lines);
        }

  

        private void BindSource(UserProfileViewModel model)
        {
            FullName.Text = model.FullName;
            DOB.Text = model.DateOfBirth;
            HomeAddress.Text = model.HomeAddress;
            FirmAddress.Text = model.FirmAddress;
            JobTitle.Text = model.JobTitle;
            EmploymentStatus.Text = model.EmploymentStatus;
            JobFunction.Text = model.JobFunction;
        }

        public Boolean ConfirmButtonEnabled
        {
            get
            {
                // lets check if employment status is one of the ones that has job, check that firm is set
                String employedStatusesToCheck_str = System.Configuration.ConfigurationManager.AppSettings.Get("Gdpr.Employed.Status.Check.For.Firm");
                List<String> employedStatusesToCheck = new List<string>();
                if (!String.IsNullOrEmpty(employedStatusesToCheck_str))
                {
                    employedStatusesToCheck_str = employedStatusesToCheck_str.ToLower();
                    employedStatusesToCheck = new List<String>(employedStatusesToCheck_str.Split(','));
                }

                bool _confirmButtonEnabled = true;

                if (employedStatusesToCheck.Contains(EmploymentStatus.Text.ToString().ToLower()))
                {
                    // lets check if firm is set
                    if (FirmAddress.Text.Equals(new NotAvailableString()))
                    {
                        // we need to disable confirm button and show different message
                        _confirmButtonEnabled = false;
                    }
                }

                return _confirmButtonEnabled;
            }
        }
    }

    public class UserProfileViewModel
    {
        public NotAvailableString FullName { get; set; }
        public NotAvailableString DateOfBirth { get; set; }
        public NotAvailableString HomeAddress { get; set; }
        public NotAvailableString FirmAddress { get; set; }
        public NotAvailableString JobTitle { get; set; }
        public NotAvailableString JobFunction { get; set; }
        public NotAvailableString EmploymentStatus { get; set; }
    }
}
