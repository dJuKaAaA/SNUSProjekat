using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trending.Core
{
    public class NavigationService : ObservableObject
    {
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            private set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        private Func<Type, ViewModelBase> _viewModelFactory;

        public EventHandler<NavigationCompletedEventArgs> NavigationCompleted;

        public NavigationService(Func<Type, ViewModelBase> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public void NavigateTo<TViewModel>(object param = null) where TViewModel : ViewModelBase
        {
            if (typeof(TViewModel) != CurrentViewModel?.GetType())
            {
                Type previousViewModel = CurrentViewModel?.GetType();
                CurrentViewModel = _viewModelFactory.Invoke(typeof(TViewModel));
                NavigationCompleted?.Invoke(this, new NavigationCompletedEventArgs(previousViewModel, CurrentViewModel?.GetType()));
            }
        }
    }
}
