using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trending.Core;

namespace Trending.MVVM.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public NavigationService NavigationService { get; }

        public MainViewModel(
            NavigationService navigationService)
        {
            NavigationService = navigationService;
            NavigationService.NavigateTo<LogInViewModel>();
        }
    }
}
