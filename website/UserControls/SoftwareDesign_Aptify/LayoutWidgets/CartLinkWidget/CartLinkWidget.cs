using System;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI;

namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.CartLinkWidget
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
    ///             .LoadOrAddWidget<CartLinkWidget>("CartLinkWidget")
    ///                 .SetTitle("CartLinkWidget")
    ///                 .SetDescription("CartLinkWidget")
    ///                 .LocalizeUsing<ModuleResourceClass>() //Optional
    ///                 .SetCssClass(WidgetCssClass) // You can use a css class to add an icon (Optional)
    ///             .Done()
    ///         .Done()
    ///     .Done();
    /// </remarks>
    /// <see cref="http://www.sitefinity.com/documentation/documentationarticles/user-guide/widgets"/>
    [Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesigner(typeof(SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.CartLinkWidget.Designer.CartLinkWidgetDesigner))]
    public class CartLinkWidget : SimpleView
    {
        #region Properties
        /// <summary>
        /// Gets or sets the message that will be displayed in the label.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// guid from designer for our selected page
        /// </summary>
        public Guid SelectedPageID { get; set; }

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
                    return CartLinkWidget.layoutTemplatePath;
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
            GetPageLinkInfo();
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
            }
            else
            {
                PageLink.NavigateUrl = "/cart";
            }

        }
        #endregion

        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/UserControls/SoftwareDesign_Aptify/LayoutWidgets/CartLinkWidget/CartLinkWidget.ascx";
        #endregion
    }
}
