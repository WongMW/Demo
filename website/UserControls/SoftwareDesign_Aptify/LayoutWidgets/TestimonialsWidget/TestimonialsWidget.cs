using System;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web;
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
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Modules.Libraries;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Telerik.Sitefinity.RelatedData;

namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.TestimonialsWidget
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
    ///             .LoadOrAddWidget<TestimonialsWidget>("TestimonialsWidget")
    ///                 .SetTitle("TestimonialsWidget")
    ///                 .SetDescription("TestimonialsWidget")
    ///                 .LocalizeUsing<ModuleResourceClass>() //Optional
    ///                 .SetCssClass(WidgetCssClass) // You can use a css class to add an icon (Optional)
    ///             .Done()
    ///         .Done()
    ///     .Done();
    /// </remarks>
    /// <see cref="http://www.sitefinity.com/documentation/documentationarticles/user-guide/widgets"/>
    [Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesigner(typeof(SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.TestimonialsWidget.Designer.TestimonialsWidgetDesigner))]
    public class TestimonialsWidget : SimpleView
    {
        #region Properties
        /// <summary>
        /// Gets or sets the message that will be displayed in the label.
        /// </summary>
        public string SelectedTestimonialIds { get; set; }
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
                    return TestimonialsWidget.layoutTemplatePath;
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
        protected virtual Label MessageLabel
        {
            get
            {
                return this.Container.GetControl<Label>("MessageLabel", true);
            }
        }
        /// <summary>
        /// Gets the text box control.
        /// </summary>
        /// <value>The text box control.</value>
        internal protected virtual Repeater TestimonialRepeater
        {
            get
            {
                return this.Container.GetControl<Repeater>("TestimonialRepeater", true);
            }
        }
        /// <summary>
        /// Gets the text box control.
        /// </summary>
        /// <value>The text box control.</value>
        internal protected virtual Repeater ImageRepeater
        {
            get
            {
                return this.Container.GetControl<Repeater>("ImageRepeater", true);
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
            if (this.SelectedTestimonialIds != null)
            {
                List<Guid> idsList = new JavaScriptSerializer().Deserialize<List<Guid>>(this.SelectedTestimonialIds);
                List<DynamicContent> testimonials = new List<DynamicContent>();
                var providerName = String.Empty;
                DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
                Type testimonialType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Testimonials.Testimonial");

                foreach (var id in idsList)
                {
                    var testimonial = dynamicModuleManager.GetDataItem(testimonialType, id);

                    if (testimonial != null)
                    {
                        Telerik.Sitefinity.Libraries.Model.Image image = (Telerik.Sitefinity.Libraries.Model.Image)testimonial.GetRelatedItems("Image").SingleOrDefault();
                        if (image != null)
                        {
                            testimonial.SetValue("ImageUrl", image.Url);
                        }
                        testimonials.Add(testimonial);
                    }
                }
                this.TestimonialRepeater.DataSource = testimonials;
                this.TestimonialRepeater.DataBind();

                this.ImageRepeater.DataSource = testimonials;
                this.ImageRepeater.DataBind();
            }
        }

        #endregion

        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/UserControls/SoftwareDesign_Aptify/LayoutWidgets/TestimonialsWidget/TestimonialsWidget.ascx";
        #endregion
    }
}
