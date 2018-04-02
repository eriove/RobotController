using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RobotController.Model;
using Flurl.Http.Testing;
using Moq;
using Shouldly;
using Xunit;
using Xunit.Abstractions;
using RobotController.Common;

namespace RobotController.ViewModel.Tests
{
    public class MainViewModelIntegrationTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly HttpTest _httpTest = new HttpTest();
        private readonly MainViewModel _sut;
        private string _ip = "http://192.168.10.141";
        public MainViewModelIntegrationTests(ITestOutputHelper output)
        {
            _output = output;
            var resolver = new Mock<IHostNameResolver>();
            resolver.Setup(x => x.GetValidHostName(It.IsAny<string>())).Returns(Task.FromResult(_ip));
            var settings = new Mock<ISettings>();
            settings.Setup(x => x.HostName).Returns(_ip);

            IRobotModel robotModel = new RobotModel(resolver.Object, settings.Object);
            _sut = new MainViewModel(robotModel, new Mock<INavigationService>().Object);
        }

        [Fact]
        public void GoForwardShouldCallServos()
        {
            _httpTest.RespondWith("740");
            _sut.GoForward.Execute(null);

            Thread.Sleep(1000);   // To wait for events, should ideally be solved in a better way
            PrintAllCalls();
            _httpTest.ShouldHaveCalled(_ip + "/servos").WithQueryParamValue("1",255).WithQueryParamValue("2", 0);
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
            Thread.Sleep(1000);   // To wait for events, should ideally be solved in a better way
            PrintAllCalls();
            batteryVoltageWasCalled.ShouldBe(true);
        }

        [Fact]
        public void WhenMakingCallWithTimeoutErrorMessageShouldBeSet()
        {
            _httpTest.SimulateTimeout();
            bool errorWasCalled = false;
            _sut.PropertyChanged += (sender, args) =>
            {
                _output.WriteLine(args.PropertyName);
                if (args.PropertyName == nameof(MainViewModel.ErrorMessage))
                {
                    errorWasCalled = true;
                }
            };
            _sut.GoForward.Execute(null);
            Thread.Sleep(1000);   // To wait for events, should ideally be solved in a better way
            PrintAllCalls();
            errorWasCalled.ShouldBe(true);
        }

        [Fact]
        public async Task TestResolvingTmdss()
        {
            TmdsMDnsHostNameResolver resolver = new TmdsMDnsHostNameResolver();
            string result = await resolver.GetValidHostName("http://walle.local");
            result.ShouldBe(_ip);
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
