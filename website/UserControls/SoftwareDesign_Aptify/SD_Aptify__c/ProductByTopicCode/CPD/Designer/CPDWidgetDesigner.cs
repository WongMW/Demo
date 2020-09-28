using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Web.UI;

namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c.ProductByTopicCode.CPD.Designer
{
    /// <summary>
    /// Represents a designer for the <typeparamref name="SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c.ProductByTopicCode.CPD.CPDWidget"/> widget
    /// </summary>
    public class CPDWidgetDesigner : ControlDesignerBase
    {
        protected virtual string TopicCodeSettingName => "CPD.TopicCodes";
        #region Properties
        /// <inheritdoc />
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
                return string.IsNullOrEmpty(base.LayoutTemplatePath) ? layoutTemplatePath : base.LayoutTemplatePath;
            }
            set
            {
                base.LayoutTemplatePath = value;
            }
        }

        /// <summary>
        /// <summary>
        /// The ListBox for selecting TopicCodes.
        /// </summary>
        /// <value>The page selector control.</value>
        protected internal virtual ListBox TopicCodes => Container.GetControl<ListBox>("TopicCodes", false);
        protected internal virtual HiddenField HdnTopicCodes => Container.GetControl<HiddenField>("HdnTopicCodes", false);
        protected internal virtual RadTextBox ProductCount => Container.GetControl<RadTextBox>("ProductCount", false);

        #endregion




        protected override void InitializeControls(GenericContainer container)
        {
            FillTopicCodes();
        }

        private void FillTopicCodes()
        {
            TopicCodes.Items.Clear();

            var topicCodes = GetTopicCodes();

            foreach (var topicCode in topicCodes)
            {
                TopicCodes.Items.Add(topicCode);
            }
        }

        private IEnumerable<string> GetTopicCodes()
        {
            var topicCodes = ConfigurationManager.AppSettings[TopicCodeSettingName];

            return string.IsNullOrEmpty(topicCodes) ?
                Enumerable.Empty<string>() : 
                topicCodes.Split(',');
        }

        #region IScriptControl implementation

        public override IEnumerable<System.Web.UI.ScriptDescriptor> GetScriptDescriptors()
        {

            var scriptDescriptors = new List<ScriptDescriptor>(base.GetScriptDescriptors());
            var descriptor = (ScriptControlDescriptor)scriptDescriptors.Last();

            descriptor.AddElementProperty("topicCodes", this.TopicCodes.ClientID);
            descriptor.AddElementProperty("productCount", this.ProductCount.ClientID);
            descriptor.AddElementProperty("hdnTopicCodes", this.HdnTopicCodes.ClientID);

            return scriptDescriptors;
        }

        public override System.Collections.Generic.IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences()
        {
            var scripts = new List<ScriptReference>(base.GetScriptReferences())
            {
                new ScriptReference(ScriptReference)
            };
            return scripts;
        }

        #endregion


        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/UserControls/SoftwareDesign_Aptify/SD_Aptify__c/ProductByTopicCode/CPD/Designer/CPDWidgetDesigner.ascx";
        protected virtual string ScriptReference => "~/UserControls/SoftwareDesign_Aptify/SD_Aptify__c/ProductByTopicCode/CPD/Designer/CPDWidgetDesigner.js";       
        #endregion
    }
}