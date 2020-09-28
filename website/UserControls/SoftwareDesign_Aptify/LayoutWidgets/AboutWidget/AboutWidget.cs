using System;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI;

namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.AboutWidget
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
    ///             .LoadOrAddWidget<AboutWidget>("AboutWidget")
    ///                 .SetTitle("AboutWidget")
    ///                 .SetDescription("AboutWidget")
    ///                 .LocalizeUsing<ModuleResourceClass>() //Optional
    ///                 .SetCssClass(WidgetCssClass) // You can use a css class to add an icon (Optional)
    ///             .Done()
    ///         .Done()
    ///     .Done();
    /// </remarks>
    /// <see cref="http://www.sitefinity.com/documentation/documentationarticles/user-guide/widgets"/>
    [Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesigner(typeof(SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.AboutWidget.Designer.AboutWidgetDesigner))]
    public class AboutWidget : SimpleView
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
        /// Gets or sets the message that will be displayed in the label.
        /// </summary>
        public string Style { get; set; }


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
                    return AboutWidget.layoutTemplatePath;
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
        /// Reference to the Image control that shows the image thumbnail.
        /// </summary>
        protected virtual HtmlGenericControl Thumbnail
        {
            get
            {
                return this.Container.GetControl<HtmlGenericControl>("caiLogo", true);
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
        protected virtual HtmlGenericControl Box
        {
            get
            {
                return this.Container.GetControl<HtmlGenericControl>("designerLayoutRoot", true);
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
            GetPageLinkInfo();

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
                subtitleLabel.Text = "Subtite is not set!";
            }
            else
            {
                subtitleLabel.Text = this.Subtitle;
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
        /// Sets the url and title for your page link control
        /// </summary>
        public void GetPageLinkInfo()
        {
            var mgr = PageManager.GetManager();
            if (!Guid.Empty.Equals(SelectedPageID))
            {
                var pageNode = mgr.GetPageNode(SelectedPageID);
                PageLink.NavigateUrl = ResolveUrl(pageNode.GetFullUrl());
                //PageLink.Text = pageNode.Title;
                //PageLink.Target = "_blank";
            }
            else
            {
                PageLink.Style.Add("display", "none");
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
                try
                {
                    var selectedImage = libraryManager.GetImage(SelectedImageID);
                    Thumbnail.Attributes.Add("style", "background-image:url(" + selectedImage.Url + ")");
                } catch(Exception ex)
                {
                    // error happened, just set image to be not visible
                    Thumbnail.Style.Add("display", "none");
                }
            }
            else
            {
                Thumbnail.Style.Add("display", "none");
            }
        }
        #endregion

        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/UserControls/SoftwareDesign_Aptify/LayoutWidgets/AboutWidget/AboutWidget.ascx";
        #endregion
    }
}
