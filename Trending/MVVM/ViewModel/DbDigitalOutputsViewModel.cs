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
            for (int i = 0; i < 10; ++i)
            {
                Outputs.Add(new CoreDigitalOutputRef.DigitalOutput()
                {
                    TagName = "DigitalOutputTag",
                    IOAddress = 20,
                    Description = "Some description",
                    Value = true,
                });
            }
        }
    }
}
