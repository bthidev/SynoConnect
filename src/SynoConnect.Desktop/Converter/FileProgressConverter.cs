using Avalonia.Data.Converters;
using Synology.DownloadStation.Task.Results;
using System;

namespace SynoConnect.Desktop.Converter
{

    public class FileProgressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is ITaskFileResult)
            {
                ITaskFileResult temp = (ITaskFileResult)value;
                long donload = long.Parse(temp.SizeDownloaded);
                long totalsize = long.Parse(temp.Size);
                return ((donload / totalsize) * 100).ToString();
            }
            return (-1).ToString();

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}