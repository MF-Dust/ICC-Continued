using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Ink_Canvas.Converter
{
    public class IntNumberToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((double)value == 0)
            {
                return "无限制";
            }
            else
            {
                return ((double)value).ToString() + "人";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((double)value == 0)
            {
                return "无限制";
            }
            else
            {
                return ((double)value).ToString() + "人";
            }
        }
    }

    public class IntNumberToString2 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((double)value == 0)
            {
                return "自动截图";
            }
            else
            {
                return ((double)value).ToString() + "条";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((double)value == 0)
            {
                return "自动截图";
            }
            else
            {
                return ((double)value).ToString() + "条";
            }
        }
    }

    public class IsEnabledToOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool isChecked = (bool)value;
            if (isChecked == true)
            {
                return 1d;
            }
            else
            {
                return 0.35;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) { throw new NotImplementedException(); }
    }

    public class BooleanToOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool isHalfOpacity && isHalfOpacity)
            {
                return 0.5;
            }
            return 1.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class FileSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double size)
            {
                string[] sizes = { "B", "KB", "MB", "GB", "TB" };
                int order = 0;
                while (size >= 1024 && order < sizes.Length - 1)
                {
                    order++;
                    size = size / 1024;
                }
                return string.Format("{0:0.##} {1}", size, sizes[order]);
            }
            return "0 B";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
