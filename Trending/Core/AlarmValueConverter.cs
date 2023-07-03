using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Trending.CoreAnalogInputRef;

namespace Trending.Core
{
    public class AlarmValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TagAlarm alarm)
            {
                switch (alarm.Type)
                {
                    case AlarmType.Low:
                        if (alarm.Limit > alarm.AnalogInput.Value)
                        {
                            return "WARNING";
                        }
                        break;
                    case AlarmType.High:
                        if (alarm.Limit < alarm.AnalogInput.Value)
                        {
                            return "WARNING";
                        }
                        break;
                }
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
