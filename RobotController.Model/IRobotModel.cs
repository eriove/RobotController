using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitsNet;

namespace RobotController.Model
{
    public interface IRobotModel
    {
        Task GoForward();
        event EventHandler<ElectricPotentialDc> RaiseBatteryVoltageChanged;
        Task GoBackward();
        Task GoForwardLeft();
        Task GoForwardRight();
        Task Stop();
        Task RotateLeft();
        Task RotateRight();
    }
}
