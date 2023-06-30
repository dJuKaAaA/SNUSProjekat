using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        public MainViewModel(
            NavigationService navigationService)
        {
            AdminMenuVisibility = Visibility.Collapsed;

            NavigationService = navigationService;
            navigationService.NavigationCompleted += OnNavigationCompleted;
            NavigationService.NavigateTo<LogInViewModel>();
        }

        private void OnNavigationCompleted(object sender, NavigationCompletedEventArgs e)
        {
            if (e.PreviousViewModel == typeof(LogInViewModel))
            {
                AdminMenuVisibility = SignedUser.Role == CoreUserRef.Role.Admin ?
                    Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}
