using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotController.Model;
using RobotController.ViewModel;

namespace RobotController.WPF
{
    public class Wrapper
    {

        public object ViewModel { get; } = new MainViewModel(new RobotModel());
    }
}

