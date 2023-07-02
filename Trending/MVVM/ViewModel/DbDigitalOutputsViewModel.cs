using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trending.Core;

namespace Trending.MVVM.ViewModel
{
    public class DbDigitalOutputsViewModel : ViewModelBase
    {
        public ObservableCollection<CoreDigitalOutputRef.DigitalOutput> Outputs { get; set; }

        private readonly CoreDigitalOutputRef.DigitalOutputServiceClient _digitalOutputServiceClient;

        public DbDigitalOutputsViewModel()
        {
            _digitalOutputServiceClient = new CoreDigitalOutputRef.DigitalOutputServiceClient();

            LoadOutputs();
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
