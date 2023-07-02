using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Trending.Core;

namespace Trending.MVVM.ViewModel
{
    public class CreateOutputViewModel : ViewModelBase
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

		private readonly CoreAnalogOutputRef.AnalogOutputServiceClient _analogOutputServiceClient;
		private readonly CoreDigitalOutputRef.DigitalOutputServiceClient _digitalOutputServiceClient;

		public ICommand CreateOutputCommand { get; }

		public CreateOutputViewModel()
        {
			DigitalTypeSelected = true;

			_analogOutputServiceClient = new CoreAnalogOutputRef.AnalogOutputServiceClient();
			_digitalOutputServiceClient = new CoreDigitalOutputRef.DigitalOutputServiceClient();

			CreateOutputCommand = new RelayCommand(OnCreateOutput, CanCreateOutput);
        }

		private void OnCreateOutput(object o)
		{

		}

		private bool CanCreateOutput(object o)
		{
			if (DigitalTypeSelected)
			{
				return !string.IsNullOrWhiteSpace(TagName) &&
					!string.IsNullOrWhiteSpace(Description) &&
					!string.IsNullOrWhiteSpace(Description) &&
					IOAddress >= 0 && IOAddress <= 100;
			}
			if (AnalogTypeSelected)
			{
				return !string.IsNullOrWhiteSpace(TagName) &&
					!string.IsNullOrWhiteSpace(Description) &&
					!string.IsNullOrWhiteSpace(Description) &&
					IOAddress >= 0 && IOAddress <= 100 &&
					LowLimit < HighLimit && 
					!string.IsNullOrWhiteSpace(Units);
			}

			return false;
		}
    }
}
