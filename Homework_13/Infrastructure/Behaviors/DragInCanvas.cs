using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Xaml.Behaviors;

namespace Homework_13.Infrastructure.Behaviors
{
    public class DragInCanvas: Behavior<UIElement>
    {
        private Point _startPoint;
        private Canvas _canvas;
        protected override void OnAttached()
        {
            AssociatedObject.MouseLeftButtonDown += OnLeftButtonDown;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseLeftButtonDown -= OnLeftButtonDown;
            AssociatedObject.MouseMove -= OnMouseMove;
            AssociatedObject.MouseUp -= OnMouseUp;
        }

        private void OnLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if((_canvas ??= VisualTreeHelper.GetParent(AssociatedObject) as Canvas) is null)
                return;
            _startPoint = e.GetPosition(AssociatedObject);
            AssociatedObject.CaptureMouse();
            AssociatedObject.MouseMove += OnMouseMove;
            AssociatedObject.MouseUp += OnMouseUp;
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            AssociatedObject.MouseMove -= OnMouseMove;
            AssociatedObject.MouseUp -= OnMouseUp;
            AssociatedObject.ReleaseMouseCapture();
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            var obj = AssociatedObject;
            var currentPos = e.GetPosition(_canvas);
            var delta = currentPos - _startPoint;

            obj.SetValue(Canvas.LeftProperty, delta.X);
            obj.SetValue(Canvas.TopProperty, delta.Y);
        }
    }
}
