using System;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI;

namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.SingleBlockDocumentWidget
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
    ///             .LoadOrAddWidget<SingleBlockDocumentWidget>("SingleBlockDocumentWidget")
    ///                 .SetTitle("SingleBlockDocumentWidget")
    ///                 .SetDescription("SingleBlockDocumentWidget")
    ///                 .LocalizeUsing<ModuleResourceClass>() //Optional
    ///                 .SetCssClass(WidgetCssClass) // You can use a css class to add an icon (Optional)
    ///             .Done()
    ///         .Done()
    ///     .Done();
    /// </remarks>
    /// <see cref="http://www.sitefinity.com/documentation/documentationarticles/user-guide/widgets"/>
    [Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesigner(typeof(SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.SingleBlockDocumentWidget.Designer.SingleBlockDocumentWidgetDesigner))]
    public class SingleBlockDocumentWidget : SimpleView
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
        /// guid from designer for our selected document
        /// </summary>
        public Guid SelectedDocumentID { get; set; }

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
                    return SingleBlockDocumentWidget.layoutTemplatePath;
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

        /// <summary>
        /// Gets or sets the link type.
        /// </summary>
        public string LinkType { get; set; }
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

            GetDocumentInfo();
        }

        /// <summary>
        /// Sets the selected image's thumbnail to your thumbnail control
        /// </summary>
        private void GetDocumentInfo()
        {
            LibrariesManager libraryManager = LibrariesManager.GetManager();
            if (!Guid.Empty.Equals(SelectedDocumentID))
            {
                Document selectedDocument = null;
                try
                {
                    selectedDocument = libraryManager.GetDocument(SelectedDocumentID);
                } catch(Exception e)
                {

                }

                if(selectedDocument != null)
                {
                    FullLink.NavigateUrl = ResolveUrl(selectedDocument.MediaUrl);
                    FullLink.Attributes.Add("target", "_blank");

                    if (this.ButtonLabel.IsNullOrEmpty())
                    {
                        PageLink.Text = "Open Document";
                    }
                    else
                    {
                        PageLink.Text = this.ButtonLabel;
                    }
                }
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
                Thumbnail.Attributes.Add("style", "background-image:url(" + selectedImage.Url + ")");
            }
            else
            {
                Thumbnail.Style.Add("display", "none");
            }
        }
        #endregion

        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/UserControls/SoftwareDesign_Aptify/LayoutWidgets/SingleBlockDocumentWidget/SingleBlockDocumentWidget.ascx";
        #endregion
    }
}
