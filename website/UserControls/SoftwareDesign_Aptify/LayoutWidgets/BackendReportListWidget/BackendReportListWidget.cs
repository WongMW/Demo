using System;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Lifecycle;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Data;

namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.BackendReportListWidget
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
    ///             .LoadOrAddWidget<BackendReportListWidget>("BackendReportListWidget")
    ///                 .SetTitle("BackendReportListWidget")
    ///                 .SetDescription("BackendReportListWidget")
    ///                 .LocalizeUsing<ModuleResourceClass>() //Optional
    ///                 .SetCssClass(WidgetCssClass) // You can use a css class to add an icon (Optional)
    ///             .Done()
    ///         .Done()
    ///     .Done();
    /// </remarks>
    /// <see cref="http://www.sitefinity.com/documentation/documentationarticles/user-guide/widgets"/>
    [Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesigner(typeof(SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.BackendReportListWidget.Designer.BackendReportListWidgetDesigner))]
    public class BackendReportListWidget : SimpleView
    {
        #region Properties
        /// <summary>
        /// Gets or sets the message that will be displayed in the label.
        /// </summary>
        public string Message { get; set; }

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
                    return BackendReportListWidget.layoutTemplatePath;
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
        /// Reference to the Label control that shows the Message.
        /// </summary>
        protected virtual HtmlGenericControl MessageLabel
        {
            get
            {
                return this.Container.GetControl<HtmlGenericControl>("MessageLabel", true);
            }
        }
        /// <summary>
        /// Reference to the Label control that shows the Message.
        /// </summary>
        protected virtual GridView ReportGrid
        {
            get
            {
                return this.Container.GetControl<GridView>("ReportGrid", true);
            }
        }
        /// <summary>
        /// Reference to the Label control that shows the Message.
        /// </summary>
        protected virtual GridView YesGridView
        {
            get
            {
                return this.Container.GetControl<GridView>("YesGridView", true);
            }
        }
        /// <summary>
        /// Reference to the Label control that shows the Message.
        /// </summary>
        protected virtual GridView NoGridView
        {
            get
            {
                return this.Container.GetControl<GridView>("NoGridView", true);
            }
        }
        /// <summary>
        /// Reference to the Label control that shows the Message.
        /// </summary>
        protected virtual LinkButton pageLink
        {
            get
            {
                return this.Container.GetControl<LinkButton>("pageLink", false);
            }
        }

        /// <summary>
        /// Reference to the Label control that shows the Message.
        /// </summary>
        protected virtual TextBox SearchText
        {
            get
            {
                return this.Container.GetControl<TextBox>("searchText", false);
            }
        }
        /// <summary>
        /// Reference to the Label control that shows the Message.
        /// </summary>
        protected virtual Button SearchBtn
        {
            get
            {
                return this.Container.GetControl<Button>("searchBtn", false);
            }
        }
        /// <summary>
        /// Reference to the Label control that shows the Message.
        /// </summary>
        protected virtual Button viewNegativeVotes
        {
            get
            {
                return this.Container.GetControl<Button>("viewNegativeVotes", false);
            }
        }
        /// <summary>
        /// Reference to the Label control that shows the Message.
        /// </summary>
        protected virtual Button viewPositiveVotes
        {
            get
            {
                return this.Container.GetControl<Button>("viewPositiveVotes", false);
            }
        }
        /// <summary>
        /// Reference to the Label control that shows the Message.
        /// </summary>
        protected virtual Button viewAllVotes
        {
            get
            {
                return this.Container.GetControl<Button>("viewAllVotes", false);
            }
        }
        /// <summary>
        /// Reference to the Label control that shows the Message.
        /// </summary>
        protected virtual Button viewTopNegativeVotes
        {
            get
            {
                return this.Container.GetControl<Button>("viewTopNegativeVotes", false);
            }
        }
        /// <summary>
        /// Reference to the Label control that shows the Message.
        /// </summary>
        protected virtual Button viewTopPositiveVotes
        {
            get
            {
                return this.Container.GetControl<Button>("viewTopPositiveVotes", false);
            }
        }

        /// <summary>
        /// Reference to the Label control that shows the Message.
        /// </summary>
        protected virtual HiddenField StartDate
        {
            get
            {
                return this.Container.GetControl<HiddenField>("startDate", false);
            }
        }

        /// <summary>
        /// Reference to the Label control that shows the Message.
        /// </summary>
        protected virtual HiddenField EndDate
        {
            get
            {
                return this.Container.GetControl<HiddenField>("endDate", false);
            }
        }


        DynamicModuleManager dynamicModuleManager;
        Type reportType;
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
            HtmlGenericControl messageLabel = this.MessageLabel;
            messageLabel.InnerText = "Reports by Page";

            var providerName = String.Empty;
            dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
            reportType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Reports.Report");

            Button searchButton = this.SearchBtn;
            searchButton.Click += SearchButtonOnClick;

            Button viewNegativeVotes = this.viewNegativeVotes;
            viewNegativeVotes.Click += NegativeButtonOnClick;

            Button viewPositiveVotes = this.viewPositiveVotes;
            viewPositiveVotes.Click += PositiveButtonOnClick;

            Button viewAllVotes = this.viewAllVotes;
            viewAllVotes.Click += AllButtonOnClick;

            Button viewTopNegativeVotes = this.viewTopNegativeVotes;
            viewTopNegativeVotes.Click += TopNegativeButtonOnClick;

            Button viewTopPositiveVotes = this.viewTopPositiveVotes;
            viewTopPositiveVotes.Click += TopPositiveButtonOnClick;

            YesGridView.Visible = false;
            NoGridView.Visible = false;
            this.Context.User.IsInRole("Administrators");

            if (ViewState["reportView"] == null)
            {
                ViewState["reportView"] = 0;
            }

            LoadReports();
        }

        protected void LoadReports()
        {
            viewNegativeVotes.Visible = true;
            viewPositiveVotes.Visible = true;
            viewAllVotes.Visible = true;
            viewTopNegativeVotes.Visible = true;
            viewTopPositiveVotes.Visible = true;

            if (string.IsNullOrEmpty(StartDate.Value))
            {
                StartDate.Value = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrEmpty(EndDate.Value))
            {
                EndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            }

            List<DynamicContent> reports = null;

            if (SearchText.Text != "")
            {
                reports = SearchReports();
            }
            else
            {
                switch ((int)ViewState["reportView"])
                {
                    case 0:
                        reports = LatestReports(false);
                        viewNegativeVotes.Visible = false;
                        break;
                    case 1:
                        reports = LatestReports(true);
                        viewPositiveVotes.Visible = false;
                        break;
                    case 2:
                        reports = GetAllReports();
                        viewAllVotes.Visible = false;
                        break;
                    case 3:
                        reports = GetTopOfReoprts(true);
                        viewTopPositiveVotes.Visible = false;
                        break;
                    case 4:
                        reports = GetTopOfReoprts(false);
                        viewTopNegativeVotes.Visible = false;
                        break;
                    default:
                        break;
                }
            }

            if (reports != null)
            {
                var filteredReports = FilterDuplicates(reports);
                this.ReportGrid.DataSource = filteredReports;
                this.ReportGrid.DataBind();
            }

            foreach (GridViewRow row in ReportGrid.Rows)
            {
                LinkButton linkButton = (LinkButton)row.FindControl("pageLink");
                linkButton.Click += LinkButton_Click;
            }
        }

        private void SearchButtonOnClick(object sender, EventArgs eventArgs)
        {
            ViewState["reportView"] = 2;
            LoadReports();
        }

        private void PositiveButtonOnClick(object sender, EventArgs eventArgs)
        {
            ViewState["reportView"] = 1;
            LoadReports();
        }

        private void NegativeButtonOnClick(object sender, EventArgs eventArgs)
        {
            ViewState["reportView"] = 0;
            LoadReports();
        }

        private void AllButtonOnClick(object sender, EventArgs eventArgs)
        {
            ViewState["reportView"] = 2;
            LoadReports();
        }

        private void TopPositiveButtonOnClick(object sender, EventArgs eventArgs)
        {
            ViewState["reportView"] = 3;
            LoadReports();
        }

        private void TopNegativeButtonOnClick(object sender, EventArgs eventArgs)
        {
            ViewState["reportView"] = 4;
            LoadReports();
        }

        private void LinkButton_Click(object sender, EventArgs eventArgs)
        {
            LinkButton linkButton = (LinkButton)sender;
            var yesReports = RetrieveCollectionOfReports(linkButton.Text, true);
            if (yesReports != null)
            {
                this.YesGridView.DataSource = yesReports;
                this.YesGridView.DataBind();
                this.YesGridView.Visible = true;
            }

            var noReports = RetrieveCollectionOfReports(linkButton.Text, false);
            if (noReports != null)
            {
                this.NoGridView.DataSource = noReports;
                this.NoGridView.DataBind();
                this.NoGridView.Visible = true;
            }
        }

        public List<DynamicContent> FilterDuplicates(List<DynamicContent> myCollection)
        {
            List<DynamicContent> contentList = new List<DynamicContent>();

            for (var i = 0; i < myCollection.Count; i++)
            {
                String pageTitle = myCollection[i].GetValue("PageTitle").ToString();
                int index = contentList.FindIndex(f => f.GetValue("PageTitle").ToString() == pageTitle);
                if (index == -1)
                {
                    contentList.Add(myCollection[i]);
                }
            }
            // At this point myCollection contains the items from type reportType
            return contentList;
        }

        public List<DynamicContent> GetAllReports()
        {
            var startDate = DateTime.Parse(StartDate.Value);
            var endDate = DateTime.Parse(EndDate.Value);
            return dynamicModuleManager.GetDataItems(reportType).Where(r => (r.Status == ContentLifecycleStatus.Master) && (r.DateCreated >= startDate) && (r.DateCreated <= endDate)).OrderBy("PageTitle").ToList();
        }

        public List<DynamicContent> LatestReports(bool isHelpful)
        {
            var startDate = DateTime.Parse(StartDate.Value);
            var endDate = DateTime.Parse(EndDate.Value);

            String query = "Helpful =  " + isHelpful + " && Status=\"Master\"";
            return dynamicModuleManager.GetDataItems(reportType).Where(query).Where(r => (r.DateCreated >= startDate)&&(r.DateCreated<= endDate)).ToList();
        }

        public List<DynamicContent> GetTopOfReoprts(bool isHelpful)
        {
            var startDate = DateTime.Parse(StartDate.Value);
            var endDate = DateTime.Parse(EndDate.Value);

            String query = "Helpful =  " + isHelpful + " && Status=\"Master\"";
            var content = dynamicModuleManager.GetDataItems(reportType)
                .Where(query).Where(r => (r.DateCreated >= startDate) && (r.DateCreated <= endDate))
                .ToList();

            var contentCountDictionary = new Dictionary<string, DynamicContentCount>();

            foreach (var item in content)
            {
                string url = item.GetValue("PageURL").ToString();

                if (contentCountDictionary.Keys.Contains(url))
                {
                    contentCountDictionary[url].Count++;
                }
                else
                {
                    contentCountDictionary.Add(url, new DynamicContentCount()
                    {
                        Content = item,
                        Count = 1
                    });
                }
            }

            return contentCountDictionary.OrderByDescending(cd => cd.Value.Count)
                    .Take(10)
                    .Select(cd => cd.Value.Content)
                    .ToList<DynamicContent>();
        }

        public List<DynamicContent> SearchReports()
        {
            return dynamicModuleManager.GetDataItems(reportType).Where(r => r.Status == ContentLifecycleStatus.Master && r.GetValue<string>("PageTitle").ToString().Contains(SearchText.Text)).OrderBy("PageTitle").ToList();
        }

        public List<DynamicContent> RetrieveCollectionOfReports(String pageTitle, bool isHelpful)
        {
            // This is how we get the collection of Report items
            String query = "Helpful =  " + isHelpful + " && Status=\"Master\" && PageTitle=\"" + pageTitle + "\"";
            var myCollection = dynamicModuleManager.GetDataItems(reportType).Where(query).ToList();

            return myCollection;
        }
        #endregion

        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/UserControls/SoftwareDesign_Aptify/LayoutWidgets/BackendReportListWidget/BackendReportListWidget.ascx";
        #endregion

        private class DynamicContentCount
        {
            public DynamicContent Content { get; set; }
            public int Count { get; set; }
        }
    }
}
