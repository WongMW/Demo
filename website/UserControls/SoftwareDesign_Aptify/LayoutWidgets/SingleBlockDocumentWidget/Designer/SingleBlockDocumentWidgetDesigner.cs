using System;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web;

namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.SingleBlockDocumentWidget.Designer
{
    /// <summary>
    /// Represents a designer for the <typeparamref name="SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.SingleBlockDocumentWidget.SingleBlockDocumentWidget"/> widget
    /// </summary>
    public class SingleBlockDocumentWidgetDesigner : ControlDesignerBase
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
                    return SingleBlockDocumentWidgetDesigner.layoutTemplatePath;
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
        protected virtual Control Message
        {
            get
            {
                return this.Container.GetControl<Control>("Message", true);
            }
        }
        /// <summary>
        /// Gets the control that is bound to the ButtonLabel property
        /// </summary>
        protected virtual Control ButtonLabel
        {
            get
            {
                return this.Container.GetControl<Control>("ButtonLabel", true);
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

        #endregion

        #region Methods
        protected override void InitializeControls(Telerik.Sitefinity.Web.UI.GenericContainer container)
        {
            // Place your initialization logic here
            if (this.PropertyEditor != null)
            {
                var uiCulture = this.PropertyEditor.PropertyValuesCulture;
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

            descriptor.AddElementProperty("message", this.Message.ClientID);
            descriptor.AddElementProperty("buttonlabel", this.ButtonLabel.ClientID);
            descriptor.AddElementProperty("title", this.Title.ClientID);
            descriptor.AddElementProperty("style", this.Style.ClientID);
            descriptor.AddElementProperty("selectButtonSelectedImageID", this.SelectButtonSelectedImageID.ClientID);
            descriptor.AddElementProperty("deselectButtonSelectedImageID", this.DeselectButtonSelectedImageID.ClientID);
            descriptor.AddComponentProperty("selectorSelectedImageID", this.SelectorSelectedImageID.ClientID);
            descriptor.AddProperty("imageServiceUrl", this.imageServiceUrl);
            descriptor.AddElementProperty("selectButtonSelectedDocumentID", this.SelectButtonSelectedDocumentID.ClientID);
            descriptor.AddElementProperty("deselectButtonSelectedDocumentID", this.DeselectButtonSelectedDocumentID.ClientID);
            descriptor.AddComponentProperty("selectorSelectedDocumentID", this.SelectorSelectedDocumentID.ClientID);
            descriptor.AddProperty("documentServiceUrl", this.documentServiceUrl);

            return scriptDescriptors;
        }

        /// <summary>
        /// Gets a collection of ScriptReference objects that define script resources that the control requires.
        /// </summary>
        public override System.Collections.Generic.IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences()
        {
            var scripts = new List<ScriptReference>(base.GetScriptReferences());
            scripts.Add(new ScriptReference(SingleBlockDocumentWidgetDesigner.scriptReference));
            return scripts;
        }
        #endregion

        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/UserControls/SoftwareDesign_Aptify/LayoutWidgets/SingleBlockDocumentWidget/Designer/SingleBlockDocumentWidgetDesigner.ascx";
        public const string scriptReference = "~/UserControls/SoftwareDesign_Aptify/LayoutWidgets/SingleBlockDocumentWidget/Designer/SingleBlockDocumentWidgetDesigner.js";
        private string imageServiceUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Content/ImageService.svc/");
        private string documentServiceUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Content/DocumentService.svc/");

        #endregion
    }
}

