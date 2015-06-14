using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Bifrost.XAML.Converters
{
    /// <summary>
    /// Converts a double value to string with the optional string format parameter.
    /// See https://msdn.microsoft.com/en-us/library/dwhawy9k.aspx for all standard
    /// double formats.
    /// </summary>
    public class DoubleToCurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var amount = (double)value;
            var format = (string)parameter;
            var culture = System.Globalization.CultureInfo.CurrentCulture;
            culture.NumberFormat.CurrencyNegativePattern = 1;
            return amount.ToString(format == null ? "N" : format, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
