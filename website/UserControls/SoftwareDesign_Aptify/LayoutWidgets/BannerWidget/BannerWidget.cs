using System;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Web.UI;

namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.BannerWidget
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
    ///             .LoadOrAddWidget<BannerWidget>("BannerWidget")
    ///                 .SetTitle("BannerWidget")
    ///                 .SetDescription("BannerWidget")
    ///                 .LocalizeUsing<ModuleResourceClass>() //Optional
    ///                 .SetCssClass(WidgetCssClass) // You can use a css class to add an icon (Optional)
    ///             .Done()
    ///         .Done()
    ///     .Done();
    /// </remarks>
    /// <see cref="http://www.sitefinity.com/documentation/documentationarticles/user-guide/widgets"/>
    [Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesigner(typeof(SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.BannerWidget.Designer.BannerWidgetDesigner))]
    public class BannerWidget : SimpleView
    {
        #region Properties
        /// <summary>
        /// Gets or sets the message that will be displayed in the label.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// guid from designer for our selected image
        /// </summary>
        public Guid SelectedImageID { get; set; }
        /// <summary>
        /// source url for our image
        /// </summary>
        public string SelectedImagePath { get; set; }

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
                    return BannerWidget.layoutTemplatePath;
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
        /// Reference to the Image control that shows the image thumbnail.
        /// </summary>
        protected virtual Image Thumbnail
        {
            get
            {
                return this.Container.GetControl<Image>("caiLogo", true);
            }
        }


        /// <summary>
        /// Reference to the HtmlGenericControl control that shows the image thumbnail.
        /// </summary>
        protected virtual HtmlGenericControl Holder
        {
            get
            {
                return this.Container.GetControl<HtmlGenericControl>("Holder", true);
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

            GetImageThumbnail();

            Label messageLabel = this.MessageLabel;
            if (string.IsNullOrEmpty(this.Message))
            {
                messageLabel.Text = "Hello, World!";
            }
            else
            {
                messageLabel.Text = this.Message;
            }
        }

        /// <summary>
        /// Sets the selected image's thumbnail to your thumbnail control
        /// </summary>
        private void GetImageThumbnail()
        {
            LibrariesManager libraryManager = LibrariesManager.GetManager();
            if (!Guid.Empty.Equals(SelectedImageID))
            {
                var selectedImage = libraryManager.GetImage(SelectedImageID);
                Holder.Style.Add("background-image", "url('" + selectedImage.Url + "')");
            }
        }
        #endregion

        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/UserControls/SoftwareDesign_Aptify/LayoutWidgets/BannerWidget/BannerWidget.ascx";
        #endregion
    }
}
