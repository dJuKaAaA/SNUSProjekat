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
    public class DigitalInputService : IDigitalInputService
    {
        public IEnumerable<DigitalInput> GetAll()
        {
            throw new NotImplementedException();
        }

        public DigitalInput GetForIOAddress(int ioAddress)
        {
            throw new NotImplementedException();
        }
    }
}
