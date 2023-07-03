using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Trending.Core;
using Trending.CoreAnalogInputRef;
using Trending.CoreDigitalInputRef;
using Trending.CoreAnalogOutputRef;
using Trending.MVVM.Model;

namespace Trending.MVVM.ViewModel
{
    public class MonitorInputsViewModel : ViewModelBase
    {
        public ObservableCollection<ObservableAnalogInput> AnalogInputs { get; set; }
        public ObservableCollection<DigitalInput> DigitalInputs { get; set; }

		private bool _digitalTypeSelected;

		public bool DigitalTypeSelected
		{
			get { return _digitalTypeSelected; }
			set 
			{
				_digitalTypeSelected = value; 
				OnPropertyChanged(); 
                if (_digitalTypeSelected)
                {
                    DigitalInputsVisibility = Visibility.Visible;
                    AnalogInputsVisibility = Visibility.Collapsed;
                }
                else
                {
                    DigitalInputsVisibility = Visibility.Collapsed;
                    AnalogInputsVisibility = Visibility.Visible;
                }
			}
		}

		private bool _analogTypeSelected;

		public bool AnalogTypeSelected
		{
			get { return _analogTypeSelected; }
			set { _analogTypeSelected = value; OnPropertyChanged(); }
		}

        private Visibility _digitalInputsVisibility;

        public Visibility DigitalInputsVisibility
        {
            get { return _digitalInputsVisibility; }
            set { _digitalInputsVisibility = value; OnPropertyChanged(); }
        }

        private Visibility _analogInputsVisibility;

        public Visibility AnalogInputsVisibility
        {
            get { return _analogInputsVisibility; }
            set { _analogInputsVisibility = value; OnPropertyChanged(); }
        }

        private readonly NavigationService _navigationService;
        private readonly CoreAnalogInputRef.AnalogInputServiceClient _analogInputServiceClient;
        private readonly CoreDigitalInputRef.DigitalInputServiceClient _digitalInputServiceClient;
        private readonly CoreAnalogInputRef.ScanServiceClient _analogScanClient;
        private readonly CoreDigitalInputRef.ScanServiceClient _digitalScanClient;

        public ICommand LogOutCommand { get; }

        public MonitorInputsViewModel(
            NavigationService navigationService)
        {
            DigitalTypeSelected = true;

            _navigationService = navigationService;
            _analogInputServiceClient = new AnalogInputServiceClient();
            _digitalInputServiceClient = new DigitalInputServiceClient();
            InputCallback inputCallback = new InputCallback();
            inputCallback.ValueChangeCompleted += OnValueChangeCompleted;
            inputCallback.BoolValueChangeCompleted += OnBoolValueChangeCompleted;
            InstanceContext ic = new InstanceContext(inputCallback);
            _digitalScanClient = new CoreDigitalInputRef.ScanServiceClient(ic);
            _analogScanClient = new CoreAnalogInputRef.ScanServiceClient(ic);

            _navigationService.NavigationCompleted += OnNavigationCompleted;

            LogOutCommand = new RelayCommand(OnLogOut, o => MainViewModel.SignedUser != null);

            LoadInputs();
        }

        private void OnNavigationCompleted(object sender, NavigationCompletedEventArgs e)
        {
            if (e.PreviousViewModel == GetType())
            {
                _analogScanClient.EndAllScans();
                //_digitalScanClient.EndAllScans();
                _navigationService.NavigationCompleted -= OnNavigationCompleted;
            }
        }

        private void OnLogOut(object o)
        {
            MainViewModel.SignedUser = null;
            _navigationService.NavigateTo<LogInViewModel>();
        }

