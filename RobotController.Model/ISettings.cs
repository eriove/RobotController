using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RobotController.Model
{
    public interface ISettings
    {
        string HostName { get; set; }
        byte LeftMiddleValue { get; set; }
        byte RightMiddleValue { get; set; }

        Task SaveAsync();
    }
}
