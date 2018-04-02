using RobotController.Common;
using RobotController.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RobotController.Xamarin.Forms
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
	    public SettingsPage(SettingsViewModel viewModel)
		{
		    InitializeComponent ();
            BindingContext = viewModel;
		}
	}
}