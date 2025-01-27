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
    public static class ClearSelectionBehavior
    {
        public static bool GetClearSelectionOnClickOutside(DependencyObject obj)
        {
            return (bool)obj.GetValue(ClearSelectionOnClickOutsideProperty);
        }

        public static void SetClearSelectionOnClickOutside(DependencyObject obj, bool value)
        {
            obj.SetValue(ClearSelectionOnClickOutsideProperty, value);
        }

        public static readonly DependencyProperty ClearSelectionOnClickOutsideProperty =
            DependencyProperty.RegisterAttached(
                "ClearSelectionOnClickOutside",
                typeof(bool),
                typeof(ClearSelectionBehavior),
                new PropertyMetadata(false, OnClearSelectionOnClickOutsideChanged));

        private static void OnClearSelectionOnClickOutsideChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ListBox listBox)
            {
                if ((bool)e.NewValue)
                {
                    listBox.Loaded += OnListBoxLoaded;
                }
                else
                {
                    listBox.Loaded -= OnListBoxLoaded;
                }
            }
        }

        private static void OnListBoxLoaded(object sender, RoutedEventArgs e)
        {
            if (sender is ListBox listBox)
            {
                var window = Window.GetWindow(listBox);
                if (window != null)
                {
                    window.PreviewMouseDown += (s, args) => OnPreviewMouseDown(listBox, args);
                }
            }
        }


        private static void OnPreviewMouseDown(ListBox listBox, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Visual visual && FindVisualParent<ListBoxItem>(visual, out _))
            {
                // Click was on a ListBoxItem, do nothing
                return;
            }

            // Click was outside ListBox, clear the selection
            listBox.SelectedItem = null;
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