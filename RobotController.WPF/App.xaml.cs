using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using RobotController.Model;
using RobotController.ViewModel;
using StructureMap;

namespace RobotController.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public sealed partial class App : Application, IDisposable
    {
        private Container _container;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _container = new Container(_ =>
            {
                _.For<IRobotModel>().Use<RobotModel>();
                _.For<MainWindow>().Use<MainWindow>();
                _.For<MainViewModel>().Use<MainViewModel>();
                _.For<IHostNameResolver>().Use<SimpleHostNameResolver>();
            });

            var mainWindow = _container.GetInstance<MainWindow>();
            mainWindow.Show();
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
