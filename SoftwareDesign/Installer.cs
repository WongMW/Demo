using System;
using System.Linq;
using System.Web.Hosting;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Abstractions.VirtualPath.Configuration;
using SoftwareDesign.ToolBoxRegister;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Utilities.TypeConverters;
using System.IO;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.News.Model;
//----
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Publishing.Pipes;
using SoftwareDesign.Search.InboundPipes;
using Telerik.Sitefinity.Publishing.Model;


namespace SoftwareDesign
{
    public class Installer
    {
        /// <summary>
        /// This is the actual method that is called by ASP.NET even before application start.
        /// </summary>
        public static void PreApplicationStart()
        {
            // With this method we subscribe for the Sitefinity Bootstrapper_Initialized event, which is fired after initialization of the Sitefinity application
            Bootstrapper.Initialized += Bootstrapper_Initialized;
        }

        /// <summary>
        /// With this method we subscribe for the Sitefinity Bootstrapper_Initialized
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Bootstrapper_Initialized(object sender, ExecutedEventArgs e)
        {
            if (e.CommandName != "RegisterRoutes" || !Bootstrapper.IsDataInitialized)
            {
                return;
            }

            if (e.CommandName == "RegisterRoutes")
            {
                var configManager = ConfigManager.GetManager();
                var virtualPathConfig = configManager.GetSection<VirtualPathSettingsConfig>();
                var jobsModuleVirtualPathConfig = new VirtualPathElement(virtualPathConfig.VirtualPaths)
                {
                    VirtualPath = "~/SoftwareDesign/*",
                    ResolverName = "EmbeddedResourceResolver",
                    ResourceLocation = "SoftwareDesign"
                };
                if (!virtualPathConfig.VirtualPaths.Contains(jobsModuleVirtualPathConfig))
                {
                    virtualPathConfig.VirtualPaths.Add(jobsModuleVirtualPathConfig);
                    configManager.SaveSection(virtualPathConfig);
                }
            }

            InstallWidgets();

            PageCustomFields.CreateCustomFieldFluentAPI("EpiserverPageId", typeof(PageNode), typeof(Lstring), UserFriendlyDataType.ShortText);
            PageCustomFields.CreateCustomFieldFluentAPI("EpiserverUrlPath", typeof(PageNode), typeof(Lstring), UserFriendlyDataType.LongText);
            PageCustomFields.CreateCustomFieldFluentAPI("EpiserverPageId", typeof(NewsItem), typeof(Lstring), UserFriendlyDataType.ShortText);



            // Vince McNamara, Dahu (vince@dahu.co.uk)
            // Start of Dahu changes - register Custom InboundPipes

           PublishingSystemFactory.UnregisterPipe(ContentInboundPipe.PipeName);
            PublishingSystemFactory.RegisterPipe(ContentInboundPipe.PipeName, typeof(CustomContentInboundPipe));
            PipeSettings contentPipeSettings = ContentInboundPipe.GetTemplatePipeSettings();
            contentPipeSettings.PipeName = "Dahu CustomContentInboundPipe";
            contentPipeSettings.Title = "Dahu CustomContentInboundPipe";
            PublishingSystemFactory.RegisterPipeSettings("CustomContentInboundPipe", contentPipeSettings);


            PublishingSystemFactory.UnregisterPipe(PageInboundPipe.PipeName);
            PublishingSystemFactory.RegisterPipe(PageInboundPipe.PipeName, typeof(CustomPageInboundPipe));
            PipeSettings pagePipeSettings = PageInboundPipe.GetTemplatePipeSettings();
            pagePipeSettings.PipeName = "Dahu CustomContentInboundPipe";
            pagePipeSettings.Title = "Dahu CustomPageInboundPipe";
            PublishingSystemFactory.RegisterPipeSettings("CustomPageInboundPipe", pagePipeSettings);

            PublishingSystemFactory.UnregisterPipe(DocumentInboundPipe.PipeName);
            PublishingSystemFactory.RegisterPipe(DocumentInboundPipe.PipeName, typeof(CustomDocumentInboundPipe));
           


          //  End Dahu Changes




        }

