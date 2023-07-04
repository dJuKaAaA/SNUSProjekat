using SCADACore.Execptions;
using SCADACore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SCADACore.Interfaces
{
    [ServiceContract]
    public interface IReportService
    {
        [OperationContract]
        IEnumerable<TagReport> GetMostRecentTagReports();

        [OperationContract]
        IEnumerable<AlarmReport> GetMostRecentAlarmReports();

        [OperationContract(IsOneWay = true)]
        void CreateAlarmReport(AlarmReport alarmReport);

        [OperationContract]
        IEnumerable<AlarmReport> GetAlarmReportsInTimePeriod(int startPeriod, int endPeriod);

        [OperationContract]
        IEnumerable<AlarmReport> GetAlarmReportsForPriority(PriorityType priority);

        [OperationContract]
        IEnumerable<TagReport> GetTagReportsInTimePeriod(int startPeriod, int endPeriod);

        [OperationContract]
        IEnumerable<TagReport> GetTagReportsForTagType(IOType tagType);

        [OperationContract]
        IEnumerable<TagReport> GetTagReportsForTagName(string tagName);

    }
}
