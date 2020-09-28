using System;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;

namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.CartLinkWidget.Designer
{
    /// <summary>
    /// Represents a designer for the <typeparamref name="SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.CartLinkWidget.CartLinkWidget"/> widget
    /// </summary>
    public class CartLinkWidgetDesigner : ControlDesignerBase
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
                    return CartLinkWidgetDesigner.layoutTemplatePath;
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
        
        //// <summary>
        /// Gets the page selector control.
        /// </summary>
        /// <value>The page selector control.</value>
        protected internal virtual PagesSelector PageSelectorSelectedPageID
        {
            get
            {
                return this.Container.GetControl<PagesSelector>("pageSelectorSelectedPageID", true);
            }
        }

        /// <summary>
        /// Gets the selector tag.
        /// </summary>
        /// <value>The selector tag.</value>
        public HtmlGenericControl SelectorTagSelectedPageID
        {
            get
            {
                return this.Container.GetControl<HtmlGenericControl>("selectorTagSelectedPageID", true);
            }
        }


        #endregion

        #region Methods
        protected override void InitializeControls(Telerik.Sitefinity.Web.UI.GenericContainer container)
        {
            // Place your initialization logic here
            if (this.PropertyEditor != null)
            {
                var uiCulture = this.PropertyEditor.PropertyValuesCulture;
                this.PageSelectorSelectedPageID.UICulture = uiCulture;
            }
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

            descriptor.AddComponentProperty("pageSelectorSelectedPageID", this.PageSelectorSelectedPageID.ClientID);
            descriptor.AddElementProperty("selectorTagSelectedPageID", this.SelectorTagSelectedPageID.ClientID);

            return scriptDescriptors;
        }

        /// <summary>
        /// Gets a collection of ScriptReference objects that define script resources that the control requires.
        /// </summary>
        public override System.Collections.Generic.IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences()
        {
            var scripts = new List<ScriptReference>(base.GetScriptReferences());
            scripts.Add(new ScriptReference(CartLinkWidgetDesigner.scriptReference));
            return scripts;
        }
        #endregion

        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/UserControls/SoftwareDesign_Aptify/LayoutWidgets/CartLinkWidget/Designer/CartLinkWidgetDesigner.ascx";
        public const string scriptReference = "~/UserControls/SoftwareDesign_Aptify/LayoutWidgets/CartLinkWidget/Designer/CartLinkWidgetDesigner.js";
        #endregion
    }
}
 
