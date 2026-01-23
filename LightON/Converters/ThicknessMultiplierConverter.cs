using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace LightON_Final
{
    public class ThicknessMultiplierConverter : IValueConverter
    {
        public double Multiplier { get; set; } = 1.0;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double thickness)
            {
                double result = thickness * Multiplier;
                return new Thickness(result);
            }
            return new Thickness(100);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
