using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trending.Core;

namespace Trending.MVVM.ViewModel
{
    public class DbAnalogOutputsViewModel : ViewModelBase
    {
        public ObservableCollection<CoreAnalogOutputRef.AnalogOutput> Outputs { get; set; }

        private readonly CoreAnalogOutputRef.AnalogOutputServiceClient _analogOutputServiceClient;

        public DbAnalogOutputsViewModel()
        {
            _analogOutputServiceClient = new CoreAnalogOutputRef.AnalogOutputServiceClient();

            LoadOutputs();
        }

        private void LoadOutputs()
        {
            Outputs = new ObservableCollection<CoreAnalogOutputRef.AnalogOutput>();
            for (int i = 0; i < 10; ++i)
            {
                Outputs.Add(new CoreAnalogOutputRef.AnalogOutput()
                {
                    TagName = "AnalogOutputTag",
                    IOAddress = 20,
                    Description = "Some description",
                    Value = 100,
                    LowLimit = 0,
                    HighLimit = 1000,
                    Units = "cm"
                });
            }
        }
    }
}
