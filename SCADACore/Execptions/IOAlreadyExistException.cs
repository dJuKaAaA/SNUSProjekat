using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCADACore.Execptions
{
    public class IOAlreadyExistException : Exception
    {
        public IOAlreadyExistException()
           : base("IO already exist!")
        {
        }
    }
}