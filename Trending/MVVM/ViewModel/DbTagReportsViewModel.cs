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
    public enum TagReportSearchCriteria
    {
        TimePeriod,
        TagType,
        TagName
    }

    public class DbTagReportsViewModel : ViewModelBase
    {
        public ObservableCollection<CoreReportServiceRef.TagReport> Reports { get; set; }

        private Visibility _timePeriodCriteriaVisibility;

        public Visibility TimePeriodCriteriaVisibility
        {
            get { return _timePeriodCriteriaVisibility; }
            set { _timePeriodCriteriaVisibility = value; OnPropertyChanged(); }
        }

        private Visibility _tagTypeCriteriaVisibility;

        public Visibility TagTypeCriteriaVisibility
        {
            get { return _tagTypeCriteriaVisibility; }
            set { _tagTypeCriteriaVisibility = value; OnPropertyChanged(); }
        }

        private Visibility _tagNameCriteriaVisibility;

        public Visibility TagNameCriteriaVisibility
        {
            get { return _tagNameCriteriaVisibility; }
            set { _tagNameCriteriaVisibility = value; OnPropertyChanged(); }
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
        public ObservableCollection<CoreReportServiceRef.IOType> TagTypes { get; set; }

        private IOType _selectedTagType;

        public IOType SelectedTagType
        {
            get { return _selectedTagType; }
            set { _selectedTagType = value; OnPropertyChanged(); }
        }

        private string _tagName;

        public string TagName
        {
            get { return _tagName; }
            set { _tagName = value; OnPropertyChanged(); }
        }

        public ObservableCollection<TagReportSearchCriteria> SearchCriterias { get; set; }

        private TagReportSearchCriteria _selectedSearchCriteria;

        public TagReportSearchCriteria SelectedSearchCriteria
        {
            get { return _selectedSearchCriteria; }
            set 
            {
                _selectedSearchCriteria = value;
                OnPropertyChanged();
                switch (_selectedSearchCriteria)
                {
                    case TagReportSearchCriteria.TimePeriod:
                        TimePeriodCriteriaVisibility = Visibility.Visible;
                        TagTypeCriteriaVisibility = Visibility.Collapsed;
                        TagNameCriteriaVisibility = Visibility.Collapsed;
                        break;
                    case TagReportSearchCriteria.TagType:
                        TimePeriodCriteriaVisibility = Visibility.Collapsed;
                        TagTypeCriteriaVisibility = Visibility.Visible;
                        TagNameCriteriaVisibility = Visibility.Collapsed;
                        break;
                    case TagReportSearchCriteria.TagName:
                        TimePeriodCriteriaVisibility = Visibility.Collapsed;
                        TagTypeCriteriaVisibility = Visibility.Collapsed;
                        TagNameCriteriaVisibility = Visibility.Visible;
                        break;
                }
            }
        }

        private readonly CoreReportServiceRef.ReportServiceClient _reportServiceClient;

        public ICommand SearchCommand { get; set; } 

        public DbTagReportsViewModel()
        {
            TagTypes = new ObservableCollection<IOType>() { IOType.AnalogOutput, IOType.AnalogInput, IOType.DigitalInput, IOType.DigitalOutput };
            SelectedTagType = TagTypes[0];

            SearchCriterias = new ObservableCollection<TagReportSearchCriteria>() { TagReportSearchCriteria.TimePeriod, TagReportSearchCriteria.TagType, TagReportSearchCriteria.TagName };
            SelectedSearchCriteria = SearchCriterias[0];

            _reportServiceClient = new CoreReportServiceRef.ReportServiceClient();

            SearchCommand = new RelayCommand(OnSearch, CanSearch);

            LoadReports();
        }

        private bool CanSearch(object o)
        {
            if (SelectedSearchCriteria == TagReportSearchCriteria.TagName)
            {
                return !string.IsNullOrWhiteSpace(TagName);
            }

            return true;
        }

        private void OnSearch(object o)
        {
            Reports.Clear();
            switch (SelectedSearchCriteria)
            {
                case TagReportSearchCriteria.TimePeriod:
                    foreach (TagReport report in _reportServiceClient.GetTagReportsInTimePeriod(StartPeriod, EndPeriod))
                    {
                        Reports.Add(report);
                    }
                    break;
                case TagReportSearchCriteria.TagType:
                    foreach (TagReport report in _reportServiceClient.GetTagReportsForTagType(SelectedTagType))
                    {
                        Reports.Add(report);
                    }
                    break;
                case TagReportSearchCriteria.TagName:
                    foreach (TagReport report in _reportServiceClient.GetTagReportsForTagName(TagName))
                    {
                        Reports.Add(report);
                    }
                    break;
            }
        }

        private void LoadReports()
        {
            Reports = new ObservableCollection<CoreReportServiceRef.TagReport>();
            foreach (TagReport report in _reportServiceClient.GetMostRecentTagReports())
            {
                Reports.Add(report);
            }
        }
    }
}
