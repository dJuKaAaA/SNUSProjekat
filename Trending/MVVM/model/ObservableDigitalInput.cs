using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trending.Core;
using Trending.CoreDigitalInputRef;

namespace Trending.MVVM.Model
{
    public class ObservableDigitalInput : ObservableObject
    {
        public DigitalInput DigitalInput { get; set; }
        public ObservableCollection<CoreDigitalInputRef.DriverType> SimDrivers { get; set; } = new ObservableCollection<DriverType>()
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
            _selectedSimDriver = SimDrivers.Where(driver => driver == DigitalInput.DriverType).First();
            OnPropertyChanged(nameof(SelectedSimDriver));
        }
    }
}
