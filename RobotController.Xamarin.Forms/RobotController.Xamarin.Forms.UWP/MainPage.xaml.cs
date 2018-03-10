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

namespace RobotController.Xamarin.Forms.UWP
{
    public sealed partial class MainPage
    {
        private readonly Container _container;
        public MainPage(/*RobotController.Xamarin.Forms.App formsApp*/)
        {
            var registry = new RobotController.Xamarin.Forms.AppRegistry();
            _container = new Container(registry);

            this.InitializeComponent();
            var mainPage = _container.GetInstance<RobotController.Xamarin.Forms.MainPage>();
            LoadApplication(new RobotController.Xamarin.Forms.App(mainPage));
        }
    }
}
