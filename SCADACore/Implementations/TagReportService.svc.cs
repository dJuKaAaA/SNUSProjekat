using SCADACore.Context;
using SCADACore.Interfaces;
using SCADACore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.UI.WebControls;

namespace SCADACore.Implementations
{
    public class TagReportService : ITagReportService
    {
        public IEnumerable<TagReport> GetMostRecent()
        {
            using (DbIOContext db = new DbIOContext())
            {
                const int recentCount = 10;
                int i = 0;
                List<TagReport> reports = new List<TagReport>();
                foreach (TagReport report in db.TagReports.OrderByDescending(report => report.Timestamp))
                {
                    if (i >= recentCount) break;

                    reports.Add(report);
                    ++i;
                }

                return reports;
            }
        }
    }
}
