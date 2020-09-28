using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.News.Model;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Model;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Web.UI;
using Telerik.Web.UI;
using Telerik.Sitefinity;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Modules.Pages;

namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.LatestNewsWidget
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
    ///             .LoadOrAddWidget<LatestNewsWidget>("LatestNewsWidget")
    ///                 .SetTitle("LatestNewsWidget")
    ///                 .SetDescription("LatestNewsWidget")
    ///                 .LocalizeUsing<ModuleResourceClass>() //Optional
    ///                 .SetCssClass(WidgetCssClass) // You can use a css class to add an icon (Optional)
    ///             .Done()
    ///         .Done()
    ///     .Done();
    /// </remarks>
    /// <see cref="http://www.sitefinity.com/documentation/documentationarticles/user-guide/widgets"/>
    [Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesigner(typeof(SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.LatestNewsWidget.Designer.LatestNewsWidgetDesigner))]
    public class LatestNewsWidget : SimpleView
    {
        #region Properties
        /// <summary>
        /// Gets or sets the limit of articles.
        /// </summary>
        public string Limit { get; set; }
        /// <summary>
        /// guid from designer for our selected page
        /// </summary>
        public Guid SelectedPageID { get; set; }
        /// <summary>
        /// Gets the text box control.
        /// </summary>
        /// <value>The text box control.</value>
        internal protected virtual Repeater NewsList
        {
            get
            {
                return this.Container.GetControl<Repeater>("NewsList", true);
            }
        }


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
                    return LatestNewsWidget.layoutTemplatePath;
                return base.LayoutTemplatePath;
            }
            set
            {
                base.LayoutTemplatePath = value;
            }
        }

        // <summary>
        /// Gets or sets the selected categories.
        /// </summary>
        /// <value>
        /// The selected categories.
        /// </value>
        public Guid[] SelectedCategories
        {
            get
            {
                if (selectedCategories == null) selectedCategories = new Guid[] { };
                return selectedCategories;
            }
            set { selectedCategories = value; }
        }

        private Guid[] selectedCategories;

        /// <summary>
        /// Intermediary property for passing categories to and from the designer
        /// </summary>
        /// <value>
        /// The category value as a comma-delimited string.
        /// </value>
        public string CategoryValue
        {
            get { return string.Join(",", SelectedCategories); }
            set
            {
                var list = new List<Guid>();
                if (value != null)
                {
                    var guids = value.Split(',');
                    foreach (var guid in guids)
                    {
                        Guid newGuid;
                        if (Guid.TryParse(guid, out newGuid))
                            list.Add(newGuid);
                    }
                }
                SelectedCategories = list.ToArray();
            }
        }

        public int? ImageSize { get; set; }
        #endregion

        #region Control References

        protected TaxonomyManager taxonomyMgr = TaxonomyManager.GetManager();
        protected NewsManager newsMgr = NewsManager.GetManager();
        protected List<Guid> usedCategoryIds = new List<Guid>();

        /// <summary>
        /// Reference to the HyperLink control that shows the page url.
        /// </summary>
        protected virtual HyperLink PageLink1
        {
            get
            {
                return this.Container.GetControl<HyperLink>("PageLink1", true);
            }
        }

        /// <summary>
        /// Reference to the HyperLink control that shows the page url.
        /// </summary>
        protected virtual HyperLink PageLink2
        {
            get
            {
                return this.Container.GetControl<HyperLink>("PageLink2", true);
            }
        }

        protected virtual Image HeaderImage
        {
            get
            {
                return this.Container.GetControl<Image>("HeaderImage", true);
            }
        }

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
            this.NewsList.DataSource = GetNewsListDataSource();
            this.NewsList.DataBind();

            GetPageLinkInfo();
        }

        /// <summary>
        /// Sets the url and title for your page link control
        /// </summary>
        public void GetPageLinkInfo()
        {
            var mgr = PageManager.GetManager();
            if (!Guid.Empty.Equals(SelectedPageID))
            {
                var pageNode = mgr.GetPageNode(SelectedPageID);
                PageLink1.NavigateUrl = ResolveUrl(pageNode.GetFullUrl());
                PageLink2.NavigateUrl = ResolveUrl(pageNode.GetFullUrl());
            }
            else
            {
                PageLink1.Style.Add("display", "none");
                PageLink2.Style.Add("display", "none");
            }

        }

        private List<CustomNewsItem> GetNewsListDataSource()
        {
            // retrieve selected categories
            var tags = new List<string>();
            List<NewsItem> news = new List<NewsItem>();
            List<CustomNewsItem> newsList = new List<CustomNewsItem>();

            foreach (var tagID in SelectedCategories)
            {
                //get news items for each selected category
                news.AddRange(GetContentByCategory(tagID));
            }

            //order all news by date
            if (news.Count > 0)
            {
                news.Sort((a, b) => b.PublicationDate.CompareTo(a.PublicationDate));
            }

            //reduce the news to the limit number
            if (!string.IsNullOrEmpty(this.Limit))
            {
                int number = int.Parse(this.Limit);
                news = news.Take(number).ToList();
            }

            foreach (NewsItem item in news)
            {
                HierarchicalTaxon category = getItemCategory(item);
                CustomNewsItem n = new CustomNewsItem();
                n.Title = item.Title;
                n.Summary = item.Summary;
                n.Url = item.ItemDefaultUrl;
                n.PublicationDate = item.PublicationDate;
                n.Category = category.Title;
                n.Image = GetImageUrl(item);
                n.Style = getItemStyleByCategory(category.Id);

                if (n.Image == "")
                {
                    n.Style += " single-no-image";
                }
                
                newsList.Add(n);
            }

            return newsList;
        }

        public string GetImageUrl(NewsItem item)
        {
            // retrieve the Screenshot value and extract the first (and only) item
            Telerik.Sitefinity.Libraries.Model.Image image = (Telerik.Sitefinity.Libraries.Model.Image)item.GetValue("Image");

            if (image?.Url != null)
            {
                var url = image.Url;

                if (ImageSize == null)
                    return url;

                var uriBuilder = new UriBuilder(url);

                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query["size"] = ImageSize.Value.ToString();

                uriBuilder.Query = query.ToString();

                url = uriBuilder.ToString();

                return url;
            }
            else
            {
                return "";

            }
        }

        private HierarchicalTaxon getItemCategory(NewsItem item)
        {
            IList<Guid> categoryIds = (IList<Guid>)item.GetValue("Category");
            Guid firstCategoryId = categoryIds.FirstOrDefault();
            HierarchicalTaxon firstCategory = null;
            if (Guid.Empty != firstCategoryId)
            {
                firstCategory = taxonomyMgr.GetTaxon<HierarchicalTaxon>(firstCategoryId);
            }

            return firstCategory;
        }

        private string getItemStyleByCategory(Guid Id)
        {
            if (!usedCategoryIds.Contains(Id))
            {
                usedCategoryIds.Add(Id);
            }

            int index = usedCategoryIds.IndexOf(Id);
            index++;
            return "style-" + index;
        }

        public List<NewsItem> GetContentByCategory(Guid categoryId)
        {
            // Get the Id of the category
            var taxonomy = taxonomyMgr.GetTaxa<HierarchicalTaxon>()
                                     .Where(t => t.Id == categoryId)
                                     .SingleOrDefault();

            if (taxonomy == null)
            {
                return new List<NewsItem>();
            }

            var taxonId = taxonomy.Id;

            // Get all news items that are assigned to this category
            var NewsItemsInCategory = newsMgr.GetNewsItems()
                .Where(p => p.Status == ContentLifecycleStatus.Live && p.GetValue<TrackedList<Guid>>("Category").Contains(taxonId));
            return NewsItemsInCategory.ToList();
        }

        public Taxon GetCategoryByTitle(string title)
        {
            HierarchicalTaxonomy category = taxonomyMgr.GetTaxonomies<HierarchicalTaxonomy>().Where(x => x.Name == "Categories").SingleOrDefault();

            if (category == null)
            {
                return null;
            }

            return category.Taxa.Where(x => x.Title == title).SingleOrDefault();
        }
        #endregion

        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/UserControls/SoftwareDesign_Aptify/LayoutWidgets/LatestNewsWidget/LatestNewsWidget.ascx";
        #endregion
    }
}
