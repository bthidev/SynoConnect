using Avalonia.Data.Converters;
using System;

namespace SynoConnect.Desktop.Converter
{
    class NumberToStringSIConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string temp = "";
            if (value is string)
            {
                if (!string.IsNullOrEmpty((string)value))
                {
                    long number;
                    long.TryParse((string)value, out number);
                    if (number > 1000000000)
                    {
                        temp = (number / 1000000000f).ToString("F2") + " Gb";
                    }
                    else if (number > 1000000)
                    {
                        temp = (number / 1000000f).ToString("F2") + " Mb";
                    }
                    else if (number > 1000)
                    {
                        temp = (number / 1000f).ToString("F2") + " Kb";
                    }
                    else
                    {
                        temp = (string)value + " b";
                    }
                }
            }
            else if (value is int)
            {
                long number = (int)value;
                if (number > 1000000000)
                {
                    temp = (number / 1000000000f).ToString("F2") + " Gb";
                }
                else if (number > 1000000)
                {
                    temp = (number / 1000000f).ToString("F2") + " Mb";
                }
                else if (number > 1000)
                {
                    temp = (number / 1000f).ToString("F2") + " Kb";
                }
                else
                {
                    temp = ((int)number).ToString() + " b";
                }
            }

            return temp;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            float number;
            string phase = ((string)value).Split(' ')[0];
            float.TryParse(phase, out number);
            string data = (string)value;
            if (data.Contains("Gb"))
            {
                number = (number * 1000000000);
            }
            else if (data.Contains("Mb"))
            {
                number = (number * 1000000f);
            }
            else if (data.Contains("Kb"))
            {
                number = (number * 1000f);
            }

            return number;
        }
    }
}