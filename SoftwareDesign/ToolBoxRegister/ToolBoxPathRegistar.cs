using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;

namespace SoftwareDesign.ToolBoxRegister
{
    public class ToolPathRegistar
    {
        private readonly string _filePath;
        private string _sectionName;
        private ToolboxSection _section;
        private Toolbox _controls;
        private string _controlName;
        private ToolboxItem _userControlEntry;
        private readonly ToolboxesConfig _config;
        private readonly Dictionary<string, string> _sectionMapping;
        private readonly Dictionary<string, string> _fileMapping;
        private readonly string _virtualBasePath;

        public ToolPathRegistar(string virtualBasePath, string filePath, ToolboxesConfig config,
            Dictionary<string, string> sectionMapping, Dictionary<string, string> fileMapping)
        {
            if (config == null) throw new ArgumentNullException("config");
            if (sectionMapping == null) throw new ArgumentNullException("sectionMapping");
            if (fileMapping == null) throw new ArgumentNullException("fileMapping");
            if (String.IsNullOrWhiteSpace(filePath)) throw new ArgumentException("Argument is null or whitespace", "filePath");
            if (!File.Exists(filePath)) throw new ArgumentException("File does not exist", "filePath");

            if (String.IsNullOrWhiteSpace(virtualBasePath))
                throw new ArgumentException("Argument is null or whitespace", "virtualBasePath");

            _virtualBasePath = virtualBasePath;
            _filePath = filePath;
            _config = config;
            _sectionMapping = sectionMapping;
            _fileMapping = fileMapping;

            UpdateSectionName();
            UpdateControlName();
        }

        private void UpdateSectionName()
        {
            var fileInfo = new FileInfo(_filePath);
            if (fileInfo.Directory == null) return;

            var dirName = fileInfo.Directory.Name;

            if (_sectionMapping.ContainsKey(dirName))
            {
                _sectionName = _sectionMapping[dirName];
            }
            else
            {
                _sectionName = dirName
                    .Replace("_", " ");
            }

        }

        private void UpdateControlName()
        {
            var controlName = Path.GetFileNameWithoutExtension(_filePath);
            if (controlName != null && _fileMapping.ContainsKey(controlName))
            {
                _controlName = _fileMapping[controlName];
            }
            else
            {
                _controlName = controlName;    
            }
            
        }

        public void RegisterOrUpdateFile()
        {
            FindControls();
            FindSection();

            if (!SectionExists())
            {
                CreateSection();
            }

            FindUserControl();

            if (!UserControlRegistered())
            {
                // Dont add new controls if they're not required
                // CreateUserControlRegistration();
            }
            else
            {
                UpdateUserControlRegistration();
            }
        }

        private void FindControls()
        {
            _controls = _config.Toolboxes["PageControls"];
        }

        private void FindSection()
        {
            if (_controls != null)
            {
                _section =
                    _controls.Sections.FirstOrDefault<ToolboxSection>(
                        e => String.Equals(e.Name, _sectionName, StringComparison.CurrentCultureIgnoreCase));
            }
        }

        private bool SectionExists()
        {
            return _section != null;
        }

        private void CreateSection()
        {
            _section = new ToolboxSection(_controls.Sections)
            {
                Name = _sectionName,
                Title = _sectionName,
                Description = _sectionName,
                ResourceClassId = typeof(PageResources).Name
            };
            _controls.Sections.Add(_section);
        }

        private void FindUserControl()
        {
            _userControlEntry = _section.Tools.FirstOrDefault<ToolboxItem>(
                e => String.Equals(e.Name, _controlName, StringComparison.CurrentCultureIgnoreCase));

            if (_userControlEntry == null && _controlName.EndsWith("__c"))
            {
                _userControlEntry = _section.Tools.FirstOrDefault<ToolboxItem>(
                e => String.Equals(e.Name, TrimEnd( _controlName,"__c"), StringComparison.CurrentCultureIgnoreCase));
            }
        }

        private static string TrimEnd(string source, string trim)
        {
            return !source.EndsWith(trim) ? source : source.Remove(source.LastIndexOf(trim, StringComparison.InvariantCultureIgnoreCase));
        }

        private bool UserControlRegistered()
        {
            return _userControlEntry != null;
        }

        private void CreateUserControlRegistration()
        {
            var tool = new ToolboxItem(_section.Tools)
            {
                Name = _controlName,
                Title = _controlName,
                Description = _controlName,
                ControlType = GetControlTypePath()
            };

            _section.Tools.Add(tool);
        }

        private string GetControlTypePath()
        {
            return _filePath
                .Replace(_virtualBasePath, "~/")
                .Replace(@"\", "/");
        }

        private void UpdateUserControlRegistration()
        {
            _userControlEntry.ControlType = GetControlTypePath();
        }
    }
}
