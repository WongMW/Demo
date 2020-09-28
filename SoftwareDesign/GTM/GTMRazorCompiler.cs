using RazorEngine;
using SoftwareDesign.GTM.Helper;

namespace SoftwareDesign.GTM
{
    public static class GTMRazorCompiler
    {
        public static string RenderPartial(string templateName, object dto = null)
        {
            // loading a template might be expensive, so be careful to cache content
            if (Razor.Resolve(templateName) == null)
            {
                // we've never seen this template before, so compile it and stick it in cache.
                var templateContent = TemplateHelper.GetTemplate(templateName);
                Razor.Compile(templateContent, templateName);
            }

            // by now, we know we've got a the template cached and ready to run; this is fast
            var renderedContent = Razor.Run(templateName, dto);
            return renderedContent;
        }

    }
}