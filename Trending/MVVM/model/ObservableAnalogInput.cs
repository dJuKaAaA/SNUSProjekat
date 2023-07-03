using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trending.Core;
using Trending.CoreAnalogInputRef;
using Trending.MVVM.ViewModel;

namespace Trending.MVVM.Model
{
    public class ObservableAnalogInput : ObservableObject
    {
        public AnalogInput AnalogInput { get; set; }
        public ObservableCollection<AlarmWarning> Warnings { get; set; } = new ObservableCollection<AlarmWarning>();
        public ObservableCollection<DriverType> SimDrivers { get; set; } = new ObservableCollection<DriverType>()
        {
            DriverType.RealTime,
            DriverType.Sine,
            DriverType.Cosine,
            DriverType.Ramp
        };

        private DriverType _selectedSimDriver;

        public DriverType SelectedSimDriver
        {
            get { return _selectedSimDriver; }
            set { _selectedSimDriver = value; OnPropertyChanged(); ValueChanged?.Invoke(this, new EventArgs()); }
        }

        public EventHandler ValueChanged;

        public void InitSimDriverValue()
        {
            _selectedSimDriver = SimDrivers.Where(driver => driver == AnalogInput.DriverType).First();
            OnPropertyChanged(nameof(SelectedSimDriver));
        }

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
