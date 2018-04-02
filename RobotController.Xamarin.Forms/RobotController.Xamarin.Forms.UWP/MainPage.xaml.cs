using System;
using RobotController.Model;
using StructureMap;
using Xamarin.Forms;

namespace RobotController.Xamarin.Forms.UWP
{
    public sealed partial class MainPage
    {
        private readonly Container _container;
        private readonly NavigationPage _navigationPage;
        public MainPage()
        {
            var registry = new AppRegistry();
            registry.For<ISettingsDictionary>().Use<SettingsDictionary>().Singleton();
            Func<INavigation> navigationFactory = NavigationFactory;
            registry.For<Func<INavigation>>().Use(navigationFactory);
            _container = new Container(registry);

            this.InitializeComponent();
            var mainPage = _container.GetInstance<Forms.MainPage>();
            SettingsPage settingsPage = _container.GetInstance<SettingsPage>();

            var navigationService = _container.GetInstance<NavigationService>();
            navigationService.SettingsPage = settingsPage;
            _navigationPage = new NavigationPage(mainPage);
            LoadApplication(new Forms.App(_navigationPage));
        }

        private INavigation NavigationFactory()
        {
            return _navigationPage.Navigation;
        }
    }
}
