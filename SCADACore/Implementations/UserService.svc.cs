using SCADACore.Context;
using SCADACore.Faults;
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
        public User CreateAccount(User user, string password)
        {
            if (GetByUsername(user.Username) != null)
            {
                UserDbFault fault = new UserDbFault("Username already exists!");
                throw new FaultException<UserDbFault>(fault, "A fault has occurred");
            }

            using (DbUserContext db = new DbUserContext())
            {
                user.Password = password;
                db.Users.Add(user);
                db.SaveChanges();
            }

            return GetByUsername(user.Username);
        }

        public IEnumerable<User> GetAll()
        {
            using (DbUserContext db = new DbUserContext())
            {
                return db.Users.ToList();
            }
        }

        public User GetById(int id)
        {
            using (DbUserContext db = new DbUserContext())
            {
                return db.Users.Where(user => user.Id == id).FirstOrDefault();
            }
        }

        public User GetByUsername(string username)
        {
            using (DbUserContext db = new DbUserContext())
            {
                return db.Users.Where(user => user.Username == username).FirstOrDefault();
            }
        }

        public User LogIn(string username, string password)
        {
            using (DbUserContext db = new DbUserContext())
            {
                return db.Users.Where(user => user.Username == username && user.Password == password).FirstOrDefault();
            }
        }
    }
}