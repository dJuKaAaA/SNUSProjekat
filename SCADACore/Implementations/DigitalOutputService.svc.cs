using SCADACore.Interfaces;
using SCADACore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SCADACore.Implementations
{
    public class DigitalOutputService : IDigitalOutputService
    {
        public IEnumerable<DigitalOutput> GetAll()
        {
            throw new NotImplementedException();
        }

        public DigitalOutput GetForIOAddress(int ioAddress)
        {
            throw new NotImplementedException();
        }
    }
}