using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using RobotController.Model;
using RobotController.ViewModel;
using StructureMap;
using Xamarin.Forms;

namespace RobotController.Xamarin.Forms.UWP
{
    public sealed partial class MainPage
    {
        private readonly Container _container;
        private Forms.MainPage _mainPage;
        public MainPage(/*RobotController.Xamarin.Forms.App formsApp*/)
        {
            var registry = new Forms.AppRegistry();
            Func<INavigation> navigationFactory = NavigationFactory;
            registry.For<Func<INavigation>>().Use(navigationFactory);
            _container = new Container(registry);

            this.InitializeComponent();
            _mainPage = _container.GetInstance<Forms.MainPage>();
           
            
            LoadApplication(new Forms.App(_mainPage));
        }

        private INavigation NavigationFactory()
        {
            return _mainPage.Navigation;
        }
    }
}
