using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Bifrost.XAML.Converters
{
    /// <summary>
    /// Converts a DateTime object to string with the optional format.
    /// </summary>
    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null || !(value is DateTime)) return value.ToString();

            string format = parameter == null ? "f" : parameter.ToString();
            DateTime dateTime = (DateTime)value;

            return dateTime.ToString(format);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
