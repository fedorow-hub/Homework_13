﻿using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using Homework_13.Infrastructure.Extensions;

namespace Homework_13.Infrastructure.Behaviors
{
    public class CloseWindow : Behavior<Button>
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
            (AssociatedObject.FindVisualRoot() as Window)?.Close();
        }
    }
}
