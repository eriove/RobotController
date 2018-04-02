using System;
using System.Collections.Generic;
using System.Text;
using RobotController.Common;
using RobotController.Model;
using RobotController.ViewModel;
using StructureMap;
using Xamarin.Forms;

namespace RobotController.Xamarin.Forms
{
    public class AppRegistry : Registry
    {
        public AppRegistry()
        {
            For<IRobotModel>().Use<RobotModel>();
            For<MainPage>().Use<MainPage>();
            For<MainViewModel>().Use<MainViewModel>();
            For<IHostNameResolver>().Use<TmdsMDnsHostNameResolver> ();
            For<ISettings>().Use<Settings>();
            For<SettingsViewModel>().Use<SettingsViewModel>();
            ForConcreteType<NavigationService>().Configure.Singleton();
            Forward<NavigationService, INavigationService>();
        }
    }
}
