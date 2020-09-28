using System;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using System.Collections.Generic;

namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.ApplicationWidget.Designer
{
    /// <summary>
    /// Represents a designer for the <typeparamref name="SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.ApplicationWidget.ApplicationWidget"/> widget
    /// </summary>
    public class ApplicationWidgetDesigner : ControlDesignerBase
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
                    return ApplicationWidgetDesigner.layoutTemplatePath;
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
        /// Gets the control that is bound to the AppOneTitle property
        /// </summary>
        protected virtual Control AppOneTitle
        {
            get
            {
                return this.Container.GetControl<Control>("AppOneTitle", true);
            }
        }
        /// <summary>
        /// Gets the control that is bound to the AppOneDate property
        /// </summary>
        protected virtual Control AppOneDate
        {
            get
            {
                return this.Container.GetControl<Control>("AppOneDate", true);
            }
        }

        protected virtual Control AppTwoTitle
        {
            get
            {
                return this.Container.GetControl<Control>("AppTwoTitle", true);
            }
        }
        /// <summary>
        /// Gets the control that is bound to the AppTwoDate property
        /// </summary>
        protected virtual Control AppTwoDate
        {
            get
            {
                return this.Container.GetControl<Control>("AppTwoDate", true);
            }
        }

        protected virtual Control AppThreeTitle
        {
            get
            {
                return this.Container.GetControl<Control>("AppThreeTitle", true);
            }
        }
        /// <summary>
        /// Gets the control that is bound to the AppThreeDate property
        /// </summary>
        protected virtual Control AppThreeDate
        {
            get
            {
                return this.Container.GetControl<Control>("AppThreeDate", true);
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
        public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            var scriptDescriptors = new List<ScriptDescriptor>(base.GetScriptDescriptors());
            var descriptor = (ScriptControlDescriptor)scriptDescriptors.Last();

            descriptor.AddElementProperty("appOneTitle", this.AppOneTitle.ClientID);
            descriptor.AddElementProperty("appOneDate", this.AppOneDate.ClientID);
            descriptor.AddElementProperty("appTwoTitle", this.AppTwoTitle.ClientID);
            descriptor.AddElementProperty("appTwoDate", this.AppTwoDate.ClientID);
            descriptor.AddElementProperty("appThreeTitle", this.AppThreeTitle.ClientID);
            descriptor.AddElementProperty("appThreeDate", this.AppThreeDate.ClientID);

            return scriptDescriptors;
        }

        /// <summary>
        /// Gets a collection of ScriptReference objects that define script resources that the control requires.
        /// </summary>
        public override System.Collections.Generic.IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences()
        {
            var scripts = new List<ScriptReference>(base.GetScriptReferences());
            scripts.Add(new ScriptReference(ApplicationWidgetDesigner.scriptReference));
            return scripts;
        }
        #endregion

        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/UserControls/SoftwareDesign_Aptify/LayoutWidgets/ApplicationWidget/Designer/ApplicationWidgetDesigner.ascx";
        public const string scriptReference = "~/UserControls/SoftwareDesign_Aptify/LayoutWidgets/ApplicationWidget/Designer/ApplicationWidgetDesigner.js";
        #endregion
    }
}
 
