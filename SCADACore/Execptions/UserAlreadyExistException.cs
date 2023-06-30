using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCADACore.Execptions
{
    public class UserAlreadyExistException : Exception
    {
        public UserAlreadyExistException()
            : base("User already exist!")
        {
        }
    }
}