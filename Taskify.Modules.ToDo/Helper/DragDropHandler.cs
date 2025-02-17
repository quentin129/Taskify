using GongSolutions.Wpf.DragDrop;
using MahApps.Metro.Controls;
using System;
using System.Collections;
using System.Configuration;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;
using Taskify.Modules.ToDo.ViewModels;

namespace Taskify.Modules.ToDo.Helper
{
    public class VerticalOnlyDragDropHandler : IDropTarget, IDragSource
    {
        private double _startX; // Speichert die X-Position beim Start

        // Speichert die Start-X-Position beim Drag-Start
        public void StartDrag(IDragInfo dragInfo)
        {
            if (dragInfo.VisualSourceItem is FrameworkElement element)
            {
                _startX = element.PointToScreen(new System.Windows.Point(0, 0)).X;
            }

            dragInfo.Effects = DragDropEffects.Move;
            dragInfo.Data = dragInfo.SourceItem;
        }

        private double initialX = double.NaN; // Speichert die ursprüngliche X-Koordinate

        public void DragOver(IDropInfo dropInfo)
        {
            ListBox listBox = dropInfo.VisualTarget as ListBox;

            double listBoxWidth = (double)listBox.GetValue(ListBox.ActualWidthProperty);

            double listBoxXMid = listBoxWidth / 2;

            ListBoxItem draggedListBoxItem = dropInfo.DragInfo.VisualSourceItem as ListBoxItem;

            double draggedListBoxItemXMid = draggedListBoxItem.ActualWidth / 2;

            

        }
        private void AnimateItemMove(FrameworkElement element, double oldY, double newY)
        {
            var transform = new TranslateTransform();
            element.RenderTransform = transform;

            var animation = new DoubleAnimation
            {
                From = oldY,
                To = newY,
                Duration = TimeSpan.FromMilliseconds(300),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            transform.BeginAnimation(TranslateTransform.YProperty, animation);
        }

        // Führt das Verschieben innerhalb der Liste aus
        public void Drop(IDropInfo dropInfo)
        {
            if (dropInfo.Data != null && dropInfo.TargetCollection is IList items)
            {
                var draggedItem = dropInfo.Data;
                var insertIndex = dropInfo.InsertIndex;

                if (items.Contains(draggedItem))
                {
                    items.Remove(draggedItem);
                }

                items.Insert(insertIndex, draggedItem);
            }
        }

        public bool CanStartDrag(IDragInfo dragInfo) => true;

        public void Dropped(IDropInfo dropInfo) { }

        public void DragDropOperationFinished(DragDropEffects operation) { }

        public void DragCancelled() { }

        public bool TryCatchOccurred(System.Exception exception) => false;

        public void DragDropOperationFinished(DragDropEffects operationResult, IDragInfo dragInfo)
        {
            //throw new NotImplementedException();
        }

        public bool TryCatchOccurredException(Exception exception)
        {
            //throw new NotImplementedException();
            return false;
        }
    }
}
