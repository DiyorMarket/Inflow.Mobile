using System;
using System.Globalization;
using Xamarin.Forms;

namespace Inflow.Mobile.Converter
{
    public class QuantityInStockToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is int quantityInStock)) return Color.Transparent;

            if (quantityInStock == 0)
            {
                return Color.LightGray; // Если количество на складе = 0, то серый цвет
            }
            else
            {
                return Color.Transparent; // Иначе прозрачный цвет
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
