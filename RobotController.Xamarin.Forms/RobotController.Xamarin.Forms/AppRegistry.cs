using System;
using System.Collections.Generic;
using System.Text;
using RobotController.Model;
using RobotController.ViewModel;
using StructureMap;

namespace RobotController.Xamarin.Forms
{
    public class AppRegistry : Registry
    {
        public AppRegistry()
        {
            For<IRobotModel>().Use<RobotModel>();
            For<MainPage>().Use<MainPage>();
            For<MainViewModel>().Use<MainViewModel>();
            For<IHostNameResolver>().Use<SimpleHostNameResolver>();
        }
    }
}
