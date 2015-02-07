namespace Treant
{
    using System;
    using System.ComponentModel.Composition;
    using System.Windows;
    using System.Windows.Controls;
    using Treant.Core;
    using Treant.Core.Extenders;
    using Treant.Services.Authentication;
    using Treant.ViewModels;

    [Export]
    public class LoginViewModel : BaseViewModel
    {
        private AuthenticationService authenticationService;

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                this.RaisePropertyChanged();
            }
        }

        public RelayCommand SignInCommand { get; set; }

        public RelayCommand ExitCommand { get; set; }

        [ImportingConstructor]
        public LoginViewModel(AuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;

            SignInCommand = new RelayCommand(SignInCommandExecute);
            ExitCommand = new RelayCommand((o) => Application.Current.Shutdown());
        }

        private void SignInCommandExecute(object obj)
        {
            if (String.IsNullOrWhiteSpace(Name))
            {
                MessageBox.Show("Invalid username", "Authentication error");
                return;
            }

            var passwordBox = obj as PasswordBox;
            var password = passwordBox.Password;

            try
            {
                this.authenticationService.Authenticate(Name, password);

                var loginWindow = Application.Current.MainWindow;
                Application.Current.MainWindow = ControlFactory.CreateWindow<MainViewModel>();
                loginWindow.Close();
                Application.Current.MainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Authentication error");
            }
        }
    }
}
