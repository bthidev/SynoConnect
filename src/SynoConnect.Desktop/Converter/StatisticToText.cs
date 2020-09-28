using Avalonia.Data.Converters;
using Synology.DownloadStation.Statistic.Results;
using System;

namespace SynoConnect.Desktop.Converter
{

    public class StatisticToText : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
                return "ul = " + ((IStatisticResult)value).EmuleSpeedUpload + " dl = " + ((IStatisticResult)value).EmuleSpeedDownload;
            else
                return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}