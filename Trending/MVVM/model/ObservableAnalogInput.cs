using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trending.Core;
using Trending.CoreAnalogInputRef;

namespace Trending.MVVM.Model
{
    public class ObservableAnalogInput : ObservableObject
    {
        public AnalogInput AnalogInput { get; set; }
        public ObservableCollection<AlarmWarning> Warnings { get; set; } = new ObservableCollection<AlarmWarning>();
    }
    
    public class AlarmWarning : ObservableObject
    {
        private TagAlarm _alarm;

        public TagAlarm Alarm
        {
            get { return _alarm; }
            set { _alarm = value; OnPropertyChanged(); }
        }

        private string _warningMessage;

        public string WarningMessage
        {
            get { return _warningMessage; }
            set { _warningMessage = value; OnPropertyChanged(); }
        }

    }
}
