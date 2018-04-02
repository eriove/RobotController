using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotController.Common;
using StructureMap;

namespace RobotController.Xamarin.Forms.UWP
{
    class UwpRegistry : Registry
    {
        public UwpRegistry()
        {
            IncludeRegistry<AppRegistry>();
            For<ISettingsView>().Use<SettingsPage>();
        }
    }
}
