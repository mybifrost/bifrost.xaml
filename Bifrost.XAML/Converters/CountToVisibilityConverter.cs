using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Bifrost.XAML.Converters
{
    /// <summary>
    /// Converts the count of an enumerable object to Visibility.
    /// Use this when you need to show an element if a list is (not) empty.
    /// By default a value > 0 will return Visible, set Inverted to True if you
    /// need the opposite behavior.
    /// </summary>
    public class CountToVisibilityConverter : IValueConverter
    {
        public bool Inverted { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int count = (int)value;

            Visibility returnValue = count > 0 ? Visibility.Visible : Visibility.Collapsed;

            if (Inverted)
            {
                returnValue = returnValue == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
            }

            return returnValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
