using System;
using System.Globalization;
using Xamarin.Forms;

namespace FitMeet.Converters
{
    public class StringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = ((string)value).Equals((string)parameter, StringComparison.OrdinalIgnoreCase);
            return result;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
