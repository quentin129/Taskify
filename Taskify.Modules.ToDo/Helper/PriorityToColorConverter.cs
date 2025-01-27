using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using Taskify.Modules.ToDo.Models;

namespace Taskify.Modules.ToDo.Helper
{
    public class PriorityToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Priority priority)
            {
                switch (priority)
                {
                    case Priority.Low:
                        return Brushes.Green;
                    case Priority.Medium:
                        return Brushes.Orange;
                    case Priority.High:
                        return Brushes.Red;
                }
            }

            return Brushes.Gray; // Fallback-Farbe
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("ConvertBack is not implemented.");
        }
    }
}
