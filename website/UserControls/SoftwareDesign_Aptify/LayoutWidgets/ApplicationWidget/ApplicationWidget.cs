using System;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;

namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.ApplicationWidget
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
    ///             .LoadOrAddWidget<ApplicationWidget>("ApplicationWidget")
    ///                 .SetTitle("ApplicationWidget")
    ///                 .SetDescription("ApplicationWidget")
    ///                 .LocalizeUsing<ModuleResourceClass>() //Optional
    ///                 .SetCssClass(WidgetCssClass) // You can use a css class to add an icon (Optional)
    ///             .Done()
    ///         .Done()
    ///     .Done();
    /// </remarks>
    /// <see cref="http://www.sitefinity.com/documentation/documentationarticles/user-guide/widgets"/>
    [Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesigner(typeof(SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.ApplicationWidget.Designer.ApplicationWidgetDesigner))]
    public class ApplicationWidget : SimpleView
    {
        #region Properties
        /// <summary>
        /// Gets or sets the message that will be displayed in the label.
        /// </summary>
        public string AppOneTitle { get; set; }
        public string AppOneDate { get; set; }
        public string AppTwoTitle { get; set; }
        public string AppTwoDate { get; set; }
        public string AppThreeTitle { get; set; }
        public string AppThreeDate { get; set; }

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
                    return ApplicationWidget.layoutTemplatePath;
                return base.LayoutTemplatePath;
            }
            set
            {
                base.LayoutTemplatePath = value;
            }
        }
        #endregion

        #region Control References
        

        protected virtual Label AppOneTitleLabel
        {
            get
            {
                return this.Container.GetControl<Label>("AppOneTitleLabel", true);
            }
        }

        protected virtual Label AppOneDateLabel
        {
            get
            {
                return this.Container.GetControl<Label>("AppOneDateLabel", true);
            }
        }

        protected virtual Label EntryMessageOne
        {
            get
            {
                return this.Container.GetControl<Label>("EntryMessageOne", true);
            }
        }

        protected virtual HtmlGenericControl ApplicationOne
        {
            get
            {
                return this.Container.GetControl<HtmlGenericControl>("ApplicationOne", true);
            }
        }

        /*Application two*/
        protected virtual Label AppTwoTitleLabel
        {
            get
            {
                return this.Container.GetControl<Label>("AppTwoTitleLabel", true);
            }
        }

        protected virtual Label AppTwoDateLabel
        {
            get
            {
                return this.Container.GetControl<Label>("AppTwoDateLabel", true);
            }
        }

        protected virtual Label EntryMessageTwo
        {
            get
            {
                return this.Container.GetControl<Label>("EntryMessageTwo", true);
            }
        }

        protected virtual HtmlGenericControl ApplicationTwo
        {
            get
            {
                return this.Container.GetControl<HtmlGenericControl>("ApplicationTwo", true);
            }
        }


        /*Application three*/
        protected virtual Label AppThreeTitleLabel
        {
            get
            {
                return this.Container.GetControl<Label>("AppThreeTitleLabel", true);
            }
        }

        protected virtual Label AppThreeDateLabel
        {
            get
            {
                return this.Container.GetControl<Label>("AppThreeDateLabel", true);
            }
        }

        protected virtual Label EntryMessageThree
        {
            get
            {
                return this.Container.GetControl<Label>("EntryMessageThree", true);
            }
        }

        protected virtual HtmlGenericControl ApplicationThree
        {
            get
            {
                return this.Container.GetControl<HtmlGenericControl>("ApplicationThree", true);
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
            Label appOneTitleLabel = this.AppOneTitleLabel;            
            if (!string.IsNullOrEmpty(this.AppOneTitle))
            {
                this.ApplicationOne.Visible = true;
                appOneTitleLabel.Text = this.AppOneTitle;
                
                Label appOneDateLabel = this.AppOneDateLabel;
                if (!string.IsNullOrEmpty(this.AppOneDate))
                {
                    appOneDateLabel.Text = this.AppOneDate;
                }
                else
                {
                    this.ApplicationOne.Attributes["class"] += " closed";
                    Label entryMessageOne = this.EntryMessageOne;
                    entryMessageOne.Text = "Now Closed";
                }
            }

            Label appTwoTitleLabel = this.AppTwoTitleLabel;
            if (!string.IsNullOrEmpty(this.AppTwoTitle))
            {
                this.ApplicationTwo.Visible = true;
                appTwoTitleLabel.Text = this.AppTwoTitle;

                Label appTwoDateLabel = this.AppTwoDateLabel;
                if (!string.IsNullOrEmpty(this.AppTwoDate))
                {
                    appTwoDateLabel.Text = this.AppTwoDate;
                }
                else
                {
                    this.ApplicationTwo.Attributes["class"] += " closed";
                    Label entryMessageTwo = this.EntryMessageTwo;
                    entryMessageTwo.Text = "Now Closed";
                }
            }

            Label appThreeTitleLabel = this.AppThreeTitleLabel;
            if (!string.IsNullOrEmpty(this.AppThreeTitle))
            {
                this.ApplicationThree.Visible = true;
                appThreeTitleLabel.Text = this.AppThreeTitle;

                Label appThreeDateLabel = this.AppThreeDateLabel;
                if (!string.IsNullOrEmpty(this.AppThreeDate))
                {
                    appThreeDateLabel.Text = this.AppThreeDate;
                }
                else
                {
                    this.ApplicationThree.Attributes["class"] += " closed";
                    Label entryMessageThree = this.EntryMessageThree;
                    entryMessageThree.Text = "Now Closed";
                }
            }

            
        }
        #endregion

        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/UserControls/SoftwareDesign_Aptify/LayoutWidgets/ApplicationWidget/ApplicationWidget.ascx";
        #endregion
    }
}
