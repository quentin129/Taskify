﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Taskify.Modules.ToDo.Helper
{
    public class InvertedBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
                return boolValue ? Visibility.Collapsed : Visibility.Visible;
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is Visibility visibility) && (visibility == Visibility.Collapsed);
        }
    }
}
