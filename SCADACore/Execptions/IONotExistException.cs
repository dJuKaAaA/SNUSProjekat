using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCADACore.Execptions
{
    public class IONotExistException : Exception
    {
        public IONotExistException()
          : base("IO not exist!")
        {
        }
    }
}