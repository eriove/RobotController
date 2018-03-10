using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RobotController.Model
{
    public interface IHostNameResolver
    {
        Task<string> GetValidHostName(string hostName);
    }

    public class SimpleHostNameResolver :IHostNameResolver
    {
        public Task<string> GetValidHostName(string hostName)
        {
            return Task.FromResult(hostName);
        }
    }
}
