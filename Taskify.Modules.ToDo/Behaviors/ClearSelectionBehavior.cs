using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Taskify.Modules.ToDo.Behaviors
{
    public class ClearSelectionBehavior : Behavior<ListBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.PreviewMouseDown += AssociatedObject_PreviewMouseDown;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewMouseDown -= AssociatedObject_PreviewMouseDown;
        }

        private void AssociatedObject_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is DependencyObject sourceElement)
            {
                var listBox = AssociatedObject;
                var dockPanel = listBox?.FindName("TaskDetailsDockPanel") as DockPanel;

                // Falls das DockPanel existiert und der Klick darin passiert, dann KEIN ClearSelection
                if (dockPanel != null && dockPanel.IsAncestorOf(sourceElement))
                {
                    return; // Nichts tun -> Selektion bleibt bestehen
                }
            }

            // Auswahl löschen, falls außerhalb geklickt wurde
            AssociatedObject.SelectedItem = null;
        }
    }
}
