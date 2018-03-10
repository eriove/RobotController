using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RobotController.Model;
using RobotController.ViewModel;
using StructureMap;
using Xamarin.Forms;

namespace RobotController.Xamarin.Forms
{
	public partial class App : Application
	{
	    private readonly Container _container;
        public App()
		{
			InitializeComponent();
		    _container = new Container(_ =>
		    {
		        _.For<IRobotModel>().Use<RobotModel>();
		        _.For<MainPage>().Use<MainPage>();
		        _.For<MainViewModel>().Use<MainViewModel>();
		        _.For<IHostNameResolver>().Use<SimpleHostNameResolver>();
            });
            MainPage = _container.GetInstance<MainPage>();
        }

	    public App(MainPage mainPage)
	    {
	        MainPage = mainPage;

	    }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
