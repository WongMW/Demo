using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Web.UI;

namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.SD_Aptify__c.ProductByTopicCode.BookList.Designer
{
    public partial class BookListWidgetDesigner : ControlDesignerBase
    {

        protected virtual string TopicCodeSettingName => "Books.TopicCodes";

        protected override string LayoutTemplateName => string.Empty;

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

        protected internal virtual ListBox TopicCodes => Container.GetControl<ListBox>("TopicCodes", false);
        protected internal virtual HiddenField HdnTopicCodes => Container.GetControl<HiddenField>("HdnTopicCodes", false);
        protected internal virtual RadTextBox ProductCount => Container.GetControl<RadTextBox>("ProductCount", false);


        protected override void InitializeControls(GenericContainer container)
        {
            FillTopicCodes();
        }

        private void FillTopicCodes()
        {
            TopicCodes.ClearSelection();
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

        public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {

            var scriptDescriptors = new List<ScriptDescriptor>(base.GetScriptDescriptors());
            var descriptor = (ScriptControlDescriptor)scriptDescriptors.Last();

            descriptor.AddElementProperty("topicCodes", this.TopicCodes.ClientID);
            descriptor.AddElementProperty("productCount", this.ProductCount.ClientID);
            descriptor.AddElementProperty("hdnTopicCodes", this.HdnTopicCodes.ClientID);

            return scriptDescriptors;
        }

        public override IEnumerable<ScriptReference> GetScriptReferences()
        {
            var scripts = new List<ScriptReference>(base.GetScriptReferences())
            {
                new ScriptReference(ScriptReference)
            };
            return scripts;
        }

        #endregion
        
        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/UserControls/SoftwareDesign_Aptify/SD_Aptify__c/ProductByTopicCode/BookList/Designer/BookListWidgetDesigner.ascx";
        protected virtual string ScriptReference => "~/UserControls/SoftwareDesign_Aptify/SD_Aptify__c/ProductByTopicCode/BookList/Designer/BookListWidgetDesigner.js";       
        #endregion
    }
}