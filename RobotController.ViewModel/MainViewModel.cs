using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RobotController.Model;
using UnitsNet;

namespace RobotController.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IRobotModel _model;
        private bool _canSendCommands = true;
        private ElectricPotentialDc _batteryVoltage;

        public MainViewModel(IRobotModel model)
        {
            _model = model;
            _model.RaiseBatteryVoltageChanged += (sender, dc) =>
            {
                BatteryVoltage = dc;
            };
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
        public RelayCommand GoForward { get; set; }
        public RelayCommand GoBackward { get; set; }
        public RelayCommand GoForwardLeft { get; set; }
        public RelayCommand GoForwardRight { get; set; }
        public RelayCommand Stop { get; set; }

        public RelayCommand RotateLeft { get; set; }
        public RelayCommand RotateRight { get; set; }
        public ElectricPotentialDc BatteryVoltage
        {
            get => _batteryVoltage;
            set {
                if (value != _batteryVoltage)
                {
                    _batteryVoltage = value;
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
                }
            }
        }
    }
}
