using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Tmds.MDns;

namespace RobotController.Model
{
    public class TmdsMDnsHostNameResolver : IHostNameResolver
    {
        private ConcurrentDictionary<string, TaskCompletionSource<string>> _tcss = new ConcurrentDictionary<string, TaskCompletionSource<string>>();
        public Task<string> GetValidHostName(string hostName)
        {
            if (hostName.StartsWith("http://"))
            {
                hostName = hostName.Substring(7);
            }
            if(_tcss.TryGetValue(hostName, out TaskCompletionSource<string> tcs))
            {
                return tcs.Task;
            }
            var tsc = new TaskCompletionSource<string>();
            _tcss[hostName] = tsc;
           
            ServiceBrowser serviceBrowser = new ServiceBrowser();
            serviceBrowser.ServiceAdded += OnService;
            serviceBrowser.ServiceRemoved += OnService;
            serviceBrowser.ServiceChanged += OnService;
            serviceBrowser.StartBrowse("_http._tcp");
            return tsc.Task;
        }

        void OnService(object sender, ServiceAnnouncementEventArgs e)
        {
            string hostName = e.Announcement.Hostname + ".local";
            if (_tcss.TryGetValue(hostName, out TaskCompletionSource<string> tcs))
            {
                tcs.SetResult("http://" + e.Announcement.Addresses.First());
            }
        }
    }
}