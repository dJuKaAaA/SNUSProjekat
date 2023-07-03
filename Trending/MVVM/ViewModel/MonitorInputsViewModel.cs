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
using Trending.CoreSimDriverRef;
using System.Threading;

namespace Trending.MVVM.ViewModel
{
    public class MonitorInputsViewModel : ViewModelBase
    {
        private readonly Dictionary<int, Thread> _analogSimDriverThreads = new Dictionary<int, Thread>();
        private readonly Dictionary<int, Thread> _digitalSimDriverThreads = new Dictionary<int, Thread>();

        public ObservableCollection<ObservableAnalogInput> AnalogInputs { get; set; }
        public ObservableCollection<ObservableDigitalInput> DigitalInputs { get; set; }

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
        private readonly SimDriverClient _simDriverClient;

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
            _simDriverClient = new SimDriverClient();

            _navigationService.NavigationCompleted += OnNavigationCompleted;

            LogOutCommand = new RelayCommand(OnLogOut, o => MainViewModel.SignedUser != null);

            LoadInputs();
        }

        private void OnValueChanged(object sender, EventArgs e)
        {
            if (sender is ObservableAnalogInput analogInput)
            {
                if (analogInput.AnalogInput.DriverType == analogInput.SelectedSimDriver)
                {
                    return;
                }

                CoreAnalogInputRef.DriverType previousDriverType = analogInput.AnalogInput.DriverType;
                _analogInputServiceClient.SetDriverType(analogInput.AnalogInput.IOAddress, analogInput.SelectedSimDriver);
                analogInput.AnalogInput.DriverType = analogInput.SelectedSimDriver;

                if (analogInput.AnalogInput.OnScan)
                {
                    if (previousDriverType == CoreAnalogInputRef.DriverType.RealTime)
                    {
                        _analogScanClient.EndScan(analogInput.AnalogInput.IOAddress);
                    }
                    else
                    {
                        Thread thread = _analogSimDriverThreads[analogInput.AnalogInput.IOAddress];
                        thread.Abort();
                        _analogSimDriverThreads.Remove(analogInput.AnalogInput.IOAddress);
                    }
                    StartAnalogScan(analogInput.AnalogInput.IOAddress);
                }
            }
            if (sender is ObservableDigitalInput digitalInput)
            {
                if (digitalInput.DigitalInput.DriverType == digitalInput.SelectedSimDriver)
                {
                    return;
                }

                CoreDigitalInputRef.DriverType previousDriverType = digitalInput.DigitalInput.DriverType;
                _digitalInputServiceClient.SetDriverType(digitalInput.DigitalInput.IOAddress, digitalInput.SelectedSimDriver);
                digitalInput.DigitalInput.DriverType = digitalInput.SelectedSimDriver;

                if (digitalInput.DigitalInput.OnScan)
                {
                    if (previousDriverType == CoreDigitalInputRef.DriverType.RealTime)
                    {
                        _digitalScanClient.EndScan(digitalInput.DigitalInput.IOAddress);
                    }
                    else
                    {
                        Thread thread = _analogSimDriverThreads[digitalInput.DigitalInput.IOAddress];
                        thread.Abort();
                        _analogSimDriverThreads.Remove(digitalInput.DigitalInput.IOAddress);
                    }
                    StartAnalogScan(digitalInput.DigitalInput.IOAddress);
                }
            }
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

                ObservableAnalogInput observableInput = new ObservableAnalogInput() { AnalogInput = input };
                observableInput.InitSimDriverValue();

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
                    observableInput.ValueChanged += OnValueChanged;
                    observableInput.Warnings.Add(warning);
                }
                AnalogInputs.Add(observableInput);

