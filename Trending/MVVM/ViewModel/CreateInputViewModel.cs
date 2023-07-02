using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Trending.Core;
using Trending.CoreAnalogInputRef;

namespace Trending.MVVM.ViewModel
{
    public class CreateInputViewModel : ViewModelBase
    {
		private bool _digitalTypeSelected;

		public bool DigitalTypeSelected
		{
			get { return _digitalTypeSelected; }
			set 
			{
				_digitalTypeSelected = value; 
				OnPropertyChanged(); 

				AnalogAttributesVisibility = _digitalTypeSelected ?
					Visibility.Collapsed : Visibility.Visible;
			}
		}

		private bool _analogTypeSelected;

		public bool AnalogTypeSelected
		{
			get { return _analogTypeSelected; }
			set { _analogTypeSelected = value; OnPropertyChanged(); }
		}

		private Visibility _analogAttributesVisibility;

		public Visibility AnalogAttributesVisibility
		{
			get { return _analogAttributesVisibility; }
			set { _analogAttributesVisibility = value; OnPropertyChanged(); }
		}

		private string _tagName;

		public string TagName
		{
			get { return _tagName; }
			set { _tagName = value; OnPropertyChanged(); }
		}

		private string _description;

		public string Description
		{
			get { return _description; }
			set { _description = value; OnPropertyChanged(); }
		}

		private int _ioAddress;

		public int IOAddress
		{
			get { return _ioAddress; }
			set { _ioAddress = value; OnPropertyChanged(); }
		}

		private int _scanTime;

		public int ScanTime
		{
			get { return _scanTime; }
			set { _scanTime = value; OnPropertyChanged(); }
		}

		private double _lowLimit;

		public double LowLimit
		{
			get { return _lowLimit; }
			set { _lowLimit = value; OnPropertyChanged(); }
		}

		private double _highLimit;

		public double HighLimit
		{
			get { return _highLimit; }
			set { _highLimit = value; OnPropertyChanged(); }
		}

		private string _units;

		public string Units
		{
			get { return _units; }
			set { _units = value; OnPropertyChanged(); }
		}

		private readonly CoreAnalogInputRef.AnalogInputServiceClient _analogInputServiceClient;
		private readonly CoreDigitalInputRef.DigitalInputServiceClient _digitalInputServiceClient;

		public ICommand CreateInputCommand { get; }

		public CreateInputViewModel()
        {
			DigitalTypeSelected = true;

			_analogInputServiceClient = new CoreAnalogInputRef.AnalogInputServiceClient();
			_digitalInputServiceClient = new CoreDigitalInputRef.DigitalInputServiceClient();

			CreateInputCommand = new RelayCommand(OnCreateInput, CanCreateInput);
        }

		private void OnCreateInput(object o)
		{
			// TODO: Catch exceptions
			if (DigitalTypeSelected)
			{
				CoreDigitalInputRef.DigitalInput input = new CoreDigitalInputRef.DigitalInput()
				{
					TagName = TagName,
					Description = Description,
					IOAddress = IOAddress,
					ScanTime = ScanTime,
					OnScan = false,
					Value = false
				};
				_digitalInputServiceClient.Create(input);
			}

			if (AnalogTypeSelected)
			{
				AnalogInput input = new AnalogInput()
				{
					TagName = TagName,
					Description = Description,
					IOAddress = IOAddress,
					ScanTime = ScanTime,
					OnScan = false,
					LowLimit = LowLimit,
					HighLimit = HighLimit,
					Units = Units,
					Value = (HighLimit + LowLimit) / 2
				};
				_analogInputServiceClient.Create(input);
			}

			MessageBox.Show("Input created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

		}

		private bool CanCreateInput(object o)
		{
			if (DigitalTypeSelected)
			{
				return !string.IsNullOrWhiteSpace(TagName) &&
					!string.IsNullOrWhiteSpace(Description) &&
					!string.IsNullOrWhiteSpace(Description) &&
					IOAddress >= 0 && IOAddress <= 100 &&
					ScanTime > 0;
			}
			if (AnalogTypeSelected)
			{
				return !string.IsNullOrWhiteSpace(TagName) &&
					!string.IsNullOrWhiteSpace(Description) &&
					!string.IsNullOrWhiteSpace(Description) &&
					IOAddress >= 0 && IOAddress <= 100 &&
					ScanTime > 0 &&
					LowLimit < HighLimit && 
					!string.IsNullOrWhiteSpace(Units);
			}

			return false;
		}

    }
}
