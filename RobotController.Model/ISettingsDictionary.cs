using System;
using System.Collections.Generic;
using System.Text;

namespace RobotController.Model
{
    public interface ISettingsDictionary
    {
        IDictionary<string,object> Dictionary { get; }
    }
}
