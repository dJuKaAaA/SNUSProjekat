using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trending.Core;

namespace Trending.MVVM.ViewModel
{
    public class DbAnalogInputsViewModel : ViewModelBase
    {
        public ObservableCollection<CoreAnalogInputRef.AnalogInput> Inputs { get; set; }

        private readonly CoreAnalogInputRef.AnalogInputServiceClient _analogInputServiceClient;

        public DbAnalogInputsViewModel()
        {
            _analogInputServiceClient = new CoreAnalogInputRef.AnalogInputServiceClient();

            LoadInputs();
        }

        private void LoadInputs()
        {
            Inputs = new ObservableCollection<CoreAnalogInputRef.AnalogInput>();
            for (int i = 0; i < 10; ++i)
            {
                Inputs.Add(new CoreAnalogInputRef.AnalogInput()
                {
                    TagName = "AnalogInputTag",
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
}
