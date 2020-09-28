using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web;

namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.BannerWidget.Designer
{
    /// <summary>
    /// Represents a designer for the <typeparamref name="SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.BannerWidget.BannerWidget"/> widget
    /// </summary>
    public class BannerWidgetDesigner : ControlDesignerBase
    {
        #region Properties
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
                    return BannerWidgetDesigner.layoutTemplatePath;
                return base.LayoutTemplatePath;
            }
            set
            {
                base.LayoutTemplatePath = value;
            }
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }
        #endregion

        #region Control references
        /// <summary>
        /// Gets the control that is bound to the Message property
        /// </summary>
        protected virtual Control Message
        {
            get
            {
                return this.Container.GetControl<Control>("Message", true);
            }
        }

        /// <summary>
        /// The LinkButton for selecting SelectedImageID.
        /// </summary>
        /// <value>The page selector control.</value>
        protected internal virtual LinkButton SelectButtonSelectedImageID
        {
            get
            {
                return this.Container.GetControl<LinkButton>("selectButtonSelectedImageID", false);
            }
        }

        /// <summary>
        /// The LinkButton for deselecting SelectedImageID.
        /// </summary>
        /// <value>The page selector control.</value>
        protected internal virtual LinkButton DeselectButtonSelectedImageID
        {
            get
            {
                return this.Container.GetControl<LinkButton>("deselectButtonSelectedImageID", false);
            }
        }

        /// <summary>
        /// Gets the RadEditor Manager dialog for inserting image, document or video for the SelectedImageID property.
        /// </summary>
        /// <value>The RadEditor Manager dialog for inserting image, document or video.</value>
        protected EditorContentManagerDialog SelectorSelectedImageID
        {
            get
            {
                return this.Container.GetControl<EditorContentManagerDialog>("selectorSelectedImageID", false);
            }
        }

        #endregion

        #region Methods
        protected override void InitializeControls(Telerik.Sitefinity.Web.UI.GenericContainer container)
        {
            // Place your initialization logic here
        }
        #endregion

        #region IScriptControl implementation
        /// <summary>
        /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
        /// </summary>
        public override System.Collections.Generic.IEnumerable<System.Web.UI.ScriptDescriptor> GetScriptDescriptors()
        {
            var scriptDescriptors = new List<ScriptDescriptor>(base.GetScriptDescriptors());
            var descriptor = (ScriptControlDescriptor)scriptDescriptors.Last();

            descriptor.AddElementProperty("message", this.Message.ClientID);
            descriptor.AddElementProperty("selectButtonSelectedImageID", this.SelectButtonSelectedImageID.ClientID);
            descriptor.AddElementProperty("deselectButtonSelectedImageID", this.DeselectButtonSelectedImageID.ClientID);
            descriptor.AddComponentProperty("selectorSelectedImageID", this.SelectorSelectedImageID.ClientID);
            descriptor.AddProperty("imageServiceUrl", this.imageServiceUrl);

            return scriptDescriptors;
        }

        /// <summary>
        /// Gets a collection of ScriptReference objects that define script resources that the control requires.
        /// </summary>
        public override System.Collections.Generic.IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences()
        {
            var scripts = new List<ScriptReference>(base.GetScriptReferences());
            scripts.Add(new ScriptReference(BannerWidgetDesigner.scriptReference));
            return scripts;
        }
        #endregion

        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/UserControls/SoftwareDesign_Aptify/LayoutWidgets/BannerWidget/Designer/BannerWidgetDesigner.ascx";
        public const string scriptReference = "~/UserControls/SoftwareDesign_Aptify/LayoutWidgets/BannerWidget/Designer/BannerWidgetDesigner.js";
        private string imageServiceUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Content/ImageService.svc/");
        #endregion
    }
}
 
