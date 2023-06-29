using SCADACore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SCADACore.Interfaces
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        User LogIn(string username, string password);

        [OperationContract]
        User CreateAccount(string username, string password);

        [OperationContract]
        IEnumerable<User> GetAll();

        [OperationContract]
        User GetById(int id);
    }
}