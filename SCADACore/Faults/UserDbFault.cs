using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SCADACore.Faults
{
    [DataContract]
    public class UserDbFault
    {
        [DataMember]
        public string Message { get; private set; }

        public UserDbFault(string message)
        {
            Message = message;
        }
    }
}