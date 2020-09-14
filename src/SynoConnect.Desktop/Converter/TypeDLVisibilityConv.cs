using Avalonia.Data.Converters;
using System;

namespace SynoConnect.Desktop.Converter
{

    public class TypeDLVisibilityConv : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (((string)value).Contains("bt"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value)
            {
                return "bt";
            }
            else
            {
                return "http";
            }
        }
    }
}