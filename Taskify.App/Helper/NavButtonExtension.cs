using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Taskify.App.Helper
{
    public static class NavButtonExtension
    {
        public static readonly DependencyProperty IsSelctedProperty = DependencyProperty.RegisterAttached("IsSelected", 
            typeof(bool), typeof(NavButtonExtension), new PropertyMetadata(false));

        public static bool GetIsSelected(Button button) => (bool)button.GetValue(IsSelctedProperty);

        public static void SetIsSelected(Button button, bool value) => button.SetValue(IsSelctedProperty, value);  
    }
}
