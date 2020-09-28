using System;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI;

namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.LinkBoxWidget
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
    ///             .LoadOrAddWidget<LinkBoxWidget>("LinkBoxWidget")
    ///                 .SetTitle("LinkBoxWidget")
    ///                 .SetDescription("LinkBoxWidget")
    ///                 .LocalizeUsing<ModuleResourceClass>() //Optional
    ///                 .SetCssClass(WidgetCssClass) // You can use a css class to add an icon (Optional)
    ///             .Done()
    ///         .Done()
    ///     .Done();
    /// </remarks>
    /// <see cref="http://www.sitefinity.com/documentation/documentationarticles/user-guide/widgets"/>
    [Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesigner(typeof(SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.LinkBoxWidget.Designer.LinkBoxWidgetDesigner))]
    public class LinkBoxWidget : SimpleView
    {
        #region Properties
        /// <summary>
        /// Gets or sets the message that will be displayed in the label.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the message that will be displayed in the label.
        /// </summary>
        public Boolean TabCheckbox { get; set; }
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
        /// guid from designer for our selected document
        /// </summary>
        public Guid SelectedDocumentID { get; set; }
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
                    return LinkBoxWidget.layoutTemplatePath;
                return base.LayoutTemplatePath;
            }
            set
            {
                base.LayoutTemplatePath = value;
            }
        }
        #endregion

    
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
        protected virtual Image Thumbnail
        {
            get
            {
                return this.Container.GetControl<Image>("image", true);
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

        /// <summary>
        /// Image size to append for dynamic image sizes
        /// </summary>
        public int? ImageSize { get; set; }

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
            GetPageLinkInfo();
            HtmlGenericControl panelHolder = this.Holder;
        }

        /// <summary>
        /// Sets the url and title for your page link control
        /// </summary>
        public void GetPageLinkInfo()
        {
            //get image 
            LibrariesManager libraryManager = LibrariesManager.GetManager();
            if (!Guid.Empty.Equals(SelectedImageID))
            {
                var selectedImage = libraryManager.GetImage(SelectedImageID);
                Thumbnail.ImageUrl = GetImageUrl(selectedImage.Url);
            }
            else
            {
                Thumbnail.Style.Add("display", "none");
            }

            //get page or document or link
            Label titleLabel = this.TitleLabel;
            var mgr = PageManager.GetManager();
            if (!Guid.Empty.Equals(SelectedPageID))
            {
                var pageNode = mgr.GetPageNode(SelectedPageID);
                PageLink.NavigateUrl = ResolveUrl(pageNode.GetFullUrl());
                titleLabel.Text = pageNode.Title;
                //PageLink.Target = "_blank";
            }
            else if (!string.IsNullOrEmpty(this.LinkText))
            {
                PageLink.NavigateUrl = ResolveUrl(LinkText);
            }
            else if (!Guid.Empty.Equals(SelectedDocumentID))
            {
                var selectedDocument = libraryManager.GetDocument(SelectedDocumentID);
                PageLink.NavigateUrl = ResolveUrl(selectedDocument.MediaUrl);
                PageLink.Attributes.Add("target", "_blank");

                if (this.ButtonLabel.IsNullOrEmpty())
                {
                    PageLink.Text = "Open Document";
                }
                else
                {
                    PageLink.Text = this.ButtonLabel;
                }
            }
            else
            {
                titleLabel.Text = "More Info";
            }

            if (!string.IsNullOrEmpty(this.Title))
            {
                titleLabel.Text = Title;
            }

            if (TabCheckbox)
            {
                PageLink.Target = "_blank";
            }
            else
            {
                PageLink.Target = null;
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
        public static readonly string layoutTemplatePath = "~/UserControls/SoftwareDesign_Aptify/LayoutWidgets/LinkBoxWidget/LinkBoxWidget.ascx";
        #endregion
    }
}
