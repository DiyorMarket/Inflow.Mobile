using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Inflow.Mobile.Converter
{
    public class IconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value)
            {
                return "yellow_cart.png";
            }
            else
            {
                return "grey_cart.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
