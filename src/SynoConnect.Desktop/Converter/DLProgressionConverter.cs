using Avalonia.Data.Converters;
using Synology.DownloadStation.Task.Results;
using System;

namespace SynoConnect.Desktop.Converter
{

    public class DLProgressionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is ITaskResult)
            {
                ITaskResult temp = (ITaskResult)value;
                long donload;
                long totalsize;
                long.TryParse(temp.Additional.Transfer.SizeDownloaded, out donload);
                long.TryParse(temp.Size, out totalsize);
                if (totalsize > 0)
                {
                    return ((donload / totalsize) * 100).ToString();
                }
                else
                {
                    return "Invalide";
                }
            }
            return (-1).ToString();

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}