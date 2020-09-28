using System;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Pages;

namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.TestimonialsWidget.Designer
{
    /// <summary>
    /// Represents a designer for the <typeparamref name="SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.TestimonialsWidget.TestimonialsWidget"/> widget
    /// </summary>
    public class TestimonialsWidgetDesigner : ControlDesignerBase
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
                    return TestimonialsWidgetDesigner.layoutTemplatePath;
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
        protected virtual HtmlGenericControl SelectedItemsList
        {
            get
            {
                return this.Container.GetControl<HtmlGenericControl>("selectedItemsList", true);
            }
        }

        /// <summary>
        /// The LinkButton for selecting Test
        /// </summary>
        protected internal virtual HyperLink SelectButton
        {
            get
            {
                return this.Container.GetControl<HyperLink>("selectButton", true);
            }
        }

        /// <summary>
        /// The Flat Selector for Test
        /// </summary>
        protected internal virtual FlatSelector ItemSelector
        {
            get
            {
                return this.Container.GetControl<FlatSelector>("ItemSelector", false);
            }
        }

        /// <summary>
        /// The LinkButton for "Done"
        /// </summary>
        protected virtual LinkButton DoneButton
        {
            get
            {
                return this.Container.GetControl<LinkButton>("lnkDone", true);
            }
        }

        /// <summary>
        /// The LinkButton for "Cancel"
        /// </summary>
        protected virtual LinkButton CancelButton
        {
            get
            {
                return this.Container.GetControl<LinkButton>("lnkCancel", true);
            }
        }

        /// <summary>
        /// The button area control
        /// </summary>
        protected virtual Control ButtonArea
        {
            get
            {
                return this.Container.GetControl<Control>("buttonAreaPanel", false);
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
                if (this.ItemSelector != null)
                {
                    this.ItemSelector.UICulture = uiCulture;
                    this.ItemSelector.ConstantFilter = "Visible=true";
                }
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

            descriptor.AddElementProperty("selectButton", this.SelectButton.ClientID);
            descriptor.AddComponentProperty("ItemSelector", this.ItemSelector.ClientID);
            descriptor.AddElementProperty("lnkDone", this.DoneButton.ClientID);
            descriptor.AddElementProperty("lnkCancel", this.CancelButton.ClientID);
            descriptor.AddElementProperty("selectedItemsList", this.SelectedItemsList.ClientID);

            return scriptDescriptors;
        }

        /// <summary>
        /// Gets a collection of ScriptReference objects that define script resources that the control requires.
        /// </summary>
        public override System.Collections.Generic.IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences()
        {
            var scripts = new List<ScriptReference>(base.GetScriptReferences());
            scripts.Add(new ScriptReference(TestimonialsWidgetDesigner.scriptReference));
            return scripts;
        }
        
        /// <summary>
        /// Gets the required by the control, core library scripts predefined in the <see cref="ScriptRef"/> enum.
        /// </summary>
        protected override ScriptRef GetRequiredCoreScripts()
        {
            return ScriptRef.JQuery | ScriptRef.JQueryUI | ScriptRef.KendoAll;
        }
        #endregion

        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/UserControls/SoftwareDesign_Aptify/LayoutWidgets/TestimonialsWidget/Designer/TestimonialsWidgetDesigner.ascx";
        public const string scriptReference = "~/UserControls/SoftwareDesign_Aptify/LayoutWidgets/TestimonialsWidget/Designer/TestimonialsWidgetDesigner.js";
        #endregion
    }
}
 