        private static void InstallWidgets()
        {
            RegisterControl("PageControls", "SDWidgets", "LoginLogoutLink", "~/SoftwareDesign/SoftwareDesign.Controls.SDWidgets.LoginLogoutLink.ascx", null);
            RegisterControl("PageControls", "SDWidgets", "uc_podcastDisplay", "~/SoftwareDesign/SoftwareDesign.Controls.SDWidgets.uc_podcastDisplay.ascx", null);
            RegisterControl("PageControls", "SDWidgets", "uc_podcastListing", "~/SoftwareDesign/SoftwareDesign.Controls.SDWidgets.uc_podcastListing.ascx", null);
            RegisterControl("PageControls", "SDWidgets", "uc_rootPageListing", "~/SoftwareDesign/SoftwareDesign.Controls.SDWidgets.uc_rootPageListing.ascx", null);
            RegisterControl("PageControls", "SDWidgets", "uc_searchResults", "~/SoftwareDesign/SoftwareDesign.Controls.SDWidgets.uc_searchResults.ascx", null);

            RegisterControl("PageControls", "SDWidgets", "AptifyForm", "~/UserControls/SoftwareDesign_Aptify/SD_Aptify__c/StudentContactForms__c.ascx", null);
            RegisterControl("PageControls", "SDWdigets", "AptifyForm", "~/UserControls/SoftwareDesign_Aptify/SD_Aptify__c/ContactForm_Prospective_Students_Queries__c.ascx", null);
            RegisterControl("PageControls", "SDWdigets", "AptifyForm", "~/UserControls/SoftwareDesign_Aptify/SD_Aptify__c/ContactForm_In_House_Training_Express_Interest__c.ascx", null);
            RegisterControl("PageControls", "SDWdigets", "AptifyForm", "~/UserControls/SoftwareDesign_Aptify/SD_Aptify__c/ContactForm_Student_Queries__cai.ascx", null);
            RegisterControl("PageControls", "SDWdigets", "AptifyForm", "~/UserControls/SoftwareDesign_Aptify/SD_Aptify__c/ContactForm_Log_Vacancies_Queries__cai.ascx", null);
            RegisterControl("PageControls", "SDWdigets", "AptifyForm", "~/UserControls/SoftwareDesign_Aptify/SD_Aptify__c/ContactForm_Technical_Queries__cai.ascx", null);
            RegisterControl("PageControls", "SDWdigets", "AptifyForm", "~/UserControls/SoftwareDesign_Aptify/SD_Aptify__c/ContactForm_AccIre_Opt_Out_Queries__cai.ascx", null);
            RegisterControl("PageControls", "SDWdigets", "AptifyForm", "~/UserControls/SoftwareDesign_Aptify/SD_Aptify__c/ContactForm_AccIre_Opt_In_Queries__cai.ascx", null);
            RegisterControl("PageControls", "SDWdigets", "AptifyForm", "~/UserControls/SoftwareDesign_Aptify/SD_Aptify__c/ContactForm_Register_School_Visit_Queries__cai.ascx", null);
            RegisterControl("PageControls", "SDWdigets", "AptifyForm", "~/UserControls/SoftwareDesign_Aptify/SD_Aptify__c/ContactForm_ConferralQueries__cai.ascx", null);


            RegisterViewMap("Resources.SDTemplates.SubscribeForm.ascx", "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.SubscribeForm, Telerik.Sitefinity");
            RegisterAptifyControls();
            RegisterAptifyLayouts();
        }

        private static void RegisterAptifyLayouts()
        {
            var path = HostingEnvironment.MapPath("~/Layouts");
            string[] fileEntries = Directory.GetFiles(path);
            foreach (string fileName in fileEntries)
            {
                String name = Path.GetFileNameWithoutExtension(fileName);
                RegisterControl("PageLayouts", "TwoColumns", name, "Telerik.Sitefinity.Web.UI.LayoutControl", "~/Layouts/" + name + ".ascx");
            }
        }

        private static void RegisterViewMap(String templatePath, String hostType)
        {
            var configManager = ConfigManager.GetManager();
            var viewMap = configManager.GetSection<ControlsConfig>();
            ConfigProperty property;
            var viewMapValue = viewMap.Properties.TryGetValue("viewMap", out property);

            if (viewMapValue)
            {
                var viewMaps = viewMap.ViewMap;
                var setting = new ViewModeControlSettings(viewMap.ViewMap);
                Type type = TypeResolutionService.ResolveType(hostType);
                setting.HostType = type;
                setting.LayoutTemplatePath = "~/SoftwareDesign/SoftwareDesign." + templatePath;

                if (!viewMaps.ContainsKey(type))
                {
                    viewMaps.Add(setting);
                    configManager.SaveSection(viewMap);
                }
            }
        }

        private static void RegisterControl(String toolBox, String sectionName, String controlName, String controlPath, String templatePath)
        {
            var configManager = ConfigManager.GetManager();
            var config = configManager.GetSection<ToolboxesConfig>();
            var controls = config.Toolboxes[toolBox];
            var section = controls.Sections.FirstOrDefault<ToolboxSection>(e => e.Name == sectionName);

            if (section == null)
            {
                section = new ToolboxSection(controls.Sections)
                {
                    Name = sectionName,
                    Title = sectionName,
                    Description = sectionName,
                    ResourceClassId = typeof(PageResources).Name
                };
                controls.Sections.Add(section);
            }

            if (section.Tools.All<ToolboxItem>(e => e.Name != controlName))
            {
                var tool = new ToolboxItem(section.Tools)
                {
                    Name = controlName,
                    Title = controlName,
                    Description = controlName,
                    ControlType = controlPath
                };

                if (templatePath != null)
                {
                    tool.LayoutTemplate = templatePath;
                }

                section.Tools.Add(tool);
            }

            configManager.SaveSection(config);
        }

        static void RegisterAptifyControls()
        {
            var virtualBasePath = HostingEnvironment.MapPath("~");
            var path = HostingEnvironment.MapPath("~/UserControls/SoftwareDesign_Aptify");

            var userControlRegistar = new UserControlRegistar(virtualBasePath, path, new[] { "LayoutWidgets", "CAI_Custom_Controls" });
            userControlRegistar.RegisterControls();
        }
    }
}
