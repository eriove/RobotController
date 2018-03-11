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

        public RobotModel(IHostNameResolver hostNameResolver)
        {
            _hostNameResolver = hostNameResolver;
        }

        private readonly string _hostName = "http://walle.local";//"http://192.168.10.113";
        private volatile string _resolvedHostName;

        private async Task SetServosAndUpdateBatteryVoltage(byte[] servoValues)
        {
            _resolvedHostName  = _resolvedHostName ?? await _hostNameResolver.GetValidHostName(_hostName);
            var query = _resolvedHostName.AppendPathSegment("servos");
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
            return SetServosAndUpdateBatteryVoltage(new byte[] { 255, 128 });
        }

        public Task GoForwardRight()
        {
            return SetServosAndUpdateBatteryVoltage(new byte[] { 128, 0});
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
