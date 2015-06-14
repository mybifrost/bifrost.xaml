using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Bifrost.XAML.Converters
{
    public class StringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var str = (string)value;

            Visibility returnValue = string.IsNullOrEmpty(str) ? Visibility.Collapsed : Visibility.Visible;

            return returnValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
