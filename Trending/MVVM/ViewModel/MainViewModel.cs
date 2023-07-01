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
    public class MainViewModel : ViewModelBase
    {
        private Visibility _adminMenuVisibility;

        public Visibility AdminMenuVisibility
        {
            get { return _adminMenuVisibility; }
            set { _adminMenuVisibility = value; OnPropertyChanged(); }
        }

        public static CoreUserRef.User SignedUser { get; set; }

        public NavigationService NavigationService { get; }

        public ICommand NavigateToCreateUserViewCommand { get; }
        public ICommand NavigateToCreateInputViewCommand { get; }
        public ICommand NavigateToCreateOutputViewCommand { get; }
        public ICommand NavigateToDbAnalogInputsViewCommand { get; }
        public ICommand NavigateToDbAnalogOutputsViewCommand { get; }
        public ICommand NavigateToDbDigitalInputsViewCommand { get; }
        public ICommand NavigateToDbDigitalOutputsViewCommand { get; }
        public ICommand NavigateToDbUsersViewCommand { get; }
        public ICommand LogOutCommand { get; }

        public MainViewModel(
            NavigationService navigationService)
        {
            AdminMenuVisibility = Visibility.Collapsed;

            NavigationService = navigationService;
            navigationService.NavigationCompleted += OnNavigationCompleted;
            NavigationService.NavigateTo<LogInViewModel>();

            NavigateToCreateUserViewCommand = new RelayCommand(o => NavigationService.NavigateTo<CreateUserViewModel>(), o => SignedUser?.Role == CoreUserRef.Role.Admin);
            NavigateToCreateInputViewCommand = new RelayCommand(o => NavigationService.NavigateTo<CreateInputViewModel>(), o => SignedUser?.Role == CoreUserRef.Role.Admin);
            NavigateToCreateOutputViewCommand = new RelayCommand(o => NavigationService.NavigateTo<CreateOutputViewModel>(), o => SignedUser?.Role == CoreUserRef.Role.Admin);
            NavigateToDbAnalogInputsViewCommand = new RelayCommand(o => NavigationService.NavigateTo<DbAnalogInputsViewModel>(), o => SignedUser?.Role == CoreUserRef.Role.Admin);
            NavigateToDbAnalogOutputsViewCommand = new RelayCommand(o => NavigationService.NavigateTo<DbAnalogOutputsViewModel>(), o => SignedUser?.Role == CoreUserRef.Role.Admin);
            NavigateToDbDigitalInputsViewCommand = new RelayCommand(o => NavigationService.NavigateTo<DbDigitalInputsViewModel>(), o => SignedUser?.Role == CoreUserRef.Role.Admin);
            NavigateToDbDigitalOutputsViewCommand = new RelayCommand(o => NavigationService.NavigateTo<DbDigitalOutputsViewModel>(), o => SignedUser?.Role == CoreUserRef.Role.Admin);
            NavigateToDbUsersViewCommand = new RelayCommand(o => NavigationService.NavigateTo<DbUsersViewModel>(), o => SignedUser?.Role == CoreUserRef.Role.Admin);
            LogOutCommand = new RelayCommand(OnLogOut, o => SignedUser?.Role == CoreUserRef.Role.Admin);
        }

        private void OnLogOut(object o)
        {
            SignedUser = null;
            NavigationService.NavigateTo<LogInViewModel>();
        }

        private void OnNavigationCompleted(object sender, NavigationCompletedEventArgs e)
        {
            if (e.PreviousViewModel == typeof(LogInViewModel)
                || e.CurrentViewModel == typeof(LogInViewModel))
            {
                AdminMenuVisibility = SignedUser?.Role == CoreUserRef.Role.Admin ?
                    Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}
