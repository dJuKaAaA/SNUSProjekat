using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Trending.Core;

namespace Trending.MVVM.ViewModel
{
    public class LogInViewModel : ViewModelBase
    {

        private string _username = string.Empty;

        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(); }
        }

        private string _password = string.Empty;

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }

        private readonly CoreUserRef.UserServiceClient _userServiceClient;

        private readonly NavigationService _navigationService;

        public ICommand LogInCommand { get; }

        public LogInViewModel(
            NavigationService navigationService)
        {
            _navigationService = navigationService;

            _userServiceClient = new CoreUserRef.UserServiceClient();
            //_userServiceClient.CreateAccount(new CoreUserRef.User()
            //{
            //    Name = "Andrew",
            //    Surname = "Tate",
            //    Username = "admin",
            //    Role = CoreUserRef.Role.Admin
            //}, "admin");

            LogInCommand = new RelayCommand(OnLogIn, CanLogIn);
        }

        private void OnLogIn(object o)
        {
            CoreUserRef.User user = _userServiceClient.LogIn(Username, Password);
            if (user != null)
            {
                MainViewModel.SignedUser = user;
                switch (MainViewModel.SignedUser.Role)
                {
                    case CoreUserRef.Role.Admin:
                        _navigationService.NavigateTo<DbAnalogInputsViewModel>();
                        break;
                    case CoreUserRef.Role.Standard:
                        _navigationService.NavigateTo<MonitorInputsViewModel>();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Invalid credentials", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanLogIn(object o)
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }
    }
}
