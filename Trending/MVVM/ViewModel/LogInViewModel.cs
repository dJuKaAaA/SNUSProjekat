using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private readonly NavigationService _navigationService;

        public ICommand LogInCommand { get; }

        public LogInViewModel(
            NavigationService navigationService)
        {
            _navigationService = navigationService;

            LogInCommand = new RelayCommand(OnLogIn, CanLogIn);
        }

        private void OnLogIn(object o)
        {
            _navigationService.NavigateTo<InputsViewModel>();
        }

        private bool CanLogIn(object o)
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }
    }
}
