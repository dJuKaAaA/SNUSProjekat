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
        [OperationContract(IsOneWay = true)]
        void StartScan(int ioAdress);

        [OperationContract(IsOneWay = true)]
        void EndScan(int ioAdress);
    }

    public interface IScanCallback
    {
        [OperationContract(IsOneWay = true)]
        void AnalogScanDone(int ioAdress, double value);

        [OperationContract(IsOneWay = true)]
        void DigitalScanDone(int ioAdress, bool value);
    }
}