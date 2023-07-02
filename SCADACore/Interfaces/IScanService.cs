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
        void StartScan(int ioAddress);

        [OperationContract(IsOneWay = true)]
        void EndScan(int ioAddress);

        [OperationContract(IsOneWay = true)]
        void EndAllScans();
    }

    public interface IScanCallback
    {
        [OperationContract(IsOneWay = true)]
        void AnalogScanDone(int ioAddress, double value);

        [OperationContract(IsOneWay = true)]
        void DigitalScanDone(int ioAddress, bool value);
    }
}