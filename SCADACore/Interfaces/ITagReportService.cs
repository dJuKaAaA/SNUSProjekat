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
    public interface ITagReportService
    {
        [OperationContract]
        IEnumerable<TagReport> GetMostRecent();
    }
}
