using System;
using System.Web.UI;

namespace SoftwareDesign.GTM
{
    public abstract class GtmBase
    {
        private readonly Page _page;
        protected string TemplateName;

        protected GtmBase(Page page)
        {
            if (page == null) throw new ArgumentNullException(nameof(page));

            _page = page;
        }

        public void Render()
        {
            var type = _page.GetType();
            _page.ClientScript.RegisterClientScriptBlock(type, this.GetType().Name, GetJs());
        }

        public void RenderWithWrapperFunction(string functionName, Func<string,string> replaceFunction = null)
        {
            var type = _page.GetType();
            var jsScript = GetJs();

            if (replaceFunction != null)
            {
                jsScript = replaceFunction(jsScript);
            }

            var script = "function " + functionName + "(){ " + RemoveScriptTags(jsScript) + " }";
            _page.ClientScript.RegisterClientScriptBlock(type, this.GetType().Name, script, true);
        }

        private static string RemoveScriptTags(string script)
        {
            return
                script
                .Replace("<script>", string.Empty)
                .Replace("</script>", string.Empty);
        }

        public void RenderAsync(UpdatePanel updatePanel)
        {
            ScriptManager.RegisterClientScriptBlock(updatePanel, updatePanel.GetType(), this.GetType().Name, GetJs(), false);
        }

        private string GetJs()
        {
            var dto = GetDto();
            return GTMRazorCompiler.RenderPartial(TemplateName, dto);
        }

        protected abstract object GetDto();

    }
}