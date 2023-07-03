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
    public interface IAnalogInputService
    {
        [OperationContract]
        IEnumerable<AnalogInput> GetAll();

        [OperationContract]
        AnalogInput GetForIOAddress(int ioAddress);

        [OperationContract]
        void Save(AnalogInput analogInput);

        [OperationContract]
        AnalogInput Create(AnalogInput input);

        [OperationContract]
        AnalogInput GetByTagName(string tagName);

        [OperationContract]
        void SetNewValue(int ioAddress, double newValue);

        [OperationContract]
        IEnumerable<TagAlarm> GetTagAlarms(string tagName);
    }
}