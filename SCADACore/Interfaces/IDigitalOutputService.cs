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
    public interface IDigitalOutputService
    {
        [OperationContract]
        IEnumerable<DigitalOutput> GetAll();

        [OperationContract]
        DigitalOutput GetForIOAddress(int ioAddress);

        [OperationContract]
        void Save(DigitalOutput digitalOutput);

        [OperationContract]
        void SetNewValue(int ioAddress, bool newValue);

        [OperationContract]
        DigitalOutput Create(DigitalOutput output);

        [OperationContract]
        DigitalOutput GetByTagName(string tagName);
        
    }
}