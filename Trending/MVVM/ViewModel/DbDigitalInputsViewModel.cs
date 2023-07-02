using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trending.Core;

namespace Trending.MVVM.ViewModel
{
    public class DbDigitalInputsViewModel : ViewModelBase
    {
        public ObservableCollection<CoreDigitalInputRef.DigitalInput> Inputs { get; set; }

        private readonly CoreDigitalInputRef.DigitalInputServiceClient _digitalInputServiceClient;

        public DbDigitalInputsViewModel()
        {
            _digitalInputServiceClient = new CoreDigitalInputRef.DigitalInputServiceClient();

            LoadInputs();
        }

        private void LoadInputs()
        {
            Inputs = new ObservableCollection<CoreDigitalInputRef.DigitalInput>();
            foreach (CoreDigitalInputRef.DigitalInput input in _digitalInputServiceClient.GetAll())
            {
                Inputs.Add(input);
            }
        }
    }
}
