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
        private string _hostName = "http://walle.local";

        private async Task SetServosAndUpdateBatteryVoltage(byte[] servoValues)
        {
            var query = _hostName.AppendPathSegment("servos");
            for (int i = 0; i < servoValues.Length; i++)
            {
                query = query.SetQueryParam((i + 1).ToString(), servoValues[i]);
            }
            var result = await  query.GetAsync().ConfigureAwait(false);
            var resultString = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (int.TryParse(resultString, out int voltageTimesHundred))
            {
                OnBatteryVoltageChanged(ElectricPotentialDc.FromVoltsDc(voltageTimesHundred * 1e-2));
            }
        }

        public event EventHandler<ElectricPotentialDc> RaiseBatteryVoltageChanged;

        public Task GoForward()
        {
            return SetServosAndUpdateBatteryVoltage(new byte[] { 255, 0 });
        }

        public Task GoBackward()
        {
            return SetServosAndUpdateBatteryVoltage(new byte[] { 0, 255 });
        }

        public Task Stop()
        {
            return SetServosAndUpdateBatteryVoltage(new byte[] { 128,128 });
        }

        public Task GoForwardLeft()
        {
            throw new NotImplementedException();
        }

        public Task GoForwardRight()
        {
            throw new NotImplementedException();
        }

       

        public async Task RotateLeft()
        {
            throw new NotImplementedException();
        }

        public async Task RotateRight()
        {
            throw new NotImplementedException();
        }

        private void OnBatteryVoltageChanged(ElectricPotentialDc batteryVoltage)
        {
            RaiseBatteryVoltageChanged?.Invoke(this, batteryVoltage);
        }
    }
}
