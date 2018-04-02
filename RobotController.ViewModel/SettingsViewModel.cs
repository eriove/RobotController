using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RobotController.Model;

namespace RobotController.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly ISettings _settings;

        public SettingsViewModel(ISettings settings)
        {
            _settings = settings;
            SaveSettings = new RelayCommand(async () =>
            {
                _canSave = false;
                SaveSettings.RaiseCanExecuteChanged();
                await settings.SaveAsync();
                _canSave = true;
                SaveSettings.RaiseCanExecuteChanged();
            },
            ()=>_canSave,true);
        }
        private bool _canSave = true;
        public RelayCommand SaveSettings { get; set; }

        public string HostName
        {
            get => _settings.HostName;
            set => _settings.HostName = value;
        }
    }
}
