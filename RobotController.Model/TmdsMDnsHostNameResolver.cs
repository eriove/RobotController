using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
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
            TaskCompletionSource<string> tcs;
            if (_tcss.TryGetValue(hostName, out tcs))
            {
                return tcs.Task;
            }
            tcs = new TaskCompletionSource<string>();
            _tcss[hostName] = tcs;
            var cancellationTokenSource = new CancellationTokenSource(1000);
            cancellationTokenSource.Token.Register(
                () => tcs.TrySetException(new InvalidOperationException("It took too long to resolve " + hostName)));

            ServiceBrowser serviceBrowser = new ServiceBrowser();
            serviceBrowser.ServiceAdded += OnService;
            serviceBrowser.ServiceRemoved += OnService;
            serviceBrowser.ServiceChanged += OnService;
            serviceBrowser.StartBrowse("_http._tcp",useSynchronizationContext:false);
            return tcs.Task;
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