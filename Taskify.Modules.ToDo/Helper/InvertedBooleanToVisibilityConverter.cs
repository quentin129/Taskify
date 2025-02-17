using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Taskify.Modules.ToDo.Helper
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        // Umwandlung von bool zu Visibility
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed; // Fallback, wenn der Wert nicht vom Typ bool ist
        }

        // Rückumwandlung von Visibility zu bool
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibilityValue)
            {
                return visibilityValue == Visibility.Visible;
            }
            return false; // Standardwert, wenn die Visibility nicht erkannt wird
        }
    }
}