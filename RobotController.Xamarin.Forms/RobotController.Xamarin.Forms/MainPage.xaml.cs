using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotController.ViewModel;
using Xamarin.Forms;

namespace RobotController.Xamarin.Forms
{
	public partial class MainPage : ContentPage
	{
		public MainPage(MainViewModel mainViewModel)
		{
			InitializeComponent();
		    BindingContext = mainViewModel;
		}
	}
}
