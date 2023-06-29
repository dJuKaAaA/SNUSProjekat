using DatabaseManager.Interfaces;
using DatabaseManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DatabaseManager.Implementations
{
    public class AnalogOutputService : IAnalogOutputService
    {
        public IEnumerable<AnalogOutput> GetAll()
        {
            throw new NotImplementedException();
        }

        public AnalogOutput GetForIOAddress(int ioAddress)
        {
            throw new NotImplementedException();
        }
    }
}
