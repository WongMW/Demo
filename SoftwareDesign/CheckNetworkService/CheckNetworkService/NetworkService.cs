using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.NetworkInformation;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using Serilog;
using System.Runtime.InteropServices;
using System.Timers;

namespace CheckNetworkService
{
    // New-Service -Name "CheckNetwork" -BinaryPathName D:\CheckNetwork\CheckNetworkService.exe

    // $service = Get-WmiObject -Class Win32_Service -Filter "Name='CheckNetwork'"
    // $service.delete()
    // or
    // sc.exe delete CheckNetwork

    public partial class NetworkService : ServiceBase
    {
        private List<string> _ips = new List<string>();
        private readonly Timer _timer;

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);

        public NetworkService()
        {
            InitializeComponent();
            _timer = new Timer();
        }

        protected override void OnStart(string[] args)
        {
            string logPath = ConfigurationManager.AppSettings["LogPath"];
            double timerInterval = Convert.ToDouble(ConfigurationManager.AppSettings["TimerInterval"]);

            // Update the service state to Start Pending.
            ServiceStatus serviceStatus = new ServiceStatus
            {
                dwCurrentState = ServiceState.SERVICE_START_PENDING,
                dwWaitHint = 100000
            };

            SetServiceStatus(ServiceHandle, ref serviceStatus);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(logPath, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            GetIps();

            _timer.Elapsed += (s, e) =>
            {
                CheckServers();
            };

            _timer.Interval = timerInterval;
            _timer.Enabled = true;

            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(ServiceHandle, ref serviceStatus);
        }

        private void GetIps()
        {
            string sectionName = "appSettings";
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //ConfigurationManager.RefreshSection(sectionName);
            AppSettingsSection appSettingSection = (AppSettingsSection)config.GetSection(sectionName);
            var ipsFromConfig = appSettingSection.Settings["IPs"].Value;

            _ips = Regex.Split(ipsFromConfig, @",").ToList();
        }


        private void CheckServers()
        {
            var pingSender = new Ping();

            foreach (var ip in _ips)
            {
                string data = "dummydatafrom32bytesaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 120;
                var reply = pingSender.Send(ip, timeout, buffer);
                if (reply?.Status != IPStatus.Success)
                {
                    Log.Information($"{ip} is not available");
                }
            }
        }

        protected override void OnStop()
        {
            _timer.Stop();
            _timer.Dispose();
        }
    }
}
