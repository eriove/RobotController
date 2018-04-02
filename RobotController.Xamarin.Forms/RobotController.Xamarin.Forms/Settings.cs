using RobotController.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RobotController.Xamarin.Forms
{
    public class Settings : ISettings
    {
        private IDictionary<string, object> _properties;
        public Settings()
        {
            _properties = Application.Current?.Properties ?? new ConcurrentDictionary<string, object>();
        }
        public string HostName
        {
            get
            {
                if (!_properties.TryGetValue(nameof(HostName), out object value))
                {
                    string hostname = "walle.local";
                    _properties[nameof(HostName)] = hostname;
                    return hostname;
                }
                else
                {
                    return value?.ToString();
                }

            }
            set => _properties[nameof(HostName)] = value;
        }

        public Task SaveAsync()
        {
            var currentApplication = Application.Current;
            if(currentApplication!=null && currentApplication.Properties == _properties)
                return Application.Current.SavePropertiesAsync();
            else
             return Task.CompletedTask;
        }
    }
}
