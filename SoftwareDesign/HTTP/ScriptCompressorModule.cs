using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;

namespace SoftwareDesign.HTTP
{
    public class ScriptCompressorModule : IHttpModule
    {

        #region IHttpModule Members

        void IHttpModule.Dispose()
        {
            // Nothing to dispose; 
        }

        void IHttpModule.Init(HttpApplication context)
        {
            context.PostRequestHandlerExecute += context_BeginRequest;
        }

        #endregion

        private static void context_BeginRequest(object sender, EventArgs e)
        {
            var app = sender as HttpApplication;

            if (app?.Context?.CurrentHandler == null) return;

            if (app.Request.RawUrl.Contains("serviceframe")) return;

            if (!(app.Context.CurrentHandler is Page || IsTelerikPageWrapper(app.Context.CurrentHandler))) return;

            //if (app.Context.Request.Url.Scheme.Contains("https")) return;

            app.Response.Filter = new WebResourceFilter(app.Response.Filter);
        }

        private static bool IsTelerikPageWrapper(IHttpHandler httpHandler)
        {
            //app.Context.CurrentHandler is Telerik.Sitefinity.Web.PageHandlerWrapper)

            return httpHandler.GetType().Name == "PageHandlerWrapper";
        }

        #region Stream filter

        private class WebResourceFilter : Stream
        {

            public WebResourceFilter(Stream sink)
            {
                _sink = sink;
            }

            private readonly Stream _sink;

            #region Properties

            public override bool CanRead => true;

            public override bool CanSeek => true;

            public override bool CanWrite => true;

            public override void Flush()
            {
                _sink.Flush();
            }

            public override long Length => 0;

            public override long Position { get; set; }

            #endregion

            #region Methods

            public override int Read(byte[] buffer, int offset, int count)
            {
                return _sink.Read(buffer, offset, count);
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                return _sink.Seek(offset, origin);
            }

            public override void SetLength(long value)
            {
                _sink.SetLength(value);
            }

            public override void Close()
            {
                _sink.Close();
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                var data = new byte[count];
                Buffer.BlockCopy(buffer, offset, data, 0, count);
                var html = Encoding.Default.GetString(buffer);
                var index = 0;
                var list = new List<string>();

                var regex =
                    new Regex(
                        "<script\\s*src=\"((?=[^\"]*(webresource.axd|scriptresource.axd))[^\"]*)\"\\s*type=\"text/javascript\"[^>]*>[^<]*(?:</script>)?",
                        RegexOptions.IgnoreCase);

                foreach (Match match in regex.Matches(html))
                {
                    if (index == 0)
                        index = html.IndexOf(match.Value, StringComparison.Ordinal);

                    var relative = match.Groups[1].Value;
                    list.Add(relative);
                    html = html.Replace(match.Value, string.Empty);
                }

                if (index > 0)
                {
                    const string script = "<script type=\"text/javascript\" src=\"js.axd?path={0}\"></script>";
                    var path = string.Empty;
                    foreach (var s in list)
                    {
                        path += HttpUtility.UrlEncode(s) + ",";
                    }

                    html = html.Insert(index, string.Format(script, path));
                }

                var outdata = Encoding.Default.GetBytes(html);
                _sink.Write(outdata, 0, outdata.GetLength(0));
            }

            #endregion

        }

        #endregion

    }
}
