using System.Windows;
using System.Windows.Input;
using Homework_13.Infrastructure.Extensions;
using Microsoft.Xaml.Behaviors;

namespace Homework_13.Infrastructure.Behaviors
{
    public class WindowTitleBarBehavior : Behavior<UIElement>
    {
        private Window? _window;

        protected override void OnAttached()
        {
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
            if (_window != null)
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
