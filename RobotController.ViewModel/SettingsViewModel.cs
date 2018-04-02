using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RobotController.Common;
using RobotController.Model;

namespace RobotController.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly ISettings _settings;
        private readonly INavigationService _navigationService;
        private readonly IRobotModel _robotModel;

        public SettingsViewModel(ISettings settings, INavigationService navigationService, IRobotModel robotModel)
        {
            _settings = settings;
            _navigationService = navigationService;
            _robotModel = robotModel;
            SaveSettingsAndClose = new RelayCommand(async () =>
            {
                _canSave = false;
                SaveSettingsAndClose.RaiseCanExecuteChanged();
                await settings.SaveAsync();
                await _navigationService.GoBackToMainScreen();
                _canSave = true;
                SaveSettingsAndClose.RaiseCanExecuteChanged();
            },
            ()=>_canSave,true);
        }
        private bool _canSave = true;
        public RelayCommand SaveSettingsAndClose { get; set; }

        public string HostName
        {
            get => _settings.HostName;
            set => _settings.HostName = value;
        }

        public byte LeftMiddleValue
        {
            get => _settings.LeftMiddleValue;
            set
            {
                if (value != _settings.LeftMiddleValue)
                {
                    _settings.LeftMiddleValue = value;
                    _robotModel.Stop();
                    RaisePropertyChanged(nameof(LeftMiddleValue));
                }
            }
        }

        public byte RightMiddleValue
        {
            get => _settings.RightMiddleValue;
            set
            {
                if (value != _settings.RightMiddleValue)
                {
                    _settings.RightMiddleValue = value;
                    _robotModel.Stop();
                    RaisePropertyChanged(nameof(RightMiddleValue));
                }
            }
        }

    }
}
