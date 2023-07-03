using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Trending.Core;

namespace Trending.MVVM.ViewModel
{
    public class DbDigitalOutputsViewModel : ViewModelBase
    {
        public ObservableCollection<CoreDigitalOutputRef.DigitalOutput> Outputs { get; set; }

        private bool _newValue;

        public bool NewValue
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

        private readonly CoreDigitalOutputRef.DigitalOutputServiceClient _digitalOutputServiceClient;

        public ICommand UpdateValueCommand { get; }

        public DbDigitalOutputsViewModel()
        {
            _digitalOutputServiceClient = new CoreDigitalOutputRef.DigitalOutputServiceClient();

            UpdateValueCommand = new RelayCommand(OnUpdateValue, o => SelectedOutput != null);

            LoadOutputs();
        }
        private void OnUpdateValue(object o)
        {
            _digitalOutputServiceClient.SetNewValue(SelectedOutput.IOAddress, NewValue);

            Outputs.Where(output => output.TagName == SelectedOutput.TagName).First().Value = NewValue;
            MessageBox.Show("Value updated successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void LoadOutputs()
        {
            Outputs = new ObservableCollection<CoreDigitalOutputRef.DigitalOutput>();
            foreach (CoreDigitalOutputRef.DigitalOutput output in _digitalOutputServiceClient.GetAll())
            {
                Outputs.Add(output);
            }
        }
    }
}
