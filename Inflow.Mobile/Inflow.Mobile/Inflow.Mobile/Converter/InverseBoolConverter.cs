using System;
using System.Globalization;
using Xamarin.Forms;

namespace Inflow.Mobile.Converter
{
    public class InverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            return !((bool)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            return value;
        }
    }
}
