using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI;

namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.PDFDownloadButtonWidget
{
    /// <summary>
    /// Class used to create custom page widget
    /// </summary>
    /// <remarks>
    /// If this widget is a part of a Sitefinity module,
    /// you can register it in the site's toolbox by adding this to the module's Install/Upgrade method(s):
    /// initializer.Installer
    ///     .Toolbox(CommonToolbox.PageWidgets)
    ///         .LoadOrAddSection(SectionName)
    ///             .SetTitle(SectionTitle) // When creating a new section
    ///             .SetDescription(SectionDescription) // When creating a new section
    ///             .LoadOrAddWidget<PDFDownloadButtonWidget>("PDFDownloadButtonWidget")
    ///                 .SetTitle("PDFDownloadButtonWidget")
    ///                 .SetDescription("PDFDownloadButtonWidget")
    ///                 .LocalizeUsing<ModuleResourceClass>() //Optional
    ///                 .SetCssClass(WidgetCssClass) // You can use a css class to add an icon (Optional)
    ///             .Done()
    ///         .Done()
    ///     .Done();
    /// </remarks>
    /// <see cref="http://www.sitefinity.com/documentation/documentationarticles/user-guide/widgets"/>
    [Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesigner(typeof(SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.PDFDownloadButtonWidget.Designer.PDFDownloadButtonWidgetDesigner))]
    public class PDFDownloadButtonWidget : SimpleView
    {
        #region Properties
        /// <summary>
        /// guid from designer for our selected page
        /// </summary>
        public Guid SelectedPageID { get; set; }
        /// <summary>
        /// guid from designer for our selected document
        /// </summary>
        public Guid SelectedDocumentID { get; set; }
        /// <summary>
        /// Gets or sets the message that will be displayed in the label.
        /// </summary>
        public string Style { get; set; }
        /// <summary>
        /// Gets or sets the message that will be displayed in the label.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the message that will be displayed in the label.
        /// </summary>
        public string CourseName { get; set; }
        /// <summary>
        /// Gets or sets the message that will be displayed in the label.
        /// </summary>
        public string InteractionType { get; set; }
        /// <summary>
        /// Gets or sets the link type.
        /// </summary>
        public string LinkType { get; set; }
        /// <summary>
        /// Gets or sets the message that will be displayed in the label.
        /// </summary>
        public string ExternalLink { get; set; }
        /// <summary>
        /// Obsolete. Use LayoutTemplatePath instead.
        /// </summary>
        protected override string LayoutTemplateName
        {
            get
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the layout template's relative or virtual path.
        /// </summary>
        public override string LayoutTemplatePath
        {
            get
            {
                if (string.IsNullOrEmpty(base.LayoutTemplatePath))
                    return PDFDownloadButtonWidget.layoutTemplatePath;
                return base.LayoutTemplatePath;
            }
            set
            {
                base.LayoutTemplatePath = value;
            }
        }
        #endregion

        #region Control References
        protected virtual HtmlGenericControl holderTxtFirstName { get { return Container.GetControl<HtmlGenericControl>("holderTxtFirstName", true); } }
        protected virtual HtmlGenericControl holderTxtLastName { get { return Container.GetControl<HtmlGenericControl>("holderTxtLastName", true); } }
        protected virtual HtmlGenericControl holderTxtEmail { get { return Container.GetControl<HtmlGenericControl>("holderTxtEmail", true); } }
        protected virtual HtmlGenericControl holderTxtCountryCode { get { return Container.GetControl<HtmlGenericControl>("holderTxtCountryCode", true); } }
        protected virtual HtmlGenericControl holderTxtMobileArea { get { return Container.GetControl<HtmlGenericControl>("holderTxtMobileArea", true); } }
        protected virtual HtmlGenericControl holderTxtNumber { get { return Container.GetControl<HtmlGenericControl>("holderTxtNumber", true); } }

        protected virtual HtmlGenericControl lblFirstNameError { get { return Container.GetControl<HtmlGenericControl>("lblFirstNameError", true); } }
        protected virtual HtmlGenericControl lblLastNameError { get { return Container.GetControl<HtmlGenericControl>("lblLastNameError", true); } }
        protected virtual HtmlGenericControl lblEmailError { get { return Container.GetControl<HtmlGenericControl>("lblEmailError", true); } }
        protected virtual HtmlGenericControl lblCountryCodeError { get { return Container.GetControl<HtmlGenericControl>("lblCountryCodeError", true); } }
        protected virtual HtmlGenericControl lblMobileAreaError { get { return Container.GetControl<HtmlGenericControl>("lblMobileAreaError", true); } }
        protected virtual HtmlGenericControl lblNumberError { get { return Container.GetControl<HtmlGenericControl>("lblNumberError", true); } }

        protected virtual HtmlGenericControl lblErrorMessage { get { return Container.GetControl<HtmlGenericControl>("lblErrorMessage", true); } }

        protected virtual CheckBox chkAllowCommunication
        {
            get
            {
                return Container.GetControl<CheckBox>("chkAllowCommunication", true);
            }
        }

        protected virtual TextBox txtFirstName
        {
            get
            {
                return this.Container.GetControl<TextBox>("txtFirstName", true);
            }
        }
        protected virtual TextBox txtLastName
        {
            get
            {
                return this.Container.GetControl<TextBox>("txtLastName", true);
            }
        }
        protected virtual TextBox txtEmail
        {
            get
            {
                return this.Container.GetControl<TextBox>("txtEmail", true);
            }
        }
        protected virtual TextBox txtCountryCode
        {
            get
            {
                return this.Container.GetControl<TextBox>("countyrcode", true);
            }
        }
        protected virtual TextBox txtMobileArea
        {
            get
            {
                return this.Container.GetControl<TextBox>("mobilearea", true);
            }
        }
        protected virtual TextBox txtNumber
        {
            get
            {
                return this.Container.GetControl<TextBox>("number", true);
            }
        }
        protected virtual TextBox txtGclid_field
        {
            get
            {
                return this.Container.GetControl<TextBox>("gclid_field", true);
            }
        }
        /// <summary>
        /// Reference to the Button control that shows the page url.
        /// </summary>
        protected virtual Button PageLink
        {
            get
            {
                return this.Container.GetControl<Button>("PageLink", true);
            }
        }
        /// <summary>
        /// Reference to the Button control that shows the page url.
        /// </summary>
        protected virtual HyperLink linkPdfButton
        {
            get
            {
                return this.Container.GetControl<HyperLink>("linkPdfButton", true);
            }
        }

        /// <summary>
        /// Reference to the Button control that shows the page url.
        /// </summary>
        protected virtual Button btnSave
        {
            get
            {
                return this.Container.GetControl<Button>("btnSave", true);
            }
        }

        /// <summary>
        /// Reference to the HyperLink control that shows the page url.
        /// </summary>
        protected virtual HyperLink DocumentLink
        {
            get
            {
                return this.Container.GetControl<HyperLink>("DocumentLink", true);
            }
        }

        protected virtual Panel pnlFormToFill
        {
            get
            {
                return this.Container.GetControl<Panel>("pnlFormToFill", true);
            }
        }

        protected virtual Panel pnlThankYou
        {
            get
            {
                return this.Container.GetControl<Panel>("pnlThankYou", true);
            }
        }

        /// <summary>
        /// Reference to the Label control that shows the Message.
        /// </summary>
        protected virtual Label StyleLabel
        {
            get
            {
                return this.Container.GetControl<Label>("StyleLabel", true);
            }
        }

        /// <summary>
        /// Reference to the Label control that shows the Message.
        /// </summary>
        protected virtual RadioButtonList LinkTypeRadioButtonList
        {
            get
            {
                return this.Container.GetControl<RadioButtonList>("LinkTypeRadioButtonList", true);
            }
        }

        /// <summary>
        /// Reference to the HtmlGenericControl control that shows the image thumbnail.
        /// </summary>
        protected virtual HtmlGenericControl Block
        {
            get
            {
                return this.Container.GetControl<HtmlGenericControl>("block", true);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initializes the controls.
        /// </summary>
        /// <param name="container"></param>
        /// <remarks>
        /// Initialize your controls in this method. Do not override CreateChildControls method.
        /// </remarks>
        protected override void InitializeControls(GenericContainer container)
        {
            if (this.LinkType == "Page") {
                GetPageLinkInfo();
            } else if (this.LinkType == "Document") {
                GetDocumentInfo();
            } else { // External
                GetExternalLink();
            }

            if (!this.Title.IsNullOrEmpty() && PageLink.Text.IsNullOrEmpty())
            {
                PageLink.Text = this.Title;
            }
            
            HtmlGenericControl block = this.Block;
            if (!string.IsNullOrEmpty(this.Style))
            {
                block.Attributes["class"] += " " + this.Style;
            }
            else
            {
                block.Attributes["class"] += " style-1";
            }

            PageLink.Click += PageLink_Click;
            btnSave.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            lblErrorMessage.Visible = false;

            var regexFirstName = new Regex(@"^[a-zA-Z\s]+$");
            var regexLastName = new Regex(@"^[a-zA-Z\s]+$");
            var regexEmail = new Regex(@"^([\w\.\-\+]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var regexCCode = new Regex(@"^\d+$"); // min 2 - max 3
            var regexMobileArea = new Regex(@"^\d+$"); // min 2 - max 5
            var regexMobile = new Regex(@"^\d+$");

            var firstName = txtFirstName.Text;
            var lastName = txtLastName.Text;
            var email = txtEmail.Text;
            // coursename from page title or field
            // interactionType from field
            // redirectionlink - ???
            var allowCommunication = chkAllowCommunication.Checked;
            var ccode = txtCountryCode.Text;
            var marea = txtMobileArea.Text;
            var mobile = txtNumber.Text;
            var gclid_field = txtGclid_field.Text;

            var isFormValid = true;

            if(!isFieldValid(firstName, regexFirstName))
            {
                isFormValid = false;
                // firstname is invalid
                lblFirstNameError.InnerText = "First name is required";
                markAsError(holderTxtFirstName, true);
            } else {
                markAsError(holderTxtFirstName, false);
            }

            if (!isFieldValid(lastName, regexLastName))
            {
                isFormValid = false;
                // lastname is invalid
                lblLastNameError.InnerText = "Last name is required";
                markAsError(holderTxtLastName, true);
            }
            else
            {
                markAsError(holderTxtLastName, false);
            }

            if (!isFieldValid(email, regexEmail))
            {
                isFormValid = false;
                // email is invalid
                lblEmailError.InnerText = "Not a valid email address";
                markAsError(holderTxtEmail, true);
            }
            else
            {
                markAsError(holderTxtEmail, false);
            }

            if (!isFieldValid(ccode, regexCCode, 2, 3))
            {
                isFormValid = false;
                // ccode is invalid
                lblCountryCodeError.InnerText = "Country code must be between 2 - 3 numbers long e.g. 353 or 44";
                markAsError(holderTxtCountryCode, true);
            }
            else
            {
                markAsError(holderTxtCountryCode, false);
            }

            if (!isFieldValid(marea, regexMobileArea, 2, 5))
            {
                isFormValid = false;
                // mobile code area is invalid
                lblMobileAreaError.InnerText = "Mobile code or area code must be between 2 - 5 numbers long e.g. 01 or 087";
                markAsError(holderTxtMobileArea, true);
            }
            else
            {
                markAsError(holderTxtMobileArea, false);
            }

            if (!isFieldValid(mobile, regexMobile))
            {
                isFormValid = false;
                // mobile is invalid
                lblNumberError.InnerText = "Mobile number is required and cannot be empty";
                markAsError(holderTxtNumber, true);
            }
            else
            {
                markAsError(holderTxtNumber, false);
            }
            

            if (!isFormValid)
            {
                // at least one field is invalid 
            } else
                {
                // save all details into the DiplomasPDFDownloads entity on Aptify
                //spCreateDiplomasPDFdownloads__cai
                SqlConnection con = new SqlConnection(SoftwareDesign.Helper.GetAptifyEntitiesConnectionString()); 
                var spCreateDiplomasPDFdownloads__cai = "spCreateDiplomasPDFdownloads__cai";
                using (SqlCommand cmd = new SqlCommand(spCreateDiplomasPDFdownloads__cai))
                {
                    cmd.Connection = con;
                    // in parameter
                    cmd.Parameters.Add(new SqlParameter("@FirstName", firstName));
                    cmd.Parameters.Add(new SqlParameter("@LastName", lastName));
                    cmd.Parameters.Add(new SqlParameter("@Email", email));
                    cmd.Parameters.Add(new SqlParameter("@PhoneCountryCode", ccode));
                    cmd.Parameters.Add(new SqlParameter("@PhoneAreaCode", marea));
                    cmd.Parameters.Add(new SqlParameter("@PhoneNumber", mobile));
                    cmd.Parameters.Add(new SqlParameter("@CourseName", GetCourseName()));
                    cmd.Parameters.Add(new SqlParameter("@InterationType", InteractionType));
                    cmd.Parameters.Add(new SqlParameter("@RedirectionLink", linkPdfButton.NavigateUrl));
                    cmd.Parameters.Add(new SqlParameter("@DateCreated", DateTime.Now));
                    cmd.Parameters.Add(new SqlParameter("@AllowCommunication", allowCommunication));
                    cmd.Parameters.Add(new SqlParameter("@Gclid", gclid_field));

                    // out parameter
                    cmd.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                    cmd.Parameters["@ID"].Direction = System.Data.ParameterDirection.Output;

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    int s = cmd.ExecuteNonQuery();
                    int ival = Convert.ToInt32(cmd.Parameters["@ID"].Value.ToString());
                    if (ival > 0)
                    {
                        // success
                        pnlFormToFill.Visible = false;
                        pnlThankYou.Visible = true;

                        Page.Session[GetSessionKey()] = "1";
                    } else
                    {
                        // failure
                        lblErrorMessage.InnerText = "There was an error submitting the form at this time.";
                        lblErrorMessage.Visible = true;
                    }

                    con.Close();
                }
            }
        }

        private void markAsError(HtmlGenericControl el, Boolean hasError)
        {
            if(hasError && !el.Attributes["class"].Contains("has-error"))
            {
                el.Attributes["class"] = el.Attributes["class"] + " has-error";
            } else if(!hasError)
            {
                el.Attributes["class"] = el.Attributes["class"].Replace("has-error", "");
            }
        }

        private Boolean isFieldValid(String txt, Regex reg, int min = 0, int max = 0)
        {
            var matches = reg.Match(txt.Trim());

            if(min > 0 && max > 0)
            {
                if(txt.Trim().Length > max || txt.Trim().Length < min)
                {
                    return false;
                }
            }

            return matches.Success;
        }

        /// <summary>
        /// Sets the url and title for your page link control
        /// </summary>
        public void GetPageLinkInfo()
        {
            var mgr = PageManager.GetManager();
            if (SelectedPageID != null && !Guid.Empty.Equals(SelectedPageID))
            {
                var pageNode = mgr.GetPageNode(SelectedPageID);
                //PageLink.NavigateUrl = ResolveUrl(pageNode.GetFullUrl());
                linkPdfButton.NavigateUrl = ResolveUrl(pageNode.GetFullUrl());
                PageLink.Attributes.Remove("target");

                if (this.Title.IsNullOrEmpty())
                {
                    PageLink.Text = pageNode.Title;
                }
                else
                {
                    PageLink.Text = this.Title;
                }
            }
        }

        /// <summary>
        /// Sets the selected image's thumbnail to your thumbnail control
        /// </summary>
        private void GetDocumentInfo()
        {
            LibrariesManager libraryManager = LibrariesManager.GetManager();
            if (SelectedDocumentID != null && !Guid.Empty.Equals(SelectedDocumentID))
            {
                var selectedDocument = libraryManager.GetDocument(SelectedDocumentID);
                //PageLink.NavigateUrl = ResolveUrl(selectedDocument.MediaUrl);
                if(selectedDocument != null)
                {
                    linkPdfButton.NavigateUrl = ResolveUrl(selectedDocument.MediaUrl);
                    PageLink.Attributes.Add("target", "_blank");

                    if (this.Title.IsNullOrEmpty())
                    {
                        PageLink.Text = selectedDocument.Title;
                    }
                    else
                    {
                        PageLink.Text = this.Title;
                    }
                }
                else
                {
                    PageLink.Text = "Sorry, file not found, please contact webmaster@charteredaccountants.ie.";
                    linkPdfButton.Visible = false;
                    PageLink.Visible = true;
                    btnSave.Visible = false;

                }
            }
        }

        /// <summary>
        /// Sets the selected image's thumbnail to your thumbnail control
        /// </summary>
        private void GetExternalLink()
        {
            //PageLink.NavigateUrl = this.ExternalLink;
            linkPdfButton.NavigateUrl = ExternalLink;
            PageLink.Attributes.Add("target", "_blank");
        }

        private String GetSessionKey()
        {
            return "pdf_download_" + InteractionType + "_" + GetCourseName().Replace(' ', '_');
        }

        private String GetCourseName()
        {
            return String.IsNullOrEmpty(CourseName) ? Page.Title : CourseName;
        }

        protected void PageLink_Click(object sender, EventArgs e)
        {
            // checking if previously submitted

            var isSubmitted = Page.Session[GetSessionKey()] != null && Page.Session[GetSessionKey()].ToString().Equals("1");

            if(!isSubmitted)
            {
                pnlFormToFill.Visible = true;
                pnlThankYou.Visible = false;
            } else
            {
                pnlFormToFill.Visible = false;
                pnlThankYou.Visible = true;
            }
        }

        #endregion

        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/UserControls/SoftwareDesign_Aptify/LayoutWidgets/PDFDownloadButtonWidget/PDFDownloadButtonWidget.ascx";
        #endregion
    }
}
