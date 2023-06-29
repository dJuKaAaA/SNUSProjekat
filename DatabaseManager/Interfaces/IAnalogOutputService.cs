using DatabaseManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager.Interfaces
{
    [ServiceContract]
    public interface IAnalogOutputService
    {
        [OperationContract]
        IEnumerable<AnalogOutput> GetAll();

        [OperationContract]
        AnalogOutput GetForIOAddress(int ioAddress);
    }
}
