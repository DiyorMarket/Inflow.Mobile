using System;
using Xamarin.Forms;

namespace Inflow.Mobile.Converter
{
    public class HeartIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value)
            {
                return "red_heart.png";
            }
            else
            {
                return "grey_heart.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
