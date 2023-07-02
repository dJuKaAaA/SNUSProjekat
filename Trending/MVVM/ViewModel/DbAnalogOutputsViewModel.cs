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
            foreach (CoreAnalogOutputRef.AnalogOutput output in _analogOutputServiceClient.GetAll())
            {
                Outputs.Add(output);
            }
        }
    }
}
