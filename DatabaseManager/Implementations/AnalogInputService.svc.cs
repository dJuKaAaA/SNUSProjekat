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
    public class AnalogInputService : IAnalogInputService
    {
        public IEnumerable<AnalogInput> GetAll()
        {
            throw new NotImplementedException();
        }

        public AnalogInput GetForIOAddress(int ioAddress)
        {
            throw new NotImplementedException();
        }
    }
}
