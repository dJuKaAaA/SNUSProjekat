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
            foreach (CoreAnalogInputRef.AnalogInput input in _analogInputServiceClient.GetAll())
            {
                Inputs.Add(input);
            }
        }
    }
}
