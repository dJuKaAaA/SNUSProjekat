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
            for (int i = 0; i < 10; ++i)
            {
                Inputs.Add(new CoreDigitalInputRef.DigitalInput()
                {
                    TagName = "DigitalInputTag",
                    IOAddress = 20,
                    Description = "Some description",
                    ScanTime = 500,
                    OnScan = false,
                    Value = true,
                });
            }
        }
    }
}
