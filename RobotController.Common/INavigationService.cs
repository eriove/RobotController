using System.Threading.Tasks;

namespace RobotController.Common
{
    public interface INavigationService
    {
        Task ShowSettings();

        Task GoBackToMainScreen();
    }
}
