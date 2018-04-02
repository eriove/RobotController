using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using RobotController.Model;

namespace RobotController.Xamarin.Forms.UWP
{
    public class SettingsDictionary : ISettingsDictionary
    {
        private const string ContainerName = "RobotControllerSettings";
        private readonly ApplicationDataContainer _localSettings;

        public SettingsDictionary()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Containers.TryGetValue(ContainerName, out ApplicationDataContainer readSettings))
            {
                _localSettings = readSettings;
            }
            else
            {
                _localSettings = localSettings.CreateContainer(ContainerName,
                    ApplicationDataCreateDisposition.Always);
            }
            
        }

        public IDictionary<string, object> Dictionary => _localSettings.Values;
    }
}
