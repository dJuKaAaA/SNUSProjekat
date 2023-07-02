using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SCADACore.Exceptions
{
    [DataContract]
    public class UserDbException : Exception
    {
        public UserDbException(string message) : base(message) { }
    }
}