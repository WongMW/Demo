using SitefinityWebApp.BusinessFacade.Interfaces.Form.Helper;
using SitefinityWebApp.BusinessFacade.Interfaces.Form.Validate;
using SitefinityWebApp.BusinessFacade.Interfaces.SQL;
using SitefinityWebApp.BusinessFacade.Services.Form.Helper;
using SitefinityWebApp.BusinessFacade.Services.Form.Validate;
using SitefinityWebApp.BusinessFacade.Services.SQL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using static SitefinityWebApp.BusinessFacade.Resources.Enum.GeneralEnum;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.AML
{
    public partial class AMLWhistleblowing__c : System.Web.UI.UserControl
    {
        private const string spCreateAMLWhistleblowing__cai = "spCreateAMLWhistleblowing__cai";

        //DI References
        protected readonly GeneralInterface _validateService = new GeneralService();
        protected readonly HelperInterface _formHelperService = new HelperService();
        protected readonly SqlInterface _sqlService = new SqlService();

        public delegate void MyDelegare(List<TextBox> listOfTextBoxes);

        public List<TextBox> listOfTextBoxes = new List<TextBox>();
        public List<CheckBox> listOfCheckBoxes = new List<CheckBox>();

        protected void Page_Load(object sender, EventArgs e)
        {
            lblSuccessMessage.Visible = false;
            lblErrorMessage.Visible = false;

            if (!IsPostBack)
            {
                listOfTextBoxes.Add(txtFirstName);
                listOfTextBoxes.Add(txtLastName);
                listOfTextBoxes.Add(txtAddress);
                listOfTextBoxes.Add(txtEmail);
                listOfTextBoxes.Add(txtCountryCode);
                listOfTextBoxes.Add(txtMobileArea);
                listOfTextBoxes.Add(txtNumber);
                listOfTextBoxes.Add(txtEntitiesDisclose);
                listOfTextBoxes.Add(txtInfoDisclose);

                listOfCheckBoxes.Add(chkAllowSharedInfo);
                listOfCheckBoxes.Add(chkAllowCommunication);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                lblErrorMessage.Visible = false;

                var errorMessage = string.Empty;

                var firstName = txtFirstName.Text.Trim();
                var lastName = txtLastName.Text.Trim();
                var address = txtAddress.Text.Trim();
                var email = txtEmail.Text.Trim();
                var phoneCountry = txtCountryCode.Text.Trim();
                var phoneArea = txtMobileArea.Text.Trim();
                var phoneNumber = txtNumber.Text.Trim();
                var entitiesDisclose = txtEntitiesDisclose.Text.Trim();
                var infoDisclose = txtInfoDisclose.Text.Trim();
                var allowInfoShared = chkAllowSharedInfo.Checked;
                var allowCommunication = chkAllowCommunication.Checked;

                errorMessage = _validateService.FieldIsValid("First Name", firstName, ValidateRegex.RegexWithoutSpace, ValidateMessages.ErrorInvalidMessage, errorMessage, holderTxtFirstName, true, "<br />", 0, 500).Result;

                errorMessage = _validateService.FieldIsValid("Last Name", lastName, ValidateRegex.RegexWithSpaceAndSpecialCharacters, ValidateMessages.ErrorInvalidMessage, errorMessage, holderTxtLastName, true, "<br />", 0, 500).Result;

                errorMessage = _validateService.FieldIsValid("Address", address, ValidateRegex.RegexWithSpaceAndSpecialCharacters, ValidateMessages.ErrorInvalidMessage, errorMessage, holderTxtAddress, true, "<br />", 0, 500).Result;

                errorMessage = _validateService.FieldIsValid("Email", email, ValidateRegex.RegexEmail, ValidateMessages.ErrorInvalidMessage, errorMessage, holderTxtEmail, true, "<br />", 0, 50).Result;

                errorMessage = _validateService.FieldIsValid("Country code", phoneCountry, ValidateRegex.RegexNumbers, ValidateMessages.ErrorInvalidMessage, errorMessage, holderTxtCountryCode, true, "<br />", 0, 4).Result;

                errorMessage = _validateService.FieldIsValid("Mobile\area code", phoneArea, ValidateRegex.RegexNumbers, ValidateMessages.ErrorInvalidMessage, errorMessage, holderTxtMobileArea, true, "<br />", 0, 10).Result;

                errorMessage = _validateService.FieldIsValid("Phone number", phoneNumber, ValidateRegex.RegexNumbers, ValidateMessages.ErrorInvalidMessage, errorMessage, holderTxtNumber, true, "<br />", 0, 20).Result;

                errorMessage = _validateService.FieldIsValid("Incident information", entitiesDisclose, ValidateRegex.RegexDontNeed, ValidateMessages.ErrorInvalidOrRequiredMessage, errorMessage, holderTxtEntitiesDisclose, false, "<br />", 1, 1000).Result;

                errorMessage = _validateService.FieldIsValid("Information to disclose", infoDisclose, ValidateRegex.RegexDontNeed, ValidateMessages.ErrorInvalidOrRequiredMessage, errorMessage, holderTxtInfoDisclose, false, "<br />", 1, 1000).Result;

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    lblErrorMessage.InnerHtml = errorMessage;
                    lblErrorMessage.Visible = true;
                }
                else
                {
                    var parameters = new List<SqlParameter>();
                    int ival = 0;

                    parameters.Add(new SqlParameter("@Firstname", firstName));
                    parameters.Add(new SqlParameter("@Lastname", lastName));
                    parameters.Add(new SqlParameter("@Address", address));
                    parameters.Add(new SqlParameter("@Email", email));
                    parameters.Add(new SqlParameter("@PhoneCountryCode", phoneCountry));
                    parameters.Add(new SqlParameter("@PhoneAreaCode", phoneArea));
                    parameters.Add(new SqlParameter("@PhoneNumber", phoneNumber));
                    parameters.Add(new SqlParameter("@EntitiesToDisclose", entitiesDisclose));
                    parameters.Add(new SqlParameter("@InfoToDisclose", infoDisclose));
                    parameters.Add(new SqlParameter("@AllowInfoShared", allowInfoShared));
                    parameters.Add(new SqlParameter("@AllowCommunication", allowCommunication));


                    ival = _sqlService.ExecuteSPSqlCommand(spCreateAMLWhistleblowing__cai, parameters).Result;

                    if (ival > 0)
                    {
                        lblErrorMessage.Visible = false;

                        listOfTextBoxes = _formHelperService.ClearTextBoxes(listOfTextBoxes);
                        listOfCheckBoxes = _formHelperService.UncheckCheckBoxes(listOfCheckBoxes);

                        lblSuccessMessage.Text = "Your anonymous disclosure has been sent successfully.";
                        lblSuccessMessage.ForeColor = System.Drawing.Color.Blue;
                        lblSuccessMessage.Attributes.Add("class", "product_added");
                        lblSuccessMessage.Visible = true;
                    }
                    else
                    {
                        lblErrorMessage.InnerText = "There was an error submitting the form at this time.";
                        lblSuccessMessage.Visible = false;
                        lblErrorMessage.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.InnerText = "There was an error submitting the form at this time, make sure all fields are filled in correctly.";
                lblErrorMessage.Visible = true;
                Aptify.Framework.ExceptionManagement.ExceptionManager.Publish(ex);
            }
        }
    }
}