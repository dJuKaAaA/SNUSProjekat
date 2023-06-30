using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCADACore.Execptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException()
            : base("User not found!")
        {
        }
    }
}