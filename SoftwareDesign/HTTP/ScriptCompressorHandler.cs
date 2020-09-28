using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;

namespace SoftwareDesign.HTTP
{
    public class ScriptCompressorHandler : IHttpHandler
    {
        private const int DaysInCache = 30;

        /// <inheritdoc />
        /// <summary>
        /// Enables processing of HTTP Web requests by a custom 
        /// HttpHandler that implements the <see cref="T:System.Web.IHttpHandler"></see> interface.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpContext"></see> object that provides 
        /// references to the intrinsic server objects 
        /// (for example, Request, Response, Session, and Server) used to service HTTP requests.
        /// </param>
        public void ProcessRequest(HttpContext context)
        {
            var root = context.Request.Url.GetLeftPart(UriPartial.Authority);
            var path = context.Request.QueryString["path"];
            var content = string.Empty;

            if (!string.IsNullOrEmpty(path))
            {
                if (context.Cache[path] == null)
                {
                    var scripts = path.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var script in scripts)
                    {
                        var patchedScript = script.Replace("&amp;", "&");
                        // We only want to serve resource files for security reasons.
                        if (patchedScript.ToUpperInvariant().Contains("RESOURCE.AXD"))
                            content += RetrieveScript(root + patchedScript) + Environment.NewLine;
                    }

                    content = StripWhitespace(content);
                    context.Cache.Insert(path, content, null, Cache.NoAbsoluteExpiration,
                        new TimeSpan(DaysInCache, 0, 0, 0));
                }
            }

            if (path != null) content = (string) context.Cache[path];
            if (string.IsNullOrEmpty(content)) return;

            context.Response.Write(content);
            SetHeaders(content.GetHashCode(), context);

            Compress(context);
        }

        /// <summary>
        /// Retrieves the specified remote script using a WebClient.
        /// </summary>
        /// <param name="file">The remote URL</param>
        private static string RetrieveScript(string file)
        {
            string script = null;

            try
            {
                var url = new Uri(file, UriKind.Absolute);

                var request = (HttpWebRequest) WebRequest.Create(url);
                request.Method = "GET";
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (var response = (HttpWebResponse) request.GetResponse())
                {
                    var responseStream = response.GetResponseStream();
                    if (responseStream != null)
                    {
                        using (var reader = new StreamReader(responseStream))
                        {
                            script = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (System.Net.Sockets.SocketException)
            {
                // The remote site is currently down. Try again next time.
            }
            catch (UriFormatException)
            {
                // Only valid absolute URLs are accepted
            }

            return script;
        }

        /// <summary>
        /// Strips the whitespace from any .js file.
        /// </summary>
        private static string StripWhitespace(string body)
        {
            var lines = body.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
            var emptyLines = new StringBuilder();
            foreach (var line in lines)
            {
                var s = line.Trim();
                if (s.Length > 0 && !s.StartsWith("//"))
                    emptyLines.AppendLine(s.Trim());
            }

            body = emptyLines.ToString();

            // remove C styles comments
            body = Regex.Replace(body, "/\\*.*?\\*/", String.Empty, RegexOptions.Compiled | RegexOptions.Singleline);
            //// trim left
            body = Regex.Replace(body, "^\\s*", String.Empty, RegexOptions.Compiled | RegexOptions.Multiline);
            //// trim right
            body = Regex.Replace(body, "\\s*[\\r\\n]", "\r\n", RegexOptions.Compiled | RegexOptions.ECMAScript);
            // remove whitespace beside of left curly braced
            body = Regex.Replace(body, "\\s*{\\s*", "{", RegexOptions.Compiled | RegexOptions.ECMAScript);
            // remove whitespace beside of coma
            body = Regex.Replace(body, "\\s*,\\s*", ",", RegexOptions.Compiled | RegexOptions.ECMAScript);
            // remove whitespace beside of semicolon
            body = Regex.Replace(body, "\\s*;\\s*", ";", RegexOptions.Compiled | RegexOptions.ECMAScript);
            // remove newline after keywords
            body = Regex.Replace(body,
                "\\r\\n(?<=\\b(abstract|boolean|break|byte|case|catch|char|class|const|continue|default|delete|do|double|else|extends|false|final|finally|float|for|function|goto|if|implements|import|in|instanceof|int|interface|long|native|new|null|package|private|protected|public|return|short|static|super|switch|synchronized|this|throw|throws|transient|true|try|typeof|var|void|while|with)\\r\\n)",
                " ", RegexOptions.Compiled | RegexOptions.ECMAScript);

            return body;
        }

        /// <summary>
        /// This will make the browser and server keep the output
        /// in its cache and thereby improve performance.
        /// </summary>
        private static void SetHeaders(int hash, HttpContext context)
        {
            context.Response.ContentType = "text/javascript";
            context.Response.Cache.VaryByHeaders["Accept-Encoding"] = true;

            context.Response.Cache.SetExpires(DateTime.Now.ToUniversalTime().AddDays(DaysInCache));
            context.Response.Cache.SetCacheability(HttpCacheability.Public);
            context.Response.Cache.SetMaxAge(new TimeSpan(DaysInCache, 0, 0, 0));
            context.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            context.Response.Cache.SetETag("\"" + hash.ToString() + "\"");
        }

        #region Compression

        private const string Gzip = "gzip";
        private const string Deflate = "deflate";

        private static void Compress(HttpContext context)
        {
            if (context.Request.UserAgent != null && context.Request.UserAgent.Contains("MSIE 6"))
                return;

            if (IsEncodingAccepted(Gzip))
            {
                context.Response.Filter = new GZipStream(context.Response.Filter, CompressionMode.Compress);
                SetEncoding(Gzip);
            }
            else if (IsEncodingAccepted(Deflate))
            {
                context.Response.Filter = new DeflateStream(context.Response.Filter, CompressionMode.Compress);
                SetEncoding(Deflate);
            }
        }

        /// <summary>
        /// Checks the request headers to see if the specified
        /// encoding is accepted by the client.
        /// </summary>
        private static bool IsEncodingAccepted(string encoding)
        {
            return HttpContext.Current.Request.Headers["Accept-encoding"] != null &&
                   HttpContext.Current.Request.Headers["Accept-encoding"].Contains(encoding);
        }

        /// <summary>
        /// Adds the specified encoding to the response headers.
        /// </summary>
        /// <param name="encoding"></param>
        private static void SetEncoding(string encoding)
        {
            HttpContext.Current.Response.AppendHeader("Content-encoding", encoding);
        }

        #endregion

        /// <inheritdoc />
        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler"></see> instance.
        /// </summary>
        /// <value></value>
        /// <returns>true if the <see cref="T:System.Web.IHttpHandler"></see> instance is reusable; otherwise, false.</returns>
        public bool IsReusable => false;
    }

}