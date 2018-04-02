using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RobotController.Common
{
    // TODO Use INavigationService in MVVM light http://www.mvvmlight.net/doc/nav1.cshtml
    public interface ISettingsView
    {
        Task Show();
        Task Hide();
    }
}
