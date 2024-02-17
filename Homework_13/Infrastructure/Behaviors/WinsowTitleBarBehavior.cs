using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace Homework_13.Infrastructure.Behaviors
{
    public class WinsowTitleBarBehavior : Behavior<UIElement>
    {
        private Window _window;

        protected override void OnAttached()
        {
            //_window = AssociatedObject as Window ?? AssociatedObject.FindLogicalParent<Window>();
            _window = AssociatedObject.FindVisualRoot() as Window;
            if (_window is null) return;
            AssociatedObject.MouseLeftButtonDown += OnMouseDown;
        }        

        protected override void OnDetaching()
        {
            AssociatedObject.MouseLeftButtonDown -= OnMouseDown;
            _window = null;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            switch(e.ClickCount)
            {
                case 1:
                    DragMove();
                    break;
                default:
                    Maximize(); 
                    break;
            }
        }

        private void DragMove()
        {
            _window?.DragMove();
        }

        private void Maximize()
        {
            switch (_window.WindowState)
            {
                case WindowState.Normal:
                    _window.WindowState = WindowState.Maximized;
                    break;
                case WindowState.Maximized:
                    _window.WindowState = WindowState.Normal;
                    break;
            }
        }
    }
}
