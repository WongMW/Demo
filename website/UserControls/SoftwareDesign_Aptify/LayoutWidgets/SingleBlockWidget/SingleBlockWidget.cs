using System;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI;

namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.SingleBlockWidget
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
    ///             .LoadOrAddWidget<SingleBlockWidget>("SingleBlockWidget")
    ///                 .SetTitle("SingleBlockWidget")
    ///                 .SetDescription("SingleBlockWidget")
    ///                 .LocalizeUsing<ModuleResourceClass>() //Optional
    ///                 .SetCssClass(WidgetCssClass) // You can use a css class to add an icon (Optional)
    ///             .Done()
    ///         .Done()
    ///     .Done();
    /// </remarks>
    /// <see cref="http://www.sitefinity.com/documentation/documentationarticles/user-guide/widgets"/>
    [Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesigner(typeof(SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.SingleBlockWidget.Designer.SingleBlockWidgetDesigner))]
    public class SingleBlockWidget : SimpleView
    {
        #region Properties
        /// <summary>
        /// Gets or sets the message that will be displayed in the label.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the message that will be displayed in the label.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Gets or sets the message that will be displayed in the label.
        /// </summary>
        /// 
        public string ButtonLabel { get; set; }
        /// <summary>
        /// Gets or sets the buttonlabel that will be displayed in the label.
        /// </summary>
        public string Style { get; set; }
        /// <summary>
        /// guid from designer for our selected image
        /// </summary>
        public Guid SelectedImageID { get; set; }
        /// <summary>
        /// source url for our image
        /// </summary>
        public string SelectedImagePath { get; set; }
        /// <summary>
        /// guid from designer for our selected page
        /// </summary>
        public Guid SelectedPageID { get; set; }

        /// <summary>
        /// Obsolete. Use LayoutTemplatePath instead.
        /// </summary>
        public string LinkText { get; set; }
        /// <summary>
        /// Gets or sets the buttonlabel that will be displayed in the label.
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
                    return SingleBlockWidget.layoutTemplatePath;
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
        protected virtual Label TitleLabel1
        {
            get
            {
                return this.Container.GetControl<Label>("TitleLabel1", true);
            }
        }
        protected virtual Label TitleLabel2
        {
            get
            {
                return this.Container.GetControl<Label>("TitleLabel2", true);
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
        /// Reference to the Image control that shows the image thumbnail.
        /// </summary>
        protected virtual HtmlGenericControl Thumbnail
        {
            get
            {
                return this.Container.GetControl<HtmlGenericControl>("image", true);
            }
        }

        /// <summary>
        /// Reference to the HyperLink control that shows the page url.
        /// </summary>
        protected virtual HyperLink PageLink
        {
            get
            {
                return this.Container.GetControl<HyperLink>("PageLink", true);
            }
        }

        protected virtual HyperLink FullLink
        {
            get
            {
                return this.Container.GetControl<HyperLink>("FullLink", true);
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
            GetPageLinkInfo(this.Style);

            Label messageLabel = this.MessageLabel;
            if (string.IsNullOrEmpty(this.Message))
            {
                messageLabel.Text = "Message is not set!";
            }
            else
            {
                messageLabel.Text = this.Message;
            }
            Label titleLabel1 = this.TitleLabel1;
            Label titleLabel2 = this.TitleLabel2;
            if (string.IsNullOrEmpty(this.Title))
            {
                titleLabel1.Text = "Title is not set!";
                titleLabel2.Text = "Title is not set!";
            }
            else
            {
                titleLabel1.Text = this.Title;
                titleLabel2.Text = this.Title;
            }

            HtmlGenericControl panelHolder = this.Holder;
            if (!string.IsNullOrEmpty(this.Style))
            {
                panelHolder.Attributes["class"] += " " + this.Style;
            }
        }

        /// <summary>
        /// Sets the url and title for your page link control
        /// </summary>
        public void GetPageLinkInfo(String style)
        {
            var mgr = PageManager.GetManager();
            if (!Guid.Empty.Equals(SelectedPageID))
            {
                var pageNode = mgr.GetPageNode(SelectedPageID);
                FullLink.NavigateUrl = ResolveUrl(pageNode.GetFullUrl());
                PageLink.Text = pageNode.Title;
            }
            else if (!string.IsNullOrEmpty(this.LinkText))
            {
                FullLink.NavigateUrl = ResolveUrl(LinkText);
            }
            else
            {
                PageLink.Style.Add("display", "none");
            }

            if (string.IsNullOrEmpty(this.ButtonLabel))
            {
                PageLink.Text = "More Info";
            }
            else
            {
                PageLink.Text = this.ButtonLabel;
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
                Thumbnail.Attributes.Add("style", "background-image:url(" + url + ")");
            }
            else
            {
                Thumbnail.Style.Add("display", "none");
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
        #endregion

        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/UserControls/SoftwareDesign_Aptify/LayoutWidgets/SingleBlockWidget/SingleBlockWidget.ascx";
        #endregion
    }
}
