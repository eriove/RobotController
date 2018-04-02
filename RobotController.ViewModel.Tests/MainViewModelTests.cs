using System;
using Moq;
using RobotController.Common;
using RobotController.Model;
using Shouldly;
using Xunit;

namespace RobotController.ViewModel.Tests
{
    public class MainViewModelTests
    {
        private readonly MainViewModel _sut;
        private readonly Mock<IRobotModel> _robotModelMock;
        private readonly INavigationService _navigationService = new Mock<INavigationService>().Object;
        public MainViewModelTests()
        {
            _robotModelMock = new Mock<IRobotModel>();
            _sut = new MainViewModel(_robotModelMock.Object, _navigationService);
        }

        [Fact]
        public void CallingGoForwardShouldDisableAndEnableAllButtons()
        {
            int numberOfCalls = 0;
            _sut.GoForward.CanExecuteChanged += (sender, args) =>
            {
                if (numberOfCalls == 0)
                    _sut.GoForward.CanExecute(null).ShouldBe(false);
                else
                    _sut.GoForward.CanExecute(null).ShouldBe(true);
                numberOfCalls++;
            };

            _sut.GoForward.Execute(null);

            numberOfCalls.ShouldBe(2);
        }

        [Fact]
        public void GoForwardShouldCallRobotModel()
        {
            _sut.GoForward.Execute(null);
            _robotModelMock.Verify(x=>x.GoForward(), Times.Once());
        }
    }
}
