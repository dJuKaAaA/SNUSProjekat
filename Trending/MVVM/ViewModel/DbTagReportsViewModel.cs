using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trending.Core;
using Trending.CoreTagReportRef;

namespace Trending.MVVM.ViewModel
{
    public class DbTagReportsViewModel : ViewModelBase
    {
        public ObservableCollection<CoreTagReportRef.TagReport> Reports { get; set; }

        private readonly CoreTagReportRef.TagReportServiceClient _tagReportServiceClient;

        public DbTagReportsViewModel()
        {
            _tagReportServiceClient = new CoreTagReportRef.TagReportServiceClient();

            LoadReports();
        }

        private void LoadReports()
        {
            Reports = new ObservableCollection<CoreTagReportRef.TagReport>();
            foreach (TagReport report in _tagReportServiceClient.GetMostRecent())
            {
                Reports.Add(report);
            }
        }
    }
}
