using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web;
using System.Web.UI.HtmlControls;

namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.ButtonWidget.Designer
{
    /// <summary>
    /// Represents a designer for the <typeparamref name="SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.ButtonWidget.ButtonWidget"/> widget
    /// </summary>
    public class ButtonWidgetDesigner : ControlDesignerBase
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
                    return ButtonWidgetDesigner.layoutTemplatePath;
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
        /// Gets the control that is bound to the Title property
        /// </summary>
        protected virtual Control Title
        {
            get
            {
                return this.Container.GetControl<Control>("Title", true);
            }
        }
        /// <summary>
        /// Gets the control that is bound to the Message property
        /// </summary>
        protected virtual Control ExternalLink
        {
            get
            {
                return this.Container.GetControl<Control>("ExternalLink", true);
            }
        }
        /// <summary>
        /// Gets the control that is bound to the LinkType property
        /// </summary>
        protected virtual Control LinkType
        {
            get
            {
                return this.Container.GetControl<Control>("LinkType", true);
            }
        }

        /// <summary>
        /// Gets the control that is bound to the Message property
        /// </summary>
        protected virtual Control Style
        {
            get
            {
                return this.Container.GetControl<Control>("Style", true);
            }
        }

        /// <summary>
        /// The LinkButton for selecting SelectedDocumentID.
        /// </summary>
        /// <value>The page selector control.</value>
        protected internal virtual LinkButton SelectButtonSelectedDocumentID
        {
            get
            {
                return this.Container.GetControl<LinkButton>("selectButtonSelectedDocumentID", false);
            }
        }

        /// <summary>
        /// The LinkButton for deselecting SelectedDocumentID.
        /// </summary>
        /// <value>The page selector control.</value>
        protected internal virtual LinkButton DeselectButtonSelectedDocumentID
        {
            get
            {
                return this.Container.GetControl<LinkButton>("deselectButtonSelectedDocumentID", false);
            }
        }

        /// <summary>
        /// Gets the RadEditor Manager dialog for inserting document, document or video for the SelectedDocumentID property.
        /// </summary>
        /// <value>The RadEditor Manager dialog for inserting document, document or video.</value>
        protected EditorContentManagerDialog SelectorSelectedDocumentID
        {
            get
            {
                return this.Container.GetControl<EditorContentManagerDialog>("selectorSelectedDocumentID", false);
            }
        }

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

            descriptor.AddElementProperty("title", this.Title.ClientID);
            descriptor.AddElementProperty("style", this.Style.ClientID);
            descriptor.AddElementProperty("externalLink", this.ExternalLink.ClientID);
            descriptor.AddElementProperty("linkType", this.LinkType.ClientID);
            descriptor.AddElementProperty("selectButtonSelectedDocumentID", this.SelectButtonSelectedDocumentID.ClientID);
            descriptor.AddElementProperty("deselectButtonSelectedDocumentID", this.DeselectButtonSelectedDocumentID.ClientID);
            descriptor.AddComponentProperty("selectorSelectedDocumentID", this.SelectorSelectedDocumentID.ClientID);
            descriptor.AddComponentProperty("pageSelectorSelectedPageID", this.PageSelectorSelectedPageID.ClientID);
            descriptor.AddElementProperty("selectorTagSelectedPageID", this.SelectorTagSelectedPageID.ClientID);
            descriptor.AddProperty("documentServiceUrl", this.documentServiceUrl);

            return scriptDescriptors;
        }

        /// <summary>
        /// Gets a collection of ScriptReference objects that define script resources that the control requires.
        /// </summary>
        public override System.Collections.Generic.IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences()
        {
            var scripts = new List<ScriptReference>(base.GetScriptReferences());
            scripts.Add(new ScriptReference(ButtonWidgetDesigner.scriptReference));
            return scripts;
        }
        #endregion

        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/UserControls/SoftwareDesign_Aptify/LayoutWidgets/ButtonWidget/Designer/ButtonWidgetDesigner.ascx";
        public const string scriptReference = "~/UserControls/SoftwareDesign_Aptify/LayoutWidgets/ButtonWidget/Designer/ButtonWidgetDesigner.js";
        private string documentServiceUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Content/DocumentService.svc/");

        #endregion
    }
}

