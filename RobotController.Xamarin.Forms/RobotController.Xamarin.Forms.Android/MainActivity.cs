using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using StructureMap;
using Xamarin.Forms;

namespace RobotController.Xamarin.Forms.Droid
{
    [Activity(Label = "RobotController.Xamarin.Forms", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public MainActivity()
        {
            var registry = new RobotController.Xamarin.Forms.AppRegistry();
            _container = new Container(registry);
        }

        private readonly Container _container;
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
          
            var mainPage = _container.GetInstance<RobotController.Xamarin.Forms.MainPage>();
            LoadApplication(new App(new NavigationPage(mainPage)));
        }
    }
}

