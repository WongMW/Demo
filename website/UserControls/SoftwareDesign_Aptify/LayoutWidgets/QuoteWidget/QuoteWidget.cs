using System;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Web.UI;

namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.QuoteWidget
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
    ///             .LoadOrAddWidget<QuoteWidget>("QuoteWidget")
    ///                 .SetTitle("QuoteWidget")
    ///                 .SetDescription("QuoteWidget")
    ///                 .LocalizeUsing<ModuleResourceClass>() //Optional
    ///                 .SetCssClass(WidgetCssClass) // You can use a css class to add an icon (Optional)
    ///             .Done()
    ///         .Done()
    ///     .Done();
    /// </remarks>
    /// <see cref="http://www.sitefinity.com/documentation/documentationarticles/user-guide/widgets"/>
    [Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesigner(typeof(SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.QuoteWidget.Designer.QuoteWidgetDesigner))]
    public class QuoteWidget : SimpleView
    {
        #region Properties
        
        /// <summary>
        /// Gets or sets the quote that will be displayed in the label.
        /// </summary>
        public string Quote { get; set; }
        /// <summary>
        /// Gets or sets the author that will be displayed in the label.
        /// </summary>
        public string Author { get; set; }
        // <summary>
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
                    return QuoteWidget.layoutTemplatePath;
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
        /// Reference to the Label control that shows the quote.
        /// </summary>
        protected virtual Label QuoteLabel
        {
            get
            {
                return this.Container.GetControl<Label>("QuoteLabel", true);
            }
        }

        /// <summary>
        /// Reference to the Label control that shows the Author.
        /// </summary>
        protected virtual Label AuthorLabel
        {
            get
            {
                return this.Container.GetControl<Label>("AuthorLabel", true);
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

            Label authorLabel = this.AuthorLabel;
            if (string.IsNullOrEmpty(this.Author))
            {
                authorLabel.Text = "John Doe";
            }
            else
            {
                authorLabel.Text = this.Author;
            }

            Label quoteLabel = this.QuoteLabel;
            if (string.IsNullOrEmpty(this.Quote))
            {
                quoteLabel.Text = "This is a quote!";
            }
            else
            {
                quoteLabel.Text = this.Quote;
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
        public static readonly string layoutTemplatePath = "~/UserControls/SoftwareDesign_Aptify/LayoutWidgets/QuoteWidget/QuoteWidget.ascx";
        #endregion
    }
}
