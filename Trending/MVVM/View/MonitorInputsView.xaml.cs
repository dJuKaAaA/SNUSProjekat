using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Trending.CoreAnalogInputRef;
using Trending.CoreDigitalInputRef;
using Trending.MVVM.ViewModel;

namespace Trending.MVVM.View
{
    /// <summary>
    /// Interaction logic for InputsView.xaml
    /// </summary>
    public partial class MonitorInputsView : UserControl
    {
        private MonitorInputsViewModel _viewModel;

        public MonitorInputsView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel = (MonitorInputsViewModel)DataContext;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                string tagName = checkBox.Tag as string;

                AnalogInput analogInput = _viewModel.AnalogInputs.Where(input => input.TagName == tagName).FirstOrDefault();
                if (analogInput != null)
                {
                    _viewModel.StartAnalogScan(analogInput.IOAddress);
                }
                else
                {
                    DigitalInput digitalInput = _viewModel.DigitalInputs.Where(input => input.TagName == tagName).FirstOrDefault();
                    if (digitalInput == null)
                    {
                        // Some error occurred
                        return;
                    }
                    _viewModel.StartDigitalScan(digitalInput.IOAddress);
                }
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                string tagName = checkBox.Tag as string;

                AnalogInput analogInput = _viewModel.AnalogInputs.Where(input => input.TagName == tagName).FirstOrDefault();
                if (analogInput != null)
                {
                    _viewModel.EndAnalogScan(analogInput.IOAddress);
                }
                else
                {
                    DigitalInput digitalInput = _viewModel.DigitalInputs.Where(input => input.TagName == tagName).FirstOrDefault();
                    if (digitalInput == null)
                    {
                        // Some error occurred
                        return;
                    }
                    _viewModel.EndDigitalScan(digitalInput.IOAddress);
                }
            }

        }
    }
}
