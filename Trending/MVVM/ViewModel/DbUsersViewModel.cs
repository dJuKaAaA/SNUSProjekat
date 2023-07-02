using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trending.Core;

namespace Trending.MVVM.ViewModel
{
    public class DbUsersViewModel : ViewModelBase
    {
        public ObservableCollection<CoreUserRef.User> Users { get; set; }

        private readonly CoreUserRef.UserServiceClient _userServiceClient;

        public DbUsersViewModel()
        {
            _userServiceClient = new CoreUserRef.UserServiceClient();

            LoadUsers();
        }

        private void LoadUsers()
        {
            Users = new ObservableCollection<CoreUserRef.User>();
            foreach (CoreUserRef.User user in _userServiceClient.GetAll())
            {
                Users.Add(user);
            } 
        }
    }
}
