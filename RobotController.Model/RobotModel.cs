using Flurl;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Flurl.Http;
using UnitsNet;

namespace RobotController.Model
{
    public class RobotModel : IRobotModel
    {
        private readonly IHostNameResolver _hostNameResolver;
        private readonly ISettings _settings;

        public RobotModel(IHostNameResolver hostNameResolver, ISettings settings)
        {
            _hostNameResolver = hostNameResolver;
            _settings = settings;
        }

        private async Task SetServosAndUpdateBatteryVoltage(byte[] servoValues)
        {
            try
            {
                var query = _settings.HostName.AppendPathSegment("servos");
                for (int i = 0; i < servoValues.Length; i++)
                {
                    query = query.SetQueryParam((i + 1).ToString(), servoValues[i]);
                }
                var result = await query.PostAsync(null);
                var resultString = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (int.TryParse(resultString, out int voltageTimesHundred))
                {
                    OnBatteryVoltageChanged(ElectricPotentialDc.FromVoltsDc(voltageTimesHundred * 1e-2));
                }
            }
            catch (Exception e)
            {
                RaiseException?.Invoke(this, e);
            }
        }

        public event EventHandler<ElectricPotentialDc> RaiseBatteryVoltageChanged;
        public event EventHandler<Exception> RaiseException;


        public Task GoForward()
        {
            return SetServosAndUpdateBatteryVoltage(new byte[] { 0, 255 });
        }

        public Task GoBackward()
        {
            return SetServosAndUpdateBatteryVoltage(new byte[] { 255, 0 });
        }

        public Task Stop()
        {
            return SetServosAndUpdateBatteryVoltage(new byte[] { _settings.LeftMiddleValue,_settings.RightMiddleValue});
        }

        public Task GoForwardLeft()
        {
            return SetServosAndUpdateBatteryVoltage(new byte[] { 255, _settings.RightMiddleValue });
        }

        public Task GoForwardRight()
        {
            return SetServosAndUpdateBatteryVoltage(new byte[] { _settings.LeftMiddleValue, 0});
        }

       

        public Task RotateLeft()
        {
            return SetServosAndUpdateBatteryVoltage(new byte[] { 255, 255 });
        }

        public Task RotateRight()
        {
            return SetServosAndUpdateBatteryVoltage(new byte[] { 0, 0 });
        }

        private void OnBatteryVoltageChanged(ElectricPotentialDc batteryVoltage)
        {
            RaiseBatteryVoltageChanged?.Invoke(this, batteryVoltage);
        }
    }
}
