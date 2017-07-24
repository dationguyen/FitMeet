using System;
using System.Globalization;
using Xamarin.Forms;

namespace FitMeet.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value) ? Color.FromHex("#4cbea0") : Color.FromHex("#f5f5f5");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
