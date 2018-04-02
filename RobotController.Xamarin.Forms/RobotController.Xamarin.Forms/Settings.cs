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
        private readonly IDictionary<string, object> _properties;
        public Settings(ISettingsDictionary settingsDictionary)
        {
            _properties = Application.Current?.Properties ?? settingsDictionary.Dictionary;
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

        public byte LeftMiddleValue
        {
            get
            {
                if (!(_properties.TryGetValue(nameof(LeftMiddleValue), out object value) &&
                    byte.TryParse(value?.ToString() ?? "", out byte byteValue)))
                {
                    byte middleValue = 95;
                    _properties[nameof(LeftMiddleValue)] = middleValue;
                    return middleValue;
                }
                else
                {
                    return byteValue;
                }

            }
            set => _properties[nameof(LeftMiddleValue)] = value;
        }

        public byte RightMiddleValue
        {
            get
            {
                if (!(_properties.TryGetValue(nameof(RightMiddleValue), out object value) &&
                      byte.TryParse(value?.ToString() ?? "", out byte byteValue)))
                {
                    byte middleValue = 96;
                    _properties[nameof(RightMiddleValue)] = middleValue;
                    return middleValue;
                }
                else
                {
                    return byteValue;
                }

            }
            set => _properties[nameof(RightMiddleValue)] = value;
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
