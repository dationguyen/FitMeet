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

	public class ViewsToColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
            int val = int.Parse((string)value);

           
			return (val > 0) ? Color.FromHex("#c2c2c2") : Color.FromHex("#5a5a5a");
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
