using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Trending.Core;
using Trending.CoreReportServiceRef;

namespace Trending.MVVM.ViewModel
{
    public enum AlarmReportSearchCriteria
    {
        Priority,
        TimePeriod
    }

    public class DbAlarmReportsViewModel : ViewModelBase
    {
        public ObservableCollection<CoreReportServiceRef.AlarmReport> Reports { get; set; }

        private Visibility _timePeriodCriteriaVisibility;

        public Visibility TimePeriodCriteriaVisibility
        {
            get { return _timePeriodCriteriaVisibility; }
            set { _timePeriodCriteriaVisibility = value; OnPropertyChanged(); }
        }

        private Visibility _priorityCriteriaVisibility;

        public Visibility PriorityCriteriaVisibility
        {
            get { return _priorityCriteriaVisibility; }
            set { _priorityCriteriaVisibility = value; OnPropertyChanged(); }
        }

        private int _startPeriod;

        public int StartPeriod
        {
            get { return _startPeriod; }
            set { _startPeriod = value; OnPropertyChanged(); }
        }

        private int _endPeriod;

        public int EndPeriod
        {
            get { return _endPeriod; }
            set { _endPeriod = value; OnPropertyChanged(); }
        }
        public ObservableCollection<CoreReportServiceRef.PriorityType> AlarmPriorities { get; set; }

        private PriorityType _selectedAlarmPriority;

        public PriorityType SelectedAlarmPriority
        {
            get { return _selectedAlarmPriority; }
            set { _selectedAlarmPriority = value; OnPropertyChanged(); }
        }

        public ObservableCollection<AlarmReportSearchCriteria> SearchCriterias { get; set; }

        private AlarmReportSearchCriteria _selectedSearchCriteria;

        public AlarmReportSearchCriteria SelectedSearchCriteria
        {
            get { return _selectedSearchCriteria; }
            set 
            {
                _selectedSearchCriteria = value;
                OnPropertyChanged();
                switch (_selectedSearchCriteria)
                {
                    case AlarmReportSearchCriteria.Priority:
                        PriorityCriteriaVisibility = Visibility.Visible;
                        TimePeriodCriteriaVisibility = Visibility.Collapsed;
                        break;
                    case AlarmReportSearchCriteria.TimePeriod:
                        PriorityCriteriaVisibility = Visibility.Collapsed;
                        TimePeriodCriteriaVisibility = Visibility.Visible;
                        break;
                }
            }
        }

        private readonly CoreReportServiceRef.ReportServiceClient _reportServiceClient;

        public ICommand SearchCommand { get; set; } 

        public DbAlarmReportsViewModel()
        {
            AlarmPriorities = new ObservableCollection<PriorityType>() { PriorityType.High, PriorityType.Medium, PriorityType.Low };
            SelectedAlarmPriority = AlarmPriorities[0];

            SearchCriterias = new ObservableCollection<AlarmReportSearchCriteria>() { AlarmReportSearchCriteria.Priority, AlarmReportSearchCriteria.TimePeriod };
            SelectedSearchCriteria = SearchCriterias[0];

            _reportServiceClient = new CoreReportServiceRef.ReportServiceClient();

            SearchCommand = new RelayCommand(OnSearch, o => true);

            LoadReports();
        }

        private void OnSearch(object o)
        {
            Reports.Clear();
            switch (SelectedSearchCriteria)
            {
                case AlarmReportSearchCriteria.Priority:
                    foreach (AlarmReport report in _reportServiceClient.GetAlarmReportsForPriority(SelectedAlarmPriority))
                    {
                        Reports.Add(report);
                    }
                    break;
                case AlarmReportSearchCriteria.TimePeriod:
                    foreach (AlarmReport report in _reportServiceClient.GetAlarmReportsInTimePeriod(StartPeriod, EndPeriod))
                    {
                        Reports.Add(report);
                    }
                    break;
            }
        }

        private void LoadReports()
        {
            Reports = new ObservableCollection<CoreReportServiceRef.AlarmReport>();
            foreach (AlarmReport report in _reportServiceClient.GetMostRecentAlarmReports())
            {
                Reports.Add(report);
            }
        }
    }
}
