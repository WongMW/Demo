using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Web.UI;

namespace SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.Slider
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
    ///             .LoadOrAddWidget<SliderWidget>("SliderWidget")
    ///                 .SetTitle("SliderWidget")
    ///                 .SetDescription("SliderWidget")
    ///                 .LocalizeUsing<ModuleResourceClass>() //Optional
    ///                 .SetCssClass(WidgetCssClass) // You can use a css class to add an icon (Optional)
    ///             .Done()
    ///         .Done()
    ///     .Done();
    /// </remarks>
    /// <see cref="http://www.sitefinity.com/documentation/documentationarticles/user-guide/widgets"/>
    [Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesigner(typeof(SitefinityWebApp.UserControls.SoftwareDesign_Aptify.LayoutWidgets.Slider.Designer.SliderWidgetDesigner))]
    public class SliderWidget : SimpleView
    {
        #region Properties
        /// <summary>
        /// Gets or sets the message that will be displayed in the label.
        /// </summary>
        public string SelectedImagesIds { get; set; }

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
                    return SliderWidget.layoutTemplatePath;
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
           
            if (this.SelectedImagesIds != null)
            {
                List<Guid> idsList = new JavaScriptSerializer().Deserialize<List<Guid>>(this.SelectedImagesIds);
                List<ImageModel> images = new List<ImageModel>();

                foreach (var id in idsList)
                {
                    var url = GetMediaUrlByImageId(id, true);
                    Telerik.Sitefinity.Libraries.Model.Image image = GetImageByID(id);
                    if (image != null)
                    {
                        ImageModel imageModel = new ImageModel();
                        imageModel.title = image.Title;
                        imageModel.url = url;
                        imageModel.subtitle = image.AlternativeText;
                        imageModel.link = (string)Telerik.Sitefinity.Model.DataExtensions.GetValue(image, "PageURL");
                        images.Add(imageModel);
                    }
                }
                this.ImageRepeater.DataSource = images;
                this.ImageRepeater.DataBind();
                //BindData(images);
            }
        }

        private Telerik.Sitefinity.Libraries.Model.Image GetImageByID(Guid masterImageId)
        {
            LibrariesManager librariesManager = LibrariesManager.GetManager();
            Telerik.Sitefinity.Libraries.Model.Image image = librariesManager.GetImages().Where(i => i.Id == masterImageId).FirstOrDefault();

            if (image != null)
            {
                image = librariesManager.Lifecycle.GetLive(image) as Telerik.Sitefinity.Libraries.Model.Image;
            }

            return image;
        }

        private void BindData(List<ImageModel> list)
        {
            this.ImageRepeater.DataSource = list;
            this.ImageRepeater.DataBind();
        }

        public static string GetMediaUrlByImageId(Guid masterImageId, bool resolveAsAbsolutUrl)
        {
            var manager = LibrariesManager.GetManager();

            // Get the master version of the image
            var image = manager.GetImages().FirstOrDefault(i => i.Id == masterImageId);

            var mediaUlr = String.Empty;

            if (image != null)
            {
                // Resolve the media URL
                mediaUlr = image.ResolveMediaUrl(resolveAsAbsolutUrl);
            }

            return mediaUlr;
        }

        #endregion

        #region Private members & constants
        public static readonly string layoutTemplatePath = "~/UserControls/SoftwareDesign_Aptify/LayoutWidgets/Slider/SliderWidget.ascx";
        #endregion
    }
}
