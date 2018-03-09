using System;
using System.Threading;
using System.Threading.Tasks;
using RobotController.Model;
using Flurl.Http.Testing;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace RobotController.ViewModel.Tests
{
    public class MainViewModelIntegrationTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly HttpTest _httpTest = new HttpTest();
        private readonly MainViewModel _sut;
        private readonly IRobotModel _robotModel;
        public MainViewModelIntegrationTests(ITestOutputHelper output)
        {
            _output = output;
            _robotModel = new RobotModel();
            _sut = new MainViewModel(_robotModel);
        }

        [Fact]
        public void GoForwardShouldCallServos()
        {
            _httpTest.RespondWith("740");
            _sut.GoForward.Execute(null);
            PrintAllCalls();
            _httpTest.ShouldHaveCalled("http://walle.local/servos").WithQueryParamValue("1",255).WithQueryParamValue("2", 0);
        }

        [Fact]
        public void WhenMakingAnyCallVoltageShouldBeUpdated()
        {
            _httpTest.RespondWith("740");
            bool batteryVoltageWasCalled = false;
            _sut.PropertyChanged += (sender, args) =>
            {
                _output.WriteLine(args.PropertyName);
                if (args.PropertyName == nameof(MainViewModel.BatteryVoltage))
                {
                    batteryVoltageWasCalled = true;
                }
            };
            _sut.GoForward.Execute(null);
            PrintAllCalls();
            batteryVoltageWasCalled.ShouldBe(true);
        }

        private void PrintAllCalls()
        {
            foreach (var calls in _httpTest.CallLog )
            {
                _output.WriteLine(calls.ToString());
            }
        }

        public void Dispose()
        {
            _httpTest.Dispose();
        }
    }
}
