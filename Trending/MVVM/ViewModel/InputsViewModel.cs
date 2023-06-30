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
using Trending.CoreAnalogOutputRef;

namespace Trending.MVVM.ViewModel
{
    public class InputsViewModel : ViewModelBase
    {
        public ObservableCollection<AnalogInput> AnalogInputs { get; set; }

        private CoreAnalogInputRef.AnalogInputServiceClient _analogInputServiceClient;
        private CoreAnalogOutputRef.AnalogOutputServiceClient _analogOutputServiceClient;
        private CoreAnalogInputRef.ScanServiceClient _scanClient;
        public InputCallback InputCallback { get; }

        public ICommand StartScanCommand { get; }
        public ICommand EndScanCommand { get; }

        public InputsViewModel()
        {
            _analogInputServiceClient = new CoreAnalogInputRef.AnalogInputServiceClient();
            _analogOutputServiceClient = new CoreAnalogOutputRef.AnalogOutputServiceClient();

            int ioAddress = 20;
            //_analogInputServiceClient.Save(new AnalogInput()
            //{
            //    TagName = "AnalogInput1",
            //    IOAddress = ioAddress,
            //    Description = "Some description",
            //    ScanTime = 500,
            //    OnScan = false,
            //    Value = 100,
            //    LowLimit = 0,
            //    HighLimit = 1000,
            //    Units = "cm"
            //});
            //_analogOutputServiceClient.Save(new AnalogOutput()
            //{
            //    TagName = "AnalogOutput1",
            //    IOAddress = ioAddress,
            //    Description = "Some description",
            //    Value = 100,
            //    LowLimit = 0,
            //    HighLimit = 1000,
            //    Units = "cm"
            //});

            InputCallback = new InputCallback();
            InstanceContext ic = new InstanceContext(InputCallback);
            _scanClient = new CoreAnalogInputRef.ScanServiceClient(ic);

            LoadAnalogInputs();

            StartScanCommand = new RelayCommand(o => _scanClient.StartScan(ioAddress), o => true);
            EndScanCommand = new RelayCommand(o => _scanClient.EndScan(ioAddress), o => true);
        }

        public void LoadAnalogInputs()
        {
            AnalogInputs = new ObservableCollection<AnalogInput>();
            for (int i = 0; i < 10; ++i)
            {
                AnalogInputs.Add(new AnalogInput()
                {
                    TagName = "AnalogInput1",
                    IOAddress = 20,
                    Description = "Some description",
                    ScanTime = 500,
                    OnScan = false,
                    Value = 100,
                    LowLimit = 0,
                    HighLimit = 1000,
                    Units = "cm"
                });
            }

        }
    }

    public class InputCallback : ObservableObject, CoreAnalogInputRef.IScanServiceCallback
    {
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

        public void AnalogScanDone(int ioAdress, double value)
        {
            Value = value;
        }

        public void DigitalScanDone(int ioAdress, bool value)
        {
            BoolValue = value;
        }
    }
}
