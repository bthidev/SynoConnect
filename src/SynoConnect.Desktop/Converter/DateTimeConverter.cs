using Avalonia.Data.Converters;
using System;

namespace SynoConnect.Desktop.Converter
{
    class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string temp = "";
            if (value is int)
            {
                DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                temp = epoch.AddSeconds((int)value).ToString("dd/MM/yy HH:mm");

            }

            return temp;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}