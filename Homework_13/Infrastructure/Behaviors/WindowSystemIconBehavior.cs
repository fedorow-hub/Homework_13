
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Homework_13.Infrastructure.Extensions;
using Microsoft.Xaml.Behaviors;

namespace Homework_13.Infrastructure.Behaviors
{
    class WindowSystemIconBehavior : Behavior<Image>
    {
        protected override void OnAttached()
        {
            AssociatedObject.MouseLeftButtonDown += OnButtonDown;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseLeftButtonDown -= OnButtonDown;
        }

        private void OnButtonDown(object sender, MouseButtonEventArgs e)
        {
            var window = AssociatedObject.FindVisualRoot() as Window;
            if (window is null) return;

            e.Handled = true;

            if (e.ClickCount > 1)
                window.Close();
            else
                window.SendMessage(WM.SYSCOMMAND, SC.KEYMENU);
        }
    }
}
