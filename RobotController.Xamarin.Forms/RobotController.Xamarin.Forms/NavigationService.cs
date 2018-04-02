using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RobotController.Common;
using Xamarin.Forms;

namespace RobotController.Xamarin.Forms
{
    public class NavigationService : INavigationService
    {
        private readonly Func<INavigation> _navigationFactory;

        public NavigationService(Func<INavigation> navigationFactory)
        {
            _navigationFactory = navigationFactory;
        }

        private INavigation _navigation;
        public INavigation Navigation => _navigation ?? (_navigation = _navigationFactory());

        public Page SettingsPage { get; set; }

        public Task ShowSettings()
        {
            if(Navigation==null)
                throw new InvalidOperationException($"Set {nameof(Navigation)} before calling {nameof(ShowSettings)}");
            return Navigation.PushAsync(SettingsPage);
        }

        public Task GoBackToMainScreen()
        {
            if (Navigation == null)
                throw new InvalidOperationException($"Set {nameof(Navigation)} before calling {nameof(GoBackToMainScreen)}");
            return Navigation.PopAsync();
        }
    }
}
