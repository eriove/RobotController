using System;
using System.Linq;
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
        private string _ip = "http://192.168.10.113";
        public MainViewModelIntegrationTests(ITestOutputHelper output)
        {
            _output = output;
            IRobotModel robotModel = new RobotModel(new TmdsMDnsHostNameResolver());
            _sut = new MainViewModel(robotModel);
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
        public async Task TestResolving()
        {
            ILookup<string, string> domains = await ZeroconfResolver.BrowseDomainsAsync();
            var responses = await ZeroconfResolver.ResolveAsync(domains.Select(x=>x.Key));
            foreach (var resp in responses)
            {
                _output.WriteLine(resp.ToString());
                _output.WriteLine(resp.IPAddress);
            }
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
