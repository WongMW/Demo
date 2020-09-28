using Aptify.Framework.ExceptionManagement;
using System;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI;

namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.ButtonWidget
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
    ///             .LoadOrAddWidget<ButtonWidget>("ButtonWidget")
    ///                 .SetTitle("ButtonWidget")
    ///                 .SetDescription("ButtonWidget")
    ///                 .LocalizeUsing<ModuleResourceClass>() //Optional
    ///                 .SetCssClass(WidgetCssClass) // You can use a css class to add an icon (Optional)
    ///             .Done()
    ///         .Done()
    ///     .Done();
    /// </remarks>
    /// <see cref="http://www.sitefinity.com/documentation/documentationarticles/user-guide/widgets"/>
    [Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesigner(typeof(SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.ButtonWidget.Designer.ButtonWidgetDesigner))]
    public class ButtonWidget : SimpleView
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
                    return ButtonWidget.layoutTemplatePath;
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
        /// Reference to the HyperLink control that shows the page url.
        /// </summary>
        protected virtual HyperLink DocumentLink
        {
            get
            {
                return this.Container.GetControl<HyperLink>("DocumentLink", true);
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

            if (!this.Title.IsNullOrEmpty())
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
            try
            {
                LibrariesManager libraryManager = LibrariesManager.GetManager();
                if (!Guid.Empty.Equals(SelectedDocumentID))
                {
                    var selectedDocument = libraryManager.GetDocument(SelectedDocumentID);
                    PageLink.NavigateUrl = ResolveUrl(selectedDocument.MediaUrl);
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
            }
            catch (Exception e)
            {
                ExceptionManager.Publish(e);
            }
        }

        /// <summary>
        /// Sets the selected image's thumbnail to your thumbnail control
        /// </summary>
        private void GetExternalLink()
        {
            PageLink.NavigateUrl = this.ExternalLink;
            PageLink.Attributes.Add("target", "_blank");
        }

        #endregion

        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/UserControls/SoftwareDesign_Aptify/LayoutWidgets/ButtonWidget/ButtonWidget.ascx";
        #endregion
    }
}
