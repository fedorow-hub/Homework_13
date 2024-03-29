﻿using System.Windows;
using System.Windows.Controls;
using Homework_13.Infrastructure.Extensions;
using Microsoft.Xaml.Behaviors;

namespace Homework_13.Infrastructure.Behaviors
{
    class MinimizeWindow : Behavior<Button>
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
            if (window is null) return;

            window.WindowState = WindowState.Minimized;
        }
    }
}
