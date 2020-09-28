using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareDesign.GTM.Helper
{
    internal static class TemplateHelper
    {
        internal static string GetTemplate(string path)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(
                $"SoftwareDesign.GTM.Templates.{path}"))
            {
                if (stream == null) throw new InvalidTemplateException($"Invalid path: {path}");

                TextReader tr = new StreamReader(stream);
                return tr.ReadToEnd();
            }
        }
    }
}
