using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.GDPR.Designer
{
    public class GDPRModalDesigner: ControlDesignerBase
    {
        #region Properties
        /// <summary>
        /// Obsolete. Use LayoutTemplatePath instead.
        /// </summary>
        protected override string LayoutTemplateName => string.Empty;

        /// <inheritdoc />
        /// <summary>
        /// Gets the layout template's relative or virtual path.
        /// </summary>
        public override string LayoutTemplatePath
        {
            get
            {
                return string.IsNullOrEmpty(base.LayoutTemplatePath) ? 
                    GDPRModalDesigner.layoutTemplatePath : 
                    base.LayoutTemplatePath;
            }
            set
            {
                base.LayoutTemplatePath = value;
            }
        }
        #endregion

        #region Control references
        protected virtual Control MonthsToHideAfterApproval
        {
            get { return Container.GetControl<Control>("MonthsToHideAfterApproval", true); }
        }

        #endregion



        protected override void InitializeControls(GenericContainer container)
        {
            
        }

        #region IScriptControl implementation
        /// <inheritdoc />
        /// <summary>
        /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
        /// </summary>
        public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            var scriptDescriptors = new List<ScriptDescriptor>(base.GetScriptDescriptors());
            var descriptor = (ScriptControlDescriptor)scriptDescriptors.Last();

            descriptor.AddElementProperty("monthsToHideAfterApproval", MonthsToHideAfterApproval.ClientID);

            return scriptDescriptors;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets a collection of ScriptReference objects that define script resources that the control requires.
        /// </summary>
        public override IEnumerable<ScriptReference> GetScriptReferences()
        {
            var scripts = new List<ScriptReference>(base.GetScriptReferences())
            {
                new ScriptReference(GDPRModalDesigner.scriptReference)
            };
            return scripts;
        }
        #endregion

        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/UserControls/CAI_Custom_Controls/GDPR/Designer/GDPRModalDesigner.ascx";
        public const string scriptReference = "~/UserControls/CAI_Custom_Controls/GDPR/Designer/GDPRModalDesigner.js";
        #endregion
    }
}