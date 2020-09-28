using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;

namespace SoftwareDesign.Handlers
{
    public class ServerHtmlHandler : IHttpHandler
    {
        private IPAddress LocalIPAddress()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                return null;
            }

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            return host
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        }

        public void ProcessRequest(HttpContext context)
        {
            var response = "<!DOCTYPE html><html><head>{0}</head><body>{1}</body></html>";

            var ipAddr = LocalIPAddress();
            var ipAddrLastPart = "000";
            if(ipAddr != null)
            {
                var parts = ipAddr.ToString().Split('.');
                if(parts.Length > 0)
                {
                    ipAddrLastPart = parts[parts.Length - 1];
                }
            }

            var htmlHead = "";
            var htmlBody = String.Format("<h1>SERVER:{0}-{1}</h1>", ipAddrLastPart, Environment.MachineName);

            response = String.Format(response, htmlHead, htmlBody);

            context.Response.Write(response);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}