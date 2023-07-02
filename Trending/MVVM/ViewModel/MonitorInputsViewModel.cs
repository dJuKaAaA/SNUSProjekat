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

namespace Trending.MVVM.ViewModel
{
    public class MonitorInputsViewModel : ViewModelBase
    {
        public ObservableCollection<AnalogInput> AnalogInputs { get; set; }
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
        private readonly CoreAnalogInputRef.ScanServiceClient _analogScanClient;
        private readonly CoreDigitalInputRef.ScanServiceClient _digitalScanClient;
        public InputCallback InputCallback { get; }

        public ICommand LogOutCommand { get; }

        public MonitorInputsViewModel(
            NavigationService navigationService)
        {
            DigitalTypeSelected = true;

            _navigationService = navigationService;
            _analogInputServiceClient = new AnalogInputServiceClient();
            InputCallback = new InputCallback();
            InputCallback.ValueChangeCompleted += OnValueChangeCompleted;
            InputCallback.BoolValueChangeCompleted += OnBoolValueChangeCompleted;
            InstanceContext ic = new InstanceContext(InputCallback);
            _analogScanClient = new CoreAnalogInputRef.ScanServiceClient(ic);
            _digitalScanClient = new CoreDigitalInputRef.ScanServiceClient(ic);

            LogOutCommand = new RelayCommand(OnLogOut, o => MainViewModel.SignedUser != null);

            LoadInputs();
        }

        private void OnLogOut(object o)
        {
            MainViewModel.SignedUser = null;
            _navigationService.NavigateTo<LogInViewModel>();
        }

        public void LoadInputs()
        {
            AnalogInputs = new ObservableCollection<AnalogInput>();
            AnalogInputs.Add(_analogInputServiceClient.GetForIOAddress(20));

            DigitalInputs = new ObservableCollection<DigitalInput>();

            LoadDummyData();
        }

        private void LoadDummyData()
        {
            //for (int i = 0; i < 10; ++i)
            //{
            //    // TODO: if ScanOn is true => turn on scanning here
            //    AnalogInputs.Add(new AnalogInput()
            //    {
            //        TagName = $"AnalogInputTag{i}",
            //        IOAddress = i,
            //        Description = "Some description",
            //        ScanTime = 500,
            //        OnScan = false,
            //        Value = 100,
            //        LowLimit = 0,
            //        HighLimit = 1000,
            //        Units = "cm"
            //    });
            //}

            //for (int i = 0; i < 10; ++i)
            //{
            //    // TODO: if ScanOn is true => turn on scanning here
            //    DigitalInputs.Add(new DigitalInput()
            //    {
            //        TagName = $"DigitalInputTag{i}",
            //        IOAddress = 10 + i,
            //        Description = "Some description",
            //        ScanTime = 500,
            //        OnScan = false,
            //        Value = true,
            //    });
            //}
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
            AnalogInput analogInput = AnalogInputs.Where(input => input.IOAddress == ioAddress).FirstOrDefault();
            analogInput.Value = value;
            OnPropertyChanged(nameof(analogInput.Value));
        }

        private void UpdateDigitalValue(int ioAddress, bool value)
        {
            DigitalInput digitalInput = DigitalInputs.Where(input => input.IOAddress == ioAddress).FirstOrDefault();
            digitalInput.Value = value;
            OnPropertyChanged(nameof(digitalInput.Value));
        }
    }

    public class InputCallback : ObservableObject, CoreAnalogInputRef.IScanServiceCallback
    {

        public EventHandler<ValueChangeEventArgs> ValueChangeCompleted;
        public EventHandler<BoolValueChangeEventArgs> BoolValueChangeCompleted;

        private double _value;

        public double Value
        {
            get { return _value; }
            set { _value = value; OnPropertyChanged(); }
        }

        private bool _boolValue;

        public bool BoolValue
        {
            get { return _boolValue; }
            set { _boolValue = value; OnPropertyChanged(); }
        }

        public void AnalogScanDone(int ioAddress, double value)
        {
            Value = value;
            ValueChangeCompleted?.Invoke(this, new ValueChangeEventArgs(ioAddress, value));
        }

        public void DigitalScanDone(int ioAddress, bool value)
        {
            BoolValue = value;
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
