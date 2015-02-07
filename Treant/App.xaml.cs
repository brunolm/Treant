using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Treant.Core;
using Treant.Services.Authentication;

namespace Treant
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // TODO: Remove
            #region Automatic login - Temp - Remove

            MefBootstrap.Resolve<AuthenticationService>().Authenticate("brunolm", "password");

            Application.Current.MainWindow = ControlFactory.CreateWindow<MainViewModel>();
            Application.Current.MainWindow.Show();

            return;

            #endregion

            MainWindow = ControlFactory.CreateWindow<LoginViewModel>();
            MainWindow.Show();
        }
    }
}
