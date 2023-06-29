using SCADACore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SCADACore.Interfaces
{
    [ServiceContract]
    public interface IDigitalInputService
    {
        [OperationContract]
        IEnumerable<DigitalInput> GetAll();

        [OperationContract]
        DigitalInput GetForIOAddress(int ioAddress);
    }
}