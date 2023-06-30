using SCADACore.Context;
using SCADACore.Execptions;
using SCADACore.Interfaces;
using SCADACore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SCADACore.Implementations
{
    public class UserService : IUserService
    {
        public User CreateAccount(string username, string password)
        {
            using (var dbContext = new DbUserContext())
            {
                User existingUser = dbContext.Users.Where(u => u.Username == username).FirstOrDefault();
                if (existingUser != null) throw new UserAlreadyExistException();

                User newUser = new User { Username = username, Password = password, Role = Role.Standard };
                dbContext.Users.Add(newUser);
                dbContext.SaveChanges();
                return newUser;
            }
        }

        public IEnumerable<User> GetAll()
        {
            using (var dbContext = new DbUserContext())
            {
                List<User> users = dbContext.Users.ToList();
                return users;
            }
        }

        public User GetById(int id)
        {
            using (var dbContext = new DbUserContext())
            {
                User user = dbContext.Users.Where(u => u.Id == id).FirstOrDefault();

                if (user == null) throw new UserNotFoundException();

                return user;
            }
        }

        public User LogIn(string username, string password)
        {
            using (var dbContext = new DbUserContext())
            {
                User user = dbContext.Users.Where(u => u.Username == username && u.Password == password).FirstOrDefault();
                if (user == null) throw new UserNotFoundException();
                return user;
            }
        }
    }
}