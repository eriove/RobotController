using System.Threading.Tasks;
using System.Windows;
using RobotController.Model;

namespace RobotController.WPF
{
    public class WpfSettings : ISettings
    {
        public string HostName
        {
            get
            {
                if (!Application.Current.Properties.Contains(nameof(HostName)))
                {
                    string hostname = "http://walle.local";
                    Application.Current.Properties[nameof(HostName)] = hostname;
                    return hostname;
                }
                else
                {
                    return Application.Current.Properties[nameof(HostName)].ToString();
                }

            }
            set => Application.Current.Properties[nameof(HostName)] = value;
        }

        public byte LeftMiddleValue { get; set; }
        public byte RightMiddleValue { get; set; }

        public Task SaveAsync()
        {
            return Task.CompletedTask;
        }
    }
}