using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SCADACore.Interfaces
{
    [ServiceContract(CallbackContract = typeof(IScanCallback))]
    public interface IScanService
    {
        [OperationContract]
        void StartScan(int ioAdress);

        [OperationContract]
        void EndScan(int ioAdress);
    }

    public interface IScanCallback
    {
        [OperationContract]
        void AnalogScanDone(int ioAdress, double value);

        [OperationContract]
        void DigitalScanDone(int ioAdress, bool value);
    }
}