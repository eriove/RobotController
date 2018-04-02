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
	public partial class SettingsPage : ContentPage, ISettingsView
	{
	    private readonly Func<INavigation> _navigationOfFirstPage;

	    public SettingsPage(SettingsViewModel viewModel, Func<INavigation> navigationOfFirstPage)
		{
		    _navigationOfFirstPage = navigationOfFirstPage;
		    InitializeComponent ();
            BindingContext = viewModel;
		}

        public async Task Show()
        {
            await _navigationOfFirstPage().PushModalAsync(this);
        }

	    public async Task Hide()
	    {
	        await _navigationOfFirstPage().PopAsync();
        }
	}
}