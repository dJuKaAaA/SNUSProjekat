﻿using SCADACore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SCADACore.Interfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITagReportService" in both code and config file together.
    [ServiceContract]
    public interface ITagReportService
    {
        [OperationContract]
        IEnumerable<TagReport> GetMostRecent();
    }
}
