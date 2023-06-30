using SCADACore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SCADACore.Execptions
{
    [DataContract]
    public class IONotExistException : Exception
    {
        public IONotExistException(IOType ioType)
          : base($"{ioType.ToString()} not exist!")
        {
        }
    }
}