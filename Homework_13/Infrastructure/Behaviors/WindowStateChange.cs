using System.Windows;
using System.Windows.Controls;
using Homework_13.Infrastructure.Extensions;
using Microsoft.Xaml.Behaviors;

namespace Homework_13.Infrastructure.Behaviors
{
    class WindowStateChange : Behavior<Button>
    {
        protected override void OnAttached()
        {
            AssociatedObject.Click += OnButtonClick;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Click -= OnButtonClick;
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            var window = AssociatedObject.FindVisualRoot() as Window;
            if(window is null) return;

            switch (window.WindowState)
            {
                case WindowState.Normal:
                    window.WindowState = WindowState.Maximized;
                    break;
                case WindowState.Maximized:
                    window.WindowState = WindowState.Normal;
                    break;
            }
        }
    }
}