                if (input.OnScan)
                {
                    StartAnalogScan(input.IOAddress);
                }
            }

            DigitalInputs = new ObservableCollection<ObservableDigitalInput>();
            foreach (DigitalInput input in _digitalInputServiceClient.GetAll())
            {
                ObservableDigitalInput observableInput = new ObservableDigitalInput()
                {
                    DigitalInput = input,
                };
                observableInput.InitSimDriverValue();

                observableInput.ValueChanged += OnValueChanged;
                DigitalInputs.Add(observableInput);

                if (input.OnScan)
                {
                    StartDigitalScan(input.IOAddress);
                }
            }

        }

        public void StartAnalogScan(int ioAddress)
        {
            ObservableAnalogInput input = AnalogInputs.First(oi => oi.AnalogInput.IOAddress == ioAddress);
            if (input.SelectedSimDriver == CoreAnalogInputRef.DriverType.RealTime)
            {
                _analogScanClient.StartScan(ioAddress);
            }
            else
            {
                _analogInputServiceClient.ChangeScanStatus(ioAddress, true);

                Thread thread = new Thread(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(input.AnalogInput.ScanTime);
                        CoreSimDriverRef.DriverType driver = CoreSimDriverRef.DriverType.Sine;

                        switch (input.AnalogInput.DriverType)
                        {
                            case CoreAnalogInputRef.DriverType.Sine:
                                driver = CoreSimDriverRef.DriverType.Sine;
                                break;
                            case CoreAnalogInputRef.DriverType.Cosine:
                                driver = CoreSimDriverRef.DriverType.Cosine;
                                break;
                            case CoreAnalogInputRef.DriverType.Ramp:
                                driver = CoreSimDriverRef.DriverType.Ramp;
                                break;
                        }

                        var val = _simDriverClient.GenerateValue(driver);
                        UpdateAnalogValue(ioAddress, val);
                        _analogInputServiceClient.SetNewValue(ioAddress, val);
                    }
                });

                _analogSimDriverThreads[input.AnalogInput.IOAddress] = thread;
                thread.Start();
            }
        }

        public void EndAnalogScan(int ioAddress)
        {
            ObservableAnalogInput input = AnalogInputs.First(oi => oi.AnalogInput.IOAddress == ioAddress);
            if (input.SelectedSimDriver == CoreAnalogInputRef.DriverType.RealTime)
            {
                _analogScanClient.EndScan(ioAddress);
            }
            else
            {
                _analogInputServiceClient.ChangeScanStatus(ioAddress, false);
                Thread thread = _analogSimDriverThreads[ioAddress];
                thread.Abort();
                _analogSimDriverThreads.Remove(ioAddress);
            }
        }
        public void StartDigitalScan(int ioAddress)
        {
            _digitalScanClient.StartScan(ioAddress);
            ObservableDigitalInput input = DigitalInputs.First(oi => oi.DigitalInput.IOAddress == ioAddress);
            if (input.SelectedSimDriver == CoreDigitalInputRef.DriverType.RealTime)
            {
                _digitalScanClient.StartScan(ioAddress);
            }
            else
            {
                _digitalInputServiceClient.ChangeScanStatus(ioAddress, true);

                Thread thread = new Thread(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(input.DigitalInput.ScanTime);
                        CoreSimDriverRef.DriverType driver = CoreSimDriverRef.DriverType.Sine;

                        switch (input.DigitalInput.DriverType)
                        {
                            case CoreDigitalInputRef.DriverType.Sine:
                                driver = CoreSimDriverRef.DriverType.Sine;
                                break;
                            case CoreDigitalInputRef.DriverType.Cosine:
                                driver = CoreSimDriverRef.DriverType.Cosine;
                                break;
                            case CoreDigitalInputRef.DriverType.Ramp:
                                driver = CoreSimDriverRef.DriverType.Ramp;
                                break;
                        }

                        var val = _simDriverClient.GenerateValue(driver);
                        bool boolVal = val > 0;
                        UpdateDigitalValue(ioAddress, boolVal);
                        _digitalInputServiceClient.SetNewValue(ioAddress, boolVal);
                    }
                });

                _digitalSimDriverThreads[input.DigitalInput.IOAddress] = thread;
                thread.Start();
            }
        }

        public void EndDigitalScan(int ioAddress)
        {
            ObservableDigitalInput input = DigitalInputs.First(oi => oi.DigitalInput.IOAddress == ioAddress);
            if (input.SelectedSimDriver == CoreDigitalInputRef.DriverType.RealTime)
            {
                _digitalScanClient.EndScan(ioAddress);
            }
            else
            {
                _digitalInputServiceClient.ChangeScanStatus(ioAddress, false);
                Thread thread = _digitalSimDriverThreads[ioAddress];
                thread.Abort();
                _digitalSimDriverThreads.Remove(ioAddress);
            }
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
            ObservableDigitalInput digitalInput = DigitalInputs.Where(input => input.DigitalInput.IOAddress == ioAddress).FirstOrDefault();
            digitalInput.DigitalInput.Value = value;
            OnPropertyChanged(nameof(digitalInput.DigitalInput.Value));
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
