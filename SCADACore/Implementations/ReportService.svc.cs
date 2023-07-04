using SCADACore.Context;
using SCADACore.Execptions;
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
    public class ReportService : IReportService
    {
        private readonly int _maxFetchCount = 20;

        public void CreateAlarmReport(AlarmReport alarmReport)
        {
            using (var db = new DbIOContext())
            {
                db.AlarmReports.Add(alarmReport);
                db.SaveChanges();
            }
        }

        public IEnumerable<AlarmReport> GetAlarmReportsForPriority(PriorityType priority)
        {
            using (var db = new DbIOContext())
            {
                int i = 0;
                List<AlarmReport> reports = new List<AlarmReport>();
                List<AlarmReport> filteredReports = db.AlarmReports
                    .Where(report => report.TagAlarm.Priority == priority)
                    .OrderByDescending(report => report.Timestamp).ToList();
                foreach (AlarmReport report in filteredReports)
                {
                    TagAlarm alarm = db.TagAlarms.FirstOrDefault(a => a.Id == report.AlarmId);
                    report.TagAlarm = alarm;

                    if (i >= _maxFetchCount) break;

                    reports.Add(report);
                    ++i;
                }

                return reports;
            }
        }

        public IEnumerable<AlarmReport> GetAlarmReportsInTimePeriod(int startPeriod, int endPeriod)
        {
            using (var db = new DbIOContext())
            {
                int i = 0;
                List<AlarmReport> reports = new List<AlarmReport>();
                List<AlarmReport> filteredReports = db.AlarmReports
                    .Where(report => report.Timestamp >= startPeriod && report.Timestamp <= endPeriod)
                    .OrderByDescending(report => report.Timestamp).ToList();
                foreach (AlarmReport report in filteredReports)
                {
                    TagAlarm alarm = db.TagAlarms.FirstOrDefault(a => a.Id == report.AlarmId);
                    report.TagAlarm = alarm;

                    if (i >= _maxFetchCount) break;

                    reports.Add(report);
                    ++i;
                }

                return reports;
            }
        }

        public IEnumerable<AlarmReport> GetMostRecentAlarmReports()
        {
            using (var db = new DbIOContext())
            {
                int i = 0;
                List<AlarmReport> reports = new List<AlarmReport>();
                List<AlarmReport> filteredReports = db.AlarmReports.OrderByDescending(report => report.Timestamp).ToList();
                foreach (AlarmReport report in filteredReports)
                {
                    TagAlarm alarm = db.TagAlarms.FirstOrDefault(a => a.Id == report.AlarmId);
                    report.TagAlarm = alarm;

                    if (i >= _maxFetchCount) break;

                    reports.Add(report);
                    ++i;
                }

                return reports;
            }
        }

        public IEnumerable<TagReport> GetMostRecentTagReports()
        {
            using (DbIOContext db = new DbIOContext())
            {
                int i = 0;
                List<TagReport> reports = new List<TagReport>();
                List<TagReport> filteredReports = db.TagReports.OrderByDescending(report => report.Timestamp).ToList();
                foreach (TagReport report in filteredReports)
                {
                    if (i >= _maxFetchCount) break;

                    reports.Add(report);
                    ++i;
                }

                return reports;
            }
        }

        public IEnumerable<TagReport> GetTagReportsForTagName(string tagName)
        {
            using (var db = new DbIOContext())
            {
                int i = 0;
                List<TagReport> reports = new List<TagReport>();
                List<TagReport> filteredReports = db.TagReports
                    .Where(report => report.TagName == tagName)
                    .OrderByDescending(report => report.Timestamp).ToList();
                foreach (TagReport report in filteredReports)
                {
                    if (i >= _maxFetchCount) break;

                    reports.Add(report);
                    ++i;
                }

                return reports;

            }
        }

        public IEnumerable<TagReport> GetTagReportsForTagType(IOType tagType)
        {
            using (var db = new DbIOContext())
            {
                int i = 0;
                List<TagReport> reports = new List<TagReport>();
                List<TagReport> filteredReports = db.TagReports
                    .Where(report => report.TagType == tagType)
                    .OrderByDescending(report => report.Timestamp).ToList();
                foreach (TagReport report in filteredReports)
                {
                    if (i >= _maxFetchCount) break;

                    reports.Add(report);
                    ++i;
                }

                return reports;

            }
        }

        public IEnumerable<TagReport> GetTagReportsInTimePeriod(int startPeriod, int endPeriod)
        {
            using (var db = new DbIOContext())
            {
                int i = 0;
                List<TagReport> reports = new List<TagReport>();
                List<TagReport> filteredReports = db.TagReports
                    .Where(report => report.Timestamp >= startPeriod && report.Timestamp <= endPeriod)
                    .OrderByDescending(report => report.Timestamp).ToList();
                foreach (TagReport report in filteredReports)
                {
                    if (i >= _maxFetchCount) break;

                    reports.Add(report);
                    ++i;
                }

                return reports;

            }
        }
    }
}
