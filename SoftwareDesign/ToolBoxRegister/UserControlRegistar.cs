using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SoftwareDesign.ToolBoxRegister
{
    public class UserControlRegistar
    {
        private readonly string _path;
        private readonly IEnumerable<string> _excludePaths;
        private readonly ToolBoxConfigRegistrar _toolBoxConfigRegistrar;
        private readonly Dictionary<string, List<string>> _directories;

        public UserControlRegistar(string virtualBasePath, string path, IEnumerable<string> excludePaths)
        {
            if (excludePaths == null) throw new ArgumentNullException("excludePaths");
            if (String.IsNullOrWhiteSpace(path)) throw new ArgumentException("Argument is null or whitespace", "path");
            if (!Directory.Exists(path))
            {
                throw new ArgumentException("Directory not found", "path");
            }

            if (String.IsNullOrWhiteSpace(virtualBasePath))
                throw new ArgumentException("Argument is null or whitespace", "virtualBasePath");

            _path = path;
            _excludePaths = excludePaths;
            _directories = new Dictionary<string, List<string>>();

            _toolBoxConfigRegistrar = new ToolBoxConfigRegistrar(virtualBasePath);
        }

        public void RegisterControls()
        {
            RegisterDirectoriesAndFiles();
            ProcessDirectoriesAndFiles();
        }

        private void RegisterDirectoriesAndFiles()
        {
            foreach (var directory in Directory.GetDirectories(_path)
                .Where(DirNotExcluded))
            {
                _directories.Add(directory, new List<string>());

                foreach (var file in Directory.GetFiles(directory, "*.ascx"))
                {
                    _directories[directory].Add(file);
                }
            }
        }

        private void ProcessDirectoriesAndFiles()
        {
            foreach (var file in _directories.SelectMany(directory => directory.Value))
            {
                _toolBoxConfigRegistrar.RegisterPath(file);
            }

            _toolBoxConfigRegistrar.Save();
        }


        private bool DirNotExcluded(string dirName)
        {
            return !_excludePaths.Contains(dirName, StringComparer.CurrentCultureIgnoreCase);
        }
    }

}
