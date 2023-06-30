using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trending.Core
{
    public class NavigationCompletedEventArgs : EventArgs
    {
        public Type PreviousViewModel { get; set; }
        public Type CurrentViewModel { get; set; }

        public NavigationCompletedEventArgs()
        {
            
        }

        public NavigationCompletedEventArgs(Type previousViewModel, Type currentViewModel)
        {
            PreviousViewModel = previousViewModel;
            CurrentViewModel = currentViewModel;
        }
    }
}