        public void LoadInputs()
        {
            AnalogInputs = new ObservableCollection<ObservableAnalogInput>();
            foreach (AnalogInput input in _analogInputServiceClient.GetAll())
            {
                if (input.OnScan)
                {
                    StartAnalogScan(input.IOAddress);
                }

                ObservableAnalogInput observableInput = new ObservableAnalogInput() { AnalogInput = input };

                input.Alarms = _analogInputServiceClient.GetTagAlarms(input.TagName);
                foreach (TagAlarm alarm in input.Alarms)
                {
                    alarm.AnalogInput = input;

                    AlarmWarning warning = new AlarmWarning()
                    {
                        Alarm = alarm,
                        WarningMessage = string.Empty,
                    };
                    switch (alarm.Type)
                    {
                        case AlarmType.Low:
                            if (alarm.Limit > alarm.AnalogInput.Value)
                            {
                                warning.WarningMessage = "CAUTION";
                            }
                            break;
                        case AlarmType.High:
                            if (alarm.Limit < alarm.AnalogInput.Value)
                            {
                                warning.WarningMessage = "CAUTION";
                            }
                            break;
                    }
                    observableInput.Warnings.Add(warning);
                }
                AnalogInputs.Add(observableInput);
            }

            DigitalInputs = new ObservableCollection<DigitalInput>();
            foreach (DigitalInput input in _digitalInputServiceClient.GetAll())
            {
                if (input.OnScan)
                {
                    StartDigitalScan(input.IOAddress);
                }
                DigitalInputs.Add(input);
            }

        }

        public void StartAnalogScan(int ioAddress)
        {
            _analogScanClient.StartScan(ioAddress);
        }

        public void EndAnalogScan(int ioAddress)
        {
            _analogScanClient.EndScan(ioAddress);
        }
        public void StartDigitalScan(int ioAddress)
        {
            _digitalScanClient.StartScan(ioAddress);
        }

        public void EndDigitalScan(int ioAddress)
        {
            _digitalScanClient.EndScan(ioAddress);
        }

        private void OnValueChangeCompleted(object sender, ValueChangeEventArgs e)
        {
            UpdateAnalogValue(e.IOAddress, e.Value);
        }
        private void OnBoolValueChangeCompleted(object sender, BoolValueChangeEventArgs e)
        {
            UpdateDigitalValue(e.IOAddress, e.Value);
        }

        private void UpdateAnalogValue(int ioAddress, double value)
        {
            ObservableAnalogInput analogInput = AnalogInputs.Where(input => input.AnalogInput.IOAddress == ioAddress).FirstOrDefault();
            analogInput.AnalogInput.Value = value;
            OnPropertyChanged(nameof(analogInput.AnalogInput.Value));
            foreach (AlarmWarning warning in analogInput.Warnings)
            {
                warning.WarningMessage = string.Empty;
                switch (warning.Alarm.Type)
                {
                    case AlarmType.Low:
                        if (warning.Alarm.Limit > warning.Alarm.AnalogInput.Value)
                        {
                            warning.WarningMessage = "CAUTION";
                        }
                        break;
                    case AlarmType.High:
                        if (warning.Alarm.Limit < warning.Alarm.AnalogInput.Value)
                        {
                            warning.WarningMessage = "CAUTION";
                        }
                        break;
                }
            }
        }

        private void UpdateDigitalValue(int ioAddress, bool value)
        {
            DigitalInput digitalInput = DigitalInputs.Where(input => input.IOAddress == ioAddress).FirstOrDefault();
            digitalInput.Value = value;
            OnPropertyChanged(nameof(digitalInput.Value));
        }
    }

    public class InputCallback : CoreAnalogInputRef.IScanServiceCallback
    {

        public EventHandler<ValueChangeEventArgs> ValueChangeCompleted;
        public EventHandler<BoolValueChangeEventArgs> BoolValueChangeCompleted;

        public void AnalogScanDone(int ioAddress, double value)
        {
            ValueChangeCompleted?.Invoke(this, new ValueChangeEventArgs(ioAddress, value));
        }

        public void DigitalScanDone(int ioAddress, bool value)
        {
            BoolValueChangeCompleted?.Invoke(this, new BoolValueChangeEventArgs(ioAddress, value));
        }

    }

    public class ValueChangeEventArgs : EventArgs
    {
        public int IOAddress { get; } 
        public double Value { get; }

        public ValueChangeEventArgs(int ioAddress, double value)
        {
            IOAddress = ioAddress;
            Value = value;
        }

    }

    public class BoolValueChangeEventArgs : EventArgs
    {
        public int IOAddress { get; } 
        public bool Value { get; }

        public BoolValueChangeEventArgs(int ioAddress, bool value)
        {
            IOAddress = ioAddress;
            Value = value;
        }
    }
}
