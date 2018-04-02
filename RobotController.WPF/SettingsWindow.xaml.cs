using System;
using System.Threading.Tasks;
using System.Windows;
using RobotController.Common;
using RobotController.ViewModel;

namespace RobotController.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }
        public SettingsWindow(SettingsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        public void CloseWindow(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
