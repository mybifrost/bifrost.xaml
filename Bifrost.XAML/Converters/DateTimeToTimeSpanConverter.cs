using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Bifrost.XAML.Converters
{
    /// <summary>
    /// Converts a DateTime object to the relevant TimeSpan.
    /// Use this to bind a TimePicker control to a DateTime property.
    /// </summary>
    public class DateTimeToTimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                DateTime date = (DateTime)value;
                return date - date.Date;
            }
            catch
            {
                return TimeSpan.MinValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
