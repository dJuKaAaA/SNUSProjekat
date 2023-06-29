using DatabaseManager.Interfaces;
using DatabaseManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DatabaseManager.Implementations
{
    public class UserService : IUserService
    {
        public User CreateAccount(string username, string password)
        {
            throw new NotImplementedException();
        }

        public User LogIn(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
