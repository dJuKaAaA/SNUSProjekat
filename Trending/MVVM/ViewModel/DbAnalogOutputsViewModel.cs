using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Trending.Core;

namespace Trending.MVVM.ViewModel
{
    public class DbAnalogOutputsViewModel : ViewModelBase
    {
        public ObservableCollection<CoreAnalogOutputRef.AnalogOutput> Outputs { get; set; }

        private double _newValue;

        public double NewValue
        {
            get { return _newValue; }
            set { _newValue = value; OnPropertyChanged(); }
        }

        private CoreAnalogOutputRef.AnalogOutput _selectedOutput;

        public CoreAnalogOutputRef.AnalogOutput SelectedOutput
        {
            get { return _selectedOutput; }
            set { _selectedOutput = value; OnPropertyChanged(); }
        }

        private readonly CoreAnalogOutputRef.AnalogOutputServiceClient _analogOutputServiceClient;

        public ICommand UpdateValueCommand { get; }

        public DbAnalogOutputsViewModel()
        {
            _analogOutputServiceClient = new CoreAnalogOutputRef.AnalogOutputServiceClient();

            UpdateValueCommand = new RelayCommand(OnUpdateValue, o => SelectedOutput != null);

            LoadOutputs();
        }

        private void OnUpdateValue(object o)
        {
            _analogOutputServiceClient.SetNewValue(SelectedOutput.IOAddress, NewValue);

            if (NewValue > SelectedOutput.HighLimit) NewValue = SelectedOutput.HighLimit;
            if (NewValue < SelectedOutput.LowLimit) NewValue = SelectedOutput.LowLimit;
            Outputs.Where(output => output.TagName == SelectedOutput.TagName).First().Value = NewValue;
            MessageBox.Show("Value updated successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void LoadOutputs()
        {
            Outputs = new ObservableCollection<CoreAnalogOutputRef.AnalogOutput>();
            foreach (CoreAnalogOutputRef.AnalogOutput output in _analogOutputServiceClient.GetAll())
            {
                Outputs.Add(output);
            }
        }
    }
}
