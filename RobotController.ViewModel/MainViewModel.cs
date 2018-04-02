using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RobotController.Common;
using RobotController.Model;
using UnitsNet;

namespace RobotController.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IRobotModel _model;
        private bool _canSendCommands = true;
        

        public MainViewModel(IRobotModel model, ISettingsView settingsView)
        {
            _model = model;
            _model.RaiseBatteryVoltageChanged += (sender, dc) =>
            {
                BatteryVoltage = dc;
            };

            _model.RaiseException += (sender, exception) =>
            {
                ErrorMessage = exception.Message;
            };

            ShowSettings = new RelayCommand(async () => await settingsView.Show());

            GoForward = new RelayCommand(async () =>
            {
                CanSendCommands = false;
                await _model.GoForward();
                CanSendCommands = true;
            }, () => CanSendCommands);
            GoBackward = new RelayCommand(async () =>
            {
                CanSendCommands = false;
                await _model.GoBackward();
                CanSendCommands = true;
            }, () => CanSendCommands);
            GoForwardLeft = new RelayCommand(async () =>
            {
                CanSendCommands = false;
                await _model.GoForwardLeft();
                CanSendCommands = true;
            }, () => CanSendCommands);
            GoForwardRight = new RelayCommand(async () =>
            {
                CanSendCommands = false;
                await _model.GoForwardRight();
                CanSendCommands = true;
            }, () => CanSendCommands);
            Stop = new RelayCommand(async () =>
            {
                CanSendCommands = false;
                await _model.Stop();
                CanSendCommands = true;
            }, () => CanSendCommands);
            RotateLeft = new RelayCommand(async () =>
            {
                CanSendCommands = false;
                await _model.RotateLeft();
                CanSendCommands = true;
            }, () => CanSendCommands);
            RotateRight = new RelayCommand(async () =>
            {
                CanSendCommands = false;
                await _model.RotateRight();
                CanSendCommands = true;
            }, () => CanSendCommands);
        }

        public RelayCommand ShowSettings { get; set; }

        public RelayCommand GoForward { get; set; }
        public RelayCommand GoBackward { get; set; }
        public RelayCommand GoForwardLeft { get; set; }
        public RelayCommand GoForwardRight { get; set; }
        public RelayCommand Stop { get; set; }

        public RelayCommand RotateLeft { get; set; }
        public RelayCommand RotateRight { get; set; }
        private ElectricPotentialDc _batteryVoltage;
        public ElectricPotentialDc BatteryVoltage
        {
            get => _batteryVoltage;
            set
            {
                if (!value.Equals(_batteryVoltage,ElectricPotentialDc.FromVoltsDc(0.001)))
                {
                    _batteryVoltage = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (value != _errorMessage)
                {
                    _errorMessage = value;
                    RaisePropertyChanged();
                }
            }
        }


        public bool CanSendCommands
        {
            get { return _canSendCommands; }
            set
            {
                if (value != _canSendCommands)
                {
                    _canSendCommands = value;
                    RaisePropertyChanged();
                    GoForward.RaiseCanExecuteChanged();
                    GoForwardLeft.RaiseCanExecuteChanged();
                    GoForwardRight.RaiseCanExecuteChanged();
                    Stop.RaiseCanExecuteChanged();
                    RotateLeft.RaiseCanExecuteChanged();
                    RotateRight.RaiseCanExecuteChanged();
                }
            }
        }
    }
}
