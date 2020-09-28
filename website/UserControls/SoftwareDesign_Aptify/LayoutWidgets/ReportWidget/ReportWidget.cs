using System;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web;
using System.Web.UI.HtmlControls;


namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.ReportWidget
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
    ///             .LoadOrAddWidget<ReportWidget>("ReportWidget")
    ///                 .SetTitle("ReportWidget")
    ///                 .SetDescription("ReportWidget")
    ///                 .LocalizeUsing<ModuleResourceClass>() //Optional
    ///                 .SetCssClass(WidgetCssClass) // You can use a css class to add an icon (Optional)
    ///             .Done()
    ///         .Done()
    ///     .Done();
    /// </remarks>
    /// <see cref="http://www.sitefinity.com/documentation/documentationarticles/user-guide/widgets"/>
    [Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesigner(typeof(SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.ReportWidget.Designer.ReportWidgetDesigner))]
    public class ReportWidget : SimpleView
    {
        #region Properties
        /// <summary>
        /// Gets or sets the message that will be displayed in the label.
        /// </summary>
        public string Message { get; set; }

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
                    return ReportWidget.layoutTemplatePath;
                return base.LayoutTemplatePath;
            }
            set
            {
                base.LayoutTemplatePath = value;
            }
        }
        #endregion

        #region Control References
        /// <summary>
        /// Reference to the Label control that shows the Message.
        /// </summary>
        protected virtual Label MessageLabel
        {
            get
            {
                return this.Container.GetControl<Label>("MessageLabel", true);
            }
        }

        /// <summary>
        /// Reference to the Label control that shows the Message.
        /// </summary>
        protected virtual LinkButton YesVote
        {
            get
            {
                return this.Container.GetControl<LinkButton>("YesVote", true);
            }
        }

        /// <summary>
        /// Reference to the Label control that shows the Message.
        /// </summary>
        protected virtual LinkButton NoVote
        {
            get
            {
                return this.Container.GetControl<LinkButton>("NoVote", true);
            }
        }

        /// <summary>
        /// Reference to the Label control that shows the Message.
        /// </summary>
        protected virtual HtmlGenericControl CommentHolder
        {
            get
            {
                return this.Container.GetControl<HtmlGenericControl>("CommentHolder", true);
            }
        }

        /// <summary>
        /// Reference to the Label control that shows the Message.
        /// </summary>
        protected virtual TextBox CommentBox
        {
            get
            {
                return this.Container.GetControl<TextBox>("CommentBox", true);
            }
        }

        /// <summary>
        /// Reference to the Label control that shows the Message.
        /// </summary>
        protected virtual LinkButton SubmitButton
        {
            get
            {
                return this.Container.GetControl<LinkButton>("SubmitButton", true);
            }
        }
        /// <summary>
        /// Reference to the Label control that shows the Message.
        /// </summary>
        protected virtual HtmlGenericControl HelpfulForm
        {
            get
            {
                return this.Container.GetControl<HtmlGenericControl>("HelpfulForm", true);
            }
        }
        /// <summary>
        /// Reference to the Label control that shows the Message.
        /// </summary>
        protected virtual Label SuccessMessage
        {
            get
            {
                return this.Container.GetControl<Label>("Message", true);
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
            LinkButton yesButton = this.YesVote;
            yesButton.Click += yesButton_Click;

            LinkButton noButton = this.NoVote;
            noButton.Click += noButton_Click;

            LinkButton submitButton = this.SubmitButton;
            submitButton.Click += submitButton_Click;
        }

        void submitButton_Click(object sender, EventArgs e)
        {
            String comment = this.CommentBox.Text;
            CreateReport(false, comment);
        }

        void noButton_Click(object sender, EventArgs e)
        {
            this.CommentHolder.Visible = true;
        }

        void yesButton_Click(object sender, EventArgs e)
        {
            CreateReport(true, null);
        }

        public void CreateReport(Boolean helpful, String comment)
        {
            // Set the provider name for the DynamicModuleManager here. All available providers are listed in
            var providerName = String.Empty;

            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
            dynamicModuleManager.Provider.SuppressSecurityChecks = true;
            Type reportType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Reports.Report");
            DynamicContent reportItem = dynamicModuleManager.CreateDataItem(reportType);

            var page = new PageManager().GetPageNode(new Guid(SiteMapBase.GetCurrentProvider().CurrentNode.Key));

            // This is how values for the properties are set
            reportItem.SetValue("PageURL", page.GetUrl());
            reportItem.SetValue("PageTitle", page.Title);
            reportItem.SetValue("Helpful", helpful);

            if (comment != null)
            {
                reportItem.SetValue("Comments", comment);
            }

            Lstring idString = "Report-" + DateTime.Now.ToString() + "-" + page.Title;
            reportItem.SetValue("ReportID", idString);
            reportItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
            reportItem.SetValue("PublicationDate", DateTime.Now);
            reportItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");

            // You need to call SaveChanges() in order for the items to be actually persisted to data store
            dynamicModuleManager.SaveChanges();

            this.HelpfulForm.Visible = false;
            this.SuccessMessage.Visible = true;
        }


        #endregion

        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/UserControls/SoftwareDesign_Aptify/LayoutWidgets/ReportWidget/ReportWidget.ascx";
        #endregion
    }
}
