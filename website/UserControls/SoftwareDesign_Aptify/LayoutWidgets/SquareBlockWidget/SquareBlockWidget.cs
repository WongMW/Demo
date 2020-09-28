using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Modules.Pages;

namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.SquareBlockWidget
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
    ///             .LoadOrAddWidget<SquareBlockWidget>("SquareBlockWidget")
    ///                 .SetTitle("SquareBlockWidget")
    ///                 .SetDescription("SquareBlockWidget")
    ///                 .LocalizeUsing<ModuleResourceClass>() //Optional
    ///                 .SetCssClass(WidgetCssClass) // You can use a css class to add an icon (Optional)
    ///             .Done()
    ///         .Done()
    ///     .Done();
    /// </remarks>
    /// <see cref="http://www.sitefinity.com/documentation/documentationarticles/user-guide/widgets"/>
    [Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesigner(typeof(SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.SquareBlockWidget.Designer.SquareBlockWidgetDesigner))]
    public class SquareBlockWidget : SimpleView
    {
        #region Properties
        /// <summary>
        /// Gets or sets the message that will be displayed in the label.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Gets or sets the message that will be displayed in the label.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the message that will be displayed in the label.
        /// </summary>
        public string Subtitle { get; set; }
        /// <summary>
        /// Gets or sets the message that will be displayed in the label.
        /// </summary>
        public string Style { get; set; }
        /// <summary>
        /// guid from designer for our selected image
        /// </summary>
        public Guid SelectedImageID { get; set; }
        /// <summary>
        /// guid from designer for our selected page
        /// </summary>
        public Guid SelectedPageID { get; set; }
        /// <summary>
        /// guid from designer for our selected document
        /// </summary>
        public Guid SelectedDocumentID { get; set; }
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
                    return SquareBlockWidget.layoutTemplatePath;
                return base.LayoutTemplatePath;
            }
            set
            {
                base.LayoutTemplatePath = value;
            }
        }
        public int? ImageSize { get; set; }
        #endregion

        #region Control References
        /// <summary>
        /// Reference to the HyperLink control that shows the page url.
        /// </summary>
        protected virtual HyperLink BlockLink
        {
            get
            {
                return this.Container.GetControl<HyperLink>("BlockLink", true);
            }
        }
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
        protected virtual Label TitleLabel
        {
            get
            {
                return this.Container.GetControl<Label>("TitleLabel", true);
            }
        }
        /// <summary>
        /// Reference to the Label control that shows the Message.
        /// </summary>
        protected virtual Label SubtitleLabel
        {
            get
            {
                return this.Container.GetControl<Label>("SubtitleLabel", true);
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
        /// Reference to the HtmlGenericControl control that shows the image thumbnail.
        /// </summary>
        protected virtual HtmlGenericControl Holder
        {
            get
            {
                return this.Container.GetControl<HtmlGenericControl>("Holder", true);
            }
        }

        /// <summary>
        /// Reference to the HtmlGenericControl control that shows the image thumbnail.
        /// </summary>
        protected virtual HtmlGenericControl Box
        {
            get
            {
                return this.Container.GetControl<HtmlGenericControl>("designerLayoutRoot", true);
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
                messageLabel.Text = "Message is not set!";
            }
            else
            {
                messageLabel.Text = this.Message;
            }
            Label titleLabel = this.TitleLabel;
            if (string.IsNullOrEmpty(this.Title))
            {
                titleLabel.Text = "Title is not set!";
            }
            else
            {
                titleLabel.Text = this.Title;
            }
            Label subtitleLabel = this.SubtitleLabel;
            if (string.IsNullOrEmpty(this.Subtitle))
            {
                subtitleLabel.Text = "Hello, World!";
            }
            else
            {
                subtitleLabel.Text = this.Subtitle;
            }

            if (this.LinkType == "Page")
            {
                GetPageLinkInfo();
            }
            else if (this.LinkType == "Document")
            {
                GetDocumentInfo();
            }
            else
            { // External
                GetExternalLink();
            }

            HtmlGenericControl box = this.Box;
            if (!string.IsNullOrEmpty(this.Style))
            {
                box.Attributes["class"] += " " + this.Style;
            }
            else
            {
                box.Attributes["class"] += " style-1";
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
                var url = GetImageUrl(selectedImage.Url);
                Holder.Style.Add("background-image", "url('" + url + "')");
            }
        }

        private string GetImageUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                return url;

            if (ImageSize == null)
                return url;

            var uriBuilder = new UriBuilder(url);

            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["size"] = ImageSize.Value.ToString();

            uriBuilder.Query = query.ToString();

            return uriBuilder.ToString();
        }

        /// <summary>
        /// Sets the url and title for your page link control
        /// </summary>
        public void GetPageLinkInfo()
        {
            var mgr = PageManager.GetManager();
            if (!Guid.Empty.Equals(SelectedPageID))
            {
                var pageNode = mgr.GetPageNode(SelectedPageID);
                BlockLink.NavigateUrl = ResolveUrl(pageNode.GetFullUrl());
                BlockLink.Attributes.Remove("target");
            }
        }

        /// <summary>
        /// Sets the selected image's thumbnail to your thumbnail control
        /// </summary>
        private void GetDocumentInfo()
        {
            LibrariesManager libraryManager = LibrariesManager.GetManager();
            if (!Guid.Empty.Equals(SelectedDocumentID))
            {
                var selectedDocument = libraryManager.GetDocument(SelectedDocumentID);
                BlockLink.NavigateUrl = ResolveUrl(selectedDocument.MediaUrl);
                BlockLink.Attributes.Add("target", "_blank");
            }
        }

        /// <summary>
        /// Sets the selected image's thumbnail to your thumbnail control
        /// </summary>
        private void GetExternalLink()
        {
            BlockLink.NavigateUrl = this.ExternalLink;
            BlockLink.Attributes.Add("target", "_blank");
        }
        #endregion

        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/UserControls/SoftwareDesign_Aptify/LayoutWidgets/SquareBlockWidget/SquareBlockWidget.ascx";
        #endregion
    }
}
