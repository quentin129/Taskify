using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;

namespace Taskify.Modules.ToDo.Behaviors
{
    public static class ListBoxBehavior
    {
        public static bool GetClearSelectionOnEmptyClick(DependencyObject obj)
        {
            return (bool)obj.GetValue(ClearSelectionOnEmptyClickProperty);
        }

        public static void SetClearSelectionOnEmptyClick(DependencyObject obj, bool value)
        {
            obj.SetValue(ClearSelectionOnEmptyClickProperty, value);
        }

        public static readonly DependencyProperty ClearSelectionOnEmptyClickProperty =
            DependencyProperty.RegisterAttached(
                "ClearSelectionOnEmptyClick",
                typeof(bool),
                typeof(ListBoxBehavior),
                new PropertyMetadata(false, OnClearSelectionOnEmptyClickChanged));

        private static void OnClearSelectionOnEmptyClickChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ListBox listBox)
            {
                if ((bool)e.NewValue)
                {
                    listBox.PreviewMouseDown += ListBox_PreviewMouseDown;
                }
                else
                {
                    listBox.PreviewMouseDown -= ListBox_PreviewMouseDown;
                }
            }
        }

        private static void ListBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBox listBox)
            {
                if (e.OriginalSource is not Visual visual || !FindVisualParent<ListBoxItem>(visual, out _))
                {
                    listBox.SelectedItem = null; // Clear selection if click is outside an item
                }
            }
        }

        private static bool FindVisualParent<T>(DependencyObject obj, out T parent) where T : DependencyObject
        {
            parent = null;
            while (obj != null && obj is not T)
            {
                obj = VisualTreeHelper.GetParent(obj);
            }

            if (obj is T typedParent)
            {
                parent = typedParent;
                return true;
            }

            return false;
        }
    }
}